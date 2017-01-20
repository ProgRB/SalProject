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
using Salary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for WaitWindow.xaml
    /// </summary>
    public partial class WaitWindow : Window
    {
        private string _contentText;
        public WaitWindow(string content, AbortableBackgroundWorker related_worker=null)
        {
            _contentText = content;
            BackWorker = related_worker;
            InitializeComponent();

        }
        public string ContentText
        {
            get
            {
                return _contentText;
            }
        }

        public AbortableBackgroundWorker BackWorker
        {
            get;
            set;
        }

        public bool IsAbortable
        {
            get
            {
                return BackWorker != null;
            }
        }
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsAbortable)
            {
                BackWorker.Abort();
            }
        }

    }
}
