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
using System.Linq.Expressions;
using System.ComponentModel;
using Salary.Helpers;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for ReportGroupView.xaml
    /// </summary>
    public partial class ReportGroupView : UserControl, INotifyPropertyChanged
    {
        Dictionary<Decimal?, List<ReportGroup>> dic = new Dictionary<decimal?, List<ReportGroup>>();
        OracleDataAdapter oda_ReportGroup, oda_rel_payment;
        DataSet ds;
        private DateTime? _selectedDate =DateTime.Now;
        public ReportGroupView()
        {
            oda_ReportGroup = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectrReportGroup.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            oda_ReportGroup.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, DateTime.Now, ParameterDirection.Input);
            oda_ReportGroup.TableMappings.Add("Table", "ReportGroup");

            oda_rel_payment = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectReportSetting.sql"), Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            oda_rel_payment.SelectCommand.Parameters.Add("p_report_group_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            oda_rel_payment.SelectCommand.BindByName = true;
            oda_rel_payment.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            oda_rel_payment.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            oda_rel_payment.SelectCommand.Parameters.Add("c3", OracleDbType.RefCursor, ParameterDirection.Output);
            oda_rel_payment.TableMappings.Add("Table", "ReportSetting");
            oda_rel_payment.TableMappings.Add("Table1", "Subdivs");
            oda_rel_payment.TableMappings.Add("Table2", "Orders");
            ds = new DataSet();
            InitializeComponent();

        }

        /// <summary>
        /// Источник данных - дерево групп
        /// </summary>
        public IEnumerable<ReportGroup> TreeSource
        {
            get
            {
                if (ds.Tables.Contains("ReportGroup"))
                    ds.Tables["ReportGroup"].Clear();
                oda_ReportGroup.SelectCommand.Parameters[0].Value = SelectedDate;
                oda_ReportGroup.Fill(ds);
                DataRowCollection dr = ds.Tables["ReportGroup"].Rows;
                dic.Clear();
                List<ReportGroup> ll = new List<ReportGroup>();
                foreach (DataRow r in dr)
                {
                    if (dic.TryGetValue(r.Field2<Decimal>("PARENT_GROUP_ID"), out ll))
                        ll.Add(new ReportGroup() { ReportGroupID = r.Field2<Decimal>("REPORT_GROUP_ID"), GroupCode = r["GROUP_CODE"].ToString(), GroupName = r["GROUP_NAME"].ToString(), ParentGroupID=r.Field2<Decimal?>("PARENT_GROUP_ID")
                        , Comment = r["GROUP_COMMENT"].ToString(), SortNumber = r.Field2<Decimal?>("SORT_NUMBER") });
                    else
                    {
                        ll = new List<ReportGroup>();
                        ll.Add(new ReportGroup() { ReportGroupID = r.Field2<Decimal>("REPORT_GROUP_ID"), GroupCode = r["GROUP_CODE"].ToString(), GroupName = r["GROUP_NAME"].ToString(), ParentGroupID = r.Field2<Decimal?>("PARENT_GROUP_ID")
                                                   ,Comment = r["GROUP_COMMENT"].ToString(), SortNumber = r.Field2<Decimal?>("SORT_NUMBER")
                        });
                        dic.Add(r.Field2<Decimal>("PARENT_GROUP_ID"), ll);
                    }
                }
                List<ReportGroup> res = new List<ReportGroup>();
                foreach (var p in dic)
                {
                    foreach (var r1 in p.Value)
                    {
                        if (r1.ParentGroupID==null)
                            res.Add(r1);
                        if (dic.TryGetValue(r1.ReportGroupID, out ll))
                            r1.ChildItems = ll;
                    }
                }
                if (string.IsNullOrWhiteSpace(FilterCodeGroup) && string.IsNullOrWhiteSpace(FilterNameGroup))
                    return res.OrderBy(r => r.GroupCode);
                else
                    return res.Where(r =>  FindChildItems(r))
                        .OrderBy(r=>r.GroupCode);
            }
        }

        /// <summary>
        /// Проверяем дочерние элементы группы
        /// </summary>
        /// <param name="group"></param>
        /// <param name="highlight"></param>
        /// <returns></returns>
        private bool FindChildItems(ReportGroup group, bool highlight = true)
        { 
            bool fl = false;
            if (CheckGroupCondition(group))
            {
                group.IsHighlighted = highlight;
                fl=true;
            }
            else group.IsHighlighted = false;
            bool fl1 = false;
            if (group.ChildItems != null)
                foreach (var p in group.ChildItems) // цель пройти все дочерние элементы и подсветить их, если надо
                    if (FindChildItems(p, highlight)) fl1 = true;
            group.IsExpanded = fl1; // если дочерних элементов есть совпадения, то раскрываем узел, иначе скрываем
            return fl || fl1;
        }

        /// <summary>
        /// Проверка групы на соответствие фильтру
        /// </summary>
        /// <param name="group_name"></param>
        /// <returns></returns>
        private bool CheckGroupCondition(ReportGroup group)
        {
            return group.GroupCode.Contains(FilterCodeGroup ?? string.Empty) && group.GroupName.ToUpper().Contains((FilterNameGroup ?? string.Empty).ToUpper());
        }

        /// <summary>
        /// Выбранная дата
        /// </summary>
        public DateTime? SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(r=>r.SelectedDate);
                OnPropertyChanged(r => TreeSource);
            }
        }
        string _filtercodeGroup;
        /// <summary>
        /// Фильтр по коду группы родительской
        /// </summary>
        public string FilterCodeGroup
        {
            get
            {
                return _filtercodeGroup;
            }
            set
            {
                _filtercodeGroup = value;
                OnPropertyChanged(r=>r.FilterCodeGroup);
            }
        }

        string _filterNameGroup;
        /// <summary>
        /// Фильтр по коду группы родительской
        /// </summary>
        public string FilterNameGroup
        {
            get
            {
                return _filterNameGroup;
            }
            set
            {
                _filterNameGroup = value;
                OnPropertyChanged(r => r.FilterNameGroup);
            }
        }

        private void OnPropertyChanged<T>(Expression<Func<ReportGroupView, T>> exp)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs((exp.Body as MemberExpression).Member.Name));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (ds.Tables.Contains("ReportSetting"))
            {
                ds.Tables["ReportSetting"].Clear();
                ds.Tables["ORDERS"].Rows.Clear();
                ds.Tables["SUBDIVS"].Rows.Clear();
            }
            if (MainTree.SelectedValue != null && MainTree.SelectedValue is ReportGroup)
                oda_rel_payment.SelectCommand.Parameters[0].Value = (MainTree.SelectedValue as ReportGroup).ReportGroupID;
            else
                oda_rel_payment.SelectCommand.Parameters[0].Value = null;
            try
            {
                oda_rel_payment.Fill(ds);
                if (listCodePayment.ItemsSource == null)
                {
                    listCodePayment.ItemsSource = new DataView(ds.Tables["ReportSetting"], "", "CODE_PAYMENT", DataViewRowState.CurrentRows);
                }
                if (dgSubdivs.ItemsSource == null)
                {
                    dgSubdivs.ItemsSource = new DataView(ds.Tables["SUBDIVS"], "", "CODE_SUBDIV", DataViewRowState.CurrentRows);
                }
                if (dgOrders.ItemsSource == null)
                {
                    dgOrders.ItemsSource = new DataView(ds.Tables["ORDERS"], "", "ORDER_FILTER", DataViewRowState.CurrentRows);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения данных по группе"+ex.GetFormattedException(), "Зарплата предприятия");
            }
        }

        private CollectionViewSource TreeCollectionSource
        {
            get
            {
                return (CollectionViewSource)this.FindResource("TreeSource");
            }
        }

        private void Add_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command);
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ReportSetting f = new ReportSetting(null, (MainTree.SelectedItem!=null?(object)(MainTree.SelectedItem as ReportGroup).ReportGroupID:null));
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                OnPropertyChanged(t => t.TreeSource);
            }
        }

        private void Selected_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && MainTree != null && MainTree.SelectedItem!=null;
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ReportSetting f = new ReportSetting((MainTree.SelectedItem as ReportGroup).ReportGroupID, null);
            f.Owner = Window.GetWindow(this);
            if (f.ShowDialog() == true)
            {
                object _selectedItem = MainTree.SelectedValue;
                OnPropertyChanged(t => t.TreeSource);
                if (_selectedItem != null)
                {
                    ReportGroup old_group = _selectedItem as ReportGroup;
                    ReportGroup rr;
                    foreach (ReportGroup r in TreeSource)
                    {
                        rr = r[old_group.ReportGroupID];
                        if (rr != null)
                        {
                            rr.IsSelected = true;
                            rr.IsExpanded = true;
                            break;
                        }
                    }
                }
            }
        }

        OracleCommand cmdDelete;
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить группу настроек и все вложенные?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (cmdDelete == null)
                {
                    cmdDelete = new OracleCommand(string.Format("begin {0}.REPORT_GROUP_DELETE(:p_REPORT_GROUP_ID);end;", Connect.SchemaSalary), Connect.CurConnect);
                    cmdDelete.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, null, ParameterDirection.Input);
                }
                cmdDelete.Parameters["p_REPORT_GROUP_ID"].Value = (MainTree.SelectedItem as ReportGroup).ReportGroupID;
                OracleTransaction tr = Connect.CurConnect.BeginTransaction();
                try
                {
                    cmdDelete.ExecuteNonQuery();
                    tr.Commit();
                    OnPropertyChanged(t => t.TreeSource);
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.GetFormattedException(), "Ошибка удаления");
                }
            }
        }

        private void Clone_Executed(object sender, ExecutedRoutedEventArgs e)
        {


        }

        private void Filter_click(object sender, RoutedEventArgs e)
        {
            OnPropertyChanged(r => r.TreeSource);
        }

        private void ToolBar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OnPropertyChanged(r => r.TreeSource);
            }
        }
    }
    public partial class ReportGroup:NotificationObject
    {
        public Decimal ReportGroupID
        {
            get;
            set;
        }
        public string GroupCode
        {
            get;
            set;
        }
        public string GroupName
        {
            get;
            set;
        }
        IEnumerable<ReportGroup> _childItems;

        /// <summary>
        /// Дочерние элемены группы
        /// </summary>
        public IEnumerable<ReportGroup> ChildItems
        {
            get
            {
                if (_childItems != null)
                    return _childItems.OrderBy(r => new Tuple<decimal?, string>(r.SortNumber, r.GroupCode));
                else
                    return _childItems;
            }
            set
            {
                _childItems = value;
            }
        }

        /// <summary>
        /// Родительская группа
        /// </summary>
        public Decimal? ParentGroupID
        {
            get;
            set;
        }

        public ReportGroup this[decimal group_id]
        { 
            get
            {
                this.IsExpanded = true;
                if (this.ReportGroupID == group_id)
                {
                    this.IsExpanded = true;
                    return this;
                }
                else
                {
                    ReportGroup res;
                    if (ChildItems != null)
                    {
                        foreach (ReportGroup r in ChildItems)
                        {
                            res = r[group_id];
                            if (res != null)
                            {

                                return res;
                            }
                        }
                    }
                    return null;
                }
            }
        }

        bool _isSelected = false;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }
        bool _isExpanded = true;
        private bool _isHighlighted = false;
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                _isExpanded = value;
                RaisePropertyChanged(() => IsExpanded);
            }
        }

        public string Comment
        {
            get;
            set;
        }

        /// <summary>
        /// Подсвечена ли группа для отчетности
        /// </summary>
        public bool IsHighlighted
        {
            get
            {
                return _isHighlighted;
            }
            set
            {
                _isHighlighted = value;
                RaisePropertyChanged(() => IsHighlighted);
            }
        }
    }

}
