declare
begin
	open :c1 for select * from {1}.REPORT_GROUP where report_group_id=:p_report_group_id;
	open :c2 for select DECODE(REPORT_SETTING_ID,null, 0, 1) USE_FOR_CALC,  REPORT_SETTING_ID, REPORT_GROUP_ID, 
					PAYMENT_TYPE_ID, GROUP_SORT_ORDER, code_payment, name_payment, type_payment_type_id
					from {1}.payment_type 
					left join (select * from {1}.REPORT_SETTING  where report_group_id=:p_report_group_id)  using (payment_type_id);
	open :c3 for select report_group_id, group_code, group_name from {1}.report_group;
	open :c4 for select NVL2(REPORT_SETTING_SUBDIV_ID, 1, 0) FL,  REPORT_SETTING_SUBDIV_ID, subdiv_id, code_subdiv, subdiv_name, report_group_id, sub_date_start, sub_date_end
		from 
			(select * from apstaff.subdiv where nvl(sub_date_end, date'3000-01-01')>date'2011-01-01' and parent_id=0 and subdiv_id!=0 and code_subdiv is not null start with subdiv_id=0 connect by prior subdiv_id=parent_id)
			left join (select * from {1}.report_setting_subdiv where report_group_id=:p_report_group_id) using (subdiv_id);
	open :c5 for select * from {1}.REPORT_SETTING_ORDER where report_group_id=:p_report_group_id;
end;