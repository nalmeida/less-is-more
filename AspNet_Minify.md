# `Common.Minify` #

O objetivo da Minify é compactar e reduzir a quantidade de requisições reunindo as chamadas dos arquivos JS ou CSS.
Além disso, ela também possui alguns recursos que ajudam na construção de um CSS ([AspNet\_Minify#Minify\_e\_CSS](AspNet_Minify#Minify_e_CSS.md)).

O processo de uso da classe é dividido em 2 (ou 3) partes:

  * Registre os arquivos em **`App_Code/Global.asax.cs`**;
  * Crie os grupos (opcional) em **`App_Code/Global.asax.cs`**;
  * Insira a tag ou conteúdo na página final;

## 1. Registro dos arquivos ##

Os registros de arquivos são obrigatórios para processar e guardar o conteúdo no cache. Todos os registros precisam de um nome único que será usado como referencia no futuro.

<font color='#ff0000'><b>É altamente recomendável centralizar o registro de arquivos e grupos no global.asax.cs, como indicado acima.</b></font>

Existem 2 formas de registrar um arquivo:

### Registrando um arquivo local ###

Os arquivos locais são buscados dentro da pasta **`~/js/`** ou **`~/locales/global/css/`**:
```
<% Common.Minify.Register("JQuery", "jquery.js"); %> // será referenciado no futuro como "JQuery"
```

Existe um shortcut que atribue o nome do arquivo para o registro:
```
<% Common.Minify.Register("jquery.js"); %> // será referenciado no futuro como "jquery.js"
```


### Registrando um arquivo externo ###

É possível registrar um arquivo em outro domínio com fallback local:
```
<% Common.Minify.Register("jQuery", "jquery.js", "http://ajax.googleapis.com/ajax/libs/jquery/1.4.3/jquery.min.js"); %>
```
Caso o arquivo na CDN do google não esteja acessível, carrega o local.


## 2. Grupos ##

Após o registro dos arquivos é possível concatená-los em apenas uma requisição através dos grupos.
Cada grupo deve ter um nome único que será usado futuramente.

<font color='#ff0000'><b>É altamente recomendável centralizar o registro de arquivos e grupos no global.asax.cs, como indicado acima.</b></font>

Para criar um grupo:
```
<%
	// registra os arquivos
	Common.Minify.Register("jQuery", "jquery.js", "http://ajax.googleapis.com/ajax/libs/jquery/1.4.3/jquery.min.js");
	Common.Minify.Register("teste.js");
	Common.Minify.Register("core", "core/teste.js");
	
	// adiciona ao grupo
	Common.Minify.Add("Structure", "jQuery");
	Common.Minify.Add("Structure", "teste.js"); // "teste.js" é o id gerado pelo shortcut
	Common.Minify.Add("Structure", "core");
%>
```
Neste caso o grupo "Structure" contém os arquivos **`jQuery`**, **`teste.js`** e **`core`**.

**Importante**: Apenas arquivos do mesmo tipo podem fazer parte de um grupo.

## 3. Inserção ##

Esta etapa final insere a **chamada do arquivo** ou **seu conteúdo** na página.

### Inserção da tag ###

Os métodos `Common.Minify.WriteTag` são encarregados de inserir a tag html do CSS ou JS.

Ao escrever o código abaixo na aplicação "http://localhost/application/":
```
<%
	// registra os arquivos
	Common.Minify.Register("jQuery", "jquery.js", "http://ajax.googleapis.com/ajax/libs/jquery/1.4.3/jquery.min.js");
	Common.Minify.Register("teste.js");
	Common.Minify.Register("core", "core/teste.js");

	// adiciona ao grupo
	Common.Minify.Add("Structure", "jQuery");
	Common.Minify.Add("Structure", "teste.js"); // "teste.js" é o id gerado pelo shortcut
	Common.Minify.Add("Structure", "core");
	
	// escreve a tag
	Common.Minify.WriteTag("Structure");
%>
```

Resultará na tag abaixo, com o conteúdo de **`jquery.js`** + **`teste.js`** + **`core/teste.js`**.
```
<script type="text/javascript" src="http://localhost/application/minify.aspx?Structure"></script>
```

Além do método **`WriteTag`**, existe também o **`GetTag`**, que apenas retorna a string do resultado.

### Inserção do conteúdo ###

Caso seja necessário, é possível inserir o conteúdo de um JS ou CSS na página através do comando `Common.Minify.Write`:

```
<%
	// registra os arquivos
	Common.Minify.Register("jQuery", "jquery.js", "http://ajax.googleapis.com/ajax/libs/jquery/1.4.3/jquery.min.js");
	Common.Minify.Register("teste.js");
	Common.Minify.Register("core", "core/teste.js");

	// adiciona ao grupo
	Common.Minify.Add("Structure", "jQuery");
	Common.Minify.Add("Structure", "teste.js"); // "teste.js" é o id gerado pelo shortcut
	Common.Minify.Add("Structure", "core");
	
	// escreve a tag
	Common.Minify.Write("Structure");
%>
```
Resulta em:
```
(function(E,A){function U(){return false}function ba(){return true}function ja(a,b,d){d[0].type=a;return c.event.handle.apply(b,d)}function Ga(a){var b,d,e=[],f=[],h,k,l,n,s,v,B,D;k=c.data(this,this.nodeType?"events":"__events__");if(typeof k==="function")k=k.events;if(!(a.liveFired===this||!k||!k.live||a.button&&a.type==="click")){if(a.namespace)D=RegExp("(^|\\.)"+a.namespace.split(".").join("\\.(?:.*\\.)?")+"(\\.|$)");a.liveFired=this;var H=k.live.slice(0);for(n=0;n<H.length;n++){k=H[n];k.origType.replace(X,

(....... restante dos javascripts adicionado)
```


## Modo "debug" ##

No `Web.Config` é possível definir uma chave com o valor:
```
<add key="MinifyDebug" value="true"/>
```

Desta forma, todos os scripts serão escritos separadamente.


## Minify e JS ##

A Minify cria uma versão do objeto `Common.Util` para o JS deixando as propriedades abaixo acessíveis:

```
Common.Util.Root;
Common.Util.LanguagePath;
Common.Util.GlobalPath;
```

## Minify e CSS ##

Para arquivos CSS, além de concatenar, comprimir e guardar no cache do servidor, existem algumas palavras-chave que a minify substitui pelas variáveis de ambiente:

```
"$root/" por Common.Util.AssetsRoot;
"$global/" por Common.Util.GlobalPath;
"$language/" por Common.Util.LanguagePath;
```

Além disto é possível criar suas próprias variáveis dentro de um CSS:
```
$colorError{red}
$fc{
	overflow:hidden;
	zoom:1;
}

h1{
	color:$colorError;
	$fc
}
```