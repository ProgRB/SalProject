using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Salary.ViewModel
{
    class AppCommands
    {
		
		/************************************************************* ИНИЦИАЛИЗАЦИЯ КОМАНД!!!!!!   *************************************************************/
        static AppCommands()
        {

#region Зарплатные команды
            InputGestureCollection g_save = new InputGestureCollection();
            g_save.Add(new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl+S"));
            _saveAddRetention = new RoutedUICommand("Сохранить удержания", "saveAddRetention", typeof(AppCommands), g_save);

            _addRetention = new RoutedUICommand("Добавить удержание", "addRetention", typeof(AppCommands));
            _deleteRetention = new RoutedUICommand("Удалить удержание", "deleteRetention", typeof(AppCommands));
            _editRetention = new RoutedUICommand("Изменить удержание", "editRetention", typeof(AppCommands));
            _editRetentionTransfer = new RoutedUICommand("Изменить сотрудника удержания", "EditRetentionTransfer", typeof(AppCommands));

            _loadAddRetentToFile = new RoutedUICommand("Выгрузить справочник в текстовый файл", "loadAddRetentToFile", typeof(AppCommands));
            _loadAddRetentFromFile = new RoutedUICommand("Загрузить справочник из текстового файла", "loadAddRetenFromFile", typeof(AppCommands));
            _openViewAlimony = new RoutedUICommand("Просмотр и ведение алиментов", "openViewAlimony", typeof(AppCommands));
            _openViewSalary = new RoutedUICommand("Просмотр и расчет ЗП", "openViewSalary", typeof(AppCommands));
             
            // Справочник видов оплат
            _savePayType = new RoutedUICommand("Сохранить вид оплат", "savePayType", typeof(AppCommands), g_save);
            _addPayType = new RoutedUICommand("Добавить вид оплат", "addPayType", typeof(AppCommands));
            _editPayType = new RoutedUICommand("Редактировать вид оплат", "editPayType", typeof(AppCommands));
            _deletePayType = new RoutedUICommand("Удалить вид оплат", "deletePayType", typeof(AppCommands));
            _openViewPayType = new RoutedUICommand("Справочник видов оплат", "openViewPayTypeCatalog", typeof(AppCommands));
            _incPayTypePriority = new RoutedUICommand("Поднять порядок расчета на 1", "IncPayTypePriority", typeof(AppCommands));

            // связка шифров оплат и методов расчетов
            _addCalcRelation = new RoutedUICommand("Добавить расчет к шифру", "AddCalcRelation", typeof(AppCommands));
            _editCalcRelation = new RoutedUICommand("Добавить расчет к шифру", "EditCalcRelation", typeof(AppCommands));
            _deleteCalcRelation = new RoutedUICommand("Добавить расчет к шифру", "DeleteCalcRelation", typeof(AppCommands));
            _saveCalcRelation = new RoutedUICommand("Сохранить настройки", "SaveCalcRelation", typeof(AppCommands));

            //Свойства шифров оплат
            AddPaymentProperty = new RoutedUICommand("Добавить свойство", "AddCalcRelation", typeof(AppCommands));
            DeletePaymentProperty = new RoutedUICommand("Удалить свойство", "AddCalcRelation", typeof(AppCommands));
            SavePaymentProperty = new RoutedUICommand("Сохранить свойства", "AddCalcRelation", typeof(AppCommands));


            // Haлоговые вычеты
            _saveTaxDiscount = new RoutedUICommand("Сохранить налоговые вычеты", "saveTaxDiscount", typeof(AppCommands), g_save);
            _addTaxDiscount = new RoutedUICommand("Добавить налоговый вычет", "addTaxDiscount", typeof(AppCommands));
            _editTaxDiscount = new RoutedUICommand("Редактировать налоговый вычет", "editTaxDiscount", typeof(AppCommands));
            _deleteTaxDiscount = new RoutedUICommand("Удалить налоговый вычет", "deleteTaxDiscount", typeof(AppCommands));
            _updateTaxDiscountFromDependents = new RoutedUICommand("Обновить вычеты из таблицы иждевенцев", "updateTaxDiscountFromDependents", typeof(AppCommands));

            // Редактирование ЗП
            _addEmpPaySalary = new RoutedUICommand("Добавить запись в ЗП сотруднику", "addEmpPaySalary", typeof(AppCommands));
            _editEmpPaySalary = new RoutedUICommand("Редактировать запись ЗП сотрудника", "editEmpPaySalary", typeof(AppCommands));
            _deleteEmpPaySalary = new RoutedUICommand("Удалить запись из ЗП", "deleteEmpPaySalary", typeof(AppCommands));
            _saveEmpPaySalary = new RoutedUICommand("Сохранить запись ЗП ", "saveEmpPaySalary", typeof(AppCommands), g_save);
            _reLoadEmpSalaryFromTable = new RoutedUICommand("Обновить начисления из табеля", "ReLoadEmpSalaryFromTable", typeof(AppCommands));

            _saveRetentSettings = new RoutedUICommand("Сохранить метод расчета", "saveRetentSettings", typeof(AppCommands), g_save);
            _addRetentSettings = new RoutedUICommand("Добавить метод расчета", "addRetentSettings", typeof(AppCommands));
            _deleteRetentSettings = new RoutedUICommand("Удалить метода расчета", "deleteRetentSettings", typeof(AppCommands));
            _editRetentSettings = new RoutedUICommand("Редактировать метода расчета", "editRetentSettings", typeof(AppCommands));

            _openViewMethodRetCalc = new RoutedUICommand("Методы расчета", "openViewMethodRetCalc", typeof(AppCommands));
            OpenViewPaymentProperty = new RoutedUICommand("Свойства видов оплат", "openViewPayType", typeof(AppCommands));

            ViewSalaryHistory = new RoutedUICommand("История изменений", "ViewSalaryHistory", typeof(AppCommands));

            // Аванс и команды по нему 
            AddAdvance = new RoutedUICommand("Добавить запись в аванс", "editEmpPaySalary", typeof(AppCommands));
            EditAdvance = new RoutedUICommand("Редактировать запись в авансе", "editEmpPaySalary", typeof(AppCommands));
            DeleteAdvance = new RoutedUICommand("Удалить запись из аванса", "editEmpPaySalary", typeof(AppCommands));
            CalcEmpAdvance = new RoutedUICommand("Пересчитать аванс сотрудника", "editEmpPaySalary", typeof(AppCommands));
            CalcSubdivAdvance = new RoutedUICommand("Рассчитать аванс подразделения", "CalcSubdivEmpRetent", typeof(AppCommands));
            LoadSubdivTableAdvance = new RoutedUICommand("Сформировать авансовые данные из табеля", "LoadSubdivTableIntoSalary", typeof(AppCommands));
            UnloadAdvanceData = new RoutedUICommand("Выгрузить выплату аванса в кассу", "UploadAdvanceToCache", typeof(AppCommands));


            CompareCacheAndSalary = new RoutedUICommand("Невыплаченные выплаты по кассе", "editEmpPaySalary", typeof(AppCommands));


            FormAVDEmpDataTable = new RoutedUICommand("Сформировать данные по среднедневному заработку для отчетности", "CloseSubdivForSalary", typeof(AppCommands));
            LoadTableBrigageToSalary = new RoutedUICommand("Загрузка зарплаты по табелям бригад (Сдельно)", "CloseSubdivForSalary", typeof(AppCommands));

            #endregion
            #region Алименты
            // АЛИМЕНТЫ
            _addAlimony = new RoutedUICommand("Добавить алимент", "addAlimony", typeof(AppCommands));
            _editAlimony = new RoutedUICommand("Редактировать алимент", "editAlimony", typeof(AppCommands));
            _deleteAlimony = new RoutedUICommand("Удалить алимент", "deleteAlimony", typeof(AppCommands));
            _addSalaryDoc = new RoutedUICommand("Добавить документ", "addSalaryDoc", typeof(AppCommands));
            _editSalaryDoc = new RoutedUICommand("Редактировать документ", "editSalaryDoc", typeof(AppCommands));
            _deleteSalaryDoc = new RoutedUICommand("Удалить документ", "deleteSalaryDoc", typeof(AppCommands));
            _saveSalaryDoc = new RoutedUICommand("Сохранить документ", "SaveSalaryDoc", typeof(AppCommands), g_save);
            _loadAlimonyToTxt = new RoutedUICommand("Выгрузить справочник в текстовый формат", "LoadAlimonyToTxt", typeof(AppCommands));
            _alimonyCard = new RoutedUICommand("Карточка алиментов", "AlimonyCard", typeof(AppCommands));
            _viewReportLoadAlimonyToTxt = new RoutedUICommand("Образец выгрузки справочника в отчет", "ViewReportLoadAlimonyToTxt", typeof(AppCommands));
            _loadAlimonyIntoDB = new RoutedUICommand("Загрузить данные алиментов в выбранный месяц", "LoadAlimonyIntoDB", typeof(AppCommands));
            _loadAlimonyIntoCash = new RoutedUICommand("Записать данные за выбранный месяц в КАССУ", "LoadAlimonyIntoCash", typeof(AppCommands));
            Rep_AllAlimony = new RoutedUICommand("Все алименты отчет", "AlimonyCard", typeof(AppCommands));

            /// Перечисление прочих удержаний по суду
            SaveCompanyAccount = new RoutedUICommand("Сохранить счет", "EditAlimony", typeof(AppCommands));
            AddCompanyAccount = new RoutedUICommand("Добавить счет организации", "EditAlimony", typeof(AppCommands));
            EditCompanyAccount = new RoutedUICommand("Редактировать счет организации", "EditAlimony", typeof(AppCommands));
            DeleteCompanyAccount = new RoutedUICommand("Удалить счет организации", "EditAlimony", typeof(AppCommands));
            OpenViewCompanyAccount = new RoutedUICommand("Справочник счетов организаций", "EditAlimony", typeof(AppCommands));

            /// добавление копий удержаний в подразделения
            _addCopyRetention = new RoutedUICommand("Добавить подразделение", "AddCopyRetention", typeof(AppCommands));
            _editCopyRetention = new RoutedUICommand("Редактировать подразделение", "EditCopyRetention", typeof(AppCommands));
            _deleteCopyRetention = new RoutedUICommand("Удалить подразделение", "DeleteCopyRetention", typeof(AppCommands));
            _saveCopyRetention = new RoutedUICommand("Сохранить подразделение копии", "SaveCopyRetention", typeof(AppCommands), g_save);

            _calcFullEmpRetent = new RoutedUICommand("Расчет зарплаты сотрудника", "CalcEmpRetent", typeof(AppCommands));
            _calcSubdivEmpRetent = new RoutedUICommand("Расчет зарплаты и взносов подразделения", "CalcSubdivEmpRetent", typeof(AppCommands));

            _calcEmpExpZoneAdd = new RoutedUICommand("Расчет надбавок стаж и поясн.", "calcEmpExpZoneAdd", typeof(AppCommands));
            _calcSubdivZoneAdd = new RoutedUICommand("Расчет надбавок стаж и поясн. в подразделении", "CalcSubdivExpZoneAdd", typeof(AppCommands));
            _editSalaryOrder = new RoutedUICommand("Поиск и редактирование заказов", "editSalaryOrder", typeof(AppCommands));
    #endregion
            // закрытие подразделений
            _openViewSalarySubdivClose = new RoutedUICommand("Закрытие ЗП подразделений", "OpenViewSalarySubdivClose", typeof(AppCommands));
            _addSubdivForSalary = new RoutedUICommand("Добавить подразделение для закрытия ЗП", "AddSubdivForSalary", typeof(AppCommands));
            _deleteSubdivForSalary = new RoutedUICommand("Удалить подразделение для закрытия ЗП", "DeleteSubdivForSalary", typeof(AppCommands));
            _saveSubdivForSalary = new RoutedUICommand("Сохранить подразделение для закрытия ЗП", "SaveSubdivForSalary", typeof(AppCommands), g_save);
            _closeSubdivForSalary = new RoutedUICommand("Закрыть ЗП по текущий месяц", "CloseSubdivForSalary", typeof(AppCommands));

            /// Справочник индексации 
            OpenViewExceptCalcAvg = new RoutedUICommand("Справочник индексации сотрудников", "EDIT_EXCEPT_CALC_AVG", typeof(AppCommands));
            AddExceptCalcAVG = new RoutedUICommand("Добавить в исключения", "EDIT_EXCEPT_CALC_AVG", typeof(AppCommands));
            DeleteExceptCalcAVG = new RoutedUICommand("Добавить в исключения", "EDIT_EXCEPT_CALC_AVG", typeof(AppCommands));
            SaveExceptCalc = new RoutedUICommand("Сохранить исключения тарифной базы", "EDIT_EXCEPT_CALC_AVG", typeof(AppCommands));

            _loadSubdivTableIntoSalary = new RoutedUICommand("Загрузить начисления из табеля для всего подразделения", "LoadSubdivTableIntoSalary", typeof(AppCommands));

            _changeClientAccountEmp = new RoutedUICommand("Выбрать плательщика алиментов", "ChangeClientAccountEmp", typeof(AppCommands));

            

            // статьи затрат
            _openViewTypeCostItem = new RoutedUICommand("Статьи затрат", "OpenViewTypeCostItem", typeof(AppCommands));
            _addTypeCostItem = new RoutedUICommand("Добавить статью затрат", "AddTypeCostItem", typeof(AppCommands));
            _deleteTypeCostItem = new RoutedUICommand("Удалить статью затрат", "DeleteTypeCostItem", typeof(AppCommands));
            _addCostItemSetting = new RoutedUICommand("Добавить шифр к статье", "AddCostItemSetting", typeof(AppCommands));
            _deleteCostItemSetting = new RoutedUICommand("Удалить шифр из статьи затрат", "DeleteCostItemSetting", typeof(AppCommands));
            _saveTypeCostItem = new RoutedUICommand("Сохранить изменения статьи затрат", "SaveTypeCostItem", typeof(AppCommands), g_save);
     #region Счета перечислений

            // счета для перечисления
            _clientAccount = new RoutedUICommand("Счета/адреса сотрудника", "ClientAccountView", typeof(AppCommands));
            _saveClientAccount = new RoutedUICommand("Сохранить счет", "SaveClientAccount", typeof(AppCommands));
            _addClientAccount = new RoutedUICommand("Добавить счет/адрес для сотрудника", "AddClientAccount", typeof(AppCommands));
            _editClientAccount = new RoutedUICommand("Редактировать счет/адрес для сотрудника", "EditClientAccount", typeof(AppCommands));
            _deleteClientAccount = new RoutedUICommand("Удалить счет/адрес сотрудника", "DeleteClientAccount", typeof(AppCommands));
            _saveClientRetentAccount = new RoutedUICommand("Сохранить счет", "SaveClientAccount", typeof(AppCommands));
            _addClientRetentAccount = new RoutedUICommand("Добавить счет/адрес для сотрудника", "AddClientAccount", typeof(AppCommands));
            _deleteClientRetentAccount = new RoutedUICommand("Удалить счет/адрес сотрудника", "DeleteClientAccount", typeof(AppCommands));

            AddClientRetention = new RoutedUICommand("Добавить перечисление", "EditClientAccount", typeof(AppCommands));
            EditClientRetention = new RoutedUICommand("Редактировать перечисление", "EditClientAccount", typeof(AppCommands));
            DeleteClientRetention = new RoutedUICommand("Удалить перечисление", "EditClientAccount", typeof(AppCommands));
            LoadPerDataToAccount = new RoutedUICommand("Заполнить автоматически паспортные данные сотрудника", "EditClientAccount", typeof(AppCommands));

            OpenViewEmpAccounts = new RoutedUICommand("Перечисления сотрудников", "OpenViewEmpAccounts", typeof(AppCommands));
            CopyAccountsFromMainWork = new RoutedUICommand("Скопировать назначение перечислений с основного места работы", "EditClientAccount", typeof(AppCommands));
            
            _сalcVacEmpTransfer = new RoutedUICommand("Начисление отпускных", "CalcVacEmpTransfer", typeof(AppCommands));

            ViewEmpRetentAccount = new RoutedUICommand("Перечисления сотрудника", "EditClientAccount", typeof(AppCommands));
            ViewEmpSalaryRetents = new RoutedUICommand("Показать все перечисленные записи", "EditClientAccount", typeof(AppCommands));

            ReplaceClientRetention = new RoutedUICommand("Заменить перечисление", "EditClientAccount", typeof(AppCommands));

            RepUploadTxtClientAccount = new RoutedUICommand("Выгрузить справочиник в текстовый формат", "EditClientAccount", typeof(AppCommands));
            RepLoadTxtClientAccount = new RoutedUICommand("Загрузить данные из текста", "EditClientAccount", typeof(AppCommands));

            RepAllTransferredSum = new RoutedUICommand("Сводный отчет по шифра оплат для перечисления", "EditClientAccount", typeof(AppCommands));

            OpenViewDocumTransfer = new RoutedUICommand("Прочие перечисления по приказам", "ViewOtherTransfer", typeof(AppCommands));
            AddDocumTransfer = new RoutedUICommand("Добавить документ/приказ перечисления", "EditOtherTransfer", typeof(AppCommands));
            EditDocumTransfer = new RoutedUICommand("Редактировать документ", "EditOtherTransfer", typeof(AppCommands));
            DeleteDocumTransfer = new RoutedUICommand("Удалить документ", "EditOtherTransfer", typeof(AppCommands));
            SaveDocumTransfer = new RoutedUICommand("Сохранить документ", "EditOtherTransfer", typeof(AppCommands));

            AddDocTransferRelation = new RoutedUICommand("Добавить запись", "EditOtherTransfer", typeof(AppCommands));
            DeleteDocTransferRelation = new RoutedUICommand("Удалить запись", "EditOtherTransfer", typeof(AppCommands));

            RepSalaryMissionTransferNote = new RoutedUICommand("Служебные записки на перечисление командировочных", "CartularyOtherTransfer", typeof(AppCommands));

            #endregion
            // расчет отпускных
            DumpVacToSalary = new RoutedUICommand("Сформировать записи в зарплату", "addEmpPaySalary", typeof(AppCommands));

            _openViewSalaryVac = new RoutedUICommand("Расчет отпускных", "OpenViewSalaryVac", typeof(AppCommands));
            _addSalaryDocum = new RoutedUICommand("Добавить документ", "AddSalaryDocum", typeof(AppCommands));
            _editSalaryDocum = new RoutedUICommand("Редактировать документ", "EditSalaryDocum", typeof(AppCommands));
            _deleteSalaryDocum = new RoutedUICommand("Удалить документ", "EditSalaryDocum", typeof(AppCommands));
            _createDocumentVac = new RoutedUICommand("Сформировать документы", "CreateDocumentVac", typeof(AppCommands));
            _saveSalaryDocum = new RoutedUICommand("Сохранить документ", "EditSalaryDocum", typeof(AppCommands));
            _calcVacTypeByDocum = new RoutedUICommand("Рассчитать придержания и перечисление", "CalcVacTypeByDocum", typeof(AppCommands));
            _CalcCheckedVacTypeByDocum = new RoutedUICommand("Рассчитать придержания и перечисление для отмеченных", "CalcVacTypeByDocum", typeof(AppCommands));
            _addSalaryDocumRelation = new RoutedUICommand("Добавить данные ЗП в документ", "AddSalaryDocumRelation", typeof(AppCommands));
            _deleteSalaryDocumRelation = new RoutedUICommand("Удалить данные из документа", "DeleteSalaryDocumRelation", typeof(AppCommands));
            _addSalaryVac = new RoutedUICommand("Добавить придержание", "AddSalaryVac", typeof(AppCommands));
            EditSalaryVac = new RoutedUICommand("Изменить придержание", "AddSalaryVac", typeof(AppCommands));
            _deleteSalaryVac = new RoutedUICommand("Удалить придержание", "DeleteSalaryVac", typeof(AppCommands));
            DumpSalaryVacs = new RoutedUICommand("Формирование отпускных в зарплату", "EditSalaryDocum", typeof(AppCommands));

            LockSalaryDocum = new RoutedUICommand("Закрыть документ", "OpenCartularyPaid", typeof(AppCommands));
            UnlockSalaryDocum = new RoutedUICommand("Открыть документ", "OpenCartularyPaid", typeof(AppCommands));

            ReAddSalaryDocumRelation = new RoutedUICommand("Доформировать документ", "EditSalaryDocum", typeof(AppCommands));

            EditSalaryDocTransfer = new RoutedUICommand("Изменить сотрудника", "EditSalaryDocum", typeof(AppCommands));

            _editRefSalaryData = new RoutedUICommand("Изменить привязанные данные к ЗП", "EditRefSalaryData", typeof(AppCommands));
            _viewAccountCard = new RoutedUICommand("Карточка сотрудника", "ViewAccountCard", typeof(AppCommands));


#region Распределение и связанное с ним /*************************************************************************************************************/
            OpenViewDistribution = new RoutedUICommand("Просмотр и распределение", "ViewSalaryDictribution", typeof(AppCommands));

            Rep_SalaryDistr1 = new RoutedUICommand("Свод по шифрам заказов", "ViewSalaryDictribution", typeof(AppCommands));
            Rep_SalaryDistrSecond = new RoutedUICommand("Вкладыш к отчету по ЗП", "ViewSalaryDictribution", typeof(AppCommands));
            Rep_SalaryDistrReceive = new RoutedUICommand("Принятые/переданные затраты по заказам", "ViewSalaryDictribution", typeof(AppCommands));
            Rep_SalaryDistrMainDues = new RoutedUICommand("Свод по заказам (взносы)", "ViewSalaryDictribution", typeof(AppCommands));

            Rep_QuarterReservDistr = new RoutedUICommand("Распределение резерва квартальной премии", "ViewSalaryDictribution", typeof(AppCommands));
            Rep_QuarterReservMem = new RoutedUICommand("Резерв квартальной премии (198 мем.ордер)", "ViewSalaryDictribution", typeof(AppCommands));
            Rep_VacReservMem = new RoutedUICommand("Резерв отпуска (197 мем.ордер)", "ViewSalaryDictribution", typeof(AppCommands));
            Rep_VacReservDistr = new RoutedUICommand("Распределение резерва отпусков", "ViewSalaryDictribution", typeof(AppCommands));


            CalcSalaryFullDistribution = new RoutedUICommand("Расчет распределения затрат", "CalcSalaryDistribution", typeof(AppCommands));
            CalcBaseDistribution = new RoutedUICommand("Расчет базы распределения затрат", "CalcSalaryDistribution", typeof(AppCommands));

            Rep_DistrControl = new RoutedUICommand("Проверка распределения и заказов", "ViewSalaryDictribution", typeof(AppCommands));

            ReplaceDistrBaseOrder = new RoutedUICommand("Замена заказов в базе распределения", "EditSalaryDistribution", typeof(AppCommands));
            ReplaceOrders = new RoutedUICommand("Заменить заказы", "EditSalaryDistribution", typeof(AppCommands));

            AddDistribReciveSubdiv = new RoutedUICommand("Добавить принятные/переданные затраты", "addEmpPaySalary", typeof(AppCommands));
            EditDistribReciveSubdiv = new RoutedUICommand("Редактировать принятные/переданные затраты", "addEmpPaySalary", typeof(AppCommands));
            DeleteDistribReciveSubdiv = new RoutedUICommand("Удалить принятные/переданные затраты", "addEmpPaySalary", typeof(AppCommands));

            AddCorrSalaryDistr = new RoutedUICommand("Добавить корректировку для базы распределения", "EditCorrelationSalDistrib", typeof(AppCommands));
            EditCorrSalaryDistr = new RoutedUICommand("Изменить корректировку для базы распределения", "EditCorrelationSalDistrib", typeof(AppCommands));
            DeleteCorrSalaryDistr = new RoutedUICommand("Удалить корректировку для базы распределения", "EditCorrelationSalDistrib", typeof(AppCommands));
            SaveSalaryAddCorr = new RoutedUICommand("Сохранить корректировку для базы распределения", "EditCorrelationSalDistrib", typeof(AppCommands));

            AddCustomDistribution = new RoutedUICommand("Добавить строку для распределения", "EditCorrelationSalDistrib", typeof(AppCommands));
            DeleteCustomDistribution = new RoutedUICommand("Удалить строку распределения", "EditCorrelationSalDistrib", typeof(AppCommands));
            SaveCustomDistribution = new RoutedUICommand("Сохранить строки распределения", "EditCorrelationSalDistrib", typeof(AppCommands));

            SaveReciveSubdiv = new RoutedUICommand("Сохранить принятные/переданные затраты", "addEmpPaySalary", typeof(AppCommands));

            PrintACPUDopzMain = new RoutedUICommand("Печать свода по шифрам заказов через АЦПУ", "addEmpPaySalary", typeof(AppCommands));
            PrintACPUDopzDues = new RoutedUICommand("Печать взносов по шифрам заказов через АЦПУ", "addEmpPaySalary", typeof(AppCommands));

            UploadDopz26 = new RoutedUICommand("Выгрузка данных для файла Доп26", "DumpSalaryDistribution", typeof(AppCommands));
            UploadDopN = new RoutedUICommand("Выгрузка данных для файла ДопН (только 146 цех)", "DumpSalaryDistribution", typeof(AppCommands));
            UploadDopz8 = new RoutedUICommand("Выгрузка данных для файла Допз8", "DumpSalaryDistribution", typeof(AppCommands));
            UploadDopz8_2 = new RoutedUICommand("Выгрузка данных для файла Допз8 для ЭВМ", "DumpSalaryDistribution", typeof(AppCommands));

            UploadDopz = new RoutedUICommand("Выгрузка данных для файла Допз (нераспределенный)", "DumpSalaryDistribution", typeof(AppCommands));
            UploadDop20 = new RoutedUICommand("Выгрузка данных для файла Допз20 (позаказка)", "DumpSalaryDistribution", typeof(AppCommands));

            UploadMem21 = new RoutedUICommand("Выгрузка данных для 21 проводки (мем. ордер)", "DumpSalaryDistribution", typeof(AppCommands));

            UploadDopzPril21 = new RoutedUICommand("Выгрузка данных для 21 проводки (приложение)", "DumpSalaryDistribution", typeof(AppCommands));

            UploadMem197 = new RoutedUICommand("Выгрузка 197 мемориального ордера", "DumpSalaryDistribution", typeof(AppCommands));
            UploadMem198 = new RoutedUICommand("Выгрузка 198 мемориального ордера", "DumpSalaryDistribution", typeof(AppCommands));


#endregion

#region Отчеты ЭУ
            RepCommonEMReport = new RoutedUICommand("Сводный отчет по зарплате", "ViewEMReport", typeof(AppCommands));
                
                
#endregion

            // расчет документов начисления
            CalcEmpSalaryDocum = new RoutedUICommand("Рассчитать документ на выбранный месяц", "editEmpPaySalary", typeof(AppCommands));
            // реестры 
            _openViewCartulary = new RoutedUICommand("Реестры перечислений", "OpenViewCartulary", typeof(AppCommands));
            _addCartulary = new RoutedUICommand("Добавить новый реестр", "AddCartulary", typeof(AppCommands));
            _editCartulary = new RoutedUICommand("Редактировать реестр", "EditCartulary", typeof(AppCommands));
            _deleteCartulary = new RoutedUICommand("Удалить реестр", "DeleteCartulary", typeof(AppCommands));
            _saveCartulary = new RoutedUICommand("Сохранить реестр", "SaveCartulary", typeof(AppCommands), g_save);
            _saveCartularyPaid = new RoutedUICommand("Сохранить перечисление по реестру", "EditCartularyPaid", typeof(AppCommands), g_save);
            _addCartularyPaid = new RoutedUICommand("Добавить перечисление по реестру", "EditCartularyPaid", typeof(AppCommands));
            _editCartularyPaid = new RoutedUICommand("Редактировать перечисление по реестру", "EditCartularyPaid", typeof(AppCommands));
            _deleteCartularyPaid = new RoutedUICommand("Удалить перечисление по реестру", "EditCartularyPaid", typeof(AppCommands));
            _createAutoCartulary = new RoutedUICommand("Сформировать реестр", "CreateAutoCartulary", typeof(AppCommands));
            _changePaidCartularyTransfer = new RoutedUICommand("Выбрать сотрудника", "SaveCartularyPaid", typeof(AppCommands));
			CloseCartulary = new RoutedUICommand("Закрыть реестр", "CloseCartularyPaid", typeof(AppCommands));
            OpenCartulary = new RoutedUICommand("Открыть реестр", "OpenCartularyPaid", typeof(AppCommands));
            _uploadSalaryChanges540 = new RoutedUICommand("Выгрузка изменений по шифрам оплат за период", "UploadSalaryChanges540", typeof(AppCommands));
            RepCartularyVsSalary = new RoutedUICommand("Сравнение реестров и заработной платы", "SALARY_CARTULARY_REPORTS", typeof(AppCommands));
            CreatePaidCartularyRef = new RoutedUICommand("Cвязать записи реестра с зарплатой", "EditCartulary", typeof(AppCommands));

            // Свойсва для шифров оплат
            AddPropertyToPayment = new RoutedUICommand("Добавить свойство к шифру оплат", "SaveCalcRelation", typeof(AppCommands));
            DeletePropertyToPayment = new RoutedUICommand("Удалить свойство у шифра оплат", "SaveCalcRelation", typeof(AppCommands));

            
            //перечисления реестров
            FileSumTransfer = new RoutedUICommand("Формирование файла для перечисления в банк", "RegisterFileTransfer", typeof(AppCommands));

            RepUploadTxtSalaryPayNote = new RoutedUICommand("Платежная ведомость в текстовый файл", "EditCartulary", typeof(AppCommands));
            Unload987ToTxt = new RoutedUICommand("Выгрузка данных перечисленной зарплаты в банк (987 в.о)", "EditCartulary", typeof(AppCommands));
            UnloadSalaryToZarpl = new RoutedUICommand("Выгрузка данных в дбфку ZARPL", "UploadSalaryChanges540", typeof(AppCommands));

            AddMessage = new RoutedUICommand("Добавить сообщение", "MessageEdit", typeof(AppCommands));
            DeleteMessage = new RoutedUICommand("Удалить сообщение", "MessageEdit", typeof(AppCommands));
            SaveMessage = new RoutedUICommand("Сохранить сообщения", "MessageEdit", typeof(AppCommands));

            ViewEditMessage = new RoutedUICommand("Системные сообщения", "MessageEdit", typeof(AppCommands));



            // сводный отчет формирование
            OpenViewCosolidReport = new RoutedUICommand("Сводный отчет", "saveEmpPaySalary", typeof(AppCommands));
            AddConsolidItem = new RoutedUICommand("Добавить запись", "saveEmpPaySalary", typeof(AppCommands));
            DeleteConsolidItem = new RoutedUICommand("Удалить выбранную запись", "saveEmpPaySalary", typeof(AppCommands));
            SaveColsolidReport = new RoutedUICommand("Сохранить изменения", "saveEmpPaySalary", typeof(AppCommands));


            //Типы банков редактор
            OpenViewTypeBank = new RoutedUICommand("Справочник банков", "EditTypeBank", typeof(AppCommands));
            AddTypeBank = new RoutedUICommand("Добавить новый банк", "EditTypeBank", typeof(AppCommands));
            EditTypeBank = new RoutedUICommand("Редактировать банк", "EditTypeBank", typeof(AppCommands));
            DeleteTypeBank = new RoutedUICommand("Удалить выбранный банк", "EditTypeBank", typeof(AppCommands));
            SaveTypeBank = new RoutedUICommand("Сохранить банки", "EditTypeBank", typeof(AppCommands));


            //Команды проверки XML файлов
            OpenViewCheckXML = new RoutedUICommand("Проверка XML файлов", "SalaryCheckXML", typeof(AppCommands));

            ExtendTaxDiscounts = new RoutedUICommand("Продление стандартных вычетов на год", "CloseSubdivForSalary", typeof(AppCommands));

            #region Команды отчетов
            _repSalByDegreeAndPayType = new RoutedUICommand("Сформировать отчет по категориям и видам оплат", "RepSalByDegreeAndPayType", typeof(AppCommands));
            _repRetentByDegree = new RoutedUICommand("Сформировать отчет по удержаниям", "RepRetentByDegree", typeof(AppCommands));
            _repSalByDegreeAndOrders = new RoutedUICommand("Сформировать отчет по категориям, видам оплат и заказам", "RepSalByDegreeAndOrders", typeof(AppCommands));
            RepRetentByOrdersEmp = new RoutedUICommand("Сформировать отчет по заказам и сотрудникам", "RepSalByDegreeAndOrders", typeof(AppCommands));
            _repPostTransferAlimony = new RoutedUICommand("Сформировать отчет почтовых переводов", "RepPostTransferAlimony", typeof(AppCommands));
            _repSubSalDeptor = new RoutedUICommand("Должники подразделения", "RepSubSalDeptor", typeof(AppCommands));
            _repSubEmpRetent = new RoutedUICommand("Сотрудники с удержаниями", "RepSubEmpRetent", typeof(AppCommands));
            RepSubRetentDocs = new RoutedUICommand("Документы удержания сотрудников", "RepSubEmpRetent", typeof(AppCommands));
            _repSubRetentSumView = new RoutedUICommand("Удержания в месяце", "RepSubRetentSumView", typeof(AppCommands));
            _repCalcReportSal = new RoutedUICommand("Протокол последнего расчета ЗП в подразделении в месяце", "RepCalcReportSal", typeof(AppCommands));
            _rep_SubConsolidation = new RoutedUICommand("Свод по подразделению", "Rep_SubConsolidation", typeof(AppCommands));
            _rep_ConsolidDept = new RoutedUICommand("Свод по заводу", "Rep_ConsolidDept", typeof(AppCommands));
            _repAlimonyDeptor = new RoutedUICommand("Задолжности по алиментам", "Rep_AlimonyDeptor", typeof(AppCommands));
            _repAlimonyBalance = new RoutedUICommand("Оборотная ведомость", "Rep_AlimonyBalance", typeof(AppCommands));
            _repAlimonyCatalog = new RoutedUICommand("Справочник алиментов", "Rep_AlimonyCatalog", typeof(AppCommands));
            _repAlimonyTransfers = new RoutedUICommand("Реестр перечислений алиментов", "RepAlimonyTransfers", typeof(AppCommands));
            
            RepRegisterReports = new RoutedUICommand("Реестры перечисления", "RepAllRegisterTransfer", typeof(AppCommands));
            RepSalaryTransferNote = new RoutedUICommand("Служебные на перевод зарплаты", "RepAllRegisterTransfer", typeof(AppCommands));
            RepSalaryPayNote = new RoutedUICommand("Платежная ведомость ф. Т-53", "RepAllRegisterTransfer", typeof(AppCommands));
            RepConsolidVacReport = new RoutedUICommand("Сводная ведомость отпускных", "RepSubEmpRetent", typeof(AppCommands));

            RepSickPaymentRegister = new RoutedUICommand("Реестр по листам нетрудоспособности", "RepSubEmpRetent", typeof(AppCommands));

            _repVacSalDeptAndPaid = new RoutedUICommand("Ведомость перечислений и начисления отпускных", "RepVacSalDeptAndPaid", typeof(AppCommands));
            _repVacSalDeptAndPaidSelected = new RoutedUICommand("Ведомость перечислений и начисления отпускных для отмеченных", "RepVacSalDeptAndPaid", typeof(AppCommands));
            _repVacNote = new RoutedUICommand("Записка-расчет о предоставлении отпуска", "RepVacNote", typeof(AppCommands));

            _empAVGDayPrice = new RoutedUICommand("Средний дневной день", "EmpAVGDayPrice", typeof(AppCommands));
            _empMissionDayPrice = new RoutedUICommand("Средний в рабочих днях", "EmpAVGDayPrice", typeof(AppCommands));
            EmpAVGDayPrice_Short = new RoutedUICommand("Средний дневной за 3 и 12 месяцев", "EmpAVGDayPrice", typeof(AppCommands));
            EmpMissionDayPrice_Short = new RoutedUICommand("Средний в рабочих днях за 3 и 12 месяцев", "EmpAVGDayPrice", typeof(AppCommands));

            _rep_ConsolidSalary = new RoutedUICommand("Сводный отчет по шифрам оплат сотрудников", "Rep_ConsolidSalary", typeof(AppCommands));
            _rep_EmpSalaryListForAccount = new RoutedUICommand("Расчетные листы сотрудников", "Rep_EmpSalaryListForAccount", typeof(AppCommands));
            _rep_EmpTaxDiscount = new RoutedUICommand("Справка об удержании НДФЛ c сотрудников", "Rep_EmpTaxDiscount", typeof(AppCommands));
            _rep_SalaryByPaymentType = new RoutedUICommand("Зарплата по шифрам оплат за период", "Rep_SalaryByPaymentType", typeof(AppCommands));

            _viewSalaryChanges = new RoutedUICommand("Просмотр внесенных изменений", "ViewSalaryChanges", typeof(AppCommands));
            _repSalNoteByPeriod = new RoutedUICommand("Справка в отдел Субсидий (6 мес.)", "ViewEditSalaryReports", typeof(AppCommands));
            _repSalNoteAlimonyByPeriod = new RoutedUICommand("Справка об удержании алиментов", "ViewEditSalaryReports", typeof(AppCommands));

            UnloadAVGDuesPercent = new RoutedUICommand("Выгрузка среднего процента ЕСН по заводу", "SalaryUnloadData", typeof(AppCommands));

            Rep_EmpDieSalaryNote = new RoutedUICommand("Справка о доходе для расчета больничного", "Rep_EmpSalaryListForAccount", typeof(AppCommands));
            RepSalaryErrors = new RoutedUICommand("Протокол возможных ошибок", "RepCalcReportSal", typeof(AppCommands));

            Rep_AddPremiumCatalog = new RoutedUICommand("Взносы к пенсии", "AdditionPremiumSalary", typeof(AppCommands));
            Rep_AddPremiumRegister = new RoutedUICommand("Взносы к пенсии", "AdditionPremiumSalary", typeof(AppCommands));
            Rep_CompareRetent401402 = new RoutedUICommand("Сравнительный отчет изменений по взносам 401 402", "AdditionPremiumSalary", typeof(AppCommands));

            RepSubEmpTransferRetent = new RoutedUICommand("Перечисление удержаний", "RepSubEmpRetent", typeof(AppCommands));

            Rep_EmpDueCard = new RoutedUICommand("Карточка учета взносов", "RepSubEmpRetent", typeof(AppCommands));

            Rep_TableCompare = new RoutedUICommand("Сравнение по табелю", "RepRetentByDegree", typeof(AppCommands));

            Rep_EmpAccountNote = new RoutedUICommand("Счета сотрудника", "EmpAccountsReports", typeof(AppCommands));

            Rep_CartularyConsolidSubdiv = new RoutedUICommand("Сводный отчет по подразделениям", "SALARY_CARTULARY_REPORTS_CURRENT", typeof(AppCommands));
            Rep_CartularyConsolidTypeBank = new RoutedUICommand("Сводный отчет по банкам перечисления", "SALARY_CARTULARY_REPORTS_CURRENT", typeof(AppCommands));

            Rep_PaymentTableSheet = new RoutedUICommand("Ведомость начисления по шифру оплат", "SalaryAVGTableReports", typeof(AppCommands));

            RepTypeBankEmpTransfer = new RoutedUICommand("Кол-во по банкам сотрудников перечисляющих ЗП", "SALARY_CARTULARY_REPORTS", typeof(AppCommands));

            EmpAvgSickDayPrice = new RoutedUICommand("Средняя стоимость для оплаты больничного листа", "EmpAVGDayPrice", typeof(AppCommands));

            // Отчеты аванса
            RepAdvanceMainReport = new RoutedUICommand("Ведомость аванса", "RepSubEmpRetent", typeof(AppCommands));
            RepAdvanceCacheT_53 = new RoutedUICommand("Платежная ведомость аванса в кассу", "RepSubEmpRetent", typeof(AppCommands));

            // отчеты пеерчислений в банки бабосиков
            RepTypeBankSumTransfer = new RoutedUICommand("Перечисление в банки", "SALARY_CARTULARY_REPORTS", typeof(AppCommands));
            RepEmpTransferByRegisters = new RoutedUICommand("Перечисление на счета по выбранным сотрудникам", "SALARY_CARTULARY_REPORTS", typeof(AppCommands));


            RepAVGPrintACPD = new RoutedUICommand("Печать через АЦПУ", "RepSubEmpRetent", typeof(AppCommands));

            RepSalaryCountDaysChanges = new RoutedUICommand("Изменения по дням отработки в закрытых периодах", "RepSubEmpRetent", typeof(AppCommands));
            RepSalaryDiscountChildEnd = new RoutedUICommand("Сотрудники и иждивенцы", "RepSubEmpRetent", typeof(AppCommands));

            RepSalaryTransferNoteAttachment = new RoutedUICommand("Приложения к служебным запискам", "RepAllRegisterTransfer", typeof(AppCommands));
            RepSalaryTransferRetent = new RoutedUICommand("Сводный отчет по удержаниям и/л", "Rep_AlimonyCatalog", typeof(AppCommands));

            ///Отчет документов начисления
            RepEmpSickDocumPrint = new RoutedUICommand("Печать справки расчета больничного", "RepSubEmpRetent", typeof(AppCommands));
            RepPrintSalDocACPD = new RoutedUICommand("Печать документов больничных и пособий", "RepSubEmpRetent", typeof(AppCommands));

            RepDuesVersionCompare = new RoutedUICommand("Протокол изменения расчетов взносов", "RepSubEmpRetent", typeof(AppCommands));

            Rep2NDFLReport = new RoutedUICommand("Справка по форме 2НДФЛ", "RepSubEmpRetent", typeof(AppCommands));
            Rep_2NDFLErrors = new RoutedUICommand("Ошибки расчетов 2НДФЛ", "RepSubEmpRetent", typeof(AppCommands));

            RepSalaryDocumForPeriod = new RoutedUICommand("Документы начисления за период", "RepSubEmpRetent", typeof(AppCommands));

            RepSalaryTaxLucre = new RoutedUICommand("Материальная выгода и налог", "RepSubEmpRetent", typeof(AppCommands));

            RepEmpTaxDiscount = new RoutedUICommand("Налоговые вычеты по типам", "RepSubEmpRetent", typeof(AppCommands));

            RepRetentDocOrder = new RoutedUICommand("Распоряжение удержания", "RepSubEmpRetent", typeof(AppCommands));

            Rep_ClosedOrdersSalary = new RoutedUICommand("Протокол закрытых и несуществующих заказов", "RepSubEmpRetent", typeof(AppCommands));

        #endregion

            _UpdateControlRoles = new RoutedUICommand("Обновить настройки привилегий доступа", "UpdateControlRoles", typeof(AppCommands));

            ViewUsersControl = new RoutedUICommand("Пользователи систем", "USER_ADMIN", typeof(AppCommands));

        #region УЧЕТ НДФЛ !!!!!!!
            OpenViewTaxes = new RoutedUICommand("Учет НДФЛ", "ViewTaxCompany", typeof(AppCommands));
            SaveTaxCompany = new RoutedUICommand("Сохранить организацию", "EditTaxCompany", typeof(AppCommands));

            EditTaxCompany = new RoutedUICommand("Изменить организацию", "EditTaxCompany", typeof(AppCommands));
            AddTaxCompany = new RoutedUICommand("Добавить организацию", "EditTaxCompany", typeof(AppCommands));
            DeleteTaxCompany = new RoutedUICommand("Удалить организацию", "EditTaxCompany", typeof(AppCommands));

            ViewTaxEmpDocum = new RoutedUICommand("Открыть документы для просмотра", "ViewTaxCompany", typeof(AppCommands));
            AddTaxEmpDocum = new RoutedUICommand("Добавить документ НДФЛ", "EditTaxDocum", typeof(AppCommands));
            EditTaxEmpDocum = new RoutedUICommand("Редактировать документ", "ViewTaxCompany", typeof(AppCommands));
            DeleteTaxEmpDocum = new RoutedUICommand("Удалить документ", "EditTaxDocum", typeof(AppCommands));
            SaveTaxEmpDocum = new RoutedUICommand("Сохранить документ НДФЛ", "EditTaxDocum", typeof(AppCommands));

            LoadTaxesDocum = new RoutedUICommand("Загрузить документы ндфл сотрудников", "EditTaxDocum", typeof(AppCommands));

            RepTaxesConsolidation = new RoutedUICommand("Отчет по налогам сводный", "ViewTaxCompany", typeof(AppCommands));
            RepTaxesDocumCommon = new RoutedUICommand("Отчет по документам 2НДФЛ", "ViewTaxCompany", typeof(AppCommands));

            SaveForeignEmp = new RoutedUICommand("Сохранить данные сотрудника", "EditTaxCompany", typeof(AppCommands));

            Upload2NDFL = new RoutedUICommand("Выгрузить данные для ФНС", "EditTaxCompany", typeof(AppCommands));
            RelocateNegativSalary = new RoutedUICommand("Перераспределение отрицательных записей доходов", "EditTaxCompany", typeof(AppCommands));
            #endregion

            #region Выгрузка данных в текстовые файлы
            SalaryTabForPrint = new RoutedUICommand("Расчетные листы сотрудников для бухгалтерии", "ReportForPrintSalary", typeof(AppCommands));
            UnloadSalary = new RoutedUICommand("Выгрузка ЗП в текстовый файл", "SalaryUnloadData", typeof(AppCommands));
        #endregion

    #region Взносы и команды по ним
            RepDuesAllSubdivFundes = new RoutedUICommand("Взносы внебюджетные фонды за период", "RepSalaryDues", typeof(AppCommands));
            RepDuesSubdivFundes = new RoutedUICommand("Взносы внебюджетные фонды за период(по подразделению)", "RepSalaryDues", typeof(AppCommands));
            RepDuesNotRetPaymentAllSubdiv = new RoutedUICommand("Необлагаемые суммы по взносам по подразделению", "RepSalaryDues", typeof(AppCommands));
            RepDuesOverLimit = new RoutedUICommand("Превышения предельной величины базы по сотрудникам", "RepSalaryDues", typeof(AppCommands));
            RepDuesListInvalid = new RoutedUICommand("Список работников-инвалидов за период", "RepSalaryDues", typeof(AppCommands));
            RepDuesAvgHeadFundes = new RoutedUICommand("Отчет по заработной плате (Главная страница)", "RepSalaryDues", typeof(AppCommands));
            RepDuesHarmProff = new RoutedUICommand("Доп взносы по льготным профессиям", "RepSalaryDues", typeof(AppCommands));

            SaveReportGroup = new RoutedUICommand("Сохранить группу настроек", "SalaryReportGroup", typeof(AppCommands));
            OpenViewReportGroup = new RoutedUICommand("Группы шифров для отчетности", "SalaryReportGroup", typeof(AppCommands));

            AddReportGroup = new RoutedUICommand("Добавить группу", "SalaryReportGroup", typeof(AppCommands));
            EditReportGroup = new RoutedUICommand("Редактировать группу", "SalaryReportGroup", typeof(AppCommands));
            DeleteReportGroup = new RoutedUICommand("Удалить группу", "SalaryReportGroup", typeof(AppCommands));
            CloneReportGroup = new RoutedUICommand("Добавить следующую группу", "SalaryReportGroup", typeof(AppCommands));

            UnloadPFR_Dop = new RoutedUICommand("Выгрузка данных по взносам доп. тарифов за вредные условия труда", "SalaryUnloadData", typeof(AppCommands));
    #endregion


            #region Выгрузка данных в XML файлы
            UploadNoNDFL2 = new RoutedUICommand("Выгрузка данных по справке 2-НДФЛ в XML файл", "SalaryUploadXML", typeof(AppCommands));

            #endregion


            /**********************************************************    РАЗДЕЛ ЭКОНОМИСТА(-КУ) ;-)       ****************************************************************************************/
            OpenViewEconView = new RoutedUICommand("Просмотр отчетности", "EconSalaryViewEasy", typeof(AppCommands));
            ViewEconCard = new RoutedUICommand("Карточка сотрудника", "EconSalaryViewEmp", typeof(AppCommands));
#region Отчеты по заработной плате
            RepSalaryByMonths = new RoutedUICommand("Зарплата по месяцам", "EconSalaryViewEmp", typeof(AppCommands));
            RepSalaryByPaymentType = new RoutedUICommand("Зарплата по видам оплат", "EconSalaryViewEmp", typeof(AppCommands));
            RepSalaryFPWByDegree = new RoutedUICommand("Распределение ФОТ по категориям", "EconSalaryViewSubdivOnly", typeof(AppCommands));
            RepActualDetails = new RoutedUICommand("Фактически закрытые наряды", "EconSalaryViewSubdivOnly", typeof(AppCommands));
            
            RepSalaryMothersPayment = new RoutedUICommand("Отчет по 243, 270 видам оплат", "EconSalaryViewEmp", typeof(AppCommands));
            RepSalaryPivotForCodeOrder = new RoutedUICommand("Зарплата и балансовые счета", "EconSalaryViewEmp", typeof(AppCommands));
            RepSalaryPivotForCodeOrder2 = new RoutedUICommand("Зарплата по б/с за период", "EconSalaryViewSubdivOnly", typeof(AppCommands));

            RepSalaryByGroupAndDegree = new RoutedUICommand("Отчет по фонду ЗП", "EconSalaryViewSubdivOnly", typeof(AppCommands));
            RepSalaryBySubdivAndEmp = new RoutedUICommand("Зарплата по подразделению", "EconSalaryViewEmp", typeof(AppCommands));
            RepSalByDegreeAndOrders1 = new RoutedUICommand("Ведомость по заказам и видам оплат", "EconSalaryViewSubdivOnly", typeof(AppCommands));

            RepDistributionEconReport = new RoutedUICommand("Позаказный учет (данные из распределения затрат на заказы производства)", "EconSalaryViewPiece", typeof(AppCommands));
            RepDistributionEconReport1 = new RoutedUICommand("Позаказный учет по всем заказам (данные из распределения затрат на заказы производства)", "EconSalaryViewPiece", typeof(AppCommands));
            RepDistributionEconSubdiv = new RoutedUICommand("Позаказный учет в разрезе подразделений и б/с", "EconSalaryViewPiece", typeof(AppCommands));
#endregion

#region Отчеты экономиста
            RepShortBySubdiv = new RoutedUICommand("Срочный отчет по подразделениям", "EconSalaryViewSubdiv", typeof(AppCommands));
            RepShortBySubdivGroupMaster = new RoutedUICommand("Срочный отчет по группам мастера", "EconSalaryViewSubdiv", typeof(AppCommands));
            RepShortBySubdivGroupMasterAndEmp = new RoutedUICommand("Срочный отчет по группам мастера (подробный)", "EconSalaryViewSubdiv", typeof(AppCommands));
            RepShortAccumBySubdiv = new RoutedUICommand("Накопительный срочный отчет по подразделениям", "EconSalaryViewSubdiv", typeof(AppCommands));
            RepShortAccumBySubdivPeriod = new RoutedUICommand("Накопительный срочный отчет за период", "EconSalaryViewSubdiv", typeof(AppCommands));
            RepAvgWorkAndWorkerClassific = new RoutedUICommand("Сводка по разрядам рабочих и работ", "EconSalaryViewEasy", typeof(AppCommands));
            RepReportByPosition = new RoutedUICommand("Отчет по профессиям", "EconSalaryViewPiece", typeof(AppCommands));
            RepReportByPositionAccum = new RoutedUICommand("Отчет по профессиям (накопительный)", "EconSalaryViewPiece", typeof(AppCommands));

            RepEconBookByPosition = new RoutedUICommand("Книга экономиста по профессиям", "EconSalaryViewPiece", typeof(AppCommands));
            RepActualPercentNormPieceWork = new RoutedUICommand("Фактический процент выполнения норм", "EconSalaryViewPiece", typeof(AppCommands));
            RepActualHourPricePieceWork = new RoutedUICommand("Фактическая стоимость нормочаса", "EconSalaryViewPiece", typeof(AppCommands));
            RepAccumPieceWork = new RoutedUICommand("Накопительная сводка по сдельщикам", "RepAccumPieceWork", typeof(AppCommands));
            RepPercentPieceWork = new RoutedUICommand("Процент выполнения норм сдельщиками", "EconSalaryViewPiece", typeof(AppCommands));
            RepSalaryToAlongPieceWorker = new RoutedUICommand("Зарплата на одного рабочего", "EconSalaryViewPiece", typeof(AppCommands));
            RepCountEmpPieceWorker = new RoutedUICommand("Численность сдельщиков", "EconSalaryViewPiece", typeof(AppCommands));
            RepOverTableHoursPieceWorker = new RoutedUICommand("Сверурочные/субботние сдельщиков", "EconSalaryViewPiece", typeof(AppCommands));
            RepHoursForAlongPieceWorker = new RoutedUICommand("Отработка в чел/час на 1 сдельщика", "EconSalaryViewPiece", typeof(AppCommands));
            RepNormHoursForAlongPieceWorker = new RoutedUICommand("Отработка в нормочасах на 1 сдельщика", "EconSalaryViewPiece", typeof(AppCommands));
            RepPercentPremPieceSalary = new RoutedUICommand("Процент премии от сдельной ЗП", "EconSalaryViewPiece", typeof(AppCommands));
            RepCountEmpSalaryByValue = new RoutedUICommand("О количестве сотрудников получающих зарплаты в указанных интервалах", "EconSalaryViewPiece", typeof(AppCommands));

            RepSalaryByPaymentTypeEM = new RoutedUICommand("Отчет по видам оплат (ЭУ)", "EconSalaryEmpEcManagment", typeof(AppCommands));

            Rep_08DegreePiecePrice = new RoutedUICommand("Основные повременщики (08 категория)", "EconSalaryViewPiece", typeof(AppCommands));
#endregion
            /***********************************************************************************************************************************************************************************/
                     
            
            /************************       Просмотр и печать табулек для операторов       */
            OpenViewPrintTabs = new RoutedUICommand("Печать расчетных листов", "ViewPrintTabsEmp", typeof(AppCommands));
            PrintEmpTabs = new RoutedUICommand("Печать расчетных листов", "ViewPrintTabsEmp", typeof(AppCommands));

            /************************************                         НАРЯДЫ                      ********************************************/
#region Команды для нарядов  
            AddTableBrigage = new RoutedUICommand("Добавить сотрудника в табель бригады", "EditTableBrigage", typeof(AppCommands));           
            EditTableBrigage = new RoutedUICommand("Редактировать сотрудника табеля бригады", "EditTableBrigage", typeof(AppCommands));           
            DeleteTableBrigage = new RoutedUICommand("Удалить сотрудника из табеля бригады", "EditTableBrigage", typeof(AppCommands));           
            SaveTableBrigage = new RoutedUICommand("Сохранить табель бригады", "EditTableBrigage", typeof(AppCommands));
            GenerateTableBrigage = new RoutedUICommand("Автоматическое заполнение табеля бригады", "EditTableBrigage", typeof(AppCommands));

            OpenViewBrigage = new RoutedUICommand("Бригадные наряды и табель бригад", "ViewTableBrigage", typeof(AppCommands));

            AddPieceWork = new RoutedUICommand("Добавить новый наряд", "EditPieceWork", typeof(AppCommands));
            EditPieceWork = new RoutedUICommand("Редактировать наряд", "EditPieceWork", typeof(AppCommands));
            DeletePieceWork = new RoutedUICommand("Удалить выбранный наряд", "EditPieceWork", typeof(AppCommands));

            BrigageDictionary = new RoutedUICommand("Справочник бригад", "EditTableBrigage", typeof(AppCommands));
            AddBrigage = new RoutedUICommand("Добавить бригаду", "EditTableBrigage", typeof(AppCommands));
            DeleteBrigage = new RoutedUICommand("Удалить бригаду", "EditTableBrigage", typeof(AppCommands));
            SaveBrigage = new RoutedUICommand("Сохранить бригады", "EditTableBrigage", typeof(AppCommands));

        #region Отчеты по нарядам
            RepTableBrigage = new RoutedUICommand("Табель бригады", "ViewTableBrigage", typeof(AppCommands));
            RepTableBrigage2 = new RoutedUICommand("Распределение ЗП по КТУ", "ViewTableBrigage", typeof(AppCommands));
        #endregion
#endregion
        }
		
        #region Command declaration
        private static RoutedUICommand _saveAddRetention, _addRetention, _deleteRetention, _loadAddRetentToFile, _editRetention,
                _loadAddRetentFromFile, _openViewAlimony, _openViewSalary, _openViewPayType,
                _saveTaxDiscount,
                _savePayType, _addPayType, _editPayType, _deletePayType, _openViewMethodRetCalc,
                _incPayTypePriority,
                _addEmpPaySalary, _editEmpPaySalary, _deleteEmpPaySalary, _saveEmpPaySalary, _reLoadEmpSalaryFromTable,
                _saveRetentSettings, _addRetentSettings, _deleteRetentSettings, _editRetentSettings,
                _addTaxDiscount, _editTaxDiscount, _deleteTaxDiscount,
                _addAlimony, _changeClientAccountEmp, _editAlimony, _deleteAlimony,
                _addSalaryDoc, _editSalaryDoc, _deleteSalaryDoc, _saveSalaryDoc,
                _calcFullEmpRetent, _calcSubdivEmpRetent,
                _calcEmpExpZoneAdd, _calcSubdivZoneAdd,
                _editSalaryOrder,
                _openViewSalarySubdivClose, _addSubdivForSalary, _deleteSubdivForSalary, _saveSubdivForSalary,
                _loadSubdivTableIntoSalary,

                _repSalByDegreeAndPayType, _repPostTransferAlimony,
                _repRetentByDegree, _repSalByDegreeAndOrders, _repSubSalDeptor
                , _repSubEmpRetent, _repSubRetentSumView,
                _addCalcRelation
                , _viewAccountCard;

        public static RoutedUICommand ViewAccountCard
        {
            get { return AppCommands._viewAccountCard; }
            set { AppCommands._viewAccountCard = value; }
        }
        private static RoutedUICommand _editCalcRelation;

        public static RoutedUICommand EditCalcRelation
        {
            get { return AppCommands._editCalcRelation; }
            set { AppCommands._editCalcRelation = value; }
        }
        private static RoutedUICommand _deleteCalcRelation;
        private static RoutedUICommand _saveCalcRelation;
        private static RoutedUICommand _repCalcReportSal;
        private static RoutedUICommand _addTypeCostItem;

        private static RoutedUICommand _deleteTypeCostItem;
        private static RoutedUICommand _addCostItemSetting;

        public static RoutedUICommand AddCostItemSetting
        {
            get { return AppCommands._addCostItemSetting; }
            set { AppCommands._addCostItemSetting = value; }
        }
        private static RoutedUICommand _deleteCostItemSetting;
        private static RoutedUICommand _saveTypeCostItem;
        private static RoutedUICommand _openViewTypeCostItem;
        private static RoutedUICommand _rep_SubConsolidation;
        private static RoutedUICommand _rep_ConsolidDept;
        private static RoutedUICommand _repAlimonyDeptor;
        private static RoutedUICommand _repAlimonyBalance;
        private static RoutedUICommand _repAlimonyCatalog;
        private static RoutedUICommand _clientAccount;
        private static RoutedUICommand _saveClientAccount;
        private static RoutedUICommand _сalcVacEmpTransfer;
        private static RoutedUICommand _addSalaryDocum;
        private static RoutedUICommand _editSalaryDocum;
        private static RoutedUICommand _deleteSalaryDocum;
        private static RoutedUICommand _createDocumentVac;
        private static RoutedUICommand _saveSalaryDocum;
        private static RoutedUICommand _openViewSalaryVac;
        private static RoutedUICommand _calcVacTypeByDocum;
        private static RoutedUICommand _addSalaryDocumRelation;
        private static RoutedUICommand _deleteSalaryDocumRelation;
        private static RoutedUICommand _addSalaryVac;
        private static RoutedUICommand _deleteSalaryVac;
        private static RoutedUICommand _CalcCheckedVacTypeByDocum;
        private static RoutedUICommand _UpdateControlRoles;
        private static RoutedUICommand _repVacSalDeptAndPaid;
        private static RoutedUICommand _empAVGDayPrice;
        private static RoutedUICommand _closeSubdivForSalary;
        private static RoutedUICommand _repVacSalDeptAndPaidSelected;
        private static RoutedUICommand _addClientAccount;
        private static RoutedUICommand _deleteClientAccount;
        private static RoutedUICommand _repVacNote;
        private static RoutedUICommand _rep_ConsolidSalary;
        private static RoutedUICommand _rep_EmpSalaryListForAccount;
        private static RoutedUICommand _editRefSalaryData;
        private static RoutedUICommand _rep_EmpTaxDiscount;
        private static RoutedUICommand _rep_SalaryByPaymentType;
        private static RoutedUICommand _updateTaxDiscountFromDependents;
        private static RoutedUICommand _alimonyCard;
        private static RoutedUICommand _loadAlimonyToTxt;
        private static RoutedUICommand _addCartulary;
        private static RoutedUICommand _editCartulary;
        private static RoutedUICommand _deleteCartulary;
        private static RoutedUICommand _saveCartulary;
        private static RoutedUICommand _openViewCartulary;
        private static RoutedUICommand _createAutoCartulary;
        private static RoutedUICommand _saveCartularyPaid;
        private static RoutedUICommand _addCopyRetention;
        private static RoutedUICommand _editCopyRetention;

        public static RoutedUICommand EditCopyRetention
        {
            get { return AppCommands._editCopyRetention; }
            set { AppCommands._editCopyRetention = value; }
        }
        private static RoutedUICommand _deleteCopyRetention;
        private static RoutedUICommand _saveCopyRetention;
        private static RoutedUICommand _viewReportLoadAlimonyToTxt;
        private static RoutedUICommand _saveClientRetentAccount;
        private static RoutedUICommand _addClientRetentAccount;

        public static RoutedUICommand AddClientRetentAccount
        {
            get { return AppCommands._addClientRetentAccount; }
            set { AppCommands._addClientRetentAccount = value; }
        }
        private static RoutedUICommand _deleteClientRetentAccount;
        private static RoutedUICommand _editClientAccount;
        private static RoutedUICommand _editRetentionTransfer;
        private static RoutedUICommand _loadAlimonyIntoDB;
        private static RoutedUICommand _loadAlimonyIntoCash;
        private static RoutedUICommand _changePaidCartularyTransfer;
        private static RoutedUICommand _addCartularyPaid;
        private static RoutedUICommand _editCartularyPaid;

        public static RoutedUICommand EditCartularyPaid
        {
            get { return AppCommands._editCartularyPaid; }
            set { AppCommands._editCartularyPaid = value; }
        }
        private static RoutedUICommand _deleteCartularyPaid;
        private static RoutedUICommand _repAlimonyTransfers;
        private static RoutedUICommand _empMissionDayPrice;
        private static RoutedUICommand _uploadSalaryChanges540;
        private static RoutedUICommand _viewSalaryChanges;
        private static RoutedUICommand _repSalNoteByPeriod;
        private static RoutedUICommand _repSalNoteAlimonyByPeriod;

        public static RoutedUICommand RepSalNoteAlimonyByPeriod
        {
            get { return AppCommands._repSalNoteAlimonyByPeriod; }
            set { AppCommands._repSalNoteAlimonyByPeriod = value; }
        }

        public static RoutedUICommand RepSalNoteByPeriod
        {
            get { return AppCommands._repSalNoteByPeriod; }
            set { AppCommands._repSalNoteByPeriod = value; }
        }

        public static RoutedUICommand ViewSalaryChanges
        {
            get { return AppCommands._viewSalaryChanges; }
            set { AppCommands._viewSalaryChanges = value; }
        }


        public static RoutedUICommand UploadSalaryChanges540
        {
            get { return AppCommands._uploadSalaryChanges540; }
            set { AppCommands._uploadSalaryChanges540 = value; }
        }

        public static RoutedUICommand EmpMissionDayPrice
        {
            get { return AppCommands._empMissionDayPrice; }
            set { AppCommands._empMissionDayPrice = value; }
        }

        public static RoutedUICommand RepAlimonyTransfers
        {
            get { return AppCommands._repAlimonyTransfers; }
            set { AppCommands._repAlimonyTransfers = value; }
        }

        public static RoutedUICommand DeleteCartularyPaid
        {
            get { return AppCommands._deleteCartularyPaid; }
            set { AppCommands._deleteCartularyPaid = value; }
        }

        public static RoutedUICommand AddCartularyPaid
        {
            get { return AppCommands._addCartularyPaid; }
            set { AppCommands._addCartularyPaid = value; }
        }

        public static RoutedUICommand ChangePaidCartularyTransfer
        {
            get { return AppCommands._changePaidCartularyTransfer; }
            set { AppCommands._changePaidCartularyTransfer = value; }
        }

        public static RoutedUICommand LoadAlimonyIntoCash
        {
            get { return AppCommands._loadAlimonyIntoCash; }
            set { AppCommands._loadAlimonyIntoCash = value; }
        }

        public static RoutedUICommand LoadAlimonyIntoDB
        {
            get { return AppCommands._loadAlimonyIntoDB; }
            set { AppCommands._loadAlimonyIntoDB = value; }
        }

        public static RoutedUICommand EditRetentionTransfer
        {
            get { return AppCommands._editRetentionTransfer; }
            set { AppCommands._editRetentionTransfer = value; }
        }

        public static RoutedUICommand EditClientAccount
        {
            get { return AppCommands._editClientAccount; }
            set { AppCommands._editClientAccount = value; }
        }

        public static RoutedUICommand DeleteClientRetentAccount
        {
            get { return AppCommands._deleteClientRetentAccount; }
            set { AppCommands._deleteClientRetentAccount = value; }
        }

        public static RoutedUICommand SaveClientRetentAccount
        {
            get { return AppCommands._saveClientRetentAccount; }
            set { AppCommands._saveClientRetentAccount = value; }
        }

        public static RoutedUICommand ViewReportLoadAlimonyToTxt
        {
            get { return AppCommands._viewReportLoadAlimonyToTxt; }
            set { AppCommands._viewReportLoadAlimonyToTxt = value; }
        }

        public static RoutedUICommand SaveCopyRetention
        {
            get { return AppCommands._saveCopyRetention; }
            set { AppCommands._saveCopyRetention = value; }
        }

        public static RoutedUICommand DeleteCopyRetention
        {
            get { return AppCommands._deleteCopyRetention; }
            set { AppCommands._deleteCopyRetention = value; }
        }

        public static RoutedUICommand AddCopyRetention
        {
            get { return AppCommands._addCopyRetention; }
            set { AppCommands._addCopyRetention = value; }
        }

        public static RoutedUICommand SaveCartularyPaid
        {
            get { return AppCommands._saveCartularyPaid; }
            set { AppCommands._saveCartularyPaid = value; }
        }

        public static RoutedUICommand CreateAutoCartulary
        {
            get { return AppCommands._createAutoCartulary; }
            set { AppCommands._createAutoCartulary = value; }
        }

        public static RoutedUICommand OpenViewCartulary
        {
            get { return AppCommands._openViewCartulary; }
            set { AppCommands._openViewCartulary = value; }
        }

        public static RoutedUICommand SaveCartulary
        {
            get { return AppCommands._saveCartulary; }
            set { AppCommands._saveCartulary = value; }
        }

        public static RoutedUICommand EditCartulary
        {
            get { return AppCommands._editCartulary; }
            set { AppCommands._editCartulary = value; }
        }

        public static RoutedUICommand DeleteCartulary
        {
            get { return AppCommands._deleteCartulary; }
            set { AppCommands._deleteCartulary = value; }
        }

        public static RoutedUICommand AddCartulary
        {
            get { return AppCommands._addCartulary; }
            set { AppCommands._addCartulary = value; }
        }

        public static RoutedUICommand LoadAlimonyToTxt
        {
            get { return AppCommands._loadAlimonyToTxt; }
            set { AppCommands._loadAlimonyToTxt = value; }
        }


        public static RoutedUICommand AlimonyCard
        {
            get { return AppCommands._alimonyCard; }
            set { AppCommands._alimonyCard = value; }
        }

        public static RoutedUICommand UpdateTaxDiscountFromDependents
        {
            get { return AppCommands._updateTaxDiscountFromDependents; }
            set { AppCommands._updateTaxDiscountFromDependents = value; }
        }

        public static RoutedUICommand Rep_SalaryByPaymentType
        {
            get { return AppCommands._rep_SalaryByPaymentType; }
            set { AppCommands._rep_SalaryByPaymentType = value; }
        }

        public static RoutedUICommand Rep_EmpTaxDiscount
        {
            get { return AppCommands._rep_EmpTaxDiscount; }
            set { AppCommands._rep_EmpTaxDiscount = value; }
        }

        public static RoutedUICommand EditRefSalaryData
        {
            get { return AppCommands._editRefSalaryData; }
            set { AppCommands._editRefSalaryData = value; }
        }

        public static RoutedUICommand Rep_EmpSalaryListForAccount
        {
            get { return AppCommands._rep_EmpSalaryListForAccount; }
            set { AppCommands._rep_EmpSalaryListForAccount = value; }
        }

        public static RoutedUICommand Rep_ConsolidSalary
        {
            get { return AppCommands._rep_ConsolidSalary; }
            set { AppCommands._rep_ConsolidSalary = value; }
        }

        public static RoutedUICommand RepVacNote
        {
            get { return AppCommands._repVacNote; }
            set { AppCommands._repVacNote = value; }
        }

        public static RoutedUICommand DeleteClientAccount
        {
            get { return AppCommands._deleteClientAccount; }
            set { AppCommands._deleteClientAccount = value; }
        }

        public static RoutedUICommand AddClientAccount
        {
            get { return AppCommands._addClientAccount; }
            set { AppCommands._addClientAccount = value; }
        }

        public static RoutedUICommand RepVacSalDeptAndPaidSelected
        {
            get { return AppCommands._repVacSalDeptAndPaidSelected; }
            set { AppCommands._repVacSalDeptAndPaidSelected = value; }
        }

        public static RoutedUICommand CloseSubdivForSalary
        {
            get { return AppCommands._closeSubdivForSalary; }
            set { AppCommands._closeSubdivForSalary = value; }
        }

        public static RoutedUICommand EmpAVGDayPrice
        {
            get { return AppCommands._empAVGDayPrice; }
            set { AppCommands._empAVGDayPrice = value; }
        }

        public static RoutedUICommand RepVacSalDeptAndPaid
        {
            get { return AppCommands._repVacSalDeptAndPaid; }
            set { AppCommands._repVacSalDeptAndPaid = value; }
        }

        public static RoutedUICommand UpdateControlRoles
        {
            get { return AppCommands._UpdateControlRoles; }
            set { AppCommands._UpdateControlRoles = value; }
        }

        public static RoutedUICommand CalcCheckedVacTypeByDocum
        {
            get { return AppCommands._CalcCheckedVacTypeByDocum; }
            set { AppCommands._CalcCheckedVacTypeByDocum = value; }
        }

        public static RoutedUICommand DeleteSalaryVac
        {
            get { return AppCommands._deleteSalaryVac; }
            set { AppCommands._deleteSalaryVac = value; }
        }

        public static RoutedUICommand AddSalaryVac
        {
            get { return AppCommands._addSalaryVac; }
            set { AppCommands._addSalaryVac = value; }
        }

        public static RoutedUICommand DeleteSalaryDocumRelation
        {
            get { return AppCommands._deleteSalaryDocumRelation; }
            set { AppCommands._deleteSalaryDocumRelation = value; }
        }

        public static RoutedUICommand AddSalaryDocumRelation
        {
            get { return AppCommands._addSalaryDocumRelation; }
            set { AppCommands._addSalaryDocumRelation = value; }
        }

        public static RoutedUICommand CalcVacTypeByDocum
        {
            get { return AppCommands._calcVacTypeByDocum; }
            set { AppCommands._calcVacTypeByDocum = value; }
        }

        public static RoutedUICommand OpenViewSalaryVac
        {
            get { return AppCommands._openViewSalaryVac; }
            set { AppCommands._openViewSalaryVac = value; }
        }

        public static RoutedUICommand SaveSalaryDocum
        {
            get { return AppCommands._saveSalaryDocum; }
            set { AppCommands._saveSalaryDocum = value; }
        }

        public static RoutedUICommand CreateDocumentVac
        {
            get { return AppCommands._createDocumentVac; }
            set { AppCommands._createDocumentVac = value; }
        }

        public static RoutedUICommand DeleteSalaryDocum
        {
            get { return AppCommands._deleteSalaryDocum; }
            set { AppCommands._deleteSalaryDocum = value; }
        }

        public static RoutedUICommand EditSalaryDocum
        {
            get { return AppCommands._editSalaryDocum; }
            set { AppCommands._editSalaryDocum = value; }
        }

        public static RoutedUICommand AddSalaryDocum
        {
            get { return AppCommands._addSalaryDocum; }
            set { AppCommands._addSalaryDocum = value; }
        }

        public static RoutedUICommand CalcVacEmpTransfer
        {
            get { return AppCommands._сalcVacEmpTransfer; }
            set { AppCommands._сalcVacEmpTransfer = value; }
        }

        public static RoutedUICommand SaveClientAccount
        {
            get { return AppCommands._saveClientAccount; }
            set { AppCommands._saveClientAccount = value; }
        }

        public static RoutedUICommand ClientAccount
        {
            get { return AppCommands._clientAccount; }
            set { AppCommands._clientAccount = value; }
        }

        public static RoutedUICommand RepAlimonyCatalog
        {
            get { return AppCommands._repAlimonyCatalog; }
            set { AppCommands._repAlimonyCatalog = value; }
        }

        public static RoutedUICommand RepAlimonyBalance
        {
            get { return AppCommands._repAlimonyBalance; }
            set { AppCommands._repAlimonyBalance = value; }
        }

        public static RoutedUICommand RepAlimonyDeptor
        {
            get { return AppCommands._repAlimonyDeptor; }
            set { AppCommands._repAlimonyDeptor = value; }
        }

        public static RoutedUICommand Rep_ConsolidDept
        {
            get { return AppCommands._rep_ConsolidDept; }
            set { AppCommands._rep_ConsolidDept = value; }
        }

        public static RoutedUICommand Rep_SubConsolidation
        {
            get { return AppCommands._rep_SubConsolidation; }
            set { AppCommands._rep_SubConsolidation = value; }
        }

        public static RoutedUICommand OpenViewTypeCostItem
        {
            get { return AppCommands._openViewTypeCostItem; }
            set { AppCommands._openViewTypeCostItem = value; }
        }

        public static RoutedUICommand SaveTypeCostItem
        {
            get { return AppCommands._saveTypeCostItem; }
            set { AppCommands._saveTypeCostItem = value; }
        }

        public static RoutedUICommand DeleteCostItemSetting
        {
            get { return AppCommands._deleteCostItemSetting; }
            set { AppCommands._deleteCostItemSetting = value; }
        }

        public static RoutedUICommand DeleteTypeCostItem
        {
            get { return AppCommands._deleteTypeCostItem; }
            set { AppCommands._deleteTypeCostItem = value; }
        }

        public static RoutedUICommand AddTypeCostItem
        {
            get { return AppCommands._addTypeCostItem; }
            set { AppCommands._addTypeCostItem = value; }
        }

        public static RoutedUICommand RepCalcReportSal
        {
            get { return AppCommands._repCalcReportSal; }
            set { AppCommands._repCalcReportSal = value; }
        }

        public static RoutedUICommand SaveCalcRelation
        {
            get { return AppCommands._saveCalcRelation; }
            set { AppCommands._saveCalcRelation = value; }
        }

        public static RoutedUICommand DeleteCalcRelation
        {
            get { return AppCommands._deleteCalcRelation; }
            set { AppCommands._deleteCalcRelation = value; }
        }

        public static RoutedUICommand AddCalcRelation
        {
            get { return AppCommands._addCalcRelation; }
            set { AppCommands._addCalcRelation = value; }
        }

        #endregion
        
#region Comman declare2
        static void _repSalByDegreeAndPayType_CanExecuteChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        public static RoutedCommand SaveAddRetention
        {
            get { return _saveAddRetention; }
        }
        public static RoutedCommand AddRetention
        {
            get { return _addRetention; }
        }
        public static RoutedCommand DeleteRetention
        {
            get { return _deleteRetention; }
        }
        public static RoutedCommand EditRetention
        {
            get { return _editRetention; }
        }


        public static RoutedCommand LoadAddRetentToFile
        {
            get { return _loadAddRetentToFile; }
        }
        public static RoutedCommand LoadAddRetentFromFile
        {
            get { return _loadAddRetentFromFile; }
        }

        public static RoutedCommand OpenViewAlimony
        {
            get { return _openViewAlimony; }
        }
        public static RoutedCommand OpenViewSalary
        {
            get { return _openViewSalary; }
        }
        public static RoutedCommand SavePayType
        {
            get { return _savePayType; }
        }
        public static RoutedCommand AddPayType
        {
            get { return _addPayType; }
        }
        public static RoutedCommand EditPayType
        {
            get { return _editPayType; }
        }
        public static RoutedCommand DeletePayType
        {
            get { return _deletePayType; }
        }
        public static RoutedCommand OpenViewPayType
        {
            get { return _openViewPayType; }
        }
        public static RoutedUICommand IncPayTypePriority
        {
            get
            {
                return _incPayTypePriority;
            }
        }
        
        public static RoutedUICommand AddEmpPaySalary
        {
            get { return _addEmpPaySalary; }
        }
        public static RoutedUICommand EditEmpPaySalary
        {
            get { return _editEmpPaySalary; }
        }
        public static RoutedUICommand DeleteEmpPaySalary
        {
            get { return _deleteEmpPaySalary; }
        }
        public static RoutedUICommand SaveEmpPaySalary
        {
            get { return _saveEmpPaySalary; }
        }
        public static RoutedUICommand ReLoadEmpSalaryFromTable
        {
            get { return _reLoadEmpSalaryFromTable; }
        }
        public static RoutedUICommand AddEmpTaxSalary
        {
            get { return _addEmpPaySalary; }
        }
        public static RoutedUICommand DeleteEmpTaxSalary
        {
            get { return _deleteEmpPaySalary; }
        }
        public static RoutedUICommand SaveEmpTaxSalary
        {
            get { return _saveEmpPaySalary; }
        }
        public static RoutedUICommand ReCaclTaxSalary
        {
            get { return _reLoadEmpSalaryFromTable; }
        }
        public static RoutedUICommand SaveRetentSettings
        {
            get { return _saveRetentSettings; }
        }
        public static RoutedUICommand AddRetentSettings
        {
            get { return _addRetentSettings; }
        }
        public static RoutedUICommand DeleteRetentSettings
        {
            get { return _deleteRetentSettings; }
        }
        public static RoutedUICommand EditRetentSettings
        {
            get { return _editRetentSettings; }
        }

        public static RoutedUICommand OpenViewMethodRetCalc
        {
            get { return _openViewMethodRetCalc; }
        }

        public static RoutedUICommand AddTaxDiscount
        {
            get { return _addTaxDiscount; }
        }
        public static RoutedUICommand EditTaxDiscount
        {
            get { return _editTaxDiscount; }
        }
        public static RoutedUICommand DeleteTaxDiscount
        {
            get
            {
                return _deleteTaxDiscount;
            }
        }
        public static RoutedCommand SaveTaxDiscount
        {
            get { return _saveTaxDiscount; }
        }

        public static RoutedUICommand ChangeClientAccountEmp
        {
            get
            {
                return _changeClientAccountEmp;
            }
        }
        public static RoutedUICommand EditAlimony
        {
            get
            {
                return _editAlimony;
            }
        }
        public static RoutedUICommand DeleteAlimony
        {
            get
            {
                return _deleteAlimony;
            }
        }
        
        public static RoutedUICommand AddSalaryDoc
        {
            get
            {
                return _addSalaryDoc;
            }
        }
        public static RoutedUICommand EditSalaryDoc
        {
            get
            {
                return _editSalaryDoc;
            }
        }
        public static RoutedUICommand DeleteSalaryDoc
        {
            get
            {
                return _deleteSalaryDoc;
            }
        }
        public static RoutedUICommand SaveSalaryDoc
        {
            get
            {
                return _saveSalaryDoc;
            }
        }

        public static RoutedUICommand CalcFullEmpRetent
        {
            get
            {
                return _calcFullEmpRetent;
            }
        }

        public static RoutedUICommand CalcSubdivEmpRetent
        {
            get
            {
                return _calcSubdivEmpRetent;
            }
        }
        public static RoutedUICommand CalcEmpExpZoneAdd
        {
            get
            {
                return _calcEmpExpZoneAdd;
            }
        }
        public static RoutedUICommand CalcSubdivExpZoneAdd
        {
            get
            {
                return _calcSubdivZoneAdd;
            }
        }

        public static RoutedUICommand EditSalaryOrder
        {
            get
            {
                return _editSalaryOrder;
            }
        }

        public static RoutedUICommand OpenViewSalarySubdivClose
        {
            get
            {
                return _openViewSalarySubdivClose;
            }
        }
        public static RoutedUICommand AddSubdivForSalary
        {
            get
            {
                return _addSubdivForSalary;
            }
        }
        public static RoutedUICommand DeleteSubdivForSalary
        {
            get
            {
                return _deleteSubdivForSalary;
            }
        }
        public static RoutedUICommand SaveSubdivForSalary
        {
            get
            {
                return _saveSubdivForSalary;
            }
        }

        public static RoutedUICommand LoadSubdivTableIntoSalary
        {
            get
            {
                return _loadSubdivTableIntoSalary;
            }
        }

        public static RoutedUICommand RepSalByDegreeAndPayType
        {
            get
            {
                return _repSalByDegreeAndPayType;
            }
        }
        public static RoutedUICommand RepRetentByDegree
        {
            get
            {
                return _repRetentByDegree;
            }
        }
        public static RoutedUICommand RepSalByDegreeAndOrders
        {
            get
            {
                return _repSalByDegreeAndOrders;
            }
        }
        public static RoutedUICommand RepPostTransferAlimony
        {
            get
            {
                return _repPostTransferAlimony;
            }
        }
        public static RoutedUICommand RepSubSalDeptor
        {
            get
            {
                return _repSubSalDeptor;
            }
        }

        public static RoutedUICommand RepSubEmpRetent
        {
            get
            {
                return _repSubEmpRetent;
            }
        }

        public static RoutedUICommand RepSubRetentSumView
        {
            get
            {
                return _repSubRetentSumView;
            }
        }


        public static RoutedUICommand SalaryTabForPrint { get; set; }

        public static RoutedUICommand AddPropertyToPayment { get; set; }

        public static RoutedUICommand DeletePropertyToPayment { get; set; }

        public static RoutedUICommand OpenViewPaymentProperty { get; set; }

        public static RoutedUICommand SavePaymentProperty { get; set; }

        public static RoutedUICommand OpenViewExceptCalcAvg { get; set; }

        public static RoutedUICommand SaveExceptCalc { get; set; }

        public static RoutedUICommand AddExceptCalcAVG { get; set; }

        public static RoutedUICommand DeleteExceptCalcAVG { get; set; }

        public static RoutedUICommand AddPaymentProperty { get; set; }

        public static RoutedUICommand DeletePaymentProperty { get; set; }

        public static RoutedUICommand RepDuesAllSubdivFundes { get; set; }

        public static RoutedUICommand SaveReportGroup { get; set; }

        public static RoutedUICommand OpenViewReportGroup { get; set; }

        public static RoutedUICommand AddReportGroup { get; set; }

        public static RoutedUICommand EditReportGroup { get; set; }

        public static RoutedUICommand DeleteReportGroup { get; set; }

        public static RoutedUICommand RepDuesNotRetPaymentAllSubdiv { get; set; }

        public static RoutedUICommand RepDuesOverLimit { get; set; }

        public static RoutedUICommand RepDuesListInvalid { get; set; }

        public static RoutedUICommand UnloadAVGDuesPercent { get; set; }

        public static RoutedUICommand EmpAVGDayPrice_Short { get; set; }

        public static RoutedUICommand EmpMissionDayPrice_Short { get; set; }

        public static RoutedUICommand Rep_EmpDieSalaryNote { get; set; }

        public static RoutedUICommand CloseCartulary { get; set; }

        public static RoutedUICommand OpenCartulary { get; set; }

        public static RoutedUICommand OpenViewEmpAccounts { get; set; }

        public static RoutedUICommand AddClientRetention { get; set; }

        public static RoutedUICommand EditClientRetention { get; set; }

        public static RoutedUICommand DeleteClientRetention { get; set; }

        public static RoutedUICommand LoadPerDataToAccount { get; set; }

        public static RoutedUICommand RepSubRetentDocs { get; set; }

        public static RoutedUICommand Rep_AllAlimony { get; set; }

        public static RoutedUICommand RepSalaryErrors { get; set; }

        public static RoutedUICommand ViewEmpRetentAccount { get; set; }

        public static RoutedUICommand ReplaceClientRetention { get; set; }

        public static RoutedUICommand AddMessage { get; set; }

        public static RoutedUICommand DeleteMessage { get; set; }

        public static RoutedUICommand SaveMessage { get; set; }

        public static RoutedUICommand ViewEditMessage { get; set; }

        public static RoutedUICommand ViewEmpSalaryRetents { get; set; }

        public static RoutedUICommand Rep_AddPremiumCatalog { get; set; }

        public static RoutedUICommand Rep_AddPremiumRegister { get; set; }

        public static RoutedUICommand UnloadSalary { get; set; }

        public static RoutedUICommand OpenViewCosolidReport { get; set; }

        public static RoutedUICommand AddConsolidItem { get; set; }

        public static RoutedUICommand DeleteConsolidItem { get; set; }

        public static RoutedUICommand SaveColsolidReport { get; set; }

        public static RoutedUICommand FileSumTransfer { get; set; }

        public static RoutedUICommand RepSubEmpTransferRetent { get; set; }

        public static RoutedUICommand RepRetentByOrdersEmp { get; set; }

        public static RoutedUICommand RepRegisterReports { get; set; }

        public static RoutedUICommand RepSalaryTransferNote { get; set; }

        public static RoutedUICommand RepDuesSubdivFundes { get; set; }

        public static RoutedUICommand RepUploadTxtClientAccount { get; set; }

        public static RoutedUICommand RepLoadTxtClientAccount { get; set; }

        public static RoutedUICommand Rep_TableCompare { get; set; }

        public static RoutedUICommand DumpVacToSalary { get; set; }

        public static RoutedUICommand RepDuesAvgHeadFundes { get; set; }

        public static RoutedUICommand RepDuesHarmProff { get; set; }

        public static RoutedUICommand Rep_EmpAccountNote { get; set; }

        public static RoutedUICommand RepAllTransferredSum { get; set; }

        public static RoutedUICommand DumpSalaryVacs { get; set; }

        public static RoutedUICommand EditSalaryVac { get; set; }

        public static RoutedUICommand EditSalaryDocTransfer { get; set; }

        public static RoutedUICommand LockSalaryDocum { get; set; }

        public static RoutedUICommand UnlockSalaryDocum { get; set; }

        public static RoutedUICommand ReAddSalaryDocumRelation { get; set; }

        public static RoutedUICommand RepSalaryPayNote { get; set; }

        public static RoutedUICommand RepConsolidVacReport { get; set; }

        public static RoutedUICommand RepUploadTxtSalaryPayNote { get; set; }

        public static RoutedUICommand RepCartularyVsSalary { get; set; }

        public static RoutedUICommand AddTypeBank { get; set; }

        public static RoutedUICommand DeleteTypeBank { get; set; }

        public static RoutedUICommand SaveTypeBank { get; set; }

        public static RoutedUICommand OpenViewTypeBank { get; set; }

        public static RoutedUICommand EditTypeBank { get; set; }

        public static RoutedUICommand Rep_CartularyConsolidSubdiv { get; set; }

        public static RoutedUICommand Rep_CartularyConsolidTypeBank { get; set; }

        public static RoutedUICommand Unload987ToTxt { get; set; }

        public static RoutedUICommand Rep_PaymentTableSheet { get; set; }

        public static RoutedUICommand RepTypeBankEmpTransfer { get; set; }

        public static RoutedUICommand UnloadPFR_Dop { get; set; }

        public static RoutedUICommand EmpAvgSickDayPrice { get; set; }

        public static RoutedUICommand AddAdvance { get; set; }

        public static RoutedUICommand EditAdvance { get; set; }

        public static RoutedUICommand DeleteAdvance { get; set; }

        public static RoutedUICommand CalcEmpAdvance { get; set; }

        public static RoutedUICommand CalcSubdivAdvance { get; set; }

        public static RoutedUICommand LoadSubdivTableAdvance { get; set; }

        public static RoutedUICommand RepAdvanceMainReport { get; set; }

        public static RoutedUICommand UnloadAdvanceData { get; set; }

        public static RoutedUICommand RepAdvanceCacheT_53 { get; set; }

        public static RoutedUICommand RepTypeBankSumTransfer { get; set; }

        public static RoutedUICommand CompareCacheAndSalary { get; set; }

        public static RoutedUICommand Rep_EmpDueCard { get; set; }

        public static RoutedUICommand OpenViewEconView { get; set; }

        public static RoutedUICommand RepEmpTransferByRegisters { get; set; }

        public static RoutedUICommand RepSalaryByMonths { get; set; }

        public static RoutedUICommand RepEconBookByPosition { get; set; }

        public static RoutedUICommand RepSalaryByPaymentType { get; set; }

        public static RoutedUICommand RepSalaryFPWByDegree { get; set; }

        public static RoutedUICommand RepActualDetails { get; set; }

        public static RoutedUICommand RepAVGPrintACPD { get; set; }

        public static RoutedUICommand RepShortBySubdiv { get; set; }

        public static RoutedUICommand ViewEconCard { get; set; }

        public static RoutedUICommand RepShortAccumBySubdiv { get; set; }

        public static RoutedUICommand RepSalaryPivotForCodeOrder { get; set; }

        public static RoutedUICommand RepSalaryPivotForCodeOrder2 { get; set; }

        public static RoutedUICommand RepSalaryByGroupAndDegree { get; set; }

        public static RoutedUICommand RepSalaryBySubdivAndEmp { get; set; }

        public static RoutedUICommand RepSalaryMothersPayment { get; set; }

        public static RoutedUICommand CloneReportGroup { get; set; }

        public static RoutedUICommand RepAccumPieceWork { get; set; }

        public static RoutedUICommand RepPercentPieceWork { get; set; }

        public static RoutedUICommand RepSalaryToAlongPieceWorker { get; set; }

        public static RoutedUICommand RepCountEmpPieceWorker { get; set; }

        public static RoutedUICommand RepOverTableHoursPieceWorker { get; set; }

        public static RoutedUICommand RepHoursForAlongPieceWorker { get; set; }

        public static RoutedUICommand RepNormHoursForAlongPieceWorker { get; set; }

        public static RoutedUICommand RepPercentPremPieceSalary { get; set; }

        public static RoutedUICommand RepActualHourPricePieceWork { get; set; }

        public static RoutedUICommand RepActualPercentNormPieceWork { get; set; }

        public static RoutedUICommand RepAvgWorkAndWorkerClassific { get; set; }

        public static RoutedUICommand RepReportByPosition { get; set; }

        public static RoutedUICommand RepCountEmpSalaryByValue { get; set; }

        public static RoutedUICommand RepSalByDegreeAndOrders1 { get; set; }

        public static RoutedUICommand RepSalaryCountDaysChanges { get; set; }

        public static RoutedUICommand RepSalaryDiscountChildEnd { get; set; }

        public static RoutedUICommand CalcEmpSalaryDocum { get; set; }

        public static RoutedUICommand RepEmpSickDocumPrint { get; set; }

        public static RoutedUICommand RepSickPaymentRegister { get; set; }

        public static RoutedUICommand RepPrintSalDocACPD { get; set; }

        public static RoutedUICommand RepDuesVersionCompare { get; set; }

        public static RoutedUICommand RepShortBySubdivGroupMaster { get; set; }

        public static RoutedUICommand Rep2NDFLReport { get; set; }

        public static RoutedUICommand RepSalaryDocumForPeriod { get; set; }

        public static RoutedUICommand SaveCompanyAccount { get; set; }

        public static RoutedUICommand AddCompanyAccount { get; set; }

        public static RoutedUICommand EditCompanyAccount { get; set; }

        public static RoutedUICommand DeleteCompanyAccount { get; set; }

        public static RoutedUICommand OpenViewCompanyAccount { get; set; }

        public static RoutedUICommand RepSalaryTransferNoteAttachment { get; set; }

        public static RoutedUICommand RepSalaryTransferRetent { get; set; }

        public static RoutedUICommand Rep_2NDFLErrors { get; set; }

        public static RoutedUICommand RepSalaryTaxLucre { get; set; }
        
        public static RoutedUICommand RepEmpTaxDiscount { get; set; }

        public static RoutedUICommand RepRetentDocOrder { get; set; }

        public static RoutedUICommand OpenViewCheckXML { get; set; }

        public static RoutedUICommand UploadNoNDFL2 { get; set; }

        public static RoutedUICommand RepShortBySubdivGroupMasterAndEmp { get; set; }

        public static RoutedUICommand OpenViewDistribution { get; set; }

        public static RoutedUICommand Rep_SalaryDistr1 { get; set; }

        public static RoutedUICommand Rep_SalaryDistrSecond { get; set; }

        public static RoutedUICommand Rep_SalaryDistrReceive { get; set; }

        public static RoutedUICommand RepCommonEMReport { get; set; }

        public static RoutedUICommand Rep_SalaryDistrMainDues { get; set; }

        public static RoutedUICommand CalcSalaryFullDistribution { get; set; }

        public static RoutedUICommand Rep_ClosedOrdersSalary { get; set; }

        public static RoutedUICommand RepSalaryByPaymentTypeEM { get; set; }

        public static RoutedUICommand Rep_DistrControl { get; set; }

        public static RoutedUICommand RepReportByPositionAccum { get; set; }

        public static RoutedUICommand RepShortAccumBySubdivPeriod { get; set; }

        public static RoutedUICommand CalcBaseDistribution { get; set; }

        public static RoutedUICommand AddDistribReciveSubdiv { get; set; }

        public static RoutedUICommand EditDistribReciveSubdiv { get; set; }

        public static RoutedUICommand DeleteDistribReciveSubdiv { get; set; }

        public static RoutedUICommand SaveReciveSubdiv { get; set; }

        public static RoutedUICommand PrintACPUDopzMain { get; set; }

        public static RoutedUICommand PrintACPUDopzDues { get; set; }

        public static RoutedUICommand UploadDopz26 { get; set; }

        public static RoutedUICommand UploadDopN { get; set; }

        public static RoutedUICommand UploadDopz8 { get; set; }

        public static RoutedUICommand UploadDopz { get; set; }

        public static RoutedUICommand UploadDopz5 { get; set; }

        public static RoutedUICommand UploadMem21 { get; set; }

        public static RoutedUICommand UploadDopz8_2 { get; set; }

        public static RoutedUICommand AddCorrSalaryDistr { get; set; }

        public static RoutedUICommand EditCorrSalaryDistr { get; set; }

        public static RoutedUICommand DeleteCorrSalaryDistr { get; set; }

        public static RoutedUICommand SaveSalaryAddCorr { get; set; }

        public static RoutedUICommand ReplaceDistrBaseOrder { get; set; }

        public static RoutedUICommand ReplaceOrders { get; set; }

        public static RoutedUICommand UploadDop20 { get; set; }

        public static RoutedUICommand UploadDopzPril21 { get; set; }

        public static RoutedUICommand CreatePaidCartularyRef { get; set; }

        public static RoutedUICommand RepDistributionEconReport { get; set; }

        public static RoutedUICommand RepDistributionEconReport1 { get; set; }

        public static RoutedUICommand RepDistributionEconSubdiv { get; set; }

        public static RoutedUICommand ViewSalaryHistory { get; set; }

        public static RoutedUICommand Rep_QuarterReservDistr { get; set; }

        public static RoutedUICommand Rep_VacReservDistr { get; set; }

        public static RoutedUICommand Rep_QuarterReservMem { get; set; }

        public static RoutedUICommand Rep_VacReservMem { get; set; }

        public static RoutedUICommand UploadMem197 { get; set; }

        public static RoutedUICommand UploadMem198 { get; set; }

        public static RoutedUICommand ViewUsersControl { get; set; }

        public static RoutedUICommand OpenViewPrintTabs { get; set; }

        public static RoutedUICommand PrintEmpTabs { get; set; }

        public static RoutedUICommand AddTableBrigage { get; set; }

        public static RoutedUICommand EditTableBrigage { get; set; }

        public static RoutedUICommand DeleteTableBrigage { get; set; }

        public static RoutedUICommand SaveTableBrigage { get; set; }

        public static RoutedUICommand Rep_08DegreePiecePrice { get; set; }

        public static RoutedUICommand OpenViewBrigage { get; set; }

        public static RoutedUICommand RepTableBrigage { get; set; }

        public static RoutedUICommand AddPieceWork { get; set; }

        public static RoutedUICommand EditPieceWork { get; set; }

        public static RoutedUICommand DeletePieceWork { get; set; }

        public static RoutedUICommand CopyAccountsFromMainWork { get; set; }

        public static RoutedUICommand GenerateTableBrigage { get; set; }

        public static RoutedUICommand RepTableBrigage2 { get; set; }

        public static RoutedUICommand BrigageDictionary { get; set; }

        public static RoutedUICommand AddBrigage { get; set; }

        public static RoutedUICommand DeleteBrigage { get; set; }

        public static RoutedUICommand SaveBrigage { get; set; }

        public static RoutedUICommand AddCustomDistribution { get; set; }

        public static RoutedUICommand DeleteCustomDistribution { get; set; }

        public static RoutedUICommand SaveCustomDistribution { get; set; }
    #endregion

        public static RoutedUICommand SaveTaxCompany { get; set; }

        public static RoutedUICommand OpenViewTaxes { get; set; }

        public static RoutedUICommand EditTaxCompany { get; set; }

        public static RoutedUICommand DeleteTaxCompany { get; set; }

        public static RoutedUICommand AddTaxCompany { get; set; }

        public static RoutedUICommand ViewTaxEmpDocum { get; set; }
        public static RoutedUICommand LoadTableBrigageToSalary { get; private set; }
        public static RoutedUICommand FormAVDEmpDataTable { get; private set; }
        public static RoutedUICommand OpenViewDocumTransfer { get; private set; }
        public static RoutedUICommand AddDocumTransfer { get; private set; }
        public static RoutedUICommand EditDocumTransfer { get; private set; }
        public static RoutedUICommand SaveDocumTransfer { get; private set; }
        public static RoutedUICommand DeleteDocumTransfer { get; private set; }
        public static RoutedUICommand DeleteDocTransferRelation { get; private set; }
        public static RoutedUICommand AddDocTransferRelation { get; private set; }
        public static RoutedUICommand RepSalaryMissionTransferNote { get; private set; }
        public static RoutedUICommand AddTaxEmpDocum { get; private set; }
        public static RoutedUICommand EditTaxEmpDocum { get; private set; }
        public static RoutedUICommand DeleteTaxEmpDocum { get; private set; }
        public static RoutedUICommand SaveTaxEmpDocum { get; private set; }
        public static RoutedUICommand RepTaxesConsolidation { get; private set; }
        public static RoutedUICommand LoadTaxesDocum { get; private set; }
        public static RoutedUICommand ExtendTaxDiscounts { get; private set; }
        public static RoutedUICommand Rep_CompareRetent401402 { get; private set; }
        public static RoutedUICommand SaveForeignEmp { get; private set; }
        public static RoutedUICommand Upload2NDFL { get; private set; }
        public static RoutedUICommand RepTaxesDocumCommon { get; private set; }
        public static RoutedUICommand RelocateNegativSalary { get; private set; }
        public static RoutedUICommand UnloadSalaryToZarpl { get; private set; }
    }

}
