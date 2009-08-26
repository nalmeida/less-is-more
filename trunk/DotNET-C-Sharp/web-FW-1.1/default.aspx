<%@ Page language="c#"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="pt" lang="pt">
<head>
	<title><%=Interface.Config.Title%></title>
	<meta name="description" content="<%=Interface.Config.Title%>" />
	<meta name="keywords" content="<%=Interface.Config.Title%>" />
	
	<meta name="copyright" content="F.biz" />
	<meta name="author" content="F.biz - http://www.fbiz.com.br/" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="MSSmartTagsPreventParsing" content="TRUE" />
	<meta name="robots" content="INDEX, FOLLOW" />

	<meta http-equiv="pragma" content="no-cache" />
	<meta http-equiv="cache-control" content="no-cache" />
	<meta http-equiv="expires" content="Fri, 13 Jul 2001 00:00:01 GMT" />

	<link rel="shortcut icon" href="<%=Interface.Util.GlobalPath%>img/favicon.ico" />
	
	<link rel="stylesheet" type="text/css" href="<%=Interface.Util.Root%>minify.aspx?main.css<%=Interface.Util.Bpc%>&amp;v=<%=Interface.Util.Version%>" media="screen" />
	<script type="text/javascript" src="<%=Interface.Util.Root%>minify.aspx?main.js|functions.js<%=Interface.Util.Bpc%>&amp;v=<%=Interface.Util.Version%>"></script>
</head>
<body>
	<form name="frm" id="frm" action="#" runat="server">
		<div id="content">
			<!--
			
			ooooo           oooooooooooo    ooooo          .o.                                          .o. 
			`888'           `888'     `8    `888'         .888.                                         888 
			 888             888             888         .8"888.                                        888 
			 888             888oooo8        888        .8' `888.                                       Y8P 
			 888             888    "        888       .88ooo8888.                                      `8' 
			 888       o     888       o     888      .8'     `888.                                     .o. 
			o888ooooood8    o888ooooood8    o888o    o88o     o8888o   ANTES DE PERGUNTAR PRO CARNEIRO  Y8P 

			E DEPOIS ME APAGUE!

			-->
			<div>
				<style type="text/css">
					body{
						padding:10px;
					}
					body, h1, h2, h3, p{
						font-family: Arial, Verdana, "Trebuchet MS";
						font-size:13px;
					}
					p, p *{
						color: #818285;
					}
					h1, h2, h3{
						color: #000000;
					}
					h1{
						padding-bottom:5px;
						border-bottom: 1px solid #818285;
						font-size:22px;
					}
					h2{
						font-size:19px;
					}
					h3{
						font-size:13px;
						margin-bottom:0px;
					}
					p{
						margin-top:0;
					}
					code{
						color: #FF8000;
						font-weight:bold;
					}
					.collapseContainer{
						width:45%;
						margin-right:5%;
						float:left;display:inline;
					}
					.collapse{
						padding:10px;
					}
					.collapse:hover{
						padding:10px;
						background:#F5F5FA;
					}
					.red,
					.red *{
						color:red;
					}
				</style>
				<h1>
					Módulo padrão C# v15.4.1 - utf-8
				</h1>
				
				<div class="collapseContainer">
					<h2>Variáveis do servidor</h2>
					<div class="collapse">
						<p>
							<strong>Sempre</strong> use as variáveis do servidor para chamar as páginas, assim não existe a dependência de um arquivo para uma pasta específica.
						</p>
						
						<h3>Título do site</h3>
						<p>
							<code>&lt;%=Interface.Config.Title%&gt;</code> = "<%=Interface.Config.Title%>"
						</p>
						<h3>Endereço da raíz do site</h3>
						<p>
							<code>&lt;%=Interface.Util.Root%&gt;</code> = "<%=Interface.Util.Root%>"
						</p>
						<h3>Endereço da raíz dos arquivos externos globais</h3>
						<p>
							<code>&lt;%=Interface.Util.GlobalPath%&gt;</code> = "<%=Interface.Util.GlobalPath%>"
						</p>
						<h3>Endereço da raíz dos arquivos externos de uma lígua específica (a princípio é "pt-BR" hard)</h3>
						<p>
							<code>&lt;%=Interface.Util.LanguagePath%&gt;</code> = "<%=Interface.Util.LanguagePath%>"
						</p>
						<h3>Lígua atual (a princípio é "pt-BR" hard)</h3>
						<p>
							<code>&lt;%=Interface.Util.Language%&gt;</code> = "<%=Interface.Util.Language%>"
						</p>
						<h3>Versão</h3>
						<p>
							Versão atual dos arquivos JS, CSS, imagens, etc. O valor é colocado "hard" dentro do arquivo. Sempre que precisar subir para produção uma versão atual do CSS, JS, etc deve-se alterar o valor de Version <strong>seguindo o seguinte padrão: major.minor[.build[.revision]]</strong> <a href="http://en.wikipedia.org/wiki/Software_versioning" target="_blank">(saiba mais sobre como criar um núnero de versão correto aqui)</a>.
						</p>
						<p>
							<code>&lt;%=Interface.Util.Version%&gt;</code> = "<%=Interface.Util.Version%>"
						</p>
						<p>
							<em>Classes se encontram em \App_Code\Interface\Config.cs e \App_Code\Interface\Util.cs</em>
						</p>						
					</div>
				</div>
				
				<div class="collapseContainer">
					<h2>minify.aspx</h2>
					<div class="collapse">
						<p>
							<strong>Todos os JS e CSS</strong> devem passar pela "<strong><%=Interface.Util.Root%>minify.aspx</strong>". Ela remove espaços duplos, enters, tabs e comentários, concatena arquivos em uma única requisição, mantém o arquivo em cache e retorna na compactação GZIP.
							No caso dos css, substitui a string <strong>"var(root)/"</strong> por "<%=Interface.Util.Root%>"
						</p>
						<p>
							Para usar chame o arquivo da seguinte forma:<br />
							"<code>&lt;%=Interface.Util.Root%&gt;minify.aspx?main.css|estilo.css|estilo2.css</code>"<br />
						</p>
						<p>
							Esta linha escreverá na página: <br />
							"<%=Interface.Util.Root%>minify.aspx?main.css|estilo.css|estilo2.css"
						</p>
						<p>
							Caso queira chamar um css específico de alguma língua, use<br />
							"<code>&lt;%=Interface.Util.Root%&gt;minify.aspx?main.css|estilo.css,&lt;%=Interface.Util.Language%&gt;|estilo2.css</code>"
						</p>
						<p>
							Esta linha escreverá na página: <br />
							"<%=Interface.Util.Root%>minify.aspx?main.css|estilo.css,<%=Interface.Util.Language%>|estilo2.css"
						</p>
						<p>
							Para os arquivos no formato original e desabilitar o CACHE do usuário, passe o parâmetro via querystring <code>bpc=1</code>.<br />
							"<code>&lt;%=Interface.Util.Root%&gt;minify.aspx?main.css|estilo.css,&lt;%=Interface.Util.Language%&gt;|estilo2.css&amp;bpc=1</code>"<br />
						</p>
						<p>
							Este parâmetro pode ser passado direto na página (no caso, a default.aspx):<br />
							"<%=Interface.Util.Root%>default.aspx?bpc=1"
						</p>
						<p>
							Para evitar problemas de imagens no CACHE do usuário deve-se passar o parâmetro <code>v=VERSÃO</code>.
							Este &quot;v&quot; será concatenado a todas as imagens de dentro dos CSS's. <strong class="red">Se <code>bpc=1</code> v será igual a um número randomico.</strong> Ex.:
							"<code>background:url(<%=Interface.Util.Root%>/locales/global/img/px.gif?v=<%=Interface.Util.Version%>) 0 0 no-repeat;</code>"<br />
						</p>
					</div>
				</div>
				
				
			</div>
			<!-- ME APAGUE! -->
		</div>
	</form>
	
	<%if(Interface.Util.Root.IndexOf(".") > -1){%>
	<script type="text/javascript">
		var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
		document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
	</script>
	<script type="text/javascript">
		var pageTracker = _gat._getTracker("##CODE##");
	</script>
	<%} else {%>
	<script type="text/javascript">
		var pageTracker = {
			_trackPageview: function (w){
				if(window['console']){
					if(console.info) console.info('analytics: "' + w + '"');
				}
			},
			_trackEvent: function (){
				if(window['console']){
					var _arrArguments = [];
					var _arrArgumentsLen = arguments.length;
					for(var i = 0; i< _arrArgumentsLen; i++) {
						_arrArguments[i] = arguments[i];
					}
					if(console.info) console.info('analytics event: ("' + _arrArguments.join("\",\"") + '")');
				}
			},
			_setVar: function (w){
				if(window['console']){
					if(console.info) console.info('analytics setvar: "' + w + '"');
				}
			}
		};
	</script>
	<%}%>
	<script type="text/javascript">
		pageTracker._trackPageview(cphCustomAnalytics);
		Init.run();
	</script>
</body>
</html>