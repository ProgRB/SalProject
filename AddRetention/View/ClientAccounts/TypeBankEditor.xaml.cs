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
using Oracle.DataAccess.Client;
using System.Data;
using Salary.Helpers;
using EntityGenerator;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for TypeBankEditor.xaml
    /// </summary>
    public partial class TypeBankEditor : Window
    {
        public TypeBankEditor(decimal? type_bank_id)
        {
            Model = new TypeBankModel(type_bank_id);
            InitializeComponent();
            cbBankName.ItemsSource = Model.DistinctBankNameSource;
            DataContext = Model;
        }

        public TypeBankModel Model
        {
            get;
            set;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void save_canExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.HasChanges;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Model.Save())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

    }

    public class CheckableTypeAccount: TypeAccount
    {
        public CheckableTypeAccount(DataRow r)
        {
            DataRow = r;
            IsChecked = CheckContains(TypeAccountID) != null;
        }

        public CheckableTypeAccount():base()
        { 
        }

        bool _isChecked = false;
        /// <summary>
        /// Отмечено для свзязи с типом банка и счета
        /// </summary>
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                if (_isChecked)
                {
                    if (CheckContains(TypeAccountID) == null) //Если такого типа нет в данных - то добавляем его, иначе будем наоборот удалять.
                    {
                        DataRow rr = DataSet.Tables["BANK_FOR_TYPE_ACCOUNT"].NewRow();
                        rr["TYPE_ACCOUNT_ID"] = TypeAccountID;
                        DataSet.Tables["BANK_FOR_TYPE_ACCOUNT"].Rows.Add(rr);
                    }
                }
                else
                    if (CheckContains(TypeAccountID) != null)
                    {
                        CheckContains(TypeAccountID).Delete();
                    }
                RaisePropertyChanged(()=>IsChecked);
            }
        }

        /// <summary>
        /// Проверяет есть ли указанный тип в отношении банка и типа счета
        /// </summary>
        /// <param name="TypeaccountID"></param>
        /// <returns></returns>
        private DataRow CheckContains(decimal? TypeaccountID)
        {
            return DataSet.Tables["BANK_FOR_TYPE_ACCOUNT"].Select("TYPE_ACCOUNT_ID=" + (TypeaccountID ?? -1).ToString()).FirstOrDefault();
        }
    }

    /// <summary>
    /// Типы банков
    /// </summary>
    public class TypeBankModel : TypeBank
    {
        OracleDataAdapter odaBank_For_Type_Account, odaType_Bank;
        DataSet ds;
        public TypeBankModel(decimal? p_type_bank_id)
        {
            ds = new DataSet();

#region Отношение банка и типов счетов

            odaBank_For_Type_Account = new OracleDataAdapter();
            odaBank_For_Type_Account.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.BANK_FOR_TYPE_ACCOUNT_UPDATE(:p_BANK_FOR_TYPE_ACCOUNT_ID,:p_TYPE_BANK_ID,:p_TYPE_ACCOUNT_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaBank_For_Type_Account.InsertCommand.BindByName = true;
            odaBank_For_Type_Account.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaBank_For_Type_Account.InsertCommand.Parameters.Add("p_BANK_FOR_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "BANK_FOR_TYPE_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;
            odaBank_For_Type_Account.InsertCommand.Parameters["p_BANK_FOR_TYPE_ACCOUNT_ID"].DbType = DbType.Decimal;
            odaBank_For_Type_Account.InsertCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, ParameterDirection.Input);
            odaBank_For_Type_Account.InsertCommand.Parameters.Add("p_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_ACCOUNT_ID").Direction = ParameterDirection.Input; 
            
            odaBank_For_Type_Account.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.BANK_FOR_TYPE_ACCOUNT_UPDATE(:p_BANK_FOR_TYPE_ACCOUNT_ID,:p_TYPE_BANK_ID,:p_TYPE_ACCOUNT_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaBank_For_Type_Account.UpdateCommand.BindByName = true;
            odaBank_For_Type_Account.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaBank_For_Type_Account.UpdateCommand.Parameters.Add("p_BANK_FOR_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "BANK_FOR_TYPE_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;
            odaBank_For_Type_Account.UpdateCommand.Parameters["p_BANK_FOR_TYPE_ACCOUNT_ID"].DbType = DbType.Decimal;
            odaBank_For_Type_Account.UpdateCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, ParameterDirection.Input);
            odaBank_For_Type_Account.UpdateCommand.Parameters.Add("p_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_ACCOUNT_ID").Direction = ParameterDirection.Input; 
            
            odaBank_For_Type_Account.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.BANK_FOR_TYPE_ACCOUNT_DELETE(:p_BANK_FOR_TYPE_ACCOUNT_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaBank_For_Type_Account.DeleteCommand.BindByName = true;
            odaBank_For_Type_Account.DeleteCommand.Parameters.Add("p_BANK_FOR_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "BANK_FOR_TYPE_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;
#endregion
            odaType_Bank = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectTypeBankData.sql"), Connect.CurConnect);
            odaType_Bank.SelectCommand.BindByName = true;
            odaType_Bank.SelectCommand.Parameters.Add("p_type_bank_id", OracleDbType.Decimal, p_type_bank_id, ParameterDirection.Input);
            odaType_Bank.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaType_Bank.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaType_Bank.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaType_Bank.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            odaType_Bank.TableMappings.Add("Table", "TYPE_BANK");
            odaType_Bank.TableMappings.Add("Table1", "TYPE_BANK1");
            odaType_Bank.TableMappings.Add("Table2", "TYPE_ACCOUNT");
            odaType_Bank.TableMappings.Add("Table3", "BANK_FOR_TYPE_ACCOUNT");
            odaType_Bank.AcceptChangesDuringUpdate = false;


            if (odaType_Bank.Fill(ds) == 0)
            {
                DataRow r = ds.Tables["TYPE_BANK"].NewRow();
                ds.Tables["TYPE_BANK"].Rows.Add(r);
            }
            DataRow = ds.Tables["TYPE_BANK"].Rows[0];

#region Адаптер типа банка
            odaType_Bank.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.TYPE_BANK_UPDATE(:p_TYPE_BANK_ID,:p_CURRENT_ACCOUNT,:p_BANK_NAME,:p_CORRESPONDENT_ACCOUNT,:p_BANK_OFFICE,:p_BRANCH_BANK,:p_TRN,:p_BANK_IDENT_CODE,:p_CUSTOM_SIGN,:p_PPC,:p_CONTRACT_CODE,:p_CONTRACT_DATE,:p_BANK_ADDRESS);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaType_Bank.InsertCommand.BindByName = true;
            odaType_Bank.InsertCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID").Direction = ParameterDirection.InputOutput;
            odaType_Bank.InsertCommand.Parameters["p_TYPE_BANK_ID"].DbType = DbType.Decimal;
            odaType_Bank.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaType_Bank.InsertCommand.Parameters.Add("p_CURRENT_ACCOUNT", OracleDbType.Varchar2, 0, "CURRENT_ACCOUNT").Direction = ParameterDirection.Input;
            odaType_Bank.InsertCommand.Parameters.Add("p_BANK_NAME", OracleDbType.Varchar2, 0, "BANK_NAME").Direction = ParameterDirection.Input;
            odaType_Bank.InsertCommand.Parameters.Add("p_CORRESPONDENT_ACCOUNT", OracleDbType.Varchar2, 0, "CORRESPONDENT_ACCOUNT").Direction = ParameterDirection.Input;
            odaType_Bank.InsertCommand.Parameters.Add("p_BANK_OFFICE", OracleDbType.Varchar2, 0, "BANK_OFFICE").Direction = ParameterDirection.Input;
            odaType_Bank.InsertCommand.Parameters.Add("p_BRANCH_BANK", OracleDbType.Varchar2, 0, "BRANCH_BANK").Direction = ParameterDirection.Input;
            odaType_Bank.InsertCommand.Parameters.Add("p_TRN", OracleDbType.Varchar2, 0, "TRN").Direction = ParameterDirection.Input;
            odaType_Bank.InsertCommand.Parameters.Add("p_BANK_IDENT_CODE", OracleDbType.Varchar2, 0, "BANK_IDENT_CODE").Direction = ParameterDirection.Input;
            odaType_Bank.InsertCommand.Parameters.Add("p_CUSTOM_SIGN", OracleDbType.Decimal, 0, "CUSTOM_SIGN").Direction = ParameterDirection.Input;
            odaType_Bank.InsertCommand.Parameters.Add("p_PPC", OracleDbType.Varchar2, 0, "PPC").Direction = ParameterDirection.Input;
            odaType_Bank.InsertCommand.Parameters.Add("p_CONTRACT_CODE", OracleDbType.Varchar2, 0, "CONTRACT_CODE").Direction = ParameterDirection.Input;
            odaType_Bank.InsertCommand.Parameters.Add("p_CONTRACT_DATE", OracleDbType.Date, 0, "CONTRACT_DATE").Direction = ParameterDirection.Input;
            odaType_Bank.InsertCommand.Parameters.Add("p_BANK_ADDRESS", OracleDbType.Varchar2, 0, "BANK_ADDRESS").Direction = ParameterDirection.Input;

            odaType_Bank.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.TYPE_BANK_UPDATE(:p_TYPE_BANK_ID,:p_CURRENT_ACCOUNT,:p_BANK_NAME,:p_CORRESPONDENT_ACCOUNT,:p_BANK_OFFICE,:p_BRANCH_BANK,:p_TRN,:p_BANK_IDENT_CODE,:p_CUSTOM_SIGN,:p_PPC,:p_CONTRACT_CODE,:p_CONTRACT_DATE,:p_BANK_ADDRESS);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaType_Bank.UpdateCommand.BindByName = true;
            odaType_Bank.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaType_Bank.UpdateCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID").Direction = ParameterDirection.InputOutput;
            odaType_Bank.UpdateCommand.Parameters["p_TYPE_BANK_ID"].DbType = DbType.Decimal;
            odaType_Bank.UpdateCommand.Parameters.Add("p_CURRENT_ACCOUNT", OracleDbType.Varchar2, 0, "CURRENT_ACCOUNT").Direction = ParameterDirection.Input;
            odaType_Bank.UpdateCommand.Parameters.Add("p_BANK_NAME", OracleDbType.Varchar2, 0, "BANK_NAME").Direction = ParameterDirection.Input;
            odaType_Bank.UpdateCommand.Parameters.Add("p_CORRESPONDENT_ACCOUNT", OracleDbType.Varchar2, 0, "CORRESPONDENT_ACCOUNT").Direction = ParameterDirection.Input;
            odaType_Bank.UpdateCommand.Parameters.Add("p_BANK_OFFICE", OracleDbType.Varchar2, 0, "BANK_OFFICE").Direction = ParameterDirection.Input;
            odaType_Bank.UpdateCommand.Parameters.Add("p_BRANCH_BANK", OracleDbType.Varchar2, 0, "BRANCH_BANK").Direction = ParameterDirection.Input;
            odaType_Bank.UpdateCommand.Parameters.Add("p_TRN", OracleDbType.Varchar2, 0, "TRN").Direction = ParameterDirection.Input;
            odaType_Bank.UpdateCommand.Parameters.Add("p_BANK_IDENT_CODE", OracleDbType.Varchar2, 0, "BANK_IDENT_CODE").Direction = ParameterDirection.Input;
            odaType_Bank.UpdateCommand.Parameters.Add("p_CUSTOM_SIGN", OracleDbType.Decimal, 0, "CUSTOM_SIGN").Direction = ParameterDirection.Input;
            odaType_Bank.UpdateCommand.Parameters.Add("p_PPC", OracleDbType.Varchar2, 0, "PPC").Direction = ParameterDirection.Input;
            odaType_Bank.UpdateCommand.Parameters.Add("p_CONTRACT_CODE", OracleDbType.Varchar2, 0, "CONTRACT_CODE").Direction = ParameterDirection.Input;
            odaType_Bank.UpdateCommand.Parameters.Add("p_CONTRACT_DATE", OracleDbType.Date, 0, "CONTRACT_DATE").Direction = ParameterDirection.Input;
            odaType_Bank.UpdateCommand.Parameters.Add("p_BANK_ADDRESS", OracleDbType.Varchar2, 0, "BANK_ADDRESS").Direction = ParameterDirection.Input;
            odaType_Bank.AcceptChangesDuringUpdate = false;
#endregion
        }

        List<CheckableTypeAccount> _bankForAccount;
        public List<CheckableTypeAccount> BankForTypeAccountSource
        { 
            get
            {
                if (_bankForAccount == null)
                    _bankForAccount = ds.Tables["TYPE_ACCOUNT"].Rows.OfType<DataRow>().Select(r => new CheckableTypeAccount(r)).ToList();
                return _bankForAccount;
            }
        }

        /// <summary>
        /// Для подбора имени уже существующего банка 
        /// </summary>
        public string[] DistinctBankNameSource
        {
            get
            {
                if (ds != null && ds.Tables.Contains("TYPE_BANK1"))
                    return ds.Tables["TYPE_BANK1"].Rows.OfType<DataRow>().Select(t => t["BANK_NAME"].ToString()).Distinct().OrderBy(t => t).ToArray();
                else
                    return null;
            }
        }

        /// <summary>
        /// Сохраннение данных по модели
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                if (this.EntityState == DataRowState.Added)
                    this.TypeBankID = null;
                odaType_Bank.Update(ds.Tables["TYPE_BANK"]);
                foreach (DataRow t in ds.Tables["BANK_FOR_TYPE_ACCOUNT"].Rows) // для всех зависимых строк сбрасываем айдишник на пусто и айди типа банка
                    if (t.RowState != DataRowState.Deleted)
                    {
                        if (t.RowState == DataRowState.Added)
                            t["BANK_FOR_TYPE_ACCOUNT_ID"] = DBNull.Value;
                    }
                odaBank_For_Type_Account.UpdateCommand.Parameters["P_TYPE_BANK_ID"].Value = odaBank_For_Type_Account.InsertCommand.Parameters["P_TYPE_BANK_ID"].Value =
                    TypeBankID;
                odaBank_For_Type_Account.Update(ds.Tables["BANK_FOR_TYPE_ACCOUNT"]);
                tr.Commit();
                ds.AcceptChanges();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
                return false;
            }
        }


        /// <summary>
        /// Если ли изменения в модели
        /// </summary>
        public bool HasChanges 
        {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }
    }

}
