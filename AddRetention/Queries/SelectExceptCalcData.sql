begin
	open :c1 for select except_calc_avg_id, 
				(select max(transfer_id) from {0}.transfer where worker_id=(select worker_id from {0}.transfer where transfer_id=ex.transfer_id)) transfer_id,
				date_start,
				date_end,
				base_tariff    
			from {1}.except_calc_avg ex;
	open :c2 for 
		select worker_id, 
			max(transfer_id) keep (dense_rank last order by date_transfer) transfer_id,
			to_char(trunc(min(date_transfer)), 'DD.MM.yyyy') date_hire,
			per_num,
			emp_last_name||' '||substr(emp_first_name, 1,1)||'. '||substr(emp_middle_name,1,1)||'.' FIO,
			decode(sign_comb, 1, 'X', '') SIGN_COMB
		from
			{0}.transfer 
			join {0}.emp using (per_num)
		group by worker_id, per_num, emp_last_name, emp_first_name, emp_middle_name, sign_comb;
end;