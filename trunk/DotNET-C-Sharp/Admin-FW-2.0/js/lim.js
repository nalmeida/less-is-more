if(!window["lim"]) var lim = {};
(function(scope, $) {
	
	// namespace
	if(!scope) scope = window;
	
	scope.login = function() {
		$('#button').click(function(){
			if(scope.Common)
				window.location.href = scope.Common.Util.Root + "Home";
			return false;
		})
	}
	
	scope.tabs = function(cl){
		cl = cl || '.tabs';

		var querystring = function(){		
			var tab = null;
			if(document.location.href.split('#').length > 0 && document.location.href.split('#')[1])
				tab = '#' + document.location.href.split('#')[1];
			return tab;
		};
		var zera = function(){
			lis.removeClass("active");
			tabsContents.hide();
		};

		var tabsHolder = $(cl);
		if(!tabsHolder.length){
			return;
		}
		var tabsNavigation = $('.tabsNavigation', tabsHolder);
		var links = tabsNavigation.find('a');
		var lis = tabsNavigation.find('li');
		var tabsContents = $('.tabContent');
		var location = querystring() || (links[0].href.split('#')[1] ? '#' + links[0].href.split('#')[1] : '');

		zera();

		if(location){
			links.filter('a[href='+ location +']').parent().addClass('active');
			$(location).show();
		}
			
		links.click(function(){
			zera();
			$(this).parent().addClass('active');
			if(this.href.split('#')[1])
				tabsContents.filter('#'+this.href.split('#')[1]+'-gen').show();
		});
		
		tabsContents.each(function(){
			this.id += '-gen';
		});
		
	}
	
	scope.applyZebra = function(elm){
		$('tr:nth-child(even)', $(elm) || $('.zebra')).addClass('even');
	}
	
	scope.tablesort = function(cl){
		$(cl || '.tablesorter')
			.tablesorter()
			.bind('sortStart', function(){
				$('tr', this).removeClass('even');
			})
			.bind('sortEnd', function(){
				lim.applyZebra(this);
			});
	}
	
	scope.nav = function(el){
		var el = el ||  $('body').attr('class');
			el = el.toLowerCase();
		$('#nav ul').find('li a')
			.parent().removeClass('active').end()
			.removeClass('active').each(function(){
				if( $(this).text().toLowerCase().match(el) )
					$(this).addClass('active').parent().addClass('active');
			});
	}  
		
})(lim, jQuery);