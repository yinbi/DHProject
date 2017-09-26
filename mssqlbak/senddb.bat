@echo off
ftp -s:%~dp0\senddb.cfg >> %~dp0\log\log.log
del %~dp0\*.rar
echo end============================================