declare
begin
	open :c for  select * from {1}.TYPE_PAYMENT_TYPE;
	open :c1 for select * from {1}.RETENT_CALC_METHOD;
	open :c2 for select pay_type_id, PAY_TYPE_NAME from {0}.pay_type;
	open :c3 for select * from {1}.consider_type;
end;