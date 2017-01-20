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
using System.Data;
using System.Collections;
using System.ComponentModel;
using Salary.View;
using Oracle.DataAccess.Client;
using Salary.Helpers;
using Salary.Interfaces;
using LibrarySalary.Helpers;
using System.Linq.Expressions;
using System.Xml.Serialization;
using System.IO;

namespace Salary.ViewReporting
{
    /// <summary>
    /// Interaction logic for FilterReporting.xaml
    /// </summary>
    public partial class FilterReporting : Window, INotifyPropertyChanged, ICustomFilter
    {
        private List<DataRowView> _source;
        private decimal? subdiv_id;
        private DateTime? _date_begin;
        private DateTime? _date_end;
        public FilterReporting(List<DataRowView> _srs, decimal? _subdiv_id, DateTime selectedDate):this(_srs, _subdiv_id, new DateTime(selectedDate.Year, selectedDate.Month,1),
                new DateTime(selectedDate.Year, selectedDate.Month, DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month)))
        {
        }
        public FilterReporting(decimal? _subdiv_id, DateTime? dateBegin, DateTime? dateEnd):this(null, _subdiv_id, dateBegin.Value, dateEnd.Value)
        {
        }

        public FilterReporting(decimal? _subdiv_id, DateTime selectedDate, decimal[] type_payment_filter)
            : this(null, _subdiv_id, selectedDate.Trunc("MOnth"),  new DateTime(selectedDate.Year, selectedDate.Month, DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month)), 
            false, true, true, type_payment_filter)
        {
            AllowPaymentTypes = true;

        }

        public FilterReporting(List<DataRowView> _srs, decimal? _subdiv_id, DateTime selectedDateBegin, DateTime selectedDateEnd, bool allowEmpFilter = true, bool allowBeginSelection = true, bool allowEndSelection = true, decimal[] type_payment_filter = null)
        {
            subdiv_id = _subdiv_id;
            _source = _srs;
            if (_srs == null)
            {
                _empListVisible = false;
                _bySubdiv = true;
            }
            if (type_payment_filter != null)
                type_pay_filter = type_payment_filter;
            _date_begin = selectedDateBegin;
            _date_end = selectedDateEnd;
            InitializeComponent();
            AllowBegin = allowBeginSelection ? Visibility.Visible : Visibility.Collapsed;
            AllowEnd = allowEndSelection ? Visibility.Visible : Visibility.Collapsed;
            rbByEmpList.IsEnabled = allowEmpFilter;
            dgEmpList.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(TheDataGrid_PreviewMouseLeftButtonDown);
            this.DataContext = this;
        }

        void TheDataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // get the DataGridRow at the clicked point
            DataGrid d = sender as DataGrid;
            var o = DataGridHelper.TryFindFromPoint<DataGridRow>(d, e.GetPosition(d));
            // only handle this when Ctrl or Shift not pressed 
            ModifierKeys mods = Keyboard.PrimaryDevice.Modifiers;
            if (o != null && ((int)(mods & ModifierKeys.Control) == 0 && (int)(mods & ModifierKeys.Shift) == 0))
            {
                o.IsSelected = !o.IsSelected;
                e.Handled = true;
            }
        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            if (IsColumnSelectAllowed && SelectedColumns.Count == 0)
            {
                MessageBox.Show(this, "Выбрано ни одного столбца для формирования отчета", "Ошибка фильтра");
            }
            this.DialogResult = true;
            Close();
        }
        public List<DataRowView> GridSource
        {
            get
            {
                return _source;
            }
        }

        bool _bySubdiv = true;
        public bool BySubdivReport
        {
            get
            {
                return _bySubdiv;
            }
            set
            {
                _bySubdiv = value;
            }
        }
        bool _isSubdivAllowed = true;
        public bool IsSubdivAllowed
        {
            get
            {
                return _isSubdivAllowed;
            }
            set
            {
                _isSubdivAllowed = value;
                OnPropertyChanged("IsSubdivAllowed");
            }
        }


        /// <summary>
        /// Показывать ли список профессий для выбора
        /// </summary>
        public bool AllowCodePos
        {
            get
            {
                return _allowCodePos;
            }
            set
            {
                _allowCodePos = value;
                OnPropertyChanged("AllowCodePos");
            }
        }

        /// <summary>
        /// Шифр профессии для фильтра
        /// </summary>
        public string CodePos
        {
            get
            {
                return _codePos;
            }
            set
            {
                _codePos = value;
                OnPropertyChanged("CodePos");
            }
        }

        /// <summary>
        /// Источник для получения профессий
        /// </summary>
        public DataView PositionSource
        {
            get
            {
                if (_positionSource == null)
                {
                    LoadPositions();
                }
                return _positionSource;
            }
        }
        /// <summary>
        /// Показывать ли список сотрудников для фильтра
        /// </summary>
        public bool IsEmpListVisible
        {
            get
            {
                return _empListVisible;
            }
            set
            {
                _empListVisible = value;
                OnPropertyChanged("IsEmpListVisible");
            }
        }

        /// <summary>
        /// Выбранные сотрудники в фильтре сотрудников
        /// </summary>
        public IList SelectedRows
        {
            get
            {
                return dgEmpList.SelectedItems;
            }
        }

        public Decimal? SubdivID
        {
            get { return subdiv_id; }
            set { subdiv_id = value; OnPropertyChanged("SubdivID"); }
        }

        public DateTime? DateEnd
        {
            get { return _date_end; }
            set { _date_end = value; }
        }

        Visibility _allowBegin = Visibility.Visible;
        public Visibility AllowBegin
        {
            get
            {
                return _allowBegin;
            }
            set
            {
                _allowBegin = value;
                OnPropertyChanged("AllowBegin");
            }
        }

        Visibility _allowEnd = Visibility.Visible;
        public Visibility AllowEnd
        {
            get
            {
                return _allowEnd;
            }
            set
            {
                _allowEnd = value;
                OnPropertyChanged("AllowEnd");
            }
        }

        public DateTime? DateBegin
        {
            get
            {
                return _date_begin;
            }
            set
            {
                _date_begin = value;
                RaisePropertyChanged(()=>DateBegin);
            }
        }


        private void PerformCustomSort(DataGridColumn column)
        {
            ListSortDirection direction = (column.SortDirection != ListSortDirection.Ascending) ?
                                ListSortDirection.Ascending : ListSortDirection.Descending;
            column.SortDirection = direction;
            ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(dgEmpList.ItemsSource);
            MySort mySort = new MySort(direction, column);
            lcv.CustomSort = mySort;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private bool _empListVisible;
        private bool _allowCodePos = false;
        private DataView _positionSource;
        private  string _codePos;
        private bool _allowPaymentTypes = false;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void Check_all_Checked(object sender, RoutedEventArgs e)
        {
            if (checkAll.IsChecked == true)
                dgEmpList.SelectAll();
            else
                dgEmpList.SelectedItems.Clear();

        }

        /// <summary>
        /// Загрузка данных по профессиям
        /// </summary>
        public void LoadPositions()
        {
            OracleDataAdapter odaLoad = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectPositionsForFilter.sql"), Connect.CurConnect);
            try
            {
                DataTable t = new DataTable();
                odaLoad.Fill(t);
                _positionSource = new DataView(t, "", "CODE_POS", DataViewRowState.CurrentRows);
            }
            catch (Exception ex)
            { 

            }
        }

        /// <summary>
        /// Показывать выбор видов оплат
        /// </summary>
        public bool AllowPaymentTypes 
        {
            get
            {
                return _allowPaymentTypes;
            }

            set 
            {
                _allowPaymentTypes = value;
                RaisePropertyChanged(()=>AllowPaymentTypes);
            }
        }

        /// <summary>
        /// Считать выбранными все виды оплат
        /// </summary>
        public bool AllPaymentChecked 
        {
            get
            {
                return _allPaymentChecked;
            }

            set 
            {
                _allPaymentChecked = value;
                OnPropertyChanged("AllPaymentChecked");
            }
        }

        
        /// <summary>
        /// Выбранные коды видов оплат
        /// </summary>
        public string[] SelectedCodePayments 
        {
            get
            {
                if (AllPaymentChecked)
                    return ListPaymentSource.Select(r => r.CodePayment).ToArray();
                else
                    if (cbTypePayment.SelectedItems != null)
                        return cbTypePayment.SelectedItems.OfType<PaymentTypeChecked>().Select(r=>r.CodePayment).ToArray();
                    else return null;
            }
        }

        /// <summary>
        /// Получаем выбранные айдишники для шифров оплат
        /// </summary>
        public decimal[] SelectedPaymentIDs
        {
            get
            {
                if (AllPaymentChecked)
                    return ListPaymentSource.Select(r => r.PaymentTypeID.GetValueOrDefault()).ToArray();
                else
                    if (cbTypePayment.SelectedItems != null)
                        return cbTypePayment.SelectedItems.OfType<PaymentTypeChecked>().Select(r => r.PaymentTypeID.GetValueOrDefault()).ToArray();
                    else return null;
            }
        }


        /// <summary>
        /// Источник данных для видов оплат
        /// </summary>
        public List<PaymentTypeChecked> ListPaymentSource
        {
            get
            {
                if (_list_payment == null)
                    _list_payment = AppDataSet.Tables["PAYMENT_TYPE"].Rows.OfType<DataRow>().Where(t => type_pay_filter.Contains(Convert.ToInt32(t.Field<Decimal>("TYPE_PAYMENT_TYPE_ID"))))
                        .Select(r => new PaymentTypeChecked(r.Field<Decimal?>("PAYMENT_TYPE_ID"), r.Field<string>("CODE_PAYMENT"), false)).OrderBy(r => r.CodePayment).ToList();
                return _list_payment;
            }
        }
        List<PaymentTypeChecked> _list_payment;

        private decimal[] type_pay_filter = new decimal[]{1,2,3,4,5,6,7,8,9,10, 11, 12,13, 14,15};
        private bool _allPaymentChecked = true;
        private bool _alloGroupMaster = false;
        private string _groupMaster="*";
        private byte _countOrderSignes=4;
        private bool _countOrderIsEnabled = false;
        private bool _isCodeDegreeEnabled;

        /// <summary>
        /// Показывать ли для ввода группу мастера
        /// </summary>
        public bool AllowGroupMaster 
        {
            get
            {
                return _alloGroupMaster;
            }
            set
            {
                _alloGroupMaster = value;
                RaisePropertyChanged(() => AllowGroupMaster);
            }
        }

        /// <summary>
        /// Группа мастера для фильтра
        /// </summary>
        public string GroupMaster 
        {
            get
            {
                return _groupMaster;
            }
            set
            {
                _groupMaster = value;
                RaisePropertyChanged(()=>GroupMaster);
            }
        }

        /// <summary>
        /// Код выбранного подразделения
        /// </summary>
        public string CodeSubdiv 
        {
            get
            {
                if (SubdivSelector1 != null)
                    return SubdivSelector1.CodeSubdiv;
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Кол-во знаков в заказе для создания группировки и балансового счета
        /// </summary>
        public byte CountOrderSignes
        {
            get
            {
                return _countOrderSignes;
            }
            set
            {
                _countOrderSignes = value;
                RaisePropertyChanged(()=>CountOrderSignes);
            }
        }

        public bool IsCountOrderSignesEnabled
        {
            get
            {
                return _countOrderIsEnabled;
            }
            set
            {
                _countOrderIsEnabled = value;
                RaisePropertyChanged(()=>IsCountOrderSignesEnabled);
            }
        }

        /// <summary>
        /// Доступен ли выбор категории
        /// </summary>
        public bool IsCodeDegreeEnabled 
        {
            get
            {
                return _isCodeDegreeEnabled;
            }
            set
            {
                _isCodeDegreeEnabled = value;
                RaisePropertyChanged(()=>IsCodeDegreeEnabled);
            }
        }

        List<CheckableDegree> _degreeView;
        DataTable tableDegree = new DataTable();
        private List<ListColumnFilter> _selectionColumnSource;
        private bool _isColumnSelectAllow;
        private string _orderFilter="%";
        private bool _isFilterEnabled = false;
        /// <summary>
        /// Источник данных для списка категорий
        /// </summary>
        public List<CheckableDegree> DegreeSource
        {
            get
            {
                if (_degreeView == null)
                {
                    tableDegree = AppDataSet.Tables["DEGREE"].Copy();
                    _degreeView = tableDegree.AsEnumerable().Select(r => new CheckableDegree(r.Field2<Decimal>("DEGREE_ID"), r["CODE_DEGREE"].ToString(), r["DEGREE_NAME"].ToString())).ToList();
                    foreach (CheckableDegree r in _degreeView)
                        r.IsChecked = true;
                }
                return _degreeView;
            }
        }

        /// <summary>
        /// Выбранные категории для фильтра
        /// </summary>
        public decimal[] SelectedDegreeIDs
        {
            get
            {
                return DegreeSource.Where(r => r.IsChecked).Select(r => r.DegreeID).ToArray();
            }
            set
            {
                foreach(var p in DegreeSource)
                    p.IsChecked = false;
                foreach (decimal r in value)
                {
                    DegreeSource.Where(t => t.DegreeID == r).First().IsChecked = true;
                }
            }
        }

        public DateTime GetDate()
        {
            return this.DateEnd.Value;
        }

        public DateTime GetDateBegin()
        {
            return this.DateBegin.Value;
        }

        public DateTime GetDateEnd()
        {
            return this.DateEnd.Value;
        }

        public decimal? GetSubdivID()
        {
            return this.SubdivID;
        }
        public decimal[] GetDegreeIDs()
        {
            return this.SelectedDegreeIDs;
        }

        private void Check_allColumn_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox c = sender as CheckBox;
            foreach (ListColumnFilter r in SelectionColumnSource)
                r.IsSelected = c.IsChecked??false;
        }

        /// <summary>
        /// Источник данных для списка колонок выбора
        /// </summary>
        public List<ListColumnFilter> SelectionColumnSource
        {
            get
            {
                return _selectionColumnSource;
            }
            set
            {
                _selectionColumnSource = value;
                RaisePropertyChanged(() => SelectionColumnSource);
            }
        }

        /// <summary>
        /// Заполняет автоматически список столбцов для выбора по имени отчета формируемого
        /// </summary>
        /// <param name="NameReport"></param>
        public void FillSelectionColumn(string NameReport)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(ListColumnFilterCollection));
                var xmlRoot = AppXmlHelper.GetElements("ListColumns");
                var xmlElement =xmlRoot.Where(r => r.Attribute("Name").Value == NameReport).FirstOrDefault();
                string st = xmlElement.ToString();
                var t = (ListColumnFilterCollection) xs.Deserialize(new StringReader(st));
                SelectionColumnSource = t.Columns.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка составления списка столбцов");
            }
        }

        /// <summary>
        /// Доступен ли выбор столбцов
        /// </summary>
        public bool IsColumnSelectAllowed
        {
            get
            {
                return _isColumnSelectAllow;
            }
            set
            {
                _isColumnSelectAllow = value;
                RaisePropertyChanged(() => IsColumnSelectAllowed);
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            RaisePropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Выбранные колонки для отчета
        /// </summary>
        public List<ListColumnFilter> SelectedColumns
        {
            get
            {
                return SelectionColumnSource.Where(r => r.IsSelected).ToList();
            }
        }

        /// <summary>
        /// Фильтр на заказы по отчетам
        /// </summary>
        public string OrderFilter 
        {
            get
            {
                return _orderFilter;
            }
            set
            {
                _orderFilter = value;
                RaisePropertyChanged(() => OrderFilter);
            }
        }

        /// <summary>
        /// Доступен ли в форме выбор фильтра по заказу
        /// </summary>
        public bool IsFilterOrderVisible
        {
            get
            {
                return _isFilterEnabled;
            }
            set
            {
                _isFilterEnabled = value;
                RaisePropertyChanged(() => IsFilterOrderVisible);
            }
        }
    }

    public class CheckableDegree: NotificationObject
    {
        public string CodeDegree
        {
            get;
            set;
        }
        public string DegreeName
        {
            get;
            set;
        }
        public decimal DegreeID
        {
            get;
            set;
        }
        bool _isSelected = false;
        public bool IsChecked
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsChecked);
            }
        }
        public CheckableDegree(decimal degreeId, string code, string name)
        {
            DegreeID = degreeId;
            CodeDegree = code;
            DegreeName = name;
        }
    }

    [Serializable()]
    public class ListColumnFilter : NotificationObject
    {
        private string _columnName;
        private string _aliasName;
        private string _isSelected;

        /// <summary>
        /// Наименование колонка бд для выбора
        /// </summary>
        [XmlAttribute("ColumnName")]
        public string ColumnName
        {
            get
            {
                return _columnName;
            }
            set
            {
                _columnName = value;
                RaisePropertyChanged(() => ColumnName);
            }
        }

        /// <summary>
        /// Наименование алиаса для колонки
        /// </summary>
        [XmlAttribute("AliasName")]
        public string AliasName
        {
            get
            {
                return _aliasName;
            }
            set
            {
                _aliasName = value;
                RaisePropertyChanged(() => AliasName);
            }
        }

        /// <summary>
        /// Выбрано ли поле для формирования
        /// </summary>
        [XmlIgnore()]
        public bool IsSelected
        {
            get
            {
                return bool.Parse(_isSelected??"False");
            }
            set
            {
                _isSelected = value.ToString();
                RaisePropertyChanged(() => IsSelected);
            }
        }

        [XmlAttribute("IsSelected")]
        public string IsSelectedString
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
            }
        }
    }

    [Serializable()]
    [XmlRoot("ListColumns")]
    public class ListColumnFilterCollection
    {
        [XmlArray("Columns")]
        [XmlArrayItem("ListColumnFilter", typeof(ListColumnFilter))]
        public ListColumnFilter[] Columns
        {
            get;set;
        }
    }
}
