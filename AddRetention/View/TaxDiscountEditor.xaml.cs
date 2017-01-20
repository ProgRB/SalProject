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
    /// Interaction logic for TaxDiscountEditor.xaml
    /// </summary>
    public partial class TaxDiscountEditor : Window
    {
        DataSet ds = new DataSet();
        OracleDataAdapter odaEmp_Tax_Discount;
        public TaxDiscountEditor(object transfer_id, DateTime? currentDate, object EMP_TAX_DISCOUNT_ID = null)
        {
            InitializeComponent();
            odaEmp_Tax_Discount = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectEmpTaxDiscData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaEmp_Tax_Discount.SelectCommand.BindByName = true;
            odaEmp_Tax_Discount.SelectCommand.Parameters.Add("p_EMP_TAX_DISCOUNT_ID", OracleDbType.Decimal, EMP_TAX_DISCOUNT_ID, ParameterDirection.Input);
            odaEmp_Tax_Discount.SelectCommand.Parameters.Add("p_DATE", OracleDbType.Date, currentDate, ParameterDirection.Input);
            odaEmp_Tax_Discount.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaEmp_Tax_Discount.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);

            odaEmp_Tax_Discount.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.EMP_TAX_DISCOUNT_UPDATE(:p_EMP_TAX_DISCOUNT_ID,:p_TRANSFER_ID,:p_DATE_START_DISC,:p_DATE_END_DISC,:p_TYPE_DISCOUNT_ID,:p_SUM_DISCOUNT,:p_CODE_DOCUM,:p_DATE_DOCUM,:p_CODE_DOC_GETTER);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaEmp_Tax_Discount.InsertCommand.BindByName = true;
            odaEmp_Tax_Discount.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaEmp_Tax_Discount.InsertCommand.Parameters.Add("p_EMP_TAX_DISCOUNT_ID", OracleDbType.Decimal, 0, "EMP_TAX_DISCOUNT_ID").Direction = ParameterDirection.InputOutput;
            odaEmp_Tax_Discount.InsertCommand.Parameters["p_EMP_TAX_DISCOUNT_ID"].DbType = DbType.Decimal;
            odaEmp_Tax_Discount.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.InsertCommand.Parameters.Add("p_DATE_START_DISC", OracleDbType.Date, 0, "DATE_START_DISC").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.InsertCommand.Parameters.Add("p_DATE_END_DISC", OracleDbType.Date, 0, "DATE_END_DISC").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.InsertCommand.Parameters.Add("p_TYPE_DISCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_DISCOUNT_ID").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.InsertCommand.Parameters.Add("p_SUM_DISCOUNT", OracleDbType.Decimal, 0, "SUM_DISCOUNT").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.InsertCommand.Parameters.Add("p_CODE_DOCUM", OracleDbType.Varchar2, 0, "CODE_DOCUM").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.InsertCommand.Parameters.Add("p_DATE_DOCUM", OracleDbType.Date, 0, "DATE_DOCUM").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.InsertCommand.Parameters.Add("p_CODE_DOC_GETTER", OracleDbType.Varchar2, 0, "CODE_DOC_GETTER").Direction = ParameterDirection.Input; 
            
            odaEmp_Tax_Discount.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.EMP_TAX_DISCOUNT_UPDATE(:p_EMP_TAX_DISCOUNT_ID,:p_TRANSFER_ID,:p_DATE_START_DISC,:p_DATE_END_DISC,:p_TYPE_DISCOUNT_ID,:p_SUM_DISCOUNT,:p_CODE_DOCUM,:p_DATE_DOCUM,:p_CODE_DOC_GETTER);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaEmp_Tax_Discount.UpdateCommand.BindByName = true;
            odaEmp_Tax_Discount.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaEmp_Tax_Discount.UpdateCommand.Parameters.Add("p_EMP_TAX_DISCOUNT_ID", OracleDbType.Decimal, 0, "EMP_TAX_DISCOUNT_ID").Direction = ParameterDirection.InputOutput;
            odaEmp_Tax_Discount.UpdateCommand.Parameters["p_EMP_TAX_DISCOUNT_ID"].DbType = DbType.Decimal;
            odaEmp_Tax_Discount.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.UpdateCommand.Parameters.Add("p_DATE_START_DISC", OracleDbType.Date, 0, "DATE_START_DISC").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.UpdateCommand.Parameters.Add("p_DATE_END_DISC", OracleDbType.Date, 0, "DATE_END_DISC").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.UpdateCommand.Parameters.Add("p_TYPE_DISCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_DISCOUNT_ID").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.UpdateCommand.Parameters.Add("p_SUM_DISCOUNT", OracleDbType.Decimal, 0, "SUM_DISCOUNT").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.UpdateCommand.Parameters.Add("p_CODE_DOCUM", OracleDbType.Varchar2, 0, "CODE_DOCUM").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.UpdateCommand.Parameters.Add("p_DATE_DOCUM", OracleDbType.Date, 0, "DATE_DOCUM").Direction = ParameterDirection.Input;
            odaEmp_Tax_Discount.UpdateCommand.Parameters.Add("p_CODE_DOC_GETTER", OracleDbType.Varchar2, 0, "CODE_DOC_GETTER").Direction = ParameterDirection.Input; 
            
            odaEmp_Tax_Discount.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.EMP_TAX_DISCOUNT_DELETE(:p_EMP_TAX_DISCOUNT_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaEmp_Tax_Discount.DeleteCommand.BindByName = true;
            odaEmp_Tax_Discount.DeleteCommand.Parameters.Add("p_EMP_TAX_DISCOUNT_ID", OracleDbType.Decimal, 0, "EMP_TAX_DISCOUNT_ID").Direction = ParameterDirection.InputOutput;            
            odaEmp_Tax_Discount.TableMappings.Add("Table", "TaxData");
            odaEmp_Tax_Discount.TableMappings.Add("Table1", "TYPE_TAX");
            odaEmp_Tax_Discount.Fill(ds);

            cbTypeDisc.ItemsSource = new DataView(ds.Tables["TYPE_TAX"], "", "CODE_DISC, DATE_START", DataViewRowState.CurrentRows);
            if (TaxData == null)
            {
                ds.Tables["TaxData"].Rows.Add(ds.Tables["TaxData"].NewRow());
                TaxData["transfer_id"] = transfer_id;
            }
            this.DataContext = new DataView(ds.Tables["TaxData"], "", "", DataViewRowState.CurrentRows);
        }
        private DataRow TaxData
        {
            get
            {
                if (ds.Tables["TaxData"].Rows.Count > 0) return ds.Tables["TaxData"].Rows[0]; else return null;
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && ds != null && ds.HasChanges() && Array.TrueForAll<DependencyObject>(mainGrid.Children.Cast<UIElement>().ToArray(), t=>Validation.GetHasError(t)==false);
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                odaEmp_Tax_Discount.Update(ds.Tables["TaxData"]);
                tr.Commit();
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
            }
        }
        public object this[string field]
        {
            get
            {
                return TaxData[field];
            }
            set
            {
                TaxData[field] = value;
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
