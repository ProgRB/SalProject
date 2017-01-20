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
using Oracle.DataAccess.Client;
using System.Data;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for FindOrders.xaml
    /// </summary>
    public partial class FindOrders : Window
    {
        private static RoutedUICommand _addNewOrder, _selectOrder;
        public static RoutedUICommand AddNewOrderSalary
        {
            get { return _addNewOrder; }
        }

        public static RoutedUICommand SelectOrder
        {
            get { return _selectOrder; }
        }

        private decimal _order_id;
        public decimal Order_ID
        {
            get { return _order_id; }
        }

        private string _order_name;
        public string Order_name
        {
            get { return _order_name; }
        }

        public FindOrders(string _order_name)
        {
            InitializeComponent();
            OrdersTable.DefaultView.RowFilter = "";
            tbOrder_name.Text = _order_name;
            dgOrders.ItemsSource = OrdersTable.DefaultView;            
        }

        private DataTable OrdersTable
        {
            get
            {
                return AppDataSet.Tables["ORDER"];
            }
        }

        static FindOrders()
        {
            _addNewOrder = new RoutedUICommand("Добавить новый заказ", "AddNewOrderSalary", typeof(FindOrders));
            _selectOrder = new RoutedUICommand("Выбрать заказ", "SelectOrder", typeof(FindOrders));
        }

        private void tbOrder_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbOrder_name.Text.Trim() != "")
            {
                OrdersTable.DefaultView.RowFilter =
                    "ORDER_NAME like '" + tbOrder_name.Text.Trim() + "%'";
            }
            else
            {
                OrdersTable.DefaultView.RowFilter = "";
            }
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void tbOrder_name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = ("0123456789").IndexOf(e.Text) < 0;
        }

        private void AddNewOrder_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState(e.Command) && OrdersTable.DefaultView.Count == 0 &&
                tbOrder_name != null && tbOrder_name.Text.Trim().Length == 13)
            {
                e.CanExecute = true;
            }
        }

        private void AddNewOrder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand(string.Format("select {0}.GetOrderId(:p_order_code) from dual", Connect.SchemaApstaff), Connect.CurConnect);
            cmd.Parameters.Add("p_order_code", OracleDbType.Varchar2, tbOrder_name.Text.Trim(), ParameterDirection.Input);
            try
            {
                object[] newRow = new object[] { cmd.ExecuteScalar(), tbOrder_name.Text.Trim()};
                OrdersTable.Rows.Add(newRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "АРМ \"Зарплата предпириятия\" - Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectOrder.Execute(null, null);
        }

        private void SelectOrder_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgOrders != null && dgOrders.SelectedItem != null)
            {
                e.CanExecute = true;
            }
        }

        private void SelectOrder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView drView = dgOrders.SelectedItem as DataRowView;
            _order_id = Convert.ToDecimal(drView["ORDER_ID"]);
            _order_name = drView["ORDER_NAME"].ToString();
            this.DialogResult = true;
            this.Close();
        }
    }
}
