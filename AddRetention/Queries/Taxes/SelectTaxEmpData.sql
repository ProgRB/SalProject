declare
	addr  apstaff.EMP_FULL_ADDRESS_TYPE;
begin
	open :c1 for select * from apstaff.emp where per_num= :p_per_num;
	open :c2 for select * from apstaff.per_data where per_num= :p_per_num;
	open :c3 for select * from apstaff.passport where per_num= :p_per_num;
	if :p_per_num is not null then
		addr := apstaff.EMP_FULL_ADDRESS_TYPE(:p_per_num);
	end if;
	open :c4 for select addr.country COUNTRY, addr.region REGION, addr.code_region CODE_REGION, addr.district DISTRICT, 
				addr.city CITY, addr.locality LOCALITY, addr.street STREET, 
                addr.house HOUSE, addr.bulk BULK, addr.flat FLAT, addr.post_index POST_INDEX from dual;
end;