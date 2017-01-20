using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.DirectoryServices.ActiveDirectory;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Data;
//using System.Data.EntityClient;
//using System.Data.Metadata.Edm;
namespace Salary
{
    public class Connect
    {
        private static  OracleConnection _connect=null;
        private static string _schema1,_schema2;
        public static Dictionary<string, string> parameters;

        /// <summary>
        /// Статичный конструктор соединения - считывает настройки соединения
        /// </summary>
        static Connect()
        {
            if (File.Exists(Connect.CurrentAppPath+@"\connect.ini"))
            {
                FileStream f = new FileStream(Connect.CurrentAppPath + @"\connect.ini", FileMode.Open, FileAccess.Read);
                StreamReader r = new StreamReader(f);
                parameters = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
                string[] s = new string[] { };
                string st;
                while (!r.EndOfStream)
                {
                    st = r.ReadLine().Trim();
                    if (st.Length > 1 && st.Substring(0, 2) == "//") continue;
                    s = st.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    parameters.Add(s[0].ToUpper(), s[1].ToUpper());
                }
                r.Close();
                _schema1 = parameters["SCHEMENAME1"];
                _schema2 = parameters["SCHEMENAME2"];
                parameters.Add("UserID", null);
            }
        }

        /// <summary>
        /// Статичный метод создания соединения, возврадает соединение и результат попытки соединения
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="pass"></param>
        /// <param name="new_pass"></param>
        /// <returns></returns>
        private static Tuple<OracleConnection, ResultConnection> CreateNewConnection(string user_id, string pass, string new_pass = null )
        {
            try
            {
                string con_string = Connect.GetConnectionStringForODP_Net(user_id, pass);
                OracleConnection cn_connect = new OracleConnection(con_string);
                try
                {
                    parameters["UserID"] = user_id;
                    try
                    {
                        cn_connect.Open();
                        if (!string.IsNullOrEmpty(new_pass))
                        {
                            if (CheckPasswordPattern(new_pass, user_id))
                                ChangeUserPassword(new_pass, cn_connect);
                            else
                                return new Tuple<OracleConnection, ResultConnection>(null, new ResultConnection(ConnectState.ImpossibleNewPassword));
                        }
                    }
                    catch (OracleException ee)
                    {
                        if (ee.Number == 28001)
                            if (string.IsNullOrEmpty(new_pass) || CheckPasswordPattern(new_pass, user_id)) //если не задан новый пароль или он соответствует требованиям
                                cn_connect.OpenWithNewPassword(new_pass);
                            else
                                return new Tuple<OracleConnection, ResultConnection>(null, new ResultConnection(ConnectState.ImpossibleNewPassword));
                        else
                            throw ee;
                    }
                    SetPassword(new_pass ?? pass);
                    try
                    {
                        File.WriteAllLines(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\AddRetentLogin.ini", new string[] { user_id.ToUpper() });
                    }
                    catch { };
                    return new Tuple<OracleConnection, ResultConnection>(cn_connect, new ResultConnection(ConnectState.Open));
                }
                catch (OracleException e)
                {
                    switch (e.Number)
                    {
                        case 1017: return new Tuple<OracleConnection, ResultConnection>(null, new ResultConnection(ConnectState.InvalidPassword, null)); break;
                        case 28001: return new Tuple<OracleConnection, ResultConnection>(null, new ResultConnection(ConnectState.PassExpired, e)); break;
                        case 28000: return new Tuple<OracleConnection, ResultConnection>(null, new ResultConnection(ConnectState.AccountLock, e)); break;
                        case -20220: return new Tuple<OracleConnection, ResultConnection>(_connect, new ResultConnection(ConnectState.ImpossibleNewPassword, e)); break;
                        default: new ResultConnection(ConnectState.OtherError, e); break;
                    }
                    return new Tuple<OracleConnection, ResultConnection>(null, new ResultConnection(ConnectState.OtherError, e));
                }
            }
            catch (Exception e)
            {
                return new Tuple<OracleConnection, ResultConnection>(null, new ResultConnection(ConnectState.OtherError, e));
            }
        }

        /// <summary>
        /// Команда изменения пароля на новый.
        /// </summary>
        /// <param name="new_pass"></param>
        /// <param name="connect1"></param>
        public static void ChangeUserPassword(string new_pass, OracleConnection connect1)
        {
            new OracleCommand(string.Format("ALTER USER {0} IDENTIFIED BY \"{1}\"", UserID, new_pass), connect1).ExecuteNonQuery();
        }


        /// <summary>
        /// Проверка пароля на соответствие правилам
        /// </summary>
        /// <param name="password"></param>
        /// <param name="user_name"></param>
        /// <returns></returns>
        public static bool CheckPasswordPattern(string password, string user_name)
        {
            if (Regex.IsMatch(user_name.ToUpper(), password.ToUpper())) // пароль не является подстрокой имени пользователя
                return false;
            if (password.Contains('"')) return false; // не содержит двойных кавычек (чтобы не было проблем)
            if (password.Length < 3) return false; // не менее 3х симоволов
            return true;
        }

        /// <summary>
        /// Статичный метод создания нвого соединения
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="pass"></param>
        /// <param name="new_pass"></param>
        /// <returns></returns>
        public static ResultConnection NewConnection(string user_id, string pass, string new_pass = null)
        {
            string con_string = Connect.GetConnectionStringForODP_Net(user_id, pass);
            Tuple<OracleConnection, ResultConnection> res = CreateNewConnection(user_id, pass, new_pass);
            _connect = res.Item1;
            return res.Item2;
        }

        public static OracleConnection CreateNewUserConnection()
        {
            try
            {
                try
                {
                    string con_string = Connect.GetConnectionStringForODP_Net(Connect.UserID, ProtectString.Unprotect(pass_data));
                    OracleConnection cn = new OracleConnection(con_string);
                    cn.Open();
                    return cn;
                }
                catch (OracleException e)
                {
                    switch (e.Number)
                    {
                        case 1017: MessageBox.Show("Вы ввели неправильное имя пользователя или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); break;
                        case 28001: MessageBox.Show("Срок действия временного пароля истек. Вы обязаны установить новый пароль, неравный табельному номеру!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); break;
                        case 28000: MessageBox.Show("Пользователь заблокирован!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); break;
                        default: MessageBox.Show(string.Format("Error code: {0} \nMessage: {1} \nCode: {2}", e.ErrorCode, e.Message, e.Number), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Объект: {0} \nMessage: {1}", e.TargetSite, e.Message), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }

       

        private static bool ReConnect()
        {
            while (_connect.State!= System.Data.ConnectionState.Open || !Ping())
            {
                Thread.Sleep(1000);
                _connect.Close();
                try
                {
                    _connect.Open();
                }
                catch 
                {
                    OracleConnection.ClearAllPools();
                };
            }
            return true;
        }

        /// <summary>
        /// Возвращает текущее соединение или пытается его восстановить
        /// </summary>
        public static OracleConnection CurConnect
        {
            get
            {
                if (_connect == null) return _connect;
                if (!Ping())
                {
                    if (ConnectionBroken != null) // Если сервер не пингуется, то сообщаем что соединение сброшено
                        ConnectionBroken(null, EventArgs.Empty);
                    ReConnect();
                }
                return _connect;
            }
        }

        /// <summary>
        /// Указатель на метод, вызывающийся при невозможности проверить соединение с сервером.
        /// </summary>
        public static event EventHandler ConnectionBroken;


        /// <summary>
        /// Закрыть текущее соединение без попытки пересоздания
        /// </summary>
        public static void CloseConnection()
        {
            if (_connect != null)
            {
                OracleConnection.ClearAllPools();
                _connect.Close();
            }
        }
        
        /// <summary>
        /// Пытаемся пропинговать сервер и порт на доступность.
        /// </summary>
        /// <returns></returns>
        public static bool Ping()
        {
            try
            {
                new OracleCommand("select null from dual", _connect).ExecuteNonQuery();
                return true;
            }
            catch
            {
                try
                {
                    TcpClient tc = new TcpClient(parameters["server"], int.Parse(parameters["port"]));
                    tc.Close();
                    if (_connect.State == System.Data.ConnectionState.Open)
                        _connect.Close();
                    _connect.Open();
                    new OracleCommand("select null from dual", _connect).ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static string SchemaApstaff
        {
            get { return _schema1; }
        }
        public static string SchemaSalary
        {
            get { return _schema2; }
        }
        public static void Commit()
        {
            new Oracle.DataAccess.Client.OracleCommand("commit", Connect.CurConnect).ExecuteNonQuery();
        }
        
        /// <summary>
        /// Подразумевалось как свойство что приложение (соединеие) занято выполнением
        /// </summary>
        public static bool AppBusy
        {
            get { return _AppBusy; }
            set { _AppBusy = value; }
        }

        /// <summary>
        /// Имя пользователя в БД
        /// </summary>
        public static string UserID
        {
            get
            {
                return parameters["UserID"];
            }
            set
            {
                parameters["UserID"] = value;
            }
        }

        /// <summary>
        /// ФИО пользователя в базе данных - настоящие.
        /// </summary>
        public static string UserFullName
        {
            get
            {
                if (_userName == null)
                    GetUserName();
                return _userName;
            }
        }

        /// <summary>
        /// Подразделение в котором числится сотрудник
        /// </summary>
        public static string CodeSubdiv
        {
            get
            {
                if (_codeSubdiv == null)
                    GetUserName();
                return _codeSubdiv;
            }
        }
        private static void GetUserName()
        {
            if (_connect != null)
            {
                try
                {
                    OracleCommand cmd = new OracleCommand("begin APSTAFF.GET_CURRENT_USER_DATA(:c);end;", _connect);
                    cmd.BindByName = true;
                    cmd.Parameters.Add("c", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                    using (OracleDataAdapter r = new OracleDataAdapter(cmd))
                    {
                        DataTable t = new DataTable();
                        r.Fill(t);
                        if (t.Rows.Count>0)
                        {
                            _userName = t.Rows[0]["FIO"] as string;
                            _codeSubdiv = t.Rows[0]["CODE_SUBDIV"] as string;
                        }
                    }
                }
                catch (Exception ex)
                { }
            }

        }

        protected static byte[] pass_data;
        private static bool _AppBusy;

        /// <summary>
        /// Сохраняем пароль в зашифрованном виде в памяти
        /// </summary>
        /// <param name="pass"></param>
        public static void SetPassword(string pass)
        {
            pass_data = ProtectString.Protect(pass);
        }
        static Dictionary<string, DateTime> Vals = new Dictionary<string, DateTime>(StringComparer.CurrentCultureIgnoreCase);
        private static string _userName;
        private static string _codeSubdiv;
        
        /// <summary>
        /// Проверка даты блокировки приложения из файла
        /// </summary>
        public static DateTime? BlockApp
        {
            get
            {
                try
                {
                    Vals.Clear();
                    int pos = 0;
                    DateTime date1, dateTimeNow;
                    using (DomainController dc = DomainController.GetDomainController(new DirectoryContext(DirectoryContextType.DirectoryServer, "ds00.uuap.com")))
                    {
                        dateTimeNow = TimeZoneInfo.ConvertTimeFromUtc(dc.CurrentTime, TimeZoneInfo.FindSystemTimeZoneById("North Asia East Standard Time"));
                    }
                    string[] lines = WriteSafeReadAllLines(Connect.CurrentAppPath + @"\TimeBlock.ini");
                    foreach(string str in lines)
                    {
                        pos = str.IndexOf(' ');
                        if (pos != -1)
                        {
                            DateTime.TryParse(str.Substring(pos + 1), out date1);
                            Vals.Add(str.Substring(0, pos), date1);
                        }
                        else
                            Vals.Add(str, new DateTime());
                    }
                    if (Vals["TimeBlockBegin"] != new DateTime() && Vals["TimeBlockEnd"] != new DateTime())
                    {
                        if (Vals["TimeBlockBegin"] <= dateTimeNow && Vals["TimeBlockEnd"] >= dateTimeNow)
                            return Vals["TimeBlockEnd"];
                    }
                    else
                    {
                        if (Vals["TimeBlockEnd"] != new DateTime() && Vals["TimeBlockEnd"] >= dateTimeNow)
                            return Vals["TimeBlockEnd"];
                    }
                    return null;
                }
                catch
                {
                    return null;
                }
            }
        }

        private static string[] WriteSafeReadAllLines(String path)
        {
            using (var csv = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(csv))
            {
                List<string> file = new List<string>();
                while (!sr.EndOfStream)
                {
                    file.Add(sr.ReadLine());
                }

                return file.ToArray();
            }
        }

        public static string GetConnectionStringForODP_Net(string _user, string pass)
        {
            return string.Format("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={2})));POOLING=True;User Id={3};Password=\"{4}\";Incr Pool Size=1; Decr Pool Size=1;Max Pool Size=4;",
                            parameters["SERVER"],
                            parameters["PORT"],
                            parameters["SID"],
                            _user,
                            pass);
        }

        public static string GetConnectionString(string _user, string pass)
        {
            return
                    string.Format("DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={2})));POOLING=FALSE;USER ID={3};PASSWORD={4};",
                        parameters["SERVER"],
                        parameters["PORT"],
                        parameters["SID"],
                        _user,
                        pass);
        }
        public static string Password
        {
            get
            {
                return ProtectString.Unprotect(pass_data);
            }
        }

        /// <summary>
        /// Возвращает директорию приложения на компьютере пользователя
        /// </summary>
        public static string UserSpecialFolder
        {
            get
            {
                return System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            }
        }

        /// <summary>
        /// Возвращает папку где выполняется приложение. Корневой каталог
        /// </summary>
        public static string CurrentAppPath
        {
            get
            { return System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName); }
        }
#region Раздел соединения EntityFramework
        public static string GetEFConnectionMetadata()
        {
            string metadata = EFMetadata,
                    provider = "Oracle.DataAccess.Client",
            serverName = Connect.GetConnectionString(Connect.UserID, Connect.Password);
            string conn_string = string.Format("metadata={0};provider={1};provider connection string=\"{2}\";",
                metadata,
                provider,
                serverName);
            return conn_string;
        }
        public static string EFMetadata
        {
            get
            {
                return "res://*/SalaryModel.csdl|res://*/SalaryModel.ssdl|res://*/SalaryModel.msl";
            }
        }
#endregion
    }


    public class ResultConnection
    {
        public ConnectState State
        {
            get;
            set;
        }
        public Exception Exception
        {
            get;
            set;
        }
        public ResultConnection(ConnectState state, Exception ex=null)
        {
            State = state;
            Exception = ex;
        }
    }
    public enum ConnectState
    {
        Open = 1,
        InvalidPassword = 2,
        PassExpired = 3,
        AccountLock = 4,
        ImpossibleNewPassword = 5,
        OtherError = 6
    }
    public class ProtectString
    {
        private static byte[] s_entropy = { 8, 7, 15, 15 };
        public static byte[] Protect(string stringData)
        {
            try
            {
                return ProtectedData.Protect(System.Text.Encoding.Unicode.GetBytes(stringData), s_entropy, DataProtectionScope.LocalMachine);
            }
            catch { };
            return null;
        }
        public static string Unprotect(byte[] data)
        {
            try
            {
                return System.Text.Encoding.Unicode.GetString(ProtectedData.Unprotect(data, s_entropy, DataProtectionScope.LocalMachine));
            }
            catch { };
            return null;
        }
    }

    
}
