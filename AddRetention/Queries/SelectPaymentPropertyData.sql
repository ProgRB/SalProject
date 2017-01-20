begin
	open :c1 for select * from {1}.payment_property;
	open :c2 for select * from {1}.property_area;
	open :c3 for select * from {1}.prop_possible_value;
	open :c4 for select * from {1}.type_payment_type;
	open :c5 for select * from {1}.property_type;
end;