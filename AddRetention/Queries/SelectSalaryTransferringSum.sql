declare
begin
	{1}.SALARY_REPORTS.SelectEmpTransferingSum(:p_transfer_id, date'2013-01-01', date'3000-01-01', :c);
end;