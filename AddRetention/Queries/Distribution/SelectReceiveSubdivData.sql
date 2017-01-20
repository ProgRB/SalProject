begin
	open :c1 for select * from {1}.SAL_SUBDIV_RECEIVE where SAL_SUBDIV_RECEIVE_ID =:p_SAL_SUBDIV_RECEIVE_ID;
end;
