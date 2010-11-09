/**
 * Cria um Objeto pageTracker com os métodos de _trackPageview e _trackEvent para contabilização do Analytics.
 * IMPORTANTE: O "post" dos dados precisa ser feito para uma versão modificada do FBGA disponível em: https://svn.desenv:8443/svn/interface/facebook/fbga/www/
 * @author Nicholas Almeida
 * @since 18/10/2010
 */
var pageTracker = {
	PAGE_VIEW: 'trackPageview',
	PAGE_EVENT: 'trackEvent',
	UACode: null,
	URLProxy: null,
	domain: 'facebook.com',
	title: '',
	_testRequiredParameters: function() {
		if(!pageTracker.UACode) {
			alert('[pageTracker.' + p_obj.func + '] pageTracker.UACode is undefined;', 'ERROR');
			return false;
		}
		if(!pageTracker.URLProxy) {
			alert('[pageTracker.' + p_obj.func + '] pageTracker.URLProxy is undefined;', 'ERROR');
			return false;
		}
		if(!pageTracker.domain) {
			alert('[pageTracker.' + p_obj.func + '] pageTracker.domain is undefined;', 'ERROR');
			return false;
		}	
	},
	_trackPageview: function (p_str){
		
		var tmpObj = {
			url: pageTracker.URLProxy,
			cache: false,
			data: {
				'googlecode': 		pageTracker.UACode,
				'googledomain': 	pageTracker.domain,
				
				'pagetitle': 		pageTracker.title,
				'pagelink': 		p_str,
				
				'func': 			pageTracker.PAGE_VIEW
			}
		};
		
		pageTracker._testRequiredParameters(tmpObj.data);
		
		FbjQuery.ajax(tmpObj);
	},
	_trackEvent: function (p_category, p_action, p_label, p_value){
		
		var tmpObj = {
			url: pageTracker.URLProxy,
			cache: false,
			data: {
				'googlecode': 		pageTracker.UACode,
				'googledomain': 	pageTracker.domain,
				
				'category': 		p_category || '',
				'action': 			p_action || '',
				'label': 			p_label || '',
				'value': 			p_value || '',
				
				'func': 			pageTracker.PAGE_VIEW
			}
		};
		
		pageTracker._testRequiredParameters(tmpObj.data);
		
		FbjQuery.ajax(tmpObj);
		
	},
	_setVar: function (w){
	}
	
};