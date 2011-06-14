<?php
define("SLASH", PHP_OS == 'WINNT' || PHP_OS=='WIN32' ? '\\' : '/' );
define("ROOT_PATH", dirname(__FILE__).SLASH);

require_once(ROOT_PATH . "library/Constants.php");
require_once(ROOT_PATH . "library/Config.php");
require_once(ROOT_PATH . "library/Util.php");
require_once(ROOT_PATH . "library/App.php");
require_once(CONTROLLER_PATH . "Controller.php");

$app =& App::getInstance();

$controller = $app->processRequest()->getController();

if(!$controller){
	$app->getDefaultView();
}else{
	$app->getAction($controller);
}