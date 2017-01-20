declare
begin
	open :c1 for select * from {1}.CLIENT_ACCOUNT where transfer_id is null;
	open :c2 for select * from {1}.TYPE_BANK;
	open :c3 for select * from {1}.BANK_FOR_TYPE_ACCOUNT;
end;