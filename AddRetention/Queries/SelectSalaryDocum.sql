begin
	open :c for 
	select salary_docum_id, 
			t.per_num,
			emp_last_name||' '||substr(emp_first_name, 1,1)||'.  '||substr(emp_middle_name, 1,1)||'.' fio,
			decode(sign_comb,1,'X', ' ') sign_comb,
			(select 'X' from {0}.transfer where worker_id=t.worker_id and type_transfer_id=3) SIGN_FIRED, 
			date_doc,
			code_doc,
			DATE_CLOSE,
			DATE_FORM_DOCUM,
			t.worker_id,
			sd.transfer_id,
			case when 
				type_sal_docum_id!=1 then 10000000
				when
				abs(
                NVL((select sum(sum_sal*type_payment_sign) sum_sal
                    from 
                        {1}.view_salary_by_subdiv 
                        join {1}.salary_doc_relation using (salary_id) 
                        join {1}.payment_type using (payment_type_id)
                        join {1}.type_payment_type using (type_payment_type_id)
                    where salary_docum_id=sd.salary_docum_id),0)
                    +
                    NVL((select sum(sum_sal*type_payment_sign) sum_sal
                       from 
                        {1}.salary_vac
                        join {1}.payment_type using (payment_type_id)
                        join {1}.type_payment_type using (type_payment_type_id)
                    where salary_docum_id=sd.salary_docum_id),0))
                    >
					case when exists(select 1
									from 
										{1}.view_salary_by_subdiv 
										join {1}.salary_doc_relation using (salary_id) 
										join {1}.payment_type using (payment_type_id)
									where salary_docum_id=sd.salary_docum_id and code_payment='274') then 10 else  0 end 
			then 1 
			else 0 END sign_error
	from {1}.salary_docum sd
		left join {0}.transfer t on (t.transfer_id=sd.transfer_id)
		left join {0}.emp e on  (e.per_num=t.per_num)
	where date_doc between trunc(:p_date_begin) and trunc(:p_date_end)+86399/86400 
			and doc_subdiv_id in (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id)
			and type_sal_docum_id=:p_type_sal_docum_id;
end;