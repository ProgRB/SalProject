select
    code_subdiv, 
    order_name,
    sum(decode(TYPE_SALARY_DISTR_CODE_ID, 19, dist_value)) ALL_SUM,
    sum(decode(TYPE_SALARY_DISTR_CODE_ID, 21, dist_value)) ALL_SUM2
from
    {1}.salary_distribution
    join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id)using (subdiv_id)
    join {0}.orders using (order_id)
where
    calc_date=trunc(:p_date,'month')
    and TYPE_SALARY_DISTR_CODE_ID in (19, 21)
	and order_name not like '9104%'
	and code_subdiv not in ('039', '091', '139', '145')
group by code_subdiv, order_name
order by code_subdiv, order_name