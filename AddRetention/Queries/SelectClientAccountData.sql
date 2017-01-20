begin
	open :c1 for select * from {1}.CLIENT_ACCOUNT where client_account_id=:p_client_account_id;
	open :c2 for select * from {1}.TYPE_BANK;
	open :c3 for select * from {1}.TYPE_ACCOUNT;
	open :c4 for select emp_last_name||' '||emp_first_name||' '||emp_middle_name FIO from {0}.transfer join {0}.emp using (per_num) where transfer_id=:p_transfer_id;
	open :c5 for select * from {0}.TYPE_PER_DOC;
	open :c6 for select * from {1}.BANK_FOR_TYPE_ACCOUNT;
end;