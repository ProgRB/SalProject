begin
	open :c1 for select * from {1}.SALARY_ADD_CORRELATION where SALARY_ADD_CORRELATION_ID=:p_SALARY_ADD_CORRELATION_ID;
	open :c2 for select * from {1}.type_operation order by code_operation;	
end;
