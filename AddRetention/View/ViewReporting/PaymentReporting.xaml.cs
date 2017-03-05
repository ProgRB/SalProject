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

using Salary.View;
using Oracle.DataAccess.Client;
using System.Data;
using Salary.Reports;
using Salary.Helpers;
using Microsoft.Reporting.WinForms;
using System.IO;
using OfficeOpenXml;
using Salary.Interfaces;
using LibrarySalary.Helpers;

namespace Salary.ViewReporting
{
    /// <summary>
    /// Interaction logic for PaymentReporting.xaml
    /// </summary>
    public partial class PaymentReporting : UserControl
    {
        /// <summary>
        /// Отчет по месяцам - сотрудники и  их ЗП за какой-либо период
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryByMonths_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(null, Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Year"), Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("RepSalaryByMonths.sql"), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                    (s, pw) =>
                    {
                        OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                        DataTable t = new DataTable();
                        a.Fill(t);
                        pw.Result = t;
                    },
                        oda, oda.SelectCommand,
                    (s, pw) =>
                    {
                        if (pw.Cancelled) return;
                        if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                        else
                            ViewReportWindow.ShowReport(this, "Отчет по зарабтной плате сотрудников по месяцам", "Rep_SalaryByMonths.rdlc", pw.Result as DataTable, null, System.Drawing.Printing.Duplex.Default, false);
                    });
            }
        }

        /// <summary>
        /// Отчет книга экономиста по пррофессиям. Для нарядников выводится профессия и сколько он получил в течении периода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Если параметер=Null то выводим по группа мастера отчет</param>
        private void RepEconBookByPosition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
                FilterReporting f = new FilterReporting(null, Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Year"), Model.FilterEmp.SelectedDate.Trunc("Month"));
                if (e.Parameter == null)
                    f.AllowGroupMaster = true;
                else
                    f.AllowCodePos = true;
                f.Owner = Window.GetWindow(this);
                if (f.ShowDialog() == true)
                {
                    OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(e.Parameter==null?"RepEconBookByGroupMaster.sql":"RepEconBookByPosition.sql"), Connect.CurConnect);
                    oda.SelectCommand.BindByName = true;
                    oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                    oda.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                    oda.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                    if (e.Parameter == null)
                        oda.SelectCommand.Parameters.Add("p_group_master", OracleDbType.Varchar2, f.GroupMaster.Trim(), ParameterDirection.Input);
                    else
                        oda.SelectCommand.Parameters.Add("p_code_pos", OracleDbType.Varchar2, f.CodePos, ParameterDirection.Input);
                    oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                    AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        (s, pw) =>
                        {
                            OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                            DataTable t = new DataTable();
                            a.Fill(t);
                            pw.Result = t;
                        },
                            oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Отчет Книга экономиста", e.Parameter==null? "Rep_EconBookByGroupMaster.rdlc": "Rep_EconBookByPosition.rdlc", pw.Result as DataTable,
                                    new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString()),
                                    new ReportParameter("P_CODE_SUBDIV", f.CodeSubdiv)
                                    });
                        });
                }
        }


        /// <summary>
        /// Отчет по видам оплат для экономистов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Если параметр Налл то считаем что без заказов нужен отчет</param>
        private void RepSalaryByPaymentType_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Month"), new decimal[] { 1, 6});
            f.AllowPaymentTypes = true;
            string query = null, report_templ = null, real_template = null, title = null;
            if (e.Parameter == null) // 
            {
                query = Queries.GetQuery("RepSalaryPaymentTypeEcon.sql");
                f.FillSelectionColumn("RepEconByPayment");
                query = string.Format(query, Connect.SchemaApstaff,
                    Connect.SchemaSalary, string.Join(", ", f.SelectedColumns.Select(r => string.Format("{0} \"{1}\"", r.ColumnName, r.AliasName))));
                real_template = "Rep_SalaryByPaymentType.xlsx";
                report_templ = string.Format("Rep_SalaryByPaymentType{0}.xlsx", DateTime.Now.Ticks % 100000);
                title = "Отчет по видам оплат";
            }
            else if (e.Parameter.ToString() == "ANY") // если это отчет произовольной формы, то будем подставлять имена столбцов в запрос
            {
                query = Queries.GetQuery("RepSalaryPaymentTypeEcon.sql");
                f.IsColumnSelectAllowed = true;
                f.FillSelectionColumn("RepEconByPayment");
                real_template = "Rep_SalaryByPaymentTypeAny.xlsx";
                report_templ = string.Format("Rep_SalaryByPaymentType{0}.xlsx", DateTime.Now.Ticks%100000);
                title = "Отчет по видам оплат";
            }
            else if (e.Parameter.ToString() == "EM")
            {
                query = Queries.GetQueryWithSchema("RepSalaryPaymentTypeEconEM.sql");
                real_template = "Rep_SalaryByPaymentType.xlsx";
                report_templ = string.Format("Rep_SalaryByPaymentType{0}.xlsx", DateTime.Now.Ticks % 100000);
                title = "Отчет по видам оплат";
            }
            else if (e.Parameter.ToString() == "SHIFT")
            {
                query = Queries.GetQueryWithSchema("RepSalaryPaymentTypeWithShiftCode.sql");
                real_template = "Rep_SalaryByPaymentTypeWithShiftRegion.xlsx";
                report_templ = string.Format("Rep_SalaryByPaymentType{0}.xlsx", DateTime.Now.Ticks % 100000);
                title = "Отчет по видам оплат с МВЗ";
            }
            else
            {
                query = Queries.GetQueryWithSchema("RepSalaryPaymentTypeWithOrderEcon.sql");
                real_template = "Rep_SalaryByPaymentTypeWithOrder.xlsx";
                report_templ = string.Format("Rep_SalaryByPaymentTypeWithOrder{0}.xlsx", DateTime.Now.Ticks % 100000);
                title = "Отчет по видам оплат с заказами";
            }

            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                if (e.Parameter!=null && e.Parameter.ToString() == "ANY") // если это отчет произовольной формы, то будем подставлять имена столбцов в запрос
                {
                    query = Queries.GetQuery("RepSalaryPaymentTypeEcon.sql");
                    query = string.Format(query, Connect.SchemaApstaff,
                        Connect.SchemaSalary, string.Join(", ", f.SelectedColumns.Select(r => string.Format("{0} \"{1}\"", r.ColumnName, r.AliasName))));
                }
                OracleDataAdapter oda = new OracleDataAdapter(query, Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                oda.SelectCommand.Parameters.Add("p_payment_type_ids", OracleDbType.Array, f.SelectedPaymentIDs, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                string codeSubdiv = f.SubdivSelector1.CodeSubdiv, dateBegin = f.DateBegin.Value.ToString("MM yyyy"), dateEnd = f.DateEnd.Value.ToString("MM yyyy");
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                    (s, pw) =>
                    {
                        OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                        DataTable t = new DataTable();
                        a.Fill(t);
                        string NewFileName = System.IO.Path.GetTempPath() + report_templ;
                        File.Copy(Connect.CurrentAppPath + @"\Reports\"+real_template, NewFileName);
                        ExcelPackage ep = new ExcelPackage(new FileInfo(NewFileName));
                        ExcelWorkbook wb = ep.Workbook; 
                        ExcelWorksheet ws = wb.Worksheets[1];
                        ws.Cells["A1"].Value = string.Format("ЗАРПЛАТА ПО ВИДАМ ОПЛАТ с {0} по {1} по подразделению {2}", dateBegin, dateEnd, codeSubdiv);
                        ws.Cells[5, 1].LoadFromDataTable(t, true, OfficeOpenXml.Table.TableStyles.Light1);
                        ep.Save();
                        pw.Result = NewFileName;
                        ep.Dispose();
                    },
                        oda, oda.SelectCommand,
                    (s, pw) =>
                    {
                        if (pw.Cancelled) return;
                        if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                        else
                        {
                            System.Diagnostics.Process.Start(pw.Result.ToString());
                        }
                    });
            }
            
        }

        /// <summary>
        /// Расперделение ФОТ по категориям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryFPWByDegree_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(null, Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter("RepSalaryFPWByCodeDegree.sql", f,
                    FilterParameter.p_subdiv_id, FilterParameter.p_date_begin, FilterParameter.p_date_end, FilterParameter.c);
                oda.SelectCommand.Parameters.Add("p_all_degree", OracleDbType.Decimal, e.Parameter == null ? 0 : 1, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            if (e.Parameter == null)
                            {
                                ViewReportWindow.ShowReport(this, "Отчет распределение ФОТ по категориям", "Rep_SalaryFPWByDegree.rdlc", (pw.Result as DataSet).Tables[0],
                                    new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString())
                                    }, System.Drawing.Printing.Duplex.Default, false);
                            }
                            else
                                ViewReportWindow.ShowReport(this, "Отчет структура ЗП по категориям", "Rep_SalaryFPWByDegreeTotal.rdlc", (pw.Result as DataSet).Tables[0],
                                   new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString())
                                   }, System.Drawing.Printing.Duplex.Default, false);
                        });
            }
        }

        /// <summary>
        /// Фактические наряды отчет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepActualDetails_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(null, Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("RepActualDetailInPeriod.sql"), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        (s, pw) =>
                        {
                            OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                            DataTable t = new DataTable();
                            a.Fill(t);
                            pw.Result = t;
                        },
                            oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Отчет фактические наряды за период", "Rep_ActualDetails.rdlc", pw.Result as DataTable,
                                    new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString())
                                    }, System.Drawing.Printing.Duplex.Default, false);
                        });
            }
        }

        /// <summary>
        /// Срочный отчет по подразделениям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Параметр передает список категорий, которые надо вычислять</param>
        private void RepShortBySubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoPieceWorkerReport("Срочный отчет по сдельщикам за период", "RepShortBySubdiv.rdlc", e, Model.FilterEmp.SelectedDate.Trunc("Month"));
            /*FilterReporting f = new FilterReporting(null, Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.Owner = Window.GetWindow(this);
            f.IsCodeDegreeEnabled = true;
            f.SelectedDegreeIDs = e.Parameter.ToString().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(t => decimal.Parse(t)).ToArray();
            if (f.ShowDialog() == true)
            {
                decimal[] degreeids = f.SelectedDegreeIDs;
                OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_ShortBySubdiv.sql"), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_degree_ids", OracleDbType.Array, degreeids, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        (s, pw) =>
                        {
                            OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                            DataTable t = new DataTable();
                            a.Fill(t);
                            pw.Result = t;
                        },
                        oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Срочный отчет по сдельщикам за период", "RepShortBySubdiv.rdlc", pw.Result as DataTable,
                                    new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString()),
                                    new ReportParameter("P_DEGREE_IDS", string.Join(", ", f.GetDegreeIDs().Select(r=>r.ToString().PadLeft(2,'0'))))
                                    }, System.Drawing.Printing.Duplex.Default, false);
                        });
            }*/
        }
        /// <summary>
        /// Срочный отчет по подразделениям- накопительная сводка
        /// </summary>
        /// <param name="sender">Параметрами передается категории для формирования</param>
        /// <param name="e">Параметр передает список категорий, которые надо вычислять</param>
        private void RepShortAccumulateBySubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Year"), Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.Owner = Window.GetWindow(this);
            f.IsCodeDegreeEnabled = true;
            f.SelectedDegreeIDs = e.Parameter.ToString().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(t => decimal.Parse(t)).ToArray();
            if (f.ShowDialog() == true)
            {
                decimal[] degreeids = f.SelectedDegreeIDs;
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter("Rep_ShortBySubdiv.sql", f, FilterParameter.p_subdiv_id, FilterParameter.p_date_begin, FilterParameter.p_date_end,
                    FilterParameter.p_degree_ids, FilterParameter.c);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            ViewReportWindow.ShowReport(this, "Срочный отчет (накопительный) по сдельщикам за период", "RepShortAccumulateBySubdiv.rdlc", (pw.Result as DataSet).Tables[0],
                                new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString()),
                                new ReportParameter("P_DEGREE_IDS", string.Join(", ", f.GetDegreeIDs().Select(r=>r.ToString().PadLeft(2,'0'))))
                                }, System.Drawing.Printing.Duplex.Default, false);
                        });
            }
        }

        /// <summary>
        /// Открытие личной карточки сотрудника для экономистов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewEconCard_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EmpEconCard f = new EmpEconCard(Model.EmpCollection.SelectedItem["TRANSFER_ID"]);
            f.Owner = Window.GetWindow(this);
            f.ShowDialog();
        }

        /// <summary>
        /// Отчет по зарплате, список сотрудников и пайвот по заказам (подстрока из заказов)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryPivotForCodeOrder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(null, Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.Owner = Window.GetWindow(this);
            f.IsCountOrderSignesEnabled = true;
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("RepSalaryPivotOrderCode.sql"), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_count_char", OracleDbType.Decimal, f.CountOrderSignes, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);

                string report_real = "Rep_SalaryByPivotCodeOrder.xlsx";
                string report_templ = string.Format("Rep_SalaryByPivotCodeOrder{0}.xlsx", DateTime.Now.Ticks % 100000);
                string dateBegin = f.DateBegin.Value.ToShortDateString(), dateEnd = f.DateEnd.Value.ToShortDateString(), codeSubdiv = f.CodeSubdiv;
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        (s, pw) =>
                        {
                            OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                            DataTable t = new DataTable();
                            a.Fill(t);
                            string NewFileName = System.IO.Path.GetTempPath() + report_templ;
                            File.Copy(Connect.CurrentAppPath + @"\Reports\" + report_real, NewFileName);
                            ExcelPackage ep = new ExcelPackage(new FileInfo(NewFileName));
                            ExcelWorkbook wb = ep.Workbook;
                            ExcelWorksheet ws = wb.Worksheets[1];
                            ws.Cells["A1"].Value = string.Format("ЗАРПЛАТА ПО БАЛАНСОВЫМ СЧЕТАМ с {0} по {1} по подразделению {2}", dateBegin, dateEnd, codeSubdiv);
                            ws.Cells[2, 1].LoadFromDataTable(t, true, OfficeOpenXml.Table.TableStyles.Light1);
                            ep.Save();
                            pw.Result = NewFileName;
                            ep.Dispose();
                        },
                        oda, oda.SelectCommand,
                    (s, pw) =>
                    {
                        if (pw.Cancelled) return;
                        if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                        else
                        {
                            System.Diagnostics.Process.Start(pw.Result.ToString());
                        }
                    });
            }
        }

        /// <summary>
        /// Отчет по баланосовым счетам, подразделениям, категориям и сводная таблица по месяцам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryPivotForCodeOrder2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(null, Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.DateBegin = Model.FilterEmp.SelectedDate.Trunc("Year");
            f.IsCountOrderSignesEnabled = true;
            f.CountOrderSignes = 13;
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("RepSalaryByOrderNamePivotPDate.sql"), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_count_char", OracleDbType.Decimal, f.CountOrderSignes, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        (s, pw) =>
                        {
                            OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                            DataTable t = new DataTable();
                            a.Fill(t);
                            pw.Result = t;
                        },
                            oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Отчет по б/с за период", "Rep_SalaryByOrdersPivotPayDate.rdlc", pw.Result as DataTable,
                                    new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString())
                                    }, System.Drawing.Printing.Duplex.Default, false);
                        });
            }
        }

        /// <summary>
        /// Отчет по фонду заработной платы. Статьи оплаты и группировка по категориям и первым 4м цифрам заказа. Фильтр на 4 цифры заказа ебанутый еще есть какой-то. Он даже отпускные фильтрует... ебалайство а не отчет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RRepSalaryByGroupAndDegree_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(null, Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter("RepSalaryByGroupAndDegree.sql", f,
                    FilterParameter.p_subdiv_id, FilterParameter.p_date_begin, FilterParameter.p_date_end, FilterParameter.c);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        (s, pw) =>
                        {
                            OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                            DataTable t = new DataTable();
                            a.Fill(t);
                            pw.Result = t;
                        },
                            oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Отчет по фонду ЗП", "Rep_SalaryByPayGroupAndDegree.rdlc", pw.Result as DataTable,
                                    new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString()),
                                    new ReportParameter("P_CODE_SUBDIV", f.CodeSubdiv)
                                    });
                        });
            }
        }

        /// <summary>
        /// Отчет по подразделению- список сотрудников и их ЗП за период
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryBySubdivAndEmp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(null, Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.IsCodeDegreeEnabled = true;
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("RepSalarySubdivEmp.sql"), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_degree_filter", OracleDbType.Array, f.SelectedDegreeIDs, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        (s, pw) =>
                        {
                            OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                            DataTable t = new DataTable();
                            a.Fill(t);
                            pw.Result = t;
                        },
                            oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Отчет зарплата по подразделению", e.Parameter == null ? "Rep_SalaryBySubdivEmp.rdlc" : "Rep_SalaryBySubdivEmpWithOrder.rdlc", pw.Result as DataTable,
                                    new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString()),
                                    new ReportParameter("P_CODE_SUBDIV", f.CodeSubdiv)
                                    });
                        });
            }
        }

        /// <summary>
        /// Отчет выплаты по 243, 270 виду оплат. Можно было бы сделать и на любые виды оплат, только пока не за чем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryMothersPayment_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(null, Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.DateBegin = Model.FilterEmp.SelectedDate.Trunc("Year");
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("RepSalaryMothersPayment.sql"), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        (s, pw) =>
                        {
                            OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                            DataTable t = new DataTable();
                            a.Fill(t);
                            pw.Result = t;
                        },
                            oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Отчет выплаты по уходу за детьми", "Rep_SalaryMothersPayment.rdlc", pw.Result as DataTable,
                                    new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString()),
                                    new ReportParameter("P_CODE_SUBDIV", f.CodeSubdiv)
                                    });
                        });
            }
        }

        /// <summary>
        /// Отчет о накопительной ведомости по основным сдельщикам стоимость часа, сумма нарядов и часов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepAccumPieceWork_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoPieceWorkerReport("Отчет Накопительный по сдельщикам", "Rep_AccumDetailWork.rdlc", e);
        }

        /// <summary>
        /// Сводка по среднему разряду работ и рабочих
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepAvgWorkAndWorkerClassific_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate, Model.FilterEmp.SelectedDate);
            f.Owner = Window.GetWindow(this);
            f.AllowBegin =  System.Windows.Visibility.Hidden;
            f.IsCodeDegreeEnabled = true;
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("RepAvgWorkAndWorkerClassific.sql", f, FilterParameter.p_subdiv_id, FilterParameter.p_date, FilterParameter.p_degree_ids);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...",
                        a, a.SelectCommand, 
                        (p, pw) =>
                            {
                                ViewReportWindow.ShowReport(this, "Сводка по средним разрядам рабочих и работ", "Rep_AvgWorkAndWorkerClassific.rdlc",
                                    (pw.Result as DataSet).Tables[0], new ReportParameter[] { new ReportParameter("P_DATE", f.GetDate().ToShortDateString()),
                                        new ReportParameter("P_DEGREE_IDS", string.Join(", ", f.GetDegreeIDs().Select(r=>r.ToString().PadLeft(2,'0')))) });
                            });
            }
        }

        /// <summary>
        /// Отчет по профессиям.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepReportByPosition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate, Model.FilterEmp.SelectedDate);
            f.IsCodeDegreeEnabled = true;
            f.SelectedDegreeIDs =  e.Parameter.ToString().Split(new char[]{',',' '}, StringSplitOptions.RemoveEmptyEntries).Select(t=>decimal.Parse(t)).ToArray();
            f.Owner = Window.GetWindow(this);
            f.AllowBegin =  System.Windows.Visibility.Collapsed;
            f.AllowCodePos = true;
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("Rep_ReportByPosition.sql", f, FilterParameter.p_subdiv_id, FilterParameter.p_date_begin, FilterParameter.p_date_end, FilterParameter.p_degree_ids, FilterParameter.c);
                a.SelectCommand.Parameters["p_date_begin"].Value = f.GetDateEnd();
                a.SelectCommand.Parameters.Add("p_code_pos", OracleDbType.Varchar2, f.CodePos, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...",
                        a, a.SelectCommand, 
                        (p, pw) =>
                            {
                                ViewReportWindow.ShowReport(this, "Отчет по профессиям", "Rep_SalaryByPosition.rdlc",
                                    (pw.Result as DataSet).Tables[0], 
                                    new ReportParameter[] { 
                                        new ReportParameter("P_DATE", f.DateEnd.Value.ToShortDateString()),
                                        new ReportParameter("P_CODE_DEGREEDS", string.Join(", ",f.GetDegreeIDs().Select(t=>t.ToString().PadLeft(2,'0'))))});
                            });
            }
        }

        /// <summary>
        /// Отчет сводка о зарплатах - кто в какие интервалы попадает своей зарплатой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepCountEmpSalaryByValue_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate, Model.FilterEmp.SelectedDate);
            f.Owner = Window.GetWindow(this);
            f.AllowBegin = System.Windows.Visibility.Collapsed;
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("Rep_ReportCountValuePayment.sql", f, FilterParameter.p_subdiv_id, FilterParameter.p_date_begin, 
                    FilterParameter.p_date_end, FilterParameter.c);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...",
                        a, a.SelectCommand,
                        (p, pw) =>
                        {
                            ViewReportWindow.ShowReport(this, "Отчет о заработках сдельщиков", "Rep_CountEmpSalaryByValues.rdlc",
                                (pw.Result as DataSet).Tables[0],
                                new ReportParameter[] 
                                { 
                                        new ReportParameter("P_DATE", f.DateEnd.Value.ToShortDateString())
                                });
                        });
            }
        }

        /// <summary>
        /// Ведомость по заказам и видам оплат. Доступна только людям имеющим доступ на просмотр по подразделению
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalByDegreeAndOrders_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate, Model.FilterEmp.SelectedDate);
            f.Owner = Window.GetWindow(this);
            f.IsFilterOrderVisible = true;
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("RepSalByDegreeOrdersOrPayTypeEcon.sql", f, FilterParameter.p_subdiv_id, FilterParameter.p_date_begin, FilterParameter.p_date_end, FilterParameter.c);
                a.SelectCommand.Parameters.Add("p_order_filter", OracleDbType.Varchar2, f.OrderFilter, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...",
                        a, a.SelectCommand,
                        (p, pw) =>
                        {
                            ViewReportWindow.ShowReport(this, "\"Ведомость по категориям и заказам\"", "Rep_SalByDegreeOrdersEcon.rdlc", (pw.Result as DataSet).Tables[0],
                                    new ReportParameter[] { new ReportParameter("CODE_SUBDIV", f.CodeSubdiv), 
                                    new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()), 
                                    new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString()) 
                                    }.ToList());
                        });
            }
        }

        /// <summary>
        /// Выполнение срочного отчета за период
        /// </summary>
        /// <param name="caption">Надпись на экране</param>
        /// <param name="ReportRDLC">Шаблон макет</param>
        /// <param name="e">параметр от команды выполняемый</param>
        /// <param name="startDate">Дата начала фильтра, если не указан то начало года</param>
        private void DoPieceWorkerReport(string caption, string ReportRDLC, ExecutedRoutedEventArgs e, DateTime? startDate =null)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, startDate??Model.FilterEmp.SelectedDate.Trunc("Year"), Model.FilterEmp.SelectedDate.Trunc("Month"));

            f.Owner = Window.GetWindow(this);
            f.IsCodeDegreeEnabled = true;
            f.SelectedDegreeIDs = e.Parameter.ToString().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(t => decimal.Parse(t)).ToArray();
            if (f.ShowDialog() == true)
            {
                decimal[] degreeids = f.SelectedDegreeIDs;
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter("Rep_ShortBySubdiv.sql", f, FilterParameter.p_subdiv_id, FilterParameter.p_date_begin, FilterParameter.p_date_end,
                    FilterParameter.p_degree_ids, FilterParameter.c);
                // если выбрана 1ая категория, то отчет требуется в разрезе видов производства
                string actualReportRDLC = string.Format("{0}Sub{1}", System.IO.Path.GetFileNameWithoutExtension(ReportRDLC), new FileInfo(ReportRDLC).Extension);
                actualReportRDLC = degreeids.Length == 1 && degreeids[0] == 1 || !ViewReportWindow.ExistsReport(actualReportRDLC) ? ReportRDLC: actualReportRDLC;
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            ViewReportWindow.ShowReport(this, caption, actualReportRDLC, (pw.Result as DataSet).Tables[0],
                                new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString()),
                                new ReportParameter("P_DEGREE_IDS", string.Join(", ", f.GetDegreeIDs().Select(r=>r.ToString().PadLeft(2,'0'))))
                                }, System.Drawing.Printing.Duplex.Default, false);
                        });
            }
        }
        /// <summary>
        /// Фактический процент выполнения норм
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepActualPercentNormPieceWork_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoPieceWorkerReport("Фактический процент выполнения норм", "Rep_ActualPercentNormPieceWork.rdlc", e);
        }

        /// <summary>
        /// Фактическая стоимость часа по сдельщикам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepActualHourPricePieceWork_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoPieceWorkerReport("Фактическая стоимость часа", "Rep_ActualHourPricePieceWork.rdlc", e);
        }

        /// <summary>
        /// Зарплата на одного рабочего-сдельщика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryToAlongPieceWorker_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoPieceWorkerReport("Зарплата на одного рабочего сдельщика", "Rep_SalayToAlonePieceWorker.rdlc", e);
        }

        /// <summary>
        /// Среднесписочная численность по сдельщикам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepAVGCountPieceWorker_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoPieceWorkerReport("Среднесписочная численность сдельщиков", "Rep_AVGCountPieceWorker.rdlc", e);
        }

        /// <summary>
        /// Сверхурочные и субботние сдельщиков
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RRepOverTableHoursPieceWorker_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoPieceWorkerReport("Сверхурочные/субботние сдельщиков", "Rep_OverHoursPieceWorker.rdlc", e);
        }

        /// <summary>
        /// Отработка в чел/часах на одного человека
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepHoursForAlongPieceWorker_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoPieceWorkerReport("Отработка в чел/час для сдельщиков", "Rep_HoursForAlongPieceWorker.rdlc", e);
        }

        /// <summary>
        /// Отработка в нормочасах на одного человека
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepNormHoursForAlongPieceWorker_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoPieceWorkerReport("Отработка в нормочасах для сдельщиков", "Rep_NormHoursForAlongPieceWorker.rdlc", e);
        }

        /// <summary>
        /// Процент премии от сдельной зп по подразделению
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepPercentPremPieceSalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoPieceWorkerReport("Процент премии от сдельной ЗП", "Rep_PercentPremPieceSalary.rdlc", e);
        }

        /// <summary>
        /// Срочный отчет по группам мастера вроде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepShortBySubdivGroupMaster_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Month"), Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.Owner = Window.GetWindow(this);
            f.IsCodeDegreeEnabled = true;
            f.SelectedDegreeIDs = e.Parameter.ToString().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(t => decimal.Parse(t)).ToArray();
            if (f.ShowDialog() == true)
            {
                decimal[] degreeids = f.SelectedDegreeIDs;
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter("Rep_ShortBySubdivGroupMaster.sql", f, FilterParameter.p_subdiv_id, FilterParameter.p_date_begin, FilterParameter.p_date_end,
                    FilterParameter.p_degree_ids, FilterParameter.c);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            ViewReportWindow.ShowReport(this, "Срочный отчет по группам мастера", "RepShortBySubdiv2.rdlc", (pw.Result as DataSet).Tables[0],
                                new ReportParameter[]{ new ReportParameter("P_DATE", f.DateBegin.Value.ToShortDateString()),
                                new ReportParameter("P_DEGREE_IDS", string.Join(", ", f.GetDegreeIDs().Select(r=>r.ToString().PadLeft(2,'0'))))
                                }, System.Drawing.Printing.Duplex.Default, false);
                        });
            }
        }

        /// <summary>
        /// Срочный отчет по группам мастера и сотрудникам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepShortBySubdivGroupMasterAndEmp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Month"), Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.Owner = Window.GetWindow(this);
            f.IsCodeDegreeEnabled = true;
            f.SelectedDegreeIDs = e.Parameter.ToString().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(t => decimal.Parse(t)).ToArray();
            if (f.ShowDialog() == true)
            {
                decimal[] degreeids = f.SelectedDegreeIDs;
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter("Rep_ShortReportByEmpAndGroupMaster.sql", f, FilterParameter.p_subdiv_id, FilterParameter.p_date_begin, FilterParameter.p_date_end,
                    FilterParameter.p_degree_ids, FilterParameter.c);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            ViewReportWindow.ShowReport(this, "Срочный отчет по группам мастера", "Rep_ShortRepByEmpAndGroupMaster.rdlc", (pw.Result as DataSet).Tables[0],
                                new ReportParameter[]{ 
                                        new ReportParameter("P_DATE1", f.GetDateBegin().ToShortDateString()),
                                        new ReportParameter("P_DATE2", f.GetDateEnd().ToShortDateString()),
                                new ReportParameter("P_DEGREE_IDS", string.Join(", ", f.GetDegreeIDs().Select(r=>r.ToString().PadLeft(2,'0'))))
                                }, System.Drawing.Printing.Duplex.Default, false);
                        });
            }
        }

        /// <summary>
        /// Отчет ограниченный только  для БЗЭУ - сводный отчет по видам оплат и нормам выполненеия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepCommonEMReport_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(null, Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Month"));
            f.AllowBegin = System.Windows.Visibility.Collapsed;
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter("RepSalaryEMCommonReport.sql", f, FilterParameter.p_date, FilterParameter.p_subdiv_id, FilterParameter.c);
                oda.SelectCommand.BindByName = true;
                Microsoft.Win32.SaveFileDialog sf = new Microsoft.Win32.SaveFileDialog();
                sf.AddExtension = true;
                sf.Filter = "Файлы Excel (xlsx)|*.xlsx";
                sf.OverwritePrompt = true;
                if (sf.ShowDialog(Window.GetWindow(this)) == true)
                {
                    AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                            (s, pw) =>
                            {
                                string NewFileName = ((object[])pw.Argument)[1] as string;
                                if (File.Exists(NewFileName))
                                    File.Delete(NewFileName);
                                OracleDataAdapter a = ((object[])pw.Argument)[0] as OracleDataAdapter;
                                DataTable t = new DataTable();
                                a.Fill(t);
                                ExcelPackage ep = new ExcelPackage(new FileInfo(NewFileName));
                                ExcelWorkbook wb = ep.Workbook;
                                ExcelWorksheet ws = wb.Worksheets.Add("Сводный отчет для ЭУ"+(wb.Worksheets.Count==0?"":wb.Worksheets.Count.ToString()));
                                ws.Cells[2, 1].LoadFromDataTable(t, true, OfficeOpenXml.Table.TableStyles.Light1);
                                ep.Save();
                                pw.Result = NewFileName;
                                ep.Dispose();
                            },
                            new object[]{oda, sf.FileName}, oda.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных и формирования");
                            else
                            {
                                System.Diagnostics.Process.Start(pw.Result.ToString());
                            }
                        });
                }
            }
        }

        /// <summary>
        /// Накопительный отчет по проффессиям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepReportByPositionAccum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("year"), Model.FilterEmp.SelectedDate);
            f.IsCodeDegreeEnabled = true;
            f.SelectedDegreeIDs = e.Parameter.ToString().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(t => decimal.Parse(t)).ToArray();
            f.Owner = Window.GetWindow(this);
            f.AllowCodePos = true;
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("Rep_ReportByPosition.sql", f, FilterParameter.p_subdiv_id, FilterParameter.p_date_begin, FilterParameter.p_date_end, 
                    FilterParameter.p_degree_ids, FilterParameter.c);
                a.SelectCommand.Parameters["p_date_begin"].Value = f.GetDateBegin();
                a.SelectCommand.Parameters["p_date_end"].Value = f.GetDateEnd();
                a.SelectCommand.Parameters.Add("p_code_pos", OracleDbType.Varchar2, f.CodePos, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...",
                        a, a.SelectCommand,
                        (p, pw) =>
                        {
                            ViewReportWindow.ShowReport(this, "Отчет по профессиям", "Rep_SalaryByPositionAll.rdlc",
                                (pw.Result as DataSet).Tables[0],
                                new ReportParameter[] { 
                                        new ReportParameter("P_DATE1", f.GetDateBegin().ToShortDateString()),
                                        new ReportParameter("P_DATE2", f.GetDateEnd().ToShortDateString()),
                                        new ReportParameter("P_CODE_SUBDIV", f.CodeSubdiv),
                                        new ReportParameter("P_CODE_DEGREEDS", string.Join(", ",f.GetDegreeIDs().Select(t=>t.ToString().PadLeft(2,'0'))))});
                        });
            }
        }

        /// <summary>
        /// Отчет срочный за период по любой херне
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepShortAccumBySubdivPeriod_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoPieceWorkerReport("Срочный отчет по сдельщикам за период", "RepShortBySubdivPeriod.rdlc", e);
        }

        /// <summary>
        /// Отчет по данным из распределения затрат, группа фильтра отчетности=3008001
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepDistributionEconReport_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Year"), Model.FilterEmp.SelectedDate);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter(@"Distribution\Rep_DistributionEconReport.sql", f, FilterParameter.p_date_begin, FilterParameter.p_date_end, FilterParameter.p_subdiv_id, FilterParameter.c);
                oda.SelectCommand.Parameters.Add("p_group_code", OracleDbType.Varchar2, "3008001", ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "Учет по заказам", "Rep_DistributionEconReport.rdlc", (pw.Result as DataSet).Tables[0],
                            new ReportParameter[]{ new ReportParameter("P_DATE1", f.GetDateBegin().ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.GetDateEnd().ToShortDateString()),
                                    new ReportParameter("P_CODE_SUBDIV", f.CodeSubdiv),
                                    new ReportParameter("P_HIDE_OTOTAL", "False")
                            });
                    });
            }
        }

        /// <summary>
        /// оТчет по всем заказам подразделения-итоги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepDistributionEconReport1_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Year"), Model.FilterEmp.SelectedDate);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter(@"Distribution\Rep_DistributionEconReport.sql", f, FilterParameter.p_date_begin, FilterParameter.p_date_end, FilterParameter.p_subdiv_id, FilterParameter.c);
                oda.SelectCommand.Parameters.Add("p_group_code", OracleDbType.Varchar2, null, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "Учет по заказам", "Rep_DistributionEconReport.rdlc", (pw.Result as DataSet).Tables[0],
                            new ReportParameter[]{ new ReportParameter("P_DATE1", f.GetDateBegin().ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.GetDateEnd().ToShortDateString()),
                                    new ReportParameter("P_CODE_SUBDIV", f.CodeSubdiv),
                                    new ReportParameter("P_HIDE_OTOTAL", "True")
                            });
                    });
            }
        }

        /// <summary>
        /// Отчет по заказам в разрезе подразделений и б/счетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepDistributionEconSubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Year"), Model.FilterEmp.SelectedDate);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter(@"Distribution\Rep_DistributionEconReport.sql", f, FilterParameter.p_date_begin, FilterParameter.p_date_end, FilterParameter.p_subdiv_id, FilterParameter.c);
                oda.SelectCommand.Parameters.Add("p_group_code", OracleDbType.Varchar2, "3008010", ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "Учет по заказам", "Rep_DistributionEconReportSubdiv.rdlc", (pw.Result as DataSet).Tables[0],
                            new ReportParameter[]{ new ReportParameter("P_DATE1", f.GetDateBegin().ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.GetDateEnd().ToShortDateString()),
                                    new ReportParameter("P_CODE_SUBDIV", f.CodeSubdiv)
                            });
                    });
            }
        }

        /// <summary>
        /// Отчет по основным повременщикам 08 категория, ЗП и процент от сдельной зп
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_08DegreePiecePricey_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(Model.FilterEmp.SubdivID, Model.FilterEmp.SelectedDate.Trunc("Year"), Model.FilterEmp.SelectedDate);
            f.IsCodeDegreeEnabled = false;
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter(true,
                    "begin SALARY.SALARY_REPORT_SUBDIV.Rep08DegreePiecePrice(:p_subdiv_id, :p_date_begin, :p_date_end, :c);end;",
                    f, FilterParameter.p_date_begin, FilterParameter.p_date_end, FilterParameter.p_subdiv_id, FilterParameter.c);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "Отчет по основным повременщикам", "Rep_08DegreePiecePrice.rdlc", (pw.Result as DataSet).Tables[0],
                            new ReportParameter[]{ new ReportParameter("P_DATE1", f.GetDateBegin().ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.GetDateEnd().ToShortDateString()),
                                    new ReportParameter("P_CODE_SUBDIV", f.CodeSubdiv)
                            });
                    });
            }
        }
    }
    
}
