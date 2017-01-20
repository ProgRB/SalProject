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

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for EmpFinder.xaml
    /// </summary>
    public partial class EmpFinder : Window
    {
        DataSet ds = new DataSet();
        OracleDataAdapter a;
        public EmpFinder()
        {
            InitializeComponent();
            a = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectActualEmps.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_fio", OracleDbType.Varchar2, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_per_num", OracleDbType.Varchar2, ParameterDirection.Input);
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
            if (ds.Tables.Contains("Emps"))
                ds.Tables["Emps"].Clear();
            a.SelectCommand.Parameters["p_fio"].Value = tbFIO.Text;
            a.SelectCommand.Parameters["p_per_num"].Value = tbPerNum.Text;
            a.Fill(ds, "Emps");
            if (dgEmps.ItemsSource == null)
            {
                dgEmps.ItemsSource = new DataView(ds.Tables["Emps"], "", "DATE_TRANSFER desc", DataViewRowState.CurrentRows);
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
                return string.Format("{0} {1} {2}", SelectedItem["EMP_LAST_NAME"], SelectedItem["EMP_FIRST_NAME"], SelectedItem["EMP_MIDDLE_NAME"]);
            }
        }
        public string PerNum
        {
            get
            {
                return SelectedItem["PER_NUM"].ToString();
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
