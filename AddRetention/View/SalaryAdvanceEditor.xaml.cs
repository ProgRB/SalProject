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
using Salary.Model;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for SalaryAdvanceEditor.xaml
    /// </summary>
    public partial class SalaryAdvanceEditor : Window
    {
        public SalaryAdvanceEditor(decimal? salary_adv_id, decimal? transfer_id)
        {
            InitializeComponent();
            _model = new SalaryAdvance(salary_adv_id, transfer_id);
            this.DataContext = Model;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        SalaryAdvance _model;
        public SalaryAdvance Model
        {
            get
            {
                return _model;
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && string.IsNullOrEmpty(Model.Error);
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Model.Save())
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
