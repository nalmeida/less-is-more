<?php

$config = array(
	// Config variables
	"site-title"	=> "",
	"site-description"	=> "",
	"site-keywords"	=> "",
	"site-h1"	=> "",
	"site-authorName"	=> "",
	"site-companyName	"	=> "",
	
	// Basic variables
	"root-script"			=> "index.php",
	"root-assets"			=> "assets/",
	"root-uploads"			=> "uploads/",
	
	"default-controller"	=> "home",
	"default-action"		=> "index",
	"default-master-page"	=> "master.page".EXT,
	"use-master-page"		=> true,
	"use-short-tags"		=> true,
	"lim-namespace"			=> "http://www.w3.org/TR/html4/"
);

$config["lim-document-wrapper"] = "<root xmlns:lim='".$config['lim-namespace'] ."'>{content}</root>";
