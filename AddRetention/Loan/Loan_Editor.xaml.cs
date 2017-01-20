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
using System.ComponentModel;
using LibrarySalary.Helpers;
using Salary.Loan.Classes;
using System.Data.Linq.Mapping;

namespace Salary.Loan
{
    /// <summary>
    /// Interaction logic for Loan_Editor.xaml
    /// </summary>
    public partial class Loan_Editor : Window
    {
        LoanModel _model;
        public Loan_Editor(decimal? p_loan_id)
        {
            _model = new LoanModel(p_loan_id);
            InitializeComponent();
            this.DataContext = Model;
        }
        
        /// <summary>
        /// Данные источника по текущей строке
        /// </summary>
        public LoanModel Model
        {
            get
            {
                return _model;
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && Model != null && Model.HasChanges && string.IsNullOrWhiteSpace(Model.Error);
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Save();
        }

        private void SelectBorrower_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState((e.Command as RoutedUICommand).Name) && 
                (Model.SignRegistrationDog == null || Model.SignRegistrationDog == 0))
                e.CanExecute = true;
        }

        private void SelectBorrower_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Find_Emp find_Emp = new Find_Emp(DateTime.Today);
            find_Emp.Owner = Window.GetWindow(this);
            if (find_Emp.ShowDialog() == true)
            {
                Model.CODE_SUBDIV = find_Emp.Code_Subdiv;
                Model.FIO = find_Emp.Last_Name + " " + find_Emp.First_Name + " " + find_Emp.Middle_Name;
                Model.TransferID = find_Emp.Transfer_ID;
                Model.PER_NUM = find_Emp.Per_Num;
                OracleCommand _ocNew_Ordinal_Number = new OracleCommand(string.Format(
                    @"SELECT {0}.LOAN_DML_PKG.GET_NEW_ORDINAL_NUMBER(:p_PER_NUM, :p_LOAN_ID) FROM DUAL", Connect.SchemaSalary),
                    Connect.CurConnect);
                _ocNew_Ordinal_Number.BindByName = true;
                _ocNew_Ordinal_Number.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2).Value = Model.PER_NUM;
                _ocNew_Ordinal_Number.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal).Value = Model.LoanID;
                Model.OrdinalNumber = Convert.ToDecimal(_ocNew_Ordinal_Number.ExecuteScalar());
            }
        }

        private void btClearClient_Account_ID_Click(object sender, RoutedEventArgs e)
        {
            Model.ClientAccountID = null;
        }
    }

    /// <summary>
    /// Класс унаследованный от типа записи зарплаты
    /// </summary>
    public class LoanModel : EntityGenerator.Loan, IDataErrorInfo
    {
        public DataSet _ds;
        OracleDataAdapter _daLoan, _daClientAccount;
        
        public LoanModel(decimal? p_loan_id)
            : base()
        {
            _ds = new DataSet();
            _ds.Tables.Add("LOAN");
            _ds.Tables.Add("CLIENT_ACCOUNT");
            _daLoan = new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/SelectLoan.sql"), Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
            _daLoan.SelectCommand.BindByName = true;
            _daLoan.SelectCommand.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal).Value = p_loan_id;
            //_daLoan.SelectCommand.Parameters.Add("p_SIGN_ARCHIVE", OracleDbType.Decimal);
            _daLoan.SelectCommand.Parameters.Add("p_SIGN_REGISTRATION_DOG", OracleDbType.Decimal);
            _daLoan.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            _daLoan.TableMappings.Add("Table", "LOAN");

            #region Adapter Loan
            // Insert
            _daLoan.InsertCommand = new OracleCommand(string.Format(
                @"begin {1}.LOAN_DML_PKG.LOAN_UPDATE(:p_LOAN_ID,:p_PROTOCOL_NUMBER,:p_PROTOCOL_DATE,
                    :p_CONTRACT_NUMBER,:p_CONTRACT_DATE,:p_TRANSFER_ID,:p_LOAN_DATE,:p_LOAN_SUM,:p_LOAN_TERM,:p_ORDINAL_NUMBER,:p_RETENTION_BY_CONTRACT,
                    :p_RETENTION_BY_FACT,:p_SIGN_RETENTION,:p_SIGN_MATERIAL_BENEFIT,:p_PURPOSE_LOAN_ID,:p_TYPE_LOAN_ID,:p_SIGN_ARCHIVE,
                    :p_SIGN_REGISTRATION_DOG,:p_LOAN_DATE_END,:p_CLIENT_ACCOUNT_ID); 
                end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            _daLoan.InsertCommand.BindByName = true;
            _daLoan.InsertCommand.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal, 0, "LOAN_ID").Direction = ParameterDirection.InputOutput;
            _daLoan.InsertCommand.Parameters["p_LOAN_ID"].DbType = DbType.Decimal;
            _daLoan.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            _daLoan.InsertCommand.Parameters.Add("p_PROTOCOL_NUMBER", OracleDbType.Varchar2, 0, "PROTOCOL_NUMBER");
            _daLoan.InsertCommand.Parameters.Add("p_PROTOCOL_DATE", OracleDbType.Date, 0, "PROTOCOL_DATE");
            _daLoan.InsertCommand.Parameters.Add("p_CONTRACT_NUMBER", OracleDbType.Varchar2, 20, "CONTRACT_NUMBER").Direction = 
                ParameterDirection.InputOutput;
            _daLoan.InsertCommand.Parameters.Add("p_CONTRACT_DATE", OracleDbType.Date, 0, "CONTRACT_DATE");
            _daLoan.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _daLoan.InsertCommand.Parameters.Add("p_LOAN_DATE", OracleDbType.Date, 0, "LOAN_DATE");
            _daLoan.InsertCommand.Parameters.Add("p_LOAN_SUM", OracleDbType.Decimal, 0, "LOAN_SUM");
            _daLoan.InsertCommand.Parameters.Add("p_LOAN_TERM", OracleDbType.Decimal, 0, "LOAN_TERM");
            _daLoan.InsertCommand.Parameters.Add("p_ORDINAL_NUMBER", OracleDbType.Int16, 0, "ORDINAL_NUMBER");
            _daLoan.InsertCommand.Parameters.Add("p_RETENTION_BY_CONTRACT", OracleDbType.Decimal, 0, "RETENTION_BY_CONTRACT").Direction = 
                ParameterDirection.InputOutput;
            _daLoan.InsertCommand.Parameters["p_RETENTION_BY_CONTRACT"].DbType = DbType.Decimal;
            _daLoan.InsertCommand.Parameters.Add("p_RETENTION_BY_FACT", OracleDbType.Decimal, 0, "RETENTION_BY_FACT").Direction =
                ParameterDirection.InputOutput;
            _daLoan.InsertCommand.Parameters["p_RETENTION_BY_FACT"].DbType = DbType.Decimal;
            _daLoan.InsertCommand.Parameters.Add("p_SIGN_RETENTION", OracleDbType.Int16, 0, "SIGN_RETENTION").Direction =
                ParameterDirection.InputOutput;
            _daLoan.InsertCommand.Parameters["p_SIGN_RETENTION"].DbType = DbType.Decimal;
            _daLoan.InsertCommand.Parameters.Add("p_SIGN_MATERIAL_BENEFIT", OracleDbType.Int16, 0, "SIGN_MATERIAL_BENEFIT").Direction =
                ParameterDirection.InputOutput;
            _daLoan.InsertCommand.Parameters["p_SIGN_MATERIAL_BENEFIT"].DbType = DbType.Decimal;
            _daLoan.InsertCommand.Parameters.Add("p_PURPOSE_LOAN_ID", OracleDbType.Decimal, 0, "PURPOSE_LOAN_ID");
            _daLoan.InsertCommand.Parameters.Add("p_TYPE_LOAN_ID", OracleDbType.Decimal, 0, "TYPE_LOAN_ID");
            _daLoan.InsertCommand.Parameters.Add("p_SIGN_ARCHIVE", OracleDbType.Int16, 0, "SIGN_ARCHIVE").Direction =
                ParameterDirection.InputOutput;
            _daLoan.InsertCommand.Parameters["p_SIGN_ARCHIVE"].DbType = DbType.Decimal;
            _daLoan.InsertCommand.Parameters.Add("p_SIGN_REGISTRATION_DOG", OracleDbType.Int16, 0, "SIGN_REGISTRATION_DOG").Direction =
                ParameterDirection.InputOutput;
            _daLoan.InsertCommand.Parameters["p_SIGN_REGISTRATION_DOG"].DbType = DbType.Decimal;
            _daLoan.InsertCommand.Parameters.Add("p_LOAN_DATE_END", OracleDbType.Date, 0, "LOAN_DATE_END");
            _daLoan.InsertCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID");
            // Update
            _daLoan.UpdateCommand = _daLoan.InsertCommand;

            #endregion

            _daLoan.Fill(_ds.Tables["LOAN"]);

            if (_ds.Tables["LOAN"].Rows.Count > 0)
            {
                _ds.Tables["LOAN"].Rows[0].AcceptChanges();
                //_ds.Tables["LOAN"].Rows[0].SetAdded();
                DataRow = _ds.Tables["LOAN"].Rows[0];
            }
            else
            {
                DataRow = _ds.Tables["LOAN"].DefaultView.AddNew().Row;
                _ds.Tables["LOAN"].Rows.Add(DataRow);
                _ds.Tables["LOAN"].Rows[0].AcceptChanges();
                _ds.Tables["LOAN"].Rows[0].SetAdded();
            }

            // CLIENT_ACCOUNT
            _daClientAccount = new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/SelectClient_Account_For_Emp.sql"), Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
            _daClientAccount.SelectCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM").Value = DataRow["PER_NUM"];
            _daClientAccount.SelectCommand.BindByName = true;
            _daClientAccount.Fill(_ds.Tables["CLIENT_ACCOUNT"]);
        }

        private void UpdateClientAccounts(object per_num)
        {
            _ds.Tables["CLIENT_ACCOUNT"].Clear();
            _daClientAccount.SelectCommand.Parameters["p_PER_NUM"].Value = per_num;
            _daClientAccount.Fill(_ds.Tables["CLIENT_ACCOUNT"]);
        }
        
        #region Источники данных для списков
        
        /// <summary>
        /// Источник данных для типа ссуды
        /// </summary>
        public DataView TypeLoanSource
        {
            get
            {
                return new DataView(LoanDataSet.Type_Loan, "", "", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Источник данных для цели ссуды
        /// </summary>
        public DataView PurposeLoanSource
        {
            get
            {
                return new DataView(LoanDataSet.Purpose_Loan, "", "", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Источник данных для счетов сотрудника
        /// </summary>
        public DataView ClientAccountSource
        {
            get
            {
                return new DataView(_ds.Tables["CLIENT_ACCOUNT"], "", "", DataViewRowState.CurrentRows);
            }
        }

        #endregion
        
        /// <summary>
        /// Сохранение данных функция
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                _daLoan.Update(new DataRow[] { _ds.Tables["LOAN"].Rows[0] });
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

        #region Class member and overloads
        ///// <summary>
        ///// Перегрузка шифра оплат, для генерации событий обновления формы
        ///// </summary>
        //public new decimal? PaymentTypeID
        //{
        //    get
        //    {
        //        return base.PaymentTypeID;
        //    }
        //    set
        //    {
        //        base.PaymentTypeID = value;
        //        if (PaymentTypeID != value)
        //        {
        //            if (RetentionSource != null)
        //                RetentionSource.RowFilter = string.Format("PAYMENT_TYPE_ID={0}", PaymentTypeID ?? -1);
        //            if (value != null) /*Устанавливаем заказ по умолчанию, если таковой требуется*/
        //            {
        //                var payvalue = AppDataSet.Tables["PAYMENT_TYPE"].Select(string.Format("PAYMENT_TYPE_ID={0}", PaymentTypeID)).Select
        //                        (r => new
        //                        {
        //                            DefOrderID = r.Field2<Decimal?>("DEF_ORDER_ID"),
        //                            TypeRefSalary = r.Field2<Decimal?>("RELAT_TYPE_REF_ID")
        //                        }).FirstOrDefault();
        //                if (payvalue.DefOrderID != null && OrderID != payvalue.DefOrderID)
        //                    OrderID = payvalue.DefOrderID;
        //                TypeRefSalaryID = payvalue.TypeRefSalary; // а так же устанавливаем тип связуемых данных для записи по типу оплат
        //                RefID = null;
        //            }
        //        }
        //        RaisePropertyChanged(() => PaymentTypeID);
        //        RaisePropertyChanged(() => PayDate);
        //        RaisePropertyChanged(() => SubdivID);
        //        RaisePropertyChanged(() => RetentionSource);
        //        RaisePropertyChanged(() => IsHourEditEnabled);
        //        RaisePropertyChanged(() => IsDaysEditEnabled);
        //        RaisePropertyChanged(() => IsSecondDataEnabled);
        //        RaisePropertyChanged(() => Error);
        //    }
        //}

        ///// <summary>
        ///// Выбранная дата зарплаты
        ///// </summary>
        //public new DateTime? PayDate
        //{
        //    get
        //    {
        //        return base.PayDate;
        //    }
        //    set
        //    {
        //        base.PayDate = value;
        //        RaisePropertyChanged(() => SubdivID);
        //        RaisePropertyChanged(() => PaymentTypeID);
        //        RaisePropertyChanged(() => IsSecondDataEnabled);
        //        RaisePropertyChanged(() => IsHourEditEnabled);
        //        RaisePropertyChanged(() => IsDaysEditEnabled);
        //        RaisePropertyChanged(() => Error);
        //    }
        //}

        ///// <summary>
        ///// Подразделение выбранной записи
        ///// </summary>
        //public new decimal? SubdivID
        //{
        //    get
        //    {
        //        return base.SubdivID;
        //    }
        //    set
        //    {
        //        base.SubdivID = value;
        //        RaisePropertyChanged(() => SubdivID);
        //        RaisePropertyChanged(() => PaymentTypeID);
        //        RaisePropertyChanged(() => PayDate);
        //        RaisePropertyChanged(() => IsSecondDataEnabled);
        //        RaisePropertyChanged(() => Error);
        //    }
        //}

        ///// <summary>
        ///// Связанные данные с заработной платой сотрудника
        ///// </summary>
        //[Column(Name = "REF_ROW_DATA")]
        //public string RefRowData
        //{
        //    get
        //    {
        //        return base.GetDataRowField<string>(() => RefRowData);
        //    }
        //    set
        //    {
        //        base.UpdateDataRow<string>(() => RefRowData, value);
        //    }
        //}

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        [Column(Name = "FIO")]
        public string FIO
        {
            get
            {
                return base.GetDataRowField<string>(() => FIO);
            }
            set
            {
                base.UpdateDataRow<string>(() => FIO, value);
            }
        }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        [Column(Name = "PER_NUM")]
        public string PER_NUM
        {
            get
            {
                return base.GetDataRowField<string>(() => PER_NUM);
            }
            set
            {
                base.UpdateDataRow<string>(() => PER_NUM, value);
                UpdateClientAccounts(value);
                RaisePropertyChanged(() => Error);
            }
        }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        [Column(Name = "CODE_SUBDIV")]
        public string CODE_SUBDIV
        {
            get
            {
                return base.GetDataRowField<string>(() => CODE_SUBDIV);
            }
            set
            {
                base.UpdateDataRow<string>(() => CODE_SUBDIV, value);
            }
        }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        [Column(Name = "LOAN_REMAINDER")]
        public decimal? LOAN_REMAINDER
        {
            get
            {
                return base.GetDataRowField<decimal?>(() => LOAN_REMAINDER);
            }
            set
            {
                base.UpdateDataRow<decimal?>(() => LOAN_REMAINDER, value);
            }
        }

        public String ProtocolNumber
        {
            get
            {
                return base.ProtocolNumber;
            }
            set
            {
                base.ProtocolNumber = value;
                RaisePropertyChanged(() => Error);
            }
        }

        public Decimal? LoanTerm
        {
            get
            {
                return base.LoanTerm;
            }
            set
            {
                base.LoanTerm = value;
                RaisePropertyChanged(() => Error);
            }
        }

        public Decimal? LoanSum
        {
            get
            {
                return base.LoanSum;
            }
            set
            {
                base.LoanSum = value;
                RaisePropertyChanged(() => Error);
            }
        }

        public DateTime? LoanDate
        {
            get
            {
                return base.LoanDate;
            }
            set
            {
                base.LoanDate = value;
                RaisePropertyChanged(() => Error);
            }
        }

        /// <summary>
        /// Доступно ли редактирование данных для записи
        /// </summary>
        public bool IsEditDataEnabled
        {
            get
            {
                return SignRegistrationDog == 0 || SignRegistrationDog == null;
            }
        }
        
        /// <summary>
        /// Доступно ли редактирование данных для записи
        /// </summary>
        public bool IsOpenEditLoan
        {
            get
            {
                return LoanDateEnd == null;
            }
        }
        #endregion

        /// <summary>
        /// Ошибка для конкретного поля в классе
        /// </summary>
        /// <param name="column_name">имя поля</param>
        /// <returns></returns>
        public new string this[string column_name]
        {
            get
            {
                if (column_name == "PER_NUM" && PER_NUM == null)
                    return "Требуется выбрать заемщика";
                if (column_name == "ProtocolNumber" && ProtocolNumber == null)
                    return "Требуется ввести номер протокола";
                if (column_name == "LoanSum" && (LoanSum == null || LoanSum <= 0))
                    return "Неверно введена сумма займа";
                if (column_name == "LoanTerm" && (LoanTerm == null || LoanTerm <= 0))
                    return "Неверно введен срок займа";
                if (column_name == "LoanDate" && LoanDate == null)
                    return "Неверно введена дата займа";
                /*if (column_name == "PaymentTypeID")
                {
                    if (PaymentTypeID == null) return "Требуется выбрать шифр оплат";
                }

                if ((column_name == "PayDate" || column_name == "SubdivID" || column_name == "PayDate") && !CheckOpenedSubdiv() && !CheckFieldAccess(FieldEditType.Days) && !CheckFieldAccess(FieldEditType.Hours))
                    return "Для выбранных значений даты и подразделения редактирование закрыто";


                if (column_name == "TransferID" && TransferID == null)
                    return "Требуется указать перевод сотрудника";
                */
                return string.Empty;

            }
        }

        /// <summary>
        /// Общая ошибка по всей записи
        /// </summary>
        public string Error
        {
            get
            {
                if (PER_NUM == null)
                    return "Требуется выбрать заемщика";
                if (ProtocolNumber == null)
                    return "Требуется ввести номер протокола";
                if ((LoanSum == null || LoanSum <= 0))
                    return "Неверно введена сумма займа";
                if ((LoanTerm == null || LoanTerm <= 0))
                    return "Неверно введен срок займа";
                if (LoanDate == null)
                    return "Неверно введена дата займа";
                /*if (PayDate == null) return "Требуется выбрать дату записи (дата заработной платы)";
                if (PaymentTypeID == null) return "Требуется указать шифр оплат";

                if (!CheckOpenedSubdiv() && !CheckFieldAccess(FieldEditType.Days) && !CheckFieldAccess(FieldEditType.Hours))
                    return "Для выбранных значений даты и подразделения редактирование закрыто";

                if (TransferID == null) return "Требуется указать перевод сотрудника";*/
                return string.Empty;
            }
        }

        public bool HasChanges
        {
            get
            {
                return _ds != null && _ds.HasChanges();
            }
        }
    }
}
