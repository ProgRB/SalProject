using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Salary.Helpers;
using System.Data;
using Salary.View;
using System.Threading;
using Oracle.DataAccess.Client;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.ComponentModel;
using LibrarySalary.Helpers;

namespace Salary.ViewModel
{
    public class PaymentReportingViewModel:NotificationObject
    {
        private AsyncCollectionList<DataRowView> _empCollection;
        private OracleDataAdapter odaSalary, odaDetail;
        private EmpReportingProvider _filterEmp;
        private bool _isUpdatable = true;
        DataSet ds;
        public PaymentReportingViewModel()
        {
            ds = new DataSet();
            _filterEmp = new EmpReportingProvider();
            _salaryFilters = new SalaryFilter();
            _salaryFilters.PropertyChanged += new PropertyChangedEventHandler(_salaryFilters_PropertyChanged);
            _filterEmp.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_filterEmp_PropertyChanged);
            _empCollection = new AsyncCollectionList<DataRowView>(_filterEmp, "WORKER_ID");
            _empCollection.CurrentItemChanged += new EventHandler(_empCollection_CurrentItemChanged);
            odaSalary = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectEmpEconSalary.sql"), Connect.CurConnect);
            odaSalary.SelectCommand.BindByName = true;
            odaSalary.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaSalary.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaSalary.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            odaSalary.SelectCommand.Parameters.Add("p_fullyearsign", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaSalary.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary.TableMappings.Add("Table", "Salary");

            odaDetail = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectEmpDetails.sql"), Connect.CurConnect);
            odaDetail.SelectCommand.BindByName = true;
            odaDetail.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            odaDetail.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaDetail.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaDetail.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            odaDetail.TableMappings.Add("Table", "Detail");

            App.CloseNotification.SubdivClosed += new CloseNotification.SubdivCloseChangedDelegate(AppNotification_SubdivClosed);
        }

        /// <summary>
        /// Закрыто ли подразделение для редактирования
        /// </summary>
        public bool IsSubdivClosed
        {
            get
            {
                if (FilterEmp.SubdivID==null)
                    return false;
                if (FilterEmp.SubdivID==0)
                    return AppDataSet.Tables["SUBDIV_FOR_CLOSE"].DefaultView.OfType<DataRowView>().Where(r=>r["APP_NAME"].ToString()=="SALARY").All(r=>r.Row.Field2<DateTime?>("DATE_CLOSING")>=FilterEmp.SelectedDate);
                else
                    return AppDataSet.Tables["SUBDIV_FOR_CLOSE"].DefaultView.OfType<DataRowView>().Where(r=>r["APP_NAME"].ToString()=="SALARY" && r.Row.Field2<Decimal?>("SUBDIV_ID")==FilterEmp.SubdivID).All(r=>r.Row.Field2<DateTime?>("DATE_CLOSING")>=FilterEmp.SelectedDate);
            }
        }

        /// <summary>
        /// Сообщение о закрытости подразделения
        /// </summary>
        public string CloseStateMessage
        {
            get
            {
                if (IsSubdivClosed)
                    return string.Format("Зарплата подразделения за {0:MMMM yyyy} закрыта", FilterEmp.SelectedDate);
                else return string.Format("Зарплата подразделения за {0:MMMM yyyy} не закрыта бухгалтерией!", FilterEmp.SelectedDate);
            }
        }

        void AppNotification_SubdivClosed(object sender, CloseNotification.AppNames appName, IEnumerable<EntityGenerator.SubdivForClose> subdivs)
        {
            if (appName == CloseNotification.AppNames.Salary)
            {
                RaisePropertyChanged(() => IsSubdivClosed);
                RaisePropertyChanged(() => CloseStateMessage);
            }
        }

        void _salaryFilters_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FilterTypePayment")
            {
                if (_salarySource != null)
                    (_salarySource.SourceCollection as DataView).RowFilter = _salaryFilters.FilterTypePayment;
            }
        }

        void _empCollection_CurrentItemChanged(object sender, EventArgs e)
        {
            UpdateCurrentTab();
            
        }

        /// <summary>
        /// Обновляем выбранную вкладку в просмотре по сотруднику
        /// </summary>
        private void UpdateCurrentTab()
        {
            if (SelectedTabIndex == 0)
                UpdateSalaryView();
            else if (SelectedTabIndex == 1)
                UpdateDetailView();
        }

        int? _selectedTabIndex = 0;

        /// <summary>
        /// Выбранный индекс вкладки просмотра
        /// </summary>
        public int? SelectedTabIndex
        {
            get
            {
                return _selectedTabIndex;
            }
            set
            {
                if (_selectedTabIndex != value)
                {
                    _selectedTabIndex = value;
                    RaisePropertyChanged(() => SelectedTabIndex);
                    UpdateCurrentTab();
                }
            }
        }

        void _filterEmp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(() => IsSubdivClosed);
            RaisePropertyChanged(() => CloseStateMessage);
            if (IsUpdatable)
                _empCollection.LoadDataAsync();
        }

        /*/// <summary>
        /// Свойство - загружается ли сейчас коллекция сотрудников
        /// </summary>
        public bool IsEmpCollectionLoading
        {
            get
            {
                return true;
            }
        }*/

        /// <summary>
        /// Коллекция загрузки данных по сотруднику
        /// </summary>
        public AsyncCollectionList<DataRowView> EmpCollection
        {
            get
            {
                return _empCollection;
            }
        }

        ListCollectionView _salarySource=null;
        /// <summary>
        /// Зарплата сотрудника
        /// </summary>
        public ListCollectionView SalarySource
        {
            get
            {
                if (GrantedRoles.CheckRole("SALARY_ECON_VIEW_EMP") && ds != null && ds.Tables.Contains("SALARY") && _salarySource == null)
                {
                    ds.Tables["SALARY"].DefaultView.RowFilter = _salaryFilters.FilterTypePayment;
                    ListCollectionView cv = new ListCollectionView(ds.Tables["Salary"].DefaultView);
                    cv.GroupDescriptions.Add(new PropertyGroupDescription("PAY_MONTH", new TruncDateConverter()));
                    cv.GroupDescriptions.Add(new PropertyGroupDescription("TYPE_PAYMENT_TYPE_ID", new PayTypeConverter()));
                    cv.GroupDescriptions.Add(new PropertyGroupDescription("CODE_PAYMENT"));
                    cv.SortDescriptions.Add(new SortDescription("PAY_MONTH", ListSortDirection.Ascending));
                    cv.SortDescriptions.Add(new SortDescription("CODE_PAYMENT", ListSortDirection.Ascending));
                    _salarySource = cv;
                }
                return _salarySource;
            }
        }

        /// <summary>
        /// Фильтр сотрудников
        /// </summary>
        public EmpReportingProvider FilterEmp
        {
            get
            {
                return _filterEmp;
            }
        }

        SalaryFilter _salaryFilters;
        private Exception _salaryLoadException;
        public SalaryFilter SalaryFilters
        {
            get
            {
                return _salaryFilters;
            }
            set
            {
                _salaryFilters = value;
            }
        }

        internal void UpdateSalaryView()
        {
            if (ds.Tables.Contains("Salary"))
            {
                ds.Tables["SALARY"].Rows.Clear();
            }
            if (EmpCollection.SelectedItem != null)
            {
                odaSalary.SelectCommand.Parameters["p_transfer_id"].Value = EmpCollection.SelectedItem["transfer_id"];
                odaSalary.SelectCommand.Parameters["p_subdiv_id"].Value = FilterEmp.SubdivID;
                odaSalary.SelectCommand.Parameters["p_date"].Value = FilterEmp.SelectedDate;
                odaSalary.SelectCommand.Parameters["p_fullyearsign"].Value = SalaryFilters.ShowFullYear;
                try
                {
                    odaSalary.Fill(ds);
                }
                catch (Exception ex)
                {
                    SalaryLoadException = ex;
                }
                if (_salarySource == null)
                    RaisePropertyChanged(() => SalarySource);
                else
                    SalarySource.Refresh();
            }
        }

        internal void UpdateDetailView()
        {
            if (ds.Tables.Contains("Detail"))
            { 
                ds.Tables["Detail"].Rows.Clear();
            }
            if (EmpCollection.SelectedItem != null)
            {
                odaDetail.SelectCommand.Parameters["p_transfer_id"].Value = EmpCollection.SelectedItem["Transfer_id"];
                odaDetail.SelectCommand.Parameters["p_date"].Value = FilterEmp.SelectedDate;
                odaDetail.SelectCommand.Parameters["p_subdiv_id"].Value = FilterEmp.SubdivID;
                try
                {
                    odaDetail.Fill(ds);
                }
                catch (Exception ex)
                {
                    SalaryLoadException = ex;
                }
                if (_detailSource == null)
                    RaisePropertyChanged(() => DetailSource);
                /*else
                    DetailSource.Ref*/
            }
        }

        /// <summary>
        /// Ошибка при загрузке данных по ЗП сотрудника
        /// </summary>
        public Exception SalaryLoadException
        {
            get
            {
                return _salaryLoadException;
            }
            set
            {
                _salaryLoadException = value;
                RaisePropertyChanged(() => SalaryLoadException);
            }
        }

        internal void UpdateEmpList(BindingGroup p)
        {
            this.IsUpdatable = false;
            p.UpdateSources();
            _empCollection.LoadDataAsync();
            this.IsUpdatable = true;
        }

        /// <summary>
        /// Требуется ли обновлять список при изменении фильтра
        /// </summary>
        public bool IsUpdatable
        {
            get
            {
                return _isUpdatable;
            }
            set
            {
                _isUpdatable = value;
            }
        }

        /// <summary>
        /// Список шифров оплат для фильтра
        /// </summary>
        public DataView PaymentFilterSource
        {
            get
            {
                return new DataView(AppDataSet.Tables["PAYMENT_TYPE"], "", "CODE_PAYMENT", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Список подразделений для фильтра
        /// </summary>
        public DataView SubdivFilterSource
        {
            get
            {
                if (_subdivSource == null)
                {
                    DataTable t = AppDataSet.Tables["ACCESS_SUBDIV"].Copy();
                    t.Columns.Add("CODE_SUBDIV_VALUE").Expression = "CODE_SUBDIV+iif(SUB_ACTUAL_SIGN=1,'','<не актуально>')";
                    _subdivSource = new DataView(t, "APP_NAME in ('SALARY_VIEW','SALARY')", "CODE_SUBDIV", DataViewRowState.CurrentRows);
                }
                return _subdivSource;
            }
        }

        DataView _detailSource;
        private DataView _subdivSource;

        /// <summary>
        /// Источник данных для нарядов
        /// </summary>
        public DataView DetailSource
        {
            get
            {
                if (_detailSource == null && ds != null && ds.Tables.Contains("Detail"))
                    _detailSource = new DataView(ds.Tables["Detail"], "", "", DataViewRowState.CurrentRows);
                    
                return _detailSource;
            }
            set
            {
                _detailSource = value;
                RaisePropertyChanged(() => DetailSource);
            }
        }
    }

    /// <summary>
    /// Класс фильтра данных и асинхронной загрузки 
    /// </summary>
    public class EmpReportingProvider: NotificationObject, IItemsProvider<DataRowView> 
    {
        private DateTime _selectedDate = DateTime.Today.Day>5? DateTime.Today.Trunc("Month"): DateTime.Today.AddMonths(-1).Trunc("Month");
        private decimal? _subdivID;
        private decimal? _paymentTypeID;
        private string _perNum;

        OracleDataAdapter odaListEmp;
        private bool is_loading = false;
        string odaQuery;
        private SynchronizationContext curSyncContext;

        public EmpReportingProvider()
        {
            curSyncContext = SynchronizationContext.Current;
            odaQuery = Queries.GetQueryWithSchema("SelectEmpListReporting.sql");
        }
        public IList<DataRowView> FetchData()
        {
            if (!GrantedRoles.CheckRole("SALARY_ECON_VIEW_EMP")) return null;
            if (is_loading)
            {
                lock (odaListEmp)
                {
                    odaListEmp.SelectCommand.Cancel();
                    is_loading = false;
                }
            }
            is_loading = true;
            string exmes;
            odaListEmp = new OracleDataAdapter(odaQuery, Connect.CurConnect);
            odaListEmp.SelectCommand.BindByName = true;
            odaListEmp.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, SubdivID, ParameterDirection.Input);
            odaListEmp.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, SelectedDate, ParameterDirection.Input);
            odaListEmp.SelectCommand.Parameters.Add("p_payment_type_id", OracleDbType.Decimal, PaymentTypeID, ParameterDirection.Input);
            odaListEmp.SelectCommand.Parameters.Add("p_per_num", OracleDbType.Varchar2, PerNum, ParameterDirection.Input);
            odaListEmp.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            DataTable t = new DataTable();
            if (odaListEmp != null)
                try
                {
                    lock (odaListEmp)
                        odaListEmp.Fill(t);
                }
                catch (RuntimeWrappedException ex)
                {
                    exmes = ex.Message;
                }
                catch (Exception ex1)
                {
                    exmes = ex1.Message;
                }
                finally
                {
                    odaListEmp.Dispose();
                    is_loading = false;
                }
            return t.DefaultView.OfType<DataRowView>().ToList();
        }

        public void CancelFetch()
        {
            if (odaListEmp!=null && odaListEmp.SelectCommand!=null)
                lock (odaListEmp.SelectCommand)
                {
                    if (odaListEmp.SelectCommand != null && is_loading)
                        odaListEmp.SelectCommand.Cancel();
                }
        }

        public SynchronizationContext GetSyncContext()
        {
            return curSyncContext;
        }

        /// <summary>
        /// Выбранная дата для фильтра
        /// </summary>
        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    RaisePropertyChanged(() => SelectedDate);
                }
            }
        }

        /// <summary>
        /// Подразделение для фильтра
        /// </summary>
        public decimal? SubdivID
        {
            get
            {
                return _subdivID;
            }
            set
            {
                if (_subdivID != value)
                {
                    _subdivID = value;
                    RaisePropertyChanged(() => SubdivID);
                }
            }
        }

        /// <summary>
        /// Шифр оплат
        /// </summary>
        public decimal? PaymentTypeID
        {
            get
            {
                return _paymentTypeID;
            }
            set
            {
                if (_paymentTypeID != value)
                {
                    _paymentTypeID = value;
                    RaisePropertyChanged(() => PaymentTypeID);
                }
            }
        }

        /// <summary>
        /// Табельный номер для фильтра
        /// </summary>
        public string PerNum
        {
            get
            {
                return _perNum;
            }
            set
            {
                if (_perNum != value)
                {
                    _perNum = value;
                    RaisePropertyChanged(() => PerNum);
                }
            }
        }

        public void IncMonth()
        {
            SelectedDate = SelectedDate.AddMonths(1);
        }
        public void DecMonth()
        {
            SelectedDate = SelectedDate.AddMonths(-1);
        }
    }

    public class SalaryFilter : NotificationObject
    {
        private bool _fullYearSign = false;
        private DataTable type_filter;

        public SalaryFilter()
        {
            type_filter = new DataTable();
            type_filter.Columns.Add("FL", typeof(bool));
            type_filter.Columns.Add("TYPE_PAYMENT_TYPE_ID", typeof(decimal));
            type_filter.Columns.Add("TYPE_PAYMENT_TYPE_NAME", typeof(string));
            type_filter.ColumnChanged += new DataColumnChangeEventHandler(type_filter_ColumnChanged);
            try
            {
                XDocument r = XDocument.Load(File.OpenRead("XmlData/SalaryXmlData.xml"));
                var types = r.Descendants("TypePayment");
                foreach( var p in types)
                {
                    type_filter.Rows.Add(p.Attribute("IsChecked").Value, p.Attribute("ID").Value, p.Attribute("Name").Value);
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }

        void type_filter_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Row != null)
                RaisePropertyChanged(() => FilterTypePayment);
        }
        /// <summary>
        /// Показывать за весь год ЗП
        /// </summary>
        public bool ShowFullYear
        {
            get
            {
                return _fullYearSign;
            }
            set
            {
                _fullYearSign = value;
                RaisePropertyChanged(() => ShowFullYear);

            }
        }

        /// <summary>
        /// Фильтрация элементов по типу выплаты
        /// </summary>
        public DataView TypePaymentFilterSource
        {
            get
            {
                return new DataView(type_filter, "", "TYPE_PAYMENT_TYPE_ID", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Фильтр по типа оплат
        /// </summary>
        public string FilterTypePayment
        {
            get
            {
                return type_filter.Rows.OfType<DataRow>().Count(t => t.Field<bool>("FL")) > 0 ?
                    string.Format("TYPE_PAYMENT_TYPE_ID in ({0})",
                                    string.Join(",", type_filter.Rows.OfType<DataRow>().
                                                Where(t => t.Field<bool>("FL")).
                                                Select(p => p.Field<decimal>("TYPE_PAYMENT_TYPE_ID"))))
                                                : string.Empty;
            }
        }
    }
}
