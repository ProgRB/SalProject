declare
begin
	open :c1 for select  
			CURRENT_ACCOUNT, 
			BANK_NAME, 
			CORRESPONDENT_ACCOUNT, 
			--BANK_OFFICE, 
			--BRANCH_BANK, 
			TRN TRN_BANK, 
			BANK_IDENT_CODE, 
			--CUSTOM_SIGN, 
			PPC as PPC_BANK, 
			CONTRACT_CODE, 
			CONTRACT_DATE, 
			BANK_ADDRESS,
			(select TYPE_CARTULARY_NAME from {1}.cartulary join {1}.type_cartulary using (type_cartulary_id) where 
				cartulary_id=:p_cartulary_id) TYPE_CARTULARY_NAME,
		    case when (select type_cartulary_id from {1}.cartulary where cartulary_id=:p_cartulary_id)
				in (7) then 'удержаний из заработной платы' else 'заработной платы (или прочее)' end TEXT
		from {1}.type_bank where TRN=:p_TRN and rownum=1;
	open :c2 for
		select 
			PASSPORT_SERIES,
			PASSPORT_NUMBER,
			OWNER_FAMILY||' '||OWNER_NAME||' '||OWNER_MIDDLE_NAME as OWNER_NAME,
			NUMBER_ACCOUNT,
			NUMBER_CARD,
			case when regexp_like(paid_comment, 'с(.)+пр(\S)* (\d)+', 'i') then 
				regexp_substr(paid_comment, 'с(.)+пр(\S)* (\S+)', 1, 1, 'i')
			else null end WRIT,
			case when regexp_like(paid_comment, 'кр(.)+дог(\S)*(\s)+(\d)+', 'i') then 
				regexp_substr(paid_comment, 'кр(.)+дог(\S)*(\s)+(\S+)', 1, 1, 'i')
			else null end CONTRACT_CODE,
			sum(SUM_SAL) sum_sal
		from
			{1}.view_salary_transfer
			join {1}.client_account using (client_account_id)
			join {1}.type_bank using (type_bank_id)
		where
			cartulary_id = :p_cartulary_id
			and nvl(custom_sign,0)=0
			and TRN=:p_TRN
		group by 
			PASSPORT_SERIES,
			PASSPORT_NUMBER,
			OWNER_FAMILY,
			OWNER_NAME,
			OWNER_MIDDLE_NAME,
			NUMBER_ACCOUNT,
			NUMBER_CARD,
			paid_comment
		having sum(sum_sal)!=0
		order by owner_name;
end;