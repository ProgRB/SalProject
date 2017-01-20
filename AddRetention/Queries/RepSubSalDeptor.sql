select per_num, 
	decode(sign_comb,1,'X','') as sign_comb,
	code_subdiv,
	emp_last_name||' '||substr(emp_first_name,1,1)||'.'||substr(emp_middle_name, 1,1)||'.' as fio1,
    sum(case when type_payment_type_id in (1,4) then sum_sal*TYPE_PAYMENT_SIGN else 0 end) as sum_sal,
    sum(case when type_payment_type_id in (5,9) then sum_sal*TYPE_PAYMENT_SIGN else 0 end) as sum_sal1,
    sum(sum_sal*TYPE_PAYMENT_SIGN) as all_sum
from 
	{1}.view_salary_by_subdiv s
    join {0}.emp using (per_num)
    join {1}.payment_type using (payment_type_id)
	join {1}.TYPE_PAYMENT_TYPE using (type_payment_type_id)
	join apstaff.subdiv using (subdiv_id)
where subdiv_id in (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) and 
	pay_date between trunc(:p_date1, 'month') and add_months(trunc(:p_date2,'month'),1)-1/86400 and 
    type_payment_type_id in (1,4,5,9) 
	and CODE_PAYMENT not in ('287', '287Ê','287Î')
group by per_num, sign_comb, emp_last_name, emp_first_name, emp_middle_name, code_subdiv
having sum(sum_sal*TYPE_PAYMENT_SIGN)<0
order by fio1