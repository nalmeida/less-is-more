<?php

define("SLASH", PHP_OS == 'WINNT' || PHP_OS=='WIN32' ? '\\' : '/' );
define("ROOT_PATH", dirname(__FILE__).SLASH);
define("EXT", ".php");
define("BASEPATH", rtrim(str_ireplace(basename(__FILE__), '', $_SERVER['SCRIPT_NAME']), '/'));

$library_dir = @ dir(ROOT_PATH . SLASH . 'library' . SLASH);
if ($library_dir){
	while(($libraryFile = $library_dir->read()) !== false) {
		if (!preg_match('|^\.+$|', $libraryFile) && preg_match("|\\".EXT."$|", $libraryFile)){
			require_once(ROOT_PATH . SLASH . 'library'. SLASH . $libraryFile);
		}
	}
}

$path = ltrim($_SERVER['PATH_INFO'], '/');

$combineCss =& CombineCss::getInstance();

echo $combineCss->getCss($path);