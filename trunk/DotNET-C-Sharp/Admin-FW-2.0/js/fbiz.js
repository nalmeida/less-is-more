(function(scope, $) {
	
	// namespace
	
	scope.fbiz.login = function() {
		$('#button').click(function(){
			window.location.href = fbiz.Common.Util.Root + "Home";
			return false;
		})
	}
	
	scope.fbiz.tabs = function(cl){
		cl = cl ? cl : '.tabs'
		
		var querystring = function(){		
			var tab = null
				if(scope.location.href.match(/\#/))
					tab = '#' + scope.location.href.split(/#/)[1]
			return 	tab	
		}
		
		var tabs = $('.tabs ul').eq(0)
		var location = querystring() || '#' + tabs.find('li a').eq(0).attr('href').split('#')[1]
		
		var zera = function(){
			tabs.parent().children('div').hide().end().end()
				.find('li').removeClass('active')			
		}
		
		zera()
		
		tabs.find('a[href='+ location +']').parent().addClass('active')
		$(location).show()
			
		tabs.find('a').click(function(){
			zera()
			$(this).addClass('destaque').parent().addClass('active')			
			$( '#'+ this.href.split(/#/)[1] ).show()
			return false
		})					
		
	}
	
	scope.fbiz.tablesort = function(cl){
		cl = cl ? cl : '.tablesorter'
		$(cl).tablesorter()
	}
	
	scope.fbiz.nav = function(el){
		var el = el ? el :  $('body').attr('class')
			el = el.toLowerCase()
		$('#nav ul').find('li a')
			.parent().removeClass('active').end()
			.removeClass('active').each(function(){		
				if( $(this).text().toLowerCase().match(el) )  
					$(this).addClass('active').parent().addClass('active') 
			})
			
	}  
		
})(window, jQuery);