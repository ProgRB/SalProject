select
    code_subdiv,
    worker_id,
    per_num,
    FIO,
    pay_date,
    max(code_degree) keep (dense_rank last order by date_transfer) code_degree,
    max(code_pos) keep (dense_rank last order by date_transfer) code_pos,
    nullif(max(classific) keep (dense_rank last order by date_transfer),0) classific,
    max(code_tariff_grid) keep (dense_rank last order by date_transfer) code_tariff_grid,
    max(salary) keep (dense_rank last order by date_transfer) salary,
    sum(sum_sal) sum_sal,
    sum(hours_table) hours_table,
    sum(hours_piece) hours_piece,
    sum(hours106) hours106, 
    sum(hours124) hours124,
    sum(hours125) hours125,
    sum(hours222) hours222
from
    {1}.view_salary_econ_short
    join (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
    join (select transfer_id, date_transfer from {0}.transfer) using (transfer_id)
where
    pay_date between trunc(:p_date_begin,'month') and :p_date_end
	and type_payment_type_id=1
group by code_subdiv, worker_id, per_num, fio, pay_date
order by code_subdiv, per_num