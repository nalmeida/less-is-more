<?php
/**
* 
*/
class Home extends Controller{
	
	function Home($querystring){
		parent::__construct($querystring);
	}
	
	public function index(){
		$this->renderView('home/index');
	}
	
}
