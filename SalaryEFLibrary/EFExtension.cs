using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Oracle.DataAccess.Client;
using System.ComponentModel;
using System.Linq.Expressions;
using EntityGenerator;

namespace EntityGenerator
{
    public static class EntityExtensionConverter
    {
        public static T ConvertToEntity<T>(this DataRow tableRow) where T : new()
        {
            // Create a new type of the entity I want
            Type t = typeof(T);
            T returnObject = new T();

            foreach (DataColumn col in tableRow.Table.Columns)
            {
                string colName = col.ColumnName;

                // Look for the object's property with the columns name, ignore case
                PropertyInfo pInfo = t.GetProperty(colName.ToLower(),
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // did we find the property ?
                if (pInfo != null)
                {
                    object val = tableRow[colName];

                    // is this a Nullable<> type
                    bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null || pInfo.PropertyType.IsArray);
                    if (IsNullable)
                    {
                        if (val is System.DBNull)
                        {
                            val = null;
                        }
                        else
                        {
                            // Convert the db type into the T we have in our Nullable<T> type
                            val = Convert.ChangeType(val, Nullable.GetUnderlyingType(pInfo.PropertyType) ?? pInfo.PropertyType);
                        }
                    }
                    else
                    {
                        // Convert the db type into the type of the property in our entity
                        val = Convert.ChangeType(val, pInfo.PropertyType);
                    }
                    // Set the value of the property with the value from the db
                    pInfo.SetValue(returnObject, val, null);
                }
            }

            // return the entity object with values
            return returnObject;
        }
        public static List<T> ConvertToList<T>(this DataTable table) where T : new()
        {
            Type t = typeof(T);

            // Create a list of the entities we want to return
            List<T> returnObject = new List<T>();

            // Iterate through the DataTable's rows
            foreach (DataRow dr in table.Rows)
            {
                // Convert each row into an entity object and add to the list
                T newRow = dr.ConvertToEntity<T>();
                returnObject.Add(newRow);
            }

            // Return the finished list
            return returnObject;
        }

        /// <summary>
        /// Конвертирует строку в указанный тип, унаследованный от RowEntityBase
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablerow"></param>
        /// <returns></returns>
        public static T ConvertToRowEntity<T>(this DataRow tablerow) where T : new()
        {
            T returnObject = new T();
            Type t = typeof(T);
            PropertyInfo pInfo = t.GetProperty("DataRow",
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (pInfo != null)
                pInfo.SetValue(returnObject, tablerow, null);
            return returnObject;
        }

        /// <summary>
        /// Конвертируем таблицу в список сущностей
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">входная таблица</param>
        /// <returns></returns>
        public static List<T> ConvertToEntityList<T>(this DataTable table) where T : RowEntityBase, new()
        {
            if (table == null)
                return null;
            else
                return table.Rows.OfType<DataRow>().Where(r=>r.RowState!= DataRowState.Detached && r.RowState!=DataRowState.Deleted).Select(r => new T() { DataRow = r }).ToList();
        }
    }
    

    public class DataEntityBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Строка исчтоник
        /// </summary>
        public DataRow DataRow
        {
            get
            {
                return _dataRow;
            }
            set
            {
                _dataRow = value;
            }
        }

        //private object 
        
        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            RaisePropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private System.Data.DataRow _dataRow;
    }
}
