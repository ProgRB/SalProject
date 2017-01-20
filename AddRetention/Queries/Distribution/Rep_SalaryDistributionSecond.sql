select
    code_subdiv, 
    order_name, 
    sum(case when TYPE_DISTR_CODE in ('S101', 'S102', 'S105', 'S122', 'S239', 'S300', 'SDOP') then dist_value end) ALL_SUM,
    sum(case when TYPE_DISTR_CODE='S300' then dist_value end) S300,
    sum(case when TYPE_DISTR_CODE='S6905' then dist_value end) S05,
    sum(case when TYPE_DISTR_CODE='S6902' then dist_value end) S02,
    sum(case when TYPE_DISTR_CODE='S6901' then dist_value end) S01,
    sum(case when TYPE_DISTR_CODE='S6904' then dist_value end) S04,
    sum(case when TYPE_DISTR_CODE='S6907' then dist_value end) S07,
    sum(case when TYPE_DISTR_CODE='S6906' then dist_value end) S06,
    sum(case when TYPE_DISTR_CODE in ('S6905', 'S6902', 'S6901', 'S6904', 'S6907', 'S6906') then dist_value end) ALL_SUM2
from
    {1}.salary_distribution
    join {1}.TYPE_SALARY_DISTR_CODE using (TYPE_SALARY_DISTR_CODE_ID)
    join {0}.orders using (order_id)
    join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
where 
    calc_date = trunc(:p_date,'month')
	and TYPE_DIST_SOURCE_ID in (2)
group by code_subdiv, order_name
order by code_subdiv, order_name 
