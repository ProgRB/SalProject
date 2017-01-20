select * from
(
select 
    emp_last_name||' '||substr(emp_first_name,1,1)||'. '||substr(emp_middle_name,1,1)||'.' FIO1,
    per_num,
    sign_comb,
	null as TYPE_COST_NAME,
    sum(sum_sal) sum_sal,
    code_subdiv, 
	row_number() over (partition by code_subdiv order by per_num, sign_comb) order_number,
	count(*) over (partition by code_subdiv) rnumber
from
    {1}.view_salary_by_subdiv vst
    join {0}.EMP using (per_num)
	join {1}.PAYMENT_TYPE using (payment_Type_id)
    join (select subdiv_id, code_subdiv from {0}.subdiv 
		  start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id and code_subdiv!='039') s using (subdiv_id)
where trunc(pay_date,'month')=trunc(:p_date,'month')
	and code_payment='271'
group by per_num, sign_comb, emp_last_name, emp_first_name, emp_middle_name, code_subdiv
having sum(sum_sal)!=0
)
where rnumber>1
order by code_subdiv, per_num, sign_comb