@echo off

if "%~1"=="/?" goto UsageHelp
if "%~3"=="" goto UsageHelp
if NOT EXIST "%~3" goto FileNotFound

if "%~1"=="Debug" goto DebugMode
if "%~1"=="Release" goto ReleaseMode

echo CodeSigner: Unrecognized configuration name `%~1'; expected `Debug' or `Release'

rem =============== RELEASE MODE ===============
:ReleaseMode
if "%~2"=="AnyCPU" goto x86
if "%~2"=="Any CPU" goto x86
if "%~2"=="x86" goto x86
if "%~2"=="x64" goto x64

echo CodeSigner: Unrecognized platform name `%~2'; expected `Any CPU', `x86', or `x64'. Aborting.
goto eof

:x86
echo CodeSigner: Release build (32-bit): signing.
set BITTINESS=x86
goto post_bittiness
:x64
echo CodeSigner: Release build (64-bit): signing.
set BITTINESS=x64
:post_bittiness

set ST_EXE="C:\Program Files (x86)\Microsoft Visual Studio\Shared\NuGetPackages\microsoft.windows.sdk.buildtools\10.0.22621.1\bin\10.0.22621.0\%BITTINESS%\signtool.exe"
if EXIST %ST_EXE% goto DoSign
set ST_EXE="C:\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\%BITTINESS%\signtool.exe"
if EXIST %ST_EXE% goto DoSign

echo CodeSigner: couldn't find SIGNTOOL.EXE in any of the expected places. Exiting.
goto eof

:DoSign
%ST_EXE% sign /q /n Open /t http://time.certum.pl/ /fd sha256 /sha1 %CERTUM_HASH% "%~3"

if errorlevel 0 goto eof
echo CodeSigner: error: expected errorlevel 0 from signtool.exe, got nonzero.

goto eof

rem =============== DEBUG MODE ===============
:DebugMode
echo Debug build: not signing
goto eof

rem =============== USAGE HELP ===============
:FileNotFound
echo CodeSigner: expected a valid file (path to EXE), got `%~2'
:UsageHelp
echo Usage:
echo CodeSigner.bat Release^|Debug Any CPU^|x86^|x64 "path\to\exe"


:eof
