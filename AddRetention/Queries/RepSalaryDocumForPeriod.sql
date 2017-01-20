select 
    code_subdiv,
    per_num,
    emp_last_name||' '||substr(emp_first_name,1,1)||'.'||substr(emp_middle_name,1,1)||'.' FIO1,
    sum(sum_sal) sum_sal,
    doc_begin DATE_BEGIN,
    doc_end DATE_END,
    TYPE_SAL_DOC_NAME DOC_CODE,
    last_calc_date as rem_date     
from 
    {1}.salary_docum sd
	join (select transfer_id, per_num from {0}.transfer) using (transfer_id)
	join {0}.emp using (per_num)
	join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id 
			connect by prior subdiv_id=parent_id
			and code_subdiv!='039') s on (sd.doc_subdiv_id=s.subdiv_id)
    join {1}.type_sal_docum using (type_sal_docum_id)
	left join (select salary_docum_id, sum_sal from {1}.view_salary_docum vsd 
				where pay_date between trunc(:p_date_begin,'month') and add_months(trunc(:p_date_end, 'month'), 1)-1/86400) using (salary_docum_id)
where 
    trunc(:p_date_begin, 'month')<=doc_end and add_months(trunc(:p_date_end,'month'),1)-1/86400>=doc_begin
	and type_sal_docum_id member of :p_salary_docum_ids
group by code_subdiv, per_num, emp_last_name, emp_first_name, emp_middle_name, doc_begin, doc_end, TYPE_SAL_DOC_NAME, last_calc_date
order by code_subdiv, per_num