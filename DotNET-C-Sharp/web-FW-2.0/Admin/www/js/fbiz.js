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
		
		/* Tabs Animation */
		var tabs = $(cl)
			tabs			
			.find('div').hide().end()
			.find('div:first').show().end()
			.find('ul li:first').addClass('active').parent()
			
			.find('a').click(function(){ 
				tabs.find('li').removeClass('active')
				$(this).parent().addClass('active')				
				var currentTab = $(this).attr('href')				
				tabs.find('div').hide()
				$(currentTab).show()
				return false
			})
	}
	
	scope.fbiz.tablesort = function(cl){
		cl = cl ? cl : '.tablesorter'
		$(cl).tablesorter()
	}
	
	scope.fbiz.nav = function(el){
		var el = el ? el :  $('body').attr('class')
		$('#nav ul').find('li a')
			.parent().removeClass('active').end()
			.removeClass('active').each(function(){		
				if( $(this).text().toLowerCase().match(el) )  
					$(this).addClass('active').parent().addClass('active') 
			})
			
	}  
		
})(window, jQuery);