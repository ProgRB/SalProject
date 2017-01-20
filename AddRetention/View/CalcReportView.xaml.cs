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
using System.Windows.Shapes;
using System.ComponentModel;
using Oracle.DataAccess.Client;
using System.Data;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for CalcReportView.xaml
    /// </summary>
    public partial class CalcReportView : Window, INotifyPropertyChanged
    {
        BackgroundWorker bwCalcSubdivRetent = new BackgroundWorker();
        DataSet ds;
        private object _p_date;
        bool _autoUpdate = true, _autoScroll=true;
        System.Timers.Timer t;
        decimal? _timerTick=2;
        private OracleConnection connect;
        OracleCommand cmd_CalcRetent, cmd_CalcSubdivAdvance;
        DataTable dtTypePaymentCalc = new DataTable();
    
        public CalcReportView(object p_subdiv_id, object p_date)
        {
            _p_date = p_date;
            dtTypePaymentCalc = AppDataSet.Tables["TYPE_PAYMENT_TYPE"].Copy();
            dtTypePaymentCalc.Columns.Add("FL", typeof(bool));
            foreach (DataRow r in TypePaymentCalc)
                r["FL"] = true;
            cmd_CalcRetent = new OracleCommand(string.Format("begin {1}.CALC_SUBDIV_EMP_RETENTION(:p_subdiv_id, :p_date, :p_calced_type);end;", Connect.SchemaApstaff, Connect.SchemaSalary));
            cmd_CalcRetent.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, p_subdiv_id, ParameterDirection.Input);
            cmd_CalcRetent.Parameters.Add("p_date", OracleDbType.Date, p_date, ParameterDirection.Input);
            cmd_CalcRetent.Parameters.Add("p_calced_type", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
            cmd_CalcRetent.BindByName = true;

            cmd_CalcSubdivAdvance = new OracleCommand(string.Format("begin {1}.CALC_SUBDIV_ADVANCE(:p_subdiv_id, :p_date);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmd_CalcSubdivAdvance.BindByName = true;
            cmd_CalcSubdivAdvance.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            cmd_CalcSubdivAdvance.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);

            InitializeComponent();
            t = new System.Timers.Timer();
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            bwCalcSubdivRetent = new BackgroundWorker();
            bwCalcSubdivRetent.WorkerSupportsCancellation = true;
            bwCalcSubdivRetent.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwCalcSubdivRetent_RunWorkerCompleted);
            ds = new DataSet();
            this.PropertyChanged += new PropertyChangedEventHandler(CalcReportView_PropertyChanged);
            this.DataContext = this;
            CalcReportView_PropertyChanged(this, new PropertyChangedEventArgs("AutoUpdate"));
        }

        public DataRow[] TypePaymentCalc
        {
            get
            {
                return dtTypePaymentCalc.Select("TYPE_PAYMENT_TYPE_ID IN (1,2,3,6,9)");
            }
        }
        public bool this[decimal index]
        {
            get
            {
                return (bool)TypePaymentCalc.First(p => p.Field<Decimal>("TYPE_PAYMENT_TYPE_ID") == index)["FL"];
            }
            set
            {
                TypePaymentCalc.First(p => p.Field<Decimal>("TYPE_PAYMENT_TYPE_ID") == index)["FL"] = value;
                OnPropertyChanged("IsAdvanceEnable");
            }
        }

        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.BeginInvoke((Action)delegate() { if (IsRun) UpdateReportState(); });
        }

        void CalcReportView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AutoUpdate")
            {
                if (AutoUpdate)
                {
                    t.Stop();
                    t.Interval = (int)(TimerInterval * 1000);
                    t.Start();
                }
                else
                    t.Stop();
            }
            else if (e.PropertyName == "TimerInterval")
            {
                if (TimerInterval.HasValue && TimerInterval > 0)
                {
                    t.Stop();
                    t.Interval = (int)(this.TimerInterval * 1000);
                    t.Start();
                }
            }
        }

        public bool IsRun
        {
            get
            {
                return bwCalcSubdivRetent.IsBusy;
            }
        }

        public decimal? SubdivID
        {
            get
            {
                return (decimal?)cmd_CalcRetent.Parameters["p_subdiv_id"].Value;
            }
            set
            {
                cmd_CalcRetent.Parameters["p_subdiv_id"].Value = value;
                OnPropertyChanged("SubdivID");
            }
        }

        public bool AutoUpdate
        {
            get
            {
                return _autoUpdate;
            }
            set
            {
                _autoUpdate = value;
                OnPropertyChanged("AutoUpdate");
            }
        }

        public bool AutoScroll
        {
            get
            {
                return _autoScroll;
            }
            set
            {
                _autoScroll = value;
                OnPropertyChanged("AutoScroll");
            }
        }
        public decimal? TimerInterval
        {
            get
            {
                return _timerTick;
            }
            set
            {
                _timerTick = value;
                OnPropertyChanged("TimerInterval");
            }
        }

        public DateTime SelectedDate
        {
            get
            {
                return (DateTime)_p_date;
            }
            set
            {
                _p_date = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        bool _isAdvanceCalc=false;
        public bool IsAdvanceCalc
        {
            get
            {
                return _isAdvanceCalc;
            }
            set
            {
                _isAdvanceCalc = value;
                OnPropertyChanged("IsAdvanceCalc");
            }
        }

        public bool IsAdvanceEnable
        {
            get
            {
                return Array.TrueForAll(new int[] { 1, 2, 3, 6, 9 }, r => !this[r]);
            }
        }

        void bwCalcSubdivRetent_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnPropertyChanged("IsRun");
            if (e.Error == null)
            {
                MessageBox.Show(Window.GetWindow(this), "Расчет ЗП успешно закончен!", "Зарплата предприятия");
            }
            else
            {
                MessageBox.Show(Window.GetWindow(this), "Ошибка выполнения расчета:" + e.Error.GetFormattedException(), "Зарплата предприятия");
            }
            
        }
        void  CalcSubdivRetention(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (connect == null) connect = Connect.CreateNewUserConnection();
                cmd_CalcRetent.Connection = connect;
                cmd_CalcRetent.Parameters["p_subdiv_id"].Value = (e.Argument as object[])[0];
                cmd_CalcRetent.Parameters["p_date"].Value = (e.Argument as object[])[1];
                cmd_CalcRetent.Parameters["p_calced_type"].Value = (e.Argument as object[])[2];
                cmd_CalcRetent.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                switch (ex.Number)
                {
                    case 1013: throw new Exception("Выполнение расчета прервано");
                    default: throw new Exception(string.Format("Код ошибки:{0} Сообщение: {1}", ex.Number, ex.GetFormattedException()));
                }
            }
        }

        void CalcSubdivAdvance(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (connect == null) connect = Connect.CreateNewUserConnection();
                cmd_CalcSubdivAdvance.Connection = connect;
                cmd_CalcSubdivAdvance.Parameters["p_subdiv_id"].Value = (e.Argument as object[])[0];
                cmd_CalcSubdivAdvance.Parameters["p_date"].Value = (e.Argument as object[])[1];
                cmd_CalcSubdivAdvance.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                switch (ex.Number)
                {
                    case 1013: throw new Exception("Выполнение расчета прервано");
                    default: throw new Exception(string.Format("Код ошибки:{0} Сообщение: {1}", ex.Number, ex.GetFormattedException()));
                }
            }
        }

        private void btRefreshClick(object sender, RoutedEventArgs e)
        {
            UpdateReportState();
        }

        private void UpdateReportState()
        {
            new OracleDataAdapter(string.Format(Queries.GetQuery("GetCaclReport.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect).Fill(ds, "CalcReport");
            if (dgReportCalc.ItemsSource == null)
            {
                dgReportCalc.ItemsSource = new DataView(ds.Tables["CalcReport"], "", "", DataViewRowState.CurrentRows);
                ds.Tables["CalcReport"].PrimaryKey = new DataColumn[] { ds.Tables["CalcReport"].Columns["CALC_SAL_REPORT_ID"] };
            }
            if (_autoScroll) dgReportCalc.ScrollIntoView((dgReportCalc.ItemsSource as DataView)[(dgReportCalc.ItemsSource as DataView).Count - 1]);
        }

        private void CalcStart_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && !IsRun && (TypePaymentCalc.Any(p => (bool)p["FL"] || IsAdvanceCalc));
        }

        private void CalcStart_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ds.Tables.Contains("CalcReport"))
                ds.Tables["CalcReport"].Clear();
            bwCalcSubdivRetent.DoWork -= CalcSubdivRetention;
            bwCalcSubdivRetent.DoWork -= CalcSubdivAdvance;
            if (IsAdvanceEnable && IsAdvanceCalc)
                bwCalcSubdivRetent.DoWork += CalcSubdivAdvance;
            else
                bwCalcSubdivRetent.DoWork += CalcSubdivRetention;
            bwCalcSubdivRetent.RunWorkerAsync(new object[] { SubdivID, SelectedDate, TypePaymentCalc.Where(p => (bool)p["FL"]).Select(p => p.Field<Decimal>("TYPE_PAYMENT_TYPE_ID")).ToArray() });
            this.OnPropertyChanged("IsRun");
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            if (bwCalcSubdivRetent.IsBusy)
            {
                cmd_CalcRetent.Cancel();
                bwCalcSubdivRetent.CancelAsync();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                if (bwCalcSubdivRetent.IsBusy)
                {
                    MessageBox.Show("Не возможно закрыть во время запущенного расчета");
                    e.Cancel = true;
                }
                else
                {
                    bwCalcSubdivRetent.CancelAsync();
                    connect.Close();
                }
            }
            catch { };
        }
    }
}
