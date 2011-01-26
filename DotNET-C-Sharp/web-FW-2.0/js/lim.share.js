if(!window['lim']){
	var lim = {};
}

(function(scope){
	
	if(!scope){
		scope = window;
	}
	
	
	/**
	 * lazy link for the "share button" in social networks
	 * @author Marcelo Miranda Carneiro - mcarneiro@gmail.com
	 * @usage <code>
		// Example with all possible options

		// SHARE TO TWITTER
		lim.share.run('twitter', {url: 'http://www.site.com/', msg: 'message here'});
		
		// SHARE TO FACEBOOK
		// (IMPORTANT! Facebook users title and description metatags for texts and link rel="image_src"
		// for thumbs, otherwise, it uses the first imagem of the site)
		lim.share.run('facebook', {url: 'http://www.site.com/', title: 'title here'});
		
		// SHARE TO ORKUT
		lim.share.run('orkut', {url: 'http://www.site.com/', title: 'title here', msg: 'comment here'});
		
	 * </code>
	 */
	scope.share = {
		_built:false,
		_types: [],
		_callback: null,
		address: {
			facebook	: 'http://www.facebook.com/sharer.php?u={url}&t={title}',
			twitter		: 'http://twitter.com/share/?url={url}&text={msg}',
			orkut		: 'http://promote.orkut.com/preview?nt=orkut.com&tt={title}&du={url}&cn={msg}&tn={thumb}',
			delicious	: 'http://www.delicious.com/save?url={url}&title={title}&notes={msg}',
			buzz		: 'http://www.google.com/buzz/post?message={msg}&url={url}&imageurl={thumb}'
		},
		rules: {
			buzz: function(p_address, p_data){
				switch(true){
					case (!p_data):
						throw new Error('[ERROR] lim.share buzz: data object is required.');
						return false;
				}
				return true;
			},
			facebook: function(p_address, p_data){
				switch(true){
					case (!p_data):
						throw new Error('[ERROR] lim.share facebook: data object is required.');
						return false;
					case (!p_data.url):
						throw new Error('[ERROR] lim.share facebook: link URL (data.url) is required.');
						return false;
				}
				return true;
			},
			orkut: function(p_address, p_data){
				switch(true){
					case (!p_data):
						throw new Error('[ERROR] lim.share orkut: data object is required.');
						return false;
					case (!p_data.url):
						throw new Error('[ERROR] lim.share orkut: link URL (data.url) is required.');
						return false;
					case (!p_data.title):
						throw new Error('[ERROR] lim.share orkut: title (data.title) is required.');
						return false;
				}
				return true;
			},
			twitter: function(p_address, p_data){
				switch(true){
					case (!p_data):
						throw new Error('[ERROR] lim.share twitter: data object is required.');
						return false;
					case (!p_data.url):
						throw new Error('[ERROR] lim.share twitter: status (data.url) is required.');
						return false;
				}
				return true;
			},
			delicious: function(p_address, p_data){
				switch(true){
					case (!p_data):
						throw new Error('[ERROR] lim.share delicious: data object is required.');
						return false;
					case (!p_data.url):
						throw new Error('[ERROR] lim.share delicious: link URL (data.url) is required.');
						return false;
					case (!p_data.title):
						throw new Error('[ERROR] lim.share delicious: title (data.title) is required.');
						return false;
				}
				return true;
			}
		},
		_validateType: function(p_type){
			for(var n in this.address){
				if(p_type == n){
					return true;
					break;
				}
			}
			return false;
		},
		
		init: function(p_callback){
			for(var n in this.address){
				this._types[this._types.length] = n;
			}
			this._callback = p_callback;
			this._built = true;
		},
		run: function(p_type, p_data, p_callback){
			if(!this._built) this.init();
			
			if(!this._validateType(p_type)){
				throw new Error('[ERROR] lim.share: Not a valid type. Available Types: "' + this._types.join('", "') + '"');
				return false;
			}
			
			var regexp;
			var address = this.address[p_type];
			for(var n in p_data){
				regexp = new RegExp('\\{'+n+'\\}', 'g');
				address = address.replace(regexp, encodeURIComponent(p_data[n] || ''));
			}
			address = address.replace(/\{.*?\}/g, '');

			if(typeof(this.rules[p_type]) == 'function' && !this.rules[p_type](address, p_data)){
				return false;
			}
			
			switch(true){
				case (typeof(p_callback) == 'function'):
					p_callback(address, p_type, p_data);
					break;
				case (typeof(this._callback) == 'function'):
					this._callback(address, p_type, p_data);
					break;
				default:
					window.open(address);
					break;
			}
			return true;
		}
		
	};
	
})(lim);