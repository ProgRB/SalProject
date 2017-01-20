select
    code_payment,
    code_subdiv,
    per_num,
    fio,
	TYPE_DOCUM_NAME,
    sum(sum_sal) sum_sal,
    trunc(pay_date,'month') pay_date
from
    salary.view_salary_by_subdiv
	join salary.payment_type using (payment_type_id)
	join (select per_num, initcap(emp_last_name)||' '||substr(emp_first_name, 1, 1)||'.'||substr(emp_middle_name,1,1)||'.' fio from apstaff.emp) using (per_num)
    join (select subdiv_id, code_subdiv from apstaff.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id and code_subdiv!='039') using (subdiv_id)
	left join (select type_sal_doc_name as TYPE_DOCUM_NAME, salary_id 
				from salary.view_salary_docum 
					join (select salary_docum_id, type_sal_docum_id from salary.salary_docum) using (salary_docum_id)
					join salary.type_sal_docum using (type_sal_docum_id)
				where type_sal_docum_id in (4,5)
			  ) using (salary_id)
where
    pay_date between trunc(:p_date_begin,'year') and add_months(trunc(:p_date_end,'month'),1)-1/86400
    and code_payment in ('243', '270')
group by code_payment, code_subdiv, per_num, fio, trunc(pay_date, 'month'),TYPE_DOCUM_NAME
order by code_payment, code_subdiv, per_num