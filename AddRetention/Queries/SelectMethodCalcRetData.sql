declare 
begin
	open :c1 for select * from {1}.RETENT_CALC_METHOD where RETENT_CALC_METHOD_ID=:p_RETENT_CALC_METHOD_ID;
	open :c2 for select * from {1}.TYPE_RETENT_CALC_SUM;
	open :c3 for select * from {1}.TYPE_GROUP_RETENTION;
	open :c4 for select * from {1}.TYPE_REVENUE;
end;