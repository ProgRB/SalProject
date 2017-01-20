using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Data;
using Salary.ViewModel;
using Salary.Interfaces;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Globalization;
using LibrarySalary.Helpers;
using LibrarySalary.ViewModel;
using Salary.Helpers;

namespace Salary.View
{
    public partial class Payment
    {
        DataSet ds;
        OracleDataAdapter odaTaxDiscount, odaRetention, odaAdvance;
        OracleDataAdapter odaSalary, odaSalaryDocum, odaSalaryDocumPayment;
        Dictionary<int, Action> event_dictionary = new Dictionary<int, Action>();
        OracleCommand cmd_UpdateSalaryFromTable;
        OracleCommand cmd_CalcEmpRetention, cmd_LoadSubdivTable
            , cmd_CalcEmpZoneExpAdd, cmd_CalcSubdivZoneExpAdd,
            cmd_CalcEmpAdvance, cmd_LoadEmpAdvance;

        //private DataView sal_view;

        static DependencyProperty IsEmpLoadingProperty = DependencyProperty.Register("IsEmpLoading", typeof(bool?), typeof(Payment), new PropertyMetadata(true));

        public static DependencyProperty EmpFormHeaderProperty = DependencyProperty.Register("EmpFormHeader", typeof(string), typeof(Payment), new PropertyMetadata("Зарплата сотрудников"));

        private DataTable salaryRowFilter;

        void EmpCollection_CurrentItemChanged(object sender, EventArgs e)
        {
            UpdateCurrentSalaryTab();
        }

        private void AllCaclTypePayment_Check(object sender, RoutedEventArgs e)
        {
            foreach (DataRow r in calced_type_payment.Rows)
                r["FL"] = miCheckAllCalcPayments.IsChecked;
        }

        void EmpCollection_LoadFinished(object sender, EventArgs e)
        {
            if (dgEmpList.SelectedItem != null) dgEmpList.ScrollIntoView(dgEmpList.SelectedItem);
        }

        void salaryRowFilter_RowChanged(object sender, DataRowChangeEventArgs e)
        {

            if (e.Row != null && PaymentSalaryView != null)
            {
                (PaymentSalaryView.SourceCollection as DataView).RowFilter = (salaryRowFilter.Rows.OfType<DataRow>().Count(t => t.Field<bool>("FL")) > 0 ?
                    string.Format("TYPE_PAYMENT_TYPE_ID in ({0})",
                                    string.Join(",", salaryRowFilter.Rows.OfType<DataRow>().
                                                Where(t => t.Field<bool>("FL")).
                                                Select(p => p.Field<decimal>("TYPE_PAYMENT_TYPE_ID"))))
                                                : "");
                PaymentSalaryView.Refresh();
            }
        }

        private DataTable calced_type_payment;

        private static List<string> firedByProperty = new List<string>(new string[] { "ShowFullYearSign", "ShowSubdivSalarySign", "SelectedDate", "PaymentTypeId", "PerNum", "SubdivID", "OrderNameFilter", "DegreeID", "LastName" });
        /// <summary>
        /// События изменения какого-либо фильтра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PaymentFilter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (firedByProperty.Contains(e.PropertyName))
            {
                if (e.PropertyName == "ShowFullYearSign" || e.PropertyName == "ShowSubdivSalarySign")
                    UpdateCurrentSalaryTab();
                else
                    LoadEmpListAsync();
                if (OwnerTabBase!=null)
                    OwnerTabBase.HeaderText = "Зарплата сотрудников" + (EmpFilterItempProvider.SubdivID.HasValue ? string.Format(" ({0})", EmpFilterItempProvider.CodeSubdiv) : string.Empty);
            }
        }

        /// <summary>
        /// Владелец контрола
        /// </summary>
        public ViewTabBase OwnerTabBase
        {
            get;
            set;
        }

        #region Вспомогательные данные
        /// <summary>
        /// Возвращает выбранный трансфер в списке сотрудников либо налл если не выбран сотрудник
        /// </summary>
        public Decimal? SelectedTransferID
        {
            get
            {
                if (EmpCollection.SelectedItem != null) return EmpCollection.SelectedItem.Row.Field2<Decimal?>("Transfer_id");
                else return null;
            }
        }

        /// <summary>
        /// Выбранный табельный сотрудник в списке сотрудников
        /// </summary>
        public string SelectedPerNum
        {
            get
            {
                if (EmpCollection.SelectedItem != null) return EmpCollection.SelectedItem.Row.Field2<string>("PER_NUM");
                else return null;
            }
        }

        DataRowView _selectedDocum;
        /// <summary>
        /// Выбранный документ в таблице документов начисления
        /// </summary>
        public DataRowView SelectedSalaryDocum
        {
            get
            {
                return _selectedDocum;
            }
            set
            {
                if (_selectedDocum != value)
                {
                    _selectedDocum = value;
                    OnPropertyChanged("SelectedDocum");
                }
                UpdateSalDocumPayment();
            }
        }

        private ListCollectionView PaymentSalaryView
        {
            get
            {
                return dgEmpPaySalary.ItemsSource as ListCollectionView;
            }
        }

        /// <summary>
        /// Возвращает представление для списка выбора рассчитываемых шифров оплат
        /// </summary>
        public DataView CalcedTypePaymentDependency
        {
            get
            {
                return new DataView(calced_type_payment, "", "ORDER_NUMBER", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Возвращает отмеченные типы шифров оплат для расчета зависимых шифров оплат
        /// </summary>
        public decimal[] GetCalcedTypePayment()
        {
            return CalcedTypePaymentDependency.OfType<DataRowView>().Where(p => p["TYPE_PAYMENT_TYPE_ID"] != null && p["TYPE_PAYMENT_TYPE_ID"] != DBNull.Value && (bool)p["FL"]).Select(p => p.Row.Field<Decimal>("TYPE_PAYMENT_TYPE_ID")).ToArray();
        }

        /// <summary>
        /// ОБновляет текущую вкладку, согласно вложеннныым в словарь обработчикам
        /// 
        /// </summary>
        private void UpdateCurrentSalaryTab()
        {
            if (tcSalaryTab != null && tcSalaryTab.SelectedItem != null && event_dictionary.ContainsKey((tcSalaryTab.SelectedItem as TabItem).GetHashCode()))
                event_dictionary[(tcSalaryTab.SelectedItem as TabItem).GetHashCode()].Invoke();
        }
        private void Expander_Exp(object sender, RoutedEventArgs e)
        {
            Expander ex = sender as Expander;
            ExpandStateSaver.states_exp[(ex.DataContext as CollectionViewGroup).Name.ToString()] = ex.IsExpanded;
        }

        private void Expander_Coll(object sender, RoutedEventArgs e)
        {
            try
            {
                Expander ex = sender as Expander;
                ExpandStateSaver.states_exp[(ex.DataContext as CollectionViewGroup).Name.ToString()] = ex.IsExpanded;
            }
            catch { };
        }

        public IList SelectedSalaryItems
        {
            get
            {
                return (dgEmpPaySalary != null ? dgEmpPaySalary.SelectedItems : null);
            }
        }

        /// <summary>
        /// Реализация интерфейса оповещения об изменении свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void dgEmpSalary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.OnPropertyChanged("SelectedSalaryItems");
        }

        bool _showFullYearSignField = false, _showSubdivSalarySign = false;
        public string CodeSubdiv
        {
            get
            {
                if (cbCodeSubdiv.SelectedValue != null)
                    return (cbCodeSubdiv.SelectedItem as DataRowView)["CODE_SUBDIV"].ToString();
                else return string.Empty;
            }
        }

        /// <summary>
        /// Признак показывать данные за полный год
        /// </summary>
        public bool ShowFullYearSign
        {
            get
            {
                return _showFullYearSignField;
            }
            set
            {
                _showFullYearSignField = value;
                this.OnPropertyChanged("ShowFullYearSign");
            }
        }
        /// <summary>
        /// Показывать подразделение только по выбранному в фильтре
        /// </summary>
        public bool ShowSubdivSalarySign
        {
            get
            {
                return _showSubdivSalarySign;
            }
            set
            {
                _showSubdivSalarySign = value;
                this.OnPropertyChanged("ShowSubdivSalarySign");
            }
        }
        /*public decimal? PAYMENT_TYPE_ID
        {
            get
            {
                return _payment_type_id;
            }
            set
            {
                if (_payment_type_id != value)
                {
                    _payment_type_id = value;
                    this.OnPropertyChanged("PAYMENT_TYPE_ID");
                }
            }
        }*/

        /// <summary>
        /// Загружаются ли список сотрудников
        /// </summary>
        public bool? IsEmpLoading
        {
            get
            {
                return (bool?)GetValue(IsEmpLoadingProperty);
            }
            set
            {
                SetValue(IsEmpLoadingProperty, value);
            }
        }

        private AsyncCollectionList<DataRowView> _empCollection;
        private EmpItemProvider filterEmpProvider = new EmpItemProvider();
        public AsyncCollectionList<DataRowView> EmpCollection
        {
            get
            {
                if (_empCollection == null)
                {
                    _empCollection = new AsyncCollectionList<DataRowView>(filterEmpProvider, "TRANSFER_ID");
                }
                return _empCollection;
            }
            set
            {
                _empCollection = value;
            }
        }

        public EmpItemProvider EmpFilterItempProvider
        {
            get
            {
                return filterEmpProvider;
            }
            set
            {
                filterEmpProvider = value;
            }
        }

        bool _isFormBusy = false;
        public bool IsPaymentBusy
        {
            get
            {
                return _isFormBusy;
            }
            set
            {
                _isFormBusy = value;
                OnPropertyChanged("IsPaymentBusy");
            }
        }

        /// <summary>
        /// Свойство зависимости - через него будем обновлять подразделение в заголовке вкладки
        /// </summary>
        public string EmpFormHeader
        {
            get
            {
                return (string)GetValue(EmpFormHeaderProperty);
            }
            set
            {
                SetValue(EmpFormHeaderProperty, value);
            }
        }

#endregion 

        private void PerformCustomSort(DataGridColumn column)
        {
            ListSortDirection direction = (column.SortDirection != ListSortDirection.Ascending) ?
                                ListSortDirection.Ascending : ListSortDirection.Descending;
            column.SortDirection = direction;
            ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(dgEmpList.ItemsSource);
            MySort mySort = new MySort(direction, column);
            lcv.CustomSort = mySort;
        }
    }

    /// <summary>
    /// Провайдер представляющий асинхронно данные из бд с указанными фильтрами
    /// </summary>
    public class EmpItemProvider : IItemsProvider<DataRowView>, INotifyPropertyChanged, ICustomFilter
    {
        string odaQuer;
        SynchronizationContext sc;
        decimal? subdivID, paymentTypeId;
        string perNum, _orderName;

        protected bool is_loading = false;

        OracleDataAdapter odaListEmp;
        DateTime? selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        /// <summary>
        /// Фильтр айдишники подразделения
        /// </summary>
        [OracleParameterMapping(ParameterName = "p_subdiv_id")]
        public decimal? SubdivID
        {
            get { return subdivID; }
            set
            {
                if (subdivID != value)
                {
                    subdivID = value; 
                    OnPropertyChanged("SubdivID");
                }
            }
        }

        /// <summary>
        /// Фильтр кода подразделения
        /// </summary>
        public string CodeSubdiv
        {
            get
            {
                if (SubdivID == null)
                    return "<не указано>";
                else
                    if (SubdivID == 0)
                        return "У-УАЗ";
                    else
                        return AppDataSet.Tables["SUBDIV"].Compute("Max(CODE_SUBDIV)", "SUBDIV_ID=" + SubdivID).ToString();
            }
        }

        /// <summary>
        /// Фильтр выбранной даты
        /// </summary>
        [OracleParameterMapping(ParameterName = "p_date")]
        public DateTime? SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }

        /// <summary>
        /// Фильтр табельного номера
        /// </summary>
        public string PerNum
        {
            get { return perNum; }
            set
            {
                perNum = value;
                OnPropertyChanged("PerNum");
            }
        }

        public decimal? PaymentTypeId
        {
            get { return paymentTypeId; }
            set { paymentTypeId = value; OnPropertyChanged("PaymentTypeId"); }
        }
        public EmpItemProvider()
        {
            sc = SynchronizationContext.Current;
            odaQuer = string.Format(Queries.GetQuery("SelectEmpInPeriod.sql"), Connect.SchemaApstaff, Connect.SchemaSalary);
        }

        /// <summary>
        /// Фильтр параметров  - заказ
        /// </summary>
        public string OrderNameFilter
        {
            get
            {
                return _orderName;
            }
            set
            {
                _orderName = value;
                OnPropertyChanged("OrderNameFilter");
            }
        }

        /// <summary>
        /// А так же категория фильтра
        /// </summary>
        public decimal? DegreeID
        {
            get
            {
                return _degreeID;
            }
            set
            {
                _degreeID = value;
                OnPropertyChanged("DegreeID");
            }
        }

        /// <summary>
        /// Фильтр фио сотрудника
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        IEnumerable _degreeSource = null;
        /// <summary>
        /// Источник данных для фильтра категорий
        /// </summary>
        public IEnumerable DegreeSource
        {
            get
            {
                if (_degreeSource == null)
                    _degreeSource = 
                        AppDataSet.Tables["DEGREE"].Rows.OfType<DataRow>()
                        .Select(r => new { DegreeID = r.Field2<Decimal?>("DEGREE_ID"), CodeDegree = r.Field2<string>("CODE_DEGREE") })
                        .Union( new string[]{"Все"}.Select(w=> new {DegreeID=(decimal?)null, CodeDegree=w}))
                        .OrderBy(p=>p.DegreeID);
                return _degreeSource;
            }
        }

        public IList<DataRowView> FetchData()
        {
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
            odaListEmp = new OracleDataAdapter(odaQuer, Connect.CurConnect);
            odaListEmp.SelectCommand.BindByName = true;
            odaListEmp.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, SubdivID, ParameterDirection.Input);
            odaListEmp.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, SelectedDate, ParameterDirection.Input);
            odaListEmp.SelectCommand.Parameters.Add("p_payment_type_id", OracleDbType.Decimal, PaymentTypeId, ParameterDirection.Input);
            odaListEmp.SelectCommand.Parameters.Add("p_per_num", OracleDbType.Varchar2, PerNum, ParameterDirection.Input);
            odaListEmp.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            odaListEmp.SelectCommand.Parameters.Add("p_order_name", OracleDbType.Varchar2, OrderNameFilter, ParameterDirection.Input);
            odaListEmp.SelectCommand.Parameters.Add("p_degree_id", OracleDbType.Decimal, DegreeID, ParameterDirection.Input);
            odaListEmp.SelectCommand.Parameters.Add("p_last_name", OracleDbType.Varchar2, LastName, ParameterDirection.Input);
            DataTable t = new DataTable();
            if (odaListEmp != null)
                try
                {
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
            /*if (is_loading)
            {
                try
                {
                    odaListEmp.SelectCommand.Cancel();
                }
                catch { };
            }*/
        }

        public SynchronizationContext GetSyncContext()
        {
            return sc;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private decimal? _degreeID;
        private string _lastName;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public DateTime GetDate()
        {
            return selectedDate.Value;
        }

        public DateTime GetDateBegin()
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateEnd()
        {
            throw new NotImplementedException();
        }

        public decimal? GetSubdivID()
        {
            return subdivID;
        }

        public decimal[] GetDegreeIDs()
        {
            if (_degreeID.HasValue)
                return new decimal[] { _degreeID.Value };
            else
                return null;
        }
    }


    /// <summary>
    /// Компарер класс для сравнения данных сотрудников в списке
    /// </summary>
    public class MySort : IComparer
    {
        public MySort(ListSortDirection direction, DataGridColumn column)
        {
            Direction = direction;
            Column = column;
        }

        public ListSortDirection Direction
        {
            get;
            private set;
        }

        public DataGridColumn Column
        {
            get;
            private set;
        }

        int StringCompare(string s1, string s2)
        {
            if (Direction == ListSortDirection.Ascending)
                return s1.CompareTo(s2);
            return s2.CompareTo(s1);
        }

        int IComparer.Compare(object X, object Y)
        {
            DataRowView r1 = X as DataRowView, r2 = Y as DataRowView;
            if (r1[Column.SortMemberPath] is Decimal)
                if (Direction == ListSortDirection.Ascending)
                    return decimal.Compare(r1[Column.SortMemberPath] == DBNull.Value ? 0 : (decimal)r1[Column.SortMemberPath], r2[Column.SortMemberPath] == DBNull.Value ? 0 : (decimal)r2[Column.SortMemberPath]);
                else
                    return decimal.Compare(r2[Column.SortMemberPath] == DBNull.Value ? 0 : (decimal)r2[Column.SortMemberPath], r1[Column.SortMemberPath] == DBNull.Value ? 0 : (decimal)r1[Column.SortMemberPath]);
            else
                if (Direction == ListSortDirection.Ascending)
                    return StringComparer.CurrentCulture.Compare(r1[Column.SortMemberPath] == DBNull.Value ? "" : (string)r1[Column.SortMemberPath], r2[Column.SortMemberPath] == DBNull.Value ? "" : (string)r2[Column.SortMemberPath]);
                else
                    return StringComparer.CurrentCulture.Compare(r2[Column.SortMemberPath] == DBNull.Value ? "" : (string)r2[Column.SortMemberPath], r1[Column.SortMemberPath] == DBNull.Value ? "" : (string)r1[Column.SortMemberPath]);
        }
    }

    public class DetRowTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultSalTemplate { get; set; }
        public DataTemplate DefaultRetTemplate { get; set; }
        public DataTemplate AlimonyTemplate { get; set; }
        public DataTemplate DefaultTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataRowView r = item as DataRowView;
            if (r != null)
            {
                if (r["TYPE_PAYMENT_TYPE_ID"].ToString() == "1") return DefaultSalTemplate;
                else
                    if (r["TYPE_PAYMENT_TYPE_ID"].ToString() == "9")
                        return DefaultRetTemplate;
                    else
                        return DefaultTemplate;
            }
            else return DefaultTemplate;
        }
    }

    #region Конвертеры для отображения
    /// <summary>
    /// Конвертер из айдишника типа шифра оплат в наименование шифра оплат
    /// </summary>
    public class PayTypeConverter : IValueConverter
    {
        static public Dictionary<decimal, Tuple<string, decimal, decimal>> dic_type_pay_type;
        static PayTypeConverter()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime && Connect.CurConnect != null)
            {
                try
                {
                    DataTable t = AppDataSet.Tables["TYPE_PAYMENT_TYPE"];
                    dic_type_pay_type = t.Rows.OfType<DataRow>().ToDictionary(e => e.Field2<Decimal>("TYPE_PAYMENT_TYPE_ID"),
                        e => new Tuple<string, decimal, decimal>(
                        e["TYPE_PAYMENT_TYPE_NAME"].ToString(), e.Field2<Decimal?>("PAYMENT_SIGN_SALARY") ?? 0, e.Field2<Decimal?>("TYPE_PAYMENT_SIGN") ?? 0));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.GetType() == typeof(Decimal) && dic_type_pay_type.ContainsKey((decimal)value))
                return dic_type_pay_type[(decimal)value].Item1;
            else return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class GroupRetentionConverter : IValueConverter
    {
        private static Dictionary<decimal, string> d;
        static GroupRetentionConverter()
        {
            d = new Dictionary<decimal, string>();
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                try
                {
                    DataTable t = new DataTable();
                    new OracleDataAdapter(string.Format("select TYPE_GROUP_RETENTION_ID, TYPE_GROUP_RET_NAME from {1}.TYPE_GROUP_RETENTION", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect).Fill(t);
                    for (int i = 0; i < t.Rows.Count; ++i)
                    {
                        d.Add((decimal)t.Rows[i]["TYPE_GROUP_RETENTION_ID"], t.Rows[i]["TYPE_GROUP_RET_NAME"].ToString());
                    }
                }
                catch { };
            }
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value != DBNull.Value && value.GetType() == typeof(decimal) && d.ContainsKey((decimal)value))
                return d[(decimal)value];
            else return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ExpandStateSaver : IMultiValueConverter
    {
        public static Dictionary<string, bool> states_exp = new Dictionary<string, bool>();
        public ExpandStateSaver()
        { }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] != null && values[1] != null)
            {
                string st = values[1].ToString();
                if (states_exp.ContainsKey(st))
                    return states_exp[st];
                else return false;
            }
            else return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object[] { true, null };
        }
    }

    public class GroupToSumConv : IValueConverter
    {
        public string SignField
        {
            get;
            set;
        }
        public static decimal GetSumCollection(IEnumerable<object> collection, string SignField1)
        {
            decimal k = 0;
            foreach (object t in collection)
            {
                if (t is CollectionViewGroup)
                    k += GetSumCollection((t as CollectionViewGroup).Items, SignField1);
                else
                    if (t is DataRowView && (t as DataRowView).Row.RowState != DataRowState.Deleted && (t as DataRowView).Row.RowState != DataRowState.Detached && (t as DataRowView)["SUM_SAL"] != DBNull.Value)
                    {
                        DataRowView p = t as DataRowView;
                        k +=
                            (SignField1 == "TYPE_PAYMENT_SIGN" ? PayTypeConverter.dic_type_pay_type[(decimal)p["TYPE_PAYMENT_TYPE_ID"]].Item3 : PayTypeConverter.dic_type_pay_type[(decimal)p["TYPE_PAYMENT_TYPE_ID"]].Item2) * (decimal)(t as DataRowView)["SUM_SAL"];
                    }
            }
            return k;
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return GetSumCollection((IEnumerable<object>)value, SignField);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SelectedToSumConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return GroupToSumConv.GetSumCollection((IEnumerable<object>)value, "TYPE_PAYMENT_SIGN");
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TruncDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DateTime t = (DateTime)value;
                return t.ToString("MMMM yyyy");
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ItemsToSumRowConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                IEnumerable<object> v = value as IEnumerable<object>;
                var result = from row in v.OfType<DataRowView>()
                             where row.Row.RowState != DataRowState.Deleted && row.Row.RowState != DataRowState.Detached
                             group row by row["CODE_PAYMENT"] into grp
                             select
                             new
                             {
                                 CODE_PAYMENT = grp.Key,
                                 HOURS = grp.Sum(p => p["HOURS"] == DBNull.Value ? 0 : (Decimal)p["HOURS"]),
                                 DAYS = grp.Sum(p => p["DAYS"] == DBNull.Value ? 0 : (Decimal)p["DAYS"]),
                                 SUM_SAL = grp.Sum(p => p["SUM_SAL"] == DBNull.Value ? 0 : (Decimal)p["SUM_SAL"])
                             };

                return result;
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NonZeroConverter : IValueConverter
    {
        public object TrueValue
        { get; set; }
        public object FalseValue
        {
            get;
            set;
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return (value != null && (decimal)value == 0 ? TrueValue : FalseValue);
            }
            catch { }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ImageLockConverter : IMultiValueConverter
    {
        public List<decimal> GetFields(string FieldName, IEnumerable items)
        {
            List<Decimal> arr = new List<Decimal>();
            if (items != null)
            {
                foreach (object r in items)
                    if (r is DataRowView && (r as DataRowView).Row.RowState != DataRowState.Deleted && (r as DataRowView).Row.RowState != DataRowState.Detached)
                    {
                        arr.Add((decimal)(r as DataRowView)[FieldName]);
                    }
                    else
                    {
                        if (r is CollectionViewGroup)
                            arr.AddRange(GetFields(FieldName, (r as CollectionViewGroup).Items));
                    }
                return arr;
            }
            return null;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values[0] != null && values[1] != null)
            {
                DateTime t = DateTime.ParseExact((string)values[0], "MMMM yyyy", new CultureInfo("ru-RU"));
                List<Decimal> l = GetFields("SUBDIV_ID", values[1] as IEnumerable);
                if (l.Count > 0)
                {
                    DataRow[] rt = AppDataSet.Tables["SUBDIV_FOR_CLOSE"].Select(string.Format("APP_NAME='SALARY' and DATE_CLOSING<#{1}# and SUBDIV_ID in ({0})", string.Join(",", l), t.Date.ToString("MM/dd/yyyy")));
                    return rt.Length == 0 ? true : false;
                }
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
