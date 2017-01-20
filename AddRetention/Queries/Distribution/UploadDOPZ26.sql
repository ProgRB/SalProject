begin
	open :c for
	q'[
		with function ff(k number, cnt number, dec_part  number := 2) return varchar is
			begin
				return to_char(nvl(k,0)*power(10, dec_part), 'S'||lpad('0', cnt, '0'));
			end;
			select 
				lpad(code_subdiv,4, '0')||
				to_char(sort_number, 'FM00')|| 
				lpad(order_name, 14, '0') || 
				code_operation||
				ff(sum(case when TYPE_SALARY_DISTR_CODE_ID=1 then dist_value else 0 end), 7, 1) ||-- H101,
				ff(sum(case when  TYPE_SALARY_DISTR_CODE_ID=2 then dist_value else 0 end), 11, 2) ||-- S101,
				ff(sum(case when  TYPE_SALARY_DISTR_CODE_ID=3 then dist_value else 0 end), 7, 1) ||-- H102,
				ff(sum(case when  TYPE_SALARY_DISTR_CODE_ID in (4,5,6,7,8, 9) then dist_value else 0 end), 11, 2) || -- S102,
				ff(0, 11, 2) ||-- S105,
				ff(0, 11, 2) ||-- S122,
				ff(sum(case when  TYPE_SALARY_DISTR_CODE_ID=8 then dist_value else 0 end), 11, 2) ||-- S300,
				ff(0, 11, 2) ||-- S239,
				ff(0, 11, 2) ||--SDOP,
				ff(sum(case when type_salary_distr_code_id in (2, 4, 5, 6, 7, 8, 9) then dist_value else 0 end), 11, 2) ||-- SALL,
				ff(sum(case when  TYPE_SALARY_DISTR_CODE_ID=13 then dist_value else 0 end), 11, 2) || --SST1,
				ff(sum(case when  TYPE_SALARY_DISTR_CODE_ID=14 then dist_value else 0 end), 11, 2) || -- SST2,
				ff(sum(case when  TYPE_SALARY_DISTR_CODE_ID=17 then dist_value else 0 end), 11, 2) ||-- SST5,
				ff(sum(case when  TYPE_SALARY_DISTR_CODE_ID=15 then dist_value else 0 end), 11, 2) || --SST3,
				ff(sum(case when  TYPE_SALARY_DISTR_CODE_ID=16 then dist_value else 0 end), 11, 2) || --SST4,
				ff(sum(case when  TYPE_SALARY_DISTR_CODE_ID=18 then dist_value else 0 end), 11, 2)  --SST6
			from {1}.salary_distribution
				join {0}.SUBDIV using (subdiv_id)
				join {0}.orders using (order_id)
				join {1}.type_operation using (type_operation_id)
			where calc_date= trunc(:p_date, 'month')
				and type_dist_source_id in (2)
			group by code_subdiv, order_name, sort_number, code_operation
			order by code_subdiv, order_name]' using :p_date;
end;