declare
begin
	open :c1 for select * from {1}.RETENTION where retention_id=:p_retention_id;
	open :c2 for select pt.PAYMENT_TYPE_ID, CODE_PAYMENT, NAME_PAYMENT, sign_individual, 
				case when CODE_PAYMENT in ('275','277') then 1 else 0 end as SIGN_ALIMONY,
				case when CODE_PAYMENT IN ('292') then 1 else 0 end SIGN_MANY_ACCOUNT
		from {1}.PAYMENT_TYPE pt
			join (select * from {1}.PAYMENT_CALC_RELATION where sysdate between date_start_calc and date_end_calc) pcr on (pcr.payment_type_id=pt.payment_type_id)
			join  {1}.retent_calc_method rcm on (rcm.retent_calc_method_id=pcr.retent_calc_method_id)
		where TYPE_PAYMENT_TYPE_ID=9
			and code_payment not in ('417','287Ê');
	open :c3 for select emp_last_name||' '||emp_first_name||' '||emp_middle_name as fio, per_num from {0}.transfer join {0}.emp using (per_num)
			where transfer_id=:p_transfer_id;
	open :c4 for select * from {1}.CLIENT_RETENT_RELATION where retention_id=:p_retention_id;
	open :c5 for select SALARY_DOC_ID, DOC_NAME, DOC_CODE, DOC_DATE from {1}.SALARY_DOC
			where transfer_id in (select transfer_id from {0}.transfer start with transfer_id=:p_transfer_id 
				connect by NOCYCLE prior transfer_id=from_position or prior from_position=transfer_id);	
	open :c6 for select * from TABLE({1}.Select_retent_Salary(:p_retention_id));
end;