select
	client_account_id,
    NAME_TYPE_ACCOUNT,
    BANK_NAME,
    NUMBER_ACCOUNT,
    NUMBER_CARD,
    owner_family||' '||substr(owner_name,1,1)||'. '||substr(owner_middle_name,1,1)||'.' OWNER_FIO,
    PLF_NAME,
    PLF_ADDRESS
from
    {1}.client_account
    left join {1}.type_bank using (type_bank_id)
    left join {1}.TYPE_ACCOUNT using (type_account_id)
where
    transfer_id in (select transfer_id from {0}.transfer where per_num=(select per_num from {0}.transfer where transfer_id=:p_transfer_id))