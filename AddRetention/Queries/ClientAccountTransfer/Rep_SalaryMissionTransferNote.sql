select  
		max(CURRENT_ACCOUNT) over (partition by bank_ident_code, trn, correspondent_account, COMPANY_NAME) CURRENT_ACCOUNT, 
		MAX(BANK_NAME) KEEP (DENSE_RANK FIRST ORDER BY BANK_OFFICE) over (PARTITION BY bank_ident_code, trn) BANK_NAME, 
		CORRESPONDENT_ACCOUNT, 
		TRN TRN_BANK, 
		BANK_IDENT_CODE, 
		PPC as PPC_BANK,
		CUSTOM_SIGN, 
		BANK_ADDRESS,
		TYPE_CARTULARY_NAME,
		TYPE_CARTULARY_ID,
		BALANCE_ACCOUNT CODE_ACCOUNT,
		COMPANY_NAME,
		OKATO,
		BCC_CODE,
		case when custom_sign=1 or count(distinct number_account) over (partition by bank_ident_code, trn)=1 then OWNER_FAMILY||' '||OWNER_NAME||' '||OWNER_MIDDLE_NAME else null end as OWNER_NAME,
		per_num,
		FIO,
		case when custom_sign=1 or count(distinct number_account) over (partition by bank_ident_code, trn)=1 then NUMBER_ACCOUNT end NUMBER_ACCOUNT,
		case when custom_sign=1 or count(distinct number_card) over (partition by bank_ident_code, trn)=1 then NUMBER_CARD end NUMBER_CARD,
		decode(custom_sign,1, BANK_OFFICE) BANK_OFFICE,
		CODE_SUBDIV,
		FIN_PLAN_CODE PAY_CODE,
		CHECK_DATE,
		CODE_DOCUM CONTRACT_CODE,
		TRANSFER_MESSAGE MESSAGE,
        sum_sal
	from
		SALARY.view_salary_transfer
		join (select transfer_id, emp_last_name||' '||emp_first_name||' '||emp_middle_name FIO, per_num from APSTAFF.transfer join APSTAFF.emp using (per_num)) t using (transfer_id)
		left join (select FIN_PLAN_CODE, check_date, code_docum, salary_id from SALARY.V_DOCUM_TRANSFER_RELATION join SALARY.DOCUM_TRANSFER using (DOCUM_TRANSFER_ID)) using (salary_id)
		join SALARY.client_account using (client_account_id)
		join SALARY.type_bank using (type_bank_id)
		join APSTAFF.subdiv using (subdiv_id)
		join SALARY.cartulary using (cartulary_id)
		join (select TYPE_CARTULARY_NAME, TRANSFER_MESSAGE, BALANCE_ACCOUNT, TYPE_CARTULARY_ID from SALARY.type_cartulary) using (type_cartulary_id)
	where
		cartulary_id = :p_cartulary_id
        and sum_sal!=0
order by bank_name