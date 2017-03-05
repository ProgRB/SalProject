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
using Salary.ViewModel;
using System.Data;
using System.ComponentModel;
using Oracle.DataAccess.Client;
using Salary.Helpers;
using Salary.Reports;
using Microsoft.Reporting.WinForms;
using System.IO;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for EmpAccounts.xaml
    /// </summary>
    public partial class EmpAccounts : UserControl
    {
        public EmpAccounts()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EmpAccounts_Loaded);

        }

        void EmpAccounts_Loaded(object sender, RoutedEventArgs e)
        {
            Model.PropertyChanged += new PropertyChangedEventHandler(Model_PropertyChanged);
        }

        void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ArchivRetent")
                ((CollectionViewSource)this.FindResource("RetentSource")).View.Refresh();
        }

        public EmpAccountsViewModel Model
        {
            get
            {
                if (this.IsLoaded)
                    return (EmpAccountsViewModel)this.FindResource("Model");
                else return null;
            }
        }

        private void Filter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                filterBindingGroup.UpdateSources();
                Model.UpdateEmpList();
            }
        }

        private void btFilter_Click(object sender, RoutedEventArgs e)
        {
            filterBindingGroup.UpdateSources();
            Model.UpdateEmpList();
        }

        private void AddAccount_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.EmpView.SelectedItem != null;
        }

        private void AddAccount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ClientAccountEditor f = new ClientAccountEditor(Model.EmpView.SelectedItem.Row.Field2<Decimal?>("TRANSFER_ID"));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateClientAccount();
            }
        }

        private void EditAccount_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.ClientAccountView.SelectedItem != null && Model.EmpView.SelectedItem != null;
        }

        private void EditAccount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ClientAccountEditor f = new ClientAccountEditor(Model.EmpView.SelectedItem.Row.Field2<Decimal?>("TRANSFER_ID"), Model.ClientAccountView.SelectedItem.Row.Field2<Decimal>("CLIENT_ACCOUNT_ID"));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateClientAccount();
            }
        }


        OracleCommand cmdDeleteAccount;
        private void DeleteAccount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный счет без возможности восстановления?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (cmdDeleteAccount == null)
                {
                    cmdDeleteAccount = new OracleCommand(string.Format("begin {0}.CLIENT_ACCOUNT_DELETE(:p_client_account_id);end;", Connect.SchemaSalary), Connect.CurConnect);
                    cmdDeleteAccount.Parameters.Add("p_client_account_id", OracleDbType.Decimal, ParameterDirection.Input);
                    cmdDeleteAccount.BindByName = true;
                }
                cmdDeleteAccount.Parameters[0].Value = Model.ClientAccountView.SelectedItem.Row.Field2<Decimal?>("client_account_id");
                if (cmdDeleteAccount.TryExecuteNonQuerWithTransaction(Connect.CurConnect))
                    Model.UpdateClientAccount();
            }
        }

        private void AddRetent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.EmpView.SelectedItem != null;
        }

        private void AddRetent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RetentionEditor f = new RetentionEditor(Model.EmpView.SelectedItem["transfer_id"], null);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateEmpRetent();
            }
        }

        private void EditRetent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.EmpView.SelectedItem != null && Model.EmpRetent.SelectedItem != null;
        }

        private void EditRetent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RetentionEditor f = new RetentionEditor(Model.EmpView.SelectedItem["transfer_id"], Model.EmpRetent.SelectedItem["RETENTION_ID"]);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateEmpRetent();
            }
        }

        OracleCommand cmdDeleteRetent;
        private void DeleteRetent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cmdDeleteRetent == null)
            {
                cmdDeleteRetent = new OracleCommand(string.Format("begin {0}.RETENTION_DELETE(:p_retention_id);end;", Connect.SchemaSalary), Connect.CurConnect);
                cmdDeleteRetent.BindByName = true;
                cmdDeleteRetent.Parameters.Add("p_retention_id", OracleDbType.Decimal, ParameterDirection.Input);
            }
            cmdDeleteRetent.Parameters["p_retention_id"].Value = Model.EmpRetent.SelectedItem["RETENTION_ID"];
            if (cmdDeleteRetent.TryExecuteNonQuerWithTransaction(Connect.CurConnect))
            {
                Model.UpdateEmpRetent();
            }
        }

        private void RetentSource_Filter(object sender, FilterEventArgs e)
        {
            if (Model.ArchivRetent)
            {
                e.Accepted = true;
            }
            else
            {
                DataRowView r = e.Item as DataRowView;
                DateTime d1 = r["DATE_START_RET"] == DBNull.Value ? DateTime.MinValue : r.Row.Field2<DateTime>("DATE_START_RET"),
                    d2 = r["DATE_END_RET"] == DBNull.Value ? DateTime.MaxValue : r.Row.Field2<DateTime>("DATE_END_RET");
                DateTime t1 = new DateTime(Model.EmpProvider.SelectedDate.Year, Model.EmpProvider.SelectedDate.Month, 1);
                DateTime t2 = t1.AddMonths(1).AddSeconds(-1);
                e.Accepted = t1 <= d2 && t2 >= d1;
            }
        }

        private void ViewEmpRetentAccount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EmpRetentAccountEditor f = new EmpRetentAccountEditor((Model.EmpView.SelectedItem as DataRowView)["TRANSFER_ID"]);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateEmpRetent();
            }
        }

        private void ViewEmpSalaryRetent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EmpSalaryAccountRetent f = new EmpSalaryAccountRetent(Model.EmpView.SelectedItem["TRANSFER_ID"]);
            f.Owner = Window.GetWindow(this);
            f.ShowDialog();
        }

        private void ReportSubdiv_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void Rep_AddPremiumCatalog_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            decimal k = (decimal)AppDataSet.Tables["PAYMENT_TYPE"].Compute("MAX(PAYMENT_TYPE_ID)", "CODE_PAYMENT=" + e.Parameter.ToString());
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepAdditionPremSalaryCatalog.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.EmpProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.EmpProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_payment_type_id", OracleDbType.Decimal, k, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета", (y, pw) =>
                {
                    OracleDataAdapter d = pw.Argument as OracleDataAdapter;
                    DataTable t = new DataTable();
                    d.Fill(t);
                    pw.Result = t;
                },
                a, a.SelectCommand, (y, pw) =>
                    {
                        if (pw.Cancelled)
                            return;
                        else if (pw.Error != null)
                            MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования");
                        else
                            ViewReportWindow.ShowReport(this, string.Format("Справочник по взносам в пенс. фонды (вид оплат {0})", e.Parameter), "Rep_AddPremCatalog.rdlc",
                                pw.Result as DataTable, new ReportParameter[]{
                                            new ReportParameter("P_DATE", Model.EmpProvider.SelectedDate.ToShortDateString()), 
                                            new ReportParameter("CODE_SUBDIV", subdivSelector.CodeSubdiv), 
                                            new ReportParameter("CODE_PAYMENT", e.Parameter.ToString())}.ToList(), System.Drawing.Printing.Duplex.Default, false);
                    });
        }

        private void Rep_AddPremiumRegister_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            decimal k = (decimal)AppDataSet.Tables["PAYMENT_TYPE"].Compute("MAX(PAYMENT_TYPE_ID)", "CODE_PAYMENT=" + e.Parameter.ToString());
            Salary.Reports.ReportsAddPrem.AddPremRegister(this, Model.EmpProvider.SelectedDate, Model.EmpProvider.SubdivID, k, e.Parameter);
        }

        private void RepSalaryErrors_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ReportsEmpPayments.GetEmpErrors(this, 0, Model.EmpProvider.SelectedDate, new decimal[] { 6 });
        }

        private void RepUploadTxtClientAccount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(string.Format("Выгрузить справочник по состоянию на {0}?", Model.EmpProvider.SelectedDate.ToString("MMMM yyyy")), "Выгрузка данных", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectUnloadTxtClientAccount.sql"), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.EmpProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_code_payment", OracleDbType.Varchar2, e.Parameter, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных",
                    (s, pw) =>
                    {
                        OracleDataAdapter aa = pw.Argument as OracleDataAdapter;
                        DataTable t = new DataTable();
                        aa.Fill(t);
                        pw.Result = t;
                    },
                        a, a.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled)
                                return;
                            else
                                if (pw.Error != null)
                                    MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования данных");
                                else
                                {
                                    System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
                                    sf.Filter = "Текстовые файлы (*.txt)|*.txt";
                                    sf.InitialDirectory = Connect.parameters["NSPR_DIR"];
                                    sf.FileName = "vop_" + e.Parameter.ToString();
                                    if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
                                    {
                                        try
                                        {
                                            File.WriteAllLines(sf.FileName, (pw.Result as DataTable).Rows.OfType<DataRow>().Select(r => r[0].ToString()).ToArray());
                                            MessageBox.Show(string.Format("Файл сформирован. Кол-во записей {0}", (pw.Result as DataTable).Rows.Count), "Успешно сформирован");
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message, "Ошибка записи файла");
                                        }
                                    }
                                }
                        });
            }
        }

        private void RepLoadTxtClientAccount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog sf = new System.Windows.Forms.OpenFileDialog();
            sf.Filter = "Текстовые файлы (*.txt)|*.txt";
            sf.InitialDirectory = Connect.parameters["EC1036Dir"];
            string s = e.Parameter.ToString();
            sf.FileName = s == "487" ? string.Format("avsber{0}.txt", Model.EmpProvider.SelectedDate.ToString("MM")) :
                    string.Format("s488{0}.txt", Model.EmpProvider.SelectedDate.ToString("ddMM"));
            if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] st = null;
                try
                {
                    st = File.ReadAllLines(sf.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка чтения файла");
                    return;
                }
                OracleCommand cmd = new OracleCommand(string.Format("begin {1}.LoadSalDataToDB(:p_st);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd.BindByName = true;
                cmd.Parameters.Add("p_st", OracleDbType.Varchar2, st, ParameterDirection.Input);
                cmd.ArrayBindCount = st.Length;
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Загрузка данных",
                    (ss, pw) =>
                    {
                        OracleTransaction tr = Connect.CurConnect.BeginTransaction();
                        try
                        {
                            (pw.Argument as OracleCommand).ExecuteNonQuery();
                            tr.Commit();
                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            throw ex;
                        }
                    },
                    cmd, cmd,
                    (ss, pw) =>
                    {
                        if (pw.Cancelled) return;
                        else if (pw.Error != null)
                            MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка загрузки данных");
                        else
                            MessageBox.Show(string.Format("Количество записей загружено: {0}", st.Length));
                    });
            }
        }

        private void Rep_EmpAccountNote_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_EmpAccountsNote.sql"), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_worker_id", OracleDbType.Decimal, Model.EmpView.SelectedItem["WORKER_ID"], ParameterDirection.Input);
            try
            {
                DataTable t = new DataTable();
                a.Fill(t);
                ViewReportWindow.ShowReport(this, "Счета сотрудника", "Rep_EmpAccountNote.rdlc", t, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования данных");
            }
        }

        private void RepAllTransferredSum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("RepAllTransfersSum.sql"), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.EmpProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("C", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных",
                 a, a.SelectCommand,
                    (s, pw) =>
                    {
                        if (pw.Cancelled) return;
                        else if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования");
                        else
                            ViewReportWindow.ShowReport(this, "Сводный отчет", "Rep_AllTransferredSum.rdlc", (pw.Result as DataSet).Tables[0], new ReportParameter[] { new ReportParameter("P_DATE", Model.EmpProvider.SelectedDate.ToShortDateString()) });
                    });
        }

        private void RepTypeBankEmpTransfer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
            {
                OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("RepTypeBankEmpTransfer2.sql"), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, Model.EmpProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, Model.EmpProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.EmpProvider.SubdivID, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных",
                     a, a.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            else if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования");
                            else
                                ViewReportWindow.ShowReport(this, "Отчет Кто куда ЗП перечисляет (банки)", "Rep_TypeBankEmpTransfer2.rdlc", (pw.Result as DataSet).Tables[0],
                                    new ReportParameter[] { 
                                    new ReportParameter("P_DATE", Model.EmpProvider.SelectedDate.ToShortDateString()) ,
                                    new ReportParameter("P_CODE_SUBDIV", Model.EmpProvider.CodeSubdiv) 
                                });
                        });
            }
            else
            {
                OracleDataAdapter a = new OracleDataAdapter("begin SALARY.SALARY_TRANSFER.SelectTypeBankEmpFull(:p_date_begin, :p_date_end, :p_subdiv_id, :c);end;", Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, Model.EmpProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, Model.EmpProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.EmpProvider.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных",
                    a, a.SelectCommand,
                        (s, pw) =>
                        {
                            ViewReportWindow.ShowReport(this, "Отчет по перечислениям сотрудников", "Rep_TypeBankEmpTransferFull.rdlc", (pw.Result as DataSet).Tables[0],
                                new ReportParameter[] { 
                                new ReportParameter("P_DATE", Model.EmpProvider.SelectedDate.ToShortDateString()) ,
                                new ReportParameter("P_CODE_SUBDIV", Model.EmpProvider.CodeSubdiv) 
                            });
                        });
            }
        }

        /// <summary>
        /// Перечисление по реестрам для конкретных сотрудников за период
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepEmpTransferByRegisters_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RepFilterByEmp f = new RepFilterByEmp(Model.EmpView.ToList(), Model.EmpProvider.SubdivID, Model.EmpProvider.SelectedDate);
            f.IsSubdivAllowed = false;
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("RepEmpTransferByRegister.sql"), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_transfer_ids", OracleDbType.Array, f.SelectedRows.OfType<DataRowView>().Select(t => t.Row.Field2<decimal>("WORKER_ID")).ToArray(), ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных",
                     a, a.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            else if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования");
                            else
                                ViewReportWindow.ShowReport(this, "Отчет Перечисление за период по сотрудникам", "Rep_EmpTransferSubByPeriod.rdlc", (pw.Result as DataSet).Tables[0],
                                    new ReportParameter[] { 
                                        new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()) ,
                                        new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString())
                                    });
                        });
            }
        }

        /// <summary>
        /// Копирует для совместителя его данные перечисления по основному месту работы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyAccountsFromMainWork_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand("begin SALARY.CLIENT_ACCOUNT_COPY_TOCOMB(:p_transfer_id);end;", Connect.CurConnect);
            cmd.BindByName = true;
            cmd.Parameters.Add("p_transfer_id", OracleDbType.Decimal, Model.EmpView.SelectedItem["TRANSFER_ID"], ParameterDirection.Input);
            try
            {
                cmd.ExecuteNonQuery();
                Model.UpdateEmpRetent();
                MessageBox.Show(Window.GetWindow(this), "Успешно выполнено", "Зарплата предприятия", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Window.GetWindow(this), ex.GetFormattedException(), "Ошибка обновления данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditRetentSignComb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.EmpView.SelectedItem != null
                && Model.EmpView.SelectedItem["SIGN_COMB"] != DBNull.Value;
        }

        /// <summary>
        /// Отчет по изменениям в 401 402 взносамх по сравнению с пред. месяцем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_CompareRetent401402_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter("begin SALARY.SALARY_TRANSFER.ComparePFRRetentsReport(:p_date, :c); end;", Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.EmpProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета по изменениям", a, a.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Изменения за период", @"ClientAccounts/Rep_ComparePFRRetent.rdlc", (pw.Result as DataSet).Tables[0],
                        new ReportParameter[] { new ReportParameter("P_DATE", Model.EmpProvider.SelectedDate.ToShortDateString()) });
                });
        }
    }



    public class PeriodToExpandedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (Array.TrueForAll(values, t => t != null && t != DBNull.Value && t!=DependencyProperty.UnsetValue))
            { 
                return CheckForPeriod(values[0] as IEnumerable<object>, values[1]);
            }
            else return false;
        }

        public string FirstProperty
        {
            get;set;
        }
        public string SecondProperty
        {
            get;set;
        }

        public bool CheckForPeriod(IEnumerable<object> collection, object value)
        { 
            foreach( object t in collection)
            {
                if (t is CollectionViewGroup)
                {
                    if (CheckForPeriod((t as CollectionViewGroup).Items, value))
                        return true;
                }
                else 
                {
                    PropertyDescriptor pdesc = TypeDescriptor.GetProperties(t)[FirstProperty];
                    if (pdesc.PropertyType== typeof(DateTime))
                    {
                        object val1= pdesc.GetValue(t), val2=TypeDescriptor.GetProperties(t)[SecondProperty].GetValue(t);
                        if (((DateTime)value) >= (val1 == DBNull.Value ? DateTime.MinValue : (DateTime)val1)
                            && ((DateTime)value)<=(val2==DBNull.Value?DateTime.MaxValue : (DateTime?)val2))
                            return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
