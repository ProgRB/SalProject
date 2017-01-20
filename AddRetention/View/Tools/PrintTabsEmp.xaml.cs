using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Salary.Helpers;
using System.Data;
using Oracle.DataAccess.Client;
using LibrarySalary.Helpers;
using Salary.Reports;

namespace Salary.View.Tools
{
    /// <summary>
    /// Interaction logic for PrintTabsEmp.xaml
    /// </summary>
    public partial class PrintTabsEmp : UserControl
    {
        private PrintEmpsViewModel _model;
        public PrintTabsEmp()
        {
            _model = new PrintEmpsViewModel();
            InitializeComponent();
            DataContext = Model;
        }

        /// <summary>
        /// Модель представления данных формы
        /// </summary>
        public PrintEmpsViewModel Model
        {
            get
            {
                return _model;
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void Print_executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.PrintReport();
        }
    }

    public class PrintEmpsViewModel : NotificationObject
    {
        OracleDataAdapter odaPrint, odaEmp;
        DataSet ds;

        private DateTime? _selectedDate;
        private decimal? _subdivID;
        private DataView _empSource;
        
        public PrintEmpsViewModel()
        {
            _selectedDate = DateTime.Today.AddMonths(-1).Trunc("Month");
            odaPrint =new OracleDataAdapter(@" begin SALARY.SelectEmpTabSalary(:p_subdiv_id, :p_date, :c, :p_per_nums);end;", Connect.CurConnect);
            odaPrint.SelectCommand.BindByName = true;
            odaPrint.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal,  null, ParameterDirection.Input);
            odaPrint.SelectCommand.Parameters.Add("p_date", OracleDbType.Date,  null, ParameterDirection.Input);
            odaPrint.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor,  ParameterDirection.Output);
            odaPrint.SelectCommand.Parameters.Add("p_per_nums", OracleDbType.Array, null,  ParameterDirection.Input).UdtTypeName="SALARY.VARCHAR_COLLECTION_TYPE";
            odaPrint.TableMappings.Add("Table", "PRINT");

            odaEmp = new OracleDataAdapter("", Connect.CurConnect);
            odaEmp.SelectCommand.BindByName = true;
            odaEmp.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            odaEmp.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaEmp.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            odaEmp.TableMappings.Add("Table", "EMP");
            ds = new DataSet();
        }

        /// <summary>
        /// Выбранная дата для печати
        /// </summary>
        public DateTime? SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged(() => SelectedDate);
            }
        }

        /// <summary>
        /// Выбранное подразделение для печати
        /// </summary>
        public decimal? SubdivID
        {
            get
            {
                return _subdivID;
            }
            set
            {
                _subdivID = value;
            }
        }

        /// <summary>
        /// Список сотрудников для печати
        /// </summary>
        public DataView EmpSource
        { 
            get
            {
                if (_empSource==null && ds != null && ds.Tables.Contains("EMP"))
                {
                    ds.Tables["EMP"].Columns.Add("FL", typeof(decimal)).DefaultValue=1m;
                    _empSource = new DataView(ds.Tables["EMP"], "", "PER_NUM", DataViewRowState.CurrentRows);
                }
                return _empSource;
            }
        }

        /// <summary>
        /// Обновление списка сотрудников
        /// </summary>
        public void UpdateEmpList()
        {
            if (ds.Tables.Contains("EMP"))
                ds.Tables["EMP"].Rows.Clear();
            try
            {
                odaEmp.SelectCommand.Parameters["p_date"].Value = SelectedDate;
                odaEmp.SelectCommand.Parameters["p_subdiv_id"].Value = SubdivID;
                odaEmp.Fill(ds);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка обновления данных");
            }
            RaisePropertyChanged(()=>EmpSource);
        }

        /// <summary>
        /// Печать отчета самого
        /// </summary>
        internal void PrintReport()
        {
            odaPrint.SelectCommand.Parameters["p_date"].Value = SelectedDate;
            odaPrint.SelectCommand.Parameters["p_subdiv_id"].Value = SubdivID;
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных",
                odaPrint, odaPrint.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Расчетные листы сотрудников",
                        "Rep_NoteAccountForEmp.rdlc", (pw.Result as DataSet).Tables[0], null);
                });
        }
    }
}
