select client_account_id, NAME_TYPE_ACCOUNT,
	case when type_account_id in (3,4) then PLF_NAME else owner_family||' '||substr(owner_name,1,1)||'. '||substr(owner_middle_name,1,1)||'.' end PLF_NAME,
	substr(PLF_ADDRESS, 1, 60)||'...' PLF_ADDRESS,
	NUMBER_ACCOUNT,
	NUMBER_CARD,
	bank_name,
	type_account_id,
	INSURANCE_NUM,
	PER_INSURANCE_NUM,
	PLF_INDEX,
	TRANSFER_ID,
	COMPANY_NAME
from {1}.client_account join {1}.type_account using (type_account_id)
	left join {1}.type_bank using (type_bank_id)
where transfer_id in 
(select transfer_id from {0}.transfer where per_num = (select per_num from {0}.transfer where transfer_id = :p_transfer_id ))
and 
	((select code_payment from {1}.payment_type where payment_type_id=:p_payment_type_id) in ('275', '277', '417') and type_account_id in (2,3,4,5)
	or 
     (select code_payment from {1}.payment_type where payment_type_id=:p_payment_type_id) in ('292') and type_account_id in (2,5)
	or 
     (select code_payment from {1}.payment_type where payment_type_id=:p_payment_type_id) in ('287', '287Ê', '287Î', '487', '488') and type_account_id in (1)
	or 
     (select code_payment from {1}.payment_type where payment_type_id=:p_payment_type_id) in ('401', '402') and type_account_id in (6)
	 )
union all
select client_account_id, NAME_TYPE_ACCOUNT,
	case when type_account_id in (3,4) then PLF_NAME else owner_family||' '||substr(owner_name,1,1)||'. '||substr(owner_middle_name,1,1)||'.' end PLF_NAME,
	substr(PLF_ADDRESS, 1, 60)||'...' PLF_ADDRESS,
	NUMBER_ACCOUNT,
	NUMBER_CARD,
	bank_name,
	type_account_id,
	INSURANCE_NUM,
	PER_INSURANCE_NUM,
	PLF_INDEX,
	TRANSFER_ID,
	COMPANY_NAME
from {1}.client_account join {1}.type_account using (type_account_id)
	left join {1}.type_bank using (type_bank_id)
where transfer_id is null
	and (select code_payment from {1}.payment_type where payment_type_id=:p_payment_type_id) in ('292')