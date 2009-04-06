/**
 *
 * openBox
 *
 * @author	Marcelo Carneiro, mcarneiro@gmail.com
 * @author	Nicholas Almeida, www.nicholasalmeida.com
 * @version	2.3.0
 * @since 18/08/2006
 * @requires $, gTag, addEvent, delEvent, createElm, sDiv, hDiv, fxAlpha, winW, winH
 * @usage
		<code>
			openBox('boxTeste')
			openBox('boxTeste', {vAlign:'top'}) - topo e centro;
			openBox('boxTeste', {vAlign:'top'}) - topo e centro;
			openBox('boxTeste', {vAlign:'top', hAlign:'left'}) - topo e à esquerda;
			openBox('boxTeste', {vAlign:'top', hAlign:'right'}) - topo e à direita;
			openBox('boxTeste', {vAlign:'bottom'}) - base e ao centro;
			openBox('boxTeste', {vAlign:'bottom', hAlign:'left'}) - base e à esquerda;
			openBox('boxTeste', {vAlign:'bottom', hAlign:'right'}) - base e à direita;
			openBox('boxTeste', {vAlign:100, hAlign:100}) - topo: 100px, esquerda: 100px;
			openBox('boxTeste', {vAlign:100, hAlign:100, fix:true}) - topo: 100px, esquerda: 100px, FIXO;
			openBox('boxTeste', {vAlign:100, hAlign:100, doScroll:true}) - topo: 100px, esquerda: 100px, FIXO e força scroll até o box;
			openBox('boxTeste', {vAlign:100, hAlign:100, doScroll:100}) - topo: 100px, esquerda: 100px, FIXO e força scroll a 100px;
			openBox('boxTeste', {closeBox:true}) - FECHA ao clicar no fundo;
			openBox('boxTeste', {onOpen:openFnc, onClose:closeFnc}) - Executa função ao abrir e ao fechar o pop;
			
			// see full list of examples at http://interface.desenv/html/exemplos/openBox.html
		</code>
*/

//control vars
var ___onCloseFnc___ = null;
var __lastScrollPos__ = 0;
var __pageHeight__ = 0;
var __pageWidth__ = 0;
var __outOfThePage__ = '-5000px';
var ___bgBoxElm___ = 'openBoxPopBg';
var __tempParam__ = {};
var __configByOpenBox__ = false;
var __lastBoxConfig__ = {};
	__lastBoxConfig__.bgColor = null;
	__lastBoxConfig__.alpha = null;
	__lastBoxConfig__.zIndex = null;
	__lastBoxConfig__.fx = null;
	__lastBoxConfig__.popDefaultStyle = null;
	__lastBoxConfig__.avoidEsc = null;

//openBox image vars
var tmpImgLoader;
var imagemPop;
var loaderPersId;

//config box vars
if(window['__popDefaultStyle__'] == null){
	var __popDefaultStyle__ = null
}
var ___bgColor___ = null;
var ___bgAlpha___ = null;
var ___zIndex___ = null;
var ___avoidEsc___ = null;
var ___effect___ = false;
var __tempBoxId__ = '';
//hide and show selects for ie
function hSelects(){for(var i=0;i<gTag('select').length;i++) gTag('select')[i].style.visibility = 'hidden';return false;}
function sSelects(){for(var m=0;m<gTag('select').length;m++) gTag('select')[m].style.visibility = 'visible';return false;}
//get document scroll position
function winHPos(){return document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop;}
//get max height to generate boxBg
function pageWidth(){
	var htmlWidth = gTag('body')[0].scrollWidth;
	var bodyWidth = gTag('html')[0].scrollWidth;
	
	return (htmlWidth > bodyWidth) ? htmlWidth : bodyWidth;
}
function pageHeight(){
	if(!$('spanTempOpenBox'))
		createElm(null,['span',{id:"spanTempOpenBox"},'']);
	var elms = document.getElementsByTagName('body')[0].childNodes;
	var __elmsPos__ = new Array();
	var __maxHeight__ = 0;
	var htmlHeight = gTag('html')[0].scrollHeight > gTag('html')[0].offsetHeight ? gTag('html')[0].scrollHeight : gTag('html')[0].offsetHeight - 4;
	var bodyHeight = gTag('body')[0].scrollHeight;
	var compareHeight = (htmlHeight > bodyHeight) ? htmlHeight : bodyHeight;
	for(var i=0; i<elms.length; i++){
		if(elms[i].nodeType==1 && elms[i].id != ___bgBoxElm___)
			__elmsPos__.push([getPos(elms[i])[1],elms[i].offsetHeight]);
	}
	for(var i=0; i<__elmsPos__.length; i++)
		if(__elmsPos__[i][0] >= __maxHeight__)
			__maxHeight__ = __elmsPos__[i][0] + __elmsPos__[i][1];
	return ((__maxHeight__ > compareHeight) ? __maxHeight__ : compareHeight);
}

//config default openBox settings
function configBox(o){
	if(typeof(o) == 'string' && o == 'reset'){
		o = false;
	}else{
		o = o || __lastBoxConfig__;
	}
	___bgColor___ = o.bgColor || '000000';
	___bgAlpha___ = o.alpha || 50;
	___zIndex___ = o.zIndex || 10;
	___effect___ = (o.fx === false || o.fx === 'false') ? false : true;
	___avoidEsc___ = (o.avoidEsc === true || o.avoidEsc === 'true') ? true : false;
	__popDefaultStyle__ = o.popDefaultStyle || 'padding:10px;background-color:#fff;border:2px solid #E6E7E8;';
	
	if(!__configByOpenBox__){
		__lastBoxConfig__.bgColor = ___bgColor___;
		__lastBoxConfig__.alpha = ___bgAlpha___;
		__lastBoxConfig__.zIndex = ___zIndex___;
		__lastBoxConfig__.fx = ___effect___;
		__lastBoxConfig__.popDefaultStyle = __popDefaultStyle__;
		__lastBoxConfig__.avoidEsc = ___avoidEsc___;
	}
}
configBox();

function openBox(e, o){
	/*{
		vAlign: 'top' or 'middle' or 'bottom' or Number (top) //defValue: middle
		hAlign: 'left' or 'center' or 'right' or Number (left) or String ("XX.n") or ("XX.p") //defValue: center
			.n sufix centers the box and sum the value to negative left-margin. Ex: "15.n" will center and add 15px to the left
			.n sufix centers the box and reduces the value to negative left-margin. Ex: "15.p" will center and add 15px to the right
		fix: boolean //defValue: false
		closeBox: boolean //defValue: false
		doScroll: boolean //defValue: false
		onOpen: function
		onClose: function
		img:{
			id:'idBox',
			src:'img.jpg',
			loader:'img.gif', //personalized loader
			closeImg: booleam //defValue: true
		}
		config:{
			o.bgColor: string //defValue: '000000'
			o.alpha: number //defValue: 50
			o.zIndex: number //defValue: 10
			o.popDefaultStyle: string //defValue: 'padding:10px;background-color:#fff;border:2px solid #E6E7E8;'
			o.fx: boolean //false
		}
	}*/

	if(is.ie5){
		alert('Este recurso não está disponível para o internet Explorer 5.0.');
		try{
			if(o.img && o.img.src)
				window.open(o.img.src);
		}catch(e){
			//
		}
		return false;
	}
	
	o = o || false;
	var va = o.vAlign || 'middle';
	var ha = o.hAlign || 'center';
	var fix = (o.fix === true || o.fix === 'true') ? true : false;
	var doScroll = o.doScroll || ((o.doScroll === true || o.doScroll === 'true') ? true : false);
	var cls = (o.closeBox === true || o.closeBox === 'true') ? true : false;
	var onOpen = o.onOpen || false;
	var onClose = o.onClose || false;
	var config = o.config || false;

	//if doScroll is defined, fix will be "true" and Vertical align OR the defined number OR doScroll value
	if(doScroll){
		fix = true;
		va = (typeof(va) == 'number') ? va : doScroll;
	}

	if(config){
		__configByOpenBox__ = true;
		configBox(config);
	}
	
	var obj = {};
	obj.vAlign = va;
	obj.hAlign = ha;
	obj.fix = fix;
	obj.doScroll = doScroll;
	obj.closeBox = cls;	
	obj.onOpen = onOpen;
	obj.onClose = onClose;
	obj.avoidEsc = ___avoidEsc___;
	
	//add key event (esc)
	if(!___avoidEsc___)
		addEvent(document,'keyup',openBoxKeyEvt);

	if(!window['fxAlpha'] || is.ie55)
		___effect___ = false;

	//set page size
	__pageHeight__ = pageHeight();
	__pageWidth__ = pageWidth();

	//create box default container (for openBox image)
	if(!e){
		if(!$('boxPopDefault'))
			createElm(false,[
				'div',{id:'boxPopDefault', style:__popDefaultStyle__},
					''
			]);
		e = 'boxPopDefault';
	}
	__tempBoxId__ = e;
	
	//create bgBox
	var extra;
	if(is.ie){
		extra = 'background:#'+___bgColor___+';filter:Alpha(Opacity='+___bgAlpha___+')';
	}else{
		extra = 'background:#'+___bgColor___+';opacity:'+(___bgAlpha___/100);
	}
	if(!$(___bgBoxElm___)){
		createElm(false,[
			'div',{id:___bgBoxElm___, style:'position:absolute;z-index:'+___zIndex___+';left:0,top:0;'+extra},
				''
		]);
	}
	delete extra;

	//background event to close the Box
	if(cls)
		addEvent($(___bgBoxElm___),'click',function(){
			closeBox(e);
		});
	
	hideBox(e);
	hideBox(___bgBoxElm___);
		
	//openBox image
	if(o.img){
		var closeImg = (o.img.closeImg === false || o.img.closeImg === 'false') ? false : true;
		/*
			img{
				id:'imageId'
				src:'../img/img.jpg'
				dontCloseImg: boolean
			}
		*/
		
		//create image element
		if(!$('imgTarget')){
			var img = document.createElement('img');
			img.setAttribute('id','imgTarget');
			$(e).appendChild(img);
		}
		//create loader element
		if(!o.img.loader){
			if(!$('boxImgLoader')){
				createElm(false,[
					'div',{id:'boxImgLoader', style:'position:absolute; top:'+__outOfThePage__+'; left:'+__outOfThePage__+';width:280px;height:auto;background:#F1F1F1;'},[
						'p',{style:'margin:50px; 100px;text-align:center;'},
							'carregando imagem...'
					]
				]);
				loaderPersId = 'boxImgLoader';
			}
		}else{
			//personalized loader
			if($(o.img.loader)){
				loaderPersId = $(o.img.loader).id;
				$(loaderPersId).style.position = 'absolute';
				$(loaderPersId).style.left = 
				$(loaderPersId).style.top = __outOfThePage__;
			}else{
				alert('openBox Error (o.img): Loader ID "'+o.img.loader+'" not found');
			}
		}
		hideBox(loaderPersId);
		
		//start image loader
		imagemPop = new Image();
		imagemPop.src = o.img.src;
		clearInterval(tmpImgLoader);
		tmpImgLoader = setInterval(function(){
			if(imagemPop.error){
				clearInterval(tmpImgLoader);
				alert('Erro ao carregar a imagem:\n'+imagemPop.src);
			}
			if(imagemPop.complete){
				clearInterval(tmpImgLoader);
				setTimeout(function(){
					if($(loaderPersId)){
						if(___effect___){
							fxAlpha(loaderPersId,1,0,function(){
								__afterLoadImageActions(e,o,obj,imagemPop,closeImg);
							},1);
						}else{
							hideBox(loaderPersId);
							__afterLoadImageActions(e,o,obj,imagemPop,closeImg);
						}
					}
				},500);
			}else{
				$(e).style.visibility = 'hidden';
			}
		}, 50);
	}
	
	$(e).style.top = __outOfThePage__;
	$(e).style.left = __outOfThePage__;
	
	alignBox(e, obj);
	
	//effect stuff
	prepareAlphaBox(___bgBoxElm___);
	prepareAlphaBox(e);
	
	if(o.img)
		$(loaderPersId).style.visibility = 'visible';
	
	/*********************
	 * IE SELECT FIX     *
	 *********************/
	
	//hide selects for ie
	if(is.ie)
		hSelects();
	//show pop selects
	var popCombos = gTag('select', $(e));
	for(var i=0; i<popCombos.length; i++)
		popCombos[i].style.visibility = "visible";
	
	/*********************
	 * IE SELECT FIX END *
	 *********************/
	
	if(___effect___){
		if(o.img){
			hDiv(e);
			fxFolded[___bgBoxElm___] = true;
			fxAlpha(___bgBoxElm___,0,(___bgAlpha___/100),function(){
				sDiv(e);
				fxFolded[loaderPersId] = true;
				fxAlpha(loaderPersId,0,1,function(){
					__applyFunctions(obj);
				},1);
			},1);
		}else{
			hDiv(e);
			fxFolded[___bgBoxElm___] = true;
			fxAlpha(___bgBoxElm___,0,(___bgAlpha___/100),function(){
				sDiv(e);
				fxFolded[e] = true;
				fxAlpha(e,0,1,function(){
					__applyFunctions(obj);
				},1);
			},1);
		}
	}else{
		showBox(___bgBoxElm___,___bgAlpha___);
		__applyFunctions(obj);
		if(o.img){
			showBox(loaderPersId);
		}else{
			showBox(e);
		}
	}
	return true;
}

function alignBox(e, o){
	
	var va = o.vAlign;
	var ha = o.hAlign;
	var doScroll = o.doScroll;
	var fix = o.fix;
	var bgBox = $(___bgBoxElm___);
	
	$(e).style.position = 'absolute';
	$(e).style.zIndex = ___zIndex___+2;

	//aligna and resize box
	bgBox.style.height = __pageHeight__ +'px';
	bgBox.style.width = '100%';
	bgBox.style.top = 0;
	bgBox.style.left = 0;

	__tempParam__['elm'] = e;
	__tempParam__['obj'] = o;
	__doTheAlign(e,va,ha,fix);
	
	//doScroll (forces window.scrollTo when the o.fix is set to fix
	if(doScroll){
		var boxTop = Number($(e).style.top.split('px')[0]);
		if(winHPos() > boxTop || (winH() + winHPos()) < boxTop){
			__lastScrollPos__ = winHPos();
			if(!isNaN(doScroll)){
				window.scrollTo(0,doScroll);
			}else if(doScroll === true){
				if(!isNaN(va)){
					window.scrollTo(0,va);
				}else{
					alert('AlignBox Error (doScroll): The "vAlign" parameter MUST be a number.');
				}
			}
		}
	}
}

function closeBox(e){
	//remove events
	delEvent(document, 'keyup', openBoxKeyEvt);
	delEvent(window, 'scroll', __openBoxOnResizeFnc);
	delEvent(window, 'resize', __openBoxOnResizeFnc);
	delEvent(window, 'resize', __adjustBoxBgSize);

	if(___effect___){
		fxAlpha(e,1,0,function(){
			hDiv(e);
			fxAlpha(___bgBoxElm___,(___bgAlpha___/100),0,function(){
				__doCloseBox(e)
			},2);
		},1);
	}else{
		__doCloseBox(e)
	}
	
	if(__configByOpenBox__){
		configBox();
		__configByOpenBox__ = false;
	}
}

/**
 * SUPPORT FUNCTIONS
 */
//closeBox function
function __doCloseBox(e){
	//run custom function after close
	if(___onCloseFnc___)
		try{
			___onCloseFnc___();
		}catch(e){
			alert('openBox Error (o.onClose):\n"'+e+'".');
		}

	clearInterval(tmpImgLoader);
	
	//delete elements
	if($('imgTarget'))
		delElm('imgTarget');
	
	if($('boxImgLoader'))
		delElm('boxImgLoader');
	
	if($(loaderPersId))
		hDiv(loaderPersId);
	
	if($(___bgBoxElm___))
		delElm(___bgBoxElm___);
		
	if($('boxPopDefault'))
		delElm('boxPopDefault');
	
	if($(e))
		hDiv(e);

	if(is.ie)
		sSelects();

	//send to the last scroll position;
	if(__lastScrollPos__ != 0)
		window.scrollTo(0,__lastScrollPos__);

	//reset vars
	imagemPop =
	___onCloseFnc___ =
	__tempBoxId__ = null;
	__tempParam__ = {};
	__lastScrollPos__ = 0;

}
//apply event functions
function __applyFunctions(o){
	//exec onOpen Function (right after box opens)
	if(o.onOpen)
		try{
			o.onOpen();
		}catch(e){
			alert('openBox Error (o.onOpen):\n"'+e+'".');
		}
	
	//defines onColse Function
	if(o.onClose)
		___onCloseFnc___ = o.onClose;
	//apply window functions
	if(!o.fix){
		addEvent(window, 'scroll', __openBoxOnResizeFnc);
		addEvent(window, 'resize', __openBoxOnResizeFnc);
	}else{
		addEvent(window, 'resize', __adjustBoxBgSize);
	}
}
//alignBox Listener
function __doTheAlign(e,va,ha,fix){
	var w = $(e).offsetWidth;
	var h = $(e).offsetHeight;
	
	//align personalized loader
	if($('boxImgLoader') || $(loaderPersId)){
		var boxLoader2 = $('boxImgLoader') || $(loaderPersId);
		if(boxLoader2 == $('boxImgLoader'))sDiv('boxImgLoader');
		var ww = boxLoader2.offsetWidth;
		var hh = boxLoader2.offsetHeight;
		boxLoader2.style.position = 'absolute';
		boxLoader2.style.top = (winHPos()+(winH()/2)-(hh/2))+'px';
		boxLoader2.style.marginLeft = (winW()/2)+'px';
		boxLoader2.style.left = (-(ww/2))+'px';
		boxLoader2.style.zIndex = ___zIndex___+1;
	}
	//align box vertically
	if(isNaN(va)){
		switch(va){
			case 'top':
				$(e).style.top = winHPos()+'px';
				break;
			case 'middle':
				$(e).style.top = (winHPos()+(winH()/2)-(h/2))+'px';
				break;
			case 'bottom':
					if(is.ie){
						$(e).style.top = ((winHPos()+winH()-h)-4)+'px';
					}else{
						$(e).style.top = (winHPos()+winH()-h)+'px';
					}
				break;
			default:
				alert('alignBox Error (custom vertical align): Parameter "'+ va +'" is undefined');
		}
	}else{
		if(fix){
			$(e).style.top = va+'px';
		}else{
			$(e).style.top = winHPos()+va+'px';
		}
	}
	//align box horizontally
	if(isNaN(ha)){
		if(ha.split('.n').length>1){
			$(e).style.left = '50%';
			$(e).style.marginLeft = (-($(e).offsetWidth/2)-ha.split('.n')[0])+'px';
			ha = 'false';
		}
		if(ha.split('.p').length>1){
			$(e).style.left = '50%';
			$(e).style.marginLeft = (ha.split('.p')[0]-($(e).offsetWidth/2))+'px';
			ha = 'false';
		}
		switch(ha){
			case 'left':
				$(e).style.marginLeft = 0;
				$(e).style.left = 0;
				break;
			case 'center':
				$(e).style.marginLeft = (-($(e).offsetWidth/2))+'px';
				$(e).style.left = '50%';
				$(e).style.right = 'auto';
				break;
			case 'right':
				$(e).style.marginLeft = 0;
				$(e).style.left = 'auto';
				$(e).style.right = 0;
				break;
			case 'false':
				break;
			default:
				alert('alignBox Error (custom horizontal align): Parameter "'+ ha +'"  is undefined');
		}
	}else{
		$(e).style.marginLeft = 0;
		$(e).style.left = ha+'px';
	}
	
	//set bgBox size
	__adjustBoxBgSize();

}
function __afterLoadImageActions(e,o,obj,imagemPop,closeImg){
	var tmpImg = $('imgTarget');

	if($('boxImgLoader'))
		delElm('boxImgLoader');
	tmpImg.width = imagemPop.width;
	tmpImg.height = imagemPop.height;
	tmpImg.src = imagemPop.src;
	tmpImg.title = 'Clique na imagem para fechá-la.';
	
	$(e).style.top = __outOfThePage__;
	$(e).style.left = __outOfThePage__;
	
	prepareAlphaBox(e);
	
	alignBox(e, obj);

	if(___effect___){
		fxFolded[e] = true;
		fxAlpha(e,0,1,null,1);
	}else{
		showBox(e);
	}
	
	if(closeImg){
		$('imgTarget').style.cursor = 'pointer';
		addEvent($('imgTarget'),'click',function(){
			closeBox(e);
		});
	}else{
		$('imgTarget').setAttribute('onclick',false);
	}
	delete tmpImg;
}

//onResize function
function __openBoxOnResizeFnc(){
	__doTheAlign(__tempParam__['elm'], __tempParam__['obj'].vAlign, __tempParam__['obj'].hAlign);
}

//onResize function (when fix = true);
function __adjustBoxBgSize(){
	var bgBox = $(___bgBoxElm___);

	if(!bgBox)
		return false;

	if(bgBox.offsetWidth <= __pageWidth__){ //width
		if(is.ie6 && (winW() < pageWidth())){ 
			// bgBox.style.width = (pageWidth()-20)+'px';
			bgBox.style.width = pageWidth()+'px';
		}else{
			bgBox.style.width = winW()+'px';
		}
	}else{
		bgBox.style.width = '100%';
	}

	if(winH() > __pageHeight__){ //height
		bgBox.style.height = winH() +'px';
		if(is.ie){
			bgBox.style.top = (winHPos()-4) +'px';
		}else{
			bgBox.style.top = winHPos() +'px';
		}
	}else if(__pageHeight__ <= bgBox.offsetHeight){
		bgBox.style.height = pageHeight()+'px';
	}else{
		bgBox.style.height = __pageHeight__ +'px';
	}
	return true;
}

//hideDiv showDiv (alpha)
function hideBox(e){
	if(___effect___){
		$(e).style.opacity = '0';
		$(e).style.filter = 'Alpha(Opacity=0)';
	}
	$(e).style.visibility = 'hidden';
	sDiv(e);
}
function prepareAlphaBox(e){
	if(___effect___){
		$(e).style.opacity = '0';
		$(e).style.filter = 'Alpha(Opacity=0)';
		$(e).style.visibility = 'visible';
		sDiv(e);
	}else{
		$(e).style.visibility = 'hidden';
	}
}
function showBox(e, alpha){
	if(___effect___ || alpha){
		alpha = alpha || 100;
		$(e).style.opacity = (alpha/100);
		$(e).style.filter = 'Alpha(Opacity='+alpha+')';
	}
	$(e).style.visibility = 'visible';
	sDiv(e);
}

/**
 * KEY FUNCTION
 */
function openBoxKeyEvt(evt){
	evt = evt || window.event;
	if(evt.keyCode == 27) //esc
		closeBox(__tempBoxId__);
}