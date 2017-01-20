using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Salary.View;
using LibrarySalary.Helpers;
using Oracle.DataAccess.Client;
using System.Windows;
using System.ComponentModel;
using Hardcodet.Wpf.TaskbarNotification;
using EntityGenerator;
using Salary.View.Tools;
using System.Diagnostics;

namespace Salary.Helpers
{
    public class CloseNotification
    {
        private DataSet ds;
        public static MTObservableCollection<AppNotify> listNotify;
        private static bool _isEnabled = false;

        private static Dictionary<string, string> AppDescriptions;

        /// <summary>
        /// Активно ли оповещение.
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    if (value)
                        RegisterNotification();
                    else
                        RemoveNotification();
                    OnPropertyChanged("IsEnabled");
                }
            }
        }
        private void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
                PropertyChanged(null, new PropertyChangedEventArgs(Name));
        }

        public OracleDependency subdivCloseDependecy;

        /// <summary>
        /// Доступно ли оповещение для роли
        /// </summary>
        private bool NotificationAvailable
        {
            get
            {
                return GrantedRoles.CheckRole("SALARY_VIEW") || GrantedRoles.CheckRole("SALARY_ECON_VIEW_EASY") || GrantedRoles.CheckRole("SALARY_PIECE_VIEW");
            }
        }

        /// <summary>
        /// Конструктор оповещения
        /// </summary>
        public CloseNotification()
        {
            try
            {
                if (!NotificationAvailable)
                    return;
                ds = new DataSet();
                ds.Tables.Add(AppDataSet.Tables["SUBDIV"].Copy());
                listNotify = new MTObservableCollection<AppNotify>();
                mainIcon = (TaskbarIcon) App.Current.FindResource("AppMainNotifyIcon");

                mainIcon.TrayBalloonTipClicked +=mainIcon_TrayBalloonTipClicked; //+= new EventHandler(appTrayIcon_BalloonTipClicked);
                mainIcon.TrayMouseDoubleClick += mainIcon_TrayMouseDoubleClick;  //+= new EventHandler(appTrayIcon_DoubleClick);
                         
                new OracleDataAdapter(string.Format("select *  from {1}.subdiv_for_close", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect).Fill(ds, "SUBDIV_FOR_CLOSE");
                ds.Tables["SUBDIV_FOR_CLOSE"].PrimaryKey = new DataColumn[] { ds.Tables["SUBDIV_FOR_CLOSE"].Columns["SUBDIV_FOR_CLOSE_ID"] };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            };
        }

        /// <summary>
        /// Показываем всплывающее сообщение для приложения
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="duration"></param>
        public void ShowNotification(string title, string content, int duration = 5000,  BalloonIcon icon = BalloonIcon.Info)
        {
            App.Current.Dispatcher.BeginInvoke(
                new Action(() => ShowNotify(title, content, duration, icon)), null);
        }

        private void ShowNotify(string title, string content, int duration, BalloonIcon icon)
        {
            MainIcon.ShowCustomBalloon(new CustomTooltip(new CustomTooltipModel() { NotificationHeader = title, NotificationContent = content }), System.Windows.Controls.Primitives.PopupAnimation.Slide, duration);
        }

        private TaskbarIcon mainIcon;

        /// <summary>
        /// Главная иконка программы
        /// </summary>
        public TaskbarIcon MainIcon
        {
            get
            {
                if (mainIcon==null)
                    mainIcon = (TaskbarIcon)App.Current.FindResource("AppMainNotifyIcon");
                return mainIcon;
            }
        }

        void mainIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            mainIcon_TrayBalloonTipClicked(null, null);
        }

        /// <summary>
        /// Что происходит при двойном нажатии на иконку или клик на подсказку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainIcon_TrayBalloonTipClicked(object sender, RoutedEventArgs e)
        {
            if (NotifyForm == null || !NotifyForm.IsLoaded)
            {
                Salary.View.NotifyList f = new Salary.View.NotifyList(listNotify);
                NotifyForm = f;
                NotifyForm.Show();
                NotifyForm.Activate();
            }
            else
            {
                NotifyForm.Show();
                NotifyForm.Activate();
            }
        }

        /// <summary>
        /// Регистрация нового события
        /// </summary>
        public void RegisterNotification()
        {
            if (!NotificationAvailable)
                return;
            try
            {
                OracleCommand cmd = new OracleCommand(string.Format(@"select subdiv_for_close_id, LAST_DATE_PROCESSING from {1}.subdiv_for_close",
                            Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd.CommandType = CommandType.Text;
                subdivCloseDependecy = new OracleDependency(cmd);
                subdivCloseDependecy.OnChange += new OnChangeEventHandler(d_OnChange);
                subdivCloseDependecy.QueryBasedNotification = true;
                cmd.Notification.IsNotifiedOnce = false;
                cmd.Notification.Timeout = 3600;
                cmd.AddRowid = true;
                cmd.ExecuteNonQuery();
                AppDataSet.UpdateSet("SUBDIV_FOR_CLOSE");
            }
            catch (Exception ex)
            { 
                
            }

            try// Загрузим список приложений для пояснения что было закрыто из файла SalaryXmlData.xml
            {
                AppDescriptions = AppXmlHelper.GetElements("ClosedApp").Select(r => new { Code = r.Attribute("Code").Value, Description = r.Attribute("Description").Value })
                    .ToDictionary(r=>r.Code, r=>r.Description);
            }
            catch 
            {
                AppDescriptions = new Dictionary<string, string>();
            };
            //OnCloseStateChanged();
        }

        /// <summary>
        /// Удаляем регистрацию оповещения
        /// </summary>
        public void RemoveNotification()
        {
            if (subdivCloseDependecy != null && Connect.CurConnect != null)
            {
                try
                {
                    subdivCloseDependecy.RemoveRegistration(Connect.CurConnect);
                }
                catch (Exception ex)
                { }
            }
        }

        /// <summary>
        /// Обработка выхода из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ExitMenu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрыть программу Зарплата Предприятия?", "Выход из программы", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void d_OnChange(object sender, OracleNotificationEventArgs eventArgs)
        {
            try
            {
                switch (eventArgs.Info)
                {
                    // Если сервер был выключен, то сообщаем об этом.
                    case OracleNotificationInfo.Shutdown: ShowNotification("Ошибка работы с сервером", "Сервер был временно отключен. Ведутся технические работы. Дождитесь возобновления работы сервера.", 15000, BalloonIcon.Error);
                        listNotify.Add(new AppNotify("Ошибка работы с сервером", "Сервер был временно отключен (технические работы или неполадки). Дождитесь возобновления работы сервера.")); 
                        break;
                    case OracleNotificationInfo.End:
                        RegisterNotification(); 
                        break;
                    case OracleNotificationInfo.Update:
                        string st1 = string.Join(",", eventArgs.Details.Rows.OfType<DataRow>().Select(p => "'" + p["ROWID"].ToString() + "'").ToArray());
                        OracleDataAdapter a = new OracleDataAdapter(string.Format("select s1.* from {1}.subdiv_for_close s1 join {0}.subdiv s2 on (s1.subdiv_id=s2.subdiv_id) where s1.rowid in ({2})", Connect.SchemaApstaff, Connect.SchemaSalary, st1), Connect.CurConnect);
                        if (ds.Tables.Contains("NOTIFY_CLOSE"))
                            ds.Tables["NOTIFY_CLOSE"].Rows.Clear();
                        a.Fill(ds, "NOTIFY_CLOSE");
                        DataTable t = ds.Tables["NOTIFY_CLOSE"];
                        if (!t.ParentRelations.Contains("fk1"))
                            t.ParentRelations.Add("fk1", ds.Tables["SUBDIV_FOR_CLOSE"].Columns["SUBDIV_FOR_CLOSE_ID"], t.Columns["SUBDIV_FOR_CLOSE_ID"], false);

#region Обработка подразделений - по какую дату обработано, не закрытия это - только для зарплаты пока что
                        DataRow[] res = t.Select("last_date_processing<>Parent(fk1).last_date_processing"); // проверяем какие записи были обработаны
                        if (res.Length > 0 && GrantedRoles.CheckRole("SALARY_EDIT")) // если роль редактирования доступна - только им показывать обработку подразделений
                        {
                            var data = from r in res
                                       group r by r.Field2<DateTime?>("LAST_DATE_PROCESSING") into g
                                       select new
                                       {
                                           g.Key,
                                           Joined = string.Join(", ", g.OrderBy(s => s["CODE_SUBDIV"]).Select(s => s["CODE_SUBDIV"]))
                                       };
                            string st = "";
                            foreach (var s in data)
                            {
                                string st2 = string.Format("По {0} обработаны подразделения: {1}\n", s.Key.Value.ToString("MMMM yyyy"), s.Joined);
                                listNotify.Add(new AppNotify("Обработка подразделений", st2));
                                st += st2;
                            }
                            if (st.Length > 0) st = st.Substring(0, st.Length - 1);
                            ShowNotification("Обработка подразделений", string.IsNullOrEmpty(st) ? "Не указаны подразделения" : st, 15000);
                        }
#endregion
                        
                        //res = t.Select("date_closing<>Parent(fk1).date_closing"); // ищем записи, которые были обновлены на сервере - изменилась дата закрытия.
                        var oldSubdivs = ds.Tables["SUBDIV_FOR_CLOSE"].ConvertToEntityList<SubdivForClose>(); 
                        var newSubdivs = ds.Tables["NOTIFY_CLOSE"].ConvertToEntityList<SubdivForClose>();
                        var changedSubdiv = (from old in oldSubdivs
                                  join nn in newSubdivs on old.SubdivForCloseID equals nn.SubdivForCloseID
                                  where old.DateClosing != nn.DateClosing
                                  select new { OldValue = old, NewValue = nn }).ToList();
                        if (changedSubdiv.Count > 0)
                        {
                            var data = from r in changedSubdiv
                                       group r by new
                                            {
                                                AppName = r.NewValue.AppName,
                                                DateClosing = r.NewValue.DateClosing,
                                                NewLessOld = r.NewValue.DateClosing < r.OldValue.DateClosing
                                            } into g
                                       select new // составляем список по датам какое подразделение было закрыто/открыто
                                       {
                                           AppName = GetAppEnum(g.Key.AppName),
                                           AppCode = g.Key.AppName,
                                           DateClosing = g.Key.DateClosing,
                                           NewLessOld = g.Key.NewLessOld,
                                           JoinedCode = string.Join(", ", g.OrderBy(s => s.NewValue.Subdiv.CodeSubdiv).Select(s => s.NewValue.Subdiv.CodeSubdiv)),
                                           Subdivs = g.Select(s=>s.NewValue)
                                       };
                            string st = "";
                            foreach (var s in data)
                            {
                                string st2 = string.Format("По {0} {1} раздел \"{3}\" подразделений: {2}\n", 
                                    s.DateClosing.Value.ToString("MMMM yyyy"), s.NewLessOld ? "открыт" : "закрыт", s.JoinedCode,
                                    AppDescriptions.ContainsKey(s.AppCode)? AppDescriptions[s.AppCode]: s.AppCode);
                                listNotify.Add(new AppNotify(string.Format("Закрытие/открытие подразделений. Раздел \"{0}\"", AppDescriptions.ContainsKey(s.AppCode) ? AppDescriptions[s.AppCode] : s.AppCode), st2));
                                st += st2;
                            }
                            if (st.Length > 0) st = st.Substring(0, st.Length - 1);
                                ShowNotification("Закрытие/открытие подразделений", string.IsNullOrEmpty(st) ? "Не указаны подразделения" : st, 15000); 
                            //mainIcon.ShowBalloonTip("Закрытие/открытие подразделений", string.IsNullOrEmpty(st) ? "Не указаны подразделения" : st, BalloonIcon.Info);
                            AppDataSet.UpdateSet("SUBDIV_FOR_CLOSE");
                            //после того как получили список измененных подразделений собираем список приложений и подразделений для обновления 
                            var changed_apps = (from r in data
                                          group r by r.AppName into g
                                          select new
                                              {
                                                  AppName = g.Key,
                                                  SubdivsClosed = g.SelectMany(s=>s.Subdivs)
                                              }).ToList();
                            
                            ///Здесь обновляем локальную таблицу закрытия
                            foreach (DataRow r in t.Rows)
                            {
                                r.GetParentRow("fk1")["last_date_processing"] = r["last_date_processing"];
                                r.GetParentRow("fk1")["date_closing"] = r["date_closing"];
                            }

                            // генерируем события, что для такого то закрытия изменились такие-то подразделения
                            foreach (var p in changed_apps)                
                            {
                                OnCloseStateChanged(p.AppName, p.SubdivsClosed);
                            }
                            
                        }
                        else
                        {
                            AppDataSet.UpdateSet("SUBDIV_FOR_CLOSE");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Debug.WriteLine("Error on show close Subdiv Notification:"+ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Форма показа сообщений системных. Используется будет для показа этих сообщений
        /// </summary>
        public NotifyList NotifyForm
        {
            get;
            set;
        }

        /// <summary>
        /// Указатель на событие вызывается только когда обновляются подразделения
        /// </summary>
        public event SubdivCloseChangedDelegate SubdivClosed;

        public delegate void SubdivCloseChangedDelegate(object sender, CloseNotification.AppNames app_name, IEnumerable<EntityGenerator.SubdivForClose> closedSubdiv);

        /// <summary>
        /// Генерируем событие, что закрытость изменилась.
        /// </summary>
        private void OnCloseStateChanged(string appName, IEnumerable<EntityGenerator.SubdivForClose> subdivs)
        {
            OnCloseStateChanged(GetAppEnum(appName), subdivs);
        }
        /// <summary>
        /// Генерируем событие, что закрытость изменилась.
        /// </summary>
        private void OnCloseStateChanged(AppNames appName, IEnumerable<EntityGenerator.SubdivForClose> subdivs)
        {
            if (SubdivClosed != null)
                SubdivClosed(this, appName, subdivs);
        }

        /// <summary>
        /// Получаем имя приложения из текстового поля
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        private AppNames GetAppEnum(string appName)
        {
            AppNames s =AppNames.Unknown;
            if (Enum.TryParse<AppNames>(appName.Replace("_", ""), true, out s))
                return s;
            else
                return AppNames.Unknown;
        }

        public enum AppNames
        { 
            Salary = 0,
            PieceWork = 1,
            Distribution=2,
            Unknown = 3
        }
    }
}
