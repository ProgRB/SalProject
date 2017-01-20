declare
	k number;
begin
	select count(*) into k 
	from  {1}.subdiv_for_close 
	where subdiv_id=:p_subdiv_id and date_closing<trunc(:p_date_end,'month') and app_name='SALARY';
	if k>0 then raise_application_error(-20229, 'Зарплата за указанный период еще не закрыта бухгалтерией'); end if;
	if (:p_group_master is null or :p_group_master='*') then
		open :c for
			with v_salary as 
			(
				select 
					nvl(max(group_master) keep (dense_rank last order by case when group_master is null then 0 else 1 end, date_transfer), 
						(
							select max(name_group_master) keep (dense_rank last order by begin_group)
							from apstaff.emp_group_master join apstaff.transfer tt using (transfer_id) 
							where tt.worker_id=vsp.worker_id and begin_group<add_months(pay_date,1)
						)) group_master,
					pay_date,
					worker_id,
					per_num,
					fio,
					code_subdiv,
					sum(hours_table) hours_table,
					sum(hours106) hours106,
					sum(hours124) hours124,
					sum(hours101N) hours_piece,
					sum(sum101) sum_piece,
					sum(nvl(sum101,0)+nvl(sum106,0)+nvl(sum124,0)+nvl(sum104,0)+nvl(sum150,0)+nvl(sum239,0)+nvl(sum300,0)+nvl(sum_other,0)) all_sum
				from
					{1}.view_salary_piecework vsp
					join (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
					join (select transfer_id, date_transfer from {0}.transfer) using (transfer_id)
				where pay_date between :p_date_begin and :p_date_end
				group by pay_date,worker_id, per_num, fio, code_subdiv
			)
			select 
				t.group_master,
				pay_date,
				worker_id,
				t.per_num,
				t.fio,
				code_subdiv,
				hours_table,
				hours106,
				hours124,
				hours_piece,
				sum_piece,
				all_sum
			from
				(select add_months(trunc(:p_date_begin,'month'),level-1) pay_date from dual connect by level<=round(months_between(last_day(:p_date_end), :p_date_begin)))
				join (select distinct worker_id, per_num, fio, group_master, code_subdiv from  v_salary) t on (1=1)
				left join v_salary using (pay_date, worker_id, code_subdiv)
			order by pay_date, group_master, t.per_num;
	else
		open :c for
			with v_salary as 
			(
				select 
					nvl(max(group_master) keep (dense_rank last order by case when group_master is null then 0 else 1 end, date_transfer), 
						(
							select max(name_group_master) keep (dense_rank last order by begin_group)
							from apstaff.emp_group_master join apstaff.transfer tt using (transfer_id) 
							where tt.worker_id=vsp.worker_id and begin_group<add_months(pay_date,1)
						)) group_master,
					pay_date,
					worker_id,
					per_num,
					fio,
					code_subdiv,
					sum(hours_table) hours_table,
					sum(hours106) hours106,
					sum(hours124) hours124,
					sum(hours101N) hours_piece,
					sum(sum101) sum_piece,
					sum(nvl(sum101,0)+nvl(sum106,0)+nvl(sum124,0)+nvl(sum104,0)+nvl(sum150,0)+nvl(sum239,0)+nvl(sum300,0)+nvl(sum_other,0)) all_sum
				from
					{1}.view_salary_piecework vsp
					join (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
					join (select transfer_id, date_transfer from {0}.transfer) using (transfer_id)
				where pay_date between :p_date_begin and :p_date_end
					and group_master like replace(:p_group_master,'*', '%')
				group by pay_date,worker_id, per_num, fio, code_subdiv
			)
			select 
				t.group_master,
				pay_date,
				worker_id,
				t.per_num,
				code_subdiv,
				t.fio,
				hours_table,
				hours106,
				hours124,
				hours_piece,
				sum_piece,
				all_sum
			from
				(select add_months(trunc(:p_date_begin,'month'),level-1) pay_date from dual connect by level<=round(months_between(last_day(:p_date_end), :p_date_begin)))
				join (select distinct worker_id, per_num, fio, group_master, code_subdiv from  v_salary) t on (1=1)
				left join v_salary using (pay_date, worker_id, code_subdiv)
			order by pay_date, group_master, t.per_num;
	end if;
end;