declare
begin
	open :c1 for select * from {0}.emp where per_num= :p_per_num;
	open :c2 for select * from {0}.per_data where per_num= :p_per_num;
	open :c3 for select * from {0}.passport where per_num= :p_per_num;
end;