<%@ Page masterpagefile="~/MasterPage.master" Language="C#" %>

<asp:content contentplaceholderid="cphCustomTitle" runat="server">Módulo Padrão C# 15.7 - utf-8</asp:content>

<asp:content contentplaceholderid="cphCustomHeader" runat="server"><% // content place holder do header %></asp:content>

<asp:content ID="Content5" contentplaceholderid="cphBodyProperties" runat="server">class="mensagens"</asp:content>

<asp:content contentplaceholderid="cphMainContent" runat="server">
    <div id="primary_content">
       	<h2>Mensagens</h2>
        <p>As mensagens disponíveis no modulo padrão são de Sucesso e Erro. Só copiar o código.</p>
			
		<!--  START NOTIFICATION BOXES -->       
        <div class="success">Sucesso - Esta é uma mensagem de sucesso.</div>
        <div class="fail">Erro - Esta é uma mensagem de erro.</div>
               
    </div>
    <!--  end div #primary_content -->
</asp:content>
