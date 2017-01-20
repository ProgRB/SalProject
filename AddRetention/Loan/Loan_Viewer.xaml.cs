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
using LibrarySalary.Helpers;
using Salary;
using Salary.Loan.Classes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using Salary.View;
using System.Data.OleDb;

namespace Salary.Loan
{
    /// <summary>
    /// Interaction logic for Loan_Viewer.xaml
    /// </summary>
    public partial class Loan_Viewer : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private Visibility _isVisibleAddLoan = Visibility.Visible;
        public Visibility IsVisibleAddLoan
        {
            get
            {
                return _isVisibleAddLoan;
            }
        }

        private Visibility _isVisibleRetention = Visibility.Visible;
        public Visibility IsVisibleRetention
        {
            get
            {
                return _isVisibleRetention;
            }
        }

        private decimal? _transfer_ID;
        public decimal? Transfer_ID
        {
            get { return _transfer_ID; }
            set { _transfer_ID = value; }
        }

        private decimal? _loan_ID;
        public decimal? Loan_ID
        {
            get { return _loan_ID; }
            set
            {
                if (value != this._loan_ID)
                {
                    this._loan_ID = value;
                    OnPropertyChanged("Loan_ID");
                }
            }
        }
        private bool? _sign_reg_dog;
        public bool? Sign_reg_dog
        {
            get { return _sign_reg_dog; }
            set { _sign_reg_dog = value; }
        }
        public bool? Sing_Visible_Button_Guarantor
        {
            get { return !_sign_reg_dog; }
        }
        private DataSet _ds = new DataSet(), _dsRetention = new DataSet();
        private OracleDataAdapter _daLoan = new OracleDataAdapter(), _daGuarantor_Loan = new OracleDataAdapter(),
            _daRetention = new OracleDataAdapter();
        string stFilter = "LOAN_DATE_END is null";
        DataRowView _currentRow;

        public Loan_Viewer(bool sign_reg_dog)
        {
            this.PropertyChanged += new PropertyChangedEventHandler(Loan_Viewer_PropertyChanged);
            _isVisibleAddLoan = sign_reg_dog ? Visibility.Collapsed : Visibility.Visible;
            _isVisibleRetention = sign_reg_dog ? Visibility.Visible : Visibility.Collapsed;
            Sign_reg_dog = sign_reg_dog;
            InitializeComponent();

            _ds.Tables.Add("LOAN");
            _ds.Tables.Add("LOAN_ROW");
            _ds.Tables.Add("GUARANTOR_LOAN");
            _daLoan.TableMappings.Add("Table", "LOAN");
            // Select
            _daLoan.SelectCommand = new OracleCommand(string.Format(Queries.GetQuery("Loan/SelectLoan.sql"),
                Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
            _daLoan.SelectCommand.BindByName = true;
            _daLoan.SelectCommand.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal);
            _daLoan.SelectCommand.Parameters.Add("p_SIGN_REGISTRATION_DOG", OracleDbType.Decimal).Value = sign_reg_dog;
            _daLoan.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            // Delete
            _daLoan.DeleteCommand = new OracleCommand(string.Format(
                @"begin 
                    {1}.LOAN_DML_PKG.LOAN_DELETE(:LOAN_ID); 
                end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            _daLoan.DeleteCommand.BindByName = true;
            _daLoan.DeleteCommand.Parameters.Add("LOAN_ID", OracleDbType.Decimal, 0, "LOAN_ID");

            // Select
            _daGuarantor_Loan.SelectCommand = new OracleCommand(string.Format(Queries.GetQuery("Loan/SelectGuarantor_Loan.sql"),
                Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
            _daGuarantor_Loan.SelectCommand.BindByName = true;
            _daGuarantor_Loan.SelectCommand.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal);
            // Insert
            _daGuarantor_Loan.InsertCommand = new OracleCommand(string.Format(
                @"BEGIN
                        {0}.LOAN_DML_PKG.GUARANTOR_LOAN_UPDATE(:GUARANTOR_LOAN_ID, :TRANSFER_ID, :LOAN_ID, :GUARANTOR_CONTRACT_NUMBER, :GUARANTOR_CONTRACT_DATE);
                    END;", Connect.SchemaSalary), Connect.CurConnect);
            _daGuarantor_Loan.InsertCommand.BindByName = true;
            _daGuarantor_Loan.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            _daGuarantor_Loan.InsertCommand.Parameters.Add("GUARANTOR_LOAN_ID", OracleDbType.Decimal, 0, "GUARANTOR_LOAN_ID").Direction =
                ParameterDirection.InputOutput;
            _daGuarantor_Loan.InsertCommand.Parameters["GUARANTOR_LOAN_ID"].DbType = DbType.Decimal;
            _daGuarantor_Loan.InsertCommand.Parameters.Add("TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _daGuarantor_Loan.InsertCommand.Parameters.Add("LOAN_ID", OracleDbType.Decimal, 0, "LOAN_ID");
            _daGuarantor_Loan.InsertCommand.Parameters.Add("GUARANTOR_CONTRACT_NUMBER", OracleDbType.Varchar2, 20, "GUARANTOR_CONTRACT_NUMBER").Direction =
                ParameterDirection.InputOutput;
            _daGuarantor_Loan.InsertCommand.Parameters["GUARANTOR_CONTRACT_NUMBER"].DbType = DbType.String;
            _daGuarantor_Loan.InsertCommand.Parameters.Add("GUARANTOR_CONTRACT_DATE", OracleDbType.Date, 0, "GUARANTOR_CONTRACT_DATE").Direction = 
                ParameterDirection.InputOutput;
            _daGuarantor_Loan.InsertCommand.Parameters["GUARANTOR_CONTRACT_DATE"].DbType = DbType.Date;
            // Update
            _daGuarantor_Loan.UpdateCommand = _daGuarantor_Loan.InsertCommand;
            // Delete
            _daGuarantor_Loan.DeleteCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.GUARANTOR_LOAN_DELETE(:GUARANTOR_LOAN_ID);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daGuarantor_Loan.DeleteCommand.BindByName = true;
            _daGuarantor_Loan.DeleteCommand.Parameters.Add("GUARANTOR_LOAN_ID", OracleDbType.Decimal, 0, "GUARANTOR_LOAN_ID");
            
            GetLoans();
            // 04.04.2016 - начал загружать и текущие ссуды и архивные, чтобы была возможность просмотра всех ссуд в одном окне
            // добавил фильтр и здесь сразу выполняю нужные операции
            chView_Archive.IsChecked = false;
            _ds.Tables["LOAN"].DefaultView.RowFilter = "LOAN_DATE_END is null";            
            dgLoan.DataContext = _ds.Tables["LOAN"].DefaultView;
            var _dvSubdiv = (from r in _ds.Tables["LOAN"].AsEnumerable()
                             select new { SubdivID = r["SUBDIV_ID"], CodeSubdiv = r["CODE_SUBDIV"], SubdivName = r["SUBDIV_NAME"] }).Distinct();
            cbCodeSubdiv.ItemsSource = _dvSubdiv;
            cbSubdivName.ItemsSource = _dvSubdiv;
            cbCodeSubdiv.SelectedItem = null;
            cbPurpose_Loan.ItemsSource = LoanDataSet.Purpose_Loan.DefaultView;
            cbType_Loan.ItemsSource = LoanDataSet.Type_Loan.DefaultView;

            dgGuarantor_Loan.DataContext = _ds.Tables["GUARANTOR_LOAN"].DefaultView;

            _daRetention = new OracleDataAdapter(string.Format(
                @"BEGIN
                    {0}.LOAN_REPORTS.GetRetention(:p_LOAN_ID, :c1);
                END;", Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
            _daRetention.SelectCommand.BindByName = true;
            _daRetention.SelectCommand.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal, 0, ParameterDirection.Input);
            _daRetention.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            _daRetention.TableMappings.Add("Table", "SALARY");
            _daRetention.Fill(_dsRetention);
        }

        void Loan_Viewer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Loan_ID")
            {
                GetGuarantor_Loan(this.Loan_ID);
                GetRetention(this.Loan_ID);
            }
        }

        void GetLoans()
        {
            dgLoan.DataContext = null;
            _ds.Tables["LOAN"].Clear();
            _daLoan.Fill(_ds.Tables["LOAN"]);
            dgLoan.DataContext = _ds.Tables["LOAN"].DefaultView;
            _ds.Tables["LOAN"].CaseSensitive = false;
        }

        void GetGuarantor_Loan(decimal? loan_ID)
        {
            dgGuarantor_Loan.DataContext = null;
            if (loan_ID != null)
            {
                _ds.Tables["GUARANTOR_LOAN"].Clear();
                _daGuarantor_Loan.SelectCommand.Parameters["p_LOAN_ID"].Value = loan_ID;
                _daGuarantor_Loan.Fill(_ds.Tables["GUARANTOR_LOAN"]);
                dgGuarantor_Loan.DataContext = _ds.Tables["GUARANTOR_LOAN"].DefaultView;
            }
        }

        void GetRetention(decimal? loan_ID)
        {
            if (_dsRetention != null && _dsRetention.Tables.Contains("SALARY"))
            {
                _dsRetention.Tables["SALARY"].BeginLoadData();
                _dsRetention.Tables["SALARY"].Rows.Clear();
            }
            if (loan_ID == null)
            {
                ListCollectionView l = (dgEmpPaySalary.ItemsSource as ListCollectionView);
                if (l != null)
                    l.Refresh();
                return;
            }            
            try
            {
                _daRetention.SelectCommand.Parameters["p_LOAN_ID"].Value = loan_ID;
                _daRetention.Fill(_dsRetention);

                if (dgEmpPaySalary.ItemsSource == null)
                {
                    //sal_view = new DataView(ds.Tables["Salary"], "", "TYPE_PAYMENT_TYPE_ID, PAY_MONTH, CODE_PAYMENT", DataViewRowState.CurrentRows);
                    ListCollectionView cv = new ListCollectionView(_dsRetention.Tables["SALARY"].DefaultView);
                    cv.GroupDescriptions.Add(new PropertyGroupDescription("NOTE_DOC"));
                    cv.SortDescriptions.Add(new SortDescription("PAY_DATE", ListSortDirection.Ascending));
                    dgEmpPaySalary.DataContext = cv;
                }
                else
                    (dgEmpPaySalary.ItemsSource as ListCollectionView).Refresh();
            }
            catch
            {
                if (_dsRetention != null & _dsRetention.Tables.Contains("SALARY"))
                    _dsRetention.Tables["SALARY"].EndLoadData();
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private void Filter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //filterGroup.UpdateSources();
            }
        }
        
        private void Expander_Exp(object sender, RoutedEventArgs e)
        {
            Expander ex = sender as Expander;
            ExpandStateSaver.states_exp[(ex.DataContext as CollectionViewGroup).Name.ToString()] = ex.IsExpanded;
        }

        private void Expander_Coll(object sender, RoutedEventArgs e)
        {
            try
            {
                Expander ex = sender as Expander;
                ExpandStateSaver.states_exp[(ex.DataContext as CollectionViewGroup).Name.ToString()] = ex.IsExpanded;
            }
            catch { };
        }

        private void btApplyFilterLoan_Click(object sender, RoutedEventArgs e)
        {
            stFilter = "";
            if (cbCodeSubdiv.SelectedValue != null)
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "SUBDIV_ID = " + cbCodeSubdiv.SelectedValue,
                    stFilter != "" ? "and" : "").Trim();
            }
            if (tbPer_num.Text.Trim() != "")
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "PER_NUM = " + tbPer_num.Text.Trim().PadLeft(5, '0'),
                    stFilter != "" ? "and" : "").Trim();
            }
            if (tbEmp_last_name.Text.Trim() != "")
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "EMP_LAST_NAME like '%" + tbEmp_last_name.Text.Trim() + "%'",
                    stFilter != "" ? "and" : "").Trim();
            }
            if (tbEmp_first_name.Text != "")
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "EMP_FIRST_NAME like '%" + tbEmp_first_name.Text.Trim() + "%'",
                    stFilter != "" ? "and" : "").Trim();
            }
            if (tbEmp_middle_name.Text != "")
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "EMP_MIDDLE_NAME like '%" + tbEmp_middle_name.Text.Trim() + "%'",
                    stFilter != "" ? "and" : "").Trim();
            }
            if (tbLOAN_TERM.Text != "")
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "LOAN_TERM = " + Convert.ToInt16(tbLOAN_TERM.Text),
                    stFilter != "" ? "and" : "").Trim();
            }
            if (tbPROTOCOL_NUMBER.Text != "")
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "PROTOCOL_NUMBER = '" + tbPROTOCOL_NUMBER.Text + "'",
                    stFilter != "" ? "and" : "").Trim();
            }
            if (tbCONTRACT_NUMBER.Text != "")
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "CONTRACT_NUMBER = '" + tbCONTRACT_NUMBER.Text + "'",
                    stFilter != "" ? "and" : "").Trim();
            }
            if (dpPeriodBegin.SelectedDate != null)
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "LOAN_DATE >= #" + dpPeriodBegin.SelectedDate.Value.ToString("MM.dd.yyyy") + "#",
                    stFilter != "" ? "and" : "").Trim();
            }
            if (dpPeriodEnd.SelectedDate != null)
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "LOAN_DATE <= #" + dpPeriodEnd.SelectedDate.Value.ToString("MM.dd.yyyy") + "#",
                    stFilter != "" ? "and" : "").Trim();
            }
            if (dpLoan_Date_End_Begin.SelectedDate != null)
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "LOAN_DATE_END >= #" + dpLoan_Date_End_Begin.SelectedDate.Value.ToString("MM.dd.yyyy") + "#",
                    stFilter != "" ? "and" : "").Trim();
            }
            if (dpLoan_Date_End_End.SelectedDate != null)
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "LOAN_DATE_END <= #" + dpLoan_Date_End_End.SelectedDate.Value.ToString("MM.dd.yyyy") + "#",
                    stFilter != "" ? "and" : "").Trim();
            }
            if (cbPurpose_Loan.SelectedValue != null)
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "PURPOSE_LOAN_ID = " + cbPurpose_Loan.SelectedValue,
                    stFilter != "" ? "and" : "").Trim();
            }
            if (cbType_Loan.SelectedValue != null)
            {
                stFilter = string.Format("{0} {2} {1}", stFilter, "TYPE_LOAN_ID = " + cbType_Loan.SelectedValue,
                    stFilter != "" ? "and" : "").Trim();
            }
            if (chView_Archive.IsChecked != null)
            {
                if (chView_Archive.IsChecked == true)
                {
                    stFilter = string.Format("{0} {2} {1}", stFilter, "LOAN_DATE_END is not null",
                        stFilter != "" ? "and" : "").Trim();
                }
                else
                {
                    stFilter = string.Format("{0} {2} {1}", stFilter, "LOAN_DATE_END is null",
                        stFilter != "" ? "and" : "").Trim();
                }
            }
            if (chSIGN_RETENTION.IsChecked != null)
            {
                if (chSIGN_RETENTION.IsChecked == true)
                {
                    stFilter = string.Format("{0} {2} {1}", stFilter, "SIGN_RETENTION = 1",
                        stFilter != "" ? "and" : "").Trim();
                }
                else
                {
                    stFilter = string.Format("{0} {2} {1}", stFilter, "SIGN_RETENTION = 0",
                        stFilter != "" ? "and" : "").Trim();
                }
            }
            if (chSIGN_MATERIAL_BENEFIT.IsChecked != null)
            {
                if (chSIGN_MATERIAL_BENEFIT.IsChecked == true)
                {
                    stFilter = string.Format("{0} {2} {1}", stFilter, "SIGN_MATERIAL_BENEFIT = 1",
                        stFilter != "" ? "and" : "").Trim();
                }
                else
                {
                    stFilter = string.Format("{0} {2} {1}", stFilter, "SIGN_MATERIAL_BENEFIT = 0",
                        stFilter != "" ? "and" : "").Trim();
                }
            }
            _ds.Tables["LOAN"].DefaultView.RowFilter = stFilter;
        }

        private void btClearFilterLoan_Click(object sender, RoutedEventArgs e)
        {
            stFilter = "LOAN_DATE_END is null";
            cbCodeSubdiv.SelectedValue = null;
            tbPer_num.Text = "";
            tbEmp_last_name.Text = "";
            tbEmp_first_name.Text = "";
            tbEmp_middle_name.Text = "";
            tbLOAN_TERM.Text = "";
            tbPROTOCOL_NUMBER.Text = "";
            tbCONTRACT_NUMBER.Text = "";
            dpPeriodBegin.SelectedDate = null;
            dpPeriodEnd.SelectedDate = null;
            dpLoan_Date_End_Begin.SelectedDate = null;
            dpLoan_Date_End_End.SelectedDate = null;
            cbPurpose_Loan.SelectedValue = null;
            cbType_Loan.SelectedValue = null;
            chView_Archive.IsChecked = false;
            _ds.Tables["LOAN"].DefaultView.RowFilter = stFilter;
        }

        private void EditLoan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()) &&
                dgLoan != null && dgLoan.SelectedCells.Count > 0)
                e.CanExecute = true;
        }

        private void EditLoan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Loan_Editor loan_Edit = new Loan_Editor(Convert.ToDecimal((dgLoan.SelectedCells[0].Item as DataRowView)["LOAN_ID"]));
            loan_Edit.Owner = Window.GetWindow(this);
            loan_Edit.ShowDialog();
            if (loan_Edit.Model.LoanID != null)
                UpdateCurrentLoan(loan_Edit.Model.LoanID);
        }

        private void UpdateCurrentLoan(object loan_ID)
        {
            _ds.Tables["LOAN_ROW"].Clear();
            _daLoan.SelectCommand.Parameters["p_LOAN_ID"].Value = loan_ID;
            _daLoan.Fill(_ds.Tables["LOAN_ROW"]);
            if (_ds.Tables["LOAN_ROW"].Rows.Count > 0)
            {
                _ds.Tables["LOAN"].PrimaryKey = new DataColumn[] { _ds.Tables["LOAN"].Columns["LOAN_ID"] };
                _ds.Tables["LOAN"].LoadDataRow(_ds.Tables["LOAN_ROW"].Rows[0].ItemArray, LoadOption.OverwriteChanges);
            }
            else
            {
                _ds.Tables["LOAN"].PrimaryKey = new DataColumn[] { _ds.Tables["LOAN"].Columns["LOAN_ID"] };
                _ds.Tables["LOAN"].Rows.Remove(_ds.Tables["LOAN"].Rows.Find(loan_ID));
            }
            _daLoan.SelectCommand.Parameters["p_LOAN_ID"].Value = null;
        }
        
        private void PrintLoan_Contract_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Salary.View.SignesRecord[] sr = null;
            if (Salary.View.Signes.Show(0, "Loan_Contract", "Выберите подписанта договора", 1, ref sr) == true)
            {
                PrintLoan_Contract(((DataRowView)dgLoan.SelectedCells[0].Item)["LOAN_ID"], sr);
            }
        }

        void PrintLoan_Contract(object loan_ID, Salary.View.SignesRecord[] sr)
        {
            OracleDataAdapter oda = new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/RepLoan_Contract.sql"), Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_loan_id", OracleDbType.Decimal, loan_ID, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            Salary.Helpers.AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Договор займа...", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    DataSet dd = pw.Result as DataSet;
                    Salary.Reports.ViewReportWindow.ShowReport(this, "Договор займа", "Rep_Loan/Loan_Contract.rdlc",
                        //new Salary.Reports.SubReport[] { new Salary.Reports.SubReport("Subreport1", "Rep_Loan/Loan_Contract_Subreport.rdlc", dd.Tables[1]) },
                        new DataTable[] { dd.Tables[0] },
                        new Microsoft.Reporting.WinForms.ReportParameter[] { 
                                new Microsoft.Reporting.WinForms.ReportParameter("P_SIGNES_POS", sr[0].PosName),
                                new Microsoft.Reporting.WinForms.ReportParameter("P_SIGNES_FIO", sr[0].EmpName)
                                    });
                });
        }

        private void PrintGuarantor_Contract_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Salary.View.SignesRecord[] sr = null;
            if (Salary.View.Signes.Show(0, "Loan_Contract", "Выберите подписанта договора", 1, ref sr) == true)
            {
                PrintGuarantor_Contract(((DataRowView)dgLoan.SelectedCells[0].Item)["LOAN_ID"], sr);
            }
        }

        void PrintGuarantor_Contract(object loan_ID, Salary.View.SignesRecord[] sr)
        {
            OracleDataAdapter oda = new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/RepGuarantor_Contract.sql"), Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_loan_id", OracleDbType.Decimal, loan_ID, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            Salary.Helpers.AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Договор поручительства...", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    DataSet dd = pw.Result as DataSet;
                    if (dd.Tables[0].Rows.Count > 0 )
                        Salary.Reports.ViewReportWindow.ShowReport(this, "Договор поручительства", "Rep_Loan/Guarantor_Contract.rdlc",
                            new DataTable[] { dd.Tables[0] },
                            new Microsoft.Reporting.WinForms.ReportParameter[] { 
                                new Microsoft.Reporting.WinForms.ReportParameter("P_SIGNES_POS", sr[0].PosName),
                                new Microsoft.Reporting.WinForms.ReportParameter("P_SIGNES_FIO", sr[0].EmpName)
                                    });
                });
        }

        private void PrintSchedule_Of_Payments_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Salary.View.SignesRecord[] sr = null;
            if (Salary.View.Signes.Show(0, "Loan_Contract", "Выберите подписанта договора", 1, ref sr) == true)
            {
                PrintSchedule_Of_Payments(((DataRowView)dgLoan.SelectedCells[0].Item)["LOAN_ID"], sr);
            }
        }

        void PrintSchedule_Of_Payments(object loan_ID, Salary.View.SignesRecord[] sr)
        {
            OracleDataAdapter oda = new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/RepSchedule_Of_Payments.sql"), Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_loan_ID", OracleDbType.Decimal, loan_ID, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            Salary.Helpers.AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "График платежей...", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    DataSet dd = pw.Result as DataSet;
                    Salary.Reports.ViewReportWindow.ShowReport(this, "График платежей", "Rep_Loan/Schedule_Of_Payments.rdlc",
                        new DataTable[] { dd.Tables[0], dd.Tables[1] },
                        new Microsoft.Reporting.WinForms.ReportParameter[] { 
                                new Microsoft.Reporting.WinForms.ReportParameter("P_SIGNES_POS", sr[0].PosName),
                                new Microsoft.Reporting.WinForms.ReportParameter("P_SIGNES_FIO", sr[0].EmpName)
                                    });
                });
        }

        private void PrintStatement_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PrintStatement(((DataRowView)dgLoan.SelectedCells[0].Item)["LOAN_ID"]);
        }

        void PrintStatement(object loan_ID)
        {
            OracleDataAdapter oda = new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/RepStatement.sql"), Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_loan_ID", OracleDbType.Decimal, loan_ID, ParameterDirection.Input);
            Salary.Helpers.AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Заявление...", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    DataSet dd = pw.Result as DataSet;
                    Salary.Reports.ViewReportWindow.ShowReport(this, "Заявление", "Rep_Loan/Statement.rdlc",
                        new DataTable[] { dd.Tables[0] },
                        new Microsoft.Reporting.WinForms.ReportParameter[] { });
                });
        }

        private void PrintStatement_Transfer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PrintStatement_Transfer(((DataRowView)dgLoan.SelectedCells[0].Item)["LOAN_ID"]);
        }

        void PrintStatement_Transfer(object loan_ID)
        {
            OracleDataAdapter oda = new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/RepStatement.sql"), Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_loan_ID", OracleDbType.Decimal, loan_ID, ParameterDirection.Input);
            Salary.Helpers.AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Заявление для перечисления...", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    DataSet dd = pw.Result as DataSet;
                    Salary.Reports.ViewReportWindow.ShowReport(this, "Заявление для перечисления", "Rep_Loan/Statement_Transfer.rdlc",
                        new DataTable[] { dd.Tables[0] },
                        new Microsoft.Reporting.WinForms.ReportParameter[] { });
                });
        }

        private void PrintAllDocuments_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Salary.View.SignesRecord[] sr = null;
            if (Salary.View.Signes.Show(0, "Loan_Contract", "Выберите подписанта документов", 1, ref sr) == true)
            {
                PrintLoan_Contract(((DataRowView)dgLoan.SelectedCells[0].Item)["LOAN_ID"], sr);
                PrintGuarantor_Contract(((DataRowView)dgLoan.SelectedCells[0].Item)["LOAN_ID"], sr);
                PrintSchedule_Of_Payments(((DataRowView)dgLoan.SelectedCells[0].Item)["LOAN_ID"], sr);
                PrintStatement(((DataRowView)dgLoan.SelectedCells[0].Item)["LOAN_ID"]);
                PrintStatement_Transfer(((DataRowView)dgLoan.SelectedCells[0].Item)["LOAN_ID"]);
            }
        }

        private void dgLoan_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgLoan.SelectedCells.Count == 0)
            {
                this.Transfer_ID = null;
                this.Loan_ID = null;
            }
            else if (_currentRow != (DataRowView)dgLoan.SelectedCells[0].Item)
            {
                _currentRow = (DataRowView)dgLoan.SelectedCells[0].Item;
                this.Transfer_ID = (decimal)_currentRow["TRANSFER_ID"];
                this.Loan_ID = (decimal)_currentRow["LOAN_ID"];
            }
        }

        private void AddLoan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void AddLoan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Loan_Editor loan_Edit = new Loan_Editor(-1);
            loan_Edit.Owner = Window.GetWindow(this);
            loan_Edit.ShowDialog();
            if (loan_Edit.Model.LoanID != null)
                UpdateCurrentLoan(loan_Edit.Model.LoanID);
        }

        private void DeleteLoan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Ссуды", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                while (dgLoan.SelectedCells.Count > 0)
                {
                    ((DataRowView)dgLoan.SelectedCells[0].Item).Delete();
                }
                SaveLoan();
            }
        }

        /// <summary>
        /// Сохранение данных (только удаленные записи)
        /// </summary>
        /// <returns></returns>
        public bool SaveLoan()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                _daLoan.DeleteCommand.Transaction = tr;
                _daLoan.Update(_ds.Tables["LOAN"]);
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException());
                return false;
            }
        }

        private void Account_Cash_Order_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Cash_Order_Editor _cash_Order_Editor = new Cash_Order_Editor(2, this.Loan_ID);
            _cash_Order_Editor.Owner = Window.GetWindow(this);
            _cash_Order_Editor.ShowDialog();            
        }

        private void AddGuarantor_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()) &&
                this.Loan_ID != null)
                e.CanExecute = true;
        }

        private void AddGuarantor_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView newRow = _ds.Tables["GUARANTOR_LOAN"].DefaultView.AddNew();
            newRow["LOAN_ID"] = this.Loan_ID;
            _ds.Tables["GUARANTOR_LOAN"].Rows.Add(newRow.Row);
            dgGuarantor_Loan.SelectedItem = newRow;
            Guarantor_Loan_Editor _guarantor = new Guarantor_Loan_Editor(newRow);
            _guarantor.Owner = Window.GetWindow(this);
            if (_guarantor.ShowDialog() == true)
            {
                SaveGuarantor_Loan();
            }
            else
            {
                _ds.Tables["GUARANTOR_LOAN"].RejectChanges();
            }
        }

        private void DeleteGuarantor_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgGuarantor_Loan != null && dgGuarantor_Loan.SelectedCells.Count > 0 &&
                ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void DeleteGuarantor_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Ссуды",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                while (dgGuarantor_Loan.SelectedCells.Count > 0)
                {
                    (dgGuarantor_Loan.SelectedCells[0].Item as DataRowView).Delete();
                    SaveGuarantor_Loan();
                }
            }
            dgGuarantor_Loan.Focus();
        }

        private void EditGuarantor_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Guarantor_Loan_Editor _guarantor = new Guarantor_Loan_Editor(dgGuarantor_Loan.SelectedCells[0].Item as DataRowView);
            _guarantor.Owner = Window.GetWindow(this);
            if (_guarantor.ShowDialog() == true)
            {
                SaveGuarantor_Loan();
            }
            else
            {
                _ds.Tables["GUARANTOR_LOAN"].RejectChanges();
            }
        }

        void SaveGuarantor_Loan()
        {
            OracleTransaction transact = Connect.CurConnect.BeginTransaction();
            try
            {
                _daGuarantor_Loan.InsertCommand.Transaction = transact;
                _daGuarantor_Loan.UpdateCommand.Transaction = transact;
                _daGuarantor_Loan.DeleteCommand.Transaction = transact;
                _daGuarantor_Loan.Update(_ds.Tables["GUARANTOR_LOAN"]);
                transact.Commit();
            }
            catch (Exception ex)
            {
                transact.Rollback();
                MessageBox.Show(ex.Message, "Ссуды: Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                _ds.Tables["GUARANTOR_LOAN"].RejectChanges();
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private void Transfer_Loan_To_Guarantor_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите перевести данные ссуды на Поручителя?", 
                "Ссуды", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                FilterByPeriod f = new FilterByPeriod();
                f.Owner = Window.GetWindow(this);
                f.Model.IsEndEnabled = false;
                f.Title = "Дата перевода ссуды";
                if (f.ShowDialog() == true)
                {
                    OracleCommand _ocTransfer_Loan = new OracleCommand(string.Format(
                        @"BEGIN
                            {0}.LOAN_DML_PKG.TRANSFER_LOAN_TO_GUARANTOR(:p_LOAN_ID, :p_GUARANTOR_LOAN_ID, :p_LOAN_DATE_END, :p_NEW_LOAN_ID);
                        END;", Connect.SchemaSalary), Connect.CurConnect);
                    _ocTransfer_Loan.UpdatedRowSource = UpdateRowSource.OutputParameters;
                    _ocTransfer_Loan.BindByName = true;
                    _ocTransfer_Loan.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal).Value = Loan_ID;
                    _ocTransfer_Loan.Parameters.Add("p_GUARANTOR_LOAN_ID", OracleDbType.Decimal).Value =
                        ((DataRowView)dgGuarantor_Loan.SelectedCells[0].Item)["GUARANTOR_LOAN_ID"];
                    _ocTransfer_Loan.Parameters.Add("p_LOAN_DATE_END", OracleDbType.Date).Value = f.Model.DateBegin;
                    _ocTransfer_Loan.Parameters.Add("p_NEW_LOAN_ID", OracleDbType.Decimal).Direction = ParameterDirection.Output;
                    OracleTransaction transact = Connect.CurConnect.BeginTransaction();
                    try
                    {
                        _ocTransfer_Loan.Transaction = transact;
                        _ocTransfer_Loan.ExecuteNonQuery();
                        transact.Commit();
                        MessageBox.Show("Изменения сохранены в БД.", "Ссуды", MessageBoxButton.OK, MessageBoxImage.Information);
                        UpdateCurrentLoan(Loan_ID);
                        UpdateCurrentLoan(_ocTransfer_Loan.Parameters["p_NEW_LOAN_ID"].Value);
                    }
                    catch (Exception ex)
                    {
                        transact.Rollback();
                        MessageBox.Show(ex.Message, "Ссуды: Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Transfer_Loan_To_Third_Person_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите перевести данные ссуды на Третье лицо?",
                "Ссуды", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Find_Emp find_Emp = new Find_Emp(DateTime.Today);
                find_Emp.Owner = Window.GetWindow(this);
                if (find_Emp.ShowDialog() == true)
                {
                    FilterByPeriod f = new FilterByPeriod();
                    f.Owner = Window.GetWindow(this);
                    f.Model.IsEndEnabled = false;
                    f.Title = "Дата перевода ссуды";
                    if (f.ShowDialog() == true)
                    {
                        OracleCommand _ocTransfer_Loan = new OracleCommand(string.Format(
                            @"BEGIN
                                {0}.LOAN_DML_PKG.TRANSFER_LOAN_TO_THIRD_PERSON(:p_LOAN_ID, :p_TRANSFER_ID, :p_LOAN_DATE_END, :p_NEW_LOAN_ID);
                            END;", Connect.SchemaSalary), Connect.CurConnect);
                        _ocTransfer_Loan.UpdatedRowSource = UpdateRowSource.OutputParameters;
                        _ocTransfer_Loan.BindByName = true;
                        _ocTransfer_Loan.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal).Value = Loan_ID;
                        _ocTransfer_Loan.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal).Value = find_Emp.Transfer_ID;
                        _ocTransfer_Loan.Parameters.Add("p_LOAN_DATE_END", OracleDbType.Date).Value = f.Model.DateBegin;
                        _ocTransfer_Loan.Parameters.Add("p_NEW_LOAN_ID", OracleDbType.Decimal).Direction = ParameterDirection.Output;
                        OracleTransaction transact = Connect.CurConnect.BeginTransaction();
                        try
                        {
                            _ocTransfer_Loan.Transaction = transact;
                            _ocTransfer_Loan.ExecuteNonQuery();
                            transact.Commit();
                            MessageBox.Show("Изменения сохранены в БД.", "Ссуды", MessageBoxButton.OK, MessageBoxImage.Information);
                            UpdateCurrentLoan(Loan_ID);
                            UpdateCurrentLoan(_ocTransfer_Loan.Parameters["p_NEW_LOAN_ID"].Value);
                        }
                        catch (Exception ex)
                        {
                            transact.Rollback();
                            MessageBox.Show(ex.Message, "Ссуды: Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void Receipt_Cash_Order_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Cash_Order_Editor _cash_Order_Editor = new Cash_Order_Editor(1, this.Loan_ID);
            _cash_Order_Editor.Owner = Window.GetWindow(this);
            _cash_Order_Editor.ShowDialog();   
        }
        
        private void LoanMenuItem_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void PrintControl_Register_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByPeriod f = new FilterByPeriod();
            f.Owner = Window.GetWindow(this);
            f.Model.IsEndEnabled = false;
            if (f.ShowDialog() == true)
            {
                //OracleDataAdapter oda = new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/RepLoan_Contract.sql"), Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
                //oda.SelectCommand.BindByName = true;
                //oda.SelectCommand.Parameters.Add("p_BEGIN_PERIOD", OracleDbType.Date, f.Model.DateBegin, ParameterDirection.Input);
                //oda.SelectCommand.Parameters.Add("p_END_PERIOD", OracleDbType.Date, f.Model.DateEnd, ParameterDirection.Input);
                //Salary.Helpers.AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Договор займа...", oda, oda.SelectCommand,
                //    (p, pw) =>
                //    {
                //        DataSet dd = pw.Result as DataSet;
                //        Salary.Reports.ViewReportWindow.ShowReport(this, "Контрольная ведомость", "Rep_Loan/Loan_Contract.rdlc",
                //            //new Salary.Reports.SubReport[] { new Salary.Reports.SubReport("Subreport1", "Rep_Loan/Loan_Contract_Subreport.rdlc", dd.Tables[1]) },
                //            new DataTable[] { dd.Tables[0] },
                //            new Microsoft.Reporting.WinForms.ReportParameter[] { 
                //                new Microsoft.Reporting.WinForms.ReportParameter("P_MONTH", f.Model.DateBegin.Value.Month.ToString()),
                //                new Microsoft.Reporting.WinForms.ReportParameter("P_YEAR", f.Model.DateBegin.Value.Year.ToString())
                //                    });
                //    });
            }
        }

        private void PrintCirculating_Register_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByPeriod f = new FilterByPeriod();
            f.Owner = Window.GetWindow(this);
            f.Model.IsEndEnabled = false;
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/RepCirculating_Register.sql"), Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_DATE_REPORT", OracleDbType.Date, f.Model.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                Salary.Helpers.AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Оборотная ведомость по ссудам...", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        DataSet dd = pw.Result as DataSet;
                        Salary.Reports.ViewReportWindow.ShowReport(this, "Оборотная ведомость", "Rep_Loan/RepCirculating_Register.rdlc",
                            //new Salary.Reports.SubReport[] { new Salary.Reports.SubReport("Subreport1", "Rep_Loan/Loan_Contract_Subreport.rdlc", dd.Tables[1]) },
                            new DataTable[] { dd.Tables[0] },
                            new Microsoft.Reporting.WinForms.ReportParameter[] { 
                                new Microsoft.Reporting.WinForms.ReportParameter("P_MONTH", f.Model.DateBegin.Value.Month.ToString()),
                                new Microsoft.Reporting.WinForms.ReportParameter("P_YEAR", f.Model.DateBegin.Value.Year.ToString())
                                    });
                    });
            }
        }

        private void PrintIssued_Loan_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByPeriod f = new FilterByPeriod();
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/RepIssued_Loan.sql"), Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_PERIOD_BEGIN", OracleDbType.Date, f.Model.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_PERIOD_END", OracleDbType.Date, f.Model.DateEnd, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                Salary.Helpers.AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Выданные ссуды...", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        DataSet dd = pw.Result as DataSet;
                        Salary.Reports.ViewReportWindow.ShowReport(this, "Выданные ссуды", "Rep_Loan/RepIssued_Loan.rdlc",
                            new DataTable[] { dd.Tables[0] },
                            new Microsoft.Reporting.WinForms.ReportParameter[] { 
                                new Microsoft.Reporting.WinForms.ReportParameter("P_PERIOD_BEGIN", f.Model.DateBegin.Value.ToShortDateString()),
                                new Microsoft.Reporting.WinForms.ReportParameter("P_PERIOD_END", f.Model.DateEnd.Value.ToShortDateString())
                                    });
                    });
            }
        }

        private void PrintRepaid_Loan_Execute(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void PrintMaterial_Benefit_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByPeriod f = new FilterByPeriod();
            f.Owner = Window.GetWindow(this);
            f.Model.IsEndEnabled = false;
            if (f.ShowDialog() == true)
            {                
                OracleDataAdapter oda = new OracleDataAdapter(string.Format((@"begin
	                    {0}.LOAN_REPORTS.RepMaterial_Benefit(:p_DATE_REPORT, :c1);
                    end;"), Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_DATE_REPORT", OracleDbType.Date, f.Model.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                Salary.Helpers.AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Материальная выгода...", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        DataSet dd = pw.Result as DataSet;
                        Salary.Reports.ViewReportWindow.ShowReport(this, "Материальная выгода", "Rep_Loan/RepMaterial_Benefit.rdlc",
                            new DataTable[] { dd.Tables[0] },
                            new Microsoft.Reporting.WinForms.ReportParameter[] { 
                                new Microsoft.Reporting.WinForms.ReportParameter("P_MONTH", f.Model.DateBegin.Value.Month.ToString()),
                                new Microsoft.Reporting.WinForms.ReportParameter("P_YEAR", f.Model.DateBegin.Value.Year.ToString())
                                    });
                    });
            }
        }

        private void PrintMaterial_Benefit_Dismiss_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByPeriod f = new FilterByPeriod();
            f.Owner = Window.GetWindow(this);
            f.Model.IsEndEnabled = false;
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = new OracleDataAdapter(string.Format((@"begin
	                    {0}.LOAN_REPORTS.RepMaterial_Benefit_Dismiss(:p_DATE_REPORT, :c1);
                    end;"), Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_DATE_REPORT", OracleDbType.Date, f.Model.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                Salary.Helpers.AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Материальная выгода...", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        DataSet dd = pw.Result as DataSet;
                        Salary.Reports.ViewReportWindow.ShowReport(this, "Материальная выгода", "Rep_Loan/RepMaterial_Benefit.rdlc",
                            new DataTable[] { dd.Tables[0] },
                            new Microsoft.Reporting.WinForms.ReportParameter[] {
                                new Microsoft.Reporting.WinForms.ReportParameter("P_MONTH", f.Model.DateBegin.Value.Month.ToString()),
                                new Microsoft.Reporting.WinForms.ReportParameter("P_YEAR", f.Model.DateBegin.Value.Year.ToString())
                                    });
                    });
            }
        }

        private void Report_With_Choice_Requisites_Execute(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void RepRetention_By_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Salary.Reports.ViewReportWindow.ShowReport(this, "Удержания по ссуде", "Rep_Loan/RepRetention_By_Loan.rdlc",
                new DataTable[] { _dsRetention.Tables["SALARY"] },
                new Microsoft.Reporting.WinForms.ReportParameter[] { 
                    new Microsoft.Reporting.WinForms.ReportParameter("P_FIO", _currentRow["FIO"].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("P_CODE_SUBDIV", _currentRow["CODE_SUBDIV"].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("P_PER_NUM", _currentRow["PER_NUM"].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("P_ORDINAL_NUMBER", _currentRow["ORDINAL_NUMBER"].ToString())
                        });
        }

        private void RepLoan_Holder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataTable _dt = _ds.Tables["LOAN"].DefaultView.Table.Select("LOAN_REMAINDER <> 0 and RETENTION_BY_FACT = 0").ToArray().OfType<DataRow>().Select(r => 
                new
                {
                    CONTRACT_NUMBER = r.Field2<string>("CONTRACT_NUMBER"),
                    CODE_SUBDIV = r.Field2<string>("CODE_SUBDIV"),
                    PER_NUM = r.Field2<string>("PER_NUM"),
                    EMP_LAST_NAME = r.Field2<string>("EMP_LAST_NAME"),
                    EMP_FIRST_NAME = r.Field2<string>("EMP_FIRST_NAME"),
                    EMP_MIDDLE_NAME = r.Field2<string>("EMP_MIDDLE_NAME"),
                    LOAN_DATE = r.Field2<DateTime>("LOAN_DATE"),
                    LOAN_TERM = r.Field2<decimal>("LOAN_TERM"),
                    LOAN_SUM = r.Field2<decimal>("LOAN_SUM"),
                    RETENTION_BY_CONTRACT = r.Field2<decimal>("RETENTION_BY_CONTRACT"),
                    RETENTION_BY_FACT = r.Field2<decimal>("RETENTION_BY_FACT"),
                    LOAN_REMAINDER = r.Field2<decimal>("LOAN_REMAINDER"),
                    PURPOSE_LOAN_NOTE = r.Field2<string>("PURPOSE_LOAN_NOTE")
                }).CopyToDataTable();
            Salary.Reports.ViewReportWindow.ShowReport(this, "Должники по ссуде", "Rep_Loan/RepLoan_Holder.rdlc",
                new DataTable[] { _dt },
                new Microsoft.Reporting.WinForms.ReportParameter[] { 
                    new Microsoft.Reporting.WinForms.ReportParameter("P_DATE", DateTime.Today.ToShortDateString())
                        });
        }

        private void DumpMaterial_Benefit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByPeriod f = new FilterByPeriod();
            f.Owner = Window.GetWindow(this);
            f.Model.IsEndEnabled = false;
            f.Title = "Выбор периода";
            if (f.ShowDialog() == true)
            {
                OracleCommand _ocDumpMaterial_Benefit = new OracleCommand(string.Format(
                    @"BEGIN
                        {0}.LOAN_REPORTS.Material_Benefit_To_Salary(:p_DATE_REPORT);
                    END;", Connect.SchemaSalary), Connect.CurConnect);
                _ocDumpMaterial_Benefit.BindByName = true;
                _ocDumpMaterial_Benefit.Parameters.Add("p_DATE_REPORT", OracleDbType.Date).Value = f.Model.DateBegin;
                OracleTransaction transact = Connect.CurConnect.BeginTransaction();
                try
                {
                    _ocDumpMaterial_Benefit.Transaction = transact;
                    _ocDumpMaterial_Benefit.ExecuteNonQuery();
                    transact.Commit();
                    MessageBox.Show("Данные для удержания сформированы.", "Ссуды", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    transact.Rollback();
                    MessageBox.Show(ex.Message, "Ссуды", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Approve_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите установить признак подписания документов по ссуде?",
                "Ссуды", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OracleCommand _ocApprove_Loan = new OracleCommand(string.Format(
                    @"BEGIN
                        {0}.LOAN_DML_PKG.APPROVE_LOAN(:p_LOAN_ID);
                    END;", Connect.SchemaSalary), Connect.CurConnect);
                _ocApprove_Loan.UpdatedRowSource = UpdateRowSource.OutputParameters;
                _ocApprove_Loan.BindByName = true;
                _ocApprove_Loan.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal).Value = Loan_ID;
                OracleTransaction transact = Connect.CurConnect.BeginTransaction();
                try
                {
                    _ocApprove_Loan.Transaction = transact;
                    _ocApprove_Loan.ExecuteNonQuery();
                    transact.Commit();
                    MessageBox.Show("Изменения сохранены в БД.", "Ссуды", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateCurrentLoan(Loan_ID);
                }
                catch (Exception ex)
                {
                    transact.Rollback();
                    MessageBox.Show(ex.Message, "Ссуды: Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btRefreshState_Click(object sender, RoutedEventArgs e)
        {
            GetLoans();
        }
    }

    public class LoanRowColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != DependencyProperty.UnsetValue && values[0] != DBNull.Value)
                return new SolidColorBrush(Colors.WhiteSmoke);
            if (values[1] != DependencyProperty.UnsetValue && values[1] != DBNull.Value)
                return new SolidColorBrush(Color.FromRgb(254,226,169));
            return new SolidColorBrush(Colors.White);
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public class LoanForegroundColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != DependencyProperty.UnsetValue && values[1] != DependencyProperty.UnsetValue)
                if (System.Convert.ToDecimal(values[0]) == 0 && System.Convert.ToDecimal(values[1]) != 0)
                    return new SolidColorBrush(Colors.Red);
            return new SolidColorBrush(Colors.Black);
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public class LoanRowVisibleDetailsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "")
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
    
    public class DocRetGroupSumConv : IValueConverter
    {
        public string SignField
        {
            get;
            set;
        }
        public static decimal GetSumCollection(IEnumerable<object> collection, string SignField1)
        {
            decimal k = 0;
            foreach (object t in collection)
            {
                if (t is CollectionViewGroup)
                    k += GetSumCollection((t as CollectionViewGroup).Items, SignField1);
                else
                    if (t is DataRowView && (t as DataRowView).Row.RowState != DataRowState.Deleted && (t as DataRowView).Row.RowState != DataRowState.Detached && (t as DataRowView)["SUM_SAL"] != DBNull.Value)
                    {
                        DataRowView p = t as DataRowView;
                        k +=
                            (decimal)(t as DataRowView)["SUM_SAL"];
                    }
            }
            return k;
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return GetSumCollection((IEnumerable<object>)value, SignField);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
