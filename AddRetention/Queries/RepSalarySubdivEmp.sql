select 
    worker_id,
    per_num,
    fio,
    order_name,
    sum(case when code_payment in ('101Í', '102') then sum_sal else null end) SUM_SAL,
    sum(case when code_payment in ('104', '105') then sum_sal else null end) SUM104,
    sum(case when code_payment in ('239') then sum_sal else null end) SUM239,
    sum(case when code_payment in ('310', '320', '330') then sum_sal else null end) SUM300,
    sum(case when code_payment not in ('101Í', '102', '105', '104', '239','310', '320', '330') then sum_sal else null end) SUM_OTHER,
    SUM(sum_sal) ALL_SUM
from 
    {1}.view_salary_econ_short
    left join {0}.orders using (order_id)
    join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
where pay_date between trunc(:p_date_begin, 'month') and add_months(trunc(:p_date_end,'month'), 1)-1/86400
	and type_payment_type_id=1
	and degree_id member of :p_degree_filter
group by worker_id, per_num, fio, order_name
order by per_num, order_name