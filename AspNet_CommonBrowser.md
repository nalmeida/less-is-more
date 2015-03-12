# `Common.Browser` - Classe para detecção de browser + versão #

Compara e verifica qual browser está sendo usado através do `user-agent`. A sigla do browser por enquanto é definida dentro da classe (na construtora), assim como a lógica para verificação da versão.


## `Common.Browser.Is` ##

Verifica se é o browser passado. Formas possíveis de uso:

  * `Common.Browser.Is("ie")`: Verifica se é Internet Explorer (independente da versão);
  * `Common.Browser.Is("ie", "6")`: Verifica se é IE 6;
  * `Common.Browser.Is("ie", ">=6")`: Verifica se é maior-igual ao IE 6;
  * `Common.Browser.Is("ie", ">6")`: Verifica se é maior ao IE 6;
  * `Common.Browser.Is("ie", "<6")`: Verifica se é menor ao IE 6;
  * `Common.Browser.Is("ie", "<=6")`: Verifica se é menor-igual ao IE 6;

É possível definir a versão de forma mais específica:

  * `Common.Browser.Is("firefox", "3.5")`: Verifica se é Firefox 3.5;
  * `Common.Browser.Is("firefox", ">3.5")`: Verifica se é maior ao Firefox 3.5;
  * `Common.Browser.Is("firefox", ">=3.6")`: Verifica se é maior-igual ao firefox 3.6;


### Valores possíveis ###

  * `internet-explorer` ou `ie`;
  * `firefox`;
  * `google-chrome` ou `chrome`;
  * `safari`;
  * `iphone`;
  * `ipad`;
  * `opera`;


## `Common.Browser.IsAny` ##

O método "IsAny" verifica se é ao menos um dos browsers listados. O método de chamada dos browsers é o mesmo do método Is, porém separando os parâmetros por espaço:
```
<% if(Common.Browser.IsAny("safari", "firefox >=3.6", "chrome 6")){%>
	Olá safari, firefox maior que 3.6 ou chrome 6!;
<%}%>
```


## `Common.Browser.getVersion` ##

Retorna a versão do browser atual.


## `Common.Browser.getName` ##

Retorna o nome do browser.


## `Common.Browser.getCSSClassName` ##

Retorna o nome do browser + nome e versão para uso em classes do CSS. Exemplo:

```
<%=Common.Browser.getCSSClassName()%> // "ie ie6";
```