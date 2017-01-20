using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;
using Oracle.DataAccess.Client;
using Salary.Helpers;
using System.ComponentModel;
using System.Windows;
using EntityGenerator;
using System.Data.Linq.Mapping;
using System.Windows.Controls;
using LibrarySalary.Helpers;

namespace Salary.Model
{
    [Table(Name="RETENTION")]
    public class RetentionModel : RowEntityBase, IDataErrorInfo
    {
        DataSet ds;

        OracleDataAdapter odaSave;
        OracleDataAdapter odaClientAccount;
        OracleDataAdapter odaClient_Retent_Relation;
        private ClientRetentRelationModel _currentAccountRelation;

        public RetentionModel(Decimal? transfer_id, decimal? retention_id = null)
        {
            ds = new DataSet();

            #region Инициализация адаптера сохранения удержания

            odaSave = new OracleDataAdapter(Queries.GetQueryWithSchema(@"SelectRetentionData.sql"), Connect.CurConnect);
            odaSave.SelectCommand.BindByName = true;
            odaSave.SelectCommand.Parameters.Add("p_retention_id", OracleDbType.Decimal, retention_id, ParameterDirection.Input);
            odaSave.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, transfer_id, ParameterDirection.Input);
            odaSave.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSave.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSave.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSave.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSave.SelectCommand.Parameters.Add("c5", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSave.SelectCommand.Parameters.Add("c6", OracleDbType.RefCursor, ParameterDirection.Output);
            
            odaSave.InsertCommand = new OracleCommand(string.Format(@"begin {1}.RETENTION_UPDATE
                          (
                           :p_RETENTION_ID
                          ,:p_TRANSFER_ID
                          ,:p_PAYMENT_TYPE_ID
                          ,:p_ORDER_NUMBER
                          ,:p_ORIGINAL_SUM
                          ,:p_RETENT_PERCENT
                          ,:p_RETENT_SUM
                          ,:p_REMAIN_SUM
                          ,:p_DATE_ADD
                          ,:p_DATE_START_RET
                          ,:p_DATE_END_RET
                          ,:p_SALARY_DOC_ID 
                          ,:p_POST_TRANSFER_SIGN
                          ,:p_DATE_REM_CALC
                          ); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSave.InsertCommand.BindByName = true;
            odaSave.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").DbType = DbType.Decimal;
            odaSave.InsertCommand.Parameters["P_RETENTION_ID"].Direction = ParameterDirection.InputOutput;
            odaSave.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSave.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            odaSave.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            odaSave.InsertCommand.Parameters.Add("p_ORDER_NUMBER", OracleDbType.Decimal, 0, "ORDER_NUMBER");
            odaSave.InsertCommand.Parameters.Add("p_ORIGINAL_SUM", OracleDbType.Decimal, 0, "ORIGINAL_SUM");
            odaSave.InsertCommand.Parameters.Add("p_RETENT_PERCENT", OracleDbType.Decimal, 0, "RETENT_PERCENT");
            odaSave.InsertCommand.Parameters.Add("p_RETENT_SUM", OracleDbType.Decimal, 0, "RETENT_SUM");
            odaSave.InsertCommand.Parameters.Add("p_REMAIN_SUM", OracleDbType.Decimal, 0, "REMAIN_SUM");
            odaSave.InsertCommand.Parameters.Add("p_DATE_ADD", OracleDbType.Date, 0, "DATE_ADD");
            odaSave.InsertCommand.Parameters.Add("p_DATE_START_RET", OracleDbType.Date, 0, "DATE_START_RET");
            odaSave.InsertCommand.Parameters.Add("p_DATE_END_RET", OracleDbType.Date, 0, "DATE_END_RET");
            odaSave.InsertCommand.Parameters.Add("p_SALARY_DOC_ID", OracleDbType.Decimal, 0, "SALARY_DOC_ID");
            odaSave.InsertCommand.Parameters.Add("p_POST_TRANSFER_SIGN", OracleDbType.Decimal, 0, "POST_TRANSFER_SIGN");
            odaSave.InsertCommand.Parameters.Add("p_DATE_REM_CALC", OracleDbType.Date, 0, "DATE_REM_CALC");

            odaSave.UpdateCommand = new OracleCommand(string.Format(@"begin {1}.RETENTION_UPDATE
                          (
                           :p_RETENTION_ID
                          ,:p_TRANSFER_ID
                          ,:p_PAYMENT_TYPE_ID
                          ,:p_ORDER_NUMBER
                          ,:p_ORIGINAL_SUM
                          ,:p_RETENT_PERCENT
                          ,:p_RETENT_SUM
                          ,:p_REMAIN_SUM
                          ,:p_DATE_ADD
                          ,:p_DATE_START_RET
                          ,:p_DATE_END_RET
                          ,:p_SALARY_DOC_ID 
                          ,:p_POST_TRANSFER_SIGN
                          ,:p_DATE_REM_CALC
                          ); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSave.UpdateCommand.BindByName = true;
            odaSave.UpdateCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").DbType = DbType.Decimal;
            odaSave.UpdateCommand.Parameters["P_RETENTION_ID"].Direction = ParameterDirection.InputOutput;
            odaSave.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSave.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            odaSave.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            odaSave.UpdateCommand.Parameters.Add("p_ORDER_NUMBER", OracleDbType.Decimal, 0, "ORDER_NUMBER");
            odaSave.UpdateCommand.Parameters.Add("p_ORIGINAL_SUM", OracleDbType.Decimal, 0, "ORIGINAL_SUM");
            odaSave.UpdateCommand.Parameters.Add("p_RETENT_PERCENT", OracleDbType.Decimal, 0, "RETENT_PERCENT");
            odaSave.UpdateCommand.Parameters.Add("p_RETENT_SUM", OracleDbType.Decimal, 0, "RETENT_SUM");
            odaSave.UpdateCommand.Parameters.Add("p_REMAIN_SUM", OracleDbType.Decimal, 0, "REMAIN_SUM");
            odaSave.UpdateCommand.Parameters.Add("p_DATE_ADD", OracleDbType.Date, 0, "DATE_ADD");
            odaSave.UpdateCommand.Parameters.Add("p_DATE_START_RET", OracleDbType.Date, 0, "DATE_START_RET");
            odaSave.UpdateCommand.Parameters.Add("p_DATE_END_RET", OracleDbType.Date, 0, "DATE_END_RET");
            odaSave.UpdateCommand.Parameters.Add("p_SALARY_DOC_ID", OracleDbType.Decimal, 0, "SALARY_DOC_ID");
            odaSave.UpdateCommand.Parameters.Add("p_POST_TRANSFER_SIGN", OracleDbType.Decimal, 0, "POST_TRANSFER_SIGN");
            odaSave.UpdateCommand.Parameters.Add("p_DATE_REM_CALC", OracleDbType.Date, 0, "DATE_REM_CALC");
            #endregion

            odaClientAccount = new OracleDataAdapter(string.Format(Queries.GetQuery(@"SelectClientAccountForRetent.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaClientAccount.SelectCommand.BindByName = true;
            odaClientAccount.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, transfer_id, ParameterDirection.Input);
            odaClientAccount.SelectCommand.Parameters.Add("p_payment_type_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaClientAccount.TableMappings.Add("Table", "CLIENT_ACCOUNT");

            #region Адаптер сохранения перечисления

            odaClient_Retent_Relation = new OracleDataAdapter();
            odaClient_Retent_Relation.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.CLIENT_RETENT_RELATION_UPDATE(:p_CLIENT_RETENT_RELATION_ID,:p_RETENTION_ID,:p_CLIENT_ACCOUNT_ID,:p_DATE_BEGIN_RELATION,
                                            :p_DATE_END_RELATION,:p_RESTRICT_SUM,:p_RELATION_COMMENT, :p_BCC_CODE, :p_OKATO);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaClient_Retent_Relation.InsertCommand.BindByName = true;
            odaClient_Retent_Relation.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaClient_Retent_Relation.InsertCommand.Parameters.Add("p_CLIENT_RETENT_RELATION_ID", OracleDbType.Decimal, 0, "CLIENT_RETENT_RELATION_ID").Direction = ParameterDirection.InputOutput;
            odaClient_Retent_Relation.InsertCommand.Parameters["p_CLIENT_RETENT_RELATION_ID"].DbType = DbType.Decimal;
            odaClient_Retent_Relation.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal).Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.InsertCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.InsertCommand.Parameters.Add("p_DATE_BEGIN_RELATION", OracleDbType.Date, 0, "DATE_BEGIN_RELATION").Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.InsertCommand.Parameters.Add("p_DATE_END_RELATION", OracleDbType.Date, 0, "DATE_END_RELATION").Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.InsertCommand.Parameters.Add("p_RESTRICT_SUM", OracleDbType.Decimal, 0, "RESTRICT_SUM").Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.InsertCommand.Parameters.Add("p_RELATION_COMMENT", OracleDbType.Varchar2, 0, "RELATION_COMMENT").Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.InsertCommand.Parameters.Add("p_BCC_CODE", OracleDbType.Varchar2, 0, "BCC_CODE").Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.InsertCommand.Parameters.Add("p_OKATO", OracleDbType.Varchar2, 0, "OKATO").Direction = ParameterDirection.Input;

            odaClient_Retent_Relation.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.CLIENT_RETENT_RELATION_UPDATE(:p_CLIENT_RETENT_RELATION_ID,:p_RETENTION_ID,:p_CLIENT_ACCOUNT_ID,
                        :p_DATE_BEGIN_RELATION,:p_DATE_END_RELATION,:p_RESTRICT_SUM,:p_RELATION_COMMENT, :p_BCC_CODE, :p_OKATO);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaClient_Retent_Relation.UpdateCommand.BindByName = true;
            odaClient_Retent_Relation.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaClient_Retent_Relation.UpdateCommand.Parameters.Add("p_CLIENT_RETENT_RELATION_ID", OracleDbType.Decimal, 0, "CLIENT_RETENT_RELATION_ID").Direction = ParameterDirection.InputOutput;
            odaClient_Retent_Relation.UpdateCommand.Parameters["p_CLIENT_RETENT_RELATION_ID"].DbType = DbType.Decimal;
            odaClient_Retent_Relation.UpdateCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal).Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.UpdateCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.UpdateCommand.Parameters.Add("p_DATE_BEGIN_RELATION", OracleDbType.Date, 0, "DATE_BEGIN_RELATION").Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.UpdateCommand.Parameters.Add("p_DATE_END_RELATION", OracleDbType.Date, 0, "DATE_END_RELATION").Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.UpdateCommand.Parameters.Add("p_RESTRICT_SUM", OracleDbType.Decimal, 0, "RESTRICT_SUM").Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.UpdateCommand.Parameters.Add("p_RELATION_COMMENT", OracleDbType.Varchar2, 0, "RELATION_COMMENT").Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.UpdateCommand.Parameters.Add("p_BCC_CODE", OracleDbType.Varchar2, 0, "BCC_CODE").Direction = ParameterDirection.Input;
            odaClient_Retent_Relation.UpdateCommand.Parameters.Add("p_OKATO", OracleDbType.Varchar2, 0, "OKATO").Direction = ParameterDirection.Input;

            odaClient_Retent_Relation.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.CLIENT_RETENT_RELATION_DELETE(:p_CLIENT_RETENT_RELATION_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaClient_Retent_Relation.DeleteCommand.BindByName = true;
            odaClient_Retent_Relation.DeleteCommand.Parameters.Add("p_CLIENT_RETENT_RELATION_ID", OracleDbType.Decimal, 0, "CLIENT_RETENT_RELATION_ID").Direction = ParameterDirection.InputOutput;

            #endregion
            
            odaSave.TableMappings.Add("Table", "RETENTION");
            odaSave.TableMappings.Add("Table1", "PAYMENT_TYPE");
            odaSave.TableMappings.Add("Table2", "EMP_DATA");
            odaSave.TableMappings.Add("Table3", "CLIENT_RETENT_RELATION");
            odaSave.TableMappings.Add("Table4", "SAL_DOCS");
            odaSave.TableMappings.Add("Table5", "SAL_RETENT_DATA");

            odaSave.Fill(ds);

            if (RetentionTable.Rows.Count == 0)
            {
                DataRow r = ds.Tables["RETENTION"].NewRow();
                r["TRANSFER_ID"] = (transfer_id==null?DBNull.Value:(object)transfer_id);
                ds.Tables["RETENTION"].Rows.Add(r);
                this.DataRow = r;
            }
            else
                this.DataRow = RetentionTable.Rows[0];

            GetClientAccounts(TRANSFER_ID, PAYMENT_TYPE_ID);

            ds.Tables["CLIENT_RETENT_RELATION"].DefaultView.Sort = "DATE_BEGIN_RELATION";

            if (ds.Tables["EMP_DATA"].Rows.Count == 0)
                ds.Tables["EMP_DATA"].Rows.Add(ds.Tables["EMP_DATA"].NewRow());
        }

        public DataTable RetentionTable
        {
            get
            {
                if (ds != null && ds.Tables.Contains("RETENTION"))
                    return ds.Tables["RETENTION"];
                else
                    return null;
            }
        }

        public DataTable ClientRelationTable
        {
            get
            {
                return ds.Tables["CLIENT_RETENT_RELATION"];
            }
        }

        DataView _vClientSource;
        public DataView ClientAccountSource
        {
            get
            {
                if (_vClientSource==null)
                    _vClientSource = new DataView(ds.Tables["CLIENT_ACCOUNT"], "TYPE_ACCOUNT_ID<>5", "", DataViewRowState.CurrentRows);
                return _vClientSource;
            }
        }

        DataView _compAccountSource;

        /// <summary>
        /// Источник данных - список компаний
        /// </summary>
        public DataView CompanyAccountSource
        {
            get
            {
                if (_compAccountSource==null)
                    _compAccountSource =  new DataView(ds.Tables["CLIENT_ACCOUNT"], "TYPE_ACCOUNT_ID=5", "COMPANY_NAME, NUMBER_ACCOUNT", DataViewRowState.CurrentRows);
                return _compAccountSource;
            }
        }

        EntityRelationList<ClientRetentRelationModel> _clientRelSource;
        public EntityRelationList<ClientRetentRelationModel> ClientAccountRelationSource
        {
            get
            {
                if (_clientRelSource == null && ds != null && ds.Tables.Contains("CLIENT_RETENT_RELATION"))
                {
                    _clientRelSource = this.CreateRelationCollection<ClientRetentRelationModel>(ds, "RETENTION_ID");
                    _clientRelSource.ListChanged += new ListChangedEventHandler(_clientRelSource_ListChanged);
                }
                return _clientRelSource;
            }
        }

        /// <summary>
        /// Обрабатываем изменения элемента списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _clientRelSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            RaisePropertyChanged(() => Error);// провереям есть ли ошибки в данных
        }

        public DataView RetentSalaryByRetentSource
        {
            get
            {
                return ds.Tables["SAL_RETENT_DATA"].DefaultView;
            }
        }

        public DataView PaymentSource
        {
            get
            {
                return new DataView(ds.Tables["PAYMENT_TYPE"], "", "CODE_PAYMENT", DataViewRowState.CurrentRows);
            }
        }

        public DataView SalDocSource
        {
            get
            {
                return ds.Tables["SAL_DOCS"].DefaultView;
            }
        }

        /// <summary>
        /// Признак того что может быть больше одного перечисления для данного вида оплат. Например перечисление по суду в разные организации
        /// </summary>
        public bool SignManyAccount
        {
            get
            {
                if (PAYMENT_TYPE_ID != null)
                    return PaymentSource.OfType<DataRowView>().Where(r => r.Row.Field2<Decimal?>("PAYMENT_TYPE_ID") == PAYMENT_TYPE_ID).Select(r => r.Row.Field2<decimal?>("SIGN_MANY_ACCOUNT")==1m).FirstOrDefault();
                else return false;
            }
        }

    #region Class Methods
        private void GetClientAccounts(decimal? transfer_id, decimal? payment_type_id)
        {
            if (ds.Tables.Contains("CLIENT_ACCOUNT"))
                ds.Tables["CLIENT_ACCOUNT"].Clear();
            odaClientAccount.SelectCommand.Parameters["p_transfer_id"].Value = transfer_id;
            odaClientAccount.SelectCommand.Parameters["p_payment_type_id"].Value = payment_type_id;
            odaClientAccount.Fill(ds);
            if (!ds.Tables["CLIENT_ACCOUNT"].Columns.Contains("DISP_EXP"))
            {
                ds.Tables["CLIENT_ACCOUNT"].Columns.Add("DISP_EXP").Expression = "NAME_TYPE_ACCOUNT+' '+PLF_NAME+ ' ('+PLF_ADDRESS+')'";
                ds.Tables["CLIENT_ACCOUNT"].PrimaryKey = new DataColumn[] { ds.Tables["CLIENT_ACCOUNT"].Columns["CLIENT_ACCOUNT_ID"] };
            }
            RaisePropertyChanged(() => ClientAccountSource);
        }

        public void UpdateClientAccounts()
        {
            GetClientAccounts(this.TRANSFER_ID, this.PAYMENT_TYPE_ID);
        }
        public void UpdateSalDoc()
        {
            if (ds.Tables.Contains("SAL_DOCS"))
                ds.Tables["SAL_DOCS"].PrimaryKey = new DataColumn[] { ds.Tables["SAL_DOCS"].Columns["SALARY_DOC_ID"] };
            //oda_salDocs.Fill(ds);
        }

        public bool Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                if (ds.Tables["RETENTION"].Rows[0].RowState == DataRowState.Added)
                    this.RETENTION_ID = null;
                odaSave.Update(new DataRow[] { ds.Tables["RETENTION"].Rows[0] });
                odaClient_Retent_Relation.UpdateCommand.Parameters["p_RETENTION_ID"].Value =
                    odaClient_Retent_Relation.InsertCommand.Parameters["p_RETENTION_ID"].Value = RETENTION_ID;
                odaClient_Retent_Relation.Update(ds.Tables["CLIENT_RETENT_RELATION"]);
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
                return false;
            }
        }
    #endregion

    #region Class Members

        [Column(Name = "RETENTION_ID")]
        public decimal? RETENTION_ID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RETENTION_ID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RETENTION_ID, value);
            }
        }

        [Column(Name = "TRANSFER_ID", CanBeNull = false)]
        public decimal? TRANSFER_ID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TRANSFER_ID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TRANSFER_ID, value);
                GetClientAccounts(TRANSFER_ID, PAYMENT_TYPE_ID);
                RaisePropertyChanged(() => Error);
            }
        }

        [Column(Name = "PAYMENT_TYPE_ID", CanBeNull = false)]
        public decimal? PAYMENT_TYPE_ID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PAYMENT_TYPE_ID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PAYMENT_TYPE_ID, value);
                GetClientAccounts(TRANSFER_ID, PAYMENT_TYPE_ID);
                RaisePropertyChanged(() => RETENT_SUM);
                RaisePropertyChanged(() => RETENT_PERCENT);
                RaisePropertyChanged(() => SignManyAccount);
                RaisePropertyChanged(() => Error);
            }

        }

        [Column(Name = "ORDER_NUMBER")]
        public decimal? ORDER_NUMBER
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ORDER_NUMBER);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ORDER_NUMBER, value);
            }

        }

        [Column(Name = "ORIGINAL_SUM")]
        public decimal? ORIGINAL_SUM
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ORIGINAL_SUM);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ORIGINAL_SUM, value);
                RaisePropertyChanged(() => REMAIN_SUM);
                RaisePropertyChanged(() => DATE_REM_CALC);
                RaisePropertyChanged(() => Error);
            }

        }

        [Column(Name = "RETENT_PERCENT")]
        public decimal? RETENT_PERCENT
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RETENT_PERCENT);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RETENT_PERCENT, value);
                RaisePropertyChanged(() => RETENT_SUM);
                RaisePropertyChanged(() => Error);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "RETENT_SUM")]
        public decimal? RETENT_SUM
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RETENT_SUM);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RETENT_SUM, value);
                RaisePropertyChanged(() => RETENT_PERCENT);
                RaisePropertyChanged(() => Error);
            }

        }

        [Column(Name = "REMAIN_SUM")]
        public decimal? REMAIN_SUM
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => REMAIN_SUM);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => REMAIN_SUM, value);
                RaisePropertyChanged(() => SumRemainNow);
                RaisePropertyChanged(() => Error);
            }

        }

        [Column(Name = "DATE_ADD")]
        public DateTime? DATE_ADD
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DATE_ADD);
            }
            private set
            {
                UpdateDataRow<DateTime?>(() => DATE_ADD, value);
            }

        }

        [Column(Name = "DATE_START_RET", CanBeNull = false)]
        public DateTime? DATE_START_RET
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DATE_START_RET);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DATE_START_RET, value);
                RaisePropertyChanged(() => Error);
            }

        }

        [Column(Name = "DATE_END_RET")]
        public DateTime? DATE_END_RET
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DATE_END_RET);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DATE_END_RET, value);
                RaisePropertyChanged(() => Error);
            }

        }

        [Column(Name = "SALARY_DOC_ID")]
        public decimal? SALARY_DOC_ID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SALARY_DOC_ID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SALARY_DOC_ID, value);
            }

        }

        [Column(Name = "DATE_REM_CALC")]
        public DateTime? DATE_REM_CALC
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DATE_REM_CALC);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DATE_REM_CALC, value);
                RaisePropertyChanged(() => SumRemainNow);
                RaisePropertyChanged(() => Error);
            }
        }

        public string EmpName
        {
            get
            {
                return ds.Tables["EMP_DATA"].Rows[0]["FIO"].ToString();
            }
            set
            {
                ds.Tables["EMP_DATA"].Rows[0]["FIO"] = value;
                RaisePropertyChanged(() => EmpName);
            }
        }

        public string PerNum
        {
            get
            {
                return ds.Tables["EMP_DATA"].Rows[0]["PER_NUM"].ToString();
            }
            set
            {
                ds.Tables["EMP_DATA"].Rows[0]["PER_NUM"] = value;
                RaisePropertyChanged(() => PerNum);
                RaisePropertyChanged(() => Error);
            }
        }

        public ClientRetentRelationModel CurrentAccountRelation
        {
            get
            {
                return _currentAccountRelation;
            }
            set
            {
                _currentAccountRelation = value;
                RaisePropertyChanged(() => CurrentAccountRelation);
            }
        }

        public decimal? SumRemainNow
        {
            get
            {
                if (REMAIN_SUM==null)
                    return null;
                else 
                {
                    DateTime minDate = DATE_REM_CALC??DateTime.MinValue;
                    Decimal? v =ds.Tables["SAL_RETENT_DATA"].Rows.OfType<DataRow>().Where(r => (r.Field2<DateTime?>("PAY_DATE") ?? DateTime.MinValue) >= minDate).Sum(r => r.Field2<Decimal?>("SUM_SAL"));
                    return REMAIN_SUM - v;
                }
            }
        }
    #endregion
        public string Error
        {
            get 
            { 
                if (PAYMENT_TYPE_ID == null) return "Требуется выбрать шифр оплат"; 
                if (DATE_START_RET == null) return "Требуется выбрать дату начала документа удержания"; 
                if (string.IsNullOrEmpty(PerNum)) return "Требуется выбрать сотрудника"; 
                if ((ORIGINAL_SUM ?? 0) != 0 && DATE_REM_CALC==null) return "Требуется выбрать дату, на которую рассчитан остаток"; 
                if ((ORIGINAL_SUM ?? 0) != 0 && REMAIN_SUM == null) return "Требуется указать остаток на дату рассчета остатка";
                if (PAYMENT_TYPE_ID != null && RETENT_SUM == null && RETENT_PERCENT == null &&
                                        PaymentSource.OfType<DataRowView>().Count(t => t.Row.Field2<Decimal?>("PAYMENT_TYPE_ID") == PAYMENT_TYPE_ID && t.Row.Field2<Decimal?>("SIGN_INDIVIDUAL") == 1m) > 0)
                    return "Требуется указать процент или сумму ежемесячного удержания";
                else
                    if (!SignManyAccount && ClientAccountRelationSource.OfType<ClientRetentRelationModel>().WithContext().Any(r => r.Previous != null && r.Previous.DateEndRelation >= r.Current.DateBeginRelation))
                        return "Даты перечислений не должны пересекаться";
                    else
                        return ClientAccountRelationSource.OfType<ClientRetentRelationModel>().Select(r => r.Error).FirstOrDefault(r => !string.IsNullOrEmpty(r)) ?? string.Empty;
                return null;
            }
        }

        public new string this[string columnName]
        {
            get 
            {
                switch (columnName)
                {
                    case "PAYMENT_TYPE_ID": if (PAYMENT_TYPE_ID == null) return "Требуется выбрать шифр оплат"; break;
                    case "DATE_START_RET": if (DATE_START_RET == null) return "Требуется выбрать дату начала документа удержания"; break;
                    case "PerNum": if (string.IsNullOrEmpty(PerNum)) return "Требуется выбрать сотрудника"; break;
                    case "DATE_REM_CALC": if ((ORIGINAL_SUM ?? 0) != 0 && DATE_REM_CALC==null) return "Требуется выбрать дату, на которую рассчитан остаток"; break;
                    case "REMAIN_SUM": if ((ORIGINAL_SUM ?? 0) != 0 && REMAIN_SUM == null) return "Требуется указать остаток на дату рассчета остатка(поле ниже)"; break;
                    case "RETENT_SUM": if (PAYMENT_TYPE_ID != null && RETENT_SUM == null && RETENT_PERCENT == null &&
                                        PaymentSource.OfType<DataRowView>().Count(t => t.Row.Field2<Decimal?>("PAYMENT_TYPE_ID") == PAYMENT_TYPE_ID && t.Row.Field2<Decimal?>("SIGN_INDIVIDUAL") == 1m) > 0)
                            return "Требуется указать процент или сумму ежемесячного удержания";break;
                    case "RETENT_PERCENT": if (PAYMENT_TYPE_ID != null && RETENT_SUM == null && RETENT_PERCENT == null &&
                                        PaymentSource.OfType<DataRowView>().Count(t => t.Row.Field2<Decimal?>("PAYMENT_TYPE_ID") == PAYMENT_TYPE_ID && t.Row.Field2<Decimal?>("SIGN_INDIVIDUAL") == 1m) > 0)
                            return "Требуется указать процент или сумму ежемесячного удержания";break;
                    default: return null;
                }
                return null;
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
        /// Добавляет новую строку перечисления в данные
        /// </summary>
        public void AddClientRetentRelation()
        {
            if (PAYMENT_TYPE_ID == null)
                return;
            if (!SignManyAccount)
            {
                DataRow last_row = ds.Tables["CLIENT_RETENT_RELATION"].Select("ISNULL(DATE_END_RELATION, #1/1/3000#)=#1/1/3000#", "DATE_BEGIN_RELATION", DataViewRowState.CurrentRows).LastOrDefault();
                if (last_row != null)
                {
                    last_row["DATE_END_RELATION"] = DateTime.Today.Trunc("Month").AddSeconds(-1);
                }
                DataRow r = ds.Tables["CLIENT_RETENT_RELATION"].NewRow();
                r["DATE_BEGIN_RELATION"] = DateTime.Today.Trunc("Month");
                ds.Tables["CLIENT_RETENT_RELATION"].Rows.Add(r);
                ClientAccountRelationSource.Add(new ClientRetentRelationModel(r));
            }
            else
            { 
                DataRow r = ds.Tables["CLIENT_RETENT_RELATION"].NewRow();
                r["DATE_BEGIN_RELATION"] = new DateTime(1900, 01, 01);
                ds.Tables["CLIENT_RETENT_RELATION"].Rows.Add(r);
                ClientAccountRelationSource.Add(new ClientRetentRelationModel(r));
            }
            RaisePropertyChanged(()=> ClientAccountRelationSource);
        }

        /// <summary>
        /// Удаляет текущий выбранный элемент из коллекции
        /// </summary>
        public void DeleteCurrentAccountRelation()
        {
            ClientAccountRelationSource.Remove(CurrentAccountRelation);
            RaisePropertyChanged(() => ClientAccountRelationSource);
        }

    }

    [Table(Name="CLIENT_RETENT_RELATION")]
    public partial class ClientRetentRelationModel : ClientRetentRelation, IDataErrorInfo
    {
        public ClientRetentRelationModel()
        {
        }
        public ClientRetentRelationModel(DataRow r)
        {
            base.DataRow = r;
            _isEmpAccount = ClientAccount == null?true:ClientAccount.TransferID==null?false:true;
        }

        public string Error
        {
            get
            {
                if (ClientAccountID == null)
                    return "Не выбран счет для перечисления";
                else
                    if (DateBeginRelation > DateEndRelation)
                        return "Дата начала перечисления не может быть больше даты окончания перечисления";
                    else if (DateBeginRelation == null)
                        return "Не установлена дата начала перечисления";
                return string.Empty;
            }
        }

        bool _isEmpAccount;

        /// <summary>
        /// Признак привязан ли сотруднику счет, или это счет компании.
        /// </summary>
        public bool IsEmpAccount
        {
            get
            {
                return _isEmpAccount;
            }
            set
            {
                if (value != _isEmpAccount)
                {
                    _isEmpAccount = value;
                    RaisePropertyChanged(() => IsEmpAccount);
                }
            }
        }

        public new string this[string column_name]
        {
            get
            {
                if (column_name=="ClientAccountID")
                if (ClientAccountID == null)
                    return "Требуется выбрать счет перечисления";
                
                return string.Empty;
            }
        }

        public ClientAccount ClientAccount
        {
            get
            {
                if (ClientAccountID == null)
                    return null;
                else
                    return this.DataSet.Tables["CLIENT_ACCOUNT"].Rows.OfType<DataRow>().Where(r => r.RowState != DataRowState.Deleted && r.Field2<Decimal?>("CLIENT_ACCOUNT_ID") == ClientAccountID)
                        .Select(r => new ClientAccount() { DataRow = r }).FirstOrDefault();
            }
        }
    }

}
