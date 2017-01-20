declare
begin
	open :c1 for select * from {1}.EMP_TAX_DISCOUNT where emp_tax_discount_id=:p_EMP_TAX_DISCOUNT_ID;
	open :c2 for select * from {1}.TYPE_DISCOUNT
		where :p_date between date_start and date_end;
end;