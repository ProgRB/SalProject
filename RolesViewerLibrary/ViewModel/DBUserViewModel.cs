using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Salary.Helpers;
using Oracle.DataAccess.Client;
using System.Data;
using System.Windows;
using EntityGenerator;


namespace RolesViewerLibrary
{
    public class DBUserViewModel:NotificationObject
    {
        OracleConnection connect;
        OracleDataAdapter odaUsers;

        DataSet ds;
        private DBUser _currentUser;
        private List<DBUser> _userSource;
        private  DBAccessSubdiv _currentAccessSubdiv;
        private UserViewFilter userFilter;
        private List<ApplicationAccess> _appAccessSource;

        public DBUserViewModel(OracleConnection _connect)
        {
            UsersKadrExplicit.LoadDictionary(_connect);
            ds = new DataSet();
            connect = _connect;
            odaUsers = new OracleDataAdapter(@"select * from
                            (
                                select username, 
                                    case when account_status like '%EXPIRED%' then 1 else 0 end is_expired,
                                    case when account_status like '%LOCKED%' then 1 else 0 end is_locked,
                                    lock_date,
                                    created,
                                    last_login,
                                    nvl(per_num, case when regexp_like(username, '\w{3}\d{5}') then substr(username, 4) end) per_num
                                from dba_users
                                    left join (select upper(LOGIN) username, per_num from apstaff.users_kadr) using (username)
                            ) t
                            left join 
                                (select per_num, max(code_subdiv) keep (dense_rank last order by date_transfer) code_subdiv,
                                    max(pos_name) keep (dense_rank last order by date_transfer) pos_name,
                                    case when max(type_transfer_id) keep (dense_rank last order by date_transfer) =3 then 'X' end SIGN_FIRED
                                 from
                                    apstaff.transfer
                                    left join  apstaff.position using (pos_id)
                                    left join apstaff.subdiv using (subdiv_id)
                                 where sign_comb=0
                                 group by per_num
                                ) using (per_num)
                            left join (select per_num, photo, EMP_LAST_NAME||' '||SUBSTR(EMP_FIRST_NAME,1,1)||'.'||substr(emp_middle_name,1,1)||'.' FIO 
                                        from apstaff.emp) using (per_num)
                            where
                                (:p_code_subdiv is null or 
                                 exists(select 1 from apstaff.subdiv where code_subdiv=:p_code_subdiv 
                                        start with subdiv_id in (select subdiv_id from apstaff.access_subdiv 
                                                                    where USER_NAME=t.username and (:p_app_name is null or app_name=:p_APP_NAME))
                                        connect by prior subdiv_id=parent_id))
                            order by USERNAME", connect);
            odaUsers.SelectCommand.BindByName = true;
            odaUsers.SelectCommand.Parameters.Add("P_CODE_SUBDIV", OracleDbType.Varchar2, "", ParameterDirection.Input);
            odaUsers.SelectCommand.Parameters.Add("P_APP_NAME", OracleDbType.Varchar2, "", ParameterDirection.Input);
            odaUsers.TableMappings.Add("Table", "USERS");

            var p1 = new OracleDataAdapter("select * from apstaff.subdiv", connect);
            p1.Fill(ds, "SUBDIV");

            var p2 = new OracleDataAdapter(@"select distinct app_name from apstaff.access_subdiv order by 1", connect).Fill(ds, "APP_ACCESS");
                      

        }

        /// <summary>
        /// Текущий выбранный сотрудник
        /// </summary>
        public DBUser CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                RaisePropertyChanged(() => CurrentUser);
            }

        }

        /// <summary>
        /// Список доступных приложений доступа для доступа
        /// </summary>
        public List<ApplicationAccess> ApplicationAccessSource
        {
            get
            {
                if (_appAccessSource == null)
                {
                    ds.Tables["APP_ACCESS"].Columns.Add("ALIAS");
                    ds.Tables["APP_ACCESS"].Columns.Add("ID", typeof(decimal));
                    var aliases = AppXmlHelper.GetElements("AppAccessAlias").Select(r=>new {AppName= r.Attribute("AppName").Value, Alias = r.Attribute("Alias").Value, ID = decimal.Parse(r.Attribute("ID").Value)})
                        .ToDictionary(r => r.AppName, r=> new Tuple<string, decimal>(r.Alias, r.ID));
                    foreach (DataRow r in ds.Tables["APP_ACCESS"].Rows)
                    {
                        r["ALIAS"] = aliases.ContainsKey(r.Field2<string>("APP_NAME")) ? aliases[r.Field2<string>("APP_NAME")].Item1 : r.Field2<string>("APP_NAME");
                        r["ID"] = aliases.ContainsKey(r.Field2<string>("APP_NAME")) ? aliases[r.Field2<string>("APP_NAME")].Item2 : 100;
                    }
                    _appAccessSource = ds.Tables["APP_ACCESS"].ConvertToEntityList<ApplicationAccess>().OrderBy(r=> new Tuple<decimal?, string>( r.ID, r.AppName)).ToList();
                }
                return _appAccessSource;
            }
        }

        /// <summary>
        /// Все подразделения завода
        /// </summary>
        public List<Subdiv> SubdivAllSource
        { 
            get
            {
                return ds.Tables["SUBDIV"].Rows.OfType<DataRow>().Select(r => new Subdiv() { DataRow = r })
                    .OrderBy(r=>new Tuple<string, short?>(r.CodeSubdiv, r.SubActualSign)).ToList();//сортируем по коду подразделения и по актуальности
            }
        }

        /// <summary>
        /// Загружаем данные по пользователям системы
        /// </summary>
        public void LoadUsers()
        {
            try
            {
                if (ds.Tables.Contains("USERS"))
                    ds.Tables["USERS"].Rows.Clear();
                odaUsers.SelectCommand.SetParameters(this.UserFilter);
                odaUsers.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения списка пользователей\n"+ex.Message, "Просмотр пользователей");
            }
        }

        /// <summary>
        /// Источник списка пользователей
        /// </summary>
        public List<DBUser> UserSource
        {
            get
            {
                if (_userSource == null)
                {
                    LoadUsers();
                    UpdateUserSource();
                }
                return _userSource.Where(r=>r.UserName.ToUpper().Contains((UserFilter.UserName??string.Empty).ToUpper())).ToList();
            }
        }

        private void UpdateUserSource()
        {
            _userSource = ds.Tables["USERS"].Rows.OfType<DataRow>().Select(r => new DBUser(connect, ds) { DataRow = r }).ToList();
        }

        /// <summary>
        /// Текущее выбранное подразделение в строчке
        /// </summary>
        public DBAccessSubdiv CurrentAccessSubdiv 
        { 
            
            get
            {
                return _currentAccessSubdiv;
            }
            set
            {
                _currentAccessSubdiv = value;
                RaisePropertyChanged(()=>CurrentAccessSubdiv);
            }
        }

        /// <summary>
        /// Фильтр по пользователям
        /// </summary>
        public UserViewFilter UserFilter
        {
            get
            {
                if (userFilter == null)
                {
                    userFilter = new UserViewFilter();
                    userFilter.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(userFilter_PropertyChanged);
                }
                return userFilter;
            }
        }

        void userFilter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName=="UserName")
                RaisePropertyChanged(() => UserSource);
        }

        /// <summary>
        /// Принудительно обновляем весь список пользователей
        /// </summary>
        internal void RefreshUserList()
        {
            LoadUsers();
            UpdateUserSource();
            RaisePropertyChanged(() => UserSource);
        }
    }

    public class UserViewFilter:NotificationObject
    {
        private string _user_name;
        private string _accessCodeSubdiv;
        private string appName;

        /// <summary>
        /// Фильтр пользователя
        /// </summary>
        [OracleParameterMapping(ParameterName = "P_USER_NAME")]
        public string UserName
        {
            get
            {
                return _user_name;
            }
            set
            {
                _user_name = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        /// <summary>
        /// Подразделение доступа фильтр
        /// </summary>
        [OracleParameterMapping(ParameterName = "P_CODE_SUBDIV")]
        public string AccessCodeSubdiv
        {
            get
            {
                return _accessCodeSubdiv;
            }
            set
            {
                _accessCodeSubdiv = value;
                RaisePropertyChanged(() => AccessCodeSubdiv);
            }
        }

        /// <summary>
        /// Имя приложения для фильтра
        /// </summary>
        [OracleParameterMapping(ParameterName ="p_app_name")]
        public string ApplicationName
        {
            get
            {
                return appName;
            }
            set
            {
                appName = value;
                RaisePropertyChanged(() => ApplicationName);
            }
        }

    }
}
