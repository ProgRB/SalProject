using EntityGenerator;
using Oracle.DataAccess.Client;
using Salary.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace Salary.ViewModel
{
    public class BrigageViewModel: NotificationObject
    {
        DataSet ds;
        DataView _listBrigage;
        Decimal? _subdivID;
        private Brigage _currentBrigage;
        OracleDataAdapter odaBrigage;

        public BrigageViewModel()
        {
            ds = new DataSet();
            ds.Tables.Add(AppDataSet.Tables["SUBDIV"].Copy());
            odaBrigage = new OracleDataAdapter(@"select * from salary.brigage where subdiv_id in (select subdiv_id from apstaff.subdiv_roles_all where
                                                subdiv_id in (select subdiv_id from apstaff.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id))", Connect.CurConnect);
            odaBrigage.SelectCommand.BindByName = true;
            odaBrigage.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, SubdivID, ParameterDirection.Input);
            odaBrigage.TableMappings.Add("Table", "BRIGAGE");

            odaBrigage.InsertCommand = new OracleCommand(string.Format(@"BEGIN {1}.BRIGAGE_UPDATE(p_BRIGAGE_ID=>:p_BRIGAGE_ID,p_BRIGAGE_CODE=>:p_BRIGAGE_CODE,p_BRIGAGE_NAME=>:p_BRIGAGE_NAME,p_GROUP_MASTER=>:p_GROUP_MASTER,p_SUBDIV_ID=>:p_SUBDIV_ID,p_DATE_BEGIN_BRIGAGE=>:p_DATE_BEGIN_BRIGAGE,p_DATE_END_BRIGAGE=>:p_DATE_END_BRIGAGE);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaBrigage.InsertCommand.BindByName = true;
            odaBrigage.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaBrigage.InsertCommand.Parameters.Add("p_BRIGAGE_ID", OracleDbType.Decimal, 0, "BRIGAGE_ID").Direction = ParameterDirection.InputOutput;
            odaBrigage.InsertCommand.Parameters["p_BRIGAGE_ID"].DbType = DbType.Decimal;
            odaBrigage.InsertCommand.Parameters.Add("p_BRIGAGE_CODE", OracleDbType.Varchar2, 0, "BRIGAGE_CODE").Direction = ParameterDirection.Input;
            odaBrigage.InsertCommand.Parameters.Add("p_BRIGAGE_NAME", OracleDbType.Varchar2, 0, "BRIGAGE_NAME").Direction = ParameterDirection.Input;
            odaBrigage.InsertCommand.Parameters.Add("p_GROUP_MASTER", OracleDbType.Varchar2, 0, "GROUP_MASTER").Direction = ParameterDirection.Input;
            odaBrigage.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaBrigage.InsertCommand.Parameters.Add("p_DATE_BEGIN_BRIGAGE", OracleDbType.Date, 0, "DATE_BEGIN_BRIGAGE").Direction = ParameterDirection.Input;
            odaBrigage.InsertCommand.Parameters.Add("p_DATE_END_BRIGAGE", OracleDbType.Date, 0, "DATE_END_BRIGAGE").Direction = ParameterDirection.Input; 

            odaBrigage.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {1}.BRIGAGE_UPDATE(p_BRIGAGE_ID=>:p_BRIGAGE_ID,p_BRIGAGE_CODE=>:p_BRIGAGE_CODE,p_BRIGAGE_NAME=>:p_BRIGAGE_NAME,p_GROUP_MASTER=>:p_GROUP_MASTER,p_SUBDIV_ID=>:p_SUBDIV_ID,p_DATE_BEGIN_BRIGAGE=>:p_DATE_BEGIN_BRIGAGE,p_DATE_END_BRIGAGE=>:p_DATE_END_BRIGAGE);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaBrigage.UpdateCommand.BindByName = true;
            odaBrigage.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaBrigage.UpdateCommand.Parameters.Add("p_BRIGAGE_ID", OracleDbType.Decimal, 0, "BRIGAGE_ID").Direction = ParameterDirection.InputOutput;
            odaBrigage.UpdateCommand.Parameters["p_BRIGAGE_ID"].DbType = DbType.Decimal;
            odaBrigage.UpdateCommand.Parameters.Add("p_BRIGAGE_CODE", OracleDbType.Varchar2, 0, "BRIGAGE_CODE").Direction = ParameterDirection.Input;
            odaBrigage.UpdateCommand.Parameters.Add("p_BRIGAGE_NAME", OracleDbType.Varchar2, 0, "BRIGAGE_NAME").Direction = ParameterDirection.Input;
            odaBrigage.UpdateCommand.Parameters.Add("p_GROUP_MASTER", OracleDbType.Varchar2, 0, "GROUP_MASTER").Direction = ParameterDirection.Input;
            odaBrigage.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaBrigage.UpdateCommand.Parameters.Add("p_DATE_BEGIN_BRIGAGE", OracleDbType.Date, 0, "DATE_BEGIN_BRIGAGE").Direction = ParameterDirection.Input;
            odaBrigage.UpdateCommand.Parameters.Add("p_DATE_END_BRIGAGE", OracleDbType.Date, 0, "DATE_END_BRIGAGE").Direction = ParameterDirection.Input; 

            odaBrigage.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {1}.BRIGAGE_DELETE(:p_BRIGAGE_ID);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaBrigage.DeleteCommand.BindByName = true;
            odaBrigage.DeleteCommand.Parameters.Add("p_BRIGAGE_ID", OracleDbType.Decimal, 0, "BRIGAGE_ID").Direction = ParameterDirection.InputOutput;

        }

        /// <summary>
        /// Подразделения
        /// </summary>
        [OracleParameterMapping(ParameterName = "p_subdiv_id")]
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

        /// <summary>
        /// Список бригад
        /// </summary>
        public List<Brigage> BrigageSource
        {
            get
            {
                if (_listBrigage == null && ds != null && ds.Tables.Contains("BRIGAGE"))
                {
                    _listBrigage = new DataView(ds.Tables["BRIGAGE"], "", "", DataViewRowState.CurrentRows);
                }
                if (_listBrigage != null)
                    return _listBrigage.OfType<DataRowView>().Select(r => new Brigage() { DataRow = r.Row }).ToList();
                else
                    return new List<Brigage>();
            }
        }


        List<Subdiv> _subdivSource;
        public List<Subdiv> SubdivSource
        { 
            get
            {
                if (_subdivSource==null)
                    _subdivSource = AppDataSet.Tables["ACCESS_SUBDIV"].Select("APP_NAME='PIECE_WORK'").Select(r=>new Subdiv(){DataRow=r}).OrderBy(r=>r.CodeSubdiv).ToList();
                return _subdivSource;
            }
        }

        /// <summary>
        /// Загружает 
        /// </summary>
        public void LoadBrigages()
        {
            Exception ex = odaBrigage.TryFillWithClear(ds, this);
            if (ex != null)
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
            RaisePropertyChanged(() => BrigageSource);
        }

        public void AddNew()
        {
            DataRowView r = _listBrigage.AddNew();
            r["SUBDIV_ID"] = SubdivID;
            r["DATE_BEGIN_BRIGAGE"] = DateTime.Today;
            RaisePropertyChanged(() => BrigageSource);
        }

        /// <summary>
        /// Текущая выбранная бригада
        /// </summary>
        public Brigage CurrentBrigage
        {
            get
            {
                return _currentBrigage;
            }
            set
            {
                _currentBrigage = value;
                RaisePropertyChanged(() => CurrentBrigage);
            }
        }

        public void DeleteBrigage()
        {
            if (CurrentBrigage != null)
                CurrentBrigage.DataRow.Delete();
            RaisePropertyChanged(() => BrigageSource);
        }

        public Exception Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                odaBrigage.Update(ds.Tables["BRIGAGE"], "BRIGAGE_ID");
                tr.Commit();
                return null;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения данных");
                return ex;
            }
        }

        /// <summary>
        /// Имееются ли изменения в модели
        /// </summary>
        public bool HasChanges 
        {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }
    }
}
