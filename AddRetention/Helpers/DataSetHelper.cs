using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Linq.Expressions;

namespace Salary.Helpers
{
    public static class DataSetHelper
    {
        public static void SetPrimaryColumn(this DataTable t, string columnName)
        {
            t.PrimaryKey = new DataColumn[]{t.Columns[columnName]};
            t.Columns[columnName].AutoIncrement = true;
            t.Columns[columnName].AutoIncrementSeed = -2;
            t.Columns[columnName].AutoIncrementStep = -1;
        }

        /// <summary>
        /// Таблица сводная из данных
        /// </summary>
        /// <typeparam name="T">источник данных с типом</typeparam>
        /// <typeparam name="TColumn">Колонка для создания своднных данных</typeparam>
        /// <typeparam name="TRow">Колонки для показа данных (не развертываются)</typeparam>
        /// <typeparam name="TData">Обработчик результата данных</typeparam>
        /// <param name="source"></param>
        /// <param name="columnSelector"></param>
        /// <param name="rowSelector"></param>
        /// <param name="dataSelector"></param>
        /// <returns></returns>
        public static DataTable ToPivotTable<T, TColumn, TRow, TData>(this IEnumerable<T> source, Func<T, TColumn> columnSelector,
                        Expression<Func<T, TRow>> rowSelector, Func<IEnumerable<T>, TData> dataSelector, string rowName)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn(rowName));
            var columns = source.Select(columnSelector).Distinct().OrderBy(r=>r);
 
            foreach (var column in columns)
                table.Columns.Add(new DataColumn(column.ToString()));
 
            var rows = source.GroupBy(rowSelector.Compile())
                             .Select(rowGroup => new
                             {
                                 Key = rowGroup.Key,
                                 Values = columns.GroupJoin(
                                     rowGroup,
                                     c => c,
                                     r => columnSelector(r),
                                     (c, columnGroup) => dataSelector(columnGroup))
                             });
 
            foreach (var row in rows)
            {
                var dataRow = table.NewRow();
                var items = row.Values.Cast<object>().ToList();
                items.Insert(0, row.Key);
                dataRow.ItemArray = items.ToArray();
                table.Rows.Add(dataRow);
            }
 
            return table;
        }
    }
}
