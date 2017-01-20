declare
begin
	{1}.Salary_Reports.SelectEmpMissionCalcReport(:p_transfer_id, trunc(:p_date,'month')-1/86400, :c1, :c2, :c3);
	open :c4 for select emp_last_name||' '||substr(emp_first_name,1,1)||'. '||substr(emp_middle_name,1,1)||'.' FIO, per_num, code_subdiv
		from 
	 {0}.transfer join {0}.subdiv using (subdiv_id) join {0}.emp using (per_num) where transfer_id=:p_transfer_id;
end;