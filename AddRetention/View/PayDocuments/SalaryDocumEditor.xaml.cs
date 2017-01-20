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
using System.Data;
using Oracle.DataAccess.Client;
using System.ComponentModel;
using Salary.Model;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for SalaryDocumEditor.xaml
    /// </summary>
    public partial class SalaryDocumEditor : Window
    {
        public SalaryDocumEditor(decimal? _subdiv_id, decimal? p_salary_docum_id=null, decimal? transfer_id=null, DateTime? selectedDate=null)
        {
            InitializeComponent();
            _model = new SalaryDocumModel(_subdiv_id, transfer_id, p_salary_docum_id, selectedDate);
            this.DataContext = Model;
        }

        SalaryDocumModel _model;
        public SalaryDocumModel Model
        {
            get
            {
                return _model;
            }
        }


        /// <summary>
        /// Сохранение докмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgPaymentValue.CommitEdit(DataGridEditingUnit.Row, true);
            dgPayChange.CommitEdit(DataGridEditingUnit.Row, true);
            dgPeriodDoc.CommitEdit(DataGridEditingUnit.Row, true);
            if (Model.Save())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void Save_CanExected(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model!=null && Model.HasChanges && string.IsNullOrWhiteSpace(Model.Error);
        }
    
        private void EditTransfer_CanExected(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model!=null && Model.IsNew;
        }

        /// <summary>
        /// Редактирование сотрудника в документе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditSalaryDocTransfer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EmpFinder f = new EmpFinder();
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.TransferID = f.SelectedItem.Row.Field2<Decimal?>("TRANSFER_ID");
                Model.FIO = string.Format("{0} {1} {1}", f.SelectedItem["EMP_LAST_NAME"], f.SelectedItem["EMP_FIRST_NAME"], f.SelectedItem["EMP_MIDDLE_NAME"]);
                Model.PerNum = f.SelectedItem["PER_NUM"].ToString();
            }
        }

        private void ClearDocum_Click(object sender, RoutedEventArgs e)
        {
            Model.RegDocID = null;
        }

        private void ClearRelatedDocum_Click(object sender, RoutedEventArgs e)
        {
            Model.RelatedDocumID = null;
        }
    }
}
