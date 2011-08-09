<% 
	//
	// ====================== IMPORTANTE =======================
	//
	// Não usar a tag original do Analytics que, normalmente, é passada por BI. Só trocar o ##CODE## pelo "UA-XXXXXXX-X" que é enviado.
	//
	// APAGAR ESSE COMENTÁRIO AO COLOCAR O CÓDIGO CORRETO.
	//
	// =========================================================
	//
%>
<script type="text/javascript">
	<% if(Util.isLocal){ %>
	var _gaq = {
		push: function(p_arr) {
			var arr = p_arr;
			var func = arr[0];
			arr.shift();
			arr = arr.join('\',\'');
			if(window['console']) {
				console.info('_gaq.push([\'' + func + '\',\'' + arr + '\']);');
			}
		}
	};
	<% } %>
	var _gaq = _gaq || [];
		_gaq.push(['_setAccount', '##CODE##']);
		_gaq.push(['_trackPageview','<%=ViewData["TrackValue"]%>']);
</script>	

<% if(!Util.isLocal){ %>
<script type="text/javascript">
	(function() {
		var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
		ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
		var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
	})();
</script>
<% } %>
