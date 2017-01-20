select subdiv_id, code_subdiv, subdiv_name, 
nvl(to_char(sub_date_start,'DD-MM-YYYY'),'<нет даты>') as sub_date_start, nvl(to_char(sub_date_end,'DD-MM-YYYY'),'<нет даты>') as sub_date_end 
from {0}.subdiv
where parent_id=0