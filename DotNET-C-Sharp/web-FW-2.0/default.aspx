<%@ Page masterpagefile="~/MasterPage.master" Language="C#" %>

<asp:content contentplaceholderid="cphCustomTitle" runat="server">Módulo Padrão C# 15.5 - utf-8</asp:content>

<asp:content contentplaceholderid="cphCustomHeader" runat="server">
<% // content place holder do header %>
</asp:content>


<asp:content contentplaceholderid="cphMainContent" runat="server">
	<% // content place holder criado para exemplo da página de teste %>
	Conteúdo <code>&lt;asp:content contentplaceholderid=&quot;cphMainContent&quot; runat=&quot;server&quot;&gt;</code> substituído!
</asp:content>

<asp:content contentplaceholderid="cphCustomAnalytics" runat="server"><% //content place hoder do parâmetro (texto) do analytics %></asp:content>
