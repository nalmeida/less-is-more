<?php
/**
 * Common.Util
 * @since 03/06/2011
 * @version 0.1
 * @author Willian Martins - wmartins@fbiz.com.br
 */
class App {
	
	private static $instance;
	
	private 
		$parsedURL,
		$route,
		$className,
		$classPath,
		$config;
	
	private function App(){
		global $config;
		
		$this->config = $config;
	}
	
	public static function getInstance() {
		
		if (!isset(self::$instance)) {
			$class = __CLASS__;
			self::$instance = new $class;
		}
		
		return self::$instance;
	}
	
	public function processRequest($basepath){
		$this->parsedURL = parse_url(str_ireplace($basepath, '', $_SERVER["REQUEST_URI"]));
		
		$this->route = explode("/", trim($this->parsedURL["path"], "/"));
		$this->route[0] = $this->route[0] == "" ? $this->config["default-controller"] : $this->route[0];
		
		$this->className = ucfirst(strtolower($this->route[0]));
		$this->classPath = CONTROLLER_PATH . $this->className .EXT;
		
		return $this;
	}
	
	public function getController(){
		if(file_exists($this->classPath)){
			require($this->classPath);
			return new $this->className($this->parsedURL["query"]);
		}
		
		return false;
	}
	
	public function getAction($currentController){
		$actionName = count($this->route) > 1 ? strtolower($this->route[1]) : $this->config["default-action"];
		
		if(method_exists($currentController, $actionName)){
			$currentController->$actionName();
			return true;
		}
		
		return $this->getDefaultView();
	}
	
	public function getDefaultView(){
		require_once CONTROLLER_PATH . "Controller" . EXT;
		
		$this->route[1] = ($this->route[1]) ? $this->route[1] : $this->config["default-action"];
		
		$gerericController = new Controller($this->parsedURL["query"]);
		$gerericController->renderView(implode("/", $this->route));
	}
	
	public function __clone(){ trigger_error('Clone is not allowed.', E_USER_ERROR); }
}