select
	*
from
	salary.type_cartulary
where
	type_cartulary_id in (select type_cartulary_id from salary.type_cartulary_access where APSTAFF.DB_HELPER.IS_ROLE_ENABLED(ROLE_NAME)>0 
		or role_name in (select role from session_roles )
		or role_name=ORA_LOGIN_USER)
