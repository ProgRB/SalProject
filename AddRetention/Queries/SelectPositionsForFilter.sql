select code_pos, max(pos_name) keep (dense_rank last order by pos_actual_sign) pos_name
from 
{0}.position
group by code_pos