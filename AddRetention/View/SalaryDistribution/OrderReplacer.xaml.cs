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
using System.ComponentModel;
using System.Data;
using LibrarySalary.Helpers;
using Oracle.DataAccess.Client;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for OrderRepacer.xaml
    /// </summary>
    public partial class OrderReplacer : Window
    {
        private OrderReplaceModel _model;
        public OrderReplacer(DateTime selectedDate, decimal? subdiv_id, decimal? def_old_order_id=null)
        {
            _model = new OrderReplaceModel(selectedDate, subdiv_id);
            InitializeComponent();
            _model.OldOrderID = def_old_order_id;
            DataContext = Model;
        }

        public OrderReplaceModel Model
        {
            get
            {
                return _model;
            }
        }


        /// <summary>
        /// Обработчик проверки доступности команды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ControlRoles.GetState(e.Command) && Model != null && string.IsNullOrEmpty(Model.Error);
        }

        /// <summary>
        /// Обработчик команды сохранения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(this, string.Format("Вы действительно хотите заменить заказ {0} на {1} по подразделению №{2} за {3:MMMM yyyy}?", 
                                        Model.OldOrderName, Model.NewOrderName, Model.CodeSubdiv, Model.SelectedDate), "Зарплата предприятия",
                                        MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
                Model.Save();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }

    public class OrderReplaceModel : NotificationObject, IDataErrorInfo
    {
        private decimal? _subdivID;
        private decimal? _oldOrderID;
        private decimal? _newOrderID;
        private DateTime? _selectedDate;

        DataSet ds;

        /// <summary>
        /// Конструктор с загрузкой данных
        /// </summary>
        /// <param name="subdiv_id"></param>
        public OrderReplaceModel(DateTime selecteDate, decimal? subdiv_id)
        {
            // TODO: Complete member initialization
            this._subdivID = subdiv_id;
            _selectedDate = selecteDate;
            ds = new DataSet();
            OracleDataAdapter oda = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Distribution/SelectOrderForReplace.sql"), Connect.CurConnect);
            oda.SelectCommand.BindByName = true;
            oda.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, SelectedDate, ParameterDirection.Input);
            oda.SelectCommand.Parameters.Add("c1", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.SelectCommand.Parameters.Add("c2", OracleDbType.RefCursor, ParameterDirection.Output);
            oda.TableMappings.Add("Table", "OLD_ORDERS");
            oda.TableMappings.Add("Table1", "NEW_ORDERS");

            oda.Fill(ds);
        }

        /// <summary>
        /// Выбранная дата для замены
        /// </summary>
        public DateTime? SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged(() => SelectedDate);
            }
        }

        /// <summary>
        /// Подразделение где проводится замена заказа
        /// </summary>
        public decimal? SubdivID
        {
            get
            {
                return _subdivID;
            }
            set
            {
                _subdivID = value;
                RaisePropertyChanged(() => SubdivID);
            }
        }

        /// <summary>
        /// Выбранный код подразделения
        /// </summary>
        public string CodeSubdiv
        {
            get
            {
                if (SubdivID == null)
                    return "";
                if (SubdivID == 0)
                    return "У-УАЗ";
                else
                    return AppDataSet.Tables["SUBDIV"].Rows.OfType<DataRow>().Where(r => r.Field2<Decimal>("SUBDIV_ID") == _subdivID).Select(r => r["CODE_SUBDIV"].ToString()).FirstOrDefault();
            }
        }

        /// <summary>
        /// Выбранный заказ новый
        /// </summary>
        public string NewOrderName
        {
            get
            {
                if (NewOrderID == null)
                    return "(не указан)";
                else
                    return ds.Tables["NEW_ORDERS"].Rows.OfType<DataRow>().Where(r => r.Field2<Decimal>("ORDER_ID") == NewOrderID).Select(r => r["ORDER_NAME"].ToString()).FirstOrDefault();
            }
        }

        /// <summary>
        /// Выбранный заказ старый
        /// </summary>
        public string OldOrderName
        {
            get
            {
                if (OldOrderID == null)
                    return "(не указан)";
                else
                    return ds.Tables["OLD_ORDERS"].Rows.OfType<DataRow>().Where(r => r.Field2<Decimal>("ORDER_ID") == OldOrderID).Select(r => r["ORDER_NAME"].ToString()).FirstOrDefault();
            }
        }

        
        /// <summary>
        /// Старый заказ, заменяемый
        /// </summary>
        public decimal? OldOrderID
        {
            get
            {
                return _oldOrderID;
            }
            set
            {
                _oldOrderID = value;
                RaisePropertyChanged(() => OldOrderID);
            }
        }

        /// <summary>
        /// Новый заказ на который требуется заменить
        /// </summary>
        public decimal? NewOrderID
        {
            get
            {
                return _newOrderID;
            }
            set
            {
                _newOrderID = value;
                RaisePropertyChanged(() => NewOrderID);
            }
        }

        /// <summary>
        /// Список заказов новый
        /// </summary>
        public DataView NewOrderSource
        {
            get
            {
                return new DataView(ds.Tables["NEW_ORDERS"], "", "", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Список заказов старый
        /// </summary>
        public DataView OldOrderSource
        {
            get
            {
                return new DataView(ds.Tables["OLD_ORDERS"], "", "", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Ошибка для всей модели
        /// </summary>
        public string Error
        {
            get 
            {
                if (SubdivID == null)
                    return "Не указано подразделение";
                if (NewOrderID == null)
                    return "Не указан заказ на который требуется заменить";
                if (OldOrderID == null)
                    return "Не указан заказ который требуется изменить";
                return string.Empty;
            }
        }

        /// <summary>
        /// Ошибка для конкретного поля
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get 
            {
                if (columnName == "SubdivID" && SubdivID == null) return "Не указано подразделение";
                if (columnName == "NewOrderID" && NewOrderID == null) return "Не указан старый заменяемый заказ";
                if (columnName == "OldOrderID" && OldOrderID == null) return "Не указан новый заказ";
                return string.Empty;
            }
        }

        /// <summary>
        /// Сама процедура замены заказов
        /// </summary>
        internal void Save()
        {
            OracleTransaction tr = Connect.CurConnect.BeginTransaction();
            OracleCommand cmd = new OracleCommand(string.Format("begin {1}.OrderReplace(:p_date, :p_subdiv_id, :p_old_order_id, :p_new_order_id, :p_table_name);end;", Connect.SchemaApstaff, Connect.SchemaSalary), Connect.CurConnect);
            cmd.BindByName=true;
            cmd.Parameters.Add("p_date", OracleDbType.Date, SelectedDate, ParameterDirection.Input);
            cmd.Parameters.Add("p_subdiv_id", OracleDbType.Decimal, SubdivID, ParameterDirection.Input);
            cmd.Parameters.Add("p_old_order_id", OracleDbType.Decimal, OldOrderID, ParameterDirection.Input);
            cmd.Parameters.Add("p_new_order_id", OracleDbType.Decimal, NewOrderID, ParameterDirection.Input);
            cmd.Parameters.Add("p_table_name", OracleDbType.Varchar2, "SALARY_ADDITION", ParameterDirection.Input);
            try
            {
                cmd.ExecuteNonQuery();
                tr.Commit();
                MessageBox.Show("Замена успешно произведена!", "Зарплата предприятия");
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show(ex.GetFormattedException(), "Зарплата предприятия");
            }
        }
    }

}
