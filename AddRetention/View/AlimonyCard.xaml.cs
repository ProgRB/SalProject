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
using System.Data;
using Oracle.DataAccess.Client;
using EntityGenerator;
using System.ComponentModel;
using System.Data.OleDb;
using Salary.Helpers;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for AlimonyCard.xaml
    /// </summary>
    public partial class AlimonyCard : Window, INotifyPropertyChanged
    {
        
        private DataSet ds;
        OracleDataAdapter odaEmpData;
        private EmpAllData emp_data;
        DataView dvSalaryAlimony, dvCashAlimony;
        public AlimonyCard(object transfer_id, DateTime selectedDate)
        {
            ds = new DataSet();
            odaEmpData = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectEmpAlimonyData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaEmpData.SelectCommand.BindByName = true;
            odaEmpData.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, transfer_id, ParameterDirection.Input);
            odaEmpData.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaEmpData.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            //odaEmpData.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            //odaEmpData.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaEmpData.TableMappings.Add("Table", "EmpData");
            odaEmpData.TableMappings.Add("Table1", "ALIMONY_SALARY_DATA");
            //odaEmpData.TableMappings.Add("Table2", "CASHBOX_DATA");
            LoadEmpData(transfer_id);
            InitializeComponent();
            this.PropertyChanged += new PropertyChangedEventHandler(AlimonyCard_PropertyChanged);
        }

        void AlimonyCard_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "YearFilter")
            {
                if (dvSalaryAlimony != null)
                    dvSalaryAlimony.RowFilter = string.Format("pay_date >= #1/1/{0}# and pay_date<=#12/31/{0}#", YearFilter);
                if (dvCashAlimony != null)
                    dvCashAlimony.RowFilter = string.Format("syst_data >= #1/1/{0}# and syst_data<=#12/31/{0}#", YearFilter);
            }
        }

        public EmpAllData CurrentEmpData
        {
            get
            {
                return emp_data;
            }
            set
            {
                emp_data = value;
                OnPropertyChanged("CurrentEmpData");
            }
        }

        int _yearFilter = DateTime.Now.Year;
        public int YearFilter
        {
            get
            { return _yearFilter; }
            set
            {
                _yearFilter = value;
                OnPropertyChanged("YearFilter");
            }
        }

        public DataView SalaryAlimony
        {
            get
            {
                return dvSalaryAlimony;
            }
            set
            {
                dvSalaryAlimony = value;
                OnPropertyChanged("SalaryAlimony");
            }
        }

        public DataView CashAlimony
        {
            get
            {
                return dvCashAlimony;
            }
            set
            {
                dvCashAlimony = value;
                OnPropertyChanged("CashAlimony");
            }
        }
        

        private void LoadEmpData(object transfer_id)
        {
            odaEmpData.SelectCommand.Parameters["p_transfer_id"].Value = transfer_id;
            try
            {
                odaEmpData.Fill(ds);
                if (ds.Tables["EmpData"].Rows.Count > 0)
                    CurrentEmpData = ds.Tables["EmpData"].Rows[0].ConvertToRowEntity<EmpAllData>();
                else
                    CurrentEmpData = null;
                if (dvSalaryAlimony==null)
                    SalaryAlimony = new DataView(ds.Tables["ALIMONY_SALARY_DATA"], string.Format("pay_date >= #1/1/{0}# and pay_date<=#12/31/{0}#", YearFilter), "PAY_DATE, CODE_PAYMENT", DataViewRowState.CurrentRows);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка загрузки данных");
            }                
        }

        private void LoadEmpCash()
        {
            if (CheckVfpOleDb.IsInstalled())
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += (o, ea) =>
                    {
                        OleDbConnection fpConnecton = new OleDbConnection(string.Format("Provider=VFPOLEDB.1;Data Source={0};Mode=Read;", Connect.parameters["KassaFile"]));
                        try
                        {
                            fpConnecton.Open();
                            OleDbDataAdapter a = new OleDbDataAdapter(string.Format(Queries.GetQuery("SelectKassaData.sql")), fpConnecton);
                            a.SelectCommand.Parameters.Add(new OleDbParameter("p_per_num", CurrentEmpData.PerNum));
                            a.SelectCommand.Parameters.Add(new OleDbParameter("p_sign_comb", CurrentEmpData.SignComb == 1 ? "2" : ""));
                            a.TableMappings.Add("Table", "CASHBOX_DATA");
                            a.Fill(ds);
                            fpConnecton.Close();
                            if (CashAlimony == null)
                            {
                                CashAlimony = new DataView(ds.Tables["CASHBOX_DATA"], string.Format("SYST_DATA >= #1/1/{0}# and SYST_DATA<=#12/31/{0}#", YearFilter), "SYST_DATA", DataViewRowState.CurrentRows);
                            }
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
                bw.RunWorkerCompleted += (o, eo) => { xcbusyIndicator.IsBusy = false;};
                xcbusyIndicator.IsBusy = true;
                bw.RunWorkerAsync();
                tcontrolMain.SelectionChanged -= TabControl_SelectionChanged;
            }
            else
                MessageBox.Show("Не установлен драйвер FoxPro. Обратитесь к системным администраторам для установки", "Ошибка чтения таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is TabItem && (e.AddedItems[0] as TabItem).Name == "tabItemCashRecord" && CashAlimony==null)
                LoadEmpCash();
        }

        private void btRefreshAlimonyCash_Click(object sender, RoutedEventArgs e)
        {
            LoadEmpCash();
        }
    }
}
