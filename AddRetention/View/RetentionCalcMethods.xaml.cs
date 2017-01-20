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
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for RetentionSettings.xaml
    /// </summary>
    public partial class RetentionCalcMethods : UserControl
    {
        DataSet ds = new DataSet();
        OracleCommand cmd = new OracleCommand(string.Format(Queries.GetQuery("SelectAllRetCalcMethods.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
        OracleDataAdapter odaTaxedPayType = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectPayTypeForCalcMethods.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
        public RetentionCalcMethods()
        {
            InitializeComponent();
            ds.Tables.Add("RetentMethods");
            ds.Tables.Add("TaxedPayType");
            dgRetentMethod.SelectionChanged += new SelectionChangedEventHandler(dgRetentMethod_SelectionChanged);
            odaTaxedPayType.SelectCommand.BindByName = true;
            odaTaxedPayType.SelectCommand.Parameters.Add("p_RETENT_CALC_METHOD_ID", OracleDbType.Decimal, 0, ParameterDirection.Input);
            FillMethods();
            dgRetentMethod.ItemsSource = new DataView(ds.Tables["RetentMethods"], "", "METHOD_NAME", DataViewRowState.CurrentRows);
            CommandManager.InvalidateRequerySuggested();
        }
        private void FillMethods()
        {
            ds.Tables["RetentMethods"].Rows.Clear();
            new OracleDataAdapter(cmd).Fill(ds, "RetentMethods");
        }
        private void FillTaxedType()
        {
            ds.Tables["TaxedPayType"].Rows.Clear();
            if (dgRetentMethod.SelectedItem != null)
            {
                odaTaxedPayType.SelectCommand.Parameters["p_RETENT_CALC_METHOD_ID"].Value = (dgRetentMethod.SelectedItem as DataRowView)["RETENT_CALC_METHOD_ID"];
                odaTaxedPayType.Fill(ds, "TaxedPayType");
                if (dgTaxedPayType.ItemsSource == null)
                    dgTaxedPayType.ItemsSource = new DataView(ds.Tables["TaxedPayType"], "", "CODE_PAYMENT", DataViewRowState.CurrentRows);
            }
        }
        void dgRetentMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillTaxedType();
        }
       
        private void AddRetSetting_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }

        private void AddRetSet_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RetentionCalcSetting f = new RetentionCalcSetting(null);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog()==true)
            {
                FillMethods();
            }
        }

        private void DelRetSetting_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = dgRetentMethod != null && dgRetentMethod.SelectedItem != null && ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }

        private void DelRetSet_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный метод расчета?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    OracleCommand cmd = new OracleCommand(string.Format("begin {0}.RETENT_CALC_METHOD_DELETE(:p_RETENT_CALC_METHOD_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
                    cmd.BindByName = true;
                    cmd.Parameters.Add("p_RETENT_CALC_METHOD_ID", OracleDbType.Decimal, (dgRetentMethod.CurrentItem as DataRowView)["RETENT_CALC_METHOD_ID"], ParameterDirection.Input);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException());
                }
            }
        }

        private void EditRetSetting_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = dgRetentMethod != null && dgRetentMethod.SelectedItem != null && ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }

        private void EditRetSet_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RetentionCalcSetting f = new RetentionCalcSetting((dgRetentMethod.SelectedItem as DataRowView)["RETENT_CALC_METHOD_ID"]);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                FillMethods();
            }
        }  
    }
}
