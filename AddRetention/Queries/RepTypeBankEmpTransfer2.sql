select per_num, 
	emp_last_name||' '||substr(emp_first_name,1,1)||'.'||substr(emp_middle_name,1,1)||'.' FIO,
    listagg(bank_name, '; ') within group (order by bank_name) bank_name
from
(select distinct per_num, emp_last_name, emp_first_name, emp_middle_name, bank_name
     from 
    (
        select worker_id, max(transfer_id) keep (dense_rank last order by date_transfer) as transfer_id, per_num
        from
            ( select 
                worker_id,  
                transfer_id,
                trunc(date_transfer) date_transfer,
                subdiv_id, 
                decode(type_transfer_id, 3, trunc(date_transfer)+1/86400, lead(trunc(date_transfer)-1/86400, 1, date'3000-01-01') over (partition by worker_id order by date_transfer)) end_transfer,
                per_num
             from {0}.transfer
            )
            join (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
        where trunc(:p_date_begin,'month')<=end_transfer and add_months(trunc(:p_date_end,'month'),1)-1/86400>=date_transfer
        group by worker_id, per_num
    ) 
    join 
        (select 
            worker_id, retention_id,
            order_number
         from 
            {1}.retention
            join {0}.transfer using (transfer_id)
            join {1}.payment_type using (payment_type_id)
         where trunc(:p_date_begin,'month')<=nvl(date_end_ret, date'3000-01-01') and add_months(trunc(:p_date_end,'month'),1)-1/86400>=date_start_ret
            and code_payment='287'
        ) using (worker_id)
    join {0}.emp using (per_num)
    join ( select retention_id, max(client_account_id) keep (dense_rank last order by date_begin_relation) client_account_id
            from  
                {1}.client_retent_relation
            where date_begin_relation<=add_months(trunc(:p_date_end,'month'),1)-1/86400
            group by retention_id
          ) using (retention_id)
    join {1}.client_account using (client_account_id)
    join {1}.type_bank using (type_bank_id)
)
group by per_num, emp_last_name, emp_first_name, emp_middle_name
order by per_num