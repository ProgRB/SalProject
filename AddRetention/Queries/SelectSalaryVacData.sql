begin
	open :c1 for select * from {1}.salary_vac where salary_vac_id=:p_salary_vac_id;
	open :c2 for
		select retention_id, NVL(rel_payment_type_id, payment_type_id) payment_type_id, 
			retent_percent, retent_sum, date_start_ret, date_end_ret, order_number,
			'удерж. по '||case when nvl(retent_percent,0)=0 then TO_CHAR(retent_sum,'FM999999999.00L',  'NLS_NUMERIC_CHARACTERS = '', ''  NLS_CURRENCY = '' р.'' ') else TO_CHAR(retent_percent, 'FM999999990.00L',  'NLS_NUMERIC_CHARACTERS = '', ''  NLS_CURRENCY = ''%'' ') end
			||' c '||NVL(to_char(date_start_ret, 'dd/MM/yyyy'), '<нет даты>')||' по '||NVL(to_char(date_end_ret, 'dd/MM/yyyy'), '<нет даты>')
			||' шифром оплат '||code_payment DISP_EXP 
        from
		(
        select retention_id, payment_type_id, 
                case when sign_individual = 1 then retent_percent  else percent_retent  end retent_percent, 
                case when sign_individual =1 then retent_sum else   sum_retent end retent_sum, 
                date_start_ret, date_end_ret, code_payment,
                NVL(order_number,1) order_number
            from {1}.retention r
                join {1}.payment_type using (payment_type_id)
                left join (select payment_type_id, percent_retent, sum_retent, sign_individual 
						from {1}.payment_calc_relation join {1}.retent_calc_method using (retent_calc_method_id) where sysdate between date_start_calc and date_end_calc) rcm using (payment_type_id)
            where r.transfer_id in (select transfer_id from {0}.transfer where worker_id in 
				(select worker_id from {0}.transfer where transfer_id =(select transfer_id from {1}.salary_docum where salary_docum_id=:p_salary_docum_id )))
		)
         left join (select 10 payment_type_id, 44 rel_payment_type_id from dual union all select 10 payment_type_id, 10 rel_payment_type_id from dual union all select 12 payment_type_id, 44 rel_payment_type_id from dual union all select 12 payment_type_id, 12 rel_payment_type_id from dual)
         using (payment_type_id);
end;