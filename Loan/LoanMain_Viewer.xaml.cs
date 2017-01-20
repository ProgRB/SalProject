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
using System.Collections.ObjectModel;
using LibrarySalary.ViewModel;
using System.ComponentModel;
using LibrarySalary.Helpers;
using Loan.Classes;
using System.Data;
using Oracle.DataAccess.Client;
using Salary;

namespace Loan
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class LoanMain_Viewer : UserControl
    {
        private static DataSet _dsDictionary = new DataSet();

        public static DataSet DsDictionary
        {
            get { return LoanMain_Viewer._dsDictionary; }
            set { LoanMain_Viewer._dsDictionary = value; }
        }

        public static DataTable Type_Loan
        {
            get { return _dsDictionary.Tables["TYPE_LOAN"]; }
        }

        public static DataTable Purpose_Loan
        {
            get { return _dsDictionary.Tables["PURPOSE_LOAN"]; }
        }

        public ViewTabLoanCollection OpenTabs
        {
            get
            {
                return (ViewTabLoanCollection)this.FindResource("OpenTabs");
            }
        }

        public LoanMain_Viewer()
        {
            InitializeComponent();
            if (ControlRoles.GetState(((RoutedUICommand)miViewLoan.Command).Name.ToUpper())) 
                ViewLoan_Execute(null, null);
        }

        static LoanMain_Viewer()
        {
            // TYPE_LOAN
            _dsDictionary.Tables.Add("TYPE_LOAN");
            new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/SelectTypeLoan.sql"), Connect.SchemaSalary), Connect.CurConnect).
                Fill(_dsDictionary.Tables["TYPE_LOAN"]);
            // PURPOSE_LOAN
            _dsDictionary.Tables.Add("PURPOSE_LOAN");
            new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/SelectPurposeLoan.sql"), Connect.SchemaSalary), Connect.CurConnect).
                Fill(_dsDictionary.Tables["PURPOSE_LOAN"]);
        }

        private void LoanMenuItem_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ControlRoles.GetState(((RoutedUICommand)e.Command).Name.ToUpper()))
                e.CanExecute = true;
        }

        private void ViewType_Loan_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            if (!OpenTabs.ContainsTab("Типы ссуд"))
                OpenTabs.AddNewTab("Типы ссуд", new Type_Loan_Viewer());
        }

        private void ViewPurpose_Loan_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            if (!OpenTabs.ContainsTab("Цели получения ссуды"))
                OpenTabs.AddNewTab("Цели получения ссуды", new Purpose_Loan_Viewer());
        }

        private void ViewLoan_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            if (!OpenTabs.ContainsTab("Ссуды"))
                OpenTabs.AddNewTab("Ссуды", new Loan_Viewer());
        }
    }
}
