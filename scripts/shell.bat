REM usage is "shell user password"
REM
echo
echo argument 1 = %1
echo argument 2 = %2
REM
call C:\Windows\System32\cmd.exe /c ""C:\Program Files (x86)\Git\bin\sh.exe" --login -i -- GetMerchello-Documentation.sh %1 %2"
exit /b

