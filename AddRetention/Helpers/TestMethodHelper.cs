using LibrarySalary.ViewModel;
using Salary.View.Taxes;
using Salary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Salary.Helpers
{
    class TestMethodHelper
    {
        public void RunTest()
        {
            try
            {
                AppCommands.OpenViewTaxes.Execute(null, null);
                var p = App.Current.FindResource("OpenTabs");
                if (p != null)
                {
                    ViewTabCollection v = p as ViewTabCollection;
                    var t = ViewTabCollection.OpenTabs.Where(r => r.ContentData.GetType() == typeof(TaxCompanyViewer)).FirstOrDefault();
                    if (t != null)
                    {
                        var u = t.ContentData as TaxCompanyViewer;
                        u.Model.SelectedDate = new DateTime(2016, 9, 1);
                    }
                }
            }
            catch 
            { }
        }
    }
}
