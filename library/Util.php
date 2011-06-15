<?php
/**
 * Common.Util
 * @since 03/06/2011
 * @version 0.1
 * @author Willian Martins - wmartins@fbiz.com.br
 */
class Util {
	
	private static $instance;
	
	private
		$_port,
		$_host,
		$_language,
		$_assetsRoot,
		$_uploadRoot,
		$_globalUploadPath,
		$_languageUploadPath,
		$rawUrl;
	
	private function Util(){
		global $config;
		
		self::language("pt-BR");
		self::host($_SERVER["SERVER_NAME"]);
		
		self::assetsRoot($config["root-assets"] ? $config["root-assets"] : self::root() . "locales/");
		self::uploadRoot($config["root-uploads"] ? $config["root-uploads"] : self::assetsRoot() . "uploads/");
		self::globalUploadPath(self::uploadRoot() . 'global/');
		self::languageUploadPath(self::uploadRoot() . self::language() . '/' );
	}
	
	public static function getInstance() {
		
		if (!isset(self::$instance)) {
			$class = __CLASS__;
			self::$instance = new $class;
		}
		
		return self::$instance;
	}
	
	public function language($lang = '') {
		if($lang){
			$this->_language = $lang;
		}else{
			return $this->_language;
		}
	}
	
	public function port() {
		return ($_SERVER["SERVER_PORT"] == "80") ? "" : (":" + $_SERVER["SERVER_PORT"]);
	}
	
	public function protocol() {
		return ($_SERVER["HTTPS"] == "" || $_SERVER["HTTPS"] == "off") ? "http" : "https";
	}
	
	public function uploadRoot($path = '') {
		if($path){
			$this->_uploadRoot = $path;
		}else{
			return $this->_uploadRoot;
		}
	}
	
	public function globalUploadPath($path = '') {
		if($path){
			$this->_globalUploadPath = $path;
		}else{
			return $this->_globalUploadPath;
		}
	}
	
	public function languageUploadPath($path = '') {
		if($path){
			$this->_languageUploadPath = $path;
		}else{
			return $this->_languageUploadPath;
		}
	}
	
	public function assetsRoot($path = '') {
		if($path){
			$this->_assetsRoot = $path;
		}else{
			return $this->_assetsRoot;
		}
	}
	
	public function host($host = ''){
		if($host){
			$this->_host = $host;
		}else{
			return $this->_host;
		}
	}
	
	public function root(){
		return rtrim($this->protocol() . "://" . $this->_host . $this->port()) . BASEPATH . "/";
	}
	
	public function globalPath(){
		return self::assetsRoot() . "global/";
	}
	
	public function languagePath(){
		return self::assetsRoot() . self::language(). "/";
	}
	
	public function rawURL(){
		$parsedURL = parse_url($_SERVER["REQUEST_URI"]);
		$path = rtrim($parsedURL["path"], "/") . "/";
		$query = ($parsedURL["query"])? "?" . $parsedURL["query"] : "";
		
		return self::root() . ltrim($path, "/") . $query;
	}
	
	public function __clone(){ trigger_error('Clone is not allowed.', E_USER_ERROR); }

}