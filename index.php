<?php
define("SLASH", PHP_OS == 'WINNT' || PHP_OS=='WIN32' ? '\\' : '/' );
define("ROOT_PATH", dirname(__FILE__).SLASH);
define("EXT", ".php");
define("BASEPATH", rtrim(str_ireplace(basename(__FILE__), '', $_SERVER['SCRIPT_NAME']), '/'));

$basename = basename(__FILE__);

$library_dir = @ dir(ROOT_PATH . SLASH . 'library' . SLASH);
if ($library_dir){
	while(($libraryFile = $library_dir->read()) !== false) {
		if (!preg_match('|^\.+$|', $libraryFile) && preg_match("|\\".EXT."$|", $libraryFile)){
			require_once(ROOT_PATH . SLASH . 'library'. SLASH . $libraryFile);
		}
	}
}

require_once(CONTROLLER_PATH . "Controller.php");

$app =& App::getInstance();

$controller = $app->processRequest(BASEPATH, $basename)->getController();

if(!$controller){
	$app->getDefaultView();
}else{
	$app->getAction($controller);
}