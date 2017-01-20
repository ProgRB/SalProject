select * 
from 
	{1}.TYPE_DISCOUNT
where 
	:p_date between nvl(date_start, date'1000-01-01') and nvl(date_end, date'3000-01-01')
order by code_disc