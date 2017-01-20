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
using Oracle.DataAccess.Client;
using System.Data;
using Microsoft.Reporting.WinForms;
using Salary.Helpers;
using System.ComponentModel;
using System.IO;
using Salary;
using LibrarySalary.Helpers;


namespace Salary.View
{
    /// <summary>
    /// Interaction logic for SalaryVacCalc.xaml
    /// </summary>
    public partial class SalaryVacCalc : UserControl
    {
        public SalaryVacCalc()
        {
            InitializeComponent();
            this.DataContext = Model;
        }

        SalaryVacModel _model;
        public SalaryVacModel Model
        {
            get
            {
                if (_model == null)
                    _model = new SalaryVacModel();
                return _model;
            }
        }

        private void AddSalaryDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryDocumEditor f = new SalaryDocumEditor(subdivSelector1.SubdivId, null);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.LoadDocums();
            }
        }

        private void EditSalaryDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryDocumEditor f = new SalaryDocumEditor(subdivSelector1.SubdivId, (decimal)Model.CurrentDocument["SALARY_DOCUM_ID"]);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.LoadDocums();
            }
        }

        private void CreateDocumentVac_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.SubdivID != null;
        }

        private void DocumIsSelected_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.CurrentDocument != null;
        }

        /// <summary>
        /// Расчет придержаний для одного документа по одной кнопке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcVacTypeByDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.CalcDocumens();
        }

        private void EditSalaryDocum_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.CurrentDocument != null;
        }

        private void DeleteSalaryDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить документ и связанные с ним данные (Только документ, в зарплате данные останутся)?", "Удаление документа", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Model.DeleteCurrentDocum();
            }
        }

        private void AddSalaryDocumRel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.CurrentDocument != null;
        }

        private void AddSalaryDocumRelation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AddNewSalaryRelation(this);
        }

        private void EditSalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.EditSalaryRelation(this);
        }

        private void DeleteSalaryDocumRelation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.CurrentDocument != null && Model.CurrentSalary != null;
        }

        private void DeleteSalaryDocumRelation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Убрать выбранную запись из документа (останется в зарплате)?", "Исключение из документа", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Model.ExcludeCurrentSalary();
        }

        private void DeleteSalary_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись из заработной платы и документа?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Model.DeleteCurrentSalary();
        }

        private void AddSalaryVac_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryVacEditor f = new SalaryVacEditor(Model.CurrentDocumID, null);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.CurrentDocument = Model.CurrentDocument;
            }
        }

        private void EditSalaryVac_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SalaryVacEditor f = new SalaryVacEditor(Model.CurrentDocumID, Model.CurrentSalaryVac["SALARY_VAC_ID"]);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.CurrentDocument = Model.CurrentDocument;
            }
        }

        private void DeleteSalaryVac_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.CurrentSalaryVac != null;
        }

        private void DeleteSalaryVac_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранное придержание?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Model.DeleteSalaryVacCurrent();
        }

        private void CheckAll_Checked(object sender, RoutedEventArgs e)
        {
            Model.CheckAllDocs((sender as CheckBox).IsChecked);
        }

        private void CalcCheckedVacTypeByDocum_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.GetCountChecked() > 0;
        }

        private void CalcCheckedVacTypeByDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgSalDocum.CommitEdit();
            if (MessageBox.Show("Пересчитать данные по выбранным документам?", "Пересчет документов", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Model.CalcDocumens();

            }
        }

        /// <summary>
        /// Сводная ведомость по отпускам за месяц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepConsolidVacReport_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepSelectVacConsolid.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, Model.DateBegin, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, Model.SubdivID, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                DataTable t = new DataTable();
                a.Fill(t);
                Reports.ViewReportWindow.ShowReport(this, "\"Сводная ведомость отпускных за месяц\"", e.Parameter.ToString(), t, 
                    new ReportParameter[]{ new ReportParameter("P_DATE", dpDateBegin.SelectedDate.Value.ToString("dd-MM-yyyy")),
                                           new ReportParameter("P_CODE_SUBDIV", subdivSelector1.CodeSubdiv)}.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException());
            }
        }

        private void Report_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void ReportSelected_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model.GetCountChecked() > 0;
        }

        private void RepVacSalDeptAndPaid_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepSelectVacSalDeptAndPaid.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                a.SelectCommand.BindByName = true;
                a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, dpDateBegin.SelectedDate.Value, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, dpDateEnd.SelectedDate.Value, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, subdivSelector1.SubdivId, ParameterDirection.Input);
                a.SelectCommand.Parameters.Add("p_doc_ids", OracleDbType.Array).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                a.SelectCommand.Parameters["p_doc_ids"].Value = Model.CheckedIdsDocum;
                a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                DataTable t = new DataTable();
                a.Fill(t);
                Reports.ViewReportWindow.ShowReport(this, "\"Начисление и перечисление отпускных\"", "Rep_VacSalDeptAndPaid.rdlc", t, new ReportParameter[]{ new ReportParameter("P_DATE1", dpDateBegin.SelectedDate.Value.ToString("dd-MM-yyyy")),
                    new ReportParameter("P_DATE2", dpDateEnd.SelectedDate.Value.ToString("dd-MM-yyyy")),
                    new ReportParameter("CODE_SUBDIV", subdivSelector1.CodeSubdiv)}.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException());
            }
        }

        private void RepVacNote_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
            {
                try
                {
                    OracleDataAdapter a = new OracleDataAdapter(string.Format(Queries.GetQuery("RepSelectVacSalDeptAndPaid.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
                    a.SelectCommand.BindByName = true;
                    a.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, dpDateBegin.SelectedDate.Value, ParameterDirection.Input);
                    a.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, dpDateEnd.SelectedDate.Value, ParameterDirection.Input);
                    a.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, subdivSelector1.SubdivId, ParameterDirection.Input);
                    a.SelectCommand.Parameters.Add("p_doc_ids", OracleDbType.Array).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
                    a.SelectCommand.Parameters["p_doc_ids"].Value = Model.CheckedIdsDocum;
                    a.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
                    DataSet t = new DataSet();
                    a.Fill(t);
                    Reports.ViewReportWindow.ShowReport(this, "\"Записка расчет\"", "Rep_VacNoteCalc.rdlc", t.Tables[0], null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException());
                }
            }
            else
            {

            }
        }

        private void btFilter_Click(object sender, RoutedEventArgs e)
        {
            filterGroup.UpdateSources();
            Model.LoadDocums();
        }

        private void CreateDocumentVac_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            filterGroup.UpdateSources();
            if (MessageBox.Show(string.Format("Создать документы по отпускным за выбранный период?\n(c {0:dd.MM.yyyy} по {1:dd.MM.yyyy} подразделение № {2}", Model.DateBegin, Model.DateEnd,
                subdivSelector1.CodeSubdiv), "Формирование документов", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Model.CreateDocumentsByPeriod();
                Model.LoadDocums();
            }
        }

        private void DumpSalaryVacs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            filterGroup.UpdateSources();
            VacToSalaryImport f = new VacToSalaryImport(Model.SubdivID, Model.DateBegin, Model.DateEnd, true);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                Model.LoadDocums();
            }
        }

        private void GroupBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                filterGroup.UpdateSources();
                Model.LoadDocums();
            }
        }

        private void LockSalaryDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.ChangeLockChecked(e.Parameter);
        }

        private void UnlockSalaryDocum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Model.ChangeLockChecked(e.Parameter);
        }

        private void ReAddSalaryDocumRelation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Доформировать документ по сотруднику?", "Зарплата предприятия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Model.ReformEmpDocum();
        }

        private void EmpAVGDayPrice_Short_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(e.Parameter.ToString() + ".sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_salary_docum_id", OracleDbType.Array, Model.CheckedIdsDocum, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";
            oda.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(this, "Формирование отчета...",
                (p, s) =>
                {
                    OracleDataAdapter a = s.Argument as OracleDataAdapter;
                    DataTable t = new DataTable();
                    a.Fill(t);
                    s.Result = t;
                }, oda, oda.SelectCommand,
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
                            sf.FileName = Connect.parameters["AVGReportName"];
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
    /// Модель для показа данных по документам отпускных
    /// </summary>
    public class SalaryVacModel : NotificationObject
    {
        public SalaryVacModel()
        {
            odaSalaryDocumsData = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectSalDocsData.sql"), Connect.CurConnect);
            odaSalaryDocumsData.SelectCommand.BindByName = true;
            odaSalaryDocumsData.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaSalaryDocumsData.TableMappings.Add("Table", "TYPE_SAL_DOCUM");

            odaSalaryDocumsData.Fill(ds);
            _typeSalDocumID = ds.Tables["TYPE_SAL_DOCUM"].Rows[0].Field2<Decimal?>("TYPE_SAL_DOCUM_ID");
            
            odaDocumLoad = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectSalaryDocum.sql"), Connect.CurConnect);
            odaDocumLoad.SelectCommand.BindByName = true;
            odaDocumLoad.SelectCommand.Parameters.Add("p_date_begin", OracleDbType.Date, null, ParameterDirection.Input);
            odaDocumLoad.SelectCommand.Parameters.Add("p_date_end", OracleDbType.Date, null, ParameterDirection.Input);
            odaDocumLoad.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaDocumLoad.SelectCommand.Parameters.Add("p_type_sal_docum_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaDocumLoad.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor,  ParameterDirection.Output);
            odaDocumLoad.TableMappings.Add("Table", "SalaryDocum");

            odaDocData = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectSalaryDocRelation.sql"), Connect.CurConnect);
            odaDocData.SelectCommand.BindByName = true;
            odaDocData.SelectCommand.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaDocData.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            odaDocData.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaDocData.TableMappings.Add("Table", "SALARY");
            odaDocData.TableMappings.Add("Table1", "SALARY_VAC");

    #region Адаптеры и команды обновления
            cmdCalcVacDocum = new OracleCommand(string.Format("begin {1}.CALC_VAC_DOCUMENT(:p_salary_docum_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmdCalcVacDocum.BindByName = true;
            cmdCalcVacDocum.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, null, ParameterDirection.Input);

            cmdCreateDoc = new OracleCommand(string.Format("begin {1}.GENERATE_SALARY_VAC_DOCUM(p_date_begin=>:p_date_begin, p_date_end=>:p_date_end, p_subdiv_id=>:p_subdiv_id, p_type_sal_docum_id=>:p_type_sal_docum_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmdCreateDoc.BindByName = true;
            cmdCreateDoc.Parameters.Add("p_date_begin", OracleDbType.Date, ParameterDirection.Input);
            cmdCreateDoc.Parameters.Add("p_date_end", OracleDbType.Date, ParameterDirection.Input);
            cmdCreateDoc.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, ParameterDirection.Input);
            cmdCreateDoc.Parameters.Add("P_type_sal_docum_id", OracleDbType.Decimal, ParameterDirection.Input);

            cmdReloadDoc = new OracleCommand(string.Format("begin {1}.GENERATE_SALARY_VAC_DOCUM(p_date_begin=>:p_date_begin, p_date_end=>:p_date_end, p_subdiv_id=>:p_subdiv_id, p_type_sal_docum_id=>:p_type_sal_docum_id, p_salary_docum_id=>:p_salary_docum_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmdReloadDoc.BindByName = true;
            cmdReloadDoc.Parameters.Add("p_date_begin", OracleDbType.Date, ParameterDirection.Input);
            cmdReloadDoc.Parameters.Add("p_date_end", OracleDbType.Date, ParameterDirection.Input);
            cmdReloadDoc.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, ParameterDirection.Input);
            cmdReloadDoc.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, ParameterDirection.Input);
            cmdReloadDoc.Parameters.Add("p_type_sal_docum_id", OracleDbType.Decimal, ParameterDirection.Input);

            cmdDeleteDocum = new OracleCommand(string.Format("begin {1}.SALARY_DOCUM_DELETE(:p_salary_doc_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmdDeleteDocum.BindByName = true;
            cmdDeleteDocum.Parameters.Add("p_salary_doc_id", OracleDbType.Decimal, null, ParameterDirection.Input);

            cmdAddSalaryVacRelation = new OracleCommand(string.Format("begin {1}.SALARY_DOC_RELATION_UPDATE(:p_salary_doc_relation_id, :p_salary_docum_id, :p_salary_id); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmdAddSalaryVacRelation.BindByName=true;
            cmdAddSalaryVacRelation.Parameters.Add("p_salary_doc_relation_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            cmdAddSalaryVacRelation.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            cmdAddSalaryVacRelation.Parameters.Add("p_salary_id", OracleDbType.Decimal, null, ParameterDirection.Input);

            cmdDeleteSalary = new OracleCommand(string.Format("begin {1}.SALARY_DELETE(:p_SALARY_ID, :p_CALC_RELATION); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmdDeleteSalary.BindByName = true;
            cmdDeleteSalary.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, ParameterDirection.Input);
            cmdDeleteSalary.Parameters.Add("p_CALC_RELATION", OracleDbType.Array, ParameterDirection.Input).UdtTypeName = "SALARY.NUMBER_COLLECTION_TYPE";

            cmdExcludeSalary = new OracleCommand(string.Format("begin {1}.SALARY_DOC_RELATION_DELETE(:p_salary_doc_relation_id);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmdExcludeSalary.BindByName = true;
            cmdExcludeSalary.Parameters.Add("p_salary_doc_relation_id", OracleDbType.Decimal, null, ParameterDirection.Input);

            cmdDeleteSalaryVac = new OracleCommand(string.Format(@"BEGIN {0}.SALARY_VAC_DELETE(:p_SALARY_VAC_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            cmdDeleteSalaryVac.BindByName = true;
            cmdDeleteSalaryVac.Parameters.Add("p_SALARY_VAC_ID", OracleDbType.Decimal, 0, "SALARY_VAC_ID").Direction = ParameterDirection.InputOutput;

            cmdLockDocum = new OracleCommand(string.Format("begin {1}.SALARY_DOCUM_LOCK(:p_salary_docum_id, :p_is_closing); end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmdLockDocum.BindByName = true;
            cmdLockDocum.Parameters.Add("p_salary_docum_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            cmdLockDocum.Parameters.Add("p_is_closing", OracleDbType.Decimal, null, ParameterDirection.Input);
    #endregion
            ds.Tables.Add("SALARY").Columns.AddRange(new DataColumn[] { new DataColumn("TYPE_PAYMENT_TYPE_ID", typeof(int)), new DataColumn("CODE_PAYMENT", typeof(string)), new DataColumn("DAYS", typeof(decimal)) });
        }

        public DataView SalaryDocum
        { 
            get
            {
                if (_empView ==null && ds.Tables.Contains("SalaryDocum"))
                    _empView=  new DataView(ds.Tables["SalaryDocum"], "", "", DataViewRowState.CurrentRows);
                return _empView;
            }
        }

        public DataRowView CurrentDocument
        {
            get
            {
                return _currentDocum;
            }
            set
            {
                _currentDocum = value;
                RaisePropertyChanged(() => CurrentDocument);
                LoadDocData(CurrentDocumID);
                RaisePropertyChanged(() => SalaryDocRelation);
                RaisePropertyChanged(() => SalaryVac);
            }
        }

        public DataRowView CurrentSalary
        {
            get
            {
                return _currentSalary;
            }
            set
            {
                _currentSalary = value;
                RaisePropertyChanged(() => CurrentSalary);
            }
        }
        public DataRowView CurrentSalaryVac
        {
            get
            {
                return _currentSalaryVac;
            }
            set
            {
                _currentSalaryVac = value;
                RaisePropertyChanged(() => CurrentSalaryVac);
            }
        }

        public DataView SalaryDocRelation
        {
            get
            {
                if (_salaryDocRelation == null && ds.Tables.Contains("SALARY"))
                    _salaryDocRelation = new DataView(ds.Tables["SALARY"], "", "CODE_PAYMENT", DataViewRowState.CurrentRows);
                return _salaryDocRelation;
            }
        }

        public DataView SalaryVac
        {
            get
            {
                if (_salaryVac == null && ds.Tables.Contains("SALARY_VAC"))
                    _salaryVac = new DataView(ds.Tables["SALARY_VAC"], "", "CODE_PAYMENT", DataViewRowState.CurrentRows);
                return _salaryVac;
            }
        }

        /// <summary>
        /// Загрузка документов
        /// </summary>
        public void LoadDocums()
        {
            bool fl = false;
            odaDocumLoad.SelectCommand.Parameters["p_date_begin"].Value= DateBegin;
            odaDocumLoad.SelectCommand.Parameters["p_date_end"].Value= DateEnd;
            odaDocumLoad.SelectCommand.Parameters["p_subdiv_id"].Value=SubdivID;
            odaDocumLoad.SelectCommand.Parameters["p_type_sal_docum_id"].Value = TypeSalDocumID;
            if (ds.Tables.Contains(odaDocumLoad.TableMappings[0].DataSetTable))
                ds.Tables[odaDocumLoad.TableMappings[0].DataSetTable].Rows.Clear();
            else fl = true;
            try
            {
                odaDocumLoad.Fill(ds);
                if (fl)
                {
                    ds.Tables[odaDocumLoad.TableMappings[0].DataSetTable].Columns.Add("FL", typeof(bool)).DefaultValue=false;
                    foreach (DataRow r in ds.Tables[odaDocumLoad.TableMappings[0].DataSetTable].Rows)
                        r["FL"] = false;
                }

                RaisePropertyChanged(() => SalaryDocum);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
            }
        }

        /// <summary>
        /// Загрузка данных по документу
        /// </summary>
        /// <param name="salaryDocumID"></param>
        public void LoadDocData(decimal? salaryDocumID)
        {
            if (ds.Tables.Contains("SALARY_VAC"))
            {
                ds.Tables["SALARY"].Rows.Clear();
                ds.Tables["SALARY_VAC"].Rows.Clear();
            }
            if (salaryDocumID != null)
            {
                odaDocData.SelectCommand.Parameters["p_salary_docum_id"].Value = salaryDocumID;
                try
                {
                    odaDocData.Fill(ds);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
                }
            }
        }
        

        /// <summary>
        /// Дата начала периода для фильтра
        /// </summary>
        public DateTime? DateBegin
        {
            get
            {
                return dateBegin;
            }
            set
            {
                dateBegin = value;
                RaisePropertyChanged(() => DateBegin);
            }
        }

        /// <summary>
        /// Дата окончания периода для фильтра документов
        /// </summary>
        public DateTime? DateEnd
        {
            get
            {
                return dateEnd;
            }
            set
            {
                dateEnd = value;
                RaisePropertyChanged(() => DateEnd);
            }
        }

        /// <summary>
        /// Подразделение выбранное
        /// </summary>
        public decimal? SubdivID
        {
            get
            {
                return _subdivID;
            }
            set
            {
                _subdivID = value;
                RaisePropertyChanged(() => SubdivID);
            }
        }

        DataSet ds = new DataSet();

        DataView _empView, _salaryDocRelation;
        decimal? _subdivID;
        DateTime? dateBegin = DateTime.Today.Trunc("Month"), 
            dateEnd = DateTime.Today.Trunc("Month").AddMonths(1).AddSeconds(-1);

        OracleDataAdapter odaDocumLoad, odaDocData;
        DataRowView _currentDocum;
        private OracleCommand cmdCalcVacDocum;

        public void CheckAllDocs(bool? nullable)
        {
            if (ds!=null && ds.Tables.Contains("SalaryDocum"))
                foreach (DataRow r in ds.Tables["SalaryDocum"].Rows)
                    r["FL"] = nullable;
        }

        public void CalcDocumens()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                Decimal[] array = this.CheckedIdsDocum;
                cmdCalcVacDocum.ArrayBindCount = array.Length;
                cmdCalcVacDocum.Parameters["p_salary_docum_id"].Value = array;
                cmdCalcVacDocum.ExecuteNonQuery();
                tr.Commit();
                LoadDocData(_currentDocum==null? null :_currentDocum.Row.Field2<Decimal?>("SALARY_DOCUM_ID"));
                RaisePropertyChanged(() => SalaryDocRelation);
                RaisePropertyChanged(() => SalaryVac);
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка расчета");
            }

            
        }

        public int GetCountChecked()
        {
            return ds.Tables.Contains("SalaryDocum")? (int)ds.Tables["SalaryDocum"].Compute("COUNT(SALARY_DOCUM_ID)", "FL=True"):0;
        }

        public decimal[] CheckedIdsDocum
        {
            get
            {
                return ds.Tables.Contains("SalaryDocum")? ds.Tables["SalaryDocum"].Select("FL=True").Select(t => t.Field2<Decimal?>("SALARY_DOCUM_ID")??-1).ToArray() :null;
            }
        }

        OracleCommand cmdCreateDoc;
        private DataView _salaryVac;
        private DataRowView _currentSalary;
        private DataRowView _currentSalaryVac;
        private OracleCommand cmdDeleteDocum;
        private OracleCommand cmdAddSalaryVacRelation;
        private OracleCommand cmdDeleteSalary;
        private OracleCommand cmdExcludeSalary;
        private OracleCommand cmdDeleteSalaryVac;
        private OracleCommand cmdLockDocum;
        private OracleCommand cmdReloadDoc;
        public void CreateDocumentsByPeriod()
        {
            
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            cmdCreateDoc.Parameters["p_date_begin"].Value = dateBegin;
            cmdCreateDoc.Parameters["p_date_end"].Value = dateEnd;
            cmdCreateDoc.Parameters["p_subdiv_id"].Value = SubdivID;
            cmdCreateDoc.Parameters["P_type_sal_docum_id"].Value = TypeSalDocumID;
            try
            {
                cmdCreateDoc.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования документов");
            }
        }

        public void DeleteCurrentDocum()
        {
            
            cmdDeleteDocum.Parameters["p_salary_doc_id"].Value = CurrentDocument["SALARY_DOCUM_ID"];
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                cmdDeleteDocum.ExecuteNonQuery();
                tr.Commit();
                LoadDocums();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления документа");
            }
        }

        public void AddNewSalaryRelation(object sender)
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                SalaryEditor f = new SalaryEditor(sender, CurrentDocument["TRANSFER_ID"], null, CurrentDocument.Row.Field2<DateTime?>("DATE_DOC") ?? DateTime.Today, tr);
                f.Owner = Window.GetWindow((DependencyObject)sender);
                if (f.ShowDialog() == true)
                {
                    cmdAddSalaryVacRelation.Parameters["p_salary_docum_id"].Value = CurrentDocumID;
                    cmdAddSalaryVacRelation.Parameters["p_salary_id"].Value = f.Model.SalaryID;

                    cmdAddSalaryVacRelation.ExecuteNonQuery();
                    tr.Commit();
                    LoadDocData(CurrentDocumID);
                    RaisePropertyChanged(() => SalaryDocRelation);
                    RaisePropertyChanged(() => SalaryVac);
                }
                else
                    tr.Rollback();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка добавления связи");
            }
        }
        public void EditSalaryRelation(object sender)
        {
            SalaryEditor f = new SalaryEditor(sender, CurrentDocument["TRANSFER_ID"], CurrentSalary["SALARY_ID"], CurrentDocument.Row.Field2<DateTime?>("DATE_DOC") ?? DateTime.Today);
            f.Owner = Window.GetWindow((DependencyObject)sender);
            if (f.ShowDialog() == true)
            {
                LoadDocData(CurrentDocumID);
            }
        }
        public decimal? CurrentDocumID
        {
            get
            {
                if (CurrentDocument != null)
                    return CurrentDocument.Row.Field2<Decimal?>("SALARY_DOCUM_ID");
                else return null;
            }
        }

        public void DeleteCurrentSalary()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                cmdDeleteSalary.Parameters["p_SALARY_ID"].Value = CurrentSalary["SALARY_ID"];
                cmdDeleteSalary.ExecuteNonQuery();
                tr.Commit();
                LoadDocData(CurrentDocumID);
                RaisePropertyChanged(() => SalaryDocRelation);
                RaisePropertyChanged(() => SalaryVac);
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException());
            }
        }

        public void ExcludeCurrentSalary()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                cmdExcludeSalary.Parameters["p_SALARY_DOC_RELATION_ID"].Value = CurrentSalary["SALARY_DOC_RELATION_ID"];
                cmdExcludeSalary.ExecuteNonQuery();
                tr.Commit();
                LoadDocData(CurrentDocumID);
                RaisePropertyChanged(() => SalaryDocRelation);
                RaisePropertyChanged(() => SalaryVac);
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException());
            }
        }

        public void DeleteSalaryVacCurrent()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                cmdDeleteSalaryVac.Parameters["P_SALARY_VAC_ID"].Value = CurrentSalaryVac["SALARY_VAC_ID"];
                cmdDeleteSalaryVac.ExecuteNonQuery();
                tr.Commit();
                LoadDocData(CurrentDocumID);
                RaisePropertyChanged(() => SalaryDocRelation);
                RaisePropertyChanged(() => SalaryVac);
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления");
            }
        }

        public void ChangeLockChecked(object p)
        {
            decimal d = Convert.ToDecimal(p);
            cmdLockDocum.Parameters["p_salary_docum_id"].Value = CheckedIdsDocum;
            cmdLockDocum.ArrayBindCount = CheckedIdsDocum.Length;
            cmdLockDocum.Parameters["p_is_closing"].Value = CheckedIdsDocum.Select(t => d).ToArray();
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                cmdLockDocum.ExecuteNonQuery();
                tr.Commit();
                LoadDocums();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка выполнения");
            }
        }

        /// <summary>
        /// Доформирование документа, согласно записям заработной платы
        /// </summary>
        internal void ReformEmpDocum()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            cmdReloadDoc.Parameters["p_date_begin"].Value = dateBegin;
            cmdReloadDoc.Parameters["p_date_end"].Value = dateEnd;
            cmdReloadDoc.Parameters["p_subdiv_id"].Value = SubdivID;
            cmdReloadDoc.Parameters["p_salary_docum_id"].Value = CurrentDocumID;
            cmdReloadDoc.Parameters["p_type_sal_docum_id"].Value = TypeSalDocumID;
            try
            {
                cmdReloadDoc.ExecuteNonQuery();
                tr.Commit();
                LoadDocData(CurrentDocumID);
                RaisePropertyChanged(() => SalaryDocRelation);
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Ошибка формирования документов");
            }
        }

        decimal? _typeSalDocumID;
        private OracleDataAdapter odaSalaryDocumsData;
        /// <summary>
        /// Тип документа.
        /// </summary>
        public decimal? TypeSalDocumID
        {
            get
            {
                return _typeSalDocumID;
            }
            set
            {
                _typeSalDocumID = value;
                RaisePropertyChanged(() => TypeSalDocumID);
            }
        }

        /// <summary>
        /// Источник данных для типов документов
        /// </summary>
        public DataView TypeDocumSource
        {
            get
            {
                return ds.Tables["TYPE_SAL_DOCUM"].DefaultView;
            }
        }
    }

}
