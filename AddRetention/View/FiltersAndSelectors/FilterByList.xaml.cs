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
using System.Collections;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Salary.Interfaces;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for FilterByList.xaml
    /// </summary>
    public partial class FilterByList : Window, INotifyPropertyChanged, ICustomFilter
    {
        public FilterByList(IEnumerable<object> _itemsSource, DataGridColumn[] columns, bool selectAll=true)
        {
            _source = _itemsSource;
            InitializeComponent();
            ObservableCollection<DataGridColumn> new_columns = new ObservableCollection<DataGridColumn>(dgEmpList.Columns.Concat(columns));
            dgEmpList.Columns.Clear();
            foreach (DataGridColumn d in new_columns)
                dgEmpList.Columns.Add(d);
            dgEmpList.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(TheDataGrid_PreviewMouseLeftButtonDown);
            if (selectAll) dgEmpList.SelectAll();
            DataContext = this;
        }

        /// <summary>
        /// Вызываем контруктор для формы
        /// </summary>
        /// <param name="_itemsSource">источник данных для показа списка выбора</param>
        /// <param name="columns">Колонки которые покажутся для выбора</param>
        /// <param name="selectAll">выбрал ли все элементы в списке</param>
        /// <param name="subdivAllowed">доступен ли выбор подразделения</param>
        /// <param name="periodAllowed">доступен ли выбор периода</param>
        public FilterByList(IEnumerable<object> _itemsSource, DataGridColumn[] columns, bool selectAll, bool subdivAllowed=false, bool periodAllowed=false)
        {
            _source = _itemsSource;
            InitializeComponent();
            _isSubdivAllowed = subdivAllowed;
            _isPeriodAllowed = periodAllowed;
            ObservableCollection<DataGridColumn> new_columns = new ObservableCollection<DataGridColumn>(dgEmpList.Columns.Concat(columns));
            dgEmpList.Columns.Clear();
            foreach (DataGridColumn d in new_columns)
                dgEmpList.Columns.Add(d);
            dgEmpList.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(TheDataGrid_PreviewMouseLeftButtonDown);
            if (selectAll) dgEmpList.SelectAll();
            DataContext = this;
        }

        void TheDataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // get the DataGridRow at the clicked point
            var o = DataGridHelper.TryFindFromPoint<DataGridRow>(dgEmpList, e.GetPosition(dgEmpList));
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
            this.DialogResult = true;
            Close();
        }
        IEnumerable<object> _source;
        public IEnumerable<object> GridSource
        {
            get
            {
                return _source;
            }
        }

        decimal? _subdiv_id;
        DateTime? _dateBegin, _dateEnd;

        public decimal? SubdivID
        {
            get
            {
                return _subdiv_id;
            }
            set
            {
                _subdiv_id = value;
                OnPropertyChanged("SubdivID");
            }
        }

        /// <summary>
        /// выбранная дата начала
        /// </summary>
        public DateTime? DateBegin
        {
            get
            {
                return _dateBegin;
            }
            set
            {
                _dateBegin = value;
                OnPropertyChanged("DateBegin");
            }
        }

        /// <summary>
        /// выбранная дата начала
        /// </summary>
        public DateTime? DateEnd
        {
            get
            {
                return _dateEnd;
            }
            set
            {
                _dateEnd = value;
                OnPropertyChanged("DateEnd");
            }
        }

        public IList SelectedRows
        {
            get
            {
                return dgEmpList.SelectedItems;
            }
        }

        /// <summary>
        /// Возвращает значения свойства для выбранных записей в виде массива
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key_property"></param>
        /// <returns></returns>
        public T[] SelectedValues<T>(string key_property)
        {
            if (dgEmpList.SelectedItems != null && dgEmpList.SelectedItems.Count > 0)
            {
                return dgEmpList.SelectedItems.OfType<object>().Select(r=> (T)TypeDescriptor.GetProperties(r)[key_property].GetValue(r)).ToArray();
            }
            else
                return new T[]{};
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
        private bool _isSubdivAllowed=false;
        private bool _isPeriodAllowed=false;
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
        /// Доступно ли выбор подразделения
        /// </summary>
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

        public bool IsPeriodAllowed
        {
            get
            {
                return _isPeriodAllowed;
            }
            set
            {
                _isPeriodAllowed = value;
                OnPropertyChanged("IsPeriodAllowed");
            }
        }

        public DateTime GetDate()
        {
            return DateBegin.Value;
        }

        public DateTime GetDateBegin()
        {
            return DateBegin.Value;
        }

        public DateTime GetDateEnd()
        {
            return DateEnd.Value;
        }

        public decimal? GetSubdivID()
        {
            return SubdivID;
        }

        public decimal[] GetDegreeIDs()
        {
            throw new NotImplementedException();
        }
    }
}
