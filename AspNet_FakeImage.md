# Fake Image #

A Fake Image pode ser útil para prever o tamanho de imagens dinâmicas evitando gerar arquivos desnecessários. Apesar de hoje em dia existirem muitos serviços externos que fazem isso muito bem, é sempre bom ter alguma coisa funcionando local.

Para utilizar apenas chamar o handler passando `width`, `height` (ou `quad` para "quadrados") e `color` (opcional com o hexadecimal da cor):

```
<img src="<%=Common.Util.GlobalPath%>img/fake_image.aspx?width=100&amp;height=200&amp;color=fefede" />
```

Se nada for passado, retorna um quadrado de 100px.