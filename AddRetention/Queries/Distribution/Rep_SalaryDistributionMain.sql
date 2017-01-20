select
    code_subdiv, 
    order_name, 
    type_operation_id, 
	TYPE_DIST_SOURCE_ID as TYPE_SOURCE_ID,
	sort_number as order_number,
    sum(case when TYPE_DISTR_CODE='H101' then dist_value end) H101,
    sum(case when TYPE_DISTR_CODE='S101' then dist_value end) S101,
    sum(case when TYPE_DISTR_CODE='H102' then dist_value end) H102,
    sum(case when TYPE_DISTR_CODE='S102' then dist_value end) S102,
    sum(case when TYPE_DISTR_CODE='S105' then dist_value end) S105,
    sum(case when TYPE_DISTR_CODE='S122' then dist_value end) S122,
    sum(case when TYPE_DISTR_CODE='S239' then dist_value end) S239,
    sum(case when TYPE_DISTR_CODE='S300' then dist_value end) S300,
    sum(case when TYPE_DISTR_CODE='SDOP' then dist_value end) SOTHER,
    sum(case when TYPE_DISTR_CODE in ('S101', 'S102', 'S105', 'S122', 'S239', 'S300', 'SDOP') then dist_value end) ALL_SUM,
    sum(case when TYPE_DISTR_CODE like 'S69%' then dist_value end) ALL_SUM2
from
    {1}.salary_distribution
    join {1}.TYPE_SALARY_DISTR_CODE using (TYPE_SALARY_DISTR_CODE_ID)
    join {0}.orders using (order_id)
    join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
	join {1}.type_operation using (type_operation_id)
where 
    calc_date = trunc(:p_date,'month')
	and TYPE_DIST_SOURCE_ID  in (1, 4)
group by code_subdiv, order_name, type_operation_id, sort_number, TYPE_DIST_SOURCE_ID
order by code_subdiv, sort_number, order_name, type_operation_id 
