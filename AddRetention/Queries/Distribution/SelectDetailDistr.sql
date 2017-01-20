select
	code_subdiv, order_name, mak as type_operation_id, code_degree, sum(hours) as hours, sum(sum_sal) as sum_sal
from
	{1}.VIEW_DETAIL_SUBDIV
	join {0}.ORDERS using (ORDER_ID)
	join {0}.DEGREE using (degree_id)
	join (select subdiv_id, code_subdiv from {0}.SUBDIV start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
where
	work_date between trunc(:p_date,'month') and add_months(trunc(:p_date,'month'), 1)-1/86400
group by code_subdiv, order_name, mak, code_degree
