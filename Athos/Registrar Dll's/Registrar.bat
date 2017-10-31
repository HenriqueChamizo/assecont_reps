cd /d %~dp0
if /i "%PROCESSOR_IDENTIFIER:~0,3%"=="X86" (
	echo system is x86
	copy .\*.dll %windir%\system32\
	regsvr32 %windir%\system32\MSADODC.OCX
	regsvr32 %windir%\system32\MSDATGRD.OCX
	regsvr32 %windir%\system32\MSDATLST.OCX
	regsvr32 %windir%\system32\MSFLXGRD.OCX
	regsvr32 %windir%\system32\MSMASK32.OCX
	regsvr32 %windir%\system32\mswinsck.ocx
	regsvr32 %windir%\system32\MSCOMM32.OCX
	regsvr32 %windir%\system32\MSCOMCT2.OCX
	regsvr32 %windir%\system32\COMCT332.OCX
	regsvr32 %windir%\system32\TABCTL32.OCX
	regsvr32 %windir%\system32\COMDLG32.OCX
	regsvr32 %windir%\system32\DBLIST32.OCX
	regsvr32 %windir%\system32\MSCHRT20.OCX
	regsvr32 %windir%\system32\MSINET.OCX
	regsvr32 %windir%\system32\MSCOMCTL.OCX
	regsvr32 %windir%\system32\MSBIND.DLL
	regsvr32 %windir%\system32\scrrun.dll
	regsvr32 %windir%\system32\StarRep.dll
	) else (
		echo system is x64
		copy .\*.dll %windir%\SysWOW64\
		regsvr32 %windir%\SysWOW64\zkemkeeperbz900.dll
		regsvr32 %windir%\SysWOW64\MSADODC.OCX
		regsvr32 %windir%\SysWOW64\MSDATGRD.OCX
		regsvr32 %windir%\SysWOW64\MSDATLST.OCX
		regsvr32 %windir%\SysWOW64\MSFLXGRD.OCX
		regsvr32 %windir%\SysWOW64\MSMASK32.OCX
		regsvr32 %windir%\SysWOW64\mswinsck.ocx
		regsvr32 %windir%\SysWOW64\MSCOMM32.OCX
		regsvr32 %windir%\SysWOW64\MSCOMCT2.OCX
		regsvr32 %windir%\SysWOW64\COMCT332.OCX
		regsvr32 %windir%\SysWOW64\TABCTL32.OCX
		regsvr32 %windir%\SysWOW64\COMDLG32.OCX
		regsvr32 %windir%\SysWOW64\DBLIST32.OCX
		regsvr32 %windir%\SysWOW64\MSCHRT20.OCX
		regsvr32 %windir%\SysWOW64\MSINET.OCX
		regsvr32 %windir%\SysWOW64\MSCOMCTL.OCX
		regsvr32 %windir%\SysWOW64\MSBIND.DLL
		regsvr32 %windir%\SysWOW64\scrrun.dll
		regsvr32 %windir%\SysWOW64\StarRep.dll
	)
pause

