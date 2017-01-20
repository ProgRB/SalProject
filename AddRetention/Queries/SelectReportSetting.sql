declare
begin
	open :c1 for
		select
			report_setting_id,
			code_payment,
			NAME_PAYMENT
		from {1}.REPORT_SETTING join {1}.payment_type using (payment_type_id)
		where report_group_id=:p_report_group_id;
	open :c2 for select subdiv_id, code_subdiv, subdiv_name from {1}.REPORT_SETTING_SUBDIV join {0}.subdiv using (subdiv_id) where report_group_id=:p_report_group_id;
	open :c3 for select * from {1}.REPORT_SETTING_ORDER where report_group_id=:p_report_group_id;
end;