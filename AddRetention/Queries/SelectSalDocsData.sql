begin
	open :c1 for select * from {1}.TYPE_SAL_DOCUM where vac_sign=1 order by type_sal_docum_id;
end;