declare
begin
    open :c for
        select 
            *
        from
            (
                select code_subdiv as "Подразделение", order_name "Заказ", type_operation_id as "Опция",  TYPE_DISTR_CODE, dist_value
                from 
                    {1}.salary_distribution
                    join (select subdiv_id, code_subdiv from {0}.subdiv start with subdiv_id=:p_subdiv_id connect by prior subdiv_id=parent_id) using (subdiv_id)
                    join {0}.orders using (order_id)
                    join {1}.type_salary_distr_code using (type_salary_distr_code_id)
                where
                    calc_date=trunc(:p_date,'month')
            )
       pivot
       (
            sum(dist_value)
            for type_distr_code in ('H101' "Часы нарядов", 'S101' "Сумма нарядов", 'H102' "Часы 102", 'S102' "Сумма 102", 'S105' "Сумма премии", 'S122' "Сумма команд-ые", 'S239' "Сумма поясной кф-т", 'S300' "Сумма за стаж", 
                    'SDOP' "Прочая ЗП", 'S6905' "Взносы б/с 6905", 'S6902' "Взносы б/с 6902", 'S6901' "Взносы б/с 6901", 'S6904' "Взносы б/с 6904", 'S6907' "Взносы б/с 6907", 'S6906' "Взносы б/с 6906",
					'RES226' "Резерв отпуска", 'RES69_VAC' "Резерв взносов на отпуск",
					'RES169' "Резерв кв-ой премии", 'RES69_QUAR' "Резерв взносов на кврт.")
       )
       order by "Подразделение", "Заказ";
end;
    