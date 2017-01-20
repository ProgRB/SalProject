select 
    '0268' pay_code,
    'паспорт гражданина РФ' TYPE_PASSPORT,
    PASSPORT_SERIES,
    PASSPORT_NUMBER,
    OWNER_FAMILY,
    OWNER_NAME,
    OWNER_MIDDLE_NAME,
    NUMBER_ACCOUNT,
    sum(SUM_SAL) sum_sal
from
    {1}.view_salary_transfer
    join {1}.client_account using (client_account_id)
    join {1}.type_bank using (type_bank_id)
where
    cartulary_id = :p_cartulary_id
    and nvl(custom_sign,0)=0
    and TRN=:p_TRN
group by 
	PASSPORT_SERIES,
    PASSPORT_NUMBER,
    OWNER_FAMILY,
    OWNER_NAME,
    OWNER_MIDDLE_NAME,
    NUMBER_ACCOUNT
having sum(sum_sal)!=0