select * 
from {1}.SALARY_DOCUM
where 
	transfer_id in (select transfer_id from apstaff.transfer 
					where worker_id=(select worker_id from apstaff.transfer where transfer_id=:p_transfer_id))
	and doc_begin>=add_months(:p_date, -12)
	and salary_docum_id != nvl(:p_salary_docum_id,-1)