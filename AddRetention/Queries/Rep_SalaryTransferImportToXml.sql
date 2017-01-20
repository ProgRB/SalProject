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
       XMLElement( "СчетаПК",
            XMLAttributes( 
                    sysdate as "ДатаФормирования",
                    '09160017' as "НомерДоговора",
                    'АО У-УАЗ' as "НаименованиеОрганизации",
                    '0323018510' as "ИНН",
                    (select max(CURRENT_ACCOUNT) keep (dense_rank FIRST order by BANK_OFFICE) FROM {1}.TYPE_BANK WHERE TRN=:p_TRN and custom_sign=0) as "РасчетныйСчетОрганизации",
                    ' ' as "ИдПервичногоДокумента",
                    (select CARTULARY_NUM from {1}.CARTULARY where cartulary_id=:p_cartulary_id) as "НомерРеестра",
                    (select DATE_CLOSE_CART from {1}.CARTULARY where cartulary_id=:p_cartulary_id) as "ДатаРеестра"
                 ),   
                        (select XMLElement("ЗачислениеЗарплаты", 
                            XMLAgg(
                                  XMLElement("Сотрудник",  
									XMLAttributes(rownum as "Нпп"),
                                    XMLForest(trim(owner_family) as "Фамилия", 
                                                    trim(owner_name) as "Имя",
                                                    trim(owner_middle_name) as "Отчество",
                                                    '8601' as "ОтделениеБанка",
                                                    '8601' as "ФилиалОтделенияБанка",
                                                    number_account as "ЛицевойСчет",
                                                    sum_sal as "Сумма")
                                      )
                                 )       
                              )
                           from v_cartulary
                        ),
                        xmlElement("ВидЗачисления", '01'),
                        xmlElement("ПлатежноеПоручение", ' '),
                        xmlElement("ДатаПлатежногоПоручения", sysdate),
                        xmlElement("КонтрольныеСуммы", (select xmlForest(count(distinct number_account) as "КоличествоЗаписей", sum(sum_sal) as "СуммаИтого") from v_cartulary))    
         ), VERSION '1.0" encoding="windows-1251', STANDALONE YES) 
                as CLOB VERSION '1.0' INDENT SIZE=4 ) 
                as XML
from dual