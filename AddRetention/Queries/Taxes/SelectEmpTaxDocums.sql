declare
	l_date_begin Date := to_date('01'||to_char(trunc((extract(month from :p_date)-1)/3)*3+1, 'FM00')||extract(year from :p_date), 'ddmmyyyy'); --берем начало квартала
	l_date_end Date := add_months(l_date_begin, 3)-1/86400;
begin
	open :c for
        select 
            TAX_EMP_DOCUM_ID,
            PER_NUM,
            EMP_LAST_NAME,
            EMP_FIRST_NAME,
            EMP_MIDDLE_NAME,
            DOCUM_SIGN,
            TAX_PERCENT,
            sum_sal,
            sum_discount,
            decode(lock_sign, 1, 'X') lock_sign,
            retent_tax,
            calced_tax,
            nvl(retent_tax,0)-nvl(calced_tax,0) as differ_tax,
            docum_date
        from 
            SALARY.TAX_EMP_DOCUM
            left join APSTAFF.EMP USING (PER_NUM)
            left join (select tax_emp_docum_id, sum(SUM_SAL) sum_sal from salary.tax_docum_payment group by tax_emp_docum_id) using (tax_emp_docum_id)
            left join (select tax_emp_docum_id, sum(SUM_DISCOUNT) sum_discount from salary.tax_docum_discount group by tax_emp_docum_id) using (tax_emp_docum_id)
        where TAX_COMPANY_ID = :p_TAX_COMPANY_ID
            and docum_date between l_date_begin and l_date_end
			and (:p_percent is null or  tax_percent = :p_percent)
		order by per_num;
end;