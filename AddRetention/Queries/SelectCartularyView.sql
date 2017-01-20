select c.*, (select count(*) from salary.view_salary_transfer where cartulary_id=c.cartulary_id) ROW_COUNT
from {1}.cartulary c
where type_cartulary_id=:p_type_cartulary_id and 
	date_cartulary between trunc(:p_date,'month') and add_months(trunc(:p_date,'month'),1)-1/86400
	and cartulary_subdiv_id in (select subdiv_id from apstaff.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id)
order by date_cartulary desc