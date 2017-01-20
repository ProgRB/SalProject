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
    /// Interaction logic for Refinancing_Rate_Viewer.xaml
    /// </summary>
    public partial class Refinancing_Rate_Viewer : UserControl
    {
        private static OracleDataAdapter _daRefinancing_Rate = new OracleDataAdapter();
        public Refinancing_Rate_Viewer()
        {
            InitializeComponent();
            //GetRefinancing_Rate();
            dgRefinancing_Rate.DataContext = LoanDataSet.Refinancing_Rate.DefaultView;
        }

        static Refinancing_Rate_Viewer()
        {
            // Insert
            _daRefinancing_Rate.InsertCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.REFINANCING_RATE_UPDATE(:REFINANCING_RATE_ID, :REFINANCING_RATE_DATE, :REFINANCING_RATE);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daRefinancing_Rate.InsertCommand.BindByName = true;
            _daRefinancing_Rate.InsertCommand.Parameters.Add("REFINANCING_RATE_ID", OracleDbType.Decimal, 0, "REFINANCING_RATE_ID").Direction = 
                ParameterDirection.InputOutput;
            _daRefinancing_Rate.InsertCommand.Parameters["REFINANCING_RATE_ID"].DbType = DbType.Decimal;
            _daRefinancing_Rate.InsertCommand.Parameters.Add("REFINANCING_RATE_DATE", OracleDbType.Date, 0, "REFINANCING_RATE_DATE");
            _daRefinancing_Rate.InsertCommand.Parameters.Add("REFINANCING_RATE", OracleDbType.Decimal, 0, "REFINANCING_RATE");
            // Update
            _daRefinancing_Rate.UpdateCommand = _daRefinancing_Rate.InsertCommand;
            // Delete
            _daRefinancing_Rate.DeleteCommand = new OracleCommand(string.Format(
                @"BEGIN
                    {0}.LOAN_DML_PKG.REFINANCING_RATE_DELETE(:REFINANCING_RATE_ID);
                END;", Connect.SchemaSalary), Connect.CurConnect);
            _daRefinancing_Rate.DeleteCommand.BindByName = true;
            _daRefinancing_Rate.DeleteCommand.Parameters.Add("REFINANCING_RATE_ID", OracleDbType.Decimal, 0, "REFINANCING_RATE_ID");
        }

        void GetRefinancing_Rate()
        {
            LoanDataSet.Refinancing_Rate.Clear();
            _daRefinancing_Rate.Fill(LoanDataSet.Refinancing_Rate);
        }

        private void dg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ((DataGrid)sender).CommitEdit();
            ((DataGrid)sender).BeginEdit();
        }

        private void AddRefinancing_Rate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void AddRefinancing_Rate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView newRow = LoanDataSet.Refinancing_Rate.DefaultView.AddNew();
            LoanDataSet.Refinancing_Rate.Rows.InsertAt(newRow.Row, 0);
            dgRefinancing_Rate.SelectedItem = newRow;
        }

        private void DeleteRefinancing_Rate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgRefinancing_Rate != null && dgRefinancing_Rate.SelectedCells.Count > 0 &&
                ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void DeleteRefinancing_Rate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Ссуды",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                while (dgRefinancing_Rate.SelectedCells.Count > 0)
                {
                    (dgRefinancing_Rate.SelectedCells[0].Item as DataRowView).Delete();
                    SaveRefinancing_Rate();
                }
            }
            dgRefinancing_Rate.Focus();
        }

        private void SaveRefinancing_Rate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (LoanDataSet.Refinancing_Rate.GetChanges() != null &&
                ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void SaveRefinancing_Rate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgRefinancing_Rate.CommitEdit(DataGridEditingUnit.Row, true);
            SaveRefinancing_Rate();
        }

        void SaveRefinancing_Rate()
        {
            OracleTransaction transact = Connect.CurConnect.BeginTransaction();
            try
            {
                _daRefinancing_Rate.InsertCommand.Transaction = transact;
                _daRefinancing_Rate.UpdateCommand.Transaction = transact;
                _daRefinancing_Rate.DeleteCommand.Transaction = transact;
                _daRefinancing_Rate.Update(LoanDataSet.Refinancing_Rate);
                transact.Commit();
            }
            catch (Exception ex)
            {
                transact.Rollback();
                MessageBox.Show(ex.Message, "Ссуды - Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                LoanDataSet.Refinancing_Rate.RejectChanges();
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private void CancelRefinancing_Rate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (LoanDataSet.Refinancing_Rate.GetChanges() != null)
                e.CanExecute = true;
        }

        private void CancelRefinancing_Rate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить все несохраненные изменения?", "Ссуды", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                LoanDataSet.Refinancing_Rate.RejectChanges();
        }
    }
}
