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
using LibrarySalary.Helpers;
using Salary.Loan.Classes;
using System.Data.Linq.Mapping;

namespace Salary.Loan
{
    /// <summary>
    /// Interaction logic for Loan_Editor.xaml
    /// </summary>
    public partial class Guarantor_Loan_Editor : Window
    {
        DataRowView _drView;
        public Guarantor_Loan_Editor(DataRowView drView)
        {
            InitializeComponent();
            this.DataContext = drView;
            _drView = drView;
        }
        
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState((e.Command as RoutedUICommand).Name) &&
                _drView != null && _drView.DataView.Table.GetChanges() != null)
                e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void SelectGuarantor_Loan_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState((e.Command as RoutedUICommand).Name))
                e.CanExecute = true;
        }

        private void SelectGuarantor_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Find_Emp find_Emp = new Find_Emp(DateTime.Today);
            find_Emp.Owner = Window.GetWindow(this);
            if (find_Emp.ShowDialog() == true)
            {
                _drView["CODE_SUBDIV"] = find_Emp.Code_Subdiv;
                _drView["EMP_LAST_NAME"] = find_Emp.Last_Name;
                _drView["EMP_FIRST_NAME"] = find_Emp.First_Name;
                _drView["EMP_MIDDLE_NAME"] = find_Emp.Middle_Name;
                _drView["PER_NUM"] = find_Emp.Per_Num;
                _drView["TRANSFER_ID"] = find_Emp.Transfer_ID;
            }
        }

    }
}
