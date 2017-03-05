using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.Mapping;
using Oracle.DataAccess.Client;

namespace EntityGenerator
{
    public partial class SalaryDocum
    {
        /// <summary>
        /// Сссылка на тип документа
        /// </summary>
        public TypeSalDocum TypeSalDocum
        {
            get
            {
                if (DataSet != null && DataSet.Tables.Contains("TYPE_SAL_DOCUM") && TypeSalDocumID != null)
                    return DataSet.Tables["TYPE_SAL_DOCUM"].Rows.OfType<DataRow>().Where(r => r.Field2<decimal?>("TYPE_SAL_DOCUM_ID") == TypeSalDocumID).Select(r => new TypeSalDocum() { DataRow = r }).FirstOrDefault();
                else
                    return null;
            }
        }
    }

    /// <summary>
    /// Адрес сотрудника
    /// </summary>
    public partial class EmpAddress: AddressNoneKladr
    {

        public EmpAddress()
            : base()
        { 
        }

        public EmpAddress(DataRow r)
            : base()
        {
            DataRow = r;
        }

        /// <summary>
        /// Номер дома
        /// </summary>
        [Column(Name="HOUSE")]
        public string House
        {
            get
            {
                return this.GetDataRowField<string>(() => House);
            }
            set
            {
                this.UpdateDataRow<string>(() => House, value);
            }
        }

        /// <summary>
        /// Номер блока или корпуса
        /// </summary>
        [Column(Name = "BULK")]
        public string Bulk
        {
            get
            {
                return this.GetDataRowField<string>(() => Bulk);
            }
            set
            {
                this.UpdateDataRow<string>(() => Bulk, value);
            }
        }

        /// <summary>
        /// Номер квартиры
        /// </summary>
        [Column(Name = "FLAT")]
        public string Flat
        {
            get
            {
                return this.GetDataRowField<string>(() => Flat);
            }
            set
            {
                this.UpdateDataRow<string>(() => Flat, value);
            }
        }

        /// <summary>
        /// Индекс
        /// </summary>
        [Column(Name = "POST_CODE")]
        public string PostCode
        {
            get
            {
                return this.GetDataRowField<string>(() => PostCode);
            }
            set
            {
                this.UpdateDataRow<string>(() => PostCode, value);
            }
        }

        /// <summary>
        /// Код региона
        /// </summary>
        [Column(Name = "CODE_REGION")]
        public string CodeRegion
        {
            get
            {
                return this.GetDataRowField<string>(() => CodeRegion);
            }
            set
            {
                this.UpdateDataRow<string>(() => CodeRegion, value);
            }
        }

        /// <summary>
        /// Тип адреса
        /// </summary>
        [Column(Name = "TYPE_ADDRESS")]
        public AddressType TypeAddress
        {
            get
            {
                return (AddressType)(Convert.ToInt32(this.GetDataRowField<Decimal>("TYPE_ADDRESS")));
            }
        }
    }

    /// <summary>
    /// Перечисление типов адресов сотрудников
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// Адрес по прописке
        /// </summary>
        HomeAddress = 1,
        /// <summary>
        /// Адрес фактического проживания
        /// </summary>
        LifeAddress =2, 
        /// <summary>
        /// Адрес не указанный в КЛАДР
        /// </summary>
        NoneKladr=3
    }

    public partial class Transfer
    {
        /// <summary>
        /// Подразделение код для перевода
        /// </summary>
        [Column(Name="CODE_SUBDIV")]
        public string CodeSubdiv
        {
            get
            {
                if (SubdivID==null) 
                    return null;
                if (DataSet.Tables.Contains("SUBDIV"))
                    return DataSet.Tables["SUBDIV"].Select("SUBDIV_ID="+SubdivID).Select(r=>r.Field2<string>("CODE_SUBDIV")).FirstOrDefault();
                else
                    return this.GetDataRowField<string>(() => CodeSubdiv);
            }
        }

        /// <summary>
        /// Должность подразделения
        /// </summary>
        [Column(Name = "POS_NAME")]
        public string PosName
        {
            get
            {
                if (PosID==null)
                    return null;
                if (DataSet.Tables.Contains("POSITION"))
                    return DataSet.Tables["POSITION"].Select("POS_ID="+PosID).Select(r=>r.Field2<string>("POS_NAME")).FirstOrDefault();
                else
                    return this.GetDataRowField<string>(() => PosName);
            }
        }

        /// <summary>
        /// Бух данные сотрудника по переводу
        /// </summary>
        public AccountData AccountData
        {
            get
            {
                return DataSet.Tables["ACCOUNT_DATA"].Rows.OfType<DataRow>().Select(r => new AccountData() { DataRow = r }).Where(r => r.TransferID == this.TransferID).FirstOrDefault();
            }
        }

        /// <summary>
        /// Наименование типа перевода
        /// </summary>
        public string TypeTransfer
        {
            get
            {
                if (TypeTransferID==null) return string.Empty;
                switch ((int)TypeTransferID.Value)
                {
                    case 1 : return "Приём";
                    case 2: return "Перевод";
                    case 3: return "Увольнение";
                    case 7: return "Работа по договору подряда";
                    case 8:return	"Ученики (61 категория)";
                }
                return  string.Empty;
            }
        }

    }

    public partial class AccountData
    {
        /// <summary>
        /// Список среди льготных профессий для доп. взносов по вредности
        /// </summary>
        [Column(Name="SPECIAL_CONDITIONS")]
        public string SpecialConditions
        {
            get
            {
                if (PrivilegedPositionID == null)
                    return null;
                if (DataSet.Tables.Contains("PRIVILEGED_POSITION"))
                    return DataSet.Tables["PRIVILEGED_POSITION"].Select("PRIVILEGED_POSITION_ID=" + PrivilegedPositionID).Select(r => r.Field2<string>("SPECIAL_CONDITIONS")).FirstOrDefault();
                else
                    return this.GetDataRowField<string>(() => SpecialConditions);
            }
        }

        /// <summary>
        /// Код тарифной сетки для бух данных
        /// </summary>
        [Column(Name = "CODE_TARIFF_GRID")]
        public string CodeTariffGrid
        {
            get
            {
                if (TariffGridID == null)
                    return null;
                if (DataSet.Tables.Contains("TARIFF_GRID"))
                    return DataSet.Tables["TARIFF_GRID"].Select("TARIFF_GRID_ID=" + TariffGridID).Select(r => r.Field2<string>("CODE_TARIFF_GRID")).FirstOrDefault();
                else
                    return this.GetDataRowField<string>(() => CodeTariffGrid);
            }
        }
    }

    public partial class TableBrigageModel:TableBrigage
    {
        /// <summary>
        /// ссылка на перевод сотрудника
        /// </summary>
        public EmpAccountData EmpData
        {
            get
            {
                if (this.DataSet.Tables.Contains("TRANSFER"))
                    return DataSet.Tables["TRANSFER"].Rows.OfType<DataRow>().Select(r => new EmpAccountData() { DataRow = r }).Where(r => r.TransferID == this.TransferID).FirstOrDefault();
                return null;
            }
        }

        /// <summary>
        /// Перевод сотрудника
        /// </summary>
        public new decimal? TransferID
        {
            get
            {
                return base.TransferID;
            }
            set
            {
                base.TransferID = value;
                RaisePropertyChanged(() => EmpData);
            }
        }

        /// <summary>
        /// Часы за месяц по нарядам сотрудника (по КТУ)
        /// </summary>
        [Column(Name="PIECE_HOURS")]
        public decimal? PieceHours
        {
            get
            {
                return GetDataRowField<decimal?>(() => PieceHours);
            }
        }

        /// <summary>
        /// Сумма за месяц по нарядам сотрудника (по КТУ)
        /// </summary>
        [Column(Name="PIECE_SUM")]
        public decimal? PieceSum
        {
            get
            {
                return GetDataRowField<decimal?>(() => PieceSum);
            }
        }
    }

    public partial class EmpAccountData
    {
        /// <summary>
        /// Фамилия И.О. сотрудника
        /// </summary>
        public string FIO
        {
            get
            {
                return string.Format("{0} {1}.{2}.", this.EmpLastName, this.EmpFirstName.Substring(0, 1), 
                    this.EmpMiddleName.Length==0?"":this.EmpMiddleName.Substring(0, 1));
            }
        }
    }

    public partial class Brigage
    {
        /// <summary>
        /// Подразделение для бригады
        /// </summary>
        public Subdiv Subdiv
        {
            get
            {
                return GetParentEntity<Subdiv, decimal?>(()=>SubdivID);
            }
        }
    }

    public partial class SubdivForClose
    {
        /// <summary>
        /// Подразделение для закрытия
        /// </summary>
        public Subdiv Subdiv
        {
            get
            {
                return GetParentEntity<Subdiv, decimal?>(()=>SubdivID);
            }
        }

        /// <summary>
        /// Доступно ли редактирование закрытия записи текущей
        /// </summary>
        public bool IsEditableProcessing
        {
            get
            {
                if (AppName=="SALARY")
                {
                    return  DataSet.Tables["ACCESS_SUBDIV"].Select(string.Format("APP_NAME='SALARY' and SUBDIV_ID={0}", SubdivID)).Length > 0;
                }
                else if (AppName=="PIECE_WORK")
                {
                }
                return false;
            }
        }
    }

    public partial class TaxCompany
    {
        /// <summary>
        /// Конструктор с созданием адаптера сохранения для модели
        /// </summary>
        /// <param name="connect"></param>
        public TaxCompany(OracleConnection connect)
        {
            this.AdapterConnection = connect;
            DataAdapter = new OracleDataAdapter("select * from SALARY.TAX_COMPANY", connect);
            DataAdapter.InsertCommand = new OracleCommand(string.Format(@"BEGIN {1}.TAX_COMPANY_UPDATE(p_TAX_COMPANY_ID=>:p_TAX_COMPANY_ID,p_COMPANY_NAME=>:p_COMPANY_NAME,p_OKTMO=>:p_OKTMO,p_TEL=>:p_TEL,p_INN=>:p_INN,p_KPP=>:p_KPP,p_AGENT_STATUS=>:p_AGENT_STATUS,p_AGENT_NAME=>:p_AGENT_NAME,p_DATE_BEGIN=>:p_DATE_BEGIN,p_DATE_END=>:p_DATE_END,p_COMMENTS=>:p_COMMENTS,p_SHORT_COMPANY_NAME=>:p_SHORT_COMPANY_NAME,p_AGENT_DOCUMENT=>:p_AGENT_DOCUMENT);end;", "APSTAFF", "SALARY"), AdapterConnection);
            DataAdapter.InsertCommand.BindByName = true;
            DataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            DataAdapter.InsertCommand.Parameters.Add("p_TAX_COMPANY_ID", OracleDbType.Decimal, 0, "TAX_COMPANY_ID").Direction = ParameterDirection.InputOutput;
            DataAdapter.InsertCommand.Parameters["p_TAX_COMPANY_ID"].DbType = DbType.Decimal;
            DataAdapter.InsertCommand.Parameters.Add("p_COMPANY_NAME", OracleDbType.Varchar2, 0, "COMPANY_NAME").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_OKTMO", OracleDbType.Varchar2, 0, "OKTMO").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_TEL", OracleDbType.Varchar2, 0, "TEL").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_INN", OracleDbType.Varchar2, 0, "INN").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_KPP", OracleDbType.Varchar2, 0, "KPP").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_AGENT_STATUS", OracleDbType.Varchar2, 0, "AGENT_STATUS").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_AGENT_NAME", OracleDbType.Varchar2, 0, "AGENT_NAME").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_DATE_BEGIN", OracleDbType.Date, 0, "DATE_BEGIN").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_DATE_END", OracleDbType.Date, 0, "DATE_END").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_COMMENTS", OracleDbType.Varchar2, 0, "COMMENTS").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_SHORT_COMPANY_NAME", OracleDbType.Varchar2, 0, "SHORT_COMPANY_NAME").Direction = ParameterDirection.Input;
            DataAdapter.InsertCommand.Parameters.Add("p_AGENT_DOCUMENT", OracleDbType.Varchar2, 0, "AGENT_DOCUMENT").Direction = ParameterDirection.Input;

            DataAdapter.UpdateCommand = new OracleCommand(string.Format(@"BEGIN {1}.TAX_COMPANY_UPDATE(p_TAX_COMPANY_ID=>:p_TAX_COMPANY_ID,p_COMPANY_NAME=>:p_COMPANY_NAME,p_OKTMO=>:p_OKTMO,p_TEL=>:p_TEL,p_INN=>:p_INN,p_KPP=>:p_KPP,p_AGENT_STATUS=>:p_AGENT_STATUS,p_AGENT_NAME=>:p_AGENT_NAME,p_DATE_BEGIN=>:p_DATE_BEGIN,p_DATE_END=>:p_DATE_END,p_COMMENTS=>:p_COMMENTS,p_SHORT_COMPANY_NAME=>:p_SHORT_COMPANY_NAME,p_AGENT_DOCUMENT=>:p_AGENT_DOCUMENT);end;", "APSTAFF", "SALARY"), AdapterConnection);
            DataAdapter.UpdateCommand.BindByName = true;
            DataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            DataAdapter.UpdateCommand.Parameters.Add("p_TAX_COMPANY_ID", OracleDbType.Decimal, 0, "TAX_COMPANY_ID").Direction = ParameterDirection.InputOutput;
            DataAdapter.UpdateCommand.Parameters["p_TAX_COMPANY_ID"].DbType = DbType.Decimal;
            DataAdapter.UpdateCommand.Parameters.Add("p_COMPANY_NAME", OracleDbType.Varchar2, 0, "COMPANY_NAME").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_OKTMO", OracleDbType.Varchar2, 0, "OKTMO").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_TEL", OracleDbType.Varchar2, 0, "TEL").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_INN", OracleDbType.Varchar2, 0, "INN").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_KPP", OracleDbType.Varchar2, 0, "KPP").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_AGENT_STATUS", OracleDbType.Varchar2, 0, "AGENT_STATUS").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_AGENT_NAME", OracleDbType.Varchar2, 0, "AGENT_NAME").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_DATE_BEGIN", OracleDbType.Date, 0, "DATE_BEGIN").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_DATE_END", OracleDbType.Date, 0, "DATE_END").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_COMMENTS", OracleDbType.Varchar2, 0, "COMMENTS").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_SHORT_COMPANY_NAME", OracleDbType.Varchar2, 0, "SHORT_COMPANY_NAME").Direction = ParameterDirection.Input;
            DataAdapter.UpdateCommand.Parameters.Add("p_AGENT_DOCUMENT", OracleDbType.Varchar2, 0, "AGENT_DOCUMENT").Direction = ParameterDirection.Input;

            DataAdapter.DeleteCommand = new OracleCommand(string.Format(@"BEGIN {1}.TAX_COMPANY_DELETE(:p_TAX_COMPANY_ID);end;", "APSTAFF", "SALARY"), AdapterConnection);
            DataAdapter.DeleteCommand.BindByName = true;
            DataAdapter.DeleteCommand.Parameters.Add("p_TAX_COMPANY_ID", OracleDbType.Decimal, 0, "TAX_COMPANY_ID").Direction = ParameterDirection.InputOutput;

        }

        public TaxCompany() : base()
        { 

        }
    }


    public partial class TaxDocumPayment
    {
        public int? PayDateMonth
        {
            get
            {
                return PayDate?.Month;
            }
            set
            {
                if (value == null)
                    PayDate = null;
                else
                    PayDate = new DateTime(PayDate.Value.Year, value.Value, PayDate.Value.Day);
            }
        }
    }
}
