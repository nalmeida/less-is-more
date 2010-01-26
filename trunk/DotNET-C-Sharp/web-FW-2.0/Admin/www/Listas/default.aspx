<%@ Page masterpagefile="~/MasterPage.master" Language="C#" %>

<asp:content contentplaceholderid="cphCustomTitle" runat="server">Módulo Padrão C# 15.7 - utf-8</asp:content>

<asp:content contentplaceholderid="cphCustomHeader" runat="server"><% // content place holder do header %></asp:content>

<asp:content ID="Content5" contentplaceholderid="cphBodyProperties" runat="server">class="Listas"</asp:content>

<asp:content contentplaceholderid="cphMainContent" runat="server">
    <div id="primary_content">
       	<h2>Tipos de listas</h2>
        <p>Abaixo seguem os tipos de listas existentes em html</p>
        
		<!-- Lista de definição -->
		<h3>Lista de definição - DL</h3>
        <dl>
            <dt>Título</dt>
            <dd>Descrição</dd>
        </dl>
		
        <!-- Lista ordenada -->
		<h3>Lista ordenada</h3>
        <ol>
            <li>Lista Item 1</li>
            <li>Lista Item 2</li>
            <li>Lista Item 3</li>
        </ol>
		
        <!--  Lista não-ordenada -->
		<h3>Lista não-ordenada</h3>
        <ul>
            <li>Lista Item 1</li>
            <li>Lista Item 2</li>
            <li>Lista Item 3</li>
        </ul>
               
    </div>
    <!--  end div #primary_content -->
</asp:content>
