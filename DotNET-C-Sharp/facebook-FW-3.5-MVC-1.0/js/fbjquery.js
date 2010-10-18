var alert = function(p_str, p_tit) {
	var dialog = new Dialog().showMessage(p_tit || ' ', p_str);
};
/**
 * Cria um Objeto $ com os mesmos métodos do jQuery só que usando as funções disponíveis no Facebook
 * @author Nicholas Almeida
 * @since 18/10/2010
 * 
 * @usage

	// Importante. O Javascript deve ser inserido DENTRO da tag <script>. Não funcionará se chamado direto no <script src="...">
	<h2 class="h2">jQuery (nalmeida 11/06/2010)</h2>
	<div id="htmlDemo">
		#htmlDemo
	</div>
	<a href="#" onclick="$('#htmlDemo').text('loading...');$.ajax({url: '<%=Common.Util.Root%>ajax.aspx', 'dataType':'json', error: onAjaxError, complete: function(data){$('#htmlDemo').text(data.po)}});">ajax statico JSON</a><br />
	<a href="#" onclick="$('#htmlDemo').text('loading...');$.ajax({url: '<%=Common.Util.Root%>ajax.aspx', 'cache':false, data:{'nome':'nicholas'}, error: onAjaxError, complete: function(data){$('#htmlDemo').text(data)}});">ajax statico passando parâmentros</a><br />
	<a href="#" onclick="$('#htmlDemo').text('loading...');$.ajax({url: '<%=Common.Util.Root%>ajax.aspx', error: onAjaxError, complete: function(data){$('#htmlDemo').text(data)}});">ajax statico</a><br />
	<a href="#" onclick="$('#htmlDemo').load({url: '<%=Common.Util.Root%>ajax_html.aspx'}).text('carregando HTML Brasil!...');">ajax LOAD</a><br />
	<a href="#" onclick="$('#htmlDemo').ajax({url: '<%=Common.Util.Root%>ajax.aspx', error: onAjaxError, complete: function(data){$('#htmlDemo').text(data)}}).text('carregando...');">ajax elemento</a><br />
	<a href="#" onclick="$('#htmlDemo').css({'background':'blue', 'color':'white'});">CSS</a><br />
	<a href="#" onclick="$('#htmlDemo').html('<i><b>conteúdo do setInnerXHTML</b></i>');">html</a><br />
	<fb:js-string var="str_example">Pre-rendered FBML content.</fb:js-string>
	<a href="#" onclick="$('#htmlDemo').fbml(str_example);">fbml</a><br />
	<a href="#" onclick="$('#htmlDemo').text('conteúdo do setInnerFBML');">text</a><br />
	<a href="#" onclick="$('#htmlDemo').hide();">hide</a><br />
	<a href="#" onclick="$('#htmlDemo').show();">show</a><br />
	<a href="#" onclick="$('#htmlDemo').html('<i><b>conteúdo do setInnerFBML</b></i>').css('background', 'yellow');">chain</a><br />
	<a href="#" onclick="$('.aprovada').hide();">classes</a>
 */
var FbjQuery = {

	// CONSTANTES
	VERSION: '1.0',
	ROOT: document.getElementById('root') || document, // Todo o conteúdo deve estar dentro de um div com id "root". Ele é que contém todos os métodos que o Facebook disponibiliza
	/*
		Métodos disponíveis no Bacebook
		addClassName(a), addEventListener(f, b), appendChild(a), callSWF(e), cloneNode(c), constructorfbjs_dom(c, a), focus(), getAbsoluteLeft(), getAbsoluteTop(), getAccessKey(), getAction(), getChecked(), getChildNodes(), getClassName(), getClientHeight(), getClientWidth(), getColSpan(), getCols(), getDir(a), getDisabled(), getElementsByTagName(b), getFirstChild(), getForm(), getHref(), getId(), getLastChild(), getMaxLength(), getMethod(), getName(), getNextSibling(), getNodeType(), getOffsetHeight(), getOffsetWidth(), getOptions(), getParentNode(), getPreviousSibling(), getReadOnly(), getRowSpan(), getRows(), getScrollHeight(), getScrollLeft(), getScrollTop(), getScrollWidth(a), getSelected(), getSelectedIndex(), getSelection(), getSrc(), getStyle(a), getTabIndex(), getTagName(), getTarget(), getTitle(), getType(), getValue(), getdir(a), hasClassName(a), insertBefore(b, a), listEventListeners(e), purgeEventListeners(e), removeChild(a), removeClassName(a), removeEventListener(f, b), replaceChild(a, b), select(), serialize(), setAccessKey(a), setAction(a), setChecked(a), setClassName(a), setColSpan(a), setCols(a), setDir(a), setDisabled(a), setHref(a), setId(b), setInnerFBML(a), setInnerXHTML(b), setMaxLength(a), setMethod(a), setName(a), setReadOnly(a), setRowSpan(a), setRows(a), setScrollLeft(a), setScrollTop(a), setSelected(a), setSelectedIndex(a), setSelection(c, a), setSrc(a), setStyle(a, b), setTabIndex(a), setTarget(a), setTextValue(b), setTitle(a), setType(a), setValue(a), submit(), toggleClassName(a)
	*/
	
	// Static Methods
	// AJAX
	genericAjax: function(p_url, p_query, p_responsetype, p_complete, p_error, p_requirelogin, p_uselocalproxy) {

		var _url = p_url;
		var _query = p_query || null;
		var _responseType = p_responsetype || Ajax.RAW; // Ajax.RAW, Ajax.JSON, Ajax.FBML;
		var _complete = p_complete || function(){};
		var _error = p_error || function(){};
		var _requireLogin = p_requirelogin || false;
		var _useLocalProxy = p_uselocalproxy || false;
		var _ajax = new Ajax();
			_ajax.responseType = _responseType;
			_ajax.requireLogin = _requireLogin;
			_ajax.useLocalProxy = _useLocalProxy;
			_ajax.ondone = _complete
			_ajax.onerror = _error
		_ajax.post(_url, _query);

	},
	ajax: function(p_objAjax) {
		var _objAjax = p_objAjax;
		var _cache;
		var _url, _query, _responseType, _complete, _error, _requirelogin, _uselocalproxy; // FB Objects
		
		if(FbjQuery.isObject(_objAjax)) {
			var respType;
			switch(_objAjax.dataType) {
				// case 'jsonp' :
				// case 'script' :
				// 	throw new Error('ERROR: dataType "' + _objAjax.dataType + '" not avaliable for FbjQuery.')
				// 	break;
				case 'html' :
					respType = Ajax.FBML;
					break;
				case 'json' :
					respType = Ajax.JSON;
					break;
				default : // xml, text
					respType = Ajax.RAW;
					break;
			}
			_url = _objAjax.url;
			_query = _objAjax.data;
			_responseType = respType; // Ajax.RAW, Ajax.JSON, Ajax.FBML;
			_complete = _objAjax.complete || function(){};
			_error = _objAjax.error || function(){};
			_requirelogin = _objAjax.requireLogin || false;
			_uselocalproxy = _objAjax.useLocalProxy || false;
			_cache = (_objAjax.cache != null && (_objAjax.cache === false || _objAjax.cache == 'false')) ? false : true;
		}
		else if(typeof(_objAjax) == 'string') _url = _objAjax;
		
		if(_cache === false) {
			_url += (_url.match(/\?/) ? '&' : '?') + 'rnd=' + Math.random(10000);
		}
		FbjQuery.genericAjax(_url, _query, _responseType, _complete, _error, _requirelogin, _uselocalproxy);
	},
	
	post: function(p_objAjax) {
		FbjQuery.ajax(p_objAjax);
	},
	
	get: function(p_objAjax) {
		FbjQuery.ajax(p_objAjax);
	},
	
	// UTIL
	/**
	@see http://forum.jquery.com/topic/why-use-jquery-isobject-in-jquery-extend
	*/
	isObject: function(p_obj){
		// return Object.prototype.toString.call(p_obj) === '[object Object]';
		return (typeof(p_obj) == 'object' && p_obj != null && p_obj.length == null);
	},
	isArray: function(p_obj){
		// return Object.prototype.toString.call(p_obj) === '[object Array]';
		return (typeof(p_obj) == 'object' && p_obj != null && p_obj.length != null);
	},
	isFunction: function(p_fn) {
		//the following condition should be in there too, but FBJS prevents access to Array
		//fn.constructor != [].constructor
		return !!p_fn && typeof p_fn != 'string' && !p_fn.nodeName &&  /^[\s[]?function/.test( p_fn + '' );
	},
	extend: function() {
		// copy reference to target object
		var target = arguments[0] || {},
			i = 1,
			length = arguments.length,
			deep = false,
			options;

		// Handle a deep copy situation
		if(typeof(target) == "boolean") {
			deep = target;
			target = arguments[1] || {};
			// skip the boolean and the target
			i = 2;
		}

		// Handle case when target is a string or something (possible in deep copy)
		if(typeof(target) != "object" && typeof(target) != "function") {
			target = {};
		}

		// extend jQuery itself if only one argument is passed
		if(length == i) {
			target = this;
			--i;
		}

		for(; i < length; i++) {
			// Only deal with non-null/undefined values
			if((options = arguments[i])) {
				// Extend the base object
				for(var name in options) {
					var src = target[name],
						copy = options[name];

					// Prevent never-ending loop
					if(target === copy) { continue; }

					// Recurse if we're merging object values
					if (deep && copy && typeof(copy) == "object" && !copy.getNodeType) {
						target[name] = FBjqRY.extend(deep, // Never move original objects, clone them
										src || (copy.length ? [] : {}), copy);
					} else if (copy !== undefined) { // Don't bring in undefined values
						target[name] = copy;
					}
				}
			}
		}
		// Return the modified object
		return target;
	},
	util: {
		convertObjCSSProp: function(p_obj){
			var tmpObj = {};
			for(var i in p_obj) {
				tmpObj[FbjQuery.util.transformCSSPropToJSProp(i)] = p_obj[i];
			}
			return tmpObj;
		},
		transformCSSPropToJSProp: function(p_str){ 
			var arrText = p_str.split('-');
			var arrConverted = [];
				arrConverted.push(arrText[0]);
			
			for(var i=1, len = arrText.length; i < len; i++) {
				var firtsLetter = arrText[i].slice(0,1);
					firtsLetter = firtsLetter.toUpperCase();
				var word = arrText[i].slice(1,arrText[i].length)
				arrConverted.push(firtsLetter + word);
			}
			return arrConverted.join('');
		}
	},

	// Dynamic Class
	Class: function(p_selector){
		// private methods FIRST
		var _applyFBJS = function(p_func, p_args, p_context) {
			var _context = p_context || _root;
			for(var i = 0; i < _$len; i++) {
				_context[p_func].apply(_$[i], p_args);
			}
		}

		// Selectors
		var _getById = function(p_id) {
			return [document.getElementById(p_id)];
		};

		var _getByTag = function(p_tag) {
			return _root.getElementsByTagName(p_tag || '*');
		};

		var _getByClass = function(p_class, p_context) {
			var _arrTags = _getByTag(p_context);
			var _arrHasClass = [];

			for(var i = 0, len = _arrTags.length; i < len; i++) {
				var _elm = _arrTags[i];

				if(_arrTags[i].hasClassName(p_class)) {
					_arrHasClass[_arrHasClass.length] = _arrTags[i];
				}
			}

			return _arrHasClass;
		};	

		// Getting the OBJ!
		var _$;
		var _$len;
		var _root = FbjQuery.ROOT;
		var _this = this;
		
		if(FbjQuery.isObject(p_selector)) { // Já é um objeto DOM
			_$ = [p_selector];
		} else {
			var selector = p_selector.slice(0,1);
			_$ = null;
			_$len = 0;
			
			// constructor
			if(selector == '#') {
				_$ = _getById(p_selector.replace('#',''));
			} else if(selector == '.') {
				_$ = _getByClass(p_selector.replace('.',''));
			} else {
				_$ = _getByTag(p_selector);
			}
			
		}
		this.length = _$len = _$.length;

		// public methods returning "this"
		this.hide = function() {
			_applyFBJS('setStyle', ['display', 'none']);
			return this;
		};

		this.show = function() {
			_applyFBJS('setStyle', ['display', 'block']);
			return this;
		};

		this.text = function(p_text) {
			_applyFBJS('setTextValue', [p_text]);
			return this;
		};

		this.fbml = function(p_html) {
			_applyFBJS('setInnerFBML', [p_html], document);
			return this;
		};

		this.html = function(p_html) {
			_applyFBJS('setInnerXHTML', [p_html]);
			return this;
		};

		this.css = function(p_prop, p_value) {
			var _objStyle = {};
			
			if(arguments.length == 0 ) {
				return this;
			} else if(arguments.length == 1 ) { // Getter ou passado por Objeto
			 
				if(FbjQuery.isObject(p_prop)) { // Setter por objeto
					_objStyle = FbjQuery.util.convertObjCSSProp(p_prop);
				} else if(p_prop == 'string'){ // Getter
					return _applyFBJS('getStyle', [FbjQuery.util.transformCSSPropToJSProp(p_prop)]);
				}
				
			} else if(arguments.length == 2 ){ // Setter por "prop":"valor"
				_objStyle[FbjQuery.util.transformCSSPropToJSProp(p_prop)] = p_value;
			}
			
			_applyFBJS('setStyle', [_objStyle]);
			
			return this;
		};
		
		this.espirro = function(p_objAjax) {
			var tmpObj = {
				dataType:'html',
				error: function() {
					alert('ERRO!')
				},
				complete: function(response){
					_this.fbml(response);
				}
			};
			var newObj = FbjQuery.extend(p_objAjax, tmpObj);
			FbjQuery.ajax(newObj);
			return this;
		};
		

		// Métodos Ajax que funcionam exatamente igual
		var _ajaxArr = ['ajax','post','get'];
		for(var i = 0; _ajaxArr[i]; i++) {
			(function() {
				var _ajaxName = _ajaxArr[i];
				_this[_ajaxName] = function(p_objAjax) {
					FbjQuery.ajax(p_objAjax);
					return _this;
				};
			})();
		};
		_ajaxArr = null;
	
		this.bind = function(p_event, p_handler, p_usecapture) {
			_applyFBJS('addEventListener', [p_event, p_handler, p_usecapture]);
			return this;
		};
		
		// Fazendo bind de todos os eventos possíveis!
		// TODO: testar quais realmente funcionam!
		var _eventsArr = ['blur','focus','focusin','focusout','load','resize','scroll','unload','click','dblclick','mousedown','mouseup','mousemove','mouseover','mouseout','mouseenter','mouseleave','change','select','submit','keydown','keypress','keyup','error'];
		for(var i = 0; _eventsArr[i]; i++) {
			(function() {
				var _eventName = _eventsArr[i];
				_this[_eventName] = function(p_handler, p_usecapture) {
					return _this.bind(_eventName, p_handler, p_usecapture)
				};
			})();
		};
		_eventsArr = null;
	}

};
var $ = function(p_selector) {
	return new FbjQuery.Class(p_selector);
};
FbjQuery.extend($, FbjQuery, {Class:null});