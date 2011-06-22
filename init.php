<?php
$app =& App::getInstance();

$controller = $app->processRequest(BASEPATH, basename(__FILE__))->getController();

if(!$controller){
	$app->getDefaultView();
}else{
	$app->getAction($controller);
}