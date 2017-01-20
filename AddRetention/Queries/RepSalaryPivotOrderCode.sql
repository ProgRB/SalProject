declare
    p_count_char varchar(20) :=:p_count_char;
    p_subdiv_id Number := :p_subdiv_id;
    orders_st varchar(32760);
	k number;
begin
	select count(*) into k 
	from  {1}.subdiv_for_close 
	where subdiv_id=:p_subdiv_id and date_closing<trunc(:p_date_end,'month') and app_name='SALARY';
	if k>0 then raise_application_error(-20229, 'Зарплата за указанный период еще не закрыта бухгалтерией'); end if;
	
    for item in 
    (
        select distinct nvl(substr(order_name,1, p_count_char), lpad('0', p_count_char, '0')) order_name
        from 
            {1}.view_salary_econ_short
            left join {0}.orders using (order_id)
            join (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
        where pay_date between trunc(:p_date_begin, 'month') and add_months(trunc(:p_date_end, 'month'), 1)-1/86400 and sum_sal!=0
        order by order_name
    )
    loop
        orders_st := orders_st||item.order_name||',';
    end loop;
    orders_st := substr(orders_st,1, length(orders_st)-1);
   open :c for
q'[select *
from
(
    select per_num "Таб.№",  fio "ФИО", decode(sign_comb,1,'X') "Совм.", code_subdiv "ПОДР", code_degree "Категория", code_pos "Шифр должн.", FORM_OPERATION_ID "Вид произ-ва", order_name, sum(sum_sal) sum_sal, 
        sum(sum(sum_sal)) over (partition by worker_id, per_num, sign_comb, fio, code_subdiv, code_degree, code_pos, FORM_OPERATION_ID) "ФОТ"
    from
    (
        select worker_id, per_num, sign_comb, fio, code_subdiv, code_degree, code_pos, FORM_OPERATION_ID,
                 to_number(nvl(substr(order_name,1, :p_count_char), lpad('0', :p_count_char, '0'))) order_name, sum_sal
        from 
            {1}.view_salary_econ_short
            left join {0}.orders using (order_id)
            join (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
        where pay_date between trunc(:p_date_begin, 'month') and add_months(trunc(:p_date_end, 'month'), 1)-1/86400
			and type_payment_type_id=1
            and sum_sal!=0
     )
     group by worker_id, per_num, sign_comb, fio, code_subdiv, code_degree, code_pos, FORM_OPERATION_ID, order_name
)
pivot
(
    sum(sum_sal)
    for order_name in (]'||orders_st||')
)
order by "ПОДР", "Таб.№"' using p_count_char, p_count_char, p_subdiv_id, :p_date_begin, :p_date_end;
end;