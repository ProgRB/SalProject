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
using System.Data.Common;
using Salary.ViewModel;
using Salary.Helpers;
using Salary.Model;
using System.Reflection;
using System.ComponentModel;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for AlimonyEditor.xaml
    /// </summary>
    public partial class ClientAccountEditor : Window
    {
        public ClientAccountEditor(object transfer_id, object client_account_id=null)
        {
            _clientAccount = new ClientAccountModel((decimal?)transfer_id, (decimal?)client_account_id);            
            InitializeComponent();
            gbFIO.DataContext = ClientAccount.DataSet.Tables["EMP_DATA"].DefaultView[0];
            this.DataContext = ClientAccount;
        }

        ClientAccountModel _clientAccount;
        public ClientAccountModel ClientAccount
        {
            get
            {
                return _clientAccount;
            }
        }

        public object SelectedClientAccountID
        {
            get
            {
                return ClientAccount.ClientAccountID;
            }
        }

        private void SaveClientAccount_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && ClientAccount.HasChanges && !ValidationHelper.IsFrameworkElementsHasErrors(new DependencyObject[]{gbFIO, gbAlimonyData});
        }

        private void ChangeClientAccountEmp_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void CommandSaveClientAccount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ClientAccount.Save())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void CommandChangeClientAccountEmp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EmpFinder f = new EmpFinder();
            f.Owner= Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                ClientAccount.TransferID = f.SelectedItem.Row.Field<Decimal?>("TRANSFER_ID");
                ClientAccount.DataSet.Tables["EMP_DATA"].Rows[0]["FIO"] = string.Format("{0} {1} {2}", f.SelectedItem["EMP_LAST_NAME"], f.SelectedItem["EMP_FIRST_NAME"], f.SelectedItem["EMP_MIDDLE_NAME"]);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Array.TrueForAll(e.Text.ToArray(), r=>Char.IsDigit(r));
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private static List<DataColumnMapping> LoadPerDataMapping = 
             new List<DataColumnMapping>(new DataColumnMapping[]{ new DataColumnMapping("EMP_LAST_NAME", "OwnerFamily"), new DataColumnMapping("EMP_FIRST_NAME", "OwnerName"),
                    new DataColumnMapping("EMP_MIDDLE_NAME", "OwnerMiddleName"), new DataColumnMapping("NUM_PASSPORT", "PassportNumber"),
                    new DataColumnMapping("SERIA_PASSPORT", "PassportSeries"), new DataColumnMapping("WHO_GIVEN", "GetPlace"),
                    new DataColumnMapping("CITY_BIRTH", "GetCity"),
             new DataColumnMapping("INSURANCE_NUM", "InsuranceNum"),
             new DataColumnMapping("WHEN_GIVEN", "DateDoc"),
             new DataColumnMapping("TYPE_PER_DOC_ID", "CodeDoc")});

        private void LoadPerData_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OracleDataAdapter cmd = new OracleDataAdapter(string.Format("begin {0}.GET_PER_DATA_FOR_ACCOUNT(:p_transfer_id, :c);end;", Connect.SchemaSalary), Connect.CurConnect);
                cmd.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, ClientAccount.TransferID, ParameterDirection.Input);
                cmd.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                DataTable t = new DataTable();
                cmd.Fill(t);
                foreach (DataColumnMapping map in LoadPerDataMapping)
                {
                    PropertyInfo c = ClientAccount.GetType().GetProperty(map.DataSetColumn);
                    c.SetValue(ClientAccount, (t.Rows[0][map.SourceColumn]==DBNull.Value? null: t.Rows[0][map.SourceColumn]), null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных сотрудника");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.RemovedItems.Count==0)
            {
                DataRowView r = e.AddedItems[0] as DataRowView;
                if (r != null && r["TYPE_ACCOUNT_ID"].ToString()=="1")
                    AppCommands.LoadPerDataToAccount.Execute(this, null);
            }
        }        
    }

    public class ClientAccountModel : ClientAccount, IDataErrorInfo
    {
        OracleDataAdapter odaClient_Account;
        DataSet ds;
        Lookup<decimal, decimal> bank_account_mapping;

        public ClientAccountModel(decimal? transfer_id, decimal? client_account_id)
        {
            ds = new System.Data.DataSet();
            odaClient_Account = new OracleDataAdapter(string.Format(Queries.GetQuery(@"SelectClientAccountData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaClient_Account.SelectCommand.BindByName = true;
            odaClient_Account.SelectCommand.Parameters.Add("p_client_account_id", OracleDbType.Decimal, client_account_id, ParameterDirection.Input);
            odaClient_Account.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, transfer_id, ParameterDirection.Input);
            odaClient_Account.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaClient_Account.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaClient_Account.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaClient_Account.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            odaClient_Account.SelectCommand.Parameters.Add("c5", OracleDbType.RefCursor, ParameterDirection.Output);
            odaClient_Account.SelectCommand.Parameters.Add("c6", OracleDbType.RefCursor, ParameterDirection.Output);
            odaClient_Account.TableMappings.Add("Table", "CLIENT_ACCOUNT");
            odaClient_Account.TableMappings.Add("Table1", "TYPE_BANK");
            odaClient_Account.TableMappings.Add("Table2", "TYPE_ACCOUNT");
            odaClient_Account.TableMappings.Add("Table3", "EMP_DATA");
            odaClient_Account.TableMappings.Add("Table4", "TYPE_PER_DOC");
            odaClient_Account.TableMappings.Add("Table5", "BANK_FOR_TYPE_ACCOUNT");
            odaClient_Account.Fill(ds);

            bank_account_mapping = (Lookup<decimal, decimal>)(from r in ds.Tables["BANK_FOR_TYPE_ACCOUNT"].AsEnumerable()
                                                              select r).ToLookup(p => p.Field2<decimal>("TYPE_ACCOUNT_ID"), p => p.Field2<decimal>("TYPE_BANK_ID"));

            ds.Tables["TYPE_BANK"].Columns.Add("DISP_EXP", typeof(string)).Expression = "BANK_NAME+ '(Отд№ '+ISNULL(BANK_OFFICE,'<не указано>')+')'";

#region Инициализация адаптера сохранения
            odaClient_Account.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.CLIENT_ACCOUNT_UPDATE(:p_CLIENT_ACCOUNT_ID,:p_TRANSFER_ID,:p_NUMBER_ACCOUNT,:p_TYPE_BANK_ID,:p_OWNER_NAME,:p_OWNER_FAMILY,:p_OWNER_MIDDLE_NAME,
                                :p_PASSPORT_SERIES,:p_PASSPORT_NUMBER,:p_GET_PLACE,:p_GET_CITY,:p_NUMBER_CARD,:p_PLF_NAME,:p_PLF_ADDRESS,:p_PLF_INDEX,:p_TYPE_ACCOUNT_ID,:p_INSURANCE_NUM,:p_PER_INSURANCE_NUM,
                                :p_CODE_DOC,:p_DATE_DOC, :p_COMPANY_NAME, :p_SEPARATE_SIGN);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaClient_Account.InsertCommand.BindByName = true;
            odaClient_Account.InsertCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;
            odaClient_Account.InsertCommand.Parameters["p_CLIENT_ACCOUNT_ID"].DbType = DbType.Decimal;
            odaClient_Account.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_NUMBER_ACCOUNT", OracleDbType.Varchar2, 0, "NUMBER_ACCOUNT").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_OWNER_NAME", OracleDbType.Varchar2, 0, "OWNER_NAME").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_OWNER_FAMILY", OracleDbType.Varchar2, 0, "OWNER_FAMILY").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_OWNER_MIDDLE_NAME", OracleDbType.Varchar2, 0, "OWNER_MIDDLE_NAME").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_PASSPORT_SERIES", OracleDbType.Varchar2, 0, "PASSPORT_SERIES").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_PASSPORT_NUMBER", OracleDbType.Varchar2, 0, "PASSPORT_NUMBER").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_GET_PLACE", OracleDbType.Varchar2, 0, "GET_PLACE").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_GET_CITY", OracleDbType.Varchar2, 0, "GET_CITY").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_NUMBER_CARD", OracleDbType.Varchar2, 0, "NUMBER_CARD").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_PLF_NAME", OracleDbType.Varchar2, 0, "PLF_NAME").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_PLF_ADDRESS", OracleDbType.Varchar2, 0, "PLF_ADDRESS").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_PLF_INDEX", OracleDbType.Varchar2, 0, "PLF_INDEX").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_ACCOUNT_ID").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_INSURANCE_NUM", OracleDbType.Varchar2, 0, "INSURANCE_NUM").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_PER_INSURANCE_NUM", OracleDbType.Varchar2, 0, "PER_INSURANCE_NUM").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_CODE_DOC", OracleDbType.Decimal, 0, "CODE_DOC").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_DATE_DOC", OracleDbType.Date, 0, "DATE_DOC").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_COMPANY_NAME", OracleDbType.Varchar2, 0, "COMPANY_NAME").Direction = ParameterDirection.Input;
            odaClient_Account.InsertCommand.Parameters.Add("p_SEPARATE_SIGN", OracleDbType.Decimal, 0, "SEPARATE_SIGN").Direction = ParameterDirection.Input;


            odaClient_Account.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.CLIENT_ACCOUNT_UPDATE(:p_CLIENT_ACCOUNT_ID,:p_TRANSFER_ID,:p_NUMBER_ACCOUNT,:p_TYPE_BANK_ID,:p_OWNER_NAME,:p_OWNER_FAMILY,:p_OWNER_MIDDLE_NAME,
                                :p_PASSPORT_SERIES,:p_PASSPORT_NUMBER,:p_GET_PLACE,:p_GET_CITY,:p_NUMBER_CARD,:p_PLF_NAME,:p_PLF_ADDRESS,:p_PLF_INDEX,:p_TYPE_ACCOUNT_ID,:p_INSURANCE_NUM,:p_PER_INSURANCE_NUM,
                                :p_CODE_DOC,:p_DATE_DOC, :p_COMPANY_NAME, :p_SEPARATE_SIGN);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaClient_Account.UpdateCommand.BindByName = true;
            odaClient_Account.UpdateCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;
            odaClient_Account.UpdateCommand.Parameters["p_CLIENT_ACCOUNT_ID"].DbType = DbType.Decimal;
            odaClient_Account.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_NUMBER_ACCOUNT", OracleDbType.Varchar2, 0, "NUMBER_ACCOUNT").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_OWNER_NAME", OracleDbType.Varchar2, 0, "OWNER_NAME").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_OWNER_FAMILY", OracleDbType.Varchar2, 0, "OWNER_FAMILY").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_OWNER_MIDDLE_NAME", OracleDbType.Varchar2, 0, "OWNER_MIDDLE_NAME").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_PASSPORT_SERIES", OracleDbType.Varchar2, 0, "PASSPORT_SERIES").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_PASSPORT_NUMBER", OracleDbType.Varchar2, 0, "PASSPORT_NUMBER").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_GET_PLACE", OracleDbType.Varchar2, 0, "GET_PLACE").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_GET_CITY", OracleDbType.Varchar2, 0, "GET_CITY").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_NUMBER_CARD", OracleDbType.Varchar2, 0, "NUMBER_CARD").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_PLF_NAME", OracleDbType.Varchar2, 0, "PLF_NAME").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_PLF_ADDRESS", OracleDbType.Varchar2, 0, "PLF_ADDRESS").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_PLF_INDEX", OracleDbType.Varchar2, 0, "PLF_INDEX").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_ACCOUNT_ID").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_INSURANCE_NUM", OracleDbType.Varchar2, 0, "INSURANCE_NUM").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_PER_INSURANCE_NUM", OracleDbType.Varchar2, 0, "PER_INSURANCE_NUM").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_CODE_DOC", OracleDbType.Decimal, 0, "CODE_DOC").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_DATE_DOC", OracleDbType.Date, 0, "DATE_DOC").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_COMPANY_NAME", OracleDbType.Varchar2, 0, "COMPANY_NAME").Direction = ParameterDirection.Input;
            odaClient_Account.UpdateCommand.Parameters.Add("p_SEPARATE_SIGN", OracleDbType.Decimal, 0, "SEPARATE_SIGN").Direction = ParameterDirection.Input;

            odaClient_Account.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.CLIENT_ACCOUNT_DELETE(:p_CLIENT_ACCOUNT_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaClient_Account.DeleteCommand.BindByName = true;
            odaClient_Account.DeleteCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;

#endregion

            if (ds.Tables["CLIENT_ACCOUNT"].Rows.Count == 0)
            {
                DataRow r = ds.Tables["CLIENT_ACCOUNT"].NewRow();
                ds.Tables["CLIENT_ACCOUNT"].Rows.Add(r);
                r["TRANSFER_ID"] = transfer_id;
                ds.AcceptChanges();
            }
            if (ds.Tables["EMP_DATA"].Rows.Count == 0)
            {
                ds.Tables["EMP_DATA"].Rows.Add(ds.Tables["EMP_DATA"].NewRow());
                ds.AcceptChanges();
            }
            DataRow = ds.Tables["CLIENT_ACCOUNT"].Rows[0];
        }


        /// <summary>
        /// Переопределим тип счета - требуется обновлять список банков доступных для типа счета
        /// </summary>
        public new decimal? TypeAccountID
        {
            get
            {
                return base.TypeAccountID;
            }
            set
            {
                if (value != base.TypeAccountID)
                {
                    base.TypeAccountID = value;
                    RaisePropertyChanged(() => TypeBankSource);
                }
            }
        }

        
        public DataView TypeAccountSource
        {
            get
            {
                return new DataView(DataSet.Tables["TYPE_ACCOUNT"], "", "TYPE_ACCOUNT_ID", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Источник данных для списка банков
        /// </summary>
        public Array TypeBankSource
        {
            get
            {
                var p = from r in DataSet.Tables["TYPE_BANK"].AsEnumerable()
                        where bank_account_mapping.Contains(TypeAccountID??-1) && bank_account_mapping[TypeAccountID.Value].Contains(r.Field2<Decimal>("TYPE_BANK_ID"))
                        group r by r["BANK_NAME"].ToString() into g
                        select new { TypeBankID=-1m, BankName = g.Key, Caption = g.Key, Padding = new Thickness(5, 2, 1, 2), Level = 1, Office = "000",
                            DisplayExpr = g.Key, INN= string.Empty, CustomSign="-"};
                var p2 = from r in DataSet.Tables["TYPE_BANK"].AsEnumerable()
                         where bank_account_mapping.Contains(TypeAccountID ?? -1) && bank_account_mapping[TypeAccountID.Value].Contains(r.Field2<Decimal>("TYPE_BANK_ID"))
                         select new {TypeBankID=r.Field2<Decimal>("TYPE_BANK_ID"), BankName = r.Field2<string>("BANK_NAME"), Caption = r.Field2<string>("BANK_OFFICE") + "  " + r.Field2<string>("CURRENT_ACCOUNT")
                             +"   БИК: "+r.Field2<string>("BANK_IDENT_CODE"), Padding = new Thickness(15, 2, 1, 2), Level = 2, Office=r.Field2<string>("BANK_OFFICE"),
                            DisplayExpr = r.Field2<string>("DISP_EXP"), INN=r.Field<String>("TRN"), CustomSign=(r.Field<decimal?>("CUSTOM_SIGN")==1m?"X":"-")};
                return
                    p.Union(p2).OrderBy(t=>new Tuple<string, int, string>(t.BankName, t.Level, t.Office)).ToArray();
            }
        }


        public DataView TypePerDocSource
        {
            get
            {
                return new DataView(ds.Tables["TYPE_PER_DOC"], "", "TYPE_PER_DOC_ID", DataViewRowState.CurrentRows);
            }
        }

        public bool Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                odaClient_Account.Update(ds, "CLIENT_ACCOUNT");
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException());
                return false;
            }
        }

        public bool HasChanges 
        {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }

        string IDataErrorInfo.Error
        {
            get 
            {
                if (TypeAccountID == null) return "Требуется выбрать тип счета";
                if (TypeAccount.NeedTypeBank == 1m && TypeBankID == null) return "Требуется выбрать тип банка";
                if (TypeAccount.NeedTypeBank == 1m && (NumberAccount == null && NumberCard == null)) return "Требуется номер счета или номер карты";
                return string.Empty;
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get 
            {
                switch (columnName)
                {
                    case "TypeAccountID": if (TypeAccountID == null) return "Требуется выбрать тип счета"; break;
                    case "TypeBankID": if (TypeAccount != null && TypeAccount.NeedTypeBank == 1m && TypeBankID == null) return "Требуется выбрать тип банка"; break;
                    case "NumberAccount": if (TypeAccount != null && TypeAccount.NeedTypeBank == 1m && (NumberAccount == null && NumberCard == null)) return "Укажите номер счета или карты"; break;
                    case "NumberCard": if (TypeAccount != null && TypeAccount.NeedTypeBank == 1m && (NumberAccount == null && NumberCard == null)) return "Укажите номер счета или карты"; break;
                }
                return string.Empty;
            }
        }
    }
}
