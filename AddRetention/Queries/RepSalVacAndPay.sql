select * from 
(
	select 
		code_subdiv, 
		per_num,
		emp_last_name||' '||substr(emp_first_name,1,1)||'. '||substr(emp_middle_name,1,1)||'.' fio1,
		sum(case when type_payment_type_id=1 then sum_sal else 0 end) as SUM_SAL,
		sum(case when type_payment_type_id!=9 then sum_sal else 0 end) as SUM_PAID,
		sdr.salary_docum_id
	from 
		{1}.salary_by_subdiv s
		join {1}.payment_type using (payment_type_id)
		left join salary_doc_relation sdr on (s.salary_id=sdr.salary_id)
	where payment_type_id in ()
	and subdiv_id in (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id)
	and pay_date between trunc(:p_date_begin) and trunc(:p_date_end)+86399/86400
	group by code_subdiv, per_num, emp_last_name, emp_first_name, emp_middle_name, sdr.salary_docum_id
) t
left join 
	(select sum(sum_sal) ret_sum, salary_docum_id, 
		code_payment
	 from {1}.salary_vac join {1}.retention r using (retention_id)
		join {1}.payment_type using (payment_type_id)
	 group by salary_docum_id, code_payment
	) s on (t.salary_docum_id=s.salary_docum_id)
	 
