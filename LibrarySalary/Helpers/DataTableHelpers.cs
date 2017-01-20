using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace Salary
{
    public class ObjectShredder<T>
    {
        private System.Reflection.FieldInfo[] _fi;
        private System.Reflection.PropertyInfo[] _pi;
        private System.Collections.Generic.Dictionary<string, int> _ordinalMap;
        private System.Type _type;

        // ObjectShredder constructor.
        public ObjectShredder()
        {
            _type = typeof(T);
            _fi = _type.GetFields();
            _pi = _type.GetProperties();
            _ordinalMap = new Dictionary<string, int>();
        }

        /// <summary>
        /// Loads a DataTable from a sequence of objects.
        /// </summary>
        /// <param name="source">The sequence of objects to load into the DataTable.</param>
        /// <param name="table">The input table. The schema of the table must match that 
        /// the type T.  If the table is null, a new table is created with a schema 
        /// created from the public properties and fields of the type T.</param>
        /// <param name="options">Specifies how values from the source sequence will be applied to 
        /// existing rows in the table.</param>
        /// <returns>A DataTable created from the source sequence.</returns>
        public DataTable Shred(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            // Load the table from the scalar sequence if T is a primitive type.
            if (typeof(T).IsPrimitive)
            {
                return ShredPrimitive(source, table, options);
            }

            // Create a new table if the input table is null.
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            // Initialize the ordinal map and extend the table schema based on type T.
            table = ExtendTable(table, typeof(T));

            // Enumerate the source sequence and load the object values into rows.
            table.BeginLoadData();
            using (IEnumerator<T> e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (options != null)
                    {
                        table.LoadDataRow(ShredObject(table, e.Current), (LoadOption)options);
                    }
                    else
                    {
                        table.LoadDataRow(ShredObject(table, e.Current), true);
                    }
                }
            }
            table.EndLoadData();

            // Return the table.
            return table;
        }

        public DataTable ShredPrimitive(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            // Create a new table if the input table is null.
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            if (!table.Columns.Contains("Value"))
            {
                table.Columns.Add("Value", GetUnderTypeNotNullable(typeof(T)));
            }

            // Enumerate the source sequence and load the scalar values into rows.
            table.BeginLoadData();
            using (IEnumerator<T> e = source.GetEnumerator())
            {
                Object[] values = new object[table.Columns.Count];
                while (e.MoveNext())
                {
                    values[table.Columns["Value"].Ordinal] = e.Current;

                    if (options != null)
                    {
                        table.LoadDataRow(values, (LoadOption)options);
                    }
                    else
                    {
                        table.LoadDataRow(values, true);
                    }
                }
            }
            table.EndLoadData();

            // Return the table.
            return table;
        }

        public object[] ShredObject(DataTable table, T instance)
        {

            FieldInfo[] fi = _fi;
            PropertyInfo[] pi = _pi;

            if (instance.GetType() != typeof(T))
            {
                // If the instance is derived from T, extend the table schema
                // and get the properties and fields.
                ExtendTable(table, instance.GetType());
                fi = instance.GetType().GetFields();
                pi = instance.GetType().GetProperties();
            }

            // Add the property and field values of the instance to an array.
            Object[] values = new object[table.Columns.Count];
            foreach (FieldInfo f in fi)
            {
                values[_ordinalMap[f.Name]] = f.GetValue(instance);
            }

            foreach (PropertyInfo p in pi)
            {
                values[_ordinalMap[p.Name]] = p.GetValue(instance, null);
            }

            // Return the property and field values of the instance.
            return values;
        }

        private Type GetUnderTypeNotNullable(Type t)
        {
            if (t == typeof(string)) return t;
            if (!t.IsValueType) return null;
            if (Nullable.GetUnderlyingType(t) != null) return Nullable.GetUnderlyingType(t);
            else return t;
        }

        public DataTable ExtendTable(DataTable table, Type type)
        {
            // Extend the table schema if the input table was null or if the value 
            // in the sequence is derived from type T.            
            foreach (FieldInfo f in type.GetFields())
            {
                if (!_ordinalMap.ContainsKey(f.Name))
                {
                    // Add the field as a column in the table if it doesn't exist
                    // already.
                    DataColumn dc = table.Columns.Contains(f.Name) ? table.Columns[f.Name]
                        : table.Columns.Add(f.Name, GetUnderTypeNotNullable(f.FieldType));

                    // Add the field to the ordinal map.
                    _ordinalMap.Add(f.Name, dc.Ordinal);
                }
            }
            foreach (PropertyInfo p in type.GetProperties())
            {
                if (!_ordinalMap.ContainsKey(p.Name))
                {
                    // Add the property as a column in the table if it doesn't exist
                    // already.
                    DataColumn dc = table.Columns.Contains(p.Name) ? table.Columns[p.Name]
                        : table.Columns.Add(p.Name, GetUnderTypeNotNullable(p.PropertyType));

                    // Add the property to the ordinal map.
                    _ordinalMap.Add(p.Name, dc.Ordinal);
                }
            }

            // Return the table.
            return table;
        }
    }

    public static class CustomLINQtoDataSetMethods
    {
        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source)
        {
            return new ObjectShredder<T>().Shred(source, null, null);
        }

        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source,
                                                    DataTable table, LoadOption? options)
        {
            return new ObjectShredder<T>().Shred(source, table, options);
        }
    }

    /// <summary>
    /// Расширение для ДатаРоу для обработки значение DBNull.Value
    /// </summary>
    public static class DataRowExtension
    {
        public static T Field2<T>(this DataRow sender, string FieldName)
        {
            if (sender[FieldName] == DBNull.Value) return default(T);
            else return sender.Field<T>(FieldName);
        }
    }

    public static class DataTableExtension
    {
        /// <summary>
        /// Объединение текущей таблицы с временной (слияние) по указанному ключу, который может быть строковый или decimal
        /// Предполагается что структура таблиц одинаковая
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tempTable">Временная таблица из которой берутся данные</param>
        /// <param name="keyColumn">Ключевое поле таблиц</param>
        public static void Merge(this DataTable sender, DataTable tempTable, string keyColumn)
        {
            if (sender.Columns[keyColumn].DataType == typeof(String))
            {
                HashSet<string> dic = new HashSet<string>(sender.Rows.OfType<DataRow>().
                    Where(r => r.RowState != DataRowState.Deleted).Select(r => (string)r[keyColumn]));
                string s;
                foreach (DataRow p in tempTable.Rows)
                {
                    s = (string)p[keyColumn];
                    if (!dic.Contains(s))
                    {
                        dic.Add(s);
                        sender.Rows.Add(p.ItemArray);
                    }
                }
            }
            else
            {
                HashSet<decimal> dic = new HashSet<decimal>(sender.Rows.OfType<DataRow>().
                    Where(r => r.RowState != DataRowState.Deleted).Select(r => (decimal)r[keyColumn]));
                decimal s;
                foreach (DataRow p in tempTable.Rows)
                {
                    s = p.Field<decimal>(keyColumn);
                    if (!dic.Contains(s))
                    {
                        dic.Add(s);
                        sender.Rows.Add(p.ItemArray);
                    }
                }
            }
        }
    }

}
