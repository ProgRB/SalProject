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
using Oracle.DataAccess.Client;
using System.Data;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for RetentionCalcSetting.xaml
    /// </summary>
    public partial class RetentionCalcSetting : Window
    {
        OracleDataAdapter odaSaveRetSettings, odaMethodSave;
        DataSet ds;
        public RetentionCalcSetting(object p_RETENT_CALC_METHOD_ID)
        {
            InitializeComponent();
            ds = new DataSet();
            ds.Tables.Add("CALC_METHOD");
            ds.Tables.Add("CALC_METHOD_SETTING");
            odaMethodSave = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectMethodCalcRetData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaMethodSave.SelectCommand.BindByName = true;
            odaMethodSave.SelectCommand.Parameters.Add("p_RETENT_CALC_METHOD_ID", OracleDbType.Decimal, p_RETENT_CALC_METHOD_ID, ParameterDirection.Input);
            odaMethodSave.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaMethodSave.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaMethodSave.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaMethodSave.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            odaMethodSave.UpdateCommand = new OracleCommand(string.Format(@"begin {0}.RETENT_CALC_METHOD_UPDATE(:p_RETENT_CALC_METHOD_ID, :p_MAX_SALARY_PERCENT, :p_PERCENT_RETENT
                                , :p_SUM_RETENT, :p_METHOD_NAME, :p_SIGN_INDIVIDUAL, :p_USE_TAX_DISC, :p_DECR_FOR_OTHER_RETENT, 
                                :p_TYPE_RETENT_CALC_SUM_ID, :p_TYPE_GROUP_RETENTION_ID, :p_ROUND_DECIMAL_PLACES, :p_SIGN_CALC_ORIGINAL_SUM, :p_FORMULA, :p_TYPE_REVENUE_ID); end;", Connect.SchemaSalary), Connect.CurConnect);
            odaMethodSave.UpdateCommand.BindByName = true;
            odaMethodSave.UpdateCommand.Parameters.Add("p_RETENT_CALC_METHOD_ID", OracleDbType.Decimal, ParameterDirection.InputOutput).DbType = DbType.Decimal;
            odaMethodSave.UpdateCommand.Parameters.Add("p_MAX_SALARY_PERCENT", OracleDbType.Decimal, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.Parameters.Add("p_PERCENT_RETENT", OracleDbType.Decimal, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.Parameters.Add("p_SUM_RETENT", OracleDbType.Decimal, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.Parameters.Add("p_METHOD_NAME", OracleDbType.Varchar2, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.Parameters.Add("p_USE_TAX_DISC", OracleDbType.Decimal, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.Parameters.Add("p_SIGN_INDIVIDUAL", OracleDbType.Decimal, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.Parameters.Add("p_DECR_FOR_OTHER_RETENT", OracleDbType.Decimal, 0, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.Parameters.Add("p_TYPE_RETENT_CALC_SUM_ID", OracleDbType.Decimal, 0, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.Parameters.Add("p_TYPE_GROUP_RETENTION_ID", OracleDbType.Decimal, 0, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.Parameters.Add("p_ROUND_DECIMAL_PLACES", OracleDbType.Decimal, 0, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.Parameters.Add("p_SIGN_CALC_ORIGINAL_SUM", OracleDbType.Decimal, 0, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.Parameters.Add("p_FORMULA", OracleDbType.Varchar2, 0, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.Parameters.Add("p_TYPE_REVENUE_ID", OracleDbType.Varchar2, 0, ParameterDirection.Input);
            odaMethodSave.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaMethodSave.TableMappings.Add("Table", "CALC_METHOD");
            odaMethodSave.TableMappings.Add("Table1", "TYPE_RETENT_CALC_SUM");
            odaMethodSave.TableMappings.Add("Table2", "TYPE_GROUP_RET");
            odaMethodSave.TableMappings.Add("Table3", "TYPE_REVENUE");
            odaMethodSave.Fill(ds);
            cbTYPE_RET_CALC_SUM.ItemsSource = new DataView(ds.Tables["TYPE_RETENT_CALC_SUM"], "", "", DataViewRowState.CurrentRows);
            cbTypeGroupRetention.ItemsSource = new DataView(ds.Tables["TYPE_GROUP_RET"], "", "", DataViewRowState.CurrentRows);
            cbPAY_GROUP.ItemsSource = new DataView(ds.Tables["TYPE_REVENUE"], "", "", DataViewRowState.CurrentRows);
            if (ds.Tables["CALC_METHOD"].Rows.Count==0)
            {
                ds.Tables["CALC_METHOD"].Rows.Add(ds.Tables["CALC_METHOD"].NewRow());
            }
            LoadTaxedPay();
            this.DataContext = new DataView(ds.Tables["CALC_METHOD"], "", "", DataViewRowState.CurrentRows)[0];
        }

        private void LoadTaxedPay()
        {
            if (odaSaveRetSettings == null)
            {
                odaSaveRetSettings = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectPTForRetentSetting.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                odaSaveRetSettings.TableMappings.Add("Table", "CALC_METHOD_SETTING");
                odaSaveRetSettings.TableMappings.Add("Table1", "TYPE_PAYMENT_TYPE");
                odaSaveRetSettings.SelectCommand.BindByName = true;
                odaSaveRetSettings.SelectCommand.Parameters.Add("p_RETENT_CALC_METHOD_ID", OracleDbType.Decimal, ParameterDirection.Input);
                odaSaveRetSettings.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                odaSaveRetSettings.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);

                odaSaveRetSettings.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.RETENT_CALC_SETTING_UPDATE(:p_RETENT_SETTING_ID,:p_RETENT_CALC_METHOD_ID,:p_SAL_PAY_TYPE_ID,
                                                :p_NOT_RET_SUM,:p_USE_FOR_CALC,:p_USE_FOR_RELATION,:p_USE_FOR_OTHER_CALC);end;", Connect.SchemaSalary), Connect.CurConnect);
                odaSaveRetSettings.UpdateCommand.BindByName = true;
                odaSaveRetSettings.UpdateCommand.Parameters.Add("p_RETENT_SETTING_ID", OracleDbType.Decimal, 0, "RETENT_SETTING_ID").Direction = ParameterDirection.InputOutput;
                odaSaveRetSettings.UpdateCommand.Parameters["p_RETENT_SETTING_ID"].DbType = DbType.Decimal;
                odaSaveRetSettings.UpdateCommand.Parameters.Add("p_RETENT_CALC_METHOD_ID", OracleDbType.Decimal, 0, "RETENT_CALC_METHOD_ID").Direction = ParameterDirection.Input;
                odaSaveRetSettings.UpdateCommand.Parameters.Add("p_SAL_PAY_TYPE_ID", OracleDbType.Decimal, 0, "SAL_PAY_TYPE_ID").Direction = ParameterDirection.Input;
                odaSaveRetSettings.UpdateCommand.Parameters.Add("p_NOT_RET_SUM", OracleDbType.Decimal, 0, "NOT_RET_SUM").Direction = ParameterDirection.Input;
                odaSaveRetSettings.UpdateCommand.Parameters.Add("p_USE_FOR_CALC", OracleDbType.Decimal, 0, "USE_FOR_CALC").Direction = ParameterDirection.Input;
                odaSaveRetSettings.UpdateCommand.Parameters.Add("p_USE_FOR_RELATION", OracleDbType.Decimal, 0, "USE_FOR_RELATION").Direction = ParameterDirection.Input;
                odaSaveRetSettings.UpdateCommand.Parameters.Add("p_USE_FOR_OTHER_CALC", OracleDbType.Decimal, 0, "USE_FOR_OTHER_CALC").Direction = ParameterDirection.Input;

                odaSaveRetSettings.DeleteCommand = new OracleCommand(string.Format(@"begin {0}.RETENT_CALC_SETTING_DELETE(:p_RETENT_SETTING_ID); end;", Connect.SchemaSalary), Connect.CurConnect);
                odaSaveRetSettings.DeleteCommand.BindByName = true;
                odaSaveRetSettings.DeleteCommand.Parameters.Add("p_RETENT_SETTING_ID", OracleDbType.Decimal, 0, "RETENT_SETTING_ID");
            }
            if (ds.Tables.Contains("CALC_METHOD_SETTING"))
                ds.Tables["CALC_METHOD_SETTING"].Rows.Clear();
            odaSaveRetSettings.SelectCommand.Parameters["p_RETENT_CALC_METHOD_ID"].Value = this["RETENT_CALC_METHOD_ID"];
            odaSaveRetSettings.Fill(ds);
            ds.Tables["CALC_METHOD_SETTING"].Columns.Add("ALL_CHECKED", typeof(decimal)).Expression = "USE_FOR_OTHER_CALC+USE_FOR_RELATION+USE_FOR_CALC";
            if (dgTaxedPayType.ItemsSource == null)
            {
                cbTypePaymentType.ItemsSource = new DataView(ds.Tables["TYPE_PAYMENT_TYPE"], "", "TYPE_PAYMENT_TYPE_ID", DataViewRowState.CurrentRows);
                cbTypePaymentType.SelectedValue = 1m;
                dgTaxedPayType.ItemsSource = new DataView(ds.Tables["CALC_METHOD_SETTING"], string.Format("TYPE_PAYMENT_TYPE_ID={0}", cbTypePaymentType.SelectedValue), "CODE_PAYMENT", DataViewRowState.CurrentRows);
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ds != null && ds.HasChanges() && ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }

        private void SaveRetent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgTaxedPayType.CancelEdit();
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                if (ds.Tables["Calc_METHOD"].GetChanges() != null)
                {
                    odaMethodSave.UpdateCommand.Parameters["p_RETENT_CALC_METHOD_ID"].Value = this["RETENT_CALC_METHOD_ID"];
                    odaMethodSave.UpdateCommand.Parameters["p_MAX_SALARY_PERCENT"].Value = this["MAX_SALARY_PERCENT"];
                    odaMethodSave.UpdateCommand.Parameters["p_PERCENT_RETENT"].Value = this["PERCENT_RETENT"];
                    odaMethodSave.UpdateCommand.Parameters["p_SUM_RETENT"].Value = this["SUM_RETENT"];
                    odaMethodSave.UpdateCommand.Parameters["p_METHOD_NAME"].Value = this["METHOD_NAME"];
                    odaMethodSave.UpdateCommand.Parameters["p_USE_TAX_DISC"].Value = this["USE_TAX_DISC"];
                    odaMethodSave.UpdateCommand.Parameters["p_SIGN_INDIVIDUAL"].Value = this["SIGN_INDIVIDUAL"];
                    odaMethodSave.UpdateCommand.Parameters["p_DECR_FOR_OTHER_RETENT"].Value = this["DECR_FOR_OTHER_RETENT"];
                    odaMethodSave.UpdateCommand.Parameters["p_TYPE_RETENT_CALC_SUM_ID"].Value = this["TYPE_RETENT_CALC_SUM_ID"];
                    odaMethodSave.UpdateCommand.Parameters["p_TYPE_GROUP_RETENTION_ID"].Value = this["TYPE_GROUP_RETENTION_ID"];
                    odaMethodSave.UpdateCommand.Parameters["p_ROUND_DECIMAL_PLACES"].Value = this["ROUND_DECIMAL_PLACES"];
                    odaMethodSave.UpdateCommand.Parameters["p_SIGN_CALC_ORIGINAL_SUM"].Value = this["SIGN_CALC_ORIGINAL_SUM"];
                    odaMethodSave.UpdateCommand.Parameters["p_FORMULA"].Value = this["FORMULA"];
                    odaMethodSave.UpdateCommand.Parameters["p_TYPE_REVENUE_ID"].Value = this["TYPE_REVENUE_ID"];
                    odaMethodSave.UpdateCommand.ExecuteNonQuery();
                    this["RETENT_CALC_METHOD_ID"] = odaMethodSave.UpdateCommand.Parameters["p_RETENT_CALC_METHOD_ID"].Value;
                }
                foreach (DataRow r in ds.Tables["CALC_METHOD_SETTING"].Rows)
                if (r.RowState == DataRowState.Modified)
                {
                    if (r["RETENT_SETTING_ID"] == DBNull.Value)
                    {
                        if ((decimal?)r["ALL_CHECKED"] >0)
                        {
                            odaSaveRetSettings.UpdateCommand.Parameters["p_RETENT_SETTING_ID"].Value = r["RETENT_SETTING_ID"];
                            odaSaveRetSettings.UpdateCommand.Parameters["p_RETENT_CALC_METHOD_ID"].Value = this["RETENT_CALC_METHOD_ID"];
                            odaSaveRetSettings.UpdateCommand.Parameters["p_SAL_PAY_TYPE_ID"].Value = r["SAL_PAY_TYPE_ID"];
                            odaSaveRetSettings.UpdateCommand.Parameters["p_NOT_RET_SUM"].Value = r["NOT_RET_SUM"];
                            odaSaveRetSettings.UpdateCommand.Parameters["p_USE_FOR_CALC"].Value = r["USE_FOR_CALC"];
                            odaSaveRetSettings.UpdateCommand.Parameters["p_USE_FOR_OTHER_CALC"].Value = r["USE_FOR_OTHER_CALC"];
                            odaSaveRetSettings.UpdateCommand.Parameters["p_USE_FOR_RELATION"].Value = r["USE_FOR_RELATION"];
                            odaSaveRetSettings.UpdateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                        if ((decimal?)r["ALL_CHECKED"] > 0)
                        {
                            odaSaveRetSettings.UpdateCommand.Parameters["p_RETENT_SETTING_ID"].Value = r["RETENT_SETTING_ID"];
                            odaSaveRetSettings.UpdateCommand.Parameters["p_RETENT_CALC_METHOD_ID"].Value = this["RETENT_CALC_METHOD_ID"];
                            odaSaveRetSettings.UpdateCommand.Parameters["p_SAL_PAY_TYPE_ID"].Value = r["SAL_PAY_TYPE_ID"];
                            odaSaveRetSettings.UpdateCommand.Parameters["p_NOT_RET_SUM"].Value = r["NOT_RET_SUM"];
                            odaSaveRetSettings.UpdateCommand.Parameters["p_USE_FOR_CALC"].Value = r["USE_FOR_CALC"];
                            odaSaveRetSettings.UpdateCommand.Parameters["p_USE_FOR_OTHER_CALC"].Value = r["USE_FOR_OTHER_CALC"];
                            odaSaveRetSettings.UpdateCommand.Parameters["p_USE_FOR_RELATION"].Value = r["USE_FOR_RELATION"];
                            odaSaveRetSettings.UpdateCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            odaSaveRetSettings.DeleteCommand.Parameters["p_RETENT_SETTING_ID"].Value = r["RETENT_SETTING_ID"];
                            odaSaveRetSettings.DeleteCommand.ExecuteNonQuery();
                        }
                }
                tr.Commit();
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show("Ошибка сохранения" + ex.GetFormattedException());
            }
        }
        public object this[string field]
        {
            get
            {
                return ds.Tables["CALC_METHOD"].Rows[0][field];
            }
            set
            {
                ds.Tables["CALC_METHOD"].Rows[0][field] = value;
            }
        }

        private void cbTypePaymentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTypePaymentType.SelectedValue!=null)
                (dgTaxedPayType.ItemsSource as DataView).RowFilter = string.Format("TYPE_PAYMENT_TYPE_ID={0}", cbTypePaymentType.SelectedValue);
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SelectAll_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter!=null)
                foreach (DataRowView dr in (dgTaxedPayType.ItemsSource as DataView))
                {
                    dr[e.Parameter.ToString()] = (e.Source as CheckBox).IsChecked;
                }
            e.Handled = true;
        }
    }
}
