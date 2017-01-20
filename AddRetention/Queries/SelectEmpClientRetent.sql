select name_payment,
    code_payment, 
	retention_id,
	order_number,
    date_start_ret,
    date_end_ret,
	retent_percent,
	retent_sum,
    date_begin_relation,
    date_end_relation,
    number_card,
	NUMBER_ACCOUNT,
    bank_name,
    OWNER_FAMILY||' '||OWNER_NAME||' '||OWNER_MIDDLE_NAME as OWNER_FIO,
    PASSPORT_SERIES||' '||PASSPORT_NUMBER as PASSPORT_NUMBER,
    PLF_NAME,
    PLF_ADDRESS,
    NAME_TYPE_ACCOUNT,
	NVL(GROUP_SORT_ORDER,0) sort_number
from
    {1}.report_group
    join {1}.report_setting using (report_group_id)
    join {1}.retention using (payment_type_id)
    join {1}.payment_type using (payment_type_id)
    join {0}.transfer using (transfer_id)
    left join {1}.CLIENT_RETENT_RELATION  using (retention_id)
    left join {1}.client_account using (client_account_id)
	left join {1}.type_account using (type_account_id)
    left join {1}.type_bank using (type_bank_id)
where 
   group_code in ('0031', '0033')
   and worker_id=:p_worker_id