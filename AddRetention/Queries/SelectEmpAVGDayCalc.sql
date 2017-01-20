declare
begin
	{1}.Salary_Reports.SelectEmpAVGCalcReport(:p_transfer_id, add_months(trunc(:p_date,'month'),-:p_count_month), trunc(:p_date,'month')-1/86400, :c1, :c2, :c3);
	open :c4 for select emp_last_name||' '||substr(emp_first_name,1,1)||'. '||substr(emp_middle_name,1,1)||'.' FIO, per_num, code_subdiv,
		(select to_number(formula_to_use, '999999D9999', 'NLS_NUMERIC_CHARACTERS=.,') from {1}.payment_calc_relation where payment_type_id= 160 and trunc(:p_date,'month')-1/86400 between date_start_calc and date_end_calc) cf_vac
		from 
	 {0}.transfer join {0}.subdiv using (subdiv_id) join {0}.emp using (per_num) where transfer_id=:p_transfer_id;
end;