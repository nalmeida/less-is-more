<?php
/**
* 
*/
class Controller{
	
	protected $queryString,
		$masterPage,
		$config,
		$util,
		$commom,
		$regexTag;
		
	private 
		$viewDictionary;
	
	function Controller($queryString = '') {
		global $config;
		
		$this->viewDictionary = array();
		
		$this->util =& Util::getInstance();
		$this->commom =& Commom::getInstance();
		
		parse_str($queryString, $this->queryString);
		
		$this->regexTag = "#<lim:([^>]*)?>(.+)?</lim:\\1>#si";
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
		preg_replace_callback($this->regexTag, array(&$this, "_createContent"), $content);
	
	}
	
	protected function _parseMasterPage($content){
		return preg_replace_callback($this->regexTag, array(&$this, "_replaceContent"), $content);
	}
	
	protected function _getMasterPage($content){
		return $this->_loadContent(VIEW_PATH."shared/".$this->masterPage, $content);
	}
	
	private function _loadContent($path){
		if(file_exists($path)){
			ob_start();
				if ((bool) @ini_get('short_open_tag') === false && $config['use-short-tags'] == true) {
					echo eval('?>'.preg_replace("/;*\s*\?>/", "; ?>", str_replace('<?=', '<?php echo ', file_get_contents($path))));
				} else {
					include($path);
				}
				$fileContent = ob_get_contents();
			ob_end_clean();
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
