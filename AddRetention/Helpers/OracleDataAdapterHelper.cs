using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Salary.Interfaces;
using Oracle.DataAccess.Client;
using System.Data;
using LibrarySalary.Helpers;
using System.Data.Common;

namespace Salary.Helpers
{
    public static class OracleAdapterHelper
    {
        /// <summary>
        /// Возвращает адаптер, построенный на аргументах - параметрах
        /// </summary>
        /// <param name="queryName"></param>
        /// <param name="filter"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static OracleDataAdapter GetDefaultAdapter(string queryName, ICustomFilter filter, params FilterParameter[] args)
        {
            return GetDefaultAdapter(false, queryName, filter, args);
        }

        /// <summary>
        /// Возвращает адаптер, построенный на аргументах - параметрах
        /// </summary>
        /// <param name="IsQueryString">Параметр - входная строка уже запрос готовый (True), или же надо получать из файла </param>
        /// <param name="queryName"></param>
        /// <param name="filter"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static OracleDataAdapter GetDefaultAdapter(bool IsQueryString, string query, ICustomFilter filter, params FilterParameter[] args)
        {
            OracleDataAdapter a = new OracleDataAdapter(IsQueryString? query: Queries.GetQueryWithSchema(query), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            foreach (FilterParameter e in args)
            {
                switch (e)
                {
                    case FilterParameter.c: a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output); break;
                    case FilterParameter.p_date: a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, filter.GetDate(), ParameterDirection.Input); break;
                    case FilterParameter.p_date_begin: a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, filter.GetDateBegin(), ParameterDirection.Input); break;
                    case FilterParameter.p_date_end: a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, filter.GetDateEnd(), ParameterDirection.Input); break;
                    case FilterParameter.p_subdiv_id: a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, filter.GetSubdivID(), ParameterDirection.Input); break;
                    case FilterParameter.p_degree_ids: a.SelectCommand.Parameters.Add("p_degree_ids", OracleDbType.Array, filter.GetDegreeIDs(), ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE"; break;
                }
            }
            return a;
        }

        /// <summary>
        /// Обновление таблицы, со сбросом айдишника в Null для вставленных строк
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="table">Таблица обновляемая</param>
        /// <param name="IDsField">Поле наименование айдишника</param>
        /// <param name="ResetIDsForInsertRows"></param>
        /// <returns></returns>
        public static int Update(this OracleDataAdapter sender, DataTable table, string IDsField)
        {
            foreach (DataRow r in table.Rows)
            if (r.RowState== DataRowState.Added)
            {
                r[IDsField] = DBNull.Value;
            }
            return sender.Update(table);
        }

        /// <summary>
        /// Попытка обновить данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ds">DataSet для обновления данных</param>
        /// <param name="classWithParameters">Класс содержащий информацию о параметрамх ([OracleParameterMapping] attribute)</param>
        /// <returns></returns>
        public static Exception TryFillWithClear(this OracleDataAdapter sender, DataSet ds, object classWithParameters)
        {
            if (ds==null) ds = new DataSet();
            foreach (DataTableMapping p in sender.TableMappings)
            {
                if (ds.Tables.Contains(p.DataSetTable))
                {
                    ds.Tables[p.DataSetTable].Rows.Clear();
                }
            }
            sender.SelectCommand.SetParameters(classWithParameters);
            try
            {
                sender.Fill(ds);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }

    public enum FilterParameter
    {
        /// <summary>
        /// Выбранная дата
        /// </summary>
        p_date = 1, // простая дата от фильтра
        /// <summary>
        /// Выбранное подразделение
        /// </summary>
        p_subdiv_id = 2, //подразделение от фильтра
        /// <summary>
        /// Дата начала периода
        /// </summary>
        p_date_begin = 3, // дата начала фильтра
        /// <summary>
        /// Дата окончания периода
        /// </summary>
        p_date_end = 4, // дата окончания фильтра
        /// <summary>
        /// Тип RefCursor переменная
        /// </summary>
        c = 5, // RefCursor тип для возвратаб
        /// <summary>
        /// Список выбранных категорий, параметр будет с таким же названием
        /// </summary>
        p_degree_ids = 6
    }
}
