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
using EntityGenerator;
using System.ComponentModel;
using Salary.Helpers;
using LibrarySalary.Helpers;


namespace Salary.View
{
    /// <summary>
    /// Interaction logic for CartPaidEditor.xaml
    /// </summary>
    public partial class CartPaidEditor : Window, INotifyPropertyChanged
    {
        DataSet ds;
        OracleDataAdapter odaCartulary_Paid, odaClientAccount;
        public CartPaidEditor(object p_cartulary_id, object p_cartulary_paid_id, decimal? TypeCartularyid = null)
        {
            typecartID = TypeCartularyid;
            ds = new DataSet();
            ds.Tables.Add("CLIENT_ACCOUNT");
            odaCartulary_Paid = new OracleDataAdapter(String.Format(Queries.GetQuery("SelectCartularyPaidData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaCartulary_Paid.SelectCommand.BindByName = true;
            odaCartulary_Paid.SelectCommand.Parameters.Add("p_cartulary_paid_id", OracleDbType.Decimal, p_cartulary_paid_id, ParameterDirection.Input);
            odaCartulary_Paid.TableMappings.Add("Table", "Cartulary_Paid");

            odaCartulary_Paid.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.CARTULARY_PAID_UPDATE(:p_CARTULARY_PAID_ID,:p_CARTULARY_ID,:p_PAID_SUM,:p_TRANSFER_ID,:p_PAYMENT_TYPE_ID,:p_CLIENT_ACCOUNT_ID,:p_SALARY_ID, :p_PAID_COMMENT, :p_BCC_CODE, :p_OKATO);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaCartulary_Paid.InsertCommand.BindByName = true;
            odaCartulary_Paid.InsertCommand.Parameters.Add("p_CARTULARY_PAID_ID", OracleDbType.Decimal, 0, "CARTULARY_PAID_ID").Direction = ParameterDirection.InputOutput;
            odaCartulary_Paid.InsertCommand.Parameters["p_CARTULARY_PAID_ID"].DbType = DbType.Decimal;
            odaCartulary_Paid.InsertCommand.Parameters.Add("p_CARTULARY_ID", OracleDbType.Decimal, 0, "CARTULARY_ID").Direction = ParameterDirection.Input;
            odaCartulary_Paid.InsertCommand.Parameters.Add("p_PAID_SUM", OracleDbType.Decimal, 0, "PAID_SUM").Direction = ParameterDirection.Input;
            odaCartulary_Paid.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaCartulary_Paid.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input;
            odaCartulary_Paid.InsertCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.Input;
            odaCartulary_Paid.InsertCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, 0, "SALARY_ID").Direction = ParameterDirection.Input; 
            odaCartulary_Paid.InsertCommand.Parameters.Add("p_PAID_COMMENT", OracleDbType.Varchar2, 0, "PAID_COMMENT").Direction = ParameterDirection.Input;
            odaCartulary_Paid.InsertCommand.Parameters.Add("p_BCC_CODE", OracleDbType.Varchar2, 0, "BCC_CODE").Direction = ParameterDirection.Input; 
            odaCartulary_Paid.InsertCommand.Parameters.Add("p_OKATO", OracleDbType.Varchar2, 0, "OKATO").Direction = ParameterDirection.Input;

            odaCartulary_Paid.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.CARTULARY_PAID_UPDATE(:p_CARTULARY_PAID_ID,:p_CARTULARY_ID,:p_PAID_SUM,:p_TRANSFER_ID,:p_PAYMENT_TYPE_ID,:p_CLIENT_ACCOUNT_ID,:p_SALARY_ID, :p_PAID_COMMENT, :p_BCC_CODE, :p_OKATO);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaCartulary_Paid.UpdateCommand.BindByName = true;
            odaCartulary_Paid.UpdateCommand.Parameters.Add("p_CARTULARY_PAID_ID", OracleDbType.Decimal, 0, "CARTULARY_PAID_ID").Direction = ParameterDirection.InputOutput;
            odaCartulary_Paid.UpdateCommand.Parameters["p_CARTULARY_PAID_ID"].DbType = DbType.Decimal;
            odaCartulary_Paid.UpdateCommand.Parameters.Add("p_CARTULARY_ID", OracleDbType.Decimal, 0, "CARTULARY_ID").Direction = ParameterDirection.Input;
            odaCartulary_Paid.UpdateCommand.Parameters.Add("p_PAID_SUM", OracleDbType.Decimal, 0, "PAID_SUM").Direction = ParameterDirection.Input;
            odaCartulary_Paid.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaCartulary_Paid.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input;
            odaCartulary_Paid.UpdateCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.Input;
            odaCartulary_Paid.UpdateCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, 0, "SALARY_ID").Direction = ParameterDirection.Input;
            odaCartulary_Paid.UpdateCommand.Parameters.Add("p_PAID_COMMENT", OracleDbType.Varchar2, 0, "PAID_COMMENT").Direction = ParameterDirection.Input;
            odaCartulary_Paid.UpdateCommand.Parameters.Add("p_BCC_CODE", OracleDbType.Varchar2, 0, "BCC_CODE").Direction = ParameterDirection.Input;
            odaCartulary_Paid.UpdateCommand.Parameters.Add("p_OKATO", OracleDbType.Varchar2, 0, "OKATO").Direction = ParameterDirection.Input; 
            

            odaCartulary_Paid.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.CARTULARY_PAID_DELETE(:p_CARTULARY_PAID_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaCartulary_Paid.DeleteCommand.BindByName = true;
            odaCartulary_Paid.DeleteCommand.Parameters.Add("p_CARTULARY_PAID_ID", OracleDbType.Decimal, 0, "CARTULARY_PAID_ID").Direction = ParameterDirection.InputOutput;

            odaCartulary_Paid.Fill(ds);

            odaClientAccount = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectClientAccountForRetent.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaClientAccount.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaClientAccount.SelectCommand.Parameters.Add("p_payment_type_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaClientAccount.TableMappings.Add("Table", "CLIENT_ACCOUNT");

            InitializeComponent();

            if (this["cartulary_paid_id"] == null)
            {
                ds.Tables["Cartulary_Paid"].Rows.Add(ds.Tables["Cartulary_paid"].NewRow());
                ds.Tables["CARTULARY_PAID"].Rows[0]["CARTULARY_ID"] = p_cartulary_id;
                ds.AcceptChanges();
                ds.Tables["Cartulary_Paid"].ColumnChanged += new DataColumnChangeEventHandler(CartPaidEditor_ColumnChanged);
            }
            UpdateClientAccounts(this["transfer_id"], this["payment_type_id"]);
            this.DataContext = ds.Tables["Cartulary_paid"].DefaultView[0];
        }

        decimal? typecartID=null;
        public decimal? TypeCartularyID
        {
            get
            {
                return typecartID;
            }
            set
            {
                typecartID = value;
                OnPropertyChanged("TypeCartularyID");
            }
        }

        public bool IsEditableAccount
        {
            get
            {
                if (TypeCartularyID == null)
                    return true;
                else return AppDataSet.Tables["TYPE_CARTULARY"].Rows.OfType<DataRow>().Where(r => r.Field2<Decimal?>("TYPE_CARTULARY_ID") == TypeCartularyID).Select(r => (r.Field2<Decimal?>("SIGN_NO_ACCOUNT")??0) == 0).FirstOrDefault();
            }
        }

        void CartPaidEditor_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Column.ColumnName.ToUpper() == "PAYMENT_TYPE_ID" || e.Column.ColumnName.ToUpper() == "TRANSFER_ID")
                UpdateClientAccounts(e.Row["transfer_id"], e.Row["payment_Type_id"]);
        }

        public DataView ClientAccountSource
        {
            get
            {
                return new DataView(ds.Tables["CLIENT_ACCOUNT"], "", "", DataViewRowState.CurrentRows);
            }
        }


        private void UpdateClientAccounts(object transfer_id, object payment_type_id)
        {
            ds.Tables["CLIENT_ACCOUNT"].Clear();
            odaClientAccount.SelectCommand.Parameters["p_transfer_id"].Value = transfer_id;
            odaClientAccount.SelectCommand.Parameters["p_payment_type_id"].Value = payment_type_id;
            odaClientAccount.Fill(ds);
        }

        public object this[string field]
        {
            get
            {
                if (ds.Tables.Contains("Cartulary_paid") && ds.Tables["Cartulary_paid"].Rows.Count > 0)
                    return ds.Tables["Cartulary_paid"].Rows[0][field];
                else return null;
            }
            set
            {
                if (ds.Tables.Contains("Cartulary_paid") && ds.Tables["Cartulary_paid"].Rows.Count > 0)
                    ds.Tables["Cartulary_paid"].Rows[0][field] = value;
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Save_canExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && ds != null && ds.HasChanges() && !ValidationHelper.IsElementHasErrors(this);
        }

        private void save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                if (!this.IsEditableAccount)
                    ds.Tables["CARTULARY_PAID"].Rows[0]["CLIENT_ACCOUNT_ID"] = 0;
                odaCartulary_Paid.Update(ds.Tables["cartulary_paid"]);
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

        private void ChangeTransfer_canExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void ChangeTransfer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EmpFinder f = new EmpFinder();
            if (f.ShowDialog() == true)
            {
                this["transfer_id"] = f.SelectedItem["transfer_id"];
                this["FIO"] = f.FullFIO;
                this["PER_NUM"] = f.PerNum;

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        { 
            if (PropertyChanged!=null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
