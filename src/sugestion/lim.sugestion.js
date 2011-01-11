;(function(namespace, $){
	
	var Sugestion = {
		
		Class :function( options ){

			//Private
			var pattern =  /\{(\w*)\}/g;
			var template = options.template;
			var timer = null;
			var self = this;

			//Public
			this.timeout = options.timeout || 500;
			this.query = '';
			
			this.inject = function(json){
				var html = template.replace(pattern, function(key){
					return json[key.slice(1, -1)];
				})
				return html;
			}
			
			this.get = function(options){
				$.getJSON( options.url, options.params, function(data){ options.callback(data); });
			}

			this.request = function(value){
				clearTimeout( timer );
				var self = this;
				timer = setTimeout(function(){
					options.params[options.change] = self.query = value;
					self.get(options);
				}, this.timeout);
			}

			this.onkeyup = function(e){ 
					if( e.keyCode < 47 && e.keyCode != 8 )
						return true;
					if(this.value)
						self.request( this.value ); 
			}

			//Constructor
			;(function(){
				$(options.target).keyup( this.onkeyup );
			}).call(this);
		}
	}

	namespace.Sugestion = Sugestion.Class;

})(lim, jQuery)