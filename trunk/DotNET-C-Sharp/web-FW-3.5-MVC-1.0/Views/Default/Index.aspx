<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content ID="track" contentplaceholderid="cphValues" runat="server"><%
ViewData["TrackValue"] = "";
%></asp:content>

<asp:content ID="indexHeader" contentplaceholderid="cphCustomHeader" runat="server">
<% // content place holder do header %>
</asp:content>

<asp:Content ID="indexContent" ContentPlaceHolderID="cphContent" runat="server">
Hello Less is More MVC 1.0!
</asp:Content>

