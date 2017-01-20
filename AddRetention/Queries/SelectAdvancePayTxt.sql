select code_subdiv||per_num||sum_sal||p1||p2||p3||fio||p4||sign_comb, sum_number as sum_sal
from
(select 
    lpad(code_subdiv,4,'0') code_subdiv,
    lpad(per_num, 6, '0') per_num,
	sum(sum_sal) as sum_number,
    to_char(NVL(sum(sum_sal),0)*100, 'FM000000000000') sum_sal,
    '000000000000' p1,
    '000000000000' p2,
    '000000000000' p3,
    rpad(emp_last_name||' '||substr(emp_first_name,1,1)||' '||substr(emp_middle_name,1,1),22,' ') FIO,
    '0' p4,
    decode(sign_comb,1,'2',' ') sign_comb
from
    {1}.view_salary_by_subdiv vst
    join {0}.emp using (per_num)
    join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id
			and code_subdiv!='039') s using (subdiv_id)
    join {1}.payment_type using (payment_type_id)
where trunc(pay_date,'month')=trunc(:p_date,'month')
    and code_payment='271'
group by code_subdiv, per_num, emp_last_name, emp_first_name, emp_middle_name, sign_comb
having sum(sum_sal)!=0
order by code_subdiv, per_num)
union all 
select chr(26), 0 from dual