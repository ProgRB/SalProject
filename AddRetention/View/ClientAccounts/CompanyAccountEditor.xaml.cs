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
using Salary.Helpers;
using System.Data;
using Oracle.DataAccess.Client;
using EntityGenerator;
using System.ComponentModel;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for CompanyAccountEditor.xaml
    /// </summary>
    public partial class CompanyAccountEditor : UserControl
    {
        private CompanyAccountViewModel _model;
        public CompanyAccountEditor()
        {
            _model = new CompanyAccountViewModel();
            InitializeComponent();
            DataContext = Model;
        }

        public CompanyAccountViewModel Model
        {
            get
            {
                return _model;
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.HasChanges;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgCompanyAccount.CommitEdit(DataGridEditingUnit.Cell, true);
            Model.SaveModel();
        }

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddNew();
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.CurrentAccount != null;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный счет без возможости восстановления?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Model.DeleteCurrent();
            }
        }
    }

    public class CompanyAccountViewModel : NotificationObject
    {
        DataSet ds;
        OracleDataAdapter odaClient_Account;
        Lookup<decimal, decimal> bank_account_mapping;

        public CompanyAccountViewModel()
        {
            ds = new DataSet();
            odaClient_Account = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectCompanyAccountData.sql"), Connect.CurConnect);
            odaClient_Account.SelectCommand.BindByName = true;
            odaClient_Account.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaClient_Account.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaClient_Account.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaClient_Account.TableMappings.Add("Table", "CLIENT_ACCOUNT");
            odaClient_Account.TableMappings.Add("Table1", "TYPE_BANK");
            odaClient_Account.TableMappings.Add("Table2", "BANK_FOR_TYPE_ACCOUNT");

#region Адаптер загрузки и сохранения
            odaClient_Account.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.CLIENT_ACCOUNT_UPDATE(:p_CLIENT_ACCOUNT_ID,:p_TRANSFER_ID,:p_NUMBER_ACCOUNT,:p_TYPE_BANK_ID,:p_OWNER_NAME,:p_OWNER_FAMILY,:p_OWNER_MIDDLE_NAME,:p_PASSPORT_SERIES,:p_PASSPORT_NUMBER,:p_GET_PLACE,:p_GET_CITY,
                :p_NUMBER_CARD,:p_PLF_NAME,:p_PLF_ADDRESS,:p_PLF_INDEX,:p_TYPE_ACCOUNT_ID,:p_INSURANCE_NUM,:p_PER_INSURANCE_NUM,:p_CODE_DOC,:p_DATE_DOC,:p_COMPANY_NAME,:p_SEPARATE_SIGN);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaClient_Account.InsertCommand.BindByName = true;
            odaClient_Account.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
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
            odaClient_Account.InsertCommand.Parameters.Add("p_SEPARATE_SIGN", OracleDbType.Varchar2, 0, "SEPARATE_SIGN").Direction = ParameterDirection.Input; 
            
            odaClient_Account.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.CLIENT_ACCOUNT_UPDATE(:p_CLIENT_ACCOUNT_ID,:p_TRANSFER_ID,:p_NUMBER_ACCOUNT,:p_TYPE_BANK_ID,:p_OWNER_NAME,:p_OWNER_FAMILY,:p_OWNER_MIDDLE_NAME,:p_PASSPORT_SERIES,:p_PASSPORT_NUMBER,:p_GET_PLACE,:p_GET_CITY,
                :p_NUMBER_CARD,:p_PLF_NAME,:p_PLF_ADDRESS,:p_PLF_INDEX,:p_TYPE_ACCOUNT_ID,:p_INSURANCE_NUM,:p_PER_INSURANCE_NUM,:p_CODE_DOC,:p_DATE_DOC,:p_COMPANY_NAME,:p_SEPARATE_SIGN);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaClient_Account.UpdateCommand.BindByName = true;
            odaClient_Account.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
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
            odaClient_Account.UpdateCommand.Parameters.Add("p_SEPARATE_SIGN", OracleDbType.Varchar2, 0, "SEPARATE_SIGN").Direction = ParameterDirection.Input; 
            
            odaClient_Account.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.CLIENT_ACCOUNT_DELETE(:p_CLIENT_ACCOUNT_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaClient_Account.DeleteCommand.BindByName = true;
            odaClient_Account.DeleteCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;

#endregion
            odaClient_Account.Fill(ds);
            ds.Tables["TYPE_BANK"].Columns.Add("DISP_EXP", typeof(string)).Expression = "BANK_NAME+ '(Отд№ '+ISNULL(BANK_OFFICE,'<не указано>')+')'";

            bank_account_mapping = (Lookup<decimal, decimal>)(from r in ds.Tables["BANK_FOR_TYPE_ACCOUNT"].AsEnumerable()
                                                              select r).ToLookup(p => p.Field2<decimal>("TYPE_ACCOUNT_ID"), p => p.Field2<decimal>("TYPE_BANK_ID"));
        }
        DataView _companyAccountSource;
        /// <summary>
        /// Источник данных для 
        /// </summary>
        public DataView CompanyAccountSource
        {
            get
            {
                if (_companyAccountSource == null && ds != null && ds.Tables.Contains("CLIENT_ACCOUNT"))
                {
                    _companyAccountSource = new DataView(ds.Tables["CLIENT_ACCOUNT"], "", "COMPANY_NAME", DataViewRowState.CurrentRows);
                }
                return _companyAccountSource;
            }
        }

        public Array TypeBankSource
        {
            get
            {
                var p = from r in ds.Tables["TYPE_BANK"].AsEnumerable()
                        where bank_account_mapping.Contains(5m) && bank_account_mapping[5m].Contains(r.Field2<Decimal>("TYPE_BANK_ID"))
                        group r by r["BANK_NAME"].ToString() into g
                        select new
                        {
                            TypeBankID = -1m,
                            BankName = g.Key,
                            Caption = g.Key,
                            Padding = new Thickness(5, 2, 1, 2),
                            Level = 1,
                            Office = "000",
                            DisplayExpr = g.Key,
                            INN=string.Empty
                        };
                var p2 = from r in ds.Tables["TYPE_BANK"].AsEnumerable()
                         where bank_account_mapping.Contains(5m) && bank_account_mapping[5m].Contains(r.Field2<Decimal>("TYPE_BANK_ID"))
                         select new
                         {
                             TypeBankID = r.Field2<Decimal>("TYPE_BANK_ID"),
                             BankName = r.Field2<string>("BANK_NAME"),
                             Caption = r.Field2<string>("BANK_OFFICE") + "  " + r.Field2<string>("CURRENT_ACCOUNT")
                                 + "   БИК: " + r.Field2<string>("BANK_IDENT_CODE"),
                             Padding = new Thickness(15, 2, 1, 2),
                             Level = 2,
                             Office = r.Field2<string>("BANK_OFFICE"),
                             DisplayExpr = r.Field2<string>("DISP_EXP"),
                             INN=r.Field2<string>("TRN")
                         };
                return
                    p.Union(p2).OrderBy(t => new Tuple<string, int, string>(t.BankName, t.Level, t.Office)).ToArray();
            }
        }

        /// <summary>
        /// Сохраняем введенные данные
        /// </summary>
        public void SaveModel()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                foreach (DataRow r in ds.Tables["CLIENT_ACCOUNT"].Rows)
                {
                    if (r.RowState == DataRowState.Added)
                    {
                        r["CLIENT_ACCOUNT_ID"] = DBNull.Value;
                        r["TYPE_ACCOUNT_ID"] = 5m;
                    }
                }
                odaClient_Account.Update(ds.Tables["CLIENT_ACCOUNT"]);
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
            }
        }

        /// <summary>
        /// есть ли несохраненные изменения
        /// </summary>
        public bool HasChanges
        {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }

        internal void AddNew()
        {
            _companyAccountSource.AddNew();
        }

        DataRowView _currentAccount;

        /// <summary>
        /// Текущий выбранный счет
        /// </summary>
        public DataRowView CurrentAccount 
        {
            get
            {
                return _currentAccount;
            }
            set
            {
                _currentAccount = value;
                RaisePropertyChanged(() => CurrentAccount);
            }
        }

        internal void DeleteCurrent()
        {
            CurrentAccount.Delete();
        }
    }
}
