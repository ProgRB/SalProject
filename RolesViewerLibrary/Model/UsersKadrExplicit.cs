using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace RolesViewerLibrary
{
    class UsersKadrExplicit
    {
        static Dictionary<string, string> users = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        static Dictionary<string, string> role_notes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Проверяет пользователя на наличие в базе и если он есть - то возвращается его табельный иначе null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CheckName(string name)
        {
            if (users.ContainsKey(name))
                return users[name];
            else
                return null;
        }

        /// <summary>
        /// Загрузка данных по нестандартным именам пользователей БД
        /// </summary>
        /// <param name="connect"></param>
        public static void LoadDictionary(OracleConnection connect)
        {
            try
            {
                DataSet ds = new DataSet();
                OracleDataAdapter a = new OracleDataAdapter(@"
    declare 
    begin 
        open :c1 for select * from apstaff.USERS_KADR;
        open :c2 for select * from apstaff.role_note;
    end;", connect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                a.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
                a.TableMappings.Add("Table", "USER_KADR");
                a.TableMappings.Add("Table1", "ROLE_NOTE");
                a.Fill(ds);
                users.Clear();
                users = ds.Tables["USER_KADR"].Rows.OfType<DataRow>().Select(r => new { Key = r["LOGIN"].ToString(), Value = r["LOGIN"].ToString() })
                    .ToDictionary(r => r.Key, r => r.Value, StringComparer.OrdinalIgnoreCase);
                role_notes.Clear();
                role_notes = ds.Tables["ROLE_NOTE"].Rows.OfType<DataRow>().Select(r => new { Key = r["ROLE_NAME"].ToString(), Value = r["ROLE_DESCRIPTION"].ToString() })
                    .ToDictionary(r => r.Key, r => r.Value, StringComparer.OrdinalIgnoreCase);

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ошибка получения данных описания ролей и пользвателей " + ex.Message, "Просмотр пользователей");
            }

        }

        /// <summary>
        /// Возвращает описание роли или ее название если описания нет
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static string GetRoleDescription(string roleName)
        {
            if (role_notes.ContainsKey(roleName))
                return role_notes[roleName];
            return
                roleName;
        }
    }
}
