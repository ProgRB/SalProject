select * from (
    with Tr_per as 
        (select V.TRANSFER_ID, V.PER_NUM, V.DATE_TRANSFER, V.END_TRANSFER, V.SIGN_COMB, V.DEGREE_ID,V.POS_ID,V.SUBDIV_ID,WORKER_ID, FROM_POSITION
        from (
            SELECT transfer_id,per_num,WORKER_ID,FROM_POSITION, degree_id,POS_ID,subdiv_id,SIGN_COMB,trunc(date_transfer) date_transfer,
                LEAD(trunc(date_transfer),1, DECODE(type_transfer_id,3,TRUNC(date_transfer)+1,date'3000-01-01')) 
                OVER (PARTITION BY WORKER_ID ORDER BY date_transfer)-1/86400 end_transfer 
            FROM {0}.TRANSFER T
            START WITH T.SIGN_CUR_WORK = 1 or T.TYPE_TRANSFER_ID = 3
            CONNECT BY NOCYCLE PRIOR T.FROM_POSITION = T.TRANSFER_ID) V
        WHERE V.END_TRANSFER >= :p_date_begin and V.DATE_TRANSFER <= :p_date_end)
    select TR.TRANSFER_ID,TR.PER_NUM, WORKER_ID,FROM_POSITION,
        (select S.CODE_SUBDIV from {0}.SUBDIV S where TR.SUBDIV_ID = S.SUBDIV_ID) CODE_SUBDIV, 
        (select S.SUBDIV_NAME from {0}.SUBDIV S where TR.SUBDIV_ID = S.SUBDIV_ID) SUBDIV_NAME, 
        (select E.EMP_LAST_NAME from {0}.EMP E where TR.PER_NUM = E.PER_NUM) LAST_NAME, 
        (select E.EMP_FIRST_NAME from {0}.EMP E where TR.PER_NUM = E.PER_NUM) FIRST_NAME, 
        (select E.EMP_MIDDLE_NAME from {0}.EMP E where TR.PER_NUM = E.PER_NUM) MIDDLE_NAME, 
        (select E.EMP_SEX from {0}.EMP E where TR.PER_NUM = E.PER_NUM) EMP_SEX, 
        (select E.EMP_BIRTH_DATE from {0}.EMP E where TR.PER_NUM = E.PER_NUM) EMP_BIRTH_DATE, 
        DECODE(TR.SIGN_COMB,1,'X') COMB, TR.SIGN_COMB,
        (select P.POS_NAME from {0}.POSITION P where TR.POS_ID = P.POS_ID) POS_NAME
    from Tr_per TR
)
order by CODE_SUBDIV, LAST_NAME, FIRST_NAME, MIDDLE_NAME