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
using Salary.Helpers;
using EntityGenerator;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for EmpAccountCard.xaml
    /// </summary>
    public partial class EmpAccountCard : Window
    {
        /// <summary>
        /// Класс формы просмотра бух. данных сотрудника
        /// </summary>
        /// 

        EmpAccountCardViewModel _model;
        
        public EmpAccountCard(object transfer_id)
        {
            _model = new EmpAccountCardViewModel((decimal)transfer_id);
            InitializeComponent();
            cbTariffGrid.ItemsSource = new DataView(AppDataSet.Tables["TARIFF_GRID"], "", "CODE_TARIFF_GRID", DataViewRowState.CurrentRows);
            this.DataContext = Model;
        }

        public EmpAccountCardViewModel Model
        {
            get
            {
                return _model;
            }
        }


    }

    public class EmpAccountCardViewModel:NotificationObject
    {
        private DataSet ds;
        OracleDataAdapter odaEmpData;
        private EmpAllData emp_data;
        private AccountData account_data;
        PerData per_data;
        public EmpAccountCardViewModel(decimal transfer_id)
        {
            ds = new DataSet();
            odaEmpData = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectEmpAccountCardData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaEmpData.SelectCommand.BindByName = true;
            odaEmpData.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, transfer_id, ParameterDirection.Input);
            odaEmpData.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaEmpData.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaEmpData.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaEmpData.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            odaEmpData.SelectCommand.Parameters.Add("c5", OracleDbType.RefCursor, ParameterDirection.Output);
            odaEmpData.SelectCommand.Parameters.Add("c6", OracleDbType.RefCursor, ParameterDirection.Output);
            //odaEmpData.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaEmpData.TableMappings.Add("Table", "EmpData");
            odaEmpData.TableMappings.Add("Table1", "ACCOUNT_DATA");
            odaEmpData.TableMappings.Add("Table2", "PER_DATA");
            odaEmpData.TableMappings.Add("Table3", "DEPENDENTS");
            odaEmpData.TableMappings.Add("Table4", "Address");
            odaEmpData.TableMappings.Add("Table5", "PREV_TRANSFERS");
            LoadEmpData(transfer_id);
            try
            {
                OracleCommand cmd = new OracleCommand(@"begin APSTAFF.TABLE_AUDIT_EX_INSERT(:p_TABLE_ID, :p_TYPE_KARD);end;", Connect.CurConnect);
                cmd.Parameters.Add("p_TABLE_ID", OracleDbType.Decimal, transfer_id, ParameterDirection.Input);
                cmd.Parameters.Add("p_TYPE_KARD", OracleDbType.Varchar2, "SALAC", ParameterDirection.Input);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Текущий данные по сотруднику
        /// </summary>
        public EmpAllData CurrentEmpData
        {
            get
            {
                return emp_data;
            }
            set
            {
                emp_data = value;
                RaisePropertyChanged(() => CurrentEmpData);
            }
        }

        /// <summary>
        /// Бух данные по сотруднику
        /// </summary>
        public AccountData CurrentAccountData
        {
            get { return account_data; }
            set { account_data = value; 
                RaisePropertyChanged(() => CurrentAccountData); }
        }
        public PerData CurrentPerData
        {
            get { return per_data; }
            set { per_data = value; RaisePropertyChanged(()=>CurrentPerData); }
        }

        DataTable dependentTable;

        /// <summary>
        /// Представление иждивенцев и вычетов
        /// </summary>
        public DataView EmpDependents
        {
            get
            {
                if (dependentTable == null)
                {
                    FormTaxDiscountData();
                }
                if (dependentTable != null)
                    return dependentTable.DefaultView;
                return null;
            }
        }


        /// <summary>
        /// Загрузка данных по сотрудникам
        /// </summary>
        /// <param name="transfer_id"></param>
        private void LoadEmpData(object transfer_id)
        {
            odaEmpData.SelectCommand.Parameters["p_transfer_id"].Value = transfer_id;
            try
            {
                odaEmpData.Fill(ds);
                if (ds.Tables["EmpData"].Rows.Count > 0)
                    CurrentEmpData = new EmpAllData() { DataRow = ds.Tables["EmpData"].Rows[0] };
                else
                    CurrentEmpData = null;
                if (ds.Tables["ACCOUNT_DATA"].Rows.Count > 0)
                    CurrentAccountData = ds.Tables["ACCOUNT_DATA"].Select("TRANSFER_ID="+CurrentEmpData.TransferID)
                        .Select(r=>r.ConvertToRowEntity<AccountData>()).FirstOrDefault();
                else
                    CurrentAccountData = null;
                if (ds.Tables["PER_DATA"].Rows.Count > 0)
                    CurrentPerData = ds.Tables["PER_DATA"].Rows[0].ConvertToRowEntity<PerData>();
                else
                    CurrentPerData = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка загрузки данных");
            }
        }

        /// <summary>
        /// Формирование данных по вычетам в виде таблицы
        /// </summary>
        private void FormTaxDiscountData()
        {
            if (ds.Tables.Contains("DEPENDENTS"))
                dependentTable = ds.Tables["DEPENDENTS"].Rows.OfType<DataRow>().Where(r => r.Field2<DateTime>("PAY_DATE").Year == SelectedYear)
                    .OrderBy(r => r.Field2<DateTime?>("PAY_DATE")).ToPivotTable(
                    r => r["CODE_DISC"],
                    r => r.Field2<DateTime?>("PAY_DATE").Value.ToString("MM"),
                    r => r.Count(), "Месяц");
        }

        int _selectedYear = DateTime.Today.Year;

        /// <summary>
        /// Выбранный год в фильтре
        /// </summary>
        public int SelectedYear
        {
            get
            {
                return _selectedYear;
            }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    FormTaxDiscountData();
                    RaisePropertyChanged(()=>EmpDependents);
                    RaisePropertyChanged(()=>SelectedYear);
                }
            }
        }

        /// <summary>
        /// Эмуляция для выбора года) через 100 лет я уже точно не будут здесь работать
        /// </summary>
        public List<int> YearsItems
        {
            get
            {
                List<int> l = new List<int>();
                for (int i = 2000; i < 2100; ++i)
                    l.Add(i);
                return l;
            }
        }

        List<EmpAddress> _empAddress;
        /// <summary>
        /// Список адресов сотрудника
        /// </summary>
        public List<EmpAddress> EmpAddresses
        {
            get
            {
                if (ds != null && ds.Tables.Contains("Address") && _empAddress == null)
                    _empAddress = ds.Tables["Address"].Rows.OfType<DataRow>().Select(r => new EmpAddress(r)).ToList();
                return _empAddress;
            }
        }

        /// <summary>
        /// Адрес прописки
        /// </summary>
        public EmpAddress HomeAddress
        {
            get
            {
                if (EmpAddresses == null)
                    return null;
                return EmpAddresses.Where(r => r.TypeAddress == AddressType.HomeAddress).FirstOrDefault();
            }
        }

        /// <summary>
        /// Адрес не в КЛАДР
        /// </summary>
        public EmpAddress AddressNoneKladr
        {
            get
            {
                if (EmpAddresses == null)
                    return null;
                return EmpAddresses.Where(r => r.TypeAddress == AddressType.NoneKladr).FirstOrDefault();
            }
        }

        List<Transfer> _prevTransfers;
        private Transfer _selectedPrevTransfer;
        /// <summary>
        /// Источник данных - переводы история
        /// </summary>
        public IEnumerable<Transfer> PrevTrasferSource
        { 
            get
            {
                if (_prevTransfers == null && ds.Tables.Contains("PREV_TRANSFERS"))
                {
                    _prevTransfers = ds.Tables["PREV_TRANSFERS"].ConvertToEntityList<Transfer>();
                    _selectedPrevTransfer = _prevTransfers.FirstOrDefault();
                }
                if (_prevTransfers!=null)
                    return _prevTransfers.OrderBy(r => r.DateTransfer);
                return _prevTransfers;
            }
        }

        /// <summary>
        /// Выбранный предыдущий перевод
        /// </summary>
        public Transfer SelectedPrevTransfer
        {
            get
            {
                return _selectedPrevTransfer;
            }
            set
            {
                _selectedPrevTransfer = value;
                RaisePropertyChanged(() => SelectedPrevTransfer);
            }
        }
    }
}
