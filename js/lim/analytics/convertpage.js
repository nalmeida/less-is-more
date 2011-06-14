if(!window['lim']){
	var lim = {};
}
if(!window.lim['analytics']){
	window.lim.analytics = {};
}
(function(scope, $) {
	// namespace
	if(!scope) scope = window;
	
	/**
	* Método para chamar uma ou mais páginas de conversão dentro de um iframe
	* @author Nicholas Almeida e Marcelo Carneiro
	* @since 5 Mai 2010 17:38:40 BRT
	* @param {string} ou {array} url desejada ou várias URLs 
	* @usage
		$(function(){
			lim.analytics.convertPage('<%=Common.Util.Root%>convertion/google-adwords.html');
			// ou
			lim.analytics.convertPage('http://www.google.com.br', 'http://w3.org');
		});
	*/
	scope.convertPage = function () {
		
		this.VERSION = '1.0.0';
		
		if(arguments.length == 0){
			throw new Error("ERRO!");
			return;
		}
		
		$.each(arguments, function(){
			$(document.body).append(
				$(document.createElement('iframe')).attr({
					src: this,
					id: 'convertPageIframe_' + parseInt(Math.random() * 10000)
				}).css({
					position:'absolute',
					top:'-1000px',
					left:'-1000px',
					width:'1px',
					height:'1px'
				}).load(function() {
					$(this).remove();
				})
			);
		});
	};

})(lim.analytics, jQuery);