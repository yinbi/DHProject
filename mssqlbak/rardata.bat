@echo off

cd /d %~dp0

forfiles /P "." /S /M *.bak /D 0 /C "cmd /c cd /d C:\\WinRAR & rar.exe a %~dp0\..\bakfilerar\@file.rar %~dp0\@file" 

echo end.