<?php
/**
* 
*/
class CombineCss
{
	private static $instance;
	
	private $files,
			$parsed,
			$util,
			$regexVars;
	
	private function CombineCss(){
		$this->files = array();
		$this->parsed = array();
		$this->util =& Util::getInstance();

		$this->regexVars = "#\\$([a-z0-9_]+?)/#i";
	}
	
	public function add($path){
		
		if(false === file_get_contents($path, 0, null, 0, 1)){
			$this->files[] = " /*File: $path not found. please check the path name */ ";
		}else{
			$this->files[] = file_get_contents($path);
		}
		
		return $this;
	}
	
	public function getCss($path){
		header('Content-type: text/css');
		$files = explode('|', $path);
		$length = count($files);
		$output = '';
		
		for ($i=0; $i < $length; $i++) {
			if(preg_match('#\.css$#', $files[$i])){
				if($stream = fopen($this->util->globalPath().'css/'.$files[$i], 'r')){
					$output .= stream_get_contents($stream);
					fclose($stream);
				}
			}
		}
		
		if(substr($output, 0, 3) == pack('CCC', 239, 187, 191)) {
			 $output = substr($output, 3);
		}
		
		echo self::_parseCSS($output);
		exit();
	}
	
	private function _parseCSS($content){
		return preg_replace_callback($this->regexVars, array(&$this, "_replaceVars"), $content);
	}
	
	private function _replaceVars($matches){
		return method_exists($this->util, $matches[1])
					? call_user_func(array(&$this->util, $matches[1]))
					: $matches[0];
	}
	
	public static function getInstance() {
		
		if (!isset(self::$instance)) {
			$class = __CLASS__;
			self::$instance = new $class;
		}
		
		return self::$instance;
	}
	
	public function __clone(){ trigger_error('Clone is not allowed.', E_USER_ERROR); }
}
