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
using System.ComponentModel;
using Oracle.DataAccess.Client;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for TypeBankEditor.xaml
    /// </summary>
    public partial class TypeBankViewer : UserControl, INotifyPropertyChanged
    {
        DataSet ds = new DataSet();
        OracleDataAdapter odaType_Bank;
        public TypeBankViewer()
        {
            odaType_Bank = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectTypeBankView.sql"), Connect.CurConnect);
            odaType_Bank.TableMappings.Add("Table", "TYPE_BANK");

            odaType_Bank.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.TYPE_BANK_DELETE(:p_TYPE_BANK_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaType_Bank.DeleteCommand.BindByName = true;
            odaType_Bank.DeleteCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, null, ParameterDirection.InputOutput);
            odaType_Bank.Fill(ds);
            InitializeComponent();
            this.DataContext = this;
        }

        

        DataView _typeBankSource;
        public DataView TypeBankSource
        {
            get
            {
                if (_typeBankSource == null && ds != null && ds.Tables.Contains("TYPE_BANK"))
                {
                    UpdateTypeBank();
                    _typeBankSource = new DataView(ds.Tables["TYPE_BANK"], "", "BANK_NAME", DataViewRowState.CurrentRows);
                }
                return _typeBankSource;
            }
        }

        DataRowView _selectedTypeBank;
        public DataRowView SelectedTypeBank
        {
            get
            {
                return _selectedTypeBank;
            }
            set
            {
                _selectedTypeBank = value;
            }
        }

        private void add_canExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TypeBankEditor f = new TypeBankEditor(null);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog()==true)
            {
                UpdateTypeBank();
            }
        }

        private void delete_canExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && SelectedTypeBank != null;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный банк без возможности восстановления?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OracleTransaction tr = Connect.CurConnect.BeginTransaction();
                try
                {
                    odaType_Bank.DeleteCommand.Parameters["P_TYPE_BANK_ID"].Value = SelectedTypeBank["TYPE_BANK_ID"];
                    odaType_Bank.DeleteCommand.ExecuteNonQuery();
                    tr.Commit();
                    UpdateTypeBank();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.GetFormattedException(), "Зарплата Предприятия. Ошибка удаления");
                }
            }
        }

        private void Save_canExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && ds != null && ds.HasChanges();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void UpdateTypeBank()
        { 
            if (ds.Tables.Contains("TYPE_BANK"))
                ds.Tables["TYPE_BANK"].Rows.Clear();
            try
            {
                odaType_Bank.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
            }
            OnPropertyChanged("DistinctBankNameSource");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateTypeBank();
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TypeBankEditor f = new TypeBankEditor(SelectedTypeBank.Row.Field2<Decimal>("TYPE_BANK_ID"));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                UpdateTypeBank();
            }
        }
    }
}
