using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using System.ComponentModel;
using LibrarySalary.Helpers;
using System.Diagnostics;

namespace Salary
{
    public class AppDataSet
    {
        static DataSet ds;
        static Dictionary<string, OracleDataAdapter> dic_set = new Dictionary<string, OracleDataAdapter>(StringComparer.CurrentCultureIgnoreCase);
        static Dictionary<string, OracleDataAdapter> dic_set_lazy = new Dictionary<string, OracleDataAdapter>(StringComparer.CurrentCultureIgnoreCase);
        static AppDataSet()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                ds = new DataSet();
                dic_set.Add("ORDER", new OracleDataAdapter(string.Format(@"select order_id, order_name from {0}.orders order by order_name", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                dic_set.Add("DEGREE", new OracleDataAdapter(string.Format(@"select * from {0}.DEGREE order by code_degree", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                dic_set.Add("SUBDIV", new OracleDataAdapter(string.Format(@"select DECODE(subdiv_id, 0, '000', CODE_SUBDIV) code_subdiv, DECODE(SUBDIV_ID,0, 'У-УАЗ', SUBDIV_NAME) subdiv_name, subdiv_id, sub_actual_sign, 
                                                                            WORK_TYPE_ID, SERVICE_ID, SUB_DATE_START, SUB_DATE_END, PARENT_ID, FROM_SUBDIV, TYPE_SUBDIV_ID, GR_WORK_ID
                                                                                from {0}.SUBDIV 
                                                                            where (parent_id=0 or subdiv_id=0) order by code_subdiv, sub_actual_sign desc", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                dic_set.Add("ACCESS_SUBDIV", new OracleDataAdapter(string.Format(@"select code_subdiv, subdiv_name, subdiv_id, app_name, sub_actual_sign from {0}.SUBDIV_ROLES_ALL where sub_level<3 and subdiv_id!=201 order by code_subdiv, sub_actual_sign desc", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                dic_set.Add("PAYMENT_TYPE", new OracleDataAdapter(string.Format(@"select PAYMENT_TYPE_ID, CODE_PAYMENT, NAME_PAYMENT, TYPE_PAYMENT_TYPE_ID, DEF_ORDER_ID, IS_ALLOW_PAST_DAYS_EDIT, IS_ALLOW_PAST_HOUR_EDIT, CONSIDER_TYPE_ID, RELAT_TYPE_REF_ID  from {1}.PAYMENT_TYPE 
                                                            left join (select payment_type_id, def_order_id, IS_ALLOW_PAST_DAYS_EDIT, IS_ALLOW_PAST_HOUR_EDIT, RELAT_TYPE_REF_ID from {1}.payment_calc_relation where sysdate between DATE_START_CALC and DATE_END_CALC) using (payment_type_id)", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                dic_set.Add("TYPE_PAYMENT_TYPE", new OracleDataAdapter(string.Format(@"select * from {1}.TYPE_PAYMENT_TYPE", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                if (GrantedRoles.CheckRole("SALARY_EDIT"))
                    dic_set.Add("TYPE_ROW_SALARY", new OracleDataAdapter(string.Format(@"select * from {1}.TYPE_ROW_SALARY", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
               // if (GrantedRoles.CheckRole("SALARY_VIEW") || GrantedRoles.CheckRole("SALARY_CARTULARY_VIEW") || GrantedRoles.CheckRole("LOAN_VIEW"))
                {
                    dic_set.Add("TYPE_CARTULARY", new OracleDataAdapter(string.Format("select * from {1}.TYPE_CARTULARY", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                    dic_set.Add("TYPE_REF_SALARY", new OracleDataAdapter(string.Format(@"select * from {1}.TYPE_REF_SALARY", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                }
                dic_set.Add("SUBDIV_FOR_CLOSE", new OracleDataAdapter(string.Format("select subdiv_for_close_id, subdiv_id, LAST_DATE_PROCESSING, date_closing, APP_NAME  from {1}.subdiv_for_close", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                dic_set.Add("TARIFF_GRID", new OracleDataAdapter(string.Format("select * from {0}.TARIFF_GRID", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                dic_set.Add("PROPERTY_TYPE", new OracleDataAdapter(string.Format("select * from {1}.PROPERTY_TYPE", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                dic_set.Add("PAYMENT_PROPERTY", new OracleDataAdapter(string.Format("select * from {1}.PAYMENT_PROPERTY", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                dic_set.Add("PROP_POSSIBLE_VALUE", new OracleDataAdapter(string.Format("select * from {1}.PROP_POSSIBLE_VALUE", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));
                UpdateAll();

                dic_set_lazy.Add("TYPE_SAL_DOCUM", new OracleDataAdapter(string.Format("select * from {1}.TYPE_SAL_DOCUM", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect));

                /*EntityConnection conn = new EntityConnection(Library.GetEFConnectionMetadata());
                conn.Open();
                using (SalaryEntities context = new SalaryEntities(conn))
                {
                    var cur_payment = context.ExecuteStoreQuery<PAYMENT_TYPE>(,
                    var res = from c in context.PAYMENT_TYPE
                              where c.CODE_PAYMENT == "287"
                              select c;
                    foreach (var t in res)
                    {
                        Console.WriteLine(t.CODE_PAYMENT);
                    }
                }*/
            }
        }

        public AppDataSet()
        {
        }

        public DataTable this[string TableName]
        {
            get
            {
                return ds.Tables[TableName];
            }
        }

        /// <summary>
        /// Таблица категорий
        /// </summary>
        public DataTable DEGREE
        {
            get
            {
                return ds.Tables["DEGREE"];
            }
        }

        /// <summary>
        /// Тип документов таблица
        /// </summary>
        public static DataTable TypeSalDocum
        {
            get
            {
                if (!ds.Tables.Contains("TYPE_SAL_DOCUM"))
                    dic_set_lazy["TYPE_SAL_DOCUM"].Fill(ds, "TYPE_SAL_DOCUM");
                return ds.Tables["TYPE_SAL_DOCUM"];
            }
        }

        public static DataView GetSubdivView(string Sort = null)
        {
            return new DataView(ds.Tables["ACCESS_SUBDIV"], "", Sort, DataViewRowState.CurrentRows);
        }

        /// <summary>
        /// Загрузка данных по конкретной таблице данных
        /// </summary>
        /// <param name="TableName"></param>
        public static void UpdateSet(string TableName)
        {
            if (dic_set.ContainsKey(TableName))
            {
                if (ds.Tables.Contains(TableName))
                    ds.Tables[TableName].Clear();
                try
                {
                    dic_set[TableName].Fill(ds, TableName);
                    if (ds.Tables[TableName].PrimaryKey.Length == 0 && ds.Tables[TableName].Columns.Contains(TableName + "_ID"))
                    ds.Tables[TableName].PrimaryKey = new DataColumn[] { ds.Tables[TableName].Columns[TableName + "_ID"] };
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(string.Format("Не могу загрузить таблицу {0}. {1}", TableName, ex.Message));
                }
                
            }
        }

        public static DataTableCollection Tables
        {
            get
            {
                return ds.Tables;
            }
        }

        /// <summary>
        /// Обновить все справочники
        /// </summary>
        public static void UpdateAll()
        {
            foreach (string s in dic_set.Keys)
            {
                UpdateSet(s);
            }
        }

    }
}
