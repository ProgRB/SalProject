declare
	k number;
begin
	select count(*) into k 
	from  {1}.subdiv_for_close 
	where subdiv_id=:p_subdiv_id and date_closing<trunc(:p_date_end,'month') and app_name='SALARY';
	if k>0 then raise_application_error(-20229, 'Зарплата за указанный период еще не закрыта бухгалтерией'); end if;
	
	if :p_code_pos is null then 
		open :c for
			select
				code_subdiv,
				worker_id,
				per_num,
				FIO,
				pay_date,
				code_pos,
				pos_name,
				sum(hours_table) hours_table,
				sum(sum101) as sum_piece,
				sum(hours106) hours106, 
				sum(sum106) sum106,
				sum(hours124) hours124,
				sum(sum124) SUM124,
				sum(sum104) SUM104,
				sum(sum150) sum150,
				sum(sum_other) sum_other,
				sum(sum239) sum239,
				sum(sum300) sum300    
			from
				{1}.view_salary_piecework
				join (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
				join (select transfer_id, date_transfer from {0}.transfer) using (transfer_id)
			where
				pay_date between trunc(:p_date_begin,'month') and :p_date_end
			group by code_subdiv, worker_id, per_num, fio, pay_date, code_pos, pos_name
			--having per_num='10494'
			order by code_subdiv, code_pos, per_num;
	else
		open :c for
			select
				code_subdiv,
				worker_id,
				per_num,
				FIO,
				pay_date,
				code_pos,
				pos_name,
				sum(hours_table) hours_table,
				sum(sum101) as sum_piece,
				sum(hours106) hours106, 
				sum(sum106) sum106,
				sum(hours124) hours124,
				sum(sum124) SUM124,
				sum(sum104) SUM104,
				sum(sum150) sum150,
				sum(sum_other) sum_other,
				sum(sum239) sum239,
				sum(sum300) sum300    
			from
				{1}.view_salary_piecework
				join (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
				join (select transfer_id, date_transfer from {0}.transfer) using (transfer_id)
			where
				pay_date between trunc(:p_date_begin,'month') and :p_date_end
				and code_pos =:p_code_pos
			group by code_subdiv, worker_id, per_num, fio, pay_date, code_pos, pos_name
			order by code_subdiv, code_pos, per_num;
	end if;
end;