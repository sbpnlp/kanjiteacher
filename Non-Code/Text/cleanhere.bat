if "%1" == "" goto doitHere

:doitatpercentone
cd %1
call ..\cleanIT
cd ..

goto end

:doitHere
call cleanIT.bat

:end
