select 
	per_num, 
	TRANSFER_ID, 
	emp_last_name||' '||substr(emp_first_name,1,1)||'.'||substr(emp_middle_name,1,1)||'. '||per_num as FIO,
	DECODE(sign_comb,1,'Совмещ.',' ') as sign_comb,
	CODE_SUBDIV
from 
	(select per_num, trunc(date_transfer) as date_transfer,
            DECODE(type_transfer_id,3,trunc(date_transfer)+86399/86400, 
                lead(trunc(date_transfer),1,date'3000-01-01') over(partition by per_num, sign_comb order by date_transfer)) as end_transfer,
                subdiv_id,
                type_transfer_id,
                degree_id,
                sign_cur_work,
                sign_comb,
                transfer_id
		from {0}.transfer 
	) t
	join {0}.emp using (per_num)
	join {0}.subdiv using (subdiv_id)
where (sign_cur_work=1 or type_transfer_id=3) and end_transfer>=add_months(trunc(sysdate,'month'),-12)
order by emp_last_name, emp_first_name