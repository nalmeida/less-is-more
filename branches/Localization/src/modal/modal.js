/**
 * @class Modal
 * @author Eduardo Ottaviani
 * @version 1.0
 * @usage
 * 		var modal = new Modal()
 * 			console.log(modal)
 */

;(function(scope, $){
	
	/**
	 * Singleton de Utilidades gerais
	 */
	var Util = {
		viewport_height :function(options){
			if(options && !options.target.is('body')) return options.target.height();
			return (
				window.innerHeight ||
				document.documentElement.clientHeight ||
				document.body.clientHeight
			);
		},
		
		inline_block :(function(){
			if ( $.browser.msie ) { return !(parseInt($.browser.version, 10) <= 7 ) }
			else{ return true }
		})(),
		
		ie6 :!!($.browser.msie && (parseInt( $.browser.version, 10 ) == 6 ))
	}
	
	/**
	 * Css do template do Modal
	 */
	var Css = {
		
		styles :{
			
			modal_iframe :{
				opacity:0,
				position:'absolute',
				left:0,
				top:0,
				width:'100%',
				height:'100%'
			},
			
			overlay_and_container :{
				position:'absolute',
				width:'100%',
				height:'100%',
				top:0,
				left:0,
				zIndex:998
			},
			
			content_and_guide :{
				display: Util.inline_block ? 'inline-block' :'inline',
				zoom:1,
				verticalAlign:'middle',
				textAlign:'left'
			},
			
			container :{
				overflow:'visible',
				textAlign:'center',
				zIndex:999
			},
			
			guide :{ height:'100%' }
		},
		
		relationship :{
			modal_iframe :'.modal-overlay-ie',
			overlay_and_container :'#modal-overlay, #modal-container',
			content_and_guide :'#modal-content, .modal-guide',
			container:'#modal-container',
			guide :'.modal-guide'
		},

		inject :function( template ){
			var elements = null;
			var template = template.html;
				for(var el in this.relationship){
					elements = template.filter( this.relationship[el] )
						if( !elements.length ){
							elements = template.find( this.relationship[el] )
						};
					elements.css( Css.styles[el] );
				}
		}
		
	}
	
	/**
	 * Template do Modal
	 */
	var Template = {
		
		html :'<div id="modal-overlay">'+ (Util.ie6? '<iframe class="modal-overlay-ie"></iframe>' :'') +'</div>' +
			'<div id="modal-container">' +
				'<span class="modal-guide"></span>' +
				'<div id="modal-content"></div>' +
			'</div>',
			
		create :function(){

			var template = $(Template.html);
			
			var o = {
				content				:template.find('#modal-content'),
				overlay_container	:template.filter('#modal-overlay, #modal-container'),
				overlay				:template.filter('#modal-overlay'),
				container			:template.filter('#modal-container'),
				html				:template
			}

			Css.inject( o );
			return o;
		}
	}
	
	var Fx = {
		open :{
			start	:function(){},
			end		:function(){}
		},
		close :{
			start	:function(){},
			end		:function(){}
		}
	}

	/**
	 * Classe de ações da modal
	 */
	var Action = {
		
		Class :function(instance){
			
			this.initialize = function(){
				
				Fx.open.start(instance)
				var template = instance.template;
				var options = instance.options;
				
				instance.top = $(window).scrollTop();
				
				template.content.append('<div class="modal-loader"></div>');
				template.container.height( Util.viewport_height() );
				
				options.target.prepend( template.html );
				
					if( options.esc ){
						$(document).bind('keydown.modal', function(e){
							if( e.keyCode == 27 ){ instance.scope.close(); return false }
						});
					}
				
				$(window).bind('resize.modal', function(){ 
					template.container.height( Util.viewport_height() );
				});
				
				template.overlay.height( Util.viewport_height(options) );
				template.container.css('top', $(window).scrollTop());
				
				$(instance.scope).trigger('show');
			}
			
			this.restore = function(){
				$(document).unbind('keypress.modal');
				$(window).unbind('resize.modal');
			}
			
			this.loaded = function(callback){
				var template = instance.template;
					template.content.find('.modal-close').click( instance.scope.close );
						if(callback){ 
							callback.call(this, template.content) 
						};
				Fx.open.end(instance)
			}
			
			this.closed = function(){
				Fx.close.start(instance)
				instance.template.content.empty();
				instance.template.html.remove();
				this.restore();
				$(instance.scope).trigger('close');
				Fx.close.end(instance)
			}
			
			
		}
	}
	
	/**
	 * @class Modal
	 */
	var Modal = {
		
		Class :function(){

			var self = {
				scope	:this,
				html	:null,
				body	:null,
				cache	:null,
				top		:null,
				options	:null,
				template: Template.create()
			}
			
			var action = new Action.Class( self );
	
			self.options = { 
				esc		:true, 
				opacity	:0.6,
				bgcolor	:'#000'
			};
			
			self.template.overlay.css({ opacity :self.options.opacity, backgroundColor :self.options.bgcolor });
			
			/**
			 * Estado do Modal
			 * 1 aberto
			 * 0 fechado
			 */	
			this.state = 0;
			
			/**
			 * @params {Object} custom Opções para costumização do Modal
			 */
			this.setup = function(custom){ self.options = $.extend(self.options, custom); };
			
			/**
			 * Atualiza o conteúdo do Modal quando estiver aberto
			 * @params {String|jQuery} html Html do elemento a ser mostrado no modal.
			 */
			this.update = function(html){
				if(html.replace){ self.template.content.html( html ); }
				else if(html.append){ self.template.content.empty().append(html); }
				return this;
			}
			/**
			 * Abre o Modal
			 * @params {String|jQuery} html Html a ser mostrado no modal.
			 * @params {Function} callback Callback.
			 */
			this.open = function(html, callback){
				action.initialize();
				this.update( html );
				action.loaded(callback);
				this.state = 1;
				return this;
			}
			/** 
			*Abre o Modal
			* @params {String} url Carrega o conteúdo do html a partir de um Ajax.
			* @params {Function} callback Callback.
			*/
			this.ajax = function(url, callback){
				action.initialize();
				self.template.content.load(url, function(response){
					action.loaded(callback);
				})
				this.state = 1;
				return this;
			}
			/**
			 * Fecha o Modal
			 */
			this.close = function(){ 
				action.closed();
				this.state = 0;
			}
			/**
			 * Getter para resgatar o conteúdo do modal.
			 */
			this.get_content = function(){ return self.template.content };
			this.get_viewport = Util.viewport_height;
			
			/**
			 * Passa funções para efeitos especiais
			 * @param {object} fx Objeto contendo duas funções, uma start e outra end
			 */
			
			/**
			 * Contructor
			 */
			;(function(){
				self.html = $('html');
				self.body = $('body'); 
				self.options.target	= self.body;
			}).call(this);
			
		}
		
	}

	scope.Modal = Modal.Class;
	scope.Modal.Fx = Fx;
	
})( window, jQuery);