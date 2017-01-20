declare
    l_date_begin date := to_date('01'||to_char(trunc((extract(month from :p_date)-1)/3)*3+1, 'FM00')||extract(year from :p_date), 'ddmmyyyy'); --берем начало квартала
    l_date_end Date := add_months(l_date_begin, 3)-1/86400;
    p_tax_company_id number := :p_tax_company_id;
begin
    open :c for 
    select 
        code_subdiv "Подразделение",
        per_num "Таб.№",
        to_char(pay_date, 'mm')  "Месяц",
        inn "Инн",
        tax_percent "ставка",
        pay_code "Код дохода",
        sum(sum_sal)  "Сумма дохода"
    from
        salary.tax_emp_docum
        join apstaff.emp using (per_num)
        left join (select tax_emp_docum_id, pay_date, pay_code,  sum(sum_sal) sum_sal, sum(sum_disc) sum_discount, 0 as sum_retent from salary.tax_docum_payment group by tax_emp_docum_id, pay_date, pay_code
                   ) using (tax_emp_docum_id)
        join apstaff.per_data using (per_num)
        join (select per_num, max(code_subdiv) keep (dense_rank last order by date_transfer) code_Subdiv from apstaff.transfer join apstaff.subdiv using (subdiv_id) group by per_num) using (per_num)
    where
        docum_date between l_date_begin and l_date_end 
        and tax_company_id = p_tax_company_id
    group by tax_emp_docum_id, code_subdiv, per_num, pay_date, inn, tax_percent, pay_code
    order by code_subdiv, per_num;
end;