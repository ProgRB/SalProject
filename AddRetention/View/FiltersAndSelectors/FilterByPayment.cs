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

namespace Salary.View
{

    /// <summary>
    /// Interaction logic for FilterPaymentChanges.xaml
    /// </summary>
    public partial class FilterByPayment : Window
    {
        private DateTime? _date_begin;
        private DateTime? _date_end;
        private Decimal? _subdivID;

        private int[] type_pay_filter = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        /// <summary>
        /// выбор фильтра для выгрузки данных
        /// </summary>
        /// <param name="subdiv_id">подразделение, аайдишник</param>
        /// <param name="period1"> дата начала изменений</param>
        /// <param name="period2"> дата окончания изменений</param>
        /// <param name="type_payment_filter"> дата начала периода ЗП</param>
        public FilterByPayment(object subdiv_id, DateTime period1, DateTime period2, int[] type_payment_filter = null)
        {
            SubdivID = decimal.Parse(subdiv_id.ToString());
            DateBegin = period1;
            DateEnd = period2;
            if (type_payment_filter != null)
                type_pay_filter = type_payment_filter;
            InitializeComponent();
        }

        private List<PaymentTypeChecked> _list_payment;

        public List<PaymentTypeChecked> ListPaymentSource
        {
            get
            {
                if (_list_payment == null)
                    _list_payment = AppDataSet.Tables["PAYMENT_TYPE"].Rows.OfType<DataRow>().Where(t=>type_pay_filter.Contains(Convert.ToInt32(t.Field<Decimal>("TYPE_PAYMENT_TYPE_ID"))))
                        .Select(r => new PaymentTypeChecked(r.Field<Decimal?>("PAYMENT_TYPE_ID"), r.Field<string>("CODE_PAYMENT"), false)).OrderBy(r => r.CodePayment).ToList();
                return _list_payment;
            }
        }

        public string CodePayment
        {
            get { return (cbTypePayment.SelectedItem != null ? cbTypePayment.Text : ""); }
        }

        /// <summary>
        /// Выбранный код подразделения
        /// </summary>
        public string CodeSubdiv
        {
            get
            {
                return SubdivSelector1.CodeSubdiv;
            }
        }

        /// <summary>
        /// Список выбранных айдишников
        /// </summary>
        public decimal[] PaymentTypeIDs
        {
            get
            {
                if (cbTypePayment.SelectedItems != null)
                    return cbTypePayment.SelectedItems.OfType<PaymentTypeChecked>().Select(r=>r.PaymentTypeID.Value).ToArray();
                else return null;
            }
        }
        /// <summary>
        /// Номер подразделения уникальный
        /// </summary>
        public Decimal? SubdivID
        {
            get { return _subdivID; }
            set { _subdivID = value; }
        }

        /// <summary>
        /// Период за который прошли шифры оплат
        /// </summary>
        public DateTime? DateEnd
        {
            get { return _date_end; }
            set { _date_end = value; }
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
            }
        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
    }
    
}
