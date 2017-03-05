declare
    --p_tax_docum_ids salary.number_collection_Type := :p_tax_docum_ids;
begin
    open :c1 for 
    select 
        TAX_PERCENT percent,
        sum(sum_sal) as sum_sal1,
        nvl(sum(sum_sal),0) - nvl(sum(sum_discount1),0) - nvl(sum(sum_discount2), 0) as sum_sal2,
        sum(calced_tax) taxed_sum,
        sum(retent_tax) ret_sum 
    from
        salary.tax_emp_docum
        left join (select tax_emp_docum_id, sum(sum_sal) sum_sal, sum(sum_disc) as sum_discount1 from salary.tax_docum_payment group by tax_emp_docum_id) using (tax_emp_docum_id)
        left join (select tax_emp_docum_id, sum(sum_discount) sum_discount2 from salary.tax_docum_discount group by tax_emp_docum_id) using (tax_emp_docum_id)
    where tax_emp_docum_id in (select column_value from TABLE(:p_tax_docum_ids))
    group by tax_percent;
end;