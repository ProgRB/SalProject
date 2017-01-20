using Salary.Helpers;
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

namespace Salary.View.Tools
{
    /// <summary>
    /// Interaction logic for CustomTooltip.xaml
    /// </summary>
    public partial class CustomTooltip : UserControl
    {
        private CustomTooltipModel _model;
        public CustomTooltip(CustomTooltipModel model)
        {
            _model = model;
            InitializeComponent();
            DataContext = Model;
        }

        public CustomTooltipModel Model
        {
            get
            {
                return _model;
            }
        }
    }

    public class CustomTooltipModel : NotificationObject
    {
        private string _notifHeader;
        private object _notifContent;
        /// <summary>
        /// Заголовок всплывающего сообщения
        /// </summary>
        public string NotificationHeader
        {
            get
            {
                return _notifHeader;
            }
            set
            {
                _notifHeader = value;
                RaisePropertyChanged(() => NotificationHeader);
            }
        }

        /// <summary>
        /// Содержание всплывающего сообщения
        /// </summary>
        public object NotificationContent
        {
            get
            {
                return _notifContent;
            }
            set
            {
                _notifContent = value;
                RaisePropertyChanged(() => NotificationContent);
            }
        }
    }
}
