select 
    per_num tab_n,
    OWNER_FAMILY||' '||OWNER_NAME||' '||OWNER_MIDDLE_NAME name,
    decode(code_doc, 21, substr(PASSPORT_SERIES,1,2)||' '||substr(PASSPORT_SERIES,3,2)||' '||PASSPORT_NUMBER, PASSPORT_SERIES||'/'||passport_number)  sernum,
    nvl(to_char(decode(code_doc, 1, 1, 21, 8, 3, 19, 4, 4)),' ') passcode,
    nvl(GET_PLACE, ' ') pwhr,
    sum(SUM_SAL) sum_pay,
    NVL(date_doc, date'1000-01-01') pdat,
    nvl(get_city,' ') pgorod,
    lpad(trim(number_card),12,'0') acc
from
    {1}.view_salary_transfer
    join {0}.transfer using (transfer_id)
    join {1}.client_account using (client_account_id)
    join {1}.type_bank using (type_bank_id)
where
    cartulary_id = :p_cartulary_id
    and nvl(custom_sign,0)=0
    and TRN=:p_TRN
group by 
    per_num,
    decode(code_doc, 21, substr(PASSPORT_SERIES,1,2)||' '||substr(PASSPORT_SERIES,3,2)||' '||PASSPORT_NUMBER, PASSPORT_SERIES||'/'||passport_number),
    OWNER_FAMILY,
    OWNER_NAME,
    decode(code_doc, 1, 1, 21, 8, 3, 19, 4, 4),
    OWNER_MIDDLE_NAME,
    GET_PLACE,
    date_doc,    
    NUMBER_card,
    get_city
having sum(sum_sal)!=0
order by tab_n