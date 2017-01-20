using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EntityGenerator;

namespace Salary.Helpers
{
    public class AppDictionaries
    {
        private static Dictionary<string, decimal> _codePaymentToIDs;
        private static Dictionary<decimal, PaymentType> _codePaymentIDToValue;
        private static Dictionary<decimal, Subdiv> _subdivIdToValue;
        private static Dictionary<string, decimal> _orderCodeToID;
        /// <summary>
        /// Словарь типа "CODE_PAYMENT-PAYMENT_TYPE_ID"
        /// </summary>
        public static Dictionary<string, decimal> CodePaymentToID
        {
            get
            {
                if (_codePaymentToIDs == null)
                    _codePaymentToIDs = AppDataSet.Tables["PAYMENT_TYPE"].Rows.OfType<DataRow>().ToDictionary(r => r["CODE_PAYMENT"].ToString(), r => r.Field2<decimal>("PAYMENT_TYPE_ID"));
                return _codePaymentToIDs;
            }
        }

        /// <summary>
        /// Словарь типа Айдишник вида оплат - к виду оплат классу
        /// </summary>
        public static Dictionary<decimal, PaymentType> CodePaymentIDToValue
        {
            get
            {
                if (_codePaymentIDToValue == null)
                    _codePaymentIDToValue = AppDataSet.Tables["PAYMENT_TYPE"].Rows.OfType<DataRow>().ToDictionary(r => r.Field2<Decimal>("PAYMENT_TYPE_ID"), r => new PaymentType() { DataRow = r });
                return _codePaymentIDToValue;
            }
        }

        /// <summary>
        /// Словарь типа Уникальный номер - к подразделению (полные данные)
        /// </summary>
        public static Dictionary<decimal, Subdiv> SubdivIDToValue
        {
            get
            {
                if (_subdivIdToValue == null)
                    _subdivIdToValue = AppDataSet.Tables["SUBDIV"].Rows.OfType<DataRow>().ToDictionary(r => r.Field2<Decimal>("SUBDIV_ID"), r => new Subdiv() { DataRow = r });
                return _subdivIdToValue;
            }
        }

        /// <summary>
        /// Словарь типа  - код заказа к айдишнику заказа
        /// </summary>
        public static Dictionary<string, decimal> OrderCodeToID
        {
            get
            {
                if (_orderCodeToID == null)
                    _orderCodeToID = AppDataSet.Tables["ORDER"].Rows.OfType<DataRow>().ToDictionary(r => r.Field2<string>("ORDER_NAME"), r => r.Field2<decimal>("ORDER_ID"));
                return _orderCodeToID;
            }
        }
    }
}
