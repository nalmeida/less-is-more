<%@ Page masterpagefile="~/MasterPage.master" Language="C#" %>

<asp:content contentplaceholderid="cphCustomTitle" runat="server">Módulo Padrão C# 15.7 - utf-8</asp:content>

<asp:content contentplaceholderid="cphCustomHeader" runat="server"><% // content place holder do header %></asp:content>

<asp:content ID="Content5" contentplaceholderid="cphBodyProperties" runat="server">class="login"</asp:content>

<asp:content contentplaceholderid="cphMainContent" runat="server">
<!-- START ADMIN WRAPPER -->
<div id="admin_wrapper">
	<h1>F.biz Admin</h1>
	
	<p>Bem-vindo ao Admin!</p>
	
	<!--
		<div class="attention">mensagem de alerta </div>
	-->
	
	<form action="index1.html" method="get">
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
	</form>
</div>
<!-- END ADMIN WRAPPER -->
</asp:content>
