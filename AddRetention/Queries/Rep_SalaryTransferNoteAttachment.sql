select 
	t.*,
	count(*) over (partition by rank_value) as RNUMBER
from
(
	select  
		dense_rank() over (order by CORRESPONDENT_ACCOUNT, 
									decode(custom_sign,1,BANK_OFFICE),  
									TRN, 
									BANK_IDENT_CODE, 
									PPC,
									CUSTOM_SIGN, 
									BANK_ADDRESS, 
									COMPANY_NAME,
									decode(custom_sign,1,  OWNER_FAMILY||' '||OWNER_NAME||' '||OWNER_MIDDLE_NAME),
									case when custom_sign=1 or type_account_id in (5) then NUMBER_ACCOUNT else null end,
									decode(custom_sign,1, NUMBER_CARD),
									decode(custom_sign,1, fio),
									type_cartulary_id,
									date_cartulary,
									TYPE_CARTULARY_NAME,
									OKATO,
									BCC_CODE,
									case when SEPARATE_SIGN=1 or CUSTOM_SIGN=1 and type_cartulary_id=7 
									then PAID_COMMENT||' (Сотрудник '||FIO||')' ELSE NULL END) rank_value,
		max(CURRENT_ACCOUNT) over (partition by bank_ident_code, trn, correspondent_account, company_name) CURRENT_ACCOUNT, 
		MAX(BANK_NAME) KEEP (DENSE_RANK FIRST ORDER BY BANK_OFFICE) over (PARTITION BY BANK_IDENT_CODE, TRN)   BANK_NAME, 
		CORRESPONDENT_ACCOUNT, 
		TRN TRN_BANK, 
		BANK_IDENT_CODE, 
		PPC as PPC_BANK,
		CUSTOM_SIGN, 
		BANK_ADDRESS,
		TYPE_CARTULARY_NAME,
		decode(custom_sign,1, OWNER_FAMILY||' '||OWNER_NAME||' '||OWNER_MIDDLE_NAME) as OWNER_NAME,
		FIO,
		code_subdiv,
		case when custom_sign=1 or type_account_id in (5) then NUMBER_ACCOUNT else null end NUMBER_ACCOUNT,
		decode(custom_sign,1, NUMBER_CARD) NUMBER_CARD,
		decode(custom_sign,1,BANK_OFFICE) BANK_OFFICE,
		sum_sal,
		COMPANY_NAME,
		OKATO,
		BCC_CODE,
		PAID_COMMENT MESSAGE,
		case when TYPE_CARTULARY_ID in (7) then 'Удержано из заработной платы за '||to_char(date_cartulary , 'Month yyyy', 'NLS_DATE_LANGUAGE=Russian')||'г.' else null end
		||case when SEPARATE_SIGN=1 or CUSTOM_SIGN=1 and type_cartulary_id=7 
		  then PAID_COMMENT||' (Сотрудник '||FIO||')' ELSE NULL END    TEXT
	from
		salary.view_salary_transfer
		join (select transfer_id, emp_last_name||' '||emp_first_name||' '||emp_middle_name FIO from apstaff.transfer join apstaff.emp using (per_num)) t using (transfer_id)
		join salary.client_account using (client_account_id)
		join salary.type_bank using (type_bank_id)
		join apstaff.subdiv using (subdiv_id)
		join salary.cartulary using (cartulary_id)
		join salary.type_cartulary using (type_cartulary_id)
	where
		cartulary_id = :p_cartulary_id
) t