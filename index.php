<?php
define("SLASH", PHP_OS == 'WINNT' || PHP_OS=='WIN32' ? '\\' : '/' );
define("ROOT_PATH", dirname(__FILE__).SLASH);
define("BASEPATH", rtrim(str_ireplace(basename(__FILE__), '', $_SERVER['SCRIPT_NAME']), '/'));

require_once(ROOT_PATH . "library/Constants.php");
require_once(ROOT_PATH . "library/Config.php");
require_once(ROOT_PATH . "library/Util.php");
require_once(ROOT_PATH . "library/App.php");
require_once(CONTROLLER_PATH . "Controller.php");

$app =& App::getInstance();

$controller = $app->processRequest(BASEPATH, basename(__FILE__))->getController();

if(!$controller){
	$app->getDefaultView();
}else{
	$app->getAction($controller);
}