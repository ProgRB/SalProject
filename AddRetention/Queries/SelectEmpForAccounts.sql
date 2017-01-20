select transfer_id, t.per_num, t.worker_id,
    emp_last_name||' '||emp_first_name||' '||emp_middle_name FIO,
    code_subdiv,
    decode(t1.sign_comb,1,'X') sign_comb,
    /*e.photo*/
    (select min(date_transfer) from {0}.transfer where worker_id=t.worker_id) date_hire,
    (select max(decode(type_transfer_id, 3, date_transfer)) from {0}.transfer where worker_id=t.worker_id) date_fire,
	pos_name,
	code_degree
from
(
    select 
        max(transfer_id) keep (dense_rank last order by date_transfer) transfer_id, per_num, worker_id
    from
        (select 
                max(transfer_id) keep (dense_rank last order by date_transfer) transfer_id,
                max(subdiv_id) keep (dense_rank last order by date_transfer) subdiv_id,
                per_num,
                worker_id,
                min(trunc(date_transfer)) date_transfer,
                NVL(max(decode(type_transfer_id, 3, trunc(date_transfer)+86399/86400)), date'3000-01-01') end_transfer
        from {0}.transfer
        group by per_num, worker_id
        ) t
    where trunc(:p_date, 'month')<= end_transfer and add_months(trunc(:p_date,'month'),1)-1/86400>= date_transfer
        and subdiv_id in (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id)
    group by per_num, worker_id
    union
    select tt.transfer_id, tt.per_num, worker_id
    from
        {1}.retention
        join {0}.transfer using (transfer_id)
        join (select worker_id, 
                per_num,
                max(transfer_id) keep (dense_rank last order by date_transfer) transfer_id,
                max(subdiv_id) keep (dense_rank last order by date_transfer) subdiv_id
               from {0}.transfer 
               where date_transfer<=:p_date
               group by worker_id, per_num) tt using (worker_id)
    where 
       payment_type_id in (select payment_type_id from {1}.report_setting join {1}.report_group using (report_group_id) where group_code='0031')
        and trunc(:p_date, 'month')<=nvl(date_end_ret, date'3000-01-01') and add_months(trunc(:p_date,'month'),1)-1/86400>=date_start_ret
        and tt.subdiv_id in (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id)
) t
join {0}.transfer t1 using (transfer_id)
join apstaff.position using (pos_id)
join apstaff.degree using (degree_id)
join {0}.subdiv using (subdiv_id)
join {0}.emp e on (t.per_num=e.per_num)
where (:p_per_num is null or t.per_num=lpad(trim(:p_per_num),5,'0'))
    and (:p_fio is null or upper(emp_last_name) like upper(:p_fio)||'%' or exists(select * from {1}.client_account join {0}.transfer using (transfer_id) where worker_id=t1.worker_id and upper(owner_family) like upper(:p_fio)||'%'))
order by code_subdiv, per_num, sign_comb, date_hire
    
