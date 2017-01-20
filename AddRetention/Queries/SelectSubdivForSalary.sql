select 
	SUBDIV_FOR_CLOSE_ID, 
	SUBDIV_ID, 
	add_months(trunc(date_closing,'month'),1)-1/86400 as date_closing, 
	APP_NAME,
	DATE_CHANGE, 
	LAST_DATE_PROCESSING,
	SUB_DATE_START, SUB_DATE_END
from {1}.SUBDIV_FOR_CLOSE 
	join {0}.SUBDIV using (subdiv_id)
where APP_NAME=:p_APP_NAME