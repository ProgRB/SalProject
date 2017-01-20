select
    '0'||
    code_subdiv||
    '9879'||
    decode(sign_comb,1,'2',' ')||
    '987'||
    lpad(' ',15,' ')||
    per_num||
    lpad(to_char(sum(sum_sal)*100,'FM99999999999'), 18, '0')||
    lpad(' ', 7, ' ') 
from 
{1}.view_salary_transfer
join {0}.subdiv using (subdiv_id)
join (select transfer_id, per_num, sign_comb from {0}.transfer) using (transfer_id)
where cartulary_id=:p_cartulary_id
group by code_subdiv, per_num, sign_comb, client_account_id
having sum(sum_sal)!=0
order by code_subdiv, per_num, sign_comb