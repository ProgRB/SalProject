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
using System.Collections;
using EntityGenerator;
using Salary.Helpers;
using System.Runtime.Serialization;
using Salary.Model;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for RetentionEditor.xaml
    /// </summary>
    public partial class RetentionEditor : Window
    {
        public RetentionEditor(object transfer_id, object retention_id, RetentionGroup rg = RetentionGroup.UsualRetention)
        {
            _model = new RetentionModel((decimal?)transfer_id, (decimal?)retention_id);
            InitializeComponent();
            //gbEmpData.DataContext = Model;
        }

        public RetentionModel Model
        {
            get
            {
                return _model;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && Model!=null && Model.HasChanges && dpMain != null && string.IsNullOrEmpty(Model.Error);
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgClientAccounts.CommitEdit(DataGridEditingUnit.Cell, true);
            if (Model.Save())
            {
                this.DialogResult = true;
                Close();
            }
        }

        private void AddSalDoc_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }

        private void AddSalDoc_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryDocEditor f = new SalaryDocEditor(Model.TRANSFER_ID);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateSalDoc();
            }
        }

        private void EditSalDoc_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && Model!=null && cbCodeDoc.SelectedValue != null;
        }

        private void EditSalDoc_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryDocEditor f = new SalaryDocEditor(Model.TRANSFER_ID, cbCodeDoc.SelectedValue);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateSalDoc();
            }
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            ppDateCalc.IsOpen = true;
        }

        private void AddClientRetent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model!=null && Model.TRANSFER_ID!=null;
        }

        private void AddClientRetent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddClientRetentRelation();
        }

        private void DeleteClientAccount_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model!=null && Model.CurrentAccountRelation!=null;
        }

        private void DeleteClient_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.DeleteCurrentAccountRelation();
        }

        private void AddClientAccount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ClientAccountEditor f = new ClientAccountEditor(Model.TRANSFER_ID);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateClientAccounts();
                Model.CurrentAccountRelation.ClientAccountID = (decimal?) f.SelectedClientAccountID;
            }
        }

        private void EditClientAccount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ClientAccountEditor f = new ClientAccountEditor(Model.TRANSFER_ID, e.Parameter);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.UpdateClientAccounts();
                Model.CurrentAccountRelation.ClientAccountID = (decimal?)f.SelectedClientAccountID;
            }
        }

        private void EditClient_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void EditRetentionTransfer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.TRANSFER_ID==null;
        }

        private void EditRetentionTransfer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EmpFinder f = new EmpFinder();
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.TRANSFER_ID = f.SelectedItem.Row.Field2<decimal?>("TRANSFER_ID");
                Model.EmpName = string.Format("{0} {1} {1}", f.SelectedItem["EMP_LAST_NAME"], f.SelectedItem["EMP_FIRST_NAME"], f.SelectedItem["EMP_MIDDLE_NAME"]);
                Model.PerNum = f.SelectedItem["PER_NUM"].ToString();
            }
        }

        private bool isManualEditCommit = false;
        private RetentionModel _model;
        private void dgClientAccounts_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!isManualEditCommit)
            {
                isManualEditCommit = true;
                DataGrid grid = sender as DataGrid;
                bool fl = false;
                try
                {
                    fl = grid.CommitEdit(DataGridEditingUnit.Row, true);
                }
                catch
                { }
                finally
                {
                   /* if (!fl)
                        grid.CancelEdit();*/
                }
                isManualEditCommit = false;
            }
            //dgClientAccounts.BindingGroup.ValidateWithoutUpdate();
        }

        private void dgClientAccounts_CurrentCellChanged(object sender, EventArgs e)
        {
            /*(sender as DataGrid).BeginEdit();
            dgClientAccounts.BindingGroup.ValidateWithoutUpdate();*/
        }

    }

    public enum RetentionGroup
    {
        Taxes=1,
        Dues=2,
        UsualRetention=3
    }


    public class SourceToDisplayConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[0] != DependencyProperty.UnsetValue && values[1] != DependencyProperty.UnsetValue)
            {
                DataRow r;
                DataTable t = null;
                if (values[1] is ICollectionView)
                    t = ((values[1] as ICollectionView).SourceCollection as DataView).Table;
                else if (values[1] is DataView)
                    t = (values[1] as DataView).Table;
                else throw new Exception("Конвертеру SourceToDisplayConverter требуется ICollectionView  или DataView источник данных");
                if ((r = t.Rows.Find(values[0])) !=null)
                {
                    return r[parameter.ToString()];
                }
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NotIntersectPeriod : ValidationRule
    {
        public string DateStartField
        {
            get;
            set;
        }
        public string DateEndField
        {
            get;
            set;
        }
        public string ErrorMessage
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            BindingGroup r = value as BindingGroup;
            if (r != null && r.Items.Count>0)
            {
                DataView dv = ((RetentionModel)r.Items[0]).ClientRelationTable.DefaultView;
                var items = dv.OfType<DataRowView>().OrderBy(t => new Tuple<DateTime?, DateTime?>(t.Row.Field<DateTime?>(DateStartField) ?? DateTime.MinValue, t.Row.Field<DateTime?>(DateEndField) ?? DateTime.MaxValue)).
                    Select(t=> new { DateStart = t.Row.Field<DateTime?>(DateStartField) ?? DateTime.MinValue, DateEnd = t.Row.Field<DateTime?>(DateEndField) ?? DateTime.MaxValue }).ToList();
                for (int i = 0; i<items.Count;++i)
                    if (i>0 && items[i].DateStart<items[i-1].DateEnd)
                        return new ValidationResult(false, ErrorMessage);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class LessCompareRule : ValidationRule
    {
        public string FirstValueField
        {
            get;
            set;
        }
        public string SecondValueField
        {
            get;
            set;
        }
        public string ErrorMessage
        {
            get;
            set;
        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ClientRetentRelationModel val1 = ((value as BindingGroup).Items[0] as ClientRetentRelationModel);
            if (val1 == null) return ValidationResult.ValidResult;
            DataRow r = val1.DataRow;
            if (r.RowState == DataRowState.Deleted || r.RowState == DataRowState.Detached)
                return ValidationResult.ValidResult;
            Type val = r[FirstValueField].GetType();
            bool res = false;
            if (val == typeof(Decimal))
                res = (r.Field2<Decimal?>(FirstValueField) ?? Decimal.MinValue) < (r.Field2<Decimal?>(SecondValueField) ?? Decimal.MaxValue);
            else 
                if (val == typeof(DateTime))
                res = (r.Field2<DateTime?>(FirstValueField) ?? DateTime.MinValue) < (r.Field2<DateTime?>(SecondValueField) ?? DateTime.MaxValue);
            else
                if (val == typeof(string))
                res = string.Compare(r.Field2<string>(FirstValueField), r.Field2<string>(SecondValueField))<0;
            if (res) 
                return ValidationResult.ValidResult;
            else
                return new ValidationResult(false, ErrorMessage);
                
        }
    }

    public class DateToRemConveter : IMultiValueConverter
    {
        public string SumField
        {
            get;
            set;
        }
        public string CompareField
        {
            get;
            set;
        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0]!=null && values[2]!=null && values[2]!=DependencyProperty.UnsetValue)
            {
                
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ClientAccountTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EmpAccountTemplate
        {
            get;
            set;
        }
        public DataTemplate InsuranceAccountTemplate
        {
            get;
            set;
        }
        public DataTemplate AlimonyAccountTemplate
        {
            get;
            set;
        }
        public DataTemplate ListAccountTemplate
        {
            get;
            set;
        }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                DataRowView r = item as DataRowView;
                if (r["TYPE_ACCOUNT_ID"] == null || r["TYPE_ACCOUNT_ID"] == DBNull.Value)
                    return new DataTemplate();
                switch (r["TYPE_ACCOUNT_ID"].ToString())
                {
                    case "1": return EmpAccountTemplate; break;
                    case "2": return ListAccountTemplate; break;
                    case "3": return AlimonyAccountTemplate; break;
                    case "4": return AlimonyAccountTemplate; break;
                    case "5": return EmpAccountTemplate; break;
                    case "6": return InsuranceAccountTemplate; break;
                    default: return new DataTemplate(); break;
                }
            }
            return null;
        }
    }

}
