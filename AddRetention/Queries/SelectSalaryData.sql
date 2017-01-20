begin
	{1}.GetSalaryData(:p_transfer_id, :p_date, :p_salary_id, :c1);
    open :c2 for 
		select retention_id, NVL(rel_payment_type_id, payment_type_id) payment_type_id, retent_percent, retent_sum, 
			date_start_ret, date_end_ret, CODE_DOC, order_number,
			'удерж. по '||case when nvl(retent_percent,0)=0 then TO_CHAR(retent_sum,'FM999999999.00L',  'NLS_NUMERIC_CHARACTERS = '', ''  NLS_CURRENCY = '' р.'' ') 
							   else TO_CHAR(retent_percent, 'FM999999990.00L',  'NLS_NUMERIC_CHARACTERS = '', ''  NLS_CURRENCY = ''%'' ') end
			||' c '||NVL(to_char(date_start_ret, 'dd/MM/yyyy'), '<нет даты>')||' по '||NVL(to_char(date_end_ret, 'dd/MM/yyyy'), '<нет даты>')||' шифром оплат '||code_payment DISP_EXP 
        from
		(
			select retention_id, payment_type_id, 
                case when sign_individual = 1 then retent_percent  else percent_retent  end retent_percent, 
                case when sign_individual =1 then retent_sum else   sum_retent end retent_sum, 
                date_start_ret, date_end_ret, code_payment,
                NVL( DOC_CODE, '<код не указан>') as CODE_DOC,
                NVL(order_number,1) order_number
            from salary.retention r
                left join salary.salary_doc sd using (salary_doc_id)
                join salary.payment_type using (payment_type_id)
                left join (select payment_type_id, percent_retent, sum_retent, sign_individual from salary.payment_calc_relation join salary.retent_calc_method using (retent_calc_method_id) where sysdate between date_start_calc and date_end_calc) rcm using (payment_type_id)
            where r.transfer_id in (select transfer_id from apstaff.transfer where worker_id=(select worker_id from {0}.transfer where transfer_id=:p_transfer_id))
		 )
         left join (select 10 payment_type_id, 44 rel_payment_type_id from dual union all select 10 payment_type_id, 10 rel_payment_type_id from dual union all select 12 payment_type_id, 44 rel_payment_type_id from dual union all select 12 payment_type_id, 12 rel_payment_type_id from dual
					Union all select 20 payment_type_id, 20 rel_payment_type_id from dual Union all select 20 payment_type_id, 312 rel_payment_type_id from dual
					union all select 88 , 88 from dual union all select 88 , 320 from dual union all select 20 , 324 from dual
					union all select 88 , 326 from dual)
         using (payment_type_id);
	open :c3 for
		select t.transfer_id,
            date_transfer,
            code_subdiv,
            pos_name,
            nvl(special_conditions,'<нет вредности>') HARM_GROUP
		from 
			apstaff.transfer t
			join apstaff.subdiv using (subdiv_id)
			join apstaff.position using (pos_id)
			left join  apstaff.account_data ad on (decode(t.type_transfer_id,3,from_position,t.transfer_id)=ad.transfer_id)
			left join apstaff.privileged_position pp on (pp. privileged_position_id=ad.privileged_position_id)
		where worker_id=(select worker_id from apstaff.transfer where transfer_id=:p_transfer_id)
		order by date_transfer;		
	open :c4 for select * from {1}.VIEW_CARTULARY_PAID where salary_id=:p_salary_id;	
end;