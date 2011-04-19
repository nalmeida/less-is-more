<%@ Page masterpagefile="~/MasterPage.master" Language="C#" %>
<%@ Register TagPrefix="uc" TagName="navigation" Src="~/inc/top-navigation.ascx" %>

<asp:content contentplaceholderid="cphCustomTitle" runat="server">Módulo Padrão C# 15.7 - utf-8</asp:content>

<asp:content contentplaceholderid="cphCustomHeader" runat="server"><% // content place holder do header %></asp:content>

<asp:content ID="Content5" contentplaceholderid="cphBodyProperties" runat="server">class="formularios"</asp:content>

<asp:content contentplaceholderid="cphMainContent" runat="server">
    
	<div class="primary_content">
       	<h2>Como Usar</h2>
		
        <p>Caso o Item de menu não tenha sub-menu, inserir a classe "no-submenu"</p>
		
		<p>Quando o Item de menu estiver selecionado, inserir a classe "current"</p>
			   
    </div>
    <!--  end div #primary_content -->
</asp:content>
