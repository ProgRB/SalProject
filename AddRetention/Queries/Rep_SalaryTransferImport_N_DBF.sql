select 'аспъряйне няа ╧8601' A,
    ' ' B,
    ' ' C,
    ' ' D,
    ' ' E,
    ' ' F,
    ' ' G,
    to_number(null) sum_field,
    to_number(null) count_field
from dual
union all
select 'й окюрефмнлс онпсвемхч ╧' A, '1' B, 'нр' C, to_char(sysdate,'dd.mm.yyyy') D, ' ' E, ' ' F, ' ' G, null s1, null s2 from dual
union all
select 'гювхякемхе' A, '01' B, '810' C, ' ' D, ' ' E, ' ' F, ' ' G, null s1, null s2 from dual
union all
select 'мюхлемнбюмхе,нцпм,явер' A, 'нюн с-сюг, 1020300887793' B, 
	(select max(CURRENT_ACCOUNT) keep (dense_rank FIRST order by BANK_OFFICE) FROM {1}.TYPE_BANK WHERE TRN=:p_TRN and custom_sign=0) C, ' ' D, ' ' E, ' ' F, ' ' G, null s1, null s2 from dual
union all
select 'он днцнбнпс' A, '09160017' B, 'нр' C, '20.10.2003' D, ' ' E, ' ' F, ' ' G, null s1, null s2 from dual
union all
select '╧ о/о' A, 'мнлеп яверю' B, 'тюлхкхъ' C, 'хлъ' D, 'нрвеярбн' E,'ясллю' F, 'опхлевюмхе' G, null s1, null s2 from dual
union all
select a,b,c,d,e,f,g, s1, s2 from
(select 
    nvl(to_char(decode(grouping(number_account),0, row_number() over (order by OWNER_FAMILY, OWNER_NAME, OWNER_MIDDLE_NAME), null)),' ') A, 
    decode(grouping(number_account),0, number_account, 'хрнцн:') B,
    decode(grouping(number_account),0, OWNER_FAMILY,' ') C,
    decode(grouping(number_account),0, OWNER_NAME,' ') D,
    decode(grouping(number_account),0, OWNER_MIDDLE_NAME,' ') E,
    to_char(sum(SUM_SAL), 'FM999999999999999999990.00') F,
    ' ' G,
    decode(grouping(number_account),0, sum(sum_sal), null) s1,
    decode(grouping(number_account),0,1, null) as s2
from
    {1}.view_SALARY_transfer
    join {0}.transfer using (transfer_id)
    join {1}.client_account using (client_account_id)
    join {1}.type_bank using (type_bank_id)
where
    cartulary_id = :p_cartulary_id
    and nvl(custom_sign,0)=0
    and TRN=:p_TRN
group by rollup
    ((number_account,
    OWNER_FAMILY,
    OWNER_NAME,
    OWNER_MIDDLE_NAME))
having sum(sum_sal)!=0
order by owner_family, owner_name, owner_middle_name)