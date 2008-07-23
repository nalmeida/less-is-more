<%@ language="VBScript" %>
<%
Option Explicit

dim obASPErrorLog, obFBIZLogErro, obWinNTsysinfo
dim stComputerName, stSite, stServerIP, stUserIP, stErrType, stErrFile, stSourceCode, stDescription, stASPCode
dim idErrCod, nmLine, nmColumn
dim fgErrorWritten

Response.Buffer = true

If Response.Buffer Then
    Response.Clear
    Response.Status = "500 nmernal Server Error"
    Response.ContentType = "text/html"
    Response.Expires = 0
End If

set obWinNTsysinfo = CreateObject("WinNTSystemInfo")
stComputerName = (obWinNTsysinfo.ComputerName)
set obWinNTsysinfo = nothing

set obASPErrorLog = Server.GetLastError
stSite		  = LCase(Request.ServerVariables("SERVER_NAME"))
stServerIP    = Request.ServerVariables("LOCAL_ADDR")
stUserIP      = Request.ServerVariables("REMOTE_ADDR")
idErrCod	  = obASPErrorLog.Number
stErrType	  = Server.HTMLEncode(obASPErrorLog.Category)
stErrFile     = obASPErrorLog.File
nmLine        = obASPErrorLog.Line
nmColumn	  = obASPErrorLog.Column
stSourceCode  = Server.HTMLEncode(obASPErrorLog.Source)
stASPCode     = obASPErrorLog.ASPCode

stDescription = Server.HTMLEncode(obASPErrorLog.ASPDescription)
if stDescription = "" then
	stDescription = Server.HTMLEncode(obASPErrorLog.Description)
end if

set obASPErrorLog = nothing

Set obFBIZLogErro = Server.CreateObject("FBIZLogErro.clsFBIZLogErro")
obFBIZLogErro.subLogErro500 stComputerName, _
                            stSite, _
                            stServerIP, _
                            stUserIP, _
                            idErrCod, _
                            stErrType, _
                            stErrFile, _
                            nmLine, _
                            nmColumn, _
                            stSourceCode, _
                            stDescription
Set obFBIZLogErro = Nothing
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 3.2 Final//EN">

<html dir=ltr>

<head>
<style>
a:link			{font:8pt/11pt verdana; color:FF0000}
a:visited		{font:8pt/11pt verdana; color:#4e4e4e}
</style>

<META NAME="ROBOTS" CONTENT="NOINDEX">

<title>The page cannot be displayed</title>

<META HTTP-EQUIV="Content-Type" Content="text-html; charset=Windows-1252">
</head>

<script> 
function Homepage(){
<!--
// in real bits, urls get returned to our script like this:
// res://shdocvw.dll/http_404.htm#http://www.DocURL.com/bar.htm 

	//For testing use DocURL = "res://shdocvw.dll/http_404.htm#https://www.microsoft.com/bar.htm"
	DocURL=document.URL;
	
	//this is where the http or https will be, as found by searching for :// but skipping the res://
	protocolIndex=DocURL.indexOf("://",4);
	
	//this finds the ending slash for the domain server 
	serverIndex=DocURL.indexOf("/",protocolIndex + 3);

	//for the href, we need a valid URL to the domain. We search for the # symbol to find the begining 
	//of the true URL, and add 1 to skip it - this is the BeginURL value. We use serverIndex as the end marker.
	//urlresult=DocURL.substring(protocolIndex - 4,serverIndex);
	BeginURL=DocURL.indexOf("#",1) + 1;
	urlresult=DocURL.substring(BeginURL,serverIndex);
		
	//for display, we need to skip after http://, and go to the next slash
	displayresult=DocURL.substring(protocolIndex + 3 ,serverIndex);
	InsertElementAnchor(urlresult, displayresult);
}

function HtmlEncode(text)
{
    return text.replace(/&/g, '&amp').replace(/'/g, '&quot;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
}

function TagAttrib(name, value)
{
    return ' '+name+'="'+HtmlEncode(value)+'"';
}

function PrintTag(tagName, needCloseTag, attrib, inner){
    document.write( '<' + tagName + attrib + '>' + HtmlEncode(inner) );
    if (needCloseTag) document.write( '</' + tagName +'>' );
}

function URI(href)
{
    IEVer = window.navigator.appVersion;
    IEVer = IEVer.substr( IEVer.indexOf('MSIE') + 5, 3 );

    return (IEVer.charAt(1)=='.' && IEVer >= '5.5') ?
        encodeURI(href) :
        escape(href).replace(/%3A/g, ':').replace(/%3B/g, ';');
}

function InsertElementAnchor(href, text)
{
    PrintTag('A', true, TagAttrib('HREF', URI(href)), text);
}

//-->
</script>

<body bgcolor="FFFFFF">

<table width="410" cellpadding="3" cellspacing="5">

  <tr>    
    <td align="left" valign="middle" width="360">
	<h1 style="COLOR:000000; FONT: 13pt/15pt verdana"><!--Problem-->Internal Server Error</h1>
    </td>
  </tr>
  
  <tr>
    <td width="400" colspan="2">
	<font style="COLOR:000000; FONT: 8pt/11pt verdana">There is a problem with the page you are trying to reach and it cannot be displayed.</font></td>
  </tr>
  
  <tr>
    <td width="400" colspan="2">
	<font style="COLOR:000000; FONT: 8pt/11pt verdana">

	<hr color="#C0C0C0" noshade>
	
    <p>Please try the following:</p>

	<ul>
      <li id="instructionsText1">Click the 
      <a href="javascript:location.reload()">
      Refresh</a> button, or try again later.<br>
      </li>
	  
      <li>Open the 
	  
	  <script>
	  <!--
	  if (!((window.navigator.userAgent.indexOf("MSIE") > 0) && (window.navigator.appVersion.charAt(0) == "2")))
	  {
	  	 Homepage();
	  }
	  //-->
	  </script>

	  home page, and then look for links to the information you want. </li>
    </ul>
	
    <h2 style="font:8pt/11pt verdana; color:000000">HTTP 500.100 - Internal Server
    Error - ASP error<br>
    Internet Information Services</h2>

	<hr color="#C0C0C0" noshade>
	
	<p>Technical Information (for support personnel)</p>

<ul>
<li>Error Type:<br>
<%
  Dim bakCodepage
  on error resume next
	  bakCodepage = Session.Codepage
	  Session.Codepage = 1252
  on error goto 0
  Response.Write stErrType
  If stASPCode > "" Then Response.Write ", " & Server.HTMLEncode(stASPCode)
  Response.Write Server.HTMLEncode(" (0x" & Hex(idErrCod) & ")" ) & "<br>"  
  Response.Write stDescription & "<br>" 

  fgErrorWritten = False

  If stSourceCode > "" Then
    stSite = LCase(Request.ServerVariables("SERVER_NAME"))
    stServerIP = Request.ServerVariables("LOCAL_ADDR")
    stUserIP =  Request.ServerVariables("REMOTE_ADDR")
    If (stSite = "localhost" Or stServerIP = stUserIP) And stErrFile <> "?" Then
      Response.Write Server.HTMLEncode(stErrFile)
      If nmLine > 0 Then Response.Write ", line " & nmLine
      If nmColumn > 0 Then Response.Write ", column " & nmColumn
      Response.Write "<br>"
      Response.Write "<font style=""COLOR:000000; FONT: 8pt/11pt courier new""><b>"
      Response.Write Server.HTMLEncode(stSourceCode) & "<br>"
      If nmColumn > 0 Then Response.Write String((nmColumn - 1), "-") & "^<br>"
      Response.Write "</b></font>"
      fgErrorWritten = True
    End If
  End If

  If Not fgErrorWritten And stErrFile <> "?" Then
    Response.Write "<b>" & Server.HTMLEncode(stErrFile)
    If nmLine > 0 Then Response.Write Server.HTMLEncode(", line " & nmLine)
    If nmColumn > 0 Then Response.Write ", column " & nmColumn
    Response.Write "</b><br>"
  End If
%>
</li>
<p>
<li>Browser Type:<br>
<%= Server.HTMLEncode(Request.ServerVariables("HTTP_USER_AGENT")) %>
</li>
<p>
<li>Page:<br>
<%  dim stMethod
  stMethod = Request.ServerVariables("REQUEST_METHOD")

  Response.Write stMethod & " "

  If stMethod = "POST" Then
    Response.Write Request.TotalBytes & " bytes to "
  End If

  Response.Write Request.ServerVariables("SCRIPT_NAME")
  dim lngPos
  lngPos = InStr(Request.QueryString, "|")

  If lngPos > 1 Then
    Response.Write "?" & Server.HTMLEncode(Left(Request.QueryString, (lngPos - 1)))
  End If

  Response.Write "</li>"

  If stMethod = "POST" Then
    Response.Write "<p><li>POST Data:<br>"
    If Request.TotalBytes > lngMaxFormBytes Then
       Response.Write Server.HTMLEncode(Left(Request.Form, 200)) & " . . ."
    Else
      Response.Write Server.HTMLEncode(Request.Form)
    End If
    Response.Write "</li>"
  End If

%>
<p>
<li>Time:<br>
<%
  Response.Write Server.HTMLEncode(FormatDateTime(Now(), 1) & ", " & FormatDateTime(Now(), 3))
  on error resume next
	  Session.Codepage = bakCodepage 
  on error goto 0
%>
</li>
</p>
    </font></td>
  </tr>
  
</table>
</body>
</html>