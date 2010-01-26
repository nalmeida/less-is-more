<%@ Page masterpagefile="~/MasterPage.master" Language="C#" %>

<asp:content contentplaceholderid="cphCustomTitle" runat="server">Módulo Padrão C# 15.7 - utf-8</asp:content>

<asp:content contentplaceholderid="cphCustomHeader" runat="server"><% // content place holder do header %></asp:content>

<asp:content ID="Content5" contentplaceholderid="cphBodyProperties" runat="server">class="login"</asp:content>

<asp:content contentplaceholderid="cphMainContent" runat="server">
<!-- START ADMIN WRAPPER -->
<div id="admin_wrapper">
	<h1>Super Simple Admin Theme</h1>
	
	<p>Welcome. To log in just hit the log in button below.</p>
	
	<div class="attention">For the demo, just hit the login button</div>
	
	<form action="index1.html" method="get">
	<p>
		<label>Username</label>
		<input name="username" class="input large" value="username" type="text" />
	</p>
		
	<p>
		<label>Password</label>
		<input name="password" class="input large" value="password" type="password" />
	</p>
	  
	<p>
		<input name="Submit" id="button" value="Login" class="button" type="submit" />
		<input name="reset" id="reset" value="Reset" class="button" type="reset" />
	</p>
	</form>
</div>
<!-- END ADMIN WRAPPER -->
</asp:content>
