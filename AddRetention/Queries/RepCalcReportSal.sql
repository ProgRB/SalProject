select *
from
    (select  
        worker_id,
        max(DATE_ERROR) keep(dense_rank last order by calc_sal_report_id) date_begin,  
        emp_last_name ||' '||substr(emp_first_name,1,1)||'.'||substr(emp_middle_name,1,1)||'.' fio1, 
		code_subdiv,
        per_num,
        code_payment,
        order_number,
        max(EX_MESSAGE) keep(dense_rank last order by calc_sal_report_id)  TEXT_MESSAGE,
        max(sal_report_type_id) keep (dense_rank last order by CALC_SAL_REPORT_ID) type_report_id,
        ret_sum,
        percent/100 percent
      from
        {1}.calc_sal_report csr
        join {1}.sal_report_type using (sal_report_type_id)
        left join 
            (select retention_id, payment_type_id, 
                order_number,
                case when sign_individual=1 then r.retent_sum else rcm.sum_retent end as ret_sum,
                case when sign_individual=1 then r.RETENT_PERCENT else rcm.PERCENT_RETENT end as PERCENT ,
				date_start_ret, date_end_ret
              from
                {1}.retention r 
                left join ( select payment_type_id, sign_individual, sum_retent,percent_retent 
                            from  {1}.payment_type join {1}.payment_calc_relation using (payment_type_id)
                                join  {1}.retent_calc_method using (retent_calc_method_id)
                       where :p_date between date_start_calc and date_end_calc ) rcm using (payment_type_id)
             ) r  on (r.retention_id=CSR.RETENTION_ID)            
        left join {1}.payment_type pt on (pt.payment_type_id=r.payment_type_id)
        join {0}.transfer t on (t.transfer_id=csr.table_id) 
		join apstaff.subdiv using (subdiv_id)
        join {0}.emp using (Per_num)
      where  
		(sal_report_type_id in (5, 10) or sal_report_type_id=2 and trunc(:p_date,'month') between trunc(nvl(date_start_ret,date'1000-01-01'),'month') and nvl(date_end_ret, date'3000-01-01'))
        and calc_date between trunc(:p_date,'month') and add_months(trunc(:p_date,'month'),1)-1/86400
        and worker_id in (select worker_id from {0}.transfer where transfer_id in 
                    (select transfer_id from {1}.view_salary_by_subdiv where subdiv_id in (select subdiv_id from {0}.subdiv 
                        start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) and pay_date between trunc(:p_date,'month') and add_months(trunc(:p_date,'month'),1)-1/86400))
      group by worker_id, code_subdiv,  csr.retention_id, emp_last_name, emp_first_name, emp_middle_name, per_num,code_payment, order_number, ret_sum, percent
    ) a
    where type_report_id in (2,5) 