begin
    open :c for
    q'[
        with function ff(k number, cnt number, dec_part  number := 2) return varchar is
            begin
                return to_char(nvl(k,0)*power(10, dec_part), 'S'||lpad('0', cnt, '0'));
            end;
            select 
                ff(code_subdiv, 3, 0)||
                ff(order_name, 13, 0) || 
                ff(code_operation, 1, 0)||
                ff(pay_type_id, 3, 0)||
                ff(sum(hours), 7, 1) ||-- hours
                ff(sum(sum_sal), 11, 2) ||
                ff(DEGREE_SIGN, 1, 0) ||
                ff(1, 1, 0) || -- признак ширпотреба
                ff(MAX(SORT_NUMBER), 1, 0) ||
                ff(nvl(code_degree, '00'), 2, 0)
            from {1}.salary_addition
                join {0}.SUBDIV using (subdiv_id)
                join {0}.orders using (order_id)
                join {1}.type_operation using (type_operation_id)
                join {1}.payment_type using (payment_type_id)
                left join {0}.degree using (degree_id)
            where calc_date= trunc(:p_date, 'month')
				and type_payment_Type_id!=2
            group by code_subdiv, order_name, code_operation, pay_type_id, code_degree, degree_sign
            order by code_subdiv, order_name, pay_type_id]' using :p_date;
end;