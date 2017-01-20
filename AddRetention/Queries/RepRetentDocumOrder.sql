select 
	per_num,
	emp_last_name||' '||emp_first_name||' '||emp_middle_name FIO1,
	code_subdiv,
	relation_comment as TEXT_MESSAGE,
	NVL(PLF_NAME, OWNER_FAMILY||' '||OWNER_NAME||' '|| OWNER_MIDDLE_NAME) PLF_NAME,
	NVL(PLF_ADDRESS, BANK_NAME||' ИНН '||TRN||' БИК '||BANK_IDENT_CODE||' счет№ '||NVL(NUMBER_ACCOUNT, NUMBER_CARD)) PLF_ADDRESS,
	RETENT_PERCENT as PERCENT,
	RETENT_SUM as RET_SUM,
	DATE_START_RET as DATE_BEGIN,
	DATE_END_RET as DATE_END,
	RETENTION_ID as ID,
	ORIGINAL_SUM as SUM_SAL2
from
	{1}.RETENTION
	join {0}.TRANSFER using (transfer_id)
	join {0}.subdiv using (subdiv_id)
	join {0}.emp using (per_num)
	left join (select retention_id, 
                    max(client_account_id) keep (dense_rank last order by date_begin_relation) client_account_id,
                    max(relation_comment) keep (dense_rank last order by date_begin_relation) relation_comment
                  from
                    (select retention_id, date_begin_relation, max(client_account_id) client_account_id, 
						listagg(relation_comment,'; ')  within group (order by relation_comment) relation_comment 
					from 
                        {1}.client_retent_relation group by retention_id, date_begin_relation)
                    group by retention_id) using (retention_id)
	left join {1}.client_account using (client_account_id)
	left join {1}.type_bank using (type_bank_id)
where
	retention_id member of :p_retent_ids