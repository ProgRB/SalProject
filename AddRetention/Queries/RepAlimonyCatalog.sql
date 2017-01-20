select 
	(select max(CODE_SUBDIV) keep (dense_rank last order by date_transfer) from {0}.transfer join {0}.subdiv using (subdiv_id) where worker_id=t.worker_id and date_transfer<last_day(:p_date)) code_subdiv,
    per_num,
    plf_index INDEX1,
    destination plf_address,
    plf_name FIO1,
    decode(sign_comb,1,'X', '') sign_comb, 
    retent_sum ret_sum,
    retent_percent/100 percent,
    NAME_TYPE_ACCOUNT TEXT_MESSAGE,
	ORDER_NUMBER RNUMBER,
	CODE_PAYMENT
from {1}.retention r
    join {0}.transfer t on (t.transfer_id = r.transfer_id)
	left join {1}.payment_type pt on (r.payment_type_id=pt.payment_type_id)
	join (select max(plf_name) keep (dense_rank last order by DATE_BEGIN_RELATION) PLF_NAME, max(plf_address) keep (dense_rank last order by DATE_BEGIN_RELATION) plf_address, 
				max(plf_index) keep (dense_rank last order by DATE_BEGIN_RELATION) plf_index,  
				max(client_account_id) keep (dense_rank last order by date_begin_relation) client_account_id,
				retention_id 
			from {1}.client_account join {1}.client_retent_relation using (client_account_id) 
		 where trunc(:p_date,'month')<=nvl(date_end_relation, date'3000-01-01') and add_months(trunc(:p_date, 'month'),1)-1/86400>=nvl(date_begin_relation, date'1000-01-01')
		 group by retention_id) crr   on (crr.retention_id=r.retention_id)
	left join {1}.VIEW_CLIENT_ACCOUNT using (client_account_id)
where :p_date between nvl(DATE_START_RET, date'1000-01-01') and NVL(DATE_END_RET, date'3000-01-01')
	and r.payment_type_id in (select payment_type_id from {1}.report_setting where report_group_id in (select report_group_id from {1}.report_group where group_code in ('11103')))