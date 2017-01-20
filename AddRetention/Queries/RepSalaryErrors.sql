declare
    errors_table {1}.salary_ERROR_COLLECTION_TYPE;
begin
    select value(p) bulk collect into errors_table
    from 
    TABLE({1}.GET_{1}_ERRORS(:p_subdiv_id, :p_date, :p_ids)) p;
    
    /* ������� ���� ������ ������� */
    open :c1 for select code_subdiv, per_num, emp_last_name||' '||substr(emp_first_name, 1,1)||'. '||substr(emp_middle_name,1,1)||'.' fio1, code_payment
    from 
        (select * from  table(errors_table) where salary_error_type_id in (1,2))
        join {0}.transfer using (transfer_id)
		join {0}.subdiv using (subdiv_id)
        join {0}.emp using (per_num)
        left join {1}.payment_type using (payment_type_id);
    
    /* ������ ���� ������ ��������� */
    open :c2 for select code_subdiv, per_num, emp_last_name||' '||substr(emp_first_name, 1,1)||'. '||substr(emp_middle_name,1,1)||'.' fio1, code_payment, sum_sal
    from 
        (select * from  table(errors_table) where salary_error_type_id in (3))
        join {0}.transfer using (transfer_id)
		join {0}.subdiv using (subdiv_id)
        join {0}.emp using (per_num)
        left join {1}.payment_type using (payment_type_id);
   
    /* ������  �� ������ �������� ���������  */     
   open :c3 for select code_subdiv, per_num, emp_last_name||' '||substr(emp_first_name, 1,1)||'. '||substr(emp_middle_name,1,1)||'.' fio1, code_payment, sum_sal
    from 
        (select * from  table(errors_table) where salary_error_type_id in (4))
        join {0}.transfer using (transfer_id)
		join {0}.subdiv using (subdiv_id)
        join {0}.emp using (per_num)
        left join {1}.payment_type using (payment_type_id); 
    
    /*   ������ ������������ - ���������� �� ���� � �����������*/ 
    open :c4 for select code_subdiv, per_num, emp_last_name||' '||substr(emp_first_name, 1,1)||'. '||substr(emp_middle_name,1,1)||'.' fio1, sum_sal, code_payment
    from 
        (select * from  table(errors_table) where salary_error_type_id in (5))
        join {0}.transfer using (transfer_id)
		join {0}.subdiv using (subdiv_id)
        join {0}.emp using (per_num)
         left join {1}.payment_type using (payment_type_id);
    
    /* ������ ������������ ��� ����� �������� - �� ���������� ������������ �� �� ���� �� 287, 487, 488 */
    open :c5 for select code_subdiv, per_num, emp_last_name||' '||substr(emp_first_name, 1,1)||'. '||substr(emp_middle_name,1,1)||'.' fio1, action_date as date_begin
    from 
        (select * from  table(errors_table) where salary_error_type_id in (6))
        join {0}.transfer using (transfer_id)
		join {0}.subdiv using (subdiv_id)
        join {0}.emp using (per_num);
	
	/* ������ ������ 620 �� ������ ���� ����� 401 �.�. */
	open :c6 for select code_subdiv, per_num, emp_last_name||' '||substr(emp_first_name, 1,1)||'. '||substr(emp_middle_name,1,1)||'.' fio1, sum_sal
    from 
        (select * from  table(errors_table) where salary_error_type_id in (7))
        join APSTAFF.transfer using (transfer_id)
		join {0}.subdiv using (subdiv_id)
        join APSTAFF.emp using (per_num);

	/* ������ � ������ ���������� ����� */
	open :c7 for select code_subdiv, per_num, emp_last_name||' '||substr(emp_first_name, 1,1)||'. '||substr(emp_middle_name,1,1)||'.' fio1, sum_sal,
		code_payment, retention_id as code_degree
    from 
        (select * from  table(errors_table) where salary_error_type_id in (8))
        join APSTAFF.transfer using (transfer_id)
		join {0}.subdiv using (subdiv_id)
		join {1}.payment_type using (payment_type_id)
        join APSTAFF.emp using (per_num);
end;