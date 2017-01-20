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
using System.Collections.ObjectModel;
using System.IO;
using AddRetention.Properties;

namespace AddRetention.View
{
    /// <summary>
    /// Interaction logic for AdditionRetentional.xaml
    /// </summary>
    public partial class AdditionRetention : UserControl
    {
        DataSet ds = new DataSet();
        OracleDataAdapter ar_adapter;
        private static RoutedUICommand _rejectCommand;
        static DataView _ListEmp;
        static AdditionRetention()
        {
            _rejectCommand = new RoutedUICommand("Отменить все несохраненные действия", "RejectUnsafeRows", typeof(AdditionRetention));
        }
        public static RoutedUICommand RejectCommand
        {
            get { return _rejectCommand; }
        }
        public AdditionRetention()
        {
            InitializeComponent();

            ar_adapter = new OracleDataAdapter(string.Format(Queries.GetQuery("GetAddRetention.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            ar_adapter.SelectCommand.BindByName = true;
            ar_adapter.SelectCommand.Parameters.Add("p_per_num", PER_NUM.Text.Trim());
            ar_adapter.SelectCommand.Parameters.Add("p_subdiv_id", PER_NUM.Text.Trim());

            ar_adapter.InsertCommand = new OracleCommand(string.Format(@"INSERT INTO {0}.ADD_RETENTION (
                                           ADD_RETENTION_ID, TRANSFER_ID, PAY_TYPE_ID, 
                                           ORDER_NUMBER, ORIGINAL_SUM, RETENT_PERCENT, 
                                           RETENT_SUM, REMAIN_SUM) 
                                        VALUES (:ADD_RETENTION_ID,
                                         :TRANSFER_ID,
                                         :PAY_TYPE_ID,
                                         :ORDER_NUMBER,
                                         :ORIGINAL_SUM,
                                         :RETENT_PERCENT,
                                         :RETENT_SUM,
                                         :REMAIN_SUM )", Connect.SchemaSalary), Connect.CurConnect);
            ar_adapter.InsertCommand.BindByName = true;
            ar_adapter.InsertCommand.Parameters.Add("ADD_RETENTION_ID", OracleDbType.Decimal, 0, "ADD_RETENTION_ID");
            ar_adapter.InsertCommand.Parameters.Add("TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            ar_adapter.InsertCommand.Parameters.Add("PAY_TYPE_ID", OracleDbType.Decimal, 0, "PAY_TYPE_ID");
            ar_adapter.InsertCommand.Parameters.Add("ORDER_NUMBER", OracleDbType.Decimal, 0, "ORDER_NUMBER");
            ar_adapter.InsertCommand.Parameters.Add("ORIGINAL_SUM", OracleDbType.Decimal, 0, "ORIGINAL_SUM");
            ar_adapter.InsertCommand.Parameters.Add("RETENT_PERCENT", OracleDbType.Decimal, 0, "RETENT_PERCENT");
            ar_adapter.InsertCommand.Parameters.Add("RETENT_SUM", OracleDbType.Decimal, 0, "RETENT_SUM");
            ar_adapter.InsertCommand.Parameters.Add("REMAIN_SUM", OracleDbType.Decimal, 0, "REMAIN_SUM");

            ar_adapter.UpdateCommand = new OracleCommand(string.Format(@"UPDATE {0}.ADD_RETENTION
                        SET
                               TRANSFER_ID      = :TRANSFER_ID,
                               PAY_TYPE_ID      = :PAY_TYPE_ID,
                               ORDER_NUMBER     = :ORDER_NUMBER,
                               ORIGINAL_SUM     = :ORIGINAL_SUM,
                               RETENT_PERCENT   = :RETENT_PERCENT,
                               RETENT_SUM       = :RETENT_SUM,
                               REMAIN_SUM       = :REMAIN_SUM
                        WHERE  ADD_RETENTION_ID = :ADD_RETENTION_ID", Connect.SchemaSalary), Connect.CurConnect);
            ar_adapter.UpdateCommand.BindByName = true;
            ar_adapter.UpdateCommand.Parameters.Add("ADD_RETENTION_ID", OracleDbType.Decimal, 0, "ADD_RETENTION_ID");
            ar_adapter.UpdateCommand.Parameters.Add("TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            ar_adapter.UpdateCommand.Parameters.Add("PAY_TYPE_ID", OracleDbType.Decimal, 0, "PAY_TYPE_ID");
            ar_adapter.UpdateCommand.Parameters.Add("ORDER_NUMBER", OracleDbType.Decimal, 0, "ORDER_NUMBER");
            ar_adapter.UpdateCommand.Parameters.Add("ORIGINAL_SUM", OracleDbType.Decimal, 0, "ORIGINAL_SUM");
            ar_adapter.UpdateCommand.Parameters.Add("RETENT_PERCENT", OracleDbType.Decimal, 0, "RETENT_PERCENT");
            ar_adapter.UpdateCommand.Parameters.Add("RETENT_SUM", OracleDbType.Decimal, 0, "RETENT_SUM");
            ar_adapter.UpdateCommand.Parameters.Add("REMAIN_SUM", OracleDbType.Decimal, 0, "REMAIN_SUM");

            ar_adapter.DeleteCommand = new OracleCommand(string.Format(@"DELETE FROM {0}.ADD_RETENTION where ADD_RETENTION_ID = :ADD_RETENTION_ID", Connect.SchemaSalary), Connect.CurConnect);
            ar_adapter.DeleteCommand.BindByName = true;
            ar_adapter.DeleteCommand.Parameters.Add("ADD_RETENTION_ID", OracleDbType.Decimal, 0, "ADD_RETENTION_ID");

            new OracleDataAdapter(string.Format(Queries.GetQuery("GetListEmp.sql"), Connect.SchemaApstaff), Connect.CurConnect).Fill(ds, "ListEmp");
            _ListEmp = new DataView(ds.Tables["ListEmp"],"","", DataViewRowState.CurrentRows);
            gridAddRetent.DataContext = _ListEmp;
            SubdivSelector1.SubdivChanged += new RoutedEventHandler(FillTableAddRet);
        }
        private DataTable AddRet
        {
            get { return ds.Tables["AddRet"]; }
        }
        private void FillTableAddRet(object sender, RoutedEventArgs e)
        {
            ar_adapter.SelectCommand.Parameters["p_SUBDIV_ID"].Value = SubdivSelector1.SubdivId;
            ar_adapter.SelectCommand.Parameters["p_PER_NUM"].Value = PER_NUM.Text.Trim();
            if (ds.Tables.Contains("AddRet"))
            {
                AddRet.Rows.Clear();
                ar_adapter.Fill(ds, "AddRet");
            }
            else
            {
                ar_adapter.Fill(ds, "AddRet");
                gridAddRetent.ItemsSource = new DataView(AddRet, "", "", DataViewRowState.CurrentRows);
            }
        }
        

        private void SaveAddRetention_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && ds.HasChanges();
        }

        private void SaveAddRetentionCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ds.HasChanges() && MessageBox.Show("Сохранить изменения?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                foreach (DataRow r in AddRet.Rows)
                    if (r.RowState!= DataRowState.Deleted && r["ADD_RETENTION_ID"]==DBNull.Value)
                    {
                        r["ADD_RETENTION_ID"] = new OracleCommand(string.Format("select {0}.ADD_RETENTION_ID_SEQ.NEXTVAL from dual", Connect.SchemaSalary), Connect.CurConnect).ExecuteScalar();
                    }
                OracleTransaction tr = Connect.CurConnect.BeginTransaction();
                ar_adapter.InsertCommand.Transaction = ar_adapter.UpdateCommand.Transaction = ar_adapter.DeleteCommand.Transaction = tr;
                try
                {
                    ar_adapter.Update(AddRet);
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show("Ошибка сохранения " + ex.Message);
                }
            }

        }
        private void AddAdditCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }
        private void AddAdditCommand_Executed(object sender, RoutedEventArgs e)
        {
            AddRet.Rows.InsertAt(AddRet.NewRow(), (gridAddRetent.SelectedIndex>-1?gridAddRetent.SelectedIndex:0));
        }

        private void DeleteAdditRetention_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && gridAddRetent!=null && gridAddRetent.SelectedItem!=null;
        }

        private void DeleteAdditRetention_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (gridAddRetent.SelectedItem as DataRowView).Delete();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FillTableAddRet(this, new RoutedEventArgs());
        }
        private void WrapPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FillTableAddRet(this, new RoutedEventArgs());
            }
        }

        private void Reject_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ds.HasChanges();
        }

        private void Reject_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить все несохраненные изменения?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ds.RejectChanges();
            }
        }

        private void LoadAddRetentToFromFile_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }

        private void LoadAddRetentToFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Сформировать файл-справочник дополнительных удержания для расчета ЗП?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                try
                {
                    FileStream f = new FileStream(Settings.Default.PathAdditRetentFile, FileMode.Create, FileAccess.ReadWrite);
                    StreamWriter sw = new StreamWriter(f);
                    OracleDataAdapter a = new OracleDataAdapter(string.Format("begin {0}.ADDIT_RETENT_FOR_FILE(:p_data); end;", Connect.SchemaSalary), Connect.CurConnect);
                    a.SelectCommand.Parameters.Add("p_data", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    DataTable t = new DataTable();
                    a.Fill(t);
                    foreach (DataRow r in t.Rows)
                    {
                        sw.WriteLine(r[0].ToString());
                    }
                    sw.Flush();
                    f.Close();
                    MessageBox.Show("Файл успешно сформирован!", "", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка формирования файла: " + ex.Message);
                }
 
            }
        }

        private void LoadAddRetentFromFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
             
        }

    }
}
