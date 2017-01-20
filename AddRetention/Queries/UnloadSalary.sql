declare
P_SUBDIVS {1}.varchar_collection_type;
k number;
p_payments {1}.number_collection_type;
begin 
    select payment_type_id bulk collect into p_payments from {1}.payment_type join {1}.payment_calc_relation using (payment_type_id) 
                    where date'2014-01-01' between date_start_calc and date_end_calc and  nvl({1}.PAYMENT_HELPER.GETNUMBERPAYMENTPROPERTY(payment_calc_relation_id, 4),0)=0 and code_payment not in ('404', '999');
    open :c for 
        select count(*) into k
            from {1}.salary
               join apstaff.subdiv using (subdiv_id)
        where 
            trunc(pay_date,'month')=trunc(:p_date,'month')
            and code_subdiv in ('300', '301', '302', '303', '304')
            and payment_type_id member of p_payments;
	if k>0 then 
		raise_application_error(-20229, 'В 300ые подразделения попали не те шифры оплат');
	end if;
	
    select code_subdiv bulk collect into p_subdivs from {0}.subdiv
	where subdiv_id in (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by 
		 prior subdiv_id=parent_id)
		and code_subdiv not in ('300', '301', '302', '303', '304');
		
    {1}.GetSalaryIBMFormat(:p_date, P_SUBDIVS, :c);
end;