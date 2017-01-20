declare
begin
	open :DiscountData for select * from {1}.emp_tax_discount etd 
		where transfer_id in (select transfer_id from {0}.transfer start with transfer_id=:p_transfer_id connect by nocycle prior transfer_id=from_position or prior from_position=transfer_id);
end;