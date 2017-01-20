declare
begin
	open :c1 for select 
					worker_id,
					transfer_id,
					per_num,
					emp_last_name,
					emp_first_name,
					emp_middle_name,
					code_subdiv
			from 
				apstaff.transfer
				join apstaff.emp using (Per_num)
				join apstaff.subdiv using (subdiv_id)
			where transfer_id in (select transfer_id from salary.v_docum_transfer_relation where docum_transfer_id=:p_docum_transfer_id 
								union all select :p_transfer_id from dual);
	open :c2 for select 
			client_account_id,
            number_account,
            number_card,
			per_num,
            bank_name,
			row_number() over (partition by per_num order by dbr nulls last) order_number
        from salary.client_account 
			join (select type_bank_id, bank_name from salary.type_bank) using (type_bank_id)
			join (select transfer_id, per_num from apstaff.transfer) using (transfer_id)
			left join (select client_account_id, max(date_begin_relation) dbr from salary.client_retent_relation 
                        where trunc(sysdate,'month') between date_begin_relation and nvl(date_end_relation, date'3000-01-01') group by client_account_id) using (client_account_id)
		where type_account_id=1
			and per_num in (select per_num from salary.v_docum_transfer_relation where docum_transfer_id=:p_docum_transfer_id
						     union all
							 select per_num from apstaff.transfer where transfer_id=:p_transfer_id)
				
			;
end;
