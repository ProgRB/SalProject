begin
	open :c1 for select * from {1}.TYPE_BANK where type_bank_id=:p_type_bank_id;
	open :c2 for select  * from {1}.TYPE_BANK;
	open :c3 for select * from {1}.TYPE_ACCOUNT;
	open :c4 for select * from {1}.BANK_FOR_TYPE_ACCOUNT where TYPE_BANK_ID=:p_TYPE_BANK_ID;
end;