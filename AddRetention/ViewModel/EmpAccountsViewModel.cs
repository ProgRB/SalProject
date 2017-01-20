using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Salary.Helpers;
using System.Windows.Data;
using System.Data;
using System.ComponentModel;
using Oracle.DataAccess.Client;
using System.Threading;
using System.Runtime.CompilerServices;
using LibrarySalary.Helpers;

namespace Salary.ViewModel
{
    public class EmpAccountsViewModel: NotificationObject
    {
        public EmpAccountsViewModel()
        {
        }

        public AsyncCollectionList<DataRowView> EmpView
        {
            get
            {
                if (_empView == null)
                {
                    _empView = new AsyncCollectionList<DataRowView>(EmpProvider);
                    /*_empView.CollectionView.Filter=(object r)=>
                        {
                            if (r != null && !string.IsNullOrEmpty(EmpProvider.Family))
                                return (r as DataRowView)["FIO"].ToString().Contains(EmpProvider.Family.ToUpper());
                            return true;
                        };*/
                    UpdateEmpList();
                    _empView.CurrentItemChanged += new EventHandler(_empView_CurrentItemChanged);
                }
                return  _empView;
            }
        }

        void _empView_CurrentItemChanged(object sender, EventArgs e)
        {
            UpdateClientAccount();
            UpdateEmpRetent();
        }
        
        public AsyncCollectionList<DataRowView> ClientAccountView
        {
            get
            {
                if (_clientAccountView == null)
                    _clientAccountView = new AsyncCollectionList<DataRowView>(ClientAccountProvider, false);
                return _clientAccountView;
            }
        }

        public AsyncCollectionList<DataRowView> EmpRetent
        {
            get
            {
                if (_empRetent == null)
                {
                    _empRetent = new AsyncCollectionList<DataRowView>(RetentProvider, false);
                }
                return _empRetent;
            }
        }


        public void UpdateClientAccount()
        {
            ClientAccountProvider.TransferID = EmpView.SelectedItem!=null?EmpView.SelectedItem.Row.Field2<Decimal?>("TRANSFER_ID"):-1;
            ClientAccountView.LoadDataAsync();
        }
        public void UpdateEmpList()
        {
            EmpView.LoadDataAsync();
        }
        public void UpdateEmpRetent()
        {
            RetentProvider.WorkerID = EmpView.SelectedItem!=null? EmpView.SelectedItem.Row.Field2<Decimal?>("WORKER_ID"):-1;
            EmpRetent.LoadDataAsync();
        }

        bool _archivRetent = false;
        public bool ArchivRetent
        {
            get
            {
                return _archivRetent;
            }
            set
            {
                _archivRetent = value;
                RaisePropertyChanged(() => ArchivRetent);
            }
        }

        public EmpItemsProvider EmpProvider
        {
            get
            {
                if (_empProvider == null)
                    _empProvider = new EmpItemsProvider();
                return _empProvider;
            }
        }
        public ClientAccountProvider ClientAccountProvider
        {
            get
            {
                if (_accountProvider == null)
                    _accountProvider = new ClientAccountProvider();
                return _accountProvider;
            }
        }
        public EmpRetentProvider RetentProvider
        {
            get
            {
                if (_retentProvider == null)
                    _retentProvider = new EmpRetentProvider();
                return _retentProvider;
            }
        }
        AsyncCollectionList<DataRowView> _empView, _clientAccountView, _empRetent;
        EmpItemsProvider _empProvider;
        EmpRetentProvider _retentProvider;
        ClientAccountProvider _accountProvider;
        Exception _ex;
        public Exception LoadException
        {
            get
            {
                return _ex;
            }
            set
            {
                _ex = value;
                RaisePropertyChanged(() => LoadException);
            }
        }
    }

    /// <summary>
    /// Провайдер получения данных - счета связанные с сотрудником
    /// </summary>
    public class ClientAccountProvider: NotificationObject, IItemsProvider<DataRowView>
    {
        OracleDataAdapter odaClientAccount;

        decimal? transferID;
        public decimal? TransferID
        {
            get
            {
                return transferID;
            }
            set
            {
                transferID = value;
                RaisePropertyChanged(() => TransferID);
            }
        }

        SynchronizationContext _sync;
        public ClientAccountProvider()
        {
            _sync = SynchronizationContext.Current;
        }

        public void CancelFetch()
        {
            if (odaClientAccount != null && odaClientAccount.SelectCommand != null)
            {
                try
                {
                    odaClientAccount.SelectCommand.Cancel();
                }
                catch
                { }
            }
        }

        public SynchronizationContext GetSyncContext()
        {
            return _sync;
        }

        public IList<DataRowView> FetchData()
        {
            string ex_message;
            odaClientAccount = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectEmpAccounts.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaClientAccount.SelectCommand.BindByName = true;
            odaClientAccount.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, TransferID, ParameterDirection.Input);
            DataTable t = new DataTable();
            if (odaClientAccount != null)
                try
                {
                    odaClientAccount.Fill(t);
                }
                catch (RuntimeWrappedException ex)
                {
                    ex_message = ex.Message;
                }
                catch (Exception ex1)
                {
                    ex_message = ex1.Message;
                }
            if (odaClientAccount!=null)
                odaClientAccount.Dispose();
            return t.DefaultView.OfType<DataRowView>().ToList();
        }
    }

    /// <summary>
    /// Провайдер получения данных - список сотрудников работающих в подразделении или перечисляющих что либо
    /// </summary>
    public class EmpItemsProvider: NotificationObject, IItemsProvider<DataRowView>
    {
        OracleDataAdapter odaEmps;

        Decimal? _subdivID=0;
        public Decimal? SubdivID
        {
            get
            {
                return _subdivID;
            }
            set
            {
                _subdivID =value;
                RaisePropertyChanged(()=>SubdivID);
            }
        }

        public string CodeSubdiv
        {
            get
            {
                return AppDataSet.Tables["SUBDIV"].Rows.OfType<DataRow>().Where(r => r.Field2<Decimal?>("SUBDIV_ID") == SubdivID).Select(r => r.Field<string>("CODE_SUBDIV")).FirstOrDefault();
            }
        }

        DateTime _selectedDate = DateTime.Now.Date;
        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged(() => SelectedDate);
            }
        }

        string per_num;
        public string PerNum
        {
            get
            {
                return per_num;
            }
            set
            {
                per_num = value;
                RaisePropertyChanged(() => PerNum);
            }
        }

        string _family;
        public string Family
        {
            get
            {
                return _family;
            }
            set
            {
                _family = value;
                RaisePropertyChanged(() => Family);
            }
        }

        SynchronizationContext sync;
        string query;
        public EmpItemsProvider()
        {
            query = string.Format(Queries.GetQuery("SelectEmpForAccounts.sql"), Connect.SchemaApstaff, Connect.SchemaSalary);
            sync = SynchronizationContext.Current;
        }

        /// <summary>
        /// Загрузка данных сотрудников - кто сейчас работает а так же кто числится что может делать какие-то перечисления
        /// </summary>
        /// <returns></returns>
        public IList<DataRowView> FetchData()
        {
            string ex_message;
            odaEmps = new OracleDataAdapter(query, Connect.CurConnect);
            odaEmps.SelectCommand.BindByName = true;
            odaEmps.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, SubdivID, ParameterDirection.Input);
            odaEmps.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, SelectedDate, ParameterDirection.Input);
            odaEmps.SelectCommand.Parameters.Add("p_per_num", OracleDbType.Varchar2, PerNum, ParameterDirection.Input);
            odaEmps.SelectCommand.Parameters.Add("p_fio", OracleDbType.Varchar2, Family, ParameterDirection.Input);
            DataTable t = new DataTable();
            if (odaEmps != null)
                try
                {
                    odaEmps.Fill(t);
                }
                catch (RuntimeWrappedException ex)
                {
                    ex_message = ex.Message;
                }
                catch (Exception ex1)
                {
                    ex_message = ex1.Message;
                }
            if (odaEmps != null)
                odaEmps.Dispose();
            return t.DefaultView.OfType<DataRowView>().ToList();
        }

        public void CancelFetch()
        {
            if (odaEmps != null && odaEmps.SelectCommand != null)
            {
                try
                {
                    odaEmps.SelectCommand.Cancel();
                }
                catch
                { }
            }
        }

        public SynchronizationContext GetSyncContext()
        {
            return sync;
        }
    }

    /// <summary>
    /// провайдер получения данных по удержаниям для перечисления
    /// </summary>
    public class EmpRetentProvider : NotificationObject, IItemsProvider<DataRowView>
    {
        OracleDataAdapter odaRetent;

        Decimal? _workerID;
        public Decimal? WorkerID
        {
            get
            {
                return _workerID;
            }
            set
            {
                _workerID =value;
                RaisePropertyChanged(()=>WorkerID);
            }
        }

        SynchronizationContext sync;
        string query;
        public EmpRetentProvider()
        {
            query = string.Format(Queries.GetQuery("SelectEmpClientRetent.sql"), Connect.SchemaApstaff, Connect.SchemaSalary);
            sync = SynchronizationContext.Current;
        }

        /// <summary>
        /// Загрузка данных сотрудников - кто сейчас работает а так же кто числится что может делать какие-то перечисления
        /// </summary>
        /// <returns></returns>
        public IList<DataRowView> FetchData()
        {
            string ex_message;
            odaRetent = new OracleDataAdapter(query, Connect.CurConnect);
            odaRetent.SelectCommand.BindByName = true;
            odaRetent.SelectCommand.Parameters.Add("p_worker_id", OracleDbType.Decimal, WorkerID, ParameterDirection.Input);
            DataTable t = new DataTable();
            if (odaRetent != null)
                try
                {
                    odaRetent.Fill(t);
                }
                catch (RuntimeWrappedException ex)
                {
                    ex_message = ex.Message;
                }
                catch (Exception ex1)
                {
                    ex_message = ex1.Message;
                }
            if (odaRetent != null)
                odaRetent.Dispose();
            return t.DefaultView.OfType<DataRowView>().ToList();
        }

        public void CancelFetch()
        {
            if (odaRetent != null && odaRetent.SelectCommand != null)
            {
                try
                {
                    odaRetent.SelectCommand.Cancel();
                }
                catch
                { }
            }
        }

        public SynchronizationContext GetSyncContext()
        {
            return sync;
        }
    }
}
