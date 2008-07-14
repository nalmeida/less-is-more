/**
 * @author	Nicholas Almeida
 * @version	2.1.1
 * @original http://www.javascript-coder.com
*/

var valColorError = "#FEFEDE";
var __buttonSubmit__;
var __newWidth__;
var __buttonSubmitSrc__;
var __cAlert__;
var __lang__;
var __cAlertDiv__;
var __cAlertTxt__;
var __oldFocus__;
var __cAlertShow__ = true;

function frmValidator(frmname, lang){
	this.formobj=document.forms[frmname];
	if(!this.formobj){
		alert('BUG: Não foi possívem acessar o formulário: '+frmname);
		return;
	};
	if(this.formobj.onsubmit){
		this.formobj.old_onsubmit = this.formobj.onsubmit;
		this.formobj.onsubmit=null;
	}else{
		this.formobj.old_onsubmit = null;
	};
	this.formobj.onsubmit=form_submit_handler;
	this.lang = __lang__ = lang;
	this.cAlert = customAlert;
	this.av = add_validation;
	this.cSubmit = change_submit;	
	this.amv = addMultiVal;
	this.customValidation=set_addnl_vfunction;
	this.clearAllValidations = clear_all_validations;
	this.formobj.onreset = rABgColor;
};
function set_addnl_vfunction(functionname){
	this.formobj.addnlvalidation = functionname;
};
function clear_all_validations(){
	for(var itr=0;itr < this.formobj.elements.length;itr++){
		this.formobj.elements[itr].validationset = null;
	};
};
function customAlert(d,t,s){
	if(!gElm(d) || !gElm(t)) {
		alert('BUG: Box id not defined or Text id not defined.');
		return;
	} else {
		__cAlert__ = true;
		__cAlertDiv__ = d;
		__cAlertTxt__ = t;
		__cAlertShow__ = s;		
	}
};
function form_submit_handler(){
	for(var itr=0;itr < this.elements.length;itr++){
		if(this.elements[itr].validationset && !this.elements[itr].validationset.validate()){
			return false;
		};
	};
	if(this.addnlvalidation){
		str =' var ret = '+this.addnlvalidation+'()';
		eval(str);
		if(!ret) return ret;
	};

	if(__buttonSubmit__){
		if (__lang__ == 'en'){
			stButton = 'Sending...'; 
		}else if(__lang__ == 'es'){
			stButton = 'Enviando...'; 
		}else{
			stButton = 'Enviando...'; 
		}

		__buttonSubmit__.title = stButton;
		if(__buttonSubmitSrc__){
			__buttonSubmit__.src = __buttonSubmitSrc__;
		}else{
			__buttonSubmit__.value = stButton;
		}
		__buttonSubmit__.disabled = 'disabled';
		__buttonSubmit__.style.cursor = 'wait';
		if(__newWidth__) {__buttonSubmit__.style.width = __newWidth__};
	}
	return true;
};
function add_validation(itemname,descriptor,errstr){
	if(!this.formobj){
		alert('BUG: Formulário não definido corretamente!');
		return;
	};
	var itemobj = this.formobj[itemname];
	if (!itemobj) alert('BUG: Não foi possível encontrar nehum campo com o name=' + itemname);
	if(itemobj.length && isNaN(itemobj.selectedIndex) ){
		itemobj = itemobj[0];
	};	
	if(!itemobj){
		alert('BUG: Não foi possível encontrar o campo com o nome: '+itemname);
		return;
	};

	if(!itemobj.validationset){
		itemobj.validationset = new ValidationSet(itemobj);
	};
	itemobj.validationset.add(descriptor,errstr);
};
function change_submit(bt,newWidth, buttonSubmitSrc){
	if(!this.formobj){
		alert('BUG: Formulário não definido corretamente!');
		return;
	};
	__buttonSubmit__ = gElm(bt);
	__newWidth__ = newWidth;
	__buttonSubmitSrc__ = buttonSubmitSrc;
};
function ValidationDesc(inputitem,desc,error){
	this.desc=desc;
	this.error=error;
	this.itemobj = inputitem;
	this.validate=vdesc_validate;
};
function vdesc_validate() {
	if(!validateInput(this.desc,this.itemobj,this.error)){
		if(!__cAlert__) this.itemobj.focus();
		return false;
	};
	return true;
};
function ValidationSet(inputitem){
	this.vSet=new Array();
	this.add= add_validationdesc;
	this.validate= vset_validate;
	this.itemobj = inputitem;
};
function add_validationdesc(desc,error){
	this.vSet[this.vSet.length]= 
	new ValidationDesc(this.itemobj,desc,error);
};
function vset_validate(){
	for(var itr=0;itr<this.vSet.length;itr++){
		if(!this.vSet[itr].validate()){
			return false;
		};
	};
	return true;
};
function validateEmail(email){
	if (email.length <= 0) {
		return true;
	};
	var splitted = email.match('^(.+)@(.+)$');
	if (splitted == null)return false;
	if (splitted[1] != null ) {
		var regexp_user = /^\"?[\w-_\.]*\"?$/;
		if (splitted[1].match(regexp_user) == null)return false;
	};
	if (splitted[2] != null) {
		var regexp_domain = /^[\w-\.]*\.[A-Za-z]{2,4}$/;
		if (splitted[2].match(regexp_domain) == null) {
			var regexp_ip = /^\[\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\]$/;
			if (splitted[2].match(regexp_ip) == null)return false;
		};
		return true;
	};
	return false;
};

function TestComparison(objValue,strCompareElement,strvalidator,strError){
	var bRet=true;
	var objCompare=null;
	if(!objValue.form){
		alert('BUG: Formulário não definido!');
		return false
	};
	objCompare = objValue.form.elements[strCompareElement];
	if(!objCompare){
		alert('BUG: Campo com o nome '+strCompareElement+' não encontrado!');
		return false;
	};
	var cmpstr='';
	switch(strvalidator){
		case 'equal': 
		case 'eq': {
			if(objValue.value != objCompare.value){
				if (__lang__ == 'en'){
					cmpstr = ' must be the same as field \"';
				}else if(__lang__ == 'es'){
					cmpstr = ' debe ser igual al campo \"';
				}else{
					cmpstr = ' deve ser igual ao campo \"';
				}
				bRet = false;
			};
			break;
		};
		case 'notequal':
		case 'noteq': {
			if(objValue.value.length > 0 && objCompare.value.length > 0 && objValue.value == objCompare.value){
				if (__lang__ == 'en'){
					cmpstr = ' must be different from field \"';
				}else if(__lang__ == 'es'){
					cmpstr = ' debe ser diferente del campo \"';
				}else{
					cmpstr = ' deve ser diferente do campo \"';
				}
				bRet = false;
			};
			break;			
		};			
	};
	if(bRet==false){
		if(!strError || strError.length==0){
			if (__lang__ == 'en'){
				strError = 'Field \"' + objValue.title + '\"' + cmpstr + objCompare.title + '\".'; 
			}else if(__lang__ == 'es'){
				strError = 'El campo \"' + objValue.title + '\"' + cmpstr + objCompare.title + '\".'; 
			}else{
				strError = 'O campo \"' + objValue.title + '\"' + cmpstr + objCompare.title + '\".'; 
			}
			cBgColor(objValue);
		};
		boxAlert(strError, objValue);
	};
	if(bRet) rBgColor(objValue);
	return bRet;
};

function TestSelMin(objValue,strMinSel,strError){
	var bret = true;
	var objcheck = objValue.form.elements[objValue.name];
	var chkcount =0;
	if(objcheck.length){
		for(var c=0;c < objcheck.length;c++){
			if(objcheck[c].checked == '1'){
				chkcount++;
			};
		};
	}else {
		chkcount = (objcheck.checked == '1')?1:0;
	};
	var minsel = eval(strMinSel);
	if(chkcount < minsel) {
		if(!strError || strError.length ==0) { 
			if (__lang__ == 'en'){
				strError = 'Select at least ' + minsel + ' option(s) for field \"' + objValue.title + '\".';
			}else if(__lang__ == 'es'){
				strError = 'Seleccione al menos ' + minsel + ' opciones para el campo \"' + objValue.title + '\".';
			}else{
				strError = 'Selecione ao menos ' + minsel + ' opção(ões) para o campo \"' + objValue.title + '\".';
			}
		};
		boxAlert(strError, objValue); 
		bret = false;
	};
	if(bret) rBgColor(objValue);
	return bret;
};

function TestSelMax(objValue,strMaxSel,strError){
	var gret = true;
	var objcheck = objValue.form.elements[objValue.name];
	var chkcount =0;
	if(objcheck.length){
		for(var c=0;c < objcheck.length;c++){
			if(objcheck[c].checked == '1'){
				chkcount++;
			};
		};
	}else {
		chkcount = (objcheck.checked == '1')?1:0;
	};
	var maxsel = eval(strMaxSel);
	if(chkcount > maxsel) {
		if(!strError || strError.length ==0) { 
			if (__lang__ == 'en'){
				strError = 'Select at the most ' + maxsel + ' option(s) for field \"' + objValue.title + '\".';
			}else if(__lang__ == 'es'){
				strError = 'Seleccione como máximo ' + maxsel + ' opciones para el campo \"' + objValue.title + '\".';
			}else{
				strError = 'Selecione no máximo ' + maxsel + ' opção(ões) para o campo \"' + objValue.title + '\".';
			}
		};
		boxAlert(strError, objValue); 
		gret = false;
	};
	if(gret) rBgColor(objValue);
	return gret;
};

function TestDontSelect(objValue,index,strError){
	var ret = true;
	if(objValue.selectedIndex == null) { 
		alert('BUG: Este comando só pode ser usado para elementos de seleção.'); 
		ret = false; 
	}else if(objValue.selectedIndex == eval(index)) { 
		if(!strError || strError.length ==0) { 
			if (__lang__ == 'en'){
				strError = 'Select one of the options for field \"' + objValue.title + '\".'; 
			}else if(__lang__ == 'es'){
				strError = 'Seleccione una de las opciones para el campo \"' + objValue.title + '\".'; 
			}else{
				strError = 'Selecione uma das opções para o campo \"' + objValue.title + '\".'; 
			}
		};                                                        
		boxAlert(strError, objValue); 
		ret =  false;
		cBgColor(objValue);                            
	}; 
	if(ret) rBgColor(objValue);
	return ret;
};

function TestRequiredInput(objValue,strError){
	var ret = true;
	if(eval(objValue.value.length) == 0) { 
		if(!strError || strError.length ==0) { 
			if (__lang__ == 'en'){
				strError = 'Field \"' + objValue.title + '\" must be filled out.'; 
			}else if(__lang__ == 'es'){
				strError = 'El campo \"' + objValue.title + '\" es obligatorio.'; 
			}else{
				strError = 'O campo \"' + objValue.title + '\" é obrigatório.'; 			
			}
		};
		boxAlert(strError, objValue);
		ret=false; 
		cBgColor(objValue);
	};
	if(ret) rBgColor(objValue);
	return ret;
};

function TestMaxLen(objValue,strMaxLen,strError){
	var ret = true;
	if(eval(objValue.value.length) > eval(strMaxLen)) { 
		if(!strError || strError.length ==0) {
			if (__lang__ == 'en'){
				strError = 'Field \"' + objValue.title + '\" can contain up to '+ strMaxLen +' character(s).'; 
				strErrorCont = 'Currently it has: ' + objValue.value.length + ' character(s).';
			}else if(__lang__ == 'es'){
				strError = 'El campo \"' + objValue.title + '\" puede contener hasta '+ strMaxLen +' caracter(es).'; 
				strErrorCont = 'Actualmente tiene: ' + objValue.value.length + ' carácter(es).';
			}else{
				strError = 'O campo \"' + objValue.title + '\" pode conter no máximo '+ strMaxLen +' caracter(es).'; 
				strErrorCont = 'Atualmente ele tem: ' + objValue.value.length + ' caracter(es).';
			}
		};
		cBgColor(objValue);
		boxAlert(strError + '\n' + strErrorCont, objValue); 
		ret = false; 
	};
	if(ret) rBgColor(objValue);
	return ret;
};

function TestMinLen(objValue,strMinLen,strError){
	var ret = true;
	if(eval(objValue.value.length) <  eval(strMinLen)) { 
		if(!strError || strError.length ==0) { 
			if (__lang__ == 'en'){
				strError = 'Field \"' + objValue.title + '\" must contain at least '+ strMinLen +' character(s).'; 
				strErrorCont = 'Currently it has: ' + objValue.value.length + ' character(s).';
			}else if(__lang__ == 'es'){
				strError = 'El campo \"' + objValue.title + '\" debe contener un mínimo de '+ strMinLen +' caracter(es).'; 
				strErrorCont = 'Actualmente tiene: ' + objValue.value.length + ' carácter(es).';
			}else{
				strError = 'O campo \"' + objValue.title + '\" deve conter no mínimo ' + strMinLen + ' caracter(es).'; 
				strErrorCont = 'Atualmente ele tem: ' + objValue.value.length + ' caracter(es).';
			}
		};
		cBgColor(objValue);
		boxAlert(strError + '\n' + strErrorCont, objValue); 
		ret = false;   
	};
	if(ret) rBgColor(objValue);
	return ret;
};

function TestInputType(objValue,strRegExp,strError,strDefaultError){
	var ret = true;
	var charpos = objValue.value.search(strRegExp); 
	if(objValue.value.length > 0 &&  charpos >= 0) { 
		if(!strError || strError.length ==0) { 
			strError = strDefaultError;
			if (__lang__ == 'en'){
				strErrorCont = 'Error of character in position: ';
			}else if(__lang__ == 'es'){
				strErrorCont = 'Error en el carácter en la posición: ';
			}else{
				strErrorCont = 'Erro no caracter na posição: ';
			}
		};
		cBgColor(objValue);
		boxAlert(strError + '\n' + strErrorCont + eval(charpos+1)+'.', objValue); 
		ret = false; 
	};
	if(ret) rBgColor(objValue);
	return ret;
};

function TestEmail(objValue,strError){
	var ret = true;
	if(objValue.value.length > 0 && !validateEmail(objValue.value)	 ) { 
		if(!strError || strError.length ==0) { 
			if (__lang__ == 'en'){
				strError = '\"' + objValue.value+'\" is not a valid e-mail for field \"' + objValue.title + '\".'; 
			}else if(__lang__ == 'es'){
				strError = '\"' + objValue.value+'\" no es un e-mail válido para el campo \"' + objValue.title + '\".'; 
			}else{
				strError = '\"' + objValue.value+'\" não é um e-mail válido para o campo \"' + objValue.title + '\".'; 
			}
		};
		boxAlert(strError, objValue); 
		ret = false; 
		cBgColor(objValue);
	};
	if(ret) rBgColor(objValue);
	return ret;
};

function TestRegExp(objValue,strRegExp,strError){
	var ret = true;
	if( objValue.value.length > 0 && !objValue.value.match(strRegExp) ) { 
		if(!strError || strError.length ==0) {
			if (__lang__ == 'en'){
				strError = 'Invalid character found in field \"' + objValue.title + '\".';
			}else if(__lang__ == 'es'){
				strError = 'Carácter no permitido encontrado en el campo \"' + objValue.title + '\".';
			}else{
				strError = 'Caracter não permitido encontrado no campo \"' + objValue.title + '\".';
			}
		};
		boxAlert(strError, objValue); 
		ret = false;
		cBgColor(objValue);
	};
	if(ret) rBgColor(objValue);
	return ret;
};

function TestSelectOneRadio(objValue,strError){
	var objradio = objValue.form.elements[objValue.name];
	if (!objradio) alert('BUG: Não foi possível encotrar nenhum radioButton no formulário.\nCertifique-se de que eles existem e que tem o atributo \"name\".');
	var one_selected=false;
	for(var r=0;r < objradio.length;r++){
		if(objradio[r].checked == '1'){
			one_selected=true;
			break;
		};
	};
	if(false == one_selected){
		if(!strError || strError.length ==0) {
			if (__lang__ == 'en'){
				strError = 'Select one of the options for field  \"'+objValue.title+'\".'; 
			}else if(__lang__ == 'es'){
				strError = 'Seleccione una de las opciones para el campo \"'+objValue.title+'\".'; 
			}else{
				strError = 'Selecione uma das opções para o campo \"'+objValue.title+'\".'; 
			}
		};	
		boxAlert(strError, objValue);
	};
	return one_selected;
};

function TestCpf(v) {
	if (v == "00000000000" || v == "11111111111" || v == "22222222222" || v == "33333333333" || v == "44444444444" || v == "55555555555" || v == "66666666666" || v == "77777777777" || v == "88888888888" || v == "99999999999") return false;
	if(!v) return true;
	var s=null;
	var r=null;
	if(v.length!=11||v.match(/1{11};|2{11};|3{11};|4{11};|5{11};|6{11};|7{11};|8{11};|9{11};|0{11};/)) return false;
	s=0;
	for(var i=0;i<9;i++) s+=parseInt(v.charAt(i))*(10-i);
	r=11-(s%11);
	if(r==10||r==11) r=0;
	if(r!=parseInt(v.charAt(9))) return false;
	s=0;
	for(var i=0;i<10;i++) s+=parseInt(v.charAt(i))*(11-i);
	r=11-(s%11);
	if(r==10||r==11) r=0;
	if(r!=parseInt(v.charAt(10))) return false;
	return true;
};

function TestCnpj(v) {
	if(!v) return true;
	var m = new Array('543298765432','6543298765432');
	var d = new Array(0,0);
	for (var t=0; t<2; t++) {
		for(x=0; x<13; x++) {
			if ((t==0 && x!=12) || t==1) d[t] += ( parseInt(v.slice(x,x+1)) * parseInt(m[t].slice(x,x+1)) );
		};
		d[t] = (d[t] * 10) % 11;
		if (d[t] == 10) d[t] = 0;
	};
	return (d[0] == parseInt(v.slice(12,13)) && d[1] == parseInt(v.slice(13,14)));
};

function TestFile(v ,ext) {
	if(!v) return true;
	var e = ext.split(",");
	for(var i=0; i<e.length; i++) {
		if (v.substr(v.lastIndexOf('.')+1)==e[i]) return true;
	};
	return false;
};

function replaceAll(str, replacements ) {
	for ( i = 0; i < replacements.length; i++ ) {
		var idx = str.indexOf( replacements[i][0] );
		while ( idx > -1 ) {
			str = str.replace( replacements[i][0], replacements[i][1] );
			idx = str.indexOf( replacements[i][0] );
		};
	};
	return str;
};
function addMultiVal(){
	var arrPar = [];
	for(var i=0; i<arguments.length; i++){
		arrPar[i] = arguments[i];
	};
	var fld = arrPar[0];
	arrPar = arrPar.slice(1,arrPar.length);
	for (var a=0;a<arrPar.length;a++){
		this.av(fld,arrPar[a]);
	};
	delete arrPar, fld;
};
function validateInput(strValidateStr,objValue,strError) {
	var ret = true;
	var epos = strValidateStr.search('='); 
	var command  = ''; 
	var cmdvalue = ''; 
	if(epos >= 0) { 
		command  = strValidateStr.substring(0,epos); 
		cmdvalue = strValidateStr.substr(epos+1); 
	}else { 
		command = strValidateStr; 
	}; 
	switch(command) { 
		case 'req': 
		case 'required': { 
			ret = TestRequiredInput(objValue,strError);
			break;             
		};
		case 'maxlength': 
		case 'maxlen': { 
			ret = TestMaxLen(objValue,cmdvalue,strError);
			break; 
		};
		case 'minlength': 
		case 'minlen': { 
			ret = TestMinLen(objValue,cmdvalue,strError);
			break; 
		};
		case 'alnum': 
		case 'alphanumeric': { 
			if (__lang__ == 'en'){
				strTxtError = 'Only alphanumeric characters (letters and numbers) are valid for field'; 
			}else if(__lang__ == 'es'){
				strTxtError = 'Sólo caracteres alfanuméricos (letras y números) son permitidos para el campo'; 
			}else{
				strTxtError = 'Apenas caracteres alfanuméricos(letras e números) são permitidos para o campo';
			}
		
			ret = TestInputType(objValue,'[^A-Za-z0-9áàãâäéèêëíìîïóòõôöúùûüçÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÔÖÚÙÛÜÇ\\s]',strError, strTxtError + ' \"' + objValue.title + '\".');
			break;
		};
		case 'num': 
		case 'number': 
		case 'numeric': {
			if (__lang__ == 'en'){
				strTxtError = 'Only numeric characters (numbers) are valid for field'; 
			}else if(__lang__ == 'es'){
				strTxtError = 'Sólo caracteres numéricos (números) son permitidos para el campo'; 
			}else{
				strTxtError = 'Apenas caracteres numéricos(números) são permitidos para o campo';
			}
		 
			ret = TestInputType(objValue,'[^0-9]',strError,  strTxtError + ' \"' + objValue.title + '\".');
			break;
		};
		case 'letters': 
		case 'let': { 
			if (__lang__ == 'en'){
				strTxtError = 'Only letters are valid for field'; 
			}else if(__lang__ == 'es'){
				strTxtError = 'Sólo letras son permitidas para el campo'; 
			}else{
				strTxtError = 'Apenas letras são permitidas para o campo';
			}
		
			ret = TestInputType(objValue,'[^A-Za-záàãâäéèêëíìîïóòõôöúùûüçÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÔÖÚÙÛÜÇ\\s]',strError, strTxtError + ' \"' + objValue.title + '\".');
			break; 
		};
		case 'restrict':
		case 'rest': { 
			if (__lang__ == 'en'){
				strTxtError = 'Only letters, numbers, \"-\" and \"_\" are valid for field'; 
			}else if(__lang__ == 'es'){
				strTxtError = 'Sólo letras, números, \"-\" y \"_\" son permitidos para el campo'; 
			}else{
				strTxtError = 'Apenas letras, números, \"-\" e \"_\"  são permitidas para o campo';
			}
			ret = TestInputType(objValue,'[^A-Za-z0-9-_]',strError, strTxtError + ' \"' + objValue.title + '\".');
			break; 
		};
		case 'email': { 
			ret = TestEmail(objValue,strError);
			break; 
		};
		case "lt":
		case "lessthan":{
		    if (isNaN(objValue.value)) {
				if (__lang__ == 'en'){
					strTxtError = 'Only numeric characters (numbers) are valid for field'; 
				}else if(__lang__ == 'es'){
					strTxtError = 'Sólo caracteres numéricos (números) son permitidos para el campo'; 
				}else{
					strTxtError = 'Apenas caracteres numéricos(números) são permitidos para o campo';
				}
					
		        boxAlert(strTxtError + ' \"' + objValue.title + '\".', objValue);
				cBgColor(objValue);
		        return false;
		    };
		    if (eval(objValue.value) > eval(cmdvalue)) {
		        if (!strError || strError.length == 0) {
					if (__lang__ == 'en'){
			            strError = 'Field \"' + objValue.title + '\" must be smaller or equal than: ' + cmdvalue + '.';
					}else if(__lang__ == 'es'){
			            strError = 'El campo \"' + objValue.title + '\" debe ser menor o igual a: ' + cmdvalue + '.';
					}else{
			            strError = 'O campo \"' + objValue.title + '\" deve ser menor ou igual a: ' + cmdvalue + '.';
					}
		        };
		        boxAlert(strError, objValue);
				cBgColor(objValue);
		        return false;
		    };
		    break;
		};
		case "gt":
		case "greaterthan":{
		    if (isNaN(objValue.value)) {
				if (__lang__ == 'en'){
					strTxtError = 'Only numeric characters (numbers) are valid for field'; 
				}else if(__lang__ == 'es'){
					strTxtError = 'Sólo caracteres numéricos (números) son permitidos para el campo'; 
				}else{
					strTxtError = 'Apenas caracteres numéricos(números) são permitidos para o campo';
				}
		        boxAlert(strTxtError + ' \"' + objValue.title + '\".', objValue);
				cBgColor(objValue);
		        return false;
		    };
		    if (eval(objValue.value) < eval(cmdvalue)) {
		        if (!strError || strError.length == 0) {
					if (__lang__ == 'en'){
			            strError = 'Field \"' + objValue.title + '\" must be bigger or equal than: ' + cmdvalue + '.';
					}else if(__lang__ == 'es'){
			            strError = 'El campo \"' + objValue.title + '\" debe ser mayor o igual a: ' + cmdvalue + '.';
					}else{
			            strError = 'O campo \"' + objValue.title + '\" deve ser maior ou igual a: ' + cmdvalue + '.';
					}
		        };
		        boxAlert(strError, objValue);
				cBgColor(objValue);
		        return false;
		    };
		    break;
		};
		case 'regex': { 
			ret = TestRegExp(objValue,cmdvalue,strError);
			break; 
		};
		case 'dontselect': { 
			ret = TestDontSelect(objValue,cmdvalue,strError);
			break; 
		};
		case 'selmin':{
			ret = TestSelMin(objValue,cmdvalue,strError);
			break;
		};
		case 'selmax':{
			ret = TestSelMax(objValue,cmdvalue,strError);
			break;
		};
		case 'selone':{
			ret = TestSelectOneRadio(objValue,strError);
			break;
		};		 
		case 'equal': 
		case 'eq': 
		case 'notequal':
		case 'noteq':{
			return TestComparison(objValue,cmdvalue,command,strError);
			break;
		};
		case 'cpf': { 
			var tmpValue = replaceAll(objValue.value, [['.', ''],[ '/', '' ],['-', ''],[' ', '']]);
			ret = TestCpf(tmpValue);
			if(!ret) {
					if (__lang__ == 'en'){
			            strErrorTxt = 'is not a valid CPF number for field';
					}else if(__lang__ == 'es'){
			            strErrorTxt = 'no es un número de CPF válido para el campo';
					}else{
			            strErrorTxt = 'não é um número de CPF válido para o campo';
					}
						
				boxAlert('\"' + objValue.value + '\" ' + strErrorTxt + ' \"' + objValue.title + '\".', objValue);
				cBgColor(objValue);
			}
			else rBgColor(objValue);
			delete tmpValue;
			break;             
		};
		case 'cnpj': { 
			var tmpValue = replaceAll(objValue.value, [['.', ''],[ '/', '' ],['-', ''],[' ', '']]);
			ret = TestCnpj(tmpValue);
			if(!ret) {
				if (__lang__ == 'en'){
		            strErrorTxt = 'is not a valid CNPJ number for field';
				}else if(__lang__ == 'es'){
		            strErrorTxt = 'no es un número de CNPJ válido para el campo';
				}else{
		            strErrorTxt = 'não é um número de CNPJ válido para o campo';
				}
						
				boxAlert('\"' + objValue.value + '\" ' + strErrorTxt + ' \"' + objValue.title + '\".', objValue);
				cBgColor(objValue);
			}
			else rBgColor(objValue);
			delete tmpValue;
			break;             
		};
		case 'file': { 
			ret = TestFile(objValue.value.toLowerCase(), cmdvalue);
			if(!ret) {
				if (__lang__ == 'en'){
					boxAlert('Field \"' + objValue.title + '\" allows only \".' + cmdvalue +' \"archives.', objValue);
				}else if(__lang__ == 'es'){
					boxAlert('El campo \"' + objValue.title + '\" permite sólo archivos con la extensión \".' + cmdvalue +' \".', objValue);
				}else{
					boxAlert('O campo \"' + objValue.title + '\" permite somente arquivos com a extensão \".' + cmdvalue +' \".', objValue);
				}
				cBgColor(objValue);
			}
			else rBgColor(objValue);
			delete tmpValue;
			break;             
		};
	};
	if(ret) rBgColor(objValue);
	return ret; 
};

function cBgColor(o){o.style.backgroundColor = valColorError;};

function rBgColor(o){o.style.backgroundColor = "";};

function rABgColor(){
	for (var itr = 0; itr < this.elements.length; itr++) {
		rBgColor(this.elements[itr]);
	};
};

function only(t,o,e) {
	if(window.event)key=window.event.keyCode;
	else if(e)key=e.which;
	else return true;
	S=(o)?o:'';
	if(t=='num'||t=='number'||t=='numeric')S+='0123456789';
	if(t=='alnum'||t=='alphanumeric')S+='abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZáàãâäéèêëíìîïóòõôöúùûüçÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÔÖÚÙÛÜÇ 0123456789';
	if(t=='let'||t=='letters')S+='abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZáàãâäéèêëíìîïóòõôöúùûüçÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÔÖÚÙÛÜÇ ';	
	if(t=='rest'||t=='restrict')S+='abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
	if(t=='email')S+='abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@-_.';
	if(key==null||key==0||key==8||key==9||key==13||key==27)return true;
	else if(S.indexOf(String.fromCharCode(key))!=-1)return true;
	else return false;
};

function not(S,e) {
	if(window.event)key=window.event.keyCode;
	else if(e)key=e.which;
	else return true;
	if(!S)return false;
	else if(key==null||key==0||key==8||key==9||key==13||key==27)return true;
	else if(S.indexOf(String.fromCharCode(key))!=-1)return false;
	else return true;
};


function jump(o,e) {
	if(window.event)key=window.event.keyCode;
	else if(e)key=e.which;
	else return;
	if (key==9||key==2||key==16) return;
	if(o.value.length==o.maxLength){
  		for(var i=0;i<o.form.length;i++){
	    	if(o.form[i]==o&&o.form[i+1]){
				if(o.form[i+1]) o.form[i+1].focus();
	    		break;
	    	};
		}
	};
};

function checkReset(f){
	if (__lang__ == 'en'){
		strErrorTxt = 'All data submitted will be deleted.\nTo delete all data click \"OK\".\nTo continue submitting information click \"Cancel\".';
	}else if(__lang__ == 'es'){
		strErrorTxt = 'Todos los datos registrados serán borrados.\nPara borrar todos los datos haga clic en \"OK\".\nPara continuar registrando haga clic en \"Cancelar\".';
	}else{
		strErrorTxt = 'Todos os dados preenchidos serão apagados.\nPara apagar todos os dados clique em \"OK\".\nPara continuar preenchendo clique em \"Cancelar\".';
	}
	if(confirm(strErrorTxt)) gElm(f).reset();
};

function clearMe(w,s){
	(w.value == s) ? w.value='' : 0;
};

function leaveMe(w,s){
	(w.value.length == 0) ? w.value = s : 0;
};

function boxAlert(x, f){
	if(__cAlert__) {
		if(__cAlertShow__){
			openBox(__cAlertDiv__);
		}  else {
			sDiv(__cAlertDiv__);
			f.focus();
		}
		gElm(__cAlertTxt__).innerHTML = x;
		__oldFocus__ = f;

	} else {
		alert(x); 
	}
};

function oldFocus(){
	__oldFocus__.focus();
};