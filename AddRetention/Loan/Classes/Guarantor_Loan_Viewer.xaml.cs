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
using LibrarySalary.Helpers;
using System.Data;
using Oracle.DataAccess.Client;
using System.ComponentModel;

namespace Salary.Loan.Classes
{
    /// <summary>
    /// Interaction logic for Guarantor_Loan_Viewer.xaml
    /// </summary>
    public partial class Guarantor_Loan_Viewer : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private decimal _loan_ID;

        public decimal Loan_ID
        {
            get { return _loan_ID; }
            set 
            {
                if (value != this._loan_ID)
                {
                    this._loan_ID = value;
                    OnPropertyChanged("Loan_ID");
                }
            }
        }

        private Visibility _visibilityButton = Visibility.Visible;

        public Visibility VisibilityButton
        {
            get { return _visibilityButton; }
            set 
            {
                _visibilityButton = value;
                tbtButton.Visibility = _visibilityButton;
            }
        }

        private static RoutedUICommand _addGuarantor_Loan, _editGuarantor_Loan, _deleteGuarantor_Loan, _saveGuarantor_Loan;
        DataTable _dtGuarantor_Loan;
        private OracleDataAdapter _daGuarantor_Loan = new OracleDataAdapter();
        public Guarantor_Loan_Viewer()
        {
            InitializeComponent();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.PropertyChanged += new PropertyChangedEventHandler(Guarantor_Loan_Viewer_PropertyChanged);

                _dtGuarantor_Loan = new DataTable();

                // Select
                _daGuarantor_Loan.SelectCommand = new OracleCommand(string.Format(Queries.GetQuery("Loan/SelectGuarantor_Loan.sql"),
                    Connect.SchemaSalary, Connect.SchemaApstaff), Connect.CurConnect);
                _daGuarantor_Loan.SelectCommand.BindByName = true;
                _daGuarantor_Loan.SelectCommand.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal);
                // Insert
                _daGuarantor_Loan.InsertCommand = new OracleCommand(string.Format(
                    @"BEGIN
                        {0}.LOAN_DML_PKG.GUARANTOR_LOAN_UPDATE(:GUARANTOR_LOAN_ID, :TRANSFER_ID, :LOAN_ID, :GUARANTOR_CONTRACT_NUMBER);
                    END;", Connect.SchemaSalary), Connect.CurConnect);
                _daGuarantor_Loan.InsertCommand.BindByName = true;
                _daGuarantor_Loan.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
                _daGuarantor_Loan.InsertCommand.Parameters.Add("GUARANTOR_LOAN_ID", OracleDbType.Decimal, 0, "GUARANTOR_LOAN_ID");
                _daGuarantor_Loan.InsertCommand.Parameters.Add("TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
                _daGuarantor_Loan.InsertCommand.Parameters.Add("LOAN_ID", OracleDbType.Decimal, 0, "LOAN_ID");
                _daGuarantor_Loan.InsertCommand.Parameters.Add("GUARANTOR_CONTRACT_NUMBER", OracleDbType.Varchar2, 0, "GUARANTOR_CONTRACT_NUMBER");
                // Update
                _daGuarantor_Loan.UpdateCommand = _daGuarantor_Loan.InsertCommand;
                // Delete
                _daGuarantor_Loan.DeleteCommand = new OracleCommand(string.Format(
                    @"BEGIN
                    {0}.LOAN_DML_PKG.GUARANTOR_LOAN_DELETE(:GUARANTOR_LOAN_ID);
                END;", Connect.SchemaSalary), Connect.CurConnect);
                _daGuarantor_Loan.DeleteCommand.BindByName = true;
                _daGuarantor_Loan.DeleteCommand.Parameters.Add("GUARANTOR_LOAN_ID", OracleDbType.Decimal, 0, "GUARANTOR_LOAN_ID");

                dgGuarantor_Loan.DataContext = _dtGuarantor_Loan.DefaultView;
            }
        }

        void Guarantor_Loan_Viewer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Loan_ID")
            {
                GetGuarantor_Loan(_loan_ID);
            }
        }
        
        void GetGuarantor_Loan(decimal loan_ID)
        {
            dgGuarantor_Loan.DataContext = null;
            _dtGuarantor_Loan.Clear();
            _daGuarantor_Loan.SelectCommand.Parameters["p_LOAN_ID"].Value = loan_ID;
            _daGuarantor_Loan.Fill(_dtGuarantor_Loan);
            dgGuarantor_Loan.DataContext = _dtGuarantor_Loan.DefaultView;
        }

        static Guarantor_Loan_Viewer()
        {
            _addGuarantor_Loan = new RoutedUICommand("Добавить поручителя", "AddGuarantor_Loan", typeof(Guarantor_Loan_Viewer));
            _editGuarantor_Loan = new RoutedUICommand("Редактировать поручителя", "EditGuarantor_Loan", typeof(Guarantor_Loan_Viewer));
            _deleteGuarantor_Loan = new RoutedUICommand("Удалить поручителя", "DeleteGuarantor_Loan", typeof(Guarantor_Loan_Viewer));
            _saveGuarantor_Loan = new RoutedUICommand("Сохранить изменения", "SaveGuarantor_Loan", typeof(Guarantor_Loan_Viewer));
        }

        public static RoutedUICommand AddGuarantor_Loan
        {
            get { return Guarantor_Loan_Viewer._addGuarantor_Loan; }
            set { Guarantor_Loan_Viewer._addGuarantor_Loan = value; }
        }

        public static RoutedUICommand EditGuarantor_Loan
        {
            get { return Guarantor_Loan_Viewer._editGuarantor_Loan; }
            set { Guarantor_Loan_Viewer._editGuarantor_Loan = value; }
        }

        public static RoutedUICommand DeleteGuarantor_Loan
        {
            get { return Guarantor_Loan_Viewer._deleteGuarantor_Loan; }
            set { Guarantor_Loan_Viewer._deleteGuarantor_Loan = value; }
        }

        public static RoutedUICommand SaveGuarantor_Loan
        {
            get { return Guarantor_Loan_Viewer._saveGuarantor_Loan; }
            set { Guarantor_Loan_Viewer._saveGuarantor_Loan = value; }
        }

        private void AddGuarantor_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()) &&
                _loan_ID != null)
                e.CanExecute = true;
        }

        private void AddGuarantor_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView newRow = _dtGuarantor_Loan.DefaultView.AddNew();
            _dtGuarantor_Loan.Rows.Add(newRow.Row);
            dgGuarantor_Loan.SelectedItem = newRow;
        }

        private void DeleteGuarantor_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgGuarantor_Loan != null && dgGuarantor_Loan.SelectedCells.Count > 0 &&
                ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void DeleteGuarantor_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Ссуды",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                while (dgGuarantor_Loan.SelectedCells.Count > 0)
                {
                    (dgGuarantor_Loan.SelectedCells[0].Item as DataRowView).Delete();
                    //SaveGuarantor_Loan();
                }
            }
            dgGuarantor_Loan.Focus();
        }

        private void EditGuarantor_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        /*void SaveGuarantor_Loan()
        {
            OracleTransaction transact = Connect.CurConnect.BeginTransaction();
            try
            {
                _daGuarantor_Loan.InsertCommand.Transaction = transact;
                _daGuarantor_Loan.UpdateCommand.Transaction = transact;
                _daGuarantor_Loan.DeleteCommand.Transaction = transact;
                _daGuarantor_Loan.Update(_dtGuarantor_Loan);
                transact.Commit();
            }
            catch (Exception ex)
            {
                transact.Rollback();
                MessageBox.Show(ex.Message, "Ссуды: Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                _dtGuarantor_Loan.RejectChanges();
            }
            CommandManager.InvalidateRequerySuggested();
        }*/

    }
}
