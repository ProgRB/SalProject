select  
		max(max(CURRENT_ACCOUNT)) over (partition by bank_ident_code, trn, correspondent_account, COMPANY_NAME) CURRENT_ACCOUNT, 
		MAX(BANK_NAME) KEEP (DENSE_RANK FIRST ORDER BY BANK_OFFICE) BANK_NAME, 
		CORRESPONDENT_ACCOUNT, 
		TRN TRN_BANK, 
		BANK_IDENT_CODE, 
		PPC as PPC_BANK,
		CUSTOM_SIGN, 
		BANK_ADDRESS,
		TYPE_CARTULARY_NAME,
		case when custom_sign=1 then OWNER_FAMILY||' '||OWNER_NAME||' '||OWNER_MIDDLE_NAME else null end as OWNER_NAME,
		decode(custom_sign,1, fio) FIO,
		case when custom_sign=1 or type_account_id in (5) then NUMBER_ACCOUNT else null end NUMBER_ACCOUNT,
		decode(custom_sign,1, NUMBER_CARD) NUMBER_CARD,
		decode(custom_sign,1,BANK_OFFICE) BANK_OFFICE,
		COMPANY_NAME,
		OKATO,
		BCC_CODE,
		CODE_SUBDIV,
		TYPE_CARTULARY_ID,
		max(PAY_CODE) as PAY_CODE,
		TRANSFER_MESSAGE MESSAGE,
		 case when TYPE_CARTULARY_ID in (7, 8) then 'Удержано из заработной платы за '||to_char(date_cartulary , 'Month yyyy', 'NLS_DATE_LANGUAGE=Russian')||'г.' else null end
		 ||' '||
		 NVL(case when SEPARATE_SIGN=1 or CUSTOM_SIGN=1 and type_cartulary_id in (7, 8) then PAID_COMMENT||' (Сотрудник '||FIO||')' ELSE NULL END,
			case when type_cartulary_id=7 and count(*) over (partition by CORRESPONDENT_ACCOUNT, TRN, BANK_IDENT_CODE, COMPANY_NAME,
									case when custom_sign=1 or type_account_id in (5) then NUMBER_ACCOUNT else null end,
									decode(custom_sign,1, NUMBER_CARD),
									decode(custom_sign,1, fio),
									OKATO,
									BCC_CODE)<2 then Max(PAID_COMMENT)||' (Сотрудник '||MAX(FIO)||')' else null end) TEXT,		
		 sum(SUM_SAL) sum_sal,
		 case
			when code_subdiv='139' then '7905'
			when code_subdiv='145' then '7904'
			else BALANCE_ACCOUNT 
		 end CODE_ACCOUNT
	from
		{1}.view_salary_transfer
		join (select transfer_id, emp_last_name||' '||emp_first_name||' '||emp_middle_name FIO from {0}.transfer join {0}.emp using (per_num)) t using (transfer_id)
		join {1}.client_account using (client_account_id)
		join {1}.type_bank using (type_bank_id)
		join {0}.subdiv using (subdiv_id)
		join {1}.cartulary using (cartulary_id)
		join {1}.type_cartulary using (type_cartulary_id)
	where
		cartulary_id = :p_cartulary_id
	group by 
		CORRESPONDENT_ACCOUNT, 
		decode(custom_sign,1,BANK_OFFICE),  
		TRN, 
		BANK_IDENT_CODE, 
		PPC,
		CUSTOM_SIGN, 
		BANK_ADDRESS,
		COMPANY_NAME,
		case when custom_sign=1 then OWNER_FAMILY||' '||OWNER_NAME||' '||OWNER_MIDDLE_NAME else null end,
		case when custom_sign=1 or type_account_id in (5) then NUMBER_ACCOUNT else null end,
		decode(custom_sign,1, NUMBER_CARD),
		decode(custom_sign,1, fio),
		code_subdiv,
		type_cartulary_id,
		date_cartulary,
		TYPE_CARTULARY_NAME,
		OKATO,
		BCC_CODE,
		BALANCE_ACCOUNT, TRANSFER_MESSAGE,
		case when SEPARATE_SIGN=1 or CUSTOM_SIGN=1 and type_cartulary_id in (7, 8) then PAID_COMMENT||' (Сотрудник '||FIO||')' ELSE NULL END
	having sum(sum_sal)!=0
order by bank_name