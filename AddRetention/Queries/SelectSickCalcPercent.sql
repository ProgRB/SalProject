select max(percent_value) keep (dense_rank last order by work_year) 
from 
	{1}.sick_calc_percent 
where work_year<=Greatest({1}.CALC_SICK_STANDING(:p_per_num, :p_date), 0.5)
