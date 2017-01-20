select
	SALARY_DISTR_CUSTOM_ID, SUM_SAL, CALC_DATE, ORDER_NUMBER, HOURS, ORDER_ID, ORDER_NAME, SUBDIV_ID, CODE_SUBDIV, DEGREE_ID, CODE_DEGREE
from
	{1}.SALARY_DISTR_CUSTOM
	join (select subdiv_id, code_subdiv from {0}.SUBDIV start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
	left join {0}.DEGREE using (DEGREE_ID)
	join {0}.ORDERS using (ORDER_ID)
where
	calc_date=trunc(:p_date,'month')
