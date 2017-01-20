select
	code_subdiv,
	per_num,
	sum_sal,
	salary_id, 
	code_payment
from
	{1}.view_salary_by_subdiv
	join {1}.payment_type using (payment_type_id)
	join (select subdiv_id, code_subdiv from {0}.subdiv start with code_subdiv=decode(:p_code_subdiv,'000','0',:p_code_subdiv) connect by prior subdiv_id=parent_id) using (subdiv_id)
where
	trunc(pay_date,'month')=trunc(:p_date,'month')
	and code_payment=:p_payment
	