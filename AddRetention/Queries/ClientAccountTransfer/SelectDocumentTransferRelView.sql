select
	code_subdiv,
	per_num,
	emp_last_name||' '||substr(emp_first_name,1,1)||'.'||substr(emp_middle_name,1,1)||'.' fio,
	pay_date,
	sum_sal,
	check_date,
	fin_plan_code,
	destination
from
	salary.v_docum_transfer_relation
	join apstaff.subdiv using (subdiv_id)
	join apstaff.emp using (per_num)
where
	docum_transfer_id=:p_docum_transfer_id
