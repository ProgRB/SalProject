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
using System.IO;
using System.Linq.Expressions;

namespace Salary
{
    /// <summary>
    /// Interaction logic for Autorization.xaml
    /// </summary>
    public partial class Autorization : Window, INotifyPropertyChanged
    {
        public Autorization()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Autorization_Loaded);
            try
            {
                StreamReader r = new StreamReader(new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\AddRetentLogin.ini", FileMode.Open,FileAccess.Read));
                string s = r.ReadLine();
                r.Close();
                user_name.Text = s;
            }
            catch { }
        }

        void Autorization_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Connect.UserID))
            {
                UserName = Connect.UserID;
                Password = Connect.Password;
                this.btOk_Click(this, null);
            }
        }
        /// <summary>
        /// Привязанный обработчик по кнопке "ок" для формы
        /// </summary>
        public event CancelEventHandler OkClick;
        public event CancelEventHandler ChangePassClick;
        public string UserName
        {
            get
            {
                return user_name.Text.Trim();
            }
            set
            {
                user_name.Text = value;
            }
        }
        public string Password
        {
            get
            {
                return pass.Password;
            }
            set
            {
                pass.Password = value;
            }
        }

        public string NewPass
        {
            get
            {
                return new_pass.Password;
            }
        }
        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            CancelEventArgs t = new CancelEventArgs(false);
            if (OkClick != null)
                OkClick(this, t);
            if (t.Cancel)
            {
                this.DialogResult = true;
                Close();
            }
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btOk_Click(null, null);
        }

        private void btOkChanges_Click(object sender, RoutedEventArgs e)
        {
            if (new_pass.Password != new_pass1.Password)
            {
                MessageBox.Show(this, "Подтверждение нового пароля не совпадает с новым паролем!", "Зарплата предприятия", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else 
            {
                CancelEventArgs t = new CancelEventArgs(false);
                if (ChangePassClick != null)
                    ChangePassClick(this, t);
                if (t.Cancel)
                {
                    this.DialogResult = true;
                    Close();
                }
            }
        }


        private void Window_Activated(object sender, EventArgs e)
        {
            if (user_name.Text != "")
            {
                pass.Focus();
            }
        }

        bool _isChangePass = false;
        public bool PasswordChangingState
        { 
            get
            {
                return _isChangePass;
            }
            set
            {
                _isChangePass=  value;
                OnPropertyChanged(() => PasswordChangingState);
            }
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged<T>(Expression<Func<T>> mem)
        { 
            if (PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs((mem.Body as MemberExpression).Member.Name));
            }
        }
    }
}
