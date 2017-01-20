echo Упаковка и копирование проекта Salary в директорию \\fs31w\FS31N_VOL1\ARHIV\BEZ\SALARY\
"%programfiles%\winrar\rar" a  "\\fs31w\FS31N_VOL1\ARHIV\BEZ\SALARY\ASalary%date:~-4,4%%date:~-7,2%%date:~-10,2%.rar" "e:\Job\ASalary"
set echo off
IF ERRORLEVEL 0 goto succ
goto errmsg
:succ 
	echo Упаковка успешно завершена
	echo Скопирова в \\fs31w\FS31N_VOL1\ARHIV\BEZ\SALARY\ASalary%date:~-4,4%%date:~-7,2%%date:~-10,2%.rar
	goto endprg
:errmsg 
	echo Ошибка упаковки или копирования проекта
:endprg
pause