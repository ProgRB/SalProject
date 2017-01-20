select copy_retention_id, subdiv_id, code_subdiv, retention_id
from {1}.COPY_RETENTION
	join {0}.subdiv using (subdiv_id)
where 
	retention_id=:p_retention_id