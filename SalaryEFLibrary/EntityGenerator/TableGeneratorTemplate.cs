/***********************************************************/
/**********   Generated at 12.01.2017 14:32:17     ********/
/*********************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Data;
using Oracle.DataAccess.Client;
using System.Data.Linq.Mapping;

namespace EntityGenerator
{
    /// <summary>
    /// Таблица удержаний
    /// </summary>

    [Table(Name="RETENTION"), SchemaName("")]
    public partial class Retention : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный номер удержания
        /// </summary>
        [Column(Name="RETENTION_ID", CanBeNull=false, IsPrimaryKey=true)]
        public Decimal? RetentionID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RetentionID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentionID, value);
            }
        }
        /// <summary>
        /// Дата начала удержания
        /// </summary>
        [Column(Name="DATE_START_RET")]
        public DateTime? DateStartRet
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateStartRet);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateStartRet, value);
            }
        }
        [Column(Name="DATE_END_RET")]
        public DateTime? DateEndRet
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateEndRet);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateEndRet, value);
            }
        }
                #endregion
    }


    [Table(Name="CLIENT_RETENT_RELATION"), SchemaName("")]
    public partial class ClientRetentRelation : RowEntityBase
    {
        #region Class Members
        [Column(Name="CLIENT_RETENT_RELATION_ID")]
        public Decimal? ClientRetentRelationID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ClientRetentRelationID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ClientRetentRelationID, value);
            }
        }
        /// <summary>
        /// Текст комментария к перечислению
        /// </summary>
        [Column(Name="RELATION_COMMENT")]
        public String RelationComment
        {
            get
            {
                return this.GetDataRowField<String>(() => RelationComment);
            }
            set
            {
                UpdateDataRow<String>(() => RelationComment, value);
            }
        }
        /// <summary>
        /// КБК
        /// </summary>
        [Column(Name="BCC_CODE")]
        public String BccCode
        {
            get
            {
                return this.GetDataRowField<String>(() => BccCode);
            }
            set
            {
                UpdateDataRow<String>(() => BccCode, value);
            }
        }
        /// <summary>
        /// ОКАТО
        /// </summary>
        [Column(Name="OKATO")]
        public String Okato
        {
            get
            {
                return this.GetDataRowField<String>(() => Okato);
            }
            set
            {
                UpdateDataRow<String>(() => Okato, value);
            }
        }
        /// <summary>
        /// Сумма ограничения для перечисления
        /// </summary>
        [Column(Name="RESTRICT_SUM")]
        public Decimal? RestrictSum
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RestrictSum);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RestrictSum, value);
            }
        }
        /// <summary>
        /// Дата окончания перечисления
        /// </summary>
        [Column(Name="DATE_END_RELATION")]
        public DateTime? DateEndRelation
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateEndRelation);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateEndRelation, value);
            }
        }
        /// <summary>
        /// Дата начала перечисления
        /// </summary>
        [Column(Name="DATE_BEGIN_RELATION")]
        public DateTime? DateBeginRelation
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateBeginRelation);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateBeginRelation, value);
            }
        }
        /// <summary>
        /// счет перечисления
        /// </summary>
        [Column(Name="CLIENT_ACCOUNT_ID")]
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
        /// Документ удержания для перечисления
        /// </summary>
        [Column(Name="RETENTION_ID")]
        public Decimal? RetentionID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RetentionID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentionID, value);
            }
        }
                #endregion
    }


    [Table(Name="REPORT_GROUP"), SchemaName("")]
    public partial class ReportGroup : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный номер кода группы
        /// </summary>
        [Column(Name="REPORT_GROUP_ID")]
        public Decimal? ReportGroupID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ReportGroupID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ReportGroupID, value);
            }
        }
        /// <summary>
        /// Комментарий к группе - чаще где используется
        /// </summary>
        [Column(Name="GROUP_COMMENT")]
        public String GroupComment
        {
            get
            {
                return this.GetDataRowField<String>(() => GroupComment);
            }
            set
            {
                UpdateDataRow<String>(() => GroupComment, value);
            }
        }
        [Column(Name="PARENT_GROUP_ID")]
        public Decimal? ParentGroupID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ParentGroupID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ParentGroupID, value);
            }
        }
        /// <summary>
        /// Подрядковый номер сортировки группы
        /// </summary>
        [Column(Name="SORT_NUMBER")]
        public Decimal? SortNumber
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SortNumber);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SortNumber, value);
            }
        }
        /// <summary>
        /// Дата окончания действия группы
        /// </summary>
        [Column(Name="DATE_END_REPORT")]
        public DateTime? DateEndReport
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateEndReport);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateEndReport, value);
            }
        }
        /// <summary>
        /// Дата начала действия группы
        /// </summary>
        [Column(Name="DATE_BEGIN_REPORT")]
        public DateTime? DateBeginReport
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateBeginReport);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateBeginReport, value);
            }
        }
        /// <summary>
        /// Краткое наименование
        /// </summary>
        [Column(Name="SHORT_GROUP_NAME")]
        public String ShortGroupName
        {
            get
            {
                return this.GetDataRowField<String>(() => ShortGroupName);
            }
            set
            {
                UpdateDataRow<String>(() => ShortGroupName, value);
            }
        }
        /// <summary>
        /// Наименование группы для расчетов
        /// </summary>
        [Column(Name="GROUP_NAME")]
        public String GroupName
        {
            get
            {
                return this.GetDataRowField<String>(() => GroupName);
            }
            set
            {
                UpdateDataRow<String>(() => GroupName, value);
            }
        }
        /// <summary>
        /// Код группы для отчетов
        /// </summary>
        [Column(Name="GROUP_CODE")]
        public String GroupCode
        {
            get
            {
                return this.GetDataRowField<String>(() => GroupCode);
            }
            set
            {
                UpdateDataRow<String>(() => GroupCode, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Счет сотрудника
    /// </summary>

    [Table(Name="CLIENT_ACCOUNT"), SchemaName("")]
    public partial class ClientAccount : RowEntityBase
    {
        #region Class Members
        [Column(Name="CLIENT_ACCOUNT_ID")]
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
        [Column(Name="DATE_DOC")]
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
        [Column(Name="CODE_DOC")]
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
        [Column(Name="PER_INSURANCE_NUM")]
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
        [Column(Name="INSURANCE_NUM")]
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
        [Column(Name="TYPE_ACCOUNT_ID", CanBeNull=false)]
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
        [Column(Name="PLF_INDEX")]
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
        [Column(Name="PLF_ADDRESS")]
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
        [Column(Name="PLF_NAME")]
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
        [Column(Name="NUMBER_CARD")]
        public String NumberCard
        {
            get
            {
                return this.GetDataRowField<String>(() => NumberCard);
            }
            set
            {
                UpdateDataRow<String>(() => NumberCard, value);
            }
        }
        /// <summary>
        /// Город бля бла
        /// </summary>
        [Column(Name="GET_CITY")]
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
        [Column(Name="GET_PLACE")]
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
        [Column(Name="PASSPORT_NUMBER")]
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
        [Column(Name="PASSPORT_SERIES")]
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
        [Column(Name="OWNER_MIDDLE_NAME")]
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
        [Column(Name="OWNER_FAMILY")]
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
        [Column(Name="OWNER_NAME")]
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
        [Column(Name="TYPE_BANK_ID")]
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
        [Column(Name="NUMBER_ACCOUNT")]
        public String NumberAccount
        {
            get
            {
                return this.GetDataRowField<String>(() => NumberAccount);
            }
            set
            {
                UpdateDataRow<String>(() => NumberAccount, value);
            }
        }
        /// <summary>
        /// Наименование организации
        /// </summary>
        [Column(Name="COMPANY_NAME")]
        public String CompanyName
        {
            get
            {
                return this.GetDataRowField<String>(() => CompanyName);
            }
            set
            {
                UpdateDataRow<String>(() => CompanyName, value);
            }
        }
        [Column(Name="TRANSFER_ID")]
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
                #endregion
    }


    [Table(Name="TYPE_ACCOUNT"), SchemaName("")]
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


    [Table(Name="BANK_FOR_TYPE_ACCOUNT"), SchemaName("")]
    public partial class BankForTypeAccount : RowEntityBase
    {
        #region Class Members
        [Column(Name="BANK_FOR_TYPE_ACCOUNT_ID")]
        public Decimal? BankForTypeAccountID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => BankForTypeAccountID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => BankForTypeAccountID, value);
            }
        }
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
        [Column(Name="TYPE_BANK_ID")]
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
                #endregion
    }

    /// <summary>
    /// Типы банков
    /// </summary>

    [Table(Name="TYPE_BANK"), SchemaName("")]
    public partial class TypeBank : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный номер банка
        /// </summary>
        [Column(Name="TYPE_BANK_ID")]
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
        /// Адрес головного отделения банка
        /// </summary>
        [Column(Name="BANK_ADDRESS")]
        public String BankAddress
        {
            get
            {
                return this.GetDataRowField<String>(() => BankAddress);
            }
            set
            {
                UpdateDataRow<String>(() => BankAddress, value);
            }
        }
        /// <summary>
        /// Дата заключения договора на услуги
        /// </summary>
        [Column(Name="CONTRACT_DATE")]
        public DateTime? ContractDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => ContractDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => ContractDate, value);
            }
        }
        /// <summary>
        /// Номер или код договора с банком
        /// </summary>
        [Column(Name="CONTRACT_CODE")]
        public String ContractCode
        {
            get
            {
                return this.GetDataRowField<String>(() => ContractCode);
            }
            set
            {
                UpdateDataRow<String>(() => ContractCode, value);
            }
        }
        /// <summary>
        /// Код причины постановки на учет (КПП)
        /// </summary>
        [Column(Name="PPC")]
        public String Ppc
        {
            get
            {
                return this.GetDataRowField<String>(() => Ppc);
            }
            set
            {
                UpdateDataRow<String>(() => Ppc, value);
            }
        }
        /// <summary>
        /// Признак отдельного реестра
        /// </summary>
        [Column(Name="CUSTOM_SIGN")]
        public Decimal? CustomSign
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CustomSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CustomSign, value);
            }
        }
        /// <summary>
        /// БИК
        /// </summary>
        [Column(Name="BANK_IDENT_CODE")]
        public String BankIdentCode
        {
            get
            {
                return this.GetDataRowField<String>(() => BankIdentCode);
            }
            set
            {
                UpdateDataRow<String>(() => BankIdentCode, value);
            }
        }
        /// <summary>
        /// ИНН
        /// </summary>
        [Column(Name="TRN")]
        public String Trn
        {
            get
            {
                return this.GetDataRowField<String>(() => Trn);
            }
            set
            {
                UpdateDataRow<String>(() => Trn, value);
            }
        }
        /// <summary>
        /// Филиал банка
        /// </summary>
        [Column(Name="BRANCH_BANK")]
        public String BranchBank
        {
            get
            {
                return this.GetDataRowField<String>(() => BranchBank);
            }
            set
            {
                UpdateDataRow<String>(() => BranchBank, value);
            }
        }
        /// <summary>
        /// Отделение банка
        /// </summary>
        [Column(Name="BANK_OFFICE")]
        public String BankOffice
        {
            get
            {
                return this.GetDataRowField<String>(() => BankOffice);
            }
            set
            {
                UpdateDataRow<String>(() => BankOffice, value);
            }
        }
        /// <summary>
        /// Корреспондентский счет
        /// </summary>
        [Column(Name="CORRESPONDENT_ACCOUNT")]
        public String CorrespondentAccount
        {
            get
            {
                return this.GetDataRowField<String>(() => CorrespondentAccount);
            }
            set
            {
                UpdateDataRow<String>(() => CorrespondentAccount, value);
            }
        }
        /// <summary>
        /// Наименование банка
        /// </summary>
        [Column(Name="BANK_NAME")]
        public String BankName
        {
            get
            {
                return this.GetDataRowField<String>(() => BankName);
            }
            set
            {
                UpdateDataRow<String>(() => BankName, value);
            }
        }
        /// <summary>
        /// Расчетный счет
        /// </summary>
        [Column(Name="CURRENT_ACCOUNT")]
        public String CurrentAccount
        {
            get
            {
                return this.GetDataRowField<String>(() => CurrentAccount);
            }
            set
            {
                UpdateDataRow<String>(() => CurrentAccount, value);
            }
        }
                #endregion
    }


    [Table(Name="SALARY_ADVANCE"), SchemaName("")]
    public partial class SalaryAdvance : RowEntityBase
    {
        #region Class Members
        [Column(Name="SALARY_ADVANCE_ID")]
        public Decimal? SalaryAdvanceID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryAdvanceID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryAdvanceID, value);
            }
        }
        [Column(Name="TYPE_ROW_SALARY_ID")]
        public Decimal? TypeRowSalaryID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeRowSalaryID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeRowSalaryID, value);
            }
        }
        [Column(Name="RETENTION_ID")]
        public Decimal? RetentionID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RetentionID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentionID, value);
            }
        }
        [Column(Name="SUBDIV_ID")]
        public Decimal? SubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }
        [Column(Name="SIGN_COMB")]
        public Decimal? SignComb
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignComb);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignComb, value);
            }
        }
        [Column(Name="PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        [Column(Name="TIME_ADD_RECORD")]
        public DateTime? TimeAddRecord
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => TimeAddRecord);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => TimeAddRecord, value);
            }
        }
        [Column(Name="ACCOUNT_ADD_SIGN")]
        public Decimal? AccountAddSign
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => AccountAddSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AccountAddSign, value);
            }
        }
        [Column(Name="DAYS")]
        public Decimal? Days
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Days);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Days, value);
            }
        }
        [Column(Name="REF_ID")]
        public Decimal? RefID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RefID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RefID, value);
            }
        }
        [Column(Name="TYPE_REF_SALARY_ID")]
        public Decimal? TypeRefSalaryID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeRefSalaryID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeRefSalaryID, value);
            }
        }
        [Column(Name="TRANSFER_ID")]
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
        [Column(Name="DEGREE_ID")]
        public Decimal? DegreeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DegreeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DegreeID, value);
            }
        }
        [Column(Name="GROUP_MASTER")]
        public String GroupMaster
        {
            get
            {
                return this.GetDataRowField<String>(() => GroupMaster);
            }
            set
            {
                UpdateDataRow<String>(() => GroupMaster, value);
            }
        }
        [Column(Name="ORDER_ID")]
        public Decimal? OrderID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => OrderID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OrderID, value);
            }
        }
        [Column(Name="EXP_ADD")]
        public Decimal? ExpAdd
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ExpAdd);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ExpAdd, value);
            }
        }
        [Column(Name="ZONE_ADD")]
        public Decimal? ZoneAdd
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ZoneAdd);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ZoneAdd, value);
            }
        }
        [Column(Name="SUM_SAL")]
        public Decimal? SumSal
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SumSal);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SumSal, value);
            }
        }
        [Column(Name="HOURS")]
        public Decimal? Hours
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Hours);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Hours, value);
            }
        }
        [Column(Name="PAYMENT_TYPE_ID")]
        public Decimal? PaymentTypeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentTypeID, value);
            }
        }
        [Column(Name="PAY_DATE")]
        public DateTime? PayDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => PayDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => PayDate, value);
            }
        }
                #endregion
    }


    [Table(Name="SALARY_DOCUM"), SchemaName("")]
    public partial class SalaryDocum : RowEntityBase
    {
        #region Class Members
        [Column(Name="SALARY_DOCUM_ID", IsPrimaryKey=true)]
        public Decimal? SalaryDocumID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryDocumID, value);
            }
        }
        /// <summary>
        /// Последнее время расчета
        /// </summary>
        [Column(Name="LAST_CALC_DATE")]
        public DateTime? LastCalcDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => LastCalcDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => LastCalcDate, value);
            }
        }
        /// <summary>
        /// Процент оплаты
        /// </summary>
        [Column(Name="PAYMENT_PERCENT")]
        public Decimal? PaymentPercent
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PaymentPercent);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentPercent, value);
            }
        }
        /// <summary>
        /// Дата начала документа
        /// </summary>
        [Column(Name="DOC_BEGIN")]
        public DateTime? DocBegin
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DocBegin);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DocBegin, value);
            }
        }
        /// <summary>
        /// Дата окончания документа
        /// </summary>
        [Column(Name="DOC_END")]
        public DateTime? DocEnd
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DocEnd);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DocEnd, value);
            }
        }
        /// <summary>
        /// Перевод сотрудника для документа
        /// </summary>
        [Column(Name="TRANSFER_ID")]
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
        /// <summary>
        /// Дата создания документа
        /// </summary>
        [Column(Name="DATE_FORM_DOCUM")]
        public DateTime? DateFormDocum
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateFormDocum);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateFormDocum, value);
            }
        }
        /// <summary>
        /// подразделение документа
        /// </summary>
        [Column(Name="DOC_SUBDIV_ID")]
        public Decimal? DocSubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DocSubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DocSubdivID, value);
            }
        }
        [Column(Name="TYPE_SAL_DOCUM_ID")]
        public Decimal? TypeSalDocumID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeSalDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeSalDocumID, value);
            }
        }
        /// <summary>
        /// Дата проведения документа
        /// </summary>
        [Column(Name="DATE_CLOSE")]
        public DateTime? DateClose
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateClose);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateClose, value);
            }
        }
        /// <summary>
        /// Дата документа
        /// </summary>
        [Column(Name="DATE_DOC")]
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
        [Column(Name="NAME_DOC")]
        public String NameDoc
        {
            get
            {
                return this.GetDataRowField<String>(() => NameDoc);
            }
            set
            {
                UpdateDataRow<String>(() => NameDoc, value);
            }
        }
        /// <summary>
        /// Код документа
        /// </summary>
        [Column(Name="CODE_DOC")]
        public String CodeDoc
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeDoc);
            }
            set
            {
                UpdateDataRow<String>(() => CodeDoc, value);
            }
        }
        /// <summary>
        /// Кол-во дней для ограничения
        /// </summary>
        [Column(Name="COUNT_RESTRICT_DAYS")]
        public Decimal? CountRestrictDays
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CountRestrictDays);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CountRestrictDays, value);
            }
        }
        /// <summary>
        /// Ссылка на документ в табеле
        /// </summary>
        [Column(Name="REG_DOC_ID")]
        public Decimal? RegDocID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RegDocID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RegDocID, value);
            }
        }
        /// <summary>
        /// Признак первичного документа
        /// </summary>
        [Column(Name="BASIC_DOC_SIGN")]
        public Decimal? BasicDocSign
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => BasicDocSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => BasicDocSign, value);
            }
        }
        /// <summary>
        /// Ссылка на документ первичный
        /// </summary>
        [Column(Name="RELATED_DOCUM_ID")]
        public Decimal? RelatedDocumID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RelatedDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RelatedDocumID, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Детали оплаты документа зарплаты
    /// </summary>

    [Table(Name="SALARY_DOCUM_DETAIL"), SchemaName("")]
    public partial class SalaryDocumDetail : RowEntityBase
    {
        #region Class Members
        [Column(Name="SALARY_DOCUM_DETAIL_ID")]
        public Decimal? SalaryDocumDetailID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryDocumDetailID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryDocumDetailID, value);
            }
        }
        [Column(Name="SALARY_DOCUM_ID")]
        public Decimal? SalaryDocumID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryDocumID, value);
            }
        }
        /// <summary>
        /// Сумма оплаты
        /// </summary>
        [Column(Name="PAYMENT_SUM")]
        public Decimal? PaymentSum
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PaymentSum);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentSum, value);
            }
        }
        /// <summary>
        /// Вид оплаты для расчета
        /// </summary>
        [Column(Name="PAYMENT_TYPE_ID")]
        public Decimal? PaymentTypeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentTypeID, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Тип документа зарплаты
    /// </summary>

    [Table(Name="TYPE_SAL_DOCUM"), SchemaName("")]
    public partial class TypeSalDocum : RowEntityBase
    {
        #region Class Members
        [Column(Name="TYPE_SAL_DOCUM_ID")]
        public Decimal? TypeSalDocumID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeSalDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeSalDocumID, value);
            }
        }
        /// <summary>
        /// Кол-во дней, ограничивающих расчет первых шифров оплат
        /// </summary>
        [Column(Name="COUNT_RESTRICT_DAYS")]
        public Decimal? CountRestrictDays
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CountRestrictDays);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CountRestrictDays, value);
            }
        }
        /// <summary>
        /// Требуется ли указывать период документа
        /// </summary>
        [Column(Name="NEED_DOC_PERIOD")]
        public Decimal? NeedDocPeriod
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => NeedDocPeriod);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => NeedDocPeriod, value);
            }
        }
        /// <summary>
        /// Признак отпуска
        /// </summary>
        [Column(Name="VAC_SIGN")]
        public Decimal? VacSign
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => VacSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => VacSign, value);
            }
        }
        [Column(Name="TYPE_SAL_DOC_NAME")]
        public String TypeSalDocName
        {
            get
            {
                return this.GetDataRowField<String>(() => TypeSalDocName);
            }
            set
            {
                UpdateDataRow<String>(() => TypeSalDocName, value);
            }
        }
        [Column(Name="NEED_CALC_PERIOD")]
        public Decimal? NeedCalcPeriod
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => NeedCalcPeriod);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => NeedCalcPeriod, value);
            }
        }
                #endregion
    }


    [Table(Name="SALARY_DOCUM_PERIOD"), SchemaName("")]
    public partial class SalaryDocumPeriod : RowEntityBase
    {
        #region Class Members
        [Column(Name="SALARY_DOCUM_PERIOD_ID")]
        public Decimal? SalaryDocumPeriodID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryDocumPeriodID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryDocumPeriodID, value);
            }
        }
        [Column(Name="SALARY_DOCUM_ID")]
        public Decimal? SalaryDocumID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryDocumID, value);
            }
        }
        [Column(Name="END_PERIOD")]
        public DateTime? EndPeriod
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => EndPeriod);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => EndPeriod, value);
            }
        }
        [Column(Name="BEGIN_PERIOD")]
        public DateTime? BeginPeriod
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => BeginPeriod);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => BeginPeriod, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Основная таблица - зарплата сотрудников
    /// </summary>

    [Table(Name="SALARY"), SchemaName("")]
    public partial class Salary : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный номер ЗП
        /// </summary>
        [Column(Name="SALARY_ID")]
        public Decimal? SalaryID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryID, value);
            }
        }
        /// <summary>
        /// Тип записи в ЗП
        /// </summary>
        [Column(Name="TYPE_ROW_SALARY_ID")]
        public Decimal? TypeRowSalaryID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeRowSalaryID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeRowSalaryID, value);
            }
        }
        /// <summary>
        /// Документ удержания
        /// </summary>
        [Column(Name="RETENTION_ID")]
        public Decimal? RetentionID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RetentionID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentionID, value);
            }
        }
        /// <summary>
        /// Подразделение
        /// </summary>
        [Column(Name="SUBDIV_ID")]
        public Decimal? SubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }
        /// <summary>
        /// Признак совмещения
        /// </summary>
        [Column(Name="SIGN_COMB")]
        public Decimal? SignComb
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignComb);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignComb, value);
            }
        }
        /// <summary>
        /// Табельный номер;
        /// </summary>
        [Column(Name="PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        /// <summary>
        /// Дата добавления или редактирования записи;
        /// </summary>
        [Column(Name="TIME_ADD_RECORD")]
        public DateTime? TimeAddRecord
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => TimeAddRecord);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => TimeAddRecord, value);
            }
        }
        /// <summary>
        /// Признак добавления записи вручную бухгалтером;
        /// </summary>
        [Column(Name="ACCOUNT_ADD_SIGN")]
        public Decimal? AccountAddSign
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => AccountAddSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AccountAddSign, value);
            }
        }
        /// <summary>
        /// Кол-во дней шифра оплат;
        /// </summary>
        [Column(Name="DAYS")]
        public Decimal? Days
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Days);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Days, value);
            }
        }
        /// <summary>
        /// Ссылка на другую таблицу привязанных данных
        /// </summary>
        [Column(Name="REF_ID")]
        public Decimal? RefID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RefID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RefID, value);
            }
        }
        /// <summary>
        /// Тип ссылки на другую таблицу;
        /// </summary>
        [Column(Name="TYPE_REF_SALARY_ID")]
        public Decimal? TypeRefSalaryID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeRefSalaryID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeRefSalaryID, value);
            }
        }
        /// <summary>
        /// Уникальный номер перевода работника, на который была выплачена ЗП;
        /// </summary>
        [Column(Name="TRANSFER_ID")]
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
        /// <summary>
        /// Категория для шифра оплат;
        /// </summary>
        [Column(Name="DEGREE_ID")]
        public Decimal? DegreeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DegreeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DegreeID, value);
            }
        }
        /// <summary>
        /// Уникальный номер группы мастера;
        /// </summary>
        [Column(Name="GROUP_MASTER")]
        public String GroupMaster
        {
            get
            {
                return this.GetDataRowField<String>(() => GroupMaster);
            }
            set
            {
                UpdateDataRow<String>(() => GroupMaster, value);
            }
        }
        /// <summary>
        /// Номер заказа уникальный
        /// </summary>
        [Column(Name="ORDER_ID")]
        public Decimal? OrderID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => OrderID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OrderID, value);
            }
        }
        /// <summary>
        /// Сумма надбавки за стаж;
        /// </summary>
        [Column(Name="EXP_ADD")]
        public Decimal? ExpAdd
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ExpAdd);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ExpAdd, value);
            }
        }
        /// <summary>
        /// Сумма надбавки за поясной коэффициент
        /// </summary>
        [Column(Name="ZONE_ADD")]
        public Decimal? ZoneAdd
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ZoneAdd);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ZoneAdd, value);
            }
        }
        /// <summary>
        /// Сумма
        /// </summary>
        [Column(Name="SUM_SAL")]
        public Decimal? SumSal
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SumSal);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SumSal, value);
            }
        }
        /// <summary>
        /// Часы
        /// </summary>
        [Column(Name="HOURS")]
        public Decimal? Hours
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Hours);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Hours, value);
            }
        }
        /// <summary>
        /// Шифр оплат;
        /// </summary>
        [Column(Name="PAYMENT_TYPE_ID")]
        public Decimal? PaymentTypeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentTypeID, value);
            }
        }
        /// <summary>
        /// Дата оплаты
        /// </summary>
        [Column(Name="PAY_DATE")]
        public DateTime? PayDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => PayDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => PayDate, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Таблица типов документов табеля
    /// </summary>

    [Table(Name="DOC_LIST"), SchemaName("")]
    public partial class DocList : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный идентификатор документа;
        /// </summary>
        [Column(Name="DOC_LIST_ID")]
        public Decimal? DocListID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DocListID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DocListID, value);
            }
        }
        /// <summary>
        /// Дата окончания действия типа документа;
        /// </summary>
        [Column(Name="DOC_END_VALID")]
        public DateTime? DocEndValid
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DocEndValid);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DocEndValid, value);
            }
        }
        /// <summary>
        /// Дата начала действия типа документа;
        /// </summary>
        [Column(Name="DOC_BEGIN_VALID")]
        public DateTime? DocBeginValid
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DocBeginValid);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DocBeginValid, value);
            }
        }
        /// <summary>
        /// Признак целого дня; (Если признак = 1, то в этот день не может быть больше видов оплат кроме данного)
        /// </summary>
        [Column(Name="SIGN_ALL_DAY")]
        public Decimal? SignAllDay
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignAllDay);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignAllDay, value);
            }
        }
        /// <summary>
        /// Признак добавления дня отгула;
        /// </summary>
        [Column(Name="ADD_HOLIDAY")]
        public Decimal? AddHoliday
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => AddHoliday);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AddHoliday, value);
            }
        }
        /// <summary>
        /// Признак расчета;
        /// </summary>
        [Column(Name="ISCALC")]
        public Decimal? Iscalc
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Iscalc);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Iscalc, value);
            }
        }
        /// <summary>
        /// Вид оплат;
        /// </summary>
        [Column(Name="PAY_TYPE_ID")]
        public Decimal? PayTypeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PayTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PayTypeID, value);
            }
        }
        /// <summary>
        /// Тип документов (оправдательный документ или сверхурочный);
        /// </summary>
        [Column(Name="DOC_TYPE")]
        public Decimal? DocType
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DocType);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DocType, value);
            }
        }
        /// <summary>
        /// Обозначение документа;
        /// </summary>
        [Column(Name="DOC_NOTE")]
        public String DocNote
        {
            get
            {
                return this.GetDataRowField<String>(() => DocNote);
            }
            set
            {
                UpdateDataRow<String>(() => DocNote, value);
            }
        }
        /// <summary>
        /// Наименование документа;
        /// </summary>
        [Column(Name="DOC_NAME")]
        public String DocName
        {
            get
            {
                return this.GetDataRowField<String>(() => DocName);
            }
            set
            {
                UpdateDataRow<String>(() => DocName, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Таблица документов в табеле
    /// </summary>

    [Table(Name="REG_DOC"), SchemaName("")]
    public partial class RegDoc : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный идентификатор регистрационного документа;
        /// </summary>
        [Column(Name="REG_DOC_ID")]
        public Decimal? RegDocID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RegDocID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RegDocID, value);
            }
        }
        /// <summary>
        /// Местонахождение;
        /// </summary>
        [Column(Name="DOC_LOCATION")]
        public String DocLocation
        {
            get
            {
                return this.GetDataRowField<String>(() => DocLocation);
            }
            set
            {
                UpdateDataRow<String>(() => DocLocation, value);
            }
        }
        [Column(Name="TRANSFER_ID")]
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
        [Column(Name="ABSENCE_ID")]
        public Decimal? AbsenceID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => AbsenceID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AbsenceID, value);
            }
        }
        /// <summary>
        /// Дата окончания; Дата окончания действия документа;
        /// </summary>
        [Column(Name="DOC_END")]
        public DateTime? DocEnd
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DocEnd);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DocEnd, value);
            }
        }
        /// <summary>
        /// Дата начала; Дата начала действия документа;
        /// </summary>
        [Column(Name="DOC_BEGIN")]
        public DateTime? DocBegin
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DocBegin);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DocBegin, value);
            }
        }
        /// <summary>
        /// Номер документа;
        /// </summary>
        [Column(Name="DOC_NUMBER")]
        public String DocNumber
        {
            get
            {
                return this.GetDataRowField<String>(() => DocNumber);
            }
            set
            {
                UpdateDataRow<String>(() => DocNumber, value);
            }
        }
        /// <summary>
        /// Дата документа;
        /// </summary>
        [Column(Name="DOC_DATE")]
        public DateTime? DocDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DocDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DocDate, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор документа;
        /// </summary>
        [Column(Name="DOC_LIST_ID")]
        public Decimal? DocListID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DocListID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DocListID, value);
            }
        }
        /// <summary>
        /// Табельный номер;
        /// </summary>
        [Column(Name="PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Таблица изменений процентов расчета
    /// </summary>

    [Table(Name="SALARY_DOCUM_PAY_CHANGE"), SchemaName("")]
    public partial class SalaryDocumPayChange : RowEntityBase
    {
        #region Class Members
        [Column(Name="SALARY_DOCUM_PAY_CHANGE_ID")]
        public Decimal? SalaryDocumPayChangeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryDocumPayChangeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryDocumPayChangeID, value);
            }
        }
        [Column(Name="SALARY_DOCUM_ID")]
        public Decimal? SalaryDocumID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryDocumID, value);
            }
        }
        [Column(Name="BY_CODE_DOC_SIGN")]
        public Decimal? ByCodeDocSign
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ByCodeDocSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ByCodeDocSign, value);
            }
        }
        /// <summary>
        /// Процент изменения расчета
        /// </summary>
        [Column(Name="PAY_VALUE")]
        public Decimal? PayValue
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PayValue);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PayValue, value);
            }
        }
        /// <summary>
        /// День, начиная с которого начинается изменения расчета
        /// </summary>
        [Column(Name="COUNT_DAYS")]
        public Decimal? CountDays
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CountDays);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CountDays, value);
            }
        }
                #endregion
    }


    [Table(Name="LOAN"), SchemaName("")]
    public partial class Loan : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный идентификатор ссуды
        /// </summary>
        [Column(Name="LOAN_ID")]
        public Decimal? LoanID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => LoanID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => LoanID, value);
            }
        }
        /// <summary>
        /// Ключ счета сотрудника
        /// </summary>
        [Column(Name="CLIENT_ACCOUNT_ID")]
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
        /// Дата окончания действия ссуды (перевод в архив)
        /// </summary>
        [Column(Name="LOAN_DATE_END")]
        public DateTime? LoanDateEnd
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => LoanDateEnd);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => LoanDateEnd, value);
            }
        }
        /// <summary>
        /// Идентификатор родительской ссуды;
        /// </summary>
        [Column(Name="PARENT_LOAN_ID")]
        public Decimal? ParentLoanID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ParentLoanID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ParentLoanID, value);
            }
        }
        /// <summary>
        /// Признак утверждения договора
        /// </summary>
        [Column(Name="SIGN_REGISTRATION_DOG")]
        public Decimal? SignRegistrationDog
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignRegistrationDog);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignRegistrationDog, value);
            }
        }
        /// <summary>
        /// Признак архивной записи
        /// </summary>
        [Column(Name="SIGN_ARCHIVE")]
        public Decimal? SignArchive
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignArchive);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignArchive, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор типа ссуды
        /// </summary>
        [Column(Name="TYPE_LOAN_ID")]
        public Decimal? TypeLoanID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeLoanID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeLoanID, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор цели ссуды
        /// </summary>
        [Column(Name="PURPOSE_LOAN_ID")]
        public Decimal? PurposeLoanID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PurposeLoanID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PurposeLoanID, value);
            }
        }
        /// <summary>
        /// Признак формирования матвыгоды
        /// </summary>
        [Column(Name="SIGN_MATERIAL_BENEFIT")]
        public Decimal? SignMaterialBenefit
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignMaterialBenefit);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignMaterialBenefit, value);
            }
        }
        /// <summary>
        /// Признак формирования удержания
        /// </summary>
        [Column(Name="SIGN_RETENTION")]
        public Decimal? SignRetention
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignRetention);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignRetention, value);
            }
        }
        /// <summary>
        /// Сумма удержаний по факту
        /// </summary>
        [Column(Name="RETENTION_BY_FACT")]
        public Decimal? RetentionByFact
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RetentionByFact);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentionByFact, value);
            }
        }
        /// <summary>
        /// Сумма удержаний по договору
        /// </summary>
        [Column(Name="RETENTION_BY_CONTRACT")]
        public Decimal? RetentionByContract
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RetentionByContract);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentionByContract, value);
            }
        }
        /// <summary>
        /// Порядковый номер ссуды
        /// </summary>
        [Column(Name="ORDINAL_NUMBER")]
        public Decimal? OrdinalNumber
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => OrdinalNumber);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OrdinalNumber, value);
            }
        }
        /// <summary>
        /// Срок ссуды
        /// </summary>
        [Column(Name="LOAN_TERM")]
        public Decimal? LoanTerm
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => LoanTerm);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => LoanTerm, value);
            }
        }
        /// <summary>
        /// Сумма ссуды
        /// </summary>
        [Column(Name="LOAN_SUM")]
        public Decimal? LoanSum
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => LoanSum);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => LoanSum, value);
            }
        }
        /// <summary>
        /// Дата ссуды
        /// </summary>
        [Column(Name="LOAN_DATE")]
        public DateTime? LoanDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => LoanDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => LoanDate, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор перевода сотрудника
        /// </summary>
        [Column(Name="TRANSFER_ID")]
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
        /// <summary>
        /// Дата договора ссуды
        /// </summary>
        [Column(Name="CONTRACT_DATE")]
        public DateTime? ContractDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => ContractDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => ContractDate, value);
            }
        }
        /// <summary>
        /// Номер договора ссуды
        /// </summary>
        [Column(Name="CONTRACT_NUMBER")]
        public String ContractNumber
        {
            get
            {
                return this.GetDataRowField<String>(() => ContractNumber);
            }
            set
            {
                UpdateDataRow<String>(() => ContractNumber, value);
            }
        }
        /// <summary>
        /// Дата протокола
        /// </summary>
        [Column(Name="PROTOCOL_DATE")]
        public DateTime? ProtocolDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => ProtocolDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => ProtocolDate, value);
            }
        }
        /// <summary>
        /// Номер протокола
        /// </summary>
        [Column(Name="PROTOCOL_NUMBER")]
        public String ProtocolNumber
        {
            get
            {
                return this.GetDataRowField<String>(() => ProtocolNumber);
            }
            set
            {
                UpdateDataRow<String>(() => ProtocolNumber, value);
            }
        }
                #endregion
    }


    [Table(Name="ADDRESS_NONE_KLADR"), SchemaName("")]
    public partial class AddressNoneKladr : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Табельный номер;
        /// </summary>
        [Column(Name="PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        /// <summary>
        /// Улица; Наименование улицы;
        /// </summary>
        [Column(Name="NAME_STREET")]
        public String NameStreet
        {
            get
            {
                return this.GetDataRowField<String>(() => NameStreet);
            }
            set
            {
                UpdateDataRow<String>(() => NameStreet, value);
            }
        }
        /// <summary>
        /// Населенный пункт; Наименование населенного пункта;
        /// </summary>
        [Column(Name="NAME_LOCALITY")]
        public String NameLocality
        {
            get
            {
                return this.GetDataRowField<String>(() => NameLocality);
            }
            set
            {
                UpdateDataRow<String>(() => NameLocality, value);
            }
        }
        /// <summary>
        /// Наименование города;
        /// </summary>
        [Column(Name="NAME_CITY")]
        public String NameCity
        {
            get
            {
                return this.GetDataRowField<String>(() => NameCity);
            }
            set
            {
                UpdateDataRow<String>(() => NameCity, value);
            }
        }
        /// <summary>
        /// Район; Наименование района;
        /// </summary>
        [Column(Name="NAME_DISTRICT")]
        public String NameDistrict
        {
            get
            {
                return this.GetDataRowField<String>(() => NameDistrict);
            }
            set
            {
                UpdateDataRow<String>(() => NameDistrict, value);
            }
        }
        /// <summary>
        /// Регион; Наименование региона;
        /// </summary>
        [Column(Name="NAME_REGION")]
        public String NameRegion
        {
            get
            {
                return this.GetDataRowField<String>(() => NameRegion);
            }
            set
            {
                UpdateDataRow<String>(() => NameRegion, value);
            }
        }
                #endregion
    }


    [Table(Name="EMP_ALL_DATA"), SchemaName("")]
    public partial class EmpAllData : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Перевод сотрудника
        /// </summary>
        [Column(Name="TRANSFER_ID")]
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
        /// <summary>
        /// ФАМИЛИЯ
        /// </summary>
        [Column(Name="EMP_LAST_NAME")]
        public String EmpLastName
        {
            get
            {
                return this.GetDataRowField<String>(() => EmpLastName);
            }
            set
            {
                UpdateDataRow<String>(() => EmpLastName, value);
            }
        }
        /// <summary>
        /// Имя
        /// </summary>
        [Column(Name="EMP_FIRST_NAME")]
        public String EmpFirstName
        {
            get
            {
                return this.GetDataRowField<String>(() => EmpFirstName);
            }
            set
            {
                UpdateDataRow<String>(() => EmpFirstName, value);
            }
        }
        /// <summary>
        /// Отчество
        /// </summary>
        [Column(Name="EMP_MIDDLE_NAME")]
        public String EmpMiddleName
        {
            get
            {
                return this.GetDataRowField<String>(() => EmpMiddleName);
            }
            set
            {
                UpdateDataRow<String>(() => EmpMiddleName, value);
            }
        }
        /// <summary>
        /// Подразделение
        /// </summary>
        [Column(Name="CODE_SUBDIV")]
        public String CodeSubdiv
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeSubdiv);
            }
            set
            {
                UpdateDataRow<String>(() => CodeSubdiv, value);
            }
        }
        /// <summary>
        /// Должность
        /// </summary>
        [Column(Name="POS_NAME")]
        public String PosName
        {
            get
            {
                return this.GetDataRowField<String>(() => PosName);
            }
            set
            {
                UpdateDataRow<String>(() => PosName, value);
            }
        }
        /// <summary>
        /// Таб.№
        /// </summary>
        [Column(Name="PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        /// <summary>
        /// Пол
        /// </summary>
        [Column(Name="EMP_SEX")]
        public String EmpSex
        {
            get
            {
                return this.GetDataRowField<String>(() => EmpSex);
            }
            set
            {
                UpdateDataRow<String>(() => EmpSex, value);
            }
        }
        /// <summary>
        /// Дата рождения
        /// </summary>
        [Column(Name="EMP_BIRTH_DATE")]
        public DateTime? EmpBirthDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => EmpBirthDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => EmpBirthDate, value);
            }
        }
        /// <summary>
        /// Категория
        /// </summary>
        [Column(Name="CODE_DEGREE")]
        public String CodeDegree
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeDegree);
            }
            set
            {
                UpdateDataRow<String>(() => CodeDegree, value);
            }
        }
        /// <summary>
        /// Уникальный номер
        /// </summary>
        [Column(Name="ID")]
        public Decimal? ID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ID, value);
            }
        }
        /// <summary>
        /// Совмещение
        /// </summary>
        [Column(Name="SIGN_COMB")]
        public Int16? SignComb
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => SignComb);
            }
            set
            {
                UpdateDataRow<Int16?>(() => SignComb, value);
            }
        }
        /// <summary>
        /// Фото
        /// </summary>
        [Column(Name="PHOTO")]
        public Byte[] Photo
        {
            get
            {
                return this.GetDataRowField<Byte[]>(() => Photo);
            }
            set
            {
                UpdateDataRow<Byte[]>(() => Photo, value);
            }
        }
        /// <summary>
        /// Дата перевода
        /// </summary>
        [Column(Name="DATE_TRANSFER")]
        public DateTime? DateTransfer
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateTransfer);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateTransfer, value);
            }
        }
        /// <summary>
        /// Окончание перевода
        /// </summary>
        [Column(Name="END_TRANSFER")]
        public DateTime? EndTransfer
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => EndTransfer);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => EndTransfer, value);
            }
        }
        /// <summary>
        /// Хбз че это
        /// </summary>
        [Column(Name="CHAR_TRANSFER_ID")]
        public Decimal? CharTransferID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CharTransferID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CharTransferID, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Общие и бухгалтерские данные по сотруднику
    /// </summary>

    [Table(Name="EMP_ACCOUNT_DATA"), SchemaName("")]
    public partial class EmpAccountData : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Перевод сотрудника
        /// </summary>
        [Column(Name="TRANSFER_ID")]
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
        /// <summary>
        /// ФАМИЛИЯ
        /// </summary>
        [Column(Name="EMP_LAST_NAME")]
        public String EmpLastName
        {
            get
            {
                return this.GetDataRowField<String>(() => EmpLastName);
            }
            set
            {
                UpdateDataRow<String>(() => EmpLastName, value);
            }
        }
        /// <summary>
        /// Имя
        /// </summary>
        [Column(Name="EMP_FIRST_NAME")]
        public String EmpFirstName
        {
            get
            {
                return this.GetDataRowField<String>(() => EmpFirstName);
            }
            set
            {
                UpdateDataRow<String>(() => EmpFirstName, value);
            }
        }
        /// <summary>
        /// Отчество
        /// </summary>
        [Column(Name="EMP_MIDDLE_NAME")]
        public String EmpMiddleName
        {
            get
            {
                return this.GetDataRowField<String>(() => EmpMiddleName);
            }
            set
            {
                UpdateDataRow<String>(() => EmpMiddleName, value);
            }
        }
        /// <summary>
        /// Подразделение
        /// </summary>
        [Column(Name="CODE_SUBDIV")]
        public String CodeSubdiv
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeSubdiv);
            }
            set
            {
                UpdateDataRow<String>(() => CodeSubdiv, value);
            }
        }
        /// <summary>
        /// Должность
        /// </summary>
        [Column(Name="POS_NAME")]
        public String PosName
        {
            get
            {
                return this.GetDataRowField<String>(() => PosName);
            }
            set
            {
                UpdateDataRow<String>(() => PosName, value);
            }
        }
        /// <summary>
        /// Таб.№
        /// </summary>
        [Column(Name="PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        /// <summary>
        /// Пол
        /// </summary>
        [Column(Name="EMP_SEX")]
        public String EmpSex
        {
            get
            {
                return this.GetDataRowField<String>(() => EmpSex);
            }
            set
            {
                UpdateDataRow<String>(() => EmpSex, value);
            }
        }
        /// <summary>
        /// Дата рождения
        /// </summary>
        [Column(Name="EMP_BIRTH_DATE")]
        public DateTime? EmpBirthDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => EmpBirthDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => EmpBirthDate, value);
            }
        }
        /// <summary>
        /// Категория
        /// </summary>
        [Column(Name="CODE_DEGREE")]
        public String CodeDegree
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeDegree);
            }
            set
            {
                UpdateDataRow<String>(() => CodeDegree, value);
            }
        }
        /// <summary>
        /// Уникальный номер
        /// </summary>
        [Column(Name="ID")]
        public Decimal? ID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ID, value);
            }
        }
        /// <summary>
        /// Совмещение
        /// </summary>
        [Column(Name="SIGN_COMB")]
        public Int16? SignComb
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => SignComb);
            }
            set
            {
                UpdateDataRow<Int16?>(() => SignComb, value);
            }
        }
        /// <summary>
        /// Фото
        /// </summary>
        [Column(Name="PHOTO")]
        public Byte[] Photo
        {
            get
            {
                return this.GetDataRowField<Byte[]>(() => Photo);
            }
            set
            {
                UpdateDataRow<Byte[]>(() => Photo, value);
            }
        }
        /// <summary>
        /// Дата перевода
        /// </summary>
        [Column(Name="DATE_TRANSFER")]
        public DateTime? DateTransfer
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateTransfer);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateTransfer, value);
            }
        }
        /// <summary>
        /// Окончание перевода
        /// </summary>
        [Column(Name="END_TRANSFER")]
        public DateTime? EndTransfer
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => EndTransfer);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => EndTransfer, value);
            }
        }
        /// <summary>
        /// Айди тарифной сетки
        /// </summary>
        [Column(Name="TARIFF_GRID_ID")]
        public Decimal? TariffGridID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TariffGridID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TariffGridID, value);
            }
        }
        /// <summary>
        /// Код тарифной сетки
        /// </summary>
        [Column(Name="CODE_TARIFF_GRID")]
        public String CodeTariffGrid
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeTariffGrid);
            }
            set
            {
                UpdateDataRow<String>(() => CodeTariffGrid, value);
            }
        }
        /// <summary>
        /// Стоимость часа тарифной сетки
        /// </summary>
        [Column(Name="TAR_HOUR")]
        public Decimal? TarHour
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TarHour);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TarHour, value);
            }
        }
        /// <summary>
        /// Разряд работника
        /// </summary>
        [Column(Name="CLASSIFIC")]
        public Decimal? Classific
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Classific);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Classific, value);
            }
        }
        /// <summary>
        /// Тарифный коэффициент работника
        /// </summary>
        [Column(Name="SALARY")]
        public Decimal? Salary
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Salary);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Salary, value);
            }
        }
                #endregion
    }


    [Table(Name="TARIFF_GRID"), SchemaName("APSTAFF")]
    public partial class TariffGrid : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный идентификатор тарифной сетки;
        /// </summary>
        [Column(Name="TARIFF_GRID_ID", CanBeNull=false)]
        public Decimal? TariffGridID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TariffGridID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TariffGridID, value);
            }
        }
        /// <summary>
        /// Шифр тарифной сетки;
        /// </summary>
        [Column(Name="CODE_TARIFF_GRID")]
        public String CodeTariffGrid
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeTariffGrid);
            }
            set
            {
                UpdateDataRow<String>(() => CodeTariffGrid, value);
            }
        }
        /// <summary>
        /// Наименование; Наименование тарифной сетки;
        /// </summary>
        [Column(Name="TARIFF_GRID_NAME")]
        public String TariffGridName
        {
            get
            {
                return this.GetDataRowField<String>(() => TariffGridName);
            }
            set
            {
                UpdateDataRow<String>(() => TariffGridName, value);
            }
        }
        [Column(Name="TYPE_TARIFF_GRID_ID", CanBeNull=false)]
        public Decimal? TypeTariffGridID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeTariffGridID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeTariffGridID, value);
            }
        }
        /// <summary>
        /// Количество часов; Количество часов (7-8 часовой день);
        /// </summary>
        [Column(Name="TAR_COUNT_HOUR")]
        public String TarCountHour
        {
            get
            {
                return this.GetDataRowField<String>(() => TarCountHour);
            }
            set
            {
                UpdateDataRow<String>(() => TarCountHour, value);
            }
        }
        /// <summary>
        /// Процент надбавки;
        /// </summary>
        [Column(Name="TAR_PERCENT")]
        public Int16? TarPercent
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => TarPercent);
            }
            set
            {
                UpdateDataRow<Int16?>(() => TarPercent, value);
            }
        }
        /// <summary>
        /// Примечание; Примечание к тарифной сетке;
        /// </summary>
        [Column(Name="TAR_NOTE")]
        public String TarNote
        {
            get
            {
                return this.GetDataRowField<String>(() => TarNote);
            }
            set
            {
                UpdateDataRow<String>(() => TarNote, value);
            }
        }
        /// <summary>
        /// Группа тарифной сетки; Группа тарифной сетки (пример: тарифные сетки: 1,1б,1а, группа - 1 );
        /// </summary>
        [Column(Name="TAR_GROUP")]
        public Decimal? TarGroup
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TarGroup);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TarGroup, value);
            }
        }
        /// <summary>
        /// Надбавка;
        /// </summary>
        [Column(Name="TAR_ADDITION")]
        public Decimal? TarAddition
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TarAddition);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TarAddition, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Общие данные по счету сотрудника
    /// </summary>

    [Table(Name="CLIENT_ACCOUNT_DATA"), SchemaName("")]
    public partial class ClientAccountData : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный номер счета
        /// </summary>
        [Column(Name="CLIENT_ACCOUNT_ID")]
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
        /// Номер счета
        /// </summary>
        [Column(Name="NUMBER_ACCOUNT")]
        public String NumberAccount
        {
            get
            {
                return this.GetDataRowField<String>(() => NumberAccount);
            }
            set
            {
                UpdateDataRow<String>(() => NumberAccount, value);
            }
        }
        /// <summary>
        /// Номер карты
        /// </summary>
        [Column(Name="NUMBER_CARD")]
        public String NumberCard
        {
            get
            {
                return this.GetDataRowField<String>(() => NumberCard);
            }
            set
            {
                UpdateDataRow<String>(() => NumberCard, value);
            }
        }
        /// <summary>
        /// Уникальный тип счета
        /// </summary>
        [Column(Name="TYPE_ACCONT_ID")]
        public Decimal? TypeAccontID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeAccontID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeAccontID, value);
            }
        }
        /// <summary>
        /// Уникальный тип банка
        /// </summary>
        [Column(Name="TYPE_BANK_ID")]
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
        /// Наименование банка
        /// </summary>
        [Column(Name="BANK_NAME")]
        public String BankName
        {
            get
            {
                return this.GetDataRowField<String>(() => BankName);
            }
            set
            {
                UpdateDataRow<String>(() => BankName, value);
            }
        }
        /// <summary>
        /// Таб.№ сотрудника
        /// </summary>
        [Column(Name="PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        /// <summary>
        /// Порядковый номер
        /// </summary>
        [Column(Name="ORDER_NUMBER")]
        public Decimal? OrderNumber
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => OrderNumber);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OrderNumber, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Паспортные данные сотрудника
    /// </summary>

    [Table(Name="PASSPORT"), SchemaName("APSTAFF")]
    public partial class Passport : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Табельный номер;
        /// </summary>
        [Column(Name="PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        /// <summary>
        /// Номер; Номер паспорта сотрудника;
        /// </summary>
        [Column(Name="NUM_PASSPORT")]
        public String NumPassport
        {
            get
            {
                return this.GetDataRowField<String>(() => NumPassport);
            }
            set
            {
                UpdateDataRow<String>(() => NumPassport, value);
            }
        }
        /// <summary>
        /// Серия; Серия паспорта сотрудника;
        /// </summary>
        [Column(Name="SERIA_PASSPORT")]
        public String SeriaPassport
        {
            get
            {
                return this.GetDataRowField<String>(() => SeriaPassport);
            }
            set
            {
                UpdateDataRow<String>(() => SeriaPassport, value);
            }
        }
        /// <summary>
        /// Кем выдан; Кем выдан паспорт сотруднику;
        /// </summary>
        [Column(Name="WHO_GIVEN")]
        public String WhoGiven
        {
            get
            {
                return this.GetDataRowField<String>(() => WhoGiven);
            }
            set
            {
                UpdateDataRow<String>(() => WhoGiven, value);
            }
        }
        /// <summary>
        /// Когда выдан; Дата выдачи паспорта сотруднику;
        /// </summary>
        [Column(Name="WHEN_GIVEN")]
        public DateTime? WhenGiven
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => WhenGiven);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => WhenGiven, value);
            }
        }
        /// <summary>
        /// Гражданство; Гражданство сотрудника;
        /// </summary>
        [Column(Name="CITIZENSHIP")]
        public String Citizenship
        {
            get
            {
                return this.GetDataRowField<String>(() => Citizenship);
            }
            set
            {
                UpdateDataRow<String>(() => Citizenship, value);
            }
        }
        /// <summary>
        /// Страна; Страна рождения сотрудника;
        /// </summary>
        [Column(Name="COUNTRY_BIRTH")]
        public String CountryBirth
        {
            get
            {
                return this.GetDataRowField<String>(() => CountryBirth);
            }
            set
            {
                UpdateDataRow<String>(() => CountryBirth, value);
            }
        }
        /// <summary>
        /// Город; Город рождения сотрудника;
        /// </summary>
        [Column(Name="CITY_BIRTH")]
        public String CityBirth
        {
            get
            {
                return this.GetDataRowField<String>(() => CityBirth);
            }
            set
            {
                UpdateDataRow<String>(() => CityBirth, value);
            }
        }
        /// <summary>
        /// Регион; Регион рождения сотрудника;
        /// </summary>
        [Column(Name="REGION_BIRTH")]
        public String RegionBirth
        {
            get
            {
                return this.GetDataRowField<String>(() => RegionBirth);
            }
            set
            {
                UpdateDataRow<String>(() => RegionBirth, value);
            }
        }
        /// <summary>
        /// Район; Район рождения сотрудника;
        /// </summary>
        [Column(Name="DISTR_BIRTH")]
        public String DistrBirth
        {
            get
            {
                return this.GetDataRowField<String>(() => DistrBirth);
            }
            set
            {
                UpdateDataRow<String>(() => DistrBirth, value);
            }
        }
        /// <summary>
        /// Населенный пункт; Населенный пункт рождения сотрудника;
        /// </summary>
        [Column(Name="LOCALITY_BIRTH")]
        public String LocalityBirth
        {
            get
            {
                return this.GetDataRowField<String>(() => LocalityBirth);
            }
            set
            {
                UpdateDataRow<String>(() => LocalityBirth, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор состояния в браке;
        /// </summary>
        [Column(Name="MAR_STATE_ID")]
        public Decimal? MarStateID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => MarStateID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => MarStateID, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор типа личного документа;
        /// </summary>
        [Column(Name="TYPE_PER_DOC_ID")]
        public Decimal? TypePerDocID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypePerDocID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypePerDocID, value);
            }
        }
                #endregion
    }


    [Table(Name="ACCOUNT_DATA"), SchemaName("")]
    public partial class AccountData : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный идентификатор бухгалтерских данных;
        /// </summary>
        [Column(Name="ACCOUNT_DATA_ID")]
        public Decimal? AccountDataID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => AccountDataID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AccountDataID, value);
            }
        }
        /// <summary>
        /// Надбавка за вредность дополнительная;
        /// </summary>
        [Column(Name="HARMFUL_ADDITION_ADD")]
        public Double? HarmfulAdditionAdd
        {
            get
            {
                return this.GetDataRowField<Double?>(() => HarmfulAdditionAdd);
            }
            set
            {
                UpdateDataRow<Double?>(() => HarmfulAdditionAdd, value);
            }
        }
        /// <summary>
        /// Надбавка за работу с шифрами;
        /// </summary>
        [Column(Name="ENCODING_ADDITION")]
        public Single? EncodingAddition
        {
            get
            {
                return this.GetDataRowField<Single?>(() => EncodingAddition);
            }
            set
            {
                UpdateDataRow<Single?>(() => EncodingAddition, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор группы расчета премии;
        /// </summary>
        [Column(Name="PREMIUM_CALC_GROUP_ID")]
        public Decimal? PremiumCalcGroupID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PremiumCalcGroupID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PremiumCalcGroupID, value);
            }
        }
        /// <summary>
        /// Надбавка за стаж работы по защите гос.тайны;
        /// </summary>
        [Column(Name="GOVSECRET_ADDITION")]
        public Double? GovsecretAddition
        {
            get
            {
                return this.GetDataRowField<Double?>(() => GovsecretAddition);
            }
            set
            {
                UpdateDataRow<Double?>(() => GovsecretAddition, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор льготной профессии;
        /// </summary>
        [Column(Name="PRIVILEGED_POSITION_ID")]
        public Decimal? PrivilegedPositionID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PrivilegedPositionID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PrivilegedPositionID, value);
            }
        }
        /// <summary>
        /// Признак гидропроцедур;
        /// </summary>
        [Column(Name="WATER_PROC")]
        public Int16? WaterProc
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => WaterProc);
            }
            set
            {
                UpdateDataRow<Int16?>(() => WaterProc, value);
            }
        }
        /// <summary>
        /// Оклад в командировке; Оклад работника на период командировки;
        /// </summary>
        [Column(Name="SALARY_MISSION")]
        public Decimal? SalaryMission
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryMission);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryMission, value);
            }
        }
        /// <summary>
        /// Признак начисления за выслугу лет;
        /// </summary>
        [Column(Name="SERVICE_ADD")]
        public Int16? ServiceAdd
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => ServiceAdd);
            }
            set
            {
                UpdateDataRow<Int16?>(() => ServiceAdd, value);
            }
        }
        /// <summary>
        /// Надбавка за разъездной характер работы;
        /// </summary>
        [Column(Name="TRIP_ADDITION")]
        public Single? TripAddition
        {
            get
            {
                return this.GetDataRowField<Single?>(() => TripAddition);
            }
            set
            {
                UpdateDataRow<Single?>(() => TripAddition, value);
            }
        }
        [Column(Name="COUNT_DEP20")]
        public Int16? CountDep20
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => CountDep20);
            }
            set
            {
                UpdateDataRow<Int16?>(() => CountDep20, value);
            }
        }
        [Column(Name="COUNT_DEP19")]
        public Int16? CountDep19
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => CountDep19);
            }
            set
            {
                UpdateDataRow<Int16?>(() => CountDep19, value);
            }
        }
        [Column(Name="COUNT_DEP16")]
        public Int16? CountDep16
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => CountDep16);
            }
            set
            {
                UpdateDataRow<Int16?>(() => CountDep16, value);
            }
        }
        [Column(Name="COUNT_DEP15")]
        public Int16? CountDep15
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => CountDep15);
            }
            set
            {
                UpdateDataRow<Int16?>(() => CountDep15, value);
            }
        }
        /// <summary>
        /// Дата доп. соглашения;
        /// </summary>
        [Column(Name="DATE_ADD_AGREE")]
        public DateTime? DateAddAgree
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateAddAgree);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateAddAgree, value);
            }
        }
        /// <summary>
        /// Дата внесения изменений;
        /// </summary>
        [Column(Name="CHANGE_DATE")]
        public DateTime? ChangeDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => ChangeDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => ChangeDate, value);
            }
        }
        /// <summary>
        /// Дата окончания выплат молодому специалисту;
        /// </summary>
        [Column(Name="DATE_END_YOUNG_SPEC")]
        public DateTime? DateEndYoungSpec
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateEndYoungSpec);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateEndYoungSpec, value);
            }
        }
        /// <summary>
        /// Сумма выплат молодому специалисту;
        /// </summary>
        [Column(Name="SUM_YOUNG_SPEC")]
        public Double? SumYoungSpec
        {
            get
            {
                return this.GetDataRowField<Double?>(() => SumYoungSpec);
            }
            set
            {
                UpdateDataRow<Double?>(() => SumYoungSpec, value);
            }
        }
        /// <summary>
        /// Надбавка за бригадирство;
        /// </summary>
        [Column(Name="CHIEF_ADDITION")]
        public Double? ChiefAddition
        {
            get
            {
                return this.GetDataRowField<Double?>(() => ChiefAddition);
            }
            set
            {
                UpdateDataRow<Double?>(() => ChiefAddition, value);
            }
        }
        /// <summary>
        /// Надбавка за классность;
        /// </summary>
        [Column(Name="CLASS_ADDITION")]
        public Double? ClassAddition
        {
            get
            {
                return this.GetDataRowField<Double?>(() => ClassAddition);
            }
            set
            {
                UpdateDataRow<Double?>(() => ClassAddition, value);
            }
        }
        /// <summary>
        /// Надбавка за секретность;
        /// </summary>
        [Column(Name="SECRET_ADDITION")]
        public Double? SecretAddition
        {
            get
            {
                return this.GetDataRowField<Double?>(() => SecretAddition);
            }
            set
            {
                UpdateDataRow<Double?>(() => SecretAddition, value);
            }
        }
        /// <summary>
        /// Количество иждивенцев (скидка 112-4000 рублей на ребенка);
        /// </summary>
        [Column(Name="COUNT_DEP21")]
        public Int16? CountDep21
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => CountDep21);
            }
            set
            {
                UpdateDataRow<Int16?>(() => CountDep21, value);
            }
        }
        /// <summary>
        /// Количество иждивенцев (скидка 110-2000 рублей на ребенка);
        /// </summary>
        [Column(Name="COUNT_DEP18")]
        public Int16? CountDep18
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => CountDep18);
            }
            set
            {
                UpdateDataRow<Int16?>(() => CountDep18, value);
            }
        }
        /// <summary>
        /// Количество иждивенцев (скидка 109-2000 рублей на ребенка);
        /// </summary>
        [Column(Name="COUNT_DEP17")]
        public Int16? CountDep17
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => CountDep17);
            }
            set
            {
                UpdateDataRow<Int16?>(() => CountDep17, value);
            }
        }
        /// <summary>
        /// Количество иждивенцев (скидка 108-1000 рублей на ребенка);
        /// </summary>
        [Column(Name="COUNT_DEP14")]
        public Int16? CountDep14
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => CountDep14);
            }
            set
            {
                UpdateDataRow<Int16?>(() => CountDep14, value);
            }
        }
        /// <summary>
        /// Дата надбавки за стаж; Дата надбавки сотрудника за стаж;
        /// </summary>
        [Column(Name="DATE_ADD")]
        public DateTime? DateAdd
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateAdd);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateAdd, value);
            }
        }
        /// <summary>
        /// Признак надбавки за стаж;
        /// </summary>
        [Column(Name="SIGN_ADD")]
        public Decimal? SignAdd
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignAdd);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignAdd, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор тарифной сетки;
        /// </summary>
        [Column(Name="TARIFF_GRID_ID")]
        public Decimal? TariffGridID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TariffGridID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TariffGridID, value);
            }
        }
        /// <summary>
        /// Разряд; Разряд сотрудника на данной должности;
        /// </summary>
        [Column(Name="CLASSIFIC")]
        public Decimal? Classific
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Classific);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Classific, value);
            }
        }
        /// <summary>
        /// Оклад; Оклад работника;
        /// </summary>
        [Column(Name="SALARY")]
        public Single? Salary
        {
            get
            {
                return this.GetDataRowField<Single?>(() => Salary);
            }
            set
            {
                UpdateDataRow<Single?>(() => Salary, value);
            }
        }
        /// <summary>
        /// Процент 13 зарплаты;
        /// </summary>
        [Column(Name="PERCENT13")]
        public Decimal? Percent13
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Percent13);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Percent13, value);
            }
        }
        /// <summary>
        /// Надбавка за совмещение;
        /// </summary>
        [Column(Name="COMB_ADDITION")]
        public Double? CombAddition
        {
            get
            {
                return this.GetDataRowField<Double?>(() => CombAddition);
            }
            set
            {
                UpdateDataRow<Double?>(() => CombAddition, value);
            }
        }
        /// <summary>
        /// Надбавка за вредность;
        /// </summary>
        [Column(Name="HARMFUL_ADDITION")]
        public Double? HarmfulAddition
        {
            get
            {
                return this.GetDataRowField<Double?>(() => HarmfulAddition);
            }
            set
            {
                UpdateDataRow<Double?>(() => HarmfulAddition, value);
            }
        }
        /// <summary>
        /// Профессиональная надбавка;
        /// </summary>
        [Column(Name="PROF_ADDITION")]
        public Double? ProfAddition
        {
            get
            {
                return this.GetDataRowField<Double?>(() => ProfAddition);
            }
            set
            {
                UpdateDataRow<Double?>(() => ProfAddition, value);
            }
        }
        /// <summary>
        /// Дата на выслугу лет;
        /// </summary>
        [Column(Name="DATE_SERVANT")]
        public DateTime? DateServant
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateServant);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateServant, value);
            }
        }
        /// <summary>
        /// Шифр налога;
        /// </summary>
        [Column(Name="TAX_CODE")]
        public Int16? TaxCode
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => TaxCode);
            }
            set
            {
                UpdateDataRow<Int16?>(() => TaxCode, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор перевода на  заводе;
        /// </summary>
        [Column(Name="TRANSFER_ID")]
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
                #endregion
    }


    [Table(Name="PER_DATA"), SchemaName("")]
    public partial class PerData : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Табельный номер;
        /// </summary>
        [Column(Name="PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор источника трудоустройства;
        /// </summary>
        [Column(Name="SOURCE_EMPLOYABILITY_ID")]
        public Decimal? SourceEmployabilityID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SourceEmployabilityID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SourceEmployabilityID, value);
            }
        }
        /// <summary>
        /// ИНН; Индивидуальный номер налогоплательщика сотрудника;
        /// </summary>
        [Column(Name="INN")]
        public String Inn
        {
            get
            {
                return this.GetDataRowField<String>(() => Inn);
            }
            set
            {
                UpdateDataRow<String>(() => Inn, value);
            }
        }
        /// <summary>
        /// Номер мед. полиса; Номер медицинского полиса сотрудника;
        /// </summary>
        [Column(Name="NUM_MED_POLUS")]
        public String NumMedPolus
        {
            get
            {
                return this.GetDataRowField<String>(() => NumMedPolus);
            }
            set
            {
                UpdateDataRow<String>(() => NumMedPolus, value);
            }
        }
        /// <summary>
        /// Сер.Мед.; Серия медицинского полиса сотрудника;
        /// </summary>
        [Column(Name="SER_MED_POLUS")]
        public String SerMedPolus
        {
            get
            {
                return this.GetDataRowField<String>(() => SerMedPolus);
            }
            set
            {
                UpdateDataRow<String>(() => SerMedPolus, value);
            }
        }
        /// <summary>
        /// Номер страх. свид.; Номер страхового пенсионного свидетельства сотрудника;
        /// </summary>
        [Column(Name="INSURANCE_NUM")]
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
        /// Признак молодого специалиста;
        /// </summary>
        [Column(Name="SIGN_YOUNG_SPEC")]
        public Decimal? SignYoungSpec
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignYoungSpec);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignYoungSpec, value);
            }
        }
        /// <summary>
        /// Профсоюз; Признак членства в профсоюзе;
        /// </summary>
        [Column(Name="SIGN_PROFUNION")]
        public Decimal? SignProfunion
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignProfunion);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignProfunion, value);
            }
        }
        /// <summary>
        /// Признак наличия пенсии;
        /// </summary>
        [Column(Name="RETIRER_SIGN")]
        public Decimal? RetirerSign
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RetirerSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetirerSign, value);
            }
        }
        /// <summary>
        /// Признак того, что сотрудник командирован на завод;
        /// </summary>
        [Column(Name="TRIP_SIGN")]
        public Decimal? TripSign
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TripSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TripSign, value);
            }
        }
                #endregion
    }


    [Table(Name="PAYMENT_TYPE"), SchemaName("")]
    public partial class PaymentType : RowEntityBase
    {
        #region Class Members
        [Column(Name="PAYMENT_TYPE_ID", IsPrimaryKey=true)]
        public Decimal? PaymentTypeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentTypeID, value);
            }
        }
        /// <summary>
        /// Допустимы отрицательные суммы в виде оплат
        /// </summary>
        [Column(Name="IS_NEGATIV_ALOWED")]
        public Decimal? IsNegativAlowed
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => IsNegativAlowed);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => IsNegativAlowed, value);
            }
        }
        /// <summary>
        /// тип измерения
        /// </summary>
        [Column(Name="CONSIDER_TYPE_ID")]
        public Decimal? ConsiderTypeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ConsiderTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ConsiderTypeID, value);
            }
        }
        /// <summary>
        /// тип шифра оплат
        /// </summary>
        [Column(Name="TYPE_PAYMENT_TYPE_ID")]
        public Decimal? TypePaymentTypeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypePaymentTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypePaymentTypeID, value);
            }
        }
        /// <summary>
        /// признак формирования в протокол
        /// </summary>
        [Column(Name="SIGN_FORM_REPORT")]
        public Decimal? SignFormReport
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignFormReport);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignFormReport, value);
            }
        }
        [Column(Name="NAME_PAYMENT")]
        public String NamePayment
        {
            get
            {
                return this.GetDataRowField<String>(() => NamePayment);
            }
            set
            {
                UpdateDataRow<String>(() => NamePayment, value);
            }
        }
        /// <summary>
        /// приоритет расчета
        /// </summary>
        [Column(Name="CALC_PRIORITY")]
        public Decimal? CalcPriority
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CalcPriority);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CalcPriority, value);
            }
        }
        /// <summary>
        /// Аналог шифра оплат в старой системе
        /// </summary>
        [Column(Name="PAY_TYPE_ID")]
        public Decimal? PayTypeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PayTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PayTypeID, value);
            }
        }
        [Column(Name="CODE_PAYMENT")]
        public String CodePayment
        {
            get
            {
                return this.GetDataRowField<String>(() => CodePayment);
            }
            set
            {
                UpdateDataRow<String>(() => CodePayment, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Таблица принятых/переданых затрат
    /// </summary>

    [Table(Name="SAL_SUBDIV_RECEIVE"), SchemaName("")]
    public partial class SalSubdivReceive : RowEntityBase
    {
        #region Class Members
        [Column(Name="SAL_SUBDIV_RECEIVE_ID", IsPrimaryKey=true)]
        public Decimal? SalSubdivReceiveID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalSubdivReceiveID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalSubdivReceiveID, value);
            }
        }
        /// <summary>
        /// Отчетный месяц
        /// </summary>
        [Column(Name="REC_DATE", CanBeNull=false)]
        public DateTime? RecDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => RecDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => RecDate, value);
            }
        }
        /// <summary>
        /// подразделение передающее затраты (откуда, передано)
        /// </summary>
        [Column(Name="RECEIVE_SUBDIV_ID", CanBeNull=false)]
        public Decimal? ReceiveSubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ReceiveSubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ReceiveSubdivID, value);
            }
        }
        /// <summary>
        /// поясной кф-т на сумму
        /// </summary>
        [Column(Name="SUBDIV_SAL")]
        public Decimal? SubdivSal
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivSal);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivSal, value);
            }
        }
        /// <summary>
        /// Сумма передаваемых затрат
        /// </summary>
        [Column(Name="SUM_SAL")]
        public Decimal? SumSal
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SumSal);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SumSal, value);
            }
        }
        /// <summary>
        /// Часы передаваемых затрат
        /// </summary>
        [Column(Name="HOURS")]
        public Decimal? Hours
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Hours);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Hours, value);
            }
        }
        /// <summary>
        /// Заказ передаваемых затрат
        /// </summary>
        [Column(Name="ORDER_ID", CanBeNull=false)]
        public Decimal? OrderID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => OrderID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OrderID, value);
            }
        }
        /// <summary>
        /// Подразделение принимающее  затраты (куда передали, принято)
        /// </summary>
        [Column(Name="SUBDIV_ID", CanBeNull=false)]
        public Decimal? SubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }
                #endregion
    }


    [Table(Name="CARTULARY"), SchemaName("SALARY")]
    public partial class Cartulary : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный номер реестра
        /// </summary>
        [Column(Name="CARTULARY_ID", CanBeNull=false)]
        public Decimal? CartularyID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CartularyID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CartularyID, value);
            }
        }
        /// <summary>
        /// Тип реестра
        /// </summary>
        [Column(Name="TYPE_CARTULARY_ID", CanBeNull=false)]
        public Decimal? TypeCartularyID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeCartularyID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeCartularyID, value);
            }
        }
        /// <summary>
        /// Дата реестра
        /// </summary>
        [Column(Name="DATE_CARTULARY")]
        public DateTime? DateCartulary
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateCartulary);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateCartulary, value);
            }
        }
        /// <summary>
        /// Дата создания реестра
        /// </summary>
        [Column(Name="DATE_CREATE")]
        public DateTime? DateCreate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateCreate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateCreate, value);
            }
        }
        /// <summary>
        /// Дата закрытия реестра
        /// </summary>
        [Column(Name="DATE_CLOSE_CART")]
        public DateTime? DateCloseCart
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateCloseCart);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateCloseCart, value);
            }
        }
        /// <summary>
        /// Наименование для реестра
        /// </summary>
        [Column(Name="CARTULARY_COMMENT")]
        public String CartularyComment
        {
            get
            {
                return this.GetDataRowField<String>(() => CartularyComment);
            }
            set
            {
                UpdateDataRow<String>(() => CartularyComment, value);
            }
        }
        /// <summary>
        /// Код реестра
        /// </summary>
        [Column(Name="CARTULARY_NUM")]
        public String CartularyNum
        {
            get
            {
                return this.GetDataRowField<String>(() => CartularyNum);
            }
            set
            {
                UpdateDataRow<String>(() => CartularyNum, value);
            }
        }
        /// <summary>
        /// Подразделенеие для котрого формируется реестр
        /// </summary>
        [Column(Name="CARTULARY_SUBDIV_ID")]
        public Decimal? CartularySubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CartularySubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CartularySubdivID, value);
            }
        }
        /// <summary>
        /// Заголовок к реестру используется для отчетов
        /// </summary>
        [Column(Name="CARTULARY_HEADER")]
        public String CartularyHeader
        {
            get
            {
                return this.GetDataRowField<String>(() => CartularyHeader);
            }
            set
            {
                UpdateDataRow<String>(() => CartularyHeader, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Таблица корректировки для формирования базы распределения
    /// </summary>

    [Table(Name="SALARY_ADD_CORRELATION"), SchemaName("SALARY")]
    public partial class SalaryAddCorrelation : RowEntityBase
    {
        #region Class Members
        [Column(Name="SALARY_ADD_CORRELATION_ID", IsPrimaryKey=true)]
        public Decimal? SalaryAddCorrelationID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryAddCorrelationID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryAddCorrelationID, value);
            }
        }
        /// <summary>
        /// Дата отчета
        /// </summary>
        [Column(Name="CALC_DATE", CanBeNull=false)]
        public DateTime? CalcDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => CalcDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => CalcDate, value);
            }
        }
        /// <summary>
        /// Категория
        /// </summary>
        [Column(Name="DEGREE_ID", CanBeNull=false)]
        public Decimal? DegreeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DegreeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DegreeID, value);
            }
        }
        [Column(Name="SUM_SAL")]
        public Decimal? SumSal
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SumSal);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SumSal, value);
            }
        }
        [Column(Name="HOURS")]
        public Decimal? Hours
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Hours);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Hours, value);
            }
        }
        [Column(Name="ORDER_ID", CanBeNull=false)]
        public Decimal? OrderID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => OrderID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OrderID, value);
            }
        }
        [Column(Name="PAYMENT_TYPE_ID", CanBeNull=false)]
        public Decimal? PaymentTypeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentTypeID, value);
            }
        }
        [Column(Name="TYPE_OPERATION_ID", CanBeNull=false)]
        public Decimal? TypeOperationID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeOperationID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeOperationID, value);
            }
        }
        [Column(Name="SUBDIV_ID", CanBeNull=false)]
        public Decimal? SubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Справочник подразделений
    /// </summary>

    [Table(Name="SUBDIV"), SchemaName("APSTAFF")]
    public partial class Subdiv : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный идентификатор подразделения, из которого образовалось данное подразделение;
        /// </summary>
        [Column(Name="SUBDIV_ID", IsPrimaryKey=true)]
        public Decimal? SubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор графика работы;
        /// </summary>
        [Column(Name="GR_WORK_ID")]
        public Decimal? GrWorkID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => GrWorkID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => GrWorkID, value);
            }
        }
        /// <summary>
        /// Наименование типа подразделения;
        /// </summary>
        [Column(Name="TYPE_SUBDIV_ID")]
        public Decimal? TypeSubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeSubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeSubdivID, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор подразделения, из которого образовалось данное подразделение;
        /// </summary>
        [Column(Name="FROM_SUBDIV")]
        public Decimal? FromSubdiv
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => FromSubdiv);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => FromSubdiv, value);
            }
        }
        /// <summary>
        /// Код подразделения; Код подразделения, которому подчиняется данное подразделение;
        /// </summary>
        [Column(Name="PARENT_ID")]
        public Decimal? ParentID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ParentID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ParentID, value);
            }
        }
        /// <summary>
        /// Дата окончания действия имени и кода для данного подразделения;
        /// </summary>
        [Column(Name="SUB_DATE_END")]
        public DateTime? SubDateEnd
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => SubDateEnd);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => SubDateEnd, value);
            }
        }
        /// <summary>
        /// Дата введения в действие кода и наименования для данного подразделения;
        /// </summary>
        [Column(Name="SUB_DATE_START")]
        public DateTime? SubDateStart
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => SubDateStart);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => SubDateStart, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор службы завода;
        /// </summary>
        [Column(Name="SERVICE_ID")]
        public Decimal? ServiceID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ServiceID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ServiceID, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор типа работы подразделения;
        /// </summary>
        [Column(Name="WORK_TYPE_ID")]
        public Decimal? WorkTypeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => WorkTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => WorkTypeID, value);
            }
        }
        /// <summary>
        /// Признак актуальности; Признак актуальности подразделения (если равен 1, то подразделение в данный момент актуально, если 0, то подразделение не актуально, то есть было когда-то, но сейчас его нет);
        /// </summary>
        [Column(Name="SUB_ACTUAL_SIGN")]
        public Int16? SubActualSign
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => SubActualSign);
            }
            set
            {
                UpdateDataRow<Int16?>(() => SubActualSign, value);
            }
        }
        /// <summary>
        /// Наименование подразделения; Текущее наименование подразделения;
        /// </summary>
        [Column(Name="SUBDIV_NAME", CanBeNull=false)]
        public String SubdivName
        {
            get
            {
                return this.GetDataRowField<String>(() => SubdivName);
            }
            set
            {
                UpdateDataRow<String>(() => SubdivName, value);
            }
        }
        /// <summary>
        /// Код подразделения; Текущий код подразделения;
        /// </summary>
        [Column(Name="CODE_SUBDIV")]
        public String CodeSubdiv
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeSubdiv);
            }
            set
            {
                UpdateDataRow<String>(() => CodeSubdiv, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Таблица истории изменений записей по зарплате
    /// </summary>

    [Table(Name="SALARY_HISTORY"), SchemaName("SALARY")]
    public partial class SalaryHistory : RowEntityBase
    {
        #region Class Members
        [Column(Name="SALARY_ID")]
        public Decimal? SalaryID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryID, value);
            }
        }
        /// <summary>
        /// Имя пользователя внесшего запись
        /// </summary>
        [Column(Name="USER_NAME")]
        public String UserName
        {
            get
            {
                return this.GetDataRowField<String>(() => UserName);
            }
            set
            {
                UpdateDataRow<String>(() => UserName, value);
            }
        }
        /// <summary>
        /// Операция обновления 1-Insert, 2 - Update, 3 - Delete
        /// </summary>
        [Column(Name="OP_ID")]
        public Decimal? OpID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => OpID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OpID, value);
            }
        }
        [Column(Name="DATE_EDIT")]
        public DateTime? DateEdit
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateEdit);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateEdit, value);
            }
        }
        [Column(Name="SESSION_ID")]
        public Decimal? SessionID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SessionID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SessionID, value);
            }
        }
        /// <summary>
        /// Документ удержания
        /// </summary>
        [Column(Name="RETENTION_ID")]
        public Decimal? RetentionID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RetentionID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentionID, value);
            }
        }
        /// <summary>
        /// Подразделение
        /// </summary>
        [Column(Name="SUBDIV_ID")]
        public Decimal? SubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }
        /// <summary>
        /// Кол-во дней шифра оплат;
        /// </summary>
        [Column(Name="DAYS")]
        public Decimal? Days
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Days);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Days, value);
            }
        }
        /// <summary>
        /// Ссылка на другую таблицу привязанных данных
        /// </summary>
        [Column(Name="REF_ID")]
        public Decimal? RefID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RefID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RefID, value);
            }
        }
        [Column(Name="TYPE_REF_SALARY_ID")]
        public Decimal? TypeRefSalaryID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeRefSalaryID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeRefSalaryID, value);
            }
        }
        /// <summary>
        /// Уникальный номер перевода работника, на который была выплачена ЗП;
        /// </summary>
        [Column(Name="TRANSFER_ID")]
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
        /// <summary>
        /// Категория для шифра оплат;
        /// </summary>
        [Column(Name="DEGREE_ID")]
        public Decimal? DegreeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DegreeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DegreeID, value);
            }
        }
        /// <summary>
        /// Номер заказа уникальный
        /// </summary>
        [Column(Name="ORDER_ID")]
        public Decimal? OrderID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => OrderID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OrderID, value);
            }
        }
        /// <summary>
        /// Сумма
        /// </summary>
        [Column(Name="SUM_SAL")]
        public Decimal? SumSal
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SumSal);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SumSal, value);
            }
        }
        /// <summary>
        /// Часы
        /// </summary>
        [Column(Name="HOURS")]
        public Decimal? Hours
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Hours);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Hours, value);
            }
        }
        /// <summary>
        /// Шифр оплат;
        /// </summary>
        [Column(Name="PAYMENT_TYPE_ID")]
        public Decimal? PaymentTypeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentTypeID, value);
            }
        }
        /// <summary>
        /// Дата оплаты
        /// </summary>
        [Column(Name="PAY_DATE")]
        public DateTime? PayDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => PayDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => PayDate, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Переводы сотрудников
    /// </summary>

    [Table(Name="TRANSFER"), SchemaName("APSTAFF")]
    public partial class Transfer : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный идентификатор перевода на  заводе;
        /// </summary>
        [Column(Name="TRANSFER_ID", IsPrimaryKey=true)]
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
        /// <summary>
        /// Примечание к должности;
        /// </summary>
        [Column(Name="POS_NOTE")]
        public String PosNote
        {
            get
            {
                return this.GetDataRowField<String>(() => PosNote);
            }
            set
            {
                UpdateDataRow<String>(() => PosNote, value);
            }
        }
        /// <summary>
        /// Иной доп.отпуск; Иной дополнительный отпуск;
        /// </summary>
        [Column(Name="ADDITIONAL_VAC")]
        public Decimal? AdditionalVac
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => AdditionalVac);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AdditionalVac, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор характера перевода;
        /// </summary>
        [Column(Name="CHAR_TRANSFER_ID")]
        public Decimal? CharTransferID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CharTransferID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CharTransferID, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор работника;
        /// </summary>
        [Column(Name="WORKER_ID")]
        public Decimal? WorkerID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => WorkerID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => WorkerID, value);
            }
        }
        /// <summary>
        /// Доп.отпуск; Дополнительный отпуск за вредность;
        /// </summary>
        [Column(Name="HARMFUL_VAC")]
        public Decimal? HarmfulVac
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => HarmfulVac);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => HarmfulVac, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор вида производства;
        /// </summary>
        [Column(Name="FORM_OPERATION_ID")]
        public Decimal? FormOperationID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => FormOperationID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => FormOperationID, value);
            }
        }
        /// <summary>
        /// Испытательный срок;Исп.срок;
        /// </summary>
        [Column(Name="PROBA_PERIOD")]
        public Decimal? ProbaPeriod
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ProbaPeriod);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ProbaPeriod, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор причины увольнения;
        /// </summary>
        [Column(Name="REASON_ID")]
        public Decimal? ReasonID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => ReasonID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ReasonID, value);
            }
        }
        /// <summary>
        /// Признак текущей работы;  Признак текущей работы (Если равен 1, то на этой должности в данный момент работают, а если 0, то в данный период на этой должности не работают );
        /// </summary>
        [Column(Name="SIGN_CUR_WORK")]
        public Int16? SignCurWork
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => SignCurWork);
            }
            set
            {
                UpdateDataRow<Int16?>(() => SignCurWork, value);
            }
        }
        /// <summary>
        /// Дата формирования книги увольнения;
        /// </summary>
        [Column(Name="DF_BOOK_DISMISS")]
        public DateTime? DfBookDismiss
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DfBookDismiss);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DfBookDismiss, value);
            }
        }
        /// <summary>
        /// Дата формирования; Дата формирования книги приказов;
        /// </summary>
        [Column(Name="DF_BOOK_ORDER")]
        public DateTime? DfBookOrder
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DfBookOrder);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DfBookOrder, value);
            }
        }
        /// <summary>
        /// Дата контракта;
        /// </summary>
        [Column(Name="DATE_CONTR")]
        public DateTime? DateContr
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateContr);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateContr, value);
            }
        }
        /// <summary>
        /// Номер трудового договора;
        /// </summary>
        [Column(Name="CONTR_EMP")]
        public String ContrEmp
        {
            get
            {
                return this.GetDataRowField<String>(() => ContrEmp);
            }
            set
            {
                UpdateDataRow<String>(() => ContrEmp, value);
            }
        }
        /// <summary>
        /// Признак канцелярии; Признак канцелярии (если документ о переводе или увольнении  или приеме сотрудника проходит через отдел кадров, то принимает значение 0, а если проходит через канцелярию 1);
        /// </summary>
        [Column(Name="CHAN_SIGN")]
        public Int16? ChanSign
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => ChanSign);
            }
            set
            {
                UpdateDataRow<Int16?>(() => ChanSign, value);
            }
        }
        /// <summary>
        /// Должность, c которой был переведен сотрудник или уволен;
        /// </summary>
        [Column(Name="FROM_POSITION")]
        public Decimal? FromPosition
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => FromPosition);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => FromPosition, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор основания;
        /// </summary>
        [Column(Name="BASE_DOC_ID")]
        public Decimal? BaseDocID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => BaseDocID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => BaseDocID, value);
            }
        }
        /// <summary>
        /// Признак приема; Признак приема (принят или нет сотрудник на работу), полсе того как Hire_Sign =1, то сотрудник попадает в основную базу работников завода, выставляется только при приеме на работу;
        /// </summary>
        [Column(Name="HIRE_SIGN")]
        public Int16? HireSign
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => HireSign);
            }
            set
            {
                UpdateDataRow<Int16?>(() => HireSign, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор источника комплектования;
        /// </summary>
        [Column(Name="SOURCE_ID")]
        public Decimal? SourceID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SourceID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SourceID, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор характера работы;
        /// </summary>
        [Column(Name="CHAR_WORK_ID")]
        public Decimal? CharWorkID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CharWorkID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CharWorkID, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор категории (сотрудника или тарифной ставки);
        /// </summary>
        [Column(Name="DEGREE_ID")]
        public Decimal? DegreeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DegreeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DegreeID, value);
            }
        }
        /// <summary>
        /// Форма оплаты; Форма оплаты (1 - сдельная, 2 - повременная);
        /// </summary>
        [Column(Name="FORM_PAY")]
        public Int16? FormPay
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => FormPay);
            }
            set
            {
                UpdateDataRow<Int16?>(() => FormPay, value);
            }
        }
        /// <summary>
        /// Дата приказа;Дата приказа (поступления на работу или увольнения или перевода, зависит от типа перевода);
        /// </summary>
        [Column(Name="TR_DATE_ORDER")]
        public DateTime? TrDateOrder
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => TrDateOrder);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => TrDateOrder, value);
            }
        }
        /// <summary>
        /// Номер приказа; Номер приказа на увольнение, прием на работу или перевод;
        /// </summary>
        [Column(Name="TR_NUM_ORDER")]
        public String TrNumOrder
        {
            get
            {
                return this.GetDataRowField<String>(() => TrNumOrder);
            }
            set
            {
                UpdateDataRow<String>(() => TrNumOrder, value);
            }
        }
        /// <summary>
        /// Дата окончания контракта;
        /// </summary>
        [Column(Name="DATE_END_CONTR")]
        public DateTime? DateEndContr
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateEndContr);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateEndContr, value);
            }
        }
        /// <summary>
        /// Дата перевода; Дата перемещения сотрудника (увольнение, перевод или прием на работу, зависит от Type_Move - тип перевода);
        /// </summary>
        [Column(Name="DATE_TRANSFER")]
        public DateTime? DateTransfer
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateTransfer);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateTransfer, value);
            }
        }
        /// <summary>
        /// Признак совмещения;
        /// </summary>
        [Column(Name="SIGN_COMB")]
        public Int16? SignComb
        {
            get
            {
                return this.GetDataRowField<Int16?>(() => SignComb);
            }
            set
            {
                UpdateDataRow<Int16?>(() => SignComb, value);
            }
        }
        /// <summary>
        /// Дата наема; Дата наема сотрудника;
        /// </summary>
        [Column(Name="DATE_HIRE")]
        public DateTime? DateHire
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateHire);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateHire, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор графика работы;
        /// </summary>
        [Column(Name="GR_WORK_ID")]
        public Decimal? GrWorkID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => GrWorkID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => GrWorkID, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор типа перевода;
        /// </summary>
        [Column(Name="TYPE_TRANSFER_ID")]
        public Decimal? TypeTransferID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeTransferID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeTransferID, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор должности;
        /// </summary>
        [Column(Name="POS_ID")]
        public Decimal? PosID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => PosID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PosID, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор подразделения, из которого образовалось данное подразделение;
        /// </summary>
        [Column(Name="SUBDIV_ID")]
        public Decimal? SubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }
        /// <summary>
        /// Табельный номер;
        /// </summary>
        [Column(Name="PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Таблица бригад
    /// </summary>

    [Table(Name="BRIGAGE"), SchemaName("")]
    public partial class Brigage : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный номер бригады (автопоследовательность)
        /// </summary>
        [Column(Name="BRIGAGE_ID")]
        public Decimal? BrigageID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => BrigageID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => BrigageID, value);
            }
        }
        /// <summary>
        /// Дата окончания действия бригады
        /// </summary>
        [Column(Name="DATE_END_BRIGAGE")]
        public DateTime? DateEndBrigage
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateEndBrigage);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateEndBrigage, value);
            }
        }
        /// <summary>
        /// Дата начала действия бригады
        /// </summary>
        [Column(Name="DATE_BEGIN_BRIGAGE")]
        public DateTime? DateBeginBrigage
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateBeginBrigage);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateBeginBrigage, value);
            }
        }
        /// <summary>
        /// Подразделение бригады
        /// </summary>
        [Column(Name="SUBDIV_ID")]
        public Decimal? SubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }
        /// <summary>
        /// Группа мастера бригады
        /// </summary>
        [Column(Name="GROUP_MASTER")]
        public String GroupMaster
        {
            get
            {
                return this.GetDataRowField<String>(() => GroupMaster);
            }
            set
            {
                UpdateDataRow<String>(() => GroupMaster, value);
            }
        }
        /// <summary>
        /// Наименование бригады
        /// </summary>
        [Column(Name="BRIGAGE_NAME")]
        public String BrigageName
        {
            get
            {
                return this.GetDataRowField<String>(() => BrigageName);
            }
            set
            {
                UpdateDataRow<String>(() => BrigageName, value);
            }
        }
        /// <summary>
        /// Код бригады
        /// </summary>
        [Column(Name="BRIGAGE_CODE")]
        public String BrigageCode
        {
            get
            {
                return this.GetDataRowField<String>(() => BrigageCode);
            }
            set
            {
                UpdateDataRow<String>(() => BrigageCode, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Таблица табелей бригад
    /// </summary>

    [Table(Name="TABLE_BRIGAGE"), SchemaName("")]
    public partial class TableBrigage : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный номер записи табеля бригады
        /// </summary>
        [Column(Name="TABLE_BRIGAGE_ID")]
        public Decimal? TableBrigageID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TableBrigageID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TableBrigageID, value);
            }
        }
        /// <summary>
        /// Дата работы сотрудника (месяц) по табелю бригады
        /// </summary>
        [Column(Name="WORK_DATE")]
        public DateTime? WorkDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => WorkDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => WorkDate, value);
            }
        }
        /// <summary>
        /// Уникальный номер подразделения
        /// </summary>
        [Column(Name="SUBDIV_ID")]
        public Decimal? SubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }
        /// <summary>
        /// Уникальный номер перевода сотрудника
        /// </summary>
        [Column(Name="TRANSFER_ID")]
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
        /// <summary>
        /// Признак совмещения сотрудника
        /// </summary>
        [Column(Name="SIGN_COMB")]
        public Decimal? SignComb
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignComb);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignComb, value);
            }
        }
        /// <summary>
        /// Коэффициент трудового участия (КТУ)
        /// </summary>
        [Column(Name="COEFFICIENT")]
        public Decimal? Coefficient
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => Coefficient);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Coefficient, value);
            }
        }
        /// <summary>
        /// Отработанные часы по табелю сотрудника
        /// </summary>
        [Column(Name="WORK_HOURS")]
        public Decimal? WorkHours
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => WorkHours);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => WorkHours, value);
            }
        }
        /// <summary>
        /// Табельный номер сотрудника (возможно устарело)
        /// </summary>
        [Column(Name="PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        /// <summary>
        /// Комментарий
        /// </summary>
        [Column(Name="COMMENTS")]
        public String Comments
        {
            get
            {
                return this.GetDataRowField<String>(() => Comments);
            }
            set
            {
                UpdateDataRow<String>(() => Comments, value);
            }
        }
        /// <summary>
        /// Категория сотрудника (возможно устарело)
        /// </summary>
        [Column(Name="DEGREE_ID")]
        public Decimal? DegreeID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DegreeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DegreeID, value);
            }
        }
        /// <summary>
        /// Уникальный номер бригады
        /// </summary>
        [Column(Name="BRIGAGE_ID")]
        public Decimal? BrigageID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => BrigageID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => BrigageID, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Класс представляющий общие данные
    /// </summary>

    [Table(Name="PIECE_WORK_DATA"), SchemaName("")]
    public partial class PieceWorkData : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Заказ
        /// </summary>
        [Column(Name="ORDER_NAME")]
        public String OrderName
        {
            get
            {
                return this.GetDataRowField<String>(() => OrderName);
            }
            set
            {
                UpdateDataRow<String>(() => OrderName, value);
            }
        }
        /// <summary>
        /// № пачки
        /// </summary>
        [Column(Name="PACKAGE_NUMBER")]
        public String PackageNumber
        {
            get
            {
                return this.GetDataRowField<String>(() => PackageNumber);
            }
            set
            {
                UpdateDataRow<String>(() => PackageNumber, value);
            }
        }
        /// <summary>
        /// Дата выполнения
        /// </summary>
        [Column(Name="COMPLETE_DATE")]
        public DateTime? CompleteDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => CompleteDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => CompleteDate, value);
            }
        }
        /// <summary>
        /// Шифр детали
        /// </summary>
        [Column(Name="DETAIL_CODE")]
        public String DetailCode
        {
            get
            {
                return this.GetDataRowField<String>(() => DetailCode);
            }
            set
            {
                UpdateDataRow<String>(() => DetailCode, value);
            }
        }
        /// <summary>
        /// Количество годных
        /// </summary>
        [Column(Name="DETAIL_COUNT")]
        public Decimal? DetailCount
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DetailCount);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DetailCount, value);
            }
        }
        /// <summary>
        /// Разряд работ
        /// </summary>
        [Column(Name="WORK_CLASSIFIC")]
        public Decimal? WorkClassific
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => WorkClassific);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => WorkClassific, value);
            }
        }
        /// <summary>
        /// Сумма зарплаты
        /// </summary>
        [Column(Name="DETAIL_SUM")]
        public Decimal? DetailSum
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DetailSum);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DetailSum, value);
            }
        }
        /// <summary>
        /// Сумма часов
        /// </summary>
        [Column(Name="DETAIL_TIME")]
        public Decimal? DetailTime
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DetailTime);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DetailTime, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Таблица для закрытия областей редактирования
    /// </summary>

    [Table(Name="SUBDIV_FOR_CLOSE"), SchemaName("")]
    public partial class SubdivForClose : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный идентификатор подразделения
        /// </summary>
        [Column(Name="SUBDIV_ID")]
        public Decimal? SubdivID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }
        /// <summary>
        /// Дата готовности (обработки) подразделения
        /// </summary>
        [Column(Name="LAST_DATE_PROCESSING")]
        public DateTime? LastDateProcessing
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => LastDateProcessing);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => LastDateProcessing, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор закрытия подразделения
        /// </summary>
        [Column(Name="SUBDIV_FOR_CLOSE_ID")]
        public Decimal? SubdivForCloseID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SubdivForCloseID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivForCloseID, value);
            }
        }
        /// <summary>
        /// Имя приложения
        /// </summary>
        [Column(Name="APP_NAME")]
        public String AppName
        {
            get
            {
                return this.GetDataRowField<String>(() => AppName);
            }
            set
            {
                UpdateDataRow<String>(() => AppName, value);
            }
        }
        /// <summary>
        /// Дата изменения
        /// </summary>
        [Column(Name="DATE_CHANGE")]
        public DateTime? DateChange
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateChange);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateChange, value);
            }
        }
        /// <summary>
        /// Дата закрытия
        /// </summary>
        [Column(Name="DATE_CLOSING")]
        public DateTime? DateClosing
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateClosing);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateClosing, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Организации для налогового учета
    /// </summary>

    [Table(Name="TAX_COMPANY"), SchemaName("SALARY")]
    public partial class TaxCompany : RowEntityBase
    {
        #region Class Members
        [Column(Name="TAX_COMPANY_ID")]
        public Decimal? TaxCompanyID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TaxCompanyID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TaxCompanyID, value);
            }
        }
        /// <summary>
        /// Наименование организации
        /// </summary>
        [Column(Name="COMPANY_NAME", CanBeNull=false)]
        public String CompanyName
        {
            get
            {
                return this.GetDataRowField<String>(() => CompanyName);
            }
            set
            {
                UpdateDataRow<String>(() => CompanyName, value);
            }
        }
        /// <summary>
        /// ОКТМО сдачи отчетности
        /// </summary>
        [Column(Name="OKTMO")]
        public String Oktmo
        {
            get
            {
                return this.GetDataRowField<String>(() => Oktmo);
            }
            set
            {
                UpdateDataRow<String>(() => Oktmo, value);
            }
        }
        /// <summary>
        /// Телефон организации
        /// </summary>
        [Column(Name="TEL")]
        public String Tel
        {
            get
            {
                return this.GetDataRowField<String>(() => Tel);
            }
            set
            {
                UpdateDataRow<String>(() => Tel, value);
            }
        }
        /// <summary>
        /// ИНН
        /// </summary>
        [Column(Name="INN", CanBeNull=false)]
        public String Inn
        {
            get
            {
                return this.GetDataRowField<String>(() => Inn);
            }
            set
            {
                UpdateDataRow<String>(() => Inn, value);
            }
        }
        /// <summary>
        /// КПП
        /// </summary>
        [Column(Name="KPP")]
        public String Kpp
        {
            get
            {
                return this.GetDataRowField<String>(() => Kpp);
            }
            set
            {
                UpdateDataRow<String>(() => Kpp, value);
            }
        }
        /// <summary>
        /// Признак агента 
        /// </summary>
        [Column(Name="AGENT_STATUS")]
        public String AgentStatus
        {
            get
            {
                return this.GetDataRowField<String>(() => AgentStatus);
            }
            set
            {
                UpdateDataRow<String>(() => AgentStatus, value);
            }
        }
        /// <summary>
        /// ФИО подписанта
        /// </summary>
        [Column(Name="AGENT_NAME")]
        public String AgentName
        {
            get
            {
                return this.GetDataRowField<String>(() => AgentName);
            }
            set
            {
                UpdateDataRow<String>(() => AgentName, value);
            }
        }
        /// <summary>
        /// Дата начала действия организации
        /// </summary>
        [Column(Name="DATE_BEGIN", CanBeNull=false)]
        public DateTime? DateBegin
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateBegin);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateBegin, value);
            }
        }
        /// <summary>
        /// Дата окончания действия организации
        /// </summary>
        [Column(Name="DATE_END", CanBeNull=false)]
        public DateTime? DateEnd
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateEnd);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateEnd, value);
            }
        }
        /// <summary>
        /// Комментарий
        /// </summary>
        [Column(Name="COMMENTS")]
        public String Comments
        {
            get
            {
                return this.GetDataRowField<String>(() => Comments);
            }
            set
            {
                UpdateDataRow<String>(() => Comments, value);
            }
        }
        /// <summary>
        /// Сокращенное название
        /// </summary>
        [Column(Name="SHORT_COMPANY_NAME")]
        public String ShortCompanyName
        {
            get
            {
                return this.GetDataRowField<String>(() => ShortCompanyName);
            }
            set
            {
                UpdateDataRow<String>(() => ShortCompanyName, value);
            }
        }
        /// <summary>
        /// Документ подтверждающий полномочия представителя
        /// </summary>
        [Column(Name="AGENT_DOCUMENT")]
        public String AgentDocument
        {
            get
            {
                return this.GetDataRowField<String>(() => AgentDocument);
            }
            set
            {
                UpdateDataRow<String>(() => AgentDocument, value);
            }
        }
                #endregion
    }


    [Table(Name="TAX_EMP_DOCUM"), SchemaName("")]
    public partial class TaxEmpDocum : RowEntityBase
    {
        #region Class Members
        [Column(Name="TAX_EMP_DOCUM_ID")]
        public Decimal? TaxEmpDocumID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TaxEmpDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TaxEmpDocumID, value);
            }
        }
        /// <summary>
        /// Таб. № документа
        /// </summary>
        [Column(Name="PER_NUM")]
        public String PerNum
        {
            get
            {
                return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        /// <summary>
        /// Налоговый статус документа
        /// </summary>
        [Column(Name="TAX_STATUS")]
        public Decimal? TaxStatus
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TaxStatus);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TaxStatus, value);
            }
        }
        /// <summary>
        /// Ссылка на документ удостоверяющего личность
        /// </summary>
        [Column(Name="TYPE_PER_DOC_ID")]
        public Decimal? TypePerDocID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypePerDocID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypePerDocID, value);
            }
        }
        /// <summary>
        /// Индекс места прописки
        /// </summary>
        [Column(Name="HOME_INDEX")]
        public String HomeIndex
        {
            get
            {
                return this.GetDataRowField<String>(() => HomeIndex);
            }
            set
            {
                UpdateDataRow<String>(() => HomeIndex, value);
            }
        }
        /// <summary>
        /// Код региона места прописки
        /// </summary>
        [Column(Name="CODE_REGION")]
        public String CodeRegion
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeRegion);
            }
            set
            {
                UpdateDataRow<String>(() => CodeRegion, value);
            }
        }
        /// <summary>
        /// Район
        /// </summary>
        [Column(Name="DISTRICT")]
        public String District
        {
            get
            {
                return this.GetDataRowField<String>(() => District);
            }
            set
            {
                UpdateDataRow<String>(() => District, value);
            }
        }
        /// <summary>
        /// Город
        /// </summary>
        [Column(Name="CITY")]
        public String City
        {
            get
            {
                return this.GetDataRowField<String>(() => City);
            }
            set
            {
                UpdateDataRow<String>(() => City, value);
            }
        }
        /// <summary>
        /// Населенный пункт
        /// </summary>
        [Column(Name="LOCALITY")]
        public String Locality
        {
            get
            {
                return this.GetDataRowField<String>(() => Locality);
            }
            set
            {
                UpdateDataRow<String>(() => Locality, value);
            }
        }
        /// <summary>
        /// Улица
        /// </summary>
        [Column(Name="STREET")]
        public String Street
        {
            get
            {
                return this.GetDataRowField<String>(() => Street);
            }
            set
            {
                UpdateDataRow<String>(() => Street, value);
            }
        }
        /// <summary>
        /// Номер дома
        /// </summary>
        [Column(Name="HOME_NUMBER")]
        public String HomeNumber
        {
            get
            {
                return this.GetDataRowField<String>(() => HomeNumber);
            }
            set
            {
                UpdateDataRow<String>(() => HomeNumber, value);
            }
        }
        /// <summary>
        /// Блок
        /// </summary>
        [Column(Name="HOUSING")]
        public String Housing
        {
            get
            {
                return this.GetDataRowField<String>(() => Housing);
            }
            set
            {
                UpdateDataRow<String>(() => Housing, value);
            }
        }
        /// <summary>
        /// Номер хаты
        /// </summary>
        [Column(Name="FLAT_NUMBER")]
        public String FlatNumber
        {
            get
            {
                return this.GetDataRowField<String>(() => FlatNumber);
            }
            set
            {
                UpdateDataRow<String>(() => FlatNumber, value);
            }
        }
        /// <summary>
        /// Налоговая ставка в процентах
        /// </summary>
        [Column(Name="TAX_PERCENT")]
        public Decimal? TaxPercent
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TaxPercent);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TaxPercent, value);
            }
        }
        /// <summary>
        /// Номер уведомления дающего право на социальный вычет
        /// </summary>
        [Column(Name="SOCIAL_NOTIFY_NUMBER")]
        public String SocialNotifyNumber
        {
            get
            {
                return this.GetDataRowField<String>(() => SocialNotifyNumber);
            }
            set
            {
                UpdateDataRow<String>(() => SocialNotifyNumber, value);
            }
        }
        /// <summary>
        /// Дата уведомления дающего право на социальный вычет
        /// </summary>
        [Column(Name="SOCIAL_NOTIFY_DATE")]
        public DateTime? SocialNotifyDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => SocialNotifyDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => SocialNotifyDate, value);
            }
        }
        /// <summary>
        /// Код уведомления дающего право на социальный вычет
        /// </summary>
        [Column(Name="SOCIAL_NOTIFY_CODE")]
        public String SocialNotifyCode
        {
            get
            {
                return this.GetDataRowField<String>(() => SocialNotifyCode);
            }
            set
            {
                UpdateDataRow<String>(() => SocialNotifyCode, value);
            }
        }
        /// <summary>
        /// Номер уведомления дающего право на имущественный вычет
        /// </summary>
        [Column(Name="ESTATE_NOTIFY_NUMBER")]
        public String EstateNotifyNumber
        {
            get
            {
                return this.GetDataRowField<String>(() => EstateNotifyNumber);
            }
            set
            {
                UpdateDataRow<String>(() => EstateNotifyNumber, value);
            }
        }
        /// <summary>
        /// Дата уведомления дающего право на имущественный вычет
        /// </summary>
        [Column(Name="ESTATE_NOTIFY_DATE")]
        public DateTime? EstateNotifyDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => EstateNotifyDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => EstateNotifyDate, value);
            }
        }
        /// <summary>
        /// Код уведомления дающего право на имущественный вычет
        /// </summary>
        [Column(Name="ESTATE_NOTIFY_CODE")]
        public String EstateNotifyCode
        {
            get
            {
                return this.GetDataRowField<String>(() => EstateNotifyCode);
            }
            set
            {
                UpdateDataRow<String>(() => EstateNotifyCode, value);
            }
        }
        /// <summary>
        /// Номер уведомления дающего право на уменьшение фиксированных авансовых платежей
        /// </summary>
        [Column(Name="ADVANCE_PAY_NOTIFY_NUMBER")]
        public String AdvancePayNotifyNumber
        {
            get
            {
                return this.GetDataRowField<String>(() => AdvancePayNotifyNumber);
            }
            set
            {
                UpdateDataRow<String>(() => AdvancePayNotifyNumber, value);
            }
        }
        /// <summary>
        /// Дата уведомления дающего право на уменьшение фиксированных авансовых платежей
        /// </summary>
        [Column(Name="ADVANCE_PAY_NOTIFY_DATE")]
        public DateTime? AdvancePayNotifyDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => AdvancePayNotifyDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => AdvancePayNotifyDate, value);
            }
        }
        /// <summary>
        /// Код уведомления дающего право на уменьшение фиксированных авансовых платежей
        /// </summary>
        [Column(Name="ADVANCE_PAY_NOTIFY_CODE")]
        public String AdvancePayNotifyCode
        {
            get
            {
                return this.GetDataRowField<String>(() => AdvancePayNotifyCode);
            }
            set
            {
                UpdateDataRow<String>(() => AdvancePayNotifyCode, value);
            }
        }
        /// <summary>
        /// Ссылка на организацию
        /// </summary>
        [Column(Name="TAX_COMPANY_ID")]
        public Decimal? TaxCompanyID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TaxCompanyID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TaxCompanyID, value);
            }
        }
        /// <summary>
        /// Номер справки
        /// </summary>
        [Column(Name="DOCUM_NUMBER")]
        public Decimal? DocumNumber
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DocumNumber);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DocumNumber, value);
            }
        }
        /// <summary>
        /// Признак справки
        /// </summary>
        [Column(Name="DOCUM_SIGN")]
        public Decimal? DocumSign
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DocumSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DocumSign, value);
            }
        }
        /// <summary>
        /// Номер корректировки
        /// </summary>
        [Column(Name="CORR_NUMBER")]
        public Decimal? CorrNumber
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CorrNumber);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CorrNumber, value);
            }
        }
        /// <summary>
        /// Признак блокировки - не обновлять при сбросе данных данный документ
        /// </summary>
        [Column(Name="LOCK_SIGN")]
        public Decimal? LockSign
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => LockSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => LockSign, value);
            }
        }
        /// <summary>
        /// Сумма налога удержанная
        /// </summary>
        [Column(Name="RETENT_TAX")]
        public Decimal? RetentTax
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => RetentTax);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentTax, value);
            }
        }
        /// <summary>
        /// Сумма налога перечисленная
        /// </summary>
        [Column(Name="TRANSFERED_TAX")]
        public Decimal? TransferedTax
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TransferedTax);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TransferedTax, value);
            }
        }
        /// <summary>
        /// Код страны гражданства
        /// </summary>
        [Column(Name="CODE_COUNTRY")]
        public String CodeCountry
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeCountry);
            }
            set
            {
                UpdateDataRow<String>(() => CodeCountry, value);
            }
        }
        /// <summary>
        /// Сумма рассчитанного налога
        /// </summary>
        [Column(Name="CALCED_TAX")]
        public Decimal? CalcedTax
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => CalcedTax);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CalcedTax, value);
            }
        }
        /// <summary>
        /// Отчетный период документа (последний месяц)
        /// </summary>
        [Column(Name="DOCUM_DATE")]
        public DateTime? DocumDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DocumDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DocumDate, value);
            }
        }
        [Column(Name="UPDATE_DATE")]
        public DateTime? UpdateDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => UpdateDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => UpdateDate, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Налоговый вычеты по документу
    /// </summary>

    [Table(Name="TAX_DOCUM_DISCOUNT"), SchemaName("")]
    public partial class TaxDocumDiscount : RowEntityBase
    {
        #region Class Members
        [Column(Name="TAX_DOCUM_DISCOUNT_ID")]
        public Decimal? TaxDocumDiscountID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TaxDocumDiscountID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TaxDocumDiscountID, value);
            }
        }
        [Column(Name="TAX_EMP_DOCUM_ID")]
        public Decimal? TaxEmpDocumID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TaxEmpDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TaxEmpDocumID, value);
            }
        }
        [Column(Name="CODE_DISCOUNT")]
        public String CodeDiscount
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeDiscount);
            }
            set
            {
                UpdateDataRow<String>(() => CodeDiscount, value);
            }
        }
        [Column(Name="SUM_DISCOUNT")]
        public Decimal? SumDiscount
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SumDiscount);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SumDiscount, value);
            }
        }
        [Column(Name="DATE_DISCOUNT")]
        public DateTime? DateDiscount
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateDiscount);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateDiscount, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Выплаты по документу
    /// </summary>

    [Table(Name="TAX_DOCUM_PAYMENT"), SchemaName("")]
    public partial class TaxDocumPayment : RowEntityBase
    {
        #region Class Members
        [Column(Name="TAX_DOCUM_PAYMENT_ID")]
        public Decimal? TaxDocumPaymentID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TaxDocumPaymentID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TaxDocumPaymentID, value);
            }
        }
        [Column(Name="TAX_EMP_DOCUM_ID")]
        public Decimal? TaxEmpDocumID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TaxEmpDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TaxEmpDocumID, value);
            }
        }
        [Column(Name="PAY_DATE")]
        public DateTime? PayDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => PayDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => PayDate, value);
            }
        }
        [Column(Name="PAY_CODE")]
        public String PayCode
        {
            get
            {
                return this.GetDataRowField<String>(() => PayCode);
            }
            set
            {
                UpdateDataRow<String>(() => PayCode, value);
            }
        }
        [Column(Name="SUM_SAL")]
        public Decimal? SumSal
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SumSal);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SumSal, value);
            }
        }
        [Column(Name="CODE_DISC")]
        public String CodeDisc
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeDisc);
            }
            set
            {
                UpdateDataRow<String>(() => CodeDisc, value);
            }
        }
        [Column(Name="SUM_DISC")]
        public Decimal? SumDisc
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SumDisc);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SumDisc, value);
            }
        }
                #endregion
    }

    /// <summary>
    /// Тип реестра
    /// </summary>

    [Table(Name="TYPE_CARTULARY"), SchemaName("SALARY")]
    public partial class TypeCartulary : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный номер типа реестра
        /// </summary>
        [Column(Name="TYPE_CARTULARY_ID", CanBeNull=false)]
        public Decimal? TypeCartularyID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeCartularyID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeCartularyID, value);
            }
        }
        /// <summary>
        /// Наименование типа реестра
        /// </summary>
        [Column(Name="TYPE_CARTULARY_NAME")]
        public String TypeCartularyName
        {
            get
            {
                return this.GetDataRowField<String>(() => TypeCartularyName);
            }
            set
            {
                UpdateDataRow<String>(() => TypeCartularyName, value);
            }
        }
        /// <summary>
        /// Сортировка просмотра реестров
        /// </summary>
        [Column(Name="SORT_CARTULARY")]
        public Decimal? SortCartulary
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SortCartulary);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SortCartulary, value);
            }
        }
        /// <summary>
        /// Признак того что реестр не имеет счета перечисления
        /// </summary>
        [Column(Name="SIGN_NO_ACCOUNT")]
        public Decimal? SignNoAccount
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SignNoAccount);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SignNoAccount, value);
            }
        }
        /// <summary>
        /// Группа типа реестра
        /// </summary>
        [Column(Name="TYPE_GROUP_CARTULARY_ID")]
        public Decimal? TypeGroupCartularyID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeGroupCartularyID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeGroupCartularyID, value);
            }
        }
        /// <summary>
        /// Статья затрат
        /// </summary>
        [Column(Name="PAY_CODE")]
        public String PayCode
        {
            get
            {
                return this.GetDataRowField<String>(() => PayCode);
            }
            set
            {
                UpdateDataRow<String>(() => PayCode, value);
            }
        }
        /// <summary>
        /// Текстовое сообщение при перечислении
        /// </summary>
        [Column(Name="TRANSFER_MESSAGE")]
        public String TransferMessage
        {
            get
            {
                return this.GetDataRowField<String>(() => TransferMessage);
            }
            set
            {
                UpdateDataRow<String>(() => TransferMessage, value);
            }
        }
        /// <summary>
        /// Балансовый счет
        /// </summary>
        [Column(Name="BALANCE_ACCOUNT")]
        public String BalanceAccount
        {
            get
            {
                return this.GetDataRowField<String>(() => BalanceAccount);
            }
            set
            {
                UpdateDataRow<String>(() => BalanceAccount, value);
            }
        }
                #endregion
    }


    [Table(Name="DOCUM_TRANSFER"), SchemaName("SALARY")]
    public partial class DocumTransfer : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный номер приказа перечисления
        /// </summary>
        [Column(Name="DOCUM_TRANSFER_ID", CanBeNull=false)]
        public Decimal? DocumTransferID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DocumTransferID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DocumTransferID, value);
            }
        }
        /// <summary>
        /// Дата приказа
        /// </summary>
        [Column(Name="DATE_DOCUM", CanBeNull=false)]
        public DateTime? DateDocum
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => DateDocum);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateDocum, value);
            }
        }
        /// <summary>
        /// Номер приказа
        /// </summary>
        [Column(Name="CODE_DOCUM")]
        public String CodeDocum
        {
            get
            {
                return this.GetDataRowField<String>(() => CodeDocum);
            }
            set
            {
                UpdateDataRow<String>(() => CodeDocum, value);
            }
        }
        /// <summary>
        /// Ссылка на тип реестра для котого формируется приказ
        /// </summary>
        [Column(Name="TYPE_CARTULARY_ID", CanBeNull=false)]
        public Decimal? TypeCartularyID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => TypeCartularyID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeCartularyID, value);
            }
        }
        [Column(Name="DOCUM_COMMENT")]
        public String DocumComment
        {
            get
            {
                return this.GetDataRowField<String>(() => DocumComment);
            }
            set
            {
                UpdateDataRow<String>(() => DocumComment, value);
            }
        }
                #endregion
    }


    [Table(Name="DOCUM_TRANSFER_RELATION"), SchemaName("SALARY")]
    public partial class DocumTransferRelation : RowEntityBase
    {
        #region Class Members
        [Column(Name="DOCUM_TRANSFER_RELATION_ID")]
        public Decimal? DocumTransferRelationID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DocumTransferRelationID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DocumTransferRelationID, value);
            }
        }
        /// <summary>
        /// Ссылка на документ перечисления
        /// </summary>
        [Column(Name="DOCUM_TRANSFER_ID", CanBeNull=false)]
        public Decimal? DocumTransferID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => DocumTransferID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DocumTransferID, value);
            }
        }
        /// <summary>
        /// Дата отчета (когда отчитаться)
        /// </summary>
        [Column(Name="CHECK_DATE", CanBeNull=false)]
        public DateTime? CheckDate
        {
            get
            {
                return this.GetDataRowField<DateTime?>(() => CheckDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => CheckDate, value);
            }
        }
        /// <summary>
        /// Статья фин плана
        /// </summary>
        [Column(Name="FIN_PLAN_CODE")]
        public String FinPlanCode
        {
            get
            {
                return this.GetDataRowField<String>(() => FinPlanCode);
            }
            set
            {
                UpdateDataRow<String>(() => FinPlanCode, value);
            }
        }
        /// <summary>
        /// Ссылка на зарплату
        /// </summary>
        [Column(Name="SALARY_ID", CanBeNull=false)]
        public Decimal? SalaryID
        {
            get
            {
                return this.GetDataRowField<Decimal?>(() => SalaryID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryID, value);
            }
        }
        /// <summary>
        /// Ссылка на перечисляемый счет сотрудника
        /// </summary>
        [Column(Name="CLIENT_ACCOUNT_ID", CanBeNull=false)]
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
                #endregion
    }

}
