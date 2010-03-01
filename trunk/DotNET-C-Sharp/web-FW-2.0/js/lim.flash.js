if(!window['lim']){
	var lim = {};
}
(function(scope, $, swfobject) {
	
	// Requirements
	if(!$ || !swfobject){
		throw new Error ('jQuery 1.3.2 and swfObject 2.2 is needed for "flash"');
		return;
	}
	
	// namespace
	if(!scope) scope = window;
	scope.flash = {};
	
	// Version
	scope.flash.VERSION = '1.1.0';
	
	/**
	 * Substitui um elemento HTML na tela por um "flash" usando a SWFObject
	 * @author Marcelo Carneiro e Nicholas Almeida
	 * @since 18:15 27/1/2010
	 * 
	 * @param {string} p_url URL do SWF
	 * @param {string} p_id ID do elemento que será substituido pelo flash
	 * @param {string} p_width Largura (pode ser em px ou %)
	 * @param {string} p_height Altura (pode ser em px ou %)
	 * @param {object} p_extra Objeto com as configurações extras para a swfobject
	 * @param {object} 	.flashvars Variáveis passadas para o flash
	 * @param {object} 	.params Parâmetros passados para o flash
	 *  					- play
	 *  					- loop
	 *  					- menu
	 *  					- quality
	 *  					- scale
	 *  					- salign
	 *  					- wmode
	 *  					- swliveconnect
	 *  					- allowscriptaccess
	 *  					- seamlesstabbing 
	 *  					- allowfullscreen
	 *  					- allownetworking
	 * @param {object} 	.attributes Atributos do flash
	 *  					- id
	 *  					- name
	 *  					- class
	 *  					- align
	 *  					- bgcolor
	 * @param {string} 	.expressInstall Caminho do arquivo expressInstall.swf
	 * @param {function}.onComplete Função executada assim que o flash é adicionado com sucesso. Passa um objeto com ref (o próprio elemento flash), id (id do flash) e success (true se foi adicionado com sucesso)
	 * @see http://code.google.com/p/swfobject
	 * @usage
		$(function(){
			function onFlashAdded(data) {
				data.ref;
				data.id;
				data.success;
			}
			var extra = {
				flashvars:{
					bpc: '<%=Common.Util.Bpc%>',
					root: '<%=Common.Util.GlobalPath%>swf/'
				},
				expressInstall: '<%=Common.Util.GlobalPath%>swf/expressInstall.swf',
				attributes: {
					bgcolor: '#000000'
				}, 
				onComplete: onFlashAdded
			};
			lim.Flash.add('<%=Common.Util.GlobalPath%>swf/fake_flash.swf', '#flashHolder', '100%', 120, extra);
		});
	 */
	scope.flash.add = function(p_url, p_id, p_width, p_height, p_extra){
		
		// Constants
		var FLASHPLAYER_DEFAULT_VERSION = '9.0.0';
		
		/**
		 * Private
		 */
		var extra = p_extra || {};
		var id = p_id.replace(/\#/gi,''); // remove o # caso venha com um
		var flashvars = extra.flashvars || {};
		
		var params = $.extend({
			menu: "false",
			allowFullScreen: true,
			allowscriptaccess: 'always'
		}, extra.params || {});
		
		var attributes = $.extend({
			name: id
		}, extra.attributes || {});

		swfobject.embedSWF(
			p_url,
			id,
			p_width,
			p_height,
			extra.version || FLASHPLAYER_DEFAULT_VERSION,
			extra.expressInstall,
			flashvars,
			params,
			attributes,
			extra.onComplete
		);
	};
	
})(lim, jQuery, swfobject);