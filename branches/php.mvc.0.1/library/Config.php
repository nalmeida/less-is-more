<?php

$config = array(
	
	"root-assets"			=> "",
	
	"root-uploads"			=> "",
	
	"assets-folder"			=> "assets/",
	
	"uploads-folder"		=> "uploads/",
	
	"default-controller"	=> "home",
	
	"default-action"		=> "index",
	
	"default-master-page"	=> "master.page".EXT,
	
	"use-master-page"		=> true,
	
	"lim-namespace"			=> "http://www.w3.org/TR/html4/"

);

$config["lim-document-wrapper"] = "<root xmlns:lim='".$config['lim-namespace'] ."'>{content}</root>";
