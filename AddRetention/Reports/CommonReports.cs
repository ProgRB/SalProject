using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Salary.Helpers;
using System.Data;
using System.Windows;
using Microsoft.Reporting.WinForms;
using Salary.View;
using LibrarySalary.Helpers;

namespace Salary.Reports
{
    class CommonReports
    {
    }
    public class ReportsAddPrem
    {
        public static void AddPremRegister(DependencyObject sender,  DateTime date, decimal? subdiv_id, object payment_type_id, object parameter)
        {
            DataTable dt = null;
            if (Signes.Show(subdiv_id, "AddPremSalaryDSV", "Выберите ответственных", 2, ref dt)== true)
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepAdditionPremSalaryRegister.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, date, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, subdiv_id, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_payment_type_id", OracleDbType.Decimal, payment_type_id, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(sender, "Формирование отчета", 
                    a, a.SelectCommand, (y, pw) =>
                    {
                        if (parameter.ToString() == "402")
                            ViewReportWindow.ShowReport(sender, string.Format("Реестр по взносам в пенс. фонды (вид оплат {0})", parameter), "Rep_AddPremRegister402.rdlc",
                            new DataTable[]{(pw.Result as DataSet).Tables[0], dt}, new ReportParameter[]{
                                            new ReportParameter("P_DATE", date.ToShortDateString())}.ToList(),
                                            System.Drawing.Printing.Duplex.Default, false);
                        else
                            ViewReportWindow.ShowReport(sender, string.Format("Реестр по взносам в пенс. фонды (вид оплат {0})", parameter), "Rep_AddPremRegister401.rdlc",
                            (pw.Result as DataSet).Tables[0], new ReportParameter[]{
                                            new ReportParameter("P_DATE", date.ToShortDateString())}.ToList(),
                                            System.Drawing.Printing.Duplex.Default, false);
                    });
            }
        }
    }

    public class ReportsEmpPayments
    {
        public static void GetEmpErrors(DependencyObject sender, decimal? subdiv_id, DateTime? date, decimal[] error_ids)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepSalaryErrors.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, subdiv_id, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, date, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_ids", OracleDbType.Array, error_ids, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
            a.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c5", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c6", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c7", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(sender, "Формирование отчета...", a, a.SelectCommand,
            (ee, pw) =>
            {
                ViewReportWindow.ShowReport(sender, "Просмотр отчета \"Возможные ошибки данных\"", "Rep_SalaryErrors.rdlc", (pw.Result as DataSet).Tables.OfType<DataTable>(),
                        new ReportParameter[] { new ReportParameter("P_DATE", date.Value.ToShortDateString()) }.ToList());
            });
        }
    }
}
