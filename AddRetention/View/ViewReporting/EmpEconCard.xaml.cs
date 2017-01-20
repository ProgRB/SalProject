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
using System.Data.EntityClient;
using Oracle.DataAccess.Client;
using System.Data;
using System.ComponentModel;
using Oracle.DataAccess.Types;
using System.IO;
using LibrarySalary.Helpers;
using EntityGenerator;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for EmpAccountCard.xaml
    /// </summary>
    public partial class EmpEconCard : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Класс формы просмотра бух. данных сотрудника
        /// </summary>
        /// 
        private DataSet ds;
        OracleDataAdapter odaEmpData;
        private EmpAllData emp_data;
        private AccountData account_data;        
        
        public EmpEconCard(object transfer_id)
        {
            ds = new DataSet();
            odaEmpData = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectEmpEconCardData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaEmpData.SelectCommand.BindByName = true;
            odaEmpData.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, transfer_id, ParameterDirection.Input);
            odaEmpData.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaEmpData.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaEmpData.TableMappings.Add("Table", "EmpData");
            odaEmpData.TableMappings.Add("Table1", "ACCOUNT_DATA");
            LoadEmpData(transfer_id);
            InitializeComponent();
            cbTariffGrid.ItemsSource = new DataView(AppDataSet.Tables["TARIFF_GRID"], "", "CODE_TARIFF_GRID", DataViewRowState.CurrentRows);
            try
            {
                OracleCommand cmd = new OracleCommand(@"begin APSTAFF.TABLE_AUDIT_EX_INSERT(:p_TABLE_ID, :p_TYPE_KARD);end;", Connect.CurConnect);
                cmd.Parameters.Add("p_TABLE_ID", OracleDbType.Decimal, transfer_id, ParameterDirection.Input);
                cmd.Parameters.Add("p_TYPE_KARD", OracleDbType.Varchar2, "SALEC", ParameterDirection.Input);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { 
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

        public AccountData CurrentAccountData
        {
            get { return account_data; }
            set { account_data = value; OnPropertyChanged("CurrentAccountData"); }
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
                if (ds.Tables["ACCOUNT_DATA"].Rows.Count > 0)
                    CurrentAccountData = ds.Tables["ACCOUNT_DATA"].Rows[0].ConvertToRowEntity<AccountData>();
                else
                    CurrentAccountData = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка загрузки данных");
            }                
        }
        
        public event PropertyChangedEventHandler  PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged!=null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

}
