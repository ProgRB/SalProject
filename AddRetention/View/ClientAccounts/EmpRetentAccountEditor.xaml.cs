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
using System.Diagnostics;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for EmpRetentAccountEditor.xaml
    /// </summary>
    public partial class EmpRetentAccountEditor : Window
    {
        
        public EmpRetentAccountEditor(object p_transfer_id)
        {
            InitializeComponent();
            Model.TransferID = (decimal)p_transfer_id;
            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Model_PropertyChanged);
            this.Loaded+=new RoutedEventHandler(EmpRetentAccountEditor_Loaded);
        }

        void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsArchiv")
            {
                EmpRetentAccountEditor_Loaded(this, null);
            }
        }

        void  EmpRetentAccountEditor_Loaded(object sender, RoutedEventArgs e)
        {
            (this.FindResource("SalaryModel") as ObjectDataProvider).Refresh();
            (this.FindResource("AdvanceModel") as ObjectDataProvider).Refresh();
            (this.FindResource("OtherModel") as ObjectDataProvider).Refresh();
            //ScrollLast(listViewSalary);
            //ScrollLast(listViewAdvance);
            //ScrollLast(listViewOther);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        public EmpRetentAccountModel Model
        {
            get
            {
                return (EmpRetentAccountModel)this.FindResource("Model");
            }
        }

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command)  && e.Parameter != null;
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddNewRetent(e.Parameter.ToString());
            switch (e.Parameter.ToString())
            {
                case "287": (this.FindResource("SalaryModel") as ObjectDataProvider).Refresh(); ScrollLast(listViewSalary); break;
                case "487": (this.FindResource("AdvanceModel") as ObjectDataProvider).Refresh(); ScrollLast(listViewAdvance); break;
                case "488": (this.FindResource("OtherModel") as ObjectDataProvider).Refresh(); ScrollLast(listViewOther); break;
            }
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && e.Parameter!=null && e.Parameter!=null;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView r = e.Parameter as DataRowView;
            string Code_payment = AppDataSet.Tables["PAYMENT_TYPE"].Select(string.Format("PAYMENT_TYPE_ID={0}", r["PAYMENT_TYPE_ID"])).Select(t=>t["CODE_PAYMENT"].ToString()).FirstOrDefault();
            Model.DeleteRetent(r);
            switch (Code_payment)
            {
                case "287": (this.FindResource("SalaryModel") as ObjectDataProvider).Refresh(); break;
                case "487": (this.FindResource("AdvanceModel") as ObjectDataProvider).Refresh(); break;
                case "488": (this.FindResource("OtherModel") as ObjectDataProvider).Refresh(); break;
            }
        }

        private void ScrollLast(ListView l)
        {
            if (l != null && l.Items.Count > 0)
                l.ScrollIntoView(l.Items[l.Items.Count - 1]);
        }

        private void Replace_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView r = e.Parameter as DataRowView;
            string Code_payment = AppDataSet.Tables["PAYMENT_TYPE"].Select(string.Format("PAYMENT_TYPE_ID={0}", r["PAYMENT_TYPE_ID"])).Select(t=>t["CODE_PAYMENT"].ToString()).FirstOrDefault();
            Model.ReplaceRetent(r);
            switch (Code_payment)
            {
                case "287": (this.FindResource("SalaryModel") as ObjectDataProvider).Refresh(); break;
                case "487": (this.FindResource("AdvanceModel") as ObjectDataProvider).Refresh(); break;
                case "488": (this.FindResource("OtherModel") as ObjectDataProvider).Refresh(); break;
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command)
                && Model.HasChanges;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Model.Save())
            {
                this.DialogResult = true;
                Close();
            }
        }

        private void AddRelation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && e.Parameter != null;
        }

        private void AddRelation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRow r = (e.Parameter as DataRowView).Row;
            Model.AddNewRelation(r);
        }

        private void DeleteRelation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && e.Parameter != null;
        }

        private void DeleteRelation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.DeleteRelation((e.Parameter as DataRowView).Row);
        }
    }

    public class EmpRetentAccountModel : NotificationObject
    {
        DataSet ds;
        private decimal? _transferID;
        Dictionary<string, decimal> payment_index;
        private bool is_loaded = false;
        int k = -1;
        public int NextSeq()
        {
            return k--;
        }
        
        // public CONSTRUCTOR
        public EmpRetentAccountModel()
        {
            try
            {
                if (AppDataSet.Tables != null)
                    payment_index = (from p in AppDataSet.Tables["PAYMENT_TYPE"].Rows.OfType<DataRow>()
                                     select new { CodePayment = p["CODE_PAYMENT"].ToString(), PaymentTypeID = p.Field2<Decimal>("PAYMENT_TYPE_ID") })
                            .ToDictionary(t => t.CodePayment, t => t.PaymentTypeID);
                is_loaded = true;
                
                odaSave = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectEmpRetentAccountData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                odaSave.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, -1, ParameterDirection.Input);
                odaSave.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                odaSave.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
                odaSave.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
                odaSave.SelectCommand.BindByName = true;
                odaSave.TableMappings.Add("Table", "RETENTION");
                odaSave.TableMappings.Add("Table1", "CLIENT_RETENT_RELATION");
                odaSave.TableMappings.Add("Table2", "CLIENT_ACCOUNT");

                odaSave.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.RETENTION_UPDATE(:p_RETENTION_ID,:p_TRANSFER_ID,:p_PAYMENT_TYPE_ID,:p_ORDER_NUMBER,:p_ORIGINAL_SUM,:p_RETENT_PERCENT,:p_RETENT_SUM,:p_REMAIN_SUM,:p_DATE_ADD,:p_DATE_START_RET,:p_DATE_END_RET,:p_SALARY_DOC_ID,:p_POST_TRANSFER_SIGN,:p_DATE_REM_CALC);end;", Connect.SchemaSalary), Connect.CurConnect);
                odaSave.InsertCommand.BindByName = true;
                odaSave.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                odaSave.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").Direction = ParameterDirection.InputOutput;
                odaSave.InsertCommand.Parameters["p_RETENTION_ID"].DbType = DbType.Decimal;
                odaSave.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
                odaSave.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input;
                odaSave.InsertCommand.Parameters.Add("p_ORDER_NUMBER", OracleDbType.Decimal, 0, "ORDER_NUMBER").Direction = ParameterDirection.Input;
                odaSave.InsertCommand.Parameters.Add("p_ORIGINAL_SUM", OracleDbType.Decimal, 0, "ORIGINAL_SUM").Direction = ParameterDirection.Input;
                odaSave.InsertCommand.Parameters.Add("p_RETENT_PERCENT", OracleDbType.Decimal, 0, "RETENT_PERCENT").Direction = ParameterDirection.Input;
                odaSave.InsertCommand.Parameters.Add("p_RETENT_SUM", OracleDbType.Decimal, 0, "RETENT_SUM").Direction = ParameterDirection.Input;
                odaSave.InsertCommand.Parameters.Add("p_REMAIN_SUM", OracleDbType.Decimal, 0, "REMAIN_SUM").Direction = ParameterDirection.Input;
                odaSave.InsertCommand.Parameters.Add("p_DATE_ADD", OracleDbType.Date, 0, "DATE_ADD").Direction = ParameterDirection.Input;
                odaSave.InsertCommand.Parameters.Add("p_DATE_START_RET", OracleDbType.Date, 0, "DATE_START_RET").Direction = ParameterDirection.Input;
                odaSave.InsertCommand.Parameters.Add("p_DATE_END_RET", OracleDbType.Date, 0, "DATE_END_RET").Direction = ParameterDirection.Input;
                odaSave.InsertCommand.Parameters.Add("p_SALARY_DOC_ID", OracleDbType.Decimal, 0, "SALARY_DOC_ID").Direction = ParameterDirection.Input;
                odaSave.InsertCommand.Parameters.Add("p_POST_TRANSFER_SIGN", OracleDbType.Decimal, 0, "POST_TRANSFER_SIGN").Direction = ParameterDirection.Input;
                odaSave.InsertCommand.Parameters.Add("p_DATE_REM_CALC", OracleDbType.Date, 0, "DATE_REM_CALC").Direction = ParameterDirection.Input;

                odaSave.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.RETENTION_UPDATE(:p_RETENTION_ID,:p_TRANSFER_ID,:p_PAYMENT_TYPE_ID,:p_ORDER_NUMBER,:p_ORIGINAL_SUM,:p_RETENT_PERCENT,:p_RETENT_SUM,:p_REMAIN_SUM,:p_DATE_ADD,:p_DATE_START_RET,:p_DATE_END_RET,:p_SALARY_DOC_ID,:p_POST_TRANSFER_SIGN,:p_DATE_REM_CALC);end;", Connect.SchemaSalary), Connect.CurConnect);
                odaSave.UpdateCommand.BindByName = true;
                odaSave.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
                odaSave.UpdateCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").Direction = ParameterDirection.InputOutput;
                odaSave.UpdateCommand.Parameters["p_RETENTION_ID"].DbType = DbType.Decimal;
                odaSave.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
                odaSave.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input;
                odaSave.UpdateCommand.Parameters.Add("p_ORDER_NUMBER", OracleDbType.Decimal, 0, "ORDER_NUMBER").Direction = ParameterDirection.Input;
                odaSave.UpdateCommand.Parameters.Add("p_ORIGINAL_SUM", OracleDbType.Decimal, 0, "ORIGINAL_SUM").Direction = ParameterDirection.Input;
                odaSave.UpdateCommand.Parameters.Add("p_RETENT_PERCENT", OracleDbType.Decimal, 0, "RETENT_PERCENT").Direction = ParameterDirection.Input;
                odaSave.UpdateCommand.Parameters.Add("p_RETENT_SUM", OracleDbType.Decimal, 0, "RETENT_SUM").Direction = ParameterDirection.Input;
                odaSave.UpdateCommand.Parameters.Add("p_REMAIN_SUM", OracleDbType.Decimal, 0, "REMAIN_SUM").Direction = ParameterDirection.Input;
                odaSave.UpdateCommand.Parameters.Add("p_DATE_ADD", OracleDbType.Date, 0, "DATE_ADD").Direction = ParameterDirection.Input;
                odaSave.UpdateCommand.Parameters.Add("p_DATE_START_RET", OracleDbType.Date, 0, "DATE_START_RET").Direction = ParameterDirection.Input;
                odaSave.UpdateCommand.Parameters.Add("p_DATE_END_RET", OracleDbType.Date, 0, "DATE_END_RET").Direction = ParameterDirection.Input;
                odaSave.UpdateCommand.Parameters.Add("p_SALARY_DOC_ID", OracleDbType.Decimal, 0, "SALARY_DOC_ID").Direction = ParameterDirection.Input;
                odaSave.UpdateCommand.Parameters.Add("p_POST_TRANSFER_SIGN", OracleDbType.Decimal, 0, "POST_TRANSFER_SIGN").Direction = ParameterDirection.Input;
                odaSave.UpdateCommand.Parameters.Add("p_DATE_REM_CALC", OracleDbType.Date, 0, "DATE_REM_CALC").Direction = ParameterDirection.Input;

                odaSave.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.RETENTION_DELETE(:p_RETENTION_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
                odaSave.DeleteCommand.BindByName = true;
                odaSave.DeleteCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").Direction = ParameterDirection.InputOutput;

                odaSave.AcceptChangesDuringUpdate = false;

                odaSaveRelation = new OracleDataAdapter("", Connect.CurConnect);

                odaSaveRelation.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.CLIENT_RETENT_RELATION_UPDATE(:p_CLIENT_RETENT_RELATION_ID,:p_RETENTION_ID,:p_CLIENT_ACCOUNT_ID,:p_DATE_BEGIN_RELATION,:p_DATE_END_RELATION);end;", Connect.SchemaSalary), Connect.CurConnect);
                odaSaveRelation.InsertCommand.BindByName = true;
                odaSaveRelation.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
                odaSaveRelation.InsertCommand.Parameters.Add("p_CLIENT_RETENT_RELATION_ID", OracleDbType.Decimal, 0, "CLIENT_RETENT_RELATION_ID").Direction = ParameterDirection.InputOutput;
                odaSaveRelation.InsertCommand.Parameters["p_CLIENT_RETENT_RELATION_ID"].DbType = DbType.Decimal;
                odaSaveRelation.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").Direction = ParameterDirection.Input;
                odaSaveRelation.InsertCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.Input;
                odaSaveRelation.InsertCommand.Parameters.Add("p_DATE_BEGIN_RELATION", OracleDbType.Date, 0, "DATE_BEGIN_RELATION").Direction = ParameterDirection.Input;
                odaSaveRelation.InsertCommand.Parameters.Add("p_DATE_END_RELATION", OracleDbType.Date, 0, "DATE_END_RELATION").Direction = ParameterDirection.Input;

                odaSaveRelation.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.CLIENT_RETENT_RELATION_UPDATE(:p_CLIENT_RETENT_RELATION_ID,:p_RETENTION_ID,:p_CLIENT_ACCOUNT_ID,:p_DATE_BEGIN_RELATION,:p_DATE_END_RELATION);end;", Connect.SchemaSalary), Connect.CurConnect);
                odaSaveRelation.UpdateCommand.BindByName = true;
                odaSaveRelation.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                odaSaveRelation.UpdateCommand.Parameters.Add("p_CLIENT_RETENT_RELATION_ID", OracleDbType.Decimal, 0, "CLIENT_RETENT_RELATION_ID").Direction = ParameterDirection.InputOutput;
                odaSaveRelation.UpdateCommand.Parameters["p_CLIENT_RETENT_RELATION_ID"].DbType = DbType.Decimal;
                odaSaveRelation.UpdateCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").Direction = ParameterDirection.Input;
                odaSaveRelation.UpdateCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.Input;
                odaSaveRelation.UpdateCommand.Parameters.Add("p_DATE_BEGIN_RELATION", OracleDbType.Date, 0, "DATE_BEGIN_RELATION").Direction = ParameterDirection.Input;
                odaSaveRelation.UpdateCommand.Parameters.Add("p_DATE_END_RELATION", OracleDbType.Date, 0, "DATE_END_RELATION").Direction = ParameterDirection.Input;

                odaSaveRelation.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.CLIENT_RETENT_RELATION_DELETE(:p_CLIENT_RETENT_RELATION_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
                odaSaveRelation.DeleteCommand.BindByName = true;
                odaSaveRelation.DeleteCommand.Parameters.Add("p_CLIENT_RETENT_RELATION_ID", OracleDbType.Decimal, 0, "CLIENT_RETENT_RELATION_ID").Direction = ParameterDirection.InputOutput;
                odaSaveRelation.AcceptChangesDuringUpdate = false;
            }
            catch { };

        }
        
        public decimal? TransferID
        {
            get
            {
                return _transferID;
            }
            set
            {
                _transferID = value;
                RaisePropertyChanged(() => TransferID);
                LoadRetention(TransferID);
            }
        }

        bool _isArchiv = false;
        public bool IsArchiv
        {
            get
            {
                return _isArchiv;
            }
            set
            {
                _isArchiv = value;
                RaisePropertyChanged(() => IsArchiv);
            }
        }
        DateTime? selectedDate = DateTime.Now;
        public DateTime? SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                selectedDate = value;
                RaisePropertyChanged(() => SelectedDate);
            }
        }
        public List<DataRowView> GetView(string CodePayment)
        {
            if (is_loaded)
            {
                List<DataRowView> l;
                if (IsArchiv)
                    l= new DataView(Retention, string.Format("PAYMENT_TYPE_ID={0}", payment_index[CodePayment]),
                                        "DATE_START_RET, DATE_END_RET, ORDER_NUMBER", DataViewRowState.CurrentRows).OfType<DataRowView>().ToList();
                else
                    l = new DataView(Retention, string.Format("PAYMENT_TYPE_ID={0} and ISNULL(DATE_START_RET,#1/1/1900#)<=#{1}# and ISNULL(DATE_END_RET,#1/1/3000#)>=#{2}#", payment_index[CodePayment],
                                        SelectedDate.Value.Trunc("Month").ToString("M/d/yyyy"), SelectedDate.Value.Trunc("Month").AddMonths(1).AddSeconds(-1).ToString("M/d/yyyy")),
                                        "ORDER_NUMBER, DATE_START_RET, DATE_END_RET", DataViewRowState.CurrentRows).OfType<DataRowView>().ToList();
                return l;
            }
            else
            {
                DataTable t = new DataTable();
                t.Columns.AddRange(new DataColumn[] { new DataColumn("DATE_START_RET", typeof(DateTime)), new DataColumn("DATE_END_RET", typeof(DateTime)) });
                return new List<DataRowView>();
            }
        }

        public DataTable Retention
        { 
            get
            {
                if (ds==null)
                {
                    ds = new DataSet();
                    LoadRetention(TransferID);
                }
                return ds.Tables["RETENTION"];
            }
        }

        public DataTable ClientRelation
        {
            get
            {
                if (ds == null)
                {
                    ds = new DataSet();
                    LoadRetention(TransferID);
                }
                return ds.Tables["CLIENT_RETENT_RELATION"];
            }
        }

        public DataView EmpAccounts
        {
            get
            {
                if (ds == null)
                {
                    ds = new DataSet();
                    LoadRetention(TransferID);
                }
                if (is_loaded)
                    return new DataView(ds.Tables["CLIENT_ACCOUNT"], "", "NUMBER_CARD, NUMBER_ACCOUNT, BANK_NAME", DataViewRowState.CurrentRows);
                else
                    return null;
            }
        }

        OracleDataAdapter odaSave, odaSaveRelation;
        public void LoadRetention(object transfer_id)
        {
            
            odaSave.SelectCommand.Parameters["p_transfer_id"].Value = transfer_id;
            try
            {
                is_loaded = false;
                odaSave.Fill(ds);
                if (ClientRelation.Constraints.Count == 0) // добавлю связь, чтобы получать дочерние элементы
                {
                    ForeignKeyConstraint fk_relation = new ForeignKeyConstraint("retention_fk_key", Retention.Columns["RETENTION_ID"], ClientRelation.Columns["RETENTION_ID"]);
                    fk_relation.DeleteRule = Rule.Cascade;
                    fk_relation.UpdateRule = Rule.Cascade;
                    ClientRelation.Constraints.Add(fk_relation);
                    ds.EnforceConstraints = true;
                    ds.Tables["RETENTION"].ColumnChanged += new DataColumnChangeEventHandler(EmpRetentAccountModel_ColumnChanged);
                    ds.Relations.Add("retention_fk", Retention.Columns["RETENTION_ID"], ClientRelation.Columns["RETENTION_ID"]);
                    ds.Tables["RETENTION"].Columns.Add("IS_CURRENT", typeof(decimal)).Expression = string.Format("IIF(ISNULL(DATE_START_RET,#1/1/1900#)<=#{0}# and ISNULL(DATE_END_RET,#1/1/3000#)>=#{1}#, 1,0)",
                                        SelectedDate.Value.Trunc("Month").ToString("M/d/yyyy"), SelectedDate.Value.Trunc("Month").AddMonths(1).AddSeconds(-1).ToString("M/d/yyyy"));
                    ds.Tables["RETENTION"].Columns.Add("IS_SELECTED", typeof(Decimal));
                }

                if (transfer_id != null && Retention.Rows.Count == 0)
                {
                    AddNewRetent("287");
                    AddNewRetent("487");
                    AddNewRetent("488");
                }
                is_loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
            }
        }

        public DataRow SelectedItem(string CodePayment)
        {
            return Retention.Rows.OfType<DataRow>().Where(t => t.RowState!= DataRowState.Deleted && t.RowState!= DataRowState.Detached && t.Field2<Decimal?>("IS_SELECTED") == 1 && t.Field2<Decimal?>("PAYMENT_TYPE_ID") == payment_index[CodePayment]).FirstOrDefault();
        }

        void EmpRetentAccountModel_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (is_loaded)
            {
                if (e.Column.ColumnName.ToUpper() == "DATE_END_RET")
                {
                    DataRow[] list = e.Row.GetChildRows("retention_fk");
                    DataRow last_row = list.Where(t=>t["DATE_END_RELATION"] == DBNull.Value).FirstOrDefault();
                    if (last_row != null)
                        last_row["DATE_END_RELATION"] = e.ProposedValue;
                }
            }
        }

        public void AddNewRetent(string CodePayment)
        {
            DataRow r = Retention.NewRow();
            int k =this.NextSeq();
            r["RETENTION_ID"] = k;
            r["DATE_START_RET"] = SelectedDate.Value.Trunc("month");
            r["RETENT_PERCENT"] = 100;
            r["PAYMENT_TYPE_ID"] = payment_index[CodePayment];
            r["ORDER_NUMBER"] = GetView(CodePayment).Max(t=>t.Row.Field2<Decimal?>("ORDER_NUMBER"))??0+1;
            r["TRANSFER_ID"] = TransferID;
            Retention.Rows.Add(r);
            AddedItems.Add(r);
            DataRow cl_row = ClientRelation.NewRow();
            cl_row["RETENTION_ID"] = k;
            cl_row["DATE_BEGIN_RELATION"] = r["DATE_START_RET"];
            ClientRelation.Rows.Add(cl_row);
        }

        public void ReplaceRetent(DataRowView old_row)
        {
            ReplaceRetent(old_row.Row);
        }
        public void ReplaceRetent(DataRow old_row)
        {
            DataRow r1 = old_row;
            if (r1 != null)
            {
                if ((r1.Field2<DateTime?>("DATE_START_RET") ?? DateTime.MinValue) > SelectedDate.Value.Trunc("Month").AddSeconds(-1))
                {
                    MessageBox.Show("Невозможно заменить удержание, потому что оно началось в текущем месяце", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                r1["DATE_END_RET"] = SelectedDate.Value.Trunc("Month").AddSeconds(-1);
                int k = this.NextSeq();
                DataRow r = Retention.NewRow();
                r["RETENTION_ID"] = k;
                r["DATE_START_RET"] = SelectedDate.Value.Trunc("month");
                r["PAYMENT_TYPE_ID"] = r1["PAYMENT_TYPE_ID"];
                r["ORDER_NUMBER"] = r1["ORDER_NUMBER"];
                r["TRANSFER_ID"] = TransferID;
                Retention.Rows.Add(r);
                AddedItems.Add(r);
                DataRow cl_row = ClientRelation.NewRow();
                cl_row["RETENTION_ID"] = k;
                cl_row["DATE_BEGIN_RELATION"] = r["DATE_START_RET"];
                ClientRelation.Rows.Add(cl_row);
            }
        }
        public void DeleteRetent(DataRowView old_row)
        {
            DeleteRetent(old_row.Row);
        }
        public void DeleteRetent(DataRow r)
        {
            r.Delete();
        }

        public bool Save()
        {
            var first_null = Retention.Select("DATE_START_RET>DATE_END_RET").FirstOrDefault();
            if (first_null != null)
            {
                DataRow fs = first_null as DataRow;
                string c_payment = AppDataSet.Tables["PAYMENT_TYPE"].Compute("MAX(CODE_PAYMENT)", "PAYMENT_TYPE_ID="+fs["PAYMENT_TYPE_ID"]).ToString();
                string error_text;
                switch (c_payment)
                {
                    case "287": error_text = "Перечисление ЗП"; break;
                    case "487": error_text = "Перечисление аванаса";break;
                    default: error_text = "Перечисление прочие";break;
                }
                MessageBox.Show(string.Format("Дата начала удержания должна быть меньше даты окончания удержания ({0})", error_text), "Ошибка данных");
                return false;
            }

            var first_null1 = ClientRelation.Select("IsNull(CLIENT_ACCOUNT_ID,-1)=-1", "", DataViewRowState.CurrentRows).FirstOrDefault();
            if (first_null1 != null)
            {
                DataRow fs = (first_null1 as DataRow).GetParentRow("retention_fk", DataRowVersion.Current);
                string c_payment = AppDataSet.Tables["PAYMENT_TYPE"].Compute("MAX(CODE_PAYMENT)", "PAYMENT_TYPE_ID=" + fs["PAYMENT_TYPE_ID"]).ToString();
                string error_text;
                switch (c_payment)
                {
                    case "287": error_text = "Перечисление ЗП"; break;
                    case "487": error_text = "Перечисление аванаса"; break;
                    default: error_text = "Перечисление прочие"; break;
                }
                MessageBox.Show(string.Format("Не выбран счет для перечисления ({0})", error_text), "Ошибка данных");
                return false;
            }

            var first_null2 = ClientRelation.Select("DATE_BEGIN_RELATION>DATE_END_RELATION", "", DataViewRowState.CurrentRows).FirstOrDefault();
            if (first_null2 != null)
            {
                DataRow fs = (first_null2 as DataRow).GetParentRow("retention_fk", DataRowVersion.Current);
                string c_payment = AppDataSet.Tables["PAYMENT_TYPE"].Compute("MAX(CODE_PAYMENT)", "PAYMENT_TYPE_ID=" + fs["PAYMENT_TYPE_ID"]).ToString();
                string error_text;
                switch (c_payment)
                {
                    case "287": error_text = "Перечисление ЗП"; break;
                    case "487": error_text = "Перечисление аванаса"; break;
                    default: error_text = "Перечисление прочие"; break;
                }
                MessageBox.Show(string.Format("Дата начала ПЕРЕЧИСЛЕНИЯ должна быть меньше даты окончания перечисления ({0})", error_text), "Ошибка данных");
                return false;
            }

            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                foreach (DataRow r in AddedItems)
                {
                    if ((r.RowState == DataRowState.Added || r.RowState == DataRowState.Modified) && r.Field2<Decimal?>("retention_id") > 0)
                    {
                        r["RETENTION_ID"] = NextSeq();
                        Array.ForEach(r.GetChildRows("retention_fk").ToArray(), (p) => { p["CLIENT_RETENT_RELATION_ID"] = DBNull.Value; });
                    }
                }
                odaSave.Update(Retention);
                odaSaveRelation.Update(ClientRelation);
                tr.Commit();
                Retention.AcceptChanges();
                ClientRelation.AcceptChanges();
                RaisePropertyChanged(() => IsArchiv);
                AddedItems.Clear();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Warning);
                RaisePropertyChanged(() => IsArchiv);
                return false;
            }
        }

        public void AddNewRelation(DataRow r)
        {
            if (r.RowState != DataRowState.Detached && r.RowState != DataRowState.Deleted)
            {
                Array.ForEach(r.GetChildRows("retention_fk", DataRowVersion.Current).Where(t => t["DATE_END_RELATION"] == DBNull.Value 
                    || t.Field2<DateTime>("DATE_END_RELATION") >= SelectedDate.Value.Trunc("Month")).ToArray(), 
                    (DataRow e) => { e["DATE_END_RELATION"] = SelectedDate.Value.Trunc("Month").AddSeconds(-1); });
                DataRow new_row = ClientRelation.NewRow();
                new_row["RETENTION_ID"] = r["RETENTION_ID"];
                new_row["DATE_BEGIN_RELATION"] = SelectedDate.Value.Trunc("Month");
                ClientRelation.Rows.Add(new_row);
            }
        }

        public void DeleteRelation(DataRow r)
        {
            r.Delete();
        }

        public bool HasChanges
        {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }

        private List<DataRow> AddedItems = new List<DataRow>();
    }
    
}
