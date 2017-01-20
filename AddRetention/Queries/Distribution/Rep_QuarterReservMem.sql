select
    code_subdiv, 
    order_name,
	account_value TEXT_MESSAGE,
    sum(dist_value) ALL_SUM
from
    {1}.salary_distribution
    join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id)using (subdiv_id)
    join {0}.orders using (order_id)
	join {1}.TYPE_SALARY_DISTR_CODE using (TYPE_SALARY_DISTR_CODE_ID)
where
    calc_date=trunc(:p_date,'month')
    and TYPE_SALARY_DISTR_CODE_ID in (20, 23)
group by code_subdiv, order_name, account_value
order by code_subdiv, order_name, account_value