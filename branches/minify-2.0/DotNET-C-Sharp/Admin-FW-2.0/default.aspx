<%@ Page masterpagefile="~/MasterPage.master" Language="C#" %>

<asp:content contentplaceholderid="cphCustomTitle" runat="server">Módulo Padrão C# 15.7 - utf-8</asp:content>

<asp:content contentplaceholderid="cphCustomHeader" runat="server">
	<link rel="stylesheet" type="text/css" href="<%=Common.Util.Root%>minify.aspx?login.css,,<%=Common.Util.Theme%>&amp;bpc=<%=Common.Util.Bpc%>&amp;v=<%=Common.Util.Version%>" media="screen" />
</asp:content>

<asp:content ID="header" contentplaceholderid="cphHeader" runat="server"></asp:content>
<asp:content ID="footer" contentplaceholderid="cphFooter" runat="server"></asp:content>
<asp:content ID="navigation" contentplaceholderid="cphNavigation" runat="server"></asp:content>
<asp:content ID="bodyproperties" contentplaceholderid="cphBodyProperties" runat="server">class="login"</asp:content>

<asp:content contentplaceholderid="cphMainContent" runat="server">
<!-- START ADMIN WRAPPER -->
<div class="verticalAligner"></div>
<div id="boxLogin">
	<div class="boxLoginWrapper">
		<h1>Admin <%=Common.Config.ProjectName%></h1>
	
		<p>Bem-vindo ao Admin!</p>
		<p>
			<label>Login</label>
			<input name="username" class="input large" value="username" type="text" />
		</p>
		<p>
			<label>Senha</label>
			<input name="password" class="input large" value="password" type="password" />
		</p>
		<p>
			<input name="Submit" id="button" value="Entrar" class="button" type="submit" />
		</p>
	</div>
</div>
<!-- END ADMIN WRAPPER -->
</asp:content>
