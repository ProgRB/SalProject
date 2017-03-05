/***********************************************************/
/**********   Generated at 20.02.2017 14:26:55     ********/
/*********************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Data;
using Oracle.DataAccess.Client;
using System.Data.Linq.Mapping;

namespace EntityGenerator
{
    
    [Table(Name="ACCOUNT_DATA"), SchemaName("APSTAFF")]
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
        		return GetDataRowField<Decimal?>("ACCOUNT_DATA_ID");
                //return this.GetDataRowField<Decimal?>(() => AccountDataID);
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
        		return GetDataRowField<Double?>("HARMFUL_ADDITION_ADD");
                //return this.GetDataRowField<Double?>(() => HarmfulAdditionAdd);
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
        		return GetDataRowField<Single?>("ENCODING_ADDITION");
                //return this.GetDataRowField<Single?>(() => EncodingAddition);
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
        		return GetDataRowField<Decimal?>("PREMIUM_CALC_GROUP_ID");
                //return this.GetDataRowField<Decimal?>(() => PremiumCalcGroupID);
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
        		return GetDataRowField<Double?>("GOVSECRET_ADDITION");
                //return this.GetDataRowField<Double?>(() => GovsecretAddition);
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
        		return GetDataRowField<Decimal?>("PRIVILEGED_POSITION_ID");
                //return this.GetDataRowField<Decimal?>(() => PrivilegedPositionID);
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
        		return GetDataRowField<Int16?>("WATER_PROC");
                //return this.GetDataRowField<Int16?>(() => WaterProc);
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
        		return GetDataRowField<Decimal?>("SALARY_MISSION");
                //return this.GetDataRowField<Decimal?>(() => SalaryMission);
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
        		return GetDataRowField<Int16?>("SERVICE_ADD");
                //return this.GetDataRowField<Int16?>(() => ServiceAdd);
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
        		return GetDataRowField<Single?>("TRIP_ADDITION");
                //return this.GetDataRowField<Single?>(() => TripAddition);
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
        		return GetDataRowField<Int16?>("COUNT_DEP20");
                //return this.GetDataRowField<Int16?>(() => CountDep20);
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
        		return GetDataRowField<Int16?>("COUNT_DEP19");
                //return this.GetDataRowField<Int16?>(() => CountDep19);
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
        		return GetDataRowField<Int16?>("COUNT_DEP16");
                //return this.GetDataRowField<Int16?>(() => CountDep16);
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
        		return GetDataRowField<Int16?>("COUNT_DEP15");
                //return this.GetDataRowField<Int16?>(() => CountDep15);
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
        		return GetDataRowField<DateTime?>("DATE_ADD_AGREE");
                //return this.GetDataRowField<DateTime?>(() => DateAddAgree);
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
        		return GetDataRowField<DateTime?>("CHANGE_DATE");
                //return this.GetDataRowField<DateTime?>(() => ChangeDate);
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
        		return GetDataRowField<DateTime?>("DATE_END_YOUNG_SPEC");
                //return this.GetDataRowField<DateTime?>(() => DateEndYoungSpec);
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
        		return GetDataRowField<Double?>("SUM_YOUNG_SPEC");
                //return this.GetDataRowField<Double?>(() => SumYoungSpec);
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
        		return GetDataRowField<Double?>("CHIEF_ADDITION");
                //return this.GetDataRowField<Double?>(() => ChiefAddition);
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
        		return GetDataRowField<Double?>("CLASS_ADDITION");
                //return this.GetDataRowField<Double?>(() => ClassAddition);
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
        		return GetDataRowField<Double?>("SECRET_ADDITION");
                //return this.GetDataRowField<Double?>(() => SecretAddition);
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
        		return GetDataRowField<Int16?>("COUNT_DEP21");
                //return this.GetDataRowField<Int16?>(() => CountDep21);
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
        		return GetDataRowField<Int16?>("COUNT_DEP18");
                //return this.GetDataRowField<Int16?>(() => CountDep18);
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
        		return GetDataRowField<Int16?>("COUNT_DEP17");
                //return this.GetDataRowField<Int16?>(() => CountDep17);
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
        		return GetDataRowField<Int16?>("COUNT_DEP14");
                //return this.GetDataRowField<Int16?>(() => CountDep14);
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
        		return GetDataRowField<DateTime?>("DATE_ADD");
                //return this.GetDataRowField<DateTime?>(() => DateAdd);
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
        		return GetDataRowField<Decimal?>("SIGN_ADD");
                //return this.GetDataRowField<Decimal?>(() => SignAdd);
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
        		return GetDataRowField<Decimal?>("TARIFF_GRID_ID");
                //return this.GetDataRowField<Decimal?>(() => TariffGridID);
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
        		return GetDataRowField<Decimal?>("CLASSIFIC");
                //return this.GetDataRowField<Decimal?>(() => Classific);
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
        		return GetDataRowField<Single?>("SALARY");
                //return this.GetDataRowField<Single?>(() => Salary);
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
        		return GetDataRowField<Decimal?>("PERCENT13");
                //return this.GetDataRowField<Decimal?>(() => Percent13);
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
        		return GetDataRowField<Double?>("COMB_ADDITION");
                //return this.GetDataRowField<Double?>(() => CombAddition);
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
        		return GetDataRowField<Double?>("HARMFUL_ADDITION");
                //return this.GetDataRowField<Double?>(() => HarmfulAddition);
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
        		return GetDataRowField<Double?>("PROF_ADDITION");
                //return this.GetDataRowField<Double?>(() => ProfAddition);
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
        		return GetDataRowField<DateTime?>("DATE_SERVANT");
                //return this.GetDataRowField<DateTime?>(() => DateServant);
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
        		return GetDataRowField<Int16?>("TAX_CODE");
                //return this.GetDataRowField<Int16?>(() => TaxCode);
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
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TransferID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.ACCOUNT_DATA_UPDATE(p_ACCOUNT_DATA_ID=>:p_ACCOUNT_DATA_ID, p_HARMFUL_ADDITION_ADD=>:p_HARMFUL_ADDITION_ADD, p_ENCODING_ADDITION=>:p_ENCODING_ADDITION, p_PREMIUM_CALC_GROUP_ID=>:p_PREMIUM_CALC_GROUP_ID, p_GOVSECRET_ADDITION=>:p_GOVSECRET_ADDITION, p_PRIVILEGED_POSITION_ID=>:p_PRIVILEGED_POSITION_ID, p_WATER_PROC=>:p_WATER_PROC, p_SALARY_MISSION=>:p_SALARY_MISSION, p_SERVICE_ADD=>:p_SERVICE_ADD, p_TRIP_ADDITION=>:p_TRIP_ADDITION, p_COUNT_DEP20=>:p_COUNT_DEP20, p_COUNT_DEP19=>:p_COUNT_DEP19, p_COUNT_DEP16=>:p_COUNT_DEP16, p_COUNT_DEP15=>:p_COUNT_DEP15, p_DATE_ADD_AGREE=>:p_DATE_ADD_AGREE, p_CHANGE_DATE=>:p_CHANGE_DATE, p_DATE_END_YOUNG_SPEC=>:p_DATE_END_YOUNG_SPEC, p_SUM_YOUNG_SPEC=>:p_SUM_YOUNG_SPEC, p_CHIEF_ADDITION=>:p_CHIEF_ADDITION, p_CLASS_ADDITION=>:p_CLASS_ADDITION, p_SECRET_ADDITION=>:p_SECRET_ADDITION, p_COUNT_DEP21=>:p_COUNT_DEP21, p_COUNT_DEP18=>:p_COUNT_DEP18, p_COUNT_DEP17=>:p_COUNT_DEP17, p_COUNT_DEP14=>:p_COUNT_DEP14, p_DATE_ADD=>:p_DATE_ADD, p_SIGN_ADD=>:p_SIGN_ADD, p_TARIFF_GRID_ID=>:p_TARIFF_GRID_ID, p_CLASSIFIC=>:p_CLASSIFIC, p_SALARY=>:p_SALARY, p_PERCENT13=>:p_PERCENT13, p_COMB_ADDITION=>:p_COMB_ADDITION, p_HARMFUL_ADDITION=>:p_HARMFUL_ADDITION, p_PROF_ADDITION=>:p_PROF_ADDITION, p_DATE_SERVANT=>:p_DATE_SERVANT, p_TAX_CODE=>:p_TAX_CODE, p_TRANSFER_ID=>:p_TRANSFER_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_ACCOUNT_DATA_ID", OracleDbType.Decimal, 0, "ACCOUNT_DATA_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_ACCOUNT_DATA_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_HARMFUL_ADDITION_ADD", OracleDbType.Decimal, 0, "HARMFUL_ADDITION_ADD");
            _dataAdapter.InsertCommand.Parameters.Add("p_ENCODING_ADDITION", OracleDbType.Decimal, 0, "ENCODING_ADDITION");
            _dataAdapter.InsertCommand.Parameters.Add("p_PREMIUM_CALC_GROUP_ID", OracleDbType.Decimal, 0, "PREMIUM_CALC_GROUP_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_GOVSECRET_ADDITION", OracleDbType.Decimal, 0, "GOVSECRET_ADDITION");
            _dataAdapter.InsertCommand.Parameters.Add("p_PRIVILEGED_POSITION_ID", OracleDbType.Decimal, 0, "PRIVILEGED_POSITION_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_WATER_PROC", OracleDbType.Decimal, 0, "WATER_PROC");
            _dataAdapter.InsertCommand.Parameters.Add("p_SALARY_MISSION", OracleDbType.Decimal, 0, "SALARY_MISSION");
            _dataAdapter.InsertCommand.Parameters.Add("p_SERVICE_ADD", OracleDbType.Decimal, 0, "SERVICE_ADD");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRIP_ADDITION", OracleDbType.Decimal, 0, "TRIP_ADDITION");
            _dataAdapter.InsertCommand.Parameters.Add("p_COUNT_DEP20", OracleDbType.Decimal, 0, "COUNT_DEP20");
            _dataAdapter.InsertCommand.Parameters.Add("p_COUNT_DEP19", OracleDbType.Decimal, 0, "COUNT_DEP19");
            _dataAdapter.InsertCommand.Parameters.Add("p_COUNT_DEP16", OracleDbType.Decimal, 0, "COUNT_DEP16");
            _dataAdapter.InsertCommand.Parameters.Add("p_COUNT_DEP15", OracleDbType.Decimal, 0, "COUNT_DEP15");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_ADD_AGREE", OracleDbType.Date, 0, "DATE_ADD_AGREE");
            _dataAdapter.InsertCommand.Parameters.Add("p_CHANGE_DATE", OracleDbType.Date, 0, "CHANGE_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_END_YOUNG_SPEC", OracleDbType.Date, 0, "DATE_END_YOUNG_SPEC");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUM_YOUNG_SPEC", OracleDbType.Decimal, 0, "SUM_YOUNG_SPEC");
            _dataAdapter.InsertCommand.Parameters.Add("p_CHIEF_ADDITION", OracleDbType.Decimal, 0, "CHIEF_ADDITION");
            _dataAdapter.InsertCommand.Parameters.Add("p_CLASS_ADDITION", OracleDbType.Decimal, 0, "CLASS_ADDITION");
            _dataAdapter.InsertCommand.Parameters.Add("p_SECRET_ADDITION", OracleDbType.Decimal, 0, "SECRET_ADDITION");
            _dataAdapter.InsertCommand.Parameters.Add("p_COUNT_DEP21", OracleDbType.Decimal, 0, "COUNT_DEP21");
            _dataAdapter.InsertCommand.Parameters.Add("p_COUNT_DEP18", OracleDbType.Decimal, 0, "COUNT_DEP18");
            _dataAdapter.InsertCommand.Parameters.Add("p_COUNT_DEP17", OracleDbType.Decimal, 0, "COUNT_DEP17");
            _dataAdapter.InsertCommand.Parameters.Add("p_COUNT_DEP14", OracleDbType.Decimal, 0, "COUNT_DEP14");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_ADD", OracleDbType.Date, 0, "DATE_ADD");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_ADD", OracleDbType.Decimal, 0, "SIGN_ADD");
            _dataAdapter.InsertCommand.Parameters.Add("p_TARIFF_GRID_ID", OracleDbType.Decimal, 0, "TARIFF_GRID_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_CLASSIFIC", OracleDbType.Decimal, 0, "CLASSIFIC");
            _dataAdapter.InsertCommand.Parameters.Add("p_SALARY", OracleDbType.Decimal, 0, "SALARY");
            _dataAdapter.InsertCommand.Parameters.Add("p_PERCENT13", OracleDbType.Decimal, 0, "PERCENT13");
            _dataAdapter.InsertCommand.Parameters.Add("p_COMB_ADDITION", OracleDbType.Decimal, 0, "COMB_ADDITION");
            _dataAdapter.InsertCommand.Parameters.Add("p_HARMFUL_ADDITION", OracleDbType.Decimal, 0, "HARMFUL_ADDITION");
            _dataAdapter.InsertCommand.Parameters.Add("p_PROF_ADDITION", OracleDbType.Decimal, 0, "PROF_ADDITION");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_SERVANT", OracleDbType.Date, 0, "DATE_SERVANT");
            _dataAdapter.InsertCommand.Parameters.Add("p_TAX_CODE", OracleDbType.Decimal, 0, "TAX_CODE");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.ACCOUNT_DATA_UPDATE(p_ACCOUNT_DATA_ID=>:p_ACCOUNT_DATA_ID, p_HARMFUL_ADDITION_ADD=>:p_HARMFUL_ADDITION_ADD, p_ENCODING_ADDITION=>:p_ENCODING_ADDITION, p_PREMIUM_CALC_GROUP_ID=>:p_PREMIUM_CALC_GROUP_ID, p_GOVSECRET_ADDITION=>:p_GOVSECRET_ADDITION, p_PRIVILEGED_POSITION_ID=>:p_PRIVILEGED_POSITION_ID, p_WATER_PROC=>:p_WATER_PROC, p_SALARY_MISSION=>:p_SALARY_MISSION, p_SERVICE_ADD=>:p_SERVICE_ADD, p_TRIP_ADDITION=>:p_TRIP_ADDITION, p_COUNT_DEP20=>:p_COUNT_DEP20, p_COUNT_DEP19=>:p_COUNT_DEP19, p_COUNT_DEP16=>:p_COUNT_DEP16, p_COUNT_DEP15=>:p_COUNT_DEP15, p_DATE_ADD_AGREE=>:p_DATE_ADD_AGREE, p_CHANGE_DATE=>:p_CHANGE_DATE, p_DATE_END_YOUNG_SPEC=>:p_DATE_END_YOUNG_SPEC, p_SUM_YOUNG_SPEC=>:p_SUM_YOUNG_SPEC, p_CHIEF_ADDITION=>:p_CHIEF_ADDITION, p_CLASS_ADDITION=>:p_CLASS_ADDITION, p_SECRET_ADDITION=>:p_SECRET_ADDITION, p_COUNT_DEP21=>:p_COUNT_DEP21, p_COUNT_DEP18=>:p_COUNT_DEP18, p_COUNT_DEP17=>:p_COUNT_DEP17, p_COUNT_DEP14=>:p_COUNT_DEP14, p_DATE_ADD=>:p_DATE_ADD, p_SIGN_ADD=>:p_SIGN_ADD, p_TARIFF_GRID_ID=>:p_TARIFF_GRID_ID, p_CLASSIFIC=>:p_CLASSIFIC, p_SALARY=>:p_SALARY, p_PERCENT13=>:p_PERCENT13, p_COMB_ADDITION=>:p_COMB_ADDITION, p_HARMFUL_ADDITION=>:p_HARMFUL_ADDITION, p_PROF_ADDITION=>:p_PROF_ADDITION, p_DATE_SERVANT=>:p_DATE_SERVANT, p_TAX_CODE=>:p_TAX_CODE, p_TRANSFER_ID=>:p_TRANSFER_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_ACCOUNT_DATA_ID", OracleDbType.Decimal, 0, "ACCOUNT_DATA_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_ACCOUNT_DATA_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_HARMFUL_ADDITION_ADD", OracleDbType.Decimal, 0, "HARMFUL_ADDITION_ADD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ENCODING_ADDITION", OracleDbType.Decimal, 0, "ENCODING_ADDITION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PREMIUM_CALC_GROUP_ID", OracleDbType.Decimal, 0, "PREMIUM_CALC_GROUP_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_GOVSECRET_ADDITION", OracleDbType.Decimal, 0, "GOVSECRET_ADDITION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PRIVILEGED_POSITION_ID", OracleDbType.Decimal, 0, "PRIVILEGED_POSITION_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_WATER_PROC", OracleDbType.Decimal, 0, "WATER_PROC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_MISSION", OracleDbType.Decimal, 0, "SALARY_MISSION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SERVICE_ADD", OracleDbType.Decimal, 0, "SERVICE_ADD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRIP_ADDITION", OracleDbType.Decimal, 0, "TRIP_ADDITION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COUNT_DEP20", OracleDbType.Decimal, 0, "COUNT_DEP20");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COUNT_DEP19", OracleDbType.Decimal, 0, "COUNT_DEP19");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COUNT_DEP16", OracleDbType.Decimal, 0, "COUNT_DEP16");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COUNT_DEP15", OracleDbType.Decimal, 0, "COUNT_DEP15");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_ADD_AGREE", OracleDbType.Date, 0, "DATE_ADD_AGREE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CHANGE_DATE", OracleDbType.Date, 0, "CHANGE_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_END_YOUNG_SPEC", OracleDbType.Date, 0, "DATE_END_YOUNG_SPEC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUM_YOUNG_SPEC", OracleDbType.Decimal, 0, "SUM_YOUNG_SPEC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CHIEF_ADDITION", OracleDbType.Decimal, 0, "CHIEF_ADDITION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CLASS_ADDITION", OracleDbType.Decimal, 0, "CLASS_ADDITION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SECRET_ADDITION", OracleDbType.Decimal, 0, "SECRET_ADDITION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COUNT_DEP21", OracleDbType.Decimal, 0, "COUNT_DEP21");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COUNT_DEP18", OracleDbType.Decimal, 0, "COUNT_DEP18");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COUNT_DEP17", OracleDbType.Decimal, 0, "COUNT_DEP17");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COUNT_DEP14", OracleDbType.Decimal, 0, "COUNT_DEP14");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_ADD", OracleDbType.Date, 0, "DATE_ADD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_ADD", OracleDbType.Decimal, 0, "SIGN_ADD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TARIFF_GRID_ID", OracleDbType.Decimal, 0, "TARIFF_GRID_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CLASSIFIC", OracleDbType.Decimal, 0, "CLASSIFIC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SALARY", OracleDbType.Decimal, 0, "SALARY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PERCENT13", OracleDbType.Decimal, 0, "PERCENT13");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COMB_ADDITION", OracleDbType.Decimal, 0, "COMB_ADDITION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_HARMFUL_ADDITION", OracleDbType.Decimal, 0, "HARMFUL_ADDITION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PROF_ADDITION", OracleDbType.Decimal, 0, "PROF_ADDITION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_SERVANT", OracleDbType.Date, 0, "DATE_SERVANT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TAX_CODE", OracleDbType.Decimal, 0, "TAX_CODE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.ACCOUNT_DATA_DELETE(p_ACCOUNT_DATA_ID => :p_ACCOUNT_DATA_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_ACCOUNT_DATA_ID", OracleDbType.Decimal, 0, "ACCOUNT_DATA_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Адрес представление
    /// </summary>

    [Table(Name="ADDRESS"), SchemaName("APSTAFF")]
    public partial class Address : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Страна
        /// </summary>
        [Column(Name="COUNTRY")]
        public String Country
        {
            get
            {
        		return GetDataRowField<String>("COUNTRY");
                //return this.GetDataRowField<String>(() => Country);
            }
            set
            {
                UpdateDataRow<String>(() => Country, value);
            }
        }
        /// <summary>
        /// Регион
        /// </summary>
        [Column(Name="REGION")]
        public String Region
        {
            get
            {
        		return GetDataRowField<String>("REGION");
                //return this.GetDataRowField<String>(() => Region);
            }
            set
            {
                UpdateDataRow<String>(() => Region, value);
            }
        }
        /// <summary>
        /// Код региона
        /// </summary>
        [Column(Name="CODE_REGION")]
        public String CodeRegion
        {
            get
            {
        		return GetDataRowField<String>("CODE_REGION");
                //return this.GetDataRowField<String>(() => CodeRegion);
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
        		return GetDataRowField<String>("DISTRICT");
                //return this.GetDataRowField<String>(() => District);
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
        		return GetDataRowField<String>("CITY");
                //return this.GetDataRowField<String>(() => City);
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
        		return GetDataRowField<String>("LOCALITY");
                //return this.GetDataRowField<String>(() => Locality);
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
        		return GetDataRowField<String>("STREET");
                //return this.GetDataRowField<String>(() => Street);
            }
            set
            {
                UpdateDataRow<String>(() => Street, value);
            }
        }
        /// <summary>
        /// Номер дома
        /// </summary>
        [Column(Name="HOUSE")]
        public String House
        {
            get
            {
        		return GetDataRowField<String>("HOUSE");
                //return this.GetDataRowField<String>(() => House);
            }
            set
            {
                UpdateDataRow<String>(() => House, value);
            }
        }
        /// <summary>
        /// Блок
        /// </summary>
        [Column(Name="BULK")]
        public String Bulk
        {
            get
            {
        		return GetDataRowField<String>("BULK");
                //return this.GetDataRowField<String>(() => Bulk);
            }
            set
            {
                UpdateDataRow<String>(() => Bulk, value);
            }
        }
        /// <summary>
        /// Квартира
        /// </summary>
        [Column(Name="FLAT")]
        public String Flat
        {
            get
            {
        		return GetDataRowField<String>("FLAT");
                //return this.GetDataRowField<String>(() => Flat);
            }
            set
            {
                UpdateDataRow<String>(() => Flat, value);
            }
        }
        /// <summary>
        /// Индекс почтовый
        /// </summary>
        [Column(Name="POST_INDEX")]
        public String PostIndex
        {
            get
            {
        		return GetDataRowField<String>("POST_INDEX");
                //return this.GetDataRowField<String>(() => PostIndex);
            }
            set
            {
                UpdateDataRow<String>(() => PostIndex, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.ADDRESS_UPDATE(p_COUNTRY=>:p_COUNTRY, p_REGION=>:p_REGION, p_CODE_REGION=>:p_CODE_REGION, p_DISTRICT=>:p_DISTRICT, p_CITY=>:p_CITY, p_LOCALITY=>:p_LOCALITY, p_STREET=>:p_STREET, p_HOUSE=>:p_HOUSE, p_BULK=>:p_BULK, p_FLAT=>:p_FLAT, p_POST_INDEX=>:p_POST_INDEX);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_COUNTRY", OracleDbType.Varchar2, 0, "COUNTRY");
            _dataAdapter.InsertCommand.Parameters.Add("p_REGION", OracleDbType.Varchar2, 0, "REGION");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_REGION", OracleDbType.Varchar2, 0, "CODE_REGION");
            _dataAdapter.InsertCommand.Parameters.Add("p_DISTRICT", OracleDbType.Varchar2, 0, "DISTRICT");
            _dataAdapter.InsertCommand.Parameters.Add("p_CITY", OracleDbType.Varchar2, 0, "CITY");
            _dataAdapter.InsertCommand.Parameters.Add("p_LOCALITY", OracleDbType.Varchar2, 0, "LOCALITY");
            _dataAdapter.InsertCommand.Parameters.Add("p_STREET", OracleDbType.Varchar2, 0, "STREET");
            _dataAdapter.InsertCommand.Parameters.Add("p_HOUSE", OracleDbType.Varchar2, 0, "HOUSE");
            _dataAdapter.InsertCommand.Parameters.Add("p_BULK", OracleDbType.Varchar2, 0, "BULK");
            _dataAdapter.InsertCommand.Parameters.Add("p_FLAT", OracleDbType.Varchar2, 0, "FLAT");
            _dataAdapter.InsertCommand.Parameters.Add("p_POST_INDEX", OracleDbType.Varchar2, 0, "POST_INDEX");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.ADDRESS_UPDATE(p_COUNTRY=>:p_COUNTRY, p_REGION=>:p_REGION, p_CODE_REGION=>:p_CODE_REGION, p_DISTRICT=>:p_DISTRICT, p_CITY=>:p_CITY, p_LOCALITY=>:p_LOCALITY, p_STREET=>:p_STREET, p_HOUSE=>:p_HOUSE, p_BULK=>:p_BULK, p_FLAT=>:p_FLAT, p_POST_INDEX=>:p_POST_INDEX);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_COUNTRY", OracleDbType.Varchar2, 0, "COUNTRY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REGION", OracleDbType.Varchar2, 0, "REGION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_REGION", OracleDbType.Varchar2, 0, "CODE_REGION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DISTRICT", OracleDbType.Varchar2, 0, "DISTRICT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CITY", OracleDbType.Varchar2, 0, "CITY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_LOCALITY", OracleDbType.Varchar2, 0, "LOCALITY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_STREET", OracleDbType.Varchar2, 0, "STREET");
            _dataAdapter.UpdateCommand.Parameters.Add("p_HOUSE", OracleDbType.Varchar2, 0, "HOUSE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BULK", OracleDbType.Varchar2, 0, "BULK");
            _dataAdapter.UpdateCommand.Parameters.Add("p_FLAT", OracleDbType.Varchar2, 0, "FLAT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_POST_INDEX", OracleDbType.Varchar2, 0, "POST_INDEX");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.ADDRESS_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        		}
		
	#endregion
    }


    [Table(Name="ADDRESS_NONE_KLADR"), SchemaName("APSTAFF")]
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
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
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
        		return GetDataRowField<String>("NAME_STREET");
                //return this.GetDataRowField<String>(() => NameStreet);
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
        		return GetDataRowField<String>("NAME_LOCALITY");
                //return this.GetDataRowField<String>(() => NameLocality);
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
        		return GetDataRowField<String>("NAME_CITY");
                //return this.GetDataRowField<String>(() => NameCity);
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
        		return GetDataRowField<String>("NAME_DISTRICT");
                //return this.GetDataRowField<String>(() => NameDistrict);
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
        		return GetDataRowField<String>("NAME_REGION");
                //return this.GetDataRowField<String>(() => NameRegion);
            }
            set
            {
                UpdateDataRow<String>(() => NameRegion, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.ADDRESS_NONE_KLADR_UPDATE(p_PER_NUM=>:p_PER_NUM, p_NAME_STREET=>:p_NAME_STREET, p_NAME_LOCALITY=>:p_NAME_LOCALITY, p_NAME_CITY=>:p_NAME_CITY, p_NAME_DISTRICT=>:p_NAME_DISTRICT, p_NAME_REGION=>:p_NAME_REGION);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_NAME_STREET", OracleDbType.Varchar2, 0, "NAME_STREET");
            _dataAdapter.InsertCommand.Parameters.Add("p_NAME_LOCALITY", OracleDbType.Varchar2, 0, "NAME_LOCALITY");
            _dataAdapter.InsertCommand.Parameters.Add("p_NAME_CITY", OracleDbType.Varchar2, 0, "NAME_CITY");
            _dataAdapter.InsertCommand.Parameters.Add("p_NAME_DISTRICT", OracleDbType.Varchar2, 0, "NAME_DISTRICT");
            _dataAdapter.InsertCommand.Parameters.Add("p_NAME_REGION", OracleDbType.Varchar2, 0, "NAME_REGION");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.ADDRESS_NONE_KLADR_UPDATE(p_PER_NUM=>:p_PER_NUM, p_NAME_STREET=>:p_NAME_STREET, p_NAME_LOCALITY=>:p_NAME_LOCALITY, p_NAME_CITY=>:p_NAME_CITY, p_NAME_DISTRICT=>:p_NAME_DISTRICT, p_NAME_REGION=>:p_NAME_REGION);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NAME_STREET", OracleDbType.Varchar2, 0, "NAME_STREET");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NAME_LOCALITY", OracleDbType.Varchar2, 0, "NAME_LOCALITY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NAME_CITY", OracleDbType.Varchar2, 0, "NAME_CITY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NAME_DISTRICT", OracleDbType.Varchar2, 0, "NAME_DISTRICT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NAME_REGION", OracleDbType.Varchar2, 0, "NAME_REGION");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.ADDRESS_NONE_KLADR_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        		}
		
	#endregion
    }


    [Table(Name="CITY"), SchemaName("APSTAFF")]
    public partial class City : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Код города; Код города (берется из kladr);
        /// </summary>
        [Column(Name="CODE_CITY", CanBeNull=false)]
        public String CodeCity
        {
            get
            {
        		return GetDataRowField<String>("CODE_CITY");
                //return this.GetDataRowField<String>(() => CodeCity);
            }
            set
            {
                UpdateDataRow<String>(() => CodeCity, value);
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
        		return GetDataRowField<String>("NAME_CITY");
                //return this.GetDataRowField<String>(() => NameCity);
            }
            set
            {
                UpdateDataRow<String>(() => NameCity, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор аббревиатуры (Населенного пункта, города, улиц и т.д.);
        /// </summary>
        [Column(Name="ABBREV_ID")]
        public Decimal? AbbrevID
        {
            get
            {
        		return GetDataRowField<Decimal?>("ABBREV_ID");
                //return this.GetDataRowField<Decimal?>(() => AbbrevID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AbbrevID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.CITY_UPDATE(p_CODE_CITY=>:p_CODE_CITY, p_NAME_CITY=>:p_NAME_CITY, p_ABBREV_ID=>:p_ABBREV_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_CODE_CITY", OracleDbType.Varchar2, 0, "CODE_CITY");
            _dataAdapter.InsertCommand.Parameters.Add("p_NAME_CITY", OracleDbType.Varchar2, 0, "NAME_CITY");
            _dataAdapter.InsertCommand.Parameters.Add("p_ABBREV_ID", OracleDbType.Decimal, 0, "ABBREV_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.CITY_UPDATE(p_CODE_CITY=>:p_CODE_CITY, p_NAME_CITY=>:p_NAME_CITY, p_ABBREV_ID=>:p_ABBREV_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_CODE_CITY", OracleDbType.Varchar2, 0, "CODE_CITY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NAME_CITY", OracleDbType.Varchar2, 0, "NAME_CITY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ABBREV_ID", OracleDbType.Decimal, 0, "ABBREV_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.CITY_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        		}
		
	#endregion
    }


    [Table(Name="DISTRICT"), SchemaName("APSTAFF")]
    public partial class District : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Код района; Код района (берется из kladr);
        /// </summary>
        [Column(Name="CODE_DISTRICT", CanBeNull=false)]
        public String CodeDistrict
        {
            get
            {
        		return GetDataRowField<String>("CODE_DISTRICT");
                //return this.GetDataRowField<String>(() => CodeDistrict);
            }
            set
            {
                UpdateDataRow<String>(() => CodeDistrict, value);
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
        		return GetDataRowField<String>("NAME_DISTRICT");
                //return this.GetDataRowField<String>(() => NameDistrict);
            }
            set
            {
                UpdateDataRow<String>(() => NameDistrict, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор аббревиатуры (Населенного пункта, города, улиц и т.д.);
        /// </summary>
        [Column(Name="ABBREV_ID")]
        public Decimal? AbbrevID
        {
            get
            {
        		return GetDataRowField<Decimal?>("ABBREV_ID");
                //return this.GetDataRowField<Decimal?>(() => AbbrevID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AbbrevID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.DISTRICT_UPDATE(p_CODE_DISTRICT=>:p_CODE_DISTRICT, p_NAME_DISTRICT=>:p_NAME_DISTRICT, p_ABBREV_ID=>:p_ABBREV_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_CODE_DISTRICT", OracleDbType.Varchar2, 0, "CODE_DISTRICT");
            _dataAdapter.InsertCommand.Parameters.Add("p_NAME_DISTRICT", OracleDbType.Varchar2, 0, "NAME_DISTRICT");
            _dataAdapter.InsertCommand.Parameters.Add("p_ABBREV_ID", OracleDbType.Decimal, 0, "ABBREV_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.DISTRICT_UPDATE(p_CODE_DISTRICT=>:p_CODE_DISTRICT, p_NAME_DISTRICT=>:p_NAME_DISTRICT, p_ABBREV_ID=>:p_ABBREV_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_CODE_DISTRICT", OracleDbType.Varchar2, 0, "CODE_DISTRICT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NAME_DISTRICT", OracleDbType.Varchar2, 0, "NAME_DISTRICT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ABBREV_ID", OracleDbType.Decimal, 0, "ABBREV_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.DISTRICT_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        		}
		
	#endregion
    }

    /// <summary>
    /// Таблица типов документов табеля
    /// </summary>

    [Table(Name="DOC_LIST"), SchemaName("APSTAFF")]
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
        		return GetDataRowField<Decimal?>("DOC_LIST_ID");
                //return this.GetDataRowField<Decimal?>(() => DocListID);
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
        		return GetDataRowField<DateTime?>("DOC_END_VALID");
                //return this.GetDataRowField<DateTime?>(() => DocEndValid);
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
        		return GetDataRowField<DateTime?>("DOC_BEGIN_VALID");
                //return this.GetDataRowField<DateTime?>(() => DocBeginValid);
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
        		return GetDataRowField<Decimal?>("SIGN_ALL_DAY");
                //return this.GetDataRowField<Decimal?>(() => SignAllDay);
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
        		return GetDataRowField<Decimal?>("ADD_HOLIDAY");
                //return this.GetDataRowField<Decimal?>(() => AddHoliday);
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
        		return GetDataRowField<Decimal?>("ISCALC");
                //return this.GetDataRowField<Decimal?>(() => Iscalc);
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
        		return GetDataRowField<Decimal?>("PAY_TYPE_ID");
                //return this.GetDataRowField<Decimal?>(() => PayTypeID);
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
        		return GetDataRowField<Decimal?>("DOC_TYPE");
                //return this.GetDataRowField<Decimal?>(() => DocType);
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
        		return GetDataRowField<String>("DOC_NOTE");
                //return this.GetDataRowField<String>(() => DocNote);
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
        		return GetDataRowField<String>("DOC_NAME");
                //return this.GetDataRowField<String>(() => DocName);
            }
            set
            {
                UpdateDataRow<String>(() => DocName, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.DOC_LIST_UPDATE(p_DOC_LIST_ID=>:p_DOC_LIST_ID, p_DOC_END_VALID=>:p_DOC_END_VALID, p_DOC_BEGIN_VALID=>:p_DOC_BEGIN_VALID, p_SIGN_ALL_DAY=>:p_SIGN_ALL_DAY, p_ADD_HOLIDAY=>:p_ADD_HOLIDAY, p_ISCALC=>:p_ISCALC, p_PAY_TYPE_ID=>:p_PAY_TYPE_ID, p_DOC_TYPE=>:p_DOC_TYPE, p_DOC_NOTE=>:p_DOC_NOTE, p_DOC_NAME=>:p_DOC_NAME);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_DOC_LIST_ID", OracleDbType.Decimal, 0, "DOC_LIST_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_DOC_LIST_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_END_VALID", OracleDbType.Date, 0, "DOC_END_VALID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_BEGIN_VALID", OracleDbType.Date, 0, "DOC_BEGIN_VALID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_ALL_DAY", OracleDbType.Decimal, 0, "SIGN_ALL_DAY");
            _dataAdapter.InsertCommand.Parameters.Add("p_ADD_HOLIDAY", OracleDbType.Decimal, 0, "ADD_HOLIDAY");
            _dataAdapter.InsertCommand.Parameters.Add("p_ISCALC", OracleDbType.Decimal, 0, "ISCALC");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAY_TYPE_ID", OracleDbType.Decimal, 0, "PAY_TYPE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_TYPE", OracleDbType.Decimal, 0, "DOC_TYPE");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_NOTE", OracleDbType.Varchar2, 0, "DOC_NOTE");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_NAME", OracleDbType.Varchar2, 0, "DOC_NAME");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.DOC_LIST_UPDATE(p_DOC_LIST_ID=>:p_DOC_LIST_ID, p_DOC_END_VALID=>:p_DOC_END_VALID, p_DOC_BEGIN_VALID=>:p_DOC_BEGIN_VALID, p_SIGN_ALL_DAY=>:p_SIGN_ALL_DAY, p_ADD_HOLIDAY=>:p_ADD_HOLIDAY, p_ISCALC=>:p_ISCALC, p_PAY_TYPE_ID=>:p_PAY_TYPE_ID, p_DOC_TYPE=>:p_DOC_TYPE, p_DOC_NOTE=>:p_DOC_NOTE, p_DOC_NAME=>:p_DOC_NAME);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_DOC_LIST_ID", OracleDbType.Decimal, 0, "DOC_LIST_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_DOC_LIST_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_END_VALID", OracleDbType.Date, 0, "DOC_END_VALID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_BEGIN_VALID", OracleDbType.Date, 0, "DOC_BEGIN_VALID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_ALL_DAY", OracleDbType.Decimal, 0, "SIGN_ALL_DAY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ADD_HOLIDAY", OracleDbType.Decimal, 0, "ADD_HOLIDAY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ISCALC", OracleDbType.Decimal, 0, "ISCALC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAY_TYPE_ID", OracleDbType.Decimal, 0, "PAY_TYPE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_TYPE", OracleDbType.Decimal, 0, "DOC_TYPE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_NOTE", OracleDbType.Varchar2, 0, "DOC_NOTE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_NAME", OracleDbType.Varchar2, 0, "DOC_NAME");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.DOC_LIST_DELETE(p_DOC_LIST_ID => :p_DOC_LIST_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_DOC_LIST_ID", OracleDbType.Decimal, 0, "DOC_LIST_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Общие и бухгалтерские данные по сотруднику
    /// </summary>

    [Table(Name="EMP_ACCOUNT_DATA"), SchemaName("APSTAFF")]
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
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
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
        		return GetDataRowField<String>("EMP_LAST_NAME");
                //return this.GetDataRowField<String>(() => EmpLastName);
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
        		return GetDataRowField<String>("EMP_FIRST_NAME");
                //return this.GetDataRowField<String>(() => EmpFirstName);
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
        		return GetDataRowField<String>("EMP_MIDDLE_NAME");
                //return this.GetDataRowField<String>(() => EmpMiddleName);
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
        		return GetDataRowField<String>("CODE_SUBDIV");
                //return this.GetDataRowField<String>(() => CodeSubdiv);
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
        		return GetDataRowField<String>("POS_NAME");
                //return this.GetDataRowField<String>(() => PosName);
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
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
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
        		return GetDataRowField<String>("EMP_SEX");
                //return this.GetDataRowField<String>(() => EmpSex);
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
        		return GetDataRowField<DateTime?>("EMP_BIRTH_DATE");
                //return this.GetDataRowField<DateTime?>(() => EmpBirthDate);
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
        		return GetDataRowField<String>("CODE_DEGREE");
                //return this.GetDataRowField<String>(() => CodeDegree);
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
        		return GetDataRowField<Decimal?>("ID");
                //return this.GetDataRowField<Decimal?>(() => ID);
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
        		return GetDataRowField<Int16?>("SIGN_COMB");
                //return this.GetDataRowField<Int16?>(() => SignComb);
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
        		return GetDataRowField<Byte[]>("PHOTO");
                //return this.GetDataRowField<Byte[]>(() => Photo);
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
        		return GetDataRowField<DateTime?>("DATE_TRANSFER");
                //return this.GetDataRowField<DateTime?>(() => DateTransfer);
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
        		return GetDataRowField<DateTime?>("END_TRANSFER");
                //return this.GetDataRowField<DateTime?>(() => EndTransfer);
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
        		return GetDataRowField<Decimal?>("TARIFF_GRID_ID");
                //return this.GetDataRowField<Decimal?>(() => TariffGridID);
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
        		return GetDataRowField<String>("CODE_TARIFF_GRID");
                //return this.GetDataRowField<String>(() => CodeTariffGrid);
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
        		return GetDataRowField<Decimal?>("TAR_HOUR");
                //return this.GetDataRowField<Decimal?>(() => TarHour);
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
        		return GetDataRowField<Decimal?>("CLASSIFIC");
                //return this.GetDataRowField<Decimal?>(() => Classific);
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
        		return GetDataRowField<Decimal?>("SALARY");
                //return this.GetDataRowField<Decimal?>(() => Salary);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => Salary, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.EMP_ACCOUNT_DATA_UPDATE(p_TRANSFER_ID=>:p_TRANSFER_ID, p_EMP_LAST_NAME=>:p_EMP_LAST_NAME, p_EMP_FIRST_NAME=>:p_EMP_FIRST_NAME, p_EMP_MIDDLE_NAME=>:p_EMP_MIDDLE_NAME, p_CODE_SUBDIV=>:p_CODE_SUBDIV, p_POS_NAME=>:p_POS_NAME, p_PER_NUM=>:p_PER_NUM, p_EMP_SEX=>:p_EMP_SEX, p_EMP_BIRTH_DATE=>:p_EMP_BIRTH_DATE, p_CODE_DEGREE=>:p_CODE_DEGREE, p_ID=>:p_ID, p_SIGN_COMB=>:p_SIGN_COMB, p_PHOTO=>:p_PHOTO, p_DATE_TRANSFER=>:p_DATE_TRANSFER, p_END_TRANSFER=>:p_END_TRANSFER, p_TARIFF_GRID_ID=>:p_TARIFF_GRID_ID, p_CODE_TARIFF_GRID=>:p_CODE_TARIFF_GRID, p_TAR_HOUR=>:p_TAR_HOUR, p_CLASSIFIC=>:p_CLASSIFIC, p_SALARY=>:p_SALARY);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_EMP_LAST_NAME", OracleDbType.Varchar2, 0, "EMP_LAST_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_EMP_FIRST_NAME", OracleDbType.Varchar2, 0, "EMP_FIRST_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_EMP_MIDDLE_NAME", OracleDbType.Varchar2, 0, "EMP_MIDDLE_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_SUBDIV", OracleDbType.Varchar2, 0, "CODE_SUBDIV");
            _dataAdapter.InsertCommand.Parameters.Add("p_POS_NAME", OracleDbType.Varchar2, 0, "POS_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_EMP_SEX", OracleDbType.Varchar2, 0, "EMP_SEX");
            _dataAdapter.InsertCommand.Parameters.Add("p_EMP_BIRTH_DATE", OracleDbType.Date, 0, "EMP_BIRTH_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_DEGREE", OracleDbType.Varchar2, 0, "CODE_DEGREE");
            _dataAdapter.InsertCommand.Parameters.Add("p_ID", OracleDbType.Decimal, 0, "ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            _dataAdapter.InsertCommand.Parameters.Add("p_PHOTO", OracleDbType.Blob, 0, "PHOTO");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_TRANSFER", OracleDbType.Date, 0, "DATE_TRANSFER");
            _dataAdapter.InsertCommand.Parameters.Add("p_END_TRANSFER", OracleDbType.Date, 0, "END_TRANSFER");
            _dataAdapter.InsertCommand.Parameters.Add("p_TARIFF_GRID_ID", OracleDbType.Decimal, 0, "TARIFF_GRID_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_TARIFF_GRID", OracleDbType.Varchar2, 0, "CODE_TARIFF_GRID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TAR_HOUR", OracleDbType.Decimal, 0, "TAR_HOUR");
            _dataAdapter.InsertCommand.Parameters.Add("p_CLASSIFIC", OracleDbType.Decimal, 0, "CLASSIFIC");
            _dataAdapter.InsertCommand.Parameters.Add("p_SALARY", OracleDbType.Decimal, 0, "SALARY");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.EMP_ACCOUNT_DATA_UPDATE(p_TRANSFER_ID=>:p_TRANSFER_ID, p_EMP_LAST_NAME=>:p_EMP_LAST_NAME, p_EMP_FIRST_NAME=>:p_EMP_FIRST_NAME, p_EMP_MIDDLE_NAME=>:p_EMP_MIDDLE_NAME, p_CODE_SUBDIV=>:p_CODE_SUBDIV, p_POS_NAME=>:p_POS_NAME, p_PER_NUM=>:p_PER_NUM, p_EMP_SEX=>:p_EMP_SEX, p_EMP_BIRTH_DATE=>:p_EMP_BIRTH_DATE, p_CODE_DEGREE=>:p_CODE_DEGREE, p_ID=>:p_ID, p_SIGN_COMB=>:p_SIGN_COMB, p_PHOTO=>:p_PHOTO, p_DATE_TRANSFER=>:p_DATE_TRANSFER, p_END_TRANSFER=>:p_END_TRANSFER, p_TARIFF_GRID_ID=>:p_TARIFF_GRID_ID, p_CODE_TARIFF_GRID=>:p_CODE_TARIFF_GRID, p_TAR_HOUR=>:p_TAR_HOUR, p_CLASSIFIC=>:p_CLASSIFIC, p_SALARY=>:p_SALARY);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EMP_LAST_NAME", OracleDbType.Varchar2, 0, "EMP_LAST_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EMP_FIRST_NAME", OracleDbType.Varchar2, 0, "EMP_FIRST_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EMP_MIDDLE_NAME", OracleDbType.Varchar2, 0, "EMP_MIDDLE_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_SUBDIV", OracleDbType.Varchar2, 0, "CODE_SUBDIV");
            _dataAdapter.UpdateCommand.Parameters.Add("p_POS_NAME", OracleDbType.Varchar2, 0, "POS_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EMP_SEX", OracleDbType.Varchar2, 0, "EMP_SEX");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EMP_BIRTH_DATE", OracleDbType.Date, 0, "EMP_BIRTH_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_DEGREE", OracleDbType.Varchar2, 0, "CODE_DEGREE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ID", OracleDbType.Decimal, 0, "ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PHOTO", OracleDbType.Blob, 0, "PHOTO");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_TRANSFER", OracleDbType.Date, 0, "DATE_TRANSFER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_END_TRANSFER", OracleDbType.Date, 0, "END_TRANSFER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TARIFF_GRID_ID", OracleDbType.Decimal, 0, "TARIFF_GRID_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_TARIFF_GRID", OracleDbType.Varchar2, 0, "CODE_TARIFF_GRID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TAR_HOUR", OracleDbType.Decimal, 0, "TAR_HOUR");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CLASSIFIC", OracleDbType.Decimal, 0, "CLASSIFIC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SALARY", OracleDbType.Decimal, 0, "SALARY");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.EMP_ACCOUNT_DATA_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        		}
		
	#endregion
    }


    [Table(Name="EMP_ALL_DATA"), SchemaName("APSTAFF")]
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
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
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
        		return GetDataRowField<String>("EMP_LAST_NAME");
                //return this.GetDataRowField<String>(() => EmpLastName);
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
        		return GetDataRowField<String>("EMP_FIRST_NAME");
                //return this.GetDataRowField<String>(() => EmpFirstName);
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
        		return GetDataRowField<String>("EMP_MIDDLE_NAME");
                //return this.GetDataRowField<String>(() => EmpMiddleName);
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
        		return GetDataRowField<String>("CODE_SUBDIV");
                //return this.GetDataRowField<String>(() => CodeSubdiv);
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
        		return GetDataRowField<String>("POS_NAME");
                //return this.GetDataRowField<String>(() => PosName);
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
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
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
        		return GetDataRowField<String>("EMP_SEX");
                //return this.GetDataRowField<String>(() => EmpSex);
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
        		return GetDataRowField<DateTime?>("EMP_BIRTH_DATE");
                //return this.GetDataRowField<DateTime?>(() => EmpBirthDate);
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
        		return GetDataRowField<String>("CODE_DEGREE");
                //return this.GetDataRowField<String>(() => CodeDegree);
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
        		return GetDataRowField<Decimal?>("ID");
                //return this.GetDataRowField<Decimal?>(() => ID);
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
        		return GetDataRowField<Int16?>("SIGN_COMB");
                //return this.GetDataRowField<Int16?>(() => SignComb);
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
        		return GetDataRowField<Byte[]>("PHOTO");
                //return this.GetDataRowField<Byte[]>(() => Photo);
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
        		return GetDataRowField<DateTime?>("DATE_TRANSFER");
                //return this.GetDataRowField<DateTime?>(() => DateTransfer);
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
        		return GetDataRowField<DateTime?>("END_TRANSFER");
                //return this.GetDataRowField<DateTime?>(() => EndTransfer);
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
        		return GetDataRowField<Decimal?>("CHAR_TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => CharTransferID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CharTransferID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.EMP_ALL_DATA_UPDATE(p_TRANSFER_ID=>:p_TRANSFER_ID, p_EMP_LAST_NAME=>:p_EMP_LAST_NAME, p_EMP_FIRST_NAME=>:p_EMP_FIRST_NAME, p_EMP_MIDDLE_NAME=>:p_EMP_MIDDLE_NAME, p_CODE_SUBDIV=>:p_CODE_SUBDIV, p_POS_NAME=>:p_POS_NAME, p_PER_NUM=>:p_PER_NUM, p_EMP_SEX=>:p_EMP_SEX, p_EMP_BIRTH_DATE=>:p_EMP_BIRTH_DATE, p_CODE_DEGREE=>:p_CODE_DEGREE, p_ID=>:p_ID, p_SIGN_COMB=>:p_SIGN_COMB, p_PHOTO=>:p_PHOTO, p_DATE_TRANSFER=>:p_DATE_TRANSFER, p_END_TRANSFER=>:p_END_TRANSFER, p_CHAR_TRANSFER_ID=>:p_CHAR_TRANSFER_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_EMP_LAST_NAME", OracleDbType.Varchar2, 0, "EMP_LAST_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_EMP_FIRST_NAME", OracleDbType.Varchar2, 0, "EMP_FIRST_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_EMP_MIDDLE_NAME", OracleDbType.Varchar2, 0, "EMP_MIDDLE_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_SUBDIV", OracleDbType.Varchar2, 0, "CODE_SUBDIV");
            _dataAdapter.InsertCommand.Parameters.Add("p_POS_NAME", OracleDbType.Varchar2, 0, "POS_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_EMP_SEX", OracleDbType.Varchar2, 0, "EMP_SEX");
            _dataAdapter.InsertCommand.Parameters.Add("p_EMP_BIRTH_DATE", OracleDbType.Date, 0, "EMP_BIRTH_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_DEGREE", OracleDbType.Varchar2, 0, "CODE_DEGREE");
            _dataAdapter.InsertCommand.Parameters.Add("p_ID", OracleDbType.Decimal, 0, "ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            _dataAdapter.InsertCommand.Parameters.Add("p_PHOTO", OracleDbType.Blob, 0, "PHOTO");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_TRANSFER", OracleDbType.Date, 0, "DATE_TRANSFER");
            _dataAdapter.InsertCommand.Parameters.Add("p_END_TRANSFER", OracleDbType.Date, 0, "END_TRANSFER");
            _dataAdapter.InsertCommand.Parameters.Add("p_CHAR_TRANSFER_ID", OracleDbType.Decimal, 0, "CHAR_TRANSFER_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.EMP_ALL_DATA_UPDATE(p_TRANSFER_ID=>:p_TRANSFER_ID, p_EMP_LAST_NAME=>:p_EMP_LAST_NAME, p_EMP_FIRST_NAME=>:p_EMP_FIRST_NAME, p_EMP_MIDDLE_NAME=>:p_EMP_MIDDLE_NAME, p_CODE_SUBDIV=>:p_CODE_SUBDIV, p_POS_NAME=>:p_POS_NAME, p_PER_NUM=>:p_PER_NUM, p_EMP_SEX=>:p_EMP_SEX, p_EMP_BIRTH_DATE=>:p_EMP_BIRTH_DATE, p_CODE_DEGREE=>:p_CODE_DEGREE, p_ID=>:p_ID, p_SIGN_COMB=>:p_SIGN_COMB, p_PHOTO=>:p_PHOTO, p_DATE_TRANSFER=>:p_DATE_TRANSFER, p_END_TRANSFER=>:p_END_TRANSFER, p_CHAR_TRANSFER_ID=>:p_CHAR_TRANSFER_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EMP_LAST_NAME", OracleDbType.Varchar2, 0, "EMP_LAST_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EMP_FIRST_NAME", OracleDbType.Varchar2, 0, "EMP_FIRST_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EMP_MIDDLE_NAME", OracleDbType.Varchar2, 0, "EMP_MIDDLE_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_SUBDIV", OracleDbType.Varchar2, 0, "CODE_SUBDIV");
            _dataAdapter.UpdateCommand.Parameters.Add("p_POS_NAME", OracleDbType.Varchar2, 0, "POS_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EMP_SEX", OracleDbType.Varchar2, 0, "EMP_SEX");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EMP_BIRTH_DATE", OracleDbType.Date, 0, "EMP_BIRTH_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_DEGREE", OracleDbType.Varchar2, 0, "CODE_DEGREE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ID", OracleDbType.Decimal, 0, "ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PHOTO", OracleDbType.Blob, 0, "PHOTO");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_TRANSFER", OracleDbType.Date, 0, "DATE_TRANSFER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_END_TRANSFER", OracleDbType.Date, 0, "END_TRANSFER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CHAR_TRANSFER_ID", OracleDbType.Decimal, 0, "CHAR_TRANSFER_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.EMP_ALL_DATA_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        		}
		
	#endregion
    }


    [Table(Name="LOCALITY"), SchemaName("APSTAFF")]
    public partial class Locality : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Код населенного пункта; Код населенного пункта (берется из kladr);
        /// </summary>
        [Column(Name="CODE_LOCALITY", CanBeNull=false)]
        public String CodeLocality
        {
            get
            {
        		return GetDataRowField<String>("CODE_LOCALITY");
                //return this.GetDataRowField<String>(() => CodeLocality);
            }
            set
            {
                UpdateDataRow<String>(() => CodeLocality, value);
            }
        }
        /// <summary>
        /// Населенный пункт; Наименование населенного пункта;
        /// </summary>
        [Column(Name="LOCALITY_NAME")]
        public String LocalityName
        {
            get
            {
        		return GetDataRowField<String>("LOCALITY_NAME");
                //return this.GetDataRowField<String>(() => LocalityName);
            }
            set
            {
                UpdateDataRow<String>(() => LocalityName, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор аббревиатуры (Населенного пункта, города, улиц и т.д.);
        /// </summary>
        [Column(Name="ABBREV_ID")]
        public Decimal? AbbrevID
        {
            get
            {
        		return GetDataRowField<Decimal?>("ABBREV_ID");
                //return this.GetDataRowField<Decimal?>(() => AbbrevID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AbbrevID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.LOCALITY_UPDATE(p_CODE_LOCALITY=>:p_CODE_LOCALITY, p_LOCALITY_NAME=>:p_LOCALITY_NAME, p_ABBREV_ID=>:p_ABBREV_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_CODE_LOCALITY", OracleDbType.Varchar2, 0, "CODE_LOCALITY");
            _dataAdapter.InsertCommand.Parameters.Add("p_LOCALITY_NAME", OracleDbType.Varchar2, 0, "LOCALITY_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_ABBREV_ID", OracleDbType.Decimal, 0, "ABBREV_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.LOCALITY_UPDATE(p_CODE_LOCALITY=>:p_CODE_LOCALITY, p_LOCALITY_NAME=>:p_LOCALITY_NAME, p_ABBREV_ID=>:p_ABBREV_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_CODE_LOCALITY", OracleDbType.Varchar2, 0, "CODE_LOCALITY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_LOCALITY_NAME", OracleDbType.Varchar2, 0, "LOCALITY_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ABBREV_ID", OracleDbType.Decimal, 0, "ABBREV_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.LOCALITY_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
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
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
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
        		return GetDataRowField<String>("NUM_PASSPORT");
                //return this.GetDataRowField<String>(() => NumPassport);
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
        		return GetDataRowField<String>("SERIA_PASSPORT");
                //return this.GetDataRowField<String>(() => SeriaPassport);
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
        		return GetDataRowField<String>("WHO_GIVEN");
                //return this.GetDataRowField<String>(() => WhoGiven);
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
        		return GetDataRowField<DateTime?>("WHEN_GIVEN");
                //return this.GetDataRowField<DateTime?>(() => WhenGiven);
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
        		return GetDataRowField<String>("CITIZENSHIP");
                //return this.GetDataRowField<String>(() => Citizenship);
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
        		return GetDataRowField<String>("COUNTRY_BIRTH");
                //return this.GetDataRowField<String>(() => CountryBirth);
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
        		return GetDataRowField<String>("CITY_BIRTH");
                //return this.GetDataRowField<String>(() => CityBirth);
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
        		return GetDataRowField<String>("REGION_BIRTH");
                //return this.GetDataRowField<String>(() => RegionBirth);
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
        		return GetDataRowField<String>("DISTR_BIRTH");
                //return this.GetDataRowField<String>(() => DistrBirth);
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
        		return GetDataRowField<String>("LOCALITY_BIRTH");
                //return this.GetDataRowField<String>(() => LocalityBirth);
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
        		return GetDataRowField<Decimal?>("MAR_STATE_ID");
                //return this.GetDataRowField<Decimal?>(() => MarStateID);
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
        		return GetDataRowField<Decimal?>("TYPE_PER_DOC_ID");
                //return this.GetDataRowField<Decimal?>(() => TypePerDocID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypePerDocID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.PASSPORT_UPDATE(p_PER_NUM=>:p_PER_NUM, p_NUM_PASSPORT=>:p_NUM_PASSPORT, p_SERIA_PASSPORT=>:p_SERIA_PASSPORT, p_WHO_GIVEN=>:p_WHO_GIVEN, p_WHEN_GIVEN=>:p_WHEN_GIVEN, p_CITIZENSHIP=>:p_CITIZENSHIP, p_COUNTRY_BIRTH=>:p_COUNTRY_BIRTH, p_CITY_BIRTH=>:p_CITY_BIRTH, p_REGION_BIRTH=>:p_REGION_BIRTH, p_DISTR_BIRTH=>:p_DISTR_BIRTH, p_LOCALITY_BIRTH=>:p_LOCALITY_BIRTH, p_MAR_STATE_ID=>:p_MAR_STATE_ID, p_TYPE_PER_DOC_ID=>:p_TYPE_PER_DOC_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_NUM_PASSPORT", OracleDbType.Varchar2, 0, "NUM_PASSPORT");
            _dataAdapter.InsertCommand.Parameters.Add("p_SERIA_PASSPORT", OracleDbType.Varchar2, 0, "SERIA_PASSPORT");
            _dataAdapter.InsertCommand.Parameters.Add("p_WHO_GIVEN", OracleDbType.Varchar2, 0, "WHO_GIVEN");
            _dataAdapter.InsertCommand.Parameters.Add("p_WHEN_GIVEN", OracleDbType.Date, 0, "WHEN_GIVEN");
            _dataAdapter.InsertCommand.Parameters.Add("p_CITIZENSHIP", OracleDbType.Varchar2, 0, "CITIZENSHIP");
            _dataAdapter.InsertCommand.Parameters.Add("p_COUNTRY_BIRTH", OracleDbType.Varchar2, 0, "COUNTRY_BIRTH");
            _dataAdapter.InsertCommand.Parameters.Add("p_CITY_BIRTH", OracleDbType.Varchar2, 0, "CITY_BIRTH");
            _dataAdapter.InsertCommand.Parameters.Add("p_REGION_BIRTH", OracleDbType.Varchar2, 0, "REGION_BIRTH");
            _dataAdapter.InsertCommand.Parameters.Add("p_DISTR_BIRTH", OracleDbType.Varchar2, 0, "DISTR_BIRTH");
            _dataAdapter.InsertCommand.Parameters.Add("p_LOCALITY_BIRTH", OracleDbType.Varchar2, 0, "LOCALITY_BIRTH");
            _dataAdapter.InsertCommand.Parameters.Add("p_MAR_STATE_ID", OracleDbType.Decimal, 0, "MAR_STATE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_PER_DOC_ID", OracleDbType.Decimal, 0, "TYPE_PER_DOC_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.PASSPORT_UPDATE(p_PER_NUM=>:p_PER_NUM, p_NUM_PASSPORT=>:p_NUM_PASSPORT, p_SERIA_PASSPORT=>:p_SERIA_PASSPORT, p_WHO_GIVEN=>:p_WHO_GIVEN, p_WHEN_GIVEN=>:p_WHEN_GIVEN, p_CITIZENSHIP=>:p_CITIZENSHIP, p_COUNTRY_BIRTH=>:p_COUNTRY_BIRTH, p_CITY_BIRTH=>:p_CITY_BIRTH, p_REGION_BIRTH=>:p_REGION_BIRTH, p_DISTR_BIRTH=>:p_DISTR_BIRTH, p_LOCALITY_BIRTH=>:p_LOCALITY_BIRTH, p_MAR_STATE_ID=>:p_MAR_STATE_ID, p_TYPE_PER_DOC_ID=>:p_TYPE_PER_DOC_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NUM_PASSPORT", OracleDbType.Varchar2, 0, "NUM_PASSPORT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SERIA_PASSPORT", OracleDbType.Varchar2, 0, "SERIA_PASSPORT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_WHO_GIVEN", OracleDbType.Varchar2, 0, "WHO_GIVEN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_WHEN_GIVEN", OracleDbType.Date, 0, "WHEN_GIVEN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CITIZENSHIP", OracleDbType.Varchar2, 0, "CITIZENSHIP");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COUNTRY_BIRTH", OracleDbType.Varchar2, 0, "COUNTRY_BIRTH");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CITY_BIRTH", OracleDbType.Varchar2, 0, "CITY_BIRTH");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REGION_BIRTH", OracleDbType.Varchar2, 0, "REGION_BIRTH");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DISTR_BIRTH", OracleDbType.Varchar2, 0, "DISTR_BIRTH");
            _dataAdapter.UpdateCommand.Parameters.Add("p_LOCALITY_BIRTH", OracleDbType.Varchar2, 0, "LOCALITY_BIRTH");
            _dataAdapter.UpdateCommand.Parameters.Add("p_MAR_STATE_ID", OracleDbType.Decimal, 0, "MAR_STATE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_PER_DOC_ID", OracleDbType.Decimal, 0, "TYPE_PER_DOC_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.PASSPORT_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        		}
		
	#endregion
    }


    [Table(Name="PER_DATA"), SchemaName("APSTAFF")]
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
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
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
        		return GetDataRowField<Decimal?>("SOURCE_EMPLOYABILITY_ID");
                //return this.GetDataRowField<Decimal?>(() => SourceEmployabilityID);
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
        		return GetDataRowField<String>("INN");
                //return this.GetDataRowField<String>(() => Inn);
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
        		return GetDataRowField<String>("NUM_MED_POLUS");
                //return this.GetDataRowField<String>(() => NumMedPolus);
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
        		return GetDataRowField<String>("SER_MED_POLUS");
                //return this.GetDataRowField<String>(() => SerMedPolus);
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
        		return GetDataRowField<String>("INSURANCE_NUM");
                //return this.GetDataRowField<String>(() => InsuranceNum);
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
        		return GetDataRowField<Decimal?>("SIGN_YOUNG_SPEC");
                //return this.GetDataRowField<Decimal?>(() => SignYoungSpec);
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
        		return GetDataRowField<Decimal?>("SIGN_PROFUNION");
                //return this.GetDataRowField<Decimal?>(() => SignProfunion);
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
        		return GetDataRowField<Decimal?>("RETIRER_SIGN");
                //return this.GetDataRowField<Decimal?>(() => RetirerSign);
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
        		return GetDataRowField<Decimal?>("TRIP_SIGN");
                //return this.GetDataRowField<Decimal?>(() => TripSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TripSign, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.PER_DATA_UPDATE(p_PER_NUM=>:p_PER_NUM, p_SOURCE_EMPLOYABILITY_ID=>:p_SOURCE_EMPLOYABILITY_ID, p_INN=>:p_INN, p_NUM_MED_POLUS=>:p_NUM_MED_POLUS, p_SER_MED_POLUS=>:p_SER_MED_POLUS, p_INSURANCE_NUM=>:p_INSURANCE_NUM, p_SIGN_YOUNG_SPEC=>:p_SIGN_YOUNG_SPEC, p_SIGN_PROFUNION=>:p_SIGN_PROFUNION, p_RETIRER_SIGN=>:p_RETIRER_SIGN, p_TRIP_SIGN=>:p_TRIP_SIGN);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_SOURCE_EMPLOYABILITY_ID", OracleDbType.Decimal, 0, "SOURCE_EMPLOYABILITY_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_INN", OracleDbType.Varchar2, 0, "INN");
            _dataAdapter.InsertCommand.Parameters.Add("p_NUM_MED_POLUS", OracleDbType.Varchar2, 0, "NUM_MED_POLUS");
            _dataAdapter.InsertCommand.Parameters.Add("p_SER_MED_POLUS", OracleDbType.Varchar2, 0, "SER_MED_POLUS");
            _dataAdapter.InsertCommand.Parameters.Add("p_INSURANCE_NUM", OracleDbType.Varchar2, 0, "INSURANCE_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_YOUNG_SPEC", OracleDbType.Decimal, 0, "SIGN_YOUNG_SPEC");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_PROFUNION", OracleDbType.Decimal, 0, "SIGN_PROFUNION");
            _dataAdapter.InsertCommand.Parameters.Add("p_RETIRER_SIGN", OracleDbType.Decimal, 0, "RETIRER_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRIP_SIGN", OracleDbType.Decimal, 0, "TRIP_SIGN");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.PER_DATA_UPDATE(p_PER_NUM=>:p_PER_NUM, p_SOURCE_EMPLOYABILITY_ID=>:p_SOURCE_EMPLOYABILITY_ID, p_INN=>:p_INN, p_NUM_MED_POLUS=>:p_NUM_MED_POLUS, p_SER_MED_POLUS=>:p_SER_MED_POLUS, p_INSURANCE_NUM=>:p_INSURANCE_NUM, p_SIGN_YOUNG_SPEC=>:p_SIGN_YOUNG_SPEC, p_SIGN_PROFUNION=>:p_SIGN_PROFUNION, p_RETIRER_SIGN=>:p_RETIRER_SIGN, p_TRIP_SIGN=>:p_TRIP_SIGN);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SOURCE_EMPLOYABILITY_ID", OracleDbType.Decimal, 0, "SOURCE_EMPLOYABILITY_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_INN", OracleDbType.Varchar2, 0, "INN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NUM_MED_POLUS", OracleDbType.Varchar2, 0, "NUM_MED_POLUS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SER_MED_POLUS", OracleDbType.Varchar2, 0, "SER_MED_POLUS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_INSURANCE_NUM", OracleDbType.Varchar2, 0, "INSURANCE_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_YOUNG_SPEC", OracleDbType.Decimal, 0, "SIGN_YOUNG_SPEC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_PROFUNION", OracleDbType.Decimal, 0, "SIGN_PROFUNION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RETIRER_SIGN", OracleDbType.Decimal, 0, "RETIRER_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRIP_SIGN", OracleDbType.Decimal, 0, "TRIP_SIGN");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.PER_DATA_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        		}
		
	#endregion
    }

    /// <summary>
    /// Таблица документов в табеле
    /// </summary>

    [Table(Name="REG_DOC"), SchemaName("APSTAFF")]
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
        		return GetDataRowField<Decimal?>("REG_DOC_ID");
                //return this.GetDataRowField<Decimal?>(() => RegDocID);
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
        		return GetDataRowField<String>("DOC_LOCATION");
                //return this.GetDataRowField<String>(() => DocLocation);
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
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
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
        		return GetDataRowField<Decimal?>("ABSENCE_ID");
                //return this.GetDataRowField<Decimal?>(() => AbsenceID);
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
        		return GetDataRowField<DateTime?>("DOC_END");
                //return this.GetDataRowField<DateTime?>(() => DocEnd);
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
        		return GetDataRowField<DateTime?>("DOC_BEGIN");
                //return this.GetDataRowField<DateTime?>(() => DocBegin);
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
        		return GetDataRowField<String>("DOC_NUMBER");
                //return this.GetDataRowField<String>(() => DocNumber);
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
        		return GetDataRowField<DateTime?>("DOC_DATE");
                //return this.GetDataRowField<DateTime?>(() => DocDate);
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
        		return GetDataRowField<Decimal?>("DOC_LIST_ID");
                //return this.GetDataRowField<Decimal?>(() => DocListID);
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
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.REG_DOC_UPDATE(p_REG_DOC_ID=>:p_REG_DOC_ID, p_DOC_LOCATION=>:p_DOC_LOCATION, p_TRANSFER_ID=>:p_TRANSFER_ID, p_ABSENCE_ID=>:p_ABSENCE_ID, p_DOC_END=>:p_DOC_END, p_DOC_BEGIN=>:p_DOC_BEGIN, p_DOC_NUMBER=>:p_DOC_NUMBER, p_DOC_DATE=>:p_DOC_DATE, p_DOC_LIST_ID=>:p_DOC_LIST_ID, p_PER_NUM=>:p_PER_NUM);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_REG_DOC_ID", OracleDbType.Decimal, 0, "REG_DOC_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_REG_DOC_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_LOCATION", OracleDbType.Varchar2, 0, "DOC_LOCATION");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_ABSENCE_ID", OracleDbType.Decimal, 0, "ABSENCE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_END", OracleDbType.Date, 0, "DOC_END");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_BEGIN", OracleDbType.Date, 0, "DOC_BEGIN");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_NUMBER", OracleDbType.Varchar2, 0, "DOC_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_DATE", OracleDbType.Date, 0, "DOC_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_LIST_ID", OracleDbType.Decimal, 0, "DOC_LIST_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.REG_DOC_UPDATE(p_REG_DOC_ID=>:p_REG_DOC_ID, p_DOC_LOCATION=>:p_DOC_LOCATION, p_TRANSFER_ID=>:p_TRANSFER_ID, p_ABSENCE_ID=>:p_ABSENCE_ID, p_DOC_END=>:p_DOC_END, p_DOC_BEGIN=>:p_DOC_BEGIN, p_DOC_NUMBER=>:p_DOC_NUMBER, p_DOC_DATE=>:p_DOC_DATE, p_DOC_LIST_ID=>:p_DOC_LIST_ID, p_PER_NUM=>:p_PER_NUM);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_REG_DOC_ID", OracleDbType.Decimal, 0, "REG_DOC_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_REG_DOC_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_LOCATION", OracleDbType.Varchar2, 0, "DOC_LOCATION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ABSENCE_ID", OracleDbType.Decimal, 0, "ABSENCE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_END", OracleDbType.Date, 0, "DOC_END");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_BEGIN", OracleDbType.Date, 0, "DOC_BEGIN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_NUMBER", OracleDbType.Varchar2, 0, "DOC_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_DATE", OracleDbType.Date, 0, "DOC_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_LIST_ID", OracleDbType.Decimal, 0, "DOC_LIST_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.REG_DOC_DELETE(p_REG_DOC_ID => :p_REG_DOC_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_REG_DOC_ID", OracleDbType.Decimal, 0, "REG_DOC_ID");
	}
		
	#endregion
    }


    [Table(Name="REGION"), SchemaName("APSTAFF")]
    public partial class Region : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Код региона; Код региона (берется из kladr);
        /// </summary>
        [Column(Name="CODE_REGION", CanBeNull=false)]
        public String CodeRegion
        {
            get
            {
        		return GetDataRowField<String>("CODE_REGION");
                //return this.GetDataRowField<String>(() => CodeRegion);
            }
            set
            {
                UpdateDataRow<String>(() => CodeRegion, value);
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
        		return GetDataRowField<String>("NAME_REGION");
                //return this.GetDataRowField<String>(() => NameRegion);
            }
            set
            {
                UpdateDataRow<String>(() => NameRegion, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор аббревиатуры (Населенного пункта, города, улиц и т.д.);
        /// </summary>
        [Column(Name="ABBREV_ID")]
        public Decimal? AbbrevID
        {
            get
            {
        		return GetDataRowField<Decimal?>("ABBREV_ID");
                //return this.GetDataRowField<Decimal?>(() => AbbrevID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AbbrevID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.REGION_UPDATE(p_CODE_REGION=>:p_CODE_REGION, p_NAME_REGION=>:p_NAME_REGION, p_ABBREV_ID=>:p_ABBREV_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_CODE_REGION", OracleDbType.Varchar2, 0, "CODE_REGION");
            _dataAdapter.InsertCommand.Parameters.Add("p_NAME_REGION", OracleDbType.Varchar2, 0, "NAME_REGION");
            _dataAdapter.InsertCommand.Parameters.Add("p_ABBREV_ID", OracleDbType.Decimal, 0, "ABBREV_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.REGION_UPDATE(p_CODE_REGION=>:p_CODE_REGION, p_NAME_REGION=>:p_NAME_REGION, p_ABBREV_ID=>:p_ABBREV_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_CODE_REGION", OracleDbType.Varchar2, 0, "CODE_REGION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NAME_REGION", OracleDbType.Varchar2, 0, "NAME_REGION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ABBREV_ID", OracleDbType.Decimal, 0, "ABBREV_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.REGION_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        		}
		
	#endregion
    }


    [Table(Name="REGISTR"), SchemaName("APSTAFF")]
    public partial class Registr : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Табельный номер;
        /// </summary>
        [Column(Name="PER_NUM", CanBeNull=false, IsPrimaryKey=true)]
        public String PerNum
        {
            get
            {
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        /// <summary>
        /// Почтовый индекс; Почтовый индекс по прописке сотрудника;
        /// </summary>
        [Column(Name="REG_POST_CODE")]
        public String RegPostCode
        {
            get
            {
        		return GetDataRowField<String>("REG_POST_CODE");
                //return this.GetDataRowField<String>(() => RegPostCode);
            }
            set
            {
                UpdateDataRow<String>(() => RegPostCode, value);
            }
        }
        /// <summary>
        /// Квартира; Номер квартиры по прописке сотрудника;
        /// </summary>
        [Column(Name="REG_FLAT")]
        public String RegFlat
        {
            get
            {
        		return GetDataRowField<String>("REG_FLAT");
                //return this.GetDataRowField<String>(() => RegFlat);
            }
            set
            {
                UpdateDataRow<String>(() => RegFlat, value);
            }
        }
        /// <summary>
        /// Дом; Номер дома по прописке сотрудника;
        /// </summary>
        [Column(Name="REG_HOUSE")]
        public String RegHouse
        {
            get
            {
        		return GetDataRowField<String>("REG_HOUSE");
                //return this.GetDataRowField<String>(() => RegHouse);
            }
            set
            {
                UpdateDataRow<String>(() => RegHouse, value);
            }
        }
        /// <summary>
        /// Корпус; Номер корпуса по прописке сотрудника;
        /// </summary>
        [Column(Name="REG_BULK")]
        public String RegBulk
        {
            get
            {
        		return GetDataRowField<String>("REG_BULK");
                //return this.GetDataRowField<String>(() => RegBulk);
            }
            set
            {
                UpdateDataRow<String>(() => RegBulk, value);
            }
        }
        /// <summary>
        /// Код улицы; Код улицы (берется из kladr), на которой проживает сотрудник по прописке;
        /// </summary>
        [Column(Name="REG_CODE_STREET")]
        public String RegCodeStreet
        {
            get
            {
        		return GetDataRowField<String>("REG_CODE_STREET");
                //return this.GetDataRowField<String>(() => RegCodeStreet);
            }
            set
            {
                UpdateDataRow<String>(() => RegCodeStreet, value);
            }
        }
        /// <summary>
        /// Телефон; Номер телефона по прописке сотрудника;
        /// </summary>
        [Column(Name="REG_PHONE")]
        public String RegPhone
        {
            get
            {
        		return GetDataRowField<String>("REG_PHONE");
                //return this.GetDataRowField<String>(() => RegPhone);
            }
            set
            {
                UpdateDataRow<String>(() => RegPhone, value);
            }
        }
        /// <summary>
        /// Дата прописки; Дата прописки сотрудника;
        /// </summary>
        [Column(Name="DATE_REG")]
        public DateTime? DateReg
        {
            get
            {
        		return GetDataRowField<DateTime?>("DATE_REG");
                //return this.GetDataRowField<DateTime?>(() => DateReg);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateReg, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор источника пополнения;
        /// </summary>
        [Column(Name="SOURCE_FILL_ID")]
        public Decimal? SourceFillID
        {
            get
            {
        		return GetDataRowField<Decimal?>("SOURCE_FILL_ID");
                //return this.GetDataRowField<Decimal?>(() => SourceFillID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SourceFillID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.REGISTR_UPDATE(p_PER_NUM=>:p_PER_NUM, p_REG_POST_CODE=>:p_REG_POST_CODE, p_REG_FLAT=>:p_REG_FLAT, p_REG_HOUSE=>:p_REG_HOUSE, p_REG_BULK=>:p_REG_BULK, p_REG_CODE_STREET=>:p_REG_CODE_STREET, p_REG_PHONE=>:p_REG_PHONE, p_DATE_REG=>:p_DATE_REG, p_SOURCE_FILL_ID=>:p_SOURCE_FILL_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters.Add("p_REG_POST_CODE", OracleDbType.Varchar2, 0, "REG_POST_CODE");
            _dataAdapter.InsertCommand.Parameters.Add("p_REG_FLAT", OracleDbType.Varchar2, 0, "REG_FLAT");
            _dataAdapter.InsertCommand.Parameters.Add("p_REG_HOUSE", OracleDbType.Varchar2, 0, "REG_HOUSE");
            _dataAdapter.InsertCommand.Parameters.Add("p_REG_BULK", OracleDbType.Varchar2, 0, "REG_BULK");
            _dataAdapter.InsertCommand.Parameters.Add("p_REG_CODE_STREET", OracleDbType.Varchar2, 0, "REG_CODE_STREET");
            _dataAdapter.InsertCommand.Parameters.Add("p_REG_PHONE", OracleDbType.Varchar2, 0, "REG_PHONE");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_REG", OracleDbType.Date, 0, "DATE_REG");
            _dataAdapter.InsertCommand.Parameters.Add("p_SOURCE_FILL_ID", OracleDbType.Decimal, 0, "SOURCE_FILL_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.REGISTR_UPDATE(p_PER_NUM=>:p_PER_NUM, p_REG_POST_CODE=>:p_REG_POST_CODE, p_REG_FLAT=>:p_REG_FLAT, p_REG_HOUSE=>:p_REG_HOUSE, p_REG_BULK=>:p_REG_BULK, p_REG_CODE_STREET=>:p_REG_CODE_STREET, p_REG_PHONE=>:p_REG_PHONE, p_DATE_REG=>:p_DATE_REG, p_SOURCE_FILL_ID=>:p_SOURCE_FILL_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters.Add("p_REG_POST_CODE", OracleDbType.Varchar2, 0, "REG_POST_CODE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REG_FLAT", OracleDbType.Varchar2, 0, "REG_FLAT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REG_HOUSE", OracleDbType.Varchar2, 0, "REG_HOUSE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REG_BULK", OracleDbType.Varchar2, 0, "REG_BULK");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REG_CODE_STREET", OracleDbType.Varchar2, 0, "REG_CODE_STREET");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REG_PHONE", OracleDbType.Varchar2, 0, "REG_PHONE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_REG", OracleDbType.Date, 0, "DATE_REG");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SOURCE_FILL_ID", OracleDbType.Decimal, 0, "SOURCE_FILL_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.REGISTR_DELETE(p_PER_NUM => :p_PER_NUM);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
	}
		
	#endregion
    }


    [Table(Name="STREET"), SchemaName("APSTAFF")]
    public partial class Street : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Код улицы; Код улицы (берется из kladr);
        /// </summary>
        [Column(Name="CODE_STREET", CanBeNull=false)]
        public String CodeStreet
        {
            get
            {
        		return GetDataRowField<String>("CODE_STREET");
                //return this.GetDataRowField<String>(() => CodeStreet);
            }
            set
            {
                UpdateDataRow<String>(() => CodeStreet, value);
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
        		return GetDataRowField<String>("NAME_STREET");
                //return this.GetDataRowField<String>(() => NameStreet);
            }
            set
            {
                UpdateDataRow<String>(() => NameStreet, value);
            }
        }
        /// <summary>
        /// Уникальный идентификатор аббревиатуры (Населенного пункта, города, улиц и т.д.);
        /// </summary>
        [Column(Name="ABBREV_ID")]
        public Decimal? AbbrevID
        {
            get
            {
        		return GetDataRowField<Decimal?>("ABBREV_ID");
                //return this.GetDataRowField<Decimal?>(() => AbbrevID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => AbbrevID, value);
            }
        }
        /// <summary>
        /// Почтовый индекс улицы;
        /// </summary>
        [Column(Name="STR_POST_CODE")]
        public String StrPostCode
        {
            get
            {
        		return GetDataRowField<String>("STR_POST_CODE");
                //return this.GetDataRowField<String>(() => StrPostCode);
            }
            set
            {
                UpdateDataRow<String>(() => StrPostCode, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.STREET_UPDATE(p_CODE_STREET=>:p_CODE_STREET, p_NAME_STREET=>:p_NAME_STREET, p_ABBREV_ID=>:p_ABBREV_ID, p_STR_POST_CODE=>:p_STR_POST_CODE);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_CODE_STREET", OracleDbType.Varchar2, 0, "CODE_STREET");
            _dataAdapter.InsertCommand.Parameters.Add("p_NAME_STREET", OracleDbType.Varchar2, 0, "NAME_STREET");
            _dataAdapter.InsertCommand.Parameters.Add("p_ABBREV_ID", OracleDbType.Decimal, 0, "ABBREV_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_STR_POST_CODE", OracleDbType.Varchar2, 0, "STR_POST_CODE");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.STREET_UPDATE(p_CODE_STREET=>:p_CODE_STREET, p_NAME_STREET=>:p_NAME_STREET, p_ABBREV_ID=>:p_ABBREV_ID, p_STR_POST_CODE=>:p_STR_POST_CODE);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_CODE_STREET", OracleDbType.Varchar2, 0, "CODE_STREET");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NAME_STREET", OracleDbType.Varchar2, 0, "NAME_STREET");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ABBREV_ID", OracleDbType.Decimal, 0, "ABBREV_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_STR_POST_CODE", OracleDbType.Varchar2, 0, "STR_POST_CODE");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.STREET_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
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
        		return GetDataRowField<Decimal?>("SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => SubdivID);
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
        		return GetDataRowField<Decimal?>("GR_WORK_ID");
                //return this.GetDataRowField<Decimal?>(() => GrWorkID);
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
        		return GetDataRowField<Decimal?>("TYPE_SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeSubdivID);
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
        		return GetDataRowField<Decimal?>("FROM_SUBDIV");
                //return this.GetDataRowField<Decimal?>(() => FromSubdiv);
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
        		return GetDataRowField<Decimal?>("PARENT_ID");
                //return this.GetDataRowField<Decimal?>(() => ParentID);
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
        		return GetDataRowField<DateTime?>("SUB_DATE_END");
                //return this.GetDataRowField<DateTime?>(() => SubDateEnd);
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
        		return GetDataRowField<DateTime?>("SUB_DATE_START");
                //return this.GetDataRowField<DateTime?>(() => SubDateStart);
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
        		return GetDataRowField<Decimal?>("SERVICE_ID");
                //return this.GetDataRowField<Decimal?>(() => ServiceID);
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
        		return GetDataRowField<Decimal?>("WORK_TYPE_ID");
                //return this.GetDataRowField<Decimal?>(() => WorkTypeID);
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
        		return GetDataRowField<Int16?>("SUB_ACTUAL_SIGN");
                //return this.GetDataRowField<Int16?>(() => SubActualSign);
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
        		return GetDataRowField<String>("SUBDIV_NAME");
                //return this.GetDataRowField<String>(() => SubdivName);
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
        		return GetDataRowField<String>("CODE_SUBDIV");
                //return this.GetDataRowField<String>(() => CodeSubdiv);
            }
            set
            {
                UpdateDataRow<String>(() => CodeSubdiv, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.SUBDIV_UPDATE(p_SUBDIV_ID=>:p_SUBDIV_ID, p_GR_WORK_ID=>:p_GR_WORK_ID, p_TYPE_SUBDIV_ID=>:p_TYPE_SUBDIV_ID, p_FROM_SUBDIV=>:p_FROM_SUBDIV, p_PARENT_ID=>:p_PARENT_ID, p_SUB_DATE_END=>:p_SUB_DATE_END, p_SUB_DATE_START=>:p_SUB_DATE_START, p_SERVICE_ID=>:p_SERVICE_ID, p_WORK_TYPE_ID=>:p_WORK_TYPE_ID, p_SUB_ACTUAL_SIGN=>:p_SUB_ACTUAL_SIGN, p_SUBDIV_NAME=>:p_SUBDIV_NAME, p_CODE_SUBDIV=>:p_CODE_SUBDIV);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_SUBDIV_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_GR_WORK_ID", OracleDbType.Decimal, 0, "GR_WORK_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_SUBDIV_ID", OracleDbType.Decimal, 0, "TYPE_SUBDIV_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_FROM_SUBDIV", OracleDbType.Decimal, 0, "FROM_SUBDIV");
            _dataAdapter.InsertCommand.Parameters.Add("p_PARENT_ID", OracleDbType.Decimal, 0, "PARENT_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUB_DATE_END", OracleDbType.Date, 0, "SUB_DATE_END");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUB_DATE_START", OracleDbType.Date, 0, "SUB_DATE_START");
            _dataAdapter.InsertCommand.Parameters.Add("p_SERVICE_ID", OracleDbType.Decimal, 0, "SERVICE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_WORK_TYPE_ID", OracleDbType.Decimal, 0, "WORK_TYPE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUB_ACTUAL_SIGN", OracleDbType.Decimal, 0, "SUB_ACTUAL_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_NAME", OracleDbType.Varchar2, 0, "SUBDIV_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_SUBDIV", OracleDbType.Varchar2, 0, "CODE_SUBDIV");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.SUBDIV_UPDATE(p_SUBDIV_ID=>:p_SUBDIV_ID, p_GR_WORK_ID=>:p_GR_WORK_ID, p_TYPE_SUBDIV_ID=>:p_TYPE_SUBDIV_ID, p_FROM_SUBDIV=>:p_FROM_SUBDIV, p_PARENT_ID=>:p_PARENT_ID, p_SUB_DATE_END=>:p_SUB_DATE_END, p_SUB_DATE_START=>:p_SUB_DATE_START, p_SERVICE_ID=>:p_SERVICE_ID, p_WORK_TYPE_ID=>:p_WORK_TYPE_ID, p_SUB_ACTUAL_SIGN=>:p_SUB_ACTUAL_SIGN, p_SUBDIV_NAME=>:p_SUBDIV_NAME, p_CODE_SUBDIV=>:p_CODE_SUBDIV);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_SUBDIV_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_GR_WORK_ID", OracleDbType.Decimal, 0, "GR_WORK_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_SUBDIV_ID", OracleDbType.Decimal, 0, "TYPE_SUBDIV_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_FROM_SUBDIV", OracleDbType.Decimal, 0, "FROM_SUBDIV");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PARENT_ID", OracleDbType.Decimal, 0, "PARENT_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUB_DATE_END", OracleDbType.Date, 0, "SUB_DATE_END");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUB_DATE_START", OracleDbType.Date, 0, "SUB_DATE_START");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SERVICE_ID", OracleDbType.Decimal, 0, "SERVICE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_WORK_TYPE_ID", OracleDbType.Decimal, 0, "WORK_TYPE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUB_ACTUAL_SIGN", OracleDbType.Decimal, 0, "SUB_ACTUAL_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_NAME", OracleDbType.Varchar2, 0, "SUBDIV_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_SUBDIV", OracleDbType.Varchar2, 0, "CODE_SUBDIV");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.SUBDIV_DELETE(p_SUBDIV_ID => :p_SUBDIV_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
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
        		return GetDataRowField<Decimal?>("TARIFF_GRID_ID");
                //return this.GetDataRowField<Decimal?>(() => TariffGridID);
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
        		return GetDataRowField<String>("CODE_TARIFF_GRID");
                //return this.GetDataRowField<String>(() => CodeTariffGrid);
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
        		return GetDataRowField<String>("TARIFF_GRID_NAME");
                //return this.GetDataRowField<String>(() => TariffGridName);
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
        		return GetDataRowField<Decimal?>("TYPE_TARIFF_GRID_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeTariffGridID);
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
        		return GetDataRowField<String>("TAR_COUNT_HOUR");
                //return this.GetDataRowField<String>(() => TarCountHour);
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
        		return GetDataRowField<Int16?>("TAR_PERCENT");
                //return this.GetDataRowField<Int16?>(() => TarPercent);
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
        		return GetDataRowField<String>("TAR_NOTE");
                //return this.GetDataRowField<String>(() => TarNote);
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
        		return GetDataRowField<Decimal?>("TAR_GROUP");
                //return this.GetDataRowField<Decimal?>(() => TarGroup);
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
        		return GetDataRowField<Decimal?>("TAR_ADDITION");
                //return this.GetDataRowField<Decimal?>(() => TarAddition);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TarAddition, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.TARIFF_GRID_UPDATE(p_TARIFF_GRID_ID=>:p_TARIFF_GRID_ID, p_CODE_TARIFF_GRID=>:p_CODE_TARIFF_GRID, p_TARIFF_GRID_NAME=>:p_TARIFF_GRID_NAME, p_TYPE_TARIFF_GRID_ID=>:p_TYPE_TARIFF_GRID_ID, p_TAR_COUNT_HOUR=>:p_TAR_COUNT_HOUR, p_TAR_PERCENT=>:p_TAR_PERCENT, p_TAR_NOTE=>:p_TAR_NOTE, p_TAR_GROUP=>:p_TAR_GROUP, p_TAR_ADDITION=>:p_TAR_ADDITION);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TARIFF_GRID_ID", OracleDbType.Decimal, 0, "TARIFF_GRID_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_TARIFF_GRID_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_TARIFF_GRID", OracleDbType.Varchar2, 0, "CODE_TARIFF_GRID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TARIFF_GRID_NAME", OracleDbType.Varchar2, 0, "TARIFF_GRID_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_TARIFF_GRID_ID", OracleDbType.Decimal, 0, "TYPE_TARIFF_GRID_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TAR_COUNT_HOUR", OracleDbType.Varchar2, 0, "TAR_COUNT_HOUR");
            _dataAdapter.InsertCommand.Parameters.Add("p_TAR_PERCENT", OracleDbType.Decimal, 0, "TAR_PERCENT");
            _dataAdapter.InsertCommand.Parameters.Add("p_TAR_NOTE", OracleDbType.Varchar2, 0, "TAR_NOTE");
            _dataAdapter.InsertCommand.Parameters.Add("p_TAR_GROUP", OracleDbType.Decimal, 0, "TAR_GROUP");
            _dataAdapter.InsertCommand.Parameters.Add("p_TAR_ADDITION", OracleDbType.Decimal, 0, "TAR_ADDITION");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.TARIFF_GRID_UPDATE(p_TARIFF_GRID_ID=>:p_TARIFF_GRID_ID, p_CODE_TARIFF_GRID=>:p_CODE_TARIFF_GRID, p_TARIFF_GRID_NAME=>:p_TARIFF_GRID_NAME, p_TYPE_TARIFF_GRID_ID=>:p_TYPE_TARIFF_GRID_ID, p_TAR_COUNT_HOUR=>:p_TAR_COUNT_HOUR, p_TAR_PERCENT=>:p_TAR_PERCENT, p_TAR_NOTE=>:p_TAR_NOTE, p_TAR_GROUP=>:p_TAR_GROUP, p_TAR_ADDITION=>:p_TAR_ADDITION);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TARIFF_GRID_ID", OracleDbType.Decimal, 0, "TARIFF_GRID_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_TARIFF_GRID_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_TARIFF_GRID", OracleDbType.Varchar2, 0, "CODE_TARIFF_GRID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TARIFF_GRID_NAME", OracleDbType.Varchar2, 0, "TARIFF_GRID_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_TARIFF_GRID_ID", OracleDbType.Decimal, 0, "TYPE_TARIFF_GRID_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TAR_COUNT_HOUR", OracleDbType.Varchar2, 0, "TAR_COUNT_HOUR");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TAR_PERCENT", OracleDbType.Decimal, 0, "TAR_PERCENT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TAR_NOTE", OracleDbType.Varchar2, 0, "TAR_NOTE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TAR_GROUP", OracleDbType.Decimal, 0, "TAR_GROUP");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TAR_ADDITION", OracleDbType.Decimal, 0, "TAR_ADDITION");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.TARIFF_GRID_DELETE(p_TARIFF_GRID_ID => :p_TARIFF_GRID_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_TARIFF_GRID_ID", OracleDbType.Decimal, 0, "TARIFF_GRID_ID");
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
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
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
        		return GetDataRowField<String>("POS_NOTE");
                //return this.GetDataRowField<String>(() => PosNote);
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
        		return GetDataRowField<Decimal?>("ADDITIONAL_VAC");
                //return this.GetDataRowField<Decimal?>(() => AdditionalVac);
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
        		return GetDataRowField<Decimal?>("CHAR_TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => CharTransferID);
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
        		return GetDataRowField<Decimal?>("WORKER_ID");
                //return this.GetDataRowField<Decimal?>(() => WorkerID);
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
        		return GetDataRowField<Decimal?>("HARMFUL_VAC");
                //return this.GetDataRowField<Decimal?>(() => HarmfulVac);
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
        		return GetDataRowField<Decimal?>("FORM_OPERATION_ID");
                //return this.GetDataRowField<Decimal?>(() => FormOperationID);
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
        		return GetDataRowField<Decimal?>("PROBA_PERIOD");
                //return this.GetDataRowField<Decimal?>(() => ProbaPeriod);
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
        		return GetDataRowField<Decimal?>("REASON_ID");
                //return this.GetDataRowField<Decimal?>(() => ReasonID);
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
        		return GetDataRowField<Int16?>("SIGN_CUR_WORK");
                //return this.GetDataRowField<Int16?>(() => SignCurWork);
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
        		return GetDataRowField<DateTime?>("DF_BOOK_DISMISS");
                //return this.GetDataRowField<DateTime?>(() => DfBookDismiss);
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
        		return GetDataRowField<DateTime?>("DF_BOOK_ORDER");
                //return this.GetDataRowField<DateTime?>(() => DfBookOrder);
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
        		return GetDataRowField<DateTime?>("DATE_CONTR");
                //return this.GetDataRowField<DateTime?>(() => DateContr);
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
        		return GetDataRowField<String>("CONTR_EMP");
                //return this.GetDataRowField<String>(() => ContrEmp);
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
        		return GetDataRowField<Int16?>("CHAN_SIGN");
                //return this.GetDataRowField<Int16?>(() => ChanSign);
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
        		return GetDataRowField<Decimal?>("FROM_POSITION");
                //return this.GetDataRowField<Decimal?>(() => FromPosition);
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
        		return GetDataRowField<Decimal?>("BASE_DOC_ID");
                //return this.GetDataRowField<Decimal?>(() => BaseDocID);
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
        		return GetDataRowField<Int16?>("HIRE_SIGN");
                //return this.GetDataRowField<Int16?>(() => HireSign);
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
        		return GetDataRowField<Decimal?>("SOURCE_ID");
                //return this.GetDataRowField<Decimal?>(() => SourceID);
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
        		return GetDataRowField<Decimal?>("CHAR_WORK_ID");
                //return this.GetDataRowField<Decimal?>(() => CharWorkID);
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
        		return GetDataRowField<Decimal?>("DEGREE_ID");
                //return this.GetDataRowField<Decimal?>(() => DegreeID);
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
        		return GetDataRowField<Int16?>("FORM_PAY");
                //return this.GetDataRowField<Int16?>(() => FormPay);
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
        		return GetDataRowField<DateTime?>("TR_DATE_ORDER");
                //return this.GetDataRowField<DateTime?>(() => TrDateOrder);
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
        		return GetDataRowField<String>("TR_NUM_ORDER");
                //return this.GetDataRowField<String>(() => TrNumOrder);
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
        		return GetDataRowField<DateTime?>("DATE_END_CONTR");
                //return this.GetDataRowField<DateTime?>(() => DateEndContr);
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
        		return GetDataRowField<DateTime?>("DATE_TRANSFER");
                //return this.GetDataRowField<DateTime?>(() => DateTransfer);
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
        		return GetDataRowField<Int16?>("SIGN_COMB");
                //return this.GetDataRowField<Int16?>(() => SignComb);
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
        		return GetDataRowField<DateTime?>("DATE_HIRE");
                //return this.GetDataRowField<DateTime?>(() => DateHire);
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
        		return GetDataRowField<Decimal?>("GR_WORK_ID");
                //return this.GetDataRowField<Decimal?>(() => GrWorkID);
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
        		return GetDataRowField<Decimal?>("TYPE_TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeTransferID);
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
        		return GetDataRowField<Decimal?>("POS_ID");
                //return this.GetDataRowField<Decimal?>(() => PosID);
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
        		return GetDataRowField<Decimal?>("SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => SubdivID);
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
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN APSTAFF.TRANSFER_UPDATE(p_TRANSFER_ID=>:p_TRANSFER_ID, p_POS_NOTE=>:p_POS_NOTE, p_ADDITIONAL_VAC=>:p_ADDITIONAL_VAC, p_CHAR_TRANSFER_ID=>:p_CHAR_TRANSFER_ID, p_WORKER_ID=>:p_WORKER_ID, p_HARMFUL_VAC=>:p_HARMFUL_VAC, p_FORM_OPERATION_ID=>:p_FORM_OPERATION_ID, p_PROBA_PERIOD=>:p_PROBA_PERIOD, p_REASON_ID=>:p_REASON_ID, p_SIGN_CUR_WORK=>:p_SIGN_CUR_WORK, p_DF_BOOK_DISMISS=>:p_DF_BOOK_DISMISS, p_DF_BOOK_ORDER=>:p_DF_BOOK_ORDER, p_DATE_CONTR=>:p_DATE_CONTR, p_CONTR_EMP=>:p_CONTR_EMP, p_CHAN_SIGN=>:p_CHAN_SIGN, p_FROM_POSITION=>:p_FROM_POSITION, p_BASE_DOC_ID=>:p_BASE_DOC_ID, p_HIRE_SIGN=>:p_HIRE_SIGN, p_SOURCE_ID=>:p_SOURCE_ID, p_CHAR_WORK_ID=>:p_CHAR_WORK_ID, p_DEGREE_ID=>:p_DEGREE_ID, p_FORM_PAY=>:p_FORM_PAY, p_TR_DATE_ORDER=>:p_TR_DATE_ORDER, p_TR_NUM_ORDER=>:p_TR_NUM_ORDER, p_DATE_END_CONTR=>:p_DATE_END_CONTR, p_DATE_TRANSFER=>:p_DATE_TRANSFER, p_SIGN_COMB=>:p_SIGN_COMB, p_DATE_HIRE=>:p_DATE_HIRE, p_GR_WORK_ID=>:p_GR_WORK_ID, p_TYPE_TRANSFER_ID=>:p_TYPE_TRANSFER_ID, p_POS_ID=>:p_POS_ID, p_SUBDIV_ID=>:p_SUBDIV_ID, p_PER_NUM=>:p_PER_NUM);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_TRANSFER_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_POS_NOTE", OracleDbType.Varchar2, 0, "POS_NOTE");
            _dataAdapter.InsertCommand.Parameters.Add("p_ADDITIONAL_VAC", OracleDbType.Decimal, 0, "ADDITIONAL_VAC");
            _dataAdapter.InsertCommand.Parameters.Add("p_CHAR_TRANSFER_ID", OracleDbType.Decimal, 0, "CHAR_TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_WORKER_ID", OracleDbType.Decimal, 0, "WORKER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_HARMFUL_VAC", OracleDbType.Decimal, 0, "HARMFUL_VAC");
            _dataAdapter.InsertCommand.Parameters.Add("p_FORM_OPERATION_ID", OracleDbType.Decimal, 0, "FORM_OPERATION_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PROBA_PERIOD", OracleDbType.Decimal, 0, "PROBA_PERIOD");
            _dataAdapter.InsertCommand.Parameters.Add("p_REASON_ID", OracleDbType.Decimal, 0, "REASON_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_CUR_WORK", OracleDbType.Decimal, 0, "SIGN_CUR_WORK");
            _dataAdapter.InsertCommand.Parameters.Add("p_DF_BOOK_DISMISS", OracleDbType.Date, 0, "DF_BOOK_DISMISS");
            _dataAdapter.InsertCommand.Parameters.Add("p_DF_BOOK_ORDER", OracleDbType.Date, 0, "DF_BOOK_ORDER");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_CONTR", OracleDbType.Date, 0, "DATE_CONTR");
            _dataAdapter.InsertCommand.Parameters.Add("p_CONTR_EMP", OracleDbType.Varchar2, 0, "CONTR_EMP");
            _dataAdapter.InsertCommand.Parameters.Add("p_CHAN_SIGN", OracleDbType.Decimal, 0, "CHAN_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_FROM_POSITION", OracleDbType.Decimal, 0, "FROM_POSITION");
            _dataAdapter.InsertCommand.Parameters.Add("p_BASE_DOC_ID", OracleDbType.Decimal, 0, "BASE_DOC_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_HIRE_SIGN", OracleDbType.Decimal, 0, "HIRE_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_SOURCE_ID", OracleDbType.Decimal, 0, "SOURCE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_CHAR_WORK_ID", OracleDbType.Decimal, 0, "CHAR_WORK_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_FORM_PAY", OracleDbType.Decimal, 0, "FORM_PAY");
            _dataAdapter.InsertCommand.Parameters.Add("p_TR_DATE_ORDER", OracleDbType.Date, 0, "TR_DATE_ORDER");
            _dataAdapter.InsertCommand.Parameters.Add("p_TR_NUM_ORDER", OracleDbType.Varchar2, 0, "TR_NUM_ORDER");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_END_CONTR", OracleDbType.Date, 0, "DATE_END_CONTR");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_TRANSFER", OracleDbType.Date, 0, "DATE_TRANSFER");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_HIRE", OracleDbType.Date, 0, "DATE_HIRE");
            _dataAdapter.InsertCommand.Parameters.Add("p_GR_WORK_ID", OracleDbType.Decimal, 0, "GR_WORK_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_TRANSFER_ID", OracleDbType.Decimal, 0, "TYPE_TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_POS_ID", OracleDbType.Decimal, 0, "POS_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN APSTAFF.TRANSFER_UPDATE(p_TRANSFER_ID=>:p_TRANSFER_ID, p_POS_NOTE=>:p_POS_NOTE, p_ADDITIONAL_VAC=>:p_ADDITIONAL_VAC, p_CHAR_TRANSFER_ID=>:p_CHAR_TRANSFER_ID, p_WORKER_ID=>:p_WORKER_ID, p_HARMFUL_VAC=>:p_HARMFUL_VAC, p_FORM_OPERATION_ID=>:p_FORM_OPERATION_ID, p_PROBA_PERIOD=>:p_PROBA_PERIOD, p_REASON_ID=>:p_REASON_ID, p_SIGN_CUR_WORK=>:p_SIGN_CUR_WORK, p_DF_BOOK_DISMISS=>:p_DF_BOOK_DISMISS, p_DF_BOOK_ORDER=>:p_DF_BOOK_ORDER, p_DATE_CONTR=>:p_DATE_CONTR, p_CONTR_EMP=>:p_CONTR_EMP, p_CHAN_SIGN=>:p_CHAN_SIGN, p_FROM_POSITION=>:p_FROM_POSITION, p_BASE_DOC_ID=>:p_BASE_DOC_ID, p_HIRE_SIGN=>:p_HIRE_SIGN, p_SOURCE_ID=>:p_SOURCE_ID, p_CHAR_WORK_ID=>:p_CHAR_WORK_ID, p_DEGREE_ID=>:p_DEGREE_ID, p_FORM_PAY=>:p_FORM_PAY, p_TR_DATE_ORDER=>:p_TR_DATE_ORDER, p_TR_NUM_ORDER=>:p_TR_NUM_ORDER, p_DATE_END_CONTR=>:p_DATE_END_CONTR, p_DATE_TRANSFER=>:p_DATE_TRANSFER, p_SIGN_COMB=>:p_SIGN_COMB, p_DATE_HIRE=>:p_DATE_HIRE, p_GR_WORK_ID=>:p_GR_WORK_ID, p_TYPE_TRANSFER_ID=>:p_TYPE_TRANSFER_ID, p_POS_ID=>:p_POS_ID, p_SUBDIV_ID=>:p_SUBDIV_ID, p_PER_NUM=>:p_PER_NUM);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_TRANSFER_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_POS_NOTE", OracleDbType.Varchar2, 0, "POS_NOTE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ADDITIONAL_VAC", OracleDbType.Decimal, 0, "ADDITIONAL_VAC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CHAR_TRANSFER_ID", OracleDbType.Decimal, 0, "CHAR_TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_WORKER_ID", OracleDbType.Decimal, 0, "WORKER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_HARMFUL_VAC", OracleDbType.Decimal, 0, "HARMFUL_VAC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_FORM_OPERATION_ID", OracleDbType.Decimal, 0, "FORM_OPERATION_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PROBA_PERIOD", OracleDbType.Decimal, 0, "PROBA_PERIOD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REASON_ID", OracleDbType.Decimal, 0, "REASON_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_CUR_WORK", OracleDbType.Decimal, 0, "SIGN_CUR_WORK");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DF_BOOK_DISMISS", OracleDbType.Date, 0, "DF_BOOK_DISMISS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DF_BOOK_ORDER", OracleDbType.Date, 0, "DF_BOOK_ORDER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_CONTR", OracleDbType.Date, 0, "DATE_CONTR");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CONTR_EMP", OracleDbType.Varchar2, 0, "CONTR_EMP");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CHAN_SIGN", OracleDbType.Decimal, 0, "CHAN_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_FROM_POSITION", OracleDbType.Decimal, 0, "FROM_POSITION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BASE_DOC_ID", OracleDbType.Decimal, 0, "BASE_DOC_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_HIRE_SIGN", OracleDbType.Decimal, 0, "HIRE_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SOURCE_ID", OracleDbType.Decimal, 0, "SOURCE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CHAR_WORK_ID", OracleDbType.Decimal, 0, "CHAR_WORK_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_FORM_PAY", OracleDbType.Decimal, 0, "FORM_PAY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TR_DATE_ORDER", OracleDbType.Date, 0, "TR_DATE_ORDER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TR_NUM_ORDER", OracleDbType.Varchar2, 0, "TR_NUM_ORDER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_END_CONTR", OracleDbType.Date, 0, "DATE_END_CONTR");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_TRANSFER", OracleDbType.Date, 0, "DATE_TRANSFER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_HIRE", OracleDbType.Date, 0, "DATE_HIRE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_GR_WORK_ID", OracleDbType.Decimal, 0, "GR_WORK_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_TRANSFER_ID", OracleDbType.Decimal, 0, "TYPE_TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_POS_ID", OracleDbType.Decimal, 0, "POS_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN APSTAFF.TRANSFER_DELETE(p_TRANSFER_ID => :p_TRANSFER_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
	}
		
	#endregion
    }


    [Table(Name="BANK_FOR_TYPE_ACCOUNT"), SchemaName("SALARY")]
    public partial class BankForTypeAccount : RowEntityBase
    {
        #region Class Members
        [Column(Name="BANK_FOR_TYPE_ACCOUNT_ID")]
        public Decimal? BankForTypeAccountID
        {
            get
            {
        		return GetDataRowField<Decimal?>("BANK_FOR_TYPE_ACCOUNT_ID");
                //return this.GetDataRowField<Decimal?>(() => BankForTypeAccountID);
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
        		return GetDataRowField<Decimal?>("TYPE_ACCOUNT_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeAccountID);
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
        		return GetDataRowField<Decimal?>("TYPE_BANK_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeBankID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeBankID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.BANK_FOR_TYPE_ACCOUNT_UPDATE(p_BANK_FOR_TYPE_ACCOUNT_ID=>:p_BANK_FOR_TYPE_ACCOUNT_ID, p_TYPE_ACCOUNT_ID=>:p_TYPE_ACCOUNT_ID, p_TYPE_BANK_ID=>:p_TYPE_BANK_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_BANK_FOR_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "BANK_FOR_TYPE_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_BANK_FOR_TYPE_ACCOUNT_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_ACCOUNT_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.BANK_FOR_TYPE_ACCOUNT_UPDATE(p_BANK_FOR_TYPE_ACCOUNT_ID=>:p_BANK_FOR_TYPE_ACCOUNT_ID, p_TYPE_ACCOUNT_ID=>:p_TYPE_ACCOUNT_ID, p_TYPE_BANK_ID=>:p_TYPE_BANK_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_BANK_FOR_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "BANK_FOR_TYPE_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_BANK_FOR_TYPE_ACCOUNT_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_ACCOUNT_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.BANK_FOR_TYPE_ACCOUNT_DELETE(p_BANK_FOR_TYPE_ACCOUNT_ID => :p_BANK_FOR_TYPE_ACCOUNT_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_BANK_FOR_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "BANK_FOR_TYPE_ACCOUNT_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Таблица бригад
    /// </summary>

    [Table(Name="BRIGAGE"), SchemaName("SALARY")]
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
        		return GetDataRowField<Decimal?>("BRIGAGE_ID");
                //return this.GetDataRowField<Decimal?>(() => BrigageID);
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
        		return GetDataRowField<DateTime?>("DATE_END_BRIGAGE");
                //return this.GetDataRowField<DateTime?>(() => DateEndBrigage);
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
        		return GetDataRowField<DateTime?>("DATE_BEGIN_BRIGAGE");
                //return this.GetDataRowField<DateTime?>(() => DateBeginBrigage);
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
        		return GetDataRowField<Decimal?>("SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => SubdivID);
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
        		return GetDataRowField<String>("GROUP_MASTER");
                //return this.GetDataRowField<String>(() => GroupMaster);
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
        		return GetDataRowField<String>("BRIGAGE_NAME");
                //return this.GetDataRowField<String>(() => BrigageName);
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
        		return GetDataRowField<String>("BRIGAGE_CODE");
                //return this.GetDataRowField<String>(() => BrigageCode);
            }
            set
            {
                UpdateDataRow<String>(() => BrigageCode, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.BRIGAGE_UPDATE(p_BRIGAGE_ID=>:p_BRIGAGE_ID, p_DATE_END_BRIGAGE=>:p_DATE_END_BRIGAGE, p_DATE_BEGIN_BRIGAGE=>:p_DATE_BEGIN_BRIGAGE, p_SUBDIV_ID=>:p_SUBDIV_ID, p_GROUP_MASTER=>:p_GROUP_MASTER, p_BRIGAGE_NAME=>:p_BRIGAGE_NAME, p_BRIGAGE_CODE=>:p_BRIGAGE_CODE);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_BRIGAGE_ID", OracleDbType.Decimal, 0, "BRIGAGE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_BRIGAGE_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_END_BRIGAGE", OracleDbType.Date, 0, "DATE_END_BRIGAGE");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_BEGIN_BRIGAGE", OracleDbType.Date, 0, "DATE_BEGIN_BRIGAGE");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_GROUP_MASTER", OracleDbType.Varchar2, 0, "GROUP_MASTER");
            _dataAdapter.InsertCommand.Parameters.Add("p_BRIGAGE_NAME", OracleDbType.Varchar2, 0, "BRIGAGE_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_BRIGAGE_CODE", OracleDbType.Varchar2, 0, "BRIGAGE_CODE");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.BRIGAGE_UPDATE(p_BRIGAGE_ID=>:p_BRIGAGE_ID, p_DATE_END_BRIGAGE=>:p_DATE_END_BRIGAGE, p_DATE_BEGIN_BRIGAGE=>:p_DATE_BEGIN_BRIGAGE, p_SUBDIV_ID=>:p_SUBDIV_ID, p_GROUP_MASTER=>:p_GROUP_MASTER, p_BRIGAGE_NAME=>:p_BRIGAGE_NAME, p_BRIGAGE_CODE=>:p_BRIGAGE_CODE);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_BRIGAGE_ID", OracleDbType.Decimal, 0, "BRIGAGE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_BRIGAGE_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_END_BRIGAGE", OracleDbType.Date, 0, "DATE_END_BRIGAGE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_BEGIN_BRIGAGE", OracleDbType.Date, 0, "DATE_BEGIN_BRIGAGE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_GROUP_MASTER", OracleDbType.Varchar2, 0, "GROUP_MASTER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BRIGAGE_NAME", OracleDbType.Varchar2, 0, "BRIGAGE_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BRIGAGE_CODE", OracleDbType.Varchar2, 0, "BRIGAGE_CODE");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.BRIGAGE_DELETE(p_BRIGAGE_ID => :p_BRIGAGE_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_BRIGAGE_ID", OracleDbType.Decimal, 0, "BRIGAGE_ID");
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
        		return GetDataRowField<Decimal?>("CARTULARY_ID");
                //return this.GetDataRowField<Decimal?>(() => CartularyID);
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
        		return GetDataRowField<Decimal?>("TYPE_CARTULARY_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeCartularyID);
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
        		return GetDataRowField<DateTime?>("DATE_CARTULARY");
                //return this.GetDataRowField<DateTime?>(() => DateCartulary);
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
        		return GetDataRowField<DateTime?>("DATE_CREATE");
                //return this.GetDataRowField<DateTime?>(() => DateCreate);
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
        		return GetDataRowField<DateTime?>("DATE_CLOSE_CART");
                //return this.GetDataRowField<DateTime?>(() => DateCloseCart);
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
        		return GetDataRowField<String>("CARTULARY_COMMENT");
                //return this.GetDataRowField<String>(() => CartularyComment);
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
        		return GetDataRowField<String>("CARTULARY_NUM");
                //return this.GetDataRowField<String>(() => CartularyNum);
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
        		return GetDataRowField<Decimal?>("CARTULARY_SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => CartularySubdivID);
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
        		return GetDataRowField<String>("CARTULARY_HEADER");
                //return this.GetDataRowField<String>(() => CartularyHeader);
            }
            set
            {
                UpdateDataRow<String>(() => CartularyHeader, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.CARTULARY_UPDATE(p_CARTULARY_ID=>:p_CARTULARY_ID, p_TYPE_CARTULARY_ID=>:p_TYPE_CARTULARY_ID, p_DATE_CARTULARY=>:p_DATE_CARTULARY, p_DATE_CREATE=>:p_DATE_CREATE, p_DATE_CLOSE_CART=>:p_DATE_CLOSE_CART, p_CARTULARY_COMMENT=>:p_CARTULARY_COMMENT, p_CARTULARY_NUM=>:p_CARTULARY_NUM, p_CARTULARY_SUBDIV_ID=>:p_CARTULARY_SUBDIV_ID, p_CARTULARY_HEADER=>:p_CARTULARY_HEADER);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_CARTULARY_ID", OracleDbType.Decimal, 0, "CARTULARY_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_CARTULARY_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_CARTULARY_ID", OracleDbType.Decimal, 0, "TYPE_CARTULARY_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_CARTULARY", OracleDbType.Date, 0, "DATE_CARTULARY");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_CREATE", OracleDbType.Date, 0, "DATE_CREATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_CLOSE_CART", OracleDbType.Date, 0, "DATE_CLOSE_CART");
            _dataAdapter.InsertCommand.Parameters.Add("p_CARTULARY_COMMENT", OracleDbType.Varchar2, 0, "CARTULARY_COMMENT");
            _dataAdapter.InsertCommand.Parameters.Add("p_CARTULARY_NUM", OracleDbType.Varchar2, 0, "CARTULARY_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_CARTULARY_SUBDIV_ID", OracleDbType.Decimal, 0, "CARTULARY_SUBDIV_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_CARTULARY_HEADER", OracleDbType.Varchar2, 0, "CARTULARY_HEADER");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.CARTULARY_UPDATE(p_CARTULARY_ID=>:p_CARTULARY_ID, p_TYPE_CARTULARY_ID=>:p_TYPE_CARTULARY_ID, p_DATE_CARTULARY=>:p_DATE_CARTULARY, p_DATE_CREATE=>:p_DATE_CREATE, p_DATE_CLOSE_CART=>:p_DATE_CLOSE_CART, p_CARTULARY_COMMENT=>:p_CARTULARY_COMMENT, p_CARTULARY_NUM=>:p_CARTULARY_NUM, p_CARTULARY_SUBDIV_ID=>:p_CARTULARY_SUBDIV_ID, p_CARTULARY_HEADER=>:p_CARTULARY_HEADER);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_CARTULARY_ID", OracleDbType.Decimal, 0, "CARTULARY_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_CARTULARY_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_CARTULARY_ID", OracleDbType.Decimal, 0, "TYPE_CARTULARY_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_CARTULARY", OracleDbType.Date, 0, "DATE_CARTULARY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_CREATE", OracleDbType.Date, 0, "DATE_CREATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_CLOSE_CART", OracleDbType.Date, 0, "DATE_CLOSE_CART");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CARTULARY_COMMENT", OracleDbType.Varchar2, 0, "CARTULARY_COMMENT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CARTULARY_NUM", OracleDbType.Varchar2, 0, "CARTULARY_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CARTULARY_SUBDIV_ID", OracleDbType.Decimal, 0, "CARTULARY_SUBDIV_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CARTULARY_HEADER", OracleDbType.Varchar2, 0, "CARTULARY_HEADER");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.CARTULARY_DELETE(p_CARTULARY_ID => :p_CARTULARY_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_CARTULARY_ID", OracleDbType.Decimal, 0, "CARTULARY_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Счет сотрудника
    /// </summary>

    [Table(Name="CLIENT_ACCOUNT"), SchemaName("SALARY")]
    public partial class ClientAccount : RowEntityBase
    {
        #region Class Members
        [Column(Name="CLIENT_ACCOUNT_ID", IsPrimaryKey=true)]
        public Decimal? ClientAccountID
        {
            get
            {
        		return GetDataRowField<Decimal?>("CLIENT_ACCOUNT_ID");
                //return this.GetDataRowField<Decimal?>(() => ClientAccountID);
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
        		return GetDataRowField<DateTime?>("DATE_DOC");
                //return this.GetDataRowField<DateTime?>(() => DateDoc);
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
        		return GetDataRowField<Decimal?>("CODE_DOC");
                //return this.GetDataRowField<Decimal?>(() => CodeDoc);
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
        		return GetDataRowField<String>("PER_INSURANCE_NUM");
                //return this.GetDataRowField<String>(() => PerInsuranceNum);
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
        		return GetDataRowField<String>("INSURANCE_NUM");
                //return this.GetDataRowField<String>(() => InsuranceNum);
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
        		return GetDataRowField<Decimal?>("TYPE_ACCOUNT_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeAccountID);
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
        		return GetDataRowField<String>("PLF_INDEX");
                //return this.GetDataRowField<String>(() => PlfIndex);
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
        		return GetDataRowField<String>("PLF_ADDRESS");
                //return this.GetDataRowField<String>(() => PlfAddress);
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
        		return GetDataRowField<String>("PLF_NAME");
                //return this.GetDataRowField<String>(() => PlfName);
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
        		return GetDataRowField<String>("NUMBER_CARD");
                //return this.GetDataRowField<String>(() => NumberCard);
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
        		return GetDataRowField<String>("GET_CITY");
                //return this.GetDataRowField<String>(() => GetCity);
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
        		return GetDataRowField<String>("GET_PLACE");
                //return this.GetDataRowField<String>(() => GetPlace);
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
        		return GetDataRowField<String>("PASSPORT_NUMBER");
                //return this.GetDataRowField<String>(() => PassportNumber);
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
        		return GetDataRowField<String>("PASSPORT_SERIES");
                //return this.GetDataRowField<String>(() => PassportSeries);
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
        		return GetDataRowField<String>("OWNER_MIDDLE_NAME");
                //return this.GetDataRowField<String>(() => OwnerMiddleName);
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
        		return GetDataRowField<String>("OWNER_FAMILY");
                //return this.GetDataRowField<String>(() => OwnerFamily);
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
        		return GetDataRowField<String>("OWNER_NAME");
                //return this.GetDataRowField<String>(() => OwnerName);
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
        		return GetDataRowField<Decimal?>("TYPE_BANK_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeBankID);
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
        		return GetDataRowField<String>("NUMBER_ACCOUNT");
                //return this.GetDataRowField<String>(() => NumberAccount);
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
        		return GetDataRowField<String>("COMPANY_NAME");
                //return this.GetDataRowField<String>(() => CompanyName);
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
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TransferID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.CLIENT_ACCOUNT_UPDATE(p_CLIENT_ACCOUNT_ID=>:p_CLIENT_ACCOUNT_ID, p_DATE_DOC=>:p_DATE_DOC, p_CODE_DOC=>:p_CODE_DOC, p_PER_INSURANCE_NUM=>:p_PER_INSURANCE_NUM, p_INSURANCE_NUM=>:p_INSURANCE_NUM, p_TYPE_ACCOUNT_ID=>:p_TYPE_ACCOUNT_ID, p_PLF_INDEX=>:p_PLF_INDEX, p_PLF_ADDRESS=>:p_PLF_ADDRESS, p_PLF_NAME=>:p_PLF_NAME, p_NUMBER_CARD=>:p_NUMBER_CARD, p_GET_CITY=>:p_GET_CITY, p_GET_PLACE=>:p_GET_PLACE, p_PASSPORT_NUMBER=>:p_PASSPORT_NUMBER, p_PASSPORT_SERIES=>:p_PASSPORT_SERIES, p_OWNER_MIDDLE_NAME=>:p_OWNER_MIDDLE_NAME, p_OWNER_FAMILY=>:p_OWNER_FAMILY, p_OWNER_NAME=>:p_OWNER_NAME, p_TYPE_BANK_ID=>:p_TYPE_BANK_ID, p_NUMBER_ACCOUNT=>:p_NUMBER_ACCOUNT, p_COMPANY_NAME=>:p_COMPANY_NAME, p_TRANSFER_ID=>:p_TRANSFER_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_CLIENT_ACCOUNT_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_DOC", OracleDbType.Date, 0, "DATE_DOC");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_DOC", OracleDbType.Decimal, 0, "CODE_DOC");
            _dataAdapter.InsertCommand.Parameters.Add("p_PER_INSURANCE_NUM", OracleDbType.Varchar2, 0, "PER_INSURANCE_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_INSURANCE_NUM", OracleDbType.Varchar2, 0, "INSURANCE_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_ACCOUNT_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PLF_INDEX", OracleDbType.Varchar2, 0, "PLF_INDEX");
            _dataAdapter.InsertCommand.Parameters.Add("p_PLF_ADDRESS", OracleDbType.Varchar2, 0, "PLF_ADDRESS");
            _dataAdapter.InsertCommand.Parameters.Add("p_PLF_NAME", OracleDbType.Varchar2, 0, "PLF_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_NUMBER_CARD", OracleDbType.Varchar2, 0, "NUMBER_CARD");
            _dataAdapter.InsertCommand.Parameters.Add("p_GET_CITY", OracleDbType.Varchar2, 0, "GET_CITY");
            _dataAdapter.InsertCommand.Parameters.Add("p_GET_PLACE", OracleDbType.Varchar2, 0, "GET_PLACE");
            _dataAdapter.InsertCommand.Parameters.Add("p_PASSPORT_NUMBER", OracleDbType.Varchar2, 0, "PASSPORT_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_PASSPORT_SERIES", OracleDbType.Varchar2, 0, "PASSPORT_SERIES");
            _dataAdapter.InsertCommand.Parameters.Add("p_OWNER_MIDDLE_NAME", OracleDbType.Varchar2, 0, "OWNER_MIDDLE_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_OWNER_FAMILY", OracleDbType.Varchar2, 0, "OWNER_FAMILY");
            _dataAdapter.InsertCommand.Parameters.Add("p_OWNER_NAME", OracleDbType.Varchar2, 0, "OWNER_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_NUMBER_ACCOUNT", OracleDbType.Varchar2, 0, "NUMBER_ACCOUNT");
            _dataAdapter.InsertCommand.Parameters.Add("p_COMPANY_NAME", OracleDbType.Varchar2, 0, "COMPANY_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.CLIENT_ACCOUNT_UPDATE(p_CLIENT_ACCOUNT_ID=>:p_CLIENT_ACCOUNT_ID, p_DATE_DOC=>:p_DATE_DOC, p_CODE_DOC=>:p_CODE_DOC, p_PER_INSURANCE_NUM=>:p_PER_INSURANCE_NUM, p_INSURANCE_NUM=>:p_INSURANCE_NUM, p_TYPE_ACCOUNT_ID=>:p_TYPE_ACCOUNT_ID, p_PLF_INDEX=>:p_PLF_INDEX, p_PLF_ADDRESS=>:p_PLF_ADDRESS, p_PLF_NAME=>:p_PLF_NAME, p_NUMBER_CARD=>:p_NUMBER_CARD, p_GET_CITY=>:p_GET_CITY, p_GET_PLACE=>:p_GET_PLACE, p_PASSPORT_NUMBER=>:p_PASSPORT_NUMBER, p_PASSPORT_SERIES=>:p_PASSPORT_SERIES, p_OWNER_MIDDLE_NAME=>:p_OWNER_MIDDLE_NAME, p_OWNER_FAMILY=>:p_OWNER_FAMILY, p_OWNER_NAME=>:p_OWNER_NAME, p_TYPE_BANK_ID=>:p_TYPE_BANK_ID, p_NUMBER_ACCOUNT=>:p_NUMBER_ACCOUNT, p_COMPANY_NAME=>:p_COMPANY_NAME, p_TRANSFER_ID=>:p_TRANSFER_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_CLIENT_ACCOUNT_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_DOC", OracleDbType.Date, 0, "DATE_DOC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_DOC", OracleDbType.Decimal, 0, "CODE_DOC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PER_INSURANCE_NUM", OracleDbType.Varchar2, 0, "PER_INSURANCE_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_INSURANCE_NUM", OracleDbType.Varchar2, 0, "INSURANCE_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_ACCOUNT_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PLF_INDEX", OracleDbType.Varchar2, 0, "PLF_INDEX");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PLF_ADDRESS", OracleDbType.Varchar2, 0, "PLF_ADDRESS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PLF_NAME", OracleDbType.Varchar2, 0, "PLF_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NUMBER_CARD", OracleDbType.Varchar2, 0, "NUMBER_CARD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_GET_CITY", OracleDbType.Varchar2, 0, "GET_CITY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_GET_PLACE", OracleDbType.Varchar2, 0, "GET_PLACE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PASSPORT_NUMBER", OracleDbType.Varchar2, 0, "PASSPORT_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PASSPORT_SERIES", OracleDbType.Varchar2, 0, "PASSPORT_SERIES");
            _dataAdapter.UpdateCommand.Parameters.Add("p_OWNER_MIDDLE_NAME", OracleDbType.Varchar2, 0, "OWNER_MIDDLE_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_OWNER_FAMILY", OracleDbType.Varchar2, 0, "OWNER_FAMILY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_OWNER_NAME", OracleDbType.Varchar2, 0, "OWNER_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NUMBER_ACCOUNT", OracleDbType.Varchar2, 0, "NUMBER_ACCOUNT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COMPANY_NAME", OracleDbType.Varchar2, 0, "COMPANY_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.CLIENT_ACCOUNT_DELETE(p_CLIENT_ACCOUNT_ID => :p_CLIENT_ACCOUNT_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Общие данные по счету сотрудника
    /// </summary>

    [Table(Name="CLIENT_ACCOUNT_DATA"), SchemaName("SALARY")]
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
        		return GetDataRowField<Decimal?>("CLIENT_ACCOUNT_ID");
                //return this.GetDataRowField<Decimal?>(() => ClientAccountID);
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
        		return GetDataRowField<String>("NUMBER_ACCOUNT");
                //return this.GetDataRowField<String>(() => NumberAccount);
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
        		return GetDataRowField<String>("NUMBER_CARD");
                //return this.GetDataRowField<String>(() => NumberCard);
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
        		return GetDataRowField<Decimal?>("TYPE_ACCONT_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeAccontID);
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
        		return GetDataRowField<Decimal?>("TYPE_BANK_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeBankID);
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
        		return GetDataRowField<String>("BANK_NAME");
                //return this.GetDataRowField<String>(() => BankName);
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
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
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
        		return GetDataRowField<Decimal?>("ORDER_NUMBER");
                //return this.GetDataRowField<Decimal?>(() => OrderNumber);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OrderNumber, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.CLIENT_ACCOUNT_DATA_UPDATE(p_CLIENT_ACCOUNT_ID=>:p_CLIENT_ACCOUNT_ID, p_NUMBER_ACCOUNT=>:p_NUMBER_ACCOUNT, p_NUMBER_CARD=>:p_NUMBER_CARD, p_TYPE_ACCONT_ID=>:p_TYPE_ACCONT_ID, p_TYPE_BANK_ID=>:p_TYPE_BANK_ID, p_BANK_NAME=>:p_BANK_NAME, p_PER_NUM=>:p_PER_NUM, p_ORDER_NUMBER=>:p_ORDER_NUMBER);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_NUMBER_ACCOUNT", OracleDbType.Varchar2, 0, "NUMBER_ACCOUNT");
            _dataAdapter.InsertCommand.Parameters.Add("p_NUMBER_CARD", OracleDbType.Varchar2, 0, "NUMBER_CARD");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_ACCONT_ID", OracleDbType.Decimal, 0, "TYPE_ACCONT_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_BANK_NAME", OracleDbType.Varchar2, 0, "BANK_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_ORDER_NUMBER", OracleDbType.Decimal, 0, "ORDER_NUMBER");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.CLIENT_ACCOUNT_DATA_UPDATE(p_CLIENT_ACCOUNT_ID=>:p_CLIENT_ACCOUNT_ID, p_NUMBER_ACCOUNT=>:p_NUMBER_ACCOUNT, p_NUMBER_CARD=>:p_NUMBER_CARD, p_TYPE_ACCONT_ID=>:p_TYPE_ACCONT_ID, p_TYPE_BANK_ID=>:p_TYPE_BANK_ID, p_BANK_NAME=>:p_BANK_NAME, p_PER_NUM=>:p_PER_NUM, p_ORDER_NUMBER=>:p_ORDER_NUMBER);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NUMBER_ACCOUNT", OracleDbType.Varchar2, 0, "NUMBER_ACCOUNT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NUMBER_CARD", OracleDbType.Varchar2, 0, "NUMBER_CARD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_ACCONT_ID", OracleDbType.Decimal, 0, "TYPE_ACCONT_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BANK_NAME", OracleDbType.Varchar2, 0, "BANK_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ORDER_NUMBER", OracleDbType.Decimal, 0, "ORDER_NUMBER");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.CLIENT_ACCOUNT_DATA_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        		}
		
	#endregion
    }


    [Table(Name="CLIENT_RETENT_RELATION"), SchemaName("SALARY")]
    public partial class ClientRetentRelation : RowEntityBase
    {
        #region Class Members
        [Column(Name="CLIENT_RETENT_RELATION_ID")]
        public Decimal? ClientRetentRelationID
        {
            get
            {
        		return GetDataRowField<Decimal?>("CLIENT_RETENT_RELATION_ID");
                //return this.GetDataRowField<Decimal?>(() => ClientRetentRelationID);
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
        		return GetDataRowField<String>("RELATION_COMMENT");
                //return this.GetDataRowField<String>(() => RelationComment);
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
        		return GetDataRowField<String>("BCC_CODE");
                //return this.GetDataRowField<String>(() => BccCode);
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
        		return GetDataRowField<String>("OKATO");
                //return this.GetDataRowField<String>(() => Okato);
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
        		return GetDataRowField<Decimal?>("RESTRICT_SUM");
                //return this.GetDataRowField<Decimal?>(() => RestrictSum);
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
        		return GetDataRowField<DateTime?>("DATE_END_RELATION");
                //return this.GetDataRowField<DateTime?>(() => DateEndRelation);
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
        		return GetDataRowField<DateTime?>("DATE_BEGIN_RELATION");
                //return this.GetDataRowField<DateTime?>(() => DateBeginRelation);
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
        		return GetDataRowField<Decimal?>("CLIENT_ACCOUNT_ID");
                //return this.GetDataRowField<Decimal?>(() => ClientAccountID);
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
        		return GetDataRowField<Decimal?>("RETENTION_ID");
                //return this.GetDataRowField<Decimal?>(() => RetentionID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentionID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.CLIENT_RETENT_RELATION_UPDATE(p_CLIENT_RETENT_RELATION_ID=>:p_CLIENT_RETENT_RELATION_ID, p_RELATION_COMMENT=>:p_RELATION_COMMENT, p_BCC_CODE=>:p_BCC_CODE, p_OKATO=>:p_OKATO, p_RESTRICT_SUM=>:p_RESTRICT_SUM, p_DATE_END_RELATION=>:p_DATE_END_RELATION, p_DATE_BEGIN_RELATION=>:p_DATE_BEGIN_RELATION, p_CLIENT_ACCOUNT_ID=>:p_CLIENT_ACCOUNT_ID, p_RETENTION_ID=>:p_RETENTION_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_CLIENT_RETENT_RELATION_ID", OracleDbType.Decimal, 0, "CLIENT_RETENT_RELATION_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_CLIENT_RETENT_RELATION_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_RELATION_COMMENT", OracleDbType.Varchar2, 0, "RELATION_COMMENT");
            _dataAdapter.InsertCommand.Parameters.Add("p_BCC_CODE", OracleDbType.Varchar2, 0, "BCC_CODE");
            _dataAdapter.InsertCommand.Parameters.Add("p_OKATO", OracleDbType.Varchar2, 0, "OKATO");
            _dataAdapter.InsertCommand.Parameters.Add("p_RESTRICT_SUM", OracleDbType.Decimal, 0, "RESTRICT_SUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_END_RELATION", OracleDbType.Date, 0, "DATE_END_RELATION");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_BEGIN_RELATION", OracleDbType.Date, 0, "DATE_BEGIN_RELATION");
            _dataAdapter.InsertCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.CLIENT_RETENT_RELATION_UPDATE(p_CLIENT_RETENT_RELATION_ID=>:p_CLIENT_RETENT_RELATION_ID, p_RELATION_COMMENT=>:p_RELATION_COMMENT, p_BCC_CODE=>:p_BCC_CODE, p_OKATO=>:p_OKATO, p_RESTRICT_SUM=>:p_RESTRICT_SUM, p_DATE_END_RELATION=>:p_DATE_END_RELATION, p_DATE_BEGIN_RELATION=>:p_DATE_BEGIN_RELATION, p_CLIENT_ACCOUNT_ID=>:p_CLIENT_ACCOUNT_ID, p_RETENTION_ID=>:p_RETENTION_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_CLIENT_RETENT_RELATION_ID", OracleDbType.Decimal, 0, "CLIENT_RETENT_RELATION_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_CLIENT_RETENT_RELATION_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_RELATION_COMMENT", OracleDbType.Varchar2, 0, "RELATION_COMMENT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BCC_CODE", OracleDbType.Varchar2, 0, "BCC_CODE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_OKATO", OracleDbType.Varchar2, 0, "OKATO");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RESTRICT_SUM", OracleDbType.Decimal, 0, "RESTRICT_SUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_END_RELATION", OracleDbType.Date, 0, "DATE_END_RELATION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_BEGIN_RELATION", OracleDbType.Date, 0, "DATE_BEGIN_RELATION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.CLIENT_RETENT_RELATION_DELETE(p_CLIENT_RETENT_RELATION_ID => :p_CLIENT_RETENT_RELATION_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_CLIENT_RETENT_RELATION_ID", OracleDbType.Decimal, 0, "CLIENT_RETENT_RELATION_ID");
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
        		return GetDataRowField<Decimal?>("DOCUM_TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => DocumTransferID);
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
        		return GetDataRowField<DateTime?>("DATE_DOCUM");
                //return this.GetDataRowField<DateTime?>(() => DateDocum);
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
        		return GetDataRowField<String>("CODE_DOCUM");
                //return this.GetDataRowField<String>(() => CodeDocum);
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
        		return GetDataRowField<Decimal?>("TYPE_CARTULARY_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeCartularyID);
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
        		return GetDataRowField<String>("DOCUM_COMMENT");
                //return this.GetDataRowField<String>(() => DocumComment);
            }
            set
            {
                UpdateDataRow<String>(() => DocumComment, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.DOCUM_TRANSFER_UPDATE(p_DOCUM_TRANSFER_ID=>:p_DOCUM_TRANSFER_ID, p_DATE_DOCUM=>:p_DATE_DOCUM, p_CODE_DOCUM=>:p_CODE_DOCUM, p_TYPE_CARTULARY_ID=>:p_TYPE_CARTULARY_ID, p_DOCUM_COMMENT=>:p_DOCUM_COMMENT);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_DOCUM_TRANSFER_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_DOCUM_TRANSFER_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_DOCUM", OracleDbType.Date, 0, "DATE_DOCUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_DOCUM", OracleDbType.Varchar2, 0, "CODE_DOCUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_CARTULARY_ID", OracleDbType.Decimal, 0, "TYPE_CARTULARY_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOCUM_COMMENT", OracleDbType.Varchar2, 0, "DOCUM_COMMENT");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.DOCUM_TRANSFER_UPDATE(p_DOCUM_TRANSFER_ID=>:p_DOCUM_TRANSFER_ID, p_DATE_DOCUM=>:p_DATE_DOCUM, p_CODE_DOCUM=>:p_CODE_DOCUM, p_TYPE_CARTULARY_ID=>:p_TYPE_CARTULARY_ID, p_DOCUM_COMMENT=>:p_DOCUM_COMMENT);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_DOCUM_TRANSFER_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_DOCUM_TRANSFER_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_DOCUM", OracleDbType.Date, 0, "DATE_DOCUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_DOCUM", OracleDbType.Varchar2, 0, "CODE_DOCUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_CARTULARY_ID", OracleDbType.Decimal, 0, "TYPE_CARTULARY_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOCUM_COMMENT", OracleDbType.Varchar2, 0, "DOCUM_COMMENT");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.DOCUM_TRANSFER_DELETE(p_DOCUM_TRANSFER_ID => :p_DOCUM_TRANSFER_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_DOCUM_TRANSFER_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_ID");
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
        		return GetDataRowField<Decimal?>("DOCUM_TRANSFER_RELATION_ID");
                //return this.GetDataRowField<Decimal?>(() => DocumTransferRelationID);
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
        		return GetDataRowField<Decimal?>("DOCUM_TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => DocumTransferID);
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
        		return GetDataRowField<DateTime?>("CHECK_DATE");
                //return this.GetDataRowField<DateTime?>(() => CheckDate);
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
        		return GetDataRowField<String>("FIN_PLAN_CODE");
                //return this.GetDataRowField<String>(() => FinPlanCode);
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
        		return GetDataRowField<Decimal?>("SALARY_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryID);
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
        		return GetDataRowField<Decimal?>("CLIENT_ACCOUNT_ID");
                //return this.GetDataRowField<Decimal?>(() => ClientAccountID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ClientAccountID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.DOCUM_TRANSFER_RELATION_UPDATE(p_DOCUM_TRANSFER_RELATION_ID=>:p_DOCUM_TRANSFER_RELATION_ID, p_DOCUM_TRANSFER_ID=>:p_DOCUM_TRANSFER_ID, p_CHECK_DATE=>:p_CHECK_DATE, p_FIN_PLAN_CODE=>:p_FIN_PLAN_CODE, p_SALARY_ID=>:p_SALARY_ID, p_CLIENT_ACCOUNT_ID=>:p_CLIENT_ACCOUNT_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_DOCUM_TRANSFER_RELATION_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_RELATION_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_DOCUM_TRANSFER_RELATION_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_DOCUM_TRANSFER_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_CHECK_DATE", OracleDbType.Date, 0, "CHECK_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_FIN_PLAN_CODE", OracleDbType.Varchar2, 0, "FIN_PLAN_CODE");
            _dataAdapter.InsertCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, 0, "SALARY_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.DOCUM_TRANSFER_RELATION_UPDATE(p_DOCUM_TRANSFER_RELATION_ID=>:p_DOCUM_TRANSFER_RELATION_ID, p_DOCUM_TRANSFER_ID=>:p_DOCUM_TRANSFER_ID, p_CHECK_DATE=>:p_CHECK_DATE, p_FIN_PLAN_CODE=>:p_FIN_PLAN_CODE, p_SALARY_ID=>:p_SALARY_ID, p_CLIENT_ACCOUNT_ID=>:p_CLIENT_ACCOUNT_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_DOCUM_TRANSFER_RELATION_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_RELATION_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_DOCUM_TRANSFER_RELATION_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOCUM_TRANSFER_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CHECK_DATE", OracleDbType.Date, 0, "CHECK_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_FIN_PLAN_CODE", OracleDbType.Varchar2, 0, "FIN_PLAN_CODE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, 0, "SALARY_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.DOCUM_TRANSFER_RELATION_DELETE(p_DOCUM_TRANSFER_RELATION_ID => :p_DOCUM_TRANSFER_RELATION_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_DOCUM_TRANSFER_RELATION_ID", OracleDbType.Decimal, 0, "DOCUM_TRANSFER_RELATION_ID");
	}
		
	#endregion
    }


    [Table(Name="LOAN"), SchemaName("SALARY")]
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
        		return GetDataRowField<Decimal?>("LOAN_ID");
                //return this.GetDataRowField<Decimal?>(() => LoanID);
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
        		return GetDataRowField<Decimal?>("CLIENT_ACCOUNT_ID");
                //return this.GetDataRowField<Decimal?>(() => ClientAccountID);
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
        		return GetDataRowField<DateTime?>("LOAN_DATE_END");
                //return this.GetDataRowField<DateTime?>(() => LoanDateEnd);
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
        		return GetDataRowField<Decimal?>("PARENT_LOAN_ID");
                //return this.GetDataRowField<Decimal?>(() => ParentLoanID);
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
        		return GetDataRowField<Decimal?>("SIGN_REGISTRATION_DOG");
                //return this.GetDataRowField<Decimal?>(() => SignRegistrationDog);
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
        		return GetDataRowField<Decimal?>("SIGN_ARCHIVE");
                //return this.GetDataRowField<Decimal?>(() => SignArchive);
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
        		return GetDataRowField<Decimal?>("TYPE_LOAN_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeLoanID);
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
        		return GetDataRowField<Decimal?>("PURPOSE_LOAN_ID");
                //return this.GetDataRowField<Decimal?>(() => PurposeLoanID);
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
        		return GetDataRowField<Decimal?>("SIGN_MATERIAL_BENEFIT");
                //return this.GetDataRowField<Decimal?>(() => SignMaterialBenefit);
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
        		return GetDataRowField<Decimal?>("SIGN_RETENTION");
                //return this.GetDataRowField<Decimal?>(() => SignRetention);
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
        		return GetDataRowField<Decimal?>("RETENTION_BY_FACT");
                //return this.GetDataRowField<Decimal?>(() => RetentionByFact);
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
        		return GetDataRowField<Decimal?>("RETENTION_BY_CONTRACT");
                //return this.GetDataRowField<Decimal?>(() => RetentionByContract);
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
        		return GetDataRowField<Decimal?>("ORDINAL_NUMBER");
                //return this.GetDataRowField<Decimal?>(() => OrdinalNumber);
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
        		return GetDataRowField<Decimal?>("LOAN_TERM");
                //return this.GetDataRowField<Decimal?>(() => LoanTerm);
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
        		return GetDataRowField<Decimal?>("LOAN_SUM");
                //return this.GetDataRowField<Decimal?>(() => LoanSum);
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
        		return GetDataRowField<DateTime?>("LOAN_DATE");
                //return this.GetDataRowField<DateTime?>(() => LoanDate);
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
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
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
        		return GetDataRowField<DateTime?>("CONTRACT_DATE");
                //return this.GetDataRowField<DateTime?>(() => ContractDate);
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
        		return GetDataRowField<String>("CONTRACT_NUMBER");
                //return this.GetDataRowField<String>(() => ContractNumber);
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
        		return GetDataRowField<DateTime?>("PROTOCOL_DATE");
                //return this.GetDataRowField<DateTime?>(() => ProtocolDate);
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
        		return GetDataRowField<String>("PROTOCOL_NUMBER");
                //return this.GetDataRowField<String>(() => ProtocolNumber);
            }
            set
            {
                UpdateDataRow<String>(() => ProtocolNumber, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.LOAN_UPDATE(p_LOAN_ID=>:p_LOAN_ID, p_CLIENT_ACCOUNT_ID=>:p_CLIENT_ACCOUNT_ID, p_LOAN_DATE_END=>:p_LOAN_DATE_END, p_PARENT_LOAN_ID=>:p_PARENT_LOAN_ID, p_SIGN_REGISTRATION_DOG=>:p_SIGN_REGISTRATION_DOG, p_SIGN_ARCHIVE=>:p_SIGN_ARCHIVE, p_TYPE_LOAN_ID=>:p_TYPE_LOAN_ID, p_PURPOSE_LOAN_ID=>:p_PURPOSE_LOAN_ID, p_SIGN_MATERIAL_BENEFIT=>:p_SIGN_MATERIAL_BENEFIT, p_SIGN_RETENTION=>:p_SIGN_RETENTION, p_RETENTION_BY_FACT=>:p_RETENTION_BY_FACT, p_RETENTION_BY_CONTRACT=>:p_RETENTION_BY_CONTRACT, p_ORDINAL_NUMBER=>:p_ORDINAL_NUMBER, p_LOAN_TERM=>:p_LOAN_TERM, p_LOAN_SUM=>:p_LOAN_SUM, p_LOAN_DATE=>:p_LOAN_DATE, p_TRANSFER_ID=>:p_TRANSFER_ID, p_CONTRACT_DATE=>:p_CONTRACT_DATE, p_CONTRACT_NUMBER=>:p_CONTRACT_NUMBER, p_PROTOCOL_DATE=>:p_PROTOCOL_DATE, p_PROTOCOL_NUMBER=>:p_PROTOCOL_NUMBER);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal, 0, "LOAN_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_LOAN_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_LOAN_DATE_END", OracleDbType.Date, 0, "LOAN_DATE_END");
            _dataAdapter.InsertCommand.Parameters.Add("p_PARENT_LOAN_ID", OracleDbType.Decimal, 0, "PARENT_LOAN_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_REGISTRATION_DOG", OracleDbType.Decimal, 0, "SIGN_REGISTRATION_DOG");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_ARCHIVE", OracleDbType.Decimal, 0, "SIGN_ARCHIVE");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_LOAN_ID", OracleDbType.Decimal, 0, "TYPE_LOAN_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PURPOSE_LOAN_ID", OracleDbType.Decimal, 0, "PURPOSE_LOAN_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_MATERIAL_BENEFIT", OracleDbType.Decimal, 0, "SIGN_MATERIAL_BENEFIT");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_RETENTION", OracleDbType.Decimal, 0, "SIGN_RETENTION");
            _dataAdapter.InsertCommand.Parameters.Add("p_RETENTION_BY_FACT", OracleDbType.Decimal, 0, "RETENTION_BY_FACT");
            _dataAdapter.InsertCommand.Parameters.Add("p_RETENTION_BY_CONTRACT", OracleDbType.Decimal, 0, "RETENTION_BY_CONTRACT");
            _dataAdapter.InsertCommand.Parameters.Add("p_ORDINAL_NUMBER", OracleDbType.Decimal, 0, "ORDINAL_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_LOAN_TERM", OracleDbType.Decimal, 0, "LOAN_TERM");
            _dataAdapter.InsertCommand.Parameters.Add("p_LOAN_SUM", OracleDbType.Decimal, 0, "LOAN_SUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_LOAN_DATE", OracleDbType.Date, 0, "LOAN_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_CONTRACT_DATE", OracleDbType.Date, 0, "CONTRACT_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_CONTRACT_NUMBER", OracleDbType.Varchar2, 0, "CONTRACT_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_PROTOCOL_DATE", OracleDbType.Date, 0, "PROTOCOL_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_PROTOCOL_NUMBER", OracleDbType.Varchar2, 0, "PROTOCOL_NUMBER");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.LOAN_UPDATE(p_LOAN_ID=>:p_LOAN_ID, p_CLIENT_ACCOUNT_ID=>:p_CLIENT_ACCOUNT_ID, p_LOAN_DATE_END=>:p_LOAN_DATE_END, p_PARENT_LOAN_ID=>:p_PARENT_LOAN_ID, p_SIGN_REGISTRATION_DOG=>:p_SIGN_REGISTRATION_DOG, p_SIGN_ARCHIVE=>:p_SIGN_ARCHIVE, p_TYPE_LOAN_ID=>:p_TYPE_LOAN_ID, p_PURPOSE_LOAN_ID=>:p_PURPOSE_LOAN_ID, p_SIGN_MATERIAL_BENEFIT=>:p_SIGN_MATERIAL_BENEFIT, p_SIGN_RETENTION=>:p_SIGN_RETENTION, p_RETENTION_BY_FACT=>:p_RETENTION_BY_FACT, p_RETENTION_BY_CONTRACT=>:p_RETENTION_BY_CONTRACT, p_ORDINAL_NUMBER=>:p_ORDINAL_NUMBER, p_LOAN_TERM=>:p_LOAN_TERM, p_LOAN_SUM=>:p_LOAN_SUM, p_LOAN_DATE=>:p_LOAN_DATE, p_TRANSFER_ID=>:p_TRANSFER_ID, p_CONTRACT_DATE=>:p_CONTRACT_DATE, p_CONTRACT_NUMBER=>:p_CONTRACT_NUMBER, p_PROTOCOL_DATE=>:p_PROTOCOL_DATE, p_PROTOCOL_NUMBER=>:p_PROTOCOL_NUMBER);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal, 0, "LOAN_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_LOAN_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_CLIENT_ACCOUNT_ID", OracleDbType.Decimal, 0, "CLIENT_ACCOUNT_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_LOAN_DATE_END", OracleDbType.Date, 0, "LOAN_DATE_END");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PARENT_LOAN_ID", OracleDbType.Decimal, 0, "PARENT_LOAN_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_REGISTRATION_DOG", OracleDbType.Decimal, 0, "SIGN_REGISTRATION_DOG");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_ARCHIVE", OracleDbType.Decimal, 0, "SIGN_ARCHIVE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_LOAN_ID", OracleDbType.Decimal, 0, "TYPE_LOAN_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PURPOSE_LOAN_ID", OracleDbType.Decimal, 0, "PURPOSE_LOAN_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_MATERIAL_BENEFIT", OracleDbType.Decimal, 0, "SIGN_MATERIAL_BENEFIT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_RETENTION", OracleDbType.Decimal, 0, "SIGN_RETENTION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RETENTION_BY_FACT", OracleDbType.Decimal, 0, "RETENTION_BY_FACT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RETENTION_BY_CONTRACT", OracleDbType.Decimal, 0, "RETENTION_BY_CONTRACT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ORDINAL_NUMBER", OracleDbType.Decimal, 0, "ORDINAL_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_LOAN_TERM", OracleDbType.Decimal, 0, "LOAN_TERM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_LOAN_SUM", OracleDbType.Decimal, 0, "LOAN_SUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_LOAN_DATE", OracleDbType.Date, 0, "LOAN_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CONTRACT_DATE", OracleDbType.Date, 0, "CONTRACT_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CONTRACT_NUMBER", OracleDbType.Varchar2, 0, "CONTRACT_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PROTOCOL_DATE", OracleDbType.Date, 0, "PROTOCOL_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PROTOCOL_NUMBER", OracleDbType.Varchar2, 0, "PROTOCOL_NUMBER");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.LOAN_DELETE(p_LOAN_ID => :p_LOAN_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_LOAN_ID", OracleDbType.Decimal, 0, "LOAN_ID");
	}
		
	#endregion
    }


    [Table(Name="PAYMENT_TYPE"), SchemaName("SALARY")]
    public partial class PaymentType : RowEntityBase
    {
        #region Class Members
        [Column(Name="PAYMENT_TYPE_ID", IsPrimaryKey=true)]
        public Decimal? PaymentTypeID
        {
            get
            {
        		return GetDataRowField<Decimal?>("PAYMENT_TYPE_ID");
                //return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
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
        		return GetDataRowField<Decimal?>("IS_NEGATIV_ALOWED");
                //return this.GetDataRowField<Decimal?>(() => IsNegativAlowed);
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
        		return GetDataRowField<Decimal?>("CONSIDER_TYPE_ID");
                //return this.GetDataRowField<Decimal?>(() => ConsiderTypeID);
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
        		return GetDataRowField<Decimal?>("TYPE_PAYMENT_TYPE_ID");
                //return this.GetDataRowField<Decimal?>(() => TypePaymentTypeID);
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
        		return GetDataRowField<Decimal?>("SIGN_FORM_REPORT");
                //return this.GetDataRowField<Decimal?>(() => SignFormReport);
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
        		return GetDataRowField<String>("NAME_PAYMENT");
                //return this.GetDataRowField<String>(() => NamePayment);
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
        		return GetDataRowField<Decimal?>("CALC_PRIORITY");
                //return this.GetDataRowField<Decimal?>(() => CalcPriority);
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
        		return GetDataRowField<Decimal?>("PAY_TYPE_ID");
                //return this.GetDataRowField<Decimal?>(() => PayTypeID);
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
        		return GetDataRowField<String>("CODE_PAYMENT");
                //return this.GetDataRowField<String>(() => CodePayment);
            }
            set
            {
                UpdateDataRow<String>(() => CodePayment, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.PAYMENT_TYPE_UPDATE(p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID, p_IS_NEGATIV_ALOWED=>:p_IS_NEGATIV_ALOWED, p_CONSIDER_TYPE_ID=>:p_CONSIDER_TYPE_ID, p_TYPE_PAYMENT_TYPE_ID=>:p_TYPE_PAYMENT_TYPE_ID, p_SIGN_FORM_REPORT=>:p_SIGN_FORM_REPORT, p_NAME_PAYMENT=>:p_NAME_PAYMENT, p_CALC_PRIORITY=>:p_CALC_PRIORITY, p_PAY_TYPE_ID=>:p_PAY_TYPE_ID, p_CODE_PAYMENT=>:p_CODE_PAYMENT);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_PAYMENT_TYPE_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_IS_NEGATIV_ALOWED", OracleDbType.Decimal, 0, "IS_NEGATIV_ALOWED");
            _dataAdapter.InsertCommand.Parameters.Add("p_CONSIDER_TYPE_ID", OracleDbType.Decimal, 0, "CONSIDER_TYPE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "TYPE_PAYMENT_TYPE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_FORM_REPORT", OracleDbType.Decimal, 0, "SIGN_FORM_REPORT");
            _dataAdapter.InsertCommand.Parameters.Add("p_NAME_PAYMENT", OracleDbType.Varchar2, 0, "NAME_PAYMENT");
            _dataAdapter.InsertCommand.Parameters.Add("p_CALC_PRIORITY", OracleDbType.Decimal, 0, "CALC_PRIORITY");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAY_TYPE_ID", OracleDbType.Decimal, 0, "PAY_TYPE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_PAYMENT", OracleDbType.Varchar2, 0, "CODE_PAYMENT");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.PAYMENT_TYPE_UPDATE(p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID, p_IS_NEGATIV_ALOWED=>:p_IS_NEGATIV_ALOWED, p_CONSIDER_TYPE_ID=>:p_CONSIDER_TYPE_ID, p_TYPE_PAYMENT_TYPE_ID=>:p_TYPE_PAYMENT_TYPE_ID, p_SIGN_FORM_REPORT=>:p_SIGN_FORM_REPORT, p_NAME_PAYMENT=>:p_NAME_PAYMENT, p_CALC_PRIORITY=>:p_CALC_PRIORITY, p_PAY_TYPE_ID=>:p_PAY_TYPE_ID, p_CODE_PAYMENT=>:p_CODE_PAYMENT);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_PAYMENT_TYPE_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_IS_NEGATIV_ALOWED", OracleDbType.Decimal, 0, "IS_NEGATIV_ALOWED");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CONSIDER_TYPE_ID", OracleDbType.Decimal, 0, "CONSIDER_TYPE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "TYPE_PAYMENT_TYPE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_FORM_REPORT", OracleDbType.Decimal, 0, "SIGN_FORM_REPORT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NAME_PAYMENT", OracleDbType.Varchar2, 0, "NAME_PAYMENT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CALC_PRIORITY", OracleDbType.Decimal, 0, "CALC_PRIORITY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAY_TYPE_ID", OracleDbType.Decimal, 0, "PAY_TYPE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_PAYMENT", OracleDbType.Varchar2, 0, "CODE_PAYMENT");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.PAYMENT_TYPE_DELETE(p_PAYMENT_TYPE_ID => :p_PAYMENT_TYPE_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Класс представляющий общие данные по нарядам
    /// </summary>

    [Table(Name="PIECE_WORK_DATA"), SchemaName("SALARY")]
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
        		return GetDataRowField<String>("ORDER_NAME");
                //return this.GetDataRowField<String>(() => OrderName);
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
        		return GetDataRowField<String>("PACKAGE_NUMBER");
                //return this.GetDataRowField<String>(() => PackageNumber);
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
        		return GetDataRowField<DateTime?>("COMPLETE_DATE");
                //return this.GetDataRowField<DateTime?>(() => CompleteDate);
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
        		return GetDataRowField<String>("DETAIL_CODE");
                //return this.GetDataRowField<String>(() => DetailCode);
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
        		return GetDataRowField<Decimal?>("DETAIL_COUNT");
                //return this.GetDataRowField<Decimal?>(() => DetailCount);
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
        		return GetDataRowField<Decimal?>("WORK_CLASSIFIC");
                //return this.GetDataRowField<Decimal?>(() => WorkClassific);
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
        		return GetDataRowField<Decimal?>("DETAIL_SUM");
                //return this.GetDataRowField<Decimal?>(() => DetailSum);
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
        		return GetDataRowField<Decimal?>("DETAIL_TIME");
                //return this.GetDataRowField<Decimal?>(() => DetailTime);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => DetailTime, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.PIECE_WORK_DATA_UPDATE(p_ORDER_NAME=>:p_ORDER_NAME, p_PACKAGE_NUMBER=>:p_PACKAGE_NUMBER, p_COMPLETE_DATE=>:p_COMPLETE_DATE, p_DETAIL_CODE=>:p_DETAIL_CODE, p_DETAIL_COUNT=>:p_DETAIL_COUNT, p_WORK_CLASSIFIC=>:p_WORK_CLASSIFIC, p_DETAIL_SUM=>:p_DETAIL_SUM, p_DETAIL_TIME=>:p_DETAIL_TIME);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_ORDER_NAME", OracleDbType.Varchar2, 0, "ORDER_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_PACKAGE_NUMBER", OracleDbType.Varchar2, 0, "PACKAGE_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_COMPLETE_DATE", OracleDbType.Date, 0, "COMPLETE_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_DETAIL_CODE", OracleDbType.Varchar2, 0, "DETAIL_CODE");
            _dataAdapter.InsertCommand.Parameters.Add("p_DETAIL_COUNT", OracleDbType.Decimal, 0, "DETAIL_COUNT");
            _dataAdapter.InsertCommand.Parameters.Add("p_WORK_CLASSIFIC", OracleDbType.Decimal, 0, "WORK_CLASSIFIC");
            _dataAdapter.InsertCommand.Parameters.Add("p_DETAIL_SUM", OracleDbType.Decimal, 0, "DETAIL_SUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_DETAIL_TIME", OracleDbType.Decimal, 0, "DETAIL_TIME");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.PIECE_WORK_DATA_UPDATE(p_ORDER_NAME=>:p_ORDER_NAME, p_PACKAGE_NUMBER=>:p_PACKAGE_NUMBER, p_COMPLETE_DATE=>:p_COMPLETE_DATE, p_DETAIL_CODE=>:p_DETAIL_CODE, p_DETAIL_COUNT=>:p_DETAIL_COUNT, p_WORK_CLASSIFIC=>:p_WORK_CLASSIFIC, p_DETAIL_SUM=>:p_DETAIL_SUM, p_DETAIL_TIME=>:p_DETAIL_TIME);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_ORDER_NAME", OracleDbType.Varchar2, 0, "ORDER_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PACKAGE_NUMBER", OracleDbType.Varchar2, 0, "PACKAGE_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COMPLETE_DATE", OracleDbType.Date, 0, "COMPLETE_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DETAIL_CODE", OracleDbType.Varchar2, 0, "DETAIL_CODE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DETAIL_COUNT", OracleDbType.Decimal, 0, "DETAIL_COUNT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_WORK_CLASSIFIC", OracleDbType.Decimal, 0, "WORK_CLASSIFIC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DETAIL_SUM", OracleDbType.Decimal, 0, "DETAIL_SUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DETAIL_TIME", OracleDbType.Decimal, 0, "DETAIL_TIME");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.PIECE_WORK_DATA_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        		}
		
	#endregion
    }


    [Table(Name="REPORT_GROUP"), SchemaName("SALARY")]
    public partial class ReportGroup : RowEntityBase
    {
        #region Class Members
        /// <summary>
        /// Уникальный номер кода группы
        /// </summary>
        [Column(Name="REPORT_GROUP_ID", IsPrimaryKey=true)]
        public Decimal? ReportGroupID
        {
            get
            {
        		return GetDataRowField<Decimal?>("REPORT_GROUP_ID");
                //return this.GetDataRowField<Decimal?>(() => ReportGroupID);
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
        		return GetDataRowField<String>("GROUP_COMMENT");
                //return this.GetDataRowField<String>(() => GroupComment);
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
        		return GetDataRowField<Decimal?>("PARENT_GROUP_ID");
                //return this.GetDataRowField<Decimal?>(() => ParentGroupID);
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
        		return GetDataRowField<Decimal?>("SORT_NUMBER");
                //return this.GetDataRowField<Decimal?>(() => SortNumber);
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
        		return GetDataRowField<DateTime?>("DATE_END_REPORT");
                //return this.GetDataRowField<DateTime?>(() => DateEndReport);
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
        		return GetDataRowField<DateTime?>("DATE_BEGIN_REPORT");
                //return this.GetDataRowField<DateTime?>(() => DateBeginReport);
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
        		return GetDataRowField<String>("SHORT_GROUP_NAME");
                //return this.GetDataRowField<String>(() => ShortGroupName);
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
        		return GetDataRowField<String>("GROUP_NAME");
                //return this.GetDataRowField<String>(() => GroupName);
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
        		return GetDataRowField<String>("GROUP_CODE");
                //return this.GetDataRowField<String>(() => GroupCode);
            }
            set
            {
                UpdateDataRow<String>(() => GroupCode, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.REPORT_GROUP_UPDATE(p_REPORT_GROUP_ID=>:p_REPORT_GROUP_ID, p_GROUP_COMMENT=>:p_GROUP_COMMENT, p_PARENT_GROUP_ID=>:p_PARENT_GROUP_ID, p_SORT_NUMBER=>:p_SORT_NUMBER, p_DATE_END_REPORT=>:p_DATE_END_REPORT, p_DATE_BEGIN_REPORT=>:p_DATE_BEGIN_REPORT, p_SHORT_GROUP_NAME=>:p_SHORT_GROUP_NAME, p_GROUP_NAME=>:p_GROUP_NAME, p_GROUP_CODE=>:p_GROUP_CODE);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, 0, "REPORT_GROUP_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_REPORT_GROUP_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_GROUP_COMMENT", OracleDbType.Varchar2, 0, "GROUP_COMMENT");
            _dataAdapter.InsertCommand.Parameters.Add("p_PARENT_GROUP_ID", OracleDbType.Decimal, 0, "PARENT_GROUP_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SORT_NUMBER", OracleDbType.Decimal, 0, "SORT_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_END_REPORT", OracleDbType.Date, 0, "DATE_END_REPORT");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_BEGIN_REPORT", OracleDbType.Date, 0, "DATE_BEGIN_REPORT");
            _dataAdapter.InsertCommand.Parameters.Add("p_SHORT_GROUP_NAME", OracleDbType.Varchar2, 0, "SHORT_GROUP_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_GROUP_NAME", OracleDbType.Varchar2, 0, "GROUP_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_GROUP_CODE", OracleDbType.Varchar2, 0, "GROUP_CODE");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.REPORT_GROUP_UPDATE(p_REPORT_GROUP_ID=>:p_REPORT_GROUP_ID, p_GROUP_COMMENT=>:p_GROUP_COMMENT, p_PARENT_GROUP_ID=>:p_PARENT_GROUP_ID, p_SORT_NUMBER=>:p_SORT_NUMBER, p_DATE_END_REPORT=>:p_DATE_END_REPORT, p_DATE_BEGIN_REPORT=>:p_DATE_BEGIN_REPORT, p_SHORT_GROUP_NAME=>:p_SHORT_GROUP_NAME, p_GROUP_NAME=>:p_GROUP_NAME, p_GROUP_CODE=>:p_GROUP_CODE);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, 0, "REPORT_GROUP_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_REPORT_GROUP_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_GROUP_COMMENT", OracleDbType.Varchar2, 0, "GROUP_COMMENT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PARENT_GROUP_ID", OracleDbType.Decimal, 0, "PARENT_GROUP_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SORT_NUMBER", OracleDbType.Decimal, 0, "SORT_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_END_REPORT", OracleDbType.Date, 0, "DATE_END_REPORT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_BEGIN_REPORT", OracleDbType.Date, 0, "DATE_BEGIN_REPORT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SHORT_GROUP_NAME", OracleDbType.Varchar2, 0, "SHORT_GROUP_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_GROUP_NAME", OracleDbType.Varchar2, 0, "GROUP_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_GROUP_CODE", OracleDbType.Varchar2, 0, "GROUP_CODE");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.REPORT_GROUP_DELETE(p_REPORT_GROUP_ID => :p_REPORT_GROUP_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_REPORT_GROUP_ID", OracleDbType.Decimal, 0, "REPORT_GROUP_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Таблица удержаний
    /// </summary>

    [Table(Name="RETENTION"), SchemaName("SALARY")]
    public partial class Retention : RowEntityBase
    {
        #region Class Members
        [Column(Name="RETENTION_ID", CanBeNull=false)]
        public Decimal? RetentionID
        {
            get
            {
        		return GetDataRowField<Decimal?>("RETENTION_ID");
                //return this.GetDataRowField<Decimal?>(() => RetentionID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentionID, value);
            }
        }
        /// <summary>
        /// Уникальный номер перевода
        /// </summary>
        [Column(Name="TRANSFER_ID", CanBeNull=false)]
        public Decimal? TransferID
        {
            get
            {
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TransferID, value);
            }
        }
        [Column(Name="PAYMENT_TYPE_ID")]
        public Decimal? PaymentTypeID
        {
            get
            {
        		return GetDataRowField<Decimal?>("PAYMENT_TYPE_ID");
                //return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentTypeID, value);
            }
        }
        [Column(Name="ORDER_NUMBER")]
        public Decimal? OrderNumber
        {
            get
            {
        		return GetDataRowField<Decimal?>("ORDER_NUMBER");
                //return this.GetDataRowField<Decimal?>(() => OrderNumber);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OrderNumber, value);
            }
        }
        /// <summary>
        /// Изначальная сумма удержания
        /// </summary>
        [Column(Name="ORIGINAL_SUM")]
        public Decimal? OriginalSum
        {
            get
            {
        		return GetDataRowField<Decimal?>("ORIGINAL_SUM");
                //return this.GetDataRowField<Decimal?>(() => OriginalSum);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OriginalSum, value);
            }
        }
        /// <summary>
        /// Процент вычислений
        /// </summary>
        [Column(Name="RETENT_PERCENT")]
        public Decimal? RetentPercent
        {
            get
            {
        		return GetDataRowField<Decimal?>("RETENT_PERCENT");
                //return this.GetDataRowField<Decimal?>(() => RetentPercent);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentPercent, value);
            }
        }
        /// <summary>
        /// Сумма ежемесячных вычислений
        /// </summary>
        [Column(Name="RETENT_SUM")]
        public Decimal? RetentSum
        {
            get
            {
        		return GetDataRowField<Decimal?>("RETENT_SUM");
                //return this.GetDataRowField<Decimal?>(() => RetentSum);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RetentSum, value);
            }
        }
        /// <summary>
        /// Остаток суммы вычислений
        /// </summary>
        [Column(Name="REMAIN_SUM")]
        public Decimal? RemainSum
        {
            get
            {
        		return GetDataRowField<Decimal?>("REMAIN_SUM");
                //return this.GetDataRowField<Decimal?>(() => RemainSum);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RemainSum, value);
            }
        }
        /// <summary>
        /// Дата добавления доп удержания;
        /// </summary>
        [Column(Name="DATE_ADD")]
        public DateTime? DateAdd
        {
            get
            {
        		return GetDataRowField<DateTime?>("DATE_ADD");
                //return this.GetDataRowField<DateTime?>(() => DateAdd);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateAdd, value);
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
        		return GetDataRowField<DateTime?>("DATE_START_RET");
                //return this.GetDataRowField<DateTime?>(() => DateStartRet);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateStartRet, value);
            }
        }
        /// <summary>
        /// Дата окончания удержания
        /// </summary>
        [Column(Name="DATE_END_RET")]
        public DateTime? DateEndRet
        {
            get
            {
        		return GetDataRowField<DateTime?>("DATE_END_RET");
                //return this.GetDataRowField<DateTime?>(() => DateEndRet);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateEndRet, value);
            }
        }
        /// <summary>
        /// Уникальный ключ документа основания ЗП
        /// </summary>
        [Column(Name="SALARY_DOC_ID")]
        public Decimal? SalaryDocID
        {
            get
            {
        		return GetDataRowField<Decimal?>("SALARY_DOC_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryDocID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SalaryDocID, value);
            }
        }
        /// <summary>
        /// Признак добавления удержания за почтовые переводы;
        /// </summary>
        [Column(Name="POST_TRANSFER_SIGN")]
        public Decimal? PostTransferSign
        {
            get
            {
        		return GetDataRowField<Decimal?>("POST_TRANSFER_SIGN");
                //return this.GetDataRowField<Decimal?>(() => PostTransferSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PostTransferSign, value);
            }
        }
        /// <summary>
        /// Дата расчета предварительного остатка
        /// </summary>
        [Column(Name="DATE_REM_CALC")]
        public DateTime? DateRemCalc
        {
            get
            {
        		return GetDataRowField<DateTime?>("DATE_REM_CALC");
                //return this.GetDataRowField<DateTime?>(() => DateRemCalc);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateRemCalc, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.RETENTION_UPDATE(p_RETENTION_ID=>:p_RETENTION_ID, p_TRANSFER_ID=>:p_TRANSFER_ID, p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID, p_ORDER_NUMBER=>:p_ORDER_NUMBER, p_ORIGINAL_SUM=>:p_ORIGINAL_SUM, p_RETENT_PERCENT=>:p_RETENT_PERCENT, p_RETENT_SUM=>:p_RETENT_SUM, p_REMAIN_SUM=>:p_REMAIN_SUM, p_DATE_ADD=>:p_DATE_ADD, p_DATE_START_RET=>:p_DATE_START_RET, p_DATE_END_RET=>:p_DATE_END_RET, p_SALARY_DOC_ID=>:p_SALARY_DOC_ID, p_POST_TRANSFER_SIGN=>:p_POST_TRANSFER_SIGN, p_DATE_REM_CALC=>:p_DATE_REM_CALC);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_RETENTION_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_ORDER_NUMBER", OracleDbType.Decimal, 0, "ORDER_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_ORIGINAL_SUM", OracleDbType.Decimal, 0, "ORIGINAL_SUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_RETENT_PERCENT", OracleDbType.Decimal, 0, "RETENT_PERCENT");
            _dataAdapter.InsertCommand.Parameters.Add("p_RETENT_SUM", OracleDbType.Decimal, 0, "RETENT_SUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_REMAIN_SUM", OracleDbType.Decimal, 0, "REMAIN_SUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_ADD", OracleDbType.Date, 0, "DATE_ADD");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_START_RET", OracleDbType.Date, 0, "DATE_START_RET");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_END_RET", OracleDbType.Date, 0, "DATE_END_RET");
            _dataAdapter.InsertCommand.Parameters.Add("p_SALARY_DOC_ID", OracleDbType.Decimal, 0, "SALARY_DOC_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_POST_TRANSFER_SIGN", OracleDbType.Decimal, 0, "POST_TRANSFER_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_REM_CALC", OracleDbType.Date, 0, "DATE_REM_CALC");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.RETENTION_UPDATE(p_RETENTION_ID=>:p_RETENTION_ID, p_TRANSFER_ID=>:p_TRANSFER_ID, p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID, p_ORDER_NUMBER=>:p_ORDER_NUMBER, p_ORIGINAL_SUM=>:p_ORIGINAL_SUM, p_RETENT_PERCENT=>:p_RETENT_PERCENT, p_RETENT_SUM=>:p_RETENT_SUM, p_REMAIN_SUM=>:p_REMAIN_SUM, p_DATE_ADD=>:p_DATE_ADD, p_DATE_START_RET=>:p_DATE_START_RET, p_DATE_END_RET=>:p_DATE_END_RET, p_SALARY_DOC_ID=>:p_SALARY_DOC_ID, p_POST_TRANSFER_SIGN=>:p_POST_TRANSFER_SIGN, p_DATE_REM_CALC=>:p_DATE_REM_CALC);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_RETENTION_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ORDER_NUMBER", OracleDbType.Decimal, 0, "ORDER_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ORIGINAL_SUM", OracleDbType.Decimal, 0, "ORIGINAL_SUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RETENT_PERCENT", OracleDbType.Decimal, 0, "RETENT_PERCENT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RETENT_SUM", OracleDbType.Decimal, 0, "RETENT_SUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REMAIN_SUM", OracleDbType.Decimal, 0, "REMAIN_SUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_ADD", OracleDbType.Date, 0, "DATE_ADD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_START_RET", OracleDbType.Date, 0, "DATE_START_RET");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_END_RET", OracleDbType.Date, 0, "DATE_END_RET");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_DOC_ID", OracleDbType.Decimal, 0, "SALARY_DOC_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_POST_TRANSFER_SIGN", OracleDbType.Decimal, 0, "POST_TRANSFER_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_REM_CALC", OracleDbType.Date, 0, "DATE_REM_CALC");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.RETENTION_DELETE(p_RETENTION_ID => :p_RETENTION_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Таблица принятых/переданых затрат
    /// </summary>

    [Table(Name="SAL_SUBDIV_RECEIVE"), SchemaName("SALARY")]
    public partial class SalSubdivReceive : RowEntityBase
    {
        #region Class Members
        [Column(Name="SAL_SUBDIV_RECEIVE_ID", IsPrimaryKey=true)]
        public Decimal? SalSubdivReceiveID
        {
            get
            {
        		return GetDataRowField<Decimal?>("SAL_SUBDIV_RECEIVE_ID");
                //return this.GetDataRowField<Decimal?>(() => SalSubdivReceiveID);
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
        		return GetDataRowField<DateTime?>("REC_DATE");
                //return this.GetDataRowField<DateTime?>(() => RecDate);
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
        		return GetDataRowField<Decimal?>("RECEIVE_SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => ReceiveSubdivID);
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
        		return GetDataRowField<Decimal?>("SUBDIV_SAL");
                //return this.GetDataRowField<Decimal?>(() => SubdivSal);
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
        		return GetDataRowField<Decimal?>("SUM_SAL");
                //return this.GetDataRowField<Decimal?>(() => SumSal);
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
        		return GetDataRowField<Decimal?>("HOURS");
                //return this.GetDataRowField<Decimal?>(() => Hours);
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
        		return GetDataRowField<Decimal?>("ORDER_ID");
                //return this.GetDataRowField<Decimal?>(() => OrderID);
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
        		return GetDataRowField<Decimal?>("SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.SAL_SUBDIV_RECEIVE_UPDATE(p_SAL_SUBDIV_RECEIVE_ID=>:p_SAL_SUBDIV_RECEIVE_ID, p_REC_DATE=>:p_REC_DATE, p_RECEIVE_SUBDIV_ID=>:p_RECEIVE_SUBDIV_ID, p_SUBDIV_SAL=>:p_SUBDIV_SAL, p_SUM_SAL=>:p_SUM_SAL, p_HOURS=>:p_HOURS, p_ORDER_ID=>:p_ORDER_ID, p_SUBDIV_ID=>:p_SUBDIV_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_SAL_SUBDIV_RECEIVE_ID", OracleDbType.Decimal, 0, "SAL_SUBDIV_RECEIVE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_SAL_SUBDIV_RECEIVE_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_REC_DATE", OracleDbType.Date, 0, "REC_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_RECEIVE_SUBDIV_ID", OracleDbType.Decimal, 0, "RECEIVE_SUBDIV_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_SAL", OracleDbType.Decimal, 0, "SUBDIV_SAL");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            _dataAdapter.InsertCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS");
            _dataAdapter.InsertCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.SAL_SUBDIV_RECEIVE_UPDATE(p_SAL_SUBDIV_RECEIVE_ID=>:p_SAL_SUBDIV_RECEIVE_ID, p_REC_DATE=>:p_REC_DATE, p_RECEIVE_SUBDIV_ID=>:p_RECEIVE_SUBDIV_ID, p_SUBDIV_SAL=>:p_SUBDIV_SAL, p_SUM_SAL=>:p_SUM_SAL, p_HOURS=>:p_HOURS, p_ORDER_ID=>:p_ORDER_ID, p_SUBDIV_ID=>:p_SUBDIV_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_SAL_SUBDIV_RECEIVE_ID", OracleDbType.Decimal, 0, "SAL_SUBDIV_RECEIVE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_SAL_SUBDIV_RECEIVE_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_REC_DATE", OracleDbType.Date, 0, "REC_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RECEIVE_SUBDIV_ID", OracleDbType.Decimal, 0, "RECEIVE_SUBDIV_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_SAL", OracleDbType.Decimal, 0, "SUBDIV_SAL");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            _dataAdapter.UpdateCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.SAL_SUBDIV_RECEIVE_DELETE(p_SAL_SUBDIV_RECEIVE_ID => :p_SAL_SUBDIV_RECEIVE_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_SAL_SUBDIV_RECEIVE_ID", OracleDbType.Decimal, 0, "SAL_SUBDIV_RECEIVE_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Основная таблица - зарплата сотрудников
    /// </summary>

    [Table(Name="SALARY"), SchemaName("SALARY")]
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
        		return GetDataRowField<Decimal?>("SALARY_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryID);
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
        		return GetDataRowField<Decimal?>("TYPE_ROW_SALARY_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeRowSalaryID);
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
        		return GetDataRowField<Decimal?>("RETENTION_ID");
                //return this.GetDataRowField<Decimal?>(() => RetentionID);
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
        		return GetDataRowField<Decimal?>("SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => SubdivID);
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
        		return GetDataRowField<Decimal?>("SIGN_COMB");
                //return this.GetDataRowField<Decimal?>(() => SignComb);
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
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
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
        		return GetDataRowField<DateTime?>("TIME_ADD_RECORD");
                //return this.GetDataRowField<DateTime?>(() => TimeAddRecord);
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
        		return GetDataRowField<Decimal?>("ACCOUNT_ADD_SIGN");
                //return this.GetDataRowField<Decimal?>(() => AccountAddSign);
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
        		return GetDataRowField<Decimal?>("DAYS");
                //return this.GetDataRowField<Decimal?>(() => Days);
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
        		return GetDataRowField<Decimal?>("REF_ID");
                //return this.GetDataRowField<Decimal?>(() => RefID);
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
        		return GetDataRowField<Decimal?>("TYPE_REF_SALARY_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeRefSalaryID);
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
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
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
        		return GetDataRowField<Decimal?>("DEGREE_ID");
                //return this.GetDataRowField<Decimal?>(() => DegreeID);
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
        		return GetDataRowField<String>("GROUP_MASTER");
                //return this.GetDataRowField<String>(() => GroupMaster);
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
        		return GetDataRowField<Decimal?>("ORDER_ID");
                //return this.GetDataRowField<Decimal?>(() => OrderID);
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
        		return GetDataRowField<Decimal?>("EXP_ADD");
                //return this.GetDataRowField<Decimal?>(() => ExpAdd);
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
        		return GetDataRowField<Decimal?>("ZONE_ADD");
                //return this.GetDataRowField<Decimal?>(() => ZoneAdd);
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
        		return GetDataRowField<Decimal?>("SUM_SAL");
                //return this.GetDataRowField<Decimal?>(() => SumSal);
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
        		return GetDataRowField<Decimal?>("HOURS");
                //return this.GetDataRowField<Decimal?>(() => Hours);
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
        		return GetDataRowField<Decimal?>("PAYMENT_TYPE_ID");
                //return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
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
        		return GetDataRowField<DateTime?>("PAY_DATE");
                //return this.GetDataRowField<DateTime?>(() => PayDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => PayDate, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.SALARY_UPDATE(p_SALARY_ID=>:p_SALARY_ID, p_TYPE_ROW_SALARY_ID=>:p_TYPE_ROW_SALARY_ID, p_RETENTION_ID=>:p_RETENTION_ID, p_SUBDIV_ID=>:p_SUBDIV_ID, p_SIGN_COMB=>:p_SIGN_COMB, p_PER_NUM=>:p_PER_NUM, p_TIME_ADD_RECORD=>:p_TIME_ADD_RECORD, p_ACCOUNT_ADD_SIGN=>:p_ACCOUNT_ADD_SIGN, p_DAYS=>:p_DAYS, p_REF_ID=>:p_REF_ID, p_TYPE_REF_SALARY_ID=>:p_TYPE_REF_SALARY_ID, p_TRANSFER_ID=>:p_TRANSFER_ID, p_DEGREE_ID=>:p_DEGREE_ID, p_GROUP_MASTER=>:p_GROUP_MASTER, p_ORDER_ID=>:p_ORDER_ID, p_EXP_ADD=>:p_EXP_ADD, p_ZONE_ADD=>:p_ZONE_ADD, p_SUM_SAL=>:p_SUM_SAL, p_HOURS=>:p_HOURS, p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID, p_PAY_DATE=>:p_PAY_DATE);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, 0, "SALARY_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_SALARY_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_ROW_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_ROW_SALARY_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            _dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_TIME_ADD_RECORD", OracleDbType.Date, 0, "TIME_ADD_RECORD");
            _dataAdapter.InsertCommand.Parameters.Add("p_ACCOUNT_ADD_SIGN", OracleDbType.Decimal, 0, "ACCOUNT_ADD_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_DAYS", OracleDbType.Decimal, 0, "DAYS");
            _dataAdapter.InsertCommand.Parameters.Add("p_REF_ID", OracleDbType.Decimal, 0, "REF_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_REF_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_REF_SALARY_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_GROUP_MASTER", OracleDbType.Varchar2, 0, "GROUP_MASTER");
            _dataAdapter.InsertCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_EXP_ADD", OracleDbType.Decimal, 0, "EXP_ADD");
            _dataAdapter.InsertCommand.Parameters.Add("p_ZONE_ADD", OracleDbType.Decimal, 0, "ZONE_ADD");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            _dataAdapter.InsertCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.SALARY_UPDATE(p_SALARY_ID=>:p_SALARY_ID, p_TYPE_ROW_SALARY_ID=>:p_TYPE_ROW_SALARY_ID, p_RETENTION_ID=>:p_RETENTION_ID, p_SUBDIV_ID=>:p_SUBDIV_ID, p_SIGN_COMB=>:p_SIGN_COMB, p_PER_NUM=>:p_PER_NUM, p_TIME_ADD_RECORD=>:p_TIME_ADD_RECORD, p_ACCOUNT_ADD_SIGN=>:p_ACCOUNT_ADD_SIGN, p_DAYS=>:p_DAYS, p_REF_ID=>:p_REF_ID, p_TYPE_REF_SALARY_ID=>:p_TYPE_REF_SALARY_ID, p_TRANSFER_ID=>:p_TRANSFER_ID, p_DEGREE_ID=>:p_DEGREE_ID, p_GROUP_MASTER=>:p_GROUP_MASTER, p_ORDER_ID=>:p_ORDER_ID, p_EXP_ADD=>:p_EXP_ADD, p_ZONE_ADD=>:p_ZONE_ADD, p_SUM_SAL=>:p_SUM_SAL, p_HOURS=>:p_HOURS, p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID, p_PAY_DATE=>:p_PAY_DATE);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, 0, "SALARY_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_SALARY_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_ROW_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_ROW_SALARY_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TIME_ADD_RECORD", OracleDbType.Date, 0, "TIME_ADD_RECORD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ACCOUNT_ADD_SIGN", OracleDbType.Decimal, 0, "ACCOUNT_ADD_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DAYS", OracleDbType.Decimal, 0, "DAYS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REF_ID", OracleDbType.Decimal, 0, "REF_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_REF_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_REF_SALARY_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_GROUP_MASTER", OracleDbType.Varchar2, 0, "GROUP_MASTER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EXP_ADD", OracleDbType.Decimal, 0, "EXP_ADD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ZONE_ADD", OracleDbType.Decimal, 0, "ZONE_ADD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            _dataAdapter.UpdateCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DELETE(p_SALARY_ID => :p_SALARY_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, 0, "SALARY_ID");
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
        		return GetDataRowField<Decimal?>("SALARY_ADD_CORRELATION_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryAddCorrelationID);
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
        		return GetDataRowField<DateTime?>("CALC_DATE");
                //return this.GetDataRowField<DateTime?>(() => CalcDate);
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
        		return GetDataRowField<Decimal?>("DEGREE_ID");
                //return this.GetDataRowField<Decimal?>(() => DegreeID);
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
        		return GetDataRowField<Decimal?>("SUM_SAL");
                //return this.GetDataRowField<Decimal?>(() => SumSal);
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
        		return GetDataRowField<Decimal?>("HOURS");
                //return this.GetDataRowField<Decimal?>(() => Hours);
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
        		return GetDataRowField<Decimal?>("ORDER_ID");
                //return this.GetDataRowField<Decimal?>(() => OrderID);
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
        		return GetDataRowField<Decimal?>("PAYMENT_TYPE_ID");
                //return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
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
        		return GetDataRowField<Decimal?>("TYPE_OPERATION_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeOperationID);
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
        		return GetDataRowField<Decimal?>("SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => SubdivID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SubdivID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.SALARY_ADD_CORRELATION_UPDATE(p_SALARY_ADD_CORRELATION_ID=>:p_SALARY_ADD_CORRELATION_ID, p_CALC_DATE=>:p_CALC_DATE, p_DEGREE_ID=>:p_DEGREE_ID, p_SUM_SAL=>:p_SUM_SAL, p_HOURS=>:p_HOURS, p_ORDER_ID=>:p_ORDER_ID, p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID, p_TYPE_OPERATION_ID=>:p_TYPE_OPERATION_ID, p_SUBDIV_ID=>:p_SUBDIV_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_SALARY_ADD_CORRELATION_ID", OracleDbType.Decimal, 0, "SALARY_ADD_CORRELATION_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_SALARY_ADD_CORRELATION_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_CALC_DATE", OracleDbType.Date, 0, "CALC_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            _dataAdapter.InsertCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS");
            _dataAdapter.InsertCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_OPERATION_ID", OracleDbType.Decimal, 0, "TYPE_OPERATION_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.SALARY_ADD_CORRELATION_UPDATE(p_SALARY_ADD_CORRELATION_ID=>:p_SALARY_ADD_CORRELATION_ID, p_CALC_DATE=>:p_CALC_DATE, p_DEGREE_ID=>:p_DEGREE_ID, p_SUM_SAL=>:p_SUM_SAL, p_HOURS=>:p_HOURS, p_ORDER_ID=>:p_ORDER_ID, p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID, p_TYPE_OPERATION_ID=>:p_TYPE_OPERATION_ID, p_SUBDIV_ID=>:p_SUBDIV_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_ADD_CORRELATION_ID", OracleDbType.Decimal, 0, "SALARY_ADD_CORRELATION_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_SALARY_ADD_CORRELATION_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_CALC_DATE", OracleDbType.Date, 0, "CALC_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            _dataAdapter.UpdateCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_OPERATION_ID", OracleDbType.Decimal, 0, "TYPE_OPERATION_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.SALARY_ADD_CORRELATION_DELETE(p_SALARY_ADD_CORRELATION_ID => :p_SALARY_ADD_CORRELATION_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_SALARY_ADD_CORRELATION_ID", OracleDbType.Decimal, 0, "SALARY_ADD_CORRELATION_ID");
	}
		
	#endregion
    }


    [Table(Name="SALARY_ADVANCE"), SchemaName("SALARY")]
    public partial class SalaryAdvance : RowEntityBase
    {
        #region Class Members
        [Column(Name="SALARY_ADVANCE_ID")]
        public Decimal? SalaryAdvanceID
        {
            get
            {
        		return GetDataRowField<Decimal?>("SALARY_ADVANCE_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryAdvanceID);
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
        		return GetDataRowField<Decimal?>("TYPE_ROW_SALARY_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeRowSalaryID);
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
        		return GetDataRowField<Decimal?>("RETENTION_ID");
                //return this.GetDataRowField<Decimal?>(() => RetentionID);
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
        		return GetDataRowField<Decimal?>("SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => SubdivID);
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
        		return GetDataRowField<Decimal?>("SIGN_COMB");
                //return this.GetDataRowField<Decimal?>(() => SignComb);
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
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
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
        		return GetDataRowField<DateTime?>("TIME_ADD_RECORD");
                //return this.GetDataRowField<DateTime?>(() => TimeAddRecord);
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
        		return GetDataRowField<Decimal?>("ACCOUNT_ADD_SIGN");
                //return this.GetDataRowField<Decimal?>(() => AccountAddSign);
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
        		return GetDataRowField<Decimal?>("DAYS");
                //return this.GetDataRowField<Decimal?>(() => Days);
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
        		return GetDataRowField<Decimal?>("REF_ID");
                //return this.GetDataRowField<Decimal?>(() => RefID);
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
        		return GetDataRowField<Decimal?>("TYPE_REF_SALARY_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeRefSalaryID);
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
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
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
        		return GetDataRowField<Decimal?>("DEGREE_ID");
                //return this.GetDataRowField<Decimal?>(() => DegreeID);
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
        		return GetDataRowField<String>("GROUP_MASTER");
                //return this.GetDataRowField<String>(() => GroupMaster);
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
        		return GetDataRowField<Decimal?>("ORDER_ID");
                //return this.GetDataRowField<Decimal?>(() => OrderID);
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
        		return GetDataRowField<Decimal?>("EXP_ADD");
                //return this.GetDataRowField<Decimal?>(() => ExpAdd);
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
        		return GetDataRowField<Decimal?>("ZONE_ADD");
                //return this.GetDataRowField<Decimal?>(() => ZoneAdd);
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
        		return GetDataRowField<Decimal?>("SUM_SAL");
                //return this.GetDataRowField<Decimal?>(() => SumSal);
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
        		return GetDataRowField<Decimal?>("HOURS");
                //return this.GetDataRowField<Decimal?>(() => Hours);
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
        		return GetDataRowField<Decimal?>("PAYMENT_TYPE_ID");
                //return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
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
        		return GetDataRowField<DateTime?>("PAY_DATE");
                //return this.GetDataRowField<DateTime?>(() => PayDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => PayDate, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.SALARY_ADVANCE_UPDATE(p_SALARY_ADVANCE_ID=>:p_SALARY_ADVANCE_ID, p_TYPE_ROW_SALARY_ID=>:p_TYPE_ROW_SALARY_ID, p_RETENTION_ID=>:p_RETENTION_ID, p_SUBDIV_ID=>:p_SUBDIV_ID, p_SIGN_COMB=>:p_SIGN_COMB, p_PER_NUM=>:p_PER_NUM, p_TIME_ADD_RECORD=>:p_TIME_ADD_RECORD, p_ACCOUNT_ADD_SIGN=>:p_ACCOUNT_ADD_SIGN, p_DAYS=>:p_DAYS, p_REF_ID=>:p_REF_ID, p_TYPE_REF_SALARY_ID=>:p_TYPE_REF_SALARY_ID, p_TRANSFER_ID=>:p_TRANSFER_ID, p_DEGREE_ID=>:p_DEGREE_ID, p_GROUP_MASTER=>:p_GROUP_MASTER, p_ORDER_ID=>:p_ORDER_ID, p_EXP_ADD=>:p_EXP_ADD, p_ZONE_ADD=>:p_ZONE_ADD, p_SUM_SAL=>:p_SUM_SAL, p_HOURS=>:p_HOURS, p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID, p_PAY_DATE=>:p_PAY_DATE);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_SALARY_ADVANCE_ID", OracleDbType.Decimal, 0, "SALARY_ADVANCE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_SALARY_ADVANCE_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_ROW_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_ROW_SALARY_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            _dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_TIME_ADD_RECORD", OracleDbType.Date, 0, "TIME_ADD_RECORD");
            _dataAdapter.InsertCommand.Parameters.Add("p_ACCOUNT_ADD_SIGN", OracleDbType.Decimal, 0, "ACCOUNT_ADD_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_DAYS", OracleDbType.Decimal, 0, "DAYS");
            _dataAdapter.InsertCommand.Parameters.Add("p_REF_ID", OracleDbType.Decimal, 0, "REF_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_REF_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_REF_SALARY_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_GROUP_MASTER", OracleDbType.Varchar2, 0, "GROUP_MASTER");
            _dataAdapter.InsertCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_EXP_ADD", OracleDbType.Decimal, 0, "EXP_ADD");
            _dataAdapter.InsertCommand.Parameters.Add("p_ZONE_ADD", OracleDbType.Decimal, 0, "ZONE_ADD");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            _dataAdapter.InsertCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.SALARY_ADVANCE_UPDATE(p_SALARY_ADVANCE_ID=>:p_SALARY_ADVANCE_ID, p_TYPE_ROW_SALARY_ID=>:p_TYPE_ROW_SALARY_ID, p_RETENTION_ID=>:p_RETENTION_ID, p_SUBDIV_ID=>:p_SUBDIV_ID, p_SIGN_COMB=>:p_SIGN_COMB, p_PER_NUM=>:p_PER_NUM, p_TIME_ADD_RECORD=>:p_TIME_ADD_RECORD, p_ACCOUNT_ADD_SIGN=>:p_ACCOUNT_ADD_SIGN, p_DAYS=>:p_DAYS, p_REF_ID=>:p_REF_ID, p_TYPE_REF_SALARY_ID=>:p_TYPE_REF_SALARY_ID, p_TRANSFER_ID=>:p_TRANSFER_ID, p_DEGREE_ID=>:p_DEGREE_ID, p_GROUP_MASTER=>:p_GROUP_MASTER, p_ORDER_ID=>:p_ORDER_ID, p_EXP_ADD=>:p_EXP_ADD, p_ZONE_ADD=>:p_ZONE_ADD, p_SUM_SAL=>:p_SUM_SAL, p_HOURS=>:p_HOURS, p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID, p_PAY_DATE=>:p_PAY_DATE);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_ADVANCE_ID", OracleDbType.Decimal, 0, "SALARY_ADVANCE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_SALARY_ADVANCE_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_ROW_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_ROW_SALARY_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TIME_ADD_RECORD", OracleDbType.Date, 0, "TIME_ADD_RECORD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ACCOUNT_ADD_SIGN", OracleDbType.Decimal, 0, "ACCOUNT_ADD_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DAYS", OracleDbType.Decimal, 0, "DAYS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REF_ID", OracleDbType.Decimal, 0, "REF_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_REF_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_REF_SALARY_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_GROUP_MASTER", OracleDbType.Varchar2, 0, "GROUP_MASTER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EXP_ADD", OracleDbType.Decimal, 0, "EXP_ADD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ZONE_ADD", OracleDbType.Decimal, 0, "ZONE_ADD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            _dataAdapter.UpdateCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.SALARY_ADVANCE_DELETE(p_SALARY_ADVANCE_ID => :p_SALARY_ADVANCE_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_SALARY_ADVANCE_ID", OracleDbType.Decimal, 0, "SALARY_ADVANCE_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Документы начисления
    /// </summary>

    [Table(Name="SALARY_DOCUM"), SchemaName("SALARY")]
    public partial class SalaryDocum : RowEntityBase
    {
        #region Class Members
        [Column(Name="SALARY_DOCUM_ID", IsPrimaryKey=true)]
        public Decimal? SalaryDocumID
        {
            get
            {
        		return GetDataRowField<Decimal?>("SALARY_DOCUM_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryDocumID);
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
        		return GetDataRowField<DateTime?>("LAST_CALC_DATE");
                //return this.GetDataRowField<DateTime?>(() => LastCalcDate);
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
        		return GetDataRowField<Decimal?>("PAYMENT_PERCENT");
                //return this.GetDataRowField<Decimal?>(() => PaymentPercent);
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
        		return GetDataRowField<DateTime?>("DOC_BEGIN");
                //return this.GetDataRowField<DateTime?>(() => DocBegin);
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
        		return GetDataRowField<DateTime?>("DOC_END");
                //return this.GetDataRowField<DateTime?>(() => DocEnd);
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
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
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
        		return GetDataRowField<DateTime?>("DATE_FORM_DOCUM");
                //return this.GetDataRowField<DateTime?>(() => DateFormDocum);
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
        		return GetDataRowField<Decimal?>("DOC_SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => DocSubdivID);
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
        		return GetDataRowField<Decimal?>("TYPE_SAL_DOCUM_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeSalDocumID);
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
        		return GetDataRowField<DateTime?>("DATE_CLOSE");
                //return this.GetDataRowField<DateTime?>(() => DateClose);
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
        		return GetDataRowField<DateTime?>("DATE_DOC");
                //return this.GetDataRowField<DateTime?>(() => DateDoc);
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
        		return GetDataRowField<String>("NAME_DOC");
                //return this.GetDataRowField<String>(() => NameDoc);
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
        		return GetDataRowField<String>("CODE_DOC");
                //return this.GetDataRowField<String>(() => CodeDoc);
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
        		return GetDataRowField<Decimal?>("COUNT_RESTRICT_DAYS");
                //return this.GetDataRowField<Decimal?>(() => CountRestrictDays);
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
        		return GetDataRowField<Decimal?>("REG_DOC_ID");
                //return this.GetDataRowField<Decimal?>(() => RegDocID);
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
        		return GetDataRowField<Decimal?>("BASIC_DOC_SIGN");
                //return this.GetDataRowField<Decimal?>(() => BasicDocSign);
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
        		return GetDataRowField<Decimal?>("RELATED_DOCUM_ID");
                //return this.GetDataRowField<Decimal?>(() => RelatedDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => RelatedDocumID, value);
            }
        }
        /// <summary>
        /// Ссылка на заказ
        /// </summary>
        [Column(Name="ORDER_ID")]
        public Decimal? OrderID
        {
            get
            {
        		return GetDataRowField<Decimal?>("ORDER_ID");
                //return this.GetDataRowField<Decimal?>(() => OrderID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => OrderID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DOCUM_UPDATE(p_SALARY_DOCUM_ID=>:p_SALARY_DOCUM_ID, p_LAST_CALC_DATE=>:p_LAST_CALC_DATE, p_PAYMENT_PERCENT=>:p_PAYMENT_PERCENT, p_DOC_BEGIN=>:p_DOC_BEGIN, p_DOC_END=>:p_DOC_END, p_TRANSFER_ID=>:p_TRANSFER_ID, p_DATE_FORM_DOCUM=>:p_DATE_FORM_DOCUM, p_DOC_SUBDIV_ID=>:p_DOC_SUBDIV_ID, p_TYPE_SAL_DOCUM_ID=>:p_TYPE_SAL_DOCUM_ID, p_DATE_CLOSE=>:p_DATE_CLOSE, p_DATE_DOC=>:p_DATE_DOC, p_NAME_DOC=>:p_NAME_DOC, p_CODE_DOC=>:p_CODE_DOC, p_COUNT_RESTRICT_DAYS=>:p_COUNT_RESTRICT_DAYS, p_REG_DOC_ID=>:p_REG_DOC_ID, p_BASIC_DOC_SIGN=>:p_BASIC_DOC_SIGN, p_RELATED_DOCUM_ID=>:p_RELATED_DOCUM_ID, p_ORDER_ID=>:p_ORDER_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_SALARY_DOCUM_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_LAST_CALC_DATE", OracleDbType.Date, 0, "LAST_CALC_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAYMENT_PERCENT", OracleDbType.Decimal, 0, "PAYMENT_PERCENT");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_BEGIN", OracleDbType.Date, 0, "DOC_BEGIN");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_END", OracleDbType.Date, 0, "DOC_END");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_FORM_DOCUM", OracleDbType.Date, 0, "DATE_FORM_DOCUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOC_SUBDIV_ID", OracleDbType.Decimal, 0, "DOC_SUBDIV_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_SAL_DOCUM_ID", OracleDbType.Decimal, 0, "TYPE_SAL_DOCUM_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_CLOSE", OracleDbType.Date, 0, "DATE_CLOSE");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_DOC", OracleDbType.Date, 0, "DATE_DOC");
            _dataAdapter.InsertCommand.Parameters.Add("p_NAME_DOC", OracleDbType.Varchar2, 0, "NAME_DOC");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_DOC", OracleDbType.Varchar2, 0, "CODE_DOC");
            _dataAdapter.InsertCommand.Parameters.Add("p_COUNT_RESTRICT_DAYS", OracleDbType.Decimal, 0, "COUNT_RESTRICT_DAYS");
            _dataAdapter.InsertCommand.Parameters.Add("p_REG_DOC_ID", OracleDbType.Decimal, 0, "REG_DOC_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_BASIC_DOC_SIGN", OracleDbType.Decimal, 0, "BASIC_DOC_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_RELATED_DOCUM_ID", OracleDbType.Decimal, 0, "RELATED_DOCUM_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DOCUM_UPDATE(p_SALARY_DOCUM_ID=>:p_SALARY_DOCUM_ID, p_LAST_CALC_DATE=>:p_LAST_CALC_DATE, p_PAYMENT_PERCENT=>:p_PAYMENT_PERCENT, p_DOC_BEGIN=>:p_DOC_BEGIN, p_DOC_END=>:p_DOC_END, p_TRANSFER_ID=>:p_TRANSFER_ID, p_DATE_FORM_DOCUM=>:p_DATE_FORM_DOCUM, p_DOC_SUBDIV_ID=>:p_DOC_SUBDIV_ID, p_TYPE_SAL_DOCUM_ID=>:p_TYPE_SAL_DOCUM_ID, p_DATE_CLOSE=>:p_DATE_CLOSE, p_DATE_DOC=>:p_DATE_DOC, p_NAME_DOC=>:p_NAME_DOC, p_CODE_DOC=>:p_CODE_DOC, p_COUNT_RESTRICT_DAYS=>:p_COUNT_RESTRICT_DAYS, p_REG_DOC_ID=>:p_REG_DOC_ID, p_BASIC_DOC_SIGN=>:p_BASIC_DOC_SIGN, p_RELATED_DOCUM_ID=>:p_RELATED_DOCUM_ID, p_ORDER_ID=>:p_ORDER_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_SALARY_DOCUM_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_LAST_CALC_DATE", OracleDbType.Date, 0, "LAST_CALC_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAYMENT_PERCENT", OracleDbType.Decimal, 0, "PAYMENT_PERCENT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_BEGIN", OracleDbType.Date, 0, "DOC_BEGIN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_END", OracleDbType.Date, 0, "DOC_END");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_FORM_DOCUM", OracleDbType.Date, 0, "DATE_FORM_DOCUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOC_SUBDIV_ID", OracleDbType.Decimal, 0, "DOC_SUBDIV_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_SAL_DOCUM_ID", OracleDbType.Decimal, 0, "TYPE_SAL_DOCUM_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_CLOSE", OracleDbType.Date, 0, "DATE_CLOSE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_DOC", OracleDbType.Date, 0, "DATE_DOC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NAME_DOC", OracleDbType.Varchar2, 0, "NAME_DOC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_DOC", OracleDbType.Varchar2, 0, "CODE_DOC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COUNT_RESTRICT_DAYS", OracleDbType.Decimal, 0, "COUNT_RESTRICT_DAYS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REG_DOC_ID", OracleDbType.Decimal, 0, "REG_DOC_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BASIC_DOC_SIGN", OracleDbType.Decimal, 0, "BASIC_DOC_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RELATED_DOCUM_ID", OracleDbType.Decimal, 0, "RELATED_DOCUM_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DOCUM_DELETE(p_SALARY_DOCUM_ID => :p_SALARY_DOCUM_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Детали оплаты документа зарплаты
    /// </summary>

    [Table(Name="SALARY_DOCUM_DETAIL"), SchemaName("SALARY")]
    public partial class SalaryDocumDetail : RowEntityBase
    {
        #region Class Members
        [Column(Name="SALARY_DOCUM_DETAIL_ID")]
        public Decimal? SalaryDocumDetailID
        {
            get
            {
        		return GetDataRowField<Decimal?>("SALARY_DOCUM_DETAIL_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryDocumDetailID);
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
        		return GetDataRowField<Decimal?>("SALARY_DOCUM_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryDocumID);
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
        		return GetDataRowField<Decimal?>("PAYMENT_SUM");
                //return this.GetDataRowField<Decimal?>(() => PaymentSum);
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
        		return GetDataRowField<Decimal?>("PAYMENT_TYPE_ID");
                //return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => PaymentTypeID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DOCUM_DETAIL_UPDATE(p_SALARY_DOCUM_DETAIL_ID=>:p_SALARY_DOCUM_DETAIL_ID, p_SALARY_DOCUM_ID=>:p_SALARY_DOCUM_ID, p_PAYMENT_SUM=>:p_PAYMENT_SUM, p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_SALARY_DOCUM_DETAIL_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_DETAIL_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_SALARY_DOCUM_DETAIL_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAYMENT_SUM", OracleDbType.Decimal, 0, "PAYMENT_SUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DOCUM_DETAIL_UPDATE(p_SALARY_DOCUM_DETAIL_ID=>:p_SALARY_DOCUM_DETAIL_ID, p_SALARY_DOCUM_ID=>:p_SALARY_DOCUM_ID, p_PAYMENT_SUM=>:p_PAYMENT_SUM, p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_DETAIL_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_DETAIL_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_SALARY_DOCUM_DETAIL_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAYMENT_SUM", OracleDbType.Decimal, 0, "PAYMENT_SUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DOCUM_DETAIL_DELETE(p_SALARY_DOCUM_DETAIL_ID => :p_SALARY_DOCUM_DETAIL_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_SALARY_DOCUM_DETAIL_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_DETAIL_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Таблица изменений процентов расчета
    /// </summary>

    [Table(Name="SALARY_DOCUM_PAY_CHANGE"), SchemaName("SALARY")]
    public partial class SalaryDocumPayChange : RowEntityBase
    {
        #region Class Members
        [Column(Name="SALARY_DOCUM_PAY_CHANGE_ID")]
        public Decimal? SalaryDocumPayChangeID
        {
            get
            {
        		return GetDataRowField<Decimal?>("SALARY_DOCUM_PAY_CHANGE_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryDocumPayChangeID);
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
        		return GetDataRowField<Decimal?>("SALARY_DOCUM_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryDocumID);
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
        		return GetDataRowField<Decimal?>("BY_CODE_DOC_SIGN");
                //return this.GetDataRowField<Decimal?>(() => ByCodeDocSign);
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
        		return GetDataRowField<Decimal?>("PAY_VALUE");
                //return this.GetDataRowField<Decimal?>(() => PayValue);
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
        		return GetDataRowField<Decimal?>("COUNT_DAYS");
                //return this.GetDataRowField<Decimal?>(() => CountDays);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CountDays, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DOCUM_PAY_CHANGE_UPDATE(p_SALARY_DOCUM_PAY_CHANGE_ID=>:p_SALARY_DOCUM_PAY_CHANGE_ID, p_SALARY_DOCUM_ID=>:p_SALARY_DOCUM_ID, p_BY_CODE_DOC_SIGN=>:p_BY_CODE_DOC_SIGN, p_PAY_VALUE=>:p_PAY_VALUE, p_COUNT_DAYS=>:p_COUNT_DAYS);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_SALARY_DOCUM_PAY_CHANGE_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_PAY_CHANGE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_SALARY_DOCUM_PAY_CHANGE_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_BY_CODE_DOC_SIGN", OracleDbType.Decimal, 0, "BY_CODE_DOC_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAY_VALUE", OracleDbType.Decimal, 0, "PAY_VALUE");
            _dataAdapter.InsertCommand.Parameters.Add("p_COUNT_DAYS", OracleDbType.Decimal, 0, "COUNT_DAYS");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DOCUM_PAY_CHANGE_UPDATE(p_SALARY_DOCUM_PAY_CHANGE_ID=>:p_SALARY_DOCUM_PAY_CHANGE_ID, p_SALARY_DOCUM_ID=>:p_SALARY_DOCUM_ID, p_BY_CODE_DOC_SIGN=>:p_BY_CODE_DOC_SIGN, p_PAY_VALUE=>:p_PAY_VALUE, p_COUNT_DAYS=>:p_COUNT_DAYS);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_PAY_CHANGE_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_PAY_CHANGE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_SALARY_DOCUM_PAY_CHANGE_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BY_CODE_DOC_SIGN", OracleDbType.Decimal, 0, "BY_CODE_DOC_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAY_VALUE", OracleDbType.Decimal, 0, "PAY_VALUE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COUNT_DAYS", OracleDbType.Decimal, 0, "COUNT_DAYS");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DOCUM_PAY_CHANGE_DELETE(p_SALARY_DOCUM_PAY_CHANGE_ID => :p_SALARY_DOCUM_PAY_CHANGE_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_SALARY_DOCUM_PAY_CHANGE_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_PAY_CHANGE_ID");
	}
		
	#endregion
    }


    [Table(Name="SALARY_DOCUM_PERIOD"), SchemaName("SALARY")]
    public partial class SalaryDocumPeriod : RowEntityBase
    {
        #region Class Members
        [Column(Name="SALARY_DOCUM_PERIOD_ID")]
        public Decimal? SalaryDocumPeriodID
        {
            get
            {
        		return GetDataRowField<Decimal?>("SALARY_DOCUM_PERIOD_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryDocumPeriodID);
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
        		return GetDataRowField<Decimal?>("SALARY_DOCUM_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryDocumID);
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
        		return GetDataRowField<DateTime?>("END_PERIOD");
                //return this.GetDataRowField<DateTime?>(() => EndPeriod);
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
        		return GetDataRowField<DateTime?>("BEGIN_PERIOD");
                //return this.GetDataRowField<DateTime?>(() => BeginPeriod);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => BeginPeriod, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DOCUM_PERIOD_UPDATE(p_SALARY_DOCUM_PERIOD_ID=>:p_SALARY_DOCUM_PERIOD_ID, p_SALARY_DOCUM_ID=>:p_SALARY_DOCUM_ID, p_END_PERIOD=>:p_END_PERIOD, p_BEGIN_PERIOD=>:p_BEGIN_PERIOD);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_SALARY_DOCUM_PERIOD_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_PERIOD_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_SALARY_DOCUM_PERIOD_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_END_PERIOD", OracleDbType.Date, 0, "END_PERIOD");
            _dataAdapter.InsertCommand.Parameters.Add("p_BEGIN_PERIOD", OracleDbType.Date, 0, "BEGIN_PERIOD");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DOCUM_PERIOD_UPDATE(p_SALARY_DOCUM_PERIOD_ID=>:p_SALARY_DOCUM_PERIOD_ID, p_SALARY_DOCUM_ID=>:p_SALARY_DOCUM_ID, p_END_PERIOD=>:p_END_PERIOD, p_BEGIN_PERIOD=>:p_BEGIN_PERIOD);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_PERIOD_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_PERIOD_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_SALARY_DOCUM_PERIOD_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_DOCUM_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_END_PERIOD", OracleDbType.Date, 0, "END_PERIOD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BEGIN_PERIOD", OracleDbType.Date, 0, "BEGIN_PERIOD");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.SALARY_DOCUM_PERIOD_DELETE(p_SALARY_DOCUM_PERIOD_ID => :p_SALARY_DOCUM_PERIOD_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_SALARY_DOCUM_PERIOD_ID", OracleDbType.Decimal, 0, "SALARY_DOCUM_PERIOD_ID");
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
        		return GetDataRowField<Decimal?>("SALARY_ID");
                //return this.GetDataRowField<Decimal?>(() => SalaryID);
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
        		return GetDataRowField<String>("USER_NAME");
                //return this.GetDataRowField<String>(() => UserName);
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
        		return GetDataRowField<Decimal?>("OP_ID");
                //return this.GetDataRowField<Decimal?>(() => OpID);
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
        		return GetDataRowField<DateTime?>("DATE_EDIT");
                //return this.GetDataRowField<DateTime?>(() => DateEdit);
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
        		return GetDataRowField<Decimal?>("SESSION_ID");
                //return this.GetDataRowField<Decimal?>(() => SessionID);
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
        		return GetDataRowField<Decimal?>("RETENTION_ID");
                //return this.GetDataRowField<Decimal?>(() => RetentionID);
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
        		return GetDataRowField<Decimal?>("SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => SubdivID);
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
        		return GetDataRowField<Decimal?>("DAYS");
                //return this.GetDataRowField<Decimal?>(() => Days);
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
        		return GetDataRowField<Decimal?>("REF_ID");
                //return this.GetDataRowField<Decimal?>(() => RefID);
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
        		return GetDataRowField<Decimal?>("TYPE_REF_SALARY_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeRefSalaryID);
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
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
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
        		return GetDataRowField<Decimal?>("DEGREE_ID");
                //return this.GetDataRowField<Decimal?>(() => DegreeID);
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
        		return GetDataRowField<Decimal?>("ORDER_ID");
                //return this.GetDataRowField<Decimal?>(() => OrderID);
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
        		return GetDataRowField<Decimal?>("SUM_SAL");
                //return this.GetDataRowField<Decimal?>(() => SumSal);
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
        		return GetDataRowField<Decimal?>("HOURS");
                //return this.GetDataRowField<Decimal?>(() => Hours);
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
        		return GetDataRowField<Decimal?>("PAYMENT_TYPE_ID");
                //return this.GetDataRowField<Decimal?>(() => PaymentTypeID);
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
        		return GetDataRowField<DateTime?>("PAY_DATE");
                //return this.GetDataRowField<DateTime?>(() => PayDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => PayDate, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.SALARY_HISTORY_UPDATE(p_SALARY_ID=>:p_SALARY_ID, p_USER_NAME=>:p_USER_NAME, p_OP_ID=>:p_OP_ID, p_DATE_EDIT=>:p_DATE_EDIT, p_SESSION_ID=>:p_SESSION_ID, p_RETENTION_ID=>:p_RETENTION_ID, p_SUBDIV_ID=>:p_SUBDIV_ID, p_DAYS=>:p_DAYS, p_REF_ID=>:p_REF_ID, p_TYPE_REF_SALARY_ID=>:p_TYPE_REF_SALARY_ID, p_TRANSFER_ID=>:p_TRANSFER_ID, p_DEGREE_ID=>:p_DEGREE_ID, p_ORDER_ID=>:p_ORDER_ID, p_SUM_SAL=>:p_SUM_SAL, p_HOURS=>:p_HOURS, p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID, p_PAY_DATE=>:p_PAY_DATE);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, 0, "SALARY_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_USER_NAME", OracleDbType.Varchar2, 0, "USER_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_OP_ID", OracleDbType.Decimal, 0, "OP_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_EDIT", OracleDbType.Date, 0, "DATE_EDIT");
            _dataAdapter.InsertCommand.Parameters.Add("p_SESSION_ID", OracleDbType.Decimal, 0, "SESSION_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DAYS", OracleDbType.Decimal, 0, "DAYS");
            _dataAdapter.InsertCommand.Parameters.Add("p_REF_ID", OracleDbType.Decimal, 0, "REF_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_REF_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_REF_SALARY_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            _dataAdapter.InsertCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.SALARY_HISTORY_UPDATE(p_SALARY_ID=>:p_SALARY_ID, p_USER_NAME=>:p_USER_NAME, p_OP_ID=>:p_OP_ID, p_DATE_EDIT=>:p_DATE_EDIT, p_SESSION_ID=>:p_SESSION_ID, p_RETENTION_ID=>:p_RETENTION_ID, p_SUBDIV_ID=>:p_SUBDIV_ID, p_DAYS=>:p_DAYS, p_REF_ID=>:p_REF_ID, p_TYPE_REF_SALARY_ID=>:p_TYPE_REF_SALARY_ID, p_TRANSFER_ID=>:p_TRANSFER_ID, p_DEGREE_ID=>:p_DEGREE_ID, p_ORDER_ID=>:p_ORDER_ID, p_SUM_SAL=>:p_SUM_SAL, p_HOURS=>:p_HOURS, p_PAYMENT_TYPE_ID=>:p_PAYMENT_TYPE_ID, p_PAY_DATE=>:p_PAY_DATE);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_SALARY_ID", OracleDbType.Decimal, 0, "SALARY_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_USER_NAME", OracleDbType.Varchar2, 0, "USER_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_OP_ID", OracleDbType.Decimal, 0, "OP_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_EDIT", OracleDbType.Date, 0, "DATE_EDIT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SESSION_ID", OracleDbType.Decimal, 0, "SESSION_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RETENTION_ID", OracleDbType.Decimal, 0, "RETENTION_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DAYS", OracleDbType.Decimal, 0, "DAYS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_REF_ID", OracleDbType.Decimal, 0, "REF_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_REF_SALARY_ID", OracleDbType.Decimal, 0, "TYPE_REF_SALARY_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ORDER_ID", OracleDbType.Decimal, 0, "ORDER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            _dataAdapter.UpdateCommand.Parameters.Add("p_HOURS", OracleDbType.Decimal, 0, "HOURS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAYMENT_TYPE_ID", OracleDbType.Decimal, 0, "PAYMENT_TYPE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.SALARY_HISTORY_DELETE(p_ => :p_);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        		}
		
	#endregion
    }

    /// <summary>
    /// Таблица для закрытия областей редактирования
    /// </summary>

    [Table(Name="SUBDIV_FOR_CLOSE"), SchemaName("SALARY")]
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
        		return GetDataRowField<Decimal?>("SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => SubdivID);
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
        		return GetDataRowField<DateTime?>("LAST_DATE_PROCESSING");
                //return this.GetDataRowField<DateTime?>(() => LastDateProcessing);
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
        		return GetDataRowField<Decimal?>("SUBDIV_FOR_CLOSE_ID");
                //return this.GetDataRowField<Decimal?>(() => SubdivForCloseID);
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
        		return GetDataRowField<String>("APP_NAME");
                //return this.GetDataRowField<String>(() => AppName);
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
        		return GetDataRowField<DateTime?>("DATE_CHANGE");
                //return this.GetDataRowField<DateTime?>(() => DateChange);
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
        		return GetDataRowField<DateTime?>("DATE_CLOSING");
                //return this.GetDataRowField<DateTime?>(() => DateClosing);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateClosing, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.SUBDIV_FOR_CLOSE_UPDATE(p_SUBDIV_ID=>:p_SUBDIV_ID, p_LAST_DATE_PROCESSING=>:p_LAST_DATE_PROCESSING, p_SUBDIV_FOR_CLOSE_ID=>:p_SUBDIV_FOR_CLOSE_ID, p_APP_NAME=>:p_APP_NAME, p_DATE_CHANGE=>:p_DATE_CHANGE, p_DATE_CLOSING=>:p_DATE_CLOSING);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_LAST_DATE_PROCESSING", OracleDbType.Date, 0, "LAST_DATE_PROCESSING");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_FOR_CLOSE_ID", OracleDbType.Decimal, 0, "SUBDIV_FOR_CLOSE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_SUBDIV_FOR_CLOSE_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_APP_NAME", OracleDbType.Varchar2, 0, "APP_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_CHANGE", OracleDbType.Date, 0, "DATE_CHANGE");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_CLOSING", OracleDbType.Date, 0, "DATE_CLOSING");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.SUBDIV_FOR_CLOSE_UPDATE(p_SUBDIV_ID=>:p_SUBDIV_ID, p_LAST_DATE_PROCESSING=>:p_LAST_DATE_PROCESSING, p_SUBDIV_FOR_CLOSE_ID=>:p_SUBDIV_FOR_CLOSE_ID, p_APP_NAME=>:p_APP_NAME, p_DATE_CHANGE=>:p_DATE_CHANGE, p_DATE_CLOSING=>:p_DATE_CLOSING);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_LAST_DATE_PROCESSING", OracleDbType.Date, 0, "LAST_DATE_PROCESSING");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_FOR_CLOSE_ID", OracleDbType.Decimal, 0, "SUBDIV_FOR_CLOSE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_SUBDIV_FOR_CLOSE_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_APP_NAME", OracleDbType.Varchar2, 0, "APP_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_CHANGE", OracleDbType.Date, 0, "DATE_CHANGE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_CLOSING", OracleDbType.Date, 0, "DATE_CLOSING");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.SUBDIV_FOR_CLOSE_DELETE(p_SUBDIV_FOR_CLOSE_ID => :p_SUBDIV_FOR_CLOSE_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_SUBDIV_FOR_CLOSE_ID", OracleDbType.Decimal, 0, "SUBDIV_FOR_CLOSE_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Таблица табелей бригад
    /// </summary>

    [Table(Name="TABLE_BRIGAGE"), SchemaName("SALARY")]
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
        		return GetDataRowField<Decimal?>("TABLE_BRIGAGE_ID");
                //return this.GetDataRowField<Decimal?>(() => TableBrigageID);
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
        		return GetDataRowField<DateTime?>("WORK_DATE");
                //return this.GetDataRowField<DateTime?>(() => WorkDate);
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
        		return GetDataRowField<Decimal?>("SUBDIV_ID");
                //return this.GetDataRowField<Decimal?>(() => SubdivID);
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
        		return GetDataRowField<Decimal?>("TRANSFER_ID");
                //return this.GetDataRowField<Decimal?>(() => TransferID);
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
        		return GetDataRowField<Decimal?>("SIGN_COMB");
                //return this.GetDataRowField<Decimal?>(() => SignComb);
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
        		return GetDataRowField<Decimal?>("COEFFICIENT");
                //return this.GetDataRowField<Decimal?>(() => Coefficient);
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
        		return GetDataRowField<Decimal?>("WORK_HOURS");
                //return this.GetDataRowField<Decimal?>(() => WorkHours);
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
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
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
        		return GetDataRowField<String>("COMMENTS");
                //return this.GetDataRowField<String>(() => Comments);
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
        		return GetDataRowField<Decimal?>("DEGREE_ID");
                //return this.GetDataRowField<Decimal?>(() => DegreeID);
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
        		return GetDataRowField<Decimal?>("BRIGAGE_ID");
                //return this.GetDataRowField<Decimal?>(() => BrigageID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => BrigageID, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.TABLE_BRIGAGE_UPDATE(p_TABLE_BRIGAGE_ID=>:p_TABLE_BRIGAGE_ID, p_WORK_DATE=>:p_WORK_DATE, p_SUBDIV_ID=>:p_SUBDIV_ID, p_TRANSFER_ID=>:p_TRANSFER_ID, p_SIGN_COMB=>:p_SIGN_COMB, p_COEFFICIENT=>:p_COEFFICIENT, p_WORK_HOURS=>:p_WORK_HOURS, p_PER_NUM=>:p_PER_NUM, p_COMMENTS=>:p_COMMENTS, p_DEGREE_ID=>:p_DEGREE_ID, p_BRIGAGE_ID=>:p_BRIGAGE_ID);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TABLE_BRIGAGE_ID", OracleDbType.Decimal, 0, "TABLE_BRIGAGE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_TABLE_BRIGAGE_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_WORK_DATE", OracleDbType.Date, 0, "WORK_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            _dataAdapter.InsertCommand.Parameters.Add("p_COEFFICIENT", OracleDbType.Decimal, 0, "COEFFICIENT");
            _dataAdapter.InsertCommand.Parameters.Add("p_WORK_HOURS", OracleDbType.Decimal, 0, "WORK_HOURS");
            _dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_COMMENTS", OracleDbType.Varchar2, 0, "COMMENTS");
            _dataAdapter.InsertCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_BRIGAGE_ID", OracleDbType.Decimal, 0, "BRIGAGE_ID");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.TABLE_BRIGAGE_UPDATE(p_TABLE_BRIGAGE_ID=>:p_TABLE_BRIGAGE_ID, p_WORK_DATE=>:p_WORK_DATE, p_SUBDIV_ID=>:p_SUBDIV_ID, p_TRANSFER_ID=>:p_TRANSFER_ID, p_SIGN_COMB=>:p_SIGN_COMB, p_COEFFICIENT=>:p_COEFFICIENT, p_WORK_HOURS=>:p_WORK_HOURS, p_PER_NUM=>:p_PER_NUM, p_COMMENTS=>:p_COMMENTS, p_DEGREE_ID=>:p_DEGREE_ID, p_BRIGAGE_ID=>:p_BRIGAGE_ID);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TABLE_BRIGAGE_ID", OracleDbType.Decimal, 0, "TABLE_BRIGAGE_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_TABLE_BRIGAGE_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_WORK_DATE", OracleDbType.Date, 0, "WORK_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUBDIV_ID", OracleDbType.Decimal, 0, "SUBDIV_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_ID", OracleDbType.Decimal, 0, "TRANSFER_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_COMB", OracleDbType.Decimal, 0, "SIGN_COMB");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COEFFICIENT", OracleDbType.Decimal, 0, "COEFFICIENT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_WORK_HOURS", OracleDbType.Decimal, 0, "WORK_HOURS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COMMENTS", OracleDbType.Varchar2, 0, "COMMENTS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DEGREE_ID", OracleDbType.Decimal, 0, "DEGREE_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BRIGAGE_ID", OracleDbType.Decimal, 0, "BRIGAGE_ID");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.TABLE_BRIGAGE_DELETE(p_TABLE_BRIGAGE_ID => :p_TABLE_BRIGAGE_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_TABLE_BRIGAGE_ID", OracleDbType.Decimal, 0, "TABLE_BRIGAGE_ID");
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
        		return GetDataRowField<Decimal?>("TAX_COMPANY_ID");
                //return this.GetDataRowField<Decimal?>(() => TaxCompanyID);
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
        		return GetDataRowField<String>("COMPANY_NAME");
                //return this.GetDataRowField<String>(() => CompanyName);
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
        		return GetDataRowField<String>("OKTMO");
                //return this.GetDataRowField<String>(() => Oktmo);
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
        		return GetDataRowField<String>("TEL");
                //return this.GetDataRowField<String>(() => Tel);
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
        		return GetDataRowField<String>("INN");
                //return this.GetDataRowField<String>(() => Inn);
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
        		return GetDataRowField<String>("KPP");
                //return this.GetDataRowField<String>(() => Kpp);
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
        		return GetDataRowField<String>("AGENT_STATUS");
                //return this.GetDataRowField<String>(() => AgentStatus);
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
        		return GetDataRowField<String>("AGENT_NAME");
                //return this.GetDataRowField<String>(() => AgentName);
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
        		return GetDataRowField<DateTime?>("DATE_BEGIN");
                //return this.GetDataRowField<DateTime?>(() => DateBegin);
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
        		return GetDataRowField<DateTime?>("DATE_END");
                //return this.GetDataRowField<DateTime?>(() => DateEnd);
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
        		return GetDataRowField<String>("COMMENTS");
                //return this.GetDataRowField<String>(() => Comments);
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
        		return GetDataRowField<String>("SHORT_COMPANY_NAME");
                //return this.GetDataRowField<String>(() => ShortCompanyName);
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
        		return GetDataRowField<String>("AGENT_DOCUMENT");
                //return this.GetDataRowField<String>(() => AgentDocument);
            }
            set
            {
                UpdateDataRow<String>(() => AgentDocument, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.TAX_COMPANY_UPDATE(p_TAX_COMPANY_ID=>:p_TAX_COMPANY_ID, p_COMPANY_NAME=>:p_COMPANY_NAME, p_OKTMO=>:p_OKTMO, p_TEL=>:p_TEL, p_INN=>:p_INN, p_KPP=>:p_KPP, p_AGENT_STATUS=>:p_AGENT_STATUS, p_AGENT_NAME=>:p_AGENT_NAME, p_DATE_BEGIN=>:p_DATE_BEGIN, p_DATE_END=>:p_DATE_END, p_COMMENTS=>:p_COMMENTS, p_SHORT_COMPANY_NAME=>:p_SHORT_COMPANY_NAME, p_AGENT_DOCUMENT=>:p_AGENT_DOCUMENT);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TAX_COMPANY_ID", OracleDbType.Decimal, 0, "TAX_COMPANY_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_TAX_COMPANY_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_COMPANY_NAME", OracleDbType.Varchar2, 0, "COMPANY_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_OKTMO", OracleDbType.Varchar2, 0, "OKTMO");
            _dataAdapter.InsertCommand.Parameters.Add("p_TEL", OracleDbType.Varchar2, 0, "TEL");
            _dataAdapter.InsertCommand.Parameters.Add("p_INN", OracleDbType.Varchar2, 0, "INN");
            _dataAdapter.InsertCommand.Parameters.Add("p_KPP", OracleDbType.Varchar2, 0, "KPP");
            _dataAdapter.InsertCommand.Parameters.Add("p_AGENT_STATUS", OracleDbType.Varchar2, 0, "AGENT_STATUS");
            _dataAdapter.InsertCommand.Parameters.Add("p_AGENT_NAME", OracleDbType.Varchar2, 0, "AGENT_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_BEGIN", OracleDbType.Date, 0, "DATE_BEGIN");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_END", OracleDbType.Date, 0, "DATE_END");
            _dataAdapter.InsertCommand.Parameters.Add("p_COMMENTS", OracleDbType.Varchar2, 0, "COMMENTS");
            _dataAdapter.InsertCommand.Parameters.Add("p_SHORT_COMPANY_NAME", OracleDbType.Varchar2, 0, "SHORT_COMPANY_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_AGENT_DOCUMENT", OracleDbType.Varchar2, 0, "AGENT_DOCUMENT");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.TAX_COMPANY_UPDATE(p_TAX_COMPANY_ID=>:p_TAX_COMPANY_ID, p_COMPANY_NAME=>:p_COMPANY_NAME, p_OKTMO=>:p_OKTMO, p_TEL=>:p_TEL, p_INN=>:p_INN, p_KPP=>:p_KPP, p_AGENT_STATUS=>:p_AGENT_STATUS, p_AGENT_NAME=>:p_AGENT_NAME, p_DATE_BEGIN=>:p_DATE_BEGIN, p_DATE_END=>:p_DATE_END, p_COMMENTS=>:p_COMMENTS, p_SHORT_COMPANY_NAME=>:p_SHORT_COMPANY_NAME, p_AGENT_DOCUMENT=>:p_AGENT_DOCUMENT);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TAX_COMPANY_ID", OracleDbType.Decimal, 0, "TAX_COMPANY_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_TAX_COMPANY_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_COMPANY_NAME", OracleDbType.Varchar2, 0, "COMPANY_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_OKTMO", OracleDbType.Varchar2, 0, "OKTMO");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TEL", OracleDbType.Varchar2, 0, "TEL");
            _dataAdapter.UpdateCommand.Parameters.Add("p_INN", OracleDbType.Varchar2, 0, "INN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_KPP", OracleDbType.Varchar2, 0, "KPP");
            _dataAdapter.UpdateCommand.Parameters.Add("p_AGENT_STATUS", OracleDbType.Varchar2, 0, "AGENT_STATUS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_AGENT_NAME", OracleDbType.Varchar2, 0, "AGENT_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_BEGIN", OracleDbType.Date, 0, "DATE_BEGIN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_END", OracleDbType.Date, 0, "DATE_END");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COMMENTS", OracleDbType.Varchar2, 0, "COMMENTS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SHORT_COMPANY_NAME", OracleDbType.Varchar2, 0, "SHORT_COMPANY_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_AGENT_DOCUMENT", OracleDbType.Varchar2, 0, "AGENT_DOCUMENT");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.TAX_COMPANY_DELETE(p_TAX_COMPANY_ID => :p_TAX_COMPANY_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_TAX_COMPANY_ID", OracleDbType.Decimal, 0, "TAX_COMPANY_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Налоговый вычеты по документу
    /// </summary>

    [Table(Name="TAX_DOCUM_DISCOUNT"), SchemaName("SALARY")]
    public partial class TaxDocumDiscount : RowEntityBase
    {
        #region Class Members
        [Column(Name="TAX_DOCUM_DISCOUNT_ID")]
        public Decimal? TaxDocumDiscountID
        {
            get
            {
        		return GetDataRowField<Decimal?>("TAX_DOCUM_DISCOUNT_ID");
                //return this.GetDataRowField<Decimal?>(() => TaxDocumDiscountID);
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
        		return GetDataRowField<Decimal?>("TAX_EMP_DOCUM_ID");
                //return this.GetDataRowField<Decimal?>(() => TaxEmpDocumID);
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
        		return GetDataRowField<String>("CODE_DISCOUNT");
                //return this.GetDataRowField<String>(() => CodeDiscount);
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
        		return GetDataRowField<Decimal?>("SUM_DISCOUNT");
                //return this.GetDataRowField<Decimal?>(() => SumDiscount);
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
        		return GetDataRowField<DateTime?>("DATE_DISCOUNT");
                //return this.GetDataRowField<DateTime?>(() => DateDiscount);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => DateDiscount, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.TAX_DOCUM_DISCOUNT_UPDATE(p_TAX_DOCUM_DISCOUNT_ID=>:p_TAX_DOCUM_DISCOUNT_ID, p_TAX_EMP_DOCUM_ID=>:p_TAX_EMP_DOCUM_ID, p_CODE_DISCOUNT=>:p_CODE_DISCOUNT, p_SUM_DISCOUNT=>:p_SUM_DISCOUNT, p_DATE_DISCOUNT=>:p_DATE_DISCOUNT);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TAX_DOCUM_DISCOUNT_ID", OracleDbType.Decimal, 0, "TAX_DOCUM_DISCOUNT_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_TAX_DOCUM_DISCOUNT_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_TAX_EMP_DOCUM_ID", OracleDbType.Decimal, 0, "TAX_EMP_DOCUM_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_DISCOUNT", OracleDbType.Varchar2, 0, "CODE_DISCOUNT");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUM_DISCOUNT", OracleDbType.Decimal, 0, "SUM_DISCOUNT");
            _dataAdapter.InsertCommand.Parameters.Add("p_DATE_DISCOUNT", OracleDbType.Date, 0, "DATE_DISCOUNT");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.TAX_DOCUM_DISCOUNT_UPDATE(p_TAX_DOCUM_DISCOUNT_ID=>:p_TAX_DOCUM_DISCOUNT_ID, p_TAX_EMP_DOCUM_ID=>:p_TAX_EMP_DOCUM_ID, p_CODE_DISCOUNT=>:p_CODE_DISCOUNT, p_SUM_DISCOUNT=>:p_SUM_DISCOUNT, p_DATE_DISCOUNT=>:p_DATE_DISCOUNT);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TAX_DOCUM_DISCOUNT_ID", OracleDbType.Decimal, 0, "TAX_DOCUM_DISCOUNT_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_TAX_DOCUM_DISCOUNT_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_TAX_EMP_DOCUM_ID", OracleDbType.Decimal, 0, "TAX_EMP_DOCUM_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_DISCOUNT", OracleDbType.Varchar2, 0, "CODE_DISCOUNT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUM_DISCOUNT", OracleDbType.Decimal, 0, "SUM_DISCOUNT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DATE_DISCOUNT", OracleDbType.Date, 0, "DATE_DISCOUNT");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.TAX_DOCUM_DISCOUNT_DELETE(p_TAX_DOCUM_DISCOUNT_ID => :p_TAX_DOCUM_DISCOUNT_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_TAX_DOCUM_DISCOUNT_ID", OracleDbType.Decimal, 0, "TAX_DOCUM_DISCOUNT_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Выплаты по документу
    /// </summary>

    [Table(Name="TAX_DOCUM_PAYMENT"), SchemaName("SALARY")]
    public partial class TaxDocumPayment : RowEntityBase
    {
        #region Class Members
        [Column(Name="TAX_DOCUM_PAYMENT_ID")]
        public Decimal? TaxDocumPaymentID
        {
            get
            {
        		return GetDataRowField<Decimal?>("TAX_DOCUM_PAYMENT_ID");
                //return this.GetDataRowField<Decimal?>(() => TaxDocumPaymentID);
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
        		return GetDataRowField<Decimal?>("TAX_EMP_DOCUM_ID");
                //return this.GetDataRowField<Decimal?>(() => TaxEmpDocumID);
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
        		return GetDataRowField<DateTime?>("PAY_DATE");
                //return this.GetDataRowField<DateTime?>(() => PayDate);
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
        		return GetDataRowField<String>("PAY_CODE");
                //return this.GetDataRowField<String>(() => PayCode);
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
        		return GetDataRowField<Decimal?>("SUM_SAL");
                //return this.GetDataRowField<Decimal?>(() => SumSal);
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
        		return GetDataRowField<String>("CODE_DISC");
                //return this.GetDataRowField<String>(() => CodeDisc);
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
        		return GetDataRowField<Decimal?>("SUM_DISC");
                //return this.GetDataRowField<Decimal?>(() => SumDisc);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => SumDisc, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.TAX_DOCUM_PAYMENT_UPDATE(p_TAX_DOCUM_PAYMENT_ID=>:p_TAX_DOCUM_PAYMENT_ID, p_TAX_EMP_DOCUM_ID=>:p_TAX_EMP_DOCUM_ID, p_PAY_DATE=>:p_PAY_DATE, p_PAY_CODE=>:p_PAY_CODE, p_SUM_SAL=>:p_SUM_SAL, p_CODE_DISC=>:p_CODE_DISC, p_SUM_DISC=>:p_SUM_DISC);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TAX_DOCUM_PAYMENT_ID", OracleDbType.Decimal, 0, "TAX_DOCUM_PAYMENT_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_TAX_DOCUM_PAYMENT_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_TAX_EMP_DOCUM_ID", OracleDbType.Decimal, 0, "TAX_EMP_DOCUM_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAY_CODE", OracleDbType.Varchar2, 0, "PAY_CODE");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_DISC", OracleDbType.Varchar2, 0, "CODE_DISC");
            _dataAdapter.InsertCommand.Parameters.Add("p_SUM_DISC", OracleDbType.Decimal, 0, "SUM_DISC");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.TAX_DOCUM_PAYMENT_UPDATE(p_TAX_DOCUM_PAYMENT_ID=>:p_TAX_DOCUM_PAYMENT_ID, p_TAX_EMP_DOCUM_ID=>:p_TAX_EMP_DOCUM_ID, p_PAY_DATE=>:p_PAY_DATE, p_PAY_CODE=>:p_PAY_CODE, p_SUM_SAL=>:p_SUM_SAL, p_CODE_DISC=>:p_CODE_DISC, p_SUM_DISC=>:p_SUM_DISC);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TAX_DOCUM_PAYMENT_ID", OracleDbType.Decimal, 0, "TAX_DOCUM_PAYMENT_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_TAX_DOCUM_PAYMENT_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_TAX_EMP_DOCUM_ID", OracleDbType.Decimal, 0, "TAX_EMP_DOCUM_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAY_DATE", OracleDbType.Date, 0, "PAY_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAY_CODE", OracleDbType.Varchar2, 0, "PAY_CODE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUM_SAL", OracleDbType.Decimal, 0, "SUM_SAL");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_DISC", OracleDbType.Varchar2, 0, "CODE_DISC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SUM_DISC", OracleDbType.Decimal, 0, "SUM_DISC");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.TAX_DOCUM_PAYMENT_DELETE(p_TAX_DOCUM_PAYMENT_ID => :p_TAX_DOCUM_PAYMENT_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_TAX_DOCUM_PAYMENT_ID", OracleDbType.Decimal, 0, "TAX_DOCUM_PAYMENT_ID");
	}
		
	#endregion
    }


    [Table(Name="TAX_EMP_DOCUM"), SchemaName("SALARY")]
    public partial class TaxEmpDocum : RowEntityBase
    {
        #region Class Members
        [Column(Name="TAX_EMP_DOCUM_ID")]
        public Decimal? TaxEmpDocumID
        {
            get
            {
        		return GetDataRowField<Decimal?>("TAX_EMP_DOCUM_ID");
                //return this.GetDataRowField<Decimal?>(() => TaxEmpDocumID);
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
        		return GetDataRowField<String>("PER_NUM");
                //return this.GetDataRowField<String>(() => PerNum);
            }
            set
            {
                UpdateDataRow<String>(() => PerNum, value);
            }
        }
        /// <summary>
        /// Налоговый статус документа
        /// </summary>
        [Column(Name="TAX_STATUS", CanBeNull=false)]
        public Decimal? TaxStatus
        {
            get
            {
        		return GetDataRowField<Decimal?>("TAX_STATUS");
                //return this.GetDataRowField<Decimal?>(() => TaxStatus);
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
        		return GetDataRowField<Decimal?>("TYPE_PER_DOC_ID");
                //return this.GetDataRowField<Decimal?>(() => TypePerDocID);
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
        		return GetDataRowField<String>("HOME_INDEX");
                //return this.GetDataRowField<String>(() => HomeIndex);
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
        		return GetDataRowField<String>("CODE_REGION");
                //return this.GetDataRowField<String>(() => CodeRegion);
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
        		return GetDataRowField<String>("DISTRICT");
                //return this.GetDataRowField<String>(() => District);
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
        		return GetDataRowField<String>("CITY");
                //return this.GetDataRowField<String>(() => City);
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
        		return GetDataRowField<String>("LOCALITY");
                //return this.GetDataRowField<String>(() => Locality);
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
        		return GetDataRowField<String>("STREET");
                //return this.GetDataRowField<String>(() => Street);
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
        		return GetDataRowField<String>("HOME_NUMBER");
                //return this.GetDataRowField<String>(() => HomeNumber);
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
        		return GetDataRowField<String>("HOUSING");
                //return this.GetDataRowField<String>(() => Housing);
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
        		return GetDataRowField<String>("FLAT_NUMBER");
                //return this.GetDataRowField<String>(() => FlatNumber);
            }
            set
            {
                UpdateDataRow<String>(() => FlatNumber, value);
            }
        }
        /// <summary>
        /// Налоговая ставка в процентах
        /// </summary>
        [Column(Name="TAX_PERCENT", CanBeNull=false)]
        public Decimal? TaxPercent
        {
            get
            {
        		return GetDataRowField<Decimal?>("TAX_PERCENT");
                //return this.GetDataRowField<Decimal?>(() => TaxPercent);
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
        		return GetDataRowField<String>("SOCIAL_NOTIFY_NUMBER");
                //return this.GetDataRowField<String>(() => SocialNotifyNumber);
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
        		return GetDataRowField<DateTime?>("SOCIAL_NOTIFY_DATE");
                //return this.GetDataRowField<DateTime?>(() => SocialNotifyDate);
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
        		return GetDataRowField<String>("SOCIAL_NOTIFY_CODE");
                //return this.GetDataRowField<String>(() => SocialNotifyCode);
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
        		return GetDataRowField<String>("ESTATE_NOTIFY_NUMBER");
                //return this.GetDataRowField<String>(() => EstateNotifyNumber);
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
        		return GetDataRowField<DateTime?>("ESTATE_NOTIFY_DATE");
                //return this.GetDataRowField<DateTime?>(() => EstateNotifyDate);
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
        		return GetDataRowField<String>("ESTATE_NOTIFY_CODE");
                //return this.GetDataRowField<String>(() => EstateNotifyCode);
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
        		return GetDataRowField<String>("ADVANCE_PAY_NOTIFY_NUMBER");
                //return this.GetDataRowField<String>(() => AdvancePayNotifyNumber);
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
        		return GetDataRowField<DateTime?>("ADVANCE_PAY_NOTIFY_DATE");
                //return this.GetDataRowField<DateTime?>(() => AdvancePayNotifyDate);
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
        		return GetDataRowField<String>("ADVANCE_PAY_NOTIFY_CODE");
                //return this.GetDataRowField<String>(() => AdvancePayNotifyCode);
            }
            set
            {
                UpdateDataRow<String>(() => AdvancePayNotifyCode, value);
            }
        }
        /// <summary>
        /// Ссылка на организацию
        /// </summary>
        [Column(Name="TAX_COMPANY_ID", CanBeNull=false)]
        public Decimal? TaxCompanyID
        {
            get
            {
        		return GetDataRowField<Decimal?>("TAX_COMPANY_ID");
                //return this.GetDataRowField<Decimal?>(() => TaxCompanyID);
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
        		return GetDataRowField<Decimal?>("DOCUM_NUMBER");
                //return this.GetDataRowField<Decimal?>(() => DocumNumber);
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
        		return GetDataRowField<Decimal?>("DOCUM_SIGN");
                //return this.GetDataRowField<Decimal?>(() => DocumSign);
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
        		return GetDataRowField<Decimal?>("CORR_NUMBER");
                //return this.GetDataRowField<Decimal?>(() => CorrNumber);
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
        		return GetDataRowField<Decimal?>("LOCK_SIGN");
                //return this.GetDataRowField<Decimal?>(() => LockSign);
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
        		return GetDataRowField<Decimal?>("RETENT_TAX");
                //return this.GetDataRowField<Decimal?>(() => RetentTax);
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
        		return GetDataRowField<Decimal?>("TRANSFERED_TAX");
                //return this.GetDataRowField<Decimal?>(() => TransferedTax);
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
        		return GetDataRowField<String>("CODE_COUNTRY");
                //return this.GetDataRowField<String>(() => CodeCountry);
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
        		return GetDataRowField<Decimal?>("CALCED_TAX");
                //return this.GetDataRowField<Decimal?>(() => CalcedTax);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CalcedTax, value);
            }
        }
        /// <summary>
        /// Отчетный период документа (последний месяц)
        /// </summary>
        [Column(Name="DOCUM_DATE", CanBeNull=false)]
        public DateTime? DocumDate
        {
            get
            {
        		return GetDataRowField<DateTime?>("DOCUM_DATE");
                //return this.GetDataRowField<DateTime?>(() => DocumDate);
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
        		return GetDataRowField<DateTime?>("UPDATE_DATE");
                //return this.GetDataRowField<DateTime?>(() => UpdateDate);
            }
            set
            {
                UpdateDataRow<DateTime?>(() => UpdateDate, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.TAX_EMP_DOCUM_UPDATE(p_TAX_EMP_DOCUM_ID=>:p_TAX_EMP_DOCUM_ID, p_PER_NUM=>:p_PER_NUM, p_TAX_STATUS=>:p_TAX_STATUS, p_TYPE_PER_DOC_ID=>:p_TYPE_PER_DOC_ID, p_HOME_INDEX=>:p_HOME_INDEX, p_CODE_REGION=>:p_CODE_REGION, p_DISTRICT=>:p_DISTRICT, p_CITY=>:p_CITY, p_LOCALITY=>:p_LOCALITY, p_STREET=>:p_STREET, p_HOME_NUMBER=>:p_HOME_NUMBER, p_HOUSING=>:p_HOUSING, p_FLAT_NUMBER=>:p_FLAT_NUMBER, p_TAX_PERCENT=>:p_TAX_PERCENT, p_SOCIAL_NOTIFY_NUMBER=>:p_SOCIAL_NOTIFY_NUMBER, p_SOCIAL_NOTIFY_DATE=>:p_SOCIAL_NOTIFY_DATE, p_SOCIAL_NOTIFY_CODE=>:p_SOCIAL_NOTIFY_CODE, p_ESTATE_NOTIFY_NUMBER=>:p_ESTATE_NOTIFY_NUMBER, p_ESTATE_NOTIFY_DATE=>:p_ESTATE_NOTIFY_DATE, p_ESTATE_NOTIFY_CODE=>:p_ESTATE_NOTIFY_CODE, p_ADVANCE_PAY_NOTIFY_NUMBER=>:p_ADVANCE_PAY_NOTIFY_NUMBER, p_ADVANCE_PAY_NOTIFY_DATE=>:p_ADVANCE_PAY_NOTIFY_DATE, p_ADVANCE_PAY_NOTIFY_CODE=>:p_ADVANCE_PAY_NOTIFY_CODE, p_TAX_COMPANY_ID=>:p_TAX_COMPANY_ID, p_DOCUM_NUMBER=>:p_DOCUM_NUMBER, p_DOCUM_SIGN=>:p_DOCUM_SIGN, p_CORR_NUMBER=>:p_CORR_NUMBER, p_LOCK_SIGN=>:p_LOCK_SIGN, p_RETENT_TAX=>:p_RETENT_TAX, p_TRANSFERED_TAX=>:p_TRANSFERED_TAX, p_CODE_COUNTRY=>:p_CODE_COUNTRY, p_CALCED_TAX=>:p_CALCED_TAX, p_DOCUM_DATE=>:p_DOCUM_DATE, p_UPDATE_DATE=>:p_UPDATE_DATE);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TAX_EMP_DOCUM_ID", OracleDbType.Decimal, 0, "TAX_EMP_DOCUM_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_TAX_EMP_DOCUM_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.InsertCommand.Parameters.Add("p_TAX_STATUS", OracleDbType.Decimal, 0, "TAX_STATUS");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_PER_DOC_ID", OracleDbType.Decimal, 0, "TYPE_PER_DOC_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_HOME_INDEX", OracleDbType.Varchar2, 0, "HOME_INDEX");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_REGION", OracleDbType.Varchar2, 0, "CODE_REGION");
            _dataAdapter.InsertCommand.Parameters.Add("p_DISTRICT", OracleDbType.Varchar2, 0, "DISTRICT");
            _dataAdapter.InsertCommand.Parameters.Add("p_CITY", OracleDbType.Varchar2, 0, "CITY");
            _dataAdapter.InsertCommand.Parameters.Add("p_LOCALITY", OracleDbType.Varchar2, 0, "LOCALITY");
            _dataAdapter.InsertCommand.Parameters.Add("p_STREET", OracleDbType.Varchar2, 0, "STREET");
            _dataAdapter.InsertCommand.Parameters.Add("p_HOME_NUMBER", OracleDbType.Varchar2, 0, "HOME_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_HOUSING", OracleDbType.Varchar2, 0, "HOUSING");
            _dataAdapter.InsertCommand.Parameters.Add("p_FLAT_NUMBER", OracleDbType.Varchar2, 0, "FLAT_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_TAX_PERCENT", OracleDbType.Decimal, 0, "TAX_PERCENT");
            _dataAdapter.InsertCommand.Parameters.Add("p_SOCIAL_NOTIFY_NUMBER", OracleDbType.Varchar2, 0, "SOCIAL_NOTIFY_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_SOCIAL_NOTIFY_DATE", OracleDbType.Date, 0, "SOCIAL_NOTIFY_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_SOCIAL_NOTIFY_CODE", OracleDbType.Varchar2, 0, "SOCIAL_NOTIFY_CODE");
            _dataAdapter.InsertCommand.Parameters.Add("p_ESTATE_NOTIFY_NUMBER", OracleDbType.Varchar2, 0, "ESTATE_NOTIFY_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_ESTATE_NOTIFY_DATE", OracleDbType.Date, 0, "ESTATE_NOTIFY_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_ESTATE_NOTIFY_CODE", OracleDbType.Varchar2, 0, "ESTATE_NOTIFY_CODE");
            _dataAdapter.InsertCommand.Parameters.Add("p_ADVANCE_PAY_NOTIFY_NUMBER", OracleDbType.Varchar2, 0, "ADVANCE_PAY_NOTIFY_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_ADVANCE_PAY_NOTIFY_DATE", OracleDbType.Date, 0, "ADVANCE_PAY_NOTIFY_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_ADVANCE_PAY_NOTIFY_CODE", OracleDbType.Varchar2, 0, "ADVANCE_PAY_NOTIFY_CODE");
            _dataAdapter.InsertCommand.Parameters.Add("p_TAX_COMPANY_ID", OracleDbType.Decimal, 0, "TAX_COMPANY_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOCUM_NUMBER", OracleDbType.Decimal, 0, "DOCUM_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOCUM_SIGN", OracleDbType.Decimal, 0, "DOCUM_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_CORR_NUMBER", OracleDbType.Decimal, 0, "CORR_NUMBER");
            _dataAdapter.InsertCommand.Parameters.Add("p_LOCK_SIGN", OracleDbType.Decimal, 0, "LOCK_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_RETENT_TAX", OracleDbType.Decimal, 0, "RETENT_TAX");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRANSFERED_TAX", OracleDbType.Decimal, 0, "TRANSFERED_TAX");
            _dataAdapter.InsertCommand.Parameters.Add("p_CODE_COUNTRY", OracleDbType.Varchar2, 0, "CODE_COUNTRY");
            _dataAdapter.InsertCommand.Parameters.Add("p_CALCED_TAX", OracleDbType.Decimal, 0, "CALCED_TAX");
            _dataAdapter.InsertCommand.Parameters.Add("p_DOCUM_DATE", OracleDbType.Date, 0, "DOCUM_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_UPDATE_DATE", OracleDbType.Date, 0, "UPDATE_DATE");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.TAX_EMP_DOCUM_UPDATE(p_TAX_EMP_DOCUM_ID=>:p_TAX_EMP_DOCUM_ID, p_PER_NUM=>:p_PER_NUM, p_TAX_STATUS=>:p_TAX_STATUS, p_TYPE_PER_DOC_ID=>:p_TYPE_PER_DOC_ID, p_HOME_INDEX=>:p_HOME_INDEX, p_CODE_REGION=>:p_CODE_REGION, p_DISTRICT=>:p_DISTRICT, p_CITY=>:p_CITY, p_LOCALITY=>:p_LOCALITY, p_STREET=>:p_STREET, p_HOME_NUMBER=>:p_HOME_NUMBER, p_HOUSING=>:p_HOUSING, p_FLAT_NUMBER=>:p_FLAT_NUMBER, p_TAX_PERCENT=>:p_TAX_PERCENT, p_SOCIAL_NOTIFY_NUMBER=>:p_SOCIAL_NOTIFY_NUMBER, p_SOCIAL_NOTIFY_DATE=>:p_SOCIAL_NOTIFY_DATE, p_SOCIAL_NOTIFY_CODE=>:p_SOCIAL_NOTIFY_CODE, p_ESTATE_NOTIFY_NUMBER=>:p_ESTATE_NOTIFY_NUMBER, p_ESTATE_NOTIFY_DATE=>:p_ESTATE_NOTIFY_DATE, p_ESTATE_NOTIFY_CODE=>:p_ESTATE_NOTIFY_CODE, p_ADVANCE_PAY_NOTIFY_NUMBER=>:p_ADVANCE_PAY_NOTIFY_NUMBER, p_ADVANCE_PAY_NOTIFY_DATE=>:p_ADVANCE_PAY_NOTIFY_DATE, p_ADVANCE_PAY_NOTIFY_CODE=>:p_ADVANCE_PAY_NOTIFY_CODE, p_TAX_COMPANY_ID=>:p_TAX_COMPANY_ID, p_DOCUM_NUMBER=>:p_DOCUM_NUMBER, p_DOCUM_SIGN=>:p_DOCUM_SIGN, p_CORR_NUMBER=>:p_CORR_NUMBER, p_LOCK_SIGN=>:p_LOCK_SIGN, p_RETENT_TAX=>:p_RETENT_TAX, p_TRANSFERED_TAX=>:p_TRANSFERED_TAX, p_CODE_COUNTRY=>:p_CODE_COUNTRY, p_CALCED_TAX=>:p_CALCED_TAX, p_DOCUM_DATE=>:p_DOCUM_DATE, p_UPDATE_DATE=>:p_UPDATE_DATE);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TAX_EMP_DOCUM_ID", OracleDbType.Decimal, 0, "TAX_EMP_DOCUM_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_TAX_EMP_DOCUM_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_PER_NUM", OracleDbType.Varchar2, 0, "PER_NUM");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TAX_STATUS", OracleDbType.Decimal, 0, "TAX_STATUS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_PER_DOC_ID", OracleDbType.Decimal, 0, "TYPE_PER_DOC_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_HOME_INDEX", OracleDbType.Varchar2, 0, "HOME_INDEX");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_REGION", OracleDbType.Varchar2, 0, "CODE_REGION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DISTRICT", OracleDbType.Varchar2, 0, "DISTRICT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CITY", OracleDbType.Varchar2, 0, "CITY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_LOCALITY", OracleDbType.Varchar2, 0, "LOCALITY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_STREET", OracleDbType.Varchar2, 0, "STREET");
            _dataAdapter.UpdateCommand.Parameters.Add("p_HOME_NUMBER", OracleDbType.Varchar2, 0, "HOME_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_HOUSING", OracleDbType.Varchar2, 0, "HOUSING");
            _dataAdapter.UpdateCommand.Parameters.Add("p_FLAT_NUMBER", OracleDbType.Varchar2, 0, "FLAT_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TAX_PERCENT", OracleDbType.Decimal, 0, "TAX_PERCENT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SOCIAL_NOTIFY_NUMBER", OracleDbType.Varchar2, 0, "SOCIAL_NOTIFY_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SOCIAL_NOTIFY_DATE", OracleDbType.Date, 0, "SOCIAL_NOTIFY_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SOCIAL_NOTIFY_CODE", OracleDbType.Varchar2, 0, "SOCIAL_NOTIFY_CODE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ESTATE_NOTIFY_NUMBER", OracleDbType.Varchar2, 0, "ESTATE_NOTIFY_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ESTATE_NOTIFY_DATE", OracleDbType.Date, 0, "ESTATE_NOTIFY_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ESTATE_NOTIFY_CODE", OracleDbType.Varchar2, 0, "ESTATE_NOTIFY_CODE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ADVANCE_PAY_NOTIFY_NUMBER", OracleDbType.Varchar2, 0, "ADVANCE_PAY_NOTIFY_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ADVANCE_PAY_NOTIFY_DATE", OracleDbType.Date, 0, "ADVANCE_PAY_NOTIFY_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_ADVANCE_PAY_NOTIFY_CODE", OracleDbType.Varchar2, 0, "ADVANCE_PAY_NOTIFY_CODE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TAX_COMPANY_ID", OracleDbType.Decimal, 0, "TAX_COMPANY_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOCUM_NUMBER", OracleDbType.Decimal, 0, "DOCUM_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOCUM_SIGN", OracleDbType.Decimal, 0, "DOCUM_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CORR_NUMBER", OracleDbType.Decimal, 0, "CORR_NUMBER");
            _dataAdapter.UpdateCommand.Parameters.Add("p_LOCK_SIGN", OracleDbType.Decimal, 0, "LOCK_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_RETENT_TAX", OracleDbType.Decimal, 0, "RETENT_TAX");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFERED_TAX", OracleDbType.Decimal, 0, "TRANSFERED_TAX");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CODE_COUNTRY", OracleDbType.Varchar2, 0, "CODE_COUNTRY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CALCED_TAX", OracleDbType.Decimal, 0, "CALCED_TAX");
            _dataAdapter.UpdateCommand.Parameters.Add("p_DOCUM_DATE", OracleDbType.Date, 0, "DOCUM_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_UPDATE_DATE", OracleDbType.Date, 0, "UPDATE_DATE");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.TAX_EMP_DOCUM_DELETE(p_TAX_EMP_DOCUM_ID => :p_TAX_EMP_DOCUM_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_TAX_EMP_DOCUM_ID", OracleDbType.Decimal, 0, "TAX_EMP_DOCUM_ID");
	}
		
	#endregion
    }


    [Table(Name="TYPE_ACCOUNT"), SchemaName("SALARY")]
    public partial class TypeAccount : RowEntityBase
    {
        #region Class Members
        [Column(Name="TYPE_ACCOUNT_ID")]
        public Decimal? TypeAccountID
        {
            get
            {
        		return GetDataRowField<Decimal?>("TYPE_ACCOUNT_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeAccountID);
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
        		return GetDataRowField<Decimal?>("NEED_TYPE_BANK");
                //return this.GetDataRowField<Decimal?>(() => NeedTypeBank);
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
        		return GetDataRowField<String>("NAME_TYPE_ACCOUNT");
                //return this.GetDataRowField<String>(() => NameTypeAccount);
            }
            set
            {
                UpdateDataRow<String>(() => NameTypeAccount, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.TYPE_ACCOUNT_UPDATE(p_TYPE_ACCOUNT_ID=>:p_TYPE_ACCOUNT_ID, p_NEED_TYPE_BANK=>:p_NEED_TYPE_BANK, p_NAME_TYPE_ACCOUNT=>:p_NAME_TYPE_ACCOUNT);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_TYPE_ACCOUNT_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_NEED_TYPE_BANK", OracleDbType.Decimal, 0, "NEED_TYPE_BANK");
            _dataAdapter.InsertCommand.Parameters.Add("p_NAME_TYPE_ACCOUNT", OracleDbType.Varchar2, 0, "NAME_TYPE_ACCOUNT");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.TYPE_ACCOUNT_UPDATE(p_TYPE_ACCOUNT_ID=>:p_TYPE_ACCOUNT_ID, p_NEED_TYPE_BANK=>:p_NEED_TYPE_BANK, p_NAME_TYPE_ACCOUNT=>:p_NAME_TYPE_ACCOUNT);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_ACCOUNT_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_TYPE_ACCOUNT_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_NEED_TYPE_BANK", OracleDbType.Decimal, 0, "NEED_TYPE_BANK");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NAME_TYPE_ACCOUNT", OracleDbType.Varchar2, 0, "NAME_TYPE_ACCOUNT");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.TYPE_ACCOUNT_DELETE(p_TYPE_ACCOUNT_ID => :p_TYPE_ACCOUNT_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_TYPE_ACCOUNT_ID", OracleDbType.Decimal, 0, "TYPE_ACCOUNT_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Типы банков
    /// </summary>

    [Table(Name="TYPE_BANK"), SchemaName("SALARY")]
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
        		return GetDataRowField<Decimal?>("TYPE_BANK_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeBankID);
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
        		return GetDataRowField<String>("BANK_ADDRESS");
                //return this.GetDataRowField<String>(() => BankAddress);
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
        		return GetDataRowField<DateTime?>("CONTRACT_DATE");
                //return this.GetDataRowField<DateTime?>(() => ContractDate);
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
        		return GetDataRowField<String>("CONTRACT_CODE");
                //return this.GetDataRowField<String>(() => ContractCode);
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
        		return GetDataRowField<String>("PPC");
                //return this.GetDataRowField<String>(() => Ppc);
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
        		return GetDataRowField<Decimal?>("CUSTOM_SIGN");
                //return this.GetDataRowField<Decimal?>(() => CustomSign);
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
        		return GetDataRowField<String>("BANK_IDENT_CODE");
                //return this.GetDataRowField<String>(() => BankIdentCode);
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
        		return GetDataRowField<String>("TRN");
                //return this.GetDataRowField<String>(() => Trn);
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
        		return GetDataRowField<String>("BRANCH_BANK");
                //return this.GetDataRowField<String>(() => BranchBank);
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
        		return GetDataRowField<String>("BANK_OFFICE");
                //return this.GetDataRowField<String>(() => BankOffice);
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
        		return GetDataRowField<String>("CORRESPONDENT_ACCOUNT");
                //return this.GetDataRowField<String>(() => CorrespondentAccount);
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
        		return GetDataRowField<String>("BANK_NAME");
                //return this.GetDataRowField<String>(() => BankName);
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
        		return GetDataRowField<String>("CURRENT_ACCOUNT");
                //return this.GetDataRowField<String>(() => CurrentAccount);
            }
            set
            {
                UpdateDataRow<String>(() => CurrentAccount, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.TYPE_BANK_UPDATE(p_TYPE_BANK_ID=>:p_TYPE_BANK_ID, p_BANK_ADDRESS=>:p_BANK_ADDRESS, p_CONTRACT_DATE=>:p_CONTRACT_DATE, p_CONTRACT_CODE=>:p_CONTRACT_CODE, p_PPC=>:p_PPC, p_CUSTOM_SIGN=>:p_CUSTOM_SIGN, p_BANK_IDENT_CODE=>:p_BANK_IDENT_CODE, p_TRN=>:p_TRN, p_BRANCH_BANK=>:p_BRANCH_BANK, p_BANK_OFFICE=>:p_BANK_OFFICE, p_CORRESPONDENT_ACCOUNT=>:p_CORRESPONDENT_ACCOUNT, p_BANK_NAME=>:p_BANK_NAME, p_CURRENT_ACCOUNT=>:p_CURRENT_ACCOUNT);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_TYPE_BANK_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_BANK_ADDRESS", OracleDbType.Varchar2, 0, "BANK_ADDRESS");
            _dataAdapter.InsertCommand.Parameters.Add("p_CONTRACT_DATE", OracleDbType.Date, 0, "CONTRACT_DATE");
            _dataAdapter.InsertCommand.Parameters.Add("p_CONTRACT_CODE", OracleDbType.Varchar2, 0, "CONTRACT_CODE");
            _dataAdapter.InsertCommand.Parameters.Add("p_PPC", OracleDbType.Varchar2, 0, "PPC");
            _dataAdapter.InsertCommand.Parameters.Add("p_CUSTOM_SIGN", OracleDbType.Decimal, 0, "CUSTOM_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_BANK_IDENT_CODE", OracleDbType.Varchar2, 0, "BANK_IDENT_CODE");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRN", OracleDbType.Varchar2, 0, "TRN");
            _dataAdapter.InsertCommand.Parameters.Add("p_BRANCH_BANK", OracleDbType.Varchar2, 0, "BRANCH_BANK");
            _dataAdapter.InsertCommand.Parameters.Add("p_BANK_OFFICE", OracleDbType.Varchar2, 0, "BANK_OFFICE");
            _dataAdapter.InsertCommand.Parameters.Add("p_CORRESPONDENT_ACCOUNT", OracleDbType.Varchar2, 0, "CORRESPONDENT_ACCOUNT");
            _dataAdapter.InsertCommand.Parameters.Add("p_BANK_NAME", OracleDbType.Varchar2, 0, "BANK_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_CURRENT_ACCOUNT", OracleDbType.Varchar2, 0, "CURRENT_ACCOUNT");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.TYPE_BANK_UPDATE(p_TYPE_BANK_ID=>:p_TYPE_BANK_ID, p_BANK_ADDRESS=>:p_BANK_ADDRESS, p_CONTRACT_DATE=>:p_CONTRACT_DATE, p_CONTRACT_CODE=>:p_CONTRACT_CODE, p_PPC=>:p_PPC, p_CUSTOM_SIGN=>:p_CUSTOM_SIGN, p_BANK_IDENT_CODE=>:p_BANK_IDENT_CODE, p_TRN=>:p_TRN, p_BRANCH_BANK=>:p_BRANCH_BANK, p_BANK_OFFICE=>:p_BANK_OFFICE, p_CORRESPONDENT_ACCOUNT=>:p_CORRESPONDENT_ACCOUNT, p_BANK_NAME=>:p_BANK_NAME, p_CURRENT_ACCOUNT=>:p_CURRENT_ACCOUNT);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_TYPE_BANK_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_BANK_ADDRESS", OracleDbType.Varchar2, 0, "BANK_ADDRESS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CONTRACT_DATE", OracleDbType.Date, 0, "CONTRACT_DATE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CONTRACT_CODE", OracleDbType.Varchar2, 0, "CONTRACT_CODE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PPC", OracleDbType.Varchar2, 0, "PPC");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CUSTOM_SIGN", OracleDbType.Decimal, 0, "CUSTOM_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BANK_IDENT_CODE", OracleDbType.Varchar2, 0, "BANK_IDENT_CODE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRN", OracleDbType.Varchar2, 0, "TRN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BRANCH_BANK", OracleDbType.Varchar2, 0, "BRANCH_BANK");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BANK_OFFICE", OracleDbType.Varchar2, 0, "BANK_OFFICE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CORRESPONDENT_ACCOUNT", OracleDbType.Varchar2, 0, "CORRESPONDENT_ACCOUNT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BANK_NAME", OracleDbType.Varchar2, 0, "BANK_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_CURRENT_ACCOUNT", OracleDbType.Varchar2, 0, "CURRENT_ACCOUNT");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.TYPE_BANK_DELETE(p_TYPE_BANK_ID => :p_TYPE_BANK_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_TYPE_BANK_ID", OracleDbType.Decimal, 0, "TYPE_BANK_ID");
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
        		return GetDataRowField<Decimal?>("TYPE_CARTULARY_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeCartularyID);
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
        		return GetDataRowField<String>("TYPE_CARTULARY_NAME");
                //return this.GetDataRowField<String>(() => TypeCartularyName);
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
        		return GetDataRowField<Decimal?>("SORT_CARTULARY");
                //return this.GetDataRowField<Decimal?>(() => SortCartulary);
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
        		return GetDataRowField<Decimal?>("SIGN_NO_ACCOUNT");
                //return this.GetDataRowField<Decimal?>(() => SignNoAccount);
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
        		return GetDataRowField<Decimal?>("TYPE_GROUP_CARTULARY_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeGroupCartularyID);
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
        		return GetDataRowField<String>("PAY_CODE");
                //return this.GetDataRowField<String>(() => PayCode);
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
        		return GetDataRowField<String>("TRANSFER_MESSAGE");
                //return this.GetDataRowField<String>(() => TransferMessage);
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
        		return GetDataRowField<String>("BALANCE_ACCOUNT");
                //return this.GetDataRowField<String>(() => BalanceAccount);
            }
            set
            {
                UpdateDataRow<String>(() => BalanceAccount, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.TYPE_CARTULARY_UPDATE(p_TYPE_CARTULARY_ID=>:p_TYPE_CARTULARY_ID, p_TYPE_CARTULARY_NAME=>:p_TYPE_CARTULARY_NAME, p_SORT_CARTULARY=>:p_SORT_CARTULARY, p_SIGN_NO_ACCOUNT=>:p_SIGN_NO_ACCOUNT, p_TYPE_GROUP_CARTULARY_ID=>:p_TYPE_GROUP_CARTULARY_ID, p_PAY_CODE=>:p_PAY_CODE, p_TRANSFER_MESSAGE=>:p_TRANSFER_MESSAGE, p_BALANCE_ACCOUNT=>:p_BALANCE_ACCOUNT);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TYPE_CARTULARY_ID", OracleDbType.Decimal, 0, "TYPE_CARTULARY_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_TYPE_CARTULARY_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_CARTULARY_NAME", OracleDbType.Varchar2, 0, "TYPE_CARTULARY_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_SORT_CARTULARY", OracleDbType.Decimal, 0, "SORT_CARTULARY");
            _dataAdapter.InsertCommand.Parameters.Add("p_SIGN_NO_ACCOUNT", OracleDbType.Decimal, 0, "SIGN_NO_ACCOUNT");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_GROUP_CARTULARY_ID", OracleDbType.Decimal, 0, "TYPE_GROUP_CARTULARY_ID");
            _dataAdapter.InsertCommand.Parameters.Add("p_PAY_CODE", OracleDbType.Varchar2, 0, "PAY_CODE");
            _dataAdapter.InsertCommand.Parameters.Add("p_TRANSFER_MESSAGE", OracleDbType.Varchar2, 0, "TRANSFER_MESSAGE");
            _dataAdapter.InsertCommand.Parameters.Add("p_BALANCE_ACCOUNT", OracleDbType.Varchar2, 0, "BALANCE_ACCOUNT");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.TYPE_CARTULARY_UPDATE(p_TYPE_CARTULARY_ID=>:p_TYPE_CARTULARY_ID, p_TYPE_CARTULARY_NAME=>:p_TYPE_CARTULARY_NAME, p_SORT_CARTULARY=>:p_SORT_CARTULARY, p_SIGN_NO_ACCOUNT=>:p_SIGN_NO_ACCOUNT, p_TYPE_GROUP_CARTULARY_ID=>:p_TYPE_GROUP_CARTULARY_ID, p_PAY_CODE=>:p_PAY_CODE, p_TRANSFER_MESSAGE=>:p_TRANSFER_MESSAGE, p_BALANCE_ACCOUNT=>:p_BALANCE_ACCOUNT);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_CARTULARY_ID", OracleDbType.Decimal, 0, "TYPE_CARTULARY_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_TYPE_CARTULARY_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_CARTULARY_NAME", OracleDbType.Varchar2, 0, "TYPE_CARTULARY_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SORT_CARTULARY", OracleDbType.Decimal, 0, "SORT_CARTULARY");
            _dataAdapter.UpdateCommand.Parameters.Add("p_SIGN_NO_ACCOUNT", OracleDbType.Decimal, 0, "SIGN_NO_ACCOUNT");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_GROUP_CARTULARY_ID", OracleDbType.Decimal, 0, "TYPE_GROUP_CARTULARY_ID");
            _dataAdapter.UpdateCommand.Parameters.Add("p_PAY_CODE", OracleDbType.Varchar2, 0, "PAY_CODE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TRANSFER_MESSAGE", OracleDbType.Varchar2, 0, "TRANSFER_MESSAGE");
            _dataAdapter.UpdateCommand.Parameters.Add("p_BALANCE_ACCOUNT", OracleDbType.Varchar2, 0, "BALANCE_ACCOUNT");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.TYPE_CARTULARY_DELETE(p_TYPE_CARTULARY_ID => :p_TYPE_CARTULARY_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_TYPE_CARTULARY_ID", OracleDbType.Decimal, 0, "TYPE_CARTULARY_ID");
	}
		
	#endregion
    }

    /// <summary>
    /// Таблица типов документа начисления
    /// </summary>

    [Table(Name="TYPE_SAL_DOCUM"), SchemaName("SALARY")]
    public partial class TypeSalDocum : RowEntityBase
    {
        #region Class Members
        [Column(Name="TYPE_SAL_DOCUM_ID", CanBeNull=false)]
        public Decimal? TypeSalDocumID
        {
            get
            {
        		return GetDataRowField<Decimal?>("TYPE_SAL_DOCUM_ID");
                //return this.GetDataRowField<Decimal?>(() => TypeSalDocumID);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => TypeSalDocumID, value);
            }
        }
        [Column(Name="TYPE_SAL_DOC_NAME")]
        public String TypeSalDocName
        {
            get
            {
        		return GetDataRowField<String>("TYPE_SAL_DOC_NAME");
                //return this.GetDataRowField<String>(() => TypeSalDocName);
            }
            set
            {
                UpdateDataRow<String>(() => TypeSalDocName, value);
            }
        }
        /// <summary>
        /// Признак отпуска
        /// </summary>
        [Column(Name="VAC_SIGN", CanBeNull=false)]
        public Decimal? VacSign
        {
            get
            {
        		return GetDataRowField<Decimal?>("VAC_SIGN");
                //return this.GetDataRowField<Decimal?>(() => VacSign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => VacSign, value);
            }
        }
        /// <summary>
        /// Требуется ли указывать период документа
        /// </summary>
        [Column(Name="NEED_DOC_PERIOD", CanBeNull=false)]
        public Decimal? NeedDocPeriod
        {
            get
            {
        		return GetDataRowField<Decimal?>("NEED_DOC_PERIOD");
                //return this.GetDataRowField<Decimal?>(() => NeedDocPeriod);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => NeedDocPeriod, value);
            }
        }
        /// <summary>
        /// Кол-во дней, ограничивающих расчет первых шифров оплат
        /// </summary>
        [Column(Name="COUNT_RESTRICT_DAYS", CanBeNull=false)]
        public Decimal? CountRestrictDays
        {
            get
            {
        		return GetDataRowField<Decimal?>("COUNT_RESTRICT_DAYS");
                //return this.GetDataRowField<Decimal?>(() => CountRestrictDays);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => CountRestrictDays, value);
            }
        }
        /// <summary>
        /// Требуется ли период для расчета документа
        /// </summary>
        [Column(Name="NEED_CALC_PERIOD")]
        public Decimal? NeedCalcPeriod
        {
            get
            {
        		return GetDataRowField<Decimal?>("NEED_CALC_PERIOD");
                //return this.GetDataRowField<Decimal?>(() => NeedCalcPeriod);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => NeedCalcPeriod, value);
            }
        }
        /// <summary>
        /// Признак исключения дней в годах расчета периодов
        /// </summary>
        [Column(Name="EXCLUDE_DAY_SIGN")]
        public Decimal? ExcludeDaySign
        {
            get
            {
        		return GetDataRowField<Decimal?>("EXCLUDE_DAY_SIGN");
                //return this.GetDataRowField<Decimal?>(() => ExcludeDaySign);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => ExcludeDaySign, value);
            }
        }
        /// <summary>
        /// Описание типа, используется для показа в справке расчета
        /// </summary>
        [Column(Name="TYPE_DESCRIPTION")]
        public String TypeDescription
        {
            get
            {
        		return GetDataRowField<String>("TYPE_DESCRIPTION");
                //return this.GetDataRowField<String>(() => TypeDescription);
            }
            set
            {
                UpdateDataRow<String>(() => TypeDescription, value);
            }
        }
        /// <summary>
        /// Доступен ли заказ для документа (выбор)
        /// </summary>
        [Column(Name="IS_ORDER_ENABLED")]
        public Decimal? IsOrderEnabled
        {
            get
            {
        		return GetDataRowField<Decimal?>("IS_ORDER_ENABLED");
                //return this.GetDataRowField<Decimal?>(() => IsOrderEnabled);
            }
            set
            {
                UpdateDataRow<Decimal?>(() => IsOrderEnabled, value);
            }
        }
        #endregion
    	
    	#region Adapter region contructor
    	public override void InitializeAdapter()
    	{
    		DataAdapter = new OracleDataAdapter();
        	_dataAdapter.InsertCommand = new OracleCommand(@"BEGIN SALARY.TYPE_SAL_DOCUM_UPDATE(p_TYPE_SAL_DOCUM_ID=>:p_TYPE_SAL_DOCUM_ID, p_TYPE_SAL_DOC_NAME=>:p_TYPE_SAL_DOC_NAME, p_VAC_SIGN=>:p_VAC_SIGN, p_NEED_DOC_PERIOD=>:p_NEED_DOC_PERIOD, p_COUNT_RESTRICT_DAYS=>:p_COUNT_RESTRICT_DAYS, p_NEED_CALC_PERIOD=>:p_NEED_CALC_PERIOD, p_EXCLUDE_DAY_SIGN=>:p_EXCLUDE_DAY_SIGN, p_TYPE_DESCRIPTION=>:p_TYPE_DESCRIPTION, p_IS_ORDER_ENABLED=>:p_IS_ORDER_ENABLED);END;", CurConnect);
        	_dataAdapter.InsertCommand.BindByName = true;
        	_dataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.InsertCommand.Parameters.Add("p_TYPE_SAL_DOCUM_ID", OracleDbType.Decimal, 0, "TYPE_SAL_DOCUM_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.InsertCommand.Parameters["p_TYPE_SAL_DOCUM_ID"].DbType = DbType.Decimal;
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_SAL_DOC_NAME", OracleDbType.Varchar2, 0, "TYPE_SAL_DOC_NAME");
            _dataAdapter.InsertCommand.Parameters.Add("p_VAC_SIGN", OracleDbType.Decimal, 0, "VAC_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_NEED_DOC_PERIOD", OracleDbType.Decimal, 0, "NEED_DOC_PERIOD");
            _dataAdapter.InsertCommand.Parameters.Add("p_COUNT_RESTRICT_DAYS", OracleDbType.Decimal, 0, "COUNT_RESTRICT_DAYS");
            _dataAdapter.InsertCommand.Parameters.Add("p_NEED_CALC_PERIOD", OracleDbType.Decimal, 0, "NEED_CALC_PERIOD");
            _dataAdapter.InsertCommand.Parameters.Add("p_EXCLUDE_DAY_SIGN", OracleDbType.Decimal, 0, "EXCLUDE_DAY_SIGN");
            _dataAdapter.InsertCommand.Parameters.Add("p_TYPE_DESCRIPTION", OracleDbType.Varchar2, 0, "TYPE_DESCRIPTION");
            _dataAdapter.InsertCommand.Parameters.Add("p_IS_ORDER_ENABLED", OracleDbType.Decimal, 0, "IS_ORDER_ENABLED");

        	_dataAdapter.UpdateCommand = new OracleCommand(@"BEGIN SALARY.TYPE_SAL_DOCUM_UPDATE(p_TYPE_SAL_DOCUM_ID=>:p_TYPE_SAL_DOCUM_ID, p_TYPE_SAL_DOC_NAME=>:p_TYPE_SAL_DOC_NAME, p_VAC_SIGN=>:p_VAC_SIGN, p_NEED_DOC_PERIOD=>:p_NEED_DOC_PERIOD, p_COUNT_RESTRICT_DAYS=>:p_COUNT_RESTRICT_DAYS, p_NEED_CALC_PERIOD=>:p_NEED_CALC_PERIOD, p_EXCLUDE_DAY_SIGN=>:p_EXCLUDE_DAY_SIGN, p_TYPE_DESCRIPTION=>:p_TYPE_DESCRIPTION, p_IS_ORDER_ENABLED=>:p_IS_ORDER_ENABLED);END;", CurConnect);
        	_dataAdapter.UpdateCommand.BindByName = true;
        	_dataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        	_dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_SAL_DOCUM_ID", OracleDbType.Decimal, 0, "TYPE_SAL_DOCUM_ID").Direction = ParameterDirection.InputOutput;
            _dataAdapter.UpdateCommand.Parameters["p_TYPE_SAL_DOCUM_ID"].DbType = DbType.Decimal;
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_SAL_DOC_NAME", OracleDbType.Varchar2, 0, "TYPE_SAL_DOC_NAME");
            _dataAdapter.UpdateCommand.Parameters.Add("p_VAC_SIGN", OracleDbType.Decimal, 0, "VAC_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NEED_DOC_PERIOD", OracleDbType.Decimal, 0, "NEED_DOC_PERIOD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_COUNT_RESTRICT_DAYS", OracleDbType.Decimal, 0, "COUNT_RESTRICT_DAYS");
            _dataAdapter.UpdateCommand.Parameters.Add("p_NEED_CALC_PERIOD", OracleDbType.Decimal, 0, "NEED_CALC_PERIOD");
            _dataAdapter.UpdateCommand.Parameters.Add("p_EXCLUDE_DAY_SIGN", OracleDbType.Decimal, 0, "EXCLUDE_DAY_SIGN");
            _dataAdapter.UpdateCommand.Parameters.Add("p_TYPE_DESCRIPTION", OracleDbType.Varchar2, 0, "TYPE_DESCRIPTION");
            _dataAdapter.UpdateCommand.Parameters.Add("p_IS_ORDER_ENABLED", OracleDbType.Decimal, 0, "IS_ORDER_ENABLED");

        	_dataAdapter.DeleteCommand = new OracleCommand(@"BEGIN SALARY.TYPE_SAL_DOCUM_DELETE(p_TYPE_SAL_DOCUM_ID => :p_TYPE_SAL_DOCUM_ID);END;", CurConnect);
        	_dataAdapter.DeleteCommand.BindByName = true;
        	_dataAdapter.DeleteCommand.Parameters.Add("p_TYPE_SAL_DOCUM_ID", OracleDbType.Decimal, 0, "TYPE_SAL_DOCUM_ID");
	}
		
	#endregion
    }

}
