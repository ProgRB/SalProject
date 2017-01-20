begin
	open :c1 for select * from {1}.PAYMENT_CALC_RELATION where PAYMENT_CALC_RELATION_ID=:p_PAYMENT_CALC_RELATION_ID;
	open :c2 for select RETENT_CALC_METHOD_ID, METHOD_NAME from {1}.RETENT_CALC_METHOD;
	open :c3 for select * from {1}.TYPE_AVG_PROPORTION;
	open :c4 for select * from {1}.TYPE_REF_SALARY;
	open :c5 for select * from {1}.PAYMENT_PROPERTY_REL where payment_calc_relation_id = :p_PAYMENT_CALC_RELATION_ID;
end;