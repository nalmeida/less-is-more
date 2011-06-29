<?php
/**
 * Common.Util
 * @since 29/06/2011
 * @version 0.1
 * @author Nicholas Almeida
 */
class Commom {
	
	private static $instance;
	
	private
		$_title,
		$_description,
		$_keywords,
		$_h1,
		$_authorName,
		$_companyName;
	
	private function Commom(){
		global $config;
	}
	
	public static function getInstance() {
		
		if (!isset(self::$instance)) {
			$class = __CLASS__;
			self::$instance = new $class;
		}
		
		return self::$instance;
	}
	
	public function title($title = '') {
		return "";
	}
	
	public function description($description = '') {
		return "";
	}
	
	public function keywords($keywords = '') {
		return "";
	}
	
	public function h1($h1 = '') {
		return "";
	}

	public function authorName($authorName = '') {
		return "";
	}

	public function companyName($companyName = '') {
		return "";
	}
	
	public function __clone(){ trigger_error('Clone is not allowed.', E_USER_ERROR); }

}