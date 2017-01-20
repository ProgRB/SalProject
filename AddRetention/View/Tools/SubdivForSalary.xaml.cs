using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Oracle.DataAccess.Client;
using System.Data;
using LibrarySalary.Helpers;
using Salary.Helpers;
using EntityGenerator;
using System.Collections;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for SubdivForSalary.xaml
    /// </summary>
    public partial class SubdivForSalary : UserControl
    {
        
        private SubdivForCloseViewModel _model;
        public SubdivForSalary()
        {
            _model = new SubdivForCloseViewModel();
            InitializeComponent();
            DataContext = Model;
        }

        public SubdivForCloseViewModel Model
        {
            get
            {
                return _model;
            }
        }

        private void AddSubdiv_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddSubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByList f = new FilterByList(AppDictionaries.SubdivIDToValue.Values.OrderBy(r => new Tuple<string, DateTime?>(r.CodeSubdiv, r.SubDateStart)),
                new DataGridColumn[] 
                { new DataGridTextColumn(){ Binding = new Binding("CodeSubdiv"), Header="Подразделение", Width=80},
                        new DataGridTextColumn(){ Binding = new Binding("SubdivName"), Header="Наименование", Width=200},
                        new DataGridCheckBoxColumn(){ Binding = new Binding("SubActualSign"), Header="Актуально", Width=50},
                        new DataGridTextColumn(){ Binding = new Binding("SubDateStart"){StringFormat="{0:dd.MM.yyyy}"}, Header="Дата открытия"},
                        new DataGridTextColumn(){ Binding = new Binding("SubDateEnd"){StringFormat="{0:dd.MM.yyyy}"}, Header="Дата закрытия"}
                }
            );
            f.Owner= Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.InsertSubdiv(f.SelectedRows.OfType<Subdiv>());
            }
        }

        private void DelSubdiv_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && Model!=null && Model.SelectedSubdiv != null;
        }

        private void DeleteSubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить подразделение из списка закрываемых?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                Model.DeleteCurrentSubdiv();
            }
        }
        
        private void Refresh_click(object sender, RoutedEventArgs e)
        {
            Model.LoadSubdivList();
        }

        
        private void SaveCloseSubdiv_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && Model != null && Model.HasChanges;
        }

        /// <summary>
        /// Сохраняем только изменения по нужным подразделениям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCloseSubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Save();
        }
   
        /// <summary>
        /// Доступно ли закрытие всех подразделений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAllSubdiv_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        /// <summary>
        /// Закрываем все подразделения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAllSubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.SaveAllToCurrentMonth();
        }

    }

    public class SubdivForCloseViewModel: NotificationObject 
    {
        public static DataSet ds;
        OracleDataAdapter odaSaveClose;
        public SubdivForCloseViewModel()
        {
            ds = new DataSet();
            ds.Tables.Add(AppDataSet.Tables["SUBDIV"].Copy());

            odaSaveClose = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectSubdivForSalary.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSaveClose.SelectCommand.BindByName = true;
            odaSaveClose.SelectCommand.Parameters.Add("p_app_name", OracleDbType.Varchar2, "SALARY", ParameterDirection.Input);
            odaSaveClose.TableMappings.Add("Table", "SUBDIV_FOR_CLOSE");

            ds.Tables.Add("SUBDIV_FOR_CLOSE");

            odaSaveClose.InsertCommand = new OracleCommand(string.Format(@"begin {1}.SUBDIV_FOR_CLOSE_UPDATE(:p_SUBDIV_FOR_CLOSE_ID, :p_SUBDIV_ID, 
                                                :p_DATE_CLOSING, :p_DATE_CHANGE, :p_APP_NAME, :p_LAST_DATE_PROCESSING); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSaveClose.InsertCommand.BindByName = true;
            odaSaveClose.InsertCommand.Parameters.Add("p_SUBDIV_FOR_CLOSE_ID", OracleDbType.Decimal, 0, "SUBDIV_FOR_CLOSE_ID").Direction = ParameterDirection.InputOutput;
            odaSaveClose.InsertCommand.Parameters["p_SUBDIV_FOR_CLOSE_ID"].DbType = DbType.Decimal;
            odaSaveClose.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            odaSaveClose.InsertCommand.Parameters.Add("p_DATE_CLOSING", OracleDbType.Date, 0, "DATE_CLOSING");
            odaSaveClose.InsertCommand.Parameters.Add("p_DATE_CHANGE", OracleDbType.Date, 0, "DATE_CHANGE").Direction = ParameterDirection.Input;
            odaSaveClose.InsertCommand.Parameters.Add("p_APP_NAME", OracleDbType.Varchar2, AppName, ParameterDirection.Input);
            odaSaveClose.InsertCommand.Parameters.Add("p_LAST_DATE_PROCESSING", OracleDbType.Date, 0, "LAST_DATE_PROCESSING");
            odaSaveClose.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

            odaSaveClose.UpdateCommand = new OracleCommand(string.Format(@"begin {1}.SUBDIV_FOR_CLOSE_UPDATE(:p_SUBDIV_FOR_CLOSE_ID, :p_SUBDIV_ID, 
                                                :p_DATE_CLOSING, :p_DATE_CHANGE, :p_APP_NAME, :p_LAST_DATE_PROCESSING); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSaveClose.UpdateCommand.BindByName = true;
            odaSaveClose.UpdateCommand.Parameters.Add("p_SUBDIV_FOR_CLOSE_ID", OracleDbType.Decimal, 0, "SUBDIV_FOR_CLOSE_ID").Direction = ParameterDirection.InputOutput;
            odaSaveClose.UpdateCommand.Parameters["p_SUBDIV_FOR_CLOSE_ID"].DbType = DbType.Decimal;
            odaSaveClose.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            odaSaveClose.UpdateCommand.Parameters.Add("p_DATE_CLOSING", OracleDbType.Date, 0, "DATE_CLOSING");
            odaSaveClose.UpdateCommand.Parameters.Add("p_DATE_CHANGE", OracleDbType.Date, 0, "DATE_CHANGE").Direction = ParameterDirection.Input;
            odaSaveClose.UpdateCommand.Parameters.Add("p_APP_NAME", OracleDbType.Varchar2, AppName, ParameterDirection.Input);
            odaSaveClose.UpdateCommand.Parameters.Add("p_LAST_DATE_PROCESSING", OracleDbType.Date, 0, "LAST_DATE_PROCESSING");
            odaSaveClose.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

            odaSaveClose.DeleteCommand = new OracleCommand(string.Format(@"begin {1}.SUBDIV_FOR_CLOSE_DELETE(:p_SUBDIV_FOR_CLOSE_ID); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSaveClose.DeleteCommand.BindByName = true;
            odaSaveClose.DeleteCommand.Parameters.Add("p_SUBDIV_FOR_CLOSE_ID", OracleDbType.Decimal, 0, "SUBDIV_FOR_CLOSE_ID");

            odaSaveClose.RowUpdated += new OracleRowUpdatedEventHandler(odaSaveClose_RowUpdated);

            AppName = AppNameSource[0].AppName;
            LoadSubdivList();

            App.CloseNotification.SubdivClosed += CloseNotification_SubdivClosed;
        }

        /// <summary>
        /// Если пришло обновление. то не важно какое подразделение или приложение - обновляем таблицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="app_name"></param>
        /// <param name="closedSubdiv"></param>
        void CloseNotification_SubdivClosed(object sender, CloseNotification.AppNames app_name, IEnumerable<SubdivForClose> closedSubdiv)
        {
            LoadSubdivList();
        }


        void odaSaveClose_RowUpdated(object sender, OracleRowUpdatedEventArgs e)
        {
            if (e.Errors is OracleException && (e.Errors as OracleException).Number == 20225)
            {
                //e.Status = UpdateStatus.SkipCurrentRow;
                e.Row.RejectChanges();
            }
        }

        /// <summary>
        /// Выбранная дата для закрытия
        /// </summary>
        public DateTime? SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                selectedDate = value;
                RaisePropertyChanged(() => SelectedDate);
            }
        }

        /// <summary>
        /// используемая область закрытия данных
        /// </summary>
        [OracleParameterMapping(ParameterName="p_app_name")]
        public string AppName
        {
            get
            {
                return _appName;
            }
            set
            {
                if (_appName != value)
                {
                    if (!HasChanges || MessageBox.Show("Имеются несохраненные изменения, которые будут утрачены. Продолжить операцию?", "Потеря изменений", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        _appName = value;
                        odaSaveClose.InsertCommand.Parameters["P_APP_NAME"].Value = odaSaveClose.UpdateCommand.Parameters["p_APP_NAME"].Value = value;
                        LoadSubdivList();
                    }
                }
                RaisePropertyChanged(() => AppName);
            }
        }

        /// <summary>
        /// Количество закрытых подразделений
        /// </summary>
        public int ClosedCount
        {
            get
            {
                return SubdivForCloseSource.Count(r => r.DateClosing.Value.Trunc("Month") >= SelectedDate.Value.Trunc("Month"));
            }
        }

        /// <summary>
        /// Список закрываемых приложений
        /// </summary>
        public List<AppNameModel> AppNameSource
        {
            get
            {
                return AppXmlHelper.GetElements("ClosedApp").Select(r => new AppNameModel { AppName = r.Attribute("Code").Value, AppComments = r.Attribute("Description").Value }).ToList();
                //return new string[][] { new string[] { "SALARY", "Расчет зарплаты" }, new string[] { "PIECE_WORK", "Расчет нарядов" } }.Select(r => new AppNameModel { AppName = r[0], AppComments = r[1] }).ToList();
            }
        }


        DataView _subdivForClose;
        private string _appName;
        private DateTime? selectedDate = DateTime.Today.Day>25? DateTime.Today.Trunc("Month"):DateTime.Today.AddMonths(-1).Trunc("Month");
        private SubdivForClose _selectedSubdiv;

        /// <summary>
        /// Источник данных для подразделений 
        /// </summary>
        public List<SubdivForClose> SubdivForCloseSource
        {
            get
            {
                if (_subdivForClose == null && ds != null && ds.Tables.Contains("SUBDIV_FOR_CLOSE"))
                {
                    _subdivForClose = new DataView(ds.Tables["SUBDIV_FOR_CLOSE"], "", "", DataViewRowState.CurrentRows);
                }
                return _subdivForClose.OfType<DataRowView>().Select(r=>new SubdivForClose(){DataRow = r.Row}).OrderBy(r=>r.Subdiv.CodeSubdiv).ToList();
            }
        }

        /// <summary>
        /// Выбранное подразделение в списке
        /// </summary>
        public SubdivForClose SelectedSubdiv
        {
            get
            {
                return _selectedSubdiv;
            }
            set
            {
                _selectedSubdiv = value;
                RaisePropertyChanged(() => SelectedSubdiv);
            }
        }

        /// <summary>
        /// Загрузка данных по закрытиям подразделений
        /// </summary>
        public void LoadSubdivList()
        {
            if (ds.Tables.Contains("SUBDIV_FOR_CLOSE"))
                ds.Tables["SUBDIV_FOR_CLOSE"].Rows.Clear();
            odaSaveClose.SelectCommand.SetParameters(this);
            try
            {
                odaSaveClose.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
            }
            RaisePropertyChanged(() => SubdivForCloseSource);
            RaisePropertyChanged(() => ClosedCount);
        }

        /// <summary>
        /// Удаляет текущую выбранну запись подразделения
        /// </summary>
        public void DeleteCurrentSubdiv()
        {
            if (SelectedSubdiv != null)
                SelectedSubdiv.DataRow.Delete();
            RaisePropertyChanged(() => SubdivForCloseSource);
        }

        /// <summary>
        /// Добавляет подраздленеие в список закрываемых если его нет в этом списке
        /// </summary>
        /// <param name="item"></param>
        public void AddSubdiv(Subdiv item)
        {
            DataRow r = ds.Tables["SUBDIV_FOR_CLOSE"].NewRow();
            r["SUBDIV_ID"] = item.SubdivID;
            r["APP_NAME"] = this.AppName;
            r["DATE_CLOSING"] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1).AddSeconds(-1);
            r["DATE_CHANGE"] = DateTime.Now;
            r["LAST_DATE_PROCESSING"] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1).AddSeconds(-1);
            ds.Tables["SUBDIV_FOR_CLOSE"].Rows.Add(r);
        }

        public void SaveAllToCurrentMonth()
        {
            foreach (var p in SubdivForCloseSource)
                p.DateClosing = SelectedDate;
            Save();
        }

        internal void Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            odaSaveClose.InsertCommand.Parameters["P_APP_NAME"].Value = odaSaveClose.UpdateCommand.Parameters["P_APP_NAME"].Value = AppName;
            try
            {
                odaSaveClose.Update(ds.Tables["SUBDIV_FOR_CLOSE"], "SUBDIV_FOR_CLOSE_ID");
                tr.Commit();
                RaisePropertyChanged(() => ClosedCount);
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
            }
        }

        public bool HasChanges
        {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }

        internal void InsertSubdiv(IEnumerable<Subdiv> subdivs)
        {
            HashSet<decimal?> exist_sub = new HashSet<decimal?>(SubdivForCloseSource.Select(r=>r.SubdivID));
            foreach (var k in subdivs.Where(r=>!exist_sub.Contains(r.SubdivID)))
            {
                AddSubdiv(k);
            }
            RaisePropertyChanged(() => SubdivForCloseSource);
        }
    }

    /// <summary>
    /// Класс представления данных по закрываемым приложениям
    /// </summary>
    public class AppNameModel
    {
        public string AppName { get; set; }
        public string AppComments { get; set; }
    }
    
    public class CurrentCloseSubdivStateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!Array.TrueForAll(values, t=>t!=null && t!=DBNull.Value && t!=DependencyProperty.UnsetValue))
                return null;
            return (string.CompareOrdinal(((DateTime)values[0]).ToString("yyyyMM"), ((DateTime)values[1]).ToString("yyyyMM")) < 0 ? 
                 string.CompareOrdinal(((DateTime)values[2]).ToString("yyyyMM"), ((DateTime)values[1]).ToString("yyyyMM")) < 0 ? Brushes.LightCoral
                 : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF5F05B")) : Brushes.LimeGreen);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CurrentDateProcessEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value != null && value!=DependencyProperty.UnsetValue && AppDataSet.Tables["ACCESS_SUBDIV"].Select(string.Format("APP_NAME='SALARY' and SUBDIV_ID={0}", value)).Length > 0);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
