<%@ Page masterpagefile="~/MasterPage.master" Language="C#" %>

<asp:content contentplaceholderid="cphCustomTitle" runat="server">Módulo Padrão C# 15.7 - utf-8</asp:content>

<asp:content contentplaceholderid="cphCustomHeader" runat="server"><% // content place holder do header %></asp:content>

<asp:content ID="Content5" contentplaceholderid="cphBodyProperties" runat="server">class="tabelas"</asp:content>

<asp:content contentplaceholderid="cphMainContent" runat="server">
    <div id="primary_content">
        <h2>Tabelas e Tabelas<span class="theme_highlight"> sorteadas</span></h2>
        <p>
            Para utilizar as tabelas de forma a usar o tema padrão do módulo, basta apenas criar 
			a estrutura do html igual a tabela do exemplo abaixo.
			<br />
			A classe <strong>tablesorter</strong> é necessária tanto para a formatação do Css quanto para a função de ordenação do  o javascript de ordenação.
			<strong></strong>
        </p>

        <table class="tablesorter">
            <thead>
                <tr>
                    <th class="header" width="22%">
                        Name
                    </th>
                    <th class="header" width="34%">
                        E-Mail
                    </th>
                    <th class="header" width="25%">
                        Telephone
                    </th>
                    <th class="header" width="19%">
                        Action
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        John Doe
                    </td>
                    <td>
                        <a href="mailto:john.doe@hotmail.com">john.doe@hotmail.com</a>
                    </td>
                    <td>
                        0130 456 890
                    </td>
                    <td>
                        <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                    </td>
                </tr>
                <tr class="even">
                    <td>
                        Jackie Ching
                    </td>
                    <td>
                        <a href="mailto:j.ching@yahoo.com">j.ching@yahoo.com</a>
                    </td>
                    <td>
                        0120 546 999
                    </td>
                    <td>
                        <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                    </td>
                </tr>
                <tr>
                    <td>
                        Samantha Dollock
                    </td>
                    <td>
                        <a href="mailto:sam_dollock@hotmail.com">sam_dollock@hotmail.com</a>
                    </td>
                    <td>
                        0141 654 001
                    </td>
                    <td>
                        <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                    </td>
                </tr>
                <tr class="even">
                    <td>
                        Kevin Dallingham
                    </td>
                    <td>
                        <a href="mailto:kevindallingham@gmail.com">kevindallingham@gmail.com</a>
                    </td>
                    <td>
                        0171 123 890
                    </td>
                    <td>
                        <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                    </td>
                </tr>
                <tr>
                    <td>
                        Jamie Tollhurst
                    </td>
                    <td>
                        <a href="#">tollhurst@yahoo.com</a>
                    </td>
                    <td>
                        0121 321 456
                    </td>
                    <td>
                        <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                    </td>
                </tr>
                <tr class="even">
                    <td>
                        Simon Wright
                    </td>
                    <td>
                        <a href="#">wrighty@gmail.com</a>
                    </td>
                    <td>
                        0181 321 456
                    </td>
                    <td>
                        <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                    </td>
                </tr>
                <tr>
                    <td>
                        Jenny Combs
                    </td>
                    <td>
                        <a href="#">combsey@gmail.com</a>
                    </td>
                    <td>
                        0131 456 988
                    </td>
                    <td>
                        <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                    </td>
                </tr>
            </tbody>
        </table>
       
        <!-- Paginator end -->
    </div>
    <!--  end div #primary_content -->
</asp:content>
