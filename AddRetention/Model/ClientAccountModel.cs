using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using EntityGenerator;
using System.ComponentModel;
using System.Data;

namespace Salary.Model
{
    /// <summary>
    /// Счет сотрудника
    /// </summary>
    [Table(Name = "CLIENT_ACCOUNT")]
    public partial class ClientAccount : RowEntityBase, IDataErrorInfo
    {
        #region Class Members


        [Column(Name = "CLIENT_ACCOUNT_ID")]
        public Decimal? ClientAccountID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ClientAccountID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ClientAccountID, value);
            }
        }

        /// <summary>
        /// Дата выдачи док-та удостоверяющего личность
        /// </summary>
        [Column(Name = "DATE_DOC")]
        public DateTime? DateDoc
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateDoc);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateDoc, value);
            }
        }

        /// <summary>
        /// Код док-та удостов. личность
        /// </summary>
        [Column(Name = "CODE_DOC")]
        public Decimal? CodeDoc
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CodeDoc);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CodeDoc, value);
            }
        }

        /// <summary>
        /// Номер договора индивидуального страхования
        /// </summary>
        [Column(Name = "PER_INSURANCE_NUM")]
        public String PerInsuranceNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerInsuranceNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerInsuranceNum, value);
            }
        }

        /// <summary>
        /// страховой номер сотрудника
        /// </summary>
        [Column(Name = "INSURANCE_NUM")]
        public String InsuranceNum
        {
            get
            {
                return this.GetDataRowField<String>(() => InsuranceNum);
            }
            set
            {
                UpdateDataRow<String>(() => InsuranceNum, value);
            }
        }

        /// <summary>
        /// Тип счета
        /// </summary>
        [Column(Name = "TYPE_ACCOUNT_ID")]
        public Decimal? TypeAccountID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeAccountID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeAccountID, value);
                RaisePropertyChanged(() => TypeBankID);
                RaisePropertyChanged(() => NumberCard);
                RaisePropertyChanged(() => NumberAccount);
            }
        }


        [Column(Name = "PLF_INDEX")]
        public String PlfIndex
        {
            get
            {
                return this.GetDataRowField<String>(() => PlfIndex);
            }
            set
            {
                UpdateDataRow<String>(() => PlfIndex, value);
            }
        }


        [Column(Name = "PLF_ADDRESS")]
        public String PlfAddress
        {
            get
            {
                return this.GetDataRowField<String>(() => PlfAddress);
            }
            set
            {
                UpdateDataRow<String>(() => PlfAddress, value);
            }
        }


        [Column(Name = "PLF_NAME")]
        public String PlfName
        {
            get
            {
                return this.GetDataRowField<String>(() => PlfName);
            }
            set
            {
                UpdateDataRow<String>(() => PlfName, value);
            }
        }

        /// <summary>
        /// Номер карты
        /// </summary>
        [Column(Name = "NUMBER_CARD")]
        public String NumberCard
        {
            get
            {
                return this.GetDataRowField<String>(() => NumberCard);
            }
            set
            {
                UpdateDataRow<String>(() => NumberCard, value);
                RaisePropertyChanged(() => NumberAccount);
            }
        }

        /// <summary>
        /// Город бля бла
        /// </summary>
        [Column(Name = "GET_CITY")]
        public String GetCity
        {
            get
            {
                return this.GetDataRowField<String>(() => GetCity);
            }
            set
            {
                UpdateDataRow<String>(() => GetCity, value);
            }
        }


        [Column(Name = "GET_PLACE")]
        public String GetPlace
        {
            get
            {
                return this.GetDataRowField<String>(() => GetPlace);
            }
            set
            {
                UpdateDataRow<String>(() => GetPlace, value);
            }
        }

        /// <summary>
        /// Номер паспорта
        /// </summary>
        [Column(Name = "PASSPORT_NUMBER")]
        public String PassportNumber
        {
            get
            {
                return this.GetDataRowField<String>(() => PassportNumber);
            }
            set
            {
                UpdateDataRow<String>(() => PassportNumber, value);
            }
        }

        /// <summary>
        /// Серия паспорта
        /// </summary>
        [Column(Name = "PASSPORT_SERIES")]
        public String PassportSeries
        {
            get
            {
                return this.GetDataRowField<String>(() => PassportSeries);
            }
            set
            {
                UpdateDataRow<String>(() => PassportSeries, value);
            }
        }


        [Column(Name = "OWNER_MIDDLE_NAME")]
        public String OwnerMiddleName
        {
            get
            {
                return this.GetDataRowField<String>(() => OwnerMiddleName);
            }
            set
            {
                UpdateDataRow<String>(() => OwnerMiddleName, value);
            }
        }


        [Column(Name = "OWNER_FAMILY")]
        public String OwnerFamily
        {
            get
            {
                return this.GetDataRowField<String>(() => OwnerFamily);
            }
            set
            {
                UpdateDataRow<String>(() => OwnerFamily, value);
            }
        }

        /// <summary>
        /// Имя владельца
        /// </summary>
        [Column(Name = "OWNER_NAME")]
        public String OwnerName
        {
            get
            {
                return this.GetDataRowField<String>(() => OwnerName);
            }
            set
            {
                UpdateDataRow<String>(() => OwnerName, value);
            }
        }


        [Column(Name = "TYPE_BANK_ID")]
        public Decimal? TypeBankID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeBankID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeBankID, value);
            }
        }

        /// <summary>
        /// Номер счета
        /// </summary>
        [Column(Name = "NUMBER_ACCOUNT")]
        public String NumberAccount
        {
            get
            {
                return this.GetDataRowField<String>(() => NumberAccount);
            }
            set
            {
                UpdateDataRow<String>(() => NumberAccount, value);
                RaisePropertyChanged(() => NumberCard);
            }
        }


        [Column(Name = "TRANSFER_ID")]
        public Decimal? TransferID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TransferID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TransferID, value);
            }
        }


        public TypeAccount TypeAccount
        {
            get
            {
                if (this.TypeAccountID == null)
                    return new TypeAccount();
                else
                {
                    TypeAccount t = new TypeAccount();
                    DataTable tt = this.DataRow.Table ?? this.DataTable;
                    t.DataRow = tt.DataSet.Tables["TYPE_ACCOUNT"].Rows.OfType<DataRow>().Where(r => r.Field2<Decimal?>("TYPE_ACCOUNT_ID") == this.TypeAccountID).FirstOrDefault();
                    return t;
                }
            }
        }
        #endregion


        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public new string this[string column_name]
        {
            get
            {
                switch (column_name)
                {
                    case "TypeAccountID": if (TypeAccountID == null) return "Требуется выбрать тип счета сотрудника"; break;
                    case "TransferID": if (TransferID == null) return "Требуется выбрать сотрудника"; break;
                    case "TypeBankID": if (TypeAccountID!=null && TypeAccount.NeedTypeBank==1 && TypeBankID == null) return "Требуется выбрать банк для перечисления"; break;
                    case "NumberAccount": if (TypeAccountID != null && TypeAccount.NeedTypeBank == 1 && NumberCard == null && NumberAccount == null) return "Требуется указать счет или номер карты"; break;
                    case "NumberCard": if (TypeAccountID != null && TypeAccount.NeedTypeBank == 1 && NumberCard == null && NumberAccount == null) return "Требуется указать счет или номер карты"; break;
                    case "PassportNumber": if (!String.IsNullOrEmpty(PassportNumber) && PassportNumber.Length != 6) return "Номер паспорта должен быть 6 цифр"; break;
                }
                return String.Empty;
            }
        }
    }

    [Table(Name="TYPE_ACCOUNT")]
    public partial class TypeAccount : RowEntityBase
    {
        #region Class Members
        
        
        [Column(Name="TYPE_ACCOUNT_ID")]
        public Decimal? TypeAccountID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeAccountID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeAccountID, value);
            }
        }

        /// <summary>
        /// Требуется ли банк для этого типа счета
        /// </summary>
        [Column(Name="NEED_TYPE_BANK")]
        public Decimal? NeedTypeBank
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => NeedTypeBank);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => NeedTypeBank, value);
            }
        }

        
        [Column(Name="NAME_TYPE_ACCOUNT")]
        public String NameTypeAccount
        {
            get
            {
                return this.GetDataRowField<String>(() => NameTypeAccount);
            }
            set
            {
                UpdateDataRow<String>(() => NameTypeAccount, value);
            }
        }
        #endregion
    }
}
