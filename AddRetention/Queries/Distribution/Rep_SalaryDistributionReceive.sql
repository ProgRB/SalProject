select
    ss.code_subdiv, 
    order_name,
	ss2.code_subdiv as rec_code_subdiv, 
    sum(case when TYPE_DISTR_CODE in ('S101', 'S102', 'S105', 'S122', 'S239', 'S300', 'SDOP') then dist_value end) ALL_SUM,
    sum(case when TYPE_DISTR_CODE='S6905' then dist_value end) S05,
    sum(case when TYPE_DISTR_CODE='S6902' then dist_value end) S02,
    sum(case when TYPE_DISTR_CODE='S6901' then dist_value end) S01,
    sum(case when TYPE_DISTR_CODE='S6904' then dist_value end) S04,
    sum(case when TYPE_DISTR_CODE='S6907' then dist_value end) S07,
    sum(case when TYPE_DISTR_CODE='S6906' then dist_value end) S06, 
    sum(case when TYPE_DISTR_CODE like 'S69%' then dist_value end) ALL_SUM2
from
    {1}.salary_distribution s
    join {1}.TYPE_SALARY_DISTR_CODE using (TYPE_SALARY_DISTR_CODE_ID)
    join {0}.orders using (order_id)
    join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) ss on (s.subdiv_id=ss.subdiv_id)
	left join apstaff.subdiv ss2 on (s.receive_subdiv_id=ss2.subdiv_id)
where 
    calc_date = trunc(:p_date,'month')
	and TYPE_DIST_SOURCE_ID = 3
group by ss.code_subdiv, ss2.code_subdiv, order_name
order by ss.code_subdiv, ss2.code_subdiv, order_name 
