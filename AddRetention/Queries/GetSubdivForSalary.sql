select SUBDIV_FOR_SALARY_ID, subdiv_id, code_subdiv, subdiv_name, parent_id,
	sub_date_start, sub_date_end 
from {1}.subdiv_for_salary
	join {0}.subdiv using (subdiv_id)
