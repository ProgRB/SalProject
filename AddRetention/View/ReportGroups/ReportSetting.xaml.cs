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
using Oracle.DataAccess.Client;
using System.Data;
using Salary.Helpers;
using LibrarySalary.Helpers;
using System.ComponentModel;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for ReportSetting.xaml
    /// </summary>
    public partial class ReportSetting : Window
    {
        ReportSettingModel _model;
        
        public ReportSetting(object p_REPORT_GROUP_ID, object p_PARENT_GROUP_ID)
        {
            _model = new ReportSettingModel((decimal?)p_REPORT_GROUP_ID, (decimal?) p_PARENT_GROUP_ID);
            InitializeComponent();
            DataContext = _model;
        }

        public ReportSettingModel Model
        {
            get
            {
                return _model;
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model != null && ControlRoles.GetState(e.Command) && !Model.HasErrors() && Model.HasChanges;
        }

        private void SaveRetent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgTaxedPayType.CancelEdit();
            dgOrderFilter.CancelEdit();
            if (Model.Save())
            {
                DialogResult = true;
                this.Close();
            }
        }


        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SelectAll_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter!=null)
                foreach (DataRowView dr in Model.SettingPaymentSource)
                {
                    dr[e.Parameter.ToString()] = (e.Source as CheckBox).IsChecked;
                }
            e.Handled = true;
        }

        /// <summary>
        /// При переходе со поля заполняем краткое имя и даты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Model.ReportGroupID == null && string.IsNullOrEmpty(Model.ShortGroupName))
            {
                Model.ShortGroupName = Model.GroupName;
                Model.DateBeginReport = new DateTime(2000, 1, 1);
                Model.DateEndReport = new DateTime(2100, 12, 31);
            }
        }
    }

    public class ReportSettingModel : EntityGenerator.ReportGroup, IDataErrorInfo
    {
        OracleDataAdapter odaReport_Setting, odaReport_Group, odaReport_Setting_Order, odaReport_Setting_Subdiv;
        DataSet ds;
        public ReportSettingModel(decimal? reportGroupId, decimal? parentGroupID)
        {

            ds = new DataSet();

            odaReport_Group = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectReportGroupData.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Group.SelectCommand.BindByName = true;
            odaReport_Group.AcceptChangesDuringUpdate = false;
            odaReport_Group.SelectCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, reportGroupId, ParameterDirection.Input);
            odaReport_Group.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaReport_Group.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaReport_Group.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            odaReport_Group.SelectCommand.Parameters.Add("c4", OracleDbType.RefCursor, ParameterDirection.Output);
            odaReport_Group.SelectCommand.Parameters.Add("c5", OracleDbType.RefCursor, ParameterDirection.Output);

            odaReport_Group.TableMappings.Add("Table", "REPORT_GROUP");
            odaReport_Group.TableMappings.Add("Table1", "REPORT_SETTING");
            odaReport_Group.TableMappings.Add("Table2", "REPORT_ALL");
            odaReport_Group.TableMappings.Add("Table3", "REPORT_SETTING_SUBDIV");
            odaReport_Group.TableMappings.Add("Table4", "REPORT_SETTING_ORDER");

            #region   адаптер основной
            odaReport_Group.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.REPORT_GROUP_UPDATE(:p_REPORT_GROUP_ID,:p_GROUP_CODE,:p_GROUP_NAME,:p_SHORT_GROUP_NAME,:p_DATE_BEGIN_REPORT,:p_DATE_END_REPORT,:p_SORT_NUMBER,:p_PARENT_GROUP_ID, :p_GROUP_COMMENT);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Group.InsertCommand.BindByName = true;
            odaReport_Group.InsertCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, 0, "REPORT_GROUP_ID").Direction = ParameterDirection.InputOutput;
            odaReport_Group.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaReport_Group.InsertCommand.Parameters["p_REPORT_GROUP_ID"].DbType = DbType.Decimal;
            odaReport_Group.InsertCommand.Parameters.Add("p_GROUP_CODE", OracleDbType.Varchar2, 0, "GROUP_CODE").Direction = ParameterDirection.Input;
            odaReport_Group.InsertCommand.Parameters.Add("p_GROUP_NAME", OracleDbType.Varchar2, 0, "GROUP_NAME").Direction = ParameterDirection.Input;
            odaReport_Group.InsertCommand.Parameters.Add("p_SHORT_GROUP_NAME", OracleDbType.Varchar2, 0, "SHORT_GROUP_NAME").Direction = ParameterDirection.Input;
            odaReport_Group.InsertCommand.Parameters.Add("p_DATE_BEGIN_REPORT", OracleDbType.Date, 0, "DATE_BEGIN_REPORT").Direction = ParameterDirection.Input;
            odaReport_Group.InsertCommand.Parameters.Add("p_DATE_END_REPORT", OracleDbType.Date, 0, "DATE_END_REPORT").Direction = ParameterDirection.Input;
            odaReport_Group.InsertCommand.Parameters.Add("p_SORT_NUMBER", OracleDbType.Decimal, 0, "SORT_NUMBER").Direction = ParameterDirection.Input;
            odaReport_Group.InsertCommand.Parameters.Add("p_PARENT_GROUP_ID", OracleDbType.Decimal, 0, "PARENT_GROUP_ID").Direction = ParameterDirection.Input;
            odaReport_Group.InsertCommand.Parameters.Add("p_GROUP_COMMENT", OracleDbType.Varchar2, 0, "GROUP_COMMENT").Direction = ParameterDirection.Input;

            odaReport_Group.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.REPORT_GROUP_UPDATE(:p_REPORT_GROUP_ID,:p_GROUP_CODE,:p_GROUP_NAME,:p_SHORT_GROUP_NAME,:p_DATE_BEGIN_REPORT,:p_DATE_END_REPORT,:p_SORT_NUMBER,:p_PARENT_GROUP_ID, :p_GROUP_COMMENT);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Group.UpdateCommand.BindByName = true;
            odaReport_Group.UpdateCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, 0, "REPORT_GROUP_ID").Direction = ParameterDirection.InputOutput;
            odaReport_Group.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaReport_Group.UpdateCommand.Parameters["p_REPORT_GROUP_ID"].DbType = DbType.Decimal;
            odaReport_Group.UpdateCommand.Parameters.Add("p_GROUP_CODE", OracleDbType.Varchar2, 0, "GROUP_CODE").Direction = ParameterDirection.Input;
            odaReport_Group.UpdateCommand.Parameters.Add("p_GROUP_NAME", OracleDbType.Varchar2, 0, "GROUP_NAME").Direction = ParameterDirection.Input;
            odaReport_Group.UpdateCommand.Parameters.Add("p_SHORT_GROUP_NAME", OracleDbType.Varchar2, 0, "SHORT_GROUP_NAME").Direction = ParameterDirection.Input;
            odaReport_Group.UpdateCommand.Parameters.Add("p_DATE_BEGIN_REPORT", OracleDbType.Date, 0, "DATE_BEGIN_REPORT").Direction = ParameterDirection.Input;
            odaReport_Group.UpdateCommand.Parameters.Add("p_DATE_END_REPORT", OracleDbType.Date, 0, "DATE_END_REPORT").Direction = ParameterDirection.Input;
            odaReport_Group.UpdateCommand.Parameters.Add("p_SORT_NUMBER", OracleDbType.Decimal, 0, "SORT_NUMBER").Direction = ParameterDirection.Input;
            odaReport_Group.UpdateCommand.Parameters.Add("p_PARENT_GROUP_ID", OracleDbType.Decimal, 0, "PARENT_GROUP_ID").Direction = ParameterDirection.Input;
            odaReport_Group.UpdateCommand.Parameters.Add("p_GROUP_COMMENT", OracleDbType.Varchar2, 0, "GROUP_COMMENT").Direction = ParameterDirection.Input;
            

            odaReport_Group.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.REPORT_GROUP_DELETE(:p_REPORT_GROUP_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Group.DeleteCommand.BindByName = true;
            odaReport_Group.DeleteCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, 0, "REPORT_GROUP_ID").Direction = ParameterDirection.InputOutput;
#endregion
            #region настройки видом оплат адаптер
            odaReport_Setting = new OracleDataAdapter();
            odaReport_Setting.AcceptChangesDuringUpdate = false;
            odaReport_Setting.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.REPORT_SETTING_UPDATE(:p_REPORT_SETTING_ID,:p_REPORT_GROUP_ID,:p_PAYMENT_TYPE_ID,:p_GROUP_SORT_ORDER);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Setting.InsertCommand.BindByName = true;
            odaReport_Setting.InsertCommand.Parameters.Add("p_REPORT_SETTING_ID", OracleDbType.Decimal, 0, "REPORT_SETTING_ID").Direction = ParameterDirection.InputOutput;
            odaReport_Setting.InsertCommand.Parameters["p_REPORT_SETTING_ID"].DbType = DbType.Decimal;
            odaReport_Setting.InsertCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, 0, "REPORT_GROUP_ID").Direction = ParameterDirection.Input;
            odaReport_Setting.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input;
            odaReport_Setting.InsertCommand.Parameters.Add("p_GROUP_SORT_ORDER", OracleDbType.Decimal, 0, "GROUP_SORT_ORDER").Direction = ParameterDirection.Input;

            odaReport_Setting.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.REPORT_SETTING_UPDATE(:p_REPORT_SETTING_ID,:p_REPORT_GROUP_ID,:p_PAYMENT_TYPE_ID,:p_GROUP_SORT_ORDER);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Setting.UpdateCommand.BindByName = true;
            odaReport_Setting.UpdateCommand.Parameters.Add("p_REPORT_SETTING_ID", OracleDbType.Decimal, 0, "REPORT_SETTING_ID").Direction = ParameterDirection.InputOutput;
            odaReport_Setting.UpdateCommand.Parameters["p_REPORT_SETTING_ID"].DbType = DbType.Decimal;
            odaReport_Setting.UpdateCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, 0, "REPORT_GROUP_ID").Direction = ParameterDirection.Input;
            odaReport_Setting.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.Input;
            odaReport_Setting.UpdateCommand.Parameters.Add("p_GROUP_SORT_ORDER", OracleDbType.Decimal, 0, "GROUP_SORT_ORDER").Direction = ParameterDirection.Input;

            odaReport_Setting.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.REPORT_SETTING_DELETE(:p_REPORT_SETTING_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Setting.DeleteCommand.BindByName = true;
            odaReport_Setting.DeleteCommand.Parameters.Add("p_REPORT_SETTING_ID", OracleDbType.Decimal).Direction = ParameterDirection.Input;
            #endregion

            #region настройки заказов

            odaReport_Setting_Order = new OracleDataAdapter("", Connect.CurConnect);
            odaReport_Setting_Order.AcceptChangesDuringUpdate = false;
            odaReport_Setting_Order.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.REPORT_SETTING_ORDER_UPDATE(:p_REPORT_SETTING_ORDER_ID,:p_REPORT_GROUP_ID,:p_ORDER_FILTER);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Setting_Order.InsertCommand.BindByName = true;
            odaReport_Setting_Order.InsertCommand.Parameters.Add("p_REPORT_SETTING_ORDER_ID", OracleDbType.Decimal, 0, "REPORT_SETTING_ORDER_ID").Direction = ParameterDirection.InputOutput;
            odaReport_Setting_Order.InsertCommand.Parameters["p_REPORT_SETTING_ORDER_ID"].DbType = DbType.Decimal;
            odaReport_Setting_Order.InsertCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaReport_Setting_Order.InsertCommand.Parameters.Add("p_ORDER_FILTER", OracleDbType.Varchar2, 0, "ORDER_FILTER").Direction = ParameterDirection.Input; 
            
            odaReport_Setting_Order.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.REPORT_SETTING_ORDER_UPDATE(:p_REPORT_SETTING_ORDER_ID,:p_REPORT_GROUP_ID,:p_ORDER_FILTER);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Setting_Order.UpdateCommand.BindByName = true;
            odaReport_Setting_Order.UpdateCommand.Parameters.Add("p_REPORT_SETTING_ORDER_ID", OracleDbType.Decimal, 0, "REPORT_SETTING_ORDER_ID").Direction = ParameterDirection.InputOutput;
            odaReport_Setting_Order.UpdateCommand.Parameters["p_REPORT_SETTING_ORDER_ID"].DbType = DbType.Decimal;
            odaReport_Setting_Order.UpdateCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaReport_Setting_Order.UpdateCommand.Parameters.Add("p_ORDER_FILTER", OracleDbType.Varchar2, 0, "ORDER_FILTER").Direction = ParameterDirection.Input; 
            
            odaReport_Setting_Order.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.REPORT_SETTING_ORDER_DELETE(:p_REPORT_SETTING_ORDER_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Setting_Order.DeleteCommand.BindByName = true;
            odaReport_Setting_Order.DeleteCommand.Parameters.Add("p_REPORT_SETTING_ORDER_ID", OracleDbType.Decimal, 0, "REPORT_SETTING_ORDER_ID").Direction = ParameterDirection.InputOutput;
            #endregion

            #region Настройки подразделений

            odaReport_Setting_Subdiv = new OracleDataAdapter("", Connect.CurConnect);

            odaReport_Setting_Subdiv.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.REPORT_SETTING_SUBDIV_UPDATE(:p_REPORT_SETTING_SUBDIV_ID,:p_SUBDIV_ID,:p_REPORT_GROUP_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Setting_Subdiv.InsertCommand.BindByName = true;
            odaReport_Setting_Subdiv.InsertCommand.Parameters.Add("p_REPORT_SETTING_SUBDIV_ID", OracleDbType.Decimal, null, ParameterDirection.InputOutput);
            odaReport_Setting_Subdiv.InsertCommand.Parameters["p_REPORT_SETTING_SUBDIV_ID"].DbType = DbType.Decimal;
            odaReport_Setting_Subdiv.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaReport_Setting_Subdiv.InsertCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, null, ParameterDirection.Input); 
            
            /*odaReport_Setting_Subdiv.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.REPORT_SETTING_SUBDIV_UPDATE(:p_REPORT_SETTING_SUBDIV_ID,:p_SUBDIV_ID,:p_REPORT_GROUP_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Setting_Subdiv.UpdateCommand.BindByName = true;
            odaReport_Setting_Subdiv.UpdateCommand.Parameters.Add("p_REPORT_SETTING_SUBDIV_ID", OracleDbType.Decimal, 0, "REPORT_SETTING_SUBDIV_ID").Direction = ParameterDirection.InputOutput;
            odaReport_Setting_Subdiv.UpdateCommand.Parameters["p_REPORT_SETTING_SUBDIV_ID"].DbType = DbType.Decimal;
            odaReport_Setting_Subdiv.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input;
            odaReport_Setting_Subdiv.UpdateCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, 0, "REPORT_GROUP_ID").Direction = ParameterDirection.Input; 
            */
            odaReport_Setting_Subdiv.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.REPORT_SETTING_SUBDIV_DELETE(:p_REPORT_SETTING_SUBDIV_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaReport_Setting_Subdiv.DeleteCommand.BindByName = true;
            odaReport_Setting_Subdiv.DeleteCommand.Parameters.Add("p_REPORT_SETTING_SUBDIV_ID", OracleDbType.Decimal, null, ParameterDirection.InputOutput);
            #endregion

            odaReport_Group.Fill(ds);

            if (ds.Tables["REPORT_GROUP"].Rows.Count == 0)
            {
                ds.Tables["REPORT_GROUP"].Rows.Add(ds.Tables["REPORT_GROUP"].NewRow());
            }
            DataRow = ds.Tables["REPORT_GROUP"].Rows[0];
            if (reportGroupId == null && parentGroupID != null)
                ParentGroupID = parentGroupID;

        }

        /// <summary>
        /// Источник данных для указания родительской группы
        /// </summary>
        public DataView ReportParentSource
        {
            get
            {
                return new DataView(ds.Tables["REPORT_ALL"], "", "GROUP_CODE", DataViewRowState.CurrentRows);
            }
        }

        DataView _settingSubdivSource;

        /// <summary>
        /// Источник данных для фильтров подразделений
        /// </summary>
        public DataView SettingSubdivSource
        {
            get
            {
                if (_settingSubdivSource == null && ds.Tables.Contains("REPORT_SETTING_SUBDIV"))
                {
                    _settingSubdivSource = new DataView(ds.Tables["REPORT_SETTING_SUBDIV"], "", "CODE_SUBDIV", DataViewRowState.CurrentRows);
                }
                return _settingSubdivSource;
            }

            set
            {
                _settingSubdivSource = value;
                RaisePropertyChanged(() => SettingSubdivSource);
            }
        }

        DataView _settingOrderSource;

        /// <summary>
        /// Источник данных для фильтров заказов
        /// </summary>
        public DataView SettingOrderSource
        {
            get
            {
                if (_settingOrderSource == null && ds.Tables.Contains("REPORT_SETTING_ORDER"))
                    _settingOrderSource = new DataView(ds.Tables["REPORT_SETTING_ORDER"], "", "", DataViewRowState.CurrentRows);
                return _settingOrderSource;
            }
            set
            {
                _settingOrderSource = value;
                RaisePropertyChanged(() => SettingOrderSource);
            }
        }

        /// <summary>
        /// Источник данных для списка видов оплат
        /// </summary>
        public DataView SettingPaymentSource
        {
            get
            {
                return new DataView(ds.Tables["REPORT_SETTING"], string.Format("TYPE_PAYMENT_TYPE_ID={0}", SelectedTypePaymentTypeID), "CODE_PAYMENT", DataViewRowState.CurrentRows);
            }
        }

        decimal? _selectedTypeID = 1;

        /// <summary>
        /// Выбранный подтип оплат, для фильтрации типов оплат
        /// </summary>
        public decimal? SelectedTypePaymentTypeID
        {
            get
            {
                return _selectedTypeID;
            }
            set
            {
                _selectedTypeID = value;
                RaisePropertyChanged(() => SelectedTypePaymentTypeID);
                RaisePropertyChanged(() => SettingPaymentSource);
            }
        }


        /// <summary>
        /// Сохранение данных
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                if (ds.Tables["REPORT_GROUP"].GetChanges() != null)
                {
                    odaReport_Group.Update(new DataRow[] { ds.Tables["REPORT_GROUP"].Rows[0] });
                }
                foreach (DataRow r in ds.Tables["REPORT_SETTING"].Rows)
                    if (r.RowState == DataRowState.Modified)
                    {
                        if (r["REPORT_SETTING_ID"] == DBNull.Value)
                        {
                            if ((decimal?)r["USE_FOR_CALC"] > 0)
                            {
                                r.AcceptChanges();
                                r.SetAdded();
                                r["REPORT_GROUP_ID"] =  ReportGroupID;
                                odaReport_Setting.Update(new DataRow[] { r });
                            }
                        }
                        else
                            if ((decimal?)r["USE_FOR_CALC"] > 0)
                            {
                                r.AcceptChanges();
                                r.SetModified();
                                r["REPORT_GROUP_ID"] = ReportGroupID;
                                odaReport_Setting.Update(new DataRow[] { r });
                            }
                            else
                            {
                                odaReport_Setting.DeleteCommand.Parameters["p_REPORT_SETTING_ID"].Value = r["REPORT_SETTING_ID"];
                                odaReport_Setting.DeleteCommand.ExecuteNonQuery();
                            }
                    }

                odaReport_Setting_Order.UpdateCommand.Parameters["p_REPORT_GROUP_ID"].Value = odaReport_Setting_Order.InsertCommand.Parameters["p_REPORT_GROUP_ID"].Value =
                    this.ReportGroupID;
                foreach (DataRow r in ds.Tables["REPORT_SETTING_ORDER"].Rows)
                    if (r.RowState== DataRowState.Added)
                    {
                        r["REPORT_SETTING_ORDER_ID"] = DBNull.Value;
                    }
                odaReport_Setting_Order.Update(ds.Tables["REPORT_SETTING_ORDER"]);

                Dictionary<int, decimal> dic_updated = new Dictionary<int,decimal>();
                for (int i=0;i< ds.Tables["REPORT_SETTING_SUBDIV"].Rows.Count; ++i)
                {
                    DataRow r = ds.Tables["REPORT_SETTING_SUBDIV"].Rows[i];
                    if (r.RowState == DataRowState.Modified)
                    {
                        if (r.Field2<Decimal?>("FL") == 1 && r["REPORT_SETTING_SUBDIV_ID"]==DBNull.Value)
                        {
                            odaReport_Setting_Subdiv.InsertCommand.Parameters["p_REPORT_SETTING_SUBDIV_ID"].Value = null;
                            odaReport_Setting_Subdiv.InsertCommand.Parameters["p_REPORT_GROUP_ID"].Value = this.ReportGroupID;
                            odaReport_Setting_Subdiv.InsertCommand.Parameters["p_SUBDIV_ID"].Value = r["SUBDIV_ID"];
                            odaReport_Setting_Subdiv.InsertCommand.ExecuteNonQuery();
                            dic_updated.Add(i, (Decimal)odaReport_Setting_Subdiv.InsertCommand.Parameters["p_REPORT_SETTING_SUBDIV_ID"].Value);
                        }
                        else 
                            if (r.Field2<Decimal?>("FL")==0 && r["REPORT_SETTING_SUBDIV_ID"]!=DBNull.Value)
                            {
                                odaReport_Setting_Subdiv.DeleteCommand.Parameters["p_REPORT_SETTING_SUBDIV_ID"].Value = r["REPORT_SETTING_SUBDIV_ID"];
                                odaReport_Setting_Subdiv.DeleteCommand.ExecuteNonQuery();
                            }
                    }
                }
                
                ///Помечаем записи что они вставлены - присваиваем айдишники
                foreach (var p in dic_updated)
                {
                    ds.Tables["REPORT_SETTING_SUBDIV"].Rows[p.Key]["REPORT_SETTING_SUBDIV_ID"] = p.Value;
                }

                foreach (DataRow r in ds.Tables["REPORT_SETTING_SUBDIV"].Rows.OfType<DataRow>().Where(p => p.Field2<Decimal?>("FL") == 0 && p["REPORT_SETTING_SUBDIV_ID"] != DBNull.Value))
                {
                    r["REPORT_SETTING_SUBDIV_ID"] = DBNull.Value;
                }

                ds.AcceptChanges();
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show("Ошибка сохранения" + ex.GetFormattedException());
                return false;
            }
        }

        /// <summary>
        /// Есть ли ошибки у модели
        /// </summary>
        /// <returns></returns>
        internal bool HasErrors()
        {
            return !string.IsNullOrEmpty(Error);
        }

        /// <summary>
        /// Ошибка модели 
        /// </summary>
        public string Error
        {
            get 
            {
                if (string.IsNullOrEmpty(GroupCode) || DateBeginReport == null || DateEndReport == null)
                    return "Не указан обязательный элемент";
                return string.Empty;
            }
        }

        /// <summary>
        /// Ошибка свойства модели
        /// </summary>
        /// <param name="column_name"></param>
        /// <returns></returns>
        public new string this[string column_name]
        {
            get
            {
                if (column_name == "GroupCode" && string.IsNullOrEmpty(GroupCode)) return "Требуется указать код группы фильтров";
                if (column_name == "DateBeginReport" && DateBeginReport == null) return "Укажите дату начала действия";
                if (column_name == "DateEndReport" && DateEndReport == null) return "Укажите дату начала действия";
                return string.Empty;
            }
        }

        public bool HasChanges
        {
            get
            {
                return ds != null && ds.HasChanges();
            }
        }
    }
}
