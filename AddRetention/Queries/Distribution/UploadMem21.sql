begin
	open :c for
	select 
		lpad('21', 5, '0')||
		ACCOUNT_VALUE||
		lpad('0',3, '0')||
		order_name || 
		code_subdiv||
		to_char(abs(nvl(sum(dist_value), 0))*100, 'FM0000000000000')|| 
		case when nvl(sum(dist_value),0)<0 then '1' else '0' end ||
		lpad('0', 5+6+6, '0')||
		case when order_name like '7621%' then '0323050464  ' else lpad(' ', 12, ' ') end || -- IKodF
		case when order_name like '7621%' and account_value like '6906%' then '1' else '0' end||--pz
		case when order_name like '7621%' then '29' else '25' end || to_char(:p_date,'mmyyyy')||
		lpad('0', 13, '0')||
		lpad('0', 8, '0')
	from {1}.salary_distribution
		join (select code_subdiv, subdiv_id from {0}.SUBDIV start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id and code_subdiv not in ('039', '139', '145')) using (subdiv_id)
		join {0}.orders using (order_id)
		join {1}.type_operation using (type_operation_id)
		join {1}.TYPE_SALARY_DISTR_CODE using (TYPE_SALARY_DISTR_CODE_ID)
	where calc_date= trunc(:p_date, 'month') and account_value is not null
		and type_dist_source_id !=6
	group by code_subdiv, order_name, account_value
	having sum(dist_value)!=0
	order by code_subdiv, order_name, account_value;
end;