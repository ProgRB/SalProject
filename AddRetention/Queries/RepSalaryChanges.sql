select 
	time_add_record date_begin,
	user_name per_num,
	pay_date,
	code_payment,
	sum_sal,
	hours,
	days,
	code_subdiv
from 
	TABLE({1}.SALARY_PKG.GetSalaryChangesDays(:p_payment_type_ids, :p_subdiv_id, :p_pay_date_begin, :p_pay_date_end, :p_date1, :p_date2))
	join {1}.PAYMENT_TYPE using (payment_type_id)
	join {0}.subdiv using (subdiv_id)