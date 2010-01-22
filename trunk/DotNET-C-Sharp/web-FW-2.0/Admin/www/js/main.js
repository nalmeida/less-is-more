/*	
	Super Simple Admin Theme
	Jeff Adams	
*/

$(document).ready(function(){
	/* Zebra Striping for Tables */					   
	$("tr:nth-child(even)").addClass("even");
	
	$("#myTable").tablesorter(); 

	
	/* Tabs Animation */
	$('#tabs div').hide();
	$('#tabs div:first').show();
	$('#tabs ul li:first').addClass('active');
	
	$('#tabs ul li a').click(function(){ 
		$('#tabs ul li').removeClass('active');
		$(this).parent().addClass('active'); 
		var currentTab = $(this).attr('href'); 
		$('#tabs div').hide();
		$(currentTab).show();
		return false;
	});

});