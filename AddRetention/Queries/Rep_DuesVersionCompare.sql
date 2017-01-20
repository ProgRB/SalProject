begin
	{1}.SALARY_REPORTS.SelectDuesVersionCompare(:p_subdiv_id, :p_date, trunc(:p_date_begin,'month'), add_months(trunc(:p_date_end,'month'),1)-1/86400, :c);
end;