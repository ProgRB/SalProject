using EntityGenerator;
using LibrarySalary.Helpers;
using Microsoft.Win32;
using OfficeOpenXml;
using Oracle.DataAccess.Client;
using Salary.Helpers;
using Salary.Reports;
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

        private void AddCompany_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute= ControlRoles.GetState(e.Command);
        }

        private void EditCompany_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && e.Parameter!=null;
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
            OracleCommand cmd = new OracleCommand("begin SALARY.SALARY_TAXES_PKG.LOAD_SUBDV_TAX_DOCUMENT(p_tax_company_id=>:p_tax_company_id, p_date=>:p_date, p_subdiv_id=>:p_subdiv_id); end;", Connect.CurConnect);
            cmd.Parameters.Add("p_tax_company_id", OracleDbType.Decimal, Model.CurrentTaxCompanyID, ParameterDirection.Input);
            cmd.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            cmd.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, 0, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Загрузка данных", cmd,
                (p, pw) =>
                {
                    MessageBox.Show(Window.GetWindow(this), "Загрузка завершена");
                });
        }

        private void AddTaxDocum_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.CurrentCompany!=null;
        }

        private void AddTaxDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TaxDocumEditor f = new TaxDocumEditor(null);
            f.Model.TaxCompanyID = Model.CurrentTaxCompanyID;
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateTaxDocums();
            }
        }

        private void DeleteTaxEmpDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный документ?", "Удаление данных", MessageBoxButton.YesNo)== MessageBoxResult.Yes)
            {
                OracleTransaction tr = Connect.CurConnect.BeginTransaction();
                try
                {
                    OracleCommand cmd = TaxEmpDocum.GetModelAdapter<TaxEmpDocum>().DeleteCommand;
                    cmd.Parameters["p_TAX_EMP_DOCUM_ID"].SourceColumn = string.Empty;
                    cmd.Parameters["p_TAX_EMP_DOCUM_ID"].Value = Model.CurrentDocumentID;
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                    Model.UpdateTaxDocums();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка удалениия данных");
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Model.SetChecked((sender as CheckBox).IsChecked);
        }

        private void Upload2NDFL_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show($"Начать формирование данных по справке 2-НДФЛ в XML формат({Model.CheckedDocumIDs.Length} записей)?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OracleDataAdapter a = new OracleDataAdapter("BEGIN SALARY.salary_xml_file.NO_NDFL2_5_04_1_TAX_DOCUM(:t, :c); END;", Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                a.SelectCommand.Parameters.Add("t", OracleDbType.Array, Model.CheckedDocumIDs, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных XML...", a, a.SelectCommand,
                    (p, pw) =>
                    {
                        SaveFileDialog sf = new SaveFileDialog();
                        sf.Filter = "Файлы XML (*.xml)|*.xml";
                        var c = Model.CurrentCompany;
                        var fileName = $"NO_NDFL2_{c.Kpp.Substring(0,4)}_{c.Kpp.Substring(0, 4)}_{c.Inn}{c.Kpp}_{DateTime.Today.ToString("yyyyMMdd")}_1";
                        sf.FileName = string.Format(fileName, DateTime.Today);
                        if (sf.ShowDialog(Window.GetWindow(this)) == true)
                        {
                            try
                            {
                                File.WriteAllLines(sf.FileName, (pw.Result as DataSet).Tables[0].Rows.OfType<DataRow>().Select(r => r[0].ToString()), Encoding.GetEncoding(1251));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Ошибка создания файла");
                            }
                        }
                    });
            }
        }

        /// <summary>
        /// Процедура перераспределения отрицательных доходов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RelocateNegativSalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы действительно хотите переспределить отрицательный доход для выбранных строк? ({Model.CheckedDocumIDs.Length} записей)",
                "Изменение данных", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OracleDataAdapter a = new OracleDataAdapter("BEGIN SALARY.SALARY_TAXES_PKG.RELOCATE_DOCUM_NEGATIVE(:t, :c); END;", Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                a.SelectCommand.Parameters.Add("t", OracleDbType.Array, Model.CheckedDocumIDs, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных отчета...", a, a.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "Протокол изменения данных в справках", @"TaxReports\Rep_TaxesAllocateControl.rdlc", (pw.Result as DataSet).Tables[0], null);
                    });
            }

        }

        /// <summary>
        /// Сводный отчет по данным налогов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepTaxesDocumCommon_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(Window.GetWindow(this), "Данные будут сформированы на основе документов, показанных на экране в данный момент", "Предупреждение для пользователя");
            OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Taxes/Rep_TaxesDocumCommon.sql"), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_tax_docum_ids", OracleDbType.Array, 
                    Model.EmpTaxDocumSource.Select(r=>r.Row.Field2<Decimal>("TAX_EMP_DOCUM_ID")).ToArray() , ParameterDirection.Input).UdtTypeName="APSTAFF.TYPE_TABLE_NUMBER";
            a.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            //a.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            //a.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...", a, a.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "Сводный отчет по справкам", @"TaxReports\Rep_TaxCommonData.rdlc", (pw.Result as DataSet).Tables.Cast<DataTable>(), null);
                    }
                );
        }


        private void Print_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            /*AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета. Ожидайте...",
                   (s, pw) =>
                   {
                       DataTable t = Model.EmpTaxDocumView.ToTable();
                       using (ExcelPackage ep = new ExcelPackage(new FileInfo(NewFileName)))
                       {
                           ExcelWorkbook wb = ep.Workbook;
                           ExcelWorksheet ws = wb.Worksheets[1];
                           ws.Cells["A1"].Value = string.Format("Распределение затрат за {0:MMMM yyyy} по подразделению {1}", Model.SelectedDate, Model.CodeSubdiv)
                               + (string.IsNullOrEmpty(Model.OrderFilter) ? "" : " с фильтром заказа " + Model.OrderFilter);
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
                   });*/
        }
    }
}
