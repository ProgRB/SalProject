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
    /// Interaction logic for SalaryDocEditor.xaml
    /// </summary>
    public partial class SalaryDocEditor : Window
    {
        DataSet ds = new DataSet();
        OracleDataAdapter odaSave = new OracleDataAdapter(string.Format(Queries.GetQuery(@"SelectSalDocData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
        public SalaryDocEditor(object transfer_id, object sal_doc_id=null)
        {
            InitializeComponent();
            odaSave.SelectCommand.BindByName = true;
            odaSave.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            odaSave.SelectCommand.Parameters.Add("p_salary_doc_id", OracleDbType.Decimal, sal_doc_id, ParameterDirection.Input);
            odaSave.TableMappings.Add("Table", "SALARY_DOC");
            odaSave.InsertCommand = new OracleCommand(string.Format(@"begin {1}.SALARY_DOC_UPDATE
                      (
                       :p_SALARY_DOC_ID
                      ,:p_DOC_NAME
                      ,:p_DOC_CODE
                      ,:p_DOC_DATE
                      ,:p_TRANSFER_ID
                      ,:p_DOC_COMMENT
                      ); END;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSave.InsertCommand.BindByName = true;
            odaSave.InsertCommand.Parameters.Add("p_SALARY_DOC_ID", OracleDbType.Decimal, sal_doc_id, ParameterDirection.InputOutput);
            odaSave.InsertCommand.Parameters.Add("p_DOC_NAME", OracleDbType.Varchar2, 0, "DOC_NAME");
            odaSave.InsertCommand.Parameters.Add("p_DOC_CODE", OracleDbType.Varchar2, 0, "DOC_CODE");
            odaSave.InsertCommand.Parameters.Add("p_DOC_DATE", OracleDbType.Date, 0, "DOC_DATE");
            odaSave.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, transfer_id, ParameterDirection.Input);
            odaSave.InsertCommand.Parameters.Add("p_DOC_COMMENT", OracleDbType.Varchar2, 0, "DOC_COMMENT");
            odaSave.Fill(ds);
            if (ds.Tables["SALARY_DOC"].Rows.Count == 0)
            {
                DataRow r = ds.Tables["SALARY_DOC"].NewRow();
                r["SALARY_DOC_ID"] = -1;
                ds.Tables["SALARY_DOC"].Rows.Add(r);
            }
            else
                ds.Tables["SALARY_DOC"].Rows[0].SetAdded();
            this.DataContext = ds.Tables["SALARY_DOC"].DefaultView[0];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && ds != null && ds.HasChanges();
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                odaSave.Update(new DataRow[] { ds.Tables["SALARY_DOC"].Rows[0] });
                tr.Commit();
                this.DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException());
            }
        }
        public object this[string field]
        {
            get
            {
                return (ds.Tables["SALARY_DOC"].Rows.Count > 0 ? ds.Tables["SALARY_DOC"].Rows[0][field] : DBNull.Value);
            }
        }
    }
}
