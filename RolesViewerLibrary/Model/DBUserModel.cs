using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Salary.Helpers;
using System.Text.RegularExpressions;
using Oracle.DataAccess.Client;
using System.Data;
using System.Windows;
using EntityGenerator;
using System.Data.Linq.Mapping;

namespace RolesViewerLibrary
{
    public class DBUser : RowEntityBase
    {
        OracleDataAdapter  odaAccessSubdiv, odaDBRoles;
        OracleConnection connect;

        public DBUser(OracleConnection _conenct, DataSet dd)
        {
            connect = _conenct;

            odaAccessSubdiv = new OracleDataAdapter(@"select * from apstaff.access_subdiv where upper(user_name)=upper(:p_user_name)", connect);
            odaAccessSubdiv.SelectCommand.BindByName = true;
            odaAccessSubdiv.SelectCommand.Parameters.Add("p_user_name", OracleDbType.Varchar2, null, ParameterDirection.Input);
            odaAccessSubdiv.TableMappings.Add("Table", "ACCESS_SUBDIV");

            odaAccessSubdiv.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.ACCESS_SUBDIV_UPDATE(:p_ACCESS_SUBDIV_ID,:p_USER_NAME,:p_SUBDIV_ID,:p_APP_NAME,:p_DATE_START_ACCESS,:p_DATE_END_ACCESS);end;", "APSTAFF"), connect);
            odaAccessSubdiv.InsertCommand.BindByName = true;
            odaAccessSubdiv.InsertCommand.Parameters.Add("p_ACCESS_SUBDIV_ID", OracleDbType.Decimal, 0, "ACCESS_SUBDIV_ID").Direction = ParameterDirection.InputOutput;
            odaAccessSubdiv.InsertCommand.Parameters["p_ACCESS_SUBDIV_ID"].DbType = DbType.Decimal;
            odaAccessSubdiv.InsertCommand.Parameters.Add("p_USER_NAME", OracleDbType.Varchar2, 0, "USER_NAME").Direction = ParameterDirection.Input;
            odaAccessSubdiv.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaAccessSubdiv.InsertCommand.Parameters.Add("p_APP_NAME", OracleDbType.Varchar2, 0, "APP_NAME").Direction = ParameterDirection.Input;
            odaAccessSubdiv.InsertCommand.Parameters.Add("p_DATE_START_ACCESS", OracleDbType.Date, 0, "DATE_START_ACCESS").Direction = ParameterDirection.Input;
            odaAccessSubdiv.InsertCommand.Parameters.Add("p_DATE_END_ACCESS", OracleDbType.Date, 0, "DATE_END_ACCESS").Direction = ParameterDirection.Input; 
            
            odaAccessSubdiv.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.ACCESS_SUBDIV_UPDATE(:p_ACCESS_SUBDIV_ID,:p_USER_NAME,:p_SUBDIV_ID,:p_APP_NAME,:p_DATE_START_ACCESS,:p_DATE_END_ACCESS);end;", "Apstaff"), connect);
            odaAccessSubdiv.UpdateCommand.BindByName = true;
            odaAccessSubdiv.UpdateCommand.Parameters.Add("p_ACCESS_SUBDIV_ID", OracleDbType.Decimal, 0, "ACCESS_SUBDIV_ID").Direction = ParameterDirection.InputOutput;
            odaAccessSubdiv.UpdateCommand.Parameters["p_ACCESS_SUBDIV_ID"].DbType = DbType.Decimal;
            odaAccessSubdiv.UpdateCommand.Parameters.Add("p_USER_NAME", OracleDbType.Varchar2, 0, "USER_NAME").Direction = ParameterDirection.Input;
            odaAccessSubdiv.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaAccessSubdiv.UpdateCommand.Parameters.Add("p_APP_NAME", OracleDbType.Varchar2, 0, "APP_NAME").Direction = ParameterDirection.Input;
            odaAccessSubdiv.UpdateCommand.Parameters.Add("p_DATE_START_ACCESS", OracleDbType.Date, 0, "DATE_START_ACCESS").Direction = ParameterDirection.Input;
            odaAccessSubdiv.UpdateCommand.Parameters.Add("p_DATE_END_ACCESS", OracleDbType.Date, 0, "DATE_END_ACCESS").Direction = ParameterDirection.Input; 
            
            odaAccessSubdiv.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.ACCESS_SUBDIV_DELETE(:p_ACCESS_SUBDIV_ID);end;", "Apstaff"), connect);
            odaAccessSubdiv.DeleteCommand.BindByName = true;
            odaAccessSubdiv.DeleteCommand.Parameters.Add("p_ACCESS_SUBDIV_ID", OracleDbType.Decimal, 0, "ACCESS_SUBDIV_ID").Direction = ParameterDirection.InputOutput;


            odaDBRoles = new OracleDataAdapter(@"select UPPER(:p_user_name) USER_NAME, GRANTEE, GRANTED_ROLE, LEVEL 
                                    from dba_role_privs
                                    start with grantee=upper(:p_user_name)
                                    connect by prior granted_role=grantee
                                    order by level", connect);
            odaDBRoles.SelectCommand.BindByName = true;
            odaDBRoles.SelectCommand.Parameters.Add("p_user_name", OracleDbType.Varchar2, null, ParameterDirection.Input);
            odaDBRoles.TableMappings.Add("Table", "ROLES");
        }

private  List<DBRole> _roles;
private List<DBAccessSubdiv> _accessSubdiv;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [OracleParameterMapping(ParameterName="p_user_name"), Column(Name="USERNAME")]
        public string UserName
        {
            get
            {
                return GetDataRowField<string>(() => UserName);
            }
            set
            {
                UpdateDataRow<string>(() => UserName, value);
            }
        }

        /// <summary>
        /// Табельный номер пользователя базы
        /// </summary>
        [OracleParameterMapping(ParameterName="p_per_num"),Column(Name="PER_NUM")]
        public string PerNum
        {
            get
            {
                return GetDataRowField<string>(() => PerNum);
            }
            set
            {
                UpdateDataRow<string>(() => PerNum, value);
            }
        }

        /// <summary>
        /// Подразделение сотрудника
        /// </summary>
        [Column(Name="CODE_SUBDIV")]
        public string CodeSubdiv
        {
            get
            {
                return GetDataRowField<string>(() => CodeSubdiv);
            }
            set
            {
                UpdateDataRow<string>(() => CodeSubdiv, value);
            }
        }


        /// <summary>
        /// ФИО работника
        /// </summary>
        [Column(Name="FIO")]
        public string FIO
        {
            get
            {
                return GetDataRowField<string>(() => FIO);
            }
            set
            {
                UpdateDataRow<string>(() => FIO, value);
            }
        }

        /// <summary>
        /// Должность работника
        /// </summary>
        [Column(Name="POS_NAME")]
        public string PosName
        {
            get
            {
                return GetDataRowField<string>(() => PosName);
            }
        }

        /// <summary>
        /// Фотографик работника
        /// </summary>
        [Column(Name="PHOTO")]
        public byte[] Photo
        {
            get
            {
                return GetDataRowField<byte[]>(()=>Photo);
            }
        }

        [Column(Name="SIGN_FIRED")]
        public string SignFired
        {
            get
            {
                return GetDataRowField<string>(() => SignFired);
            }
            set
            {
                UpdateDataRow<string>(() => SignFired, value);
            }
        }

        /// <summary>
        /// Истек ли срок действия пароля у пользователя
        /// </summary>
        public bool IsExpired
        {
            get
            {
                return GetDataRowField<decimal?>("IS_EXPIRED")==1;
            }
        }


        /// <summary>
        /// Заблокирован ли пользователь
        /// </summary>
        public bool IsLocked
        {
            get
            {
                return GetDataRowField<decimal?>("IS_LOCKED") == 1;
            }
        }

        /// <summary>
        /// Загружаем данные по доступу подразделений
        /// </summary>
        private void LoadUserAccess()
        {
            try
            {
                if (DataSet.Tables.Contains("ACCESS_SUBDIV"))
                {
                    DataRow[] rows = DataSet.Tables["ACCESS_SUBDIV"].Select(string.Format("USER_NAME='{0}'", this.UserName));
                    foreach (DataRow r in rows)
                        r.Delete();
                    DataSet.Tables["ACCESS_SUBDIV"].AcceptChanges();
                }
                odaAccessSubdiv.SelectCommand.SetParameters(this);
                odaAccessSubdiv.Fill(DataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения списка доступа подразделений\n" + ex.Message, "Просмотр пользователей");
            }
        }

        /// <summary>
        /// Загружаем данные по доступным ролям
        /// </summary>
        private void LoadUserRoles()
        {
            try
            {
                if (DataSet.Tables.Contains("ROLES"))
                {
                    DataRow[] rows = DataSet.Tables["ROLES"].Select(string.Format("USER_NAME='{0}'", this.UserName));
                    foreach (DataRow r in rows)
                        r.Delete();
                    DataSet.Tables["ROLES"].AcceptChanges();
                }
                odaDBRoles.SelectCommand.SetParameters(this);
                odaDBRoles.Fill(DataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения списка доступных ролей\n" + ex.Message, "Просмотр пользователей");
            }
        }

        public void RefreshAccessSubdiv()
        {
            LoadUserAccess();
            UpdateAccessSubdiv();
            RaisePropertyChanged(() => AccessSubdiv);
        }

        /// <summary>
        /// Список ролей пользователя
        /// </summary>
        public List<DBRole> Roles
        {
            get
            {
                if (_roles == null)
                {
                    LoadUserRoles();
                    UpdateUserRoles();
                }
                return _roles;
            }
        }

        /// <summary>
        /// Дерево ролей пользователя
        /// </summary>
        public List<DBRole> RolesTree
        { 
            get
            {
                if (_roles == null)
                {
                    LoadUserRoles();
                    UpdateUserRoles();
                }
                return _roles.Where(r => r.Level == 1).ToList(); // чтобы построит дерево ролей пользователя достаточно просто выбрать все корни по ролям
            }
        }

        /// <summary>
        /// Список доступных подразделений для сотрудника
        /// </summary>
        public List<DBAccessSubdiv> AccessSubdiv
        {
            get
            {
                if (_accessSubdiv == null)
                {
                    LoadUserAccess();
                    UpdateAccessSubdiv();
                }
                return _accessSubdiv;
            }

        }

        private void UpdateAccessSubdiv()
        {
            _accessSubdiv = DataSet.Tables["ACCESS_SUBDIV"].Select(string.Format("USER_NAME='{0}'", this.UserName))
                .Select(r => new DBAccessSubdiv(r)).OrderBy(r=>r.SubdivID).ToList();
        }

        private void UpdateUserRoles()
        {
            _roles = DataSet.Tables["ROLES"].Select(string.Format("USER_NAME='{0}'", UserName)).Select(r =>
                        new DBRole(this)
                        {
                            DBRoleName = r.Field2<string>("GRANTED_ROLE"),
                            Level = r.Field2<Decimal?>("LEVEL"),
                            Grantee = r.Field2<string>("GRANTEE")
                        }).ToList();
        }

        /// <summary>
        /// Добавляем новое подразделение в доступ подразделений
        /// </summary>
        internal void AddAccessSubdiv()
        {
            DataRow r= DataSet.Tables["ACCESS_SUBDIV"].NewRow();
            r["USER_NAME"] = this.UserName;
            DataSet.Tables["ACCESS_SUBDIV"].Rows.Add(r);
            UpdateAccessSubdiv();
            RaisePropertyChanged(() => AccessSubdiv);
            RaisePropertyChanged(() => HasChanges);
        }

        public void DeleteAccessSubdiv(DBAccessSubdiv ac)
        {
            ac.DataRow.Delete();
            UpdateAccessSubdiv();
            RaisePropertyChanged(() => AccessSubdiv);
            RaisePropertyChanged(() => HasChanges);
        }

        public bool SaveAccessSubdiv()
        {
            OracleTransaction tr = connect.BeginTransaction();
            try
            {
                odaAccessSubdiv.Update(DataSet.Tables["ACCESS_SUBDIV"]);
                tr.Commit();
                RaisePropertyChanged(() => HasChanges);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка сохранения для пользователя {0}\n ", DataSet.Tables["ACCESS_SUBDIV"].Rows.OfType<DataRow>()
                    .Where(r=>r.RowState!= DataRowState.Detached && !string.IsNullOrEmpty(r.RowError)).Select(r=>r.RowError).FirstOrDefault())+ ex.Message, "Ошибка сохранения");
                RaisePropertyChanged(() => HasChanges);
                return false;
            }

        }

        /// <summary>
        /// Есть ли изменения среди доступа подразделений
        /// </summary>
        public bool AccessSubdivHasChanges 
        {
            get
            {
                return AccessSubdiv.Any(r => r.DataTable.GetChanges() != null);
            }
        }

        /// <summary>
        /// Есть ли изменения в этом пользователе
        /// </summary>
        public bool HasChanges
        {
            get
            {
                return AccessSubdivHasChanges;
            }
        }

        /// <summary>
        /// Блокировка пользователя
        /// </summary>
        /// <returns></returns>
        public Exception  Lock()
        {
            try
            {
                new OracleCommand(string.Format("alter user {0} account lock", this.UserName), connect).ExecuteNonQuery();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        /// <summary>
        /// Разблокировка пользователя
        /// </summary>
        /// <returns></returns>
        public Exception Unlock()
        {
            try
            {
                new OracleCommand(string.Format("alter user {0} account unlock", this.UserName), connect).ExecuteNonQuery();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        /// <summary>
        /// Сброс пароля пользователя
        /// </summary>
        /// <returns></returns>
        public Exception  ResetPass()
        {
            try
            {
                new OracleCommand(string.Format("alter user {0} identified by \"{1}\" password expire", this.UserName, this.PerNum??"1"), connect).ExecuteNonQuery();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }

    /// <summary>
    /// Класс управления ролью пользователя
    /// </summary>
    public class DBRole : NotificationObject
    {
        private DBUser _owner;
        private string _dbName;
        private decimal? _level=1;
        private string _grantee;

        public DBRole()
        { 
        }

        public DBRole(DBUser owner)
        {
            _owner = owner;
        }

        /// <summary>
        /// Владелец роли - обязательное свойство
        /// </summary>
        public DBUser Owner
        {
            get
            {
                return _owner;
            }
        }

        /// <summary>
        /// Название роли в базе данных
        /// </summary>
        public string DBRoleName
        {
            get
            {
                return _dbName;
            }
            set
            {
                _dbName = value;
            }
        }

        /// <summary>
        /// Описание/комментарий роли
        /// </summary>
        public string DescriptionRole
        {
            get
            {
                return UsersKadrExplicit.GetRoleDescription(DBRoleName);
            }
        }

        /// <summary>
        /// Уровень вложенности роли
        /// </summary>
        public decimal? Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }

        /// <summary>
        /// Вложенные роли в текущую роль
        /// </summary>
        public List<DBRole> ChildRoles
        {
            get
            {
                if (Owner != null)
                {
                    return Owner.Roles.Where(r => (r.Level == this.Level + 1) && r.Grantee==this.DBRoleName).OrderBy(r=>r.DBRoleName).ToList();
                }
                else return null;
            }
        }

        /// <summary>
        /// От какой роли грантована эта роль
        /// </summary>
        public string Grantee
        {
            get
            {
                return _grantee;
            }
            set
            {
                _grantee = value;
                RaisePropertyChanged(() => Grantee);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Table(Name = "ACCESS_SUBDIV"), SchemaName("APSTAFF")]
    public class DBAccessSubdiv : RowEntityBase
    { 
        public DBAccessSubdiv(DataRow r)
        {
            DataRow = r;
        }

#region Properties

        [Column(Name="APP_NAME")]
        public string AppName
        {
            get
            {
                return this.GetDataRowField<string>(() => AppName);
            }
            set
            {
                UpdateDataRow<string>(()=>AppName, value);
            }
        }

        /// <summary>
        /// Подразделение доступа
        /// </summary>
        [Column(Name="SUBDIV_ID")]
        public decimal? SubdivID
        {
            get
            {
                return GetDataRowField<decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<decimal?>(() => SubdivID, value);
            }
        }
        /// <summary>
        /// Дата начала доступа
        /// </summary>
        [Column(Name = "DATE_START_ACCESS")]
        public DateTime? DateStartAccess
        {
            get
            {
                return GetDataRowField<DateTime?>(() => DateStartAccess);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateStartAccess, value);
            }
        }

        /// <summary>
        /// Подразделение доступа
        /// </summary>
        [Column(Name = "DATE_END_ACCESS")]
        public DateTime? DateEndAccess
        {
            get
            {
                return GetDataRowField<DateTime?>(() => DateEndAccess);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateEndAccess, value);
            }
        }

        [Column(Name="ACCESS_SUBDIV_ID", IsPrimaryKey=true)]
        public decimal? AccessSubdivID
        {
            get
            {
                return GetDataRowField<decimal?>(() => AccessSubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AccessSubdivID, value);
            }
        }

#endregion
    }

    public class ApplicationAccess:RowEntityBase
    {
        /// <summary>
        /// русское название приложения для тупых
        /// </summary>
        [Column(Name="ALIAS")]
        public string ApplicationAlias
        { 
            get
            {
                return GetDataRowField<string>("ALIAS");
            }
        }

        /// <summary>
        /// Название доступа приложения в базе данных
        /// </summary>
        [Column(Name="APP_NAME")]
        public string AppName
        { 
            get
            {
                return GetDataRowField<string>(()=>AppName);
            }
        }

        /// <summary>
        /// Айдишник для сортировки
        /// </summary>
        [Column(Name="ID")]
        public decimal? ID
        { 
            get
            {
                return GetDataRowField<decimal?>(() => ID);
            }
        }
    }
}
