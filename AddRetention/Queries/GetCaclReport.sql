select CALC_SAL_REPORT_ID, 
	csr.sal_report_type_id, 
	per_num,
	date_error,
    ex_message,
    report_message,
	order_state
from {1}.calc_sal_report csr 
join {1}.sal_report_type srt on (csr.sal_report_type_id=srt.sal_report_type_id)
left join {0}.transfer t on (t.transfer_id=csr.table_id and csr.sal_report_type_id not in (6,7))
where DATE_ERROR>=(select max(date_error) from {1}.calc_sal_report where sal_report_type_id=8)
and csr.sal_report_type_id not in (1, 3, 7, 10)
order by calc_sal_report_id  