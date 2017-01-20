select
    code_subdiv,
    sum_classific,
	count_classific,
    avg_work_classific,
	HOURS_AVG
from
    (select
        nvl(code_subdiv, 'По заводу') CODE_SUBDIV,
        sum(work_classific*hours) avg_work_classific,
		sum(hours) hours_avg
     from
        {1}.view_detail_subdiv
        join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
        join (select date_transfer, t.transfer_id, worker_id, pos_id from {0}.transfer t) using (transfer_id)
        join {0}.position using (pos_id)
     where work_date between trunc(:p_date, 'month') and add_months(trunc(:p_date, 'month'),1)-1/86400
        and work_classific>0
        --and per_num<'7'
        and (code_subdiv!='017' or code_pos!='11540')
		and degree_id member of :p_degree_ids
    group by code_subdiv, trunc(work_date,'month') 
	)
    join 
    (select
            nvl(code_subdiv, 'По заводу') CODE_SUBDIV,
            sum(decode(classific, 0, 1, classific)) sum_classific,
			count(*) count_classific
        from
            {1}.view_salary_econ_short
            join (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
        where pay_date between trunc(:p_date,'month') and add_months(trunc(:p_date,'month'), 1)-1/86400
            and code_payment='101Т'
            --and hours_table!=0
            --and classific is not null
            and (code_subdiv!='017' or code_pos!='11540')
			and degree_id member of :p_degree_ids
        group by code_subdiv
    ) using (code_subdiv)
order by code_subdiv