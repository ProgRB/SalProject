declare
begin
	open :c1 for 
		select * from {1}.retention 
		where transfer_id in (select transfer_id from {0}.transfer where worker_id=
				(select worker_id from {0}.transfer where transfer_id=:p_transfer_id))
		and payment_type_id in (select payment_type_id from {1}.report_setting 
			where report_group_id in (select report_group_id from {1}.report_group where GROUP_CODE='0031'));
	open :c2 for
		select * from {1}.CLIENT_RETENT_RELATION
		where retention_id in (select retention_id from {1}.retention 
				where transfer_id in (select transfer_id from {0}.transfer where worker_id=
						(select worker_id from {0}.transfer where transfer_id=:p_transfer_id))
				and payment_type_id in (select payment_type_id from {1}.report_setting 
					where report_group_id in (select report_group_id from {1}.report_group where GROUP_CODE='0031')));
	open :c3 for
		select * from {1}.client_account left join {1}.type_bank using (type_bank_id)
		where 
			transfer_id in (select transfer_id from {0}.transfer 
							where per_num=(select per_num from {0}.transfer where transfer_id=:p_transfer_id) )
			and type_account_id in (1);
end;