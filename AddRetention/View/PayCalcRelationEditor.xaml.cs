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
using System.ComponentModel;
using System.Linq.Expressions;
using Salary.Helpers;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for PayCalcRelationEditor.xaml
    /// </summary>
    public partial class PayCalcRelationEditor : Window, INotifyPropertyChanged
    {
        DataSet ds = new DataSet();
        OracleDataAdapter odaSave, odaPayment_Property_Rel;
        private bool IsAdded = false;
        public PayCalcRelationEditor(object p_payment_calc_relation_id, object p_payment_type_id, object p_copy_pay_calc_relation=null)
        {
            odaSave = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectPayCalcRelationData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSave.SelectCommand.BindByName=true;
            odaSave.SelectCommand.Parameters.Add("p_payment_calc_relation_id", OracleDbType.Decimal, p_payment_calc_relation_id?? p_copy_pay_calc_relation, ParameterDirection.Input);
            odaSave.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSave.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSave.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSave.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSave.SelectCommand.Parameters.Add("c5", OracleDbType.RefCursor, ParameterDirection.Output);

            odaSave.UpdateCommand = new OracleCommand(string.Format(@"begin {1}.PAYMENT_CALC_RELATION_UPDATE
                          (
                               :p_PAYMENT_CALC_RELATION_ID
                              ,:p_DATE_START_CALC 
                              ,:p_DATE_END_CALC
                              ,:p_FORMULA_TO_USE
                              ,:p_RETENT_CALC_METHOD_ID
                              ,:p_PAYMENT_TYPE_ID
                              ,:p_CALC_FOR_ALL
                              ,:p_IS_ALLOW_PAST_EDIT
                              ,:p_DEF_ORDER_ID
                              ,:p_RETENT_FROM_VAC    
                              ,:p_SIGN_NOT_INDEXED
                              ,:p_TYPE_AVG_PROP_ID
                              ,:p_MAX_PAID_IN_MONTH_AVG
                              ,:p_IS_ALLOW_PAST_DAYS_EDIT
                              ,:p_IS_ALLOW_PAST_HOUR_EDIT
                              ,:p_RELAT_TYPE_REF_ID
                          ); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaSave.UpdateCommand.BindByName = true;
            odaSave.UpdateCommand.Parameters.Add("p_PAYMENT_CALC_RELATION_ID", OracleDbType.Decimal, 0, "PAYMENT_CALC_RELATION_ID").Direction = ParameterDirection.InputOutput;
            odaSave.UpdateCommand.Parameters["p_PAYMENT_CALC_RELATION_ID"].DbType = DbType.Decimal;
            odaSave.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaSave.UpdateCommand.Parameters.Add("p_DATE_START_CALC", OracleDbType.Date, 0, "DATE_START_CALC");
            odaSave.UpdateCommand.Parameters.Add("p_DATE_END_CALC", OracleDbType.Date, 0, "DATE_END_CALC");
            odaSave.UpdateCommand.Parameters.Add("p_FORMULA_TO_USE", OracleDbType.Varchar2, 0, "FORMULA_TO_USE");
            odaSave.UpdateCommand.Parameters.Add("p_RETENT_CALC_METHOD_ID", OracleDbType.Decimal, 0, "RETENT_CALC_METHOD_ID");
            odaSave.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            odaSave.UpdateCommand.Parameters.Add("p_CALC_FOR_ALL", OracleDbType.Decimal, 0, "CALC_FOR_ALL");
            odaSave.UpdateCommand.Parameters.Add("p_IS_ALLOW_PAST_EDIT", OracleDbType.Decimal, 0, "IS_ALLOW_PAST_EDIT");
            odaSave.UpdateCommand.Parameters.Add("p_DEF_ORDER_ID", OracleDbType.Decimal, 0, "DEF_ORDER_ID");
            odaSave.UpdateCommand.Parameters.Add("p_RETENT_FROM_VAC", OracleDbType.Decimal, 0, "RETENT_FROM_VAC");
            odaSave.UpdateCommand.Parameters.Add("p_SIGN_NOT_INDEXED", OracleDbType.Decimal, 0, "SIGN_NOT_INDEXED");
            odaSave.UpdateCommand.Parameters.Add("p_TYPE_AVG_PROP_ID", OracleDbType.Decimal, 0, "TYPE_AVG_PROP_ID");
            odaSave.UpdateCommand.Parameters.Add("p_MAX_PAID_IN_MONTH_AVG", OracleDbType.Decimal, 0, "MAX_PAID_IN_MONTH_AVG");
            odaSave.UpdateCommand.Parameters.Add("p_IS_ALLOW_PAST_DAYS_EDIT", OracleDbType.Decimal, 0, "IS_ALLOW_PAST_DAYS_EDIT");
            odaSave.UpdateCommand.Parameters.Add("p_IS_ALLOW_PAST_HOUR_EDIT", OracleDbType.Decimal, 0, "IS_ALLOW_PAST_HOUR_EDIT");
            odaSave.UpdateCommand.Parameters.Add("p_RELAT_TYPE_REF_ID", OracleDbType.Decimal, 0, "RELAT_TYPE_REF_ID");

        #region Адаптер сохранения прикрепленных свойств
            odaPayment_Property_Rel = new OracleDataAdapter();
            odaPayment_Property_Rel.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.PAYMENT_PROPERTY_REL_UPDATE(:p_PAYMENT_PROPERTY_REL_ID,:p_PAYMENT_CALC_RELATION_ID,:p_PAYMENT_PROPERTY_ID,:p_PROPERTY_VALUE);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaPayment_Property_Rel.InsertCommand.BindByName = true;
            odaPayment_Property_Rel.InsertCommand.Parameters.Add("p_PAYMENT_PROPERTY_REL_ID", OracleDbType.Decimal, 0, "PAYMENT_PROPERTY_REL_ID").Direction = ParameterDirection.InputOutput;
            odaPayment_Property_Rel.InsertCommand.Parameters["p_PAYMENT_PROPERTY_REL_ID"].DbType = DbType.Decimal;
            odaPayment_Property_Rel.InsertCommand.Parameters.Add("p_PAYMENT_CALC_RELATION_ID", OracleDbType.Decimal, p_payment_calc_relation_id, ParameterDirection.Input);
            odaPayment_Property_Rel.InsertCommand.Parameters.Add("p_PAYMENT_PROPERTY_ID", OracleDbType.Decimal, 0, "PAYMENT_PROPERTY_ID").Direction = ParameterDirection.Input;
            odaPayment_Property_Rel.InsertCommand.Parameters.Add("p_PROPERTY_VALUE", OracleDbType.Varchar2, 0, "PROPERTY_VALUE").Direction = ParameterDirection.Input;

            odaPayment_Property_Rel.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.PAYMENT_PROPERTY_REL_UPDATE(:p_PAYMENT_PROPERTY_REL_ID,:p_PAYMENT_CALC_RELATION_ID,:p_PAYMENT_PROPERTY_ID,:p_PROPERTY_VALUE);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaPayment_Property_Rel.UpdateCommand.BindByName = true;
            odaPayment_Property_Rel.UpdateCommand.Parameters.Add("p_PAYMENT_PROPERTY_REL_ID", OracleDbType.Decimal, 0, "PAYMENT_PROPERTY_REL_ID").Direction = ParameterDirection.InputOutput;
            odaPayment_Property_Rel.UpdateCommand.Parameters["p_PAYMENT_PROPERTY_REL_ID"].DbType = DbType.Decimal;
            odaPayment_Property_Rel.UpdateCommand.Parameters.Add("p_PAYMENT_CALC_RELATION_ID", OracleDbType.Decimal, p_payment_calc_relation_id, ParameterDirection.Input);
            odaPayment_Property_Rel.UpdateCommand.Parameters.Add("p_PAYMENT_PROPERTY_ID", OracleDbType.Decimal, 0, "PAYMENT_PROPERTY_ID").Direction = ParameterDirection.Input;
            odaPayment_Property_Rel.UpdateCommand.Parameters.Add("p_PROPERTY_VALUE", OracleDbType.Varchar2, 0, "PROPERTY_VALUE").Direction = ParameterDirection.Input;

            odaPayment_Property_Rel.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.PAYMENT_PROPERTY_REL_DELETE(:p_PAYMENT_PROPERTY_REL_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaPayment_Property_Rel.DeleteCommand.BindByName = true;
            odaPayment_Property_Rel.DeleteCommand.Parameters.Add("p_PAYMENT_PROPERTY_REL_ID", OracleDbType.Decimal, 0, "PAYMENT_PROPERTY_REL_ID").Direction = ParameterDirection.InputOutput;

        #endregion

            odaSave.TableMappings.Add("Table", "PAY_CALC_RELATION");
            odaSave.TableMappings.Add("Table1", "RETENT_CALC_METHOD");
            odaSave.TableMappings.Add("Table2", "TYPE_AVG_PROPORTION");
            odaSave.TableMappings.Add("Table3", "TYPE_REF_SALARY");
            odaSave.TableMappings.Add("Table4", "PROPERTY_RELATION");
            odaSave.Fill(ds);
            if (ds.Tables["PAY_CALC_RELATION"].Rows.Count == 0)
            {
                ds.Tables["PAY_CALC_RELATION"].Rows.Add(ds.Tables["PAY_CALC_RELATION"].NewRow());
                ds.Tables["PAY_CALC_RELATION"].Rows[0]["PAYMENT_TYPE_ID"] = p_payment_type_id;
                ds.Tables["PAY_CALC_RELATION"].AcceptChanges();
                IsAdded = true;
            }
            else if (p_copy_pay_calc_relation != null)
            {
                ds.Tables["PAY_CALC_RELATION"].Rows[0]["PAYMENT_CALC_RELATION_ID"] = DBNull.Value;
                foreach (DataRow r in PropertyRelation.Rows)
                {
                    r["PAYMENT_PROPERTY_REL_ID"] = DBNull.Value;
                }
                IsAdded = true;
            }

            InitializeComponent();
            cbRetentMethod.ItemsSource = new DataView(ds.Tables["RETENT_CALC_METHOD"], "", "METHOD_NAME", DataViewRowState.CurrentRows);
            cbTypeAvgProportion.ItemsSource = new DataView(ds.Tables["TYPE_AVG_PROPORTION"], "", "", DataViewRowState.CurrentRows);
            cbTypeRefID.ItemsSource = new DataView(ds.Tables["TYPE_REF_SALARY"], "", "", DataViewRowState.CurrentRows);

            this.DataContext = new DataView(ds.Tables["PAY_CALC_RELATION"], "", "", DataViewRowState.CurrentRows)[0];
        }
        public DataTable PropertyRelation
        {
            get
            {
                return ds.Tables["PROPERTY_RELATION"];
            }
        }

        public DataView PropertyRelationSource
        {
            get
            {
                return PropertyRelation.DefaultView;
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                if (IsAdded)
                {
                    ds.Tables["PAY_CALC_RELATION"].Rows[0]["PAYMENT_CALC_RELATION_ID"] = DBNull.Value;
                }
                odaSave.Update(new DataRow[]{ds.Tables["PAY_CALC_RELATION"].Rows[0]});
                odaPayment_Property_Rel.UpdateCommand.Parameters["p_PAYMENT_CALC_RELATION_ID"].Value = ds.Tables["PAY_CALC_RELATION"].Rows[0]["PAYMENT_CALC_RELATION_ID"];
                odaPayment_Property_Rel.InsertCommand.Parameters["p_PAYMENT_CALC_RELATION_ID"].Value = ds.Tables["PAY_CALC_RELATION"].Rows[0]["PAYMENT_CALC_RELATION_ID"];
                odaPayment_Property_Rel.Update(PropertyRelation);
                tr.Commit();
                this.DialogResult = true;
                AppDataSet.UpdateSet("PAYMENT_TYPE");
                Close();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException());
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name)&& ds!=null && ds.HasChanges() && !ValidationHelper.IsElementHasErrors(rootGrid);
        }

        private void Add_CanExeute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void Add_executed(object sender, ExecutedRoutedEventArgs e)
        {
            PropertyRelation.Rows.Add(PropertyRelation.NewRow());
            OnPropertyChanged(p => p.PropertyRelation);
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && dgProperty!=null && dgProperty.CurrentItem != null;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (dgProperty.CurrentItem as DataRowView).Delete();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged<T>(Expression<Func<PayCalcRelationEditor, T>> exp)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs((exp.Body as MemberExpression).Member.Name));
            }
        }

    }
    public class IDValueToEnumConveter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] != null && values[0] != DependencyProperty.UnsetValue && values[1] != null && values[1] != DependencyProperty.UnsetValue)
            {
                DataRow[] rt = AppDataSet.Tables["PROP_POSSIBLE_VALUE"].Select(string.Format("POSS_VALUE_NUMBER={0} and PAYMENT_PROPERTY_ID={1}", decimal.Parse(values[1].ToString()), values[0]));
                if (rt.Length > 0)
                    return rt[0]["POSS_VALUE_VARCHAR"].ToString();
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MyCellTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate
        {
            get;
            set;
        }
        public DataTemplate EnumTemplate
        {
            get;
            set;
        }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                DataRowView dv = item as DataRowView;
                DataRow r = AppDataSet.Tables["PAYMENT_PROPERTY"].Rows.Find(dv["PAYMENT_PROPERTY_ID"]);
                if (r != null)
                    return r.Field2<Decimal?>("PROPERTY_TYPE_ID") == 3 ? EnumTemplate : DefaultTemplate;
            }
            return DefaultTemplate;
        }
    }

    public class PossibleValueConveter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] != null && values[0]!=DependencyProperty.UnsetValue && values[1] != null && values[1]!=DependencyProperty.UnsetValue)
            {
                return new DataView(values[1] as DataTable, "PAYMENT_PROPERTY_ID=" + values[0].ToString(), "", DataViewRowState.CurrentRows);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
