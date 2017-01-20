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
using System.Collections.ObjectModel;
using System.Threading;
using Salary.ViewModel;
using LibrarySalary.ViewModel;
using LibrarySalary.Helpers;
using Salary.View.Details;
using Salary.View.Taxes;

namespace Salary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LibrarySalary.Helpers.Helpers.MailBagHelper mailHelper;
        public MainWindow()
        {
            ///Добавил эту хуйню - для моделей соединение по умолчанию. Можно его переопределить в модели конкретной.
            /// Кажется весь этот код с моделями превращается в велосипед который в начале я так и не освоил, либо вначале он был изобретен с квадратными колесами
            EntityGenerator.RowEntityBase.CurConnect = Connect.CurConnect;
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
            System.Windows.Forms.InputLanguage.CurrentInputLanguage = System.Windows.Forms.InputLanguage.FromCulture(new System.Globalization.CultureInfo("ru-RU"));
            if (!App.CloseNotification.IsEnabled)
                App.CloseNotification.IsEnabled = true;

            /*mailHelper = new LibrarySalary.Helpers.Helpers.MailBagHelper();
            mailHelper.InitHooks();*/

#if DEBUG
            this.Loaded +=
                (p, pw) =>
                {
                    Helpers.TestMethodHelper t = new Helpers.TestMethodHelper();
                    t.RunTest();
                };
            
#endif
        }

        public ViewTabCollection OpenTabs
        {
            get
            {
                return (ViewTabCollection)this.FindResource("OpenTabs"); 
            }
        }


        private void MenuCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (mailHelper != null)
                mailHelper.DestroyHook();
            App.Current.Shutdown();
        }

        private void OpenSalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var p = new Salary.View.Payment();
            ViewTabBase v = OpenTabs.AddNewTab("Заработная плата сотрудников", p, new Uri("pack://application:,,,/Salary;component/Images/sallary_3232.png"));
            p.OwnerTabBase = v;
        }

        private void OpenPayType_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Справочник шифров оплат предприятия", new Salary.View.PayTypeTable());
        }

        private void OpenViewMethodCalc_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Методы расчета", new Salary.View.RetentionCalcMethods(), new Uri("pack://application:,,,/Salary;component/Images/setting_3232.png"));
        }

        private void OpenViewSubdivClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Закрытие зарплаты подразделений", new Salary.View.SubdivForSalary(), new Uri("pack://application:,,,/Salary;component/Images/locked2_3232.png"));
        }

        private void OpenViewAlimony_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Исполнительные листы", new Salary.View.AlimoniesViewer(), new Uri("pack://application:,,,/Salary;component/Images/hammer_2424.png"));
        }

        /*private void OpenViewTypeCostItem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Статьи затрат", new Salary.View.CostItemEditor());
        }*/

        private void OpenViewSalaryVac_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Расчет отпускных", new Salary.View.SalaryVacCalc(), new Uri("pack://application:,,,/Salary;component/Images/holiday.png"));
        }

        private void ServiceItem_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void UpdateControlRoles_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GrantedRoles.LoadGrantedRole();
            ControlRoles.LoadControlRoles();
            AppDataSet.UpdateAll();
        }

        private void UserManual_Click(object sender, RoutedEventArgs e)
        {
            string pathManual = Connect.CurrentAppPath + "\\User_Manual_Salary.doc";
            try
            {
                System.Diagnostics.Process.Start(pathManual);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка открытия справочника пользователя. Файл не найден");
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            View.AboutInfo f = new View.AboutInfo();
            f.ShowDialog();
        }

        private void OpenViewCartulary_CanExecute(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Просмотр реестров", new Salary.View.CartularyViewer(), new Uri("pack://application:,,,/Salary;component/Images/credit_card_4848.png"));
        }

        private void OpenViewPaymentProperty_CanExecute(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Прикрепленные свойства шифров оплат", new Salary.View.PaymentPropertyEditor());
        }

        private void OpenViewExceptCalcAvg_CanExecute(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Исключения индексации заработка", new Salary.View.ExceptAvgEditor());
        }

        private void OpenViewReportGroup_CanExecute(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Группы шифров для отчетности", new Salary.View.ReportGroupView());
        }

        private void OpenViewEmpAccounts_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Перечисления сотрудников", new Salary.View.EmpAccounts(), new Uri("pack://application:,,,/Salary;component/Images/contact_card_3232.png"));
        }

        private void ViewEditMessage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Сообщения пользователям", new Salary.View.MessageEditor());

        }

        private void OpenViewCosolidReport_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Формирование сводного отчета", new Salary.View.ConsolidSalary(), new Uri("pack://application:,,,/Salary;component/Images/balance_1616.png"));
        }

        private void OpenViewTypeBank_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Справочник банков для перечисления", new Salary.View.TypeBankViewer());

        }

        private void OpenViewEconView_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Просмотр и формирование отчетности", new Salary.ViewReporting.PaymentReporting(), new Uri("pack://application:,,,/Salary;component/Images/document_3232.png"));
        }

        private void OpenViewCompanyAccount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Счета организаций", new View.CompanyAccountEditor());
        }

        private void OpenViewCheckXML_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Проверка файлов", new View.CheckXML());
        }

        private void OpenViewDistribution_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Распределение затрат", new View.SalaryDistribView(), new Uri("pack://application:,,,/Salary;component/Images/table_1616.png"));
        }

        private void ViewUsersControl_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Управление пользователями", new RolesViewerLibrary.UserViewer(Connect.CurConnect));
        }

        private void OpenViewPrintTabs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Печать расчетных листов", new Salary.View.Tools.PrintTabsEmp());
        }

        private void OpenViewBrigage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Справочник бригад", new BrigageEditor(), new Uri("pack://application:,,,/Salary;component/Images/group_3232.png"));
        }

        private void OpenViewTabeBrigage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Табель бригад и КТУ", new TableBrigageView(), new Uri("pack://application:,,,/Salary;component/Images/group_3232.png"));
        }

        private void ViewPurpose_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Цели получения ссуды", new Salary.Loan.Purpose_Loan_Viewer());
        }

        private void ViewType_Loan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Типы ссуд", new Salary.Loan.Type_Loan_Viewer());
        }

        private void ViewRef_Rate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Ставка рефинансирования", new Salary.Loan.Refinancing_Rate_Viewer());
        }

        private void ViewLoan_Cost_Item_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Статьи затрат", new Salary.Loan.Loan_Cost_Item_Viewer());
        }

        private void ViewLoan_To_Registration_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Ссуды на оформлении", new Salary.Loan.Loan_Viewer(false), new Uri("pack://application:,,,/Salary;component/Images/money_calculator_3232.png"));
        }

        private void ViewLoan_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Ссуды", new Salary.Loan.Loan_Viewer(true), new Uri("pack://application:,,,/Salary;component/Images/kwallet_3232.png"));
        }

        private void OpenViewTaxes_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Учет НФДЛ", new TaxCompanyViewer(), new Uri("pack://application:,,,/Salary;component/Images/money_bag_128_128.png"));
        }

        private void OpenViewDocumTransfer_CanExecute(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTabs.AddNewTab("Перечисления по приказам", new View.ClientAccounts.DocumTransferView(), new Uri("pack://application:,,,/Salary;component/Images/vcard_3232.png"));
        }
    }
}
