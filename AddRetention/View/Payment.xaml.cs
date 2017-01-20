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
using System.Data;
using Oracle.DataAccess.Client;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Collections;
using Microsoft.Reporting.WinForms;
using System.Timers;
using Salary.ViewModel;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using Salary.Reports;
using System.Globalization;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using Salary.Helpers;
using Salary.Model;
using Salary.Interfaces;
using Salary.ViewReporting;
using LibrarySalary.ViewModel;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : UserControl, INotifyPropertyChanged
    {
        static Payment()
        {
            cmdDeleteSalary = new OracleCommand(string.Format("begin {1}.SALARY_DELETE(:p_SALARY_ID, :p_CALC_RELATION); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmdDeleteSalary.BindByName = true;
            cmdDeleteSalary.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, ParameterDirection.Input);
            cmdDeleteSalary.Parameters.Add("p_CALC_RELATION", OracleDbType.Array, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
        }
        
        /// <summary>
        /// Конструктор формы просмотра и расчета ЗП
        /// </summary>
        public Payment()
        {
            salaryRowFilter = AppDataSet.Tables["TYPE_PAYMENT_TYPE"].Copy();
            salaryRowFilter.Columns.Add("FL", typeof(bool));
            foreach (DataRow r in salaryRowFilter.Rows)
                r["FL"] = true;
            calced_type_payment = AppDataSet.Tables["TYPE_PAYMENT_TYPE"].Copy();
            calced_type_payment.Columns.Add("FL", typeof(bool));
            foreach (DataRow r in calced_type_payment.Rows)
                r["FL"] = true;
            InitializeComponent();
            salaryRowLocalFilter.ItemsSource = new DataView(salaryRowFilter, "", "ORDER_NUMBER", DataViewRowState.CurrentRows);
            salaryRowFilter.RowChanged += new DataRowChangeEventHandler(salaryRowFilter_RowChanged);
#region Создание команд и адаптеров
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                /*//gridSubdivSelector.DataContext; = AppDataSet.GetSubdivView("CODE_SUBDIV");
                Binding b = new Binding("SUBDIV_ID");
                b.Source = new DataView(AppDataSet.Tables["ACCESS_SUBDIV"], "", "CODE_SUBDIV", DataViewRowState.CurrentRows);
                b.Mode = BindingMode.OneWay;*/
                cbCodeSubdiv.ItemsSource = new DataView(AppDataSet.Tables["ACCESS_SUBDIV"], "APP_NAME='SALARY'", "CODE_SUBDIV", DataViewRowState.CurrentRows);
                cbSubdivName.ItemsSource = new DataView(AppDataSet.Tables["ACCESS_SUBDIV"], "APP_NAME='SALARY'", "CODE_SUBDIV", DataViewRowState.CurrentRows);
                cbPaymentType.ItemsSource = new DataView(AppDataSet.Tables["PAYMENT_TYPE"], "", "CODE_PAYMENT", DataViewRowState.CurrentRows);
                cmd_UpdateSalaryFromTable = new OracleCommand(string.Format("begin {1}.LoadEmpSalaryFromTable(:p_transfer_id, :p_subdiv_id, :p_pay_date, :p_pay_type_id); end;",Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd_UpdateSalaryFromTable.BindByName=true;
                cmd_UpdateSalaryFromTable.Parameters.Add("p_transfer_id",OracleDbType.Decimal, ParameterDirection.Input);
                cmd_UpdateSalaryFromTable.Parameters.Add("p_subdiv_id",OracleDbType.Decimal, ParameterDirection.Input);
                cmd_UpdateSalaryFromTable.Parameters.Add("p_pay_date",OracleDbType.Date, ParameterDirection.Input);
                cmd_UpdateSalaryFromTable.Parameters.Add("p_pay_type_id",OracleDbType.Decimal, ParameterDirection.Input);

                cmd_LoadSubdivTable = new OracleCommand(String.Format("begin {1}.LoadSubdivSalaryFromTable(:p_subdiv_id, :p_date);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd_LoadSubdivTable.BindByName = true;
                cmd_LoadSubdivTable.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, ParameterDirection.Input);
                cmd_LoadSubdivTable.Parameters.Add("p_date", OracleDbType.Date, ParameterDirection.Input);

                cmd_CalcEmpRetention = new OracleCommand(string.Format("begin {1}.CALC_EMP_RETENTION(:p_transfer_id,:p_subdiv_id, :p_date, :p_calced_type_payment);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd_CalcEmpRetention.BindByName = true;
                cmd_CalcEmpRetention.Parameters.Add("p_transfer_id", OracleDbType.Decimal, ParameterDirection.Input);
                cmd_CalcEmpRetention.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, ParameterDirection.Input);
                cmd_CalcEmpRetention.Parameters.Add("p_date", OracleDbType.Date, ParameterDirection.Input);
                cmd_CalcEmpRetention.Parameters.Add("p_calced_type_payment", OracleDbType.Array, ParameterDirection.Input).UdtTypeName="SALARY.NUMBER_COLLECTION_TYPE";

                cmd_CalcEmpZoneExpAdd = new OracleCommand(string.Format("begin {1}.CALCULATION_SALARY.Calc_Emp_ZoneExpAdd(:p_transfer_id, :p_subdiv_id, :p_date); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd_CalcEmpZoneExpAdd.BindByName = true;
                cmd_CalcEmpZoneExpAdd.Parameters.Add("p_transfer_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                cmd_CalcEmpZoneExpAdd.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                cmd_CalcEmpZoneExpAdd.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);

                cmd_CalcSubdivZoneExpAdd = new OracleCommand(string.Format("begin {1}.CALCULATION_SALARY.calc_subdiv_zone_exp_add(:p_subdiv_id, :p_date);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd_CalcSubdivZoneExpAdd.BindByName = true;
                cmd_CalcSubdivZoneExpAdd.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                cmd_CalcSubdivZoneExpAdd.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);

                ds = new DataSet();
                

                //ListEmpData = new TaskNotify<ICollectionView>(new Task<ICollectionView>(LoadEmpList));

                empListArea.DataContext = EmpCollection;
                exFilter.DataContext = EmpFilterItempProvider;
                //LoadEmpList();
                odaTaxDiscount = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectEmpTaxDiscount.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                odaTaxDiscount.SelectCommand.BindByName = true;
                odaTaxDiscount.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, ParameterDirection.Input);
                odaTaxDiscount.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, ParameterDirection.Input);
                odaTaxDiscount.TableMappings.Add("Table", "EmpTaxDiscount");
                LoadSalaryCatalogs();
                //Адаптер загрузки данных по зарплате сотрудника
                odaSalary = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectEmpSalary.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                odaSalary.SelectCommand.BindByName = true;
                odaSalary.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                odaSalary.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                odaSalary.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
                odaSalary.SelectCommand.Parameters.Add("p_fullyearsign", OracleDbType.Decimal, null, ParameterDirection.Input);
                odaSalary.SelectCommand.Parameters.Add("p_SubdivSalaryOnly", OracleDbType.Decimal, null, ParameterDirection.Input);
                odaSalary.SelectCommand.Parameters.Add(":c1", OracleDbType.RefCursor, ParameterDirection.Output);
                odaSalary.TableMappings.Add("Table", "Salary");
                odaSalary.DeleteCommand = new OracleCommand(string.Format("begin {1}.SALARY_DELETE(:p_SALARY_ID, :p_CALC_RELATION); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                odaSalary.DeleteCommand.BindByName = true;
                odaSalary.DeleteCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, ParameterDirection.Input);
                odaSalary.DeleteCommand.Parameters.Add("p_CALC_RELATION", OracleDbType.Array, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";

                //Адаптер загрузки данных по удержаниям
                odaRetention = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectEmpRetention.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                odaRetention.SelectCommand.BindByName = true;
                odaRetention.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                odaRetention.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
                odaRetention.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, null, ParameterDirection.Output);
                odaRetention.TableMappings.Add("Table", "EmpRetention");
                
                //Адаптерп загрузки данные по авансу
                odaAdvance= new OracleDataAdapter(Queries.GetQueryWithSchema("SelectEmpAdvance.sql"), Connect.CurConnect);
                odaAdvance.SelectCommand.BindByName = true;
                odaAdvance.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                odaAdvance.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
                odaAdvance.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                odaAdvance.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
                odaAdvance.TableMappings.Add("Table", "SALARY_ADVANCE");
                odaAdvance.TableMappings.Add("Table1", "SALARY_ADVANCE_RET");

                ///Адаптер загрузки документов
                odaSalaryDocum = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectEmpSalaryDocum.sql"), Connect.CurConnect);
                odaSalaryDocum.SelectCommand.BindByName =true;
                odaSalaryDocum.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                odaSalaryDocum.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
                odaSalaryDocum.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                odaSalaryDocum.TableMappings.Add("Table", "SALARY_DOCUM");
                odaSalaryDocum.DeleteCommand = new OracleCommand(string.Format("begin {1}.SALARY_DOCUM_DELETE(:p_salary_docum_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                odaSalaryDocum.DeleteCommand.BindByName = true;
                odaSalaryDocum.DeleteCommand.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, null, ParameterDirection.Input);

                odaSalaryDocumPayment = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectDocumPayment.sql"), Connect.CurConnect);
                odaSalaryDocumPayment.SelectCommand.BindByName = true;
                odaSalaryDocumPayment.SelectCommand.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                odaSalaryDocumPayment.TableMappings.Add("Table", "SALARY_DOCUM_PAYMENT");

                //Команда расчета аванса
                cmd_CalcEmpAdvance = new OracleCommand(string.Format("begin {1}.CALC_EMP_ADVANCE(:p_transfer_id, :p_date);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd_CalcEmpAdvance.BindByName = true;
                cmd_CalcEmpAdvance.Parameters.Add("p_transfer_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                cmd_CalcEmpAdvance.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);

                //Команда расчет начисление аванса и загрузки в ЗП
                cmd_LoadEmpAdvance = new OracleCommand(string.Format("begin {1}.Load_Advance_From_Table(:p_date, :p_subdiv_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd_LoadEmpAdvance.BindByName = true;
                cmd_LoadEmpAdvance.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
                cmd_LoadEmpAdvance.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);;

                event_dictionary.Add(tiSalary.GetHashCode(), LoadSalary);
                event_dictionary.Add(tiTaxDiscount.GetHashCode(), LoadTaxDiscData);
                event_dictionary.Add(tiRetention.GetHashCode(), LoadEmpRetention);
                event_dictionary.Add(tiAdvance.GetHashCode(), LoadEmpAdvance);
                event_dictionary.Add(tiSalaryDocum.GetHashCode(), UpdateSalaryDocum);

                tcSalaryTab.SelectionChanged += new SelectionChangedEventHandler(tcSalaryTab_SelectionChanged);
                EmpCollection.CurrentItemChanged += new EventHandler(EmpCollection_CurrentItemChanged);
                this.PropertyChanged += new PropertyChangedEventHandler(PaymentFilter_PropertyChanged);
                EmpFilterItempProvider.PropertyChanged += new PropertyChangedEventHandler(PaymentFilter_PropertyChanged);
                EmpCollection.LoadFinished += new EventHandler(EmpCollection_LoadFinished);
            }
#endregion Создание команд и адаптеров

            if (!App.CloseNotification.IsEnabled)
                App.CloseNotification.IsEnabled = true;
        }
        
    #region Процедуры загрузки данных

        /// <summary>
        /// 
        /// </summary>
        private void LoadSalaryCatalogs()
        {
            try
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("EmpCatalogData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                a.TableMappings.Add("Table", "DiscType");
                a.Fill(ds);
            }
            catch
            { }
        }

        /// <summary>
        /// Загрузка зарплатных данных по сотруднику
        /// </summary>
        private void LoadSalary()
        {
            
            if (ds!=null && ds.Tables.Contains("Salary"))
            {
                ds.Tables["Salary"].BeginLoadData();
                ds.Tables["Salary"].Rows.Clear();
            }
            if (SelectedTransferID == null)
            {
                ListCollectionView l = (dgEmpPaySalary.ItemsSource as ListCollectionView);
                if (l!=null)
                    l.Refresh();
                return;
            }
            try
            {
                odaSalary.SelectCommand.Parameters["p_transfer_id"].Value = SelectedTransferID;
                odaSalary.SelectCommand.Parameters["p_date"].Value = EmpFilterItempProvider.SelectedDate;
                odaSalary.SelectCommand.Parameters["p_fullyearsign"].Value = new DecimalToBoolConverter().ConvertBack(ShowFullYearSign, typeof(bool), null, null);
                odaSalary.SelectCommand.Parameters["p_SubdivSalaryOnly"].Value = new DecimalToBoolConverter().ConvertBack(ShowSubdivSalarySign, typeof(bool), null, null);
                odaSalary.SelectCommand.Parameters["p_subdiv_id"].Value = EmpFilterItempProvider.SubdivID;
                odaSalary.Fill(ds);
                ds.Tables[odaSalary.TableMappings[0].DataSetTable].EndLoadData();
                if (dgEmpPaySalary.ItemsSource == null)
                {
                    //sal_view = new DataView(ds.Tables["Salary"], "", "TYPE_PAYMENT_TYPE_ID, PAY_MONTH, CODE_PAYMENT", DataViewRowState.CurrentRows);
                    ListCollectionView cv = new ListCollectionView(ds.Tables["Salary"].DefaultView);
                    cv.GroupDescriptions.Add(new PropertyGroupDescription("PAY_MONTH", new TruncDateConverter()));
                    cv.GroupDescriptions.Add(new PropertyGroupDescription("TYPE_PAYMENT_TYPE_ID", new PayTypeConverter()));
                    cv.GroupDescriptions.Add(new PropertyGroupDescription("CODE_PAYMENT"));
                    cv.SortDescriptions.Add(new SortDescription("PAY_MONTH", ListSortDirection.Ascending));
                    cv.SortDescriptions.Add(new SortDescription("CODE_PAYMENT", ListSortDirection.Ascending));
                    dgEmpPaySalary.DataContext = cv;
                }
                else
                    (dgEmpPaySalary.ItemsSource as ListCollectionView).Refresh();
                    
            }
            catch 
            {
                if (ds != null & ds.Tables.Contains("Salary"))
                    ds.Tables["Salary"].EndLoadData();
            }
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Загрузка налоговых вычетов по сотруднику
        /// </summary>
        private void LoadTaxDiscData()
        {
            if (ds.Tables.Contains("EmpTaxDiscount"))
            {
                ds.Tables["EmpTaxDiscount"].Rows.Clear();
            }
            if (this.SelectedTransferID != null)
            {
                try
                {
                    odaTaxDiscount.SelectCommand.Parameters["p_transfer_id"].Value = SelectedTransferID;
                    odaTaxDiscount.SelectCommand.Parameters["p_date"].Value = EmpFilterItempProvider.SelectedDate;
                    odaTaxDiscount.Fill(ds);
                    if (dgTaxDiscount.ItemsSource == null)
                    {
                        dgTaxDiscount.ItemsSource = new DataView(ds.Tables["EmpTaxDiscount"], "", "DATE_START_DISC desc", DataViewRowState.CurrentRows);
                    }
                }
                catch { }
            }
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Загрузка данных по удержаниям
        /// </summary>
        private void LoadEmpRetention()
        {
            if (ds.Tables.Contains("EmpRetention"))
            {
                ds.Tables["EmpRetention"].Clear();
            }
            if (this.SelectedTransferID != null)
            {
                try
                {
                    odaRetention.SelectCommand.Parameters["p_transfer_id"].Value = SelectedTransferID;
                    odaRetention.SelectCommand.Parameters["p_date"].Value = EmpFilterItempProvider.SelectedDate;
                    odaRetention.Fill(ds);
                    if (dgRetention.ItemsSource == null)
                    {
                        ICollectionView cv = CollectionViewSource.GetDefaultView(new DataView(ds.Tables["EmpRetention"], "", "DATE_START_RET DESC, CODE_PAYMENT", DataViewRowState.CurrentRows));
                        cv.GroupDescriptions.Add(new PropertyGroupDescription("TYPE_GROUP_RETENTION_ID", new GroupRetentionConverter()));
                        dgRetention.DataContext = cv;
                    }
                    cbRetentionFilter_SelectionChanged(this, null);
                }
                catch { }
            }
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Загрузка данных по авансу
        /// </summary>
        private void LoadEmpAdvance()
        {
            if (ds.Tables.Contains("SALARY_ADVANCE"))
            {
                ds.Tables["SALARY_ADVANCE"].Clear();
                ds.Tables["SALARY_ADVANCE_RET"].Clear();
            }
            if (SelectedTransferID != null)
            {
                try
                {
                    odaAdvance.SelectCommand.Parameters["p_transfer_id"].Value = SelectedTransferID;
                    odaAdvance.SelectCommand.Parameters["p_date"].Value = EmpFilterItempProvider.SelectedDate;
                    odaAdvance.Fill(ds);
                    if (tiAdvance.DataContext == null || tiAdvance.DataContext is ViewTabBase)
                    {
                        tiAdvance.DataContext = ds;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных по авансу сотрудника");
                }
            }
        }

        /// <summary>
        /// Загрузка документов начисления по сотруднику
        /// </summary>
        private void LoadEmpSalaryDocum()
        {
            if (ds.Tables.Contains("SALARY_DOCUM"))
            {
                ds.Tables["SALARY_DOCUM"].Rows.Clear();
            }
            if (SelectedTransferID != null)
            {
                try
                {
                    odaSalaryDocum.SelectCommand.Parameters["p_transfer_id"].Value = SelectedTransferID;
                    odaSalaryDocum.SelectCommand.Parameters["p_date"].Value = EmpFilterItempProvider.SelectedDate;
                    odaSalaryDocum.Fill(ds);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных по документам начисления");
                }

            }
        }
    #endregion 

#region Команды локальные для ЗП
    #region Налоговые вычеты - команды и их обработчики
        
        void AddTaxDisc_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && SelectedTransferID != null;
        }

        private void AddTaxDisc_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (new TaxDiscountEditor(SelectedTransferID, EmpFilterItempProvider.SelectedDate) { Owner = Window.GetWindow(this) }.ShowDialog() == true)
            {
                LoadTaxDiscData();
            }
        }
        
        private void EditTaxDisc_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SelectedTransferID != null && dgTaxDiscount != null && dgTaxDiscount.SelectedItem != null && ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }

        private void EditTaxDisc_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (new TaxDiscountEditor(SelectedTransferID, EmpFilterItempProvider.SelectedDate, (dgTaxDiscount.SelectedItem as DataRowView)["EMP_TAX_DISCOUNT_ID"]) { Owner = Window.GetWindow(this) }.ShowDialog() == true)
            {
                LoadTaxDiscData();
            }
        }
        
        void DeleteTaxDisc_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SelectedTransferID != null && dgTaxDiscount != null && dgTaxDiscount.SelectedItem != null && ControlRoles.GetState((e.Command as RoutedUICommand).Name);
        }

        private void DeleteTaxDisc_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную скидку у сотрудника?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                OracleCommand cmd = new OracleCommand(string.Format(@"begin {1}.EMP_TAX_DISCOUNT_DELETE(:p_EMP_TAX_DISCOUNT_ID); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd.BindByName = true;
                cmd.Parameters.Add("p_EMP_TAX_DISCOUNT_ID", OracleDbType.Decimal, (dgTaxDiscount.SelectedItem as DataRowView)["EMP_TAX_DISCOUNT_ID"], ParameterDirection.Input);
                try
                {
                    cmd.ExecuteNonQuery();
                    LoadTaxDiscData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException());
                }
            }
        }

        private void UpdateTaxDiscountFromDependents_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand(string.Format("begin {1}.UpdateEmpTaxDiscount(:p_date, :p_transfer_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmd.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            cmd.Parameters.Add("p_transfer_id", OracleDbType.Decimal, SelectedTransferID, ParameterDirection.Input);
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
                RefreshTaxDics_Click(this, null);
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка обновления данных из таблицы иждивенцев");
            }
        }
    #endregion

    #region Заработная плата сотрудника - команды и обработчики

        private void AddEmpPaySalary_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && SelectedTransferID != null;
        }

        private void AddEmpPaySalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryEditor f = new SalaryEditor(this, SelectedTransferID, null, EmpFilterItempProvider.SelectedDate.Value);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                UpdateCurrentSalaryTab();
            }
        }
        
        private void EditEmpPaySalary_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = dgEmpPaySalary != null && dgEmpPaySalary.SelectedItem != null && ControlRoles.GetState((e.Command as RoutedUICommand).Name) && SelectedTransferID != null;
        }

        private void EditEmpPaySalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryEditor f = new SalaryEditor(this, SelectedTransferID, (dgEmpPaySalary.SelectedItem as DataRowView)["SALARY_ID"], EmpFilterItempProvider.SelectedDate.Value);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                UpdateCurrentSalaryTab();
            }
        }
        private void DeleteEmpPaySalary_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = dgEmpPaySalary != null && dgEmpPaySalary.SelectedItem != null && ControlRoles.GetState((e.Command as RoutedUICommand).Name) && SelectedTransferID != null;
        }

        private void DeleteEmpPaySalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(string.Format("Удалить все выбранные строки ({0} шт. - {1} в.о.)?", dgEmpPaySalary.SelectedItems.Count, string.Join(", ",dgEmpPaySalary.SelectedItems.OfType<DataRowView>().Select(r=>r["CODE_PAYMENT"]).ToArray())), "Удаление записей", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DeleteEmpPayRows(dgEmpPaySalary.SelectedItems.OfType<DataRowView>().Select(r=>r.Row.Field2<Decimal>("SALARY_ID")).ToArray());
                LoadSalary(); 
            }
        }

        /// <summary>
        /// Удаляет из зарплаты строки с заданным айдишниками
        /// </summary>
        /// <param name="array_ids"></param>
        public void DeleteEmpPayRows(Decimal[] array_ids)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                foreach (Decimal r in array_ids)
                {
                    odaSalary.DeleteCommand.Parameters["p_SALARY_ID"].Value = r;
                    odaSalary.DeleteCommand.Parameters["p_CALC_RELATION"].Value = GetCalcedTypePayment();
                    odaSalary.DeleteCommand.ExecuteNonQuery();
                }
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException());
            }
        }

        private static OracleCommand cmdDeleteSalary;
        /// <summary>
        /// Удаление записей из заработной платы команда через процедуру удаления
        /// Метод статичный, не требует создания формы Payment
        /// </summary>
        /// <param name="array_ids"></param>
        public static void DeleteEmpPayStaticRows(Decimal[] array_ids)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                foreach (Decimal r in array_ids)
                {
                    cmdDeleteSalary.Parameters["p_SALARY_ID"].Value = r;
                    cmdDeleteSalary.Parameters["p_CALC_RELATION"].Value = new decimal[] { };
                    cmdDeleteSalary.ExecuteNonQuery();
                }
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException());
            }
        }
        
        private void RefreshSalary_Click(object sender, RoutedEventArgs e)
        {
            LoadSalary();
        }

        private void ReLoadEmpPaySalary_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && SelectedTransferID != null;
        }

        private void ReLoadEmpSalaryFromTable_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Перезагрузить записи о начислениях из табеля?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OracleTransaction tr = Connect.CurConnect.BeginTransaction();
                try
                {
                    cmd_UpdateSalaryFromTable.Parameters["p_transfer_id"].Value = SelectedTransferID;
                    cmd_UpdateSalaryFromTable.Parameters["p_subdiv_id"].Value = EmpFilterItempProvider.SubdivID;
                    cmd_UpdateSalaryFromTable.Parameters["p_pay_date"].Value = EmpFilterItempProvider.SelectedDate;
                    cmd_UpdateSalaryFromTable.ExecuteNonQuery();
                    tr.Commit();
                    LoadSalary();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.GetFormattedException());
                }
                finally
                {
                }
            }
        }

    #endregion

    #region Удержания сотрудников
        
        private void AddRetention_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (SelectedTransferID != null && new RetentionEditor(SelectedTransferID, null, RetentionGroup.Taxes) { Owner = Window.GetWindow(this) }.ShowDialog() == true)
            {
                LoadEmpRetention();
            }
        }

        private void AddRetention_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && SelectedTransferID != null;
        }
        private void EditRetent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (SelectedTransferID != null && new RetentionEditor(SelectedTransferID, (dgRetention.SelectedValue as DataRowView)["retention_id"]) { Owner = Window.GetWindow(this) }.ShowDialog() == true)
            {
                LoadEmpRetention();
            }
        }

        private bool DeleteRetention(object p_retent_id)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                OracleCommand cmd = new OracleCommand(string.Format("begin {1}.RETENTION_DELETE(:p_retention_id); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                cmd.Parameters.Add("p_retention_id", OracleDbType.Decimal, p_retent_id, ParameterDirection.Input);
                cmd.ExecuteNonQuery();
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException());
                return false;
            }
        }
        private void DeleteRetent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранное удержание?", "", MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
            {
                if (DeleteRetention((dgRetention.SelectedValue as DataRowView)["retention_id"]))
                    LoadEmpRetention();
            }
        }
        private void DeleteRetent_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && SelectedTransferID != null && dgRetention != null && dgRetention.SelectedValue != null;
        }

    #endregion

    #region Аванс и всякая хрень по нему
        private void EditEmpPaySalary_CanExecuted1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && e.Parameter != null;
        }

        private void DeleteEmpRetAdvance_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранное перечисление аванса?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DeleteEmpPayRows(new Decimal[] { (e.Parameter as DataRowView).Row.Field2<Decimal>("SALARY_ID") });
                RefreshSalaryAdvance_Click(this, null);
            }
        }
        
        private void AddAdvance_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryAdvanceEditor f = new SalaryAdvanceEditor(null, SelectedTransferID);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                LoadEmpAdvance();
            }
        }
        
        private void EditEmpRetAdvance_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryEditor f = new SalaryEditor(this, SelectedTransferID, (e.Parameter as DataRowView)["SALARY_ID"], EmpFilterItempProvider.SelectedDate.Value);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                UpdateCurrentSalaryTab();
            }
        }
        private void EditAdvance_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && SelectedTransferID != null && e.Parameter != null;
        }

        private void EditAdvance_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryAdvanceEditor f = new SalaryAdvanceEditor((e.Parameter as DataRowView).Row.Field2<Decimal?>("SALARY_ADVANCE_ID"), SelectedTransferID);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                LoadEmpAdvance();
            }
        }

        private void DeleteAdvance_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись из аванса?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Warning)== MessageBoxResult.Yes)
            {
                if (SalaryAdvance.DeleteSalaryAdvance(new Decimal[]{(e.Parameter as DataRowView).Row.Field2<Decimal>("SALARY_ADVANCE_ID")}))
                    LoadEmpAdvance();
            }
        }

        /// <summary>
        /// Расчет аванса для сотрудника за выбранный месяц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcEmpAdvance_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(Window.GetWindow(this), "Рассчитать перечисление аванса для сотрудника?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                cmd_CalcEmpAdvance.Parameters["p_transfer_id"].Value = SelectedTransferID;
                cmd_CalcEmpAdvance.Parameters["p_date"].Value = EmpFilterItempProvider.SelectedDate;
                if (cmd_CalcEmpAdvance.TryExecuteNonQuerWithTransaction(Connect.CurConnect))
                    UpdateCurrentSalaryTab();
            }
        }

        /// <summary>
        /// Загрузка данных аванса из табеля для расчета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadSubdivAdvance_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(Window.GetWindow(this), string.Format("Сформировать начисления аванса из табеля за {0:MMMM yyyy} г.?", EmpFilterItempProvider.SelectedDate) , "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                cmd_LoadEmpAdvance.Parameters["p_date"].Value = EmpFilterItempProvider.SelectedDate;
                cmd_LoadEmpAdvance.Parameters["p_subdiv_id"].Value = EmpFilterItempProvider.SubdivID;
                if (cmd_LoadEmpAdvance.TryExecuteNonQuerWithTransaction(Connect.CurConnect, Window.GetWindow(this), true))
                    UpdateCurrentSalaryTab();
            }
        }

        /// <summary>
        /// Открытие формы для расчета зарплаты или аванса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcSubdivAdvance_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CalcReportView f = new CalcReportView(EmpFilterItempProvider.SubdivID, EmpFilterItempProvider.SelectedDate);
            f.Owner = Window.GetWindow(this);
            f.ShowDialog();
        }
        
        

    #endregion
#endregion

    #region Прочие команды и меню заработной платы
        private void ClientAccount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        }
        private void AccountCard_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EmpAccountCard f = new EmpAccountCard(SelectedTransferID);
            f.Owner = Window.GetWindow(this);
            f.ShowDialog();
        }
    #endregion


        
        private void LoadEmpListAsync()
        {
            EmpCollection.LoadDataAsync();
        }

        void tcSalaryTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is TabItem && event_dictionary.ContainsKey((e.AddedItems[0] as TabItem).GetHashCode()))
                event_dictionary[(e.AddedItems[0] as TabItem).GetHashCode()].Invoke();
            CommandManager.InvalidateRequerySuggested();
        }
    
        private void RefreshTaxDics_Click(object sender, RoutedEventArgs e)
        {
            LoadTaxDiscData();
        }

        /// <summary>
        /// Обновление данных по авансу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshSalaryAdvance_Click(object sender, RoutedEventArgs e)
        {
            LoadEmpAdvance();
        }
        
        /// <summary>
        /// Рассчитываем зарплату в сооответствии с выставленными настройками для сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcRetentCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // если не выбрано ни одного типа оплат, то рассчитывать то нечего. Ввыводим сообщение
            if (GetCalcedTypePayment().Length == 0)
            {
                MessageBox.Show("Не выбрано ни одного тип оплат для расчета! НАСТРОЙКИ->Автоматический расчет шифров оплат", "Ошибка");
                return;
            }
            if (MessageBox.Show(string.Format("Рассчитать ЗП сотрудника за {0} месяц? ({1})", EmpFilterItempProvider.SelectedDate.Value.ToString("MMMM"), string.Join(", ",CalcedTypePaymentDependency.Table.Select(string.Format("TYPE_PAYMENT_TYPE_ID in ({0})", string.Join(",",GetCalcedTypePayment()))).Select(p=>p["TYPE_PAYMENT_TYPE_NAME"]))), "ЗП Предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    cmd_CalcEmpRetention.Parameters["p_transfer_id"].Value = SelectedTransferID;
                    cmd_CalcEmpRetention.Parameters["p_subdiv_id"].Value = EmpFilterItempProvider.SubdivID;
                    cmd_CalcEmpRetention.Parameters["p_date"].Value = EmpFilterItempProvider.SelectedDate;
                    cmd_CalcEmpRetention.Parameters["p_calced_type_payment"].Value = GetCalcedTypePayment();
                    cmd_CalcEmpRetention.ExecuteNonQuery();
                    LoadSalary();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException());
                }
            }
        }
        
        private void CalcRetent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && SelectedTransferID != null;
        }
        
       
#region Глобальные команды подразделения и отчеты ЗП
        private void CalcSubdivRetent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState((e.Command as RoutedUICommand).Name) && EmpFilterItempProvider.SubdivID != null;
        }

        
        private void CalcSubdivRetent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CalcReportView f = new CalcReportView(EmpFilterItempProvider.SubdivID, EmpFilterItempProvider.SelectedDate);
            f.Owner = Window.GetWindow(this);
            f.ShowDialog();
        }
        
        private void Report_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && EmpFilterItempProvider.SubdivID!=null;
        }
        private void ReportByEmp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && (e.Parameter == null && EmpFilterItempProvider.SubdivID!=null || e.Parameter!=null && (int)e.Parameter == 1 && SelectedTransferID != null);
        }

        private void RepSalByDegreeAndPayType_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepSalByDegreeOrdersOrPayType.sql"), Connect.SchemaApstaff, Connect.SchemaSalary),
                Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_type_payment_type_ids", OracleDbType.Array, new decimal[] { 1, 4 }, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...", a, a.SelectCommand,
                (p, pw) =>
                {
                    Reports.ViewReportWindow.ShowReport(this, "\"Ведомость по категории и шифрам оплат\"", "Rep_SalByDegreeAndPayType.rdlc", (pw.Result as DataSet).Tables[0],
                            new ReportParameter[] { new ReportParameter("CODE_SUBDIV", CodeSubdiv), new ReportParameter("SelectedDate", EmpFilterItempProvider.SelectedDate.Value.ToString("MM yyyy")) }.ToList());
                });
        }
        private void RepSalByDegreeAndOrder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepSalByDegreeOrdersOrPayType.sql"), Connect.SchemaApstaff, Connect.SchemaSalary),
                    Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_type_payment_type_ids", OracleDbType.Array, new decimal[] { 1, 4 }, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Загрузка данных...", a, a.SelectCommand,
                    (p, pw) =>
                    {
                        Reports.ViewReportWindow.ShowReport(this, "\"Ведомость по категориям и заказам\"", "Rep_SalByDegreeOrders.rdlc", (pw.Result as DataSet).Tables[0],
                                new ReportParameter[] { new ReportParameter("CODE_SUBDIV", CodeSubdiv), new ReportParameter("SelectedDate", EmpFilterItempProvider.SelectedDate.Value.ToString("MM yyyy")) }.ToList());
                    });
            }
            else
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format("begin {1}.SALARY_TXT_REPORTS.SelectSalaryByOrdersPayment(:p_subdiv_id, :p_date, :c);end;", Connect.SchemaApstaff, Connect.SchemaSalary),
                    Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета",
                    (s, pw) =>
                    {
                        DataTable t = new DataTable();
                        a.Fill(t);
                        pw.Result = t;
                    }, a, a.SelectCommand,
                        (p, s) =>
                        {
                            if (s.Cancelled) return;
                            else if (s.Error != null) MessageBox.Show(s.Error.GetFormattedException(), "Ошибка формирования отчета");
                            else
                            {
                                System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
                                sf.DefaultExt = "TXT";
                                sf.Filter = "Текстовые файлы (.txt)|*.txt";
                                sf.InitialDirectory = Connect.parameters["ReportDirInit"];
                                sf.FileName = Connect.parameters["VedomReportName"];
                                sf.OverwritePrompt = false;
                                if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
                                {
                                    try
                                    {
                                        if (!File.Exists(sf.FileName))
                                        {
                                            FileStream f = File.Create(sf.FileName);
                                            f.Close();
                                        }
                                        File.AppendAllLines(sf.FileName, (s.Result as DataTable).Rows.OfType<DataRow>().Select(w => w[0].ToString()), Encoding.GetEncoding(866));
                                        MessageBox.Show("Записи успешно добавлены в файл для печати!");
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, "Ошибка записи в файл");
                                    }
                                }
                            }
                        });
            }
        }

        private void RepRetentByDegree_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepRetentByDegree.sql"), Connect.SchemaApstaff, Connect.SchemaSalary),
                Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date1", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date2", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Загрузка данных...", a,  a.SelectCommand,
                (p, pw) =>
                {
                    Reports.ViewReportWindow.ShowReport(this, "\"Удержания по категориям по подразделению\"", "Rep_RetentByDegree.rdlc", (pw.Result as DataSet).Tables[0],
                            new ReportParameter[] { new ReportParameter("CODE_SUBDIV", CodeSubdiv), new ReportParameter("SelectedDate", EmpFilterItempProvider.SelectedDate.Value.ToString("MM yyyy")) }.ToList());
                });
        }
        
        private void RepSubSalDeptor_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepSubSalDeptor.sql"), Connect.SchemaApstaff, Connect.SchemaSalary),
                Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date1", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date2", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получаем данные...", a, a.SelectCommand,
                (p, pw) =>
                {
                    Reports.ViewReportWindow.ShowReport(this, "\"Должники при расчете по подразделению\"", "Rep_SubSalDeptor.rdlc", (pw.Result as DataSet).Tables[0],
                            new ReportParameter[] { new ReportParameter("CODE_SUBDIV", CodeSubdiv), new ReportParameter("SelectedDate", EmpFilterItempProvider.SelectedDate.Value.ToString("MM yyyy")) }.ToList());
                });
        }

        private void RepSubEmpRetent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RepFilterByEmp f = new RepFilterByEmp(EmpCollection.ToList(), EmpFilterItempProvider.SubdivID, new DateTime(EmpFilterItempProvider.SelectedDate.Value.Year,
                    EmpFilterItempProvider.SelectedDate.Value.Month, 1), new DateTime(EmpFilterItempProvider.SelectedDate.Value.Year,
                        EmpFilterItempProvider.SelectedDate.Value.Month, 1).AddMonths(1).AddSeconds(-1));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepSubEmpRetention.sql"), Connect.SchemaApstaff, Connect.SchemaSalary),
                        Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date1", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date2", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                a.SelectCommand.Parameters.Add("p_transfer_ids", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                if (!f.BySubdivReport)
                    a.SelectCommand.Parameters["p_transfer_ids"].Value = f.SelectedRows.OfType<DataRowView>().Select(p => p.Row.Field2<Decimal>("transfer_id")).ToArray();
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Загрузка данных...", a, a.SelectCommand,
                    (p, pw) =>
                    {
                        Reports.ViewReportWindow.ShowReport(this, "\"Удержания сотрудников в подразделении\"", "Rep_SubEmpRetent.rdlc", (pw.Result as DataSet).Tables[0],
                                new ReportParameter[] { new ReportParameter("CODE_SUBDIV", f.BySubdivReport? f.SubdivSelector1.CodeSubdiv:"<не выбрано>"), new ReportParameter( "P_DATE1", f.DateBegin.Value.ToString("MMMM yyyy")),
                            new ReportParameter( "P_DATE2", f.DateEnd.Value.ToString("MMMM yyyy"))}.ToList());
                    });
            }
        }

        /// <summary>
        /// Отчет ошибок  расчета заработной платы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepCalcReportSal_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepCalcReportSal.sql"), Connect.SchemaApstaff, Connect.SchemaSalary),
                Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получаем данные...", a, a.SelectCommand,
                (p, pw) =>
                {
                    Reports.ViewReportWindow.ShowReport(this, "\"Протокол расчета\"", "Rep_CalcReportSal.rdlc", (pw.Result as DataSet).Tables[0],
                        new ReportParameter[] { new ReportParameter("CODE_SUBDIV", CodeSubdiv), new ReportParameter("SelectedDate", EmpFilterItempProvider.SelectedDate.Value.ToString("MM yyyy")) }.ToList());
            
                });
        }

        /// <summary>
        /// Сводный отчет по подразделению
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSubConsolidation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("RepConsolidSubdiv.sql", EmpFilterItempProvider, FilterParameter.p_date, FilterParameter.p_subdiv_id, FilterParameter.c);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...", a, a.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Свод по подразделению", "Rep_ConsolidSubdiv.rdlc", (pw.Result as DataSet).Tables[0],
                        new ReportParameter[] { new ReportParameter("CODE_SUBDIV", CodeSubdiv), new ReportParameter("SelectedDate", EmpFilterItempProvider.SelectedDate.Value.ToString("MMMM yyyy")) }.ToList(), System.Drawing.Printing.Duplex.Vertical);
                });
        }

        /// <summary>
        /// Сводный отчет по заводу нах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepConsolidDept_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("RepConsolidSubdiv.sql", EmpFilterItempProvider, FilterParameter.p_date, FilterParameter.c);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...", a, a.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "\"Свод по заработной плате\"", "Rep_Consolid_Dept.rdlc", (pw.Result as DataSet).Tables[0],
                            new ReportParameter[] { new ReportParameter("SelectedDate", EmpFilterItempProvider.SelectedDate.Value.ToString("MMMM yyyy")) }.ToList()
                            , System.Drawing.Printing.Duplex.Default, false);
                });
        }

        private void EmpAVGDayPrice_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectEmpAVGDayCalc.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            DateTime t = filterEmpProvider.SelectedDate.Value.Date;
            t = new DateTime(t.Year, t.Month, 1);
            a.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, SelectedTransferID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, filterEmpProvider.SelectedDate.Value.Date, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_count_month", OracleDbType.Decimal, e.Parameter, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            DataSet ds = new DataSet();
            try
            {
                a.Fill(ds);
                ViewReportWindow.ShowReport(this, "\"Расчет средневного дня\"", "Rep_EmpAVGDayFull.rdlc", new DataTable[] { ds.Tables[0], ds.Tables[1], ds.Tables[2] },
                    new ReportParameter[]{new ReportParameter("FIO", ds.Tables[3].Rows[0]["FIO"].ToString()),
                        new ReportParameter("PER_NUM", ds.Tables[3].Rows[0]["PER_NUM"].ToString()),
                        new ReportParameter("CODE_SUBDIV", ds.Tables[3].Rows[0]["CODE_SUBDIV"].ToString()),
                        new ReportParameter("DateBegin", t.AddMonths(-((int)e.Parameter)).ToString("dd/MM/yyyy")),
                        new ReportParameter("DateEnd", t.AddSeconds(-1).ToString("dd/MM/yyyy")),
                        new ReportParameter("CF_VAC", ds.Tables[3].Rows[0]["CF_VAC"].ToString())}.ToList(), System.Drawing.Printing.Duplex.Default, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException());
            }
        }

        private void EmpAVGDayPrice_Short_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("Rep_EmpAVGDayShort.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            DateTime t = filterEmpProvider.SelectedDate.Value.Date;
            t = new DateTime(t.Year, t.Month, 1);
            a.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, SelectedTransferID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, filterEmpProvider.SelectedDate.Value.Date, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            DataSet ds = new DataSet();
            try
            {
                a.Fill(ds);
                ViewReportWindow.ShowReport(this, "\"Расчет средневного дня\"", "Rep_EmpAVGDay_Short.rdlc", new DataTable[] { ds.Tables[0], ds.Tables[1], ds.Tables[2] },
                    new ReportParameter[]{new ReportParameter("FIO", ds.Tables[3].Rows[0]["FIO"].ToString()),
                        new ReportParameter("PER_NUM", ds.Tables[3].Rows[0]["PER_NUM"].ToString()),
                        new ReportParameter("CODE_SUBDIV", ds.Tables[3].Rows[0]["CODE_SUBDIV"].ToString()),
                        new ReportParameter("DateEnd", t.AddSeconds(-1).ToShortDateString()),
                        new ReportParameter("CF_VAC", ds.Tables[3].Rows[0]["CF_VAC"].ToString())}.ToList(), System.Drawing.Printing.Duplex.Default, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException());
            }
        }

        private void EmpMissionDayPrice_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepEmpMissionDayCalc.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            DateTime t = filterEmpProvider.SelectedDate.Value.Date;
            t = new DateTime(t.Year, t.Month, 1);
            a.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, SelectedTransferID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, filterEmpProvider.SelectedDate.Value.Date, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_count_month", OracleDbType.Decimal, e.Parameter, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            DataSet ds = new DataSet();
            try
            {
                a.Fill(ds);
                ViewReportWindow.ShowReport(this, "\"Расчет средней стоимости рабочего дня\"", "Rep_EmpMissionDayFull.rdlc", new DataTable[] { ds.Tables[0], ds.Tables[1], ds.Tables[2] },
                    new ReportParameter[]{new ReportParameter("FIO", ds.Tables[3].Rows[0]["FIO"].ToString()),
                        new ReportParameter("PER_NUM", ds.Tables[3].Rows[0]["PER_NUM"].ToString()),
                        new ReportParameter("CODE_SUBDIV", ds.Tables[3].Rows[0]["CODE_SUBDIV"].ToString()),
                        new ReportParameter("DateBegin", t.AddMonths(-((int)e.Parameter)).ToString("dd/MM/yyyy")),
                        new ReportParameter("DateEnd", t.AddSeconds(-1).ToString("dd/MM/yyyy"))}.ToList(), System.Drawing.Printing.Duplex.Default, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException());
            }
        }

        private void EmpMissionDayPrice_Short_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepEmpMissionDayCalc_Short.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            DateTime t = filterEmpProvider.SelectedDate.Value.Date;
            t = new DateTime(t.Year, t.Month, 1);
            a.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, SelectedTransferID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, filterEmpProvider.SelectedDate.Value.Date, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            DataSet ds = new DataSet();
            try
            {
                a.Fill(ds);
                ViewReportWindow.ShowReport(this, "\"Расчет средней стоимости рабочего дня (Сокращенный)\"", "Rep_EmpMIssionDay_Short.rdlc", new DataTable[] { ds.Tables[0], ds.Tables[1], ds.Tables[2] },
                    new ReportParameter[]{new ReportParameter("FIO", ds.Tables[3].Rows[0]["FIO"].ToString()),
                        new ReportParameter("PER_NUM", ds.Tables[3].Rows[0]["PER_NUM"].ToString()),
                        new ReportParameter("CODE_SUBDIV", ds.Tables[3].Rows[0]["CODE_SUBDIV"].ToString()),
                        new ReportParameter("DATE_END", t.AddSeconds(-1).ToShortDateString())}.ToList(), System.Drawing.Printing.Duplex.Default, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException());
            }
        }

        private void ReportEmp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && SelectedTransferID != null;
        }

        /// <summary>
        /// Сводный отчет (кассовый отчет)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_ConsolidSalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectRep_ConsolidSalary.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date1", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date2", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.BindByName = true;
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета", 
                (s, pw)=>
                    {
                        DataTable t = new DataTable();
                        (pw.Argument as OracleDataAdapter).Fill(t);
                        pw.Result = t;
                    },
                a, a.SelectCommand,
                (s, pw)=>
                    {
                        if (pw.Cancelled)
                            return;
                        else
                            if (pw.Error != null)
                                        MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования", MessageBoxButton.OK);
                            else
                                ViewReportWindow.ShowReport(this, "\"Сводный отчет по шифра оплат в подразделении\"", "Rep_ConsolidSalary.rdlc", pw.Result as DataTable,
                                    new ReportParameter[] { new ReportParameter("CODE_SUBDIV", CodeSubdiv), new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToString("MMMM yyyy")) }.ToList());
                    });
        }

        private void Rep_EmpSalaryListForAccount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectRep_EmpSalaryList.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date1", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date2", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("p_transfer_ids", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
            a.SelectCommand.BindByName = true;
            DataTable t = new DataTable();
            try
            {
                if (e.Parameter != null)
                {
                    a.SelectCommand.Parameters["p_transfer_ids"].Value = new Decimal[] { SelectedTransferID.Value };
                    a.Fill(t);
                    ViewReportWindow.ShowReport(this, "\"Расчетные листы сотрудников\"", "Rep_EmpSalaryList.rdlc", t,
                        new ReportParameter[] { new ReportParameter("CODE_SUBDIV", CodeSubdiv), new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToString("MMMM yyyy")) }.ToList()
                        , System.Drawing.Printing.Duplex.Default, false);
                }
                else
                {
                    RepFilterByEmp f = new RepFilterByEmp(EmpCollection.ToList(), EmpFilterItempProvider.SubdivID, EmpFilterItempProvider.SelectedDate.Value);
                    f.Owner = Window.GetWindow(this);
                    if (f.ShowDialog() == true)
                    {
                        if (f.BySubdivReport)
                        {
                            a.SelectCommand.Parameters["p_subdiv_id"].Value = f.SubdivID;
                        }
                        else
                            a.SelectCommand.Parameters["p_transfer_ids"].Value = f.SelectedRows.OfType<DataRowView>().Select(o => o.Row.Field<Decimal>("TRANSFER_ID")).ToArray();
                        a.SelectCommand.Parameters["p_date1"].Value = f.DateBegin;
                        a.SelectCommand.Parameters["p_date2"].Value = f.DateEnd;
                        AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета", 
                            (s, pm) =>
                            {
                                OracleDataAdapter ad = pm.Argument as OracleDataAdapter;
                                ad.Fill(t);
                            }, a, a.SelectCommand, 
                            (s, pm) =>
                            {

                                if (pm.Cancelled)
                                    return;
                                else
                                    if (pm.Error != null)
                                        MessageBox.Show(pm.Error.GetFormattedException(), "Ошибка формирования", MessageBoxButton.OK);
                                    else
                                        ViewReportWindow.ShowReport(this, "\"Расчетные листы сотрудников\"", "Rep_EmpSalaryList.rdlc", t,
                                            new ReportParameter[] { new ReportParameter("CODE_SUBDIV", CodeSubdiv), new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToString("MMMM yyyy")) }.ToList()
                                            , System.Drawing.Printing.Duplex.Default, false);
                            });                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования отчета");
            }
        }

        /// <summary>
        /// Справка по удержанию НДФЛ по заводу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_EmpTaxDiscount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("Rep_TaxDiscountEmp.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_transfer_ids", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName="SALARY.NUMBER_COLLECTION_TYPE";
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.BindByName = true;
            bool fl = true;
            if (e.Parameter != null && (int)e.Parameter == 1)
            {
                a.SelectCommand.Parameters["p_transfer_ids"].Value = new Decimal[] { SelectedTransferID.Value };
            }
            else
            {
                RepFilterByEmp f = new RepFilterByEmp(EmpCollection.ToList(), EmpFilterItempProvider.SubdivID, new DateTime(EmpFilterItempProvider.SelectedDate.Value.Year,
                    EmpFilterItempProvider.SelectedDate.Value.Month, 1), new DateTime(EmpFilterItempProvider.SelectedDate.Value.Year,
                        EmpFilterItempProvider.SelectedDate.Value.Month, 1).AddMonths(1).AddSeconds(-1));
                f.Owner = Window.GetWindow(this);
                if (f.ShowDialog() == true)
                {
                    if (f.BySubdivReport)
                        a.SelectCommand.Parameters["p_subdiv_id"].Value = f.SubdivID;
                    else
                        a.SelectCommand.Parameters["p_transfer_ids"].Value = f.SelectedRows.OfType<DataRowView>().Select(r => r.Row.Field2<Decimal>("Transfer_id")).ToArray();
                    a.SelectCommand.Parameters["p_date"].Value = f.DateEnd.Value;
                }
                else fl = true;
            }
            if (fl)
            {
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...", a, a.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "\"Справка по удержанию НДФЛ\"", "Rep_TaxDiscountEmp.rdlc", (pw.Result as DataSet).Tables[0],
                            new ReportParameter[] { new ReportParameter("CODE_SUBDIV", CodeSubdiv), 
                                    new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToShortDateString()) }.ToList());
                    });

            }
        }

        /// <summary>
        /// Отчет по шифрам оплат за период по подразделениям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_SalaryByPaymentType_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByPayment f = new FilterByPayment(EmpFilterItempProvider.SubdivID, EmpFilterItempProvider.SelectedDate.Value.Trunc("Month"), EmpFilterItempProvider.SelectedDate.Value.Trunc("Month").AddMonths(1).AddSeconds(-1));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepSalaryByPaymentType.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date1", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date2", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_payment_type_id", OracleDbType.Array, f.PaymentTypeIDs, ParameterDirection.Input).UdtTypeName="SALARY.NUMBER_COLLECTION_TYPE";
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                a.SelectCommand.BindByName = true;
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Загрузка данных",
                    (pw, p) =>
                    {
                        DataTable t = new DataTable();
                        (p.Argument as OracleDataAdapter).Fill(t);
                        p.Result = t;
                    }, a, a.SelectCommand,
                        (pw, p) =>
                        {
                            if (p.Cancelled) return;
                            else if (p.Error != null) MessageBox.Show(p.Error.GetFormattedException(), "Ошибка формирования данных");
                            else
                                ViewReportWindow.ShowReport(this, "\"Ведомость по шифру оплат за период\"",
                                    (e.Parameter== null ? "Rep_SalaryByPaymentInPeriod.rdlc": "Rep_SalaryByPaymentOverSubdiv.rdlc"), 
                                    p.Result as DataTable,
                                new ReportParameter[]{new ReportParameter("CODE_PAYMENT", f.CodePayment),
                                        new ReportParameter("P_DATE1", f.DateBegin.Value.ToString("MMMM yyyy")),
                                        new ReportParameter("P_DATE2", f.DateEnd.Value.ToString("MMMM yyyy")),
                                        new ReportParameter("CODE_SUBDIV", f.CodeSubdiv)}.ToList()
                                    , System.Drawing.Printing.Duplex.Default, false);
                        });
            }
        }

        private void Upload_Salary_540_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterPaymentChanges f = new FilterPaymentChanges(0, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateTime.Now.Date.AddSeconds(-1), new DateTime(2011,1,1), new DateTime(DateTime.Now.Year, DateTime.Now.Month,1).AddSeconds(-1));
            f.Owner= Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                IsPaymentBusy = true;
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += (et, eo)=>
                    {
                        OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectSalaryChanges.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                        a.SelectCommand.BindByName = true;
                        a.SelectCommand.Parameters.Add("p_payment_type_ids", OracleDbType.Array, f.Model.PaymentTypeIDs, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                        a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.Model.SubdivID, ParameterDirection.Input);
                        a.SelectCommand.Parameters.Add("p_pay_date_begin", OracleDbType.Date, f.Model.DateBegin, ParameterDirection.Input);
                        a.SelectCommand.Parameters.Add("p_pay_date_end", OracleDbType.Date, f.Model.DateEnd, ParameterDirection.Input);
                        a.SelectCommand.Parameters.Add("p_date1", OracleDbType.Date, f.Model.ChangeBegin, ParameterDirection.Input);
                        a.SelectCommand.Parameters.Add("p_date2", OracleDbType.Date, f.Model.ChangeEnd, ParameterDirection.Input);
                        DataTable t = new DataTable();
                        a.Fill(t);
                        var years = t.Rows.OfType<DataRow>().Select(r=>r.Field<Decimal>("YEAR")).Distinct().OrderBy(r=>r);
                        int k=0;
                        for (int i = 2001; i < 2030; ++i)
                        {
                            if (File.Exists(string.Format("{0}{1}.txt", Connect.parameters["SalaryChangeDir"], i)))
                            {
                                DateTime d = File.GetCreationTime(string.Format("{0}{1}.txt", Connect.parameters["SalaryChangeDir"], i));
                                File.Move(string.Format("{0}{1}.txt", Connect.parameters["SalaryChangeDir"], i), string.Format("{0}{1}_old_{2}.txt", Connect.parameters["SalaryChangeDir"], i,
                                    d.ToString("dd_MM_yyyy_hh_mm_ss")));
                            }
                        }
                        foreach(decimal y in years)
                        {
                            IEnumerable<string> sar = t.Rows.OfType<DataRow>().Where(r => r.Field<Decimal>("YEAR") == y).Select(r => r["SALARY_DATA"].ToString());
                            k += sar.Count();
                            File.WriteAllLines(string.Format("{0}{1}.txt",Connect.parameters["SalaryChangeDir"], y), sar);
                        }
                        eo.Result = new object[]{k, string.Join(", ",years.Select(r=>r.ToString()).ToArray())};
                    };
                bw.RunWorkerCompleted += (et, eo) => 
                { 
                    IsPaymentBusy = false;
                    if (eo.Error != null) MessageBox.Show(eo.Error.Message, "Ошибка формирования данных");
                    else MessageBox.Show(string.Format("Выгружено {0} записей. Года: {1}", (((object[])eo.Result)[0]), ((object[])eo.Result)[1]), "Сводная информация");
                };
                bw.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Отчет внесенных изменения  в ЗП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_SalaryChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterPaymentChanges f = new FilterPaymentChanges(0, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateTime.Now.Date.AddSeconds(-1), new DateTime(2011,1,1), new DateTime(DateTime.Now.Year, DateTime.Now.Month,1).AddSeconds(-1));
            f.Owner= Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                IsPaymentBusy = true;
                DataTable t = new DataTable();
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += (et, eo)=>
                    {
                        OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepSalaryChanges.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                        a.SelectCommand.BindByName = true;
                        a.SelectCommand.Parameters.Add("p_payment_type_ids", OracleDbType.Array, f.Model.PaymentTypeIDs, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                        a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.Model.SubdivID, ParameterDirection.Input);
                        a.SelectCommand.Parameters.Add("p_pay_date_begin", OracleDbType.Date, f.Model.DateBegin, ParameterDirection.Input);
                        a.SelectCommand.Parameters.Add("p_pay_date_end", OracleDbType.Date, f.Model.DateEnd, ParameterDirection.Input);
                        a.SelectCommand.Parameters.Add("p_date1", OracleDbType.Date, f.Model.ChangeBegin, ParameterDirection.Input);
                        a.SelectCommand.Parameters.Add("p_date2", OracleDbType.Date, f.Model.ChangeEnd, ParameterDirection.Input);
                        a.Fill(t);
                    };
                bw.RunWorkerCompleted += (et, eo) =>
                {
                    IsPaymentBusy = false; if (eo.Error != null) MessageBox.Show(eo.Error.Message, "Ошибка формирования данных");
                    else
                        ViewReportWindow.ShowReport(this, "Просмотр изменений внесенных в ЗП", "Rep_SalaryChanges.rdlc", t, new ReportParameter[]{
                            new ReportParameter("p_pay_dates", string.Format("с {0} по {1}", f.Model.DateBegin.Value.ToString("dd/MM/yyyy"), f.Model.DateEnd.Value.ToString("dd/MM/yyyy"))),
                            new ReportParameter("p_change_period", string.Format("с {0} по {1}", f.Model.ChangeBegin, f.Model.ChangeEnd)),
                            new ReportParameter("code_subdiv", f.Model.CodeSubdiv)}.ToList());
                };
                bw.RunWorkerAsync();
            }
        }

        private void RepSalNoteByPeriod_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DateTime dt = EmpFilterItempProvider.SelectedDate.Value;
            Tuple<int, int, string, string> tp_param = new Tuple<int, int, string, string>((int)(e.Parameter as object[])[0], (int)(e.Parameter as object[])[1], (string)(e.Parameter as object[])[2],
                    (string)(e.Parameter as Object[])[3]);
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery(tp_param.Item4), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, new DateTime(dt.Year, dt.Month, 1).AddMonths(-tp_param.Item2), ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, new DateTime(dt.Year, dt.Month, 1).AddSeconds(-1), ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("p_transfer_ids", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
            a.SelectCommand.BindByName = true;
            DataTable t = new DataTable();
            try
            {
                if (tp_param.Item1 == 0)
                {
                    a.SelectCommand.Parameters["p_transfer_ids"].Value = new decimal[] { SelectedTransferID.Value };
                    a.Fill(t);
                    SignesRecord[] st_arr = null;
                    if (Signes.Show(EmpFilterItempProvider.SubdivID, "SalaryNotePayment", "Выберите ответственных для подписи", 3, ref st_arr) == true)
                        ViewReportWindow.ShowReport(this, "\"Справка \"" + tp_param.Item3, "Rep_SalaryNotePayment.rdlc", t,
                            new ReportParameter[] { new ReportParameter("note_destination", tp_param.Item3),
                                        new ReportParameter("sign1", string.Format("{0} ________________________ {1}",st_arr[0].PosName, st_arr[0].EmpName)),
                                        new ReportParameter("sign2", string.Format("{0} ________________________ {1}",st_arr[1].PosName, st_arr[1].EmpName)),
                                        new ReportParameter("sign3", string.Format("{0} ________________________ {1}",st_arr[2].PosName, st_arr[2].EmpName)) }.ToList());
                }
                else
                {
                    
                    RepFilterByEmp f = new RepFilterByEmp(EmpCollection.ToList(), EmpFilterItempProvider.SubdivID, new DateTime(dt.Year, dt.Month,1).AddMonths(-tp_param.Item2),
                        new DateTime(dt.Year, dt.Month, 1).AddSeconds(-1));
                    f.Owner = Window.GetWindow(this);
                    if (f.ShowDialog() == true)
                    {
                        if (f.BySubdivReport)
                            a.SelectCommand.Parameters["p_subdiv_id"].Value = f.SubdivID;
                        else
                            a.SelectCommand.Parameters["p_transfer_ids"].Value = f.SelectedRows.OfType<DataRowView>().Select(o => o.Row.Field<Decimal>("TRANSFER_ID")).ToArray();
                        a.SelectCommand.Parameters["p_date_begin"].Value = f.DateBegin;
                        a.SelectCommand.Parameters["p_date_end"].Value = f.DateEnd;
                        a.Fill(t);
                        SignesRecord[] st_arr=null;
                        if (Signes.Show(f.SubdivID, "SalaryNotePayment", "Выберите ответственных для подписи", 3, ref st_arr)==true)
                            ViewReportWindow.ShowReport(this, "\"Справка \"" + tp_param.Item3, "Rep_SalaryNotePayment.rdlc", t, 
                                new ReportParameter[] { new ReportParameter("note_destination", tp_param.Item3),
                                    new ReportParameter("sign1", string.Format("{0} ________________________ {1}",st_arr[0].PosName, st_arr[0].EmpName)),
                                    new ReportParameter("sign2", string.Format("{0} ________________________ {1}",st_arr[1].PosName, st_arr[1].EmpName)),
                                    new ReportParameter("sign3", string.Format("{0} ________________________ {1}",st_arr[2].PosName, st_arr[2].EmpName))}.ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования отчета");
            }
        }

        private void RepSalNoteAlimonyByPeriod_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepAlimonyNoteRetent.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("p_transfer_ids", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
            a.SelectCommand.BindByName = true;
            DataTable t = new DataTable();
            int tp_param = (int)e.Parameter;
            try
            {
                DateTime dt = EmpFilterItempProvider.SelectedDate.Value;
                RepFilterByEmp f = new RepFilterByEmp(EmpCollection.ToList(), EmpFilterItempProvider.SubdivID, new DateTime(dt.Year, dt.Month, 1).AddMonths(-tp_param), new DateTime(dt.Year, dt.Month, 1).AddSeconds(-1));
                f.Owner = Window.GetWindow(this);
                if (f.ShowDialog() == true)
                {
                    if (f.BySubdivReport)
                    {
                        a.SelectCommand.Parameters["p_subdiv_id"].Value = f.SubdivID;
                    }
                    else
                        a.SelectCommand.Parameters["p_transfer_ids"].Value = f.SelectedRows.OfType<DataRowView>().Select(o => o.Row.Field<Decimal>("TRANSFER_ID")).ToArray();
                    a.SelectCommand.Parameters["p_date_begin"].Value = f.DateBegin;
                    a.SelectCommand.Parameters["p_date_end"].Value = f.DateEnd;
                    a.Fill(t);
                    SignesRecord[] st_arr = null;
                    if (Signes.Show(f.SubdivID, "AlimonyNoteRetent", "Выберите ответственных для подписи", 2, ref st_arr) == true)
                        ViewReportWindow.ShowReport(this, "\"Справка удержания алиментов\"", "Rep_AlimonyNoteRetent.rdlc", t,
                            new ReportParameter[] { 
                                new ReportParameter("sign1", string.Format("{0} ____________________ {1}",st_arr[0].PosName, st_arr[0].EmpName)),
                                new ReportParameter("sign2", string.Format("{0} ____________________ {1}",st_arr[1].PosName, st_arr[1].EmpName))});
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования отчета");
            }
        }

        private void SalaryTabForPrint_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                RepFilterByEmp f = new RepFilterByEmp(EmpCollection.ToList(), EmpFilterItempProvider.SubdivID, new DateTime(EmpFilterItempProvider.SelectedDate.Value.Year,
                    EmpFilterItempProvider.SelectedDate.Value.Month, 1), new DateTime(EmpFilterItempProvider.SelectedDate.Value.Year,
                        EmpFilterItempProvider.SelectedDate.Value.Month, 1).AddMonths(1).AddSeconds(-1));
                f.Owner = Window.GetWindow(this);
                if (f.ShowDialog() == true)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.AddExtension = true;
                    saveDialog.CreatePrompt = true;
                    saveDialog.DefaultExt = "txt";
                    saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|(*.*)";
                    if (saveDialog.ShowDialog()==true)
                    {
                        OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectTabForPrint.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                        a.SelectCommand.BindByName = true;
                        a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
                        a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, f.DateEnd.Value, ParameterDirection.Input);
                        a.SelectCommand.Parameters.Add("p_transfer_ids", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                        a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                        if (f.BySubdivReport)
                            a.SelectCommand.Parameters["p_subdiv_id"].Value = f.SubdivID;
                        else
                            a.SelectCommand.Parameters["p_transfer_ids"].Value = f.SelectedRows.OfType<DataRowView>().Select(p => p.Row.Field2<Decimal>("Transfer_id")).ToArray();
                        string filePath = saveDialog.FileName;
                        BackgroundWorker bw = new BackgroundWorker();
                        AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных...", 
                        (et, eo)=>
                            {
                                OracleDataAdapter aa = eo.Argument as OracleDataAdapter;
                                DataTable t = new DataTable();
                                aa.Fill(t);
                                File.WriteAllLines(filePath, t.Rows.OfType<DataRow>().Select(r => r[0].ToString()), Encoding.GetEncoding(866));
                                eo.Result = filePath;
                            }, a, a.SelectCommand,
                            (sd, eo) =>
                            {
                                if (eo.Error != null)
                                    MessageBox.Show(eo.Error.GetFormattedException(), "Ошибка формирования файла");
                                else
                                    (Window.GetWindow(this) as MainWindow).OpenTabs.AddNewTab("Просмотр файла отчета", new TextReportViewerLibrary.TextViewer() { FileSource = (string)eo.Result });
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования");
            }
        }

        private void RepDuesAllSubdivFundes_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepDuesAllFundesByPeriod.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.BindByName = true;
            try
            {
                RepFilterByEmp f = new RepFilterByEmp(EmpCollection.ToList(), EmpFilterItempProvider.SubdivID, new DateTime(EmpFilterItempProvider.SelectedDate.Value.Year, 1, 1), EmpFilterItempProvider.SelectedDate.Value, false);
                f.Owner = Window.GetWindow(this);
                if (f.ShowDialog() == true)
                {
                    if (f.BySubdivReport)
                    {
                        a.SelectCommand.Parameters["p_subdiv_id"].Value = f.SubdivID;
                    }
                    a.SelectCommand.Parameters["p_date_begin"].Value = f.DateBegin;
                    a.SelectCommand.Parameters["p_date_end"].Value = f.DateEnd;
                    AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Сбор данных. Ожидайте...",
                        (s, pw) =>
                        {
                            OracleDataAdapter aa = pw.Argument as OracleDataAdapter;
                            DataTable t = new DataTable();
                            aa.Fill(t);
                            pw.Result = t;
                        },
                            a, a.SelectCommand,
                            (s, pw) =>
                            {
                                if (pw.Cancelled) return;
                                else if (pw.Error != null)
                                    MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования данных");
                                else
                                    ViewReportWindow.ShowReport(this, "\"Взносы внебюджентные фонды за период\"", "Rep_DuesAllSubdivAllFundes.rdlc", pw.Result as DataTable,
                                        new ReportParameter[] { new ReportParameter("CODE_SUBDIV", f.SubdivSelector1.CodeSubdiv), new ReportParameter("P_DATE1", f.DateEnd.Value.ToShortDateString()) }
                                        , System.Drawing.Printing.Duplex.Default, false);
                            });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования отчета");
            }
        }

        private void RepDuesSubdivFundes_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepDuesAllFundesByPeriod.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, EmpFilterItempProvider.SelectedDate.Value.Trunc("Year"), ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.BindByName = true;
            try
            {
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Сбор данных. Ожидайте...",
                    (s, pw) =>
                    {
                        OracleDataAdapter aa = pw.Argument as OracleDataAdapter;
                        DataTable t = new DataTable();
                        aa.Fill(t);
                        pw.Result = t;
                    },
                        a, a.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            else if (pw.Error != null)
                                MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования данных");
                            else
                                ViewReportWindow.ShowReport(this, "\"Взносы внебюджентные фонды по подразделению\"", "Rep_DuesBySubdiv.rdlc", pw.Result as DataTable,
                                    new ReportParameter[] {new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToShortDateString()) });
                        });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования отчета");
            }
        }

        
        private void RepDuesNotRetPaymentAllSubdiv_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepDuesNotRetentPayment.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.BindByName = true;
            DataTable t = new DataTable();
            try
            {
                RepFilterByEmp f = new RepFilterByEmp(EmpCollection.ToList(), EmpFilterItempProvider.SubdivID, new DateTime(EmpFilterItempProvider.SelectedDate.Value.Year, 1, 1), EmpFilterItempProvider.SelectedDate.Value, false, false, true);
                f.Owner = Window.GetWindow(this);
                if (f.ShowDialog() == true)
                {
                    if (f.BySubdivReport)
                    {
                        a.SelectCommand.Parameters["p_subdiv_id"].Value = f.SubdivID;
                    }
                    a.SelectCommand.Parameters["p_date_end"].Value = f.DateEnd;
                    a.Fill(t);
                    ViewReportWindow.ShowReport(this, "\"Необлагаемые суммы по взносам за период\"", "Rep_DuesNotRetentPayment.rdlc", t,
                        new ReportParameter[] { new ReportParameter("P_DATE", f.DateEnd.Value.ToShortDateString()) }.ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования отчета");
            }
        }

        private void RepDuesOverLimit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepDuesOverLimitSum.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.BindByName = true;
            DataTable t = new DataTable();
            try
            {
                RepFilterByEmp f = new RepFilterByEmp(EmpCollection.ToList(), EmpFilterItempProvider.SubdivID, new DateTime(EmpFilterItempProvider.SelectedDate.Value.Year, 1, 1), EmpFilterItempProvider.SelectedDate.Value, false, true, true);
                f.Owner = Window.GetWindow(this);
                if (f.ShowDialog() == true)
                {
                    if (f.BySubdivReport)
                    {
                        a.SelectCommand.Parameters["p_subdiv_id"].Value = f.SubdivID;
                    }
                    a.SelectCommand.Parameters["p_date_begin"].Value = f.DateBegin;
                    a.SelectCommand.Parameters["p_date_end"].Value = f.DateEnd;
                    a.Fill(t);
                    ViewReportWindow.ShowReport(this, "\"Суммы превышения предельной базы взносов по сотрудникам\"", "Rep_DuesOverLimitRetent.rdlc", t,
                        new ReportParameter[] { 
                            new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()), 
                            new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString()),
                            new ReportParameter("CODE_SUBDIV", f.SubdivSelector1.CodeSubdiv) 
                        }.ToList()
                        , System.Drawing.Printing.Duplex.Default, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования отчета");
            }
        }

        private void RepDuesListInvalid_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepDuesListInvalid.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.BindByName = true;
            DataTable t = new DataTable();
            try
            {
                RepFilterByEmp f = new RepFilterByEmp(EmpCollection.ToList(), EmpFilterItempProvider.SubdivID, new DateTime(EmpFilterItempProvider.SelectedDate.Value.Year, 1, 1), EmpFilterItempProvider.SelectedDate.Value, false, true, true);
                f.Owner = Window.GetWindow(this);
                if (f.ShowDialog() == true)
                {
                    if (f.BySubdivReport)
                    {
                        a.SelectCommand.Parameters["p_subdiv_id"].Value = f.SubdivID;
                    }
                    a.SelectCommand.Parameters["p_date_begin"].Value = f.DateBegin;
                    a.SelectCommand.Parameters["p_date_end"].Value = f.DateEnd;
                    a.Fill(t);
                    ViewReportWindow.ShowReport(this, "\"Список работников-инвалидов\"", "Rep_DuesInvalidData.rdlc", t,
                        new ReportParameter[] { 
                            new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()), 
                            new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString()),
                            new ReportParameter("CODE_SUBDIV", f.SubdivSelector1.CodeSubdiv) 
                        }.ToList()
                        , System.Drawing.Printing.Duplex.Default, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования отчета");
            }
        }

        private void UnloadAVGDuesPercent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           if (MessageBox.Show(string.Format("Сформировать файл среднего процента по ЕСН за {0:MMMM} месяц {0:yyyy} года?", EmpFilterItempProvider.SelectedDate.Value), "Выгрузка данных",  MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
           {
            DataTable t = new DataTable();
            IsPaymentBusy = true;
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (sd, et) =>
                {
                    OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("UnloadAVGDuesPercent.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                    a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, et.Argument, ParameterDirection.Input);
                    a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                    a.SelectCommand.BindByName = true;
                    a.Fill(t);
                };
            bw.RunWorkerCompleted+=(et, eo) =>
                {
                    IsPaymentBusy=false;
                    if (eo.Error != null) 
                        MessageBox.Show(eo.Error.Message, "Ошибка формирования данных");
                    else
                    {
                        SaveFileDialog sf = new SaveFileDialog();
                        sf.Filter="Текстовые файлы (*.txt)|*.txt";
                        sf.InitialDirectory = Connect.parameters["AVGDuesFileDir"];
                        sf.FileName = string.Format("{0}presn{1:00}.txt", Connect.parameters["AVGDuesFileDir"], EmpFilterItempProvider.SelectedDate.Value.Month);
                        if (sf.ShowDialog() == true)
                        {
                            try
                            {
                                File.WriteAllLines(sf.FileName, t.Rows.OfType<DataRow>().Select(r => r[0].ToString()));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Ошибка создания файла");
                            }
                        }
                    }
                };
            bw.RunWorkerAsync(EmpFilterItempProvider.SelectedDate);
           }
        }

        private void UnloadSalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(string.Format("Начать выгрузку данных за {0} по подразделению {1}? По завершению выгрузки вам будет предложено выбрать файл назначения",
                    EmpFilterItempProvider.SelectedDate.Value.ToString("MMMM yyyy"), EmpFilterItempProvider.CodeSubdiv), "Выгрузка данных",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            { 
                OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("UnloadSalary.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных в текстовый формат",
                    (s, pw) =>
                    {
                        DataTable t = new DataTable();
                        (pw.Argument as OracleDataAdapter).Fill(t);
                        pw.Result = t;
                    },
                        a, a.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled)
                                return;
                            else
                                if (pw.Error != null)
                                    MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования данных");
                                else
                                {
                                    SaveFileDialog sf = new SaveFileDialog();
                                    sf.DefaultExt = "txt";
                                    sf.Filter = "Текстовые файлы (*.txt)|*.txt";
                                    sf.InitialDirectory = Connect.parameters["AlimonyFileDir"];
                                    if (sf.ShowDialog(Window.GetWindow(this)) == true)
                                    {
                                        try
                                        {
                                            File.WriteAllLines(sf.FileName, (pw.Result as DataTable).Rows.OfType<DataRow>().Select(w => w[0].ToString()), Encoding.GetEncoding(1251));
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message, "Ошибка формирования файла");
                                        }
                                    }
                                };
                        });

            }
        }

        private void Rep_EmpDieSalaryNote_executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("Rep_EmpDieSalaryNote.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            DateTime t = filterEmpProvider.SelectedDate.Value.Date;
            t = new DateTime(t.Year, t.Month, 1);
            a.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, SelectedTransferID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, filterEmpProvider.SelectedDate.Value.Date, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, filterEmpProvider.SelectedDate.Value.Date, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            a.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            RepFilterByEmp f = new RepFilterByEmp(new List<DataRowView> { }, null, new DateTime(t.Year, 1,1).AddMonths(-24), t.AddMonths(1).AddSeconds(-1), false);
            f.Owner = Window.GetWindow(this);
            f.IsSubdivAllowed = false;
            if (f.ShowDialog() == true)
            {
                SignesRecord[] st=null;
                if (Signes.Show(0, "SalaryDieNote", "Выберите ответственных для подписи", 2, ref st) == true)
                {
                    DataTable tb = new DataTable();
                    tb.Columns.Add("POS_NAME");
                    tb.Columns.Add("FIO1");
                    tb.Columns.Add("RNUMBER");
                    for (int i = 0; i < st.Length; ++i)
                    {
                        tb.Rows.Add(st[i].PosName, st[i].EmpName, st[i].OrderNumber);
                    }
                    a.SelectCommand.Parameters["p_date_begin"].Value = f.DateBegin;
                    a.SelectCommand.Parameters["p_date_end"].Value = f.DateEnd;
                    DataSet ds = new DataSet();
                    try
                    {
                        a.Fill(ds);
                        ViewReportWindow.ShowReport(this, "\"Справка о доходе за предшествующие годы для расчета больничного\"", "Rep_EmpDieSalaryNote.rdlc", new DataTable[] { ds.Tables[0], ds.Tables[1], ds.Tables[2], ds.Tables[3], tb },
                            null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.GetFormattedException());
                    }
                }
            }
        }

        private void RepSubRetentDocs_Short_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByPayment f = new FilterByPayment(EmpFilterItempProvider.SubdivID, new DateTime(EmpFilterItempProvider.SelectedDate.Value.Year,
                    EmpFilterItempProvider.SelectedDate.Value.Month, 1), new DateTime(EmpFilterItempProvider.SelectedDate.Value.Year,
                        EmpFilterItempProvider.SelectedDate.Value.Month, 1).AddMonths(1).AddSeconds(-1), new int[]{9});
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepSubEmpRetentDocs.sql"), Connect.SchemaApstaff, Connect.SchemaSalary),
                        Connect.CurConnect);
                DataTable t = new DataTable();
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date1", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date2", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                a.SelectCommand.Parameters.Add("p_payment_type_ids", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                a.SelectCommand.Parameters["p_payment_type_ids"].Value = f.PaymentTypeIDs.ToArray();
                try
                {
                    a.Fill(t);
                    Reports.ViewReportWindow.ShowReport(this, "\"Документы удержания сотрудников в подразделении\"", "Rep_SubRetentDocs.rdlc", t,
                        new ReportParameter[] { new ReportParameter("CODE_SUBDIV", f.SubdivSelector1.CodeSubdiv), new ReportParameter( "P_DATE", f.DateBegin.Value.Date.ToShortDateString())}.ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException());
                }
            }
        }

        /// <summary>
        /// Протокол возможных ошибок в заработной плате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryErrors_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataTable t = new DataTable();
            try
            {
                new OracleDataAdapter(string.Format(Queries.GetQuery("SelectTypeErrors.sql"), Connect.SchemaApstaff,Connect.SchemaSalary), Connect.CurConnect).Fill(t);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки списка ошибок");
                return;
            }
            DataGridColumn[] columns= new DataGridColumn[]{ new DataGridTextColumn(){ IsReadOnly = true, Binding = new Binding("TYPE_SALARY_ERROR_ID"), Header="№", Width=50},
                      new DataGridTextColumn(){ IsReadOnly = true, Binding = new Binding("ERROR_NAME"), Header="Наименование", Width=DataGridLength.Auto}};  
            FilterByList f =  new FilterByList(t.DefaultView.OfType<DataRowView>(),  columns);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                ReportsEmpPayments.GetEmpErrors(this, EmpFilterItempProvider.SubdivID , EmpFilterItempProvider.SelectedDate, f.SelectedRows.OfType<DataRowView>().Select(tr => tr.Row.Field2<Decimal>("TYPE_SALARY_ERROR_ID")).ToArray());
            }
        }

        private void Rep_AddPremiumRegister_executed(object sender, ExecutedRoutedEventArgs e)
        {
            decimal k = (decimal)AppDataSet.Tables["PAYMENT_TYPE"].Compute("MAX(PAYMENT_TYPE_ID)", "CODE_PAYMENT=" + e.Parameter.ToString());
            Salary.Reports.ReportsAddPrem.AddPremRegister(this, EmpFilterItempProvider.SelectedDate.Value, EmpFilterItempProvider.SubdivID, k, e.Parameter);
        }

        private void RepSubEmpTransferRetent_executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataTable t = new DataTable();
            try
            {
                new OracleDataAdapter(string.Format(Queries.GetQuery("SelectReportGroupTransferSum.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect).Fill(t);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки списка ошибок");
                return;
            }
            DataGridColumn[] columns = new DataGridColumn[]{ new DataGridTextColumn(){ IsReadOnly = true, Binding = new Binding("GROUP_NAME"), Header="Наименование", Width=DataGridLength.Auto}};
            FilterByList f = new FilterByList(t.DefaultView.OfType<DataRowView>(), columns);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectEmpSalaryTransferByDoc.sql"), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_groups", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                a.SelectCommand.Parameters["p_groups"].Value = f.SelectedRows.OfType<DataRowView>().Select(tr => tr.Row.Field2<Decimal>("REPORT_GROUP_ID")).ToArray();
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Загрузка данных",
                    (s, pw) =>
                    {
                        OracleDataAdapter aa = pw.Argument as OracleDataAdapter;
                        DataTable tb = new DataTable();
                        aa.Fill(tb);
                        pw.Result = tb;
                    },
                        a, a.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            else if (pw.Error != null)
                                MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Отчет о перечисляемых удержаниях", "Rep_EmpSalaryTransferByDoc.rdlc", (pw.Result as DataTable), 
                                    new ReportParameter[]{new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToShortDateString())}.ToList());
                        });
            }
        }

        private void RepRetentByOrdersEmp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_SalByOrdersEmps.sql"), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Загрузка данных",
                (s, pw) =>
                {
                    OracleDataAdapter aa = pw.Argument as OracleDataAdapter;
                    DataTable tb = new DataTable();
                    aa.Fill(tb);
                    pw.Result = tb;
                },
                    a, a.SelectCommand,
                    (s, pw) =>
                    {
                        if (pw.Cancelled) return;
                        else if (pw.Error != null)
                            MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                        else
                            ViewReportWindow.ShowReport(this, "Ведомость по заказам и сотрудникам", "Rep_EmpSalaryWithOrders.rdlc", (pw.Result as DataTable),
                                new ReportParameter[] { new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToShortDateString()) }.ToList());
                    });
        }


        private void Rep_TableCompare_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByPayment f = new FilterByPayment(EmpFilterItempProvider.SubdivID, EmpFilterItempProvider.SelectedDate.Value.Trunc("month"), EmpFilterItempProvider.SelectedDate.Value.Trunc("month"), new int[] { 1 });
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                if (f.DateBegin.Value.ToString("MMyyyy") != f.DateEnd.Value.ToString("MMyyyy"))
                    MessageBox.Show("Данный отчет можно формировать только в пределах одного месяца. Месяц для формирование выбран как дата начала установленного вами периода");
                if ((f.SubdivID ?? 0) == 0)
                {
                    MessageBox.Show("Данные отчет невозможно формировать для всего завода в целом");
                    return;
                }
                OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_TableCompare.sql"), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_payment_type_ids", OracleDbType.Array, f.PaymentTypeIDs, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование и загрузка данных",
                    (s, pw) =>
                    {
                        OracleDataAdapter aa = pw.Argument as OracleDataAdapter;
                        DataTable tb = new DataTable();
                        aa.Fill(tb);
                        pw.Result = tb;
                    },
                        a, a.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            else if (pw.Error != null)
                                MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Сравнение расчетов табеля", "Rep_TableCompare.rdlc", (pw.Result as DataTable),
                                    new ReportParameter[] { new ReportParameter("P_DATE", f.DateBegin.Value.ToShortDateString()) });
                        });
            }
        }

        private void RepDuesAvgHeadFundes_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Rep_DistributionSalaryHead(this, EmpFilterItempProvider.SubdivID, EmpFilterItempProvider.SelectedDate);
        }

        /// <summary>
        /// Формирование данных по главной странице производственного отчета
        /// </summary>
        /// <param name="subdiv_id"></param>
        /// <param name="selectedDate"></param>
        public static void Rep_DistributionSalaryHead(DependencyObject sender, decimal? subdiv_id, DateTime? selectedDate)
        {
            OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_SalaryDuesHead.sql"), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, subdiv_id, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, selectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(sender, "Формирование данных",
                (s, pw) =>
                {
                    OracleDataAdapter aa = pw.Argument as OracleDataAdapter;
                    DataTable tb = new DataTable();
                    aa.Fill(tb);
                    pw.Result = tb;
                },
                    a, a.SelectCommand,
                    (s, pw) =>
                    {
                        if (pw.Cancelled) return;
                        else if (pw.Error != null)
                            MessageBox.Show(Window.GetWindow(sender), pw.Error.GetFormattedException(), "Ошибка получения данных");
                        else
                            ViewReportWindow.ShowReport(sender, "Отчет по заработной плате", "Rep_DuesHeadReport.rdlc", (pw.Result as DataTable),
                                new ReportParameter[] { new ReportParameter("P_DATE", selectedDate.Value.ToShortDateString()) }.ToList());
                    });
        }

        private void RepDuesHarmProffession_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_SelectDuesHarmProff.sql"), Connect.CurConnect);
            a.SelectCommand.BindByName = true;
            a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных",
                (s, pw) =>
                {
                    OracleDataAdapter aa = pw.Argument as OracleDataAdapter;
                    DataTable tb = new DataTable();
                    aa.Fill(tb);
                    pw.Result = tb;
                },
                    a, a.SelectCommand,
                    (s, pw) =>
                    {
                        if (pw.Cancelled) return;
                        else if (pw.Error != null)
                            MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                        else
                            ViewReportWindow.ShowReport(this, "Взносы по льготным профессиям", "Rep_DuesHarmProffession.rdlc", (pw.Result as DataTable),
                                new ReportParameter[] { new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToShortDateString()),
                                new ReportParameter("P_CODE_SUBDIV", EmpFilterItempProvider.CodeSubdiv)}.ToList());
                    });
        }

        private void RepConsolidVacReport_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepSelectVacConsolid.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                DataTable t = new DataTable();
                a.Fill(t);
                Reports.ViewReportWindow.ShowReport(this, "\"Сводная ведомость отпускных за месяц\"", "Rep_ConsolidVacsMonth.rdlc", t,
                    new ReportParameter[]{ new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToShortDateString()),
                                           new ReportParameter("P_CODE_SUBDIV", EmpFilterItempProvider.CodeSubdiv)}.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException());
            }
        }

        private void EmpAvgSickDayPrice_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            YearsSelector f = new YearsSelector();
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_EmpAvgSickDay.sql"), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, SelectedTransferID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date1", OracleDbType.Date, f.Year1, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date2", OracleDbType.Date, f.Year2, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                a.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных",
                    (pw, s) =>
                    {
                        DataSet ds = new DataSet();
                        (s.Argument as OracleDataAdapter).Fill(ds);
                        s.Result = ds;
                    }, a, a.SelectCommand,
                        (pw, s) =>
                        {
                            if (s.Cancelled) return;
                            else if (s.Error != null) MessageBox.Show(s.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Просмотр отчета \"Расчет среднего для больничного\"", "Rep_EmpAvgSickDay.rdlc",
                                    new DataTable[] { (s.Result as DataSet).Tables[0], (s.Result as DataSet).Tables[1], new DataTable(), new DataTable() },
                                    new ReportParameter[] { new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToShortDateString()),
                                        new ReportParameter("P_HIDE_SUM", "True")});
                        });
            }
        }

        /// <summary>
        /// Ведомость по авансу для подразделения. можно либо обычную печать, либо через АЦПУ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepAdvanceMainReport_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
            {
                OracleDataAdapter a = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_AdvanceSubdiv.sql"), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных",
                    (s, pw) =>
                    {
                        OracleDataAdapter aa = pw.Argument as OracleDataAdapter;
                        DataTable tb = new DataTable();
                        aa.Fill(tb);
                        pw.Result = tb;
                    },
                        a, a.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            else if (pw.Error != null)
                                MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Ведомость аванса", "Rep_AdvanceSubdiv.rdlc", (pw.Result as DataTable),
                                    new ReportParameter[] { new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToShortDateString()),
                                new ReportParameter("P_CODE_SUBDIV", EmpFilterItempProvider.CodeSubdiv)}.ToList());
                        });
            }
            else
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format("begin {1}.SALARY_TXT_REPORTS.SelectSalaryAdvancePayment(:p_subdiv_id, :p_date, :c);end;", Connect.SchemaApstaff, Connect.SchemaSalary),
                    Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета",
                    (s, pw) =>
                    {
                        DataTable t = new DataTable();
                        a.Fill(t);
                        pw.Result = t;
                    }, a, a.SelectCommand,
                        (p, s) =>
                        {
                            if (s.Cancelled) return;
                            else if (s.Error != null) MessageBox.Show(s.Error.GetFormattedException(), "Ошибка формирования отчета");
                            else
                            {
                                System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
                                sf.DefaultExt = "TXT";
                                sf.Filter = "Текстовые файлы (.txt)|*.txt";
                                sf.InitialDirectory = Connect.parameters["ReportDirInit"];
                                sf.FileName = Connect.parameters["AdvanceReportName"];
                                sf.OverwritePrompt = false;
                                if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
                                {
                                    try
                                    {
                                        if (!File.Exists(sf.FileName))
                                        {
                                            FileStream f = File.Create(sf.FileName);
                                            f.Close();
                                        }
                                        File.AppendAllLines(sf.FileName, (s.Result as DataTable).Rows.OfType<DataRow>().Select(w => w[0].ToString()), Encoding.GetEncoding(866));
                                        MessageBox.Show("Записи успешно добавлены в файл для печати!");
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, "Ошибка записи в файл");
                                    }
                                }
                            }
                        });
            }
        }

        /// <summary>
        /// Платежная ведомость по авансу в кассу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepAdvanceCacheReport_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("RepAdvancePayT-53.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных",
                (s, pw) =>
                {
                    OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                    DataTable t = new DataTable();
                    a.Fill(t);
                    pw.Result = t;
                },
                    oda, oda.SelectCommand,
                (s, pw) =>
                {
                    if (pw.Cancelled) return;
                    if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                    else
                        ViewReportWindow.ShowReport(this, "Платежная ведомость", "Rep_PayForm_T-53.rdlc", pw.Result as DataTable, new ReportParameter[]{
                                new ReportParameter("P_TYPE_PAY_FORM", "АВАНСА"), new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToShortDateString())});
                });
        }

        /// <summary>
        /// Выгрузка текстовой печати аванса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnloadAdvanceData_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Выгрузить в текстовый файл платежную ведомость?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
                sf.Filter = "Текстовые файлы (.txt)|*.txt";
                sf.InitialDirectory = Connect.parameters["KassaDirectory"];
                sf.FileName = string.Format("avans{0:00}", EmpFilterItempProvider.SelectedDate.Value.Month);
                if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectAdvancePayTxt.sql"), Connect.CurConnect);
                        oda.SelectCommand.BindByName = true;
                        oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
                        oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                        DataTable t = new DataTable();
                        oda.Fill(t);
                        File.WriteAllLines(sf.FileName, t.Rows.OfType<System.Data.DataRow>().Select(r => r[0].ToString()), Encoding.GetEncoding(866));
                        MessageBox.Show(string.Format("Файл успешно сформирован. Выгружено {0}  записей на общую сумму {1:N2}", t.Rows.Count, t.Compute("SUM(SUM_SAL)","")), "Зарплата предприятия");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка формирования");
                    }
                }
            }
        }

        /// <summary>
        /// Карточка учета взносов. Очень сложный млять отчет, потому что дохера данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_EmpDueCard_executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_EmpDueCard.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, SelectedTransferID, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных",
                (s, pw) =>
                {
                    OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                    DataTable t = new DataTable();
                    a.Fill(t);
                    pw.Result = t;
                },
                    oda, oda.SelectCommand,
                (s, pw) =>
                {
                    if (pw.Cancelled) return;
                    if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                    else
                        ViewReportWindow.ShowReport(this, "Карточка учета взносов", "Rep_DuesEmpCard.rdlc", pw.Result as DataTable, new ReportParameter[]{new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToShortDateString())});
                });
        }

        /// <summary>
        /// распечатка средней стоимости рабочего дня и часа через АЦПУ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">в параметре передается название файла что именно выполнить</param>
        private void RepAVGPrintACPD_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RepFilterByEmp f = new RepFilterByEmp(EmpCollection.ToList(), EmpFilterItempProvider.SubdivID, EmpFilterItempProvider.SelectedDate.Value);
            f.Owner = Window.GetWindow(this);
            f.IsSubdivAllowed = false;
            f.AllowBegin = Visibility.Collapsed;
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format("begin {1}.SALARY_TXT_REPORTS.{2}(:p_worker_ids, :p_date, :c);end;", Connect.SchemaApstaff, Connect.SchemaSalary, e.Parameter),
                        Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_worker_ids", OracleDbType.Array, f.SelectedRows.OfType<DataRowView>().Select(r => r.Row.Field2<Decimal>("WORKER_ID")).ToArray(), ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета",
                    (s, pw) =>
                    {
                        DataTable t = new DataTable();
                        a.Fill(t);
                        pw.Result = t;
                    }, a, a.SelectCommand,
                        (p, s) =>
                        {
                            if (s.Cancelled) return;
                            else if (s.Error != null) MessageBox.Show(s.Error.GetFormattedException(), "Ошибка формирования отчета");
                            else
                            {
                                System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
                                sf.DefaultExt = "TXT";
                                sf.Filter = "Текстовые файлы (.txt)|*.txt";
                                sf.InitialDirectory = Connect.parameters["ReportDirInit"];
                                sf.FileName = Connect.parameters["OtherAVGPrintFileName"];
                                sf.OverwritePrompt = false;
                                if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
                                {
                                    try
                                    {
                                        if (!File.Exists(sf.FileName))
                                        {
                                            FileStream fs = File.Create(sf.FileName);
                                            fs.Close();
                                        }
                                        File.AppendAllLines(sf.FileName, (s.Result as DataTable).Rows.OfType<DataRow>().Select(w => w[0].ToString()), Encoding.GetEncoding(866));
                                        MessageBox.Show("Записи успешно добавлены в файл для печати!");
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, "Ошибка записи в файл");
                                    }
                                }
                            }
                        });
            }
        }

        /// <summary>
        /// распечатка больничных  через АЦПУ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepPrintSalDocACPD_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            try
            {
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter("SelectSalaryDocumMonthACPD.sql", EmpFilterItempProvider, FilterParameter.p_date, FilterParameter.p_subdiv_id);
                DataTable t = new DataTable();
                oda.Fill(t);
                DataGridColumn[] d = new
                    DataGridColumn[]{
                    new DataGridTextColumn(){Binding = new Binding("PER_NUM"), Header="Таб.№", Width=80},
                    new DataGridTextColumn(){Binding = new Binding("FIO"), Header="Ф.И.О.", Width=150},
                    new DataGridTextColumn(){Binding = new Binding("DOC_BEGIN"){StringFormat="d"}, Header="Дата начала", Width=100},
                    new DataGridTextColumn(){Binding = new Binding("DOC_END"){StringFormat="d"}, Header="Дата окончания", Width=100},
                    new DataGridTextColumn(){Binding = new Binding("TYPE_DOC_NAME"), Header="Тип документа", Width=200},
                    new DataGridTextColumn(){Binding = new Binding("DATE_DOC"), Header="Дата внесения документа", Width=100}};
                FilterByList f = new FilterByList(new DataView(t, "", "PER_NUM, DOC_BEGIN", DataViewRowState.CurrentRows).OfType<DataRowView>(), d, false);
                f.Owner = Window.GetWindow(this);
                if (f.ShowDialog() == true)
                {
                    OracleDataAdapter a = new OracleDataAdapter(string.Format("begin {1}.SALARY_TXT_REPORTS.SelectDocumSickNotes(:p_salary_docum_ids, :c);end;", Connect.SchemaApstaff, Connect.SchemaSalary, e.Parameter),
                            Connect.CurConnect);
                    a.SelectCommand.BindByName = true;
                    a.SelectCommand.Parameters.Add("p_salary_docum_ids", OracleDbType.Array, f.SelectedRows.OfType<DataRowView>().Select(r => r.Row.Field2<Decimal>("SALARY_DOCUM_ID")).ToArray(), ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                    a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                    AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета",
                        (s, pw) =>
                        {
                            DataTable t1 = new DataTable();
                            a.Fill(t1);
                            pw.Result = t1;
                        }, a, a.SelectCommand,
                            (p, s) =>
                            {
                                if (s.Cancelled) return;
                                else if (s.Error != null) MessageBox.Show(s.Error.GetFormattedException(), "Ошибка формирования отчета");
                                else
                                {
                                    System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
                                    sf.DefaultExt = "TXT";
                                    sf.Filter = "Текстовые файлы (.txt)|*.txt";
                                    sf.InitialDirectory = Connect.parameters["ReportDirInit"];
                                    sf.FileName = Connect.parameters["AVGSickPrintFileName"];
                                    sf.OverwritePrompt = false;
                                    if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
                                    {
                                        try
                                        {
                                            if (!File.Exists(sf.FileName))
                                            {
                                                FileStream fs = File.Create(sf.FileName);
                                                fs.Close();
                                            }
                                            File.AppendAllLines(sf.FileName, (s.Result as DataTable).Rows.OfType<DataRow>().Select(w => w[0].ToString()), Encoding.GetEncoding(866));
                                            MessageBox.Show("Записи успешно добавлены в файл для печати!");
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message, "Ошибка записи в файл");
                                        }
                                    }
                                }
                            });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования отчета");
            }
        }


        /// <summary>
        /// Отчет по внесенным изменения за закрытые периоды, в основном по дням работы это 540 541 виды оплат. Код группы настроек отчетности 2001
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryCountDaysChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterPaymentChanges f = new FilterPaymentChanges(EmpFilterItempProvider.SubdivID,
                    EmpFilterItempProvider.SelectedDate.Value,
                    EmpFilterItempProvider.SelectedDate.Value.Trunc("Month").AddMonths(1).AddSeconds(-1),
                    new DateTime(2010, 1, 1),
                    EmpFilterItempProvider.SelectedDate.Value.Trunc("Month").AddSeconds(-1));
            f.Model.IsPayTypeEnabled = false;
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("Rep_SalaryChanges.sql", f.Model, FilterParameter.p_subdiv_id, 
                    FilterParameter.p_date_begin, FilterParameter.p_date_end, FilterParameter.c);
                a.SelectCommand.Parameters.Add("p_period_begin", OracleDbType.Date, f.Model.ChangeBegin, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_period_end", OracleDbType.Date, f.Model.ChangeEnd, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this,
                    "Получение данных. Ожидайте...",
                    a, a.SelectCommand, 
                    (p, pw)=>
                        {
                                ViewReportWindow.ShowReport(this, "Отчет Изменения в отработке за период", "Rep_SalaryCountDaysChanges.rdlc", (pw.Result as DataSet).Tables.OfType<DataTable>(), 
                                    new ReportParameter[]{
                                        new ReportParameter("P_DATE1", f.Model.ChangeBegin.Value.ToShortDateString()),
                                        new ReportParameter("P_DATE2", f.Model.ChangeEnd.Value.ToShortDateString()),
                                        new ReportParameter("P_CODE_SUBDIV", f.Model.CodeSubdiv)
                                    });
                        });
            }
        }
        /// <summary>
        /// отчет у кого из сотрудников заканчивается налоговый вычет в выбранном году - 19 лет исполняется
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryDiscountChildEnd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("Rep_SalaryDiscountChildEnd.sql", EmpFilterItempProvider, FilterParameter.p_subdiv_id,
                    FilterParameter.p_date, FilterParameter.c);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this,
                "Получение данных. Ожидайте...",
                a, a.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Отчет об иждивенцах сотрудников", "Rep_SalaryDiscountChildEnd.rdlc", (pw.Result as DataSet).Tables.OfType<DataTable>(),
                        new ReportParameter[]{
                                        new ReportParameter("P_DATE", EmpFilterItempProvider.SelectedDate.Value.ToShortDateString()),
                                        new ReportParameter("P_CODE_SUBDIV", EmpFilterItempProvider.CodeSubdiv)
                                    });
                });
        }


#endregion

        /// <summary>
        /// Загрузка данных по зарплате  и премии из табельной таблицы в зарплатную
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadSubdivTable_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Перезагрузить все начисления из табеля для всего подразделения?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                cmd_LoadSubdivTable.Parameters["p_date"].Value = EmpFilterItempProvider.SelectedDate;
                cmd_LoadSubdivTable.Parameters["p_subdiv_id"].Value = EmpFilterItempProvider.SubdivID;
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, string.Format("Загрузка данных по зарплате и премии за {0:MMMM yyyy}", EmpFilterItempProvider.SelectedDate.Value),
                (p, pw) =>
                {
                    OracleCommand cmd = pw.Argument as OracleCommand;
                    cmd.ExecuteNonQuery();
                },
                    cmd_LoadSubdivTable, cmd_LoadSubdivTable,
                    (p, pw) =>
                    {
                        if (pw.Cancelled) return;
                        else if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования данных");
                        else
                            MessageBox.Show("Данные успешно сформированы", "Зарплата предприятия");
                    });
            }
        }

        private void Filter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                filterGroup.UpdateSources();
            }
        }
        
        private void btFilter_Click(object sender, RoutedEventArgs e)
        {
            filterGroup.UpdateSources();
        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            currentDate.SelectedDate = currentDate.SelectedDate.Value.AddMonths(1);
        }
        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            currentDate.SelectedDate = currentDate.SelectedDate.Value.AddMonths(-1);
        }
         

        private void ListEmpDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            e.Handled = true;
            PerformCustomSort(e.Column);
        }

        private void cbRetentionFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRetention != null && dgRetention.ItemsSource != null)
            {
                string s = (string)cbRetentionFilter.SelectedValue;
                DateTime d = EmpFilterItempProvider.SelectedDate.Value;
                (dgRetention.ItemsSource as BindingListCollectionView).CustomFilter = s == "Year" ? "" : string.Format("ISNULL(date_start_ret,#1/1/1900#)<=#{0}# and ISNULL(date_end_ret,#1/1/2500#)>=#{1}#", new DateTime(d.Year, d.Month, DateTime.DaysInMonth(d.Year, d.Month)).ToString("MM/dd/yyyy"),
                    new DateTime(d.Year, d.Month, 1).ToString("MM/dd/yyyy"));
            }
        }

        /// <summary>
        /// Выгрузка данных по доп тарифам взносов за вредность в пенсионный фонд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Параметры имя файла для выгрузки</param>
        private void UnloadPFR_Dop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(string.Format("Unload_{0}.sql",e.Parameter)), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            DataTable t = new DataTable();
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных",
                (s, pw) =>
                {
                    OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                    a.Fill(t);
                    pw.Result = DBFWriter.WriteToDBF(Connect.CurrentAppPath + @"\Reports\" + string.Format("{0}.dbf", e.Parameter), t, true);

                },
                    oda, oda.SelectCommand,
                    (s, pw) =>
                    {
                        if (pw.Cancelled)
                            return;
                        else if (pw.Error != null)
                            MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка формирования данных");
                        else
                        {
                            System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
                            sf.InitialDirectory = (string)Properties.Settings.Default["LocalUploadPath"];
                            sf.Filter = "Файлы .DBF(*.DBF)|*.dbf";
                            sf.FileName = string.Format("{0}.dbf", e.Parameter);
                            
                            if (sf.ShowDialog(new Wpf32Window(Window.GetWindow(this))) == System.Windows.Forms.DialogResult.OK)
                            {
                                try
                                {
                                    Properties.Settings.Default["LocalUploadPath"] = System.IO.Path.GetDirectoryName(sf.FileName);
                                    Properties.Settings.Default.Save();
                                    File.Copy((string)pw.Result, sf.FileName, true);
                                    MessageBox.Show(string.Format("Файл успешно сформирован. Кол-во записей: {0} шт.", t.Rows.Count), "Общие итоги");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Ошибка записи файла");
                                }
                            }
                        }
                    });
        }

#region АВАНС и связаннные с ним херни
        /// <summary>
        /// Процедура будет вызывать форму для сравнения - кто же там в гребанной кассе не получил бабки свои. Не мог
        /// ли нормально её сделать - как там получить аванс это или отпускные? что за маразм блять. По тексту что ли определять? у них и счет и статья одинаковая
        /// Не могли что ли внешний справочник завести - что это вообще за выплата такая хоть... пипец вообще
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompareCacheAndSalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CacheVsSalaryCompare f = new CacheVsSalaryCompare(EmpFilterItempProvider.SelectedDate, EmpFilterItempProvider.SubdivID);
            f.Owner = Window.GetWindow(this);
            f.ShowDialog();
        }
#endregion

#region ДОкументы начисления такие как больничные и по уходу за ребенком

        DataView _salaryDocumSource=null;
        /// <summary>
        /// Источник данных для документов начисления
        /// </summary>
        public DataView SalaryDocumSource
        {
            get
            {
                if (_salaryDocumSource == null && ds!=null)
                {
                    if (ds.Tables.Contains("SALARY_DOCUM"))
                        _salaryDocumSource = new DataView(ds.Tables["SALARY_DOCUM"], "", "", DataViewRowState.CurrentRows);
                }
                return _salaryDocumSource;
            }
        }

        DataView _salaryDocumPayment;

        /// <summary>
        /// Источник данных для выплат по документу
        /// </summary>
        public DataView SalaryDocumPayment
        {
            get
            {
                if (_salaryDocumPayment == null)
                {
                    if (ds != null && ds.Tables.Contains("SALARY_DOCUM_PAYMENT"))
                        _salaryDocumPayment = new DataView(ds.Tables["SALARY_DOCUM_PAYMENT"], "", "PAY_DATE DESC, CODE_PAYMENT", DataViewRowState.CurrentRows);
                }
                return _salaryDocumPayment;
            }
        }

        /// <summary>
        /// Обновляет не только данные, но и оповещение делает, что данные обновились
        /// </summary>
        public void UpdateSalaryDocum()
        {
            LoadEmpSalaryDocum();
            OnPropertyChanged("SalaryDocumSource");
            if (SalaryDocumSource != null && SalaryDocumSource.Count == 1)
            {
                SelectedSalaryDocum = SalaryDocumSource[0];
            }
            else
                SelectedSalaryDocum = null;
        }

        /// <summary>
        /// Обновление данных по выплатам документа
        /// </summary>
        public void UpdateSalDocumPayment()
        { 
            if (ds.Tables.Contains("SALARY_DOCUM_PAYMENT"))
                ds.Tables["SALARY_DOCUM_PAYMENT"].Rows.Clear();
            if (SelectedSalaryDocum != null)
            {
                odaSalaryDocumPayment.SelectCommand.Parameters["p_salary_docum_id"].Value = SelectedSalaryDocum["SALARY_DOCUM_ID"];
                try
                {
                    odaSalaryDocumPayment.Fill(ds);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка получения выплат по документу");
                }
            }
            OnPropertyChanged("SalaryDocumPayment");
        }

        private void RefreshSalaryDocum_Click(object sender, RoutedEventArgs e)
        {
            UpdateSalaryDocum();
        }
      
        /// <summary>
        /// Добавление нового документа начисления для сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddSalaryDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryDocumEditor f = new SalaryDocumEditor(EmpFilterItempProvider.SubdivID, null, SelectedTransferID, EmpFilterItempProvider.SelectedDate);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                // если изменение прошло, то обновляем список документов и пытаемся рассчитать если надо
                if (f.Model.CalcAfterSave)
                    Model.CalcSalaryDocum(f.Model.SalaryDocumID, EmpFilterItempProvider.SelectedDate);
                UpdateSalaryDocum();
            }
        }
        
        /// <summary>
        /// рассчет документа выбранного за текущий месяц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcSelectedDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // если изменение прошло, то обновляем список документов и пытаемся рассчитать если надо
            Model.CalcSalaryDocum(SelectedSalaryDocum.Row.Field2<Decimal?>("SALARY_DOCUM_ID"), EmpFilterItempProvider.SelectedDate);
            UpdateSalaryDocum();
        }

        /// <summary>
        /// Доступность команды для выбранного документа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedSalaryDocum_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && SelectedTransferID != null && SelectedSalaryDocum != null;
        }
        
        /// <summary>
        /// Редактированеие документа ЗП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditSalaryDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryDocumEditor f = new SalaryDocumEditor(EmpFilterItempProvider.SubdivID, SelectedSalaryDocum.Row.Field2<Decimal?>("SALARY_DOCUM_ID"), SelectedTransferID, EmpFilterItempProvider.SelectedDate);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                // если изменение прошло, то обновляем список документов и пытаемся рассчитать если надо
                if (f.Model.CalcAfterSave)
                    Model.CalcSalaryDocum(f.Model.SalaryDocumID, EmpFilterItempProvider.SelectedDate);
                UpdateSalaryDocum();
            }
        }

        /// <summary>
        /// Удаление документа ЗП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteSalarDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(Window.GetWindow(this), "Удалить выбранный документ начисления и все связанные выплаты?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                odaSalaryDocum.DeleteCommand.Parameters["p_salary_docum_id"].Value = SelectedSalaryDocum["salary_docum_id"];
                if (Model.DeleteSalaryDocum(SelectedSalaryDocum.Row.Field2<decimal?>("salary_docum_id"), true, GetCalcedTypePayment()))
                    UpdateSalaryDocum();
            }
        }

        /// <summary>
        /// Печать расчета больничного на обычную бумагу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepEmpSickDocumPrint_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(string.Format("begin {1}.SALARY_REPORTS.SelectEmpDocumSickCalcReport(:p_salary_docum_id, :c1, :c2, :c3, :c4); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, SelectedSalaryDocum["SALARY_DOCUM_ID"], ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", oda, oda.SelectCommand,
                (p, pw) =>
                {
                    ViewReportWindow.ShowReport(this, "Просмотр справки-расчет", "Rep_EmpAvgSickDay.rdlc",
                        (pw.Result as DataSet).Tables.OfType<DataTable>(), new ReportParameter[]{ 
                                new ReportParameter("P_HIDE_SUM", "False"), new ReportParameter("P_DATE", (SelectedSalaryDocum.Row.Field2<DateTime?>("DATE_DOC")??DateTime.Now).ToShortDateString())});
                });
        }
       

#endregion    

        /// <summary>
        /// Модель представления данных для формы оплаты
        /// </summary>
        public PaymentModel Model
        {
            get
            {
                if (_model == null)
                    _model = new PaymentModel();
                return _model;
            }
        }
        PaymentModel _model;

        /// <summary>
        /// Отчет реестр по листам нетрудоспособности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSickPaymentRegister_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Salary.ViewReporting.FilterReporting f = new ViewReporting.FilterReporting(EmpFilterItempProvider.SubdivID, EmpFilterItempProvider.SelectedDate, EmpFilterItempProvider.SelectedDate);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter("Rep_SickPaymentRegister.sql", f, FilterParameter.p_subdiv_id, FilterParameter.p_date_begin, FilterParameter.p_date_end);
                oda.SelectCommand.Parameters.Add("p_type_sal_docum_ids", OracleDbType.Array, e.Parameter.ToString().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(r => Convert.ToDecimal(r)).ToArray(), ParameterDirection.Input)
                    .UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "Реестра по листам нетрудоспособности", "Rep_SickPaymentRegister.rdlc",
                            (pw.Result as DataSet).Tables.OfType<DataTable>(), new ReportParameter[]{
                                    new ReportParameter("P_DATE1",  f.GetDateBegin().ToShortDateString()),
                                    new ReportParameter("P_DATE2",  f.GetDateEnd().ToShortDateString())});
                    });
            }
        }

        /// <summary>
        /// Какие изменения прошли по взносам за месяца расчета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepDuesVersionCompare_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterPaymentChanges f = new FilterPaymentChanges(EmpFilterItempProvider.SubdivID, EmpFilterItempProvider.SelectedDate.Value.Trunc("Month").AddMonths(-1).AddDays(3), DateTime.Now,
                EmpFilterItempProvider.SelectedDate.Value.Trunc("Year"), EmpFilterItempProvider.SelectedDate.Value.Trunc("Month").AddSeconds(-1));
            f.Owner = Window.GetWindow(this);
            f.Model.IsChangeEndEnabled = false;
            f.Model.ChangeBeginCaption = "Дата для сравнения с тек. состоянием";
            if (f.ShowDialog() == true)
            { 
                OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_DuesVersionCompare.sql"), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.Model.SubdivID, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.Model.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.Model.DateEnd, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, f.Model.ChangeBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных. Это может занять значительное время",
                    oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "Изменения расчетов по взносам за период", "Rep_DuesVersionCompare.rdlc",
                            (pw.Result as DataSet).Tables[0],
                            new ReportParameter[]{new ReportParameter("P_DATE", f.Model.ChangeBegin.Value.ToShortDateString()),
                                                      new ReportParameter("P_DATE_BEGIN", f.Model.DateBegin.Value.ToShortDateString()),
                                                      new ReportParameter("P_DATE_END", f.Model.DateEnd.Value.ToShortDateString())});
                    });

            }
        }

        /// <summary>
        /// Справка 2НДФЛ за выбранный год
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep2NDFLReport_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByPeriod f = new FilterByPeriod(new  FilterPeriodModel(){ DateBegin = EmpFilterItempProvider.SelectedDate.Value.Trunc("Year"), IsEndEnabled = false});
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                SignesRecord[] signes = null;
                if (Signes.Show(EmpFilterItempProvider.SubdivID, "2NDFLReport", "Выбрите ответственных", 1, ref signes, Window.GetWindow(this)) == true)
                {
                    OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_2NDLFReport.sql"), Connect.CurConnect);
                    oda.SelectCommand.BindByName = true;
                    oda.SelectCommand.Parameters.Add("p_transfer_id", OracleDbType.Decimal, SelectedTransferID, ParameterDirection.Input);
                    oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, f.Model.DateBegin, ParameterDirection.Input);
                    oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
                    oda.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
                    oda.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
                    AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", oda, oda.SelectCommand,
                        (p, pw) =>
                        {
                            DataSet dd = pw.Result as DataSet;
                            ViewReportWindow.ShowReport(this, "Справка по форме 2НДФЛ", @"Rep_2NDFL.rdlc",
                                new SubReport[] { new SubReport("Subreport1", "Rep_2NDFL_Subreport.rdlc", dd.Tables[1]), new SubReport("Subreport2", "Rep_2NDFL_Subreport2.rdlc", dd.Tables[2]) },
                                new DataTable[] { dd.Tables[0] },
                                new ReportParameter[]{
                                    new ReportParameter("P_POS_NAME", signes[0].PosName),
                                    new ReportParameter("P_FIO", signes[0].EmpName)}
                                        , System.Drawing.Printing.Duplex.Default,
                                        true, new DateTime(2015, 1, 1)
                                        );
                        });
                }
            }
        }

        /// <summary>
        /// Отчет проверяет наличие ошибок налога в 2НДФЛ и показывает список у кого они
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_2NDFLErrors_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter("Rep2NDFLErrors.sql", EmpFilterItempProvider, FilterParameter.p_date, FilterParameter.p_subdiv_id, FilterParameter.c);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Проверка данных по справкам 2НДФЛ...", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        DataSet dd = pw.Result as DataSet;
                        ViewReportWindow.ShowReport(this, "Справка по форме 2НДФЛ", "Rep_2NDFLErrors.rdlc",
                            new DataTable[] { dd.Tables[0] },
                            new ReportParameter[]{new ReportParameter("P_DATE", EmpFilterItempProvider.GetDate().ToShortDateString())});
                    });
        }

        /// <summary>
        /// Отчет документы начисления за период
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryDocumForPeriod_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterByList f = new FilterByList(AppDataSet.TypeSalDocum.DefaultView.OfType<DataRowView>(),
                new DataGridColumn[]{
                    new DataGridTextColumn(){ Binding= new Binding("TYPE_SAL_DOC_NAME"), Width=200}
                },
                true, true, true);
            f.SubdivID = EmpFilterItempProvider.SubdivID;
            f.DateBegin = EmpFilterItempProvider.SelectedDate.Value.Trunc("Month");
            f.DateEnd = EmpFilterItempProvider.SelectedDate;
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter("RepSalaryDocumForPeriod.sql", f, FilterParameter.p_date_begin, FilterParameter.p_date_end, FilterParameter.p_subdiv_id);
                oda.SelectCommand.Parameters.Add("p_salary_docum_ids", OracleDbType.Array, f.SelectedRows.OfType<DataRowView>().Select(r => r.Row.Field2<Decimal>("TYPE_SAL_DOCUM_ID")).ToArray(), ParameterDirection.Input).UdtTypeName = 
                    "SALARY.NUMBER_COLLECTION_TYPE";
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "Документы за период", "Rep_SalaryDocumForPeriod.rdlc", (pw.Result as DataSet).Tables[0],
                            new ReportParameter[]{
                                new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString())});
                    }
                );

            }
        }

        /// <summary>
        /// Отчет выплаты по 243, 270 виду оплат. Можно было бы сделать и на любые виды оплат, только пока не за чем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryMothersPayment_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(null, EmpFilterItempProvider.SubdivID, EmpFilterItempProvider.SelectedDate.Value.Trunc("Month"));
            f.DateBegin = EmpFilterItempProvider.SelectedDate.Value.Trunc("Year");
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("RepSalaryMothersPayment.sql"), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        (s, pw) =>
                        {
                            OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                            DataTable t = new DataTable();
                            a.Fill(t);
                            pw.Result = t;
                        },
                            oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Отчет выплаты по уходу за детьми", "Rep_SalaryMothersPayment.rdlc", pw.Result as DataTable,
                                    new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString()),
                                    new ReportParameter("P_CODE_SUBDIV", f.CodeSubdiv)
                                    });
                        });
            }
        }


        /// <summary>
        /// Отчет по материальной выгоде и налогу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepSalaryTaxLucre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(null, EmpFilterItempProvider.SubdivID, EmpFilterItempProvider.SelectedDate.Value.Trunc("Month"));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("Rep_SalaryTaxLucre.sql"), Connect.CurConnect);
                oda.SelectCommand.BindByName = true;
                oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, f.SubdivID, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, f.DateBegin, ParameterDirection.Input);
                oda.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, f.DateEnd, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных. Ожидайте...",
                        (s, pw) =>
                        {
                            OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                            DataTable t = new DataTable();
                            a.Fill(t);
                            pw.Result = t;
                        },
                            oda, oda.SelectCommand,
                        (s, pw) =>
                        {
                            if (pw.Cancelled) return;
                            if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                            else
                                ViewReportWindow.ShowReport(this, "Отчет по материальной выгоде", "Rep_SalaryTaxLucre.rdlc", pw.Result as DataTable,
                                    new ReportParameter[]{ new ReportParameter("P_DATE1", f.DateBegin.Value.ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.DateEnd.Value.ToShortDateString()),
                                    new ReportParameter("P_CODE_SUBDIV", f.CodeSubdiv)
                                    });
                        });
            }
        }

        /// <summary>
        /// Отчет по налоговым вычетам сотрудников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepEmpTaxDiscount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataTable t = new DataTable();
            try
            {
                OracleDataAdapter types = OracleAdapterHelper.GetDefaultAdapter("SelectTypeDiscount.sql", EmpFilterItempProvider, FilterParameter.p_date);
                types.Fill(t);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения списка вычетов");
                return;
            }
            FilterByList f = new FilterByList(t.DefaultView.OfType<DataRowView>(),
                new DataGridColumn[]{
                    new DataGridTextColumn(){ Binding= new Binding("CODE_DISC"), Header="Код", Width=100, SortDirection=ListSortDirection.Ascending, SortMemberPath="CODE_DISC"},
                    new DataGridTextColumn(){ Binding= new Binding("NAME_DISC"), Header="Наименование", Width=250}
                },
                false, true, true);
            f.SubdivID = EmpFilterItempProvider.SubdivID;
            f.DateBegin = EmpFilterItempProvider.SelectedDate.Value.Trunc("Year");
            f.DateEnd = EmpFilterItempProvider.SelectedDate;
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            { 
                OracleDataAdapter a = OracleAdapterHelper.GetDefaultAdapter("SelectTaxDiscounts.sql", f, FilterParameter.p_date_begin, FilterParameter.p_date_end, FilterParameter.p_subdiv_id);
                a.SelectCommand.Parameters.Add("p_type_discount_ids", 
                    OracleDbType.Array, f.SelectedValues<decimal>("TYPE_DISCOUNT_ID"), ParameterDirection.Input).UdtTypeName="SALARY.NUMBER_COLLECTION_TYPE";
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных...", a, a.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "Отчет по налоговым вычетам", "Rep_TaxDiscounts.rdlc", (pw.Result as DataSet).Tables[0],
                            new ReportParameter[]{ 
                                    new ReportParameter("P_DATE1", f.GetDateBegin().ToShortDateString()),
                                    new ReportParameter("P_DATE2", f.GetDateEnd().ToShortDateString())
                                });
                    });

            }
        }

        /// <summary>
        /// Выгрузка данных по 2-НДФЛ в XML файл. Вызывается соответсвующия процедура из пакета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">суффикс процедуры для выполнения в соответсвии с версией</param>
        private void UploadNoNDFL2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(string.Format("Начать формирование данных по справке 2-НДФЛ в XML формат(год: {0:yyyy}, подразделение {1})?", EmpFilterItempProvider.SelectedDate.Value,
                    EmpFilterItempProvider.CodeSubdiv), 
                    "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("Get_2NDFL_XML.sql"), Connect.SchemaApstaff, Connect.SchemaSalary, e.Parameter.ToString()), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_per_nums", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName = "SALARY.VARCHAR_COLLECTION_TYPE";
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование данных...", a, a.SelectCommand,
                    (p, pw) =>
                    {
                        SaveFileDialog sf = new SaveFileDialog();
                        sf.Filter = "Файлы XML (*.xml)|*.xml";
                        System.Xml.Linq.XElement xel = AppXmlHelper.GetElements("NDFL2FileName").FirstOrDefault();
                        string fileName = string.Empty;
                        if (xel != null)
                            fileName = xel.Attribute("Name").Value;
                        sf.FileName = string.Format(fileName, DateTime.Today);
                        if (sf.ShowDialog(Window.GetWindow(this)) == true)
                        {
                            try
                            {
                                File.WriteAllLines(sf.FileName, (pw.Result as DataSet).Tables[0].Rows.OfType<DataRow>().Select(r => r[0].ToString()), Encoding.GetEncoding(1251));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Ошибка создания файла");
                            }
                        }
                    });
            }
        }

        /// <summary>
        /// Отчет по закрытым заказам в зарплате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rep_ClosedOrdersSalary_executed(object sender, ExecutedRoutedEventArgs e)
        {
            FilterReporting f = new FilterReporting(EmpFilterItempProvider.SubdivID, EmpFilterItempProvider.SelectedDate, EmpFilterItempProvider.SelectedDate);
            f.AllowBegin = System.Windows.Visibility.Collapsed;
            if (f.ShowDialog() == true)
            {
                OracleDataAdapter oda = OracleAdapterHelper.GetDefaultAdapter("Rep_ClosedOrders.sql", f, FilterParameter.p_subdiv_id, FilterParameter.p_date, FilterParameter.c);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Получение данных", oda, oda.SelectCommand,
                    (p, pw) =>
                    {
                        ViewReportWindow.ShowReport(this, "Отчет по ошибочным заказам", "Rep_ClosedOrders.rdlc", (pw.Result as DataSet).Tables[0],
                            new ReportParameter[] { new ReportParameter("P_DATE", f.GetDate().ToShortDateString()), new ReportParameter("P_CODE_SUBDIV", f.CodeSubdiv) });
                    });
            }
        }

        /// <summary>
        /// Просматриваем форму истории 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewSalaryHistory_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            HistorySalaryEdit f = new HistorySalaryEdit(SelectedPerNum, EmpFilterItempProvider.SelectedDate);
            f.Owner = Window.GetWindow(this);
            f.ShowDialog();
        }

        /// <summary>
        /// Отчет печать платежной ведомости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepPayNoteT_53_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectCartularyListBySubdiv.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
            DataTable t = new DataTable();
            try
            {
                oda.Fill(t);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения списка реестров");
            }
            FilterByList f = new FilterByList(t.DefaultView.OfType<DataRowView>(), new DataGridColumn[]{ 
                new DataGridTextColumn(){Header="Название", Binding= new Binding("CARTULARY_COMMENT"), Width=350},
                new DataGridTextColumn(){Header="Дата создания", Binding= new Binding("DATE_CREATE"){StringFormat="G"}, Width=150},
                new DataGridTextColumn(){Header="Дата закрытия", Binding= new Binding("DATE_CLOSE_CART"){StringFormat="G"}, Width=150}
            }, false);
            f.IsSubdivAllowed = true;
            f.SubdivID = EmpFilterItempProvider.SubdivID;
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog()==true)
            {
                if (f.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Не выбран реестр для формирования");
                    return;
                }
                CartularyViewer.PrintCartularyT_53(this, f.SelectedValues<decimal?>("CARTULARY_ID").FirstOrDefault(), f.SubdivID, EmpFilterItempProvider.SelectedDate,
                    f.SelectedValues<string>("TYPE_CARTULARY_NAME").FirstOrDefault().Split(' ', ',', ';').LastOrDefault());
            }
        }

        /// <summary>
        /// Загрузка данных по бригадным нарядам в зарплату (101 вид оплат)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadTableBrigageToSalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(string.Format("Загрузить данные в зарплату по данным нарядов табелей бригад (Подразделение: {0}, месяц: {1:MMMM yyyy})?", 
                    EmpFilterItempProvider.CodeSubdiv, EmpFilterItempProvider.SelectedDate), 
                    "Загрузка зарплаты (сдельно)", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OracleCommand cmd = new OracleCommand("begin SALARY.LOAD_BRIGAGE_PIECE_TO_SALARY(:p_date, :p_subdiv_id);end;", Connect.CurConnect);
                cmd.BindByName = true;
                cmd.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                cmd.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Загружаем данные...", cmd,
                    (p, pw) =>
                    {
                        MessageBox.Show(Window.GetWindow(this), "Загрузка данных успешно завершена", "Загрузка данных");
                    });
            }
        }

        /// <summary>
        /// Формирует данные в таблицу средних чтобы можно было формировать отчеты быстро
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAVDEmpDataTable_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(string.Format("Сформировать данные по среднему дневному сотрудников? (Подразделение: {0}, месяц: {1:MMMM yyyy})?",
                    EmpFilterItempProvider.CodeSubdiv, EmpFilterItempProvider.SelectedDate),
                    "Формирование данных", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OracleCommand cmd = new OracleCommand("begin SALARY.LOAD_EMP_AVG_DATA(:p_date, :p_subdiv_id);end;", Connect.CurConnect);
                cmd.BindByName = true;
                cmd.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                cmd.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, EmpFilterItempProvider.SubdivID, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формируем данные...", cmd,
                    (p, pw) =>
                    {
                        MessageBox.Show(Window.GetWindow(this), "Загрузка данных успешно завершена", "Загрузка данных");
                    });
            }
        }

        /// <summary>
        /// Продление вычетов на год
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExtendTaxDiscount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show($"Продлить вычеты с {EmpFilterItempProvider.SelectedDate.Value.Year - 1} г. на {EmpFilterItempProvider.SelectedDate.Value.Year} г.?", "Внимание ", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                OracleCommand cmd = new OracleCommand("begin SALARY.ExtendTaxDiscount(:p_date);end;", Connect.CurConnect);
                cmd.BindByName = true;
                cmd.Parameters.Add("p_date", OracleDbType.Date, EmpFilterItempProvider.SelectedDate, ParameterDirection.Input);
                AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формируем данные...", cmd,
                    (p, pw) =>
                    {
                        MessageBox.Show(Window.GetWindow(this), "Операция выполнена", "Успешно");
                    }
                    );
            }
        }
    }

    /// <summary>
    /// Модель отображения данных по зарплате
    /// </summary>
    public class PaymentModel : NotificationObject
    {
        OracleCommand cmd_CalcSalaryDocum;
        OracleDataAdapter oda_Salary, oda_Salary_Docum;

        public PaymentModel()
        {
            // Команда расчета документа
            cmd_CalcSalaryDocum = new OracleCommand(string.Format("begin {1}.CALC_EMP_DOCUM(:p_salary_docum_id, :p_date);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmd_CalcSalaryDocum.BindByName = true;
            cmd_CalcSalaryDocum.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            cmd_CalcSalaryDocum.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);

            oda_Salary = new OracleDataAdapter("", Connect.CurConnect);
            oda_Salary.DeleteCommand = new OracleCommand(string.Format("begin {1}.SALARY_DELETE(:p_SALARY_ID, :p_CALC_RELATION); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            oda_Salary.DeleteCommand.BindByName = true;
            oda_Salary.DeleteCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, ParameterDirection.Input);
            oda_Salary.DeleteCommand.Parameters.Add("p_CALC_RELATION", OracleDbType.Array, ParameterDirection.Input).UdtTypeName="SALARY.NUMBER_COLLECTION_TYPE";

            oda_Salary_Docum = new OracleDataAdapter("", Connect.CurConnect);
            oda_Salary_Docum.DeleteCommand = new OracleCommand(string.Format("begin {1}.SALARY_DOCUM_DELETE(:p_SALARY_DOCUM_ID, :p_Recalc_sign, :p_calced_payment_type_ids); end;",
                Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            oda_Salary_Docum.DeleteCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, null, ParameterDirection.Input);
            oda_Salary_Docum.DeleteCommand.Parameters.Add("p_Recalc_sign", OracleDbType.Decimal, null, ParameterDirection.Input);
            oda_Salary_Docum.DeleteCommand.Parameters.Add("p_calced_payment_type_ids", OracleDbType.Array, null, ParameterDirection.Input).UdtTypeName="SALARY.NUMBER_COLLECTION_TYPE";
        }
        /// <summary>
        /// Рассчитывает для документа начисления за выбранную дату
        /// </summary>
        /// <param name="salary_docum_id"></param>
        /// <param name="selectedDate"></param>
        /// <returns></returns>
        public bool CalcSalaryDocum(decimal? salary_docum_id, DateTime? selectedDate)
        {
            cmd_CalcSalaryDocum.Parameters["p_salary_docum_id"].Value = salary_docum_id;
            cmd_CalcSalaryDocum.Parameters["p_date"].Value = selectedDate;
            OracleTransaction tr=  Connect.CurConnect.BeginTransaction();
            try
            {
                cmd_CalcSalaryDocum.ExecuteNonQuery();
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка расчета документа");
                return false;
            }
        }

        /// <summary>
        /// Удалить записи зарплаты
        /// </summary>
        /// <param name="salary_ids"></param>
        /// <returns></returns>
        public bool DeleteSalary(IEnumerable<decimal?> salary_ids)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                foreach (decimal? id in salary_ids)
                {
                    oda_Salary.DeleteCommand.Parameters["p_SALARY_ID"].Value = id;
                    oda_Salary.DeleteCommand.ExecuteNonQuery();
                }
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления записей из зарплаты");
                return false;
            }
        }

        /// <summary>
        /// Удаление Документа и если надо зависимых данных
        /// </summary>
        /// <param name="salary_docum_id">Айдишник документа</param>
        /// <param name="calc_sign">1 - удалить из зп данные, иначе оставить</param>
        /// <param name="calced_tp_payments">при удалени как типа оплат пересчитать надо</param>
        /// <returns></returns>
        public bool DeleteSalaryDocum(decimal? salary_docum_id, bool calc_sign, decimal[] calced_tp_payments)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                oda_Salary_Docum.DeleteCommand.Parameters["p_SALARY_DOCUM_ID"].Value = salary_docum_id;
                oda_Salary_Docum.DeleteCommand.Parameters["p_recalc_sign"].Value = calc_sign?1m:0m;
                oda_Salary_Docum.DeleteCommand.Parameters["p_calced_payment_type_ids"].Value = calced_tp_payments;
                oda_Salary_Docum.DeleteCommand.ExecuteNonQuery();
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления документа");
                return false;
            }
        }
    }
}


