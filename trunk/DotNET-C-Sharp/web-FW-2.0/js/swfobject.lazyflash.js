/**
 * "Package" for flash stuff
   @sample:
	$(function(){
		lazyFlash('<%=Common.Util.GlobalPath%>swf/main.swf', 'flashSite', '100%', '100%', {
			flashvars:{
				bpc: '<%=Common.Util.Bpc%>',
				root: '<%=Common.Util.GlobalPath%>swf/'
			},
			expressInstall: '<%=Common.Util.GlobalPath%>swf/expressInstall.swf',
			attributes: {
				bgcolor: '#000000'
			}
		});
	});
 */
(function($, swfObject, scope){
	
	if(!$ || !swfObject){
		throw new Error ('jQuery 1.3.2 and swfObject 2.2 is needed for "fbiz.lazyFlash"');
		return;
	}

	/**
	 * Automates flash insertion
	 * @param {String} url
	 * @param {String} id
	 * @param {Number} width
	 * @param {Number} height
	 * @param {Object} data
	 * @option {Object} flashvars
	 * @option {Object} params
	 * @option {Object} attributes
	 * @option {String} expressInstall
	 * @option {String} version
	 * @needs {swfObject} {jQuery}
	 */
	scope.lazyFlash = function(url, id, width, height, data){
		data = data || {};
		
		var flashvars = data.flashvars || {};
		var params = $.extend({
			menu: "false",
			allowFullScreen: true,
			allowscriptaccess: 'always'
		}, data.params || {});
		
		var attributes = $.extend({
			name: id
		}, data.attributes || {});
		
		swfobject.embedSWF(url, id, width, height, data.version || "9.0.0", data.expressInstall, flashvars, params, attributes);
	};

})(jQuery, swfobject, window);
