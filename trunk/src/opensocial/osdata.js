/**
	Create a static "OSData" (Open Social Data) Class
	
	@author Nicholas Almeida nicholasalmeida.com
	@author Marcelo Carneiro 
	@since 18:43 29/7/2008
	@usage
			// IMPORTANT
			
			// Requires: opensocial-0.7, views, flash, setprefs
			
			OSData.verbose = true;
			OSData.onComplete = function(){
				alert('OSData loaded!');
			}
			
			// IMPORTANT: 
			// --------------------------------------------
			// OSData.init() *MUST* be the last OSData call!
			// --------------------------------------------
			OSData.init(); 
			
			
*/
var OSData = {
	onComplete: null,
	verbose: false,
	
	version: '0.9',
	require: 'opensocial-0.7, views, flash, setprefs',
	user: {
		owner: {
			//data: null, // All user data as the OpenSocial sent.
			id: '',
			uid: null,
			name: null,
			url: null,
			img: null,
			
			friends: {
				FIRST: 0,
				MAX: 1000,
				ORDER: 'top_friends', // "top_friends" or ("name" unsuported)
				FILTER: 'all', // "all" or ("has_app" not working)
				list: []
			}
		},
		viewer: {
			//data: null, // All user data as the OpenSocial sent.
			id: null,
			uid: null,
			name: null,
			url: null,
			img: null
		},
		
		isOwner: function(){
			return (OSData.user.owner.id != null) ? (OSData.user.owner.id == OSData.user.viewer.id) ? true : false : false;
		},
		
		/**
		 * Returns the very basic owner user data.
		 * sample:
			OSData.user.simplify();
		*/
		simplify: function(){
			var o = {};
				o.owner = OSData._simplifyPerson(OSData.user.owner);
				
			var friends = {list:[]};
			for(var i=0; i<OSData.user.owner.friends.list.length; i++){
				friends.list.push(OSData._simplifyPerson(OSData.user.owner.friends.list[i]));
			}
			o.owner.friends = friends;
			friends = null;
			return o;
		}
	},
	app: {
		name: 'Your APP name',
		id: null,
		view: null,
		domain: null,
		canvasUrl: null,
		proxyUrl: null,
		cookie: null,
		dataToRequest: {
			OWNER: true,
			VIEWER: true,
			OSCOOKIE: true,
			FRIENDS: false
		},
		isInstalled: function(){
			return (OSData.user.owner.id != null && OSData.user.viewer.id == null) ? true : false;
		},
		
		/**
		 * params MUST follow the doc: http://code.google.com/apis/opensocial/docs/0.7/reference/gadgets.io.RequestParameters.html#METHOD
		 * sample:
		  		var postdata = {nome:"nicholas"};
						postdata = gadgets.io.encodeValues(postdata);
					var params = {};
						params[gadgets.io.RequestParameters.METHOD] = gadgets.io.MethodType.POST; 
						params[gadgets.io.RequestParameters.CONTENT_TYPE] = gadgets.io.ContentType.TEXT;
						params[gadgets.io.RequestParameters.POST_DATA] = gadgets.io.encodeValues(postdata);

					OSData.app.ajax("http://YOUR_SERVER/", onComplete, params);
		*/
		ajax: function(url, callback, params){
			gadgets.io.makeRequest(url, callback, params);
		},
		
		/**
		 * sample: OSData.app.setCookie('{variable:"value"}');
		*/
		setCookie: function(str){
			if(str.slice(0,1) != '{' || str.slice(-1) != '}') throw new Error('Cookie value (string) MUST be a stringfied object. {test:123}.');
			var encodedValue = escape(str);
			var req = null;
			var _this = this;
			if(OSData.user.isOwner()){
				req = opensocial.newDataRequest();
				req.add(req.newUpdatePersonAppDataRequest(opensocial.DataRequest.PersonId.VIEWER,'OSCookie',encodedValue),'data');
				req.send( function($data) {
					if($data.get('data')){
						if(!$data.get('data').hadError()){
							OSData.app.cookie = eval('(' + unescape($data.get('data').getOriginalDataRequest().parameters.value) + ')');
						}else{
							OSData._trace('ERROR: ' + $data.get('data').getErrorMessage());
						}
					}
				});
			}
		},
		
		getCookie: function(){
			return OSData.app.cookie;
		},
		
		activity: {
			onComplete: function(){},
			
			/* Default messages
				@usage
				
				OSData.app.activity.setDefaultActivityMessages({
						INSTALL: {
							title: ' adicionou o aplicativo ',
							text: ''
						},
						OTHER: {
							title: ' other title ',
							text: 'other body text'
						}
					}
				);
			*/
			setDefaultActivityMessages: function($messagesObj){
				var _this = OSData;
				_this.app.activity.message = $messagesObj;
			},
			
			message: {},
			
			send: function($messageObj){
				var _this = this;
				var params = {};  
					if(!$messageObj.title){
						return;
					}
					params[opensocial.Activity.Field.TITLE] = $messageObj.title;
					if($messageObj.text){
						params[opensocial.Activity.Field.BODY] = $messageObj.text;
					}
				var activity = opensocial.newActivity(params);
				opensocial.requestCreateActivity(activity, opensocial.CreateActivityPriority.LOW, _this.onComplete);
				params = null;
				activity = null;
			}
		}
	},
	
	init: function(){
		var _this = OSData;
		
		var dataReq = opensocial.newDataRequest();

        var params = {};
        //params[opensocial.DataRequest.PeopleRequestFields.PROFILE_DETAILS] = [opensocial.Person.Field.PROFILE_URL,opensocial.Person.Field.GENDER]; 
        params[opensocial.DataRequest.PeopleRequestFields.PROFILE_DETAILS] = [opensocial.Person.Field.PROFILE_URL]; 
		
		if(_this.app.dataToRequest.OWNER) dataReq.add(dataReq.newFetchPersonRequest('OWNER',params),'owner');
		if(_this.app.dataToRequest.VIEWER) dataReq.add(dataReq.newFetchPersonRequest('VIEWER',params),'viewer');
		if(_this.app.dataToRequest.OSCOOKIE) dataReq.add(dataReq.newFetchPersonAppDataRequest('OWNER', 'OSCookie'), 'cookie');

		if(_this.app.dataToRequest.FRIENDS){
			var filterOwnerFriends = {};
				filterOwnerFriends[opensocial.DataRequest.PeopleRequestFields.MAX] = _this.user.owner.friends.MAX;
				filterOwnerFriends[opensocial.DataRequest.PeopleRequestFields.FIRST] = _this.user.owner.friends.FIRST;
				filterOwnerFriends[opensocial.DataRequest.PeopleRequestFields.SORT_ORDER] = (_this.user.owner.friends.ORDER == 'name') ? opensocial.DataRequest.SortOrder.NAME : opensocial.DataRequest.SortOrder.TOP_FRIENDS;
				filterOwnerFriends[opensocial.DataRequest.PeopleRequestFields.FILTER] = (_this.user.owner.friends.FILTER == 'has_app') ? opensocial.DataRequest.FilterType.HAS_APP : opensocial.DataRequest.FilterType.ALL;
				//filterOwnerFriends[opensocial.DataRequest.PeopleRequestFields.PROFILE_DETAILS] = [opensocial.Person.Field.PROFILE_URL,opensocial.Person.Field.GENDER];
				filterOwnerFriends[opensocial.DataRequest.PeopleRequestFields.PROFILE_DETAILS] = [opensocial.Person.Field.PROFILE_URL];
			dataReq.add(dataReq.newFetchPeopleRequest('OWNER_FRIENDS', filterOwnerFriends),'friends');
		}
		
		dataReq.send(function($data){
			_this._onReceiveData($data);
		});
	},
	
	_onReceiveData: function($data){
		var _this = this;
		if(typeof($data.get) != 'function'){
			return false;
		}
		
		var owner = null;
		var viewer = null;
		var cookie = null;
		var ownerFriends = null;
		
		if(_this.app.dataToRequest.OWNER) {
			owner = $data.get('owner').hadError() ? null : $data.get('owner').getData();
			if(owner != null) _this._createPerson(owner, _this.user.owner);
		}
		
		if(_this.app.dataToRequest.VIEWER) {
			viewer = $data.get('viewer').hadError() ? null : $data.get('viewer').getData();
			if(viewer != null) _this._createPerson(viewer, _this.user.viewer);
		}
		
		if(_this.app.dataToRequest.OSCOOKIE) {
			cookie = $data.get('cookie').hadError() ? null : $data.get('cookie').getData();
			var tmpCookie;
			try {
				tmpCookie = cookie[_this.user.owner.id].OSCookie;
				try {
					if(typeof(tmpCookie) == 'string') {
						_this.app.cookie = eval('(' + unescape(tmpCookie) + ')');
					} else if(typeof(tmpCookie) == 'object') {
						_this.app.cookie = tmpCookie;
					} else {
						_this.app.cookie = null;
					}
				} catch(e){
					_this.app.cookie = null;
				}
			} catch(Er) {
				_this.app.cookie = null;
			}
			tmpCookie = null;
		}
		
		if(_this.app.dataToRequest.FRIENDS){
			ownerFriends = $data.get('friends').hadError() ? null : $data.get('friends').getData();
			var ownerFriendsArr = [];
			if(ownerFriends != null) ownerFriendsArr = ownerFriends.asArray();
			for(var i = 0; i< ownerFriendsArr.length; i++) {
				_this.user.owner.friends.list.push(_this._createPerson(ownerFriendsArr[i]));
			}
		}
		
		// app
		var urlParameters = gadgets.util.getUrlParameters();
		_this.app.domain 			= urlParameters['parent'];
		_this.app.id 				= (_this.app.id == null) ? urlParameters['gadgetId'] : _this.app.id;
		_this.app.view 				= gadgets.views.getCurrentView().getName();
		_this.app.canvasUrl 		= _this.app.domain + '/Application.aspx?appId=' + _this.app.id + '&uid=' + _this.user.owner.uid;
		_this.app.proxyUrl 			= gadgets.io.getProxyUrl('');
		
		if(_this.verbose){
			_this._trace(owner);
			_this._trace('owner.id = ' + _this.user.owner.id);
			_this._trace('owner.uid = ' + _this.user.owner.uid);
			_this._trace('owner.name = ' + _this.user.owner.name);
			_this._trace('owner.url = ' + _this.user.owner.url);
			_this._trace('owner.img = ' + _this.user.owner.img);
			_this._trace('owner.friends.list:');
			_this._trace(_this.user.owner.friends);
			_this._trace(_this.user.owner.friends.list);
			
			_this._trace(viewer);
			_this._trace('viewer.id = ' + _this.user.viewer.id);
			_this._trace('viewer.uid = ' + _this.user.viewer.uid);
			_this._trace('viewer.name = ' + _this.user.viewer.name);
			_this._trace('viewer.url = ' + _this.user.viewer.url);
			_this._trace('viewer.img = ' + _this.user.viewer.img);
			
			_this._trace('app.id = ' + _this.app.id);
			_this._trace('app.view = ' + _this.app.view);
			_this._trace('app.canvasUrl = ' + _this.app.canvasUrl);
			_this._trace('app.proxyUrl = ' + _this.app.proxyUrl);
		}

		if(_this.onComplete != null) _this.onComplete();
		
		// clear local vars
		_this = 
		owner =
		viewer = 
		cookie = 
		ownerFriends = 
		ownerFriendsArr = null;
		
	},
	
	_createPerson: function($dataObj, $personObj){
		var _this = this;
		var personObj;
		
		if(!$personObj) personObj = {};
		else personObj = $personObj;
		
		//personObj.data 	= $dataObj; // All user data as the OpenSocial sent.
		personObj.id   	= $dataObj.getField(opensocial.Person.Field.ID);
		personObj.uid 	= _this._generateUID($dataObj.getField(opensocial.Person.Field.PROFILE_URL));
		personObj.name 	= $dataObj.getField(opensocial.Person.Field.NAME).getField(opensocial.Name.Field.UNSTRUCTURED);
		personObj.url 	= $dataObj.getField(opensocial.Person.Field.PROFILE_URL);
		personObj.img 	= $dataObj.getField(opensocial.Person.Field.THUMBNAIL_URL);
		try {
			if($dataObj.getField(opensocial.Person.Field.GENDER) != null) {
				personObj.gender= $dataObj.getField(opensocial.Person.Field.GENDER)['key'];
			}
		} catch(e){
			personObj.gender= $dataObj.getField(opensocial.Person.Field.GENDER);
		}
		
		return personObj;
	},
	
	_simplifyPerson: function(obj){
		var o = {};
			o.uid = obj.uid;
			o.n = obj.name;
			o.i = obj.img.replace(/http:\/\/img..orkut.com\/images\/small\//, '');
		return o;
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
		this.customFunction = function(){};
		if(window['console']){
			console.info($m);
		} else {
			this.customFunction($m);
		}
	}
	
};
