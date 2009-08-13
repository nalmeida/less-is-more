/**
 * Classe estática para chamar marcações do Google Analytics
 * @author Nicholas Almeida
 * @since 19:18 13/8/2009
 */

var Analytics = {
	
	/**
	 * Private
	 */
	_timeout: 250,
	_pageTracker: 'pageTracker',
	
	/**
	 * Atribui valor para _pageTracker
	 */
	setTracker: function(trackerFunction) {
		this._pageTracker = trackerFunction;
	},
	
	/**
	 * Retorna a pageTracker usada
	 */
	getTracker: function() {
		return (typeof(this._pageTracker) == 'string') ? window[this._pageTracker] : this._pageTracker;
	},
	
	/**
	 * Executa um "_trackPageview"
	 * @param {string} str String da marcação desejada
	 */
	track: function(str) {
		this.getTracker()._trackPageview(str);
	},
	
	/**
	 * Executa um "_trackPageview" e abre uma URL (window.open)
	 * @param {string} str String da marcação desejada
	 * @param {string} url desejada
	 * @param {string} target por padrão '_self'
	 */
	trackAndGo: function(str, url, target) {
		this.track(str);
		if(!target) target = '_self';
		if(!url) {
			throw new Error('[ERROR] Analytics.trackAndGo: undefined URL.');
			return;
		}
		if(!url) {
			throw new Error('[ERROR] Analytics.trackAndGo: undefined URL.');
		} else {
			setTimeout(function(){
				window.open(url, target);
			}, this._timeout);
		}
	},
	
	/**
	 * Executa um "_trackPageview" e após uma função JS (_timeout 250 milisegundos)
	 * @param {function} func Função javascript válida
	 * @param {string} str String da marcação desejada
	 */
	trackAndJs: function(str, func) {
		this.track(str);
		this.callJs(func);
	},
	
	/**
	 * Executa um "_trackEvent"
	 * @param {string} category String da categoria
	 * @param {string} action String da ação
	 * @param {string} optional_label String do label
	 * @param {number} optional_value
	 */
	trackEvent: function(category, action, optional_label, optional_value) {
		if(!category || !action) {
			throw new Error('[ERROR] Analytics.trackEvent: trackEvent must contain "category" and "action" parameters.');
		} else {
			if((optional_value && isNaN(optional_value)) || optional_value === true) {
				throw new Error('[ERROR] Analytics.trackEvent: optional_value must be a number.');
			} else {
				this.getTracker()._trackEvent(category, action, optional_label, optional_value);
			}
		}
	},
	
	/**
	 * Executa um "_trackEvent" e após uma função JS (_timeout 250 milisegundos)
	 * @param {function} func Função javascript válida
	 * @param {string} category String da categoria
	 * @param {string} action String da ação
	 * @param {string} optional_label String do label
	 * @param {number} optional_value
	 */
	trackEventAndJs: function(func, category, action, optional_label, optional_value) {
		this.trackEvent(category, action, optional_label, optional_value);
		this.callJs(func);
	},

	/**
	 * Executa uma função JS (_timeout 250 milisegundos)
	 * @param {function} func Função javascript válida
	 */
	callJs: function(func) {
		setTimeout(function() {
			try {
				func();
			} catch(e) {
				throw new Error('[ERROR] Analytics.callJs: undefined function called.');
			}
		}, this._timeout);
	}
};  