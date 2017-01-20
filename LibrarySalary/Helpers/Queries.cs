using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Salary;

namespace LibrarySalary.Helpers
{
    public class Queries
    {
        /// <summary>
        /// Метод получения тела запроса по его имени 
        /// </summary>
        /// <param name="queryName">Имя запроса(файла) с расширением (пример: employee.sql)</param>
        /// <param name="year">Год для использования запроса, если не задан то не использует подпапку с годом</param>
        /// <returns>Тело запроса</returns>
        public static string GetQuery(string queryName, decimal? current_year = null)
        {
            TextReader reader = new StreamReader(Connect.CurrentAppPath + "/Queries/" + (current_year.HasValue ? current_year.Value.ToString() + "/" : string.Empty) + queryName, Encoding.GetEncoding(1251));
            string st = reader.ReadToEnd();
            reader.Close();
            return st;
        }

        public static string GetQueryWithSchema(string queryName)
        {
            return string.Format(GetQuery(queryName), Connect.SchemaApstaff, Connect.SchemaSalary);
        }

    }
    
}
