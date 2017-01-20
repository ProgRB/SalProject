using EntityGenerator;
using LibrarySalary.Helpers;
using Microsoft.Win32;
using OfficeOpenXml;
using Oracle.DataAccess.Client;
using Salary.Helpers;
using Salary.ViewModel.Salary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace Salary.View.Taxes
{
    /// <summary>
    /// Interaction logic for TaxCompanyViewer.xaml
    /// </summary>
    public partial class TaxCompanyViewer : UserControl
    {
        private TaxEmpDocumViewModel _model;
        public TaxCompanyViewer()
        {
            _model = new  TaxEmpDocumViewModel();
            InitializeComponent();

            DataContext = Model;
        }

        /// <summary>
        /// Модель представления данных для формы
        /// </summary>
        public TaxEmpDocumViewModel Model
        {
            get
            {
                return _model;
            }
        }


        private void EditCompany_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void EditCompany_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TaxCompanyEditor f = new TaxCompanyEditor((e.Parameter as TaxCompany).TaxCompanyID);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.RefreshTaxCompanies();
            }
        }

        private void AddCompany_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TaxCompanyEditor f = new TaxCompanyEditor(null);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.RefreshTaxCompanies();
            }
        }

        private void EditEmpDocum_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.CurrentDocument != null;
        }

        private void EditEmpDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TaxDocumEditor f = new TaxDocumEditor(Model.CurrentDocumentID);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog()==true)
            {
                Model.UpdateTaxDocums();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Model.UpdateTaxDocums();
        }

        private void MenuCommand_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void RepTaxesConsolidation(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter != null)
            {
                if (e.Parameter.ToString() == "Discount")
                {
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.Filter = "Файлы Excel|.xlsx";
                    string NewFileName = string.Empty;
                    if (sf.ShowDialog(Window.GetWindow(this)) == true)
                    {
                        NewFileName = sf.FileName;
                    }
                    else
                        return;
                    OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Taxes/Rep_TaxesDiscount.sql"), Connect.CurConnect);
                    oda.SelectCommand.BindByName = true;
                    oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
                    oda.SelectCommand.Parameters.Add("p_tax_company_id", OracleDbType.Decimal, Model.CurrentTaxCompanyID, ParameterDirection.Input);
                    oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                    AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                    (s, pw) =>
                    {
                        OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                        DataTable t = new DataTable();
                        a.Fill(t);
                        string real_template = @"TaxReports\Rep_TaxesDiscount.xlsx";
                        File.Copy(Connect.CurrentAppPath + @"\Reports\" + real_template, NewFileName, true);
                        ExcelPackage ep = new ExcelPackage(new FileInfo(NewFileName));
                        ExcelWorkbook wb = ep.Workbook;
                        ExcelWorksheet ws = wb.Worksheets[1];
                        ws.Cells["A1"].Value = string.Format("Отчет по налогам и вычетам");
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
                else
                if (e.Parameter.ToString() == "CodePayment")
                {
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.Filter = "Файлы Excel|.xlsx";
                    string NewFileName = string.Empty;
                    if (sf.ShowDialog(Window.GetWindow(this)) == true)
                    {
                        NewFileName = sf.FileName;
                    }
                    else
                        return;
                    OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Taxes/Rep_TaxesCodePay.sql"), Connect.CurConnect);
                    oda.SelectCommand.BindByName = true;
                    oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
                    oda.SelectCommand.Parameters.Add("p_tax_company_id", OracleDbType.Decimal, Model.CurrentTaxCompanyID, ParameterDirection.Input);
                    oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);

                    AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                    (s, pw) =>
                    {
                        OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                        DataTable t = new DataTable();
                        a.Fill(t);
                        string real_template = @"TaxReports\Rep_TaxesCodePay.xlsx";
                        File.Copy(Connect.CurrentAppPath + @"\Reports\" + real_template, NewFileName, true);
                        ExcelPackage ep = new ExcelPackage(new FileInfo(NewFileName));
                        ExcelWorkbook wb = ep.Workbook;
                        ExcelWorksheet ws = wb.Worksheets[1];
                        ws.Cells["A1"].Value = string.Format("Отчет по налогам и кодам дохода");
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
        }

        private void Expander_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Model.UpdateTaxDocums();
        }

        private void LoadTaxesDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand("begin SALARY.Load_SUBDV_TAX_DOCUMENT(p_tax_company_id=>:p_tax_company_id, p_date=>:p_date, p_subdiv_id=>:p_subdiv_id); end;", Connect.CurConnect);
            cmd.Parameters.Add("p_tax_company_id", OracleDbType.Decimal, Model.CurrentTaxCompanyID, ParameterDirection.Input);
            cmd.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            cmd.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, 0, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Загрузка данных", cmd,
                (p, pw) =>
                {
                    MessageBox.Show(Window.GetWindow(this), "Загрузка завершена");
                });
        }
    }
}
