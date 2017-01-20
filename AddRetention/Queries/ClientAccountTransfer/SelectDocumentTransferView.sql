select
	* 
from
	SALARY.DOCUM_TRANSFER
where
	type_cartulary_id=:p_type_cartulary_id
	and date_docum between :p_date_begin and :p_date_end
