using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using EntityGenerator;
using System.Windows;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using Salary.Helpers;
using System.Collections.Concurrent;
using System.Windows.Threading;
using System.Xml.Linq;
using System.IO;
using System.Data.Common;
using LibrarySalary.Helpers;

namespace Salary.Model
{
    [Table(Name = "SALARY_DOCUM")]
    public partial class SalaryDocumModel: RowEntityBase, IDataErrorInfo
    {
        DataSet ds, ds_rel;
        OracleDataAdapter odaSalary_Docum, odaSalary_Docum_Detail, odaSalary_Docum_Period, odaSalary_Docum_Pay_Change, odaAllSalDocum;
        OracleCommand cmdCalc_Emp_Percent_Standing;
        BackgroundWorker bw_loadPayment;
        public SalaryDocumModel(decimal? subdiv_id, decimal? transfer_id, decimal? salary_docum_id=null, DateTime? selectedDate =null)
        {
            ds = new DataSet();
            ds_rel = new DataSet();
            odaAllSalDocum = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectSalaryDocumDataAllDoc.sql"), Connect.CurConnect);
            odaAllSalDocum.SelectCommand.BindByName = true;
            odaAllSalDocum.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, transfer_id, ParameterDirection.Input);
            odaAllSalDocum.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, selectedDate??DateTime.Today, ParameterDirection.Input);
            odaAllSalDocum.SelectCommand.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, salary_docum_id, ParameterDirection.Input);
            odaAllSalDocum.TableMappings.Add("Table", "SALARY_DOCUM");

            odaSalary_Docum = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectSalaryDocumData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum.SelectCommand.BindByName = true;
            odaSalary_Docum.SelectCommand.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, salary_docum_id, ParameterDirection.Input);
            odaSalary_Docum.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, transfer_id, ParameterDirection.Input);
            odaSalary_Docum.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, selectedDate??DateTime.Today , ParameterDirection.Input);
            odaSalary_Docum.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Docum.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Docum.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Docum.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Docum.SelectCommand.Parameters.Add("c5", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Docum.SelectCommand.Parameters.Add("c6", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Docum.SelectCommand.Parameters.Add("c7", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Docum.SelectCommand.Parameters.Add("c8", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Docum.SelectCommand.Parameters.Add("c9", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Docum.TableMappings.Add("Table", "SALARY_DOCUM");
            odaSalary_Docum.TableMappings.Add("Table1", "TYPE_SAL_DOCUM");
            odaSalary_Docum.TableMappings.Add("Table2", "EMP");
            odaSalary_Docum.TableMappings.Add("Table3", "TYPE_DOCUM_PAY_CALC");
            odaSalary_Docum.TableMappings.Add("Table4", "SALARY_DOCUM_DETAIL");
            odaSalary_Docum.TableMappings.Add("Table5", "SALARY_DOCUM_PERIOD");
            odaSalary_Docum.TableMappings.Add("Table6", "DOC_LIST");
            odaSalary_Docum.TableMappings.Add("Table7", "REG_DOC");
            odaSalary_Docum.TableMappings.Add("Table8", "SALARY_DOCUM_PAY_CHANGE");
#region Создание адаптера
            odaSalary_Docum.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_DOCUM_UPDATE(:p_SALARY_DOCUM_ID,:p_CODE_DOC,:p_NAME_DOC,:p_DATE_DOC,:p_DATE_CLOSE,:p_TYPE_SAL_DOCUM_ID,:p_DOC_SUBDIV_ID,:p_DATE_FORM_DOCUM,:p_TRANSFER_ID,:p_DOC_END,
                    :p_LAST_CALC_DATE, :p_DOC_BEGIN, :p_COUNT_RESTRICT_DAYS, :p_REG_DOC_ID, :p_BASIC_DOC_SIGN, :p_RELATED_DOCUM_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum.InsertCommand.BindByName = true;
            odaSalary_Docum.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Docum.InsertCommand.Parameters["p_SALARY_DOCUM_ID"].DbType = DbType.Decimal;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_CODE_DOC", OracleDbType.Varchar2, 0, "CODE_DOC").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_NAME_DOC", OracleDbType.Varchar2, 0, "NAME_DOC").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_DATE_DOC", OracleDbType.Date, 0, "DATE_DOC").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_DATE_CLOSE", OracleDbType.Date, 0, "DATE_CLOSE").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_TYPE_SAL_DOCUM_ID", OracleDbType.Decimal, 0, "TYPE_SAL_DOCUM_ID").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_DOC_SUBDIV_ID", OracleDbType.Decimal, 0, "DOC_SUBDIV_ID").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_DATE_FORM_DOCUM", OracleDbType.Date, 0, "DATE_FORM_DOCUM").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_DOC_END", OracleDbType.Date, 0, "DOC_END").Direction = ParameterDirection.Input;
            /*odaSalary_Docum.InsertCommand.Parameters.Add("p_PAYMENT_SUM", OracleDbType.Decimal, 0, "PAYMENT_SUM").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_PAYMENT_PERCENT", OracleDbType.Decimal, 0, "PAYMENT_PERCENT").Direction = ParameterDirection.Input;
            */odaSalary_Docum.InsertCommand.Parameters.Add("p_LAST_CALC_DATE", OracleDbType.Date, 0, "LAST_CALC_DATE").Direction = ParameterDirection.Input; 
            odaSalary_Docum.InsertCommand.Parameters.Add("p_DOC_BEGIN", OracleDbType.Date, 0, "DOC_BEGIN").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_COUNT_RESTRICT_DAYS", OracleDbType.Decimal, 0, "COUNT_RESTRICT_DAYS").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_REG_DOC_ID", OracleDbType.Decimal, 0, "REG_DOC_ID").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_BASIC_DOC_SIGN", OracleDbType.Decimal, 0, "BASIC_DOC_SIGN").Direction = ParameterDirection.Input;
            odaSalary_Docum.InsertCommand.Parameters.Add("p_RELATED_DOCUM_ID", OracleDbType.Decimal, 0, "RELATED_DOCUM_ID").Direction = ParameterDirection.Input;

            odaSalary_Docum.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_DOCUM_UPDATE(:p_SALARY_DOCUM_ID,:p_CODE_DOC,:p_NAME_DOC,:p_DATE_DOC,:p_DATE_CLOSE,:p_TYPE_SAL_DOCUM_ID,:p_DOC_SUBDIV_ID,:p_DATE_FORM_DOCUM,:p_TRANSFER_ID,:p_DOC_END,
                    :p_LAST_CALC_DATE, :p_DOC_BEGIN, :p_COUNT_RESTRICT_DAYS, :p_REG_DOC_ID, :p_BASIC_DOC_SIGN, :p_RELATED_DOCUM_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum.UpdateCommand.BindByName = true;
            odaSalary_Docum.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Docum.UpdateCommand.Parameters["p_SALARY_DOCUM_ID"].DbType = DbType.Decimal;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_CODE_DOC", OracleDbType.Varchar2, 0, "CODE_DOC").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_NAME_DOC", OracleDbType.Varchar2, 0, "NAME_DOC").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_DATE_DOC", OracleDbType.Date, 0, "DATE_DOC").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_DATE_CLOSE", OracleDbType.Date, 0, "DATE_CLOSE").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_TYPE_SAL_DOCUM_ID", OracleDbType.Decimal, 0, "TYPE_SAL_DOCUM_ID").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_DOC_SUBDIV_ID", OracleDbType.Decimal, 0, "DOC_SUBDIV_ID").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_DATE_FORM_DOCUM", OracleDbType.Date, 0, "DATE_FORM_DOCUM").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_DOC_END", OracleDbType.Date, 0, "DOC_END").Direction = ParameterDirection.Input;
            /*odaSalary_Docum.UpdateCommand.Parameters.Add("p_PAYMENT_SUM", OracleDbType.Decimal, 0, "PAYMENT_SUM").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_PAYMENT_PERCENT", OracleDbType.Decimal, 0, "PAYMENT_PERCENT").Direction = ParameterDirection.Input;
            */odaSalary_Docum.UpdateCommand.Parameters.Add("p_LAST_CALC_DATE", OracleDbType.Date, 0, "LAST_CALC_DATE").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_DOC_BEGIN", OracleDbType.Date, 0, "DOC_BEGIN").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_COUNT_RESTRICT_DAYS", OracleDbType.Decimal, 0, "COUNT_RESTRICT_DAYS").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_REG_DOC_ID", OracleDbType.Decimal, 0, "REG_DOC_ID").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_BASIC_DOC_SIGN", OracleDbType.Decimal, 0, "BASIC_DOC_SIGN").Direction = ParameterDirection.Input;
            odaSalary_Docum.UpdateCommand.Parameters.Add("p_RELATED_DOCUM_ID", OracleDbType.Decimal, 0, "RELATED_DOCUM_ID").Direction = ParameterDirection.Input;
            

            odaSalary_Docum.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_DOCUM_DELETE(:p_SALARY_DOCUM_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum.DeleteCommand.BindByName = true;
            odaSalary_Docum.DeleteCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID").Direction = ParameterDirection.InputOutput;

            odaSalary_Docum.AcceptChangesDuringUpdate = false;

            odaSalary_Docum_Detail = new OracleDataAdapter("", Connect.CurConnect);
            odaSalary_Docum_Detail.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_DOCUM_DETAIL_UPDATE(:p_SALARY_DOCUM_DETAIL_ID,:p_PAYMENT_TYPE_ID,:p_PAYMENT_SUM,:p_SALARY_DOCUM_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum_Detail.InsertCommand.BindByName=true;
            odaSalary_Docum_Detail.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSalary_Docum_Detail.InsertCommand.Parameters.Add("p_SALARY_DOCUM_DETAIL_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_DETAIL_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Docum_Detail.InsertCommand.Parameters["p_SALARY_DOCUM_DETAIL_ID"].DbType = DbType.Decimal;
            odaSalary_Docum_Detail.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input;
            odaSalary_Docum_Detail.InsertCommand.Parameters.Add("p_PAYMENT_SUM", OracleDbType.Decimal, 0, "PAYMENT_SUM").Direction = ParameterDirection.Input;
            odaSalary_Docum_Detail.InsertCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, null, ParameterDirection.Input);	
            
            odaSalary_Docum_Detail.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_DOCUM_DETAIL_UPDATE(:p_SALARY_DOCUM_DETAIL_ID,:p_PAYMENT_TYPE_ID,:p_PAYMENT_SUM,:p_SALARY_DOCUM_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum_Detail.UpdateCommand.BindByName=true;
            odaSalary_Docum_Detail.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSalary_Docum_Detail.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_DETAIL_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_DETAIL_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Docum_Detail.UpdateCommand.Parameters["p_SALARY_DOCUM_DETAIL_ID"].DbType = DbType.Decimal;
            odaSalary_Docum_Detail.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input;
            odaSalary_Docum_Detail.UpdateCommand.Parameters.Add("p_PAYMENT_SUM", OracleDbType.Decimal, 0, "PAYMENT_SUM").Direction = ParameterDirection.Input;
            odaSalary_Docum_Detail.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, null, ParameterDirection.Input);	
            
            odaSalary_Docum_Detail.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_DOCUM_DETAIL_DELETE(:p_SALARY_DOCUM_DETAIL_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum_Detail.DeleteCommand.BindByName=true;
            odaSalary_Docum_Detail.DeleteCommand.Parameters.Add("p_SALARY_DOCUM_DETAIL_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_DETAIL_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Docum_Detail.AcceptChangesDuringUpdate = false;

            cmd_CalcPayment = new OracleCommand(string.Format("select {1}.CALC_PAYMENT_VALUE(:p_payment_type_id, :p_transfer_id, :p_date, :p_dates_array) from dual", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmd_CalcPayment.Parameters.Add("p_payment_type_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            cmd_CalcPayment.Parameters.Add("p_transfer_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            cmd_CalcPayment.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            cmd_CalcPayment.Parameters.Add("p_dates_array", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName = "SALARY.DATE_COLLECTION_TYPE";

            odaSalary_Docum_Period = new OracleDataAdapter("", Connect.CurConnect);
            odaSalary_Docum_Period.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_DOCUM_PERIOD_UPDATE(:p_SALARY_DOCUM_PERIOD_ID,:p_BEGIN_PERIOD,:p_END_PERIOD,:p_SALARY_DOCUM_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum_Period.InsertCommand.BindByName = true;
            odaSalary_Docum_Period.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSalary_Docum_Period.InsertCommand.Parameters.Add("p_SALARY_DOCUM_PERIOD_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_PERIOD_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Docum_Period.InsertCommand.Parameters["p_SALARY_DOCUM_PERIOD_ID"].DbType = DbType.Decimal;
            odaSalary_Docum_Period.InsertCommand.Parameters.Add("p_BEGIN_PERIOD", OracleDbType.Date, 0, "BEGIN_PERIOD").Direction = ParameterDirection.Input;
            odaSalary_Docum_Period.InsertCommand.Parameters.Add("p_END_PERIOD", OracleDbType.Date, 0, "END_PERIOD").Direction = ParameterDirection.Input;
            odaSalary_Docum_Period.InsertCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, null, ParameterDirection.Input); 
            
            odaSalary_Docum_Period.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_DOCUM_PERIOD_UPDATE(:p_SALARY_DOCUM_PERIOD_ID,:p_BEGIN_PERIOD,:p_END_PERIOD,:p_SALARY_DOCUM_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum_Period.UpdateCommand.BindByName = true;
            odaSalary_Docum_Period.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSalary_Docum_Period.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_PERIOD_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_PERIOD_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Docum_Period.UpdateCommand.Parameters["p_SALARY_DOCUM_PERIOD_ID"].DbType = DbType.Decimal;
            odaSalary_Docum_Period.UpdateCommand.Parameters.Add("p_BEGIN_PERIOD", OracleDbType.Date, 0, "BEGIN_PERIOD").Direction = ParameterDirection.Input;
            odaSalary_Docum_Period.UpdateCommand.Parameters.Add("p_END_PERIOD", OracleDbType.Date, 0, "END_PERIOD").Direction = ParameterDirection.Input;
            odaSalary_Docum_Period.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, ParameterDirection.Input); 
            
            odaSalary_Docum_Period.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_DOCUM_PERIOD_DELETE(:p_SALARY_DOCUM_PERIOD_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum_Period.DeleteCommand.BindByName = true;
            odaSalary_Docum_Period.DeleteCommand.Parameters.Add("p_SALARY_DOCUM_PERIOD_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_PERIOD_ID").Direction = ParameterDirection.InputOutput;

            odaSalary_Docum_Period.AcceptChangesDuringUpdate = false;

            odaSalary_Docum_Pay_Change = new OracleDataAdapter("", Connect.CurConnect);
            odaSalary_Docum_Pay_Change.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_DOCUM_PAY_CHANGE_UPDATE(:p_SALARY_DOCUM_PAY_CHANGE_ID,:p_COUNT_DAYS,:p_PAY_VALUE,:p_BY_CODE_DOC_SIGN,:p_SALARY_DOCUM_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum_Pay_Change.InsertCommand.BindByName = true;
            odaSalary_Docum_Pay_Change.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSalary_Docum_Pay_Change.InsertCommand.Parameters.Add("p_SALARY_DOCUM_PAY_CHANGE_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_PAY_CHANGE_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Docum_Pay_Change.InsertCommand.Parameters["p_SALARY_DOCUM_PAY_CHANGE_ID"].DbType = DbType.Decimal;
            odaSalary_Docum_Pay_Change.InsertCommand.Parameters.Add("p_COUNT_DAYS", OracleDbType.Decimal, 0, "COUNT_DAYS").Direction = ParameterDirection.Input;
            odaSalary_Docum_Pay_Change.InsertCommand.Parameters.Add("p_PAY_VALUE", OracleDbType.Decimal, 0, "PAY_VALUE").Direction = ParameterDirection.Input;
            odaSalary_Docum_Pay_Change.InsertCommand.Parameters.Add("p_BY_CODE_DOC_SIGN", OracleDbType.Decimal, 0, "BY_CODE_DOC_SIGN").Direction = ParameterDirection.Input;
            odaSalary_Docum_Pay_Change.InsertCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, null,ParameterDirection.Input); 
            
            odaSalary_Docum_Pay_Change.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_DOCUM_PAY_CHANGE_UPDATE(:p_SALARY_DOCUM_PAY_CHANGE_ID,:p_COUNT_DAYS,:p_PAY_VALUE,:p_BY_CODE_DOC_SIGN,:p_SALARY_DOCUM_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum_Pay_Change.UpdateCommand.BindByName = true;
            odaSalary_Docum_Pay_Change.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSalary_Docum_Pay_Change.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_PAY_CHANGE_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_PAY_CHANGE_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Docum_Pay_Change.UpdateCommand.Parameters["p_SALARY_DOCUM_PAY_CHANGE_ID"].DbType = DbType.Decimal;
            odaSalary_Docum_Pay_Change.UpdateCommand.Parameters.Add("p_COUNT_DAYS", OracleDbType.Decimal, 0, "COUNT_DAYS").Direction = ParameterDirection.Input;
            odaSalary_Docum_Pay_Change.UpdateCommand.Parameters.Add("p_PAY_VALUE", OracleDbType.Decimal, 0, "PAY_VALUE").Direction = ParameterDirection.Input;
            odaSalary_Docum_Pay_Change.UpdateCommand.Parameters.Add("p_BY_CODE_DOC_SIGN", OracleDbType.Decimal, 0, "BY_CODE_DOC_SIGN").Direction = ParameterDirection.Input;
            odaSalary_Docum_Pay_Change.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, null, ParameterDirection.Input); 
            
            odaSalary_Docum_Pay_Change.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_DOCUM_PAY_CHANGE_DELETE(:p_SALARY_DOCUM_PAY_CHANGE_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Docum_Pay_Change.DeleteCommand.BindByName = true;
            odaSalary_Docum_Pay_Change.DeleteCommand.Parameters.Add("p_SALARY_DOCUM_PAY_CHANGE_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_PAY_CHANGE_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Docum_Pay_Change.AcceptChangesDuringUpdate = false;

            cmdCalc_Emp_Percent_Standing = new OracleCommand(Queries.GetQueryWithSchema("SelectSickCalcPercent.sql"), Connect.CurConnect);
            cmdCalc_Emp_Percent_Standing.BindByName = true;
            cmdCalc_Emp_Percent_Standing.Parameters.Add("p_per_num", OracleDbType.Varchar2, null, ParameterDirection.Input);
            cmdCalc_Emp_Percent_Standing.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);

#endregion 
            odaSalary_Docum.Fill(ds);
            if (ds.Tables["SALARY_DOCUM"].Rows.Count == 0)
            {
                DataRow r = ds.Tables["SALARY_DOCUM"].NewRow();
                r["DOC_SUBDIV_ID"] = subdiv_id==0?ds.Tables["EMP"].Rows[0]["SUBDIV_ID"]:subdiv_id;
                r["TRANSFER_ID"] = transfer_id;
                ds.Tables["SALARY_DOCUM"].Rows.Add(r);
                ds.Tables["SALARY_DOCUM"].AcceptChanges();
            }
            this.DataRow = ds.Tables["SALARY_DOCUM"].Rows[0];
            FillAllRelatedDocum();
            ds_rel.Tables.Add(ds.Tables["TYPE_SAL_DOCUM"].Copy());
            bw_loadPayment = new BackgroundWorker();
            bw_loadPayment.DoWork += new DoWorkEventHandler(bw_loadPayment_DoWork);
            bw_loadPayment.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_loadPayment_RunWorkerCompleted);
            bw_loadPayment.WorkerSupportsCancellation = true;
            IsActive = true;
        }

        /// <summary>
        /// Заполняем все документы которые уже были
        /// </summary>
        private void FillAllRelatedDocum()
        {
            if (ds_rel.Tables.Contains("SALARY_DOCUM"))
                ds_rel.Tables["SALARY_DOCUM"].Rows.Clear();
            odaAllSalDocum.SelectCommand.Parameters["p_transfer_id"].Value = TransferID;
            odaAllSalDocum.SelectCommand.Parameters["p_salary_docum_id"].Value = SalaryDocumID;
            try
            {
                odaAllSalDocum.Fill(ds_rel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения зависимых документов");
            }
        }

        /// <summary>
        /// Сохранение данных по документу
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            bool _is_new = IsNew;
            try
            {
                odaSalary_Docum.Update(ds.Tables["SALARY_DOCUM"]);

                odaSalary_Docum_Detail.UpdateCommand.Parameters["p_salary_docum_id"].Value = this.SalaryDocumID;
                odaSalary_Docum_Detail.InsertCommand.Parameters["p_salary_docum_id"].Value = this.SalaryDocumID;
                odaSalary_Docum_Detail.Update(ds.Tables["SALARY_DOCUM_DETAIL"], "SALARY_DOCUM_DETAIL_ID");

                odaSalary_Docum_Period.UpdateCommand.Parameters["p_salary_docum_id"].Value = this.SalaryDocumID;
                odaSalary_Docum_Period.InsertCommand.Parameters["p_salary_docum_id"].Value = this.SalaryDocumID;
                odaSalary_Docum_Period.Update(ds.Tables["SALARY_DOCUM_PERIOD"], "SALARY_DOCUM_PERIOD_ID");

                odaSalary_Docum_Pay_Change.UpdateCommand.Parameters["p_salary_docum_id"].Value = this.SalaryDocumID;
                odaSalary_Docum_Pay_Change.InsertCommand.Parameters["p_salary_docum_id"].Value = this.SalaryDocumID;
                odaSalary_Docum_Pay_Change.Update(ds.Tables["SALARY_DOCUM_PAY_CHANGE"], "SALARY_DOCUM_PAY_CHANGE_ID");

                tr.Commit();
                ds.AcceptChanges();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                if (_is_new) this.SalaryDocumID = null; // сбрасываем айдишник документа, если вставка не прошла
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
                return false;
            }
        }
#region source of combobox
        /// <summary>
        /// Список типов документов
        /// </summary>
        public DataView TypeDocSource
        {
            get
            {
                return new DataView(ds.Tables["TYPE_SAL_DOCUM"], "", "TYPE_SAL_DOC_NAME", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Список доступных подразделений
        /// </summary>
        public DataView SubdivSource
        {
            get
            {
                return new DataView(AppDataSet.Tables["ACCESS_SUBDIV"], "APP_NAME='SALARY'", "CODE_SUBDIV", DataViewRowState.CurrentRows);
            }
        }
#endregion

        /// <summary>
        /// Данная модель является новой
        /// </summary>
        public bool IsNew
        {
            get
            {
                return this.SalaryDocumID == null;
            }
        }
        /// <summary>
        /// Есть ли изменения в модели
        /// </summary>
        public bool HasChanges
        {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }

        bool _calcAfterSave =true;
        /// <summary>
        /// Рассчитывать ли документ за текущий месяц после сохранения
        /// </summary>
        public bool CalcAfterSave
        {
            get
            {
                return _calcAfterSave;
            }
            set
            {
                _calcAfterSave = value;
                RaisePropertyChanged(() => CalcAfterSave);
            }
        }

#region Class Members


        [Column(Name = "SALARY_DOCUM_ID")]
        public Decimal? SalaryDocumID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryDocumID, value);
            }
        }

        /// <summary>
        /// Последнее время расчета
        /// </summary>
        [Column(Name = "LAST_CALC_DATE")]
        public DateTime? LastCalcDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => LastCalcDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => LastCalcDate, value);
            }
        }

        /*/// <summary>
        /// Процент оплаты
        /// </summary>
        [Column(Name = "PAYMENT_PERCENT")]
        public Decimal? PaymentPercent
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PaymentPercent);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentPercent, value);
            }
        }

        /// <summary>
        /// Сумма оплаты за единицу дня или месяца
        /// </summary>
        [Column(Name = "PAYMENT_SUM")]
        public Decimal? PaymentSum
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PaymentSum);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentSum, value);
            }
        }*/

        /// <summary>
        /// Дата окончания документа
        /// </summary>
        [Column(Name = "DOC_END", CanBeNull=false)]
        public DateTime? DocEnd
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DocEnd);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DocEnd, value);
            }
        }

        /// <summary>
        /// Дата начала документа
        /// </summary>
        [Column(Name = "DOC_BEGIN", CanBeNull=false)]
        public DateTime? DocBegin
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DocBegin);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DocBegin, value);
                FillDocumPeriod();
                FillDocumPayChange();
                RaisePropertyChanged(() => IsDocumDetailEnabled);
            }
        }

        /// <summary>
        /// Перевод сотрудника для документа
        /// </summary>
        [Column(Name = "TRANSFER_ID", CanBeNull=false)]
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

        /// <summary>
        /// Дата создания документа
        /// </summary>
        [Column(Name = "DATE_FORM_DOCUM")]
        public DateTime? DateFormDocum
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateFormDocum);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateFormDocum, value);
            }
        }

        /// <summary>
        /// подразделение документа
        /// </summary>
        [Column(Name = "DOC_SUBDIV_ID", CanBeNull=false)]
        public Decimal? DocSubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DocSubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DocSubdivID, value);
            }
        }


        /// <summary>
        /// Тип документа начисления
        /// </summary>
        [Column(Name = "TYPE_SAL_DOCUM_ID", CanBeNull=false)]
        public Decimal? TypeSalDocumID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeSalDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeSalDocumID, value);
                RaisePropertyChanged(() => IsDocumDetailEnabled);
                RaisePropertyChanged(() => MeasureName);
                FillDocumPeriod();
                FillDocumDetails(DocBegin);
                CountRestrictDays = TypeSalDocum.CountRestrictDays;
                FillDocumPayChange();
                RaisePropertyChanged(() => IsCountMeasureRestrEnabled);
                RaisePropertyChanged(() => ShowAdditionParameters);
            }
        }

        /// <summary>
        /// Дата проведения документа
        /// </summary>
        [Column(Name = "DATE_CLOSE")]
        public DateTime? DateClose
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateClose);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateClose, value);
            }
        }

        /// <summary>
        /// Дата документа
        /// </summary>
        [Column(Name = "DATE_DOC", CanBeNull=false)]
        public DateTime? DateDoc
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateDoc);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateDoc, value);
                RaisePropertyChanged(() => IsDocumDetailEnabled);
            }
        }

        /// <summary>
        /// наименование документа
        /// </summary>
        [Column(Name = "NAME_DOC")]
        public String NameDoc
        {
            get
            {
                return this.GetDataRowField<String>(() => NameDoc);
            }
            set
            {
                UpdateDataRow<String>(() => NameDoc, value);
            }
        }

        /// <summary>
        /// Код документа
        /// </summary>
        [Column(Name = "CODE_DOC")]
        public String CodeDoc
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeDoc);
            }
            set
            {
                UpdateDataRow<String>(() => CodeDoc, value);
            }
        }

        /// <summary>
        /// Ссылка на документ в табеле
        /// </summary>
        [Column(Name = "REG_DOC_ID")]
        public decimal? RegDocID
        {
            get
            {
                return this.GetDataRowField<decimal?>(() => RegDocID);
            }
            set
            {
                if (this.RegDocID != value)
                {
                    UpdateDataRow<decimal?>(() => RegDocID, value);
                    SetMainDocumValues();
                }
            }
        }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        [Column(Name="FIO", CanBeNull=false)]
        public string FIO
        {
            get
            {
                return ds.Tables["EMP"].Rows[0]["FIO"].ToString();
            }
            set
            {
                ds.Tables["EMP"].Rows[0]["FIO"] = value;
            }
        }

        /// <summary>
        /// табельный сотрудника
        /// </summary>
        [Column(Name = "PER_NUM", CanBeNull=false)]
        public string PerNum
        {
            get
            {
                return ds.Tables["EMP"].Rows[0]["PER_NUM"].ToString();
            }
            set
            {
                ds.Tables["EMP"].Rows[0]["PER_NUM"] = value;
            }
        }

        /// <summary>
        /// Тип документа по заработной плате
        /// </summary>
        public TypeSalDocum TypeSalDocum
        {
            get
            {
                if (ds!=null && ds.Tables.Contains("TYPE_SAL_DOCUM") && TypeSalDocumID!=null)
                    return ds.Tables["TYPE_SAL_DOCUM"].Rows.OfType<DataRow>().Where(r=>r.Field2<decimal?>("TYPE_SAL_DOCUM_ID")==TypeSalDocumID).Select(r=>new TypeSalDocum(){DataRow = r}).FirstOrDefault();
                else 
                    return null;
            }
        }

        /// <summary>
        /// Кол-во дней ограничивающих расчеты в видах оплат
        /// </summary>
        [Column(Name="COUNT_RESTRICT_DAYS")]
        public decimal? CountRestrictDays
        {
            get
            {
                return this.GetDataRowField<decimal?>(() => CountRestrictDays);
            }
            set
            {
                UpdateDataRow<decimal?>(() => CountRestrictDays, value);
            }
        }

        /// <summary>
        /// Признак первичного документа
        /// </summary>
        [Column(Name="BASIC_DOC_SIGN")]
        public decimal? BasicDocSign
        {
            get
            {
                return this.GetDataRowField<decimal?>(() => BasicDocSign);
            }
            set
            {
                UpdateDataRow<decimal?>(() => BasicDocSign, value);
            }
        }
        
        /// <summary>
        /// Ссылка на первичный документ
        /// </summary>
        [Column(Name = "RELATED_DOCUM_ID")]
        public decimal? RelatedDocumID
        {
            get
            {
                return this.GetDataRowField<decimal?>(() => RelatedDocumID);
            }
            set
            {
                UpdateDataRow<decimal?>(() => RelatedDocumID, value);
                if (value!=null)
                    SetDocValuesByRelated(value);
            }
        }

        /// <summary>
        /// Выбранный документ в табеле
        /// </summary>
        public RegDoc RegDoc
        {
            get
            {
                if (RegDocID == null) return null;
                else
                    return ds.Tables["REG_DOC"].Rows.OfType<DataRow>().Where(r => r.Field2<Decimal?>("REG_DOC_ID") == RegDocID)
                        .Select(r => new RegDoc() { DataRow = r }).FirstOrDefault();
            }
        }

        List<String> _listDocNameSource;
        /// <summary>
        /// Источник данных для имени документа.
        /// </summary>
        public List<string> DocNameSource
        {
            get
            {
                if (_listDocNameSource == null)
                {
                    XDocument doc = XDocument.Load(File.OpenRead(Connect.CurrentAppPath + "/XmlData/SalaryXmlData.xml"));
                    var types = doc.Descendants("PossibleDocName");
                    _listDocNameSource = types.Select(r => r.Attribute("Value").Value).ToList();
                }
                return _listDocNameSource;
            }
        }

        /// <summary>
        /// Показывать ли дополнительные параметры для типа документа
        /// </summary>
        public bool ShowAdditionParameters
        {
            get
            {
                return TypeSalDocumID == null || TypeSalDocum.VacSign == 0;
            }
        }

        bool _isActive = false;
        /// <summary>
        /// Устанавливает, активна ли сейчас модель. Используется для быстрого и безопасного заполнения данными
        /// </summary>
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                RaisePropertyChanged(() => IsActive);
            }
        }
#endregion

        /// <summary>
        /// ошибка для класса всего
        /// </summary>
        public string Error
        {
            get 
            {
                if (DateDoc == null) return "Требуется дата документа";
                if (TypeSalDocumID == null) return "Требуется указать тип документа";
                if (TypeSalDocum.NeedDocPeriod == 1 && (DocBegin == null || DocEnd == null)) return "Для данного типа документа требуется период";
                if (TransferID == null) return "Требуется указать сотрудника";
                if (DocSubdivID == null) return "Требуется указать подразделение документа";
                return string.Empty;
            }
        }

        /// <summary>
        /// Получает описание ошибки для свойства
        /// </summary>
        /// <param name="column_name"> имя свойства</param>
        /// <returns></returns>
        public new string this[string column_name]
        { 
            get
            {
                string st =base[column_name];
                if (!string.IsNullOrEmpty(st))
                    return st;
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Заведем массив константный, означающий какие типы оплат можно включать для редактирования в документ начилсления
        /// </summary>
        private readonly decimal[] includedTypePayments = {1,6};

        EntityRelationList<SalaryDocumDetailModel> _documDetailSource;
        /// <summary>
        /// Источник данных. Список видов оплат по документу
        /// </summary>
        public List<SalaryDocumDetailModel> DocumDetailSource
        {
            get
            {
                if (_documDetailSource == null)
                {
                    _documDetailSource = this.CreateRelationCollection<SalaryDocumDetailModel>(ds, "SALARY_DOCUM_ID");
                }
                return _documDetailSource.Where(r=>includedTypePayments.Contains(AppDictionaries.CodePaymentIDToValue[r.PaymentTypeID.Value].TypePaymentTypeID.Value)).ToList();
            }
        }

        EntityRelationList<SalaryDocumPeriodModel> _documPeriodSource;
        /// <summary>
        /// Источник данных для показа используемых периодов расчета по документу 
        /// </summary>
        public List<SalaryDocumPeriodModel> DocumPeriodSource
        {
            get
            {
                if (_documPeriodSource == null)
                {
                    _documPeriodSource = this.CreateRelationCollection<SalaryDocumPeriodModel>(ds, "SALARY_DOCUM_ID");
                    _documPeriodSource.ListChanged += new ListChangedEventHandler(_documPeriodSource_ListChanged);
                }
                return _documPeriodSource.ToList();
            }
        }
        /// <summary>
        /// Обработчик изменения года - требуется пересчитать данные по больничному
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _documPeriodSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                ValidateDates(_documPeriodSource.Select(r=>r.BeginPeriod).ToArray());
                FillDocumDetails(DocBegin);
            }
        }

        /// <summary>
        /// Проверка заполненности годов для расчета
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private bool ValidateDates(DateTime?[] values)
        {
            return values != null && values.Length > 0 && Array.TrueForAll(values, r => r.HasValue && r.Value.Year > 999 && r.Value.Year < 3000);
        }

        List<RegDocModel> _regDocSource;
        /// <summary>
        /// Источник данных для списка доступных документов  в табеле
        /// </summary>
        public List<RegDocModel> RegDocSource
        { 
            get
            {
                if (_regDocSource == null && ds != null && ds.Tables.Contains("REG_DOC"))
                    _regDocSource = new List<RegDocModel>(ds.Tables["REG_DOC"].Rows.OfType<DataRow>().Select(r => new RegDocModel(r)).OrderByDescending(r => r.DocBegin));
                return _regDocSource;
            }
        }

        List<SalaryDocum> _relatedDocSource;
        public List<SalaryDocum> RelatedDocSource
        {
            get
            {
                if (_relatedDocSource == null && ds_rel != null && ds_rel.Tables.Contains("SALARY_DOCUM"))
                    _relatedDocSource = new List<SalaryDocum>(ds_rel.Tables["SALARY_DOCUM"].Rows.OfType<DataRow>().Select(r => new SalaryDocum() { DataRow = r }).OrderByDescending(r => r.DocBegin));
                return _relatedDocSource;
            }
        }

        EntityRelationList<SalaryDocumPayChangeModel> _documPayChangeSource;
        /// <summary>
        /// Источник данных для изменения процента оплаты 
        /// </summary>
        public List<SalaryDocumPayChangeModel> DocumPayChangeSource
        {
            get
            {
                if (_documPayChangeSource == null)
                {
                    _documPayChangeSource = this.CreateRelationCollection<SalaryDocumPayChangeModel>(ds, "SALARY_DOCUM_ID");
                }
                return _documPayChangeSource.ToList();
            }
        }

        OracleCommand cmd_CalcPayment;
        /// <summary>
        /// Выполняет заполнение видов оплат по документу - размер оплаты
        /// </summary>
        /// <param name="calc_date">Дата для расчета видов оплат - размера начислений</param>
        private void FillDocumDetails(DateTime? calc_date)
        {
            if (!IsActive || RelatedDocumID != null) return;
            for (int i = _documDetailSource.Count - 1; i > -1; --i) //Удаляем все имеющиеся расчеты и заполняем заново.
            {
                _documDetailSource.RemoveAt(i);
            }
            if (TypeSalDocum != null && TypeSalDocum.VacSign == 1)
            {
                RaisePropertyChanged(() => DocumDetailSource);
                return;
            }
            var values = from r in ds.Tables["TYPE_DOCUM_PAY_CALC"].AsEnumerable() 
                         where r.Field2<decimal?>("TYPE_SAL_DOCUM_ID")== TypeSalDocumID
                         select r;

            foreach (DataRow row in values)//Добавляем сначала строчки для расчета
            {
                DataRow rr = ds.Tables["SALARY_DOCUM_DETAIL"].NewRow();
                rr["payment_type_id"] = row["payment_type_id"];
                _documDetailSource.Add(new SalaryDocumDetailModel(rr));
            }
            if (bw_loadPayment.IsBusy)
                bw_loadPayment.CancelAsync();
            while (bw_loadPayment.IsBusy)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                                          new Action(delegate { }));
            };
            bw_loadPayment.RunWorkerAsync(new object[]{DocBegin, TransferID, 
                DocumPeriodSource.Where(r=>r.BeginPeriod.HasValue).Select(r => r.BeginPeriod.Value).ToArray(), // массив параметры годов для расчета
                values.Select(r=>r.Field2<Decimal?>("PAYMENT_TYPE_ID")).ToArray()
                });// а потом запускаем асинхроннный расчет видов оплат
            RaisePropertyChanged(() => DocumDetailSource);
        }

        /// <summary>
        /// Асинхронная загрузка размеров выплат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bw_loadPayment_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] param = e.Argument as object[];
            cmd_CalcPayment.Parameters["p_date"].Value = param[0];
            cmd_CalcPayment.Parameters["p_transfer_id"].Value = param[1];
            cmd_CalcPayment.Parameters["p_dates_array"].Value = param[2];
            ConcurrentDictionary<decimal, decimal?> result = new ConcurrentDictionary<decimal, decimal?>();
            foreach (Decimal? row in param[3] as decimal?[])
            {
                if (bw_loadPayment.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                decimal? pay_value = null;
                cmd_CalcPayment.Parameters["p_payment_type_id"].Value = row;
                object val = cmd_CalcPayment.ExecuteScalar();
                if (val != DBNull.Value && val != null)
                    pay_value = (decimal)val;
                result.TryAdd(row.Value, pay_value);
            }
            e.Result = result;
        }

        /// <summary>
        /// После получения данных мы записываем значения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bw_loadPayment_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) return;
            if (e.Error != null)
                MessageBox.Show(e.Error.GetFormattedException(), "Ошибка расчета видов оплат");
            else
            {
                ConcurrentDictionary<decimal, decimal?> result = e.Result as ConcurrentDictionary<decimal, decimal?>;
                foreach (SalaryDocumDetailModel r in _documDetailSource)
                {
                    if (result.ContainsKey(r.PaymentTypeID.Value))
                        r.PaymentSum = result[r.PaymentTypeID.Value];
                }
            }
        }

        /// <summary>
        /// Заполняем период для расчета
        /// </summary>
        private void FillDocumPeriod()
        {
            if (!IsActive || RelatedDocumID!=null) return;
            if (_documPeriodSource == null)
                _documPeriodSource = new EntityRelationList<SalaryDocumPeriodModel>() { RelatedEntity = this };
            if (TypeSalDocumID == null || DocBegin == null || _documPeriodSource.Count > 0)
            {
                if (_documPeriodSource.Count>0)// если есть периоды для расчета, то считаем изменения для 
                    FillDocumDetails(DocBegin);
                return;
            }
            if (TypeSalDocum != null && TypeSalDocum.VacSign == 1)
            {
                RaisePropertyChanged(() => DocumPeriodSource);
                return;
            }
            if (TypeSalDocum.NeedCalcPeriod == 1)
                for (int i=0;i<2; ++i)
                {
                    DataRow rr = ds.Tables["SALARY_DOCUM_PERIOD"].NewRow();
                    ds.Tables["SALARY_DOCUM_PERIOD"].Rows.Add(rr);
                    SalaryDocumPeriodModel val = new SalaryDocumPeriodModel() { DataRow = rr };
                    val.SalaryDocumID = this.SalaryDocumID;
                    if (DocBegin.HasValue)
                    {
                        val.BeginPeriod = this.DocBegin.Value.Trunc("Year").AddYears(-(i+1));
                        val.EndPeriod = val.BeginPeriod.Value.AddYears(1).AddDays(-1);
                    }
                    else
                        val.BeginPeriod = val.EndPeriod = null;
                    _documPeriodSource.Add(val);
                }
            FillDocumDetails(DocBegin);
            RaisePropertyChanged(() => DocumPeriodSource);
        }

        /// <summary>
        /// Заполняем периоды изменения оплаты по дням
        /// </summary>
        private void FillDocumPayChange()
        {
            if (!IsActive || RelatedDocumID != null) return;
            _documPayChangeSource.Clear();
            if (TypeSalDocum != null && TypeSalDocum.VacSign == 1) // если это отпуск, никаких изменений не надо делать
            {
                RaisePropertyChanged(() => DocumPayChangeSource);
                return;
            }
            decimal? percent_calc = 100;
            if (TypeSalDocumID ==2 || TypeSalDocumID == 7 || TypeSalDocumID==8)
                percent_calc= GetStandingPercent(DocBegin);
            DataRow row = ds.Tables["SALARY_DOCUM_PAY_CHANGE"].NewRow();
            ds.Tables["SALARY_DOCUM_PAY_CHANGE"].Rows.Add(row);
            _documPayChangeSource.Add(new SalaryDocumPayChangeModel(row){PayValue = percent_calc, ByCodeDocSign=1, CountDays=1});
            if (TypeSalDocumID == 8)
            {
                row = ds.Tables["SALARY_DOCUM_PAY_CHANGE"].NewRow();
                ds.Tables["SALARY_DOCUM_PAY_CHANGE"].Rows.Add(row);
                _documPayChangeSource.Add(new SalaryDocumPayChangeModel(row) { PayValue = 50, CountDays = 11, ByCodeDocSign = 1 });
            }
            RaisePropertyChanged(() => DocumPayChangeSource);
        }

        /// <summary>
        /// Функция возращает процент, который положен сотруднику по оплате больничного
        /// </summary>
        /// <param name="calc_date">Дата расчета для стажа</param>
        /// <returns>размер процентов, число от 0 до 100%</returns>
        private decimal? GetStandingPercent(DateTime? calc_date)
        {
            try
            {
                cmdCalc_Emp_Percent_Standing.Parameters["p_date"].Value = calc_date;
                cmdCalc_Emp_Percent_Standing.Parameters["p_per_num"].Value = PerNum;
                object val = cmdCalc_Emp_Percent_Standing.ExecuteScalar();
                if (val == null || val == DBNull.Value)
                    return null;
                else
                    return (decimal)val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Доступность редактирования видов оплат
        /// </summary>
        public bool IsDocumDetailEnabled
        {
            get
            {
                return TypeSalDocumID != null && DocBegin != null && TypeSalDocum.VacSign==0;
            }
        }

        /// <summary>
        /// Заполнение данных по выбранному документу из табеля
        /// </summary>
        private void SetMainDocumValues()
        {
            if (RegDoc != null)
            {
                switch (Convert.ToInt32(RegDoc.DocListID))
                {
                    case 5: this.TypeSalDocumID = 6; break;
                    case 7: this.TypeSalDocumID = 2; break;
                }
                this.DocBegin = RegDoc.DocBegin;
                this.DocEnd = RegDoc.DocEnd;
                this.CodeDoc = RegDoc.DocNumber;
                this.DateDoc = DateTime.Today;
                this.BasicDocSign = 1;
            }
        }

        private void SetDocValuesByRelated(decimal? _salaryDocumId)
        {
            IsActive = false;
            BasicDocSign = 0m;
            try
            {
                OracleDataAdapter oda = new OracleDataAdapter((OracleCommand)odaSalary_Docum.SelectCommand.Clone());
                foreach (DataTableMapping r in odaSalary_Docum.TableMappings)
                    oda.TableMappings.Add(r.SourceTable, r.DataSetTable);
                oda.SelectCommand.Parameters["p_salary_docum_id"].Value = _salaryDocumId;
                DataSet ds1 = new DataSet();
                oda.Fill(ds1);
                TypeSalDocumID = ds1.Tables["SALARY_DOCUM"].Rows[0].Field<Decimal?>("TYPE_SAL_DOCUM_ID");
                NameDoc = ds1.Tables["SALARY_DOCUM"].Rows[0].Field<string>("NAME_DOC");

                _documDetailSource.Clear();// заполняем все данные по документку такие же как были в старом документе - это данные оплаты
                foreach (DataRow row in ds1.Tables["SALARY_DOCUM_DETAIL"].Rows)
                    _documDetailSource.Add(new SalaryDocumDetailModel(ds.Tables["SALARY_DOCUM_DETAIL"].NewRow()) 
                    { 
                        PaymentTypeID = row.Field2<Decimal?>("PAYMENT_TYPE_ID"), 
                        PaymentSum = row.Field2<Decimal?>("PAYMENT_SUM") 
                    });

                _documPeriodSource.Clear();
                foreach (DataRow row in ds1.Tables["SALARY_DOCUM_PERIOD"].Rows)
                    _documPeriodSource.Add(new SalaryDocumPeriodModel(ds.Tables["SALARY_DOCUM_PERIOD"].NewRow()) 
                        { 
                            BeginPeriod = row.Field2<DateTime?>("BEGIN_PERIOD"), 
                            EndPeriod = row.Field2<DateTime?>("END_PERIOD") 
                        });

                _documPayChangeSource.Clear();
                foreach (DataRow row in ds1.Tables["SALARY_DOCUM_PAY_CHANGE"].Rows)
                    _documPayChangeSource.Add(new SalaryDocumPayChangeModel(ds.Tables["SALARY_DOCUM_PAY_CHANGE"].NewRow()) 
                        {  ByCodeDocSign = row.Field2<Decimal?>("BY_CODE_DOC_SIGN"),  
                            CountDays = row.Field2<Decimal?>("COUNT_DAYS"),
                            PayValue = row.Field2<Decimal?>("PAY_VALUE")
                        });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения/заполнения зависимых данных");
            }
            RaisePropertyChanged(() => DocumDetailSource);
            RaisePropertyChanged(() => DocumPeriodSource);
            RaisePropertyChanged(() => DocumPayChangeSource);
            IsActive = true;
        }

        /// <summary>
        /// Доступность считать первые дни за счет предприятия
        /// </summary>
        public bool IsCountMeasureRestrEnabled
        {
            get
            {
                return TypeSalDocum != null && TypeSalDocum.CountRestrictDays != 0;
            }
        }
        /// <summary>
        /// Имя измерения - в чем измеряются виды оплаты, определяется как последний в документе вид оплат.
        /// </summary>
        public string MeasureName
        {
            get
            {
                if (TypeSalDocum == null)
                    return "день";
                else
                    switch (Convert.ToInt32(TypeSalDocum.DataRow.Field<decimal?>("TYPE_CALC_PERIOD_ID")))
                    {
                        case 1: return "час"; break;
                        case 2: return "день"; break;
                        case 3: return "месяц"; break;
                        default: return "день"; break;
                    }
            }
        }
    }

    /// <summary>
    /// Класс расширяет стандартный класс на код вида оплат и т.п.
    /// </summary>
    [Table(Name="SALARY_DOCUM_DETAIL")]
    public class SalaryDocumDetailModel : SalaryDocumDetail
    {
        public SalaryDocumDetailModel()
        {
        }

        public SalaryDocumDetailModel(DataRow r)
        {
            base.DataRow = r;
        }

        /// <summary>
        /// Код связанного вида оплат
        /// </summary>
        public string CodePayment
        {
            get
            {
                return AppDataSet.Tables["PAYMENT_TYPE"].Rows.OfType<DataRow>().Where(r => r.Field2<Decimal?>("PAYMENT_TYPE_ID") == this.PaymentTypeID).Select(r=>r.Field2<string>("CODE_PAYMENT")).FirstOrDefault();
            }
        }

        /// <summary>
        /// Наименование связанного вида оплат
        /// </summary>
        public string NamePayment
        {
            get
            {
                return AppDataSet.Tables["PAYMENT_TYPE"].Rows.OfType<DataRow>().Where(r => r.Field2<Decimal?>("PAYMENT_TYPE_ID") == this.PaymentTypeID).Select(r=>r.Field2<string>("NAME_PAYMENT")).FirstOrDefault();
            }
        }
    }


    [Table(Name = "SALARY_DOCUM_PERIOD")]
    public class SalaryDocumPeriodModel : SalaryDocumPeriod
    {
        public SalaryDocumPeriodModel(DataRow r):base()
        {
            base.DataRow = r;
        }
        public SalaryDocumPeriodModel():base()
        { 
        }
    }



    [Table(Name="REG_DOC")]
    public class RegDocModel: RegDoc
    {
        /// <summary>
        /// Конструктор с использованием строки данных
        /// </summary>
        /// <param name="r"></param>
        public RegDocModel(DataRow r):base()
        {
            DataRow = r;
        }
        /// <summary>
        /// Тип документа для документа табеля
        /// </summary>
        public DocList DocList
        {
            get
            {
                if (this.DocListID==null)
                    return null;
                else
                                    return this.DataSet.Tables["DOC_LIST"].Rows.OfType<DataRow>().Where(r=>r.Field2<Decimal?>("DOC_LIST_ID")==DocListID).Select(r=>new DocList(){DataRow=r}).FirstOrDefault();
            }
        }

        /// <summary>
        /// Признак документа - обработан ли он уже в заработной плате
        /// </summary>
        [Column(Name="DOC_SIGN")]
        public string DocSign
        {
            get
            {
                return this.GetDataRowField<string>(() => DocSign);
            }
        }
    }

    /// <summary>
    /// Изменения размера оплаты начиная с конкретного дня
    /// </summary>
    [Table(Name="SALARY_DOCUM_PAY_CHANGE")]
    public class SalaryDocumPayChangeModel : SalaryDocumPayChange
    {
        /// <summary>
        /// Конструктор для изменения оплаты
        /// </summary>
        /// <param name="r"></param>
        public SalaryDocumPayChangeModel(DataRow r):base()
        {
            DataRow = r;
        }

        public SalaryDocumPayChangeModel()
        { }
    }
}
