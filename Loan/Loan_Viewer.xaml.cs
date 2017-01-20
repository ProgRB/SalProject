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
using Salary;

namespace Loan
{
    /// <summary>
    /// Interaction logic for Loan_Viewer.xaml
    /// </summary>
    public partial class Loan_Viewer : UserControl
    {
        private DataSet _ds = new DataSet();
        private OracleDataAdapter _daLoan = new OracleDataAdapter();
        public Loan_Viewer()
        {
            InitializeComponent();

            _ds.Tables.Add("LOAN");
            // Select
            _daLoan.SelectCommand = new OracleCommand(string.Format(Queries.GetQuery("Loan/SelectLoan.sql"),
                Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
            _daLoan.SelectCommand.BindByName = true;
            _daLoan.SelectCommand.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal);
            _daLoan.SelectCommand.Parameters.Add("p_SIGN_ARCHIVE", OracleDbType.Decimal);
            // Insert
            _daLoan.InsertCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.LOAN_UPDATE(:LOAN_ID, :PROTOCOL_NUMBER, :PROTOCOL_DATE, :CONTRACT_NUMBER, :CONTRACT_DATE, 
                        :TRANSFER_ID, :LOAN_DATE, :LOAN_SUM, :LOAN_TERM, :ORDINAL_NUMBER, :RETENTION_BY_CONTRACT, :RETENTION_BY_FACT, 
                        :SIGN_RETENTION, :SIGN_MATERIAL_BENEFIT, :PURPOSE_LOAN_ID, :TYPE_LOAN_ID, :SIGN_ARCHIVE);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daLoan.InsertCommand.BindByName = true;
            _daLoan.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            _daLoan.InsertCommand.Parameters.Add("LOAN_ID", OracleDbType.Decimal, 0, "LOAN_ID");
            _daLoan.InsertCommand.Parameters.Add("PROTOCOL_NUMBER", OracleDbType.Varchar2, 0, "PROTOCOL_NUMBER");
            _daLoan.InsertCommand.Parameters.Add("PROTOCOL_DATE", OracleDbType.Date, 0, "PROTOCOL_DATE");
            _daLoan.InsertCommand.Parameters.Add("CONTRACT_NUMBER", OracleDbType.Varchar2, 0, "CONTRACT_NUMBER");
            _daLoan.InsertCommand.Parameters.Add("CONTRACT_DATE", OracleDbType.Date, 0, "CONTRACT_DATE");
            _daLoan.InsertCommand.Parameters.Add("TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _daLoan.InsertCommand.Parameters.Add("LOAN_DATE", OracleDbType.Date, 0, "LOAN_DATE");
            _daLoan.InsertCommand.Parameters.Add("LOAN_SUM", OracleDbType.Decimal, 0, "LOAN_SUM");
            _daLoan.InsertCommand.Parameters.Add("LOAN_TERM", OracleDbType.Decimal, 0, "LOAN_TERM");
            _daLoan.InsertCommand.Parameters.Add("ORDINAL_NUMBER", OracleDbType.Int16, 0, "ORDINAL_NUMBER");
            _daLoan.InsertCommand.Parameters.Add("RETENTION_BY_CONTRACT", OracleDbType.Decimal, 0, "RETENTION_BY_CONTRACT");
            _daLoan.InsertCommand.Parameters.Add("RETENTION_BY_FACT", OracleDbType.Decimal, 0, "RETENTION_BY_FACT");
            _daLoan.InsertCommand.Parameters.Add("SIGN_RETENTION", OracleDbType.Decimal, 0, "SIGN_RETENTION");
            _daLoan.InsertCommand.Parameters.Add("SIGN_MATERIAL_BENEFIT", OracleDbType.Decimal, 0, "SIGN_MATERIAL_BENEFIT");
            _daLoan.InsertCommand.Parameters.Add("PURPOSE_LOAN_ID", OracleDbType.Decimal, 0, "PURPOSE_LOAN_ID");
            _daLoan.InsertCommand.Parameters.Add("TYPE_LOAN_ID", OracleDbType.Decimal, 0, "TYPE_LOAN_ID");
            _daLoan.InsertCommand.Parameters.Add("SIGN_ARCHIVE", OracleDbType.Int16, 0, "SIGN_ARCHIVE");
            // Update
            _daLoan.UpdateCommand = _daLoan.InsertCommand;
            // Delete
            _daLoan.DeleteCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.LOAN_DELETE(:LOAN_ID);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daLoan.DeleteCommand.BindByName = true;
            _daLoan.DeleteCommand.Parameters.Add("LOAN_ID", OracleDbType.Decimal, 0, "LOAN_ID");

            GetLoan(null, 0);
            dgLoan.DataContext = _ds.Tables["LOAN"].DefaultView;
        }

        void GetLoan(decimal? loan_ID, int sign_Archive)
        {
            _ds.Tables["LOAN"].Clear();
            _daLoan.SelectCommand.Parameters["p_LOAN_ID"].Value = loan_ID;
            _daLoan.SelectCommand.Parameters["p_SIGN_ARCHIVE"].Value = sign_Archive;
            _daLoan.Fill(_ds.Tables["LOAN"]);
        }

        private void Filter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //filterGroup.UpdateSources();
            }
        }
    }
}
