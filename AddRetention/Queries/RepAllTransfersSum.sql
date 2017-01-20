declare
    t {1}.number_collection_type;
begin
    select payment_type_id bulk collect into t 
    from
        {1}.report_group
        join {1}.report_setting using (report_group_id)
    where group_code='0031Y';
    {1}.{1}_REPORTS.SELECT{1}BYPAYMENTTYPE(0, :p_date, :p_date, t, :c); 
end;