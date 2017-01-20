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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Oracle.DataAccess.Client;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for ExceptAvgEditor.xaml
    /// </summary>
    public partial class ExceptAvgEditor : UserControl
    {
        DataSet ds = new DataSet();
        OracleDataAdapter odaExcept_Calc_Avg;
        public ExceptAvgEditor()
        {
            odaExcept_Calc_Avg = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectExceptCalcData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaExcept_Calc_Avg.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaExcept_Calc_Avg.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaExcept_Calc_Avg.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.EXCEPT_CALC_AVG_UPDATE(:p_EXCEPT_CALC_AVG_ID,:p_TRANSFER_ID,:p_DATE_START,:p_DATE_END,:p_BASE_TARIFF);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaExcept_Calc_Avg.InsertCommand.BindByName=true;
            odaExcept_Calc_Avg.InsertCommand.Parameters.Add("p_EXCEPT_CALC_AVG_ID", OracleDbType.Decimal, 0, "EXCEPT_CALC_AVG_ID").Direction = ParameterDirection.InputOutput;
            odaExcept_Calc_Avg.InsertCommand.Parameters["p_EXCEPT_CALC_AVG_ID"].DbType = DbType.Decimal;
            odaExcept_Calc_Avg.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaExcept_Calc_Avg.InsertCommand.Parameters.Add("p_DATE_START", OracleDbType.Date, 0, "DATE_START").Direction = ParameterDirection.Input;
            odaExcept_Calc_Avg.InsertCommand.Parameters.Add("p_DATE_END", OracleDbType.Date, 0, "DATE_END").Direction = ParameterDirection.Input;
            odaExcept_Calc_Avg.InsertCommand.Parameters.Add("p_BASE_TARIFF", OracleDbType.Decimal, 0, "BASE_TARIFF").Direction = ParameterDirection.Input;

            odaExcept_Calc_Avg.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.EXCEPT_CALC_AVG_UPDATE(:p_EXCEPT_CALC_AVG_ID,:p_TRANSFER_ID,:p_DATE_START,:p_DATE_END,:p_BASE_TARIFF);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaExcept_Calc_Avg.UpdateCommand.BindByName = true;
            odaExcept_Calc_Avg.UpdateCommand.Parameters.Add("p_EXCEPT_CALC_AVG_ID", OracleDbType.Decimal, 0, "EXCEPT_CALC_AVG_ID").Direction = ParameterDirection.InputOutput;
            odaExcept_Calc_Avg.UpdateCommand.Parameters["p_EXCEPT_CALC_AVG_ID"].DbType = DbType.Decimal;
            odaExcept_Calc_Avg.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.Input;
            odaExcept_Calc_Avg.UpdateCommand.Parameters.Add("p_DATE_START", OracleDbType.Date, 0, "DATE_START").Direction = ParameterDirection.Input;
            odaExcept_Calc_Avg.UpdateCommand.Parameters.Add("p_DATE_END", OracleDbType.Date, 0, "DATE_END").Direction = ParameterDirection.Input;
            odaExcept_Calc_Avg.UpdateCommand.Parameters.Add("p_BASE_TARIFF", OracleDbType.Decimal, 0, "BASE_TARIFF").Direction = ParameterDirection.Input;

            odaExcept_Calc_Avg.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.EXCEPT_CALC_AVG_DELETE(:p_EXCEPT_CALC_AVG_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaExcept_Calc_Avg.DeleteCommand.BindByName = true;
            odaExcept_Calc_Avg.DeleteCommand.Parameters.Add("p_EXCEPT_CALC_AVG_ID", OracleDbType.Decimal, 0, "EXCEPT_CALC_AVG_ID").Direction = ParameterDirection.InputOutput;

            odaExcept_Calc_Avg.TableMappings.Add("Table", "ExceptCalc");
            odaExcept_Calc_Avg.TableMappings.Add("Table1", "Transfer");
            odaExcept_Calc_Avg.Fill(ds);
            ds.Tables["Transfer"].PrimaryKey = new DataColumn[] { ds.Tables["Transfer"].Columns["Transfer_id"] };
            ds.Tables["Transfer"].Columns.Add("EMP_FIO").Expression = "PER_NUM+' '+FIO+'(принят '+DATE_HIRE+')  совмещение: '+ISNULL(SIGN_COMB, '<нет>')";
            InitializeComponent();
        }

        private DataTable ExceptTable
        {
            get
            {
                return ds.Tables["ExceptCalc"];
            }
        }

        public  DataView ExceptView
        {
            get
            {
                return ds.Tables["ExceptCalc"].DefaultView;
            }
        }

        DataView dvTransfer = null;
        public DataView TransferView
        {
            get
            {
                if (dvTransfer == null)
                    dvTransfer = new DataView(ds.Tables["Transfer"], "", "PER_NUM", DataViewRowState.CurrentRows);
                return dvTransfer;
            }
        }

        private CollectionViewSource TableSource
        {
            get
            {
                return (CollectionViewSource) this.FindResource("TableSource");
            }
        }

        public DataTable EmpTable
        {
            get
            {
                return ds.Tables["Transfer"];
            }
        }

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ExceptTable.Rows.Add(ExceptTable.NewRow());
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && TableSource.View.CurrentItem != null;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (TableSource.View.CurrentItem as DataRowView).Delete();
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && ds != null && ds.HasChanges();
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgExcept.CommitEdit();
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                odaExcept_Calc_Avg.Update(ExceptTable);
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException());
            }
        }
    }
}
