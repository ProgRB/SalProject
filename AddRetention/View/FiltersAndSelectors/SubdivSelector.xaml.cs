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
using System.ComponentModel;
using EntityGenerator;

namespace Salary
{
    /// <summary>
    /// Interaction logic for SubdivSelector.xaml
    /// </summary>
    public partial class SubdivSelector : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty SubdivIdProperty, AppNameRoleProperty, ShowPromtProperty;
        private DataTable t;
        public event RoutedEventHandler SubdivChanged;
        private DataView sub_view;
        static SubdivSelector()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(null);
            metadata.PropertyChangedCallback += new PropertyChangedCallback(ValidateSubdiv);
            SubdivIdProperty = DependencyProperty.Register("SubdivId", typeof(Decimal?), typeof(SubdivSelector), metadata);
            FrameworkPropertyMetadata metadata1 = new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault);
            AppNameRoleProperty = DependencyProperty.Register("AppNameRole", typeof(string), typeof(SubdivSelector), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(AppNamePropertyChanged)));
            ShowPromtProperty = DependencyProperty.Register("ShowPromt", typeof(bool), typeof(SubdivSelector), new PropertyMetadata(true));
        }
        private static void AppNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null)
            {
                DataView dv = (d as SubdivSelector).sub_view;
                if (dv != null && !string.IsNullOrEmpty(e.NewValue.ToString()))
                    if (e.NewValue.ToString() != "APSTAFF.SUBDIV")
                        dv.RowFilter = 
                                string.Format("APP_NAME in ({0})", string.Join(",", e.NewValue.ToString().Split(new char[]{',',' '},  StringSplitOptions.RemoveEmptyEntries).Select(r=>"'"+r+"'")));
                    else
                        (d as SubdivSelector).sub_view= new DataView(AppDataSet.Tables["SUBDIV"], "SUBDIV_ID not in (201)", "CODE_SUBDIV", DataViewRowState.CurrentRows);
                (d as SubdivSelector).OnPropertyChanged("SubdivView");
            }
        }
        private static void ValidateSubdiv(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SubdivSelector s = d as SubdivSelector;
            if (s != null && (e.NewValue as Decimal?) != (e.OldValue as Decimal?) && s.SubdivChanged != null)
                s.SubdivChanged(s, new RoutedEventArgs());
        }
        public Decimal? SubdivId
        {
            get { return (Decimal?)GetValue(SubdivIdProperty); }
            set { SetValue(SubdivIdProperty, value); }
        }
        public string AppRoleName
        {
            get
            {
                return (string)GetValue(AppNameRoleProperty);
            }
            set
            {
                SetValue(AppNameRoleProperty, value);
            }
        }

        /// <summary>
        /// Свойство для скрытия приглашения "подразделение" в компоненте
        /// </summary>
        public bool ShowPromt
        {
            get
            {
                return (bool)GetValue(ShowPromtProperty);
            }
            set
            {
                SetValue(ShowPromtProperty, value);
            }
        }

        public SubdivSelector()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                if (AppRoleName == "APSTAFF.SUBDIV")
                    sub_view = new DataView(AppDataSet.Tables["SUBDIV"], "", "CODE_SUBDIV", DataViewRowState.CurrentRows);
                else
                    sub_view = new DataView(AppDataSet.Tables["ACCESS_SUBDIV"], string.Format("APP_NAME in ({0})", string.IsNullOrWhiteSpace(AppRoleName)?"''": string.Join(",", AppRoleName.Split(new char[]{',',' '},  StringSplitOptions.RemoveEmptyEntries).Select(r=>"'"+r+"'"))), "CODE_SUBDIV", DataViewRowState.CurrentRows);
                OnPropertyChanged("SubdivView");
            }
        }

        /// <summary>
        /// Код Подразделения
        /// </summary>
        public string CodeSubdiv
        {
            get
            {
                return (SubdivId.HasValue ? sub_view.Table.Select("SUBDIV_ID=" + SubdivId.Value.ToString())[0]["CODE_SUBDIV"].ToString() : "");
            }
        }

        /// <summary>
        /// Представление для показа подразделений
        /// </summary>
        public List<Subdiv> SubdivView
        {
            get
            {
                if (sub_view != null)
                    return sub_view.OfType<DataRowView>().Select(r => new Subdiv() { DataRow = r.Row }).Where(r => r != null && (ExtendedSubdivFilter == null || ExtendedSubdivFilter(r))).ToList();
                else
                    return new List<Subdiv>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        Func<Subdiv, bool> _extendedSubdivFilter;

        public Func<Subdiv, bool> ExtendedSubdivFilter
        {
            get
            {
                return _extendedSubdivFilter;
            }
            set
            {
                _extendedSubdivFilter = value;
                OnPropertyChanged("ExtendedSubdivFilter");
                OnPropertyChanged("SubdivView");
            }
        }
    }
}
