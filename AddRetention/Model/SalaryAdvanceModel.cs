using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using EntityGenerator;
using System.ComponentModel;
using Oracle.DataAccess.Client;
using System.Windows;
using System.Data;
using LibrarySalary.Helpers;

namespace Salary.Model
{
    [Table(Name = "SALARY_ADVANCE")]
    public partial class SalaryAdvance : RowEntityBase, IDataErrorInfo
    {
        private static OracleDataAdapter odaSalary_Advance;
        DataSet ds;
        public SalaryAdvance(decimal? salary_advance_id, decimal? transfer_id)
        {

            odaSalary_Advance.SelectCommand.Parameters["p_salary_advance_id"].Value=salary_advance_id;
            odaSalary_Advance.SelectCommand.Parameters["p_transfer_id"].Value = transfer_id;
            ds = new DataSet();
            odaSalary_Advance.Fill(ds);
            if (ds.Tables["SALARY_ADVANCE"].Rows.Count == 0)
            {
                DataRow r = ds.Tables["SALARY_ADVANCE"].NewRow();
                r["TRANSFER_ID"] = transfer_id;
                r["PAY_DATE"] = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 16).AddSeconds(-1);
                r["SUBDIV_ID"] = ds.Tables["EMP"].Rows[0]["SUBDIV_ID"];
                ds.Tables["SALARY_ADVANCE"].Rows.Add(r);
            }
            this.DataRow = ds.Tables["SALARY_ADVANCE"].Rows[0];
            this.DataTable = this.DataRow.Table;
        }

        static SalaryAdvance()
        {
            #region Создание адаптера сохранения

            odaSalary_Advance = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectSalaryAdvanceData.sql"), Connect.CurConnect);
            odaSalary_Advance.SelectCommand.BindByName = true;
            odaSalary_Advance.SelectCommand.Parameters.Add("p_salary_advance_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaSalary_Advance.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaSalary_Advance.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Advance.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Advance.TableMappings.Add("Table", "salary_advance");
            odaSalary_Advance.TableMappings.Add("Table1", "emp");

            odaSalary_Advance.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_ADVANCE_UPDATE(:p_SALARY_ADVANCE_ID,:p_PAY_DATE,:p_PAYMENT_TYPE_ID,:p_HOURS,:p_SUM_SAL,:p_ZONE_ADD,:p_EXP_ADD,:p_ORDER_ID,:p_GROUP_MASTER,:p_DEGREE_ID,:p_TRANSFER_ID,:p_TYPE_REF_SALARY_ID,:p_REF_ID,:p_DAYS,:p_ACCOUNT_ADD_SIGN,:p_TIME_ADD_RECORD,:p_PER_NUM,:p_SIGN_COMB,:p_SUBDIV_ID,:p_RETENTION_ID,:p_TYPE_ROW_SALARY_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Advance.InsertCommand.BindByName = true;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_SALARY_ADVANCE_ID", OracleDbType.Decimal, 0, "SALARY_ADVANCE_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Advance.InsertCommand.Parameters["p_SALARY_ADVANCE_ID"].DbType = DbType.Decimal;
            odaSalary_Advance.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_ZONE_ADD", OracleDbType.Decimal, 0, "ZONE_ADD").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_EXP_ADD", OracleDbType.Decimal, 0, "EXP_ADD").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_GROUP_MASTER", OracleDbType.Varchar2, 0, "GROUP_MASTER").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_TYPE_REF_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_REF_SALARY_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_REF_ID", OracleDbType.Decimal, 0, "REF_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_DAYS", OracleDbType.Decimal, 0, "DAYS").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_ACCOUNT_ADD_SIGN", OracleDbType.Decimal, 0, "ACCOUNT_ADD_SIGN").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_TIME_ADD_RECORD", OracleDbType.Date, 0, "TIME_ADD_RECORD").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.InsertCommand.Parameters.Add("p_TYPE_ROW_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_ROW_SALARY_ID").Direction = ParameterDirection.Input;

            odaSalary_Advance.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_ADVANCE_UPDATE(:p_SALARY_ADVANCE_ID,:p_PAY_DATE,:p_PAYMENT_TYPE_ID,:p_HOURS,:p_SUM_SAL,:p_ZONE_ADD,:p_EXP_ADD,:p_ORDER_ID,:p_GROUP_MASTER,:p_DEGREE_ID,:p_TRANSFER_ID,:p_TYPE_REF_SALARY_ID,:p_REF_ID,:p_DAYS,:p_ACCOUNT_ADD_SIGN,:p_TIME_ADD_RECORD,:p_PER_NUM,:p_SIGN_COMB,:p_SUBDIV_ID,:p_RETENTION_ID,:p_TYPE_ROW_SALARY_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Advance.UpdateCommand.BindByName = true;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_SALARY_ADVANCE_ID", OracleDbType.Decimal, 0, "SALARY_ADVANCE_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Advance.UpdateCommand.Parameters["p_SALARY_ADVANCE_ID"].DbType = DbType.Decimal;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_ZONE_ADD", OracleDbType.Decimal, 0, "ZONE_ADD").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_EXP_ADD", OracleDbType.Decimal, 0, "EXP_ADD").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_GROUP_MASTER", OracleDbType.Varchar2, 0, "GROUP_MASTER").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_TYPE_REF_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_REF_SALARY_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_REF_ID", OracleDbType.Decimal, 0, "REF_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_DAYS", OracleDbType.Decimal, 0, "DAYS").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_ACCOUNT_ADD_SIGN", OracleDbType.Decimal, 0, "ACCOUNT_ADD_SIGN").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_TIME_ADD_RECORD", OracleDbType.Date, 0, "TIME_ADD_RECORD").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").Direction = ParameterDirection.Input;
            odaSalary_Advance.UpdateCommand.Parameters.Add("p_TYPE_ROW_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_ROW_SALARY_ID").Direction = ParameterDirection.Input;

            odaSalary_Advance.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_ADVANCE_DELETE(:p_SALARY_ADVANCE_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Advance.DeleteCommand.BindByName = true;
            odaSalary_Advance.DeleteCommand.Parameters.Add("p_SALARY_ADVANCE_ID", OracleDbType.Decimal, 0, "SALARY_ADVANCE_ID").Direction = ParameterDirection.InputOutput;

            #endregion
        }

        #region Class Members


        [Column(Name = "SALARY_ADVANCE_ID")]
        public Decimal? SalaryAdvanceID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryAdvanceID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryAdvanceID, value);
            }
        }


        [Column(Name = "TYPE_ROW_SALARY_ID")]
        public Decimal? TypeRowSalaryID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeRowSalaryID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeRowSalaryID, value);
            }
        }


        [Column(Name = "RETENTION_ID")]
        public Decimal? RetentionID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RetentionID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentionID, value);
            }
        }


        [Column(Name = "SUBDIV_ID")]
        public Decimal? SubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }


        [Column(Name = "SIGN_COMB")]
        public Decimal? SignComb
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignComb);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignComb, value);
            }
        }


        [Column(Name = "PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }


        [Column(Name = "TIME_ADD_RECORD")]
        public DateTime? TimeAddRecord
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => TimeAddRecord);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => TimeAddRecord, value);
            }
        }


        [Column(Name = "ACCOUNT_ADD_SIGN")]
        public Decimal? AccountAddSign
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => AccountAddSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AccountAddSign, value);
            }
        }


        [Column(Name = "DAYS")]
        public Decimal? Days
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Days);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Days, value);
            }
        }


        [Column(Name = "REF_ID")]
        public Decimal? RefID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RefID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RefID, value);
            }
        }


        [Column(Name = "TYPE_REF_SALARY_ID")]
        public Decimal? TypeRefSalaryID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeRefSalaryID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeRefSalaryID, value);
            }
        }


        [Column(Name = "TRANSFER_ID")]
        public Decimal? TransferID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TransferID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TransferID, value);
            }
        }


        [Column(Name = "DEGREE_ID")]
        public Decimal? DegreeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DegreeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DegreeID, value);
            }
        }


        [Column(Name = "GROUP_MASTER")]
        public String GroupMaster
        {
            get
            {
                return this.GetDataRowField<String>(() => GroupMaster);
            }
            set
            {
                UpdateDataRow<String>(() => GroupMaster, value);
            }
        }


        [Column(Name = "ORDER_ID")]
        public Decimal? OrderID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => OrderID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OrderID, value);
            }
        }


        [Column(Name = "EXP_ADD")]
        public Decimal? ExpAdd
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ExpAdd);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ExpAdd, value);
            }
        }


        [Column(Name = "ZONE_ADD")]
        public Decimal? ZoneAdd
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ZoneAdd);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ZoneAdd, value);
            }
        }


        [Column(Name = "SUM_SAL")]
        public Decimal? SumSal
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SumSal);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SumSal, value);
            }
        }


        [Column(Name = "HOURS")]
        public Decimal? Hours
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Hours);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Hours, value);
            }
        }


        [Column(Name = "PAYMENT_TYPE_ID")]
        public Decimal? PaymentTypeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentTypeID, value);
            }
        }


        [Column(Name = "PAY_DATE")]
        public DateTime? PayDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => PayDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => PayDate, value);
            }
        }
        #endregion

        public string Error
        {
            get 
            { 
                if (PayDate == null) return "Требуется выбрать дату";
                if (SubdivID == null) return "Требуется выбрать подразделение"; 
                if (TransferID == null) return "Требуется выбрать сотрудника"; 
                if (PaymentTypeID == null) return "Требуется выбрать вид оплат";
                return string.Empty;
            }
        }

        public string FIO
        {
            get
            {
                return ds.Tables["EMP"].Rows[0]["FIO"].ToString();
            }
        }

        public new string this[string column_name]
        {
            get
            {
                switch (column_name)
                {
                    case "PayDate": if (PayDate == null) return "Требуется выбрать дату";break;
                    case "SubdivID": if (SubdivID == null) return "Требуется выбрать подразделение"; break;
                    case "TransferID": if (TransferID == null) return "Требуется выбрать сотрудника"; break;
                    case "PaymentTypeID": if (PaymentTypeID == null) return "Требуется выбрать вид оплат"; break;
                    default: return string.Empty;
                }
                return string.Empty;
            }
        }

        public bool Save(OracleTransaction transaction_now=null)
        {
            OracleTransaction tr = transaction_now ?? Connect.CurConnect.BeginTransaction();
            try
            {
                odaSalary_Advance.Update(new DataRow[]{this.DataRow});
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

        public DataView SubdivSource
        {
            get
            {
                return new DataView(AppDataSet.Tables["ACCESS_SUBDIV"], "APP_NAME='SALARY'", "CODE_SUBDIV", DataViewRowState.CurrentRows);
            }
        }
        public DataView DegreeSource
        {
            get
            {
                return new DataView(AppDataSet.Tables["DEGREE"], "", "CODE_DEGREE", DataViewRowState.CurrentRows);
            }
        }
        public DataView PaymentTypeSource
        {
            get
            {
                return new DataView(AppDataSet.Tables["PAYMENT_TYPE"], "TYPE_PAYMENT_TYPE_ID=1", "CODE_PAYMENT", DataViewRowState.CurrentRows);
            }
        }

        public static bool DeleteSalaryAdvance(decimal[] salary_advance_ids, OracleTransaction trans = null)
        {
            OracleTransaction tr = trans ?? Connect.CurConnect.BeginTransaction();
            try
            {
                foreach (var r in salary_advance_ids)
                {
                    odaSalary_Advance.DeleteCommand.Parameters["p_salary_advance_id"].Value = r;
                    odaSalary_Advance.DeleteCommand.ExecuteNonQuery();
                }
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления записей");
                return false;
            }
        }
    }
}
