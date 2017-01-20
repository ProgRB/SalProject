using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Salary;
using Oracle.DataAccess.Client;
using System.Data;

namespace LibrarySalary.Helpers
{
    public class ControlRoles
    {
        private static HashSet<string> _controlRoles;
        static ControlRoles()
        {
            _controlRoles = new HashSet<string>();
        }
        public static bool GetState(string ControlName)
        {
            return _controlRoles.Contains(ControlName.ToUpper());
        }
        public static bool GetState(ICommand cmd)
        {
            if (cmd is RoutedUICommand)
                return GetState((cmd as RoutedUICommand).Name);
            else return false;
        }
        public static void LoadControlRoles()
        {
            if (Connect.CurConnect != null)
            {
                _controlRoles.Clear();
                OracleCommand cmd = new OracleCommand(string.Format("select * from {0}.user_roles where upper(role_name) in (select granted_role from user_role_privs) or upper(role_name) in (select granted_role from role_role_privs) or upper(trim(role_name)) = upper(trim(ora_login_user))", Connect.SchemaApstaff), Connect.CurConnect);
                DataTable t = new DataTable();
                new OracleDataAdapter(cmd).Fill(t);
                for (int i = 0; i < t.Rows.Count; ++i)
                    if (!_controlRoles.Contains(t.Rows[i]["Component_Name"].ToString().ToUpper()))
                    {
                        _controlRoles.Add(t.Rows[i]["Component_Name"].ToString().ToUpper());
                    }
            }
        }
    }

    public class GrantedRoles
    {
        private static HashSet<string> _grantedRoles;
        static GrantedRoles()
        {
            _grantedRoles = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);
            LoadGrantedRole();
        }
        public static void LoadGrantedRole()
        {
            _grantedRoles.Clear();
            if (Connect.CurConnect != null)
            {
                OracleDataReader drGrantedRoles = new OracleCommand(string.Format(
                    @"select GRANTED_ROLE from user_role_privs union 
                      select  GRANTED_ROLE from role_role_privs"), Connect.CurConnect).ExecuteReader();
                while (drGrantedRoles.Read())
                {
                    _grantedRoles.Add(drGrantedRoles["GRANTED_ROLE"].ToString());
                }
            }
        }
        public static bool CheckRole(string RoleName)
        {
            return _grantedRoles.Contains(RoleName);
        }
        public static int CountGrantedRoles
        {
            get { return _grantedRoles.Count(); }
        }
    }
}
