<%@ Register TagPrefix="uc" TagName="cabecalho_html" Src="~/inc/cabecalho_html.ascx" %>
<%@ Register TagPrefix="uc" TagName="cabecalho" Src="~/inc/cabecalho.ascx" %>
<%@ Register TagPrefix="uc" TagName="rodape" Src="~/inc/rodape.ascx" %>
<%@ Register TagPrefix="uc" TagName="init" Src="~/inc/init.ascx" %>
<%@ Register TagPrefix="uc" TagName="google_analytics" Src="~/inc/google_analytics.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="pt" lang="pt">
<head>
	<title><%=COMMON.Config.Title%></title>
	<uc:cabecalho_html id="cabecalho_html" runat="server" />
</head>
<body>
	<form name="frm" id="frm" action="#">
<uc:cabecalho id="cabecalho" runat="server" />
		<div id="content">
			Erro 500
		</div>
<uc:rodape id="rodape" runat="server" />
	</form>
<uc:google_analytics id="google_analytics" runat="server" />
<uc:init id="init" runat="server" />
</body>
</html>