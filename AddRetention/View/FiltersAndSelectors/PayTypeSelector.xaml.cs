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
using System.Data;
using Oracle.DataAccess.Client;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for PayTypeSelector.xaml
    /// </summary>
    public partial class PayTypeSelector : Window
    {
        private static RoutedUICommand _selectCommand = new RoutedUICommand("Выбрать", "selectCommadn", typeof(PayTypeSelector));
        DataTable t = new DataTable();
        public PayTypeSelector()
        {
            InitializeComponent();
            new OracleDataAdapter(string.Format("select TYPE_PAYMENT_TYPE_ID, CODE_PAYMENT, NAME_PAYMENT, PAYMENT_TYPE_ID from {0}.payment_type", Connect.SchemaApstaff), Connect.CurConnect).Fill(t);
            t.Columns.Add("FL", typeof(decimal));
            cbTypePayment.ItemsSource = new DataView(AppDataSet.Tables["TYPE_PAYMENT_TYPE"], "", "TYPE_PAYMENT_TYPE_ID", DataViewRowState.CurrentRows);
            foreach (DataRow r in t.Rows)
                r["FL"] = 0m;
            dgPT.ItemsSource = new DataView(t, "TYPE_PAYMENT_TYPE_ID=1", "CODE_PAYMENT", DataViewRowState.CurrentRows);
        }
        public static RoutedUICommand SelectCommand
        {
            get
            {
                return _selectCommand;
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = t.GetChanges() != null;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult=true;
            this.Close();
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        public List<DataRow> SelectedPT
        {
            get
            {
                return t.Rows.Cast<DataRow>().Where(t1 => t1["FL"].ToString() == "1").ToList();
            }
        }

        private void checkAll_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkAll = (sender as CheckBox);
            foreach (DataRowView r in (dgPT.ItemsSource as DataView))
                r["FL"] = checkAll.IsChecked==true? 1m:0m;
        }

        private void cbTypePayment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTypePayment.SelectedValue != null)
                (dgPT.ItemsSource as DataView).RowFilter = string.Format("TYPE_PAYMENT_TYPE_ID={0}", cbTypePayment.SelectedValue);
        }
        
    }
}
