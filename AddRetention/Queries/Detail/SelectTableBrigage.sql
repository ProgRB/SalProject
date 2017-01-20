declare
    l_date_begin  date := trunc(:p_date,'month');
    l_date_end  date := add_months(trunc(:p_date,'month'), 1)-1/86400;
begin
    open :c1 for
        select
            t.*,
			hours_piece as piece_hours,
			sum_piece  as piece_Sum   
        from
            SALARY.TABLE_BRIGAGE t
			left join SALARY.VIEW_TABLE_BRIGAGE vt on (t.table_brigage_id=vt.table_brigage_id)
        where
            trunc(t.work_date,'month')=trunc(:p_date,'month') 
            and t.brigage_id=:p_brigage_id;

    open :c2 for
        select 
            t.transfer_id, per_num, 
            emp_last_name, emp_first_name, emp_middle_name,
            photo,
            tar_hour,
            code_tariff_grid,
            ad.tariff_grid_id,
            pos_name,
            code_degree,
            sign_comb,
            nvl(ad.classific,0) classific,
            date_transfer,
            Nullif(end_transfer, date'3000-01-01') end_transfer
        from 
             (select transfer_id, date_transfer, end_transfer, type_transfer_id, worker_id, per_num, degree_id, pos_id, from_position, sign_comb
                    from
                    (
                            select transfer_id, type_transfer_id, worker_id, subdiv_id, per_num, degree_id, pos_id, from_position, sign_comb,
                                trunc(date_transfer)+decode(type_transfer_id,3, 1, 0) date_transfer,
                                lead(trunc(date_transfer)+decode(type_transfer_id,3, 86399/86400, 0), 1, date'3000-01-01') over (partition by worker_id order by date_transfer) end_transfer
                            from
                                apstaff.transfer
                   )
                   where subdiv_id=:p_subdiv_id ) t
             join apstaff.emp using (per_num)
             join apstaff.degree using (degree_id)
             join apstaff.position using (pos_id)
             join apstaff.account_data ad on (ad.transfer_id=decode(type_transfer_id,3, from_position, t.transfer_id))
             left join APSTAFF.TARIFF_GRID_SALARY tg  on (ad.tariff_grid_id=tg.tariff_grid_id and NVL(ad.classific, 0)=tg.tar_classif and l_date_begin between tar_date and tariff_end_date)
        where t.date_transfer<=l_date_end and t.end_transfer>=l_date_begin and type_transfer_id!=3 and degree_id in (1,2)
            or t.transfer_id in (select transfer_id from salary.TABLE_BRIGAGE t
                                    where trunc(work_date,'month')=trunc(:p_date,'month') 
                                            and brigage_id=:p_brigage_id);
end;


