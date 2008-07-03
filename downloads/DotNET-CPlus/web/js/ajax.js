/**
 * @author	Nicholas Almeida
 * @version	2.1.1
 */

function ajax(o){
	if(!o.url){
		alert('URL not defined');
		return false;
	};
	try{
	    this.xmlhttp = new XMLHttpRequest();
	}catch(ee){
	    try{
	        this.xmlhttp = new ActiveXObject('Msxml2.XMLHTTP');
	    }catch(eee){
	        try{
	            this.xmlhttp = new ActiveXObject('Microsoft.XMLHTTP');
	        }catch(E){
				this.xmlhttp = false;
	        };
	    };
	};

	// vars
	this.url = (o.url) ? o.url : null;
	this.serialize = (o.serialize) ? o.serialize : null;
	this.method = (o.method) ? o.method.toUpperCase() : 'POST';
	this.debug = (o.debug) ? o.debug : false;
	this.results = null;
	this.extra = (o.extra) ? o.extra	: null;
	this.timeout = ((o.timeout) ? o.timeout : 60) * 1000;
	this.encrypt = (o.encrypt == '') ? o.encrypt : true;
	this.parameters =  (o.parameters) ? o.parameters : {};

	this._parameters = '';
	this._timer;
	this._64 = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';	
	this._e = function(_77){
		var i = 0;
		var _70 = new String('');
		var _71, _72, _73;
		var _74, _75, _76, _79;
		while (i < _77.length) {
			_71 = _77.charCodeAt(i++);
			_72 = _77.charCodeAt(i++);
			_73 = _77.charCodeAt(i++);
			_74 = _71 >> 2;
			_75 = ((_71 & 3) << 4) | (_72 >> 4);
			_76 = ((_72 & 15) << 2) | (_73 >> 6);
			_79 = _73 & 63;
			if(isNaN(_72)) _76 = _79 = 64;
			else if(isNaN(_73)) _79 = 64;
			_70 += this._64.charAt(_74)+this._64.charAt(_75);
			_70 += this._64.charAt(_76)+this._64.charAt(_79);
		};
			return _70;
	};


	this._serialize = function($f){
		var f = (typeof($f) == 'object') ? f : gElm($f);
		var ae = f.getElementsByTagName('input');
		var at = f.getElementsByTagName('textarea');
		var as = f.getElementsByTagName('select');
		var e = Array();
		var q = 0;
		while(q<ae.length){ 
			if(ae[q].type != 'button' && ae[q].type != 'reset' && ae[q].type != 'image' && ae[q].type != 'submit' && ae[q].type != 'file'){
				if(ae[q].type == 'radio'){ 
					if(ae[q].checked) e.push(ae[q]);
				} else if(ae[q].type == 'checkbox'){
					if(ae[q].checked == true) e.push(ae[q]);
				} else {
					e[e.length] = (ae[q]);
				};
			};
			q++;
		};
		var m=0;
		while(m<as.length){
			if(as[m].selectedIndex != -1){
				e[e.length] = (as[m]);
			};
			m++;
		};
		var j=0;
		while(j<at.length){
			e[e.length] = (at[j]);
			j++;
		};
		
		var k=0;
		while(k<e.length){
			this.parameters[e[k].name] = e[k].value;
			k++;
		};
	};
	if(this.serialize) this._serialize(this.serialize);
	
	// transforming parameters inline!
	for(a in this.parameters){
		this._parameters += a + '=' + ((this.encrypt && !this.debug) ? this._e(escape(this.parameters[a].toString())) : escape(this.parameters[a].toString())) + '&';
	};
	this._parameters += '&cacheBuster='+new Date().getTime();
	if (this.debug) alert('DEBUG\nurl: ' + this.url + '\nparameters: ' + this._parameters + '\nmethod: ' + this.method);
	
	// sending
	this._snd = function(xmlhttp, extra, timeout, timer){
		try{
			this.onInit = (o.onInit) ? o.onInit(extra) : null;
			timer = setTimeout(
				function(){
					xmlhttp.abort();
					if(o.onTimeout){
						o.onTimeout(extra);
					} else{
						alert('Não foi possível enviar sua requisição.\nTempo de conexão excedida.');
					}
					return false;
				}, timeout);
		    xmlhttp.onreadystatechange = function(){
				if(xmlhttp.readyState == 4 ){
					if(!xmlhttp.responseText){
						return false;
					}
					clearTimeout(timer);
					this.results = eval(xmlhttp.responseText);
					(o.onFinish) ? o.onFinish(this.results, extra): null;
					return true;
				};
				return false;
			};
			if(this.method == 'POST') {
				xmlhttp.open(this.method, this.url, true);
				xmlhttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded'); 
			    xmlhttp.send(this._parameters);
			} else {
				xmlhttp.open(this.method, this.url + '?' + this._parameters, true);
			    xmlhttp.send(null);
			};
		} catch (err){
			(o.onError) ? o.onError(this.results, this.extra) : function(){alert('Erro durante o envio.\n'+err+'\nPor favor tente novamente.');return false;};
			return false;
		};
		return true;
	};
	this._snd(this.xmlhttp, this.extra, this.timeout, this._timer);
	return true;
};
function ajaxRun(o){
	new ajax(o);
};
function include(u, t) {
	var req = false;
	if (window.XMLHttpRequest){
		try {
			req = new XMLHttpRequest();
		} catch (e) {
			req = false;
		};
	} else if (window.ActiveXObject) {
		try {
			req = new ActiveXObject('Msxml2.XMLHTTP');
		} catch (ee) {
			try {
				req = new ActiveXObject('Microsoft.XMLHTTP');
			} catch (eee) {
				req = false;
			};
		};
	}
	if (req) {
		req.open('GET', u, false);
		req.send(null);
		if(t) {
			var r;
			if(req.responseText.split('<body>').length > 1) r = req.responseText.split('<body>')[1].split('</body>')[0];
			else r = req.responseText;
			t.innerHTML = r;
			delete r;
		}
		else document.write(req.responseText);
		return req.responseText;
	} else {
		alert('Unable to load include file: ' + u);
	};
	return false;
};