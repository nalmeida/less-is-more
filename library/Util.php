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
		$_assets,
		$_minify,
		$_uploadRoot,
		$_rootScript;
	
	private function Util(){
		global $config;
		
		$this->_rootScript = $config["root-script"];
		self::language("pt-BR");
		self::host($_SERVER["SERVER_NAME"]);
		
		self::assets(self::root() . $config["root-assets"]);
		self::minify(self::root() . "minify.php/");
		self::uploadRoot(self::assets() . $config["root-uploads"]);
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
		return ($_SERVER["SERVER_PORT"] == "80") ? "" : (":" . $_SERVER["SERVER_PORT"]);
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
	
	public function assets($path = '') {
		if($path){
			$this->_assets = $path;
		}else{
			return $this->_assets;
		}
	}
	
	public function minify($path = '') {
		if($path){
			$this->_minify = $path;
		}else{
			return $this->_minify;
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
		return rtrim($this->protocol() . "://" . $this->_host . $this->port(), "/") . BASEPATH . "/";
	}
	
	public function appRoot($path = '') {
		return rtrim(self::root() . $this->_rootScript, "/") . "/";
	}
	
	public function rawURL(){
		$parsedURL = parse_url($_SERVER["REQUEST_URI"]);
		$path = rtrim($parsedURL["path"], "/") . "/";
		$query = ($parsedURL["query"])? "?" . $parsedURL["query"] : "";
		
		return self::root() . ltrim($path, "/") . $query;
	}
	
	public function __clone(){ trigger_error('Clone is not allowed.', E_USER_ERROR); }

}