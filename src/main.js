/**
 * @class Utility class that provides detailed browser information.
 */
var BrowserUtil = new function() {
	var _supported = false;
	var _version = -1;

	var _agent = navigator.userAgent;
	var _ie = false;
	var _safari = false;
	var _opera = false;
	var _mozilla = false;

	if (/MSIE/.test(_agent)) {
		_ie = true;
		_version = parseFloat(_agent.substring(_agent.indexOf('MSIE') + 4));
		_supported = _version >= 5.5;
	} else if (/AppleWebKit/.test(_agent)) {
		_safari = true;
		_version = parseFloat(_agent.substring(_agent.indexOf('Safari') + 7));
		_supported = _version >= 312;
	} else if (/Opera/.test(_agent)) {
		_opera = true;
		_version = parseFloat(navigator.appVersion);
		_supported = _version >= 9.02;
	} else if (/Firefox/.test(_agent)) {
		_mozilla = true;
		_version = parseFloat(_agent.substring(_agent.indexOf('Firefox') + 8));
		_supported = _version >= 1;
	} else if (/Netscape/.test(_agent)) {
		_mozilla = true;
		_version = parseFloat(_agent.substring(_agent.indexOf('Netscape') + 9));
		_supported = _version >= 8;
	} else if (/Mozilla/.test(_agent) && /rv:/.test(_agent)) {
		_mozilla = true;
		_version = parseFloat(_agent.substring(_agent.indexOf('rv:') + 3));
		_supported = _version >= 1.8;
	}

	/**
	 * Detects if the browser is supported.
	 * @return Boolean
	 */
	this.isSupported = function() {
		return _supported;
	}

	/**
	 * Detects the version of the browser.
	 * @return {Number}
	 */
	this.getVersion = function() {
		return _version;
	}

	/**
	 * Detects if the browser is Internet Explorer.
	 * @return Boolean
	 */
	this.isIE = function() {
	   return _ie;
	}
	
	/**
	 * Detects if the browser is Internet Explorer 5.
	 * @return Boolean
	 */
	this.isIE5 = function() {
		return this.isIE() && this.getVersion() >= 5 && this.getVersion() < 5.5;
		
	}
	
	/**
	 * Detects if the browser is Internet Explorer 5.5.
	 * @return Boolean
	 */
	this.isIE55 = function() {
		return this.isIE() && this.getVersion() >= 5.5 && this.getVersion() < 6;
		
	}
	
	/**
	 * Detects if the browser is Internet Explorer 6.
	 * @return Boolean
	 */
	this.isIE6 = function() {
		return this.isIE() && this.getVersion() >= 6 && this.getVersion() < 7;
		
	}
	
	/**
	 * Detects if the browser is Internet Explorer 7.
	 * @return Boolean
	 */
	this.isIE7 = function() {
		return this.isIE() && this.getVersion() >= 7;
		
	}

	/**
	 * Detects if the browser is Safari.
	 * @return Boolean
	 */
	this.isSafari = function() {
		return _safari;
	}

	/**
	 * Detects if the browser is Opera.
	 * @return Boolean
	 */
	this.isOpera = function() {
		return _opera;
	}

	/**
	 * Detects if the browser is Camino.
	 * @return Boolean
	 */
	this.isCamino = function() {
		return _camino;
	}

	/**
	 * Detects if the browser is Mozilla.
	 * @return Boolean
	 */
	this.isMozilla = function() {
		return _mozilla;
	}
};

/**
 * @class Class gets an HTML Element by id.
 * @static
 * @return Object
 */
var $ = function($id) {
	return document.getElementById($id);
	
};

/**
 * Return display style of an element.
 * @static
 * @return String
 */
$.getDisplay = function($id){
	return $($id).style.display;
};

/**
 * Hides or change the display mode of an element.
 * @parameters $id:String - Element id.
 * @parameters $display:String - Optional. Default 'none'. Display state.
 * @return None
 */
$.hide = function($id, $display){
	$($id).style.display = $display ? $display : 'none';
};

/**
 * Shows or change the display mode of an element.
 * @parameters $id:String - Element id.
 * @parameters $display:String - Optional. Default 'none'. Display state.
 * @return None
 */
$.show = function($id, $display){
	$($id).style.display = $display ? $display : 'block';
};

/**
 * Alternate the display mode of an element (show or hide).
 * @parameters $id:String - Element id.
 * @return None
 */
$.alternate = function($id){
	$($id).style.display = ($.getDisplay($id) == 'block' || !$.getDisplay($id)) ? $.hide($id) : $.show($id);
};

/**
 * Create a HTML element using DOM.
 * @parameters $elm:Object - Element will receive the created element. Default: document.body.
 * @parameters $node:Array - The array notation of the new element.
 * @return Object
 */
$.add = function($elm, $node){
	return $._graft(($elm || document.body), $node);
};

/**
 * Private function creates the HTML element.
 * @return Object
 */
$._graft = function(parent, t, doc){
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
				alert('ERROR $._graft: Can\'t $._graft an undefined value in a list!');
			} else if(  t[i].constructor == String || t[i].constructor == Array ) {
				$._graft( e, t[i], doc );
			} else if(  t[i].constructor == Number ) {
				$._graft( e, t[i].toString(), doc );
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
				alert('ERROR $._graft: Object ' + t[i] + ' is inscrutable as an $._graft arglet.');
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
$.del = function($id){
	try{
		$($id).parentNode.removeChild($($id));
	} catch(error){
		alert("ERROR $.del: " + error.message);
	}
};

/**
 * Return the posision x an y of a HTML element.
 * @parameters $id:String or Object - Element id or element.
 * @return Array [x,y]
 */
$.getPosition = function($id){
	var _curleft = _curtop = 0;
	var _obj = (typeof $id == 'object') ? $id : $($id);
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
 * Get all tags inside some especific element or use document.
 * @parameters $tag:String - Tag. ----------------------------------------------------------------------------------
 * @return Array elements
 */
$.tag = function ($tag, $id) {
	var i;
	var t = typeof($id);
	if(t === 'object' && $id.nodeType == 1){
		i = $id;
	} else if(t == 'string'){
		i = $($id);
	} else {
		i = document;
	}
	return i.getElementsByTagName((!$tag ? '*' : $tag));
};



Pop = new function() {
	this.defaultMessage = 'O bloqueador de pop-up está fazendo com que o link não seja aberto, Desabilite seu bloqueador e clique novamente.';
	this.popup = null;
	_this = this;
	
	/* private vars */
	var enabled = false; 
	
	this.isAvaliable = function(o){
		this.onComplete = o.onComplete || null;
		this.onError = o.onError || null;
		
		if(_this.enabled){
			if(this.onComplete) this.onComplete();
			return;
		}
		
		var _checkPopup = window.open('about:blank','testPopup','width='+(o.w ? o.w : 1)+',height='+(o.h ? o.h : 1)+',top='+(o.t ? o.t : -1000)+',left='+(o.l ? o.l : -1000)+',screenX=0,screenY=0,scrollbars=no');
		try {
			_checkPopup.close();
			_this.enabled = true;
			if(this.onComplete) this.onComplete();
		}catch(e){
			if(this.onError) this.onError();
			else alert(_this.defaultMessage);
		}
		this.onError = this.onComplete = null;
	};

	this.open = function($url, $name, $width, $height, $center, $scroll, $other){
		var l = 18;
		var t = 18;
		if ($center != false) {
			l = (screen.availWidth - $width)/2;
			t = (screen.availHeight - $height)/2;
		}
		if(_this.enabled) _this.popup = window.open($url, $name, 'width=' + $width + ', height=' + $height + ', left=' + l + ', top=' + t + ', scrollbars=' + (($scroll) ? 'yes' : 'no') + (($other) ? ', ' + $other : ''));
		else _this.isAvaliable({
								w: $width,
								h: $height,
								t: t,
								l: l,
								onComplete:function(){
									_this.popup = window.open($url, $name, 'width=' + $width + ', height=' + $height + ', left=' + l + ', top=' + t + ', scrollbars=' + (($scroll) ? 'yes' : 'no') + (($other) ? ', ' + $other : ''));
									return false;
								}
							});
		return false;
	};
}

/**/

/**
 * Get all elements with some tag.
 * @parameters $tag:String - Tag.
 * @return Array [x,y]
 */
trace = function(m){
	if(window['console']){
		console.info(m);
	} else {
		alert(m);
	}
}