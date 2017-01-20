using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace ReportLibrary
{
    public partial class ReportPreview : Form
    {
        public ReportPreview()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        public void ShowReport(string ReportFileName)
        {
           // reportViewer1.LocalReport.LoadReportDefinition(new StreamReader(app
            reportViewer1.LocalReport.ReportEmbeddedResource = "ReportLibrary.RepSalByDegree_Orders.rdlc";
           // ReportDataSource r = new ReportDataSource("ds", 
        }
    }
}