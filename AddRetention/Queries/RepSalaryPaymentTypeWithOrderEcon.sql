declare
    t salary.number_collection_type;
    cd_payments varchar(4000);
    exist_payments salary.number_collection_type;    
	k number;
begin
    select
        payment_type_id bulk collect into exist_payments
    from
		{1}.report_group
		join {1}.report_setting using (report_group_id)
	where group_code='3004' and payment_type_id member of :p_payment_type_ids;
	
	select count(*) into k 
	from  {1}.subdiv_for_close 
	where subdiv_id=:p_subdiv_id and date_closing<trunc(:p_date_end,'month') and app_name='SALARY';
	if k>0 then raise_application_error(-20229, 'Зарплата за указанный период еще не закрыта бухгалтерией'); end if;
    
    select payment_type_id bulk collect into t from salary.payment_type where type_payment_type_id in (1, 6) and payment_type_id member of exist_payments;
    select listagg(''''||code_payment||''' as "'||code_payment||'"', ',') within group (order by code_payment) into cd_payments from salary.payment_type where type_payment_type_id in (1, 6) and payment_type_id member of exist_payments;
    
    open :c for
    q'[with st as 
            ( 
                select 
                    worker_id,
                    pay_date,
                    code_subdiv,
                    subdiv_id,
                    per_num,
					sign_comb,
                    fio,
                    code_degree,
                    nvl(order_name, ' ') order_name,
                    code_payment,
                    payment_type_id,
					type_payment_type_id,
                    max(date_transfer) date_transfer,
                    max(code_pos) keep (dense_rank last order by date_transfer) code_pos,
                    max(classific) keep (dense_rank last order by date_transfer) classific,
                    max(code_tariff_grid) keep (dense_rank last order by date_transfer) code_tariff_grid,
					max(code_form_operation) keep (dense_rank last order by date_transfer) code_operation,
                    max(salary) keep (dense_rank last order by date_transfer) salary,
                    max(nullif(group_master, '000')) keep (dense_rank last order by date_transfer) group_master,
                    nullif(sum(nvl(hours_table, 0)+nvl(hours106, 0)+ nvl(hours124, 0) + nvl(hours125, 0) + nvl(hours222, 0)),0) all_hours,
                    nullif(sum(hours_table), 0) hours_table,
                    nullif(sum(hours106), 0) hours106,
                    nullif(sum(hours124), 0) hours124,
                    nullif(sum(hours125), 0) hours125,
                    nullif(sum(hours222), 0) hours222,
                    nullif(sum(hours_piece), 0) hours_piece,
                    sum(case when type_payment_type_id=1 then sum_sal else null end) as all_sum,
                    sum(sum_sal) sum_sal
                from
                    salary.view_salary_econ_short
                    left join apstaff.orders using (order_id)
                    join (select transfer_id, date_transfer, code_form_operation from apstaff.transfer left join apstaff.form_operation using (form_operation_id)) using (transfer_id) 
                    join (select subdiv_id from apstaff.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
                where pay_date between trunc(:p_date_begin,'month') and :p_date_end
                group by pay_date, subdiv_id, code_subdiv, per_num, sign_comb, fio, code_degree, worker_id, code_payment, payment_type_id, order_name, type_payment_type_id
            )
        select * from
        (
            select
                to_char(pay_date, 'mm yyyy') "Месяц",
                code_subdiv "Подр.",
                per_num "Таб.№",
				case when sign_comb=1 then 'X' end "Совм",
                fio "Ф.И.О.",
                case when 
                    code_degree ='04' then
                        case substr(code_pos,1,1)
                            when '2' then '41'
                            when '4' then '43'
                        else '42' end
                else code_degree end "Категория",
                order_name "Заказ",
                code_operation "Вид произ-ва",
                code_pos "Шифр проф.",
                classific "Разряд",
                code_tariff_grid "Тар. сетка",
                salary "Тар. кф",
                group_master "Гр.мастера",
                round(avg_count_emp, 4) "ССЧ",
                all_hours "Всего часов",
                hours_table "Урочно",
                hours106 "Сверхурочно",
                hours124 "Выходн",
                hours125 "Перераб",
                hours222 "Командир",
                hours_piece "Наряды",
                all_sum "Всего начисл",
                s2.code_payment,
                s2.sum_sal sum_sal
            from
                (
                    select
                        row_number() over (order by pay_date, code_subdiv, per_num, sign_comb, order_name) rn,
                        pay_date, code_subdiv,
                        subdiv_id, 
                        per_num, sign_comb, fio, code_degree, worker_id, order_name,
                        max(code_pos) keep (dense_rank last order by date_transfer) code_pos,
                        max(classific) keep (dense_rank last order by date_transfer) classific,
                        max(code_tariff_grid) keep (dense_rank last order by date_transfer) code_tariff_grid,
						max(code_operation) keep (dense_rank last order by date_transfer) code_operation,
                        max(salary) keep (dense_rank last order by date_transfer) salary,
                        max(group_master) keep (dense_rank last order by date_transfer) group_master,
                        nullif(sum(all_hours),0) all_hours,
                        nullif(sum(hours_table), 0) hours_table,
                        nullif(sum(hours106), 0) hours106,
                        nullif(sum(hours124), 0) hours124,
                        nullif(sum(hours125), 0) hours125,
                        nullif(sum(hours222), 0) hours222,
                        nullif(sum(hours_piece), 0) hours_piece,
                        sum(sum_sal) as all_sum
                   from
                        st
				   where type_payment_type_id=1
                   group by pay_date, subdiv_id, code_subdiv, per_num, sign_comb, fio, code_degree, worker_id, order_name
                ) 
                left join 
                (select 
                    worker_id,
                    trunc(start_period,'month') pay_date,
                    subdiv_id,
                    code_degree,       
                    sum( case when subdiv_id=11 then 
                            case when degree_id!=4 and PAY_TYPE_ID in ('540_','124All') then round(hours,0)
                                   when degree_id=4 and PAY_TYPE_ID in ('540_','124All', '222D') then round(hours,0)
                                   else null
                            end
                         else
                            case when PAY_TYPE_ID in ('540_','124All', '222D') then round(hours,0) else null end
                        end)/extract(day from last_day(trunc(start_period,'month'))) as avg_count_emp
                 from 
                    salary.view_salary_table_subdiv s
                    left join apstaff.degree using (degree_id) 
                 where start_period<add_months(trunc(:p_date_end,'month'),1) and end_period>=trunc(:p_date_begin,'month')
                 group by worker_id, trunc(start_period,'month'), code_degree, subdiv_id
               ) using (worker_id, pay_date, subdiv_id, code_degree)
               left join 
               (
                    select worker_id, pay_date, subdiv_id, code_degree,  code_payment, sum(sum_sal) sum_sal, order_name
                    from st
                    where payment_type_id member of :p_payment_type_ids
                    group by  worker_id, pay_date, subdiv_id, code_degree,  code_payment, order_name
                    having sum(sum_sal)!=0
               ) s2 using (worker_id, pay_date, subdiv_id, code_degree, order_name)
        )
        pivot
        (
            sum(sum_sal)
            for code_payment in (]'||cd_payments||')
        )
        order by "Месяц", "Подр.", "Таб.№", "Заказ"' using :p_subdiv_id, :p_date_begin, :p_date_end, :p_date_end, :p_date_begin, t;
end;