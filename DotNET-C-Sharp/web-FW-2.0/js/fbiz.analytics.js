(function(scope, $) {
	// namespace
	if(!scope.fbiz) scope.fbiz = {};
	
	/**
	 * Classe "estática" para chamar marcações do Google Analytics
	 * @author Nicholas Almeida
	 * @since 19:18 13/8/2009
	 * @see Para testar em localhost com o analytics "real", colocar pageTracker._setDomainName("none");
	 */
	var Analytics = function () {
		// Constants
		this.VERSION = '1.0.0';
		
		/**
		 * Private
		 */
		var _timeout = 250;
		var _pageTracker = 'pageTracker';
	
		/**
		 * Atribui valor para _pageTracker
		 */
		this.setTracker = function(trackerFunction) {
			_pageTracker = trackerFunction;
		};
	
		/**
		 * Retorna a pageTracker usada
		 */
		this.getTracker = function() {
			return scope[_pageTracker];
		};
	
		/**
		 * Executa um "_trackPageview"
		 * @param {string} str String da marcação desejada
		 */
		this.track = function(str) {
			this.getTracker()._trackPageview(str);
		};
	
		/**
		 * Executa um "_trackPageview" e abre uma URL (window.open)
		 * @param {string} url desejada
		 * @param {string} target por padrão '_self'
		 * @param {string} str String da marcação desejada
		 */
		this.trackAndGo = function(url, target, str) {
			this.track(str);
			if(!target) target = '_self';
			if(!url) {
				throw new Error('[ERROR] fbiz.analytics.trackAndGo: undefined URL.');
			} else {
				setTimeout(function(){
					window.open(url, target);
				}, this._timeout);
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
				throw new Error('[ERROR] fbiz.analytics.trackEvent: trackEvent must contain "category" and "action" parameters.');
			} else {
				if((optional_value && isNaN(optional_value)) || optional_value === true) {
					throw new Error('[ERROR] fbiz.analytics.trackEvent: optional_value must be a number.');
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
			if(!target) target = '_self';
			if(!url) {
				throw new Error('[ERROR] fbiz.analytics.trackAndGo: undefined URL.');
			} else {
				setTimeout(function(){
					window.open(url, target);
				}, this._timeout);
			}
		};

		/**
		 * Executa uma função JS (_timeout 250 milisegundos)
		 * @param {function} func Função javascript válida
		 */
		this.callJs = function(func) {
			setTimeout(function() {
				try {
					func();
				} catch(e) {
					throw new Error('[ERROR] fbiz.analytics.callJs: undefined function called.');
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
	
	// Static 
	scope.fbiz.Analytics = new Analytics();
	
})(window, jQuery);