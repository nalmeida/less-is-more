<% if(Request.RawUrl.IndexOf("Default.aspx") == -1 && Request.RawUrl.IndexOf("Home.aspx") == -1){%>	
	<div class="link"><a href="<%= Request.UrlReferrer %>">Voltar</a></div>
<%}%>
<div class="link">
	<a href="../home/home.aspx">In&iacute;cio</a>
</div>