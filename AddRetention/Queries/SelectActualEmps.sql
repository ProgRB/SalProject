declare
	p_fio varchar(200) := :p_fio;
begin
	open :c for
	select 
		worker_id,
		min(date_transfer) date_transfer,
		case when max(sign_cur_work)=1 then null else  max(date_transfer) end date_end_transfer,
		max(transfer_id) KEEP (dense_rank last order by date_transfer) transfer_id,
		emp_last_name,
		emp_first_name,
		emp_middle_name,
		per_num,
		DECODE(sign_comb,1,'X','') SIGN_COMB,
		max(code_subdiv) KEEP(dense_rank last order by date_transfer) code_subdiv,
		null as INN
	from 
		{0}.transfer t
		join {0}.emp using (per_num)
		join {0}.subdiv using (subdiv_id), 
		(select * from (select regexp_substr(upper(p_fio), '(\w)+',1,level) a, level lv from dual connect by level<4) pivot ( max(a) for lv in (1,2,3)) ) tb
	where :p_per_num is not null and per_num=lpad(trim(:p_per_num),5,'0') or  
		trim(p_fio) is not null 
		and ("1" is null or emp_last_name like '%'||"1"||'%') 
		and ("2" is null or emp_first_name like '%'||"2"||'%') 
		and ("3" is null or emp_middle_name like '%'||"3"||'%')
	group by worker_id, emp_last_name, emp_first_name, emp_middle_name, per_num, sign_comb
	order by per_num;
end;