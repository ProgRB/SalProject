with st as
(
	select 
		code_degree, 
		order_name as code_order,
		case when code_payment in ('239', '310', '320', '330') then null else sum(sum_sal) end SUM_SAL,  
		case
			when code_payment in ('239', '310', '320', '330') then null
			when consider_type_id =2 then sum(days) 
			else round(sum(case when code_payment='101Í' then null else hours end),2) end as HOURS, 
		CODE_PAYMENT, 
		sum(round(case when code_payment in ('239') then sum_sal else ZONE_ADD end,2)) ZONE_ADD, 
		sum(round(case when code_payment in ('310', '320', '330') then sum_sal else EXP_ADD end,2)) EXP_ADD, 
		round(sum(nvl(round(sum_sal,2),0)+nvl(round(zone_add,2),0)+nvl(round(exp_add,2),0)),2) as ALL_SUM
	from 
		{1}.view_salary_subdiv_short
		join {1}.payment_type using (payment_type_id)
		left join {0}.orders using (order_id)
		left join {0}.degree using (degree_id)
		join (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
	where pay_date between trunc(:p_date_begin,'month') and add_months(trunc(:p_date_end,'month'),1)-1/86400
		and type_payment_type_id member of :p_type_payment_type_ids and (code_payment not in ('239','310','320','330') or account_add_sign=1)
	group by order_name, code_payment, code_degree, consider_type_id
)
select 
	case when code_payment in ('242', '245', '247') then 1 else 0 end RNUMBER,
	code_degree, 
	code_order,
	sum(sum_sal) SUM_SAL,  
	sum(hours) as HOURS, 
	CODE_PAYMENT, 
	nullif(sum(zone_add),0) ZONE_ADD, 
	nullif(sum(exp_add), 0) EXP_ADD, 
	sum(all_sum) as ALL_SUM
from 
	(
		select 
			code_degree,
			code_order,
			code_payment,
			case 
				when code_degree='08' and code_payment='124' then round(sum_sal*0.5,2) 
				when code_degree='08' and code_payment='106' then round(sum_sal*0.67,2) else sum_sal 
			end sum_sal,
			case 
				when code_degree='08' and code_payment='124' then round(HOURS*0.5,2) 
				when code_degree='08' and code_payment='106' then round(HOURS*0.67,2)
				else HOURS end HOURS,
			case 
				when code_degree='08' and code_payment='124' then round(ZONE_ADD*0.5,2)
				when code_degree='08' and code_payment='106' then round(ZONE_ADD*0.67,2)
				else ZONE_ADD end ZONE_ADD,
			case 
				when code_degree='08' and code_payment='124' then round(EXP_ADD*0.5,2)
				when code_degree='08' and code_payment='106' then round(EXP_ADD*0.67,2)
				else EXP_ADD end EXP_ADD,
			case 
				when code_degree='08' and code_payment='124' then round(ALL_SUM*0.5,2) 
				when code_degree='08' and code_payment='106' then round(ALL_SUM*0.67,2)
				else ALL_SUM end ALL_SUM
		from
			st
	union all
		select 
			code_degree,
			code_order,
			'102' as code_payment,
			case 
				when  code_payment='124' then sum_sal-round(sum_sal*0.5,2) 
				when  code_payment='106' then sum_sal-round(sum_sal*0.67,2) else sum_sal 
			end sum_sal,
			case 
				when  code_payment='124' then hours-round(HOURS*0.5,2) 
				when  code_payment='106' then hours-round(HOURS*0.67,2)
				else HOURS end HOURS,
			case 
				when  code_payment='124' then zone_add-round(ZONE_ADD*0.5,2)
				when  code_payment='106' then zone_add-round(ZONE_ADD*0.67,2)
				else ZONE_ADD end ZONE_ADD,
			case 
				when  code_payment='124' then exp_add-round(EXP_ADD*0.5,2)
				when  code_payment='106' then exp_add-round(EXP_ADD*0.67,2)
				else EXP_ADD end EXP_ADD,
			case 
				when  code_payment='124' then all_sum-round(ALL_SUM*0.5,2) 
				when  code_payment='106' then all_sum-round(ALL_SUM*0.67,2)
				else ALL_SUM end ALL_SUM
		from
			st
		where code_degree='08' and code_payment in ('124', '106')
	)
group by code_degree,  code_order, CODE_PAYMENT