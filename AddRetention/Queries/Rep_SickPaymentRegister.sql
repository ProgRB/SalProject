select 
    code_subdiv, 
    per_num,
    emp_last_name||' '||substr(emp_first_name,1,1)||'.'||substr(emp_middle_name,1,1)||'.' FIO1,
    code_doc as doc_code,
    doc_begin date_begin, 
    doc_end date_end,
    case when payment_sum=(select round(size_minimum_wage*24*1.2/730, 2) from {1}.minimum_wage where doc_begin between DATE_BEGIN_MW and DATE_END_MW) then round(PAYMENT_SUM/1.2,2) else payment_sum end SUM_PAID,
    (select listagg(pay_value,'/') WITHIN GROUP (order by count_days) from {1}.SALARY_DOCUM_PAY_CHANGE where salary_docum_id=sd.salary_docum_id) TEXT_MESSAGE,
    sum(case when gr.pt_id is null then days else null end) days,
    sum(case when gr.pt_id is null then sum_sal  else null end) sum_sal1,
    sum(case when gr.pt_id is not null then days else null end) cnt,
    sum(case when gr.pt_id is not null then sum_sal  else null end) sum_sal2,
    basic_doc_sign as rnumber,
    nvl(NAME_DOC, 'Перерасчет') TYPE_MESSAGE
from
    (select distinct payment_type_id 
        from {1}.TYPE_SAL_DOCUM 
            join {1}.type_docum_pay_calc tdpc using (type_sal_docum_id)
            join {1}.payment_type using (payment_type_id)  
       where TYPE_SAL_DOCUM_ID member of :p_type_sal_docum_ids and code_payment!='235') tdpc    
    join (select salary_id, pay_date, sum_sal, days, subdiv_id, payment_type_id, code_subdiv, transfer_id  from {1}.VIEW_SALARY_BY_SUBDIV
                    join (select subdiv_id, code_subdiv from apstaff.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id
							and code_subdiv!='039') using (subdiv_id)
					join {1}.payment_type using (payment_type_id)
					left join {0}.orders using (order_id)
					where code_payment not in ('236','237') or order_name is null or order_name not like '6906%'
                ) s on (tdpc.payment_type_id=s.payment_type_id)
    join {1}.payment_type pt on (s.payment_type_id=pt.payment_type_id) 
    join (select worker_id, transfer_id, per_num from apstaff.transfer) using (transfer_id)
    join apstaff.emp using (per_num)
    left join (select payment_type_id pt_id from 
                    (select report_group_id from {1}.report_group start with group_code='3006' connect by prior report_group_id=parent_group_id)
                    join {1}.report_setting using (report_group_id)) gr on  (s.payment_type_id=gr.pt_id)
    left join {1}.SALARY_DOC_RELATION sdr on (s.salary_id=sdr.salary_id)
    left join {1}.salary_docum sd on (sd.salary_docum_id=sdr.salary_docum_id)
    left join {1}.salary_docum_detail sdd on (sd.salary_docum_id=sdd.salary_docum_id and s.payment_type_id=sdd.payment_type_id  and sdd.payment_sum!=0)
where
    pay_date between trunc(:p_date_begin, 'month') and add_months(trunc(:p_date_end,'month'), 1)-1/86400
group by sd.salary_docum_id,
     code_subdiv, 
    per_num,
    emp_last_name, emp_first_name, emp_middle_name,
    code_doc,
    doc_begin, 
    doc_end,
    sd.count_restrict_days,
    payment_sum,
    name_doc, 
    date_doc,
    basic_doc_sign
