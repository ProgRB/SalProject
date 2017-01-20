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
using System.Data;
using Oracle.DataAccess.Client;
using System.ComponentModel;
using Salary.Reports;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Data.OleDb;
using Salary.Helpers;
using System.Xml.Linq;
using Salary.Interfaces;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for AlimonysViewer.xaml
    /// </summary>
    public partial class AlimoniesViewer : UserControl
    {
        public AlimoniesViewer()
        {
            InitializeComponent();
            _model = new ReceiveOrderViewModel();
            this.DataContext = Model;
            Model.UpdateAlimonyView();
            Model.TypeRecieveOrder = Model.TypeReceiveOrderSource[0];
        }

        public ReceiveOrderViewModel Model
        {
            get
            {
                return _model;
            }
        }

        private void AddRetentionCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void AddRetentionCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RetentionEditor f = new RetentionEditor(null, null);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateAlimonyView();
            }
        }

        private void EditRetentionCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model!=null && Model.SelectedRetention != null;
        }

        private void EditRetentionCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RetentionEditor f = new RetentionEditor(Model.SelectedRetention["TRANSFER_ID"], Model.SelectedRetention["RETENTION_ID"]);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateAlimonyView();
            }
        }

        private void DeleteRetentionCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранное удержание без возможности восстановления?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Model.DeleteCurrentRetention();
            }
        }

        private void RepPostTransferAlimonyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("RepPostTransferAlimony.sql", Model, FilterParameter.p_date, FilterParameter.c);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...", a, a.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "\"Почтовые переводы\"", "Rep_PostTransferAlimony.rdlc", (pw.Result as DataSet).Tables[0], null);
                });
        }

        private void Report_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void RepAlimonyDeptor_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepAlimonyDeptor.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...", a, a.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "\"Должники по алиментам\"", "Rep_AlimonyDeptor.rdlc", (pw.Result as DataSet).Tables[0], new ReportParameter[] { new ReportParameter("SelectedDate", Model.SelectedDate.ToString("MMMM yyyy")) }.ToList());
                });
        }

        private void RepAlimonyBalance_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("RepAlimonyBalance.sql", Model, FilterParameter.p_date, FilterParameter.c );
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", a, a.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "\"Оборотная ведомость алиментов\"", "Rep_AlimonyBalance.rdlc", (pw.Result as DataSet).Tables[0], new ReportParameter[] { new ReportParameter("SelectedDate", Model.SelectedDate.ToString("MMMM yyyy")) }.ToList());
                });
        }

        private void RepAlimonyCatalog_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("RepAlimonyCatalog.sql", Model, FilterParameter.p_date);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", a, a.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "\"Справочник алиментов\"", "Rep_AlimonyCatalog.rdlc", (pw.Result as DataSet).Tables[0], new ReportParameter[] { new ReportParameter("SelectedDate", Model.SelectedDate.ToString("MMMM yyyy")) }.ToList());
                });
            
        }

        private void AlimonyCard_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AlimonyCard f = new AlimonyCard(Model.SelectedRetention["TRANSFER_ID"], Model.SelectedDate);
            f.Owner = Window.GetWindow(this);
            f.ShowDialog();
        }

        private void RepViewReportLoadAlimonyToTxt_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepLoadAlimonyToTxt.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            DataTable t = new DataTable();
            a.Fill(t);
            ViewReportWindow.ShowReport(this, "\"Просмотр справочника на выгрузку\"", "Rep_AlimonyToTxtView.rdlc", t, null);
        }

        private void LoadAlimonyToTxt_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
            sf.AddExtension = true;
            sf.FileName = string.Format("ALSPR{0:00}.txt", Model.SelectedDate.Month);
            sf.DefaultExt = "TXT";
            sf.Filter = "Текстовые файлы (*.txt)|*.txt";
            sf.OverwritePrompt = true;
            sf.Title = "Выгрузка справочника";
            sf.ValidateNames = true;
            sf.InitialDirectory = Connect.parameters["AlimonyFileDir"];
            if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    DataTable t = new DataTable();
                    OracleDataAdapter a = new OracleDataAdapter(string.Format("select * from table({0}.Alimony_Pkg.CreateUnLoadAlimonyTxt(:p_date))", Connect.SchemaSalary), Connect.CurConnect);
                    a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
                    a.Fill(t);
                    if (File.Exists(sf.FileName))
                        File.Delete(sf.FileName);
                    File.AppendAllLines(sf.FileName, t.Rows.OfType<DataRow>().Select(p => p[0].ToString()), Encoding.GetEncoding(866));
                    MessageBox.Show(string.Format("Файл справочника алиментов успешно сформирован ({0} записей). Путь: {1}", t.Rows.Count, sf.FileName), "Зарплата предприятия");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка формирования файла", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoadAlimonyIntoDB_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog f = new System.Windows.Forms.OpenFileDialog();
            f.Filter = "Текстовые файлы (*.txt)|*.txt";
            f.InitialDirectory = Connect.parameters["AlimonyFileDir"];
            f.FileName = string.Format("ALIM{0:00}.txt", Model.SelectedDate.Month);
            f.Title = string.Format("Выбор файла для загрузки алиментов за {0} !!! месяц {1} года", Model.SelectedDate.Month, Model.SelectedDate.Year);
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                xcBusyIndicator.IsBusy = true;
                OracleCommand cmd = new OracleCommand(string.Format("begin {0}.LoadAlimonyToDB(:p, :p_date);end;", Connect.SchemaSalary), Connect.CurConnect);
                cmd.Parameters.Add("p", OracleDbType.Varchar2, ParameterDirection.Input);
                cmd.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate.Date, ParameterDirection.Input);
                TextLoadHelper.LoadFinished += new RunWorkerCompletedEventHandler(LoadEnd);
                OracleCommand clearCOmmand = new OracleCommand(string.Format("begin {0}.ClearAlimonyData(:p_date);end;", Connect.SchemaSalary), Connect.CurConnect);
                clearCOmmand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
                TextLoadHelper.LoadData(f.FileName, cmd, Encoding.GetEncoding(866), clearCOmmand);
            }
        }
        private void LoadEnd(object sender, RunWorkerCompletedEventArgs e)
        {
            xcBusyIndicator.IsBusy = false;
        }
        private decimal all_sum;
        private ReceiveOrderViewModel _model;
        private void LoadAlimonyIntoCash_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(string.Format("Сформировать записи в кассу за {0} г.", Model.SelectedDate.ToString("MMMM yyyy")), "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                try
                {
                    xcBusyIndicator.IsBusy = true;
                    OracleDataAdapter a = new OracleDataAdapter(string.Format("begin {0}.ALIMONY_PKG.Get_AlimonyForCash(:p_date, :c);end;", Connect.SchemaSalary), Connect.CurConnect);
                    a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
                    a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                    DataTable t = new DataTable();
                    a.Fill(t);
                    decimal k = 0;
                    OleDbConnection fpConnecton = new OleDbConnection(string.Format("Provider=VFPOLEDB.1;Data Source={0};Mode=Read;", Connect.parameters["KassaFile"]));
                    fpConnecton.Open();
                    OleDbCommand cm = new OleDbCommand("select max(val(kas_dok)) from kassa", fpConnecton);
                    k = (decimal)cm.ExecuteScalar();
                    fpConnecton.Close();
                    all_sum = 0;
                    foreach (DataRow r in t.Rows)
                    {
                        r["KAS_DOK"] = ((int)(decimal.Parse(r["KAS_DOK"].ToString()) + k)).ToString();
                        all_sum += r.Field<Decimal>("SUMMA");
                        r.AcceptChanges();
                        r.SetAdded();
                    }
                    AlimonyWriter.WriteFinished -= CashWriteFinished;
                    AlimonyWriter.WriteFinished += CashWriteFinished;
                    AlimonyWriter.WriteData(t, "type,kas_dok,skpolz,dat_kas,bsht,dat_kor,syst_data,pndok,summa,nazv, kurs, ob_v,fl_pkurs,p_opl,skst,p_sv,p_dep,pr_sign,osn_ko,tnom,porn,podr,pm,dat_kom,dat_opl,dat_sign,nom_kas_d,sum_opl,sum_ost"
                            ,"type, dat_kas, bsht, dat_kor, syst_data, pndok, summa, nazv, kurs, ob_v, fl_pkurs, p_opl, skst, p_sv, p_dep, pr_sign, osn_ko, porn, podr, pm, dat_kom, dat_opl, dat_sign, nom_kas_d, sum_opl, sum_ost"
                            , "skpolz='ALIM' and kas_dok=? and tnom=?"
                            , "skpolz, kas_dok, tnom"
                            , "skpolz='ALIM' and kas_dok=? and tnom=?"
                            , "skpolz, kas_dok, tnom");
                }
                catch (Exception ex)
                {
                    xcBusyIndicator.IsBusy = false;
                    MessageBox.Show(ex.Message, "Ошибка формирования записей в кассу");
                }
            }
        }
        private void CashWriteFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            xcBusyIndicator.IsBusy = false;
            MessageBox.Show(string.Format("Обработано {0} записей. Общая сумма по записям: {1}", e.Result, all_sum));
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                tbFIO_Alimony.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void btClickRefresh_Click(object sender, RoutedEventArgs e)
        {
            bgFilterGroup.UpdateSources();
            Model.UpdateAlimonyView();
            //tbFIO_Alimony.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Rep_AllAlimony_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepAlimonyAll.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, 0, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            DataTable t = new DataTable();
            a.Fill(t);
            ViewReportWindow.ShowReport(this, "\"Все алименты\"", "Rep_AllAlimonies.rdlc", t, null);
        }

        /// <summary>
        /// Отчет сводный по удержаниям и куда было оно перечислено
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryTransferRetent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewReporting.FilterReporting f = new ViewReporting.FilterReporting(0, Model.SelectedDate, Model.SelectedDate);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("Rep_SalaryTransferRetent.sql", f, FilterParameter.p_date_begin, FilterParameter.p_date_end, FilterParameter.p_subdiv_id);
                a.SelectCommand.Parameters.Add("p_payment_type_ids", OracleDbType.Array, Model.TypeRecieveOrder.IncludedPaymentIDs, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...", a, a.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "Сводный отчет по исполнительным листам", "Rep_SalaryTransferRetent.rdlc", (pw.Result as DataSet).Tables[0], null);
                    });
            }
        }

        /// <summary>
        /// Распоряжения удержаний
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepRetentDocOrder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByList f = new FilterByList(Model.ReceiveOrderSource.OfType<DataRowView>().OrderByDescending(r=>r.Row.Field2<DateTime?>("DATE_ADD")),
                new DataGridColumn[]{
                    new DataGridTextColumn(){Binding=new Binding("CODE_SUBDIV"), Header="Подр."},
                    new DataGridTextColumn(){Binding=new Binding("PER_NUM"), Header="Таб.№"},
                    new DataGridTextColumn(){Binding=new Binding("FIO"), Header="ФИО"},
                    new DataGridTextColumn(){Binding=new Binding("ORDER_NUMBER"), Header="№ удерж.", Width=100},
                    new DataGridTextColumn(){Binding=new Binding("RETENT_PERCENT"), Header="% удерж."},
                    new DataGridTextColumn(){Binding=new Binding("RETENT_SUM"), Header="Сумма удерж."},
                    new DataGridTextColumn(){Binding=new Binding("DATE_ADD"), Header="Дата добавления", Width=150, SortMemberPath="DATE_ADD", SortDirection=ListSortDirection.Descending}
                },false);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                SignesRecord[] sr=null;
                if (Signes.Show(0, "RetentDocumOrder", "Выберите ответственное лицо", 1, ref sr) == true)
                {
                    OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("RepRetentDocumOrder.sql", f);
                    a.SelectCommand.Parameters.Add("p_retent_ids", OracleDbType.Array, f.SelectedValues<Decimal>("RETENTION_ID"), ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                    AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...", a,
                        a.SelectCommand,
                        (p, pw) =>
                        {
                            ViewReportWindow.ShowReport(this, "Распоряжения удержаний", "Rep_RetentDocumentOrder.rdlc", (pw.Result as DataSet).Tables[0],
                                new ReportParameter[] { new ReportParameter("P_SIGN1", sr[0].EmpName) });
                        });
                }
            }
        }
        
    }

    public class ReceiveOrderType
    {
        public ReceiveOrderType()
        {}
        public string TypeOrderName
        {
            get;set;
        }
        public string[] IncludedPayments
        {
            get;set;
        }
        /// <summary>
        /// Айдишники шифров оплат, которые входят в тип листов.
        /// </summary>
        public decimal[] IncludedPaymentIDs
        {
            get
            {
                if (IncludedPayments.Length == 0)
                    return null;
                else
                    return AppDataSet.Tables["PAYMENT_TYPE"].Select(string.Format("code_payment in ({0})",
                                string.Join(",", IncludedPayments.Select(r => "'" + r + "'"))))
                                .Select(r => r.Field2<Decimal>("PAYMENT_TYPE_ID")).ToArray();
            }
        }
    }

    public class ReceiveOrderViewModel : NotificationObject, ICustomFilter
    {
        public ReceiveOrderViewModel()
        {
            ds = new DataSet();
            oda_AlimonyView = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectAlimonyView.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            oda_AlimonyView.SelectCommand.BindByName = true;
            oda_AlimonyView.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, SelectedDate, ParameterDirection.Input);
            oda_AlimonyView.SelectCommand.Parameters.Add("p_alim_fio", OracleDbType.Varchar2, null, ParameterDirection.Input);
            oda_AlimonyView.SelectCommand.Parameters.Add("p_payment_ids", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName="SALARY.NUMBER_COLLECTION_TYPE";
            oda_AlimonyView.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            oda_Retents = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectAlimonyClientAccount.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            oda_Retents.SelectCommand.BindByName = true;
            oda_Retents.SelectCommand.Parameters.Add("p_retention_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            oda_Retents.TableMappings.Add("Table", "CLIENT_ACCOUNT");

            cmd_DeleteRetentAlimony = new OracleCommand(string.Format("begin {1}.RETENTION_DELETE(:p_retention_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmd_DeleteRetentAlimony.BindByName = true;
            cmd_DeleteRetentAlimony.Parameters.Add("p_retention_id", OracleDbType.Decimal, null, ParameterDirection.Input);
        }

        OracleDataAdapter oda_AlimonyView, oda_Retents;
        OracleCommand cmd_DeleteRetentAlimony;

        DataSet ds;

#region Class Members
        DataRowView _selectedRetention;
        /// <summary>
        /// Выбранное удержание
        /// </summary>
        public DataRowView SelectedRetention
        {
            get
            {
                return _selectedRetention;
            }
            set
            {
                if (_selectedRetention != value)
                {
                    _selectedRetention = value;
                    RaisePropertyChanged(() => SelectedRetention);
                    UpdateAlimonyAccount();
                }
            }
        }

        List<ReceiveOrderType> _listReceiveSource;
        /// <summary>
        /// Источник данных для списка типо исполнительных листов
        /// </summary>
        public List<ReceiveOrderType> TypeReceiveOrderSource
        {
            get
            {
                if (_listReceiveSource==null)
                {
                    _listReceiveSource = AppXmlHelper.GetElements("TypeReceiveOrder").Select(r=>new ReceiveOrderType()
                            { 
                                TypeOrderName= r.Attribute("Name").Value, 
                                IncludedPayments = r.Attribute("Payments").Value.Split(new char[]{',',' '}, StringSplitOptions.RemoveEmptyEntries).ToArray()
                            }).ToList();
                }
                return _listReceiveSource;
            }
        }

        ReceiveOrderType _selectedReceiveType;

        public ReceiveOrderType TypeRecieveOrder
        {
            get
            {
                return _selectedReceiveType;
            }
            set
            {
                if (_selectedReceiveType != value)
                {
                    _selectedReceiveType = value;
                    RaisePropertyChanged(() => TypeRecieveOrder);
                    UpdateAlimonyView();
                }
            }
        }

        DateTime _selectedDate = DateTime.Today;

        public DateTime SelectedDate
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
                    UpdateAlimonyView();
                }
            }
        }

        string _fioAlimony;

        /// <summary>
        /// Фамилия илиментоплательщика
        /// </summary>
        public string FioAlimony
        {
            get
            {
                return _fioAlimony;
            }
            set
            {
                if (_fioAlimony != value)
                {
                    _fioAlimony = value;
                    RaisePropertyChanged(() => FioAlimony);
                    UpdateAlimonyView();
                }
            }
        }

        public string FIO
        {
            get
            {
                return _fio;
            }
            set
            {
                _fio = value;
                RaisePropertyChanged(()=>FIO);
                UpdateFilter();
            }
        }

        string _per_num, _fio;
        public string PerNum
        {
            get
            {
                return _per_num;
            }
            set
            {
                _per_num = value;
                RaisePropertyChanged(() => PerNum);
                UpdateFilter();
            }

        }
        decimal? _subdivID;
        public decimal? SubdivID
        {
            get
            {
                return _subdivID;
            }
            set
            {
                _subdivID = value;
                RaisePropertyChanged(()=>SubdivID);
                UpdateFilter();
            }
        }

#endregion

        private void UpdateFilter()
        {
            DataView dv = ReceiveOrderSource;
            dv.RowFilter =
                string.Join(" and ", new string[]{string.IsNullOrWhiteSpace(PerNum)?"":string.Format("PER_NUM LIKE '*{0}*'", PerNum), 
                string.IsNullOrWhiteSpace(FIO)?"":string.Format("FIO like '*{0}*'", FIO),
                !SubdivID.HasValue?"":string.Format("SUBDIV_ID={0}", SubdivID)}.Where(p => p.Length > 0));
        }

        DataView _receiveOrderSource;

        /// <summary>
        /// Источник данных для списка удержаний
        /// </summary>
        public DataView ReceiveOrderSource
        {
            get
            {
                if (_receiveOrderSource ==null && ds!=null && ds.Tables.Contains("RETENTION"))
                    _receiveOrderSource = new DataView(ds.Tables["RETENTION"], "", "CODE_SUBDIV, FIO", DataViewRowState.CurrentRows);
                return _receiveOrderSource;
            }
        }

        /// <summary>
        /// Обновление просмотра списка удержания
        /// </summary>
        public void UpdateAlimonyView()
        {
            if (ds.Tables.Contains("RETENTION"))
                ds.Tables["RETENTION"].Rows.Clear();
            if (TypeRecieveOrder != null)
            {
                oda_AlimonyView.SelectCommand.Parameters["p_date"].Value = SelectedDate;
                oda_AlimonyView.SelectCommand.Parameters["p_alim_fio"].Value = FioAlimony;
                oda_AlimonyView.SelectCommand.Parameters["p_payment_ids"].Value = TypeRecieveOrder.IncludedPayments.Select(r => AppDictionaries.CodePaymentToID[r]).ToArray();

                oda_AlimonyView.Fill(ds, "RETENTION");
                RaisePropertyChanged(() => ReceiveOrderSource);
                UpdateAlimonyAccount();
            }
        }

        DataView _clientAccount;

        /// <summary>
        /// Источник данных для списка куда перечисляется удержание
        /// </summary>
        public DataView ClienAccountSource
        {
            get
            {
                if (ds!=null && ds.Tables.Contains("CLIENT_ACCOUNT") && _clientAccount==null)
                    _clientAccount = new DataView(ds.Tables["CLIENT_ACCOUNT"], "", "DATE_BEGIN_RELATION DESC", DataViewRowState.CurrentRows);
                return _clientAccount;
            }
                     
        }

        /// <summary>
        /// Обновление просмотра куда отправляется удержание
        /// </summary>
        public void UpdateAlimonyAccount()
        {
            if (ds.Tables.Contains("CLIENT_ACCOUNT"))
                ds.Tables["CLIENT_ACCOUNT"].Clear();
            if (SelectedRetention != null)
            {
                oda_Retents.SelectCommand.Parameters["p_retention_id"].Value = SelectedRetention["retention_id"];
                oda_Retents.Fill(ds);
            }
            RaisePropertyChanged(()=>ClienAccountSource);
        }

        public void DeleteCurrentRetention()
        {
            if (SelectedRetention == null) return;
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                cmd_DeleteRetentAlimony.Parameters["p_retention_id"].Value = SelectedRetention["retention_id"];
                cmd_DeleteRetentAlimony.ExecuteNonQuery();
                tr.Commit();
                UpdateAlimonyView();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления документа");
            }
        }

        public DateTime GetDate()
        {
            return SelectedDate;
        }

        public DateTime GetDateBegin()
        {
            return SelectedDate.Trunc("Month");
        }

        public DateTime GetDateEnd()
        {
            return SelectedDate.Trunc("Month").AddMonths(1).AddSeconds(-1);
        }

        public decimal? GetSubdivID()
        {
            return SubdivID;
        }

        public decimal[] GetDegreeIDs()
        {
            throw new NotImplementedException();
        }
    }

    public class AlimonyWriter
    {
        static Dictionary<string, OleDbParameter> _columnsMapping;
        public static Dictionary<string, OleDbParameter> CashColumnsMapping
        {
            get
            {
                return _columnsMapping;
            }
        }
        static AlimonyWriter()
        {
            List<OleDbParameter> cl = new List<OleDbParameter>();
            cl.Add(new OleDbParameter( "p1", OleDbType.VarChar, 18, "KAS_DOK"));
            cl.Add( new OleDbParameter("p2", OleDbType.VarChar, 1, "TYPE"));
            cl.Add( new OleDbParameter("p3", OleDbType.Numeric, 0, "NOM_KAS_D"));
            cl.Add( new OleDbParameter("p4", OleDbType.Numeric, 0, "PNDOK"));
            cl.Add( new OleDbParameter("p5", OleDbType.VarChar, 7, "SKPOLZ"));
            cl.Add( new OleDbParameter("p6", OleDbType.VarChar, 3, "PODR"));
            cl.Add( new OleDbParameter("p7", OleDbType.VarChar, 5, "TNOM"));
            cl.Add( new OleDbParameter("p8", OleDbType.VarChar, 68, "NAZV"));
            cl.Add( new OleDbParameter("p9", OleDbType.Numeric, 0, "SUMMA"));
            cl.Add( new OleDbParameter("p10", OleDbType.Numeric, 0, "SUM_OPL"));
            cl.Add( new OleDbParameter("p11", OleDbType.Numeric, 0, "SUM_OST"));
            cl.Add( new OleDbParameter("p12", OleDbType.Date, 18, "DAT_KAS"));
            cl.Add( new OleDbParameter("p13", OleDbType.VarChar, 8, "BSHT"));
            cl.Add( new OleDbParameter("p14", OleDbType.Date, 8, "DAT_KOR"));
            cl.Add( new OleDbParameter("p15", OleDbType.Date, 8, "SYST_DATA"));
            cl.Add( new OleDbParameter("p16", OleDbType.VarChar, 1, "PR70"));
            cl.Add( new OleDbParameter("p17", OleDbType.VarChar, 1, "PB"));
            cl.Add( new OleDbParameter("p18", OleDbType.VarChar, 7, "BKS"));
            cl.Add( new OleDbParameter("p19", OleDbType.VarChar, 7, "SKVHN"));
            cl.Add( new OleDbParameter("p20", OleDbType.VarChar, 7, "SKVSF"));
            cl.Add( new OleDbParameter("p21", OleDbType.Numeric, 0, "PM"));
            cl.Add( new OleDbParameter("p22", OleDbType.Numeric, 0, "KURS"));
            cl.Add( new OleDbParameter("p23", OleDbType.VarChar, 3, "OB_V"));
            cl.Add( new OleDbParameter("p24", OleDbType.Boolean, 0, "FL_PKURS"));
            cl.Add( new OleDbParameter("p25", OleDbType.VarChar, 7, "BKF"));
            cl.Add( new OleDbParameter("p26", OleDbType.VarChar, 1, "P_ZATR"));
            cl.Add( new OleDbParameter("p27", OleDbType.Boolean, 0, "P_OPL"));
            cl.Add( new OleDbParameter("p28", OleDbType.Date, 8, "DAT_KOM"));
            cl.Add( new OleDbParameter("p29", OleDbType.VarChar, 6, "NOM_PRIK"));
            cl.Add( new OleDbParameter("p30", OleDbType.VarChar, 7, "SKST"));
            cl.Add( new OleDbParameter("p31", OleDbType.Boolean, 0, "P_SV"));
            cl.Add( new OleDbParameter("p32", OleDbType.Date, 8, "DAT_OPL"));
            cl.Add( new OleDbParameter("p33", OleDbType.VarChar, 2, "PNSUD"));
            cl.Add( new OleDbParameter("p34", OleDbType.Boolean, 0, "P_DEP"));
            cl.Add( new OleDbParameter("p35", OleDbType.VarChar, 5, "NOMDEP"));
            cl.Add( new OleDbParameter("p36", OleDbType.VarChar, 1, "P_RAB"));
            cl.Add( new OleDbParameter("p37", OleDbType.VarChar, 1, "PORN"));
            cl.Add( new OleDbParameter("p38", OleDbType.VarChar, 32, "HD"));
            cl.Add( new OleDbParameter("p39", OleDbType.VarChar, 32, "HSD"));
            cl.Add( new OleDbParameter("p40", OleDbType.Date, 0, "DAT_SIGN"));
            cl.Add( new OleDbParameter("p41", OleDbType.Boolean, 0, "PR_SIGN"));
            cl.Add( new OleDbParameter("p42", OleDbType.VarChar, 150, "OSN_KO"));
            cl.Add( new OleDbParameter("p43", OleDbType.VarChar, 150, "PRIL"));
            cl.Add( new OleDbParameter("p44", OleDbType.VarChar, 150, "PROCHEE"));
            cl.Add( new OleDbParameter("p45", OleDbType.VarChar, 19, "DATA_FILE"));
            cl.Add( new OleDbParameter("p46", OleDbType.VarChar, 15, "NAME_FILE"));
            cl.Add( new OleDbParameter("p47", OleDbType.VarChar, 5, "NOM_DEP"));
            _columnsMapping = new Dictionary<string, OleDbParameter>(StringComparer.CurrentCultureIgnoreCase);
            foreach (OleDbParameter c in cl)
                _columnsMapping.Add(c.SourceColumn, c);
        }

        public static void WriteData(DataTable t, string insert_columns, string update_columns, string update_where, string update_where_columns, string delete_where, string delete_where_columns)
        {
            if (CheckVfpOleDb.IsInstalled())
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += (o, ea) =>
                {
                    OleDbConnection fpConnecton = new OleDbConnection(string.Format("Provider=VFPOLEDB.1;Data Source={0};Mode=Write;", Connect.parameters["KassaFile"]));
                    try
                    {
                        fpConnecton.Open();
                        OleDbDataAdapter a = new OleDbDataAdapter("", fpConnecton);
                #region cashAdapter
                        string[] columns = insert_columns.Split(new char[]{' ', ','}, StringSplitOptions.RemoveEmptyEntries);
                        if (columns.Length == 0)
                            throw new Exception("Количество столбцов для записи не может быть нулевым");
                        a.InsertCommand = new OleDbCommand(string.Format(@"INSERT INTO KASSA({0}) VALUES({1})",
                            string.Join(", ", columns.Select(r=>r.ToUpper())), string.Join(", ", columns.Select(r=>"?"))) , fpConnecton);
                        int i=0;
                        foreach (string s in columns)
                            a.InsertCommand.Parameters.Add(string.Format("p{0}", i++), CashColumnsMapping[s].OleDbType, CashColumnsMapping[s].Size, CashColumnsMapping[s].SourceColumn);

                        columns = update_columns.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        a.UpdateCommand = new OleDbCommand(string.Format("update KASSA set {0} where {1}",
                            string.Join(", ", columns.Select(r => r.ToUpper() + " = ?")), update_where), fpConnecton);
                        
                        foreach (string s in columns)
                            a.UpdateCommand.Parameters.Add(string.Format("p{0}", i++), CashColumnsMapping[s].OleDbType, CashColumnsMapping[s].Size, CashColumnsMapping[s].SourceColumn);
                        foreach (string s in update_where_columns.Split(new char[]{' ',','}, StringSplitOptions.RemoveEmptyEntries))
                            a.UpdateCommand.Parameters.Add(string.Format("p{0}", i++), CashColumnsMapping[s].OleDbType, CashColumnsMapping[s].Size, CashColumnsMapping[s].SourceColumn);

                        a.DeleteCommand = new OleDbCommand(string.Format(@"delete from kassa where {0}", delete_where), fpConnecton);
                        foreach (string s in delete_where_columns.Split(new char[]{' ',','}, StringSplitOptions.RemoveEmptyEntries))
                            a.DeleteCommand.Parameters.Add(string.Format("p{0}", i++), CashColumnsMapping[s].OleDbType, CashColumnsMapping[s].Size, CashColumnsMapping[s].SourceColumn);

                  #endregion

                        a.Update(t);
                        fpConnecton.Close();
                        ea.Result = t.Rows.Count;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка получения данных", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    finally
                    {
                        if (fpConnecton.State == ConnectionState.Open)
                            fpConnecton.Close();
                    }
                };
                bw.RunWorkerCompleted += (o, eo) => { OnWriteFinished(eo); };
                bw.RunWorkerAsync();
            }
            else
                MessageBox.Show("Не установлен драйвер FoxPro. Обратитесь к системным администраторам для установки", "Ошибка чтения таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        public static event  RunWorkerCompletedEventHandler WriteFinished;
        private static void OnWriteFinished(RunWorkerCompletedEventArgs e)
        {
            if (WriteFinished != null)
                WriteFinished(null, e);
        }
    }
}
