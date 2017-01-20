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
using Salary;

namespace Loan
{
    /// <summary>
    /// Interaction logic for Purpose_Loan_Viewer.xaml
    /// </summary>
    public partial class Purpose_Loan_Viewer : UserControl
    {
        private static OracleDataAdapter _daPurpose_Loan = new OracleDataAdapter();
        public Purpose_Loan_Viewer()
        {
            InitializeComponent();
            //GetPurpose_Loan();
            dgPurpose_Loan.DataContext = LoanMain_Viewer.Purpose_Loan.DefaultView;
        }

        static Purpose_Loan_Viewer()
        {
            // Select
            //_daPurpose_Loan = new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/SelectTypeLoan.sql"), Connect.SchemaSalary), Connect.CurConnect);
            //_daPurpose_Loan.SelectCommand.BindByName = true;

            // Insert
            _daPurpose_Loan.InsertCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.PURPOSE_LOAN_UPDATE(:PURPOSE_LOAN_ID, :PURPOSE_LOAN_NAME, :PURPOSE_LOAN_CODE);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daPurpose_Loan.InsertCommand.BindByName = true;
            _daPurpose_Loan.InsertCommand.Parameters.Add("PURPOSE_LOAN_ID", OracleDbType.Decimal, 0, "PURPOSE_LOAN_ID").Direction = 
                ParameterDirection.InputOutput;
            _daPurpose_Loan.InsertCommand.Parameters["PURPOSE_LOAN_ID"].DbType = DbType.Decimal;
            _daPurpose_Loan.InsertCommand.Parameters.Add("PURPOSE_LOAN_NAME", OracleDbType.Varchar2, 0, "PURPOSE_LOAN_NAME");
            _daPurpose_Loan.InsertCommand.Parameters.Add("PURPOSE_LOAN_CODE", OracleDbType.Varchar2, 0, "PURPOSE_LOAN_CODE");
            // Update
            _daPurpose_Loan.UpdateCommand = _daPurpose_Loan.InsertCommand;
            // Delete
            _daPurpose_Loan.DeleteCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.PURPOSE_LOAN_DELETE(:PURPOSE_LOAN_ID);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daPurpose_Loan.DeleteCommand.BindByName = true;
            _daPurpose_Loan.DeleteCommand.Parameters.Add("PURPOSE_LOAN_ID", OracleDbType.Decimal, 0, "PURPOSE_LOAN_ID");
        }

        void GetPurpose_Loan()
        {
            LoanMain_Viewer.Purpose_Loan.Clear();
            _daPurpose_Loan.Fill(LoanMain_Viewer.Purpose_Loan);
        }

        private void dg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ((DataGrid)sender).CommitEdit(DataGridEditingUnit.Row, true);
            ((DataGrid)sender).BeginEdit();
        }

        private void AddPurpose_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void AddPurpose_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView newRow = LoanMain_Viewer.Purpose_Loan.DefaultView.AddNew();
            LoanMain_Viewer.Purpose_Loan.Rows.Add(newRow.Row);
            dgPurpose_Loan.SelectedItem = newRow;
        }

        private void DeletePurpose_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgPurpose_Loan != null && dgPurpose_Loan.SelectedCells.Count > 0 &&
                ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void DeletePurpose_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Ссуды",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                while (dgPurpose_Loan.SelectedCells.Count > 0)
                {
                    (dgPurpose_Loan.SelectedCells[0].Item as DataRowView).Delete();
                    SavePurpose_Loan();
                }
            }
            dgPurpose_Loan.Focus();
        }

        private void SavePurpose_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (LoanMain_Viewer.Purpose_Loan.GetChanges() != null &&
                ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void SavePurpose_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgPurpose_Loan.CommitEdit(DataGridEditingUnit.Row, true);
            SavePurpose_Loan();
        }

        void SavePurpose_Loan()
        {
            OracleTransaction transact = Connect.CurConnect.BeginTransaction();
            try
            {
                _daPurpose_Loan.InsertCommand.Transaction = transact;
                _daPurpose_Loan.UpdateCommand.Transaction = transact;
                _daPurpose_Loan.DeleteCommand.Transaction = transact;
                _daPurpose_Loan.Update(LoanMain_Viewer.Purpose_Loan);
                transact.Commit();
            }
            catch (Exception ex)
            {
                transact.Rollback();
                MessageBox.Show(ex.Message, "Ссуды - Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                LoanMain_Viewer.Purpose_Loan.RejectChanges();
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private void CancelPurpose_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (LoanMain_Viewer.Purpose_Loan.GetChanges() != null)
                e.CanExecute = true;
        }

        private void CancelPurpose_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить все несохраненные изменения?", "Ссуды", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                LoanMain_Viewer.Purpose_Loan.RejectChanges();
        }
    }
}
