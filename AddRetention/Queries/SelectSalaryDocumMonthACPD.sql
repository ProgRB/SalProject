select salary_docum_id,
    per_num,
    emp_last_name||' '||substr(emp_first_name,1,1)||'.'||substr(emp_middle_name,1,1)||'.' FIO,
    DOC_BEGIN,
    doc_end,
	DATE_DOC,
    type_sal_doc_name type_doc_name
from
    {1}.salary_docum sd
    join {0}.transfer using (transfer_id)
    join {0}.emp using (per_num)
    join {1}.type_sal_docum tsd on (sd.type_sal_docum_id=tsd.type_sal_docum_id)
where
    doc_subdiv_id in (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id)
    and sd.type_sal_docum_id in (2,4,5,6,7,8,9)
        and 
            (date_doc between trunc(:p_date,'month') and add_months(trunc(:p_date,'month'),1)-1/86400
            or 
             doc_begin<=add_months(trunc(:p_date,'month'),1)-1/86400 and doc_end>=trunc(:p_date,'month')
             and exists( select 1 from {1}.view_payment join {1}.TYPE_DOCUM_PAY_CALC using (payment_type_id) where type_sal_docum_id=sd.type_sal_docum_id and sysdate between date_start_calc and date_end_calc 
                                and type_calc_period_id=3)
            )
order by per_num, doc_begin