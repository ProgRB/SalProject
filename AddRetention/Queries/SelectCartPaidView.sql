declare
	k number;
begin
	select count(*) into k from 
		{0}.USER_ROLES
	where ROLE_NAME = ora_login_user and component_name='ViewCartularyTransfer' and app_name='SALARY';
	if dbms_session.is_role_enabled('SALARY_CARTULARY_EDIT') or k>0 
	then
		open :c for
		select 
			cartulary_paid_id,
			s.code_subdiv,
			t.per_num,
			DECODE(t.sign_comb,1,'X', '') sign_comb,
			emp_last_name||' '||substr(emp_first_name,1,1)||'. '||substr(emp_middle_name,1,1)||'.' FIO,
			sum_sal PAID_SUM,
			case
			when type_cartulary_id in (1) then regexp_substr(PLF_NAME, '(\w+)')||' ('||substr(PLF_ADDRESS,1,80)||')'
			else
				nvl(NUMBER_ACCOUNT,'_____<нет счета>____')||' / '||NVL(NUMBER_card,'<нет карты>')
			end DESTINATION,
			bank_name,
			code_payment
		from 
			{1}.view_salary_transfer cp
			join {1}.cartulary using (cartulary_id)
			join {0}.transfer t on (cp.transfer_id=t.transfer_id)
			join {0}.subdiv s on (s.subdiv_id=cp.subdiv_id)
			join {0}.emp e on (t.per_num=e.per_num)
			join {1}.payment_type pt on (cp.payment_type_id=pt.payment_type_id)
			left join {1}.client_account using (client_account_id)
			left join {1}.type_bank using (type_bank_id)
		where
			cartulary_id=:p_cartulary_id
		order by s.code_subdiv, t.per_num;
	else
		open :c for
		select 
			cartulary_paid_id,
			s.code_subdiv,
			'*****' as per_num,
			'*' as sign_comb,
			'<нет доступа>' as FIO,
			sum_sal PAID_SUM,
			'*****' as DESTINATION,
			bank_name,
			code_payment
		from 
			{1}.view_salary_transfer cp
			join {1}.cartulary using (cartulary_id)
			join {0}.transfer t on (cp.transfer_id=t.transfer_id)
			join {0}.subdiv s on (s.subdiv_id=cp.subdiv_id)
			join {0}.emp e on (t.per_num=e.per_num)
			join {1}.payment_type pt on (cp.payment_type_id=pt.payment_type_id)
			left join {1}.client_account using (client_account_id)
			left join {1}.type_bank using (type_bank_id)
		where
			cartulary_id=:p_cartulary_id
		order by s.code_subdiv, t.per_num;
	end if;
end;