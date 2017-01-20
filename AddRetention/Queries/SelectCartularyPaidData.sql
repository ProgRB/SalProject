select
	c.cartulary_paid_id,
	c.cartulary_id,
	c.transfer_id,
	c.sum_sal as paid_sum,
	c.payment_type_id,
	c.client_account_id,
	c.salary_id,
	c.paid_comment,
	BCC_CODE,
	OKATO,
	EMP_LAST_NAME||' '||substr(emp_first_name,1,1)||'. '||substr(emp_middle_name,1,1)||'.' FIO,
	t.per_num
from {1}.view_salary_transfer c 
	join {0}.TRANSFER t on (c.transfer_id=t.transfer_id)
	join {0}.emp e on (t.per_num=e.per_num)
where cartulary_paid_id=:p_cartulary_paid_id