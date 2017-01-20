select 
	   :p_date_begin PAY_DATE,
       NVL(MAX(BANK_NAME) KEEP (DENSE_RANK FIRST ORDER BY BANK_OFFICE), 'Прочие ( или по проводке)') BANK_NAME, 
        decode(custom_sign,1, fio) FIO,
        case when custom_sign=1 or type_account_id in (5) then NUMBER_ACCOUNT else null end NUMBER_ACCOUNT,
        decode(custom_sign,1, NUMBER_CARD) NUMBER_CARD,
        COMPANY_NAME,
        case when OKATO is null then null else 'ОКАТО=('||OKATO||')' end OKATO,
		case when BCC_CODE is not null then 'КПП='||BCC_CODE
		else
			nvl(case when SEPARATE_SIGN=1 or CUSTOM_SIGN=1 then PAID_COMMENT||' (Сотрудник '||FIO||')' ELSE NULL END, 
				case when count(*)<2 then MAX(PAID_COMMENT)||' (Сотрудник '||MAX(FIO)||')' else null end)
	    end BCC_CODE,
        sum(sum_for_transfer) sum_sal
    from
        salary.view_retention_account
        join (select transfer_id, emp_last_name||' '||emp_first_name||' '||emp_middle_name FIO from apstaff.transfer join apstaff.emp using (per_num)) t using (transfer_id)
		join (select subdiv_id from apstaff.subdiv start with subdiv_id=nvl(:p_subdiv_id,0) connect by prior subdiv_id=parent_id and code_subdiv not in ('039','139','145')) using (subdiv_id)
        left join (select client_retent_relation_id, bcc_code, okato, relation_comment as paid_comment from  salary.client_retent_relation) using (client_retent_relation_id)
        left join salary.client_account using (client_account_id)
        left join salary.type_bank using (type_bank_id)
    where
        pay_date between trunc(:p_date_begin,'month') and add_months(trunc(:p_date_end,'month'),1)-1/86400
        and payment_type_id member of :p_payment_type_ids -- формируем в реестр только нужные шифры оплат
        and sum_for_transfer>0
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
        OKATO,
        BCC_CODE,
        case when SEPARATE_SIGN=1 or CUSTOM_SIGN=1 then PAID_COMMENT||' (Сотрудник '||FIO||')' ELSE NULL END
order by  custom_sign, bank_name, company_name, bcc_code