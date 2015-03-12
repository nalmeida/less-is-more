# Introdução #
Para facilitar a verificação da implementação das _marcações_ do Google Analytics, desenvolvemos uma técnica simples que consiste em:
  * Verificar se o site está rodando "localmente" (`http://localhost/...` ou `http://ENDERECO_IP`) usando as VariaveisDeAmbiente;
  * Em caso positivo, criar um objeto Javascript que "imita" os principais métodos da Classe real do Google Analytics;
  * Exibir um `console.info` das informações que seriam enviadas;

**Vantagens:**
  * Não é necessário estar conectado a internet;
  * É independente do carregamento do Javascript do Google Analytics;
  * Não envia dados para o servidor do Google. Isso previne aumentar enquanto se está desenvolvendo o site;
  * Dispensa o uso de programas do tipo _HTTP Sniffer_ ([HTTPFox](https://addons.mozilla.org/en-US/firefox/addon/6647/), [Fiddler](http://www.fiddler2.com/fiddler2/), etc) para verificar o que está sendo enviado.

**Desvantagens:**
  * Só funciona em navegadores que possuem a classe "`console`".

**Leia mais sobre [síncrono e assíncrono](http://pt.wikipedia.org/wiki/Comunica%C3%A7%C3%A3o_s%C3%ADncrona)**

## Google Analytics Síncrono ou Assíncrono? ##

Em Dezembro de 2009 o Google lançou uma nova tag para o Google Analytics como uma forma alternativa para a coleta de dados de maneira assíncrona- Asynchronous Snippet.

As empresas que possuem códigos pesados com muitos scripts em suas páginas sempre ficaram preocupados se o código do GA poderia influenciar no carregamento da página no browser dos visitantes, porém com este novo código isso não será mais uma preocupação, já que ele trabalha “em separado” da programação da página do seu site para que possa ser reduzido o tempo de carregamento de sua página.

As vantagens para o uso deste novo código de coleta de dados são:

  * Carga do código de rastreamento mais rápido em suas páginas da web devido a execução navegador melhorado
  * Coleta de dados e precisão
  * Eliminação da dependências quando o Javascript não foi totalmente carregado

O código de controle assíncrono está agora disponível para todos os usuários do Google Analytics. O uso do novo código de monitoramento é opcional: o código do Google Analytics existente continuará a trabalhar como está. Mas para melhorar seus tempos de carga e afinar a precisão dos dados do Google Analytics esta nova forma de coleta é a ideal.

# Teste local do método assíncrono (_novo_) #

O código abaixo é usado a para testar localmente as chamadas do Google Analytics **assíncrono**. É basicamente a criação de um objeto com métodos que "imitam" os do Analytics só que, ao invés de mandar os dados para o Google, exibem um `console.info` do que _seria_ enviado. Deve ser colocado **dentro do `<head>`**.

```
<% if(Common.Util.isLocal){ %>
<script type="text/javascript">
	var _gaq = {
		push: function(p_arr) {
			var arr = p_arr;
			var func = arr[0];
			arr.shift();
			arr = arr.join('\',\'');
			if(window['console']) console.info('_gaq.push([\'' + func + '\',\'' + arr + '\']);');
		}				
	}
</script>
<% } else { %>
<script type="text/javascript">
	(function() {
		var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
		ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
		var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
	})();
</script>
<% } %>
<script type="text/javascript">
	var _gaq = _gaq || [];
	_gaq.push(['_setAccount', '##CODE##']);
	_gaq.push(['_trackPageview','']);
</script>
```

# Teste local do método síncrono (_antigo_) #

O código abaixo é usado a para testar localmente as chamadas do Google Analytics **ssíncrono**. É basicamente a criação de um objeto com métodos que "imitam" os do Analytics só que, ao invés de mandar os dados para o Google, exibem um `console.info` do que _seria_ enviado. Deve ser colocado **antes do `</body>`**.

```
<% if(Common.Util.isLocal){ %>
<script type="text/javascript">
	var pageTracker = {
		_trackPageview: function (w){
			if(window['console']){
				if(console.info) console.info('pageTracker._trackPageview("' + w + '")');
			}
		},
		_trackEvent: function (){
			if(window['console']){
				var _arrArguments = [];
				var _arrArgumentsLen = arguments.length;
				for(var i = 0; i< _arrArgumentsLen; i++) {
					_arrArguments[i] = arguments[i];
				}
				if(console.info) console.info('pageTracker._trackEvent("' + _arrArguments.join("\",\"") + '")');
			}
		},
		_setVar: function (w){
			if(window['console']){
				if(console.info) console.info('pageTracker._setVar: "' + w + '"');
			}
		}
	};
</script>

<% } else { %>
<script type="text/javascript">
	var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
	document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
	var pageTracker = _gat._getTracker("##CODE##");
</script>
<% } %>
<script type="text/javascript">
	pageTracker._trackPageview('');
</script>
```