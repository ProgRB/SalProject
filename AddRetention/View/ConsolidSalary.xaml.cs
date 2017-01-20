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
using System.ComponentModel;
using System.Data;
using Oracle.DataAccess.Client;
using Salary.Helpers;
using System.Linq.Expressions;
using Salary.ViewModel;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for ConsolidSalary.xaml
    /// </summary>
    public partial class ConsolidSalary : UserControl, INotifyPropertyChanged
    {
        ReportGroupList ls;
        OracleDataAdapter odaConsolid_Group;
        DataSet ds;
        public ConsolidSalary()
        {
            ds = new DataSet();
            odaConsolid_Group = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectConsolidGroup.sql"), Connect.CurConnect);
            odaConsolid_Group.SelectCommand.BindByName = true;
            odaConsolid_Group.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, ParameterDirection.Input);
            odaConsolid_Group.SelectCommand.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, ParameterDirection.Input);
            odaConsolid_Group.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            odaConsolid_Group.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            odaConsolid_Group.TableMappings.Add("Table", "Report_GROUP");
            odaConsolid_Group.TableMappings.Add("Table1", "Consolid");
            odaConsolid_Group.InsertCommand = new OracleCommand(string.Format(@"BEGIN {0}.CONSOLID_GROUP_UPDATE(:p_CONSOLID_GROUP_ID,:p_REPORT_GROUP_ID,:p_REPORT_DATE,:p_CONSOLID_SUM,:p_DEC_GROUP_ID,:p_SUBDIV_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaConsolid_Group.InsertCommand.BindByName = true;
            odaConsolid_Group.InsertCommand.Parameters.Add("p_CONSOLID_GROUP_ID", OracleDbType.Decimal, 0, "CONSOLID_GROUP_ID").Direction = ParameterDirection.InputOutput;
            odaConsolid_Group.InsertCommand.Parameters["p_CONSOLID_GROUP_ID"].DbType = DbType.Decimal;
            odaConsolid_Group.InsertCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, 0, "REPORT_GROUP_ID").Direction = ParameterDirection.Input;
            odaConsolid_Group.InsertCommand.Parameters.Add("p_REPORT_DATE", OracleDbType.Date, 0, "REPORT_DATE").Direction = ParameterDirection.Input;
            odaConsolid_Group.InsertCommand.Parameters.Add("p_CONSOLID_SUM", OracleDbType.Decimal, 0, "CONSOLID_SUM").Direction = ParameterDirection.Input;
            odaConsolid_Group.InsertCommand.Parameters.Add("p_DEC_GROUP_ID", OracleDbType.Decimal, 0, "DEC_GROUP_ID").Direction = ParameterDirection.Input;
            odaConsolid_Group.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input; odaConsolid_Group.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {0}.CONSOLID_GROUP_UPDATE(:p_CONSOLID_GROUP_ID,:p_REPORT_GROUP_ID,:p_REPORT_DATE,:p_CONSOLID_SUM,:p_DEC_GROUP_ID,:p_SUBDIV_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaConsolid_Group.UpdateCommand.BindByName = true;
            odaConsolid_Group.UpdateCommand.Parameters.Add("p_CONSOLID_GROUP_ID", OracleDbType.Decimal, 0, "CONSOLID_GROUP_ID").Direction = ParameterDirection.InputOutput;
            odaConsolid_Group.UpdateCommand.Parameters["p_CONSOLID_GROUP_ID"].DbType = DbType.Decimal;
            odaConsolid_Group.UpdateCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, 0, "REPORT_GROUP_ID").Direction = ParameterDirection.Input;
            odaConsolid_Group.UpdateCommand.Parameters.Add("p_REPORT_DATE", OracleDbType.Date, 0, "REPORT_DATE").Direction = ParameterDirection.Input;
            odaConsolid_Group.UpdateCommand.Parameters.Add("p_CONSOLID_SUM", OracleDbType.Decimal, 0, "CONSOLID_SUM").Direction = ParameterDirection.Input;
            odaConsolid_Group.UpdateCommand.Parameters.Add("p_DEC_GROUP_ID", OracleDbType.Decimal, 0, "DEC_GROUP_ID").Direction = ParameterDirection.Input;
            odaConsolid_Group.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.Input; odaConsolid_Group.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {0}.CONSOLID_GROUP_DELETE(:p_CONSOLID_GROUP_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
            odaConsolid_Group.DeleteCommand.BindByName = true;
            odaConsolid_Group.DeleteCommand.Parameters.Add("p_CONSOLID_GROUP_ID", OracleDbType.Decimal, 0, "CONSOLID_GROUP_ID").Direction = ParameterDirection.InputOutput;

            odaConsolid_Group.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            odaConsolid_Group.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

            InitializeComponent();
            this.DataContext = this;
        }

        DataView cons_view;
        public DataView ReportGroup
        { 
            get
            {
                if (ds == null || !ds.Tables.Contains("Report_GROUP"))
                {
                    UpdateConsolid();
                    cons_view = new DataView(ds.Tables["Report_GROUP"], "", "", DataViewRowState.CurrentRows);
                    ds.Relations.Add("group_id_fk", ds.Tables["REPORT_GROUP"].Columns["REPORT_GROUP_ID"], ds.Tables["CONSOLID"].Columns["DEC_GROUP_ID"], true);
                    ds.Tables["REPORT_GROUP"].Columns.Add("REM_SUM_SAL", typeof(decimal)).Expression = "SUM_SAL-ISNULL(sum(child(group_id_fk).CONSOLID_SUM),0)";
                }
                if (TypeCostID == null)
                    return null;
                if (ls == null)
                    GetGroups(CurrentDate);
                string s = string.Join(",", ls[TypeCostID].AllChildGroup.Select(t=>t.ReportGroupID));
                cons_view.RowFilter = string.Format("report_group_id in ({0})", s);
                return cons_view;
            }
        }

        private void UpdateConsolid()
        {
            MessageBoxResult mb = MessageBoxResult.Cancel;
            if (ds.HasChanges())
            {
                mb=MessageBox.Show("Обнаружены несохраненные изменения. Сохранить изменения перед обновлением данных?", "Внимание", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (mb== MessageBoxResult.Yes)
                {
                    AppCommands.SaveColsolidReport.Execute(this, null);
                }
                else if (mb == MessageBoxResult.Cancel)
                {
                    filterBindingGroup.CancelEdit();
                    return;
                }
            }
            filterBindingGroup.UpdateSources();
            if (ds!=null && ds.Tables.Contains("Report_GROUP"))
            {
                ds.Tables["CONSOLID"].Rows.Clear();
                ds.Tables["Report_GROUP"].Rows.Clear();
            }
            odaConsolid_Group.SelectCommand.Parameters["p_date"].Value = CurrentDate;
            odaConsolid_Group.SelectCommand.Parameters["p_subdiv_id"].Value = SubdivID;
            try
            {
                odaConsolid_Group.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка получения данных");
            }
        }

        private void GetGroups(DateTime cur_date)
        {
            ReportGroupList.UpdateReportGroups(cur_date);
            ls = ReportGroupList.BuildTree().RollSection;
        }
        public IEnumerable<ReportGroup> GroupItemsSource
        { 
            get
            {
                if (ls == null)
                    GetGroups(CurrentDate);
                if (TypeCostID == null)
                    return null;
                if (ls == null)
                    GetGroups(CurrentDate);
                decimal[]  s = ls[TypeCostID].AllChildGroup.Select(t=>t.ReportGroupID).ToArray();
                return ls.Where(r=>s.Contains(r.ReportGroupID));
            }
        }

        DateTime cur_date = DateTime.Now;
        public DateTime CurrentDate
        {
            get
            {
                return cur_date;
            }
            set
            {
                cur_date = value;
                RaisePropertyChanged(() => CurrentDate);
                RaisePropertyChanged(() => ReportGroup);
            }
        }

        public decimal? SubdivID
        {
            get
            {
                return _subdiv_id;
            }
            set
            {
                _subdiv_id = value;
                RaisePropertyChanged(() => SubdivID);
                RaisePropertyChanged(() => ReportGroup);
            }
        }

        public decimal? TypeCostID
        {
            get
            {
                return _typeCostID;
            }
            set
            {
                _typeCostID = value;
                RaisePropertyChanged(() => TypeCostID);
                RaisePropertyChanged(() => ReportGroup);
            }
        }

        #region INotify data
        public event PropertyChangedEventHandler PropertyChanged;
        private decimal? _subdiv_id = 2;
        private decimal? _typeCostID;
        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            RaisePropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private DataTable Consolid
        {
            get
            {
                return ds.Tables["Consolid"];
            }
        }

        private void Add_executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView r = e.Parameter as DataRowView;
            DataRow new_row = Consolid.NewRow();
            new_row["dec_group_id"] = r["REPORT_GROUP_ID"];
            new_row["REPORT_DATE"] = CurrentDate;
            new_row["SUBDIV_ID"] = SubdivID;
            Consolid.Rows.Add(new_row);
        }


        private void AddConsolid_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void Delete_executed(object sender, ExecutedRoutedEventArgs e)
        {
            (e.Parameter as DataRowView).Delete();
        }

        private void DeleteConsolid_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Parameter != null && ControlRoles.GetState(e.Command);
        }

        private void Save_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && ds.HasChanges();
        }

        private void Save_executed(object sender, ExecutedRoutedEventArgs e)
        {
            object k = Consolid.Compute("COUNT(REPORT_DATE)", "ISNULL(REPORT_GROUP_ID,-1)=-1");
            if (k != null && Convert.ToInt32(k) > 0)
            {
                MessageBox.Show("Не выбрана статья назначения затрат");
                return;
            }
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            try
            {
                foreach (DataRow r in Consolid.Rows)
                {
                    if (r.RowState == DataRowState.Added)
                        r["CONSOLID_GROUP_ID"] = DBNull.Value;
                }
                odaConsolid_Group.Update(Consolid);
                tr.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFormattedException(), "Ошибка сохранения данных");
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            UpdateConsolid();
            RaisePropertyChanged(() => ReportGroup);
        }

        private void GroupBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                Filter_Click(this, null);
        }

        private void listInnerRelation_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListView, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null && !item.IsSelected)
            {
                item.IsSelected = true;
            }
        }

    }


    public class ReportGroupList : List<ReportGroup>, INotifyPropertyChanged
    {
        static DataTable reportGroupTable;
        static OracleDataAdapter oda_ReportGroup;
        static ReportGroupList()
        {
            oda_ReportGroup = new OracleDataAdapter(Queries.GetQueryWithSchema("SelectrReportGroup.sql"), Connect.CurConnect);
            oda_ReportGroup.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, DateTime.Now, ParameterDirection.Input);
            oda_ReportGroup.TableMappings.Add("Table", "ReportGroup");
            reportGroupTable = new DataTable();
        }

        public ReportGroupList RollSection
        {
            get
            {
                ReportGroupList l = new ReportGroupList();
                foreach (ReportGroup s in this)
                    l.AddRange(RollInnerNodes(s));
                return l;
            }
        }
        private ReportGroupList RollInnerNodes(ReportGroup ss)
        {
            ReportGroupList ll = new ReportGroupList();
            ll.Add(ss);
            foreach (ReportGroup  s in ss.ChildGroup)
            {
                ll.AddRange(RollInnerNodes(s));
            }
            return ll;
        }
        public static ReportGroupList BuildTree()
        {
            var lst = (from DataRow r in StaffSectionDataTable.Rows
                           where r["GROUP_CODE"].ToString() == "1000"
                           select new ReportGroup()
                           {
                               GroupName = r["GROUP_NAME"].ToString(),
                               GroupTable = StaffSectionDataTable,
                               ReportGroupID = r.Field2<Decimal>("REPORT_GROUP_ID"),
                               SortNumber = r.Field2<Decimal?>("SORT_NUMBER")
                           }).OrderBy(t => t.SortNumber).ToList();
                ReportGroupList l = new ReportGroupList();
                l.AddRange(lst);
                return l;
        }

        ReportGroup current_section;
        public ReportGroup CurrentItem
        {
            get
            {
                return current_section;
            }
            set
            {
                current_section = value;
                OnPropertyChanged(t => t.CurrentItem);
            }
        }
        public ReportGroup this[decimal? staffSectionID]
        {
            get
            {
                return this.FirstOrDefault(r => r.ReportGroupID == staffSectionID);
            }
        }
        public static DataTable StaffSectionDataTable
        {
            get
            {
                return reportGroupTable;
            }
            set
            {
                reportGroupTable = value;
            }
        }
        public static void UpdateReportGroups(DateTime cur_date)
        {
            reportGroupTable.Clear();
            if (reportGroupTable.PrimaryKey.Length == 0)
                reportGroupTable.PrimaryKey = new DataColumn[] { reportGroupTable.Columns["STAFF_SECTION_ID"] };
            oda_ReportGroup.SelectCommand.Parameters["p_date"].Value = cur_date;
            oda_ReportGroup.Fill(reportGroupTable);
        }
        #region INotifyRegion
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged<T>(Expression<Func<ReportGroupList, T>> exp)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs((exp.Body as MemberExpression).Member.Name));
            }
        }

        #endregion
    }

    public partial class ReportGroup : NotificationObject
    {
        List<ReportGroup> childSection = null;
        public List<ReportGroup> ChildGroup
        {
            get
            {
                if (childSection == null)
                    childSection = (from DataRow r in groupTable.Rows
                                    where r.Field2<Decimal?>("PARENT_GROUP_ID") == this.ReportGroupID
                                    select new ReportGroup()
                                    {
                                        GroupTable = this.GroupTable,
                                        GroupName = r["GROUP_NAME"].ToString(),
                                        ReportGroupID = r.Field2<Decimal>("REPORT_GROUP_ID"),
                                        ParentGroupID = r.Field2<Decimal?>("PARENT_GROUP_ID"),
                                        ParentGroup = this,
                                        SortNumber = r.Field2<Decimal?>("SORT_NUMBER")
                                    }).OrderBy(t => t.SortNumber).ToList();
                return childSection;
            }
        }

        public List<ReportGroup> AllChildGroup
        {
            get
            {
                List<ReportGroup> ll = new List<ReportGroup>();
                this.getAllChild(ref ll, this);
                return ll;
            }
        }

        private void getAllChild(ref List<ReportGroup> l, ReportGroup r)
        {
            if (r.ChildGroup != null)
            {
                l.AddRange(r.ChildGroup);
                foreach (ReportGroup rr in r.ChildGroup)
                {
                    getAllChild(ref l, rr);
                }
            }
        }

        DataTable groupTable;
        public DataTable GroupTable
        {
            get
            {
                return groupTable;
            }
            set
            {
                groupTable = value;
            }
        }

        public ReportGroup ParentGroup
        {
            get;
            set;
        }

        decimal? sort_number;
        public decimal? SortNumber
        {
            get
            {
                return sort_number;
            }
            set
            {
                sort_number = value;
                RaisePropertyChanged(() => SortNumber);
            }
        }

        public int Level
        {
            get
            {
                int k = 0;
                ReportGroup tempSec = this.ParentGroup;
                while (k < 100 && tempSec != null)
                {
                    ++k;
                    tempSec = tempSec.ParentGroup;
                }
                return k;
            }
        }
        public Thickness PaddingLevel
        {
            get
            {
                return new Thickness(Level * 15, 2, 0, 2);
            }
        }

        public override string ToString()
        {
            return this.GroupName;
        }
    }

}
