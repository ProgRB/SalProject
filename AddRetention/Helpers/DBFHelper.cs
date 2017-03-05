using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;
using System.Data.OleDb;
using System.IO;
using Microsoft.Win32;

namespace Salary.Helpers
{
    class DBFHelper
    {
    }
    public class DBFWriter
    {

        /// <summary>
        /// Записывает данные из таблицы в дбфку указанную по пути. Если надо удаляет данные по фильтру удаления
        /// </summary>
        /// <param name="DBFFilePath"></param>
        /// <param name="table"></param>
        public static string WriteToDBF(string DBFFilePath, DataTable table, bool HandleRowUpdatingErrors = false)
        {
            IgnoreErrors = false;
            if (!CheckVfpOleDb.IsInstalled())
                MessageBox.Show("Не установлен драйвер FoxPro. Обратитесь к системным администраторам для установки", "Ошибка записи", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                if (table == null)
                    throw new NullReferenceException();
                else
                {
                    string TableName = System.IO.Path.GetFileName(DBFFilePath);
                    string NewTablePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + TableName;
                    if (File.Exists(NewTablePath))
                        File.Delete(NewTablePath);
                    File.Copy(DBFFilePath, NewTablePath);
                    using (OleDbConnection fpConnection = new OleDbConnection(string.Format("Provider=VFPOLEDB.1;Data Source={0};Mode=Write;", NewTablePath)))
                    {
                        fpConnection.Open();
                        OleDbCommand cmdDelete = new OleDbCommand(string.Format("delete from {0}", System.IO.Path.GetFileNameWithoutExtension(DBFFilePath)), fpConnection);
                        cmdDelete.ExecuteNonQuery();
                        OleDbDataAdapter a = new OleDbDataAdapter(string.Format("select * from {0}", System.IO.Path.GetFileNameWithoutExtension(DBFFilePath)), fpConnection);
                        if (HandleRowUpdatingErrors)
                            a.RowUpdated += new OleDbRowUpdatedEventHandler(a_RowUpdated);

                        Dictionary<string, OleDbParameter> ColumnsMapping = new Dictionary<string, OleDbParameter>(StringComparer.CurrentCultureIgnoreCase);

                        OleDbDataReader reader = a.SelectCommand.ExecuteReader(CommandBehavior.KeyInfo);
                        DataTable schemaTable = reader.GetSchemaTable();
                        ColumnsMapping = schemaTable.Rows.OfType<DataRow>().OrderBy(p => p.Field2<Int32>("ColumnOrdinal"))
                                            .ToDictionary(y => y["ColumnName"].ToString(), p => new OleDbParameter(p["ColumnName"].ToString(),
                                        GetOleType(p["DataType"].ToString()), p.Field<Int32>("ColumnSize"), p["ColumnName"].ToString()));
                        string[] columns = ColumnsMapping.Keys.ToArray();

                        a.InsertCommand = new OleDbCommand(string.Format("INSERT INTO {0}({1}) VALUES({2})",
                            System.IO.Path.GetFileNameWithoutExtension(DBFFilePath),
                            string.Join(", ", columns.Select(r => r.ToUpper())), string.Join(", ", columns.Select(r => "?"))), fpConnection);

                        int i = 0;
                        foreach (string s in columns)
                            a.InsertCommand.Parameters.Add(string.Format("p{0}", i++), ColumnsMapping[s].OleDbType, ColumnsMapping[s].Size, ColumnsMapping[s].SourceColumn).IsNullable = true;

                        table.AcceptChanges();
                        for (i = 0; i < table.Rows.Count; ++i)
                        {
                            table.Rows[i].SetAdded();
                        }
                        a.Update(table);
                        fpConnection.Close();
                    }
                    return NewTablePath;
                }
            }
            return null;
        }
        
        private static bool IgnoreErrors = false;
        static void a_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            if (e.Errors != null)
            {
                if (IgnoreErrors)
                    e.Status = UpdateStatus.SkipCurrentRow;
                else
                {
                    MessageBoxResult r = (MessageBoxResult)Application.Current.Dispatcher.Invoke(new Func<OleDbRowUpdatedEventArgs, MessageBoxResult>(GetUserSolution), e);
                    
                    if (r == MessageBoxResult.Cancel)
                        e.Status = UpdateStatus.ErrorsOccurred;
                    else
                        if (r == MessageBoxResult.Yes)
                            e.Status = UpdateStatus.SkipCurrentRow;
                        else
                        {
                            IgnoreErrors = true;
                            e.Status = UpdateStatus.SkipCurrentRow;
                        }
                }
            }
            else
                e.Status = UpdateStatus.Continue;
        }

        public static MessageBoxResult GetUserSolution(OleDbRowUpdatedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show(Application.Current.MainWindow,
                        string.Format("Ошибка записи строки {0}. \nДанные {1}\nПоказывать сообщение при ошибках?", e.Errors.Message, string.Join("", e.Row.ItemArray.Select(rr=>string.Format("[{0}]",rr.ToString())))),
                        "Ошибка формирования", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            return r;
        }

        
        public static OleDbType GetOleType(string StringType)
        {
            Type t = Type.GetType(StringType);
            if (t == typeof(String))
                return OleDbType.VarChar;
            else
                if (t == typeof(Decimal))
                    return OleDbType.Numeric;
                else if (t == typeof(DateTime))
                    return OleDbType.Date;
                else if (t == typeof(Boolean))
                    return OleDbType.Boolean;
                else
                    throw new ArgumentOutOfRangeException(StringType);
        }


        /// <summary>
        /// Записывает данные из таблицы в дбфку со структурой по указанному пути, скопированную локально и предлагая сохранить ее
        /// </summary>
        /// <param name="DBFFilePath"></param>
        /// <param name="table"></param>
        public static void WriteDataTableToDBF(string DBFFilePath, DataTable table, string delete_where, bool with_delete = false, bool HandleRowUpdatingErrors = false)
        {
            IgnoreErrors = false;
            if (!CheckVfpOleDb.IsInstalled())
                MessageBox.Show("Не установлен драйвер FoxPro. Обратитесь к системным администраторам для установки", "Ошибка записи", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                if (table == null)
                    throw new NullReferenceException("Не задано таблица источник данных");
                else
                {
                    string TableName = System.IO.Path.GetFileNameWithoutExtension(DBFFilePath);
                    using (OleDbConnection fpConnection = new OleDbConnection($"Provider=VFPOLEDB.1;Data Source={DBFFilePath};Mode=Write;"))
                    {
                        fpConnection.Open();
                        OleDbCommand cmdDelete = new OleDbCommand($"delete from {TableName} {(string.IsNullOrEmpty(delete_where)?"":"where "+delete_where)}", fpConnection);
                        if (with_delete)
                            cmdDelete.ExecuteNonQuery();

                        OleDbDataAdapter a = new OleDbDataAdapter(string.Format("select * from {0}", TableName), fpConnection);
                        if (HandleRowUpdatingErrors)
                            a.RowUpdated += new OleDbRowUpdatedEventHandler(a_RowUpdated);

                        Dictionary<string, OleDbParameter> ColumnsMapping = new Dictionary<string, OleDbParameter>(StringComparer.OrdinalIgnoreCase);

                        //Получаем данные по дбфки, структуру, чтобы составить потом команды вставки
                        OleDbDataReader reader = a.SelectCommand.ExecuteReader(CommandBehavior.KeyInfo);
                        DataTable schemaTable = reader.GetSchemaTable();
                        ColumnsMapping = schemaTable.Rows.OfType<DataRow>().OrderBy(p => p.Field2<Int32>("ColumnOrdinal"))
                                            .ToDictionary(y => y["ColumnName"].ToString().ToUpper(), p => new OleDbParameter(p["ColumnName"].ToString(),
                                        GetOleType(p["DataType"].ToString()), p.Field<Int32>("ColumnSize"), p["ColumnName"].ToString()));

                        //Колонки берем из таблицы которую надо залить в дбфку
                        string[] columns = table.Columns.Cast<DataColumn>().Select(r=>r.ColumnName.ToUpper()).ToArray();

                        a.InsertCommand = new OleDbCommand(string.Format("INSERT INTO {0}({1}) VALUES({2})",
                            TableName,
                            string.Join(", ", columns.Select(r => r.ToUpper())), 
                            string.Join(", ", columns.Select(r => "?"))), fpConnection);

                        int i = 0;
                        foreach (string s in columns)
                        {
                            a.InsertCommand.Parameters.Add(string.Format("p{0}", i++), ColumnsMapping[s].OleDbType, ColumnsMapping[s].Size, ColumnsMapping[s].SourceColumn).IsNullable = true;
                        }

                        for (i = 0; i < table.Rows.Count; ++i)
                        {
                            table.Rows[i].SetAdded();
                        }
                        new OleDbCommand("SET NULL OFF", fpConnection).ExecuteNonQuery();
                        a.Update(table);
                        fpConnection.Close();
                    }
                }
            }
        }
    }
    /// <summary>
    /// Проверяет на наличие установленного драйвера FoxPro
    /// </summary>
    public static class CheckVfpOleDb
    {
        public static bool IsInstalled()
        {
            return Registry.ClassesRoot.OpenSubKey("TypeLib\\{50BAEECA-ED25-11D2-B97B-000000000000}") != null;
        }
    }
}
