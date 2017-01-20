declare
begin
	open :c1 for select * from salary.docum_transfer where docum_transfer_id = :p_docum_transfer_id;
	open :c2 for select * from salary.v_docum_transfer_relation where docum_transfer_id= :p_docum_transfer_id;
end;