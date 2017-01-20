declare
	p salary.number_collection_type := :p_code_distr_ids;
begin
	p := salary.number_collection_type(19, 20, 21, 23);
	open :c for
	select 
		lpad(case when substr(account_value,1,4)='9603' then '198' else '197' end, 5, '0')||
		ACCOUNT_VALUE||
		lpad('0',3, '0')||
		order_name || 
		code_subdiv||
		to_char(abs(nvl(sum(dist_value), 0))*100, 'FM0000000000000')|| 
		case when nvl(sum(dist_value),0)<0 then '1' else '0' end ||
		lpad('0', 5+6+6, '0')||
		lpad(' ', 12, ' ') || -- IKodF
		'0' ||--pz
		'25' || to_char(:p_date,'mmyyyy')||
		lpad('0', 13, '0')||
		lpad('0', 8, '0')
	from {1}.salary_distribution
		join (select code_subdiv, subdiv_id from {0}.SUBDIV start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id and code_subdiv not in ('039', '139', '145')) using (subdiv_id)
		join {0}.orders using (order_id)
		join {1}.TYPE_SALARY_DISTR_CODE using (TYPE_SALARY_DISTR_CODE_ID)
	where calc_date= trunc(:p_date, 'month') and account_value is not null
		and 
		(TYPE_SALARY_DISTR_CODE_ID in (19, 21)
			and order_name not like '9104%'
			and code_subdiv not in ('091')
	     or 
		 TYPE_SALARY_DISTR_CODE_ID in (20, 23)
		)
		and TYPE_SALARY_DISTR_CODE_ID member of p
	group by code_subdiv, order_name, account_value
	having sum(dist_value)!=0
	order by  substr(account_value,1,4), code_subdiv, order_name, account_value;
end;

