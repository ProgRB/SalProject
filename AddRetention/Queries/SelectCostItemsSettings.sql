begin
	open :c1 for select * from {1}.type_cost_item;
	open :c2 for select * from {1}.cost_item_setting;
end;