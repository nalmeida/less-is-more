<%
Dim strHtml
Dim strComputerName
Dim strUsername
Dim oWinNTsysinfo
Set oWinNTsysinfo = CreateObject("WinNTSystemInfo")
strComputerName = (oWinNTsysinfo.ComputerName)
strUsername = (oWinNTsysinfo.UserName)
strhtml = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><title>Informa&ccedil;&otilde;es da Conex&atilde;o</title><style type='text/css'>h1{font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;background-color: #efefef; border-bottom: 2px solid black;}ul{list-style-type: square;color: black;}li{font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;line-height: 30px;color: red;}li strong{color: black;font-size: 13px;}</style></head><body><h1>&nbsp;Informa&ccedil;&otilde;es da Conex&atilde;o</h1><ul><li><strong>Servidor:</strong> " & ucase(strcomputername) & "</li><li><strong>Ip remoto:</strong> " & request.servervariables("REMOTE_ADDR") & "</li><li><strong>Dados do host remoto:</strong> " & request.servervariables("HTTP_USER_AGENT") & "</li></ul></body></html>"
response.write strhtml
set oWinNTsysinfo = nothing
%>