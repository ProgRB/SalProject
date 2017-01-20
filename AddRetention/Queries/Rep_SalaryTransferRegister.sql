select 
    OWNER_FAMILY,
    OWNER_NAME,
    OWNER_MIDDLE_NAME,
    NUMBER_ACCOUNT,
    sum(SUM_SAL) sum_sal,
	BANK_NAME,
    TRN as TRN_BANK,
    BANK_IDENT_CODE, 
    CORRESPONDENT_ACCOUNT,
    PPC PPC_BANK,
    (select DATE_CARTULARY from {1}.cartulary where cartulary_id=:p_cartulary_id) PAY_DATE,
	case when regexp_like(paid_comment, 'с(.)+пр(\S)* (\d)+', 'i') then 
        regexp_substr(paid_comment, 'с(.)+пр(\S)* (\S+)', 1, 1, 'i')
    else null end WRIT,
	case when regexp_like(paid_comment, 'кр(.)+дог(\S)*(\s)+(\d)+', 'i') then 
        regexp_substr(paid_comment, 'кр(.)+дог(\S)*(\s)+(\S+)', 1, 1, 'i')
    else null end CONTRACT_CODE
from
    {1}.view_salary_transfer
    join {1}.client_account using (client_account_id)
    join {1}.type_bank using (type_bank_id)
where
    cartulary_id = :p_cartulary_id
    and nvl(custom_sign,0)=0
    and TRN=:p_TRN
group by OWNER_FAMILY,
    OWNER_NAME,
    OWNER_MIDDLE_NAME,
    NUMBER_ACCOUNT,
	BANK_NAME,
    TRN,
    BANK_IDENT_CODE, 
    CORRESPONDENT_ACCOUNT,
    PPC,
	paid_comment
having sum(sum_sal)!=0