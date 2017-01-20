declare
    c1 sys_refcursor;
    xe XmlType;
begin
    {1}.SALARY_REPORTS.SELECTDUESSALARYHEAD(0, :p_date, c1);
    xe:=XmlType.CreateXML(c1);
    open :c for 
    select 
        code_subdiv||code_payment|| to_char(abs(nvl(sum(sum_sal), 0))*100, 'FM0000000000000')||case when sum(sum_sal)<0 then '1' else '0' end||'000000'
            from
            (
                select extractvalue(value(t), 'ROW/CODE_SUBDIV') code_subdiv, 
                         extractvalue(value(t), 'ROW/CODE_PAYMENT') code_payment,
                         to_number(extractvalue(value(t), 'ROW/SUM_SAL')) SUM_SAL,
                         extractvalue(value(t), 'ROW/GROUP_CODE') pay_code  
                from TABLE(XMLSequence(xe.extract('ROWSET/ROW'))) t
            )
            where pay_code='4'
                and code_subdiv not in ('039', '145', '139')
       group by code_subdiv, code_payment
       having sum(sum_sal)!=0
       order by code_subdiv, code_payment;
end;