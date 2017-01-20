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
using Salary.Helpers;
using Oracle.DataAccess.Client;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for MessageEditor.xaml
    /// </summary>
    public partial class MessageEditor : UserControl
    {
        public MessageEditor()
        {
            InitializeComponent();
        }

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        public MessageEditModel Model
        {
            get
            {
                return this.FindResource("Model") as MessageEditModel;
            }
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddNewRow();
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && dgMessage != null && dgMessage.SelectedItem != null;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.DeleteRow(dgMessage.SelectedItem as DataRowView);
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.HasChanges;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Save();
        }
    }

    public class MessageEditModel : NotificationObject
    {
        DataSet ds;
        OracleDataAdapter odaMessage;
        public MessageEditModel()
        {
            try
            {
                ds = new DataSet();
                odaMessage = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectAllMessages.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                odaMessage.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.MESSAGE_UPDATE(:p_MESSAGE_ID,:p_CONTENT_MESSAGE,:p_DATE_MESSAGE,:p_APP_NAME);end;", Connect.SchemaApstaff), Connect.CurConnect);
                odaMessage.InsertCommand.BindByName = true;
                odaMessage.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
                odaMessage.InsertCommand.Parameters.Add("p_MESSAGE_ID", OracleDbType.Decimal, 0, "MESSAGE_ID").Direction = ParameterDirection.InputOutput;
                odaMessage.InsertCommand.Parameters["p_MESSAGE_ID"].DbType = DbType.Decimal;
                odaMessage.InsertCommand.Parameters.Add("p_CONTENT_MESSAGE", OracleDbType.Varchar2, 0, "CONTENT_MESSAGE").Direction = ParameterDirection.Input;
                odaMessage.InsertCommand.Parameters.Add("p_DATE_MESSAGE", OracleDbType.Date, 0, "DATE_MESSAGE").Direction = ParameterDirection.Input;
                odaMessage.InsertCommand.Parameters.Add("p_APP_NAME", OracleDbType.Varchar2, 0, "APP_NAME").Direction = ParameterDirection.Input;

                odaMessage.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.MESSAGE_UPDATE(:p_MESSAGE_ID,:p_CONTENT_MESSAGE,:p_DATE_MESSAGE,:p_APP_NAME);end;", Connect.SchemaApstaff), Connect.CurConnect);
                odaMessage.UpdateCommand.BindByName = true;
                odaMessage.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
                odaMessage.UpdateCommand.Parameters.Add("p_MESSAGE_ID", OracleDbType.Decimal, 0, "MESSAGE_ID").Direction = ParameterDirection.InputOutput;
                odaMessage.UpdateCommand.Parameters["p_MESSAGE_ID"].DbType = DbType.Decimal;
                odaMessage.UpdateCommand.Parameters.Add("p_CONTENT_MESSAGE", OracleDbType.Varchar2, 0, "CONTENT_MESSAGE").Direction = ParameterDirection.Input;
                odaMessage.UpdateCommand.Parameters.Add("p_DATE_MESSAGE", OracleDbType.Date, 0, "DATE_MESSAGE").Direction = ParameterDirection.Input;
                odaMessage.UpdateCommand.Parameters.Add("p_APP_NAME", OracleDbType.Varchar2, 0, "APP_NAME").Direction = ParameterDirection.Input;

                odaMessage.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.MESSAGE_DELETE(:p_MESSAGE_ID);end;", Connect.SchemaApstaff), Connect.CurConnect);
                odaMessage.DeleteCommand.BindByName = true;
                odaMessage.DeleteCommand.Parameters.Add("p_MESSAGE_ID", OracleDbType.Decimal, 0, "MESSAGE_ID").Direction = ParameterDirection.InputOutput;

                odaMessage.TableMappings.Add("Table", "MESSAGE");
            }
            catch
            { };
        }
        public bool HasChanges
        {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }

        public DataView View
        {
            get
            {
                if (ds == null || !ds.Tables.Contains("MESSAGE"))
                    LoadData();
                return ds.Tables["MESSAGE"].DefaultView;
            }
        }
        public void LoadData()
        {
            if (ds.Tables.Contains("MESSAGE"))
                ds.Tables["MESSAGE"].Rows.Clear();
            odaMessage.Fill(ds);
        }


        public List<string> DataApp
        {
            get
            {
                return new List<string>(new string[] { "SALARY" });
            }
        }

        public void AddNewRow()
        {
            DataRow r = ds.Tables[0].NewRow();
            r["DATE_MESSAGE"] = DateTime.Now;
            r["APP_NAME"] = "SALARY";
            ds.Tables[0].Rows.Add(r);
        }

        public void DeleteRow(DataRowView r)
        {
            r.Delete();
        }
        public void Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                odaMessage.Update(ds.Tables[0]);
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
            }
        }
    }
}
