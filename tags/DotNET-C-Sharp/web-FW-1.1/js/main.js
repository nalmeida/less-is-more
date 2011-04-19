/**
 * @author	Nicholas Almeida
 * @author	Marcelo Carneiro
 * @version	4.2.0
 */
function isDef(S) {
    return (eval('typeof(' + S + ')') != 'undefined' && eval('typeof(' + S + ')') != 'unknown');
};
function gElm(id) {
    return (is.ie4) ? document.all[id] : document.getElementById(id);
};
if(!isDef('$')) var $ = gElm;
function checkBrowser() {
    var T = this;
    var b = navigator.appName;
    var v = navigator.appVersion;
    var u = navigator.userAgent;
    if (u == null)return;
    if (b == 'Netscape')T.b = 'ns';
    else if (b == 'Microsoft Internet Explorer')T.b = 'ie';
    else T.b = b;
	if (u.indexOf('Safari')>=0) T.b = 'sa';
    T.v = parseInt(v);
    T.sa = (T.b == 'sa');
    T.ns = (T.b == 'ns' && T.v >= 4);
    T.ns4 = (T.b == 'ns' && T.v == 4);
    T.ns5 = (T.b == 'ns' && T.v == 5);
    T.ns6 = (T.b == 'ns' && T.v == 5);
    T.ie = (T.b == 'ie' && T.v >= 4);
    T.ie4 = (u.indexOf('MSIE 4') > 0);
    T.ie5 = (u.indexOf('MSIE 5.0') > 0);
    T.ie55 = (u.indexOf('MSIE 5.5') > 0);
    T.ie6 = (u.indexOf('MSIE 6.0') > 0);
    T.ie7 = (u.indexOf('MSIE 7') > 0);
    if (T.ie5)T.v = 5;
    if (T.ie55)T.v = 5.5;
    if (T.ie6)T.v = 6;
    if (T.ie7)T.v = 7;
    T.min = (T.ns || (T.ie && T.v >= 6));
    T.dom = (T.v >= 5);
    T.win = (u.indexOf('Win') > 0);
    T.mac = (u.indexOf('Mac') > 0);
};
var is =  new checkBrowser();
function openPop(u, n, w, h, c, s, o) {
    var l = 18;
    var t = 18;
    if (c) {
        l = (screen.availWidth - w)/2;
		t = (screen.availHeight -h)/2;
    }
	var popup = window.open(u, n, 'width=' + w + ', height=' + h + ', left=' + l + ', top=' + t + ', scrollbars=' + ((s) ? 'yes' : 'no') + ((o) ? ', ' + o : ''));
	setTimeout(function(){
		try {
			popup.focus();
			return false;
		} catch(error) {
			alert('Você deve desabilitar o recurso de anti pop-up para acessar este link.');
			return false;
		}
	}, 300);
    return false;
};
var iimg_off;
var iimg_over;
function pImg(src) {
    var obj = src.substring(src.lastIndexOf('/') + 1, src.lastIndexOf('.'));
    eval('i' + obj + '= new Image()');
    eval('i' + obj + '.src="' + src + '"');
};
function cImg(id, obj) {
    var tId = 'document.images[\'' + id + '\']';
    if (isDef(tId) && isDef('i' + obj))eval(tId).src = eval('i' + obj).src;
};
function sDiv(id,s) {
	var e = typeof(id) == 'object' ? id :  $(id);
    e.style.display = s ? s : 'block';
};
function hDiv(id,s) {
	var e = typeof(id) == 'object' ? id :  $(id);
    e.style.display = s ? s : 'none';
};
function shDiv(id) {
	var e = typeof(id) == 'object' ? id :  $(id);
    if (e.style.display == 'block' || !e.style.display) e.style.display = 'none';
    else e.style.display = 'block';
};
function addEvent(w, e, f, useCapture) {
	if (w.addEventListener) {
		w.addEventListener(e,f,useCapture);
		return true;
	} else if (w.attachEvent) {
		return w.attachEvent('on'+e,f);;
	} else {
		w['on'+e] = f;
		return ((w['on'+e] == null) ? false : true);
	}
	return false;
};
function delEvent(w,e,f,useCapture){
	if(w.detachEvent){
		w.detachEvent('on'+e, f);
		return true;
	}else if(w.removeEventListener){
		return w.removeEventListener(e, f, (useCapture || false));
	}else{
		w['on'+e] = null;
		return ((w['on'+e] == null) ? true : false);
	}
	return false;
}
function ie_getElementsByTagName(str) {
	if (str=="*") return document.all;
	else return document.all.tags(str);
};
if(is.ie) document.getElementsByTagName = ie_getElementsByTagName;
function gTag(t,n) {
	if(!n) n = document;
	if(!t) t = '*';
	return n.getElementsByTagName(t);
};
function gClass(c, t, n) {
	var elms = gTag(t, n);
	var cls = [];
	var arrElm = [];
	for (var i = 0; i<elms.length; i++){
		if (elms[i].className.length > 0){
			if (elms[i].className.split(c).length > 1) {
				cls = elms[i].className.split(' ');
				for (var a = 0; a < cls.length; a++){
					if (cls[a] == c){
						arrElm[arrElm.length] = elms[i];
					};
				};
				cls = null;
			};
		};
	};
	return arrElm;
};
function graft(parent, t, doc) {
    doc = (doc || parent.ownerDocument || document);
    var e;

    if(t == 'undefined') {
        throw complaining('Can\'t graft an undefined value');
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
                throw complaining('Can\'t graft an undefined value in a list!');
            } else if(  t[i].constructor == String || t[i].constructor == Array ) {
                graft( e, t[i], doc );
            } else if(  t[i].constructor == Number ) {
                graft( e, t[i].toString(), doc );
            } else if(  t[i].constructor == Object ) {
                for(var k in t[i]) {
                    // support for attaching closures to DOM objects
					if(k == 'style' && is.ie) { 
						e.style.cssText = t[i][k]; // correção para IE aplicar o style corretamente.
                    } else if(typeof(t[i][k])=='function'){
                        e[k] = t[i][k]; // aplicar função
                    } else {
						if (k == 'class') e.className = t[i][k]; // correção para o atributo class
						
						e.setAttribute( k, t[i][k] );
                    }
                }
            } else {
                throw complaining('Object ' + t[i] + ' is inscrutable as an graft arglet.');
            }
        }
    }

    parent.appendChild( e );
    return e; // return the topmost created node
};

function complaining (s) { alert("ERROR GRAFT: " + s); return new Error(s); }

function createElm(e, n){
	e = (e || document.body);
	return graft(e, n);
};

String.prototype.trim = function(){
	return this.replace(/^ +(.*[^ ]) +$/, "$1");
}

function delElm(id){
	var e = typeof(id) == 'object' ? id : $(id);
	try{
		e.parentNode.removeChild(e);
	} catch(e){
		alert("ERROR delElm: " + e.message);
	}
};

function getPos(obj) {
	var curleft = curtop = 0;
	if (obj.offsetParent) {
		curleft = obj.offsetLeft;
		curtop = obj.offsetTop;
		while (obj = obj.offsetParent) {
			curleft += obj.offsetLeft;
			curtop += obj.offsetTop;
		}
	}
	return {x:curleft,y:curtop};
};
function docH(){
	return is.ie ? document.body.scrollHeight : document.height;
};
function docW(){
	return is.ie ? document.body.scrollWidth : document.width;
};

function winW(){return window.innerWidth ? window.innerWidth : document.documentElement.offsetWidth;}
function winH(){return window.innerHeight ? window.innerHeight : document.documentElement.offsetHeight;}

if(is.ie5 || is.ns4) alert('A versão de navegador que você está usando não é compatível com este site.\nAtualize-o para que todos os elementos funcionem corretamente.');

/**************************************
 * UTIL FUNCTIONS
 **************************************/
//{
/**
 *
 *	Init. Executes function(s) on the page load.
 *	
 *	@author Marcelo Miranda Carneiro
 *	@since 15/01/2008
 *	@version 0.1.0
 *	@requires addEvent 
 *	@example
		<code>
			Init.add(FUNCTION_NAME); // for executing before </body>
			Init.addOnLoad(FUNCTION_NAME); // for executing at onLoad function
		</code>
 */
var Init = {
	_functionLoadLst: [],
	_functionOnLoadLst: [],
	add: function($fnc){
		if(typeof($fnc) != 'function')
			return false;
		this._functionLoadLst[this._functionLoadLst.length] = $fnc;
		return true;
	},
	addOnLoad: function($fnc){
		if(typeof($fnc) != 'function')
			return false;
		this._functionOnLoadLst[this._functionOnLoadLst.length] = $fnc;
		return true;
	},
	run: function(){
		var _this = this;
		var eventToLoad = function(){
			for(var i=0; i<_this._functionOnLoadLst.length; i++)
				_this._functionOnLoadLst[i]();
		};
		addEvent(window,'load', eventToLoad);
		for(var i=0; i<this._functionLoadLst.length; i++)
			this._functionLoadLst[i]();
	}
};
//}

String.prototype.trim = function(){
	return this.replace(/^\s*(.*[^ ])\s*$/, "$1");
}

String.prototype.wordLimiter = function($count){
	var reg = new RegExp('\\s*(?:\\S+\\s*){1,'+$count+'}','gi');
	return this.match(reg)[0].trim();
}