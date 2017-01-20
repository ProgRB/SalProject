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
using Salary;
using LibrarySalary.Helpers;
using Salary.Loan.Classes;

namespace Salary.Loan
{
    /// <summary>
    /// Interaction logic for Loan_Cost_Item_Viewer.xaml
    /// </summary>
    public partial class Loan_Cost_Item_Viewer : UserControl
    {
        private static OracleDataAdapter _daLoan_Cost_Item = new OracleDataAdapter(), _daItem_Fin_Plan = new OracleDataAdapter();
        private int _ID = -1;
        public Loan_Cost_Item_Viewer()
        {
            InitializeComponent();
            dgLoan_Cost_Item.DataContext = LoanDataSet.Loan_Cost_Item.DefaultView;
            dgItem_Fin_Plan.DataContext = LoanDataSet.Item_Fin_Plan.DefaultView;

        }

        static Loan_Cost_Item_Viewer()
        {
            // Insert
            _daLoan_Cost_Item.InsertCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.LOAN_COST_ITEM_UPDATE(:LOAN_COST_ITEM_ID, :LOAN_COST_ITEM_NAME, :LOAN_COST_ITEM_CODE);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daLoan_Cost_Item.InsertCommand.BindByName = true;
            _daLoan_Cost_Item.InsertCommand.Parameters.Add("LOAN_COST_ITEM_ID", OracleDbType.Decimal, 0, "LOAN_COST_ITEM_ID").Direction = 
                ParameterDirection.InputOutput;
            _daLoan_Cost_Item.InsertCommand.Parameters["LOAN_COST_ITEM_ID"].DbType = DbType.Decimal;
            _daLoan_Cost_Item.InsertCommand.Parameters.Add("LOAN_COST_ITEM_NAME", OracleDbType.Varchar2, 0, "LOAN_COST_ITEM_NAME");
            _daLoan_Cost_Item.InsertCommand.Parameters.Add("LOAN_COST_ITEM_CODE", OracleDbType.Varchar2, 0, "LOAN_COST_ITEM_CODE");
            // Update
            _daLoan_Cost_Item.UpdateCommand = _daLoan_Cost_Item.InsertCommand;
            // Delete
            _daLoan_Cost_Item.DeleteCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.LOAN_COST_ITEM_DELETE(:LOAN_COST_ITEM_ID);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daLoan_Cost_Item.DeleteCommand.BindByName = true;
            _daLoan_Cost_Item.DeleteCommand.Parameters.Add("LOAN_COST_ITEM_ID", OracleDbType.Decimal, 0, "LOAN_COST_ITEM_ID");

            // Insert
            _daItem_Fin_Plan.InsertCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.ITEM_FIN_PLAN_UPDATE(:ITEM_FIN_PLAN_ID, :ITEM_DATE, :ITEM_NOTE, :ITEM_CODE, :LOAN_COST_ITEM_ID);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daItem_Fin_Plan.InsertCommand.BindByName = true;
            _daItem_Fin_Plan.InsertCommand.Parameters.Add("ITEM_FIN_PLAN_ID", OracleDbType.Decimal, 0, "ITEM_FIN_PLAN_ID").Direction = 
                ParameterDirection.InputOutput;
            _daItem_Fin_Plan.InsertCommand.Parameters["ITEM_FIN_PLAN_ID"].DbType = DbType.Decimal;
            _daItem_Fin_Plan.InsertCommand.Parameters.Add("ITEM_DATE", OracleDbType.Date, 0, "ITEM_DATE");
            _daItem_Fin_Plan.InsertCommand.Parameters.Add("ITEM_NOTE", OracleDbType.Varchar2, 0, "ITEM_NOTE");
            _daItem_Fin_Plan.InsertCommand.Parameters.Add("ITEM_CODE", OracleDbType.Decimal, 0, "ITEM_CODE");
            _daItem_Fin_Plan.InsertCommand.Parameters.Add("LOAN_COST_ITEM_ID", OracleDbType.Decimal, 0, "LOAN_COST_ITEM_ID");
            // Update
            _daItem_Fin_Plan.UpdateCommand = _daItem_Fin_Plan.InsertCommand;
            // Delete
            _daItem_Fin_Plan.DeleteCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.ITEM_FIN_PLAN_DELETE(:ITEM_FIN_PLAN_ID);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daItem_Fin_Plan.DeleteCommand.BindByName = true;
            _daItem_Fin_Plan.DeleteCommand.Parameters.Add("ITEM_FIN_PLAN_ID", OracleDbType.Decimal, 0, "ITEM_FIN_PLAN_ID");
        }

        void GetLoan_Cost_Item()
        {
            LoanDataSet.Loan_Cost_Item.Clear();
            _daLoan_Cost_Item.Fill(LoanDataSet.Loan_Cost_Item);
        }

        private void dg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ((DataGrid)sender).CommitEdit(DataGridEditingUnit.Row, true);
            ((DataGrid)sender).BeginEdit();
        }

        private void AddLoan_Cost_Item_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void AddLoan_Cost_Item_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView newRow = LoanDataSet.Loan_Cost_Item.DefaultView.AddNew();
            newRow["LOAN_COST_ITEM_ID"] = _ID--;
            LoanDataSet.Loan_Cost_Item.Rows.Add(newRow.Row);
            dgLoan_Cost_Item.SelectedItem = newRow;
        }

        private void DeleteLoan_Cost_Item_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgLoan_Cost_Item != null && dgLoan_Cost_Item.SelectedCells.Count > 0 &&
                ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()) &&
                LoanDataSet.Loan_Cost_Item.GetChanges() == null)
                e.CanExecute = true;
        }

        private void DeleteLoan_Cost_Item_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Ссуды",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                while (dgLoan_Cost_Item.SelectedCells.Count > 0)
                {
                    (dgLoan_Cost_Item.SelectedCells[0].Item as DataRowView).Delete();
                    SaveLoan_Cost_Item();
                }
            }
            dgLoan_Cost_Item.Focus();
        }

        private void SaveLoan_Cost_Item_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (LoanDataSet.Loan_Cost_Item.GetChanges() != null &&
                ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void SaveLoan_Cost_Item_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgLoan_Cost_Item.CommitEdit(DataGridEditingUnit.Row, true);
            SaveLoan_Cost_Item();
        }

        void SaveLoan_Cost_Item()
        {
            OracleTransaction transact = Connect.CurConnect.BeginTransaction();
            try
            {
                _daLoan_Cost_Item.InsertCommand.Transaction = transact;
                _daLoan_Cost_Item.UpdateCommand.Transaction = transact;
                _daLoan_Cost_Item.DeleteCommand.Transaction = transact;
                _daLoan_Cost_Item.Update(LoanDataSet.Loan_Cost_Item);
                transact.Commit();
            }
            catch (Exception ex)
            {
                transact.Rollback();
                MessageBox.Show(ex.Message, "Ссуды - Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                LoanDataSet.Loan_Cost_Item.RejectChanges();
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private void CancelLoan_Cost_Item_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (LoanDataSet.Loan_Cost_Item.GetChanges() != null)
                e.CanExecute = true;
        }

        private void CancelLoan_Cost_Item_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить все несохраненные изменения?", "Ссуды", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                LoanDataSet.Loan_Cost_Item.RejectChanges();
        }

        private void AddItem_Fin_Plan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()) &&
                dgLoan_Cost_Item != null && dgLoan_Cost_Item.SelectedCells.Count > 0 &&
                LoanDataSet.Loan_Cost_Item.GetChanges() == null)
                e.CanExecute = true;
        }

        private void AddItem_Fin_Plan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView newRow = LoanDataSet.Item_Fin_Plan.DefaultView.AddNew();
            newRow["LOAN_COST_ITEM_ID"] = ((DataRowView)dgLoan_Cost_Item.SelectedCells[0].Item)["LOAN_COST_ITEM_ID"];
            LoanDataSet.Item_Fin_Plan.Rows.Add(newRow.Row);
            dgItem_Fin_Plan.SelectedItem = newRow;
        }

        private void DeleteItem_Fin_Plan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgItem_Fin_Plan != null && dgItem_Fin_Plan.SelectedCells.Count > 0 &&
                LoanDataSet.Item_Fin_Plan.GetChanges() == null &&
                ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void DeleteItem_Fin_Plan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Ссуды",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                while (dgItem_Fin_Plan.SelectedCells.Count > 0)
                {
                    (dgItem_Fin_Plan.SelectedCells[0].Item as DataRowView).Delete();
                    SaveItem_Fin_Plan();
                }
            }
            dgItem_Fin_Plan.Focus();
        }

        private void SaveItem_Fin_Plan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (LoanDataSet.Item_Fin_Plan.GetChanges() != null &&
                ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void SaveItem_Fin_Plan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgItem_Fin_Plan.CommitEdit(DataGridEditingUnit.Row, true);
            SaveItem_Fin_Plan();
        }

        void SaveItem_Fin_Plan()
        {
            OracleTransaction transact = Connect.CurConnect.BeginTransaction();
            try
            {
                //_ds.Tables["EMP_FL"].DefaultView.RowFilter = "LIST_FLIGHT_REM_ID IS NULL";
                //while (_ds.Tables["EMP_FL"].DefaultView.Count > 0)
                //{
                //    _ds.Tables["EMP_FL"].DefaultView[0]["LIST_FLIGHT_REM_ID"] = (dgList_Flight_Rem.SelectedItem as DataRowView)["LIST_FLIGHT_REM_ID"];
                //}
                _daItem_Fin_Plan.InsertCommand.Transaction = transact;
                _daItem_Fin_Plan.UpdateCommand.Transaction = transact;
                _daItem_Fin_Plan.DeleteCommand.Transaction = transact;
                _daItem_Fin_Plan.Update(LoanDataSet.Item_Fin_Plan);
                transact.Commit();
            }
            catch (Exception ex)
            {
                transact.Rollback();
                MessageBox.Show(ex.Message, "Ссуды - Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                LoanDataSet.Loan_Cost_Item.RejectChanges();
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private void CancelItem_Fin_Plan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (LoanDataSet.Item_Fin_Plan.GetChanges() != null)
                e.CanExecute = true;
        }

        private void CancelItem_Fin_Plan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить все несохраненные изменения?", "Ссуды", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                LoanDataSet.Item_Fin_Plan.RejectChanges();
        }

        DataRowView _currentRowView;
        private void dgLoan_Cost_Item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_currentRowView != (DataRowView)dgLoan_Cost_Item.SelectedItem)
            {
                _currentRowView = (DataRowView)dgLoan_Cost_Item.SelectedItem;
                //GetEmp_For_List();
            }
        }
    }
}
