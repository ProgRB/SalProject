using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Salary.Helpers;
using System.Collections.ObjectModel;
using LibrarySalary.ViewModel;
using System.Windows.Controls;
using System.ComponentModel;
using LibrarySalary.Helpers;

namespace Loan.Classes
{
    public class ViewTabLoanCollection : NotificationObject
    {
        static ObservableCollection<ViewTabBase> _openTabs;
        public static ObservableCollection<ViewTabBase> OpenTabs
        {
            get
            {
                if (_openTabs == null)
                    _openTabs = new ObservableCollection<ViewTabBase>();
                return _openTabs;
            }
        }
        ViewTabBase _selectedTab;
        public ViewTabBase SelectedTab
        {
            get
            {
                return _selectedTab;
            }
            set
            {
                _selectedTab = value;
                RaisePropertyChanged(() => SelectedTab);
                if (_selectedTab != null)
                    _selectedTab.ContentData.Focus();
            }
        }
        public void AddNewTab(string tabheader, UserControl content)
        {            
            ViewTabBase v = new ViewTabBase(tabheader, content);
            OpenTabs.Add(v);
            SelectedTab = v;
        }

        public static void CloseTab(ViewTabBase v)
        {
            CancelEventArgs e = new CancelEventArgs();
            v.ValidateClose(e);
            if (!e.Cancel)
            {
                OpenTabs.Remove(v);
            }
        }

        private static RelayCommand _closeTabCommand;
        public static RelayCommand CloseTabCommand
        {
            get
            {
                if (_closeTabCommand == null)
                    _closeTabCommand = new RelayCommand((e) => { CloseTab((ViewTabBase)e); });
                return _closeTabCommand;
            }
        }

        public bool ContainsTab(string tabheader)
        {
            object item = OpenTabs.Where(i => i.HeaderText.ToUpper() == tabheader.ToUpper()).FirstOrDefault();
            if (item != null)
            {
                SelectedTab = (ViewTabBase)item;
                return true;
            }
            return false;
        }
    }
}
