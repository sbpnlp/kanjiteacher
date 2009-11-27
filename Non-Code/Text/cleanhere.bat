if "%1" == "" goto doitHere

:doitatpercentone

cd %1

del *.aux
del *.bbl
del *.blg
del *.log
del *.toc
del *~
del *.bak

cd ..

goto end

:doitHere

del *.aux
del *.bbl
del *.blg
del *.log
del *.toc
del *~
del *.bak

:end
