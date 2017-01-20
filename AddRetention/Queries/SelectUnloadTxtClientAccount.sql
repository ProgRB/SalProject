declare
 l_date_begin Date := trunc(:p_date,'month');
 l_date_end date := add_months(trunc(:p_date,'month'),1)-1/86400;
begin
    open :c1 for 
		select pm||pg||code_subdiv||per_num||sign_comb||code_payment||order_number||retent_percent||retent_sum||dd a
		from
        (select to_char(:p_date, 'mm') pm,
            to_char(:p_date,'yyyy') pg,
            code_subdiv,
            t2.per_num,
            decode(t2.sign_comb,1,'2',' ') sign_comb,
            code_payment,
            nvl(order_number,1) order_number,
            to_char(nvl(retent_percent,0), 'FM000') retent_percent,
            to_char(round(nvl(case when retent_percent>0 then 0 else retent_sum end ,0)*100), 'FM00000000000') retent_sum,
            to_char(sysdate,'DD') dd
        from
            salary.retention 
            join salary.payment_type using (payment_type_id)
            join apstaff.transfer t1 using (transfer_id)
            join (select worker_id, subdiv_id, per_num, sign_comb from
                    ( select worker_id, subdiv_id, per_num, sign_comb, trunc(date_transfer) date_transfer,
                        lead(trunc(date_transfer)-1/86400, 1, date'3000-01-01') over (partition by worker_id order by date_transfer) end_transfer
                        from 
                        apstaff.transfer
                    ) where  date_transfer<=l_date_end and add_months(end_transfer,3)>=l_date_begin
                   group by worker_id, subdiv_id, per_num, sign_comb) t2 using (worker_id)
            join apstaff.subdiv ss on (ss.subdiv_id=t2.subdiv_id)
        where date_start_ret<=l_date_end and nvl(date_end_ret, date'3000-01-01')>=l_date_begin
            and payment_type_id=(select payment_type_id from salary.payment_type where code_payment=:p_code_payment)
            and t2.per_num<'79000'
        group by t2.per_num, t2.sign_comb, code_subdiv, code_payment, nvl(order_number,1), retent_percent, retent_sum
        order by code_subdiv, t2.per_num, t2.sign_comb, nvl(order_number,1)
		);
end;
    