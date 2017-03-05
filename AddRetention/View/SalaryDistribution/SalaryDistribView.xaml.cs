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
using Oracle.DataAccess.Client;
using LibrarySalary.Helpers;
using System.Data;
using Salary.Helpers;
using System.ComponentModel;
using Salary.Reports;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Collections;
using OfficeOpenXml;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for SalaryDistribView.xaml
    /// </summary>
    public partial class SalaryDistribView : UserControl
    {
        SalaryDistributionViewModel _model;
        public SalaryDistribView()
        {
            _model = new SalaryDistributionViewModel();
            InitializeComponent();
            DataContext = Model;
        }

        /// <summary>
        /// модел данных для просмотра
        /// </summary>
        public SalaryDistributionViewModel Model
        {
            get
            {
                return _model;
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        /// <summary>
        /// Основной свод распределения по заказам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_SalaryDistr1_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution/Rep_SalaryDistributionMain.sql"), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", a, a.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Свод по шифрам заказов по подразделению", "Rep_SalaryDistribMain.rdlc",
                        (pw.Result as DataSet).Tables[0],
                        new ReportParameter[] { new ReportParameter("P_DATE", Model.SelectedDate.Value.ToShortDateString()) });
                }
            );
        }

        /// <summary>
        /// Отчет по неосновным заказам (вкладыш)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_SalaryDistrSecond_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution/Rep_SalaryDistributionSecond.sql"), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", a, a.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Свод по шифрам заказов по подразделению", "Rep_SalaryDistribSecond.rdlc",
                        (pw.Result as DataSet).Tables[0],
                        new ReportParameter[] { new ReportParameter("P_DATE", Model.SelectedDate.Value.ToShortDateString()) });
                }
            );
        }

        /// <summary>
        /// Отчет по принятым затратам из подразделений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_SalaryDistrReceive_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution/Rep_SalaryDistributionReceive.sql"), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", a, a.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Свод по шифрам заказов по подразделению", "Rep_SalaryDistribReceive.rdlc",
                        (pw.Result as DataSet).Tables[0],
                        new ReportParameter[] { new ReportParameter("P_DATE", Model.SelectedDate.Value.ToShortDateString()) });
                }
            );
        }

        /// <summary>
        /// Отчет по распределению взносов на основные заказы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_SalaryDistrMainDues_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution/Rep_SalaryDistributionMainDues.sql"), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", a, a.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Свод по шифрам заказов по подразделению", "Rep_SalaryDistribMainDues.rdlc",
                        (pw.Result as DataSet).Tables[0],
                        new ReportParameter[] { new ReportParameter("P_DATE", Model.SelectedDate.Value.ToShortDateString()) });
                }
            );
        }

        /// <summary>
        /// Обработчик расчета ЗП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcSalaryFullDistribution_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(Window.GetWindow(this), string.Format("Рассчитать полностью распределение затрат по подразделению {0}  за {1:MMMM yyyy}?", Model.CodeSubdiv, Model.SelectedDate), 
                "Зарплата предприятия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Model.CalcSalaryDistribution(this);
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                bgFilterGroup.UpdateSources();
            }
        }

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            bgFilterGroup.UpdateSources();
        }

        private void ButtonFilterForce_Click(object sender, RoutedEventArgs e)
        {
            Model.UpdateCurrentTab();
        }

        /// <summary>
        /// Формирование отчета по ошибкам в распределении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_DistrControl_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution/Rep_DistrControl.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.SelectCommand.Parameters.Add("c5", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование протокола ошибок", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Проверка данных по заказам и распределения...", "Rep_DistrControl.rdlc",
                        (pw.Result as DataSet).Tables.OfType<DataTable>(), new ReportParameter[] { new ReportParameter("P_DATE", Model.SelectedDate.Value.ToShortDateString()) });
                });
        }

        /// <summary>
        /// А здесь идет расчет базы для распределения - это с учетом всех корректировок собираются наряды и ЗП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcBaseDistribution_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(Window.GetWindow(this), string.Format("Рассчитать базу распределение затрат по подразделению {0}  за {1:MMMM yyyy}?", Model.CodeSubdiv, Model.SelectedDate),
                "Зарплата предприятия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Model.CalcBaseDistribution(this);
            }
        }

        private void AddDistribReciveSubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryReceiveEditor f = new SalaryReceiveEditor(null, Model.SelectedDate);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateCurrentTab();
            }
        }

        private void EditReceive_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.CurrentSalReceiveSubdiv != null;
        }

        private void EditDistribReciveSubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryReceiveEditor f = new SalaryReceiveEditor(Model.CurrentSalReceiveSubdiv.Row.Field2<Decimal?>("SAL_SUBDIV_RECEIVE_ID"));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateCurrentTab();
            }
        }

        private void DeleteDistribReciveSubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись принятых-переданных затрат?", "Зарплата предприятия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Model.DeleteCurrentReceiveSal();
            }
        }

        /// <summary>
        /// Выгрузка данных в говнофайлы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadDopz26_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\UploadDOPZ26.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor,ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    UploadData((pw.Result as DataSet).Tables[0], string.Format(e.Parameter.ToString(), Model.SelectedDate.Value));
                });
        }

        /// <summary>
        /// А тут  запрашиваем файл и сохраняем туда данные
        /// </summary>
        /// <param name="t"></param>
        /// <param name="path"></param>
        private void UploadData(DataTable t, string path)
        { 
            System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
            sf.Filter="Текстовые файлы (*.txt)|*.txt";
            FileInfo fi = new FileInfo(path.Replace(@"h:\", @"\\fs02\PrgEcon\"));
            sf.InitialDirectory = fi.DirectoryName;
            sf.FileName = fi.Name;
            sf.OverwritePrompt = true;
            sf.RestoreDirectory = true;
            if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    File.WriteAllLines(sf.FileName, t.Rows.OfType<DataRow>().Select(r => r[0].ToString()), Encoding.GetEncoding(866));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException(), string.Format("Ошибка записи данных в файл {0}", sf.FileName));
                }
            }
        }

        private void UploadDopz8_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\UploadDOPZ8.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    UploadData((pw.Result as DataSet).Tables[0], string.Format(e.Parameter.ToString(), Model.SelectedDate.Value));
                });
        }

        /// <summary>
        /// Выгрузка 21 проводки в макете баланса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadMem21_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(string.Format("Выгрузить мемориальный ордер (21 проводка) по подразделению {0}  за {1:MMMM yyyy}", Model.CodeSubdiv, Model.SelectedDate),
                "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\UploadMem21.sql"), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        UploadData((pw.Result as DataSet).Tables[0], string.Format(e.Parameter.ToString(), Model.SelectedDate.Value));
                    });
            }
        }

        /// <summary>
        /// Выгрузка данных по базе распределения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadDopz_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(string.Format("Выгрузить файл Допз за {0:MMMM yyyy}", Model.SelectedDate),
                "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\UploadDOPZ.sql"), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        UploadData((pw.Result as DataSet).Tables[0], string.Format(e.Parameter.ToString(), Model.SelectedDate.Value));
                    });
            }
        }

        /// <summary>
        /// Выгрузка данных допз8 для ЭВМ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadDopz8_2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\UploadDOPZ8_2.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    UploadData((pw.Result as DataSet).Tables[0], string.Format(e.Parameter.ToString(), Model.SelectedDate.Value));
                });
        }

        /// <summary>
        /// ПРоверка доступности команды - если выбран текущая строчка корректировки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditCorrelation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.CurrentSalCorrAddition != null;
        }

        /// <summary>
        /// Обработчик команды добавления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCorrSalaryDistr_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRow r = null;
            if (Model.CurrentSalCorrAddition != null)
                r = Model.CurrentSalCorrAddition.Row;
            SalaryAddCorrelationEditor f = new SalaryAddCorrelationEditor(null, r);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateCurrentTab();
            }
        }

        /// <summary>
        /// Обработчик команды редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditCorrSalaryDistr_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryAddCorrelationEditor f = new SalaryAddCorrelationEditor(Model.CurrentSalCorrAddition.Row.Field2<Decimal?>("SALARY_ADD_CORRELATION_ID"));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateCurrentTab();
            }
        }

        /// <summary>
        /// Обработчик команды удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteCorrSalaryDistr_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(Window.GetWindow(this),"Удалить выбранную запись корректировки?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (Model.DeleteCurrentSalaryCorrelation())
                    Model.UpdateCurrentTab();
            }
        }

        /// <summary>
        /// Обработчик команды печати свода по заказам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintACPUDopzMain_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format("begin {1}.SALARY_TXT_REPORTS.SelectDistribMain(:p_subdiv_id, :p_date, :c);end;", Connect.SchemaApstaff, Connect.SchemaSalary),
                    Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета",
                (s, pw) =>
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    pw.Result = t;
                }, a, a.SelectCommand,
                    (p, s) =>
                    {
                        if (s.Cancelled) return;
                        else if (s.Error != null) MessageBox.Show(s.Error.GetFormattedException(), "Ошибка формирования отчета");
                        else
                        {
                            System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
                            sf.DefaultExt = "TXT";
                            sf.Filter = "Текстовые файлы (.txt)|*.txt";
                            sf.InitialDirectory = Connect.parameters["ReportDirInit"];
                            sf.FileName = "DOPZ_SVOD.TXT";
                            sf.OverwritePrompt = false;
                            if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
                            {
                                try
                                {
                                    if (!File.Exists(sf.FileName))
                                    {
                                        FileStream f = File.Create(sf.FileName);
                                        f.Close();
                                    }
                                    File.AppendAllLines(sf.FileName, (s.Result as DataTable).Rows.OfType<DataRow>().Select(w => w[0].ToString()), Encoding.GetEncoding(866));
                                    MessageBox.Show("Записи успешно добавлены в файл для печати!");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Ошибка записи в файл");
                                }
                            }
                        }
                    });
        }

        /// <summary>
        /// Обработчик команды печати свода по заказам взносов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintACPUDopzDues_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format("begin {1}.SALARY_TXT_REPORTS.SelectDistribMainDues(:p_subdiv_id, :p_date, :c);end;", Connect.SchemaApstaff, Connect.SchemaSalary),
                    Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета",
                (s, pw) =>
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    pw.Result = t;
                }, a, a.SelectCommand,
                    (p, s) =>
                    {
                        if (s.Cancelled) return;
                        else if (s.Error != null) MessageBox.Show(s.Error.GetFormattedException(), "Ошибка формирования отчета");
                        else
                        {
                            System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
                            sf.DefaultExt = "TXT";
                            sf.Filter = "Текстовые файлы (.txt)|*.txt";
                            sf.InitialDirectory = Connect.parameters["ReportDirInit"];
                            sf.FileName = "DOPZ_SVOD_VZNOS.TXT";
                            sf.OverwritePrompt = false;
                            if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
                            {
                                try
                                {
                                    if (!File.Exists(sf.FileName))
                                    {
                                        FileStream f = File.Create(sf.FileName);
                                        f.Close();
                                    }
                                    File.AppendAllLines(sf.FileName, (s.Result as DataTable).Rows.OfType<DataRow>().Select(w => w[0].ToString()), Encoding.GetEncoding(866));
                                    MessageBox.Show("Записи успешно добавлены в файл для печати!");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Ошибка записи в файл");
                                }
                            }
                        }
                    });
        }

        /// <summary>
        /// Главная страница произоводственного отчета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepDuesAvgHeadFundes_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Payment.Rep_DistributionSalaryHead(this, Model.SubdivID, Model.SelectedDate);
        }

        /// <summary>
        /// Обработчик замены заказов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplaceDistrBaseOrder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OrderReplacer f = new OrderReplacer(Model.SelectedDate.Value, Model.SubdivID, Model.CurrentSalaryAddition == null ? null : Model.CurrentSalaryAddition.Row.Field2<Decimal?>("ORDER_ID"));
            f.Owner = Window.GetWindow(this);
            f.ShowDialog();
            Model.UpdateCurrentTab();
        }

        /// <summary>
        /// Выгрузка данных для позаказки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadDop20_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // эту херню пусть пока что лиски и делает, раз делала. Файлик для этого она загрузит
        }

        /// <summary>
        /// Выгрузка данных по необлагаемым видам оплат с главной страницы производственного отчета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadDopzPril21_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\UploadPril21.sql"), Connect.CurConnect);
            // еще чуть чуть и эта ебата с русскими названиями файлов будет больше не нужна - а пока что оставлю это название так как легче запомнить куда выгружается
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    UploadData((pw.Result as DataSet).Tables[0], string.Format(e.Parameter.ToString(), Model.SelectedDate.Value));
                });
        }

        /// <summary>
        /// Отчет распределение резерва квартальной премии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_QuarterReservDistr_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\Rep_QuarterReserv.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Резерв кватальной премии", "Rep_QuarterReservDistr.rdlc", 
                        (pw.Result as DataSet).Tables[0], null);
                });
        }

        /// <summary>
        /// Распределение резерва мем ордер то же самое
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_QuarterReservMem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\Rep_QuarterReservMem.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Мемориальный ордер №198", "Rep_QuarterReservMem.rdlc",
                        (pw.Result as DataSet).Tables[0], 
                        new ReportParameter[] { new ReportParameter("P_DATE", Model.SelectedDate.Value.Trunc("Month").AddMonths(1).AddSeconds(-1).ToShortDateString()) });
                });
        }

        /// <summary>
        /// Отчет распределение резерва отпуска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_VacReservDistr_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\Rep_VacReserv.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Резерв кватальной премии", "Rep_VacReservDistr.rdlc",
                        (pw.Result as DataSet).Tables[0], null);
                });
        }

        /// <summary>
        /// Распределение резерва отпуска мем ордер то же самое
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_VacReservMem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\Rep_VacReservMem.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Мемориальный ордер №197", "Rep_VacReservMem.rdlc",
                        (pw.Result as DataSet).Tables[0], new ReportParameter[]{new ReportParameter("P_DATE", Model.SelectedDate.Value.Trunc("Month").AddMonths(1).AddSeconds(-1).ToShortDateString())});
                });
        }

        private void UploadMem197_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            UploadMems197198(new decimal[] { 19, 21 }, e);
        }

        /// <summary>
        /// Выгрузка данных по проводкам
        /// </summary>
        /// <param name="p"></param>
        private void UploadMems197198(decimal[] p, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\UploadMem197_198.sql"), Connect.CurConnect);
            // еще чуть чуть и эта ебата с русскими названиями файлов будет больше не нужна - а пока что оставлю это название так как легче запомнить куда выгружается
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, 0, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_code_distr_ids", OracleDbType.Array, p, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
            oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", oda, oda.SelectCommand,
                (pm, pw) =>
                {
                    UploadData((pw.Result as DataSet).Tables[0], string.Format(e.Parameter.ToString(), Model.SelectedDate.Value));
                });
        }

        private void UploadMem198_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            UploadMems197198(new decimal[] { 20, 23 }, e);
        }

        private void Print_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PrintDistrib_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета. Ожидайте...",
                   (s, pw) =>
                   {
                       DataTable t = Model.SalaryDistrSource.ToTable();
                       var real_template = "Rep_DistributionView.xlsx";
                       var report_templ = string.Format("распределение_отчет{0}.xlsx", DateTime.Now.Ticks%10);
                       string NewFileName = System.IO.Path.GetTempPath() + report_templ;
                       if (File.Exists(NewFileName))
                            File.Delete(NewFileName);

                       File.Copy(Connect.CurrentAppPath + @"\Reports\" + real_template, NewFileName);
                       using (ExcelPackage ep = new ExcelPackage(new FileInfo(NewFileName)))
                       {
                           ExcelWorkbook wb = ep.Workbook;
                           ExcelWorksheet ws = wb.Worksheets[1];
                           ws.Cells["A1"].Value = string.Format("Распределение затрат за {0:MMMM yyyy} по подразделению {1}", Model.SelectedDate, Model.CodeSubdiv)
                               + (string.IsNullOrEmpty(Model.OrderFilter)?"":" с фильтром заказа "+ Model.OrderFilter);
                           ws.Cells[5, 1].LoadFromDataTable(t, true, OfficeOpenXml.Table.TableStyles.Light1);
                           ep.Save();
                           pw.Result = NewFileName;
                       }
                   }, 
                   null, null,
                   (s, pw) =>
                   {
                       if (pw.Cancelled) return;
                       if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования данных");
                       else
                       {
                           System.Diagnostics.Process.Start(pw.Result.ToString());
                       }
                   });
        }

        private void PasteCustomDistr_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(Salary.ViewModel.AppCommands.SaveCustomDistribution.Name);
        }

        private void PasteCustomDist_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                List<object[]> data = ClipboardHelper.ParseClipboardXMLSpreadsheet();
                List<Tuple<string, decimal>> values = new List<Tuple<string, decimal>>();
                foreach (var p in data)
                { 
                    values.Add(new Tuple<string, decimal>(p[0]==null?"":p[0].ToString(), decimal.Parse(p[1]==null?"0":p[1].ToString())));
                }
                Model.AddCustomDistributionValues(values);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Window.GetWindow(this), ex.GetFormattedException()+"\n Формат данных должен быть скопированной областью из файла Excel состоящий из двух колонок <Заказ> <Сумма>", "Ошибка вставки данных");
            }
        }

        private void RefreshCustomDistr_Click(object sender, RoutedEventArgs e)
        {
            Model.UpdateCurrentTab();
        }

        private void AddCustomDistribution_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddCustomDistribution();
        }

        private void DeleteCustomDistrib_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.CurrentCustomDist != null;
        }

        private void DeleteCustomDistribution_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.DeleteCurrentDistr();
        }

        private void SaveCustomDistrib_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.HasCurrentDistrChanges;
        }

        private void SaveCustomDistribution_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Exception ex = Model.SaveCustomDistr();
            if (ex != null)
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения распределения");
        }


    }

    /// <summary>
    /// Модель представления данных и фильтрации по распределению ЗП
    /// </summary>
    public class SalaryDistributionViewModel : NotificationObject, IDataErrorInfo
    {
        DataSet ds;

        BackgroundWorker bw;
        OracleDataAdapter odaSalaryDistr, odaSalaryAddition, odaSalaryAddCorr, odaSalaryDistrCustom, odaSalaryReceive, odaDetail;

        public SalaryDistributionViewModel()
        { 
            ds = new DataSet();
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.DoWork += new DoWorkEventHandler(LoadDataDisribution);
            odaSalaryDistr = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution/SelectSalaryDistributionView.sql"), Connect.CurConnect);
            odaSalaryDistr.SelectCommand.BindByName = true;
            odaSalaryDistr.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            odaSalaryDistr.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaSalaryDistr.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalaryDistr.TableMappings.Add("Table", "SALARY_DIST");

            odaSalaryAddition = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution/SelectSalaryAdditionView.sql"), Connect.CurConnect);
            odaSalaryAddition.SelectCommand.BindByName = true;
            odaSalaryAddition.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            odaSalaryAddition.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaSalaryAddition.TableMappings.Add("Table", "SALARY_ADDITION");

            odaSalaryAddCorr = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution/SelectSalaryAddCorrView.sql"), Connect.CurConnect);
            odaSalaryAddCorr.SelectCommand.BindByName = true;
            odaSalaryAddCorr.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            odaSalaryAddCorr.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaSalaryAddCorr.TableMappings.Add("Table", "SALARY_ADD_CORRELATION");

            odaSalaryDistrCustom = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution/SelectSalaryDistrCustom.Sql"), Connect.CurConnect);
            odaSalaryDistrCustom.SelectCommand.BindByName = true;
            odaSalaryDistrCustom.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            odaSalaryDistrCustom.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaSalaryDistrCustom.TableMappings.Add("Table", "SALARY_DISTR_CUSTOM");

            odaSalaryDistrCustom.InsertCommand = new OracleCommand(string.Format(@"BEGIN {1}.SALARY_DISTR_CUSTOM_UPDATE(p_SALARY_DISTR_CUSTOM_ID=>:p_SALARY_DISTR_CUSTOM_ID,p_SUBDIV_ID=>:p_SUBDIV_ID,p_ORDER_ID=>:p_ORDER_ID,p_SUM_SAL=>:p_SUM_SAL,p_CALC_DATE=>:p_CALC_DATE,p_DEGREE_ID=>:p_DEGREE_ID,p_ORDER_NUMBER=>:p_ORDER_NUMBER,p_HOURS=>:p_HOURS);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSalaryDistrCustom.InsertCommand.BindByName = true;
            odaSalaryDistrCustom.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSalaryDistrCustom.InsertCommand.Parameters.Add("p_SALARY_DISTR_CUSTOM_ID", OracleDbType.Decimal, 0, "SALARY_DISTR_CUSTOM_ID").Direction = ParameterDirection.InputOutput;
            odaSalaryDistrCustom.InsertCommand.Parameters["p_SALARY_DISTR_CUSTOM_ID"].DbType = DbType.Decimal;
            odaSalaryDistrCustom.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaSalaryDistrCustom.InsertCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID").Direction = ParameterDirection.Input;
            odaSalaryDistrCustom.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL").Direction = ParameterDirection.Input;
            odaSalaryDistrCustom.InsertCommand.Parameters.Add("p_CALC_DATE", OracleDbType.Date, 0, "CALC_DATE").Direction = ParameterDirection.Input;
            odaSalaryDistrCustom.InsertCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID").Direction = ParameterDirection.Input;
            odaSalaryDistrCustom.InsertCommand.Parameters.Add("p_ORDER_NUMBER", OracleDbType.Decimal, 0, "ORDER_NUMBER").Direction = ParameterDirection.Input;
            odaSalaryDistrCustom.InsertCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS").Direction = ParameterDirection.Input;

            odaSalaryDistrCustom.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {1}.SALARY_DISTR_CUSTOM_UPDATE(p_SALARY_DISTR_CUSTOM_ID=>:p_SALARY_DISTR_CUSTOM_ID,p_SUBDIV_ID=>:p_SUBDIV_ID,p_ORDER_ID=>:p_ORDER_ID,p_SUM_SAL=>:p_SUM_SAL,p_CALC_DATE=>:p_CALC_DATE,p_DEGREE_ID=>:p_DEGREE_ID,p_ORDER_NUMBER=>:p_ORDER_NUMBER,p_HOURS=>:p_HOURS);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSalaryDistrCustom.UpdateCommand.BindByName = true;
            odaSalaryDistrCustom.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSalaryDistrCustom.UpdateCommand.Parameters.Add("p_SALARY_DISTR_CUSTOM_ID", OracleDbType.Decimal, 0, "SALARY_DISTR_CUSTOM_ID").Direction = ParameterDirection.InputOutput;
            odaSalaryDistrCustom.UpdateCommand.Parameters["p_SALARY_DISTR_CUSTOM_ID"].DbType = DbType.Decimal;
            odaSalaryDistrCustom.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaSalaryDistrCustom.UpdateCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID").Direction = ParameterDirection.Input;
            odaSalaryDistrCustom.UpdateCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL").Direction = ParameterDirection.Input;
            odaSalaryDistrCustom.UpdateCommand.Parameters.Add("p_CALC_DATE", OracleDbType.Date, 0, "CALC_DATE").Direction = ParameterDirection.Input;
            odaSalaryDistrCustom.UpdateCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID").Direction = ParameterDirection.Input;
            odaSalaryDistrCustom.UpdateCommand.Parameters.Add("p_ORDER_NUMBER", OracleDbType.Decimal, 0, "ORDER_NUMBER").Direction = ParameterDirection.Input;
            odaSalaryDistrCustom.UpdateCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS").Direction = ParameterDirection.Input;

            odaSalaryDistrCustom.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {1}.SALARY_DISTR_CUSTOM_DELETE(:p_SALARY_DISTR_CUSTOM_ID);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSalaryDistrCustom.DeleteCommand.BindByName = true;
            odaSalaryDistrCustom.DeleteCommand.Parameters.Add("p_SALARY_DISTR_CUSTOM_ID", OracleDbType.Decimal, 0, "SALARY_DISTR_CUSTOM_ID").Direction = ParameterDirection.InputOutput;


            odaSalaryReceive = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution/SelectSalaryReceive.sql"), Connect.CurConnect);
            odaSalaryReceive.SelectCommand.BindByName = true;
            odaSalaryReceive.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            odaSalaryReceive.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaSalaryReceive.TableMappings.Add("Table", "SAL_SUBDIV_RECEIVE");

            odaDetail = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution/SelectDetailDistr.sql"), Connect.CurConnect);
            odaDetail.SelectCommand.BindByName = true;
            odaDetail.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            odaDetail.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaDetail.TableMappings.Add("Table", "DETAIL");

            UpdateCurrentTab();
        }

        /// <summary>
        /// После окончания загрузки данные обновляем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsLoading = false;
            if (e.Error != null) MessageBox.Show(e.Error.GetFormattedException(), "Ошибка получения данных");
            else if (e.Cancelled == true) return;
         /*  else 
                RaisePropertyChanged(() => SalaryDistrSource);*/
        }

        DataView _salaryDistrSource;

        /// <summary>
        /// Источник данных просмотра распределения
        /// </summary>
        public DataView SalaryDistrSource
        {
            get
            {
                if (_salaryDistrSource == null && ds!=null && ds.Tables.Contains("SALARY_DIST"))
                {
                    _salaryDistrSource = new DataView(ds.Tables["SALARY_DIST"], "", "", DataViewRowState.CurrentRows);
                }
                return _salaryDistrSource;
            }
        }

        private DataView _salaryAdditionSource, _salaryAddCorrSource;


        /// <summary>
        /// Источник даннных для базы для распределения
        /// </summary>
        public DataView SalaryAdditionSource
        {
            get
            {
                if (_salaryAdditionSource == null && ds.Tables.Contains("SALARY_ADDITION"))
                {
                    _salaryAdditionSource = new DataView(ds.Tables["SALARY_ADDITION"], "", "", DataViewRowState.CurrentRows);
                    SetFilter();
                }
                return _salaryAdditionSource;
            }
        }

        public DataView SalaryAddCorrSource
        {
            get
            {
                if (_salaryAddCorrSource == null && ds.Tables.Contains("SALARY_ADD_CORRELATION"))
                {
                    _salaryAddCorrSource = new DataView(ds.Tables["SALARY_ADD_CORRELATION"], "", "", DataViewRowState.CurrentRows);
                }
                return _salaryAddCorrSource;
            }
        }

        bool _isLoading = false;

        /// <summary>
        /// Загружаем ли данные в текущий момент
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        /// <summary>
        /// Оповестим кого надо что фильтр изменился
        /// </summary>
        public bool FilterChanged
        {
            get;
            set;
        }

        /// <summary>
        /// Выбранное значение для показа базы распределения
        /// </summary>
        public TypeBaseItem SelectedTypeDistBase
        {
            get
            {
                return _selectedTypeDistBase;
            }
            set
            {
                _selectedTypeDistBase = value;
                RaisePropertyChanged(() => SelectedTypeDistBase);
                SetFilter();
            }
        }

        IEnumerable<TypeBaseItem> _typeDistBaseSource;

        /// <summary>
        /// Источник данных для списка по фильтра базы распределения
        /// </summary>
        public IEnumerable<TypeBaseItem> TypeDistBaseSource
        { 
            get
            {
                if (_typeDistBaseSource == null)
                {
                    var items = AppXmlHelper.GetElements("TYPE_DISTR_BASE_SOURCE");
                    _typeDistBaseSource = items.Select(p => new TypeBaseItem() { 
                            Name = p.Attribute("Name").Value, 
                            ID = decimal.Parse(p.Attribute("ID").Value), 
                            IncludePayments = p.Attribute("IncludePayments").Value,
                            ExcludePayments = p.Attribute("ExcludePayments").Value}).ToList();
                    _selectedTypeDistBase = _typeDistBaseSource.First();
                }
                return _typeDistBaseSource;
            }
        }


        /// <summary>
        /// Обновление текущей вкладки данных
        /// </summary>
        public void UpdateCurrentTab()
        {
            switch (SelectedTabIndex)
            {
                case 0: UpdateDistrLayout(); if (_salaryDistrSource == null) RaisePropertyChanged(() => SalaryDistrSource); break;
                case 1: UpdateSalaryAddLayout(); if (_salaryAdditionSource == null) RaisePropertyChanged(() => SalaryAdditionSource); break;
                case 2: UpdateSalaryAddCorrLayout(); if (_salaryAddCorrSource==null) RaisePropertyChanged(()=> SalaryAddCorrSource); break;
                case 3: UpdateSalaryDistCustom(); if (_salaryDistCustomSource == null) RaisePropertyChanged(() => SalaryDistCustomSource); break;
                case 4: UpdateSalaryReceive(); if (_salaryReceiveSource == null) RaisePropertyChanged(() => SalaryReceiveSource); break;
                case 5: UpdateDetail(); if (_detailSource == null) RaisePropertyChanged(() => DetailSource); break;
            }
            RaisePropertyChanged(() => FilterChanged);
        }

        /// <summary>
        /// Обновление асинхронное даных
        /// </summary>
        private void UpdateDistrLayout()
        {
            if (bw.IsBusy)
                return;
            IsLoading = true;
            bw.RunWorkerAsync(new object[] {  SubdivID, SelectedDate});
        }

        /// <summary>
        /// Обновление данных по базе распределения
        /// </summary>
        private void UpdateSalaryAddLayout()
        { 
            if (ds.Tables.Contains("SALARY_ADDITION"))
                ds.Tables["SALARY_ADDITION"].Rows.Clear();

            try
            {
                odaSalaryAddition.SelectCommand.SetParameters(this);
                odaSalaryAddition.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
            }
        }

        /// <summary>
        /// Получение данных по корректировкам допзарплаты
        /// </summary>
        private void UpdateSalaryAddCorrLayout()
        { 
            if (ds.Tables.Contains("SALARY_ADD_CORRELATION"))
                ds.Tables["SALARY_ADD_CORRELATION"].Rows.Clear();
            try
            {
                odaSalaryAddCorr.SelectCommand.SetParameters(this);
                odaSalaryAddCorr.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
            }
        }

        /// <summary>
        /// Обновление данных по разноске на заказы
        /// </summary>
        private void UpdateSalaryDistCustom()
        {
            if (ds.Tables.Contains("SALARY_DISTR_CUSTOM"))
                ds.Tables["SALARY_DISTR_CUSTOM"].Rows.Clear();

            odaSalaryDistrCustom.SelectCommand.SetParameters(this);
            try
            {
                odaSalaryDistrCustom.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения даных по разноске на заказы");
            }

        }

        /// <summary>
        /// Обновление представления по переданным затратам
        /// </summary>
        private void UpdateSalaryReceive()
        {
            if (ds.Tables.Contains("SAL_SUBDIV_RECEIVE"))
                ds.Tables["SAL_SUBDIV_RECEIVE"].Rows.Clear();

            odaSalaryReceive.SelectCommand.SetParameters(this);
            try
            {
                odaSalaryReceive.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения даных по принятым-переданным затратам");
            }
        }

        /// <summary>
        /// Обновление данных по нарядам
        /// </summary>
        private void UpdateDetail()
        {
            if (ds.Tables.Contains("DETAIL"))
                ds.Tables["DETAIL"].Rows.Clear();

            odaDetail.SelectCommand.SetParameters(this);
            try
            {
                odaDetail.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения даных по нарядам");
            }
        }

        /// <summary>
        /// Представление - частное распределение по 11, 12 цехам
        /// </summary>
        public DataView SalaryDistCustomSource
        {
            get
            {
                if (_salaryDistCustomSource == null && ds.Tables.Contains("SALARY_DISTR_CUSTOM"))
                    _salaryDistCustomSource = new DataView(ds.Tables["SALARY_DISTR_CUSTOM"], "", "CODE_SUBDIV, ORDER_NUMBER", DataViewRowState.CurrentRows);
                return _salaryDistCustomSource;
            }
        }

        /// <summary>
        /// Принятые - переданные затраты по подразделениям
        /// </summary>
        public DataView SalaryReceiveSource
        {
            get
            {
                if (_salaryReceiveSource == null && ds.Tables.Contains("SAL_SUBDIV_RECEIVE"))
                    _salaryReceiveSource = new DataView(ds.Tables["SAL_SUBDIV_RECEIVE"], "", "", DataViewRowState.CurrentRows);
                return _salaryReceiveSource;
            }
        }

        /// <summary>
        /// Представление источник для нарядов
        /// </summary>
        public DataView DetailSource
        {
            get
            {
                if (_detailSource == null && ds.Tables.Contains("DETAIL"))
                    _detailSource = new DataView(ds.Tables["DETAIL"], "", "CODE_SUBDIV", DataViewRowState.CurrentRows);
                return _detailSource;
            }
        }

        /// <summary>
        /// Текущее выбранная запись принятых затрат
        /// </summary>
        public DataRowView CurrentSalReceiveSubdiv
        {
            get
            {
                return _currentSalReceive;
            }
            set
            {
                _currentSalReceive = value;
                RaisePropertyChanged(() => CurrentSalReceiveSubdiv);
            }
        }

        /// <summary>
        /// Текущая выбранная строка в базе распределения
        /// </summary>
        public DataRowView CurrentSalaryAddition
        {
            get
            {
                return _currentSalaryAddition;
            }
            set
            {
                _currentSalaryAddition = value;
                RaisePropertyChanged(() => CurrentSalaryAddition);
            }
        }

        /// <summary>
        /// Загрузка данных по распределению
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadDataDisribution(object sender, DoWorkEventArgs e)
        { 
            if (ds.Tables.Contains("SALARY_DIST"))
                ds.Tables["SALARY_DIST"].Rows.Clear();
            odaSalaryDistr.SelectCommand.Parameters["p_subdiv_id"].Value = ((object[])e.Argument)[0];
            odaSalaryDistr.SelectCommand.Parameters["p_date"].Value = ((object[])e.Argument)[1];
            odaSalaryDistr.Fill(ds);
        }

        DateTime? _selectedDate = DateTime.Today.Trunc("Month");

        /// <summary>
        /// Выбранная дата 
        /// </summary>
        [OracleParameterMapping(ParameterName="p_date")]
        public DateTime? SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    RaisePropertyChanged(() => SelectedDate);
                    UpdateCurrentTab();
                }
            }
        }

        decimal? _subdivID = 0;
        /// <summary>
        /// Выбранное подразделение
        /// </summary>
        [OracleParameterMapping(ParameterName="p_subdiv_id")]
        public decimal? SubdivID
        {
            get
            {
                return _subdivID;
            }
            set
            {
                if (SubdivID != value)
                {
                    _subdivID = value;
                    RaisePropertyChanged(() => SubdivID);
                    UpdateCurrentTab();
                }
            }

        }

        public string CodeSubdiv
        {
            get
            {
                if (_subdivID == null) return string.Empty;
                if (_subdivID == 0) return "У-УАЗ";
                return AppDataSet.Tables["SUBDIV"].Select("SUBDIV_ID=" + _subdivID).Select(r => r["CODE_SUBDIV"].ToString()).FirstOrDefault();
            }
        }

        string _orderFilter;

        /// <summary>
        /// Фильтр по заказам
        /// </summary>
        public string OrderFilter
        {
            get
            {
                return _orderFilter;
            }
            set
            {
                _orderFilter = value;
                RaisePropertyChanged(() => OrderFilter);
                SetFilter();
            }
        }

        /// <summary>
        /// Устанавливаем фильтры для заказов и для видов оплат базы
        /// </summary>
        private void SetFilter()
        {
            if (_salaryDistrSource != null)
            {
                _salaryDistrSource.RowFilter = string.Format("Заказ like '{0}%'", _orderFilter);
            }
            if (_salaryAdditionSource != null)
            {
                _salaryAdditionSource.RowFilter = (string.IsNullOrEmpty(SelectedTypeDistBase.ExcludePayments) ?
                        string.Format("PAYMENT_TYPE_ID in ({0})", SelectedTypeDistBase.IncludePayments)
                        : string.Format("PAYMENT_TYPE_ID not in ({0})", SelectedTypeDistBase.ExcludePayments)
                        ) + " and " + string.Format("ORDER_NAME like '{0}%'", _orderFilter);
                RaisePropertyChanged(() => FilterChanged);
            }
            if (_salaryAddCorrSource != null)
                _salaryAddCorrSource.RowFilter = string.Format("ORDER_NAME like '{0}%'", _orderFilter);
            if (_detailSource != null)
                _detailSource.RowFilter = string.Format("ORDER_NAME like '{0}%'", _orderFilter);
        }

        int _selectedTabIndex = 0;
        private DataView _salaryDistCustomSource;
        private  DataView _salaryReceiveSource;
        private  DataView _detailSource;
        private DataRowView _currentSalReceive;
        private DataRowView _currentSalCorrAddition;
        private DataRowView _currentSalaryAddition;
        private TypeBaseItem _selectedTypeDistBase;

        /// <summary>
        /// Выбранная вкладка, ее номер
        /// </summary>
        public int SelectedTabIndex
        {
            get
            {
                return _selectedTabIndex;
            }
            set
            {
                _selectedTabIndex = value;
                RaisePropertyChanged(() => SelectedTabIndex);
                UpdateCurrentTab();
            }
        }

        public string Error
        {
            get { return ""; }
        }

        public string this[string columnName]
        {
            get { return ""; }
        }

       /* public DependencyObject OwnerWindow
        {
            get;
            set;
        }*/

        /// <summary>
        /// Расчет распределения.
        /// </summary>
        public void CalcSalaryDistribution(DependencyObject sender)
        {
            OracleCommand odaCalc = new OracleCommand(
                string.Format("begin {1}.SALARY_CALC_DISTR_PKG.Calc_FULL_DISTRIBUTION(:p_date, :p_subdiv_id); end;", Connect.SchemaApstaff, Connect.SchemaSalary),
                Connect.CurConnect);
            odaCalc.BindByName = true;
            odaCalc.Parameters.Add("p_date", OracleDbType.Date, SelectedDate, ParameterDirection.Input);
            odaCalc.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(sender, "Расчет данных по распределению...",
                odaCalc, 
                (p, pw) =>
                {
                    if (pw.Cancelled) return;
                    if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка расчета");
                    else
                        MessageBox.Show(Window.GetWindow(sender), "Расчет успешно закончен", "Зарплата предприятия");
                });
        }
        
        /// <summary>
        /// А здесь просто вставка данных из ЗП и по нарядам + корректировки
        /// </summary>
        public void CalcBaseDistribution(DependencyObject sender)
        {
            OracleCommand odaCalc = new OracleCommand(
                string.Format("begin {1}.SALARY_CALC_DISTR_PKG.CALC_DISTR_BASE(:p_subdiv_id, :p_date); end;", Connect.SchemaApstaff, Connect.SchemaSalary),
                Connect.CurConnect);
            odaCalc.BindByName = true;
            odaCalc.Parameters.Add("p_date", OracleDbType.Date, SelectedDate, ParameterDirection.Input);
            odaCalc.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(sender, "Расчет базы распределения...",
                odaCalc,
                (p, pw) =>
                {
                    if (pw.Cancelled) return;
                    if (pw.Error != null) MessageBox.Show(Window.GetWindow(sender), pw.Error.GetFormattedException(), "Ошибка расчета");
                    else
                        MessageBox.Show(Window.GetWindow(sender), "Расчет успешно закончен", "Зарплата предприятия");
                });
        }

        /// <summary>
        /// Удаление текущей позиции принятых переданных
        /// </summary>
        internal void DeleteCurrentReceiveSal()
        {
            if (CurrentSalReceiveSubdiv == null) return;
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                OracleCommand cmd = new OracleCommand(string.Format(@"BEGIN {0}.SAL_SUBDIV_RECEIVE_DELETE(:p_SAL_SUBDIV_RECEIVE_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
                cmd.BindByName = true;
                cmd.Parameters.Add("p_SAL_SUBDIV_RECEIVE_ID", OracleDbType.Decimal, CurrentSalReceiveSubdiv["SAL_SUBDIV_RECEIVE_ID"], ParameterDirection.InputOutput);
                cmd.ExecuteNonQuery();
                tr.Commit();
                UpdateCurrentTab();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления записи");
            }
           
        }

        /// <summary>
        /// Текущая выбранная строка по корректировке зарплаты (чаще нарядов)
        /// </summary>
        public DataRowView CurrentSalCorrAddition
        {
            get
            {
                return _currentSalCorrAddition;
            }
            set
            {
                _currentSalCorrAddition = value;
                RaisePropertyChanged(() => CurrentSalCorrAddition);
            }
        }

        /// <summary>
        /// Логика удаления записи по корректировке
        /// </summary>
        internal bool DeleteCurrentSalaryCorrelation()
        {
            OracleCommand cmd = new OracleCommand(string.Format("begin {1}.SALARY_ADD_CORRELATION_DELETE(:p_SALARY_ADD_CORRELATION_ID); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmd.Parameters.Add("p_SALARY_ADD_CORRELATION_ID", OracleDbType.Decimal, CurrentSalCorrAddition["SALARY_ADD_CORRELATION_ID"], ParameterDirection.Input);
            cmd.BindByName = true;
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления записи");
                return false;
            }
        }

        /// <summary>
        /// Распределение ручное вставка данных заказ сумма
        /// </summary>
        /// <param name="values"></param>
        internal void AddCustomDistributionValues(IEnumerable<Tuple<string, decimal>> values)
        {
            decimal k = ds.Tables["SALARY_DISTR_CUSTOM"].Rows.OfType<DataRow>().Select(r=>r.Field2<decimal?>("ORDER_NUMBER")).Max()??0;
            int i = 1;
            int cnt = values.Count();
            foreach (var p in values)
            {
                if (p.Item2 != 0 || i == cnt)
                {
                    DataRow r = ds.Tables["SALARY_DISTR_CUSTOM"].NewRow();
                    r["SUBDIV_ID"] = this.SubdivID;
                    r["CODE_SUBDIV"] = this.CodeSubdiv;
                    if (AppDictionaries.OrderCodeToID.ContainsKey(p.Item1))
                        r["ORDER_ID"] = AppDictionaries.OrderCodeToID[p.Item1];
                    else
                        throw new Exception(string.Format("Заданный заказ {0} не существует в книге заказов. Обновите справочник или исправьте заказ", p.Item1));
                    r["ORDER_NAME"] = p.Item1;
                    r["SUM_SAL"] = Math.Round(p.Item2, 2, MidpointRounding.AwayFromZero);
                    r["CALC_DATE"] = SelectedDate.Value.Trunc("Month");
                    r["DEGREE_ID"] = 1m;
                    r["CODE_DEGREE"] = "01";
                    r["ORDER_NUMBER"] = i+k+1;
                    r["HOURS"] = 0;
                    ds.Tables["SALARY_DISTR_CUSTOM"].Rows.Add(r);
                }
                ++i;
            }
            RaisePropertyChanged(() => SalaryDistCustomSource); 
        }

        /// <summary>
        /// Добавление строчки в распределение
        /// </summary>
        public void AddCustomDistribution()
        {
            DataRow r = ds.Tables["SALARY_DISTR_CUSTOM"].NewRow();
            r["SUBDIV_ID"] = this.SubdivID;
            r["CODE_SUBDIV"] = this.CodeSubdiv;
            r["CALC_DATE"] = SelectedDate.Value.Trunc("Month");
            r["DEGREE_ID"] = 1m;
            r["CODE_DEGREE"] = "01";
            r["ORDER_NUMBER"] = (decimal)(ds.Tables["SALARY_DISTR_CUSTOM"].Compute("MAX(ORDER_NUMBER)", "")??0);
            r["HOURS"] = 0;
            ds.Tables["SALARY_DISTR_CUSTOM"].Rows.Add(r);
            RaisePropertyChanged(() => SalaryDistCustomSource); 
        }

        DataRowView _currentCustomDistr;
        /// <summary>
        /// Текущая выбранная строка ручного распределения
        /// </summary>
        public DataRowView CurrentCustomDist
        {
            get
            {
                return _currentCustomDistr;
            }
            set
            {
                _currentCustomDistr = value;
                RaisePropertyChanged(() => CurrentCustomDist);
            }
        }

        /// <summary>
        /// Удаление выбранной строчки
        /// </summary>
        public void DeleteCurrentDistr()
        {
            _currentCustomDistr.Delete();
            RaisePropertyChanged(() => SalaryDistCustomSource);
        }

        /// <summary>
        /// Имеются ли изменения в ручном распределении по заказам
        /// </summary>
        public bool HasCurrentDistrChanges 
        {
            get
            {
                return ds != null && ds.Tables.Contains("SALARY_DISTR_CUSTOM") && ds.Tables["SALARY_DISTR_CUSTOM"].GetChanges() != null;
            }
        }

        /// <summary>
        /// Сохранение ручного распределения заказов
        /// </summary>
        /// <returns></returns>
        internal Exception SaveCustomDistr()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                odaSalaryDistrCustom.Update(ds.Tables["SALARY_DISTR_CUSTOM"], "SALARY_DISTR_CUSTOM_ID");
                tr.Commit();
                return null;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                return ex;
            }
        }
    }

    public class TypeBaseItem
    {
        public string Name
        {
            get;
            set;
        }

        public decimal? ID
        {
            get;
            set;
        }
        public string IncludePayments
        {
            get;
            set;
        }
        public string ExcludePayments
        {
            get;
            set;
        }
    }
}
