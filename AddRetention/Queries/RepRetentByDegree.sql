select
	code_subdiv,
    code_payment PAY_TYPE_ID,
	round(sum(greatest(sum_sal,0)),2) as sum_sal,
	round(sum(least(sum_sal,0)),2) as sum_sal1,
	round(sum(sum_sal),2) as all_sum
from {1}.view_salary_by_subdiv s
	join {1}.payment_type pt on (s.payment_type_id=pt.payment_type_id)
    left join {0}.subdiv sb on (s.subdiv_id=sb.subdiv_id)
where type_payment_type_id in (5,9) and pay_date between trunc(:p_date1,'month') and add_months(trunc(:p_date2,'month'),1)-1/86400
	and code_payment not in ('287', '287Ê')
	and s.subdiv_id in (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id)
group by code_subdiv, code_payment