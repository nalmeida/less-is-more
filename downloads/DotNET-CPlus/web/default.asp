<%
Response.Status = "301 Moved Permanently"
Response.AddHeader "Location", "./home/"
Response.End
%>