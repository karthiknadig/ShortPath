# ShortPath
Calculates short path on Windows.

## Usage:
ShortPath [-f|--full] [-?|--help]
* -f, --full : Displays path with drive and directories.
* -?, --help : Displays this help

## Examples:
```
> dir /B /S | ShortPath -f
C:\PROGRA~1\R\R-33~1.3\unins000.dat
C:\PROGRA~1\R\R-33~1.3\unins000.exe
C:\PROGRA~1\R\R-33~1.3\bin\config.sh
C:\PROGRA~1\R\R-33~1.3\bin\i386
C:\PROGRA~1\R\R-33~1.3\bin\R.exe
C:\PROGRA~1\R\R-33~1.3\bin\Rscript.exe
C:\PROGRA~1\R\R-33~1.3\bin\x64
C:\PROGRA~1\R\R-33~1.3\bin\i386\open.exe
.
.
.
```
