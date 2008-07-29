/**
	Create a static "OSData" (Open Social Data) Class
	
	@author Nicholas Almeida nicholasalmeida.com
	@author Marcelo Carneiro 
	@since 18:43 29/7/2008
	@usage
			// IMPORTANT
			
			// Requires: opensocial-0.7, views, flash, setprefs
			
			OSData.init();
			OSData.verbose = true;
			OSData.onComplete = function(){
				alert('OSData loaded!');
			}
*/
var OSData = {
	onComplete: null,
	verbose: false,
	
	version: '0.1',
	require: 'opensocial-0.7, views, flash, setprefs',
	user: {
		viewer: {
			data: null,
			id: null,
			uid: null,
			name: null,
			url: null,
			img: null
		},
		owner: {
			data: null,
			id: null,
			uid: null,
			name: null,
			url: null,
			img: null
		}
	},
	app: {
		id: null,
		view: null,
		canvasUrl: null,
		cookie: null,
		isInstalled: function(){
			return (OSData.user.owner.id != null && OSData.user.viewer.id == null) ? true : false;
		},
		
		setCookie: function(str){
			var encodedValue = escape(str);
			var req = null;
			var _this = this;
			if(OSData.isOwner()){
				req = opensocial.newDataRequest();
				req.add(req.newUpdatePersonAppDataRequest(opensocial.DataRequest.PersonId.VIEWER,'OSCookie',encodedValue),'data');
				req.send( function($data) {
					if($data.get('data')){
						if(!$data.get('data').hadError()){
							OSData.app.cookie = eval('(' + unescape($data.get('data').getOriginalDataRequest().parameters.value) + ')');
						}else{
							_this._trace("ERRO: " + $data.get('data').getErrorMessage());
						}
					}
				});
			}
		},
		getCookie: function(){
			return OSData.app.cookie;
		}
	},
	
	init: function(){
		var _this = this;
		var dataReq = opensocial.newDataRequest();

        var params = {};
        params[opensocial.DataRequest.PeopleRequestFields.PROFILE_DETAILS] = [opensocial.Person.Field.PROFILE_URL]; 

		dataReq.add(dataReq.newFetchPersonRequest('VIEWER',params),'viewer');
		dataReq.add(dataReq.newFetchPersonRequest('OWNER',params),'owner');
		dataReq.add(dataReq.newFetchPersonAppDataRequest('OWNER', 'OSCookie'), 'cookie');

		dataReq.send(function($data){
			_this._onReceiveData($data);
		});
	},
	
	isOwner: function(){
		return (OSData.user.owner.id != null) ? (OSData.user.owner.id == OSData.user.viewer.id) ? true : false : false;
	},
	
	_onReceiveData: function($data){
		//console.info($data);
		var _this = this;
		if(typeof($data.get) != 'function'){
			return false;
		}
		
		var owner = $data.get('owner').hadError() ? null : $data.get('owner').getData();
		var viewer = $data.get('viewer').hadError() ? null : $data.get('viewer').getData();
		var cookie = $data.get('cookie').hadError() ? null : $data.get('cookie').getData();
		
		// owner
		if(owner != null) {
			_this.user.owner.data 		= owner;
			_this.user.owner.id 		= owner.getField(opensocial.Person.Field.ID);
			_this.user.owner.uid 		= _this._generateUID(owner.getField(opensocial.Person.Field.PROFILE_URL));
			_this.user.owner.name 		= owner.getField(opensocial.Person.Field.NAME).getField(opensocial.Name.Field.UNSTRUCTURED);
			_this.user.owner.url 		= owner.getField(opensocial.Person.Field.PROFILE_URL);
			_this.user.owner.img 		= owner.getField(opensocial.Person.Field.THUMBNAIL_URL);
		}
		
		// viewer
		if(viewer != null) {
			_this.user.viewer.data 		= viewer;
			_this.user.viewer.id 		= viewer.getField(opensocial.Person.Field.ID);
			_this.user.viewer.uid 		= _this._generateUID(viewer.getField(opensocial.Person.Field.PROFILE_URL));
			_this.user.viewer.name 		= viewer.getField(opensocial.Person.Field.NAME).getField(opensocial.Name.Field.UNSTRUCTURED);
			_this.user.viewer.url 		= viewer.getField(opensocial.Person.Field.PROFILE_URL);
			_this.user.viewer.img 		= viewer.getField(opensocial.Person.Field.THUMBNAIL_URL);
		}
		
		// OSCookie
		try {
			_this.app.cookie = eval('(' + unescape(cookie[_this.user.owner.id].OSCookie) + ')');
		} catch(e){
			_this._trace(e);
			_this.app.cookie = null;
		}
		
		// app
		_this.app.id 				= gadgets.util.getUrlParameters()['gadgetId'];
		_this.app.view 				= gadgets.views.getCurrentView().getName();
		_this.app.canvasUrl 		= '/Application.aspx?appId=' + _this.app.id + '&uid=' + _this.user.owner.uid;
		
		if(_this.verbose){
			_this._trace(owner);
			_this._trace('owner.id = ' + _this.user.owner.id);
			_this._trace('owner.uid = ' + _this.user.owner.uid);
			_this._trace('owner.name = ' + _this.user.owner.name);
			_this._trace('owner.url = ' + _this.user.owner.url);
			_this._trace('owner.img = ' + _this.user.owner.img);
			
			_this._trace(viewer);
			_this._trace('viewer.id = ' + _this.user.viewer.id);
			_this._trace('viewer.uid = ' + _this.user.viewer.uid);
			_this._trace('viewer.name = ' + _this.user.viewer.name);
			_this._trace('viewer.url = ' + _this.user.viewer.url);
			_this._trace('viewer.img = ' + _this.user.viewer.img);
			
			_this._trace('app.id = ' + _this.app.id);
			_this._trace('app.view = ' + _this.app.view);
			_this._trace('app.canvasUrl = ' + _this.app.canvasUrl);
		}
		
		if(_this.onComplete != null) _this.onComplete();
	},
	
	_generateUID: function($data){
		try{
			var result = $data.match(/uid=([^&#]+)/);
			return (result.length > 1) ? result[1] : null;
		}catch(e){
			return false;
		}
	},
	
	_trace: function($m){
		this.customFunction = alert;
		if(window['console']){
			console.info($m);
		} else {
			this.customFunction($m);
		}
	}
	
};