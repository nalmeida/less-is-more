# Padrões de desenvolvimento do Javascript #

Regras seguidas para a construção de classes javascript para a LIM:

  * As classes sempre faz parte do "namespace" `lim`;
  * Todas as classes estão "encapsuladas" utilizando [\_closures\_](http://jibbering.com/faq/notes/closures/);
  * Documentação do código (quando necessária) utilizando [JSDoc](http://code.google.com/p/jsdoc-toolkit/w/list);
  * Referência para o padrão de escrita - [jQuery Core Style Guideline](http://docs.jquery.com/JQuery_Core_Style_Guidelines);
  * Classes validadas no [JSLint](http://www.jslint.com/);
  * Atualmente o framework utiliza a [jQuery](http://jquery.com) 1.4.2;

## Convenção da sintaxe ##

O estilo de indentação utilizado é o 1TBS ([The One True Brace Style](http://en.wikipedia.org/wiki/Indent_style#Variant:_1TBS)):

Principais princípios 1TBS:

  * As instruções de código deve estar em linhas separadas;
  * Abrir `{` a cada `if`, `else`, `while`, `function`, etc. mesmo que estes só tenham uma linha de instrução;
  * O `{` deve ficar na mesma linha do início do comando. Ex.:
```
function teste() {
```
  * Todos os fins de instrução devem terminar com `;` (ponto e vírgula);

Em adição ao estilo 1TBS também é adotado:

  * Utilização de strings com **aspas simples** `'` para o código JS e **aspas duplas** `"` **para o código HTML**;
  * Todas as Classes e métodos seguem o padrão [CamelCase](http://pt.wikipedia.org/wiki/CamelCase). Classes começam com a prileira letra maiúscula e métodos minúscula;
  * Constantes são escritas em caixa alta;
  * Variáveis ou métodos privados ou "protegidos" começam com `_` (underline);
  * Parâmetros recebidos por uma função ou método começam com `p_`;
  * Toda indentação é feita utilizando **tabulação** ao invés de espaço;

## Exemplo ##
```
var Test = function(p_some) { // Classe
	
	this.CONSTANT = 'constante';
	
	var _privateVar = 'variável privada';
	
	this.publicVar = 'variável pública';
	
	this._protectedVar = 'variável protegida';
	
	this.publicMethod = function(p_thing) {
		// Método público
	};
	
};
var newTest = new Test();
```

## Classe Modelo ##
```
/**
 * Nome da Classe
 * Breve descrição do que a Classe faz em Português
 * 
 * @author Autor 1
 * @author Autor 2 ...
 * @version 0.0.0
 * @requires NomeDasOutrasClassesNecessaria1 NomeDasOutrasClassesNecessaria2
 * @example:
   var example = new Example();
   ...
*/

if(!window['lim']){
	var lim = {};
}
(function(namespace, ...){
	
	var StaticName = {
		VERSION: '0.0.0',
		
		Class: function () { 
			
		}
	}
	
	// Static Class
	namespace.StaticName = new StaticName.Class();
	namespace.StaticName.Class = StaticName.Class;
	
	// Dynamic Class
	namespace.StaticName = StaticName.Class;
	
})(lim, ...);
```