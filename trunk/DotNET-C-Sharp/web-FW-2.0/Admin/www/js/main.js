/*	
	Super Simple Admin Theme
	Jeff Adams	
*/

$(document).ready(function(){
	
	/* Zebra Striping for Tables */					   
	$("tr:nth-child(even)").addClass("even")
	 
	fbiz.tablesort()
	fbiz.tabs()
	fbiz.nav()

});