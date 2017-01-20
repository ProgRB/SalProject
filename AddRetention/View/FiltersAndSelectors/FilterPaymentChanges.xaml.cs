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
using System.Data;
using Salary.Helpers;
using Salary.Interfaces;

namespace Salary.View
{

    /// <summary>
    /// Interaction logic for FilterPaymentChanges.xaml
    /// </summary>
    public partial class FilterPaymentChanges : Window
    {
        private  FilterPaymentModel _model;

        /// <summary>
        /// выбор фильтра для выгрузки данных
        /// </summary>
        /// <param name="subdiv_id">подразделение, аайдишник</param>
        /// <param name="date1"> дата начала изменений</param>
        /// <param name="date2"> дата окончания изменений</param>
        /// <param name="period1"> дата начала периода ЗП</param>
        /// <param name="period2"> дата окончания периода ЗП</param>
        public FilterPaymentChanges(object subdiv_id, DateTime date1, DateTime date2, DateTime period1, DateTime period2)
        {
            _model = new FilterPaymentModel()
                                    {
                                        SubdivID = decimal.Parse(subdiv_id.ToString()),
                                        DateBegin = period1,
                                        DateEnd = period2,
                                        ChangeBegin = date1,
                                        ChangeEnd = date2
                                    };
            InitializeComponent();
            this.DataContext = Model;
        }

        /// <summary>
        /// Модель данных для фильтра
        /// </summary>
        public FilterPaymentModel Model
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

    /// <summary>
    /// Вспомогательная модель для фильтра
    /// </summary>
    public class FilterPaymentModel:NotificationObject, ICustomFilter
    {
        private DateTime? _date_begin;
        private DateTime? _date_end;
        private DateTime? _change_begin, _change_end;
        private Decimal? _subdivID;
         private List<PaymentTypeChecked> _list_payment;
        private bool _isPaymentTypeEnabled = false;

        public List<PaymentTypeChecked> ListPaymentSource
        {
            get
            {
                if (_list_payment == null)
                    _list_payment = AppDataSet.Tables["PAYMENT_TYPE"].Rows.OfType<DataRow>().Select(r => new PaymentTypeChecked(r.Field<Decimal?>("PAYMENT_TYPE_ID"), r.Field<string>("CODE_PAYMENT"), false)).OrderBy(r => r.CodePayment).ToList();
                return _list_payment;
            }
        }

        /// <summary>
        /// Выбранный код подразделения
        /// </summary>
        public string CodeSubdiv
        {
            get
            {
                return AppDataSet.Tables["SUBDIV"].Rows.OfType<DataRow>().Where(r => r.Field2<Decimal?>("SUBDIV_ID") == SubdivID).Select(r=>r["CODE_SUBDIV"].ToString()).FirstOrDefault();
            }
        }

        /// <summary>
        /// Список выбранных айдишников
        /// </summary>
        public decimal[] PaymentTypeIDs
        {
            get
            {
                if (_list_payment != null)
                    return _list_payment.Where(r=>r.IsChecked).Select(r=>r.PaymentTypeID.Value).ToArray();
                else return null;
            }
        }
        /// <summary>
        /// Номер подразделения уникальный
        /// </summary>
        public Decimal? SubdivID
        {
            get { return _subdivID; }
            set 
            { 
                _subdivID = value;
                RaisePropertyChanged(() => SubdivID);
            }
        }

        /// <summary>
        /// Период за который прошли шифры оплат
        /// </summary>
        public DateTime? DateEnd
        {
            get 
            { 
                return _date_end; 
            }
            set 
            { 
                _date_end = value;
                RaisePropertyChanged(() => DateEnd);
            }
        }


        /// <summary>
        /// Период за который прошли шифры оплат
        /// </summary>
        public DateTime? DateBegin
        {
            get
            {
                return _date_begin;
            }
            set
            {
                _date_begin = value;
                RaisePropertyChanged(() => DateBegin);
            }
        }

        /// <summary>
        /// Интервал внесения записей, даты добавления
        /// </summary>
        public DateTime? ChangeEnd
        {
            get { return _change_end; }
            set 
            { 
                _change_end = value;
                RaisePropertyChanged(() => ChangeEnd);
            }
        }


        /// <summary>
        /// Интервал внесения записей, даты добавления
        /// </summary>
        public DateTime? ChangeBegin
        {
            get
            {
                return _change_begin;
            }
            set
            {
                _change_begin = value;
                RaisePropertyChanged(() => ChangeBegin);
            }
        }

        string _changeBeginCaption = "Дата начала изменений";
        public string ChangeBeginCaption
        {
            get
            {
                return _changeBeginCaption;
            }
            set
            {
                _changeBeginCaption = value;
                RaisePropertyChanged(() => ChangeBeginCaption);
            }
        }

       
#region Свойства определеяющие доступность полей

        /// <summary>
        /// Доступен ли выбор типов оплат
        /// </summary>
        public bool IsPayTypeEnabled
        {
            get
            {
                return _isPaymentTypeEnabled;
            }
            set
            {
                _isPaymentTypeEnabled = value;
                RaisePropertyChanged(() => IsPayTypeEnabled);
            }
        }

        bool _isChangeEndEnabled;
        /// <summary>
        /// Доступен ли для показа окончание периода
        /// </summary>
        public bool IsChangeEndEnabled
        {
            get
            {
                return _isChangeEndEnabled;
            }
            set
            {
                _isChangeEndEnabled = value;
                RaisePropertyChanged(() => IsChangeEndEnabled);
            }
        }
#endregion

        public DateTime GetDate()
        {
            return DateBegin.Value;
        }

        public DateTime GetDateBegin()
        {
            return DateBegin.Value;
        }

        public DateTime GetDateEnd()
        {
            return DateEnd.Value;
        }

        public decimal? GetSubdivID()
        {
            return SubdivID;
        }

        public decimal[] GetDegreeIDs()
        {
            throw new NotImplementedException();
        }
    }

    public class PaymentTypeChecked
    {
        public decimal? PaymentTypeID
        { get; set; }
        public string CodePayment
        { get; set; }
        public bool IsChecked
        { get; set; }
        public PaymentTypeChecked(decimal? _payment_typeID, string _code_payment, bool is_checked)
        {
            PaymentTypeID = _payment_typeID;
            CodePayment = _code_payment;
            IsChecked = is_checked;
        }
    }
}
