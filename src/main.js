var lim = {};

/**
 * @class Utility class that provides detailed browser information.
 */
lim.BrowserUtil = {
	_supported: false,
	_version: -1, 

	_agent: navigator.userAgent, 
	_ie: false, 
	_safari: false, 
	_opera: false, 
	_mozilla: false, 
	
	init: function(){
		if (/MSIE/.test(lim.BrowserUtil._agent)) {
			lim.BrowserUtil._ie = true;
			lim.BrowserUtil._version = parseFloat(lim.BrowserUtil._agent.substring(lim.BrowserUtil._agent.indexOf('MSIE') + 4));
			lim.BrowserUtil._supported = lim.BrowserUtil._version >= 5.5;
		} else if (/AppleWebKit/.test(lim.BrowserUtil._agent)) {
			lim.BrowserUtil._safari = true;
			lim.BrowserUtil._version = parseFloat(lim.BrowserUtil._agent.substring(lim.BrowserUtil._agent.indexOf('Safari') + 7));
			lim.BrowserUtil._supported = lim.BrowserUtil._version >= 312;
		} else if (/Opera/.test(lim.BrowserUtil._agent)) {
			lim.BrowserUtil._opera = true;
			lim.BrowserUtil._version = parseFloat(navigator.appVersion);
			lim.BrowserUtil._supported = lim.BrowserUtil._version >= 9.02;
		} else if (/Firefox/.test(lim.BrowserUtil._agent)) {
			lim.BrowserUtil._mozilla = true;
			lim.BrowserUtil._version = parseFloat(lim.BrowserUtil._agent.substring(lim.BrowserUtil._agent.indexOf('Firefox') + 8));
			lim.BrowserUtil._supported = lim.BrowserUtil._version >= 1;
		} else if (/Netscape/.test(lim.BrowserUtil._agent)) {
			lim.BrowserUtil._mozilla = true;
			lim.BrowserUtil._version = parseFloat(lim.BrowserUtil._agent.substring(lim.BrowserUtil._agent.indexOf('Netscape') + 9));
			lim.BrowserUtil._supported = lim.BrowserUtil._version >= 8;
		} else if (/Mozilla/.test(lim.BrowserUtil._agent) && /rv:/.test(lim.BrowserUtil._agent)) {
			lim.BrowserUtil._mozilla = true;
			lim.BrowserUtil._version = parseFloat(lim.BrowserUtil._agent.substring(lim.BrowserUtil._agent.indexOf('rv:') + 3));
			lim.BrowserUtil._supported = lim.BrowserUtil._version >= 1.8;
		}
	}, 
	
	/**
	 * Detects if the browser is supported.
	 * @return Boolean
	 */
	isSupported: function() {
		return lim.BrowserUtil._supported;
	},

	/**
	 * Detects the version of the browser.
	 * @return {Number}
	 */
	getVersion: function() {
		return lim.BrowserUtil._version;
	},

	/**
	 * Detects if the browser is Internet Explorer.
	 * @return Boolean
	 */
	isIE: function() {
	   return lim.BrowserUtil._ie;
	},
	
	/**
	 * Detects if the browser is Internet Explorer 5.
	 * @return Boolean
	 */
	isIE5: function() {
		return lim.BrowserUtil.isIE() && lim.BrowserUtil.getVersion() >= 5 && lim.BrowserUtil.getVersion() < 5.5;
		
	},
	
	/**
	 * Detects if the browser is Internet Explorer 5.5.
	 * @return Boolean
	 */
	isIE55: function() {
		return lim.BrowserUtil.isIE() && lim.BrowserUtil.getVersion() >= 5.5 && lim.BrowserUtil.getVersion() < 6;
	},
	
	/**
	 * Detects if the browser is Internet Explorer 6.
	 * @return Boolean
	 */
	isIE6: function() {
		return lim.BrowserUtil.isIE() && lim.BrowserUtil.getVersion() >= 6 && lim.BrowserUtil.getVersion() < 7;
	},
	
	/**
	 * Detects if the browser is Internet Explorer 7.
	 * @return Boolean
	 */
	isIE7: function() {
		return lim.BrowserUtil.isIE() && lim.BrowserUtil.getVersion() >= 7;
	},
	
	/**
	 * Detects if the browser is Internet Explorer 8.
	 * @return Boolean
	 */
	isIE8: function() {
		return lim.BrowserUtil.isIE() && lim.BrowserUtil.getVersion() >= 8;
	},

	/**
	 * Detects if the browser is Safari.
	 * @return Boolean
	 */
	isSafari: function() {
		return lim.BrowserUtil._safari;
	},

	/**
	 * Detects if the browser is Opera.
	 * @return Boolean
	 */
	isOpera: function() {
		return lim.BrowserUtil._opera;
	},

	/**
	 * Detects if the browser is Mozilla.
	 * @return Boolean
	 */
	isMozilla: function() {
		return lim.BrowserUtil._mozilla;
	}
};

/**
 * @class Class gets an HTML Element by id.
 * @parameters $id:String - Element id.
 * @return Object
 */
lim.$ = function($id) {
	return document.getElementById($id);
	
};

/**
 * Return if an abject is a HTML element.
 * @parameters $elm:Object or String - Element to be tested. 
 * @return Boolean
 */
lim.$.isHtmlObject = function($elm){
	return (typeof($elm) === 'object' && $elm.nodeType == 1) ? true : false;
};

/**
 * Return display style of an element.
 * @parameters $id:String - Element id.
 * @return String
 */
lim.$.getDisplay = function($id){
	return lim.$($id).style.display;
};

/**
 * Hides or change the display mode of an element.
 * @parameters $id:String - Element id.
 * @parameters $display:String - Optional. Default 'none'. Display state.
 * @return None
 */
lim.$.hide = function($id, $display){
	lim.$($id).style.display = $display ? $display : 'none';
};

/**
 * Shows or change the display mode of an element.
 * @parameters $id:String - Element id.
 * @parameters $display:String - Optional. Default 'none'. Display state.
 * @return None
 */
lim.$.show = function($id, $display){
	lim.$($id).style.display = $display ? $display : 'block';
};

/**
 * Alternate the display mode of an element (show or hide).
 * @parameters $id:String - Element id.
 * @return None
 */
lim.$.alternate = function($id){
	lim.$($id).style.display = (lim.$.getDisplay($id) == 'block' || !lim.$.getDisplay($id)) ? lim.$.hide($id) : lim.$.show($id);
};

/**
 * Create a HTML element using DOM.
 * @parameters $elm:Object - Element will receive the created element. Default: document.body.
 * @parameters $node:Array - The array notation of the new element.
 * @return Object
 */
lim.$.add = function($elm, $node){
	return lim.$._graft(($elm || document.body), $node);
};

/**
 * Private function creates the HTML element.
 * @return Object
 */
lim.$._graft = function(parent, t, doc){
	doc = (doc || parent.ownerDocument || document);
	var e;

	if(t == 'undefined') {
		alert('ERROR $._graft: Can\'t $._graft an undefined value');
	} else if(t.constructor == String) {
		e = doc.createTextNode( t );
	} else if(t.length == 0) {
		e = doc.createElement('span');
		e.setAttribute('class', 'fromEmptyLOL');
	} else {
		for(var i = 0; i < t.length; i++) {
			if( i == 0 && t[i].constructor == String ) {
				var snared;
				snared = t[i].match( /^([a-z][a-z0-9]*)\.([^\s\.]+)$/i );
				if( snared ) {
					e = doc.createElement(   snared[1] );
					e.setAttribute('class', snared[2] );
					continue;
				}
				snared = t[i].match( /^([a-z][a-z0-9]*)$/i );
				if( snared ) {
					e = doc.createElement( snared[1] );  // but no class
					continue;
				}

				// Otherwise:
				e = doc.createElement('span');
				e.setAttribute('class', 'namelessFromLOL');
			}
			if( t[i] == 'undefined' ) {
				alert('ERROR $._graft: Can\'t lim.$._graft an undefined value in a list!');
			} else if(  t[i].constructor == String || t[i].constructor == Array ) {
				lim.$._graft( e, t[i], doc );
			} else if(  t[i].constructor == Number ) {
				lim.$._graft( e, t[i].toString(), doc );
			} else if(  t[i].constructor == Object ) {
				for(var k in t[i]) {
					// support for attaching closures to DOM objects
					if(k == 'style' && BrowserUtil.isIE()) { 
						e.style.cssText = t[i][k]; // correção para IE aplicar o style corretamente.
					} else if(typeof(t[i][k])=='function'){
						e[k] = t[i][k]; // aplicar função
					} else {
						if (k == 'class') e.className = t[i][k]; // correção para o atributo class
						
						e.setAttribute( k, t[i][k] );
					}
				}
			} else {
				alert('ERROR lim.$._graft: Object ' + t[i] + ' is inscrutable as an lim.$._graft arglet.');
			}
		}
	}

	parent.appendChild( e );
	return e; // return the topmost created node
}

/**
 * Deletes a HTML element.
 * @parameters $id:String - Element id.
 * @return None
 */
lim.$.del = function($id){
	try{
		lim.$($id).parentNode.removeChild(lim.$($id));
	} catch(error){
		alert("ERROR lim.$.del: " + error.message);
	}
};

/**
 * Return the posision x an y of a HTML element.
 * @parameters $id:String or Object - Element id or element.
 * @return Array [x,y]
 */
lim.$.getPosition = function($id){
	var _curleft = _curtop = 0;
	var _obj = (lim.$.isHtmlObject($id)) ? $id : lim.$($id);
	if (_obj.offsetParent) {
		curleft = _obj.offsetLeft;
		curtop = _obj.offsetTop;
		while (_obj = _obj.offsetParent) {
			curleft += _obj.offsetLeft;
			curtop += _obj.offsetTop;
		}
	}
	return [curleft,curtop];
};

/**
 * Get all tags inside some especific element (or inside document).
 * @parameters $tag:String - Tag. 
 * @parameters $id:String or Object - Element id or element. Default: document
 * @return Array elements
 */
lim.$.tag = function ($tag, $id) {
	var i;
	var t = typeof($id);
	if(lim.$.isHtmlObject($id)){
		i = $id;
	} else if(t == 'string'){
		i = lim.$($id);
	} else {
		i = document;
	}
	return i.getElementsByTagName((!$tag ? '*' : $tag));
};



lim.Pop = {
	onError: alert,
	defaultMessage:'O bloqueador de pop-up está fazendo com que o link não seja aberto, Desabilite seu bloqueador e clique novamente.',
	popup:null,
	
	open:function($url, $name, $width, $height, $center, $scroll, $other){
		var l = 18;
		var t = 18;
		if ($center != false) {
			l = (screen.availWidth - $width)/2;
			t = (screen.availHeight - $height)/2;
		}
		lim.Pop.popup = window.open($url, $name, 'width=' + $width + ', height=' + $height + ', left=' + l + ', top=' + t + ', scrollbars=' + (($scroll) ? 'yes' : 'no') + (($other) ? ', ' + $other : ''));
		if(lim.Pop.popup == null) {
			this.onError(lim.Pop.defaultMessage);
			return false;
		}
		return lim.Pop.popup;
	}
};

/**
 * Executes the command console.info on Firebug or alert for other browsers.
 * @parameters $m:String - Message.
 * @return None
 */
lim.trace = function($m){
	this.customFunction = alert;
	if(window['console']){
		console.info($m);
	} else {
		this.customFunction($m);
	}
};
lim.BrowserUtil.init();