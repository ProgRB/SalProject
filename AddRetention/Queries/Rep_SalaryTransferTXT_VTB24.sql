select 'START;'||to_char(sysdate,'dd')||to_char((select date_cartulary from {1}.cartulary where cartulary_id=:p_cartulary_id), 'mmyyyy')||';001;CREDIT;Œ¿Œ ”-”¿«', to_number(null) sum_field, to_number(null) count_field from dual
union all
select * from 
    (select
        decode(grouping(owner_name),0, 
		number_card||';'||to_char(sum(SUM_SAL), 'FM999999999999999999990D00',  'nls_numeric_characters='', ''')||';'||OWNER_FAMILY||' '||OWNER_NAME||' '||OWNER_MIDDLE_NAME, 
        'END;'||to_char(count(number_card) over (),'FM99999')||';'||to_char(sum(sum_sal),'FM9999999999999990D00',  'nls_numeric_characters='', ''')||';RUR'),
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
        number_card))
    having sum(sum_sal)!=0
    order by owner_family, owner_name, owner_middle_name)