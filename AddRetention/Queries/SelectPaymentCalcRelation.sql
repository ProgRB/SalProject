select PAYMENT_CALC_RELATION_ID,
		DATE_START_CALC,
        DATE_END_CALC,
        FORMULA_TO_USE,
        IS_ALLOW_PAST_EDIT,
        ORDER_NAME,
        METHOD_NAME
from {1}.payment_calc_relation pcr
	join {1}.retent_calc_method using (retent_calc_method_id)
	left join {0}.ORDERS o on (o.order_id=pcr.def_order_id)
where payment_type_id=:p_payment_type_id