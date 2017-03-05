declare
	p_fio varchar(200) := upper(:p_fio);
	p_per_num varchar(100) := :p_per_num;
	p_inn varchar(200) := :p_inn;
begin
	open :c for
	select 
		emp_last_name,
		emp_first_name,
		emp_middle_name,
		per_num,
		DECODE(sign_comb,1,'X','') SIGN_COMB,
		code_subdiv,
		worker_id,
		date_transfer,
		end_transfer as date_end_transfer,
		transfer_id,
		INN
	from 
		apstaff.emp
		left join (select per_num, 
						sign_comb,
						worker_id,
						max(code_subdiv) keep (dense_rank last order by date_transfer) code_subdiv,
						max(date_transfer) date_transfer, 
						max(end_transfer) keep (dense_rank last order by date_transfer) end_transfer,
						max(transfer_id) keep (dense_rank last order by date_transfer) transfer_id
				   from apstaff.transfer_periods
						join apstaff.subdiv using (subdiv_id)
				   group by per_num, sign_comb, worker_id
				  ) using (per_num)
		left join apstaff.per_data using (per_num)
	where (p_per_num is null or per_num=lpad(trim(p_per_num),5,'0'))
		and (p_fio is null or
				emp_last_name like '%'||regexp_substr(p_fio, '\w+',1,1)||'%' 
				and emp_first_name like '%'||regexp_substr(p_fio, '\w+',1,2)||'%' 
				and emp_middle_name like '%'||regexp_substr(p_fio, '\w+',1,3)||'%' 
			)
		and (p_inn is null or inn like '%'||p_inn||'%')
	order by per_num;
end;