<%@ Page masterpagefile="~/MasterPage.master" Language="C#" %>

<asp:content contentplaceholderid="cphCustomTitle" runat="server">Módulo Padrão C# 15.7 - utf-8</asp:content>

<asp:content contentplaceholderid="cphCustomHeader" runat="server"><% // content place holder do header %></asp:content>

<asp:content ID="Content5" contentplaceholderid="cphBodyProperties" runat="server">class="home"</asp:content>

<asp:content contentplaceholderid="cphMainContent" runat="server">
    <div id="primary_content">
        <h2>Bem-vindo ao Admin F.biz</h2>
        <p>
           	Esta página contém todos os objetos html usados no admin até o momento.
		   <br />Copie exatamente o código respeitando sua hierarquia html para poder utilizar os temas corretamente.		   
        </p>
		<p>
			Você pode visualizar os objetos html navegando nas abas do admin, cada objeto é detalhado em cada aba.
		</p>
		
		<h2>Botões</h2>
		<p>Botões de ação para aprovação de conteúdo</p>
		<button class="button aproved" type="submit">Aprovados</button>
		<button class="button reproved" type="submit">Reprovados</button>
		<button class="button pendent" type="submit">Pendentes</button>
		
		<p>Botão padrão</p>
		<button class="button" type="submit">Enviar</button>
		
		<h2>Mensagens</h2>
		
		<!--  START NOTIFICATION BOXES -->       

        <div class="success">Sucesso - Esta é uma mensagem de sucesso.</div>
        <div class="fail">Erro - Esta é uma mensagem de erro.</div>

        <!--  END NOTIFICATION BOXES -->
		
		<h2>Tipos de listas</h2>
        <p>
        	Abaixo seguem os tipos de listas existentes em html
			<br />
			Os códigos html estão comentados, basta copiar.
		</p>
        
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
		
		<h2>Tabs</h2>
        <p>
			Para utilizar as abas basta copiar o html do código e seguir padrões conforme os comentários.			
		</p>	
		<br />
		<p>	
			Basicamente, deve existir um div geral englobando o conteúdo com a class "tabs"
			Dentro, deve existir uma lista não ordenada que nela os links para as devidas sessões.
		</p>
		<br />
		<p>
			Logo abaixo da lista não ordenada, devem vir as sessões separadas por div e cada div deve ter um id diferente.
		</p>
		<br />
		<p>			
			Os links da lista devem ter um "#" (hash) seguido dos id´s do div das sessões, assim o javascript vai abrir a aba
			correta se baseando nos id´s.
		</p>
		<br />
		<p>			
			O código está bem comentado e fácil de ser reproduzido. 
		</p>
		<br />
		
       	<!--  START TAB -->
        <div id="tabs" class="tabs">
           
		    <!--  TAB BOXES/HEADINGS -->
            <ul>
                <li class="active"><a href="#tab-1">Tabelas</a></li>
                <li><a href="#tab-2">Formulários</a></li>
                <li><a href="#tab-3">Headings</a></li>
            </ul>
           
		    <!--  START TAB 1 -->
            <div id="tab-1">
                <h3>Tabela</h3>
                
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
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16" /></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16" /></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16" /></a>
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
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16" /></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16" /></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16" /></a>
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
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16" /></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16" /></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16" /></a>
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
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16" /></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16" /></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16" /></a>
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
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16" /></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16" /></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16" /></a>
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
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16" /></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16" /></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16" /></a>
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
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16" /></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16" /></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16" /></a>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <br />
            </div>
            <!--  end div #tab1 --><!--  END TAB 1 --><!--  START TAB 2 -->
            <div id="tab-2">
                <h3>Formulário</h3>
                
		        <p>Itens  de formulário</p>
				<p>Copie o html da mesma forma para poder utilizar a opção de temas corretamente.</p>		       
		        <p>
		            <label>
		                Campo de formulário pequeno 
						Classe css: "input small"
		            </label>
		            <input name="textfield1" class="input small" id="textfield1" value="Small..." type="text" />
		        </p>
		        <p>
		            <label>
		              	Campo de formulário médio 
						Classe css: "input medium"
		            </label>
		            <input name="textfield2" class="input medium" id="textfield2" value="Medium..." type="text" />
		        </p>
		        <p>
		            <label>
		                Campo de formulário grande 
						Classe css: "input large"
		            </label>
		            <input name="textfield3" class="input large" id="textfield3" value="Large..." type="text" />
		        </p>
		        <p>
		            <label>
		                Check Boxes
		            </label>
		        </p>
		        <p>
		            <input name="check1" class="checkbox" id="check1" type="checkbox" />checkbox comum 
		        </p>
		        <p>
		            <input name="check2" class="checkbox" id="check2" checked="checked" type="checkbox" />checkbox comum 
		        </p>
		        <p>
		            <label>
		                Radio Buttons
		            </label>
		            <input name="radio" class="radio" id="radio1" value="radio1" type="radio" />radio comum 
		        </p>
		        <p>
		            <input name="radio" class="radio" id="radio2" value="radio1" type="radio" />radio comum
		        </p>
		        <p>
		            <input name="radio" class="radio" id="radio3" value="radio3" type="radio" />radio comum 
		        </p>
		        <p>
		            <label>
		                Drop Down (Combo) List
		            </label>
		            <select name="combo" class="select input">
		                <option selected="selected" value="Option 1">Option 1</option>
		                <option value="Option 2">Option 2</option>
		                <option value="Option 3">Option 3</option>
		                <option value="Option 4">Option 4</option>
		                <option value="Option 5">Option 5</option>
		            </select>
		        </p>
		        <p>
		            <label>
		               Textarea
		            </label>
		            <textarea name="textarea" cols="50" rows="7" class="styled_textarea">Text area for lots of lines of text...</textarea>
		        </p>
		        <p>
		            <input class="button" value="Submit" type="button" />
		        </p>
            </div><!--  end div #tab-2 --><!--  END TAB 2 --><!--  START TAB 3 -->
            <div id="tab-3">
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
            <!--  end div #tab-3 -->
            <!--  END TAB 3 -->
        </div>
        <!--  END TABS -->
		
		<!-- The paginator -->
	        <ul class="paginator">
	        	<li><a href="#">Previous</a></li>
	            <li class="current"><a href="#">1</a></li>
	            <li><a href="#">2</a></li>
	            <li><a href="#">3</a></li>
	            <li><a href="#">4</a></li>
	            <li><a href="#">5</a></li>
	            <li><a href="#">6</a></li>
	            <li><a href="#">7</a></li>
	            <li><a href="#">8</a></li>
	            <li><a href="#">9</a></li>
	            <li><a href="#">Next</a></li>
	        </ul>
        <!-- Paginator end -->
    </div>
    <!--  end div #primary_content -->
</asp:content>
