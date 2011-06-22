<?php
$app =& App::getInstance();

$controller = $app->processRequest(BASEPATH, $basename)->getController();

if(!$controller){
	$app->getDefaultView();
}else{
	$app->getAction($controller);
}