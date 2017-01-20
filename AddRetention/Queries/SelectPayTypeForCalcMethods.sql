select RETENT_SETTING_ID, CODE_PAYMENT, payment_type_id, NOT_RET_SUM, NAME_PAYMENT, 
	DECODE(USE_FOR_CALC,1,'X','') USE_FOR_CALC, 
	DECODE(USE_FOR_RELATION,1,'X', '') USE_FOR_RELATION, 
	DECODE(USE_FOR_OTHER_CALC, 1, 'X') USE_FOR_OTHER_CALC
from {1}.PAYMENT_TYPE pt
	join (select * from {1}.RETENT_CALC_SETTING
		where RETENT_CALC_METHOD_ID = :p_RETENT_CALC_METHOD_ID) rs 
		on (rs.SAL_PAY_TYPE_ID=pt.PAYMENT_TYPE_ID)
order by CODE_PAYMENT