using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Salary.Helpers;
using System.Data;
using Oracle.DataAccess.Client;
using System.Windows;
using LibrarySalary.Helpers;

namespace Salary.ViewModel
{
    public partial class PaymentPropertyViewModel: NotificationObject
    {
        DataSet ds;
        OracleDataAdapter odaPayment_Property, odaProp_Possible_Value;
        public PaymentPropertyViewModel()
        {
            ds = new DataSet();

            odaPayment_Property = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectPaymentPropertyData.sql"), Connect.CurConnect);

            odaPayment_Property.AcceptChangesDuringUpdate = false;
            odaPayment_Property.SelectCommand.BindByName = true;
            odaPayment_Property.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaPayment_Property.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaPayment_Property.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaPayment_Property.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            odaPayment_Property.SelectCommand.Parameters.Add("c5", OracleDbType.RefCursor, ParameterDirection.Output);
            odaPayment_Property.TableMappings.Add("Table", "PAYMENT_PROPERTY");
            odaPayment_Property.TableMappings.Add("Table1", "PROPERTY_AREA");
            odaPayment_Property.TableMappings.Add("Table2", "PROP_POSSIBLE_VALUE");
            odaPayment_Property.TableMappings.Add("Table3", "TYPE_PAYMENT_TYPE");
            odaPayment_Property.TableMappings.Add("Table4", "PROPERTY_TYPE");

            odaPayment_Property.Fill(ds);


            ds.Relations.Add(new DataRelation("prop_fk", ds.Tables["PAYMENT_PROPERTY"].Columns["PAYMENT_PROPERTY_ID"], ds.Tables["PROP_POSSIBLE_VALUE"].Columns["PAYMENT_PROPERTY_ID"], false));
            ds.Relations.Add(new DataRelation("type_prop_fk", ds.Tables["PROPERTY_TYPE"].Columns["PROPERTY_TYPE_ID"], ds.Tables["PAYMENT_PROPERTY"].Columns["PROPERTY_TYPE_ID"], false));
            ds.Tables["PAYMENT_PROPERTY"].SetPrimaryColumn("PAYMENT_PROPERTY_ID");
            ds.Tables["PAYMENT_PROPERTY"].Columns.Add("PROP_TYPE_COMMENT").Expression = "Parent(type_prop_fk).PROP_TYPE_COMMENT";
            ForeignKeyConstraint fk = new ForeignKeyConstraint("fk", ds.Tables["PAYMENT_PROPERTY"].Columns["PAYMENT_PROPERTY_ID"], ds.Tables["PROP_POSSIBLE_VALUE"].Columns["PAYMENT_PROPERTY_ID"])
            {
                AcceptRejectRule = System.Data.AcceptRejectRule.Cascade,
                DeleteRule = Rule.Cascade,
                UpdateRule = Rule.Cascade
            };
            ds.Tables["PROP_POSSIBLE_VALUE"].Constraints.Add(fk);
            ForeignKeyConstraint fk1 = new ForeignKeyConstraint("fk1", ds.Tables["PAYMENT_PROPERTY"].Columns["PAYMENT_PROPERTY_ID"], ds.Tables["PROPERTY_AREA"].Columns["PAYMENT_PROPERTY_ID"])
            {
                AcceptRejectRule = System.Data.AcceptRejectRule.Cascade,
                DeleteRule = Rule.Cascade,
                UpdateRule = Rule.Cascade
            };
            ds.Tables["PROPERTY_AREA"].Constraints.Add(fk1);

#region Adapters create
            odaPayment_Property.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.PAYMENT_PROPERTY_UPDATE(:p_PAYMENT_PROPERTY_ID,:p_PROPERTY_NAME,:p_PROPERTY_TYPE_ID,:p_PROPERTY_COMMENT);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaPayment_Property.InsertCommand.BindByName = true;
            odaPayment_Property.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaPayment_Property.InsertCommand.Parameters.Add("p_PAYMENT_PROPERTY_ID", OracleDbType.Decimal, 0, "PAYMENT_PROPERTY_ID").Direction = ParameterDirection.InputOutput;
            odaPayment_Property.InsertCommand.Parameters["p_PAYMENT_PROPERTY_ID"].DbType = DbType.Decimal;
            odaPayment_Property.InsertCommand.Parameters.Add("p_PROPERTY_NAME", OracleDbType.Varchar2, 0, "PROPERTY_NAME").Direction = ParameterDirection.Input;
            odaPayment_Property.InsertCommand.Parameters.Add("p_PROPERTY_TYPE_ID", OracleDbType.Decimal, 0, "PROPERTY_TYPE_ID").Direction = ParameterDirection.Input;
            odaPayment_Property.InsertCommand.Parameters.Add("p_PROPERTY_COMMENT", OracleDbType.Varchar2, 0, "PROPERTY_COMMENT").Direction = ParameterDirection.Input;

            odaPayment_Property.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.PAYMENT_PROPERTY_UPDATE(:p_PAYMENT_PROPERTY_ID,:p_PROPERTY_NAME,:p_PROPERTY_TYPE_ID,:p_PROPERTY_COMMENT);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaPayment_Property.UpdateCommand.BindByName = true;
            odaPayment_Property.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaPayment_Property.UpdateCommand.Parameters.Add("p_PAYMENT_PROPERTY_ID", OracleDbType.Decimal, 0, "PAYMENT_PROPERTY_ID").Direction = ParameterDirection.InputOutput;
            odaPayment_Property.UpdateCommand.Parameters["p_PAYMENT_PROPERTY_ID"].DbType = DbType.Decimal;
            odaPayment_Property.UpdateCommand.Parameters.Add("p_PROPERTY_NAME", OracleDbType.Varchar2, 0, "PROPERTY_NAME").Direction = ParameterDirection.Input;
            odaPayment_Property.UpdateCommand.Parameters.Add("p_PROPERTY_TYPE_ID", OracleDbType.Decimal, 0, "PROPERTY_TYPE_ID").Direction = ParameterDirection.Input;
            odaPayment_Property.UpdateCommand.Parameters.Add("p_PROPERTY_COMMENT", OracleDbType.Varchar2, 0, "PROPERTY_COMMENT").Direction = ParameterDirection.Input;

            odaPayment_Property.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.PAYMENT_PROPERTY_DELETE(:p_PAYMENT_PROPERTY_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaPayment_Property.DeleteCommand.BindByName = true;
            odaPayment_Property.DeleteCommand.Parameters.Add("p_PAYMENT_PROPERTY_ID", OracleDbType.Decimal, 0, "PAYMENT_PROPERTY_ID").Direction = ParameterDirection.InputOutput;

            odaProp_Possible_Value = new OracleDataAdapter("", Connect.CurConnect);
            odaProp_Possible_Value.AcceptChangesDuringUpdate = false;
            odaProp_Possible_Value.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.PROP_POSSIBLE_VALUE_UPDATE(:p_PROP_POSSIBLE_VALUE_ID,:p_POSS_VALUE_NUMBER,:p_POSS_VALUE_VARCHAR,:p_PAYMENT_PROPERTY_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaProp_Possible_Value.InsertCommand.BindByName = true;
            odaProp_Possible_Value.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaProp_Possible_Value.InsertCommand.Parameters.Add("p_PROP_POSSIBLE_VALUE_ID", OracleDbType.Decimal, 0, "PROP_POSSIBLE_VALUE_ID").Direction = ParameterDirection.InputOutput;
            odaProp_Possible_Value.InsertCommand.Parameters["p_PROP_POSSIBLE_VALUE_ID"].DbType = DbType.Decimal;
            odaProp_Possible_Value.InsertCommand.Parameters.Add("p_POSS_VALUE_NUMBER", OracleDbType.Decimal, 0, "POSS_VALUE_NUMBER").Direction = ParameterDirection.Input;
            odaProp_Possible_Value.InsertCommand.Parameters.Add("p_POSS_VALUE_VARCHAR", OracleDbType.Varchar2, 0, "POSS_VALUE_VARCHAR").Direction = ParameterDirection.Input;
            odaProp_Possible_Value.InsertCommand.Parameters.Add("p_PAYMENT_PROPERTY_ID", OracleDbType.Decimal, 0, "PAYMENT_PROPERTY_ID").Direction = ParameterDirection.Input;

            odaProp_Possible_Value.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.PROP_POSSIBLE_VALUE_UPDATE(:p_PROP_POSSIBLE_VALUE_ID,:p_POSS_VALUE_NUMBER,:p_POSS_VALUE_VARCHAR,:p_PAYMENT_PROPERTY_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaProp_Possible_Value.UpdateCommand.BindByName = true;
            odaProp_Possible_Value.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaProp_Possible_Value.UpdateCommand.Parameters.Add("p_PROP_POSSIBLE_VALUE_ID", OracleDbType.Decimal, 0, "PROP_POSSIBLE_VALUE_ID").Direction = ParameterDirection.InputOutput;
            odaProp_Possible_Value.UpdateCommand.Parameters["p_PROP_POSSIBLE_VALUE_ID"].DbType = DbType.Decimal;
            odaProp_Possible_Value.UpdateCommand.Parameters.Add("p_POSS_VALUE_NUMBER", OracleDbType.Decimal, 0, "POSS_VALUE_NUMBER").Direction = ParameterDirection.Input;
            odaProp_Possible_Value.UpdateCommand.Parameters.Add("p_POSS_VALUE_VARCHAR", OracleDbType.Varchar2, 0, "POSS_VALUE_VARCHAR").Direction = ParameterDirection.Input;
            odaProp_Possible_Value.UpdateCommand.Parameters.Add("p_PAYMENT_PROPERTY_ID", OracleDbType.Decimal, 0, "PAYMENT_PROPERTY_ID").Direction = ParameterDirection.Input;

            odaProp_Possible_Value.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.PROP_POSSIBLE_VALUE_DELETE(:p_PROP_POSSIBLE_VALUE_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaProp_Possible_Value.DeleteCommand.BindByName = true;
            odaProp_Possible_Value.DeleteCommand.Parameters.Add("p_PROP_POSSIBLE_VALUE_ID", OracleDbType.Decimal, 0, "PROP_POSSIBLE_VALUE_ID").Direction = ParameterDirection.InputOutput;
#endregion
        }

        /// <summary>
        /// Представление для свойств
        /// </summary>
        public DataView PaymentPropertySource
        {
            get
            {
                return new DataView(ds.Tables["PAYMENT_PROPERTY"], "", "PROPERTY_NAME", DataViewRowState.CurrentRows);
            }
        }

        DataRowView _currentProperty;
        public DataRowView CurrentProperty
        {
            get
            {
                return _currentProperty;
            }
            set
            {
                _currentProperty = value;
                RaisePropertyChanged(()=>CurrentProperty);
                RaisePropertyChanged(() => ProposedValues);
            }
        }

        /// <summary>
        /// Типы свойств источник
        /// </summary>
        public DataView TypePropertySource
        {
            get
            {
                return ds.Tables["PROPERTY_TYPE"].DefaultView;
            }
        }

        /// <summary>
        /// Возможные значения для выбранного свойства
        /// </summary>
        public DataView ProposedValues
        {
            get
            {
                if (CurrentProperty != null)
                    return CurrentProperty.CreateChildView("prop_fk");
                else
                    return null;
            }
        }
        /// <summary>
        /// Есть ли изменения в данных
        /// </summary>
        public bool HasChanges 
        {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }

        public void Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                int k = -2;
                foreach(DataRow r in ds.Tables["PAYMENT_PROPERTY"].Rows)
                    if (r.RowState == DataRowState.Added && r.Field2<Decimal>("PAYMENT_PROPERTY_ID")>0)
                    {
                        r["PAYMENT_PROPERTY_ID"] = --k;
                    }
                odaPayment_Property.Update(ds.Tables["PAYMENT_PROPERTY"]);
                odaProp_Possible_Value.Update(ds.Tables["PROP_POSSIBLE_VALUE"]);
                ds.AcceptChanges();
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения");
            }
        }

        public  void AddNew()
        {
            PaymentPropertySource.AddNew();
        }

        public void DeleteCurrent()
        {
            CurrentProperty.Delete();
        }
    }
}
