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
using Salary.Helpers;
using System.Data.Linq.Mapping;
using Oracle.DataAccess.Client;
using LibrarySalary.Helpers;
using System.Data;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for HistorySalaryEdit.xaml
    /// </summary>
    public partial class HistorySalaryEdit : Window
    {
        private SalaryHistoryViewModel _model;
        public HistorySalaryEdit(string per_num, DateTime? pay_date)
        {
            _model = new SalaryHistoryViewModel(per_num, pay_date);
            InitializeComponent();
            DataContext = Model;
        } 

        public SalaryHistoryViewModel Model
        {
            get
            {
                return _model;
            }
        }

        private void GroupBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Model.UpdateHistory();
            }
        }
    }

    /// <summary>
    /// Представление для данных просмотра истории
    /// </summary>
    public class SalaryHistoryViewModel: NotificationObject
    {
        OracleDataAdapter odaSalaryHistory;
        DataSet ds;

        public SalaryHistoryViewModel(string per_num, DateTime? pay_date)
        {
            ds = new DataSet();
            odaSalaryHistory = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectSalaryHistory.sql"), Connect.CurConnect);
            odaSalaryHistory.SelectCommand.BindByName = true;
            odaSalaryHistory.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, pay_date, ParameterDirection.Input);
            odaSalaryHistory.SelectCommand.Parameters.Add("p_per_num", OracleDbType.Varchar2, pay_date, ParameterDirection.Input);
            odaSalaryHistory.SelectCommand.Parameters.Add("p_code_payment", OracleDbType.Varchar2, null, ParameterDirection.Input);
            odaSalaryHistory.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalaryHistory.TableMappings.Add("Table", "SALARY_HISTORY");
            Filter = new HistoryFilter() { PerNum = per_num, SelectedDate = pay_date };
            UpdateHistory();
        }

        private List<SalaryHistoryModel> _history;

        /// <summary>
        /// Список записей в истории
        /// </summary>
        public List<SalaryHistoryModel> HistorySource
        {
            get
            {
                if (_history ==null)
                {
                    if (ds.Tables.Contains("SALARY_HISTORY"))
                        SetHistorySource();
                }
                return _history;
            }
        }

        private void SetHistorySource()
        {
            _history = ds.Tables["SALARY_HISTORY"].Rows.OfType<DataRow>().Select(r=>new SalaryHistoryModel(){DataRow = r}).ToList();
            RaisePropertyChanged(() => HistorySource);
        }

        /// <summary>
        /// Обновление данных по истории редактирования
        /// </summary>
        public void UpdateHistory()
        {
            Exception ex= odaSalaryHistory.TryFillWithClear(ds, Filter);
            if (ex != null)
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных по истории");
            else
                SetHistorySource();
        }

        public HistoryFilter Filter
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Модель записи истории
    /// </summary>
    public partial class SalaryHistoryModel: SalaryHistory
    {
        /// <summary>
        /// Код подразделения
        /// </summary>
        public string CodeSubdiv
        {
            get
            {
                return this.SubdivID == null ? string.Empty : AppDictionaries.SubdivIDToValue[SubdivID.Value].CodeSubdiv;
            }
        }

        /// <summary>
        /// Код вида оплат
        /// </summary>
        public string CodePayment
        {
            get
            {
                return this.PaymentTypeID == null ? string.Empty : AppDictionaries.CodePaymentIDToValue[PaymentTypeID.Value].CodePayment;
            }
        }

        /// <summary>
        /// Код заказа готовый
        /// </summary>
        [Column(Name="ORDER_NAME")]
        public string OrderName
        {
            get
            {
                return this.GetDataRowField<string>(() => OrderName);
            }
        }

        /// <summary>
        /// Табельный номер
        /// </summary>
        [Column(Name = "PER_NUM")]
        public string PerNum
        {
            get
            {
                return this.GetDataRowField<string>(() => PerNum);
            }
        }

        /// <summary>
        /// Категория
        /// </summary>
        [Column(Name="CODE_DEGREE")]
        public string CodeDegree
        {
            get
            {
                return this.GetDataRowField<string>(() => CodeDegree);
            }
        }

        /// <summary>
        /// Порядковый номер сортировки данных
        /// </summary>
        [Column(Name = "RN")]
        public decimal? RowNum
        {
            get
            {
                return this.GetDataRowField<decimal?>(() => RowNum);
            }
        }

    }

    /// <summary>
    /// Фильтр для истории
    /// </summary>
    public class HistoryFilter: NotificationObject
    {
        [OracleParameterMapping(ParameterName="p_per_num")]
        public string PerNum
        {
            get;set;
        }

        [OracleParameterMapping(ParameterName = "p_date")]
        public DateTime? SelectedDate
        {
            get;set;
        }

        [OracleParameterMapping(ParameterName = "p_code_payment")]
        public string CodePayment
        {
            get;set;
        }
    }
}
