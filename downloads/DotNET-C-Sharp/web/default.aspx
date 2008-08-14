<%@ Register TagPrefix="uc" TagName="cabecalho_html" Src="~/inc/cabecalho_html.ascx" %>
<%@ Register TagPrefix="uc" TagName="cabecalho" Src="~/inc/cabecalho.ascx" %>
<%@ Register TagPrefix="uc" TagName="rodape" Src="~/inc/rodape.ascx" %>
<%@ Register TagPrefix="uc" TagName="init" Src="~/inc/init.ascx" %>
<%@ Register TagPrefix="uc" TagName="google_analytics" Src="~/inc/google_analytics.ascx" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="pt" lang="pt">
<head>
	<title><%=COMMON.Config.Title%></title>
	<uc:cabecalho_html id="cabecalho_html" runat="server" />
</head>
<body>
	<form name="frm" id="frm" action="#" runat="server">
<uc:cabecalho id="cabecalho" runat="server" />
		<div id="content">
			<!-- ME APAGUE! -->
			<div style="width:50%;">
				<h1>
					Módulo padrão C# v15.3.0 - utf-8;
				</h1>
				<h2>Variáveis do servidor</h2>
				<p>
					<strong>Título do site:</strong> <br />
					<%=COMMON.Config.Title%>
				</p>
				<p>
					<strong>Endereço da raíz do site:</strong><br />
					<%=COMMON.Util.Root%>
				</p>
				<p>
					<strong>Endereço da raíz dos arquivos externos globais:</strong><br />
					<%=COMMON.Util.GlobalPath%>
				</p>
				<p>
					<strong>Endereço da raíz dos arquivos externos de uma lígua específica (a princípio é "pt-BR" hard):</strong> <br />
					<%=COMMON.Util.LanguagePath%>
				</p>
				<p>
					<strong>Lígua atual (a princípio é "pt-BR" hard):</strong><br />
					<%=COMMON.Util.Language%>
				</p>
				<p>
					<em>Classes se encontram em \App_Code\Config.cs e \App_Code\Util.cs</em>
				</p>
				<hr />
				<h2>Minify.aspx</h2>
				<p>
					<strong>Todos os JS e CSS</strong> devem passar pela "<%=COMMON.Util.Root%>inc/minify.aspx". Ela remove espaços duplos, enters, tabs e comentários, concatena arquivos em uma única requisição e, no caso dos css, substitui a string <strong>"var(root)/"</strong> por <strong>"<%=COMMON.Util.Root%>"</strong>
				</p>
				<p>
					Para usar chame o arquivo da seguinte forma:<br />
					<strong>"<%=COMMON.Util.Root%>inc/minify.aspx?main.css|estilo.css|estilo2.css"</strong>
				</p>
				<p>
					Caso queira chamar um css específico de alguma língua, use: <br />
					<strong>"<%=COMMON.Util.Root%>inc/minify.aspx?main.css|estilo.css,<%=COMMON.Util.Language%>|estilo2.css"</strong>
				</p>
			</div>
			<!-- ME APAGUE! -->
		</div>
<uc:rodape id="rodape" runat="server" />
	</form>
<uc:google_analytics id="google_analytics" runat="server" />
<uc:init id="init" runat="server" />
</body>
</html>