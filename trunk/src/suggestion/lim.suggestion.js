/**
 * @class Suggestion
 * @author Eduardo Ottaviani
 * @usage 
	var suggestion = new lim.Suggestion({
		target	:'#my-input-text',
		list	:'#list',
		
		url		: 'http://api.flickr.com/services/feeds/photos_public.gne?jsoncallback=?',
		params	:{
			tags: "cat",
			tagmode: "any",
			format: "json"
		},
		
		change	:'tags',
		template:'<li><img src="{image}" /><a href="{link}">{title}</a></li>',
		array :'items',
		
		merge	:{
			image :'media.m',
			link  :'link',
			title :'title'
		}
	});
	
	Events:
		//On data loaded
		 $(suggestion).bind('load', function(e, data){
			console.log(data);
		})
*/

;(function(namespace, $){
	
	var Mixin = {
		
		to_object :function(s, o){
			var o = Mixin.dup(o);
			var split = s.split(/\./);
				if( split.length > 1 )
					for(var i = 0; i < split.length; i++){
						o = o[split[i]];
					}
				else
					o = o[split[0]];
				
			return o;
		},
		
		dup	:function(o){
			var f = function(){};
				f.prototype = o;
			return new f;
		}
		
	}

	var Suggestion = {
		
		Class :function( options ){

			//Private
			var 
				pattern =  /\{(\w*)\}/g,
				template = options.template,
				timer = null,
				self = this,
				to_object = Mixin.to_object,
				create	= Mixin.dup,
				list = $(options.list);

			//Public
			this.timeout = options.timeout || 500;
			this.query = '';
			
			this.inject = function(json){
				var html = template.replace(pattern, function(key){
					return json[key.slice(1, -1)];
				})
				return html;
			}
			
			this.get = function(){
				$.getJSON( options.url, options.params, function(data){
					self.callback.call( self, data );
				});
			}

			this.request = function(value){
				clearTimeout( timer );
				var self = this;
				timer = setTimeout(function(){
					options.params[options.change] = self.query = value;
					self.get();
				}, this.timeout);
			}

			this.onkeyup = function(e){ 
					if( e.keyCode < 47 && e.keyCode != 8 )
						return true;
					if(this.value)
						self.request( this.value ); 
			}
			
			this.callback = options.callback || function(data){

				$(this).trigger('load', data)

				var 
					merge = options.merge,
					array = to_object( options.array, data ),
					html = '',
					o = {};
					
						for(var i = 0; i < array.length; i++){
								for( var key in merge ){
									o[key] = to_object( merge[key], array[i] ); 
								}
							html += this.inject( o );
						}
				
				list.empty().append( html );
				
			}

			//Constructor
			;(function(){
				$(options.target).keyup( this.onkeyup );
			}).call(this);
		}
	}

	namespace.Suggestion = Suggestion.Class;

})(lim, jQuery)