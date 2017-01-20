select 
	SALARY_ADD_CORRELATION_ID, 
	TYPE_OPERATION_ID, 
	HOURS, 
	code_subdiv,
	SUM_SAL, 
	code_payment, 
	order_name, 
	code_degree,
	CALC_DATE
from 
	{1}.SALARY_ADD_CORRELATION
	join {0}.orders using (order_id)
	join {1}.payment_type using (payment_type_id)
	join apstaff.degree using (degree_id)
	join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
where
	calc_date=trunc(:p_date,'month')