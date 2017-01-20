select
    code_subdiv, 
    per_num,
    emp_last_name||' '||substr(emp_first_name,1,1)||'.'||substr(emp_middle_name, 1,1)||'.' FIO1,
    sum(case when code_payment='801' then sum_sal else null end) SUM_SAL,
    sum(case when code_payment='273' then sum_sal else null end) RET_SUM
from {1}.VIEW_{1}_BY_SUBDIV
    join {1}.payment_type using (payment_type_id)
    join (select subdiv_id,  code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
    join {0}.emp using (per_num)
where 
    trunc(pay_date,'month') between trunc(:p_date_begin,'month') and :p_date_end
    and code_payment in ('273', '801')
group by code_subdiv, per_num, emp_last_name, emp_first_name, emp_middle_name