<?php
/**
* 
*/
class Controller{
	
	protected $queryString,
		$masterPage,
		$config,
		$util;
		
	private 
		$viewDictionary;
	
	const regexTag = "#<lim:([^>]*)?>(.+)?</lim:\\1>#si";
	
	function Controller($queryString = '') {
		global $config;
		
		$this->viewDictionary = array();
		
		$this->util =& Util::getInstance();
		
		parse_str($queryString, $this->queryString);
		
		$this->masterPage = $config["default-master-page"];
		$this->config = $config;
	}
	
	public function renderView($view = "index"){
		$content = $this->_getView($view);
		
		if($this->config["use-master-page"] == true){
			$this->_parseView($content);
			$output = $this->_parseMasterPage($this->_getMasterPage($content));
		}else{
			$output = $content;
		}
		
		echo $output;
	}
	
	private function _createContent($maches){
		$this->viewDictionary[$maches[1]] = $maches[2];
		// echo "maches[1] = $maches[1] => maches[2] =$maches[2]";
	}
	
	private function _replaceContent($maches){
		return ($this->viewDictionary[$maches[1]])
					? $this->viewDictionary[$maches[1]]
					: (($maches[2]) ? $maches[2] : "");
	}
	
	protected function _getView($view){
		return $this->_loadContent(VIEW_PATH.$view.EXT);
	}
	
	protected function _parseView($content){
		$this->viewDictionary = array();
		preg_replace_callback(self::regexTag, array(&$this, "_createContent"), $content);
		switch(preg_last_error()){
			case PREG_INTERNAL_ERROR:
				echo "PREG_INTERNAL_ERROR";
			break;
			case PREG_BACKTRACK_LIMIT_ERROR:
				echo "PREG_BACKTRACK_LIMIT_ERROR";
			break;
			case PREG_RECURSION_LIMIT_ERROR:
				echo "PREG_RECURSION_LIMIT_ERROR";
			break;
			case PREG_BAD_UTF8_ERROR:
				echo "PREG_BAD_UTF8_ERROR";
			break;
			case PREG_BAD_UTF8_OFFSET_ERROR:
				echo "PREG_BAD_UTF8_OFFSET_ERROR";
			break;
			default:;
		}
	}
	
	protected function _parseMasterPage($content){
		return preg_replace_callback(self::regexTag, array(&$this, "_replaceContent"), $content);
	}
	
	protected function _getMasterPage($content){
		return $this->_loadContent(VIEW_PATH."shared/".$this->masterPage, $content);
	}
	
	private function _loadContent($path){
		if(file_exists($path)){
			ob_start();
				include($path);
				$fileContent = ob_get_contents();
			ob_clean();
		}else{
			$this->_trigger404($path);
		}
		
		return $fileContent;
	}
	
	protected function _trigger404($path){
		header("Status: 404 Not Found");
		echo "<html><body><h1>Not Found!</h1> Resource $path not found</body></html>";
		exit();
	}
	
}
