select
    trunc(work_date, 'month') pay_date,
    code_subdiv,
    code_degree,
    per_num,
    decode(sign_comb,1, 'X') sign_comb,
    case when emp_last_name is null then null else emp_last_name||' '||substr(emp_first_name,1,1)||'.'||substr(emp_middle_name,1,1)||'.' end fio,
    classific,
    tar_hour hour_price,
    dt.transfer_id,
    code_tariff_grid code_tar_grid,
    work_classific classific_work,
    sum(hours) as hours,
    sum(sum_sal) sum_sal
from
    {1}.VIEW_detail_subdiv dt
    join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
    left join {0}.emp using (per_num)
    join {0}.degree using (degree_id)
    left join 
	(
		select t.transfer_id, code_tariff_grid, tar_date, tariff_end_date, tar_hour, classific from {0}.transfer t 
		join {0}.account_data ad on (decode(type_transfer_id, 3, from_position, t.transfer_id)=ad.transfer_id)
		join {0}.tariff_grid_salary tgs on (ad.tariff_grid_id=tgs.tariff_grid_id and nvl(ad.classific,0)=tgs.tar_classif)
	) t on (dt.transfer_id=t.transfer_id and work_date between tar_date and tariff_end_date)
where
    work_date between trunc(:p_date_begin,'month') and add_months(trunc(:p_date_end,'month'),1)-1/86400
group by trunc(work_date,'month'), code_degree, code_subdiv, code_tariff_grid, per_num, sign_comb, 
    emp_last_name, emp_first_name, emp_middle_name, classific, work_classific, tar_hour, dt.transfer_id
order by pay_date, code_subdiv, per_num, work_classific