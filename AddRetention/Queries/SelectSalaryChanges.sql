select 
	extract(year from pay_date) "YEAR",
	to_char(extract(month from pay_date), 'FM00')
	||to_char(substr(extract(year from pay_date), 3,2),'FM00')
	||to_char(to_number(substr(code_subdiv,1,3)),'FM000')
	||to_char(per_num,'FM00000')
	||lpad(NVL(code_degree,' '),2,' ')
	||to_char(code_payment,'FM000')
	||to_char(NVL(case when consider_type_id=1 then HOURS
				when consider_type_id=2 then DAYS
				else NVL(hours, days) end,0)*10, 'S0000000')
	||to_char(NVL(sum_sal,0),'S00000000000')
	||to_char({1}.GetEmpNAL(code_payment, sign_comb, per_num, transfer_id, pay_date), 'FM00')
	||to_char(type_payment_type_id,'FM0')
	||decode(sign_comb,1,'2', ' ')
	SALARY_DATA
from 
	TABLE({1}.SALARY_PKG.GetSalaryChangesDays(:p_payment_type_ids, :p_subdiv_id, :p_pay_date_begin, :p_pay_date_end, :p_date1, :p_date2))
	left join {0}.degree using (degree_id)
	join {1}.PAYMENT_TYPE using (payment_type_id)
	join {0}.subdiv using (subdiv_id)