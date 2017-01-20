declare
begin
	open :c1 for select distinct order_id, order_name from salary.salary_addition join {0}.orders using (order_id) where calc_date=trunc(:p_date,'month')
		order by order_name;
    open :c2 for select order_id, order_name||case when order_book_id is not null then '(книга заказов)' end order_name from apstaff.orders 
        left join {0}.orders_book using (order_id) 
        where order_name not like '000%'
        order by order_name;
end;
