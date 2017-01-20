select plat_ved.tnom per_num, plat_ved.podr code_subdiv, plat_ved.summa sum_sal , kassa.kas_dok, NAME_FILE, FIO
from kassa, plat_ved 
where type='3' and year(kassa.syst_data)= year(?) and month(kassa.syst_data)=month(?)
and kassa.kas_dok=plat_ved.kas_dok and kassa.p_sv=.t. and plat_ved.p_opl=.f. and year(plat_ved.syst_data)=year(kassa.syst_data)
and 
   (LEFT(name_file,5)='AVANS' and '271'=?)
order by code_subdiv, per_num