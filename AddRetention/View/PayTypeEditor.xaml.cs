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
    /// Interaction logic for PayTypeEditor.xaml
    /// </summary>
    public partial class PayTypeEditor : Window
    {
        DataSet ds = new DataSet();
        OracleDataAdapter odaPayType;
        public PayTypeEditor(object PAYMENT_TYPE_ID)
        {
            InitializeComponent();
            try
            {
                odaPayType = new OracleDataAdapter(string.Format("select * from {1}.payment_type where payment_type_id=:p_payment_type_id", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                odaPayType.SelectCommand.Parameters.Add("p_payment_type_id", OracleDbType.Decimal, PAYMENT_TYPE_ID, ParameterDirection.Input);
                odaPayType.SelectCommand.BindByName = true;

                odaPayType.UpdateCommand = new OracleCommand(string.Format(@"begin {1}.PAYMENT_TYPE_UPDATE(
                :p_PAYMENT_TYPE_ID, :p_CODE_PAYMENT, :p_PAY_TYPE_ID, :p_CALC_PRIORITY,
                :p_NAME_PAYMENT, :p_SIGN_FORM_REPORT, :p_TYPE_PAYMENT_TYPE_ID, :p_CONSIDER_TYPE_ID, :p_IS_NEGATIV_ALOWED); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                odaPayType.UpdateCommand.BindByName = true;
                odaPayType.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").DbType = DbType.Decimal;
                odaPayType.UpdateCommand.Parameters.Add("p_CODE_PAYMENT", OracleDbType.Varchar2, 0, "CODE_PAYMENT");
                odaPayType.UpdateCommand.Parameters.Add("p_PAY_TYPE_ID", OracleDbType.Decimal, 0, "PAY_TYPE_ID");
                odaPayType.UpdateCommand.Parameters.Add("p_CALC_PRIORITY", OracleDbType.Decimal, 0, "CALC_PRIORITY");
                odaPayType.UpdateCommand.Parameters.Add("p_NAME_PAYMENT", OracleDbType.Varchar2, 0, "NAME_PAYMENT");
                odaPayType.UpdateCommand.Parameters.Add("p_SIGN_FORM_REPORT", OracleDbType.Decimal, 0, "SIGN_FORM_REPORT");
                odaPayType.UpdateCommand.Parameters.Add("p_TYPE_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "TYPE_PAYMENT_TYPE_ID");
                odaPayType.UpdateCommand.Parameters.Add("p_CONSIDER_TYPE_ID", OracleDbType.Decimal, 0, "CONSIDER_TYPE_ID");
                odaPayType.UpdateCommand.Parameters.Add("p_IS_NEGATIV_ALOWED", OracleDbType.Decimal, 0, "IS_NEGATIV_ALOWED");
                odaPayType.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
                odaPayType.Fill(ds, "PaymentType");
                if (ds.Tables["PaymentType"].Rows.Count == 0)
                {
                    ds.Tables["PAYMENTTYPE"].Rows.Add(ds.Tables["PAYMENTTYPE"].NewRow());
                    ds.Tables["PAYMENTTYPE"].Rows[0].AcceptChanges();
                    ds.Tables["PAYMENTTYPE"].Rows[0].SetModified();
                }
                this.DataContext = new DataView(ds.Tables["PaymentType"], "", "", DataViewRowState.CurrentRows)[0];
                OracleDataAdapter d = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectPayTypeCatalogs.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                d.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                d.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                d.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
                d.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
                d.SelectCommand.BindByName = true;
                d.TableMappings.Add("Table", "TypePaymentType");
                d.TableMappings.Add("Table1", "RETENT_CALC_METHOD");
                d.TableMappings.Add("Table2", "PAY_TYPE");
                d.TableMappings.Add("Table3", "CONSIDER_TYPE");
                d.Fill(ds);
                cbTypePayType.ItemsSource = new DataView(ds.Tables["TypePaymentType"], "", "", DataViewRowState.CurrentRows);
                //cbRetentMethod.ItemsSource = new DataView(ds.Tables["RETENT_CALC_METHOD"], "", "METHOD_NAME", DataViewRowState.CurrentRows);
                ds.Tables["PAY_TYPE"].Columns.Add("DISP_EXP", typeof(string), "PAY_TYPE_ID+' ('+PAY_TYPE_NAME+')'");
                cbPayTypeId.ItemsSource = new DataView(ds.Tables["PAY_TYPE"], "", "PAY_TYPE_ID", DataViewRowState.CurrentRows);
                cbCosiderType.ItemsSource = new DataView(ds.Tables["CONSIDER_TYPE"], "", "CONSIDER_TYPE_ID", DataViewRowState.CurrentRows);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException());
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ds.Tables != null && ds.Tables.Contains("PaymentType") && ds.Tables["PaymentType"].GetChanges() != null && ControlRoles.GetState((e.Command as RoutedUICommand).Name)
                && gridMainSet.Children.Cast<FrameworkElement>().Count(t=>Validation.GetHasError(t))==0;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                odaPayType.Update(new DataRow[]{ds.Tables["PaymentType"].Rows[0]});
                tr.Commit();
                this.DialogResult = true;
                AppDataSet.UpdateSet("PAYMENT_TYPE");
                this.Close();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException());
            }
        }
    }
}
