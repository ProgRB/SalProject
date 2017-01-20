declare
begin
	open :c1 for select * from {1}.TAX_EMP_DOCUM where tax_emp_docum_id=:p_tax_emp_docum_id;
	open :c2 for select * from {1}.TAX_COMPANY;
	open :c3 for select * from {1}.TAX_DOCUM_DISCOUNT where tax_emp_docum_id=:p_tax_emp_docum_id order by date_discount, code_discount;
	open :c4 for select * from {1}.TAX_DOCUM_PAYMENT where tax_emp_docum_id=:p_tax_emp_docum_id order by pay_date, pay_code;
end;
