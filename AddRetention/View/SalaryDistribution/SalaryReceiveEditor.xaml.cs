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
using System.Windows.Shapes;
using EntityGenerator;
using System.ComponentModel;
using Oracle.DataAccess.Client;
using LibrarySalary.Helpers;
using System.Data;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for SalaryReceiveEditor.xaml
    /// </summary>
    public partial class SalaryReceiveEditor : Window
    {
        private SalaryReceiveModel _model;
        public SalaryReceiveEditor(decimal? salSubdivRecID, DateTime? selectedDate = null)
        {
            _model = new SalaryReceiveModel(salSubdivRecID, selectedDate);
            InitializeComponent();
            DataContext = Model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        /// <summary>
        /// модель для формы
        /// </summary>
        public SalaryReceiveModel Model
        {
            get
            {
                return _model;
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Model.Save())
            {
                this.DialogResult = true;
                Close();
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && string.IsNullOrEmpty(Model.Error);
        }
    }

    [System.Data.Linq.Mapping.Table(Name="SAL_SUBDIV_RECEIVE")]
    public class SalaryReceiveModel : SalSubdivReceive, IDataErrorInfo
    {
        OracleDataAdapter odaSal_Subdiv_Receive;
        DataSet ds;

        public SalaryReceiveModel(decimal? salaryReceiveID, DateTime? currentdate=null)
        {
            ds = new DataSet();
            odaSal_Subdiv_Receive = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\SelectReceiveSubdivData.sql"), Connect.CurConnect);
            odaSal_Subdiv_Receive.SelectCommand.BindByName = true;
            odaSal_Subdiv_Receive.SelectCommand.Parameters.Add("p_SAL_SUBDIV_RECEIVE_ID", OracleDbType.Decimal, salaryReceiveID, ParameterDirection.Input);
            odaSal_Subdiv_Receive.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSal_Subdiv_Receive.TableMappings.Add("Table", "SAL_SUBDIV_RECEIVE");

            #region Адаптер сохранения данных
            odaSal_Subdiv_Receive.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.SAL_SUBDIV_RECEIVE_UPDATE(:p_SAL_SUBDIV_RECEIVE_ID,:p_SUBDIV_ID,:p_ORDER_ID,:p_HOURS,:p_SUM_SAL,:p_SUBDIV_SAL,:p_RECEIVE_SUBDIV_ID,:p_REC_DATE);end;", Connect.SchemaSalary), Connect.CurConnect);            odaSal_Subdiv_Receive.InsertCommand.BindByName=true;            odaSal_Subdiv_Receive.InsertCommand.Parameters.Add("p_SAL_SUBDIV_RECEIVE_ID", OracleDbType.Decimal, 0, "SAL_SUBDIV_RECEIVE_ID").Direction = ParameterDirection.InputOutput;            odaSal_Subdiv_Receive.InsertCommand.Parameters["p_SAL_SUBDIV_RECEIVE_ID"].DbType = DbType.Decimal;            odaSal_Subdiv_Receive.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;            odaSal_Subdiv_Receive.InsertCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID").Direction = ParameterDirection.Input;            odaSal_Subdiv_Receive.InsertCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS").Direction = ParameterDirection.Input;            odaSal_Subdiv_Receive.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL").Direction = ParameterDirection.Input;            odaSal_Subdiv_Receive.InsertCommand.Parameters.Add("p_SUBDIV_SAL", OracleDbType.Decimal, 0, "SUBDIV_SAL").Direction = ParameterDirection.Input;            odaSal_Subdiv_Receive.InsertCommand.Parameters.Add("p_RECEIVE_SUBDIV_ID", OracleDbType.Decimal, 0, "RECEIVE_SUBDIV_ID").Direction = ParameterDirection.Input;            odaSal_Subdiv_Receive.InsertCommand.Parameters.Add("p_REC_DATE", OracleDbType.Date, 0, "REC_DATE").Direction = ParameterDirection.Input;	                        odaSal_Subdiv_Receive.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.SAL_SUBDIV_RECEIVE_UPDATE(:p_SAL_SUBDIV_RECEIVE_ID,:p_SUBDIV_ID,:p_ORDER_ID,:p_HOURS,:p_SUM_SAL,:p_SUBDIV_SAL,:p_RECEIVE_SUBDIV_ID,:p_REC_DATE);end;", Connect.SchemaSalary), Connect.CurConnect);            odaSal_Subdiv_Receive.UpdateCommand.BindByName=true;            odaSal_Subdiv_Receive.UpdateCommand.Parameters.Add("p_SAL_SUBDIV_RECEIVE_ID", OracleDbType.Decimal, 0, "SAL_SUBDIV_RECEIVE_ID").Direction = ParameterDirection.InputOutput;            odaSal_Subdiv_Receive.UpdateCommand.Parameters["p_SAL_SUBDIV_RECEIVE_ID"].DbType = DbType.Decimal;            odaSal_Subdiv_Receive.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;            odaSal_Subdiv_Receive.UpdateCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID").Direction = ParameterDirection.Input;            odaSal_Subdiv_Receive.UpdateCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS").Direction = ParameterDirection.Input;            odaSal_Subdiv_Receive.UpdateCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL").Direction = ParameterDirection.Input;            odaSal_Subdiv_Receive.UpdateCommand.Parameters.Add("p_SUBDIV_SAL", OracleDbType.Decimal, 0, "SUBDIV_SAL").Direction = ParameterDirection.Input;            odaSal_Subdiv_Receive.UpdateCommand.Parameters.Add("p_RECEIVE_SUBDIV_ID", OracleDbType.Decimal, 0, "RECEIVE_SUBDIV_ID").Direction = ParameterDirection.Input;            odaSal_Subdiv_Receive.UpdateCommand.Parameters.Add("p_REC_DATE", OracleDbType.Date, 0, "REC_DATE").Direction = ParameterDirection.Input;	                        odaSal_Subdiv_Receive.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.SAL_SUBDIV_RECEIVE_DELETE(:p_SAL_SUBDIV_RECEIVE_ID);end;", Connect.SchemaSalary), Connect.CurConnect);            odaSal_Subdiv_Receive.DeleteCommand.BindByName=true;            odaSal_Subdiv_Receive.DeleteCommand.Parameters.Add("p_SAL_SUBDIV_RECEIVE_ID", OracleDbType.Decimal, 0, "SAL_SUBDIV_RECEIVE_ID").Direction = ParameterDirection.InputOutput;
            #endregion

            odaSal_Subdiv_Receive.Fill(ds);
            if (ds.Tables["SAL_SUBDIV_RECEIVE"].Rows.Count == 0)
            {
                this.SetNewEntityRow(ds);
                this.RecDate = currentdate;
            }
            else this.DataRow = ds.Tables["SAL_SUBDIV_RECEIVE"].Rows[0];
        }

        /// <summary>
        /// Ошибка для всей модели
        /// </summary>
        public string Error
        {
            get 
            {
                if (this.SubdivID == null || RecDate == null || ReceiveSubdivID == null || OrderID == null)
                    return "Не заполнены все обязательные поля";
                return string.Empty;
            }
        }

        /// <summary>
        /// Ошибка для конкретного поля
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public new string this[string columnName]
        {
            get
            {
                string s = base[columnName];
                if (!string.IsNullOrEmpty(s))
                    return s;
                return string.Empty;
            }
        }

        /// <summary>
        /// Перегрузим сумму - надо будет вычислять надбавку сразу
        /// </summary>
        public new decimal? SumSal
        {
            get
            {
                return base.SumSal;
            }
            set
            {
                base.SumSal = value;
                this.SubdivSal = Math.Round((base.SumSal??0) * 0.4m, 2);
            }
        }

        /// <summary>
        /// Сохранение данных по модели
        /// </summary>
        /// <returns></returns>
        internal bool Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                odaSal_Subdiv_Receive.Update(ds.Tables["SAL_SUBDIV_RECEIVE"]);
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
                return false;
            }
        }
    }
}
