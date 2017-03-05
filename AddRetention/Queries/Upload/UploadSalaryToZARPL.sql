declare
    p_date date :=:p_date; 
    pt_ids salary.number_collection_Type;
    l_date_begin date := trunc(p_date,'month');
    l_date_end date :=  add_months(trunc(p_date, 'month'),1)-1/86400;
begin
    select payment_type_id bulk collect into pt_ids 
    from salary.payment_type pt 
        left join (select payment_calc_relation_id, payment_type_id from  salary.payment_calc_relation where date'2014-06-01' between date_start_calc and date_end_calc) pcr using (payment_type_id) 
        where NVL((select to_number(property_value)
                          from 
                            salary.payment_property_rel
                            join salary.payment_property using (payment_property_id)
                          where payment_calc_relation_id = pcr.payment_calc_relation_id
                          and payment_property_id=4),0)=0  
        or code_payment in ('239', '310', '320', '330');
open :c for
select 
	extract(month from p_date) pm,
	extract(year from p_date) pg,
        code_subdiv as podr,
        type_row_salary_id as PTN,
        ZN as ZN,
        PAY_TYPE_ID as VOP,
        hours HCAS,
        nvl(sum_sal,0) sum_z,
        nvl(zone_add,0) po,
        nvl(exp_add,0) nad,
        0 nal,
        nvl(order_name,lpad('0',13,'0')) as ZAK,
        nvl(group_master, lpad(' ',3)) as GM,
        nvl(code_degree,'  ') as KAT,
        0 pfmp
from
( select code_subdiv,
    null per_num,
    type_row_salary_id,
    zn,
    pay_type_id,
    sum(hours) hours,
    round(nvl(sum(sum_sal),0),2) as sum_sal,
    round(nvl(sum(zone_add),0),2) as zone_add,
    round(sum(nvl(exp_add,0)),2) exp_add,
    ORDER_NAME,
    group_master,
    code_degree,
    sum(yadn) yadn
    from
    ( 
        select code_subdiv,
           nvl(type_row_salary_id,0) as type_row_salary_id,
           DECODE(TYPE_PAYMENT_SIGN, -1, '9', '1') ZN,
           NVL(to_char(pay_type_id),code_payment) pay_type_id,
           round(NVL(case when consider_type_id=2 then days else case when CODE_PAYMENT='536' then NVL(hours, DAYS) else HOURS end end ,0), 1) as hours,
           sum_sal,
           zone_add,
           exp_add,      
           ORDER_NAME,
           group_master,
           code_degree,
           case when NVL(to_char(pay_type_id),code_payment)='101' and row_number() over (partition by s.transfer_id, s.order_id, s.group_master order by salary_id)=1 then
                (select sum(COUNT_YN) from salary.salary_from_table where end_period between trunc(p_date,'month') and add_months(trunc(p_date,'month'),1)-1/86400 
                and transfer_id=s.transfer_id and pay_type_id=101 and order_id=s.order_id and group_master=s.group_master)
           else null end yadn
        from 
            salary.salary s
            join salary.payment_type pt using (payment_type_id)
            join salary.type_payment_type using (type_payment_type_id)
            join apstaff.subdiv sb on (s.subdiv_id=sb.subdiv_id)
            left join apstaff.orders ord on (s.order_id=ord.order_id)
            left join apstaff.degree d on (s.degree_id=d.degree_id)
        where code_subdiv not in ('300', '301', '302', '303', '304')
            and payment_type_id in (select column_value from table(pt_ids))
            and pay_date between l_date_begin and l_date_end
            and type_payment_type_id=1 
            and (code_payment not in ('239', '300', '310', '320', '330') or account_add_sign=1)
    ) t1
    group by code_subdiv, type_row_salary_id, zn, pay_type_id, ORDER_NAME, group_master, code_degree
)
order by code_subdiv, pay_type_id, type_row_salary_id desc, code_degree, order_name, hours
;
end;



