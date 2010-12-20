﻿var OSData={onComplete:null,verbose:false,version:'0.9.2.1',require:'opensocial-0.7, views, flash, setprefs',user:{owner:{id:'',uid:null,name:null,url:null,img:null,friends:{FIRST:0,MAX:1000,ORDER:'top_friends',FILTER:'all',list:[]}},viewer:{id:null,uid:null,name:null,url:null,img:null},isOwner:function(){return(OSData.user.owner.id!=null)?(OSData.user.owner.id==OSData.user.viewer.id)?true:false:false},simplify:function(){var o={};o.owner=OSData._0(OSData.user.owner);var a={list:[]};for(var i=0;i<OSData.user.owner.friends.list.length;i++){a.list.push(OSData._0(OSData.user.owner.friends.list[i]))}o.owner.friends=a;a=null;return o}},app:{name:'Your APP name',id:null,view:null,domain:null,canvasUrl:null,proxyUrl:null,cookie:null,dataToRequest:{OWNER:true,VIEWER:true,OSCOOKIE:true,FRIENDS:false},isInstalled:function(){return(OSData.user.owner.id!=null&&OSData.user.viewer.id==null)?true:false},ajax:function(a,b,c){gadgets.io.makeRequest(a,b,c)},setCookie:function(b){if(b.slice(0,1)!='{'||b.slice(-1)!='}')throw new Error('Cookie value (string) MUST be a stringfied object. {test:123}.');var c=escape(b);var d=null;var e=this;if(OSData.user.isOwner()){d=opensocial.newDataRequest();d.add(d.newUpdatePersonAppDataRequest(opensocial.DataRequest.PersonId.VIEWER,'OSCookie',c),'data');d.send(function(a){if(a.get('data')){if(!a.get('data').hadError()){OSData.app.cookie=eval('('+unescape(a.get('data').getOriginalDataRequest().parameters.value)+')');}else{OSData._1('ERROR: '+a.get('data').getErrorMessage());}}});}},getCookie:function(){return OSData.app.cookie;},activity:{onComplete:function(){},setDefaultActivityMessages:function(a){var b=OSData;b.app.activity.message=a;},message:{},send:function(a){var b=this;var c={};if(!a.title){return;}c[opensocial.Activity.Field.TITLE]=a.title;if(a.text){c[opensocial.Activity.Field.BODY]=a.text;}var d=opensocial.newActivity(c);opensocial.requestCreateActivity(d,opensocial.CreateActivityPriority.LOW,b.onComplete);c=null;d=null;}}},init:function(){var b=OSData;var c=opensocial.newDataRequest();var d={};d[opensocial.DataRequest.PeopleRequestFields.PROFILE_DETAILS]=[opensocial.Person.Field.PROFILE_URL];if(b.app.dataToRequest.OWNER)c.add(c.newFetchPersonRequest('OWNER',d),'owner');if(b.app.dataToRequest.VIEWER)c.add(c.newFetchPersonRequest('VIEWER',d),'viewer');if(b.app.dataToRequest.OSCOOKIE)c.add(c.newFetchPersonAppDataRequest('OWNER','OSCookie'),'cookie');if(b.app.dataToRequest.FRIENDS){var e={};e[opensocial.DataRequest.PeopleRequestFields.MAX]=b.user.owner.friends.MAX;e[opensocial.DataRequest.PeopleRequestFields.FIRST]=b.user.owner.friends.FIRST;e[opensocial.DataRequest.PeopleRequestFields.SORT_ORDER]=(b.user.owner.friends.ORDER=='name')?opensocial.DataRequest.SortOrder.NAME:opensocial.DataRequest.SortOrder.TOP_FRIENDS;e[opensocial.DataRequest.PeopleRequestFields.FILTER]=(b.user.owner.friends.FILTER=='has_app')?opensocial.DataRequest.FilterType.HAS_APP:opensocial.DataRequest.FilterType.ALL;e[opensocial.DataRequest.PeopleRequestFields.PROFILE_DETAILS]=[opensocial.Person.Field.PROFILE_URL];c.add(c.newFetchPeopleRequest('OWNER_FRIENDS',e),'friends');}c.send(function(a){b._2(a);});},_2:function(a){var b=this;if(typeof(a.get)!='function'){return false;}var c=null;var d=null;var f=null;var g=null;if(b.app.dataToRequest.OWNER){c=a.get('owner').hadError()?null:a.get('owner').getData();if(c!=null)b._3(c,b.user.owner);}if(b.app.dataToRequest.VIEWER){d=a.get('viewer').hadError()?null:a.get('viewer').getData();if(d!=null)b._3(d,b.user.viewer);}if(b.app.dataToRequest.OSCOOKIE){f=a.get('cookie').hadError()?null:a.get('cookie').getData();var h;try{h=f[b.user.owner.id].OSCookie;try{if(typeof(h)=='string'){b.app.cookie=eval('('+unescape(h)+')');}else if(typeof(h)=='object'){b.app.cookie=h;}else{b.app.cookie=null;}}catch(e){b.app.cookie=null;}}catch(Er){b.app.cookie=null;}h=null;}if(b.app.dataToRequest.FRIENDS){g=a.get('friends').hadError()?null:a.get('friends').getData();var j=[];if(g!=null)j=g.asArray();for(var i=0;i<j.length;i++){b.user.owner.friends.list.push(b._3(j[i]));}}var k=gadgets.util.getUrlParameters();b.app.domain=k['parent'];b.app.id=(b.app.id==null)?k['gadgetId']:b.app.id;b.app.view=gadgets.views.getCurrentView().getName();b.app.canvasUrl=b.app.domain+'/Application.aspx?appId='+b.app.id+'&uid='+b.user.owner.uid;b.app.proxyUrl=gadgets.io.getProxyUrl('');if(b.verbose){b._1(c);b._1('owner.id = '+b.user.owner.id);b._1('owner.uid = '+b.user.owner.uid);b._1('owner.name = '+b.user.owner.name);b._1('owner.url = '+b.user.owner.url);b._1('owner.img = '+b.user.owner.img);b._1('owner.friends.list:');b._1(b.user.owner.friends);b._1(b.user.owner.friends.list);b._1(d);b._1('viewer.id = '+b.user.viewer.id);b._1('viewer.uid = '+b.user.viewer.uid);b._1('viewer.name = '+b.user.viewer.name);b._1('viewer.url = '+b.user.viewer.url);b._1('viewer.img = '+b.user.viewer.img);b._1('app.id = '+b.app.id);b._1('app.view = '+b.app.view);b._1('app.canvasUrl = '+b.app.canvasUrl);b._1('app.proxyUrl = '+b.app.proxyUrl);}if(b.onComplete!=null)b.onComplete();b=c=d=f=g=j=null;},_3:function(a,b){var c=this;var d;if(!b)d={};else d=b;d.id=a.getField(opensocial.Person.Field.ID);d.uid=c._4(a.getField(opensocial.Person.Field.PROFILE_URL));d.name=a.getDisplayName();d.url=a.getField(opensocial.Person.Field.PROFILE_URL);d.img=a.getField(opensocial.Person.Field.THUMBNAIL_URL);try{if(a.getField(opensocial.Person.Field.GENDER)!=null){d.gender=a.getField(opensocial.Person.Field.GENDER)['key'];}}catch(e){d.gender=a.getField(opensocial.Person.Field.GENDER);}return d;},_0:function(a){var o={};o.uid=a.uid;o.n=a.name;o.i=a.img.replace(/http:\/\/img..orkut.com\/images\/small\//,'');return o;},_4:function(a){try{var b=a.match(/uid=([^&#]+)/);return(b.length>1)?b[1]:null;}catch(e){return false;}},_1:function(a){this.customFunction=function(){};if(window['console']){console.info(a)}else{this.customFunction(a)}}};