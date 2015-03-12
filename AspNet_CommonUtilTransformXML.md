# `Common.Util.TransformXML` - Classe para transformação de XML utilizando XSLT #

O método `TransformXML` carrega o XML e aplica a folha de estilo (XSL). Também pode receber parametros para serem usados no XSL.

Atualmente as variáveis `Root`, `LanguagePath` e `GlobalPath` já são passadas para qualquer XSL como `$root`, `$languagePath` e `$globalPath`.

Ex. de aplicação:

Apenas a transformação:
```
<% Common.Util.TransformXML("teste.xml", "estilo.xsl"); %>
```

Transformação passando parâmetros:
```
<%
Common.Util.TransformXML("teste.xml", "estilo.xsl", new String[][] {
	new String[]{"filtro-bairro", "", "itaim"},
	new String[]{"filtro-cidade", "", "sp"}
});
%>
```

### Referência sobre XSL ###

  * http://cafeconleche.org/books/xmljava/chapters/ch17.html