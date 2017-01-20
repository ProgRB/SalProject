select order_id, 
	payment_type_id,
	code_subdiv, order_name, type_operation_id, code_payment, code_degree, hours, sum_sal 
from {1}.salary_addition
    join {1}.payment_type using (payment_type_id)
    left join {0}.orders using (order_id)
    left join {0}.degree using (degree_id)
    join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
where
    calc_date=trunc(:p_date,'month')
order by code_subdiv, order_name, code_payment