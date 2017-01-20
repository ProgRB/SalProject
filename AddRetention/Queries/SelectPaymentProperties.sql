select 
    payment_property_id,
    property_name,
    case   property_type_id 
        when 1 then property_value
        when 2 then property_value
        when 3 then  POSS_VALUE_VARCHAR
        when 4 then property_value
    end property_value,
    property_comment
from
    {1}.payment_property
    join {1}.PAYMENT_PROPERTY_REL using (payment_property_id)
    left join {1}.PROP_POSSIBLE_VALUE ppv  using (PAYMENT_PROPERTY_ID)
where 
    payment_calc_relation_id=:p_relation_id
    and (ppv.PROP_POSSIBLE_VALUE_ID is null or property_value=POSS_VALUE_NUMBER)
    