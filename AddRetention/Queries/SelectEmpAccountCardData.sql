declare
	p_per_num varchar2(10);
begin
	select per_num into p_per_num from {0}.transfer where transfer_id=:p_transfer_id;
	open :c1 for select per_num, sign_comb, t.transfer_id, worker_id as ID, code_degree, 
						pos_name, code_subdiv, emp_last_name, emp_first_name, emp_middle_name, 
						emp_sex, emp_birth_date, photo,
						(select min(date_transfer) from {0}.transfer where worker_id=t.worker_id) DATE_TRANSFER,
						(select max(case when type_transfer_id=3 then date_transfer else null end) from {0}.transfer where worker_id=t.worker_id) END_TRANSFER
				from 
					{0}.transfer  t
					join {0}.emp using (per_num)
					join {0}.subdiv using (subdiv_id)
					join {0}.degree using (degree_id)
					join {0}.position using (pos_id)
				where t.transfer_id=:p_transfer_id;
	open :c6 for select * from {0}.transfer t left join (select subdiv_id, code_subdiv, subdiv_name from {0}.subdiv) using (subdiv_id) 
					left join (select pos_id, pos_name, code_pos from {0}.position) using (pos_id) 
				 where worker_id=(select worker_id from {0}.transfer where transfer_id=:p_transfer_id);
	open :c2 for select (select decode(type_transfer_id, 3, from_position, transfer_id) from {0}.transfer where transfer_id=ad.transfer_id) transfer_id, ad.*, 
				 (select SPECIAL_CONDITIONS from apstaff.PRIVILEGED_POSITION where PRIVILEGED_POSITION_ID =ad.PRIVILEGED_POSITION_ID ) SPECIAL_CONDITIONS,
				 (select CODE_TARIFF_GRID from {0}.tariff_grid where tariff_grid_id=ad.tariff_grid_id) CODE_TARIFF_GRID
				from {0}.account_data ad where transfer_id in 
				(select transfer_id from {0}.transfer 
					where worker_id=(select worker_id from {0}.transfer where transfer_id=:p_transfer_id));
	open :c3 for select PER_NUM, TRIP_SIGN, RETIRER_SIGN, 
						APSTAFF.GET_SIGN_PROFUNION((select worker_id from {0}.transfer where transfer_id=:p_transfer_id), sysdate) SIGN_PROFUNION, 
						SIGN_YOUNG_SPEC, INSURANCE_NUM, SER_MED_POLUS, 
						NUM_MED_POLUS, INN, SOURCE_EMPLOYABILITY_ID 
				 from {0}.per_data where per_num = (select per_num from {0}.transfer where transfer_id=:p_transfer_id);
	open :c4 for select date_start_disc, date_end_disc, sum_discount,  
					dt as pay_date,
					decode(custom_sign, 0, stand_sum_disc, sum_discount) sum_disc,
					code_disc
				from {1}.emp_tax_discount 
					join {1}.type_discount using (type_discount_id)
					join (select add_months(date'2000-01-01', level) dt from dual connect by level<1200) 
						on (dt between date_start_disc and date_end_disc and every_month_sign=1 or date_start_disc = dt) 
				where transfer_id in (select transfer_id from {0}.transfer where (per_num, sign_comb)=
						(select per_num, sign_comb from {0}.transfer where transfer_id=:p_transfer_id));
	open :c5 for 
		select 
			per_num,
			(select name_street  from {0}.street where code_street =s.REG_CODE_STREET) NAME_STREET,
			(select LOCALITY_NAME||' '||abbrev_name  
				from {0}.locality 
				left join {0}.abbrev using (abbrev_id) where CODE_LOCALITY =substr(s.REG_CODE_STREET,1, 11)) NAME_LOCALITY,
			(select name_city||' '||abbrev_name from {0}.city left join {0}.abbrev using (abbrev_id) where code_city=substr(s.REG_CODE_STREET,1, 8)) NAME_CITY,
			(select name_district||' '||abbrev_name from {0}.district left join {0}.abbrev using (abbrev_id) where code_district=substr(s.REG_CODE_STREET,1, 5)) NAME_DISTRICT,
			(select NAME_REGION||' '||abbrev_name from {0}.region left join {0}.abbrev using (ABBREV_ID) where code_region=substr(s.REG_CODE_STREET,1,2)) NAME_REGION,
			substr(s.REG_CODE_STREET,1,2) CODE_REGION,
			replace(REG_HOUSE,' ','') HOUSE,
			REG_BULK "BULK",
			REG_FLAT FLAT,
			REG_POST_CODE POST_CODE,
			1 TYPE_ADDRESS
		from
			{0}.registr s
		where per_num=p_per_num
		union all
			select
				per_num,
				name_street, 
				name_locality,
				name_city,
				name_district,
				Name_region,
				(select code_region from {0}.region where UPPER(name_region)=upper(trim(s.name_region))) code_region,
				replace(REG_HOUSE,' ','') HOUSE,
				REG_BULK "BULK",
				REG_FLAT FLAT,
				REG_POST_CODE POST_CODE,
				3 TYPE_ADDRESS
			from
				{0}.address_none_kladr s
				left join {0}.registr using (per_num)
			where per_num=p_per_num;
end;