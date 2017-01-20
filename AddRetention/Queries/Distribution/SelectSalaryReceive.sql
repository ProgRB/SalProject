with 
 v_subdiv as (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id)
select
	sal_subdiv_receive_id,
	s1.code_subdiv,
	order_name,
	hours,
	sum_sal,
	subdiv_sal,
	s2.code_subdiv receive_code_subdiv
from
	{1}.SAL_SUBDIV_RECEIVE ss
	join {0}.orders using (order_id)
	join {0}.subdiv s1 on (ss.subdiv_id=s1.subdiv_id)
	join {0}.subdiv s2 on (ss.receive_subdiv_id=s2.subdiv_id)
where
	rec_date = trunc(:p_date,'month')
	and
	(
		ss.subdiv_id in (select subdiv_id from v_subdiv)
		or ss.receive_subdiv_id in (select subdiv_id from v_subdiv)
	)

