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
using System.Data;
using System.ComponentModel;
using System.Threading;
using Xceed.Wpf.DataGrid;
using Salary.Reports;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Data.OleDb;
using Salary.Helpers;
using SForms = System.Windows.Forms;
using System.Collections.ObjectModel;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for CartularyViewer.xaml
    /// </summary>
    public partial class CartularyViewer : UserControl, INotifyPropertyChanged
    {
        OracleDataAdapter odaCartulary, odaCartPaid, odaFileTransferData;
        DataSet ds;
        DataView dvCartulary, dvCartPaid;
        public CartularyViewer()
        {
            try
            {
                if (DesignerProperties.GetIsInDesignMode(this))
                    return;
                ds = new DataSet();
                ds.Tables.Add("Cartulary_Paid");
                odaCartulary = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectCartularyView.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                odaCartulary.SelectCommand.BindByName = true;
                odaCartulary.SelectCommand.Parameters.Add("p_type_cartulary_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                odaCartulary.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
                odaCartulary.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                odaCartulary.TableMappings.Add("Table", "Cartulary");

                odaCartPaid = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectCartPaidView.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                odaCartPaid.SelectCommand.BindByName = true;
                odaCartPaid.SelectCommand.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                odaCartPaid.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                odaCartPaid.TableMappings.Add("Table", "Cartulary_Paid");

                odaFileTransferData = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectFormatFileBank.sql"), Connect.CurConnect);
                odaFileTransferData.TableMappings.Add("Table", "FORMAT_FILE_BANK");

                new OracleDataAdapter(Queries.GetQueryWithSchema(@"ClientAccountTransfer\SelectCartularyTypeAccess.sql"), Connect.CurConnect).Fill(ds, "TYPE_CARTULARY");

                InitializeComponent();

                this.DataContext = this;
                this.PropertyChanged += new PropertyChangedEventHandler(CartularyViewer_PropertyChanged);
            }
            catch { }
        }

        void CartularyViewer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedDate" || e.PropertyName == "TypeCartularyID")
                UpdateCartularyView();
        }

        private void AddCartulary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void AddCartulary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CartularyEditor f = new CartularyEditor(null);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                UpdateCartularyView();
            }
        }

        /// <summary>
        /// Источник данных типы реестров
        /// </summary>
        public DataView TypeCartularySource
        {
            get
            {
                return new DataView(ds.Tables["TYPE_CARTULARY"], "", "SORT_CARTULARY", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// источник данных - список реестров
        /// </summary>
        public DataView CartularySource
        {
            get
            {
                return dvCartulary;
            }
            set
            {
                dvCartulary = value;
                OnPropertyChanged("CartularySource");
            }
        }

        /// <summary>
        /// Источник данных для выплат по рееестру
        /// </summary>
        public DataView CartularyPaidSource
        {
            get
            {
                return dvCartPaid;
            }
            set
            {
                dvCartPaid = value;
                object c = dpPaidData.FindResource("cvsPaidCartulary");
                if (c != null && c is DataGridCollectionViewSource)
                {
                    DataGridCollectionViewSource dgc = c as DataGridCollectionViewSource;
                    if (dgc != null && dgc.View != null)
                    {
                        ObservableCollection<GroupDescription> groupings = dgc.View.GroupDescriptions;
                        dgc.View.Refresh();
                        if (groupings != null)
                        {
                            dgc.View.GroupDescriptions.Clear();
                            foreach (var t in groupings)
                                dgc.View.GroupDescriptions.Add(t);
                        }
                    } 
                }
            }
        }

        DateTime? _selecteDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        /// <summary>
        /// Выбранная дата для формирования и фильтов реестров
        /// </summary>
        [OracleParameterMapping(ParameterName = "p_date")]
        public DateTime? SelectedDate
        {
            get
            {
                return _selecteDate;
            }
            set
            {
                _selecteDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        decimal? _subdiv_id = 0;
        /// <summary>
        /// Подразделение для фильтра реестров
        /// </summary>
        [OracleParameterMapping(ParameterName = "p_subdiv_id")]
        public decimal? SubdivID
        {
            get { return _subdiv_id; }
            set { _subdiv_id = value; OnPropertyChanged("SubdivID"); }
        }


        object _typeCartularyID = 1;
        /// <summary>
        /// Тип реестра для фильтра и отчетов
        /// </summary>
        [OracleParameterMapping(ParameterName="p_type_cartulary_id")]
        public object TypeCartularyID
        {
            get { return _typeCartularyID; }
            set { _typeCartularyID = value; OnPropertyChanged("TypeCartularyID"); }
        }

        bool _isBusy = false;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged("IsBusy"); }
        }


        /// <summary>
        /// Обновить список реестров в просмотре
        /// </summary>
        private void UpdateCartularyView()
        {
            object curritem = CurrentCartularyID;
            if (ds.Tables.Contains("Cartulary"))
                ds.Tables["Cartulary"].Clear();
            odaCartulary.SelectCommand.SetParameters(this);
            odaCartulary.Fill(ds);
            if (dvCartulary == null)
            {
                CartularySource = new DataView(ds.Tables["Cartulary"], "", "DATE_CARTULARY, DATE_CREATE", DataViewRowState.CurrentRows);
            }
            if (curritem!=null)
                CurrentCartulary = dvCartulary.OfType<DataRowView>().Where(r=>r["CARTULARY_ID"].Equals(curritem)).FirstOrDefault();
        }

        private bool _isLoadData= false;
        public bool IsLoadCartularyData
        {
            get
            {
                return _isLoadData;
            }
            set
            {
                _isLoadData = value;
                OnPropertyChanged("IsLoadCartularyData");
            }
        }

        /// <summary>
        /// Обновить список выплат по реестру
        /// </summary>
        /// <param name="cartulary_id"></param>
        private void UpdateCartPaidView(object cartulary_id)
        {
            IsLoadCartularyData = true;
            ds.Tables["Cartulary_Paid"].Rows.Clear();
            
            odaCartPaid.SelectCommand.Parameters["p_cartulary_id"].Value = cartulary_id;
            BackgroundWorker bw = new BackgroundWorker();
            object c = dpPaidData.FindResource("cvsPaidCartulary");
            ObservableCollection<GroupDescription> groupings = null;
            DataGridCollectionViewSource dgc =null;
            if (c != null && c is DataGridCollectionViewSource)
            {
                dgc = c as DataGridCollectionViewSource;
                if (dgc != null && dgc.View != null && dgc.View.GroupDescriptions.Count>0)
                {
                    groupings = new ObservableCollection<GroupDescription>();
                    foreach (var t in dgc.View.GroupDescriptions)
                        groupings.Add(t);
                }
            }
            bw.DoWork += (e, oe) =>
                {
                    odaCartPaid.Fill(ds);
                    if (!ds.Tables["Cartulary_Paid"].Columns.Contains("SHOW_SUM"))
                    {
                        ds.Tables["Cartulary_Paid"].Columns.Add("SHOW_SUM", typeof(string), string.Format("IIF(1={0}, PAID_SUM, -1)", GrantedRoles.CheckRole("SALARY_CARTULARY_EDIT") || ControlRoles.GetState("ViewCartularyTransfer") ? 1 : 0));
                    }
                };
            bw.RunWorkerCompleted += (e, ee) => 
                { 
                    IsLoadCartularyData = false;
                    if (dvCartPaid == null)
                    {
                        CartularyPaidSource = new DataView(ds.Tables["Cartulary_Paid"], "", "CODE_SUBDIV, PER_NUM", DataViewRowState.CurrentRows);
                    }
                    OnPropertyChanged("CartularyPaidSource");
                    if (groupings != null)
                    {
                        dgc.GroupDescriptions.Clear();
                        foreach (var t in groupings)
                            dgc.GroupDescriptions.Add(t);
                    }
                };
            bw.RunWorkerAsync();            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void FormCartulary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && (e.Parameter == null || e.Parameter.ToString() == "1" && CurrentCartulary != null);
        }

        private void CreateAutoCart_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(string.Format(e.Parameter==null?"Сформировать новый реестр '{0}' за {1:MMMM yyyy}?":"Доформировать выбранный реестр?", (cbTypeCartulary.SelectedItem as DataRowView)["TYPE_CARTULARY_NAME"], SelectedDate.Value), "Зарплата предрприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OracleCommand cmd = new OracleCommand(string.Format("begin {1}.CREATE_AUTO_CARTULARY(:p_cartulary_id, :p_type_cartulary_id, :p_subdiv_id, :p_date_begin, :p_date_end);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd.BindByName = true;
                cmd.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, e.Parameter==null? null:CurrentCartularyID, ParameterDirection.InputOutput);
                cmd.Parameters.Add("p_type_cartulary_id", OracleDbType.Decimal, TypeCartularyID, ParameterDirection.Input);
                cmd.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, SubdivID, ParameterDirection.Input);
                cmd.Parameters.Add("p_date_begin", OracleDbType.Date, SelectedDate, ParameterDirection.Input);
                cmd.Parameters.Add("p_date_end", OracleDbType.Date, null, ParameterDirection.Input);
                BackgroundWorker bw = new BackgroundWorker();
                bw.RunWorkerCompleted +=(s, pw) =>
                    {
                        IsBusy = false;
                        if (pw.Cancelled) return;
                        else if (pw.Error!=null)
                            MessageBox.Show(Window.GetWindow(this), pw.Error.GetFormattedException(), "Ошибка формирования реестра");
                        else
                            UpdateCartularyView();
                    };
                bw.DoWork+=(s, pw)=>
                    {
                        OracleCommand c = pw.Argument as OracleCommand;
                        OracleTransaction tr = Connect.CurConnect.BeginTransaction();
                        try
                        {
                            c.ExecuteNonQuery();
                            tr.Commit();
                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            throw ex;
                        }
                    };
                IsBusy = true;
                bw.RunWorkerAsync(cmd);
            }
        }

        private void EditCartulary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && dgCartulary != null && CurrentCartularyID != null;
        }

        private void EditCartulary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CartularyEditor f = new CartularyEditor(CurrentCartularyID);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                UpdateCartularyView();
            }
        }

        private void DeleteCartulary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный реестр без возможности восстановления?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                OracleCommand cmd = new OracleCommand(string.Format("begin {1}.CARTULARY_DELETE(:p_cartulary_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
                OracleTransaction tr = Connect.CurConnect.BeginTransaction();
                try
                {
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                    UpdateCartularyView();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления");
                }
            }
        }

        private void AddPaid_canExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && CurrentCartularyID != null;
        }

        private void AddPaid_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CartPaidEditor f = new CartPaidEditor(CurrentCartularyID, null, (Decimal)TypeCartularyID);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                UpdateCartPaidView(CurrentCartularyID);
            }
        }

        private object CurrentCartularyPaidID
        {
            get
            {
                if (dpPaidData != null)
                {
                    object t = dpPaidData.FindResource("cvsPaidCartulary");
                    DataGridCollectionViewSource c = t as DataGridCollectionViewSource;
                    if (c != null && c.View != null && c.View.CurrentItem != null)
                        return (c.View.CurrentItem as DataRowView)["CARTULARY_PAID_ID"];
                    else return null;
                }
                else return null;
            }
        }

        private decimal? CurrentCartularyID
        {
            get
            {
                if (CurrentCartulary != null)
                    return CurrentCartulary.Row.Field2<Decimal?>("CARTULARY_ID");
                else return null;
            }
        }

        DataRowView _currentCartulary;
        public DataRowView CurrentCartulary
        {
            get
            {
                return _currentCartulary;
            }
            set
            {
                if (_currentCartulary != value)
                {
                    _currentCartulary = value;
                    OnPropertyChanged("CurrentCartulary");
                    UpdateCartPaidView(CurrentCartularyID);
                }
            }
        }

        private DataRowView CurrentTypeCartulary
        {
            get
            {
                var t = from DataRowView r in TypeCartularySource
                        where r.Row.Field2<Decimal>("TYPE_CARTULARY_ID") == (decimal?)TypeCartularyID
                        select r;
                return t.FirstOrDefault();
            }
        }

        private void EditPaid_canExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && CurrentCartularyPaidID != null;
        }

        private void EditPaid_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CartPaidEditor f = new CartPaidEditor(CurrentCartularyID, CurrentCartularyPaidID, (decimal)TypeCartularyID);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                UpdateCartPaidView(CurrentCartularyID);
            }
        }

        private OracleCommand cmd_deletePaid;

        private void DeletePayment_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && dgxcPaid.SelectedItems != null && dgxcPaid.SelectedItems.Count > 0;
        }
        private void DeletePaid_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(string.Format("Удалить выбранные строки из реестра ({0} записей)?", dgxcPaid.SelectedItems.Count) , "Зарплата Предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (cmd_deletePaid == null)
                {
                    cmd_deletePaid = new OracleCommand(string.Format("begin {1}.CARTULARY_PAID_DELETE(:p_cartulary_paid_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                    cmd_deletePaid.Parameters.Add("p_cartulary_paid_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                }
                decimal?[] values = dgxcPaid.SelectedItems.OfType<DataRowView>().Select(r=>r.Row.Field2<Decimal?>("CARTULARY_PAID_ID")).ToArray();
                cmd_deletePaid.ArrayBindCount = values.Length;
                cmd_deletePaid.Parameters["p_cartulary_paid_id"].Value = values;
                OracleTransaction tr = Connect.CurConnect.BeginTransaction();
                try
                {
                    cmd_deletePaid.ExecuteNonQuery();
                    tr.Commit();
                    object t = dpPaidData.FindResource("cvsPaidCartulary");
                    DataRowView[] ar= dgxcPaid.SelectedItems.OfType<DataRowView>().ToArray();
                    DataGridCollectionViewSource c = t as DataGridCollectionViewSource;
                    foreach (DataRowView r in ar)
                        r.Delete();
                    //UpdateCartPaidView(CurrentCartularyID);
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления");
                }
            }
        }

        /// <summary>
        /// Формирование отчет по реестру алиментов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepAlimonyTransfers_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SignesRecord[] sr = null;
            if (Signes.Show(0, "RepAlimonyRegister", "Укажите ответственные лица", 2, ref sr, Window.GetWindow(this)) == true)
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery(@"RepAlimonyRegister.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                DataTable t = new DataTable();
                a.Fill(t);
                ViewReportWindow.ShowReport(this, "\"Реестр перечисления алиментов\"", @"Alimony\Rep_AlimonyRegister.rdlc", t, 
                    new ReportParameter[] { 
                                new ReportParameter("P_DATE", SelectedDate.Value.ToShortDateString()),
                                new ReportParameter("P_FIO1", sr[0].EmpName),
                                new ReportParameter("P_FIO2", sr[1].EmpName),
                                new ReportParameter("P_POS1", sr[0].PosName),
                                new ReportParameter("P_POS2", sr[1].PosName),
                    });
            }
        }

        private void ReportAlimony_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && CurrentCartularyID != null;
        }

        private void RepPostTransferAlimonyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepPostTransferAlimony.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            DataTable t = new DataTable();
            a.Fill(t);
            ViewReportWindow.ShowReport(this, "\"Почтовые переводы\"", "Rep_PostTransferAlimony.rdlc", t, null);
        }

        OracleCommand cmd_CloseCartulary;
        private void CloseCartulary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cmd_CloseCartulary == null)
            {
                cmd_CloseCartulary = new OracleCommand(string.Format("begin {1}.Close_cartulary(:p_cartulary_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd_CloseCartulary.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, ParameterDirection.Input);
            }
            cmd_CloseCartulary.Parameters["p_cartulary_id"].Value = CurrentCartularyID;
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                cmd_CloseCartulary.ExecuteNonQuery();
                tr.Commit();
                UpdateCartularyView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка закрытия реестра");
            }
        }

        OracleCommand cmd_OpenCartulary;
        private void OpenCartulary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cmd_OpenCartulary == null)
            {
                cmd_OpenCartulary = new OracleCommand(string.Format("begin {1}.Open_cartulary(:p_cartulary_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd_OpenCartulary.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, ParameterDirection.Input);
            }
            cmd_OpenCartulary.Parameters["p_cartulary_id"].Value = CurrentCartularyID;
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                cmd_OpenCartulary.ExecuteNonQuery();
                tr.Commit();
                UpdateCartularyView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка закрытия реестра");
            }
        }

        

#region Перечисления сотрудников. Формирование файлов

        private void ReportRegister_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && dgCartulary != null && CurrentCartularyID != null;
        }

        private void FileSumTransfer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView v = CartularySource.OfType<DataRowView>().Where(t => t.Row.Field2<Decimal?>("CARTULARY_ID") == (decimal)CurrentCartularyID && t["DATE_CLOSE_CART"] != DBNull.Value).FirstOrDefault();
            if (v == null)
            {
                MessageBox.Show("Формирование файлов допускается только из закрытых реестров", "Ошибка формирования");
                return;
            }
            Decimal k = (Decimal)e.Parameter;
            FormatFileBank format = new FormatFileBank(FormatBank.Select("FORMAT_FILE_BANK_ID=" + k).FirstOrDefault());
            FileInfo finfo = new FileInfo(format.FileName);
            switch (finfo.Extension.ToUpper())
            {
                case ".TXT": WriteToTxtFile(format); break;
                case ".DBF": WriteToDbf(format); break;
                case ".RDLC": WriteToXlsViaRDLC(format); break;
                case ".XML": WriteToXmlFile(format); break;
                default: MessageBox.Show(string.Format("Формат не поддерживается!{0}", finfo.Extension), "Ошибка формата файла"); break;
            }
        }

        /// <summary>
        /// Записывает данные в XML файл
        /// </summary>
        /// <param name="format"></param>
        private void WriteToXmlFile(FormatFileBank format)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(format.QueryName), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_TRN", OracleDbType.Varchar2, format.TRN, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_CARTULARY_ID", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
            DataTable t = new DataTable();
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных",
                    oda, oda.SelectCommand,
                    (s, pw) =>
                    {
                        System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
                        sf.InitialDirectory = (string)Properties.Settings.Default["LocalUploadPath"];
                        sf.Filter = "Файлы .Xml(*.xml)|*.xml";
                        sf.FileName = string.Format(format.OutFileName, DateTime.Now, SelectedDate, CurrentCartulary["CARTULARY_NUM"]);
                        if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
                        {
                            try
                            {
                                Properties.Settings.Default["LocalUploadPath"] = System.IO.Path.GetDirectoryName(sf.FileName);
                                Properties.Settings.Default.Save();
                                File.WriteAllLines(sf.FileName, (pw.Result as DataSet).Tables[0].Rows.OfType<System.Data.DataRow>().Select(y => y[0].ToString()).ToArray(), Encoding.GetEncoding(1251));
                                MessageBox.Show(string.Format("Файл успешно сформирован"), "Зарплата предприятия");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Ошибка записи файла");
                            }
                        }
                    });
        }

        /// <summary>
        /// создает эксель файл через отчет 
        /// </summary>
        /// <param name="format"></param>
        private void WriteToXlsViaRDLC(FormatFileBank format)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(format.QueryName), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_TRN", OracleDbType.Varchar2, format.TRN, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_CARTULARY_ID", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
            DataTable t = new DataTable();
            bool fl = false;
            try
            {
                oda.Fill(t);
                fl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
            }
            if (fl)
                ViewReportWindow.RenderToExcel(this, format.FileName, string.Format(format.OutFileName, DateTime.Now, SelectedDate, CurrentCartulary["CARTULARY_NUM"]), (string)Properties.Settings.Default["LocalUploadPath"], t, null);
        }

        /// <summary>
        /// создает дбфку и заполняет ее данными
        /// </summary>
        /// <param name="format"></param>
        private void WriteToDbf(FormatFileBank format)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(format.QueryName), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_TRN", OracleDbType.Varchar2, format.TRN, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_CARTULARY_ID", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
            DataTable t = new DataTable();
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", 
                (s, pw)=>
                    {
                        OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                        a.Fill(t);
                        pw.Result= DBFWriter.WriteToDBF(Connect.CurrentAppPath + @"\Reports\" + format.FileName, t);
                        
                    },
                    oda, oda.SelectCommand, 
                    (s, pw)=>
                        {
                            if (pw.Cancelled)
                                return;
                            else if (pw.Error != null)
                                MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования данных");
                            else
                            {
                                System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
                                sf.InitialDirectory = (string)Properties.Settings.Default["LocalUploadPath"];
                                sf.Filter = "Файлы .DBF(*.DBF)|*.dbf";
                                sf.FileName = string.Format(format.OutFileName, DateTime.Now, SelectedDate, CurrentCartulary["CARTULARY_NUM"]);
                                if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
                                {
                                    try
                                    {
                                        Properties.Settings.Default["LocalUploadPath"] = System.IO.Path.GetDirectoryName(sf.FileName);
                                        Properties.Settings.Default.Save();
                                        File.Copy((string)pw.Result, sf.FileName, true);
                                        MessageBox.Show(string.Format("Файл успешно сформирован. Общая сумма по файлу: {0:n}. Кол-во записей: {1}",
                                            t.Compute(format.SumField, ""), t.Compute(format.CountField, "")), "Общие итоги");
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, "Ошибка записи файла");
                                    }
                                }
                            }
                        });
        }

        /// <summary>
        /// Запись данных в текстовый файл
        /// </summary>
        /// <param name="format"></param>
        private void WriteToTxtFile(FormatFileBank format)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(format.QueryName), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_TRN", OracleDbType.Varchar2, format.TRN, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_CARTULARY_ID", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
            DataTable t = new DataTable();
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных",
                (s, pw) =>
                {
                    OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                    a.Fill(t);
                    pw.Result = t;

                },
                    oda, oda.SelectCommand,
                    (s, pw) =>
                    {
                        if (pw.Cancelled)
                            return;
                        else if (pw.Error != null)
                            MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования данных");
                        else
                        {
                            System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
                            sf.InitialDirectory = (string)Properties.Settings.Default["LocalUploadPath"];
                            sf.Filter = "Файлы .TXT(*.txt)|*.txt";
                            sf.FileName = string.Format(format.OutFileName, DateTime.Now, SelectedDate, CurrentCartulary["CARTULARY_NUM"]);
                            if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
                            {
                                try
                                {
                                    Properties.Settings.Default["LocalUploadPath"] = System.IO.Path.GetDirectoryName(sf.FileName);
                                    Properties.Settings.Default.Save();
                                    File.WriteAllLines(sf.FileName, (pw.Result as DataTable).Rows.OfType<System.Data.DataRow>().Select(y => y[0].ToString()).ToArray(), Encoding.GetEncoding(1251));
                                    MessageBox.Show(string.Format("Файл успешно сформирован. Общая сумма по файлу: {0:n}. Кол-во записей: {1}",
                                        t.Compute(format.SumField, ""), t.Compute(format.CountField, "")), "Общие итоги");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Ошибка записи файла");
                                }
                            }
                        }
                    });
        }

        /// <summary>
        /// Вьюха для отображения форматов файлов
        /// </summary>
        public DataView FileImportItemsSource
        { 
            get
            {
                return new DataView(FormatBank, "", "ORDER_NUMBER", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Таблица форматов и настроек файлов для банка
        /// </summary>
        public DataTable FormatBank
        {
            get
            {
                if (ds == null || !ds.Tables.Contains("FORMAT_FILE_BANK"))
                    LoadFormatFile();
                return ds.Tables["FORMAT_FILE_BANK"];
            }
        }

        private void LoadFormatFile()
        {
            if (ds == null)
                ds = new DataSet();
            if (ds.Tables.Contains("FORMAT_FILE_BANK"))
                ds.Tables["FORMAT_FILE_BANK"].Rows.Clear();
            odaFileTransferData.Fill(ds);
        }
#endregion

        private void RepRegisterReports_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Decimal k = (Decimal)e.Parameter;
            FormatFileBank format = new FormatFileBank(FormatBank.Select("FORMAT_FILE_BANK_ID=" + k).FirstOrDefault());
            if (string.IsNullOrEmpty(format.RegisterRDLC))
            {
                MessageBox.Show("Для выбранного банка не указан шаблон формирования реестра");
                return;
            }
            FileInfo finfo = new FileInfo(format.FileName);
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_SalaryRegister.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.SelectCommand.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_TRN", OracleDbType.Varchar2, format.TRN, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных",
                 oda, oda.SelectCommand,
                    (s, pw) =>
                    {
                        if (pw.Cancelled)
                            return;
                        else if (pw.Error != null)
                            MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования данных");
                        else
                            ViewReportWindow.ShowReport(this, "Реестр перечислений", format.RegisterRDLC, (pw.Result as DataSet).Tables.OfType<DataTable>().ToArray(), new ReportParameter[] { new ReportParameter("P_DATE", SelectedDate.Value.ToShortDateString()) });
                    });
        }

        private void RepSalaryTransferNote_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_SalaryTransferNote.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
            try
            {
                DataTable t = new DataTable();
                oda.Fill(t);
                ViewReportWindow.ShowReport(this, "Служебные на перечисление заработной платы", "Rep_SalaryTransferNote.rdlc", t, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирование отчета");
            }
        }

        private void btFilter_Click(object sender, RoutedEventArgs e)
        {
            UpdateCartularyView();
        }

        private void RepSalaryPayNote_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PrintCartularyT_53(this, (decimal?)CurrentCartularyID, 0, CurrentCartulary.Row.Field2<DateTime>("DATE_CARTULARY"), "ОТПУСКНЫХ");
        }

        public static void PrintCartularyT_53(DependencyObject sender, decimal? cartularyID, decimal? subdivID, DateTime? dateCartulary, string cartularyName)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("RepSalaryPayT-53.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, cartularyID, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, subdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(sender, "Получение данных",
                oda, oda.SelectCommand,
                (s, pw) =>
                {
                    ViewReportWindow.ShowReport(sender, "Платежная ведомость", "Rep_PayForm_T-53.rdlc", (pw.Result as DataSet).Tables[0], new ReportParameter[]{
                            new ReportParameter("P_TYPE_PAY_FORM", cartularyName), new ReportParameter("P_DATE", dateCartulary.Value.ToShortDateString())});
                });
        }

        private void ReportWithTypeCartulary_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && CurrentCartulary!=null && e.Parameter != null
                    && e.Parameter.ToString().Split(',', ' ').Contains(CurrentTypeCartulary["TYPE_CARTULARY_ID"].ToString());
        }

        private void RepUploadTxtSalaryPayNote_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!CurrentCartulary.Row.Field<DateTime?>("DATE_CLOSE_CART").HasValue)
            {
                MessageBox.Show("Платежную ведомость возможно сформировать только из Закрытого реестра!");
                return;
            }
            if (MessageBox.Show("Выгрузить в текстовый файл платежную ведомость?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SForms.SaveFileDialog sf = new SForms.SaveFileDialog();
                sf.Filter = "Текстовые файлы (.txt)|*.txt";
                sf.InitialDirectory = Connect.parameters["KassaDirectory"];
                sf.FileName = string.Format("OTPUSK{0:00}", CurrentCartulary.Row.Field2<DateTime?>("DATE_CARTULARY").Value.Month);
                if (sf.ShowDialog() == SForms.DialogResult.OK)
                {
                    try
                    {
                        OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectSalaryPayNote.sql"), Connect.CurConnect);
                        oda.SelectCommand.BindByName = true;
                        oda.SelectCommand.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
                        DataTable t = new DataTable();
                        oda.Fill(t);
                        File.WriteAllLines(sf.FileName, t.Rows.OfType<System.Data.DataRow>().Select(r => r[0].ToString()), Encoding.GetEncoding(866));
                        MessageBox.Show(string.Format("Файл успешно сформирован. Выгружено {0}  записей", t.Rows.Count), "Зарплата предприятия");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка формирования");
                    }
                }
            }
        }

        private void RepCartularyVsSalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("RepCartularyVSSalary.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_type_cartulary_id", OracleDbType.Decimal, TypeCartularyID, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, 0, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);

            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных",
                oda, oda.SelectCommand,
                (s, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Сравнение реестров и ЗП", "Rep_CartularyVSSalary.rdlc", (pw.Result as DataSet).Tables[0], new ReportParameter[]{
                            new ReportParameter("P_TYPE_CARTULARY", CurrentTypeCartulary["TYPE_CARTULARY_NAME"].ToString()), 
                            new ReportParameter("P_DATE", SelectedDate.Value.ToShortDateString())});
                });
        }

        private void RefreshPaid_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCartularyID!=null)
                UpdateCartPaidView(CurrentCartularyID);
        }

        private void RepCartularyConsolidSubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.CartularyPaidSource!=null)
                ViewReportWindow.ShowReport(this, "Сводная по реестру", "Rep_CartularyConsolidSubdiv.rdlc", CartularyPaidSource.Table, new ReportParameter[]{
                                new ReportParameter("P_DATE_CARTULARY", CurrentCartulary["DATE_CREATE"].ToString()), 
                                new ReportParameter("P_NAME_CARTULARY", CurrentCartulary["CARTULARY_COMMENT"].ToString())});
        }

        private void Rep_CartularyConsolidTypeBank_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.CartularyPaidSource != null)
                ViewReportWindow.ShowReport(this, "Сводная по реестру", "Rep_CartularyConsolidTypeBank.rdlc", CartularyPaidSource.Table, new ReportParameter[]{
                                new ReportParameter("P_DATE_CARTULARY", CurrentCartulary["DATE_CREATE"].ToString()), 
                                new ReportParameter("P_NAME_CARTULARY", CurrentCartulary["CARTULARY_COMMENT"].ToString())});
        }

        private void Unload987ToTxt_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                DataTable t = new DataTable();
                OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("Unload987Payment.sql"), Connect.CurConnect);
                a.SelectCommand.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
                a.Fill(t);
                System.Windows.Forms.SaveFileDialog sf = new SForms.SaveFileDialog();
                sf.FileName = "sum_987.txt";
                sf.InitialDirectory = Connect.parameters["NSPR_DIR"];
                sf.Filter = "Текстовые файлы (.txt)|*.txt";
                if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == SForms.DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(sf.FileName, false, Encoding.GetEncoding(866));
                    foreach (string s in t.Rows.OfType<System.Data.DataRow>().Select(r => r[0].ToString()))
                        sw.WriteLine(s);
                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("Файл успешно сформирован.\n Путь: " + sf.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
            }
        }

        private void RepTypeBankEmpTransfer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_TypeBankEmpTransfer.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_code_payment", OracleDbType.Varchar2, "287", ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);

            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных",
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
                        ViewReportWindow.ShowReport(this, "Сводный отчет по перечислению зарплаты на счета", "Rep_TypeBankEmpTransfer.rdlc", pw.Result as DataTable, new ReportParameter[]{
                                new ReportParameter("P_DATE", SelectedDate.Value.ToShortDateString())});
                });
        }

        private void Report_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        /// <summary>
        /// Отчет для служебных по командировочным расходам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryMissionTransferNote_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"ClientAccountTransfer/Rep_SalaryMissionTransferNote.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Служебные на перечисление", @"ClientAccounts/Rep_SalaryMissionTransferNote.rdlc", (pw.Result as DataSet).Tables[0], null);
                });
        }

        private void RepTypeBankSumTransfer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByPeriod f = new FilterByPeriod();
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            { 
                OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("RepTypeBankSumTransfer.sql"), Connect.CurConnect);
                a.SelectCommand.BindByName= true;
                a.SelectCommand.Parameters.Add("p_date1", OracleDbType.Date, f.Model.DateBegin, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date2", OracleDbType.Date, f.Model.DateEnd, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте.",
                    (p, pw) =>
                    {
                        DataTable t = new DataTable();
                        (pw.Argument as OracleDataAdapter).Fill(t);
                        pw.Result = t;
                    }, a, a.SelectCommand,
                    (p, pw) =>
                    {
                        if (pw.Cancelled) return;
                        else if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                        else
                            ViewReportWindow.ShowReport(this, "Сводный отчет по перечислениям в банки за период", "Rep_TypeBankSumTransfer.rdlc", (pw.Result as DataTable),
                                new ReportParameter[] { new ReportParameter("P_DATE1", f.Model.DateBegin.Value.ToShortDateString()), new ReportParameter("P_DATE2", f.Model.DateEnd.Value.ToShortDateString()) });
                    });
            }
        }

        private void RepSalaryTransferNoteAttachment_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_SalaryTransferNoteAttachment.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
            try
            {
                DataTable t = new DataTable();
                oda.Fill(t);
                ViewReportWindow.ShowReport(this, "Приложение к служебным на перечисление", "Rep_SalaryTransferNoteAttachment.rdlc", t, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирование отчета");
            }
        }

        /// <summary>
        /// Логика обработки обновления SALARY_ID  в реестре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatePaidCartularyRef_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(Window.GetWindow(this), "Связать данные реестра с зарплатой", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OracleCommand cmd = new OracleCommand("begin SALARY.Set_CartularyPaid_FK(:p_cartulary_id, :k); end;", Connect.CurConnect);
                cmd.BindByName = true;
                cmd.Parameters.Add("p_cartulary_id", OracleDbType.Decimal, CurrentCartularyID, ParameterDirection.Input);
                cmd.Parameters.Add("k", OracleDbType.Decimal, ParameterDirection.Output);
                OracleTransaction tr = Connect.CurConnect.BeginTransaction();
                try
                {
                    cmd.ExecuteNonQuery();
                    tr.Commit();
                    MessageBox.Show(string.Format("Обработано {0} записей", cmd.Parameters["k"].Value), "Зарплата предприятия");
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.GetFormattedException(), "Зарплата предприятия");
                }
            }
        }

    }

    public class FormatFileBank
    {
        private System.Data.DataRow datarow;
        public FormatFileBank(System.Data.DataRow r)
        {
            datarow = r;
        }

        /// <summary>
        /// Надпись в меню
        /// </summary>
        public string ItemCaption
        { 
            get
            {
                return TryGetValue<string>(datarow, "ITEM_CAPTION");
            }
        }

        /// <summary>
        /// айдишник записи
        /// </summary>
        public decimal? FormatFileBankID
        {
            get
            {
                return TryGetValue<Decimal?>(datarow, "FORMAT_FILE_BANK_ID");
            }
        }

        /// <summary>
        /// инн банка
        /// </summary>
        public string TRN
        {
            get
            {
                return TryGetValue<string>(datarow, "TRN");
            }
        }

        /// <summary>
        /// Входной файл, если требуется (для получения расширения или структуры ДБФ)
        /// </summary>
        public string FileName
        {
            get
            {
                return TryGetValue<string>(datarow, "FILE_NAME");
            }
        }

        /// <summary>
        /// Порядковый номер сортировки
        /// </summary>
        public decimal? OrderNumber
        {
            get
            {
                return TryGetValue<Decimal?>(datarow, "ORDER_NUMBER");
            }
        }

        /// <summary>
        /// Формат выходного файла
        /// </summary>
        public string OutFileName
        {
            get
            {
                return TryGetValue<string>(datarow, "OUT_FILE_NAME");
            }
        }

        /// <summary>
        /// запрос для получения данных
        /// </summary>
        public string QueryName
        {
            get
            {
                return TryGetValue<string>(datarow, "QUERY_NAME");
            }
        }

        /// <summary>
        /// поля для вычисления суммы
        /// </summary>
        public string SumField
        {
            get
            {
                return TryGetValue<string>(datarow, "SUM_FIELD");
            }
        }

        /// <summary>
        /// поле для получения кол-ва записей
        /// </summary>
        public string CountField
        {
            get
            {
                return TryGetValue<string>(datarow, "COUNT_FIELD");
            }
        }

        /// <summary>
        /// Путь к отчету для реестра
        /// </summary>
        public string RegisterRDLC
        {
            get
            {
                return TryGetValue<string>(datarow, "REGISTER_RDLC");
            }
        }
        private T TryGetValue<T>(System.Data.DataRow r, string Field)
        {
            if (r.Table.Columns.Contains(Field) && r[Field]!=DBNull.Value)
            {
                return (T)r[Field];
            }
            else return default(T);
        }
    }

    
}
