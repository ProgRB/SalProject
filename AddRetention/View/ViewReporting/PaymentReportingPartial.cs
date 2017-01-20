using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using Oracle.DataAccess.Client;
using Salary.Helpers;
using System.Data;
using Microsoft.Reporting.WinForms;
using Salary.Reports;
using System.Windows;
using Salary.ViewModel;
using System.ComponentModel;
using System.Windows.Data;
using Salary.View;
using System.Collections;
using Salary.Interfaces;
using LibrarySalary.Helpers;

namespace Salary.ViewReporting
{
    public partial class  PaymentReporting: UserControl
    {
        public static DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems", typeof(IList), typeof(PaymentReporting));
        
        private PaymentReportingViewModel _model;
        public PaymentReporting()
        {
            InitializeComponent();
            _model = new PaymentReportingViewModel();
            this.DataContext = Model;
            if (!App.CloseNotification.IsEnabled)
                App.CloseNotification.IsEnabled = true;
        }
        public PaymentReportingViewModel Model
        {
            get
            {
                return _model;
            }
        }

        /// <summary>
        /// Выбранные строчки зарплаты для суммирования
        /// </summary>
        public IList SelectedItems
        {
            get
            {
                return (IList)GetValue(SelectedItemsProperty);
            }
            set
            {
                SetValue(SelectedItemsProperty, value);
            }
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            Model.FilterEmp.IncMonth();
        }
        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            Model.FilterEmp.DecMonth();
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                bgFilterGroup.UpdateSources();
        }

        private void RefreshSalary_Click(object sender, RoutedEventArgs e)
        {
            Model.UpdateSalaryView();
        }

        private void ListEmpDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            e.Handled = true;
            PerformCustomSort(e.Column);
        }

        private void PerformCustomSort(DataGridColumn column)
        {
            ListSortDirection direction = (column.SortDirection != ListSortDirection.Ascending) ?
                                ListSortDirection.Ascending : ListSortDirection.Descending;
            column.SortDirection = direction;
            ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(Model.EmpCollection);
            MySort mySort = new MySort(direction, column);
            lcv.CustomSort = mySort;
        }

        private void Expander_Exp(object sender, RoutedEventArgs e)
        {
            Expander ex = sender as Expander;
            ExpandStateSaver.states_exp[(ex.DataContext as CollectionViewGroup).Name.ToString()] = ex.IsExpanded;
        }

        private void Expander_Coll(object sender, RoutedEventArgs e)
        {
            try
            {
                Expander ex = sender as Expander;
                ExpandStateSaver.states_exp[(ex.DataContext as CollectionViewGroup).Name.ToString()] = ex.IsExpanded;
            }
            catch { };
        }

        private void btFilter_Click(object sender, RoutedEventArgs e)
        {
            Model.UpdateEmpList(bgFilterGroup);
        }
        private void Report_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void dgEmpPaySalary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedItems = null;
            this.SelectedItems = dgEmpPaySalary.SelectedItems;
        }
        
    }
    
}
