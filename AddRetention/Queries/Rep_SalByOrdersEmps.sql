select 
	nvl(order_name,'(пустой)') code_order,
    per_num,
    emp_last_name||' '||substr(emp_first_name,1,1)||'. '||substr(emp_middle_name,1,1)||'.' FIO1,
    code_payment,
    code_degree,
    sum(sum_sal) SUM_SAL,
	sum(hours) hours,
	sum(days) days
from
    salary.view_salary_by_subdiv
    join salary.payment_type using (payment_type_id)
    join apstaff.emp using (per_num)
    left join apstaff.degree using (degree_id)
    left join apstaff.orders using (order_id)
where
    trunc(pay_date, 'month') = trunc(:p_date,'month')
    and subdiv_id in (select subdiv_id from apstaff.subdiv start with subdiv_id=:p_subdiv_id  connect by prior subdiv_id=parent_id)
    and type_payment_type_id=1
group by order_name, per_num, emp_last_name, emp_first_name, emp_middle_name, code_degree, code_payment