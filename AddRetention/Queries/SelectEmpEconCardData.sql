begin
	open :c1 for select per_num, sign_comb, t.transfer_id, worker_id as ID, code_degree, 
						pos_name, code_subdiv, emp_last_name, emp_first_name, emp_middle_name, 
						emp_sex, emp_birth_date, photo,
						(select min(date_transfer) from {0}.transfer where worker_id=t.worker_id) DATE_TRANSFER,
						(select max(case when type_transfer_id=3 then date_transfer else null end) from {0}.transfer where worker_id=t.worker_id) END_TRANSFER
				from 
					{0}.transfer  t
					join {0}.emp using (per_num)
					join {0}.subdiv using (subdiv_id)
					join {0}.degree using (degree_id)
					join {0}.position using (pos_id)
				where t.transfer_id=:p_transfer_id;
	open :c2 for select * from {0}.account_data where transfer_id=(select decode(type_transfer_id, 3, from_position, transfer_id) from {0}.transfer where transfer_id=:p_transfer_id);
end;