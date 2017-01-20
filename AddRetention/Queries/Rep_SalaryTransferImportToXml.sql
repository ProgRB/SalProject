with v_cartulary as 
(
    select owner_family, owner_name, owner_middle_name, number_account, 
        sum(sum_sal) sum_sal        
    from 
        {1}.view_SALARY_transfer
        join {0}.transfer using (transfer_id)
        join {1}.client_account using (client_account_id)
        join {1}.type_bank using (type_bank_id)
    where
        cartulary_id = :p_cartulary_id
        and nvl(custom_sign,0)=0
        and TRN=:p_TRN
    group by number_account, OWNER_FAMILY, OWNER_NAME, OWNER_MIDDLE_NAME
    having sum(sum_sal)!=0
    order by owner_family, owner_name, owner_middle_name
)
select 
    XMLSERIALIZE(DOCUMENT
       XMLRoot(
       XMLElement( "�������",
            XMLAttributes( 
                    sysdate as "����������������",
                    '09160017' as "�������������",
                    '�� �-���' as "�����������������������",
                    '0323018510' as "���",
                    (select max(CURRENT_ACCOUNT) keep (dense_rank FIRST order by BANK_OFFICE) FROM {1}.TYPE_BANK WHERE TRN=:p_TRN and custom_sign=0) as "������������������������",
                    ' ' as "���������������������",
                    (select CARTULARY_NUM from {1}.CARTULARY where cartulary_id=:p_cartulary_id) as "������������",
                    (select DATE_CLOSE_CART from {1}.CARTULARY where cartulary_id=:p_cartulary_id) as "�����������"
                 ),   
                        (select XMLElement("������������������", 
                            XMLAgg(
                                  XMLElement("���������",  
									XMLAttributes(rownum as "���"),
                                    XMLForest(trim(owner_family) as "�������", 
                                                    trim(owner_name) as "���",
                                                    trim(owner_middle_name) as "��������",
                                                    '8601' as "��������������",
                                                    '8601' as "��������������������",
                                                    number_account as "�����������",
                                                    sum_sal as "�����")
                                      )
                                 )       
                              )
                           from v_cartulary
                        ),
                        xmlElement("�������������", '01'),
                        xmlElement("������������������", ' '),
                        xmlElement("�����������������������", sysdate),
                        xmlElement("����������������", (select xmlForest(count(distinct number_account) as "�����������������", sum(sum_sal) as "����������") from v_cartulary))    
         ), VERSION '1.0" encoding="windows-1251', STANDALONE YES) 
                as CLOB VERSION '1.0' INDENT SIZE=4 ) 
                as XML
from dual