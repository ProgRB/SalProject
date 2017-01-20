declare
begin
	open :c1 for select SALARY_DOC_ID, DOC_NAME, DOC_CODE, DOC_DATE from {1}.SALARY_DOC where transfer_id in 
				(select transfer_id from {0}.transfer where worker_id=(select worker_id from {0}.transfer where transfer_id=:p_transfer_id)) ;
end;