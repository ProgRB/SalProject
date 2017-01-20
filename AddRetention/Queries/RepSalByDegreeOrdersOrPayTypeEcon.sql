declare
begin
	SALARY.SALARY_REPORT_SUBDIV.Rep_SalaryByDegreeAndOrders(:p_subdiv_id, :p_date_begin, :p_date_end, :c, :p_order_filter);	
end;
