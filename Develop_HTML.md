# Padrões de desenvolvimento do HTML #

  * Todo o código HTML é [válido segundo os padrões W3C](http://validator.w3.org/);
  * Atualmente todo o HTML utiliza o **Doctype XHTML 1.0 Transitional**;
```
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
```
  * Os atributos HTML usam aspas dúplas `"` (e aspas simples no [Javascript](Develop_JS.md)). Ex.: `<div id="teste" />`;
  * As tags `title`, `meta description` e `meta keywords` são definidas dinamicamente através de um xml para SEO;
  * Toda indentação é feita utilizando **tabulação** ao invés de espaço;
  * Todos os comentários são feitos usando a linguagem nativa do servidor. Exemplo no ASP.NET: `<% // Comentário %>` ao invés de `<!-- Comentário -->`