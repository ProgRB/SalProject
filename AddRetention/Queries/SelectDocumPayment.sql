select
	pay_date,
	code_payment,
	sum_sal,
	code_degree,
	order_name,
	code_subdiv,
	days
from
	{1}.view_salary_docum
	join {0}.subdiv using (subdiv_id)
	join {1}.payment_type using (payment_type_id)
	left join {0}.degree using (degree_id)
	left join {0}.orders using (order_id)
where
	salary_docum_id=:p_salary_docum_id
	