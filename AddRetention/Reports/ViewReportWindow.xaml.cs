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
using System.Threading;
using System.ComponentModel;
using Salary.Helpers;
using System.Windows.Forms;
using SPrint = System.Drawing.Printing;
using System.Diagnostics;

namespace Salary.Reports
{
    /// <summary>
    /// Interaction logic for ViewReportWindow.xaml
    /// </summary>
    public partial class ViewReportWindow : Window
    {
        public ViewReportWindow(string[] enabledExtensions = null)
        {
            this.EnabledExportExtension = enabledExtensions ?? new string[] { "WORD", "PDF", "EXCEL" };
            InitializeComponent();
            this.repViewer.Load += new EventHandler(repViewer_Load);
            this.repViewer.ReportExport += new ExportEventHandler(repViewer_ReportExport);
        }
#region Технические вставки для подсказок и тп
        void repViewer_ReportExport(object sender, ReportExportEventArgs e)
        {
            e.Cancel = true;
        }

        public string[] EnabledExportExtension
        {
            get;
            set;
        }
        void repViewer_Load(object sender, EventArgs e)
        {
            /*try
            {*/
                ReportViewer rep = sender as ReportViewer;
                ToolStrip toolStrip = (ToolStrip)rep.Controls.Find("toolStrip1", true)[0];
                ToolStripButton firstPage = (ToolStripButton)toolStrip.Items.Find("firstPage", true)[0];
                firstPage.AutoToolTip = false;
                firstPage.ToolTipText = "На первую страницу";
                ToolStripButton previousPage = (ToolStripButton)toolStrip.Items.Find("previousPage", true)[0];
                previousPage.AutoToolTip = false;
                previousPage.ToolTipText = "Предыдущая страница";

                ToolStripTextBox currentPage = (ToolStripTextBox)toolStrip.Items.Find("currentPage", true)[0];
                currentPage.AutoToolTip = false;
                currentPage.ToolTipText = "Текущая страница";

                ToolStripLabel totalPages = (ToolStripLabel)toolStrip.Items.Find("totalPages", true)[0];
                totalPages.AutoToolTip = false;
                totalPages.ToolTipText = "Всего страниц";

                ToolStripButton nextPage = (ToolStripButton)toolStrip.Items.Find("nextPage", true)[0];
                nextPage.AutoToolTip = false;
                nextPage.ToolTipText = "Следующая страница";
                ToolStripButton lastPage = (ToolStripButton)toolStrip.Items.Find("lastPage", true)[0];
                lastPage.AutoToolTip = false;
                lastPage.ToolTipText = "На последнюю страницу";

                ToolStripButton stop = (ToolStripButton)toolStrip.Items.Find("stop", true)[0];
                stop.AutoToolTip = false;
                stop.ToolTipText = "Остановить подготовку к просмотру";
                ToolStripButton refresh = (ToolStripButton)toolStrip.Items.Find("refresh", true)[0];
                refresh.AutoToolTip = false;
                refresh.ToolTipText = "Обновить";

                ToolStripButton print = (ToolStripButton)toolStrip.Items.Find("print", true)[0];
                print.AutoToolTip = false;
                print.Text = "Печать";
                print.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                print.ToolTipText = "Печать";
                ToolStripButton printPreview = (ToolStripButton)toolStrip.Items.Find("printPreview", true)[0];
                printPreview.AutoToolTip = false;
                printPreview.ToolTipText = "Предварительный просмотр";

                ToolStripButton pageSetup = (ToolStripButton)toolStrip.Items.Find("pageSetup", true)[0];
                pageSetup.AutoToolTip = false;
                pageSetup.ToolTipText = "Параметры страницы";


                ToolStripTextBox textToFind = (ToolStripTextBox)toolStrip.Items.Find("textToFind", true)[0];
                textToFind.AutoToolTip = false;
                textToFind.ToolTipText = "Введите текст для поиска в отчете (недоступно в предварительном просмотре)";

                ToolStripButton find = (ToolStripButton)toolStrip.Items.Find("find", true)[0];
                find.AutoToolTip = false;
                find.ToolTipText = "Найти этот текст в отчете";
                ToolStripButton findNext = (ToolStripButton)toolStrip.Items.Find("findNext", true)[0];
                findNext.AutoToolTip = false;
                findNext.ToolTipText = "Найти следующий";

                ToolStripDropDownButton export = (ToolStripDropDownButton)toolStrip.Items.Find("export", true)[0];
                export.AutoToolTip = false;
                export.Text = "Экспорт";
                export.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                export.ToolTipText = "Экспорт в другой формат";

                export.DropDownOpening += new EventHandler(export_DropDownOpening);
                //System.Windows.Forms.MessageBox.Show(string.Join(";", extensions.Select(t => t.Name)));

            /*}
            catch (Exception ex)
            { }  */          
        }

        void export_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripDropDownButton b = (ToolStripDropDownButton)sender;
            b.DropDownItems.Clear();
            RenderingExtension[] extensions = repViewer.LocalReport.ListRenderingExtensions();
            Populate(b, openExportDialog, extensions, this.EnabledExportExtension);
        }

        public static void Populate(ToolStripDropDownItem dropDown, EventHandler handler, RenderingExtension[] extensions, string[] EnabledExtension)
        {
            dropDown.DropDownItems.Clear();
            foreach (RenderingExtension extension in extensions)
            {
                if (ShouldDisplay(extension))
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Text = extension.LocalizedName;
                    item.Tag = extension;
                    item.ToolTipText = "Экспорт в формат " + extension.LocalizedName;
                    if (handler != null)
                    {
                        item.Click += handler;
                    }
                    if (EnabledExtension == null || EnabledExtension.Count(r => r.ToUpper() == (item.Text.ToUpper())) == 0)
                        item.Enabled = false;
                    dropDown.DropDownItems.Add(item);
                }
            }
        }
        void openExportDialog(object sender, EventArgs e)
        { 
            RenderingExtension ext = (RenderingExtension)((sender as ToolStripItem).Tag);
            string FileName = Connect.UserSpecialFolder + @"\1" + GetRenderExtString(ext);
            SaveFileDialog sf= new SaveFileDialog();
            sf.OverwritePrompt = true;
            sf.Filter = string.Format("Документ {0} ({1})|*{1}",ext.Name, GetRenderExtString(ext));
            if (sf.ShowDialog(new Wpf32Window(this)) == System.Windows.Forms.DialogResult.OK)
            {
                FileName = sf.FileName;
                if (this.repViewer.ExportDialog(ext, null, FileName) == System.Windows.Forms.DialogResult.OK)
                {
                    if (File.Exists(FileName))
                        Process.Start(FileName);
                }
            }
        }


        private string GetRenderExtString(RenderingExtension r)
        {
            switch (r.LocalizedName.ToUpper())
            {
                case "WORD": return ".docx"; break;
                case "EXCEL": return ".xlsx"; break;
                case "PDF": return ".pdf"; break;
                default: return string.Empty;
            }
            return null;
        }

        private static bool ShouldDisplay(RenderingExtension extension)
        {
            return extension.Visible;
        }
#endregion


        public static void ShowReport(DependencyObject sender, string Title, string path, DataTable table, IEnumerable<ReportParameter> r, SPrint.Duplex duplex = SPrint.Duplex.Simplex,
            bool PreviewPrint=true)
        {

            ShowReport(sender, Title, path, null, new DataTable[] { table }.AsEnumerable(), r, duplex, PreviewPrint);
        }

        public static void ShowReport(DependencyObject sender, string Title, string path, DataTable table, IEnumerable<ReportParameter> r, string[] EnabledExport)
        {

            ShowReport(sender, Title, path, null, new DataTable[] { table }.AsEnumerable(), r, SPrint.Duplex.Default, true, null, EnabledExport);
        }

        public static void ShowReport(DependencyObject sender, string Title, string path, IEnumerable<DataTable> tables, IEnumerable<ReportParameter> r, SPrint.Duplex duplex = SPrint.Duplex.Simplex,
            bool PreviewPrint = true)
        {

            ShowReport(sender, Title, path, null, tables, r, duplex, PreviewPrint);
        }

        /// <summary>
        /// Формирует отчет согласно заданным критериям
        /// </summary>
        /// <param name="sender"> Владелец окна отчета</param>
        /// <param name="Title">Надпись окна отчета</param>
        /// <param name="path">Путь к основном отчету</param>
        /// <param name="subreports">Подотчеты</param>
        /// <param name="tables">Таблицы данных отчета</param>
        /// <param name="r">Параметры отчета</param>
        /// <param name="duplex">Режим печати отчета</param>
        /// <param name="PreviewPrint">Показывать ли предварительный просмотр</param>
        /// <param name="EnabledExport">Доступные параметры для экспорта</param>
        public static void ShowReport(DependencyObject sender, string Title, string path, IEnumerable<SubReport> subreports, IEnumerable<DataTable> tables, IEnumerable<ReportParameter> r, SPrint.Duplex duplex = SPrint.Duplex.Simplex,
            bool PreviewPrint = true, DateTime? YearVersion = null,  string[] EnabledExport = null)
        {
            if (!ExistsReport(path, YearVersion))
            {
                System.Windows.MessageBox.Show(Window.GetWindow(sender), "Ошибка формирования. Макет отчета не найден в заданной директории", "Зарплата предприятия");
                return;
            }
            ViewReportWindow f = new ViewReportWindow(EnabledExport);
            f.repViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);            
            f.repViewer.LocalReport.LoadReportDefinition(File.OpenRead(string.Format(@"{0}\Reports\{1}{2}", Connect.CurrentAppPath, YearVersion.HasValue?YearVersion.Value.Year.ToString()+@"\":string.Empty, path)));

            if (subreports != null)
            {
                CurrentSubReports = subreports;
                foreach (SubReport st in subreports)
                {
                    f.repViewer.LocalReport.LoadSubreportDefinition(st.ReportName, File.OpenRead(string.Format(@"{0}\Reports\{1}{2}", Connect.CurrentAppPath, YearVersion.HasValue ? YearVersion.Value.Year.ToString() + @"\" : string.Empty, st.ReportPath)));
                }
            }
            else
                CurrentSubReports = null;
            f.repViewer.PrinterSettings.Duplex = duplex;
            f.repViewer.ZoomPercent = 110;
            f.repViewer.LocalReport.DataSources.Clear();
            if (tables != null)
            {
                int i = 1;
                foreach (DataTable t in tables)
                    f.repViewer.LocalReport.DataSources.Add(new ReportDataSource(string.Format("DataSet{0}", i++), t));
            }
            if (r != null)
                f.repViewer.LocalReport.SetParameters(r);
            f.repViewer.RefreshReport();
            if (PreviewPrint) f.repViewer.SetDisplayMode(DisplayMode.PrintLayout);
            if (sender!=null)
                f.Owner = Window.GetWindow(sender);
            f.Title += "    "+Title;
            f.Show();
        }

        /// <summary>
        /// Проверяет, есть ли заданный отчет в папке отчетов
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ExistsReport(string path, DateTime? YearVersion)
        {
            if (YearVersion==null)
                return File.Exists(Connect.CurrentAppPath + @"\Reports\" + path);// если версия без года, то берем версию без года, иначе в папке с годом
            else
                return File.Exists(Connect.CurrentAppPath + @"\Reports\"+YearVersion.Value.Year.ToString()+@"\" + path);
        }

        /// <summary>
        /// Проверяет, есть ли заданный отчет в папке отчетов
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ExistsReport(string path)
        {
            return ExistsReport(path, null);
        }

        private static IEnumerable<SubReport> CurrentSubReports
        {
            get;
            set;
        }

        static void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            if (CurrentSubReports != null)
            {
                e.DataSources.Clear();
                e.DataSources.Add(new ReportDataSource("DataSet1", CurrentSubReports.Where(t => t.ReportName == e.ReportPath).Select(t => t.DataSource).FirstOrDefault()));
            }
        }

        public static void ShowReport(DependencyObject sender, string Title, string path, IEnumerable<SubReport> subreports, DataTable table, IEnumerable<ReportParameter> r )
        {
            ShowReport(sender, Title, path, subreports, new DataTable[] { table }, r);
        }

        /// <summary>
        /// Выгружает рдлс репорт в эксель используя метод Render
        /// </summary>
        /// <param name="sender">окно владелец</param>
        /// <param name="path">путь к отчету RDLC</param>
        /// <param name="table">таблицы для отчета</param>
        /// <param name="r">список параметров для отчета</param>
        public static void RenderToExcel(DependencyObject sender, string path, DataTable table, IEnumerable<ReportParameter> r)
        {
            RenderToExcel(sender, path, "", string.Empty, new DataTable[] { table }, r);
        }

        /// <summary>
        /// Выгружает рдлс репорт в эксель используя метод Render
        /// </summary>
        /// <param name="sender">окно владелец</param>
        /// <param name="path">путь к отчету RDLC</param>
        /// <param name="SaveFileName">Имя для очета по умолчанию</param>
        /// <param name="table">таблицы для отчета</param>
        /// <param name="r">список параметров для отчета</param>
        public static void RenderToExcel(DependencyObject sender, string path, string SaveFileName, DataTable table, IEnumerable<ReportParameter> r)
        {
            RenderToExcel(sender, path, SaveFileName, string.Empty, new DataTable[] { table }, r);
        }

        /// <summary>
        /// Выгружает рдлс репорт в эксель используя метод Render
        /// </summary>
        /// <param name="sender">окно владелец</param>
        /// <param name="path">путь к отчету RDLC</param>
        /// <param name="SaveFileName">Имя для очета по умолчанию</param>
        /// <param name="table">таблицы для отчета</param>
        /// <param name="r">список параметров для отчета</param>
        public static void RenderToExcel(DependencyObject sender, string path, string SaveFileName, string initDirectory, DataTable table, IEnumerable<ReportParameter> r)
        {
            RenderToExcel(sender, path, SaveFileName, initDirectory , new DataTable[] { table }, r);
        }

        /// <summary>
        /// Выгружает рдлс репорт в эксель используя метод Render
        /// </summary>
        /// <param name="sender">окно владелец</param>
        /// <param name="path">путь к отчету RDLC</param>
        /// <param name="SaveFileName">имя для сохранения по умолчанию</param>
        /// <param name="tables"> таблицы для отчета</param>
        /// <param name="r">список параметров для отчета</param>
        public static void RenderToExcel(DependencyObject sender, string path, string SaveFileName, string initDirectory, DataTable[] tables, IEnumerable<ReportParameter> r)
        {
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(sender, "Формирование отчета",
            (bwk, e) =>
            {
                LocalReport f = new LocalReport();
                f.LoadReportDefinition(File.OpenRead(Connect.CurrentAppPath + @"\Reports\" + path));
                f.DataSources.Clear();
                tables = e.Argument as DataTable[];
                if (tables != null)
                    for (int i = 1; i <= tables.Length; ++i)
                        f.DataSources.Add(new ReportDataSource(string.Format("DataSet{0}", i), tables[i - 1]));
                if (r != null)
                    f.SetParameters(r);
                e.Result = f.Render("Excel");
            },
            tables, null,
            (bwk, e) =>
            {
                if (e.Cancelled) return;
                else
                    if (e.Error != null)
                        System.Windows.MessageBox.Show(e.Error.Message, "Ошибка формирования отчета");
                    else
                    {
                        try
                        {

                            SaveFileDialog sf = new SaveFileDialog();
                            sf.FileName = SaveFileName;
                            sf.Filter = "Файлы Excel|*.xls";
                            sf.InitialDirectory = initDirectory;
                            if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                File.WriteAllBytes(sf.FileName, (byte[])e.Result);
                                System.Diagnostics.Process.Start(sf.FileName);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show("Ошибка формирования", ex.Message);
                        }
                    }
            });
        }

    }

    public class SubReport
    {
        public SubReport(string reportName, string reportPath, DataTable dataSource)
        {
            ReportName = reportName;
            ReportPath = reportPath;
            DataSource = dataSource;
        }
        public string ReportName
        {
            get;
            set;
        }
        public string ReportPath
        {
            get;
            set;
        }

        public DataTable DataSource
        {
            get;
            set;
        }
    }

    public class ViewReportObject : MarshalByRefObject
    {
        protected static ObjectHandle _refobj;
        static ViewReportObject()
        {
            // This program needs to run in an application domain with
            // PermissionState.Unrestricted, so that remote assemblies can be loaded
            // with full trust. Instantiate Program in a new application domain.
            PermissionSet permissionSet =
                new PermissionSet(PermissionState.Unrestricted);
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase =
                AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            AppDomain appDomain = AppDomain.CreateDomain(
                "Trusted Domain", null, setup, permissionSet);
            appDomain.AssemblyResolve += MyResolver.MyResolveEventHandler;
            appDomain.Load(@"\\as96\salary\LibrarySalary.dll");
            /*_refobj = appDomain.CreateInstance(Assembly.LoadFrom(@"\\as96\salary\LibrarySalary.dll").FullName, "AddRetention.Reports.ViewReportWindow");
            _refobj.GetType().InvokeMember("Show", BindingFlags.Default, null, _refobj, null);*/
        }
        public static ObjectHandle RefWindow
        {
            get
            {
                return _refobj;
            }
        }
        public static void ShowReport(DependencyObject sender, string Title, string path, DataTable table, List<ReportParameter> r, System.Drawing.Printing.Duplex duplex = System.Drawing.Printing.Duplex.Simplex)
        {
            ViewReportWindow f = new ViewReportWindow();
            Assembly asm = Assembly.Load("LibrarySalary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b990337d630a81de");
            Assembly asm1 = Assembly.Load("Microsoft.ReportViewer.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
            AppDomain.CurrentDomain.Load(asm.GetName());
            f.repViewer.LocalReport.AddFullTrustModuleInSandboxAppDomain(new System.Security.Policy.StrongName(new System.Security.Permissions.StrongNamePublicKeyBlob(asm.GetName().GetPublicKeyToken()), asm.GetName().Name, asm.GetName().Version));
            f.repViewer.LocalReport.AddFullTrustModuleInSandboxAppDomain(new System.Security.Policy.StrongName(new System.Security.Permissions.StrongNamePublicKeyBlob(asm1.GetName().GetPublicKeyToken()), asm1.GetName().Name, asm1.GetName().Version));
            //f.repViewer.LocalReport.LoadReportDefinition(File.OpenRead(Connect.CurrentAppPath + @"\Reports\" + path));
            f.repViewer.LocalReport.ReportPath = "Reports/" + path;
            f.repViewer.ProcessingMode = ProcessingMode.Local;
            f.repViewer.PrinterSettings.Duplex = duplex;
            f.repViewer.LocalReport.DataSources.Clear();
            if (table != null)
                f.repViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", table));
            /*if (r!=null)
                f.repViewer.LocalReport.SetParameters(r);*/
            f.repViewer.RefreshReport();
            f.repViewer.SetDisplayMode(DisplayMode.PrintLayout);
            f.Owner = Window.GetWindow(sender);
            f.Title += Title;
            f.Show();
        }
    }
    public class MyResolver
    {
        public static Assembly MyResolveEventHandler(Object sender, ResolveEventArgs args)
        {
            // confirm args.Name contains A.dll
            String dllName = args.Name.Split(',')[0];
            if (dllName == @"\\as96\salary\LibrarySalary.dll")
            {
                return Assembly.LoadFile(@"\\as96\salary\LibrarySalary.dll");
            }
            return null;
        }
    }
}
