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

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for PaymentTypeSelector.xaml
    /// </summary>
    public partial class PaymentTypeSelector : Window
    {
        public static RoutedUICommand _acceptCommand = new RoutedUICommand("Выбрать и продолжить", "AcceptCommand", typeof(PaymentTypeSelector));
        private DataTable t = new DataTable();
        public static RoutedUICommand AcceptCommand
        {
            get
            {
                return _acceptCommand;
            }
        }
        public PaymentTypeSelector()
        {
            InitializeComponent();
            AppDataSet.UpdateSet("PAYMENT_TYPE");
            AppDataSet.UpdateSet("TYPE_PAYMENT_TYPE");
            cbTypePaymentType.ItemsSource = new DataView(AppDataSet.Tables["TYPE_PAYMENT_TYPE"], "", "", DataViewRowState.CurrentRows);
            t.Columns.Add("FL", typeof(Boolean));
            t.Columns.Add("PAYMENT_TYPE_ID", AppDataSet.Tables["PAYMENT_TYPE"].Columns["PAYMENT_TYPE_ID"].GetType());
            t.Columns.Add("CODE_PAYMENT", AppDataSet.Tables["PAYMENT_TYPE"].Columns["CODE_PAYMENT"].GetType());
            t.Columns.Add("NAME_PAYMENT", AppDataSet.Tables["PAYMENT_TYPE"].Columns["NAME_PAYMENT"].GetType());
            t.Columns.Add("TYPE_PAYMENT_TYPE_ID", AppDataSet.Tables["PAYMENT_TYPE"].Columns["TYPE_PAYMENT_TYPE_ID"].GetType());
            foreach (DataRow r in AppDataSet.Tables["PAYMENT_TYPE"].Rows)
                t.Rows.Add(false, r["PAYMENT_TYPE_ID"], r["CODE_PAYMENT"], r["NAME_PAYMENT"], r["TYPE_PAYMENT_TYPE_ID"]);
            dgPaymentType.ItemsSource = new DataView(t, string.Format("TYPE_PAYMENT_TYPE_ID={0}", cbTypePaymentType.SelectedValue), "CODE_PAYMENT", DataViewRowState.CurrentRows);
        }

        private void cbclFl_Checked(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView r in (dgPaymentType.ItemsSource as DataView))
                r["FL"] = cbclFl.IsChecked;
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = t.Rows.OfType<DataRow>().Count(p => (bool)p["FL"]) > 0;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
        public List<Object> SelectedItems
        {
            get
            {
                return t.Rows.OfType<DataRow>().Where(p => (bool)p["FL"]).Select(p => p["PAYMENT_TYPE_ID"]).ToList();
            }
        }
    }
}
