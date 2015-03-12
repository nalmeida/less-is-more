# Variáveis de Ambiente - Common.Config.`*` #

Este é um conjunto de propriedades que definem os dados do projeto como Nome do Autor, Copyright, Título da Página, Keywords, Description, etc.

A classe se encontra em \App`_`Code\Common.Interface\Config.cs.

Nesta classe as propriedades `Common.Config.Copyright`, `Common.Config.AuthorName`, `Common.Config.AuthorAddress`, `Common.Config.CompanyName` são estáticos e servem apenas para centralizar os dados.

Já o `Common.Config.Title`, `Common.Config.Description`, `Common.Config.Keywords`,  `Common.Config.H1` são dinâmicos através do preenchimento de um XML específico para SEO.

## seo.xml ##

As tags `title`, `h1`, `meta description`, e `meta keywords` das páginas a podem ser dinâmicos, buscando dados do arquivo encontrado em /App`_`Code/seo.xml.

As propriedades `Common.Config.Title`, `Common.Config.Description`, `Common.Config.Keywords`,  `Common.Config.H1`, buscam os dados do xml baseados na url atual. No caso abaixo:

```
<SEO>
	<pages>
		<url>/</url>
		<h1>H1 Módulo Padrão C# 16.2 MVC - utf-8</h1>
		<description>Modulo Padrão description</description>
		<keywords>Módulo Padrão keyword</keywords>
		<title>Módulo Padrão C# 16.2 MVC - utf-8</title>
		<frequency>3</frequency>
		<priority>2</priority>
	</pages>
	<pages>
		<url>/teste.aspx</url>
		<h1>Teste</h1>
		<description>Modulo Padrão description</description>
		<keywords>Módulo Padrão keyword</keywords>
		<title>Teste</title>
		<frequency>3</frequency>
		<priority>2</priority>
	</pages>
	...
</SEO>
```

No caso da aplicação "http://localhost/application/" o `title` seria "Módulo Padrão C# 16.2 MVC - utf-8".

Em "http://localhost/application/teste.aspx", o `title` seria "Teste".