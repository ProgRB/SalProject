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

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for CostItemEditor.xaml
    /// </summary>
    public partial class CostItemEditor : UserControl, INotifyPropertyChanged
    {
        DataSet ds;
        OracleDataAdapter odaCostItemSetting, odaTypeCostItem;
        public CostItemEditor()
        {
            odaTypeCostItem = new OracleDataAdapter();
            odaTypeCostItem.InsertCommand = new OracleCommand(string.Format("begin {1}.TYPE_COST_ITEM_INSERT(:p_TYPE_COST_ITEM_ID, :p_TYPE_ITEM_NAME, :p_ITEM_COMMENT, :p_TYPE_COST_ID, :p_COST_ITEM_GROUP_ID); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaTypeCostItem.InsertCommand.BindByName = true;
            odaTypeCostItem.InsertCommand.Parameters.Add("p_TYPE_COST_ITEM_ID", OracleDbType.Decimal, 0, "TYPE_COST_ITEM_ID");
            odaTypeCostItem.InsertCommand.Parameters.Add("p_TYPE_ITEM_NAME", OracleDbType.Varchar2, 0, "TYPE_ITEM_NAME");
            odaTypeCostItem.InsertCommand.Parameters.Add("p_ITEM_COMMENT", OracleDbType.Varchar2, 0, "ITEM_COMMENT");
            odaTypeCostItem.InsertCommand.Parameters.Add("p_TYPE_COST_ID", OracleDbType.Decimal, 0, "TYPE_COST_ID");
            odaTypeCostItem.InsertCommand.Parameters.Add("p_COST_ITEM_GROUP_ID", OracleDbType.Decimal, 0, "COST_ITEM_GROUP_ID");
            odaTypeCostItem.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

            odaTypeCostItem.UpdateCommand = new OracleCommand(string.Format("begin {1}.TYPE_COST_ITEM_UPDATE(:p_TYPE_COST_ITEM_ID, :p_TYPE_ITEM_NAME, :p_ITEM_COMMENT, :p_TYPE_COST_ID, :p_COST_ITEM_GROUP_ID); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaTypeCostItem.UpdateCommand.BindByName = true;
            odaTypeCostItem.UpdateCommand.Parameters.Add("p_TYPE_COST_ITEM_ID", OracleDbType.Decimal, 0, "TYPE_COST_ITEM_ID");
            odaTypeCostItem.UpdateCommand.Parameters.Add("p_TYPE_ITEM_NAME", OracleDbType.Varchar2, 0, "TYPE_ITEM_NAME");
            odaTypeCostItem.UpdateCommand.Parameters.Add("p_ITEM_COMMENT", OracleDbType.Varchar2, 0, "ITEM_COMMENT");
            odaTypeCostItem.UpdateCommand.Parameters.Add("p_TYPE_COST_ID", OracleDbType.Decimal, 0, "TYPE_COST_ID");
            odaTypeCostItem.UpdateCommand.Parameters.Add("p_COST_ITEM_GROUP_ID", OracleDbType.Decimal, 0, "COST_ITEM_GROUP_ID");
            odaTypeCostItem.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

            odaTypeCostItem.DeleteCommand = new OracleCommand(string.Format("begin {1}.TYPE_COST_ITEM_DELETE(:p_TYPE_COST_ITEM_ID);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaTypeCostItem.DeleteCommand.BindByName = true;
            odaTypeCostItem.DeleteCommand.Parameters.Add("p_TYPE_COST_ITEM_ID", OracleDbType.Decimal, 0, "TYPE_COST_ITEM_ID");

            odaCostItemSetting = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectCostItemsSettings.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaCostItemSetting.SelectCommand.BindByName = true;
            odaCostItemSetting.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaCostItemSetting.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);

            odaCostItemSetting.InsertCommand = new OracleCommand(string.Format("begin {1}.COST_ITEM_SETTING_UPDATE(:p_cost_item_setting_id, :p_payment_type_id, :p_type_cost_item_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaCostItemSetting.InsertCommand.BindByName = true;
            odaCostItemSetting.InsertCommand.Parameters.Add("p_cost_item_setting_id", OracleDbType.Decimal, 0, "cost_item_setting_id").Direction = ParameterDirection.InputOutput;
            odaCostItemSetting.InsertCommand.Parameters["p_cost_item_setting_id"].DbType = DbType.Decimal;
            odaCostItemSetting.InsertCommand.Parameters.Add("p_payment_type_id", OracleDbType.Decimal, 0, "payment_type_id");
            odaCostItemSetting.InsertCommand.Parameters.Add("p_type_cost_item_id", OracleDbType.Decimal, 0, "type_cost_item_id");

            odaCostItemSetting.UpdateCommand = new OracleCommand(string.Format("begin {1}.COST_ITEM_SETTING_UPDATE(:p_cost_item_setting_id, :p_payment_type_id, :p_type_cost_item_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaCostItemSetting.UpdateCommand.BindByName = true;
            odaCostItemSetting.UpdateCommand.Parameters.Add("p_cost_item_setting_id", OracleDbType.Decimal, 0, "cost_item_setting_id").Direction= ParameterDirection.InputOutput;
            odaCostItemSetting.UpdateCommand.Parameters["p_cost_item_setting_id"].DbType = DbType.Decimal;
            odaCostItemSetting.UpdateCommand.Parameters.Add("p_payment_type_id", OracleDbType.Decimal, 0, "payment_type_id");
            odaCostItemSetting.UpdateCommand.Parameters.Add("p_type_cost_item_id", OracleDbType.Decimal, 0, "type_cost_item_id");

            odaCostItemSetting.DeleteCommand = new OracleCommand(string.Format("begin {1}.COST_ITEM_SETTING_DELETE(:p_cost_item_setting_id); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaCostItemSetting.DeleteCommand.BindByName = true;
            odaCostItemSetting.DeleteCommand.Parameters.Add("p_cost_item_setting_id", OracleDbType.Decimal, 0, "cost_item_setting_id");

            odaCostItemSetting.TableMappings.Add("Table", "TypeCostItem");
            odaCostItemSetting.TableMappings.Add("Table1", "CostItemSetting");
            ds = new DataSet();
            odaCostItemSetting.Fill(ds);
            ds.Relations.Add("type_cost_item_id_fk", ds.Tables["TypeCostItem"].Columns["Type_cost_item_id"], ds.Tables["CostItemSetting"].Columns["Type_cost_item_id"]);

            ds.Tables.Add(AppDataSet.Tables["PAYMENT_TYPE"].Copy());
            ds.Tables.Add(AppDataSet.Tables["TYPE_COST"].Copy());
            ds.Tables.Add(AppDataSet.Tables["COST_ITEM_GROUP"].Copy());

            ds.Relations.Add("ptfk", ds.Tables["PAYMENT_TYPE"].Columns["PAYMENT_TYPE_ID"], ds.Tables["CostItemSetting"].Columns["PAYMENT_TYPE_ID"], false);
            ds.Relations.Add("tpcostfk", ds.Tables["TYPE_COST"].Columns["TYPE_COST_ID"], ds.Tables["TypeCostItem"].Columns["TYPE_COST_ID"], false);
            ds.Relations.Add("costgroupfk", ds.Tables["COST_ITEM_GROUP"].Columns["COST_ITEM_GROUP_ID"], ds.Tables["TypeCostItem"].Columns["COST_ITEM_GROUP_ID"], false);

            ds.Tables["TypeCostItem"].Columns.Add("TypeCostName").Expression = "Parent(tpcostfk).TYPE_COST_NAME";
            ds.Tables["TypeCostItem"].Columns.Add("CostItemGroup").Expression = "Parent(costgroupfk).COST_GROUP_NAME";
            ds.Tables["CostItemSetting"].Columns.Add("CodePayment").Expression = "Parent(ptfk).CODE_PAYMENT";

            InitializeComponent();
            //cbclTypeCost.ItemsSource = AppDataSet.Tables["TYPE_COST"].DefaultView;
            cbclCodePayment.ItemsSource = new DataView(AppDataSet.Tables["PAYMENT_TYPE"], "", "CODE_PAYMENT", DataViewRowState.CurrentRows);
            cbclNamePayment.ItemsSource = new DataView(AppDataSet.Tables["PAYMENT_TYPE"], "", "CODE_PAYMENT", DataViewRowState.CurrentRows);
            this.DataContext=this;
        }

        private void AddTypeCostItem_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }
        static OracleCommand cmd = new OracleCommand(string.Format("select {0}.TYPE_COST_ITEM_ID_SEQ.nextval from dual", Connect.SchemaSalary), Connect.CurConnect);

        private void AddTypeCostItem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRow r = ds.Tables["TypeCostItem"].NewRow();
            r["TYPE_COST_ITEM_ID"] = cmd.ExecuteScalar();
            ds.Tables["TypeCostItem"].Rows.Add(r);
        }

        private void AddCostItemSetting_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && ds!=null && listTypeCostItem!=null && listTypeCostItem.SelectedItem!=null;
        }

        private void AddCostItemSetting_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRow r = ds.Tables["CostItemSetting"].NewRow();
            r["type_cost_item_id"] = (listTypeCostItem.SelectedItem as DataRowView)["type_cost_item_id"];
            ds.Tables["CostItemSetting"].Rows.Add(r);
        }
        public DataView MainView
        {
            get
            {
                return new DataView(ds.Tables["TypeCostItem"], "", "TYPE_ITEM_NAME", DataViewRowState.CurrentRows);
            }
        }
        public DataView ChildView
        {
            get
            {
                if (listTypeCostItem.SelectedItem != null)
                    return (listTypeCostItem.SelectedItem as DataRowView).CreateChildView("type_cost_item_id_fk");
                else return null;
            }
        }

        public DataView TypeCostView
        {
            get
            {
                return AppDataSet.Tables["TYPE_COST"].DefaultView;
            }
        }

        public DataView TypeGroupView
        {
            get
            {
                return AppDataSet.Tables["COST_ITEM_GROUP"].DefaultView;
            }
        }
        private void listTypeCostItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgSetting.ItemsSource = ChildView;
        }

        private void SaveTypeCostItemCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && ds != null && ds.HasChanges();
        }

        private void SaveTypeCostItem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                odaTypeCostItem.Update(ds.Tables["TypeCostItem"]);
                odaCostItemSetting.Update(ds.Tables["CostItemSetting"]);
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.Message, "Ошибка сохранения");
            }
        }

        private void DeleteTypeCostItem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (listTypeCostItem.SelectedItem as DataRowView).Delete();
        }

        private void DeleteCostItemSetting_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && ds != null && dgSetting != null && dgSetting.SelectedItem != null;
        }

        private void DeleteCostItemSetting_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (dgSetting.SelectedItem as DataRowView).Delete();
        }

        private void dgSetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgSetting.BeginEdit();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged<T>(Expression<Func<CostItemEditor, T>> exp)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs((exp.Body as MemberExpression).Member.Name));
            }
        }
    }
}
