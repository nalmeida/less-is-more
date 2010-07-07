if(!window['lim']){
	var lim = {};
}
(function(scope) {
	// namespace
	if(!scope) scope = window;
	
	/**
	 * Classe para chamar marcações do Google Analytics
	 * @author Nicholas Almeida
	 * @since 19:18 13/8/2009
	 * @see Para testar em localhost com o analytics "real", colocar pageTracker._setDomainName("none");
	 */
	scope.Analytics = function () {
		
		/**
		 * Private
		 */
		var _timeout = 250;
		var _pageTrackerMethodName = 'pageTracker';
		var _pageTracker = window[_pageTrackerMethodName];
	
		this.gotoPage = function(url, target){
			target = target || '_self';
			switch(target){
				case '_self':
					setTimeout(function(){
						document.location.href = url;
					}, this._timeout);
					break;
				default:
					window.open(url, target);
					break;
			}
		}
	
		/**
		 * Atribui valor para _pageTracker
		 */
		this.setTracker = function(trackerFunction) {
			_pageTracker =  typeof(trackerFunction) == 'function' ? trackerFunction : window[trackerFunction];
		};
	
		/**
		 * Retorna a pageTracker usada
		 */
		this.getTracker = function() {
			return _pageTracker != null ? _pageTracker : window[_pageTrackerMethodName];
		};
	
		/**
		 * Executa um "_trackPageview"
		 * @param {string} str String da marcação desejada
		 */
		this.track = function(str) {
			_pageTracker._trackPageview(str);
		};
	
		/**
		 * Executa um "_trackPageview" e abre uma URL (window.open)
		 * @param {string} url desejada
		 * @param {string} target por padrão '_self'
		 * @param {string} str String da marcação desejada
		 */
		this.trackAndGo = function(url, target, str) {
			this.track(str);
			if(!url) {
				throw new Error('[ERROR] lim.analytics.trackAndGo: undefined URL.');
			} else {
				this.gotoPage(url, target);
			}
		};
	
		/**
		 * Executa um "_trackPageview" e após uma função JS (_timeout 250 milisegundos)
		 * @param {string} str String da marcação desejada
		 * @param {function} func Função javascript válida
		 */
		this.trackAndJs = function(func, str) {
			this.track(str);
			this.callJs(func);
		};
	
		/**
		 * Executa um "_trackEvent"
		 * @param {string} category String da categoria
		 * @param {string} action String da ação
		 * @param {string} optional_label String do label
		 * @param {number} optional_value
		 */
		this.trackEvent = function(category, action, optional_label, optional_value) {
			if(!category || !action) {
				throw new Error('[ERROR] lim.analytics.trackEvent: trackEvent must contain "category" and "action" parameters.');
			} else {
				if((optional_value && isNaN(optional_value)) || optional_value === true) {
					throw new Error('[ERROR] lim.analytics.trackEvent: optional_value must be a number.');
				} else {
					this.getTracker()._trackEvent(category, action, optional_label, optional_value);
				}
			}
		};
	
		/**
		 * Executa um "_trackEvent" e após uma função JS (_timeout 250 milisegundos)
		 * @param {function} func Função javascript válida
		 * @param {string} category String da categoria
		 * @param {string} action String da ação
		 * @param {string} optional_label String do label
		 * @param {number} optional_value
		 */
		this.trackEventAndJs = function(func, category, action, optional_label, optional_value) {
			this.trackEvent(category, action, optional_label, optional_value);
			this.callJs(func);
		};
	
		/**
		 * Executa um "_trackEvent"  e abre uma URL (window.open)
		 * @param {string} url desejada
		 * @param {string} target por padrão '_self'
		 * @param {string} category String da categoria
		 * @param {string} action String da ação
		 * @param {string} optional_label String do label
		 * @param {number} optional_value
		 */
		this.trackEventAndGo = function(url, target, category, action, optional_label, optional_value) {
			this.trackEvent(category, action, optional_label, optional_value);
			if(!url) {
				throw new Error('[ERROR] lim.analytics.trackAndGo: undefined URL.');
			} else {
				this.gotoPage(url, target);
			}
		};

		/**
		 * Executa uma função JS (_timeout 250 milisegundos)
		 * @param {function} func Função javascript válida
		 */
		this.callJs = function(func) {
			setTimeout(function() {
				if(typeof(func) == 'function'){
					func();
				}else{
					throw new Error('[ERROR] lim.analytics.callJs: undefined function called.');
				}
			}, this._timeout);
		};
	
		/**
		 * Executa um "_setVar"
		 * @param {string} str String da marcação desejada
		 */
		this.setVar = function(str) {
			this.getTracker()._setVar(str);
		};
	};
	
	// constants
	scope.Analytics.VERSION = '1.0.1';
	
	// Instancia pre gerada
	scope.analytics = new scope.Analytics();
	
})(lim);