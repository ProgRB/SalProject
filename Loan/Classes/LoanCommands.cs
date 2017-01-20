using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Loan.Classes
{
    class LoanCommands
    {
        private static RoutedUICommand _viewType_Loan, _viewPurpose_Loan, _viewLoan;
        private static RoutedUICommand _addType_Loan, _deleteType_Loan, _saveType_Loan, _cancelType_Loan,
            _addPurpose_Loan, _deletePurpose_Loan, _savePurpose_Loan, _cancelPurpose_Loan;

        static LoanCommands()
        {
            InputGestureCollection g = new InputGestureCollection();
            g.Add(new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl+N"));

            _viewType_Loan = new RoutedUICommand("Типы ссуд", "ViewType_Loan", typeof(LoanCommands));
            _viewPurpose_Loan = new RoutedUICommand("Цели получения ссуды", "ViewPurpose_Loan", typeof(LoanCommands));
            _viewLoan = new RoutedUICommand("Ссуды", "ViewLoan", typeof(LoanCommands));

            _addType_Loan = new RoutedUICommand("Добавить тип ссуды", "AddType_Loan", typeof(LoanCommands), g);
            _addPurpose_Loan = new RoutedUICommand("Добавить цель ссуды", "AddPurpose_Loan", typeof(LoanCommands), g);

            _deleteType_Loan = new RoutedUICommand("Удалить тип ссуды", "DeleteType_Loan", typeof(LoanCommands));
            _deletePurpose_Loan = new RoutedUICommand("Удалить цель ссуды", "DeletePurpose_Loan", typeof(LoanCommands));
            
            g = new InputGestureCollection();
            g.Add(new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl+S"));
            _saveType_Loan = new RoutedUICommand("Сохранить типы ссуд", "SaveType_Loan", typeof(LoanCommands), g);
            _savePurpose_Loan = new RoutedUICommand("Сохранить цели ссуд", "SavePurpose_Loan", typeof(LoanCommands), g);
            
            _cancelType_Loan = new RoutedUICommand("Отменить изменения", "CancelType_Loan", typeof(LoanCommands));
            _cancelPurpose_Loan = new RoutedUICommand("Отменить изменения", "CancelPurpose_Loan", typeof(LoanCommands));
        }


        public static RoutedUICommand AddType_Loan
        {
            get { return LoanCommands._addType_Loan; }
            set { LoanCommands._addType_Loan = value; }
        }

        public static RoutedUICommand AddPurpose_Loan
        {
            get { return LoanCommands._addPurpose_Loan; }
            set { LoanCommands._addPurpose_Loan = value; }
        }

        public static RoutedUICommand DeleteType_Loan
        {
            get { return LoanCommands._deleteType_Loan; }
            set { LoanCommands._deleteType_Loan = value; }
        }

        public static RoutedUICommand DeletePurpose_Loan
        {
            get { return LoanCommands._deletePurpose_Loan; }
            set { LoanCommands._deletePurpose_Loan = value; }
        }

        public static RoutedUICommand SaveType_Loan
        {
            get { return LoanCommands._saveType_Loan; }
            set { LoanCommands._saveType_Loan = value; }
        }

        public static RoutedUICommand SavePurpose_Loan
        {
            get { return LoanCommands._savePurpose_Loan; }
            set { LoanCommands._savePurpose_Loan = value; }
        }

        public static RoutedUICommand ViewType_Loan
        {
            get { return LoanCommands._viewType_Loan; }
            set { LoanCommands._viewType_Loan = value; }
        }

        public static RoutedUICommand CancelType_Loan
        {
            get { return LoanCommands._cancelType_Loan; }
            set { LoanCommands._cancelType_Loan = value; }
        }

        public static RoutedUICommand CancelPurpose_Loan
        {
            get { return LoanCommands._cancelPurpose_Loan; }
            set { LoanCommands._cancelPurpose_Loan = value; }
        }

        public static RoutedUICommand ViewPurpose_Loan
        {
            get { return LoanCommands._viewPurpose_Loan; }
            set { LoanCommands._viewPurpose_Loan = value; }
        }

        public static RoutedUICommand ViewLoan
        {
            get { return LoanCommands._viewLoan; }
            set { LoanCommands._viewLoan = value; }
        }


    }
}
