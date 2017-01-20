select
	client_account_id,
    owner_family,
    owner_name,
    owner_middle_name,
    passport_series,
    passport_number,
    bank_name,
    number_account,
    number_card,
    emp_last_name||' '||emp_first_name||' '||emp_middle_name FIO,
    per_num
from 
    {1}.client_account 
    join {1}.type_bank using (type_bank_id)
    join {0}.transfer t using (transfer_id)
    join {0}.emp using (per_num)
where per_num=(select max(per_num) from apstaff.transfer where worker_id=:p_worker_id)
     