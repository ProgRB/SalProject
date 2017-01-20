declare
begin
	open :c1 for 
			select 
				RETENT_SETTING_ID, 
				CODE_PAYMENT, 
				payment_type_id as SAL_PAY_TYPE_ID, 
				NOT_RET_SUM, 
				NAME_PAYMENT, 
				NVL(USE_FOR_CALC,0) USE_FOR_CALC, 
				NVL(USE_FOR_RELATION,0) USE_FOR_RELATION, 
				NVL(USE_FOR_OTHER_CALC,0) USE_FOR_OTHER_CALC,
				TYPE_PAYMENT_TYPE_ID
			from {1}.PAYMENT_TYPE pt
				left join (select * from {1}.RETENT_CALC_SETTING
					where RETENT_CALC_METHOD_ID = :p_RETENT_CALC_METHOD_ID) rs 
					on (rs.SAL_PAY_TYPE_ID=pt.PAYMENT_TYPE_ID)
			order by CODE_PAYMENT;
	open :c2 for
		select TYPE_PAYMENT_TYPE_ID, TYPE_PAYMENT_TYPE_NAME from {1}.type_payment_type;
end;