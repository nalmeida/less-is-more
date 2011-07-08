<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content ID="indexAnalytics" contentplaceholderid="cphCustomAnalytics" runat="server"><% //content place holder do parâmetro (texto) do analytics %></asp:content>

<asp:content ID="indexHeader" contentplaceholderid="cphCustomHeader" runat="server">
<% // content place holder do header %>
</asp:content>

<asp:content ID="indexHeaderIE6" contentplaceholderid="cphCustomHeaderIE6" runat="server">
<% // content place holder do header só para IE6 %>
</asp:content>

<asp:Content ID="indexContent" ContentPlaceHolderID="cphMainContent" runat="server">
	
	<h2><%= Html.Encode(ViewData["Message"]) %></h2>
	<p>
		To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
	</p>
	
</asp:Content>

