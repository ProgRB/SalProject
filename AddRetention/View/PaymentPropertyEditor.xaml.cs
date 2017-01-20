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
using Oracle.DataAccess.Client;
using System.Data;
using EntityGenerator;
using Salary.ViewModel;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for PaymentPropertyEditor.xaml
    /// </summary>
    public partial class PaymentPropertyEditor : UserControl
    {
        PaymentPropertyViewModel _model;
        public PaymentPropertyEditor()
        {   
            InitializeComponent();
            _model = new PaymentPropertyViewModel();
            this.DataContext = Model;
        }

        PaymentPropertyViewModel Model
        {
            get
            {
                return _model;
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.HasChanges;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Save();
        }

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddNew();
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.CurrentProperty != null;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранное свойство?", "Зарплата предприятия",  MessageBoxButton.YesNo,  MessageBoxImage.Question)== MessageBoxResult.Yes)
                Model.DeleteCurrent();
        }
    }

    public class PossTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NoneTemplate
        {
            get;
            set;
        }
        public DataTemplate ValueTemplate
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null && item is DataRowView)
            {
                if ((item as DataRowView).Row.Field2<Decimal?>("PROPERTY_TYPE_ID") == 3)
                    return ValueTemplate;
                else
                    return NoneTemplate;
            }
            return NoneTemplate;
        }
    }
}
