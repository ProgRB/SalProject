using LibrarySalary.Helpers;
using Salary.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Salary.View.Details
{
    /// <summary>
    /// Interaction logic for BrigageEditor.xaml
    /// </summary>
    public partial class BrigageEditor : UserControl
    {
        private BrigageViewModel _model;
        public BrigageEditor()
        {
            _model = new BrigageViewModel();
            InitializeComponent();
            DataContext = Model;
            Model.SubdivID = subdivSelector.SubdivView.OfType<DataRowView>().Select(r => r.Row.Field2<decimal?>("SUBDIV_ID")).FirstOrDefault();
            Model.LoadBrigages();
        }

        private void RefreshBrigages_Click(object sender, RoutedEventArgs e)
        {
            Model.LoadBrigages();
        }

        public BrigageViewModel Model
        {
            get
            {
                return _model;
            }
        }

        private void AddBrigage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model!=null && Model.SubdivID != null && Model.SubdivID != null;
        }

        private void AddBrigage_executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddNew();
        }

        private void DeleteBrigage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.CurrentBrigage != null;
        }

        private void DeleteBrigage_executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.DeleteBrigage();
        }

        private void SaveBrigage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && Model.HasChanges;
        }

        private void SaveBrigage_executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Save();
        }
    }
}
