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
using System.Windows.Forms;

namespace Salary
{
    /// <summary>
    /// Interaction logic for AppCloseForm.xaml
    /// </summary>
    public partial class AppCloseForm : Window
    {
        private Timer t;
        private int _remainTime = 30;
        public int RemainTime
        {
            get { return _remainTime; }
            set { _remainTime = value; }
        }
        public AppCloseForm(DateTime TimeCloseApp)
        {
            InitializeComponent();
            tbTime_Block.Text = TimeCloseApp.ToString();
            this.Closed += new EventHandler(AppCloseForm_Closed);
            t = new Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        void AppCloseForm_Closed(object sender, EventArgs e)
        {
            if (App.Current.MainWindow!=null)
                App.Current.MainWindow.Close();
        }
        
        void t_Tick(object sender, EventArgs e)
        {
            --_remainTime;
            tbRemain_time.Text = _remainTime.ToString();
            if (_remainTime < 1)
                this.Close();
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
