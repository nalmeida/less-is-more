<%@ Page masterpagefile="~/MasterPage.master" Language="C#" %>

<asp:content contentplaceholderid="cphCustomTitle" runat="server">Módulo Padrão C# 15.7 - utf-8</asp:content>

<asp:content contentplaceholderid="cphCustomHeader" runat="server"><% // content place holder do header %></asp:content>

<asp:content ID="Content5" contentplaceholderid="cphBodyProperties" runat="server">class="Listas"</asp:content>

<asp:content contentplaceholderid="cphMainContent" runat="server">
    <div id="primary_content">
       	<h2>Headings</h2>
            <p>               
				Aqui está um exemplo de tipografia usada neste tema. 
				<br />Abaixo estão listados os diferentes tipos de níveis headings.
            </p>
            <h6>Isto é um H6 Heading</h6>
            <h5>Isto é um H5 Heading</h5>
            <h4>Isto é um H4 Heading</h4>
            <h3>Isto é um H3 Heading</h3>
            <h2>Isto é um H2 Heading</h2>
           
		    <h1>Isto é um H1 Heading</h1>
            <p>
                Sed
                sed tellus neque, at porta felis. Pellentesque dignissim aliquam mi 
                eget tristique. Aliquam erat volutpat. Lorem ipsum dolor sit amet, 
                consectetur adipiscing elit. Cras sodales pretium pharetra. <a href="#">Vestibulum</a>
                porta massa ut est sagittis fringilla. Curabitur sed posuere odio. Sed
                ac nisi mi, a ultricies est. Nam elementum tristique purus, id viverra 
                mi mattis eget. Integer in adipiscing dui. Sed lacinia porttitor 
                turpis, ut accumsan nunc venenatis sed. Nunc ultricies <a href="#">imperdiet</a>
                luctus. Suspendisse mollis, nisl a congue aliquet, turpis leo feugiat 
                nulla, vitae fringilla eros neque eu risus. Mauris pellentesque 
                placerat nisi ac pharetra. 
            </p>
    </div>
    <!--  end div #primary_content -->
</asp:content>
