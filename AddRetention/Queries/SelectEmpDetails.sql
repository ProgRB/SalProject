declare
	p_subdiv_id Number := :p_subdiv_id;
begin
	open :c for
	select detail_id, code_degree, order_name, hours, sum_sal, group_master, work_classific
	from
		{1}.VIEW_DETAIL_SUBDIV
		left join {0}.degree using (degree_id)
		join {0}.orders using (order_id)
	where
		transfer_id in (select transfer_id from {0}.transfer where worker_id=(select worker_id from {0}.transfer where transfer_id=:p_transfer_id))
		and work_date = trunc(:p_date,'month')
	order by order_name, group_master, work_classific;	
end;