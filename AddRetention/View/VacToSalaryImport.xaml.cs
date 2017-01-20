using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Salary.Helpers;
using System.Data;
using Oracle.DataAccess.Client;
using System.Windows.Documents;
using System.Collections;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for VacToSalaryImport.xaml
    /// </summary>
    public partial class VacToSalaryImport : Window
    {
        public VacToSalaryImport(decimal? p_subdiv_id, DateTime? dateBegin, DateTime? dateEnd, bool IsCalcDocumAfter = false)
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(VacToSalaryImport_Closing);
            if (p_subdiv_id != null)
            {
                Model.SubdivID = p_subdiv_id;
                Model.DateBegin = dateBegin;
                Model.DateEnd = dateEnd;
                Model.CalcDocumAfter = IsCalcDocumAfter;
            }
            Model.LoadMainView();
            this.DataContext = Model;
            
        }

        void VacToSalaryImport_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Model != null)
                Model.CancelFetch();
        }
        VacToSalaryModel _model;
        public VacToSalaryModel Model
        {
            get
            {
                if (_model == null)
                    _model = new VacToSalaryModel();
                return _model;
            }
        }

        private void btClickRefresh_Click(object sender, RoutedEventArgs e)
        {
            Model.LoadMainView();
        }

        private void Dump_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model!=null && Model.CountSelected>0;
        }

        private void Dump_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Model.CurrentDump)
            {
                MessageBox.Show("Данные по текущему состоянию уже сформированы в зарплату. Чтобы посмотреть и проверить данные, обновите просмотр данных");
                return;
            }
            if (MessageBox.Show("Сфоромировать начисление отпускных для отмеченных сотрудников?", "Формирование отпускных", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DataTable t =Model.DumpVacs();
                    if (t != null)
                    {
                        this.DialogResult = true;
                        MessageBox.Show(string.Format("Успешно сформировано {0} записей на общую сумму {1} р.", t.Rows.Count, t.Rows.OfType<DataRow>().Sum(r => r.Field2<Decimal?>("SUM_SAL"))), "Зарплата предприятия");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования данных");
                }
            }
        }

        private void cbCheckAll_Checked(object sender, RoutedEventArgs e)
        {
            Model.SetCheckAll(cbCheckAll.IsChecked??false);
        }

        private void LabelVac_Click(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement f = sender as FrameworkElement;
            if (f != null)
            {
                ((ToolTip)f.ToolTip).IsOpen = true;
            }
        }
    }

    public class VacToSalaryModel : NotificationObject
    {
        DataSet ds = new DataSet();
        DataView mainView = null;
        OracleDataAdapter odaVacCompare;
        AbortableBackgroundWorker bw;
        public VacToSalaryModel(object subdiv_id=null)
        {
            odaVacCompare = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectComparedVacs.sql"), Connect.CurConnect);
            odaVacCompare.SelectCommand.BindByName=true;
            odaVacCompare.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, subdiv_id, ParameterDirection.Input);
            odaVacCompare.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, null, ParameterDirection.Input);
            odaVacCompare.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, null, ParameterDirection.Input);
            odaVacCompare.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            odaVacCompare.TableMappings.Add("Table", "VacCompare");

            cmdCreateDoc = new OracleCommand(string.Format("begin {1}.GENERATE_SALARY_VAC_DOCUM(:p_date_begin, :p_date_end, :p_subdiv_id, :p_sal_doc_ids, :p_worker_ids, :p_type_sal_docum_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmdCreateDoc.BindByName = true;
            cmdCreateDoc.Parameters.Add("p_date_begin", OracleDbType.Date, ParameterDirection.Input);
            cmdCreateDoc.Parameters.Add("p_date_end", OracleDbType.Date, ParameterDirection.Input);
            cmdCreateDoc.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, ParameterDirection.Input);
            cmdCreateDoc.Parameters.Add("p_sal_doc_ids", OracleDbType.Decimal,null, ParameterDirection.Input);
            cmdCreateDoc.Parameters.Add("p_type_sal_docum_id", OracleDbType.Decimal,null, ParameterDirection.Input);
            cmdCreateDoc.Parameters.Add("p_worker_ids", OracleDbType.Array, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
        }
        

        public List<DataRowView> MainView
        {
            get
            {
                if (mainView == null)
                {
                    if (ds != null && ds.Tables.Contains("VacCompare"))
                        mainView = new DataView(ds.Tables["VacCompare"], "", "", DataViewRowState.CurrentRows);
                }
                if (mainView != null)
                    return mainView.OfType<DataRowView>().ToList();
                else return null;
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
                RaisePropertyChanged(() => SubdivID);
            }
        }

        DateTime? dateBegin = DateTime.Today.Trunc("Month"), dateEnd = DateTime.Today;

        /// <summary>
        /// Дата окончания периода для фильтров отпусков
        /// </summary>
        public DateTime? DateEnd
        {
            get { return dateEnd; }
            set
            {
                dateEnd = value;
                RaisePropertyChanged(() => DateEnd);
            }
        }

        /// <summary>
        /// Дата начала периода для отпусков и фильтра
        /// </summary>
        public DateTime? DateBegin
        {
            get { return dateBegin; }
            set
            {
                dateBegin = value;
                RaisePropertyChanged(() => DateBegin);
            }
        }

        bool _isBusy = false;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(()=>IsBusy);
            }
        }

        public void LoadMainView()
        {
            if (IsBusy)
                return;
            bw= new AbortableBackgroundWorker();
            bw.ExecutingCommand = odaVacCompare.SelectCommand;
            odaVacCompare.SelectCommand.Parameters["p_subdiv_id"].Value = SubdivID;
            odaVacCompare.SelectCommand.Parameters["p_date_begin"].Value = DateBegin;
            odaVacCompare.SelectCommand.Parameters["p_date_end"].Value = DateEnd;
            if (ds.Tables.Contains("VacCompare"))
                ds.Tables["VacCompare"].Rows.Clear();
            bw.DoWork += (s, pw)
                =>
                {
                    OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                    a.Fill(ds);
                    if (!ds.Tables["VacCompare"].Columns.Contains("FL"))
                    {
                        ds.Tables["VacCompare"].Columns.Add("FL", typeof(bool)).DefaultValue = false;
                        foreach (DataRow r in ds.Tables["VacCompare"].Rows)
                            r["FL"] = false;
                    }
                };
            bw.RunWorkerCompleted += (s, pw) =>
                {
                    IsBusy = false;
                    if (pw.Cancelled) return;
                    else if (pw.Error != null)
                        MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                    else
                    {
                        CurrentDump = false;
                        RaisePropertyChanged(() => MainView);
                    };
                };
            IsBusy = true;
            bw.RunWorkerAsync(odaVacCompare);
        }

        /// <summary>
        /// Возвращает айдишник из справочника по коду вида оплат
        /// </summary>
        /// <param name="CodePayment"></param>
        /// <returns></returns>
        private decimal? GetPaymentType(string CodePayment)
        {
            return AppDataSet.Tables["PAYMENT_TYPE"].Rows.OfType<DataRow>().Where(r => r["CODE_PAYMENT"].ToString() == CodePayment).Select(t => t.Field2<Decimal?>("PAYMENT_TYPE_ID")).FirstOrDefault();
        }

        /// <summary>
        /// Возвращает код вида оплат
        /// </summary>
        /// <param name="payment_type_id">уникальный номер вида оплат</param>
        /// <returns></returns>
        private string GetCodePayment(decimal? payment_type_id)
        {
            return AppDataSet.Tables["PAYMENT_TYPE"].Rows.OfType<DataRow>().Where(r => r.Field2<Decimal?>("PAYMENT_TYPE_ID") == payment_type_id).Select(t => t.Field2<string>("CODE_PAYMENT")).FirstOrDefault();
        }

        private decimal? GetDefOrderID(string CodePayment, DataRow rd)
        {
            if (ds.Tables["VacCompare"].Columns.Contains("ORDER" + CodePayment))
                return rd.Field2<Decimal?>("ORDER" + CodePayment);
            else return 
                AppDataSet.Tables["PAYMENT_TYPE"].Rows.OfType<DataRow>()
                .Where(r => r["CODE_PAYMENT"].ToString() == CodePayment)
                .Select(t => t.Field2<Decimal?>("DEF_ORDER_ID")).FirstOrDefault();
        }

        private IEnumerable<object> GetPivotVacRows(string CodePayment)
        {
            decimal payment_type_id=GetPaymentType(CodePayment)??-1;
            var t = from r in ds.Tables["VacCompare"].AsEnumerable()
                    where r.Field2<bool>("FL") && r.Field2<DateTime?>("ACTUAL_BEGIN") != null
                         && r.Field2<Decimal?>("D"+CodePayment)!=null // check if vacs have days
                         && r.Field2<Decimal?>("D"+CodePayment+"_P")==null
                    select
                        new
                        {
                            SALARY_ID = (decimal?)null,
                            PAY_DATE = r.Field2<DateTime>("ACTUAL_BEGIN"),
                            PAYMENT_TYPE_ID = payment_type_id,
                            HOURS = 0m,
                            SUM_SAL = r.Field<Decimal?>("P"+CodePayment),
                            ZONE_ADD = 0m,
                            EXP_ADD = 0m,
                            ORDER_ID = GetDefOrderID(CodePayment, r)??-1,
                            GROUP_MASTER = string.Empty,
                            DEGREE_ID=r.Field2<Decimal?>("DEGREE_ID"),
                            TRANSFER_ID = r.Field2<Decimal?>("TRANSFER_ID"),
                            TYPE_REF_SALARY_ID=4m,
                            REF_ID = r.Field2<Decimal>("VAC_SCHED_ID"),
                            DAYS = r.Field2<Decimal?>("D"+CodePayment),
                            ACCOUNT_ADD_SIGN = 0m,
                            TIME_ADD_RECORD = DateTime.Now,
                            PER_NUM = r.Field2<string>("PER_NUM"),
                            SIGN_COMB = r.Field2<string>("SIGN_COMB")=="X"?1:0,
                            SUBDIV_ID = r.Field2<Decimal?>("SUBDIV_ID"),
                            RETENTION_ID = (decimal?)null,
                            TYPE_ROW_SALARY_ID = 0m
                        };
            return t;
        }

        public DataTable DumpVacs()
        {
            OracleDataAdapter odaVacDump = new OracleDataAdapter("", Connect.CurConnect);
            odaVacDump.InsertCommand = new OracleCommand(string.Format(@"begin {1}.SALARY_UPDATE(:p_SALARY_ID
                          ,:p_PAY_DATE
                          ,:p_PAYMENT_TYPE_ID
                          ,:p_HOURS
                          ,:p_SUM_SAL
                          ,:p_ZONE_ADD
                          ,:p_EXP_ADD 
                          ,:p_ORDER_ID
                          ,:p_GROUP_MASTER 
                          ,:p_DEGREE_ID 
                          ,:p_TRANSFER_ID
                          ,:p_TYPE_REF_SALARY_ID
                          ,:p_REF_ID
                          ,:p_DAYS
                          ,:p_ACCOUNT_ADD_SIGN
                          ,:p_TIME_ADD_RECORD
                          ,:p_PER_NUM
                          ,:p_SIGN_COMB
                          ,:p_SUBDIV_ID
                          ,:p_RETENTION_ID
                          ,:p_TYPE_ROW_SALARY_ID
                          ,:p_recalcDepend); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaVacDump.InsertCommand.BindByName = true;
            odaVacDump.InsertCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, 0, "SALARY_ID");
            odaVacDump.InsertCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE");
            odaVacDump.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            odaVacDump.InsertCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS");
            odaVacDump.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            odaVacDump.InsertCommand.Parameters.Add("p_ZONE_ADD", OracleDbType.Decimal, 0, "ZONE_ADD");
            odaVacDump.InsertCommand.Parameters.Add("p_EXP_ADD", OracleDbType.Decimal, 0, "EXP_ADD");
            odaVacDump.InsertCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");
            odaVacDump.InsertCommand.Parameters.Add("p_GROUP_MASTER", OracleDbType.Varchar2, 0, "GROUP_MASTER");
            odaVacDump.InsertCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            odaVacDump.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            odaVacDump.InsertCommand.Parameters.Add("p_TYPE_REF_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_REF_SALARY_ID");
            odaVacDump.InsertCommand.Parameters.Add("p_REF_ID", OracleDbType.Decimal, 0, "REF_ID");
            odaVacDump.InsertCommand.Parameters.Add("p_DAYS", OracleDbType.Decimal, 0, "DAYS");
            odaVacDump.InsertCommand.Parameters.Add("p_ACCOUNT_ADD_SIGN", OracleDbType.Decimal, 0, "ACCOUNT_ADD_SIGN");
            odaVacDump.InsertCommand.Parameters.Add("p_TIME_ADD_RECORD", OracleDbType.Date, 0, "TIME_ADD_RECORD");
            odaVacDump.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            odaVacDump.InsertCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            odaVacDump.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            odaVacDump.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID");
            odaVacDump.InsertCommand.Parameters.Add("p_TYPE_ROW_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_ROW_SALARY_ID");
            odaVacDump.InsertCommand.Parameters.Add("p_recalcDepend", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";

            var t = GetPivotVacRows("226").Union(GetPivotVacRows("227").Union(GetPivotVacRows("228").Union(GetPivotVacRows("258").Union(GetPivotVacRows("215")))));
            DataTable tb = null;
            tb = t.CopyToDataTable(tb, null);
            if (tb.Rows.Count == 0)
            {
                MessageBox.Show("Для выбранных сотрудников все записи в ЗП сформированы. Если имеются несовпадения, устраните их вручную", "Зарплата предприятия", MessageBoxButton.OK, MessageBoxImage.Information);
                return null;
            }
            foreach (DataRow r in tb.Rows)
            {
                r.SetAdded();
            }
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            
            try
            {
                odaVacDump.Update(tb);
                var exist_type_docum = (from row in tb.AsEnumerable()
                                        select GetCodePayment(row.Field2<decimal?>("PAYMENT_TYPE_ID")) == "215" ? 3m : 1m).Distinct().ToArray();
                if (CalcDocumAfter && MessageBox.Show("Сформировать документы отпускных и рассчитать их?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    CreateAndCalcDocsSelected(exist_type_docum);
                }
                tr.Commit();
                CurrentDump = true;
                return tb;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                throw ex;
            }
        }

        public void CreateAndCalcDocsSelected(decimal[] type_sal_doc_ids)
        {
            decimal max_id = (decimal) new OracleCommand(string.Format("select nvl(max(salary_docum_id),0) from {1}.salary_docum", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect).ExecuteScalar();
            cmdCreateDoc.Parameters["P_SUBDIV_ID"].Value = SubdivID;
            cmdCreateDoc.Parameters["P_DATE_BEGIN"].Value = DateBegin;
            cmdCreateDoc.Parameters["P_DATE_END"].Value = DateEnd;
            cmdCreateDoc.Parameters["P_WORKER_IDS"].Value = ds.Tables["VacCompare"].Rows.OfType<DataRow>().Where(t => t.Field2<bool>("FL")).Select(t=>t.Field2<Decimal>("WORKER_ID")).ToArray();
            foreach (decimal? val in type_sal_doc_ids)
            {
                cmdCreateDoc.Parameters["p_type_sal_docum_id"].Value = val;
                cmdCreateDoc.ExecuteNonQuery();
            }

            DataTable tt = new DataTable();
            OracleDataAdapter temp_a =new OracleDataAdapter(string.Format(@"select salary_docum_id from {1}.salary_docum join {0}.transfer using (transfer_id) 
                                    where salary_docum_id>:p_last_salary_doc_id and doc_subdiv_id=:p_subdiv_id and worker_id member of :p_worker_ids", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            temp_a.SelectCommand.BindByName=true;
            temp_a.SelectCommand.Parameters.Add("p_last_salary_doc_id", OracleDbType.Decimal, max_id, ParameterDirection.Input);
            temp_a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, SubdivID, ParameterDirection.Input);
            temp_a.SelectCommand.Parameters.Add("p_worker_ids", OracleDbType.Array, cmdCreateDoc.Parameters["P_WORKER_IDS"].Value, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";

            temp_a.Fill(tt);

            var array_calc = tt.Rows.OfType<DataRow>().Select(t => t.Field2<Decimal>("salary_docum_id")).ToArray();
            cmdCalcVacDocum = new OracleCommand(string.Format("begin {1}.CALC_VAC_DOCUMENT(:p_salary_docum_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmdCalcVacDocum.BindByName = true;
            cmdCalcVacDocum.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, array_calc, ParameterDirection.Input);
            cmdCalcVacDocum.ArrayBindCount = array_calc.Length;
            cmdCalcVacDocum.ExecuteNonQuery();
        }

        public int CountSelected
        {
            get
            {
                if (ds.Tables.Contains("VacCompare"))
                    return ds.Tables["VacCompare"].Rows.OfType<DataRow>().Count(t => t.Field2<bool>("FL"));
                else
                    return 0;
            }
        }

        public void SetCheckAll(bool value)
        {
            foreach (DataRow r in ds.Tables["VacCompare"].Rows)
                r["FL"] = value;
        }

        public void CancelFetch()
        {
            if (IsBusy && bw != null)
            {
                bw.Abort();
            }
        }

        private bool _currentDump = false;
        private OracleCommand cmdCreateDoc;
        private OracleCommand cmdCalcVacDocum;
        public bool CurrentDump
        {
            get
            {
                return _currentDump;
            }
            set
            {
                _currentDump = value;
                RaisePropertyChanged(() => CurrentDump);
            }
        }

        public bool CalcDocumAfter { get; set; }
    }

    public class VacToDataConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length > 1)
            {
                DataRowView v = values[0] as DataRowView;
                DataGridColumn c = values[1] as DataGridColumn;
                string s = SomeClass.GetTag(c).ToString();
                var p = new DataRowView[] { v }.Select(t => new { DaysVac = t["D" + s], DaysSalary = t["D" + s + "_D"], SumVac = t["P" + s], SumSalary = t["D" + s + "_P"] }).FirstOrDefault();
                return p;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
