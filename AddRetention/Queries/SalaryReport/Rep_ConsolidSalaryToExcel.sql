declare
    l_date_begin date := trunc(:p_date_begin,'month');
    l_date_end date := add_months(trunc(:p_date_end,'month'), 1)-1/86400;
    p_subdiv_id number := :p_subdiv_id;
begin
    open :c for
    select 
        code_subdiv "Подразделение", 
        trunc(pay_date,'month') "Дата",
        code_payment "Вид оплат",
        sum(sum_sal) "Сумма"
    from
        salary.view_salary_by_subdiv
        join (select subdiv_id, code_subdiv from apstaff.subdiv start with subdiv_id=p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
        join salary.payment_type using (payment_type_id)
        join (select payment_type_id from salary.report_group left join salary.report_setting using (report_group_id) 
                start with group_code='1100' and l_date_end between  date_begin_report and date_end_report
                connect by prior report_group_id=parent_group_id
					
             ) using (payment_type_id)
    where 
        trunc(pay_date,'month') between l_date_begin and l_date_end
    group by code_subdiv, code_payment, trunc(pay_date,'month')
    order by 1,2,3;
end;
