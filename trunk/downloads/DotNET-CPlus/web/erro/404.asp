<%
Option Explicit

dim obWinNTsysinfo, obLogErro
dim stComputerName, stSite, stServerIP, stFile

set obWinNTsysinfo = CreateObject("WinNTSystemInfo")
	stComputerName = (obWinNTsysinfo.ComputerName)
set obWinNTsysinfo = nothing

stServerIP = Request.ServerVariables("LOCAL_ADDR")
stFile	   = trim(Mid(Request.QueryString, 5))

set obLogErro = Server.CreateObject("FBIZLogErro.clsFBIZLogErro")
	obLogErro.subLogErro404 stComputerName, _
							stServerIP, _
							stFile
set obLogErro = nothing
%><!--#include file='../inc/start.asp'--><?xml version="1.0" encoding="iso-8859-1"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="pt" lang="pt">
<head>
	<title><%=stSiteTitle%></title>
	<!--#include file='../inc/cabecalho_html.asp'-->
	
</head>
<body>
	
</body>
</html>