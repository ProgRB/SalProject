using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Reflection;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using Oracle.DataAccess.Client;

namespace EntityGenerator
{
    public class RowEntityBase : SalaryEFLibrary.NotificationEFObject, IEditableObject
    {
        public static OracleConnection CurConnect
        {
            get;
            set;
        }

        public RowEntityBase()
        {
            this.AdapterConnection = CurConnect;
        }

        public RowEntityBase(DataRow d_row)
        {
            this.DataTable = d_row.Table;
            DataRow = d_row;
            this.AdapterConnection = CurConnect;
        }

        public RowEntityBase(DataTable ownedTable)
        {
            this.DataTable = ownedTable;
            DataRow new_row = ownedTable.NewRow();
            ownedTable.Rows.Add(new_row);
            DataRow = new_row;
        }

        DataRow data_row;

        public DataRow DataRow
        {
            get
            {
                return data_row;
            }
            set
            {
                if (data_row != value)
                {
                    data_row = value;
                    data_row.Table.RowDeleted += new DataRowChangeEventHandler(Table_RowDeleted);
                    data_row.Table.ColumnChanged += new DataColumnChangeEventHandler(Table_ColumnChanged);
                    if (_table == null && data_row!=null)
                        _table = data_row.Table;
                }
                RaisePropertyChanged(() => DataRow);
            }
        }

        void Table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            // событие обновления таблицы источника. Если строка обновилась из другого класса и другим методом, то генерируем события для этого свойства класса
            if (e.Row == this.data_row && !is_row_updating)
            {
                PropertyInfo p = this.GetType().GetProperties().Where(t => t.GetCustomAttributes(typeof(ColumnAttribute), true).Any(r => r != null && ((ColumnAttribute)r).Name == e.Column.ColumnName)).SingleOrDefault();
                if (p != null)
                    RaisePropertyChanged(p.Name);
            }
        }

        void Table_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            if (e.Row == this.data_row)
            {
                RaisePropertyChanged(() => EntityState);
            }
        }

        DataTable _table;
        public DataTable DataTable
        {
            get
            {
                return _table;
            }
            set
            {
                _table = value;
            }
        }

        public DataSet DataSet
        {
            get
            {
                if (DataTable == null)
                    if (DataRow != null) return DataRow.Table.DataSet;
                    else return null;
                else
                    return DataTable.DataSet;
            }
        }

        /// <summary>
        /// Обновления строки DataTable связанной с свойством класса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <param name="val"></param>
        public void UpdateDataRow<T>(Expression<Func<T>> expr, object val)
        {
            if (data_row != null && data_row.RowState != DataRowState.Deleted)
            {
                object[] attrib_info = (expr.Body as MemberExpression).Member.GetCustomAttributes(true);
                {
                    ColumnAttribute dm = attrib_info.OfType<ColumnAttribute>().FirstOrDefault();
                    if (dm != null)
                    {
                        is_row_updating = true;
                        data_row[dm.Name] = val == null ? DBNull.Value : val;
                        is_row_updating = false;
                        RaisePropertyChanged((expr.Body as MemberExpression).Member.Name);
                        RaisePropertyChanged(() => Error);
                    }
                    else
                        throw new Exception("Для свойства не установлен поле источник данных DataRow (Member Name)");
                }
            }
        }

        /// <summary>
        /// Получение значения поля по наименованию свойства класса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public T GetDataRowField<T>(Expression<Func<T>> expr)
        {
            if (data_row != null && data_row.RowState != DataRowState.Detached && data_row.RowState != DataRowState.Deleted)
            {
                object[] attrib_info = (expr.Body as MemberExpression).Member.GetCustomAttributes(true);
                ColumnAttribute dm = (expr.Body as MemberExpression).Member.GetCustomAttributes(true).OfType<ColumnAttribute>().FirstOrDefault();
                if (dm != null)
                {
                    return data_row.Field2<T>(dm.Name);
                }
                else
                    throw new Exception("Для свойства не установлен поле источник данных DataRow (DataMember)");
            }
            return default(T);
        }

        /// <summary>
        /// Получение значения поля по наименованию свойства класса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public T GetDataRowField<T>(string FieldValue)
        {
            if (data_row != null && data_row.RowState != DataRowState.Detached && data_row.RowState != DataRowState.Deleted)
            {
                if (FieldValue != null)
                {
                    return data_row.Field2<T>(FieldValue);
                }
                else
                    throw new Exception("Не указано получаемое поле FieldValue");
            }
            return default(T);
        }

        /// <summary>
        /// IDataErrorInfo реализация ошибки по наименованию поля
        /// </summary>
        /// <param name="column_name"></param>
        /// <returns></returns>
        public string this[string column_name]
        {
            get
            {
                PropertyInfo pinfo = this.GetType().GetProperty(column_name);
                object[] attrib_info = pinfo.GetCustomAttributes(true);
                foreach (object o in attrib_info)
                {
                    if (o is ColumnAttribute)
                    {
                        ColumnAttribute m = o as ColumnAttribute;
                        object val = pinfo.GetValue(this, null);
                        if (!m.CanBeNull && (val == null || val == DBNull.Value || string.IsNullOrEmpty(val.ToString()) || string.IsNullOrWhiteSpace(val.ToString())))
                            return "Поле не может быть пустым";
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Ошибка для класса, реализация интерфейса IDataErrorInfo
        /// </summary>
        public string Error
        {
            get
            {
                foreach (PropertyInfo pinfo in GetType().GetProperties())
                {
                    object[] attrib_info = pinfo.GetCustomAttributes(true);
                    foreach (object o in attrib_info)
                    {
                        if (o is ColumnAttribute)
                        {
                            ColumnAttribute m = o as ColumnAttribute;
                            object val = pinfo.GetValue(this, null);
                            if (!m.CanBeNull && (val == null || val == DBNull.Value || string.IsNullOrEmpty(val.ToString()) || string.IsNullOrWhiteSpace(val.ToString())))
                                return "Не заполнено обязательное поле";
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Наименование таблицы в DataSet для текущего класса
        /// </summary>
        public string SchemaTableName
        {
            get
            {
                object[] prop = this.GetType().GetCustomAttributes(typeof(TableAttribute), true);
                if (prop != null && prop.Length > 0)
                {
                    return (prop[0] as TableAttribute).Name;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Создает колллекцию по наименованию столбцов в таблицах
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds">Данные с таблицами</param>
        /// <param name="columnFK">Наименование внешнего столбца</param>
        /// <returns>Возвращает коллекцию с переопределенными методами вставки и удаления элементов</returns>
        public EntityRelationList<T> CreateRelationCollection<T>(DataSet ds, string columnFK) where T : RowEntityBase, new()
        {
            object[] prop = typeof(T).GetCustomAttributes(typeof(TableAttribute), true);
            string tableName = string.Empty;
            if (prop != null && prop.Length > 0)
            {
                tableName = (prop[0] as TableAttribute).Name;
                if (string.IsNullOrEmpty(tableName))
                    throw new NoNullAllowedException("Имя связанной таблицы не может быть пустой");
            }
            EntityRelationList<T> bl = new EntityRelationList<T>(ds.Tables[tableName].Rows.OfType<DataRow>().Where(r => r.RowState != DataRowState.Deleted && r[columnFK].Equals(this.DataRow[columnFK])).Select(r => ((T)Activator.CreateInstance(typeof(T), r))).ToList())
            {
                RelatedEntity = this,
                RelationColumn = columnFK
            };
            bl.AddingNew += new AddingNewEventHandler(_collectionAddinNew<T>);
            return bl;
        }

        /// <summary>
        /// Обработка добавления нового элемента коллекции
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _collectionAddinNew<T>(object sender, AddingNewEventArgs e) where T : RowEntityBase, new()
        {
            if (e.NewObject == null)
            {
                Type type = typeof(T);
                RowEntityBase r = (RowEntityBase)Activator.CreateInstance(type);
                r.SetNewEntityRow(this.DataRow.Table.DataSet);
                string columnName = (sender as EntityRelationList<T>).RelationColumn;
                r.DataRow[columnName] = this.DataRow[columnName];
                e.NewObject = r;
            }
        }

        /// <summary>
        /// Добавляет новую запись объекта исходя из его таблицы в аттрибуте
        /// </summary>
        /// <param name="nds"></param>
        public void SetNewEntityRow(DataSet nds)
        {
            string tableName = this.SchemaTableName;
            if (nds == null)
                throw new Exception("DataSet  не может быть пустым для модели");
            if (!nds.Tables.Contains(tableName))
                throw new Exception(string.Format("DataSet не содержит таблицу с именем {0}", tableName));
            DataRow r = nds.Tables[tableName].NewRow();
            nds.Tables[tableName].Rows.Add(r);
            this.DataRow = r;
        }

        /// <summary>
        /// Для добавленных строк делает ключ отрицательным для возможности вставки данных
        /// </summary>
        /// <param name="t"></param>
        /// <param name="primaryKey"></param>
        public static void SetIDAdded(DataTable t, string primaryKey)
        {
            if (t != null)
            {
                decimal k;
                if (!Decimal.TryParse(t.Compute("MIN(" + primaryKey + ")", "").ToString(), out k))
                    k = -1;
                foreach (DataRow r in t.Rows)
                {
                    if (r.RowState == DataRowState.Added && r.Field2<Decimal?>(primaryKey) > -1)
                        r[primaryKey] = --k;
                }
            }
        }

        public DataRowState EntityState
        {
            get
            {
                return this.DataRow.RowState;
            }
        }

        protected bool is_row_updating = false;

        public void BeginEdit()
        {
            //old_object = this.MemberwiseClone();
        }

        public void CancelEdit()
        {
            
        }

        public void EndEdit()
        {
            
        }
        
        /// <summary>
        /// Создает дочерний класс по для текущего по имени отношения в DataSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relationName">Имя (название) отношения в наборе данных</param>
        /// <returns></returns>
        public T AddChildEntity<T>(string relationName)
        {
            string tableName = this.SchemaTableName;
            if (this.DataSet == null)
                throw new Exception("DataSet  не может быть пустым для модели");
            DataTable relTable = this.DataSet.Tables.OfType<DataTable>().SingleOrDefault(t => t.Constraints.Contains(relationName));
            if (relTable == null)
                throw new Exception(string.Format("DataSet не содержит таблицу для этого отношения"));
            ForeignKeyConstraint fk = (ForeignKeyConstraint)relTable.Constraints[relationName];
            DataRow r = relTable.NewRow();
            r[fk.Columns[0].ColumnName] = this.DataRow[fk.RelatedColumns[0].ColumnName];
            relTable.Rows.Add(r);
            T val = (T)Activator.CreateInstance(typeof(T), r);
            return val;
        }

#region static helpers get data class
        /// <summary>
        /// Получаем имя таблицы для которой работает этот класс
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetTableName(Type t)
        {
            TableAttribute val = GetAttribute<TableAttribute>(t);
            if (val != null) return val.Name;
            return string.Empty;
        }

        /// <summary>
        /// Получаем название схемы для классса
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetSchemaName(Type t)
        {
            SchemaNameAttribute val = GetAttribute<SchemaNameAttribute>(t);
            if (val != null) return val.Name;
            return string.Empty;
        }

        /// <summary>
        /// Получаем какой столбей в базе является ключевым
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Возвращает строку - наименование в базе ключевого поля</returns>
        public static string GetPrimaryKey(Type t)
        {
            PropertyInfo pi = t.GetProperties().Where(r => r.GetCustomAttributes(typeof(ColumnAttribute), true).Any(p => (p as ColumnAttribute).IsPrimaryKey)).FirstOrDefault();
            if (pi != null) // если мы нашли поле с значение "ключевое" то надо получить у него атрибут колонка базы данных
            {
                return (pi.GetCustomAttributes(typeof(ColumnAttribute), true).First() as ColumnAttribute).Name;
            }
            return string.Empty;
        }

        /// <summary>
        /// Получаем аттрибут класса, если таковой имеется, иначе значение по умолчанию
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns>Возвращает соответствующий тип - значение атрибута класса</returns>
        public static T GetAttribute<T>(Type t) where T: Attribute
        {
            if (t != null)
            {
                object[] prop = t.GetCustomAttributes(typeof(T), true);
                if (prop != null && prop.Length > 0)
                {
                    return prop[0] as T;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Автоматически получает класс по ключевом полю таблицы.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetEntityByID<T>(object id) where T : RowEntityBase
        {
            return GetEntityByID<T>(id, null);
        }

        /// <summary>
        /// Получаем класс по айдишнику.
        /// </summary>
        /// <typeparam name="T">Тип получаемого класса (сущности)</typeparam>
        /// <param name="id">Значение ключевого поля</param>
        /// <param name="ds">Набор данных, если он уже создан</param>
        /// <returns></returns>
        public static T GetEntityByID<T>(object id, DataSet ds) where T:RowEntityBase
        {
            if (ds == null)
                ds = new DataSet();
            string schema=GetSchemaName(typeof(T)), table = GetTableName(typeof(T)), keyColumn = GetPrimaryKey(typeof(T));
            if (string.IsNullOrEmpty(schema))
                throw new ArgumentNullException("SchemaName", "Не установлен обязательный параметр в модели");
            if (string.IsNullOrEmpty(table))
                throw new ArgumentNullException("TableName", "Не установлен обязательный параметр в модели");
            if (string.IsNullOrEmpty(keyColumn))
                throw new ArgumentNullException("IsPrimaryKey", "Не установлен обязательный параметр в модели");
            OracleDataAdapter oda = new OracleDataAdapter(string.Format("select * from {0}.{1} where {2}=:p_{2}", schema, table, keyColumn), CurConnect);
            oda.SelectCommand.Parameters.Add("p_" + keyColumn, id);
            oda.SelectCommand.BindByName = true;
            oda.TableMappings.Add("Table", table);
            try
            {
                oda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            T result = Activator.CreateInstance<T>();
            result.DataRow = ds.Tables[table].Rows.OfType<DataRow>()
                .Where(p=>p.RowState!= DataRowState.Deleted && p.RowState!= DataRowState.Detached).LastOrDefault(); // сколько бы не было строк - берем последнюю
            return result;
        }

        /// <summary>
        /// Метод возвращает сущность в связанной таблице, или пусто
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relatedTableName">Имя взимосвязанной таблицы</param>
        /// <param name="relDateset">Связанные сэт таблиц</param>
        /// <param name="relatedColumName">колонка поиска значений</param>
        /// <param name="id">значение для поиска</param>
        /// <returns>Возвращает сущность типа указанного как параметр шаблона</returns>
        public T GetParentEntity<T>(string relatedTableName, DataSet relDateset, string relatedColumName, object id) where T : RowEntityBase, new()
        {
            if (DataSet.Tables.Contains(relatedTableName) && id!=null)
            {
                DataRow r =null;
                Type cur_type = id.GetType();
                if (cur_type == typeof(decimal?)) 
                    r = DataSet.Tables[relatedTableName].Select(string.Format("{0}={1}", relatedColumName,  (id ?? "-1"))).FirstOrDefault();
                else 
                    //if (cur_type == typeof(string)) 
                    r = DataSet.Tables[relatedTableName].Select(string.Format("{0}='{1}'", relatedColumName, id)).FirstOrDefault();
                if (r != null)
                    return new T() { DataRow = r };
                else
                    return null;
            }
            else return null;

        }

        /// <summary>
        /// Метод возвращает родительскую запись для поля
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relatedColumnName"></param>
        /// <returns></returns>
        public T GetParentEntity<T>(string relatedColumnName) where T:RowEntityBase, new()
        {
            return GetParentEntity<T>(RowEntityBase.GetTableName(typeof(T)), DataSet, relatedColumnName, this.GetDataRowField<object>(relatedColumnName));
        }

        /// <summary>
        /// Метод возвращает родительскую запись для поля
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relatedColumnName"></param>
        /// <returns></returns>
        public T GetParentEntity<T, P>(Expression<Func<P>> expr) where T : RowEntityBase, new()
        {
            MemberExpression mem = (expr.Body as MemberExpression);
            object[] attrib_info = mem.Member.GetCustomAttributes(true);
            ColumnAttribute dm = (expr.Body as MemberExpression).Member.GetCustomAttributes(true).OfType<ColumnAttribute>().FirstOrDefault();
            if (dm != null)
            {
                return GetParentEntity<T>(RowEntityBase.GetTableName(typeof(T)), DataSet, dm.Name, data_row.Field2<P>(dm.Name));
            }
            else
                return null;
            
        }

#endregion

        #region Adapter class helpers

        /// <summary>
        /// Соединение, используемое адаптером для получения/сохранения данных
        /// </summary>
        public OracleConnection AdapterConnection
        {
            get;
            set;
        }

        /// <summary>
        /// Адаптер используемый для сохранения данных модели. Имеется возможность перегрузить его.
        /// </summary>
        public  OracleDataAdapter DataAdapter
        {
            get;
            set;
        }


        /// <summary>
        /// Сохраненения данных в модели, с указанной транзакцией.
        /// </summary>
        /// <param name="currentTransaction">Текущая транзакция. Если она указана, то фиксация данных (Commit) не происходит, иначе автоматически фиксация или откат</param>
        /// <returns>Возвращает ошибку сохранениия или null</returns>
        public Exception Save(OracleTransaction currentTransaction)
        {
            OracleTransaction tr = currentTransaction ?? AdapterConnection.BeginTransaction();
            try
            {
                DataAdapter.Update(new DataRow[] { this.DataRow });
                if (currentTransaction == null)
                    tr.Commit();
                return null;
            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    tr.Rollback();
                return ex;
            }
        }

        /// <summary>
        /// Сохранение данных в модели, с новой транзакцией из указанного соединения
        /// </summary>
        /// <returns>Возвращает ошибку сохранениия или null</returns>
        public Exception Save()
        {
            return Save(null);
        }

        #endregion
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
}
