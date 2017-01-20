begin
 open :c for 
	 select 
		salary_docum_id,
		CODE_DOC, NAME_DOC, 
		DATE_DOC, 
		DOC_BEGIN,
		DOC_END,
		DATE_CLOSE, 
		TYPE_SAL_DOC_NAME, 
		CODE_SUBDIV,   
		LAST_CALC_DATE
	from 
		{1}.SALARY_DOCUM sd
		join {1}.TYPE_SAL_DOCUM tsd on (sd.TYPE_sal_docum_id=tsd.TYPE_sal_docum_id)
		join {0}.subdiv s on (s.subdiv_id=doc_subdiv_id)
	where 
		transfer_id in (select transfer_id from {0}.transfer where worker_id=(select worker_id from {0}.transfer where transfer_id=:p_transfer_id))
		and vac_sign=0
        and 
            (date_doc between trunc(:p_date,'month') and add_months(trunc(:p_date,'month'),1)-1/86400
            or 
             doc_begin<=add_months(trunc(:p_date,'month'),1)-1/86400 and doc_end>=trunc(:p_date,'month')
             and exists( select 1 from {1}.view_payment join {1}.TYPE_DOCUM_PAY_CALC using (payment_type_id) where type_sal_docum_id=sd.type_sal_docum_id and sysdate between date_start_calc and date_end_calc 
                                and type_calc_period_id=3)
            );
end;