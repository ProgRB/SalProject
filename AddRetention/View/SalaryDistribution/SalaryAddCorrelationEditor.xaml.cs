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
using Oracle.DataAccess.Client;
using LibrarySalary.Helpers;
using EntityGenerator;
using System.Data;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for SalaryAddCorrelation.xaml
    /// </summary>
    public partial class SalaryAddCorrelationEditor : Window
    {
        private SalaryAddCorrelationModel _model;
        public SalaryAddCorrelationEditor(decimal? corrID, DataRow example = null)
        {
            _model = new SalaryAddCorrelationModel(corrID, example);
            InitializeComponent();
            DataContext = Model;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        /// <summary>
        /// А это у нас модель данных для формы
        /// </summary>
        public SalaryAddCorrelationModel Model
        {
            get
            {
                return _model;
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model!=null && string.IsNullOrEmpty(Model.Error);
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Model.Save())
            {
                DialogResult = true;
                Close();
            }
        }
    }

    public class SalaryAddCorrelationModel : SalaryAddCorrelation, IDataErrorInfo
    {
        OracleDataAdapter odaSalary_Add_Correlation;
        DataSet ds;
        public SalaryAddCorrelationModel(decimal? corrID, DataRow example = null)
        {
            ds = new DataSet();
            odaSalary_Add_Correlation = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution\SelecteSalaryAddCorrData.sql"), Connect.CurConnect);
            odaSalary_Add_Correlation.SelectCommand.BindByName = true;
            odaSalary_Add_Correlation.SelectCommand.Parameters.Add("p_SALARY_ADD_CORRELATION_ID", OracleDbType.Decimal, corrID, ParameterDirection.Input);
            odaSalary_Add_Correlation.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Add_Correlation.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Add_Correlation.TableMappings.Add("Table", "SALARY_ADD_CORRELATION");
            odaSalary_Add_Correlation.TableMappings.Add("Table1", "TYPE_OPERATION");

#region Адаптер сохранения данных модели

            odaSalary_Add_Correlation.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_ADD_CORRELATION_UPDATE(:p_SALARY_ADD_CORRELATION_ID,:p_SUBDIV_ID,:p_TYPE_OPERATION_ID,:p_PAYMENT_TYPE_ID,:p_ORDER_ID,:p_HOURS,:p_SUM_SAL,:p_DEGREE_ID,:p_CALC_DATE);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Add_Correlation.InsertCommand.BindByName = true;
            odaSalary_Add_Correlation.InsertCommand.Parameters.Add("p_SALARY_ADD_CORRELATION_ID", OracleDbType.Decimal, 0, "SALARY_ADD_CORRELATION_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Add_Correlation.InsertCommand.Parameters["p_SALARY_ADD_CORRELATION_ID"].DbType = DbType.Decimal;
            odaSalary_Add_Correlation.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.InsertCommand.Parameters.Add("p_TYPE_OPERATION_ID", OracleDbType.Decimal, 0, "TYPE_OPERATION_ID").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.InsertCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.InsertCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.InsertCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.InsertCommand.Parameters.Add("p_CALC_DATE", OracleDbType.Date, 0, "CALC_DATE").Direction = ParameterDirection.Input; 
            
            odaSalary_Add_Correlation.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_ADD_CORRELATION_UPDATE(:p_SALARY_ADD_CORRELATION_ID,:p_SUBDIV_ID,:p_TYPE_OPERATION_ID,:p_PAYMENT_TYPE_ID,:p_ORDER_ID,:p_HOURS,:p_SUM_SAL,:p_DEGREE_ID,:p_CALC_DATE);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Add_Correlation.UpdateCommand.BindByName = true;
            odaSalary_Add_Correlation.UpdateCommand.Parameters.Add("p_SALARY_ADD_CORRELATION_ID", OracleDbType.Decimal, 0, "SALARY_ADD_CORRELATION_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Add_Correlation.UpdateCommand.Parameters["p_SALARY_ADD_CORRELATION_ID"].DbType = DbType.Decimal;
            odaSalary_Add_Correlation.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.UpdateCommand.Parameters.Add("p_TYPE_OPERATION_ID", OracleDbType.Decimal, 0, "TYPE_OPERATION_ID").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.UpdateCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.UpdateCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.UpdateCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.UpdateCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID").Direction = ParameterDirection.Input;
            odaSalary_Add_Correlation.UpdateCommand.Parameters.Add("p_CALC_DATE", OracleDbType.Date, 0, "CALC_DATE").Direction = ParameterDirection.Input; 
            
            odaSalary_Add_Correlation.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_ADD_CORRELATION_DELETE(:p_SALARY_ADD_CORRELATION_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Add_Correlation.DeleteCommand.BindByName = true;
            odaSalary_Add_Correlation.DeleteCommand.Parameters.Add("p_SALARY_ADD_CORRELATION_ID", OracleDbType.Decimal, 0, "SALARY_ADD_CORRELATION_ID").Direction = ParameterDirection.InputOutput;
#endregion
            odaSalary_Add_Correlation.Fill(ds);
            if (ds.Tables["SALARY_ADD_CORRELATION"].Rows.Count == 0)
            {
                DataRow r = ds.Tables["SALARY_ADD_CORRELATION"].NewRow();
                ds.Tables["SALARY_ADD_CORRELATION"].Rows.Add(r);
                if (example != null)
                {
                    /*r["SUBDIV_ID"] = example["SUBDIV_ID"];
                    r["TYPE_OPERATION_ID"] = example["TYPE_OPERATION_ID"];
                    r["PAYMENT_TYPE_ID"] = example["PAYMENT_TYPE_ID"];
                    r["ORDER_ID"] = example["ORDER_ID"];
                    r["HOURS"] = example["HOURS"];
                    r["SUM_SAL"] = example["SUM_SAL"];
                    r["DEGREE_ID"] = example["DEGREE_ID"];
                    r["CALC_DATE"] = example["CALC_DATE"];*/
                }
            }
            this.DataRow = ds.Tables["SALARY_ADD_CORRELATION"].Rows[0];
        }

        /// <summary>
        /// Сохранение изменений по модели
        /// </summary>
        /// <returns></returns>
        internal bool Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                odaSalary_Add_Correlation.Update(this.DataTable);
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
                return false;
            }
        }

        /// <summary>
        /// Источник данных для категорий 
        /// </summary>
        public DataView DegreeSource
        {
            get
            {
                return new DataView(AppDataSet.Tables["DEGREE"], "", "", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Источник данных подразделения доступные
        /// </summary>
        public DataView SubdivSource
        {
            get
            {
                return new DataView(AppDataSet.Tables["ACCESS_SUBDIV"], "APP_NAME='SALARY'", "", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Источник данных - тип операции - опция
        /// </summary>
        public DataView TypeOperationSource
        {
            get
            {
                return new DataView(ds.Tables["TYPE_OPERATION"], "", "", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Источник данных видов оплат
        /// </summary>
        public IEnumerable<PaymentType> PaymentTypeSource
        { 
            get
            {
                return Salary.Helpers.AppDictionaries.CodePaymentIDToValue.Values.Where(r=>r.TypePaymentTypeID==1 && r.CodePayment!="101Т").OrderBy(r=>r.CodePayment);
            }
        }

        /// <summary>
        /// Источник данных заказов
        /// </summary>
        public DataView OrderSource
        {
            get
            {
                return new DataView(AppDataSet.Tables["ORDER"], "", "", DataViewRowState.CurrentRows);
            }
        }
    }
}
