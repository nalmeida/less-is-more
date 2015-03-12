# Variáveis de Ambiente - Common.Util.`*` #

Todo projeto LIM possui um pacote de "utilidades" do servidor. Entre elas estão as Variáveis de Ambiente que são acessíveis através da classe estática `Commom.Util`.

Esta classe se encontra em \App`_`Code\Common.Interface\Util.cs.

Todos os caminhos de arquivos JS, CSS, IMG, SWFs, etc. ou mesmo links de navegações internos **devem** utilizar estas variáves.


## `Common.Util.Root` - a raíz do site ##

Utilize a propriedade `Common.Util.Root` para escrever o caminho da raíz da aplicação (que pode ser um diretório virtual).

Se sua aplicação está configurada em "http://localhost/application/":
```
<a href="<%=Common.Util.Root%>interna/teste.aspx">link</a>
```
resultará em
```
<a href="http://localhost/application/interna/teste.aspx">link</a>
```


## `Common.Util.AssetsRoot` - a raíz do servidor de "assets" ##

"Assets" é tudo que define as características visuais do site e é "fixo" (JPG, GIF, PNG, SWF, XML, CSS, etc que são parte da estrutura do site). Sua principal função é ser o "root" dos assets quando se usa uma  [http://en.wikipedia.org/wiki/Content\_delivery\_network CDN](.md).

A chave `ROOT-ASSETS` é definida no `Web.Config`. Caso não exista, o valor de `Config.Util.Root` é utilizado.

Este valor é utilizado nas propriedades `Common.Util.LanguagePath` e `Common.Util.GlobalPath` citadas a seguir e dificilmente é chamado direto.

Exemplo de aplicação:
```
<%=Common.Util.AssetsRoot%> // http://cdn.projeto/assets/
// ou, caso não esteja definido no Web.Config:
<%=Common.Util.AssetsRoot%> // http://localhost/
```


## `Common.Util.GlobalPath` - a raíz dos assets "globais" ##

Os assets são divididos em "globais" e "locais" (por idioma) e sempre devem ser chamados usando as propriedades `Common.Util.LanguagePath` ou `Common.Util.GlobalPath`.

Ou seja, na aplicação "http://localhost/application/", a tag abaixo:
```
<img src="<%=Common.Util.GlobalPath%>img/asset.png" />
```
resultará em
```
<img src="http://localhost/application/locales/global/img/asset.png" />
```


## `Common.Util.LanguagePath` - a raíz dos assets do idioma atual ##

Retorna o caminho dos assets "locais":

Na aplicação "http://localhost/application/", a tag abaixo:
```
<img src="<%=Common.Util.LanguagePath%>img/asset.png" />
```
resultará em
```
<img src="http://localhost/application/locales/pt-BR/img/asset.png" />
```

## `Common.Util.Language` - idioma atual ##

A princípio retorna "pt-BR" estático. Serve de preparação para caso o site se torne multilíngua. Este valor é utilizado na `Common.Util.LanguagePath`.


## `Common.Util.UploadsRoot` - a raíz do servidor de "uploads" ##

Caminho para arquivos enviados de forma dinâmica para o servidor (criados por um sistema ou admin).

A chave `ROOT-UPLOADS` é definida no `Web.Config`, Caso não exista, retorna o valor de `Config.Util.AssetsRoot`.

## `Common.Util.isLocal` - se o site está rodando localmente ##

Se o site está rodando "locamente" (http://localhost/, http://projeto/ ou em um IP).

## Versão ##
Versão atual dos arquivos JS, CSS, imagens, etc. Utilizado para forçar a limpeza do cache.
```
<%=Common.Util.Version%> // 1.0.0
```