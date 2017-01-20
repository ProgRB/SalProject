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
using Salary.Helpers;
using System.Data;
using Oracle.DataAccess.Client;
using System.Data.OleDb;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for CacheVsSalaryCompare.xaml
    /// </summary>
    public partial class CacheVsSalaryCompare : Window, INotifyPropertyChanged
    {
        DataSet ds;
        BackgroundWorker bgWorker;
        public CacheVsSalaryCompare(DateTime? selectedDate, decimal? subdiv_id)
        {
            ds = new DataSet();
            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            ds = new DataSet();
            this._current_date = selectedDate;
            InitializeComponent();
            cbSubdivSelector.SubdivId = subdiv_id;
            this.DataContext = this;
        }

        DateTime? _current_date=DateTime.Today.Trunc("Month");
        public DateTime? CurrentDate
        {
            get
            {
                return _current_date;
            }
            set
            {
                _current_date = value;
                OnPropertyChanged("CurrentDate");
            }
        }

        string _paymentType = "271";
        public string CacheFilterPaymentValue
        {
            get
            {
                return _paymentType;
            }
            set
            {
                _paymentType = value;
                OnPropertyChanged("CacheFilterPaymentValue");
            }
        }

        List<TempCompareData> _salaryItems;
        public List<TempCompareData> SalaryItems
        {
            get
            {
                return _salaryItems;
            }
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void FillData(DateTime date, string code_subdiv)
        {
            if (ds.Tables.Contains("SALARY")) ds.Tables["SALARY"].Rows.Clear();
            if (ds.Tables.Contains("Cache")) ds.Tables["Cache"].Rows.Clear();
            using (OleDbConnection fpConnection = new OleDbConnection(string.Format("Provider=VFPOLEDB.1;Data Source={0};Mode=Read;", Connect.parameters["KassaFile"])))
            {
                fpConnection.Open();
                OleDbDataAdapter cad = new OleDbDataAdapter(Queries.GetQueryWithSchema("SelectCacheNotPayData.sql"), fpConnection);
                cad.SelectCommand.Parameters.Add("p1", OleDbType.Date).Value = _current_date;
                cad.SelectCommand.Parameters.Add("p2", OleDbType.Date).Value = _current_date;
                cad.SelectCommand.Parameters.Add("p3", OleDbType.Char).Value = _paymentType;
                cad.TableMappings.Add("Table", "Cache");
                cad.Fill(ds);
            }
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectCacheSalaryPayData.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, _current_date, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_payment", OracleDbType.Varchar2, _paymentType, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_code_subdiv", OracleDbType.Varchar2, code_subdiv, ParameterDirection.Input);
            oda.TableMappings.Add("Table", "Salary");
            oda.Fill(ds);
            DataRow r;
            var res = from r1 in ds.Tables["Cache"].AsEnumerable()
                      where code_subdiv=="000" || r1.Field2<string>("CODE_SUBDIV")==code_subdiv
                      select new TempCompareData()
                      {
                        FL=false,
                        CODE_SUBDIV=r1.Field<string>("CODE_SUBDIV"),
                        PER_NUM=r1.Field2<string>("PER_NUM"),
                        FIO = r1.Field2<string>("FIO"), 
                        SUM_CACHE = r1.Field2<decimal?>("SUM_SAL"),
                        SUM_SAL = ((r=GetSalaryString(r1.Field2<string>("PER_NUM"), r1.Field2<string>("CODE_SUBDIV")))==null? null: r.Field2<Decimal?>("SUM_SAL")),
                        SALARY_ID=((r=GetSalaryString(r1.Field2<string>("PER_NUM"), r1.Field2<string>("CODE_SUBDIV")))==null?null:r.Field2<Decimal?>("SALARY_ID"))
                      };
            _salaryItems = res.ToList();
        }


        private DataRow GetSalaryString(string per_num, string code_subdiv)
        {
            return ds.Tables["Salary"].Rows.OfType<DataRow>().Where(t => t.Field2<String>("PER_NUM") == per_num &&
                            t.Field2<String>("CODE_SUBDIV") == code_subdiv).FirstOrDefault();
        }

        public void BeginGetData()
        {
            bgWorker.RunWorkerAsync(new Tuple<DateTime?, string>(_current_date, cbSubdivSelector.CodeSubdiv));
            OnPropertyChanged("IsBusy");
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnPropertyChanged("IsBusy");
            if (e.Cancelled) return;
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.GetFormattedException(), "Ошибка получения данных");
                _salaryItems = null;
            }
            OnPropertyChanged("SalaryItems");
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Tuple<DateTime?, string> t = (Tuple<DateTime?, string>)e.Argument;
            FillData(t.Item1.Value, t.Item2);
        }

        public bool IsBusy
        {
            get
            {
                return bgWorker!=null && bgWorker.IsBusy;
            }
        }

        private void btFilter_Click(object sender, RoutedEventArgs e)
        {
            if (!this.IsBusy)
                BeginGetData();
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && CheckedItems != null && CheckedItems.Count() > 0;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранные записи из заработной платы?", "Зарплата предприяти", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Payment.DeleteEmpPayStaticRows(CheckedItems.ToArray());
            }
        }

        public IEnumerable<decimal> CheckedItems
        {
            get
            {
                if (_salaryItems != null)
                    return _salaryItems.Where(t => t.FL && t.SALARY_ID.HasValue).Select(t => t.SALARY_ID.Value);
                else return null;
            }
        }

        private void CheckAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (TempCompareData t in _salaryItems)
                t.FL = (sender as CheckBox).IsChecked.Value;
        }
    }

    public class TempCompareData:NotificationObject
    {
        public decimal? SALARY_ID
        {
            get;
            set;
        }
        public string FIO
        {
            get;
            set;
        }
        public string PER_NUM
        {
            get;
            set;
        }
        public string CODE_SUBDIV
        {
            get;
            set;
        }
        bool _fl = false;
        public bool FL
        {
            get
            {
                return _fl;
            }
            set
            {
                _fl = value;
                RaisePropertyChanged(() => FL);
            }
        }
        public decimal? SUM_CACHE
        {
            get;
            set;
        }
        public decimal? SUM_SAL
        {
            get;
            set;
        }
    }
}
