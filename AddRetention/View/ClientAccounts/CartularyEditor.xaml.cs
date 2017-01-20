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
using System.ComponentModel;
using LibrarySalary.Helpers;
using EntityGenerator;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for CartularyEditor.xaml
    /// </summary>
    public partial class CartularyEditor : Window
    {
        private CartularyViewModel _model;

        public CartularyEditor(decimal? p_cartulary_id)
        {
            _model = new CartularyViewModel(p_cartulary_id);
            InitializeComponent();
            this.DataContext = Model;
        }

        public CartularyViewModel Model
        {
            get
            {
                return _model;
            }
        }

        private void Save_canExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.HasChanges;
        }

        private void save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Exception ex = Model.Save();
            if (ex != null)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
            }
            else
            {
                this.DialogResult = true;
                Close();
            }
            
        }
    }

    public class CartularyViewModel : Cartulary
    {
        DataSet ds;
        public DataView TypeCartularySource
        {
            get
            {
                return new DataView(AppDataSet.Tables["TYPE_CARTULARY"], "", "TYPE_CARTULARY_NAME", DataViewRowState.CurrentRows);
            }
        }

        public bool HasChanges
        {
            get
            {
                return ds.HasChanges();
            }
        }

        public CartularyViewModel(decimal? p_cartulary_id)
        {
            ds = new DataSet();
            
            DataAdapter = new OracleDataAdapter(String.Format(Queries.GetQuery("SelectCartularyData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            DataAdapter.SelectCommand.BindByName = true;
            DataAdapter.SelectCommand.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, p_cartulary_id, ParameterDirection.Input);
            DataAdapter.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            DataAdapter.TableMappings.Add("Table", "Cartulary");

            DataAdapter.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.CARTULARY_UPDATE(p_CARTULARY_ID=>:p_CARTULARY_ID,p_TYPE_CARTULARY_ID=>:p_TYPE_CARTULARY_ID,p_DATE_CARTULARY=>:p_DATE_CARTULARY,p_DATE_CREATE=>:p_DATE_CREATE,p_DATE_CLOSE_CART=>:p_DATE_CLOSE_CART,p_CARTULARY_COMMENT=>:p_CARTULARY_COMMENT, p_CARTULARY_SUBDIV_ID=>:p_CARTULARY_SUBDIV_ID, p_CARTULARY_NUM=>:p_CARTULARY_NUM, p_CARTULARY_HEADER=>:p_CARTULARY_HEADER);end;", Connect.SchemaSalary), Connect.CurConnect);
            DataAdapter.InsertCommand.BindByName = true;
            DataAdapter.InsertCommand.Parameters.Add("p_CARTULARY_ID", OracleDbType.Decimal, 0, "CARTULARY_ID").Direction = ParameterDirection.InputOutput;
            DataAdapter.InsertCommand.Parameters["p_CARTULARY_ID"].DbType = DbType.Decimal;
            DataAdapter.InsertCommand.Parameters.Add("p_TYPE_CARTULARY_ID", OracleDbType.Decimal, 0, "TYPE_CARTULARY_ID").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_DATE_CARTULARY", OracleDbType.Date, 0, "DATE_CARTULARY").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_DATE_CREATE", OracleDbType.Date, 0, "DATE_CREATE").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_DATE_CLOSE_CART", OracleDbType.Date, 0, "DATE_CLOSE_CART");
            DataAdapter.InsertCommand.Parameters.Add("p_CARTULARY_COMMENT", OracleDbType.Varchar2, 0, "CARTULARY_COMMENT");
            DataAdapter.InsertCommand.Parameters.Add("p_CARTULARY_SUBDIV_ID", OracleDbType.Decimal, 0, "CARTULARY_SUBDIV_ID");
            DataAdapter.InsertCommand.Parameters.Add("p_CARTULARY_NUM", OracleDbType.Varchar2, 0, "CARTULARY_NUM");
            DataAdapter.InsertCommand.Parameters.Add("p_CARTULARY_HEADER", OracleDbType.Varchar2, 0, "CARTULARY_HEADER");

            DataAdapter.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.CARTULARY_UPDATE(p_CARTULARY_ID=>:p_CARTULARY_ID,p_TYPE_CARTULARY_ID=>:p_TYPE_CARTULARY_ID,p_DATE_CARTULARY=>:p_DATE_CARTULARY,p_DATE_CREATE=>:p_DATE_CREATE,p_DATE_CLOSE_CART=>:p_DATE_CLOSE_CART,p_CARTULARY_COMMENT=>:p_CARTULARY_COMMENT, p_CARTULARY_SUBDIV_ID=>:p_CARTULARY_SUBDIV_ID, p_CARTULARY_NUM=>:p_CARTULARY_NUM, p_CARTULARY_HEADER=>:p_CARTULARY_HEADER);end;", Connect.SchemaSalary), Connect.CurConnect);
            DataAdapter.UpdateCommand.BindByName = true;
            DataAdapter.UpdateCommand.Parameters.Add("p_CARTULARY_ID", OracleDbType.Decimal, 0, "CARTULARY_ID").Direction = ParameterDirection.InputOutput;
            DataAdapter.UpdateCommand.Parameters["p_CARTULARY_ID"].DbType = DbType.Decimal;
            DataAdapter.UpdateCommand.Parameters.Add("p_TYPE_CARTULARY_ID", OracleDbType.Decimal, 0, "TYPE_CARTULARY_ID").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_DATE_CARTULARY", OracleDbType.Date, 0, "DATE_CARTULARY").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_DATE_CREATE", OracleDbType.Date, 0, "DATE_CREATE").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_DATE_CLOSE_CART", OracleDbType.Date, 0, "DATE_CLOSE_CART").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_CARTULARY_COMMENT", OracleDbType.Varchar2, 0, "CARTULARY_COMMENT").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_CARTULARY_SUBDIV_ID", OracleDbType.Decimal, 0, "CARTULARY_SUBDIV_ID");
            DataAdapter.UpdateCommand.Parameters.Add("p_CARTULARY_NUM", OracleDbType.Varchar2, 0, "CARTULARY_NUM");
            DataAdapter.UpdateCommand.Parameters.Add("p_CARTULARY_HEADER", OracleDbType.Varchar2, 0, "CARTULARY_HEADER");

            DataAdapter.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.CARTULARY_DELETE(:p_CARTULARY_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            DataAdapter.DeleteCommand.BindByName = true;
            DataAdapter.DeleteCommand.Parameters.Add("p_CARTULARY_ID", OracleDbType.Decimal, 0, "CARTULARY_ID").Direction = ParameterDirection.InputOutput;

            DataAdapter.Fill(ds);

            if (ds.Tables["CARTULARY"].Rows.Count == 0)
            {
                this.DataRow = ds.Tables["Cartulary"].Rows.Add();
                this.DateCreate = DateTime.Now;
            }
            else
                this.DataRow = ds.Tables["CARTULARY"].Rows[0];
        }
        /// <summary>
        /// Новая ли это херь
        /// </summary>
        public bool IsNew
        {
            get
            {
                return this.EntityState == DataRowState.Added;
            }
        }
    }
}
