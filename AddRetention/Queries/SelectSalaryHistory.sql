declare
begin
	open :c for
	select
		row_number() over (order by date_edit, salary_id, op_id) rn, 
		pay_date, 
		subdiv_id,
		per_num,
		payment_type_id,
		hours,
		days,
		sum_sal,
		order_name, 
		case when user_name in ('KNV14534', 'SALARY') then 'Система' else user_name end user_name,
		op_id, 
		code_degree,
		salary_id,
		date_edit,
		transfer_id,
		session_id,
		retention_id
	from 
		salary.salary_history 
		join (select transfer_id, per_num from apstaff.transfer) using (transfer_id) 
		join salary.payment_type using (payment_type_id)
		left join apstaff.degree using (degree_id)
		left join apstaff.orders using (order_id)
	where per_num=:p_per_num and trunc(pay_date,'month')=:p_date and code_payment like '%'||:p_code_payment||'%'
	order by date_edit, salary_id, op_id;
end;
