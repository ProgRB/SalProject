select 
	a1
from
(
	select code_subdiv||per_num||sum_sal||p1||p2||p3||fio||p4||sign_comb a1
	from
	(select 
		lpad(code_subdiv,4,'0') code_subdiv,
		lpad(per_num, 6, '0') per_num,
		to_char(NVL(sum(sum_sal),0)*100, 'FM000000000000') sum_sal,
		'000000000000' p1,
		'000000000000' p2,
		'000000000000' p3,
		rpad(emp_last_name||' '||substr(emp_first_name,1,1)||' '||substr(emp_middle_name,1,1),22,' ') FIO,
		'0' p4,
		decode(sign_comb,1,'2',' ') sign_comb,
		count(per_num) over (partition by code_subdiv) rn
	from
		{1}.view_salary_transfer vst
		join apstaff.transfer using (transfer_id)
		join apstaff.emp using (per_num)
		join apstaff.subdiv s on (vst.subdiv_id=s.subdiv_id)
	where cartulary_id = :p_cartulary_id
	group by code_subdiv, per_num, emp_last_name, emp_first_name, emp_middle_name, sign_comb
	having sum(sum_sal)!=0
	order by code_subdiv, per_num)
	where rn>1
)
union all
select chr(26) from dual