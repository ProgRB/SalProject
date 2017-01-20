select '«¿–œÀ¿“¿' a, to_number(null) sum_field, to_number(null) count_field from dual
union all
select * from 
    (select
        decode(grouping(owner_name),0, 
        rpad(OWNER_FAMILY||' '||OWNER_NAME||' '||OWNER_MIDDLE_NAME, 60, ' ')||
        rpad(number_account, 20, ' ')||
        lpad(to_char(sum(SUM_SAL), 'FM99999999990.00'), 27,' '), 
        '»“Œ√Œ:'||to_char(count(distinct number_account||owner_family) over (),'FM0000')||lpad(to_char(sum(sum_sal),'FM9999999999999990.00'), 17,' ')),
        decode(grouping(owner_name),0,sum(sum_sal)),
        decode(grouping(owner_name),0, 1)
    from
        {1}.view_salary_transfer
        join {0}.transfer using (transfer_id)
        join {1}.client_account using (client_account_id)
        join {1}.type_bank using (type_bank_id)
    where
        cartulary_id = :p_cartulary_id
        and nvl(custom_sign,0)=0
        and TRN=:p_TRN
    group by rollup
        ((OWNER_FAMILY,
        OWNER_NAME,
        OWNER_MIDDLE_NAME,
        number_account))
    having sum(sum_sal)!=0
    order by owner_family, owner_name, owner_middle_name)