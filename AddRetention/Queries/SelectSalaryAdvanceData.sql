begin
	open :c1 for select * from {1}.salary_advance where salary_advance_id = :p_salary_advance_id;
	open :c2 for select emp_last_name||' '||substr(emp_first_name,1,1)||'.'||substr(emp_middle_name,1,1)||'.' FIO, per_num,
			subdiv_id
		from {0}.transfer join {0}.emp using (per_num) 
		where transfer_id=:p_transfer_id;
end;