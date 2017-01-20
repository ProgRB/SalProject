select MAX(M.MESSAGE_ID) OVER() MAX_MESSAGE_ID, 
	ROW_NUMBER() over (order by date_message desc) MESSAGE_NUMBER,
    M.CONTENT_MESSAGE, M.DATE_MESSAGE, MESSAGE_ID
from {0}.MESSAGE M
where UPPER(M.APP_NAME) = UPPER(:p_APP_NAME) and 
    ((M.MESSAGE_ID > :p_MESSAGE_ID and :p_MESSAGE_ID is not null)
    or
    (M.DATE_MESSAGE between trunc(:p_begin_date) and trunc(:p_end_date)+86399/86400))
ORDER BY DATE_MESSAGE DESC