select
	*
from
	{1}.cartulary c
	join salary.type_cartulary using (type_cartulary_id)
where
	trunc(date_cartulary,'month')=trunc(:p_date,'month')
	and type_group_cartulary_id=3
	and exists(select 1 from {1}.VIEW_SALARY_SUBDIV_TRANSFER where cartulary_id=c.cartulary_id 
					and subdiv_id in (select subdiv_id from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id))
order by date_create desc