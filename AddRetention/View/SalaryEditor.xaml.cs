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
using System.Globalization;
using Salary.Helpers;
using System.Data.Linq.Mapping;
using LibrarySalary.Helpers;
using EntityGenerator;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for SalaryEditor.xaml
    /// </summary>
    public partial class SalaryEditor : Window
    {
        public static RoutedUICommand _refreshOrders = new RoutedUICommand("Обновить заказы", "refreshOrders", typeof(SalaryEditor));
        public static RoutedUICommand RefreshOrders
        {
            get
            {
                return _refreshOrders;
            }
        }

        
        public SalaryEditor(object sender, object p_transfer_id, object p_salary_id, DateTime _selectedDate, OracleTransaction currentTransaction = null)
        {
            _model = new SalaryModel((decimal)p_transfer_id, (decimal?)p_salary_id, _selectedDate, currentTransaction, (sender as Payment) == null ? null : (sender as Payment).GetCalcedTypePayment());
            InitializeComponent();
            this.DataContext = Model;
        }

        /// <summary>
        /// Данные источника по текущей строке
        /// </summary>
        public SalaryModel Model
        {
            get
            {
                return _model;
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && Model!=null && Model.HasChanges && (string.IsNullOrWhiteSpace(Model.Error)|| GrantedRoles.CheckRole("SALARY_ADMIN"));
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Model.Save())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        
        private void Refresh_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Refresh_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AppDataSet.UpdateSet("Order");
        }

        private void EditSalaryOrder_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }

        private void EditOrders_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FindOrders f = new FindOrders(tbCodeOrder.Text);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                tbCodeOrder.Text = f.Order_name;
            }
        }

        private void EditSalaryRefData_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.TypeRefSalaryID!=null;
        }

        public static OracleCommand cmdSelRefData = new OracleCommand(String.Format("select {1}.Select_Ref_RowData(:p_type_ref_id, :p_ref_id) from dual", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
        private SalaryModel _model;
        
        private void EditSalaryRefData_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RefSalaryDataSelector f = new RefSalaryDataSelector(Model.TypeRefSalaryID, Model.RefID, Model.TransferID);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.RefID = (decimal?)f.SelectedValue;
                if (cmdSelRefData.Parameters.Count == 0)
                {
                    cmdSelRefData.Parameters.Add("p_type_ref_id", OracleDbType.Decimal, ParameterDirection.Input);
                    cmdSelRefData.Parameters.Add("p_ref_id", OracleDbType.Decimal, ParameterDirection.Input);
                }
                cmdSelRefData.Parameters["p_type_ref_id"].Value = Model.TypeRefSalaryID;
                cmdSelRefData.Parameters["p_ref_id"].Value = Model.RefID;
                try
                {
                    Model.RefRowData = cmdSelRefData.ExecuteScalar() as string;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
                }
            }
        }
    
    }


    public class ValidateOrderRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null && value!=DependencyProperty.UnsetValue && value is string 
                && !string.IsNullOrEmpty((string)value) && !DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                string s = (string)value;
                object t = AppDataSet.Tables["ORDER"].Compute("COUNT(ORDER_ID)", string.Format("ORDER_NAME='{0}'", s));
                if ( Convert.ToInt32(t)!=0)
                    return ValidationResult.ValidResult;
                else return new ValidationResult(false, "Заданный заказ не найден в справочнике");
            }
            else return ValidationResult.ValidResult;
        }
    }

    public class OrderIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value != DBNull.Value && value!=DependencyProperty.UnsetValue)
            {
                try
                {
                    DataRow[] d = AppDataSet.Tables["ORDER"].Select(string.Format("ORDER_ID={0}", value));
                    if (d.Length == 0)
                        return null;
                    else
                        return d[0]["ORDER_NAME"];
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value != DBNull.Value && !string.IsNullOrEmpty((string)value) && ((string)value).Length==AppConstants.OrderLength)
            {
                DataRow[] d = AppDataSet.Tables["ORDER"].Select(string.Format("ORDER_NAME='{0}'", value));
                if (d.Length == 0)
                    return null;
                else
                    return d[0]["ORDER_ID"];
            }
            return null;
        }
    }

    public class RefSalaryAreaVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is DataRowView && (value as DataRowView)["RELAT_TYPE_REF_ID"] != DBNull.Value)
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RefIdToDataConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != DBNull.Value && values[0]!=null && values[1] != null && values[1]!=DBNull.Value)
            {
                if (SalaryEditor.cmdSelRefData.Parameters.Count == 0)
                {
                    SalaryEditor.cmdSelRefData.Parameters.Add("p_type_ref_id", OracleDbType.Decimal, ParameterDirection.Input);
                    SalaryEditor.cmdSelRefData.Parameters.Add("p_ref_id", OracleDbType.Decimal, ParameterDirection.Input);
                }
                SalaryEditor.cmdSelRefData.Parameters["p_type_ref_id"].Value = values[0];
                SalaryEditor.cmdSelRefData.Parameters["p_ref_id"].Value = values[1];
                try
                {
                    return SalaryEditor.cmdSelRefData.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
                }
                return null;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Класс унаследованный от типа записи зарплаты
    /// </summary>
    public class SalaryModel : EntityGenerator.Salary, IDataErrorInfo
    {
        public DataSet ds;
        OracleDataAdapter odaSalary;
        OracleTransaction CurrentTransaction;

        private readonly string[] documentablePayments = new string[]{"287", "488", "487", "277", "417", "275", "885"};

        public SalaryModel(decimal p_transfer_id, decimal? p_salary_id, DateTime? selectedDate, OracleTransaction currentTransaction=null, decimal[] calced_dependies = null):base()
        {
            CurrentTransaction = currentTransaction;
            ds = new DataSet();
            ds.Tables.Add(AppDataSet.TypeSalDocum.Copy());
            LoadCloseData();
            odaSalary = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectSalaryData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
#region Adapeter Region
            odaSalary.InsertCommand = new OracleCommand(string.Format(@"begin {1}.SALARY_UPDATE(:p_SALARY_ID
                          ,:p_PAY_DATE
                          ,:p_PAYMENT_TYPE_ID
                          ,:p_HOURS
                          ,:p_SUM_SAL
                          ,:p_ZONE_ADD
                          ,:p_EXP_ADD 
                          ,:p_ORDER_ID
                          ,:p_GROUP_MASTER 
                          ,:p_DEGREE_ID 
                          ,:p_TRANSFER_ID
                          ,:p_TYPE_REF_SALARY_ID
                          ,:p_REF_ID
                          ,:p_DAYS
                          ,:p_ACCOUNT_ADD_SIGN
                          ,:p_TIME_ADD_RECORD
                          ,:p_PER_NUM
                          ,:p_SIGN_COMB
                          ,:p_SUBDIV_ID
                          ,:p_RETENTION_ID
                          ,:p_TYPE_ROW_SALARY_ID
                          ,:p_recalcDepend); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSalary.InsertCommand.BindByName = true;
            odaSalary.InsertCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, 0, "SALARY_ID").Direction = ParameterDirection.InputOutput;
            odaSalary.InsertCommand.Parameters["p_SALARY_ID"].DbType = DbType.Decimal;
            odaSalary.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

            odaSalary.InsertCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE");
            odaSalary.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            odaSalary.InsertCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS");
            odaSalary.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            odaSalary.InsertCommand.Parameters.Add("p_ZONE_ADD", OracleDbType.Decimal, 0, "ZONE_ADD");
            odaSalary.InsertCommand.Parameters.Add("p_EXP_ADD", OracleDbType.Decimal, 0, "EXP_ADD");
            odaSalary.InsertCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");
            odaSalary.InsertCommand.Parameters.Add("p_GROUP_MASTER", OracleDbType.Varchar2, 0, "GROUP_MASTER");
            odaSalary.InsertCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            odaSalary.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            odaSalary.InsertCommand.Parameters.Add("p_TYPE_REF_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_REF_SALARY_ID");
            odaSalary.InsertCommand.Parameters.Add("p_REF_ID", OracleDbType.Decimal, 0, "REF_ID");
            odaSalary.InsertCommand.Parameters.Add("p_DAYS", OracleDbType.Decimal, 0, "DAYS");
            odaSalary.InsertCommand.Parameters.Add("p_ACCOUNT_ADD_SIGN", OracleDbType.Decimal, 0, "ACCOUNT_ADD_SIGN");
            odaSalary.InsertCommand.Parameters.Add("p_TIME_ADD_RECORD", OracleDbType.Date, 0, "TIME_ADD_RECORD");
            odaSalary.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            odaSalary.InsertCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            odaSalary.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            odaSalary.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID");
            odaSalary.InsertCommand.Parameters.Add("p_TYPE_ROW_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_ROW_SALARY_ID");
            odaSalary.InsertCommand.Parameters.Add("p_recalcDepend", OracleDbType.Array).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
            if (calced_dependies != null)
                odaSalary.InsertCommand.Parameters["p_recalcDepend"].Value = calced_dependies;
#endregion

            odaSalary.SelectCommand.BindByName = true;
            odaSalary.SelectCommand.Parameters.Add("p_salary_id", OracleDbType.Decimal, p_salary_id, ParameterDirection.Input);
            odaSalary.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, p_transfer_id, ParameterDirection.Input);
            odaSalary.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, selectedDate??DateTime.Now, ParameterDirection.Input);
            odaSalary.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);

            odaSalary.TableMappings.Add("Table", "Salary");
            odaSalary.TableMappings.Add("Table1", "RETENTION");
            odaSalary.TableMappings.Add("Table2", "EmpTransfers");
            odaSalary.TableMappings.Add("Table3", "VIEW_CARTULARY_PAID");
            odaSalary.Fill(ds);
            ds.Tables["SALARY"].Rows[0].AcceptChanges();
            //AppDataSet.UpdateSet("PAYMENT_TYPE");
            ds.Tables["RETENTION"].Columns.Add("DISP_EXP_RETENT").Expression = "'№ '+order_number+' '+disp_exp";
            ds.Tables["SALARY"].Rows[0].SetAdded();
            ds.Relations.Add("pt_id_fk", ds.Tables["SALARY"].Columns["PAYMENT_TYPE_ID"], ds.Tables["RETENTION"].Columns["PAYMENT_TYPE_ID"], false);

            DataRow = ds.Tables["SALARY"].Rows[0];

        }

#region Источники данных для списков

        DataView _retentionDocSource;
        /// <summary>
        /// Список документов для удержания
        /// </summary>
        public DataView RetentionSource
        {
            get
            {
                if (_retentionDocSource == null)
                    _retentionDocSource = new DataView(ds.Tables["RETENTION"], string.Format("PAYMENT_TYPE_ID={0}", PaymentTypeID ?? -1), "ORDER_NUMBER, DATE_START_RET", DataViewRowState.CurrentRows);
                else
                    _retentionDocSource.RowFilter = string.Format("PAYMENT_TYPE_ID={0}", PaymentTypeID ?? -1);
                return _retentionDocSource;
            }
        }

        /// <summary>
        /// Источник данных для категорий
        /// </summary>
        public DataView DegreeSource
        {
            get
            {
                return new DataView(AppDataSet.Tables["DEGREE"], "", "CODE_DEGREE", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Источник данных для вида оплат
        /// </summary>
        public List<PaymentType> PaymentTypeSource
        {
            get
            {
                return AppDataSet.Tables["PAYMENT_TYPE"].ConvertToEntityList<PaymentType>().OrderBy(r=>r.CodePayment).ToList();
            }
        }

        DataView _subdivSource;
        /// <summary>
        /// Доступные подразделения
        /// </summary>
        public DataView SubdivSource
        {
            get
            {
                if (_subdivSource == null)
                {
                    DataTable t = AppDataSet.Tables["ACCESS_SUBDIV"].Copy();
                    t.Columns.Add("CODE_SUBDIV_VALUE").Expression = "CODE_SUBDIV+iif(SUB_ACTUAL_SIGN=1,'','<не актуально>')";
                    _subdivSource = new DataView(t, "APP_NAME='SALARY'", "CODE_SUBDIV", DataViewRowState.CurrentRows);
                }
                return _subdivSource;
            }
        }

        /// <summary>
        /// Тип записи строки зарплаты
        /// </summary>
        public DataView TypeRowSalarySource
        {
            get
            {
                return new DataView(AppDataSet.Tables["TYPE_ROW_SALARY"], "", "", DataViewRowState.CurrentRows);
            }
        }
            
        /// <summary>
        /// Тип связанных данных
        /// </summary>
        public DataView TypeRefSource
        {
            get
            {
                return new DataView(AppDataSet.Tables["TYPE_REF_SALARY"], "", "", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Переводы сотрудника
        /// </summary>
        public DataView EmpTransferSource
        {
            get
            {
                return new DataView(ds.Tables["EmpTransfers"], "", "DATE_TRANSFER", DataViewRowState.CurrentRows);
            }
        }

        #endregion
        
        /// <summary>
        /// Сохранение данных функция
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            OracleTransaction tr = CurrentTransaction ?? Connect.CurConnect.BeginTransaction();
            try
            {
                odaSalary.Update(new DataRow[] { ds.Tables["SALARY"].Rows[0] });
                if (CurrentTransaction == null) tr.Commit();// если транзакция внешняя, то коммит не надо будет делать
                return true;
            }
            catch (Exception ex)
            {
                if (CurrentTransaction == null) tr.Rollback();
                MessageBox.Show(ex.GetFormattedException());
                return false;
            }
        }

        #region Class member and overloads
        /// <summary>
        /// Перегрузка шифра оплат, для генерации событий обновления формы
        /// </summary>
        public new  decimal? PaymentTypeID
        {
            get
            {
                return base.PaymentTypeID;
            }
            set
            {
                if (PaymentTypeID != value)
                {
                    base.PaymentTypeID = value;
                    if (RetentionSource != null)
                        RetentionSource.RowFilter = string.Format("PAYMENT_TYPE_ID={0}", PaymentTypeID ?? -1);
                    if (value != null) /*Устанавливаем заказ по умолчанию, если таковой требуется*/
                    {
                        var payvalue = AppDataSet.Tables["PAYMENT_TYPE"].Select(string.Format("PAYMENT_TYPE_ID={0}", PaymentTypeID)).Select
                                (r => new
                                    {
                                        DefOrderID = r.Field2<Decimal?>("DEF_ORDER_ID"),
                                        TypeRefSalary = r.Field2<Decimal?>("RELAT_TYPE_REF_ID")
                                    }).FirstOrDefault();
                        if (payvalue.DefOrderID != null && OrderID != payvalue.DefOrderID)
                            OrderID = payvalue.DefOrderID;
                        TypeRefSalaryID = payvalue.TypeRefSalary; // а так же устанавливаем тип связуемых данных для записи по типу оплат
                        RefID = null;
                    }
                    RaisePropertyChanged(() => PaymentType);
                }
                RaisePropertyChanged(() => PaymentTypeID);
                RaisePropertyChanged(() => PayDate);
                RaisePropertyChanged(() => SubdivID);
                RaisePropertyChanged(() => DegreeID);
                RaisePropertyChanged(() => RetentionSource);
                RaisePropertyChanged(() => RetentionID);
                RaisePropertyChanged(() => IsHourEditEnabled);
                RaisePropertyChanged(() => IsDaysEditEnabled);
                RaisePropertyChanged(() => IsSecondDataEnabled);
                RaisePropertyChanged(() => Error);
            }
        }

        /// <summary>
        /// Описание вида оплат
        /// </summary>
        public PaymentType PaymentType
        {
            get
            {
                if (PaymentTypeID == null)
                    return null;
                return PaymentTypeSource.Where(r => r.PaymentTypeID == PaymentTypeID).FirstOrDefault();
            }
        }

        /// <summary>
        /// Выбранная дата зарплаты
        /// </summary>
        public new DateTime? PayDate
        {
            get
            {
                return base.PayDate;
            }
            set
            {
                base.PayDate = value;
                RaisePropertyChanged(() => SubdivID);
                RaisePropertyChanged(() => PaymentTypeID);
                RaisePropertyChanged(() => IsSecondDataEnabled);
                RaisePropertyChanged(() => IsHourEditEnabled);
                RaisePropertyChanged(() => IsDaysEditEnabled);
                RaisePropertyChanged(() => Error);
            }
        }

        /// <summary>
        /// Подразделение выбранной записи
        /// </summary>
        public new decimal? SubdivID
        {
            get
            {
                return base.SubdivID;
            }
            set
            {
                base.SubdivID = value;
                RaisePropertyChanged(() => SubdivID);
                RaisePropertyChanged(() => PaymentTypeID);
                RaisePropertyChanged(() => PayDate);
                RaisePropertyChanged(() => IsSecondDataEnabled);
                RaisePropertyChanged(() => Error);
            }
        }

        /// <summary>
        /// Перегрузка для категории чтобы обновлялась ошибка неверной категории на экране
        /// </summary>
        public new decimal? DegreeID
        {
            get
            {
                return base.DegreeID;
            }
            set
            {
                base.DegreeID = value;
                RaisePropertyChanged(() => Error);
            }
        }

        /// <summary>
        /// Связанные данные с заработной платой сотрудника
        /// </summary>
        [Column(Name="REF_ROW_DATA")]
        public string RefRowData
        {
            get
            {
                return base.GetDataRowField<string>(() => RefRowData);
            }
            set
            {
                base.UpdateDataRow<string>(() => RefRowData, value);
            }
        }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        [Column(Name="FIO")]
        public string FIO
        {
            get
            {
                return base.GetDataRowField<string>(() => FIO);
            }
            set
            {
                base.UpdateDataRow<string>(()=>FIO, value);
            }
        }

        /// <summary>
        /// Доступно ли редактирование для часов по записи
        /// </summary>
        public bool IsHourEditEnabled
        { 
            get
            {
                return PaymentTypeID != null && PayDate != null && CheckFieldAccess( FieldEditType.Hours);
            }
        }

        /// <summary>
        /// Доступно ли редактирование для дней по записи
        /// </summary>
        public bool IsDaysEditEnabled
        { 
            get
            {
                return PaymentTypeID != null && PayDate != null && CheckFieldAccess( FieldEditType.Days);
            }
        }

        /// <summary>
        /// Доступно ли редактирование прочих данных для записи
        /// </summary>
        public bool IsSecondDataEnabled
        { 
            get
            {
                return PaymentTypeID != null && PayDate != null;
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
                if (column_name == "SubdivID" && SubdivID == null)
                    return "Требуется выбрать подразделение";
                if (column_name == "PayDate" && PayDate == null)
                    return "Требуется выбрать дату записи (дата заработной платы)";
                if (column_name == "PaymentTypeID")
                {
                    if (PaymentTypeID == null) return "Требуется выбрать шифр оплат";
                }

                if ((column_name == "PayDate" || column_name == "SubdivID" || column_name=="PayDate") && !CheckOpenedSubdiv() && !CheckFieldAccess(FieldEditType.Days) && !CheckFieldAccess(FieldEditType.Hours))
                    return "Для выбранных значений даты и подразделения редактирование закрыто";
                
                
                if (column_name == "TransferID" && TransferID == null)
                    return "Требуется указать перевод сотрудника";

                if (column_name=="RetentionID" && RetentionID == null && IsMustDocumentable) 
                    return "Требуется указать соответствующий документ удержания для данного вида оплат";

                if ((column_name=="DegreeID" || column_name=="PaymentTypeID") && PaymentType != null && PaymentType.CodePayment == "101Н" 
                        && DegreeID != 1 && DegreeID != 2 && DegreeID != 6 && DegreeID != 11)
                    return "Сдельная оплата труда доступна только для 01, 02, 06, 11 категории работников!";

                return string.Empty;

            }
        }

        /// <summary>
        /// Общая ошибка по всей записи
        /// </summary>
        public new string Error
        {
            get 
            {
                
                if (SubdivID == null) return "Требуется выбрать подразделение";
                if (PayDate == null) return "Требуется выбрать дату записи (дата заработной платы)";
                if (PaymentTypeID == null) return "Требуется указать шифр оплат";

                if (!CheckOpenedSubdiv() && !CheckFieldAccess(FieldEditType.Days) && !CheckFieldAccess(FieldEditType.Hours))
                    return "Для выбранных значений даты и подразделения редактирование закрыто";

                if (TransferID == null) return "Требуется указать перевод сотрудника";
                if (RetentionID == null && IsMustDocumentable) return "Требуется указать соответствующий документ удержания для данного вида оплат";
                if (PaymentType != null && PaymentType.CodePayment == "101Н" && DegreeID != 1 && DegreeID != 2 && DegreeID != 6 && DegreeID != 11)
                    return "Сдельная оплата труда доступна только для 01, 02, 06, 11 категории работников!";
                return string.Empty;
            }
        }

        /// <summary>
        /// Определяет, должна ли запись быть привязанной в какому-либо документу
        /// </summary>
        private bool IsMustDocumentable
        { 
            get
            {
                if (PaymentType == null)
                    return false;
                return documentablePayments.Contains(PaymentType.CodePayment);
            }
        }

        public bool HasChanges 
        {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }

        /// <summary>
        /// Проверяем доступно ли редактирование часов/дней согласно типу
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool CheckFieldAccess(FieldEditType type)
        {
            try
            {
                DataRow[] rs = AppDataSet.Tables["PAYMENT_TYPE"].Select("PAYMENT_TYPE_ID=" +(PaymentTypeID??-1));
                if (rs.Length > 0)
                {
                    string st = type== FieldEditType.Hours ? "IS_ALLOW_PAST_HOUR_EDIT" : type == FieldEditType.Days ? "IS_ALLOW_PAST_DAYS_EDIT" : "";
                    decimal type_cons = rs[0].Field2<Decimal?>("CONSIDER_TYPE_ID")?? -1;
                    return (type_cons == 2 && type == FieldEditType.Days || type_cons == 1 && type ==  FieldEditType.Hours|| type_cons == 3) // если тип поля совпадает с типом вида оплат в БД
                            && (rs[0][st].ToString() == "1" || CheckOpenedSubdiv()); // и можно редактировать любую в любую дату, или же эта дата открыта то истина
                }
                return false;
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// Проверяет, доступно ли редактирование в подразделении на эту дату
        /// </summary>
        /// <returns></returns>
        private bool CheckOpenedSubdiv()
        {
            bool fl = CheckCloseState(PayDate, SubdivID);
            return PaymentTypeID != null && SubdivID != null && !fl;
        }

        /// <summary>
        /// Загрузка закрытости подразделений
        /// </summary>
        public void LoadCloseData()
        {
            try
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format("begin {1}.SALARY_PKG.SelectSubdivForClose(:p_appName, :c); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                a.TableMappings.Add("Table", "CloseSubdiv");
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_appName", OracleDbType.Varchar2, "SALARY", ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                a.Fill(ds);
            }
            catch { }
        }

        public bool CheckCloseState(DateTime? date, Decimal? subdiv_id)
        {
            // проверяем открыто ли редактирование для выбранных 2х параметров. Кстати, этот метод вызывается и ниже чуть, из другого конвертера
            if (date==null && subdiv_id==null) return false;
            try
            {
                return ds.Tables["CloseSubdiv"].AsEnumerable().Any(p => date <= p.Field2<DateTime>("DATE_CLOSING") && p.Field2<Decimal>("SUBDIV_ID") == subdiv_id);
            }
            catch { }
            return false;
        }

        private enum FieldEditType
        { 
            Hours=1,
            Days=2
        }

        private Cartulary[] _cartulary;
        /// <summary>
        /// В какой реестр перечисления включена данная запись
        /// </summary>
        public Cartulary[] Cartulary
        {
            get
            {
                if (_cartulary == null)
                    _cartulary = TryGetCartulary();
                return _cartulary;
            }
        }

        /// <summary>
        /// Получаем все реестры куда могла попасть запись
        /// </summary>
        /// <returns></returns>
        private Cartulary[] TryGetCartulary()
        {
            try
            {
                _cartulary = ds.Tables["VIEW_CARTULARY_PAID"].Rows.OfType<DataRow>().Select(r => EntityGenerator.Cartulary.GetEntityByID<Cartulary>(r.Field<Decimal?>("CARTULARY_ID"), ds)).ToArray();
            }
            catch (Exception ex)
            { 
            }
            return _cartulary;
        }
    }
}
