using LibrarySalary.Helpers;
using Oracle.DataAccess.Client;
using Salary.ViewModel;
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
using Salary.Helpers;
using System.Data;
using Salary.Reports;
using Microsoft.Reporting.WinForms;

namespace Salary.View.Details
{
    /// <summary>
    /// Логика взаимодействия для BrigageView.xaml
    /// </summary>
    public partial class TableBrigageView : UserControl
    {
        private TableBrigageViewModel _model;
        public TableBrigageView()
        {
            _model = new TableBrigageViewModel();
            InitializeComponent();
            this.DataContext = Model;
            HashSet<decimal?> closeSubdivs = new HashSet<decimal?>(AppDataSet.Tables["SUBDIV_FOR_CLOSE"].Select("APP_NAME='PIECE_WORK'").Select(r=>r.Field2<Decimal?>("SUBDIV_ID")));
            subdivSelector.ExtendedSubdivFilter =
                (r => r!=null && closeSubdivs.Contains(r.SubdivID));
        }

        /// <summary>
        /// Модель представления данных
        /// </summary>
        public TableBrigageViewModel Model
        {
            get
            {
                return _model;
            }
        }

        /// <summary>
        /// Обновляем список бригады
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshTable_Click(object sender, RoutedEventArgs e)
        {
            if (!Model.HasChanges || MessageBox.Show(Window.GetWindow(this),
                    "В табеле бригады имеются несохраненные изменения. После обновления все изменения будут потеряны. Продолжить обновление?", 
                    "Возможная потеря данных",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question)== MessageBoxResult.Yes)
                Model.RefreshTableBrigage();
        }

        /// <summary>
        /// Обновляем список бригады
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshPieceWork_Click(object sender, RoutedEventArgs e)
        {
            Model.RefreshPieceWork();
        }

        private void AddTableBrigage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model!=null && Model.CurrentBrigageID != null;
        }

        private void AddTableBrigage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddTableBrigage();
        }

        private void DeleteTableBrigage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.CurrentTableBrigage != null;
        }

        private void DeleteTableBrigage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.DeleteTableBrigage();
        }

        private void SaveTableBrigage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.HasChanges;
        }

        /// <summary>
        /// Сохранение табеля бригады
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveTableBrigage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Exception ex = Model.Save();
            if (ex != null)
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения табеля бригады");
            else
            {
                if (Model.UpdateAfterSave)
                    Model.RefreshTableBrigage();
            }

        }

        /// <summary>
        /// Печать табеля бригады
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepTableBrigage(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Detail/Rep_TableBrigage.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_brigage_id", OracleDbType.Decimal, Model.CurrentBrigageID, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", oda,
                oda.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Табель бригады", @"Detail/Rep_TableBrigage.rdlc", 
                        new SubReport[] { new SubReport("Subreport1", @"Detail/Rep_TableBrigageCalendar.rdlc", (pw.Result as DataSet).Tables[1])},
                        (pw.Result as DataSet).Tables[0],
                        new ReportParameter[] 
                            {
                                new ReportParameter("P_DATE", Model.SelectedDate.ToShortDateString())
                            });
                });

        }

        /// <summary>
        /// Выполняем автоматическое заполнение табеля бригады
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateTableBrigage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(Window.GetWindow(this),
                 "Табель бригады будет заполнен автоматически на основе данных \"Учет рабочего времени\"." +
                    string.Format("Текущие данные за {0:MMMM yyyy} по бригаде {1} будут удалены. Продолжить операцию?", Model.SelectedDate, Model.CurrentBrigage.BrigageCode),
                "Формирование табеля бригады", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование табеля бригады",
                    (p, pw) =>
                    {
                        Model.FormAutoTableBrigage();
                    }, null, null,
                        (p, pw) =>
                        {
                            if (pw.Cancelled) return;
                            if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования табеля бригады");
                            else
                                Model.RefreshTableBrigage();
                        });
            }
        }

        /// <summary>
        /// Отчет сводка по КТУ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepTableBrigage2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Detail/Rep_TableBrigage2.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_brigage_id", OracleDbType.Decimal, Model.CurrentBrigageID, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", oda,
                oda.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Табель бригады", @"Detail/Rep_TableBrigage2.rdlc",
                        (pw.Result as DataSet).Tables[0],
                        new ReportParameter[] 
                            {
                                new ReportParameter("P_DATE", Model.SelectedDate.ToShortDateString())
                            });
                });
        }

        private void BrigageDict_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void BrigageDictionary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow main = Window.GetWindow(this) as MainWindow;
            if (main != null)
            {
                main.OpenTabs.AddNewTab("Справочник бригад", new BrigageEditor());
            }
        }

    }
}
