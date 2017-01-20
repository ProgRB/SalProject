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
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Runtime.Remoting;

namespace Salary.Reports
{
    /// <summary>
    /// Interaction logic for ViewReportWindow.xaml
    /// </summary>
    public partial class ViewReportWindow : Window
    {
        public ViewReportWindow()
        {
            InitializeComponent();
        }
        
        public void ShowReport(DependencyObject sender, string Title, string path, DataTable table, List<ReportParameter> r, System.Drawing.Printing.Duplex duplex = System.Drawing.Printing.Duplex.Simplex)
        {
            ViewReportWindow f = new ViewReportWindow();
            //f.repViewer.LocalReport.LoadReportDefinition(File.OpenRead(Connect.CurrentAppPath + @"\Reports\" + path));
            f.repViewer.LocalReport.ReportPath = "Reports/" + path;
            f.repViewer.ProcessingMode = ProcessingMode.Local;
            f.repViewer.PrinterSettings.Duplex = duplex;
            f.repViewer.LocalReport.DataSources.Clear();
            if (table!=null)
                f.repViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", table));
            if (r!=null)
                f.repViewer.LocalReport.SetParameters(r);
            f.repViewer.RefreshReport();
            f.repViewer.SetDisplayMode(DisplayMode.PrintLayout);
            f.Owner = Window.GetWindow(sender);
            f.Title += Title;
            f.Show();
        }

        public void ShowReport(DependencyObject sender, string Title, string path, DataTable[] tables, List<ReportParameter> r, System.Drawing.Printing.Duplex duplex = System.Drawing.Printing.Duplex.Simplex)
        {
            ViewReportWindow f = new ViewReportWindow();
            f.repViewer.LocalReport.LoadReportDefinition(File.OpenRead(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\Reports\" + path));
            f.repViewer.PrinterSettings.Duplex = duplex;
            f.repViewer.LocalReport.DataSources.Clear();
            //f.repViewer.LocalReport.
            if (tables != null)
                for (int i=1;i<=tables.Length;++i)
                f.repViewer.LocalReport.DataSources.Add(new ReportDataSource(string.Format("DataSet{0}", i), tables[i-1]));
            if (r != null)
                f.repViewer.LocalReport.SetParameters(r);
            f.repViewer.RefreshReport();
            f.repViewer.SetDisplayMode(DisplayMode.PrintLayout);
            f.Owner = Window.GetWindow(sender);
            f.Title += Title;
            f.Show();
        }
    }

}
