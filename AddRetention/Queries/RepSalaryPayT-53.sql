select * from 
(
	select 
		emp_last_name||' '||substr(emp_first_name,1,1)||'. '||substr(emp_middle_name,1,1)||'.' FIO1,
		per_num,
		sign_comb,
		cartulary_header as TYPE_COST_NAME,
		sum(sum_sal) sum_sal,
		code_subdiv, 
		row_number() over (partition by code_subdiv order by per_num, sign_comb) order_number,
		count(*) over (partition by code_subdiv) RNUMBER
	from
		{1}.VIEW_SALARY_SUBDIV_TRANSFER vst
		join {0}.transfer using (transfer_id)
		join {0}.EMP using (per_num)
		join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id)s on (vst.subdiv_id=s.subdiv_id)
		join salary.CARTULARY using (CARTULARY_ID)
	where cartulary_id=:p_cartulary_id
	group by per_num, sign_comb, emp_last_name, emp_first_name, emp_middle_name, code_subdiv, cartulary_header
	having sum(sum_sal)!=0
)
--where cnt>1
order by code_subdiv, per_num, sign_comb