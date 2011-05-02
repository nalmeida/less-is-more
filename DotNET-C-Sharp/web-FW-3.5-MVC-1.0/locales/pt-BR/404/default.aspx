<%@ Page Language="C#" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="pt" lang="pt">
<head runat="server">
    <title>P�gina n�o encontrada!</title>
	<meta name="description" content="" />
	<meta name="keywords" content="" />
	
	<meta name="copyright" content="F.biz" />
	<meta name="author" content="F.biz - http://www.fbiz.com.br/" />
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
	<meta name="MSSmartTagsPreventParsing" content="TRUE" />
	<meta name="robots" content="NOINDEX, FOLLOW" />

	<meta http-equiv="pragma" content="no-cache" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="Fri, 13 Jul 2001 00:00:01 GMT" />

	<link rel="shortcut icon" href="../img/favicon.ico" />
	<link rel="stylesheet" type="text/css" href="" media="screen" />
	
	<style type="text/css">
		body{
			padding:10% 20%;
			margin:0;
			font-size:17px;
			font-family: Arial, Verdana, Trebuchet;
			color:#999;
		}
		h1{
			margin:0;
			font-size:1.8em;
			color:#cecece;
		}
		.errorMessage{
			line-height:1.5em;
		}
			.highlight{
				font-size:1.2em;
				
			}
	</style>
	
</head>
<body>
	<h1>P�gina n�o encontrada!</h1>
	<p class="errorMessage">
		A p�gina
		<strong class="highlight"><%=System.Text.RegularExpressions.Regex.Replace(Request.Url.Query.ToString(), "^.[^;]*;", "")%></strong>
		n�o existe.
	</p>
	<p>
		Verifique se foi digitada corretamente ou v� para a <a href="<%=Util.Root%>">home</a>.
	</p>
</body>
</html>
<% Response.StatusCode = 404; %>

