declare
	table_doc_list_ids salary.number_collection_type := salary.number_collection_type(5,7);
begin
	open :c1 for 
		select  * from {1}.SALARY_DOCUM s where salary_docum_id=:p_salary_docum_id;
	open :c2 for select tsd.*, 
				(select max(TYPE_CALC_PERIOD_ID) from {1}.VIEW_PAYMENT join {1}.type_docum_pay_calc using (payment_type_id)  
				 where sysdate between date_start_calc and date_end_calc and type_sal_docum_id=tsd.type_sal_docum_id)
				 TYPE_CALC_PERIOD_ID
				from {1}.TYPE_SAL_DOCUM tsd;
	open :c3 for select EMP_LAST_NAME||' '||EMP_FIRST_NAME||' '||EMP_MIDDLE_NAME FIO, PER_NUM, SUBDIV_ID
				from {0}.transfer join {0}.emp using (per_num) 
			where transfer_id=nvl(:p_transfer_id, (select transfer_id from {1}.salary_docum where salary_docum_id=:p_salary_docum_id));
	open :c4 for select * from {1}.type_docum_pay_calc;
	open :c5 for select * from {1}.SALARY_DOCUM_DETAIL where salary_docum_id=:p_salary_docum_id;
	open :c6 for select * from {1}.salary_docum_period where salary_docum_id=:p_salary_docum_id;
	open :c7 for select * from {0}.doc_list where DOC_LIST_ID member of table_doc_list_ids;
	open :c8 for select rd.*, case when exists(select 1 from {1}.SALARY_DOCUM where reg_doc_id=rd.reg_doc_id) then 'X' else ' ' end DOC_SIGN 
				 from {0}.REG_DOC rd 
					where transfer_id in (select transfer_id from {0}.transfer where worker_id=
								(select worker_id from {0}.transfer where transfer_id=:p_transfer_id))
						  and DOC_LIST_ID member of table_doc_list_ids
						  and doc_begin<add_months(trunc(:p_date,'month'), 3) and doc_end>=add_months(trunc(:p_date,'month'), -12);
	open :c9 for select * from {1}.salary_docum_pay_change where salary_docum_id=:p_salary_docum_id;
end;