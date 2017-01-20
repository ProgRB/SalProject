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

namespace RolesViewerLibrary
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserViewer : UserControl
    {
        private DBUserViewModel _model;
        public UserViewer(Oracle.DataAccess.Client.OracleConnection connect)
        {
            InitializeComponent();
            _model = new DBUserViewModel(connect);
            DataContext = ViewModel;
        }

        public DBUserViewModel ViewModel
        {
            get
            {
                return _model;
            }
        }



        static UserViewer()
        { 
            AddSubdivCommand = new RoutedUICommand("Добавить доступ", "Subdiv", typeof(UserViewer));
            DeleteSubdivCommand = new RoutedUICommand("Удалить доступ", "Subdiv", typeof(UserViewer));
            SaveSubdivCommand = new RoutedUICommand("Сохранить", "Subdiv", typeof(UserViewer));
            LockUser = new RoutedUICommand("Заблокировать пользователя", "Subdiv", typeof(UserViewer));
            UnlockUser = new RoutedUICommand("Разблокировать пользователя", "Subdiv", typeof(UserViewer));
            ResetPassword = new RoutedUICommand("Сбросить пароль на табельный", "Subdiv", typeof(UserViewer));
        }

        public static RoutedUICommand SaveSubdivCommand { get; set; }

        public static RoutedUICommand AddSubdivCommand { get; set; }

        public static RoutedUICommand DeleteSubdivCommand { get; set; }

        private void CommandUser_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel!=null && ViewModel.CurrentUser != null;
        }

        private void AddSubdivCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.CurrentUser.AddAccessSubdiv();
        }

        private void SaveSubdivCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            dgAccessSubdiv.CommitEdit(DataGridEditingUnit.Row, true);
            ViewModel.CurrentUser.SaveAccessSubdiv();
        }

        private void DeleteSubdivCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.CurrentUser.DeleteAccessSubdiv(ViewModel.CurrentAccessSubdiv);
        }

        private void CommandSubdiv_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel!=null && ViewModel.CurrentUser != null && ViewModel.CurrentAccessSubdiv != null;
        }

        private void CommandSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel!=null && ViewModel.CurrentUser != null && ViewModel.CurrentUser.AccessSubdivHasChanges;
        }

        private void RefreshAccess_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null && ViewModel.CurrentUser != null)
                ViewModel.CurrentUser.RefreshAccessSubdiv();
        }

        private void RefreshUsers_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RefreshUserList();
        }

        public static RoutedUICommand LockUser { get; set; }

        public static RoutedUICommand UnlockUser { get; set; }

        public static RoutedUICommand ResetPassword { get; set; }

        private void LockUser_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Exception ex =  ViewModel.CurrentUser.Lock();
            if (ex != null)
                MessageBox.Show(Window.GetWindow(this), ex.Message, "Ошибка выполнения команды");
            //else
            //    ViewModel.RefreshUserList();
        }

        private void UnlockUser_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Exception ex = ViewModel.CurrentUser.Unlock();
            if (ex != null)
                MessageBox.Show(Window.GetWindow(this), ex.Message, "Ошибка выполнения команды");
            //else
            //    ViewModel.RefreshUserList();
        }

        private void ResetPass_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Exception ex = ViewModel.CurrentUser.ResetPass();
            if (ex != null)
                MessageBox.Show(Window.GetWindow(this), ex.Message, "Ошибка выполнения команды");
            //else
                //ViewModel.RefreshUserList();
        }
    }


}
