echo �������� � ����஢���� �஥�� Salary � ��४��� \\fs31w\FS31N_VOL1\ARHIV\BEZ\SALARY\
"%programfiles%\winrar\rar" a  "\\fs31w\FS31N_VOL1\ARHIV\BEZ\SALARY\ASalary%date:~-4,4%%date:~-7,2%%date:~-10,2%.rar" "e:\Job\ASalary"
set echo off
IF ERRORLEVEL 0 goto succ
goto errmsg
:succ 
	echo �������� �ᯥ譮 �����襭�
	echo �����஢� � \\fs31w\FS31N_VOL1\ARHIV\BEZ\SALARY\ASalary%date:~-4,4%%date:~-7,2%%date:~-10,2%.rar
	goto endprg
:errmsg 
	echo �訡�� 㯠����� ��� ����஢���� �஥��
:endprg
pause