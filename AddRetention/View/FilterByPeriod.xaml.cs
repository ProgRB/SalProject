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
using Salary;
using Salary.Helpers;
using System.ComponentModel;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for FilterByPeriod.xaml
    /// </summary>
    public partial class FilterByPeriod : Window
    {
        private FilterPeriodModel _model;
        public FilterByPeriod()
        {
            _model = new FilterPeriodModel();
            InitializeComponent();
            DataContext = Model;
        }

        public FilterByPeriod(FilterPeriodModel m)
        {
            _model = m;
            InitializeComponent();
            DataContext = Model;
        }

        public FilterPeriodModel Model
        {
            get
            {
                return _model;
            }
        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
    }

    public class FilterPeriodModel : NotificationObject
    {
        DateTime? dateBegin = DateTime.Today.Trunc("Month"), dateEnd = DateTime.Today.Trunc("Month").AddMonths(1).AddSeconds(-1);
        private bool _isBeginEnabled = true;
        private bool _isEndEnabled =true;
        public DateTime? DateBegin
        {
            get
            {
                return dateBegin;
            }
            set
            {
                dateBegin = value;
            }
        }

        public DateTime? DateEnd
        {
            get
            {
                return dateEnd;
            }
            set
            {
                dateEnd = value;
            }
        }

        
        /// <summary>
        /// Доступно ли начало периода
        /// </summary>
        public bool IsBeginEnabled
        {
            get
            {
                return _isBeginEnabled;
            }
            set
            {
                _isBeginEnabled = value;
                RaisePropertyChanged(() => IsBeginEnabled);
            }
        }
        /// <summary>
        /// Доступно ли окончание периода
        /// </summary>
        public bool IsEndEnabled
        {
            get
            {
                return _isEndEnabled;
            }
            set
            {
                _isEndEnabled = value;
                RaisePropertyChanged(() => IsEndEnabled);
            }
        }

    }
}
