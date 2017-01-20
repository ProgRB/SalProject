declare
begin
    open :c for
    select
        work_date as complete_date,
        null as detail_code,
        null as tech_kit,
        order_name,
        null as detail_count,
        work_classific,
        hours as detail_time,
        sum_sal as detail_sum,
        npa as package_number
    from
        salary.view_detail_subdiv
        join apstaff.orders using (order_id)
	where
        subdiv_id=:p_subdiv_id
        and trunc(work_date,'month')=trunc(:p_date,'month')
        and per_num = (select brigage_code from salary.brigage where brigage_id=:p_brigage_id);
end;
