# Master Pages #

Todo o conteúdo padrão para as páginas estão no arquivo "~/Views/Shared/MarterPage.master". Este arquivo serve de modelo para as páginas internas.

Abaixo uma referência rápida sobre como utilizar. Para uma referência completa, acesse http://msdn.microsoft.com/en-us/library/wtxbf3hh.aspx.

## Arquivo master ##

A `MasterPage.master` precisa conter a diretiva:
```
<%@ Master Language="C#" %>
```

Para criar um conteúdo variável, crie uma tag seguindo a lógica abaixo:
```
<asp:contentplaceholder id="cphIdDoPlaceHolder" runat="server" >
	código padrão (se houver)
</asp:contentplaceholder>
```

Caso exista a necessidade de utilizar uma tag asp (ex: `<%=Variavel %>`) dentro do conteúdo default de um contentplaceholder usar o "`#`" no lugar do "`=`" (ex: `<%#Variavel %>`) , senão o conteúdo não será substituído nas páginas filhas.

## Arquivo da página ##

No arquivo da página (na default.aspx, por exemplo), coloque a diretiva:
```
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" %>
```

A página será um XML e conterá apenas os nós relativos aos `contentPlaceHolders` da `MasterPage.master`:
```
<asp:content contentplaceholderid="cphIdDoPlaceHolder" runat="server">
	código que entrará no lugar da tag
</asp:content>
```

Para ver a `MasterPage.Master` completa, acesse os links:
  * http://code.google.com/p/less-is-more/source/browse/trunk/DotNET-C-Sharp/web-FW-3.5-MVC-1.0/Views/Shared/MasterPage.Master
  * http://code.google.com/p/less-is-more/source/browse/trunk/DotNET-C-Sharp/web-FW-3.5-MVC-1.0/Views/Default/Index.aspx