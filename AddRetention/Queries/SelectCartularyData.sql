begin
	open :c1 for select * from {1}.CARTULARY c where cartulary_id=:p_cartulary_id;
end;