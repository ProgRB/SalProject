using EntityGenerator;
using LibrarySalary.Helpers;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.Mapping;
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

namespace Salary.View.ClientAccounts
{
    /// <summary>
    /// Логика взаимодействия для DocumTransferEditor.xaml
    /// </summary>
    public partial class DocumTransferEditor : Window
    {
        private DocumTransferModel _model;

        public static RoutedUICommand ChangeEmp { get; private set; }

        public DocumTransferEditor(decimal TypeCartularyid, decimal? documTransferId)
        {
            _model = new DocumTransferModel(documTransferId);
            InitializeComponent();
            Model.TypeCartularyID = TypeCartularyid;
            DataContext = Model;
        }

        public DocumTransferModel Model
        {
            get
            {
                return _model;
            }
        }

        static DocumTransferEditor()
        {
            ChangeEmp = new RoutedUICommand("Выбрать сотрудника", "EditOtherTransfer", typeof(DocumTransferEditor));
        }

        private void Change_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);   
        }

        private void ChangeEmp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EmpFinder f = new EmpFinder();
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                DocumTransferRelationModel obj = e.Parameter as DocumTransferRelationModel;
                if (obj == null)
                    MessageBox.Show("Ошибка привзяки. Не установлен объект данных для строки", "Внутренняя ошибка");
                else
                {
                    obj.TransferID = f.SelectedItem.Row.Field2<decimal?>("TRANSFER_ID");
                }
            }
        }

        private void Add_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddNewRelationRow();
        }

        private void Delete_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model!=null && Model.CurrentRelationRow != null;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.DeleteRelationRow();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Save_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.HasChanges && string.IsNullOrEmpty(Model.Error);
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Exception ex = Model.Save();
            if (ex != null)
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения данных");
            else
            {
                DialogResult = true;
                Close();
            }
        }
    }

    public partial class DocumTransferModel : DocumTransfer, IDataErrorInfo
    {
        OracleDataAdapter odaDocum_Transfer, odaDocum_Transfer_Relation, odaLoadEmpData;
        DataSet ds;
        private ObservableCollection<DocumTransferRelationModel> _relationList;
        private DocumTransferRelationModel _currentRelationRow;

        public DocumTransferModel(decimal? documTransferID)
        {
            ds = new DataSet();
            odaDocum_Transfer = new OracleDataAdapter(Queries.GetQueryWithSchema(@"ClientAccountTransfer/SelectDocumentTransferData.sql"), Connect.CurConnect);
            odaDocum_Transfer.SelectCommand.BindByName = true;
            odaDocum_Transfer.SelectCommand.Parameters.Add("p_docum_transfer_id", OracleDbType.Decimal, documTransferID, ParameterDirection.Input);
            odaDocum_Transfer.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaDocum_Transfer.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaDocum_Transfer.TableMappings.Add("Table", "DOCUM_TRANSFER");
            odaDocum_Transfer.TableMappings.Add("Table1", "DOCUM_TRANSFER_RELATION");

            odaLoadEmpData = new OracleDataAdapter(Queries.GetQueryWithSchema(@"ClientAccountTransfer/SelectDocumOrEmpDataTransfer.sql"), Connect.CurConnect);
            odaLoadEmpData.SelectCommand.BindByName = true;
            odaLoadEmpData.SelectCommand.Parameters.Add("p_docum_transfer_id", OracleDbType.Decimal, documTransferID, ParameterDirection.Input);
            odaLoadEmpData.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, -1, ParameterDirection.Input);
            odaLoadEmpData.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaLoadEmpData.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaLoadEmpData.TableMappings.Add("Table", "EMP_ACCOUNT_DATA");
            odaLoadEmpData.TableMappings.Add("Table1", "CLIENT_ACCOUNT");

            #region Адаптеку документа
            odaDocum_Transfer.InsertCommand = new OracleCommand(string.Format(@"BEGIN {1}.DOCUM_TRANSFER_UPDATE(p_DOCUM_TRANSFER_ID=>:p_DOCUM_TRANSFER_ID,p_DATE_DOCUM=>:p_DATE_DOCUM,p_CODE_DOCUM=>:p_CODE_DOCUM,p_TYPE_CARTULARY_ID=>:p_TYPE_CARTULARY_ID,p_DOCUM_COMMENT=>:p_DOCUM_COMMENT);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaDocum_Transfer.InsertCommand.BindByName = true;
            odaDocum_Transfer.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaDocum_Transfer.InsertCommand.Parameters.Add("p_DOCUM_TRANSFER_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_ID").Direction = ParameterDirection.InputOutput;
            odaDocum_Transfer.InsertCommand.Parameters["p_DOCUM_TRANSFER_ID"].DbType = DbType.Decimal;
            odaDocum_Transfer.InsertCommand.Parameters.Add("p_DATE_DOCUM", OracleDbType.Date, 0, "DATE_DOCUM").Direction = ParameterDirection.Input;
            odaDocum_Transfer.InsertCommand.Parameters.Add("p_CODE_DOCUM", OracleDbType.Varchar2, 0, "CODE_DOCUM").Direction = ParameterDirection.Input;
            odaDocum_Transfer.InsertCommand.Parameters.Add("p_TYPE_CARTULARY_ID", OracleDbType.Decimal,0, "TYPE_CARTULARY_ID");
            odaDocum_Transfer.InsertCommand.Parameters.Add("p_DOCUM_COMMENT", OracleDbType.Varchar2, 0, "DOCUM_COMMENT").Direction = ParameterDirection.Input;

            odaDocum_Transfer.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {1}.DOCUM_TRANSFER_UPDATE(p_DOCUM_TRANSFER_ID=>:p_DOCUM_TRANSFER_ID,p_DATE_DOCUM=>:p_DATE_DOCUM,p_CODE_DOCUM=>:p_CODE_DOCUM,p_TYPE_CARTULARY_ID=>:p_TYPE_CARTULARY_ID,p_DOCUM_COMMENT=>:p_DOCUM_COMMENT);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaDocum_Transfer.UpdateCommand.BindByName = true;
            odaDocum_Transfer.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaDocum_Transfer.UpdateCommand.Parameters.Add("p_DOCUM_TRANSFER_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_ID").Direction = ParameterDirection.InputOutput;
            odaDocum_Transfer.UpdateCommand.Parameters["p_DOCUM_TRANSFER_ID"].DbType = DbType.Decimal;
            odaDocum_Transfer.UpdateCommand.Parameters.Add("p_DATE_DOCUM", OracleDbType.Date, 0, "DATE_DOCUM").Direction = ParameterDirection.Input;
            odaDocum_Transfer.UpdateCommand.Parameters.Add("p_CODE_DOCUM", OracleDbType.Varchar2, 0, "CODE_DOCUM").Direction = ParameterDirection.Input;
            odaDocum_Transfer.UpdateCommand.Parameters.Add("p_TYPE_CARTULARY_ID", OracleDbType.Decimal, 0, "TYPE_CARTULARY_ID");
            odaDocum_Transfer.UpdateCommand.Parameters.Add("p_DOCUM_COMMENT", OracleDbType.Varchar2, 0, "DOCUM_COMMENT").Direction = ParameterDirection.Input;

            odaDocum_Transfer.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {1}.DOCUM_TRANSFER_DELETE(:p_DOCUM_TRANSFER_ID);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaDocum_Transfer.DeleteCommand.BindByName = true;
            odaDocum_Transfer.DeleteCommand.Parameters.Add("p_DOCUM_TRANSFER_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_ID").Direction = ParameterDirection.InputOutput;
            odaDocum_Transfer.AcceptChangesDuringUpdate = false;
            #endregion

            #region Адаптер связей данных
            odaDocum_Transfer_Relation = new OracleDataAdapter();
            odaDocum_Transfer_Relation.InsertCommand = new OracleCommand(string.Format(@"BEGIN {1}.DOCUM_TRANSFER_RELATION_UPDATE(p_DOCUM_TRANSFER_RELATION_ID=>:p_DOCUM_TRANSFER_RELATION_ID,p_DOCUM_TRANSFER_ID=>:p_DOCUM_TRANSFER_ID,p_CHECK_DATE=>:p_CHECK_DATE,p_FIN_PLAN_CODE=>:p_FIN_PLAN_CODE,p_CLIENT_ACCOUNT_ID=>:p_CLIENT_ACCOUNT_ID, p_SUM_SAL=>:p_SUM_SAL, p_PAY_DATE=>:p_PAY_DATE, p_TRANSFER_ID=>:p_TRANSFER_ID);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaDocum_Transfer_Relation.InsertCommand.BindByName = true;
            odaDocum_Transfer_Relation.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaDocum_Transfer_Relation.InsertCommand.Parameters.Add("p_DOCUM_TRANSFER_RELATION_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_RELATION_ID").Direction = ParameterDirection.InputOutput;
            odaDocum_Transfer_Relation.InsertCommand.Parameters["p_DOCUM_TRANSFER_RELATION_ID"].DbType = DbType.Decimal;
            odaDocum_Transfer_Relation.InsertCommand.Parameters.Add("p_DOCUM_TRANSFER_ID", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaDocum_Transfer_Relation.InsertCommand.Parameters.Add("p_CHECK_DATE", OracleDbType.Date, 0, "CHECK_DATE").Direction = ParameterDirection.Input;
            odaDocum_Transfer_Relation.InsertCommand.Parameters.Add("p_FIN_PLAN_CODE", OracleDbType.Varchar2, 0, "FIN_PLAN_CODE").Direction = ParameterDirection.Input;
            odaDocum_Transfer_Relation.InsertCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.Input;
            odaDocum_Transfer_Relation.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL").Direction = ParameterDirection.Input;
            odaDocum_Transfer_Relation.InsertCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE").Direction = ParameterDirection.Input;
            odaDocum_Transfer_Relation.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;


            odaDocum_Transfer_Relation.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {1}.DOCUM_TRANSFER_RELATION_UPDATE(p_DOCUM_TRANSFER_RELATION_ID=>:p_DOCUM_TRANSFER_RELATION_ID,p_DOCUM_TRANSFER_ID=>:p_DOCUM_TRANSFER_ID,p_CHECK_DATE=>:p_CHECK_DATE,p_FIN_PLAN_CODE=>:p_FIN_PLAN_CODE,p_CLIENT_ACCOUNT_ID=>:p_CLIENT_ACCOUNT_ID, p_SUM_SAL=>:p_SUM_SAL, p_PAY_DATE=>:p_PAY_DATE, p_TRANSFER_ID=>:p_TRANSFER_ID);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaDocum_Transfer_Relation.UpdateCommand.BindByName = true;
            odaDocum_Transfer_Relation.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaDocum_Transfer_Relation.UpdateCommand.Parameters.Add("p_DOCUM_TRANSFER_RELATION_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_RELATION_ID").Direction = ParameterDirection.InputOutput;
            odaDocum_Transfer_Relation.UpdateCommand.Parameters["p_DOCUM_TRANSFER_RELATION_ID"].DbType = DbType.Decimal;
            odaDocum_Transfer_Relation.UpdateCommand.Parameters.Add("p_DOCUM_TRANSFER_ID", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaDocum_Transfer_Relation.UpdateCommand.Parameters.Add("p_CHECK_DATE", OracleDbType.Date, 0, "CHECK_DATE").Direction = ParameterDirection.Input;
            odaDocum_Transfer_Relation.UpdateCommand.Parameters.Add("p_FIN_PLAN_CODE", OracleDbType.Varchar2, 0, "FIN_PLAN_CODE").Direction = ParameterDirection.Input;
            odaDocum_Transfer_Relation.UpdateCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.Input;
            odaDocum_Transfer_Relation.UpdateCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL").Direction = ParameterDirection.Input;
            odaDocum_Transfer_Relation.UpdateCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE").Direction = ParameterDirection.Input;
            odaDocum_Transfer_Relation.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;

            odaDocum_Transfer_Relation.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {1}.DOCUM_TRANSFER_RELATION_DELETE(:p_DOCUM_TRANSFER_RELATION_ID);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaDocum_Transfer_Relation.DeleteCommand.BindByName = true;
            odaDocum_Transfer_Relation.DeleteCommand.Parameters.Add("p_DOCUM_TRANSFER_RELATION_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_RELATION_ID").Direction = ParameterDirection.InputOutput;
            odaDocum_Transfer_Relation.AcceptChangesDuringUpdate = false;
            #endregion

            odaDocum_Transfer.Fill(ds);
            odaLoadEmpData.Fill(ds);
            if (ds.Tables["DOCUM_TRANSFER"].Rows.Count == 0)
            {
                DataRow r = ds.Tables["DOCUM_TRANSFER"].NewRow();
                ds.Tables["DOCUM_TRANSFER"].Rows.Add(r);
            }
            this.DataRow = ds.Tables["DOCUM_TRANSFER"].Rows[0];
            _relationList = new ObservableCollection<DocumTransferRelationModel>(ds.Tables["DOCUM_TRANSFER_RELATION"].ConvertToEntityList<DocumTransferRelationModel>());
            foreach (var p in _relationList)
            {
                p.DocumTransfer = this;
                p.PropertyChanged += (pp, pw) =>
                  {
                      if (pw.PropertyName == "TransferID" || pw.PropertyName == "SumSal" || pw.PropertyName == "PayDate" || pw.PropertyName=="ClientAccountID")
                          RaisePropertyChanged(() => Error);
                  };
            }
            this.PropertyChanged += (p, pw) =>
            {
                if (pw.PropertyName=="DateDocum")
                    RaisePropertyChanged(() => Error);
            };
        }


        /// <summary>
        /// Источник даннх состав документа
        /// </summary>
        public ObservableCollection<DocumTransferRelationModel> DocumRelationSource
        {
            get
            {
                return _relationList;
            }
        }

        /// <summary>
        /// Загрузка данных (добавление) по сотруднику
        /// </summary>
        /// <param name="transferID"></param>
        public void LoadEmpAccountData(decimal? transferID)
        {
            DataSet temp = new DataSet();
            odaLoadEmpData.SelectCommand.Parameters["p_transfer_id"].Value = transferID;
            odaLoadEmpData.SelectCommand.Parameters["p_docum_transfer_id"].Value = DocumTransferID;
            odaLoadEmpData.Fill(temp);
            ds.Tables["EMP_ACCOUNT_DATA"].Merge(temp.Tables["EMP_ACCOUNT_DATA"], "TRANSFER_ID");
            ds.Tables["CLIENT_ACCOUNT"].Merge(temp.Tables["CLIENT_ACCOUNT"], "CLIENT_ACCOUNT_ID");
            ds.Tables["EMP_ACCOUNT_DATA"].AcceptChanges();
            ds.Tables["CLIENT_ACCOUNT"].AcceptChanges();
        }


        /// <summary>
        /// Добавление новой записи в список
        /// </summary>
        public void AddNewRelationRow()
        {
            DataRow r = ds.Tables["DOCUM_TRANSFER_RELATION"].Rows.Add();
            var temp = new DocumTransferRelationModel() { DataRow = r };
            _relationList.Add(temp);
            temp.DocumTransferID = this.DocumTransferID;
            temp.DocumTransfer = this;
            temp.PropertyChanged += (pp, pw) =>
            {
                if (pw.PropertyName != "Error")
                    RaisePropertyChanged(() => Error);
            };
            RaisePropertyChanged(() => DocumRelationSource);
        }

        /// <summary>
        /// ТЕкущая выбранная строчка в табличной части документа
        /// </summary>
        public DocumTransferRelationModel CurrentRelationRow
        {
            get
            {
                return _currentRelationRow;
            }
            set
            {
                _currentRelationRow = value;
                RaisePropertyChanged(() => CurrentRelationRow);
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
        /// Ошибка для модели
        /// </summary>
        public new string Error
        {
            get
            {
                string s = string.Empty;
                if (this.DateDocum == null)
                    s = "Не заполнена дата приказа";
                if (!string.IsNullOrEmpty(s))
                {
                    return s;
                }
                foreach (var p in DocumRelationSource)
                {
                    if (p.TransferID == null)
                    {
                        s = "Не выбран сотрудник для приказа";
                        break;
                    }
                    if (p.ClientAccountID==null)
                    {
                        s = "Не выбран счет для перечисления сотрудника";
                        break;
                    }
                    if (p.PayDate==null)
                    {
                        s = "Не выбрана дата перечисления";
                        break;
                    }
                    if (p.SumSal==null)
                    {
                        s = "Не указана сумма перечисления";
                        break;
                    }
                    if (p.CheckDate==null)
                    {
                        s = "Не указана дата отчета";
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(s))
                        return s;
                return string.Empty;
            }
        }

        /// <summary>
        /// Удаление текущей строки табличной части
        /// </summary>
        public void DeleteRelationRow()
        {
            if (_currentRelationRow != null)
            {
                _relationList.Remove(_currentRelationRow);
                _currentRelationRow.DataRow.Delete();
                RaisePropertyChanged(() => DocumRelationSource);
            }
        }

        /// <summary>
        /// Процедура сохранения данных
        /// </summary>
        /// <returns></returns>
        public new Exception Save()
        {
            OracleTransaction tr = this.AdapterConnection.BeginTransaction();
            try
            {
                odaDocum_Transfer.Update(new DataRow[] { this.DataRow });
                odaDocum_Transfer_Relation.InsertCommand.Parameters["p_DOCUM_TRANSFER_ID"].Value =
                    odaDocum_Transfer_Relation.UpdateCommand.Parameters["p_DOCUM_TRANSFER_ID"].Value = this.DocumTransferID;
                odaDocum_Transfer_Relation.Update(ds.Tables["DOCUM_TRANSFER_RELATION"]);
                tr.Commit();
                ds.AcceptChanges();
                return null;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                if (this.EntityState == DataRowState.Added)
                    this.DocumTransferID = null;
                foreach (var p in DocumRelationSource)
                    if (p.EntityState == DataRowState.Added)
                        p.DocumTransferRelationID = null;
                return ex;
            }
        }
    }

    public class DocumTransferRelationModel : DocumTransferRelation, IDataErrorInfo
    {

        EmpAccountData _transfer;

        private DocumTransferModel _documTransferModel;

        public DocumTransferRelationModel()
        {

        }

        /// <summary>
        /// документ приказа перечисления записей
        /// </summary>
        public DocumTransferModel DocumTransfer
        {
            get
            {
                return _documTransferModel;
            }
            set
            {
                _documTransferModel = value;
                RaisePropertyChanged(() => DocumTransfer);
            }
        }
        /// <summary>
        /// Перевод сотрудника для записи зарплаты
        /// </summary>
        [Column(CanBeNull = false, Name = "TRANSFER_ID")]
        public decimal? TransferID
        {
            get
            {
                return GetDataRowField<decimal?>(() => TransferID);
            }
            set
            {
                // перед обновлением проверим, есть ли этот перевод в локальной таблице, если нету то заполним данные по переводу
                if (DataSet.Tables.Contains("EMP_ACCOUNT_DATA"))
                {
                    if (DataSet.Tables["EMP_ACCOUNT_DATA"].Select(string.Format("TRANSFER_ID={0}", value ?? -1)).Length == 0)
                    {
                        ///Загружаем данные по сотрудникам
                        DocumTransfer.LoadEmpAccountData(value);
                    }
                }
                _transfer = null;
                UpdateDataRow<Decimal?>(() => TransferID, value);
                RaisePropertyChanged(() => Transfer);
                ClientAccountID = null;
                RaisePropertyChanged(() => ClientAccountsSource);
            }
        }

        /// <summary>
        /// Ссылка на счет сотрудника
        /// </summary>
        [Column(CanBeNull =false, Name = "CLIENT_ACCOUNT_ID")]
        public new decimal? ClientAccountID
        {
            get
            {
                return base.ClientAccountID;
            }
            set
            {
                base.ClientAccountID = value;
                RaisePropertyChanged(() => ClientAccount);
            }
        }

        /// <summary>
        /// Дата перечисления для записи зарплаты
        /// </summary>
        [Column(CanBeNull = false, Name = "PAY_DATE")]
        public DateTime? PayDate
        {
            get
            {
                return GetDataRowField<DateTime?>(() => PayDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => PayDate, value);
            }
        }
        /// <summary>
        /// Сумма перечисления для записи зарплаты
        /// </summary>
        [Column(CanBeNull = false, Name = "SUM_SAL")]
        public decimal? SumSal
        {
            get
            {
                return GetDataRowField<decimal?>(() => SumSal);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SumSal, value);
            }
        }

        /// <summary>
        /// ССылка на перевод и общие данные сотрудинка
        /// </summary>
        public EmpAccountData Transfer
        {
            get
            {
                if (_transfer == null)
                    _transfer = this.GetParentEntity<EmpAccountData>("EMP_ACCOUNT_DATA", DataSet, "TRANSFER_ID", this.TransferID);
                return _transfer;
            }
        }

        /// <summary>
        /// Счет куда перечислять сотруднику
        /// </summary>
        public ClientAccountData ClientAccount
        {
            get
            {
                return this.GetParentEntity<ClientAccountData>("CLIENT_ACCOUNT", DataSet, "CLIENT_ACCOUNT_ID", ClientAccountID);
            }
        }

        /// <summary>
        /// Источник данных для сотрудников - список его доступных счетов
        /// </summary>
        public List<ClientAccountData> ClientAccountsSource
        {
            get
            {
                return DataSet.Tables["CLIENT_ACCOUNT"].Select(string.Format("PER_NUM='{0}'", this.Transfer == null ? "" : this.Transfer.PerNum))
                    .Select(r => new ClientAccountData() { DataRow = r }).OrderBy(r=>r.OrderNumber).ToList();
            }
        }
    }
}
