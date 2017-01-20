select
    per_num,
    emp_last_name||' '||substr(emp_first_name,1,1)||'.'||substr(emp_middle_name,1,1)||'.' FIO1,
    date_start_disc as DATE_BEGIN,
    date_end_disc as DATE_END,
    code_disc as CODE_DISCOUNT,
    code_docum DOC_CODE,
    case when custom_sign>0 then sum_discount else stand_sum_disc end sum_sal,
    (select max(code_subdiv) keep (dense_rank last order by date_transfer) 
	 from {0}.transfer join {0}.subdiv using (subdiv_id) where worker_id=t.worker_id) code_subdiv
from
    {1}.emp_tax_discount
    join {0}.transfer t using (transfer_id)
    join {0}.emp using (per_num)
    join {1}.type_discount using (type_discount_id)
where
    date_start_disc<=add_months(trunc(:p_date_end, 'month'), 1)-1/86400
    and date_end_disc>=trunc(:p_date_begin,'month')
    and type_discount_id member of :p_type_discount_ids
    and (select max(subdiv_id) keep (dense_rank last order by date_transfer) 
		 from {0}.transfer where worker_id=t.worker_id) 
		 in (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id)
order by per_num, fio1