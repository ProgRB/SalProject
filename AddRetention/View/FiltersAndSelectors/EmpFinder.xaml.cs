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
using Oracle.DataAccess.Client;
using LibrarySalary.Helpers;
using Salary.Helpers;
using System.ComponentModel;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for EmpFinder.xaml
    /// </summary>
    public partial class EmpFinder : Window, INotifyPropertyChanged
    {
        DataSet ds = new DataSet();
        OracleDataAdapter a;
        private EmpFilter _filter;
        private DataView _empSource;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="findAllEmps">Искать ли сотрудников по всей базе табельных (и сторонних не работающих на заводе)</param>
        public EmpFinder(bool findAllEmps=false)
        {
            InitializeComponent();
            if (!findAllEmps)
            {
                a = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectActualEmps.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_fio", OracleDbType.Varchar2, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_per_num", OracleDbType.Varchar2, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            }
            else
            {
                a = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectActualEmpsAll.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_fio", OracleDbType.Varchar2, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_per_num", OracleDbType.Varchar2, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_inn", OracleDbType.Varchar2, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            }
            a.TableMappings.Add("Table", "Emps");
            DataContext = this;
        }

        static EmpFinder()
        {
            SelectEmp = new RoutedUICommand("Выбрать сотрудника", "SelectEmp", typeof(EmpFinder));
        }

        public static RoutedUICommand SelectEmp
        {
            get;
            set;
        }

        private void btFind_Click(object sender, RoutedEventArgs e)
        {
            Exception ex = a.TryFillWithClear(ds, Filter);
            if (ex != null)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных сотрудников");
            }
            else
            {
                if (_empSource == null)
                {
                    OnPropertyChanged("EmpSource");
                }
            }
        }

        public DataView EmpSource
        {
            get
            {
                if (_empSource==null)
                    _empSource= new DataView(ds.Tables["Emps"], "", "DATE_TRANSFER desc", DataViewRowState.CurrentRows);
                return _empSource;
            }
        }

        private void tbPerNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key== Key.Enter)
                btFind_Click(this, null);
        }
        public DataRowView SelectedItem
        {
            get
            {
                return dgEmps.SelectedItem as DataRowView;
            }
        }

        public string FullFIO
        {
            get
            {
                return string.Format("{0} {1} {2}", SelectedItem?.Row.Field2<string>("EMP_LAST_NAME"), SelectedItem?.Row.Field2<string>("EMP_FIRST_NAME"),
                    SelectedItem?.Row.Field2<string>("EMP_MIDDLE_NAME"));
            }
        }
        public string PerNum
        {
            get
            {
                return SelectedItem.Row.Field2<string>("PER_NUM");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = dgEmps.SelectedItem != null;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Фильтр для формы
        /// </summary>
        public EmpFilter Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new EmpFilter();
                return _filter;
            }
        }
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

    public class EmpFilter : NotificationObject
    {
        private string _fio;

        [OracleParameterMapping(ParameterName = "p_fio")]
        public string FIO
        {
            get
            {
                return _fio;
            }
            set
            {
                _fio = value;
                RaisePropertyChanged(() => FIO);
            }
        }
        private string _perNum;

        [OracleParameterMapping(ParameterName = "p_per_num")]
        public string PerNum
        {
            get
            {
                return _perNum;
            }
            set
            {
                _perNum = value;
                RaisePropertyChanged(() => PerNum);
            }
        }
        private string _inn;

        [OracleParameterMapping(ParameterName = "p_inn")]
        public string INN
        {
            get
            {
                return _inn;
            }
            set
            {
                _inn = value;
                RaisePropertyChanged(() => INN);
            }
        }

    }

    class NotNullCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
