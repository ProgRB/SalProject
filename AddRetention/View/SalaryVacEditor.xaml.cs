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
using System.ComponentModel;
using System.Linq.Expressions;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for SalaryVacEditor.xaml
    /// </summary>
    public partial class SalaryVacEditor : Window, INotifyPropertyChanged
    {
        DataSet ds = new DataSet();
        private OracleDataAdapter odaSalary_Vac;
        public SalaryVacEditor(object p_salary_docum_id, object p_salary_vac_id)
        {
            odaSalary_Vac = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectSalaryVacData.sql"), Connect.CurConnect);
            odaSalary_Vac.SelectCommand.BindByName = true;
            odaSalary_Vac.SelectCommand.Parameters.Add("p_salary_vac_id", OracleDbType.Decimal, p_salary_vac_id, ParameterDirection.Input);
            odaSalary_Vac.SelectCommand.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, p_salary_docum_id, ParameterDirection.Input);
            odaSalary_Vac.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalary_Vac.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);

            odaSalary_Vac.TableMappings.Add("Table", "SALARY_VAC");
            odaSalary_Vac.TableMappings.Add("Table1", "Retention");

            odaSalary_Vac.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_VAC_UPDATE(:p_SALARY_VAC_ID,:p_SUM_SAL,:p_RETENTION_ID,:p_SALARY_DOCUM_ID,:p_PAYMENT_TYPE_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Vac.InsertCommand.BindByName = true;
            odaSalary_Vac.InsertCommand.Parameters.Add("p_SALARY_VAC_ID", OracleDbType.Decimal, 0, "SALARY_VAC_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Vac.InsertCommand.Parameters["p_SALARY_VAC_ID"].DbType = DbType.Decimal;
            odaSalary_Vac.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL").Direction = ParameterDirection.Input;
            odaSalary_Vac.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").Direction = ParameterDirection.Input;
            odaSalary_Vac.InsertCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID").Direction = ParameterDirection.Input;
            odaSalary_Vac.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input; 
            
            odaSalary_Vac.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_VAC_UPDATE(:p_SALARY_VAC_ID,:p_SUM_SAL,:p_RETENTION_ID,:p_SALARY_DOCUM_ID,:p_PAYMENT_TYPE_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaSalary_Vac.UpdateCommand.BindByName = true;
            odaSalary_Vac.UpdateCommand.Parameters.Add("p_SALARY_VAC_ID", OracleDbType.Decimal, 0, "SALARY_VAC_ID").Direction = ParameterDirection.InputOutput;
            odaSalary_Vac.UpdateCommand.Parameters["p_SALARY_VAC_ID"].DbType = DbType.Decimal;
            odaSalary_Vac.UpdateCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL").Direction = ParameterDirection.Input;
            odaSalary_Vac.UpdateCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").Direction = ParameterDirection.Input;
            odaSalary_Vac.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID").Direction = ParameterDirection.Input;
            odaSalary_Vac.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input; 
            
            

            odaSalary_Vac.Fill(ds);
            if (ds.Tables["SALARY_VAC"].Rows.Count == 0)
            {
                DataRow r = ds.Tables["SALARY_VAC"].NewRow();
                r["SALARY_DOCUM_ID"] = p_salary_docum_id;
                ds.Tables["SALARY_VAC"].Rows.Add(r);
            }
            InitializeComponent();
            this.DataContext = ds.Tables["SALARY_VAC"].DefaultView[0];
        }

        private void Save_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && ds != null && ds.HasChanges();
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                odaSalary_Vac.Update(ds.Tables["SALARY_VAC"]);
                tr.Commit();
                this.DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
            }
        }

        DataView _retentionSource;
        public DataView RetentionSource
        {
            get
            {
                if (_retentionSource == null)
                    _retentionSource = new DataView(ds.Tables["RETENTION"], string.Format("PAYMENT_TYPE_ID={0}", MainRow.Field2<Decimal?>("PAYMENT_TYPE_ID")??-1), "ORDER_NUMBER, DATE_START_RET", DataViewRowState.CurrentRows);
                return _retentionSource;
            }
        }

        public DataView PaymentSource
        {
            get
            {
                return new DataView(AppDataSet.Tables["PAYMENT_TYPE"], "TYPE_PAYMENT_TYPE_ID=9", "CODE_PAYMENT", DataViewRowState.CurrentRows);
            }
        }

        public DataRow MainRow
        {
            get
            {
                return ds.Tables["SALARY_VAC"].Rows[0];
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RaisePropertyChanged(() => RetentionSource);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            RaisePropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
