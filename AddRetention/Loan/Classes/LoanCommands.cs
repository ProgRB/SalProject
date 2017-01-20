using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Salary.Loan.Classes
{

    class LoanCommands
    {
        private static RoutedUICommand _viewType_Loan, _viewPurpose_Loan, _viewLoan, _viewLoan_To_Registration,
            _viewRef_Rate, _viewLoan_Cost_Item, _viewLoan_From_Archive;

        private static RoutedUICommand _addType_Loan, _deleteType_Loan, _saveType_Loan, _cancelType_Loan,
            _addPurpose_Loan, _deletePurpose_Loan, _savePurpose_Loan, _cancelPurpose_Loan,
            _addGuarantor_Loan, _editGuarantor_Loan, _deleteGuarantor_Loan, _saveGuarantor_Loan, 
            _selectGuarantor_Loan, _transfer_Loan_To_Guarantor, _transfer_Loan_To_Third_Person, _receipt_Cash_Order,
            _addLoan, _editLoan, _deleteLoan, _saveLoan, _account_Cash_Order,
            _selectBorrower,
            _addRefinancing_Rate, _deleteRefinancing_Rate, _saveRefinancing_Rate, _cancelRefinancing_Rate,
            _addLoan_Cost_Item, _deleteLoan_Cost_Item, _saveLoan_Cost_Item, _cancelLoan_Cost_Item,
            _addItem_Fin_Plan, _deleteItem_Fin_Plan, _saveItem_Fin_Plan, _cancelItem_Fin_Plan,
            _printLoan_Contract, _printGuarantor_Contract, _printSchedule_Of_Payments, _printStatement, _printStatement_Transfer, _printAllDocuments,
            _printControl_Register, _printCirculating_Register, _printIssued_Loan, _printRepaid_Loan, _printMaterial_Benefit,
            _printMaterial_Benefit_Dismiss, _report_With_Choice_Requisites, _repRetention_By_Loan, _repLoan_Holder, _dumpMaterial_Benefit,
            _approve_Loan;

        static LoanCommands()
        {
            _viewType_Loan = new RoutedUICommand("Типы ссуд", "ViewType_Loan", typeof(LoanCommands));
            _viewPurpose_Loan = new RoutedUICommand("Цели получения ссуды", "ViewPurpose_Loan", typeof(LoanCommands));
            _viewLoan = new RoutedUICommand("Ссуды", "ViewLoan", typeof(LoanCommands));
            _viewLoan_From_Archive = new RoutedUICommand("Ссуды в архиве", "ViewLoan_From_Archive", typeof(LoanCommands));
            _viewLoan_To_Registration = new RoutedUICommand("Ссуды на оформлении", "ViewLoan_To_Registration", typeof(LoanCommands));
            _viewRef_Rate = new RoutedUICommand("Ставка рефинансирования", "ViewRef_Rate", typeof(LoanCommands));
            _viewLoan_Cost_Item = new RoutedUICommand("Статьи затрат", "ViewLoan_Cost_Item", typeof(LoanCommands));

            InputGestureCollection g = new InputGestureCollection();
            g.Add(new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl+N"));
            _addType_Loan = new RoutedUICommand("Добавить тип ссуды", "AddType_Loan", typeof(LoanCommands), g);
            _addPurpose_Loan = new RoutedUICommand("Добавить цель ссуды", "AddPurpose_Loan", typeof(LoanCommands), g);
            _addLoan = new RoutedUICommand("Добавить ссуду", "AddLoan", typeof(LoanCommands), g);
            _addRefinancing_Rate = new RoutedUICommand("Добавить запись", "AddRefinancing_Rate", typeof(LoanCommands), g);
            _addLoan_Cost_Item = new RoutedUICommand("Добавить статью затрат", "AddLoan_Cost_Item", typeof(LoanCommands), g);
            _addItem_Fin_Plan = new RoutedUICommand("Добавить статью затрат", "AddItem_Fin_Plan", typeof(LoanCommands), g);
            
            g = new InputGestureCollection();
            g.Add(new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+E"));
            _editLoan = new RoutedUICommand("Редактировать ссуду", "EditLoan", typeof(LoanCommands), g);

            g = new InputGestureCollection();
            g.Add(new KeyGesture(Key.Delete, ModifierKeys.Control, "Delete"));    
            _deleteType_Loan = new RoutedUICommand("Удалить тип ссуды", "DeleteType_Loan", typeof(LoanCommands), g);
            _deletePurpose_Loan = new RoutedUICommand("Удалить цель ссуды", "DeletePurpose_Loan", typeof(LoanCommands), g);
            _deleteLoan = new RoutedUICommand("Удалить ссуду", "DeleteLoan", typeof(LoanCommands), g);
            _deleteRefinancing_Rate = new RoutedUICommand("Удалить запись", "DeleteRefinancing_Rate", typeof(LoanCommands), g);
            _deleteLoan_Cost_Item = new RoutedUICommand("Удалить статью затрат", "DeleteLoan_Cost_Item", typeof(LoanCommands), g);
            _deleteItem_Fin_Plan = new RoutedUICommand("Удалить статью затрат", "DeleteItem_Fin_Plan", typeof(LoanCommands), g);

            g = new InputGestureCollection();
            g.Add(new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl+S"));
            _saveType_Loan = new RoutedUICommand("Сохранить типы ссуд", "SaveType_Loan", typeof(LoanCommands), g);
            _savePurpose_Loan = new RoutedUICommand("Сохранить цели ссуд", "SavePurpose_Loan", typeof(LoanCommands), g);
            _saveLoan = new RoutedUICommand("Сохранить ссуду", "SaveLoan", typeof(LoanCommands), g);
            _saveRefinancing_Rate = new RoutedUICommand("Сохранить ссуду", "SaveRefinancing_Rate", typeof(LoanCommands), g);
            _saveLoan_Cost_Item = new RoutedUICommand("Сохранить изменения", "SaveLoan_Cost_Item", typeof(LoanCommands), g);
            _saveItem_Fin_Plan = new RoutedUICommand("Сохранить изменения", "SaveItem_Fin_Plan", typeof(LoanCommands), g);

            g = new InputGestureCollection();
            g.Add(new KeyGesture(Key.Z, ModifierKeys.Control, "Ctrl+Z"));
            _cancelType_Loan = new RoutedUICommand("Отменить изменения", "CancelType_Loan", typeof(LoanCommands), g);
            _cancelPurpose_Loan = new RoutedUICommand("Отменить изменения", "CancelPurpose_Loan", typeof(LoanCommands), g);
            _cancelRefinancing_Rate = new RoutedUICommand("Отменить изменения", "CancelRefinancing_Rate", typeof(LoanCommands), g);
            _cancelLoan_Cost_Item = new RoutedUICommand("Отменить изменения", "CancelLoan_Cost_Item", typeof(LoanCommands), g);
            _cancelItem_Fin_Plan = new RoutedUICommand("Отменить изменения", "CancelItem_Fin_Plan", typeof(LoanCommands), g);

            _account_Cash_Order = new RoutedUICommand("Расходный кассовый ордер", "Account_Cash_Order", typeof(LoanCommands));
            _receipt_Cash_Order = new RoutedUICommand("Приходный кассовый ордер", "Receipt_Cash_Order", typeof(LoanCommands));

            _selectBorrower = new RoutedUICommand("Выбрать заемщика", "SelectBorrower", typeof(LoanCommands));
            _transfer_Loan_To_Guarantor = new RoutedUICommand("Перевести ссуду с заемщика на поручителя", "Transfer_Loan_To_Guarantor", typeof(LoanCommands));
            _transfer_Loan_To_Third_Person = new RoutedUICommand("Перевести ссуду с заемщика на третье лицо", "Transfer_Loan_To_Third_Person", typeof(LoanCommands));

            _printLoan_Contract = new RoutedUICommand("Договор займа", "PrintLoan_Contract", typeof(LoanCommands));
            _printGuarantor_Contract = new RoutedUICommand("Договор поручительства", "PrintGuarantor_Contract", typeof(LoanCommands));
            _printSchedule_Of_Payments = new RoutedUICommand("График платежей", "PrintSchedule_Of_Payments", typeof(LoanCommands));
            _printStatement = new RoutedUICommand("Заявление", "PrintStatement", typeof(LoanCommands));
            _printStatement_Transfer = new RoutedUICommand("Заявление для перечисления", "PrintStatement_Transfer", typeof(LoanCommands));
            _printAllDocuments = new RoutedUICommand("Полный пакет документов по ссуде", "PrintAllDocuments", typeof(LoanCommands));

            _addGuarantor_Loan = new RoutedUICommand("Добавить поручителя", "AddGuarantor_Loan", typeof(LoanCommands));
            _editGuarantor_Loan = new RoutedUICommand("Редактировать поручителя", "EditGuarantor_Loan", typeof(LoanCommands));
            _deleteGuarantor_Loan = new RoutedUICommand("Удалить поручителя", "DeleteGuarantor_Loan", typeof(LoanCommands));
            _saveGuarantor_Loan = new RoutedUICommand("Сохранить изменения", "SaveGuarantor_Loan", typeof(LoanCommands));
            _selectGuarantor_Loan = new RoutedUICommand("Выбрать поручителя", "SelectGuarantor_Loan", typeof(LoanCommands));

            _printControl_Register = new RoutedUICommand("Контрольная ведомость", "PrintControl_Register", typeof(LoanCommands));
            _printCirculating_Register = new RoutedUICommand("Оборотная ведомость по ссудам", "PrintCirculating_Register", typeof(LoanCommands));
            _printIssued_Loan = new RoutedUICommand("Выданные ссуды", "PrintIssued_Loan", typeof(LoanCommands));
            _printRepaid_Loan = new RoutedUICommand("Погашенные ссуды", "PrintRepaid_Loan", typeof(LoanCommands));
            _printMaterial_Benefit = new RoutedUICommand("Справка о мат.выгоде", "PrintMaterial_Benefit", typeof(LoanCommands));
            _printMaterial_Benefit_Dismiss = new RoutedUICommand("Справка о мат.выгоде по уволенным", "PrintMaterial_Benefit_Dismiss", typeof(LoanCommands));
            _report_With_Choice_Requisites = new RoutedUICommand("Отчет с выбором реквизитов", "Report_With_Choice_Requisites", typeof(LoanCommands));

            _repRetention_By_Loan = new RoutedUICommand("Печать удержаний по ссуде", "RepRetention_By_Loan", typeof(LoanCommands));
            _repLoan_Holder = new RoutedUICommand("Задолжники по ссуде", "RepLoan_Holder", typeof(LoanCommands));
            _dumpMaterial_Benefit = new RoutedUICommand("Сброс материальной выгоды для удержания из зарплаты", "DumpMaterial_Benefit", typeof(LoanCommands));
            _approve_Loan = new RoutedUICommand("Утвердить ссуду (признак подписания документов)", "Approve_Loan", typeof(LoanCommands));
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

        public static RoutedUICommand ViewLoan_To_Registration
        {
            get { return LoanCommands._viewLoan_To_Registration; }
            set { LoanCommands._viewLoan_To_Registration = value; }
        }

        public static RoutedUICommand AddLoan
        {
            get { return LoanCommands._addLoan; }
            set { LoanCommands._addLoan = value; }
        }

        public static RoutedUICommand EditLoan
        {
            get { return LoanCommands._editLoan; }
            set { LoanCommands._editLoan = value; }
        }

        public static RoutedUICommand DeleteLoan
        {
            get { return LoanCommands._deleteLoan; }
            set { LoanCommands._deleteLoan = value; }
        }

        public static RoutedUICommand SaveLoan
        {
            get { return LoanCommands._saveLoan; }
            set { LoanCommands._saveLoan = value; }
        }

        public static RoutedUICommand Account_Cash_Order
        {
            get { return LoanCommands._account_Cash_Order; }
            set { LoanCommands._account_Cash_Order = value; }
        }

        public static RoutedUICommand SelectBorrower
        {
            get { return LoanCommands._selectBorrower; }
            set { LoanCommands._selectBorrower = value; }
        }

        public static RoutedUICommand ViewRef_Rate
        {
            get { return LoanCommands._viewRef_Rate; }
            set { LoanCommands._viewRef_Rate = value; }
        }

        public static RoutedUICommand AddRefinancing_Rate
        {
            get { return LoanCommands._addRefinancing_Rate; }
            set { LoanCommands._addRefinancing_Rate = value; }
        }

        public static RoutedUICommand DeleteRefinancing_Rate
        {
            get { return LoanCommands._deleteRefinancing_Rate; }
            set { LoanCommands._deleteRefinancing_Rate = value; }
        }

        public static RoutedUICommand SaveRefinancing_Rate
        {
            get { return LoanCommands._saveRefinancing_Rate; }
            set { LoanCommands._saveRefinancing_Rate = value; }
        }

        public static RoutedUICommand CancelRefinancing_Rate
        {
            get { return LoanCommands._cancelRefinancing_Rate; }
            set { LoanCommands._cancelRefinancing_Rate = value; }
        }

        public static RoutedUICommand AddLoan_Cost_Item
        {
            get { return LoanCommands._addLoan_Cost_Item; }
            set { LoanCommands._addLoan_Cost_Item = value; }
        }

        public static RoutedUICommand DeleteLoan_Cost_Item
        {
            get { return LoanCommands._deleteLoan_Cost_Item; }
            set { LoanCommands._deleteLoan_Cost_Item = value; }
        }

        public static RoutedUICommand SaveLoan_Cost_Item
        {
            get { return LoanCommands._saveLoan_Cost_Item; }
            set { LoanCommands._saveLoan_Cost_Item = value; }
        }

        public static RoutedUICommand CancelLoan_Cost_Item
        {
            get { return LoanCommands._cancelLoan_Cost_Item; }
            set { LoanCommands._cancelLoan_Cost_Item = value; }
        }

        public static RoutedUICommand AddItem_Fin_Plan
        {
            get { return LoanCommands._addItem_Fin_Plan; }
            set { LoanCommands._addItem_Fin_Plan = value; }
        }

        public static RoutedUICommand DeleteItem_Fin_Plan
        {
            get { return LoanCommands._deleteItem_Fin_Plan; }
            set { LoanCommands._deleteItem_Fin_Plan = value; }
        }

        public static RoutedUICommand SaveItem_Fin_Plan
        {
            get { return LoanCommands._saveItem_Fin_Plan; }
            set { LoanCommands._saveItem_Fin_Plan = value; }
        }

        public static RoutedUICommand CancelItem_Fin_Plan
        {
            get { return LoanCommands._cancelItem_Fin_Plan; }
            set { LoanCommands._cancelItem_Fin_Plan = value; }
        }

        public static RoutedUICommand ViewLoan_Cost_Item
        {
            get { return LoanCommands._viewLoan_Cost_Item; }
            set { LoanCommands._viewLoan_Cost_Item = value; }
        }

        public static RoutedUICommand PrintLoan_Contract
        {
            get { return LoanCommands._printLoan_Contract; }
            set { LoanCommands._printLoan_Contract = value; }
        }

        public static RoutedUICommand PrintGuarantor_Contract
        {
            get { return LoanCommands._printGuarantor_Contract; }
            set { LoanCommands._printGuarantor_Contract = value; }
        }

        public static RoutedUICommand PrintSchedule_Of_Payments
        {
            get { return LoanCommands._printSchedule_Of_Payments; }
            set { LoanCommands._printSchedule_Of_Payments = value; }
        }

        public static RoutedUICommand PrintStatement
        {
            get { return LoanCommands._printStatement; }
            set { LoanCommands._printStatement = value; }
        }

        public static RoutedUICommand PrintStatement_Transfer
        {
            get { return LoanCommands._printStatement_Transfer; }
            set { LoanCommands._printStatement_Transfer = value; }
        }

        public static RoutedUICommand PrintAllDocuments
        {
            get { return LoanCommands._printAllDocuments; }
            set { LoanCommands._printAllDocuments = value; }
        }

        public static RoutedUICommand ViewLoan_From_Archive
        {
            get { return LoanCommands._viewLoan_From_Archive; }
            set { LoanCommands._viewLoan_From_Archive = value; }
        }

        public static RoutedUICommand AddGuarantor_Loan
        {
            get { return LoanCommands._addGuarantor_Loan; }
            set { LoanCommands._addGuarantor_Loan = value; }
        }

        public static RoutedUICommand EditGuarantor_Loan
        {
            get { return LoanCommands._editGuarantor_Loan; }
            set { LoanCommands._editGuarantor_Loan = value; }
        }

        public static RoutedUICommand DeleteGuarantor_Loan
        {
            get { return LoanCommands._deleteGuarantor_Loan; }
            set { LoanCommands._deleteGuarantor_Loan = value; }
        }

        public static RoutedUICommand SaveGuarantor_Loan
        {
            get { return LoanCommands._saveGuarantor_Loan; }
            set { LoanCommands._saveGuarantor_Loan = value; }
        }

        public static RoutedUICommand SelectGuarantor_Loan
        {
            get { return LoanCommands._selectGuarantor_Loan; }
            set { LoanCommands._selectGuarantor_Loan = value; }
        }

        public static RoutedUICommand PrintControl_Register
        {
            get { return LoanCommands._printControl_Register; }
            set { LoanCommands._printControl_Register = value; }
        }

        public static RoutedUICommand PrintCirculating_Register
        {
            get { return LoanCommands._printCirculating_Register; }
            set { LoanCommands._printCirculating_Register = value; }
        }

        public static RoutedUICommand PrintIssued_Loan
        {
            get { return LoanCommands._printIssued_Loan; }
            set { LoanCommands._printIssued_Loan = value; }
        }

        public static RoutedUICommand PrintRepaid_Loan
        {
            get { return LoanCommands._printRepaid_Loan; }
            set { LoanCommands._printRepaid_Loan = value; }
        }

        public static RoutedUICommand PrintMaterial_Benefit
        {
            get { return LoanCommands._printMaterial_Benefit; }
            set { LoanCommands._printMaterial_Benefit = value; }
        }

        public static RoutedUICommand PrintMaterial_Benefit_Dismiss
        {
            get { return LoanCommands._printMaterial_Benefit_Dismiss; }
            set { LoanCommands._printMaterial_Benefit_Dismiss = value; }
        }

        public static RoutedUICommand Report_With_Choice_Requisites
        {
            get { return LoanCommands._report_With_Choice_Requisites; }
            set { LoanCommands._report_With_Choice_Requisites = value; }
        }

        public static RoutedUICommand Transfer_Loan_To_Guarantor
        {
            get { return LoanCommands._transfer_Loan_To_Guarantor; }
            set { LoanCommands._transfer_Loan_To_Guarantor = value; }
        }

        public static RoutedUICommand Transfer_Loan_To_Third_Person
        {
            get { return LoanCommands._transfer_Loan_To_Third_Person; }
            set { LoanCommands._transfer_Loan_To_Third_Person = value; }
        }

        public static RoutedUICommand Receipt_Cash_Order
        {
            get { return LoanCommands._receipt_Cash_Order; }
            set { LoanCommands._receipt_Cash_Order = value; }
        }

        public static RoutedUICommand RepRetention_By_Loan
        {
            get { return LoanCommands._repRetention_By_Loan; }
            set { LoanCommands._repRetention_By_Loan = value; }
        }

        public static RoutedUICommand RepLoan_Holder
        {
            get { return LoanCommands._repLoan_Holder; }
            set { LoanCommands._repLoan_Holder = value; }
        }

        public static RoutedUICommand DumpMaterial_Benefit
        {
            get { return LoanCommands._dumpMaterial_Benefit; }
            set { LoanCommands._dumpMaterial_Benefit = value; }
        }

        public static RoutedUICommand Approve_Loan
        {
            get { return LoanCommands._approve_Loan; }
            set { LoanCommands._approve_Loan = value; }
        }

    }
}
