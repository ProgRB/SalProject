using Oracle.DataAccess.Client;
using Salary.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

namespace Salary.Loan
{
    /// <summary>
    /// Interaction logic for Cash_Order_Editor.xaml
    /// </summary>
    public partial class Cash_Order_Editor : Window
    {
        int _type_order;
        public bool Sign_Cash_Order
        {
            get { return _type_order == 1 ; }
        }
        LoanModel _model;
        decimal _sum_order;
        public decimal Sum_Order
        {
            get { return _sum_order; }
            set { _sum_order = value; }
        }
        public Cash_Order_Editor(int type_order, decimal? p_loan_id)
        {            
            _model = new LoanModel(p_loan_id);
            if (type_order == 2)
            {
                _sum_order = (decimal)Model.LoanSum;
            }
            _type_order = type_order;
            InitializeComponent();
            this.DataContext = Model;
        }

        /// <summary>
        /// Данные источника по текущей строке
        /// </summary>
        public LoanModel Model
        {
            get
            {
                return _model;
            }
        }

        private void btView_Order_Click(object sender, RoutedEventArgs e)
        {
            Salary.View.SignesRecord[] sr = null;
            if (Salary.View.Signes.Show(0, "Cash_Order", "Выберите подписанта ордера", (_type_order == 2 ? 2 : 1), ref sr) == true)
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format(
                            "begin {0}.LOAN_REPORTS.GetCash_Order(:p_TYPE_ORDER, :p_LOAN_ID, :p_SUM_CASH, :p_WRITE_INTO_SALARY, :c1);end;", Connect.SchemaSalary), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_TYPE_ORDER", OracleDbType.Decimal, _type_order, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal, Model.LoanID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_SUM_CASH", OracleDbType.Decimal, this.Sum_Order, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_WRITE_INTO_SALARY", OracleDbType.Decimal, 0, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                DataSet _dsReport = new DataSet();
                a.TableMappings.Add("Table", "c1");
                a.Fill(_dsReport);
                if (_type_order == 2)
                {
                    Salary.Reports.ViewReportWindow.ShowReport(this, "Кассовый ордер", "Rep_Loan/Account_Cash_Order.rdlc",
                        new DataTable[] { _dsReport.Tables["c1"] },
                        new Microsoft.Reporting.WinForms.ReportParameter[] { 
                            new Microsoft.Reporting.WinForms.ReportParameter("P_FIO_DIR", sr[0].EmpName),
                            new Microsoft.Reporting.WinForms.ReportParameter("P_FIO_ACCOUNT", sr[1].EmpName)
                        });
                }
                else
                {
                    Salary.Reports.ViewReportWindow.ShowReport(this, "Кассовый ордер", "Rep_Loan/Cash_Order.rdlc",
                        new DataTable[] { _dsReport.Tables["c1"] },
                        new Microsoft.Reporting.WinForms.ReportParameter[] { 
                            new Microsoft.Reporting.WinForms.ReportParameter("P_FIO_ACCOUNT", sr[0].EmpName)
                        });
                }
            }
        }

        private void btUnload_Into_Kassa_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Сформировать запись в кассу?", "Ссуды", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                try
                {
                    OracleDataAdapter a = new OracleDataAdapter(string.Format(
                            "begin {0}.LOAN_REPORTS.GetCash_Order(:p_TYPE_ORDER, :p_LOAN_ID, :p_SUM_CASH, :p_WRITE_INTO_SALARY, :c1);end;", Connect.SchemaSalary), Connect.CurConnect);
                    a.SelectCommand.BindByName = true;
                    a.SelectCommand.Parameters.Add("p_TYPE_ORDER", OracleDbType.Decimal, _type_order, ParameterDirection.Input);
                    a.SelectCommand.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal, Model.LoanID, ParameterDirection.Input);
                    a.SelectCommand.Parameters.Add("p_SUM_CASH", OracleDbType.Decimal, this.Sum_Order, ParameterDirection.Input);
                    a.SelectCommand.Parameters.Add("p_WRITE_INTO_SALARY", OracleDbType.Decimal, 1, ParameterDirection.Input);
                    a.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                    DataSet _dsReport = new DataSet();
                    a.TableMappings.Add("Table", "c1");
                    a.Fill(_dsReport);
                    if (_type_order == 2)
                    {
                        // Удаляем строчку, чтобы в кассе убрать предыдущий ордер по данной ссуде
                        //_dsReport.Tables["c1"].Rows[0].Delete();
                        //a.Fill(_dsReport);
                        decimal k = 0;
                        OleDbConnection fpConnection = new OleDbConnection(string.Format("Provider=VFPOLEDB.1;Data Source={0};Mode=Read;", Connect.parameters["KassaFile"]));
                        fpConnection.Open();
                        OleDbCommand cm = new OleDbCommand("select max(val(kas_dok)) from kassa", fpConnection);
                        k = (decimal)cm.ExecuteScalar();
                        fpConnection.Close();
                        _dsReport.Tables["c1"].Rows[0]["KAS_DOK"] = ((int)(decimal.Parse(_dsReport.Tables["c1"].Rows[0]["KAS_DOK"].ToString()) + k)).ToString();
                        _dsReport.Tables["c1"].Rows[0].AcceptChanges();
                        _dsReport.Tables["c1"].Rows[0].SetAdded();
                        AlimonyWriter.WriteData(_dsReport.Tables["c1"], "kas_dok, type, skpolz, nazv, summa, bsht, syst_data, ob_v, p_opl, skst, p_sv, p_dep, pr_sign, osn_ko, tnom, pnsud, podr, dat_kom, dat_kor, dat_kas, dat_opl, dat_sign, nom_kas_d, pndok, sum_opl, sum_ost, fl_pkurs, kurs"
                                , "", "", ""
                                , "skpolz='SSUDA' and type = '2' and bsht = '73400000' and tnom=? and pnsud=?"
                                , "tnom,pnsud");
                    }
                    else
                    {
                        decimal k = 0;
                        OleDbConnection fpConnection = new OleDbConnection(string.Format("Provider=VFPOLEDB.1;Data Source={0};Mode=Read;", Connect.parameters["KassaFile"]));
                        fpConnection.Open();
                        OleDbCommand cm = new OleDbCommand("select max(val(kas_dok)) from kassa", fpConnection);
                        k = (decimal)cm.ExecuteScalar();
                        fpConnection.Close();
                        _dsReport.Tables["c1"].Rows[0]["KAS_DOK"] = ((int)(decimal.Parse(_dsReport.Tables["c1"].Rows[0]["KAS_DOK"].ToString()) + k)).ToString();
                        _dsReport.Tables["c1"].Rows[0].AcceptChanges();
                        _dsReport.Tables["c1"].Rows[0].SetAdded();
                        AlimonyWriter.WriteData(_dsReport.Tables["c1"], "kas_dok, type, skpolz, nazv, summa, bsht, syst_data, ob_v, p_opl, skst, p_sv, p_dep, pr_sign, osn_ko, tnom, pnsud, podr, dat_kom, dat_kor, dat_kas, dat_opl, dat_sign, nom_kas_d, pndok, sum_opl, sum_ost, fl_pkurs, kurs"
                                , "", "", ""
                                , ""
                                , "");
                    }
                    MessageBox.Show("Данные ордера сформированы для кассы.", "Ссуды");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка формирования записи в кассу");
                }
            }
        }
    }
}
