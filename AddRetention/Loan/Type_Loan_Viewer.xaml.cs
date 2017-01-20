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
    /// Interaction logic for Type_Loan_Viewer.xaml
    /// </summary>
    public partial class Type_Loan_Viewer : UserControl
    {
        private static OracleDataAdapter _daType_Loan = new OracleDataAdapter();
        public Type_Loan_Viewer()
        {
            InitializeComponent();
            //GetType_Loan();
            dgType_Loan.DataContext = LoanDataSet.Type_Loan.DefaultView;
        }

        static Type_Loan_Viewer()
        {
            // Select
            //_daType_Loan = new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/SelectTypeLoan.sql"), Connect.SchemaSalary), Connect.CurConnect);
            //_daType_Loan.SelectCommand.BindByName = true;

            // Insert
            _daType_Loan.InsertCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.TYPE_LOAN_UPDATE(:TYPE_LOAN_ID, :TYPE_LOAN_NAME, :SIGN_CHIEF_LOAN);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daType_Loan.InsertCommand.BindByName = true;
            _daType_Loan.InsertCommand.Parameters.Add("TYPE_LOAN_ID", OracleDbType.Decimal, 0, "TYPE_LOAN_ID").Direction = 
                ParameterDirection.InputOutput;
            _daType_Loan.InsertCommand.Parameters["TYPE_LOAN_ID"].DbType = DbType.Decimal;
            _daType_Loan.InsertCommand.Parameters.Add("TYPE_LOAN_NAME", OracleDbType.Varchar2, 0, "TYPE_LOAN_NAME");
            _daType_Loan.InsertCommand.Parameters.Add("SIGN_CHIEF_LOAN", OracleDbType.Int16, 0, "SIGN_CHIEF_LOAN");
            // Update
            _daType_Loan.UpdateCommand = _daType_Loan.InsertCommand;
            // Delete
            _daType_Loan.DeleteCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.TYPE_LOAN_DELETE(:TYPE_LOAN_ID);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daType_Loan.DeleteCommand.BindByName = true;
            _daType_Loan.DeleteCommand.Parameters.Add("TYPE_LOAN_ID", OracleDbType.Decimal, 0, "TYPE_LOAN_ID");
        }

        void GetType_Loan()
        {
            LoanDataSet.Type_Loan.Clear();
            _daType_Loan.Fill(LoanDataSet.Type_Loan);
        }

        private void dg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ((DataGrid)sender).CommitEdit(DataGridEditingUnit.Row, true);
            ((DataGrid)sender).BeginEdit();
        }

        private void AddType_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void AddType_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView newRow = LoanDataSet.Type_Loan.DefaultView.AddNew();
            LoanDataSet.Type_Loan.Rows.Add(newRow.Row);
            dgType_Loan.SelectedItem = newRow;
        }

        private void DeleteType_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgType_Loan != null && dgType_Loan.SelectedCells.Count > 0 &&
                ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void DeleteType_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Ссуды",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                while (dgType_Loan.SelectedCells.Count > 0)
                {
                    (dgType_Loan.SelectedCells[0].Item as DataRowView).Delete();
                    SaveType_Loan();
                }
            }
            dgType_Loan.Focus();
        }

        private void SaveType_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (LoanDataSet.Type_Loan.GetChanges() != null &&
                ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void SaveType_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgType_Loan.CommitEdit(DataGridEditingUnit.Row, true);
            SaveType_Loan();
        }

        void SaveType_Loan()
        {
            OracleTransaction transact = Connect.CurConnect.BeginTransaction();
            try
            {
                _daType_Loan.InsertCommand.Transaction = transact;
                _daType_Loan.UpdateCommand.Transaction = transact;
                _daType_Loan.DeleteCommand.Transaction = transact;
                _daType_Loan.Update(LoanDataSet.Type_Loan);
                transact.Commit();
            }
            catch (Exception ex)
            {
                transact.Rollback();
                MessageBox.Show(ex.Message, "Ссуды - Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                LoanDataSet.Type_Loan.RejectChanges();
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private void CancelType_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (LoanDataSet.Type_Loan.GetChanges() != null)
                e.CanExecute = true;
        }

        private void CancelType_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить все несохраненные изменения?", "Ссуды", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                LoanDataSet.Type_Loan.RejectChanges();
        }
    }
}
