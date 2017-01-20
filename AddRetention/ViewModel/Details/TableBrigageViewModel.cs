using EntityGenerator;
using LibrarySalary.Helpers;
using Oracle.DataAccess.Client;
using Salary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace Salary.ViewModel
{
    public class TableBrigageViewModel: NotificationObject, IDataErrorInfo
    {
        OracleDataAdapter odaTable_Brigage, odaBrigages, odaPieceWork;
        OracleConnection connect;
        private DateTime _selectedDate = DateTime.Today.Day>3?DateTime.Today.Trunc("Month"):DateTime.Today.Trunc("Month").AddDays(-1).Trunc("Month");
        private decimal? _subdivID;

        DataSet ds;
        private List<TableBrigageModel> _brigageTableSource;
        private decimal? _currentBrigageID;
        private TableBrigageModel _currentTableBrigage;
        private bool _onlyActive = true;
        private List<PieceWorkData> _pieceWorkSource;
        BackgroundWorker bw_table, bw_piecework;
        private bool _isLoading = false;
        private List<EmpAccountData> _empSource;
        private bool _isDataLoading = false;


        public TableBrigageViewModel()
        {
            connect = Connect.CurConnect;
            bw_table = new BackgroundWorker();
            bw_table.WorkerSupportsCancellation = true;
            bw_table.DoWork += RefreshTableBrigage;
            bw_table.RunWorkerCompleted += bwTable_RunWorkerCompleted;
            bw_piecework = new BackgroundWorker();
            bw_piecework.WorkerSupportsCancellation = true;
            bw_piecework.DoWork += RefreshPieceWork;
            bw_piecework.RunWorkerCompleted += bwPieceWork_RunWorkerCompleted;
            ds = new DataSet();
            odaTable_Brigage = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Detail\SelectTableBrigage.sql"), connect);
            odaTable_Brigage.SelectCommand.BindByName = true;
            odaTable_Brigage.SelectCommand.Parameters.Add("p_brigage_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaTable_Brigage.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            odaTable_Brigage.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaTable_Brigage.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaTable_Brigage.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);

            odaTable_Brigage.TableMappings.Add("Table", "TABLE_BRIGAGE");
            odaTable_Brigage.TableMappings.Add("Table1", "TRANSFER");

            odaTable_Brigage.InsertCommand = new OracleCommand(string.Format(@"BEGIN {1}.TABLE_BRIGAGE_UPDATE(p_TABLE_BRIGAGE_ID=>:p_TABLE_BRIGAGE_ID,p_BRIGAGE_ID=>:p_BRIGAGE_ID,p_DEGREE_ID=>:p_DEGREE_ID,p_PER_NUM=>:p_PER_NUM,p_WORK_HOURS=>:p_WORK_HOURS,p_COEFFICIENT=>:p_COEFFICIENT,p_SIGN_COMB=>:p_SIGN_COMB,p_TRANSFER_ID=>:p_TRANSFER_ID,p_SUBDIV_ID=>:p_SUBDIV_ID,p_WORK_DATE=>:p_WORK_DATE, p_COMMENTS=>:p_COMMENTS);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaTable_Brigage.InsertCommand.BindByName = true;
            odaTable_Brigage.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaTable_Brigage.InsertCommand.Parameters.Add("p_TABLE_BRIGAGE_ID", OracleDbType.Decimal, 0, "TABLE_BRIGAGE_ID").Direction = ParameterDirection.InputOutput;
            odaTable_Brigage.InsertCommand.Parameters["p_TABLE_BRIGAGE_ID"].DbType = DbType.Decimal;
            odaTable_Brigage.InsertCommand.Parameters.Add("p_BRIGAGE_ID", OracleDbType.Decimal, 0, "BRIGAGE_ID").Direction = ParameterDirection.Input;
            odaTable_Brigage.InsertCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID").Direction = ParameterDirection.Input;
            odaTable_Brigage.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM").Direction = ParameterDirection.Input;
            odaTable_Brigage.InsertCommand.Parameters.Add("p_WORK_HOURS", OracleDbType.Decimal, 0, "WORK_HOURS").Direction = ParameterDirection.Input;
            odaTable_Brigage.InsertCommand.Parameters.Add("p_COEFFICIENT", OracleDbType.Decimal, 0, "COEFFICIENT").Direction = ParameterDirection.Input;
            odaTable_Brigage.InsertCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB").Direction = ParameterDirection.Input;
            odaTable_Brigage.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaTable_Brigage.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaTable_Brigage.InsertCommand.Parameters.Add("p_WORK_DATE", OracleDbType.Date, 0, "WORK_DATE").Direction = ParameterDirection.Input;
            odaTable_Brigage.InsertCommand.Parameters.Add("p_COMMENTS", OracleDbType.Varchar2, 0, "COMMENTS").Direction = ParameterDirection.Input;

            odaTable_Brigage.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {1}.TABLE_BRIGAGE_UPDATE(p_TABLE_BRIGAGE_ID=>:p_TABLE_BRIGAGE_ID,p_BRIGAGE_ID=>:p_BRIGAGE_ID,p_DEGREE_ID=>:p_DEGREE_ID,p_PER_NUM=>:p_PER_NUM,p_WORK_HOURS=>:p_WORK_HOURS,p_COEFFICIENT=>:p_COEFFICIENT,p_SIGN_COMB=>:p_SIGN_COMB,p_TRANSFER_ID=>:p_TRANSFER_ID,p_SUBDIV_ID=>:p_SUBDIV_ID,p_WORK_DATE=>:p_WORK_DATE, p_COMMENTS=>:p_COMMENTS);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaTable_Brigage.UpdateCommand.BindByName = true;
            odaTable_Brigage.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaTable_Brigage.UpdateCommand.Parameters.Add("p_TABLE_BRIGAGE_ID", OracleDbType.Decimal, 0, "TABLE_BRIGAGE_ID").Direction = ParameterDirection.InputOutput;
            odaTable_Brigage.UpdateCommand.Parameters["p_TABLE_BRIGAGE_ID"].DbType = DbType.Decimal;
            odaTable_Brigage.UpdateCommand.Parameters.Add("p_BRIGAGE_ID", OracleDbType.Decimal, 0, "BRIGAGE_ID").Direction = ParameterDirection.Input;
            odaTable_Brigage.UpdateCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID").Direction = ParameterDirection.Input;
            odaTable_Brigage.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM").Direction = ParameterDirection.Input;
            odaTable_Brigage.UpdateCommand.Parameters.Add("p_WORK_HOURS", OracleDbType.Decimal, 0, "WORK_HOURS").Direction = ParameterDirection.Input;
            odaTable_Brigage.UpdateCommand.Parameters.Add("p_COEFFICIENT", OracleDbType.Decimal, 0, "COEFFICIENT").Direction = ParameterDirection.Input;
            odaTable_Brigage.UpdateCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB").Direction = ParameterDirection.Input;
            odaTable_Brigage.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaTable_Brigage.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaTable_Brigage.UpdateCommand.Parameters.Add("p_WORK_DATE", OracleDbType.Date, 0, "WORK_DATE").Direction = ParameterDirection.Input;
            odaTable_Brigage.UpdateCommand.Parameters.Add("p_COMMENTS", OracleDbType.Varchar2, 0, "COMMENTS").Direction = ParameterDirection.Input;

            odaTable_Brigage.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {1}.TABLE_BRIGAGE_DELETE(:p_TABLE_BRIGAGE_ID);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaTable_Brigage.DeleteCommand.BindByName = true;
            odaTable_Brigage.DeleteCommand.Parameters.Add("p_TABLE_BRIGAGE_ID", OracleDbType.Decimal, 0, "TABLE_BRIGAGE_ID").Direction = ParameterDirection.InputOutput;

            odaBrigages = new OracleDataAdapter(@"select BRIGAGE_ID, BRIGAGE_CODE, BRIGAGE_NAME, GROUP_MASTER, SUBDIV_ID, 
                                                    DATE_BEGIN_BRIGAGE, NULLIF(DATE_END_BRIGAGE, date'3000-01-01') DATE_END_BRIGAGE 
                                                from SALARY.BRIGAGE", connect);
            odaBrigages.TableMappings.Add("Table", "BRIGAGE");

            odaPieceWork = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Detail\SelectBrigagePieceWork.sql"), connect);
            odaPieceWork.SelectCommand.BindByName = true;
            odaPieceWork.SelectCommand.Parameters.Add("p_brigage_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaPieceWork.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            odaPieceWork.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaPieceWork.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            odaPieceWork.TableMappings.Add("Table", "PIECE_WORK");

        }

        
        /// <summary>
        /// Выбранный месяц для редактирования
        /// </summary>
        [OracleParameterMapping(ParameterName = "p_date")]        
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
                    // обновляем список бригад если изменилось подразделение
                    decimal? currentBrigage = CurrentBrigageID;
                    RefreshBrigageList();
                    CurrentBrigageID = currentBrigage;
                    RaisePropertyChanged(() => IsSubdivClosed);
                }
            }
        }

        /// <summary>
        /// Выбранное подразделение 
        /// </summary>
        [OracleParameterMapping(ParameterName="p_subdiv_id")]
        public Decimal? SubdivID
        {
            get
            {
                return _subdivID;
            }
            set
            {
                if (value != _subdivID)
                {
                    _subdivID = value;
                    RaisePropertyChanged(() => SubdivID);
                    // обновляем список бригад если изменилось подразделение
                    RefreshBrigageList();
                    RaisePropertyChanged(() => IsSubdivClosed);
                }
            }
        }

        /// <summary>
        /// Показывать ли в выпадающем списке все бригады?
        /// </summary>
        public bool OnlyActive
        {
            get
            {
                return _onlyActive;
            }
            set
            {
                _onlyActive = value;
                RaisePropertyChanged(() => OnlyActive);
                RaisePropertyChanged(() => BrigageListSource);
            }
        }

        /// <summary>
        /// Закрыто ли подразделение для редактирования
        /// </summary>
        public bool IsSubdivClosed
        {
            get
            {
                if (SubdivID == null)
                    return false;
                if (SubdivID == 0)
                    return AppDataSet.Tables["SUBDIV_FOR_CLOSE"].DefaultView.OfType<DataRowView>().Where(r => r["APP_NAME"].ToString() == "PIECE_WORK").All(r => r.Row.Field2<DateTime?>("DATE_CLOSING") >= SelectedDate);
                else
                    return AppDataSet.Tables["SUBDIV_FOR_CLOSE"].DefaultView.OfType<DataRowView>().Where(r => r["APP_NAME"].ToString() == "PIECE_WORK" && r.Row.Field2<Decimal?>("SUBDIV_ID") == SubdivID).All(r => r.Row.Field2<DateTime?>("DATE_CLOSING") >= SelectedDate);
            }
        }


        /*public bool CanRefreshTableBrigage
        { 
            get
            {
                if (this.HasChanges)
                {

                }
        }*/

        /// <summary>
        /// Айдишник выбранной бригады в фильтре
        /// </summary>
        [OracleParameterMapping(ParameterName="p_brigage_id")]
        public decimal? CurrentBrigageID
        {
            get
            {
                return _currentBrigageID;
            }
            set
            {
                if (_currentBrigageID != value)
                {
                    _currentBrigageID = value;
                    RaisePropertyChanged(() => CurrentBrigageID);
                    RefreshTableBrigage();
                    RefreshPieceWork();
                }
            }
        }

        /// <summary>
        /// Текущая бригада в фильтре
        /// </summary>
        public Brigage CurrentBrigage 
        {
            get
            {
                if (CurrentBrigageID != null)
                    return BrigageListSource.FirstOrDefault(r => r.BrigageID == CurrentBrigageID);
                else
                    return null;
            }
        }

        /// <summary>
        /// Текущий выбранная запись табеля бригады
        /// </summary>
        public TableBrigageModel CurrentTableBrigage
        {
            get
            {
                return _currentTableBrigage;
            }
            set
            {
                _currentTableBrigage = value;
                RaisePropertyChanged(() => CurrentTableBrigage);
            }
        }

        /// <summary>
        /// Список бригад подразделения
        /// </summary>
        public List<Brigage> BrigageListSource
        {
            get
            {
                if (ds != null && ds.Tables.Contains("BRIGAGE"))
                {
                    return ds.Tables["BRIGAGE"].Rows.OfType<DataRow>().Select(r => new Brigage() { DataRow = r })
                        .Where(r => r.SubdivID == this.SubdivID && (!OnlyActive || SelectedDate <= (r.DateEndBrigage ?? DateTime.MaxValue) && SelectedDate >= r.DateBeginBrigage))
                        .OrderBy(r => r.BrigageCode).ToList();
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Обновляем источники данных для табеля бригады и сам табель
        /// </summary>
        private void RefreshTableBrigage(object sender, DoWorkEventArgs e)
        {
            Exception ex = odaTable_Brigage.TryFillWithClear(ds, e.Argument);
        }

        /// <summary>
        /// Обновление источника данных по нарядам
        /// </summary>
        private void RefreshPieceWork(object sender, DoWorkEventArgs e)
        {
            Exception ex = odaPieceWork.TryFillWithClear(ds, e.Argument);
        }

        /// <summary>
        /// Метод обновляет все данные по табелю бригады
        /// </summary>
        public void RefreshTableBrigage()
        {
            if (bw_table.IsBusy)
            {
                NeedRefreshTable = true;
                bw_table.CancelAsync();
                return;
            }
            IsDataLoading = true;
            IsLoading = true;
            bw_table.RunWorkerAsync(this);
        }
        /// <summary>
        /// Метод обновляет все данные по нарядам
        /// </summary>
        public void RefreshPieceWork()
        {
            if (bw_piecework.IsBusy)
            {
                NeedRefreshPieceWork = true;
                bw_piecework.CancelAsync();
                return;
            }
            IsDataLoading = true;
            IsLoading = true;
            bw_piecework.RunWorkerAsync(this);
        }

        /// <summary>
        /// ПО заверщение загрузки выполняем действия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bwTable_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (NeedRefreshTable)
            {
                NeedRefreshTable = false;
                RefreshTableBrigage();
                return;
            }
            IsDataLoading = false;
            if (e.Cancelled) return;
            if (e.Error != null)
            {
                IsDataLoading = false;
                IsLoading = false;
                MessageBox.Show(e.Error.GetFormattedException(), "Ошибка получения данных");
            }
            else
            {
                IsDataLoading = false;
                RaisePropertyChanged(() => EmpSource);
                RaisePropertyChanged(() => TableBrigageSource);
                IsLoading = false;
            }
        }
        /// <summary>
        /// ПО заверщение загрузки выполняем действия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bwPieceWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (NeedRefreshPieceWork)
            {
                NeedRefreshPieceWork = false;
                RefreshPieceWork();
                return;
            }
            IsDataLoading = false;
            if (e.Cancelled) return;
            if (e.Error != null)
            {
                IsLoading = false;
                MessageBox.Show(e.Error.GetFormattedException(), "Ошибка получения данных");
            }
            else
            { 
                RaisePropertyChanged(() => PieceWorkSource);
                IsLoading = false;
            }
        }

        /// <summary>
        /// Обновление списка доступных бригад
        /// </summary>
        public void RefreshBrigageList()
        {
            odaBrigages.TryFillWithClear(ds, this);
            RaisePropertyChanged(() => BrigageListSource);

        }

        /// <summary>
        /// Источник данных табель бригады
        /// </summary>
        public List<TableBrigageModel> TableBrigageSource
        {
            get
            {
                if (ds != null && ds.Tables.Contains("TABLE_BRIGAGE") && !IsDataLoading)
                {
                    _brigageTableSource = ds.Tables["TABLE_BRIGAGE"].Rows.OfType<DataRow>().Where(r=>r.RowState!= DataRowState.Deleted && r.RowState!= DataRowState.Detached).Select(r => new TableBrigageModel() { DataRow = r }).ToList();
                }
                else _brigageTableSource = new List<TableBrigageModel>();
                return _brigageTableSource;
            }
        }

        /// <summary>
        /// Источник данных - список сотрудников
        /// </summary>
        public List<EmpAccountData> EmpSource
        {
            get
            {
                if (ds != null && ds.Tables.Contains("TRANSFER") && !IsDataLoading)
                    _empSource = ds.Tables["TRANSFER"].Rows.OfType<DataRow>().Where(r => r.RowState != DataRowState.Deleted && r.RowState != DataRowState.Detached).Select(r => new EmpAccountData() { DataRow = r }).ToList();
                else
                    _empSource = new List<EmpAccountData>();
                return _empSource;
            }
        }

        /// <summary>
        /// Источник данных для нарядов бригад
        /// </summary>
        public List<PieceWorkData> PieceWorkSource
        {
            get
            {
                if (ds != null && ds.Tables.Contains("PIECE_WORK") && !IsDataLoading)
                {
                    _pieceWorkSource = (from t in ds.Tables["PIECE_WORK"].AsEnumerable()
                                        where t.RowState == DataRowState.Unchanged
                                        select new PieceWorkData() { DataRow = t }
                                       ).ToList();
                }
                else
                    _pieceWorkSource = new List<PieceWorkData>();
                return _pieceWorkSource;
            }
        }

        /// <summary>
        /// Добавление новой записи в табель бригады
        /// </summary>
        public void AddTableBrigage()
        {
            DataRow r = ds.Tables["TABLE_BRIGAGE"].NewRow();
            r["WORK_DATE"] = SelectedDate;
            r["BRIGAGE_ID"] = CurrentBrigageID;
            ds.Tables["TABLE_BRIGAGE"].Rows.Add(r);
            RaisePropertyChanged(() => TableBrigageSource);
            CurrentTableBrigage = TableBrigageSource.FirstOrDefault(e => e.DataRow == r);
        }

        /// <summary>
        /// Удалить запись из текущего табеля
        /// </summary>
        public void DeleteTableBrigage()
        {
            CurrentTableBrigage.DataRow.Delete();
            RaisePropertyChanged(() => TableBrigageSource);
        }

        /// <summary>
        /// Сохраняем табель бригады
        /// </summary>
        /// <returns></returns>
        public Exception Save()
        {
            OracleTransaction tr = connect.BeginTransaction();
            try
            {
                odaTable_Brigage.Update(ds.Tables["TABLE_BRIGAGE"], "TABLE_BRIGAGE_ID");
                tr.Commit();
                return null;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                return ex;
            }
        }

        /// <summary>
        /// Если ли изменения в нашем датасэте
        /// </summary>
        public bool HasChanges 
        {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }

        /// <summary>
        /// Находится ли контрол в процессе загрузки
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        /// <summary>
        /// Находится ли адаптер в процессе загрузки
        /// </summary>
        public bool IsDataLoading
        {
            get
            {
                return _isDataLoading;
            }
            set
            {
                _isDataLoading = value;
                RaisePropertyChanged(() => IsDataLoading);
            }
        }

        public bool NeedRefreshTable { get; set; }
        public bool NeedRefreshPieceWork { get; set; }

#region IDataError region
        public string Error
        {
            get 
            {
                return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get 
            {
                if (columnName == "CurrentBrigageID" && CurrentBrigageID == null)
                    return "Требуется выбрать бригаду";
                return string.Empty;
            }
        }
#endregion


        /// <summary>
        /// Автоматически заполняет табель бригады используя предыдущий месяц бригадников и учет рабочего времени
        /// </summary>
        /// <returns></returns>
        public void FormAutoTableBrigage()
        {
            OracleCommand cmd = new OracleCommand("begin SALARY.TABLE_BRIGAGE_AUTOFILL(:p_brigage_id, :p_date);end;", connect);
            cmd.BindByName = true;
            cmd.Parameters.Add("p_brigage_id", OracleDbType.Decimal, CurrentBrigageID, ParameterDirection.Input);
            cmd.Parameters.Add("p_date", OracleDbType.Date, SelectedDate, ParameterDirection.Input);
            OracleTransaction tr = connect.BeginTransaction();
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
            }
            catch 
            {
                tr.Rollback();
                throw;
            }
        }


        bool _updateAfterSave = true;
        /// <summary>
        /// Обновлять ли просмотр табеля после сохранения
        /// </summary>
        public bool UpdateAfterSave 
        {
            get
            {
                return _updateAfterSave;
            }
            set
            {
                _updateAfterSave = value;
                RaisePropertyChanged(() => UpdateAfterSave);
            }
        }
    }
}
