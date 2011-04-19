<?php
/**
 * 
 */

if(!defined('MPWDGT_DEFAULT_TEXT')){
	define('MPWDGT_DEFAULT_TEXT',"Esse é um conteúdo de widget padrão para ser inserido num sidebar padrão. Esse sidebar pode ser instanciado em qualquer lugar do site pela função dynamic_sidebar()<br /> O código desse widget está inserido em /inc/widget/example-widget.php");
}

if(!defined('MPWDGT_DEFAULT_TITLE')){
	define('MPWDGT_DEFAULT_TITLE',"Exemplo de Widget");
}

class ExampleWidget extends WP_Widget{
	/**
	* 
	*  Contrutora da classe exemplo
	*/
	function ExampleWidget(){
		$widget_ops = array('classname' => 'Example', 'description' => __( "Widget de exemplo") );
		$control_ops = array();
		$this->WP_Widget('example-widget', __('Exemplo'), $widget_ops, $control_ops);
	}

	/**
	* Mostra the Widget
	* 
	*/
	function widget($args, $instance){
		extract($args);
		$title = apply_filters('widget_title', empty($instance['title']) ? '&nbsp;' : $instance['title']);
		$texto = empty($instance['texto']) ? MPWDGT_DEFAULT_TEXT: $instance['texto'];
		
		//Antes do widget 
		echo $before_widget;
		
		//o Título
		if ( $title )
		echo $before_title . $title . $after_title;
		
		//Imprime o widget 
		/*
		 * Aqui é feito o processamento do código
		 */
		echo $texto; 
				
		//Após do widget
		echo $after_widget;
	}

  /**
    * Salva as configurações do Widget
    *
    */
    function update($new_instance, $old_instance){
      $instance = $old_instance;
      $instance['title'] = strip_tags(stripslashes($new_instance['title']));
      $instance['texto'] = strip_tags(stripslashes($new_instance['texto']));

    return $instance;
  }

  /**
    * Cria o formulário de edição do Widget
    *
    */
    function form($instance){
      //Defaults
      $instance = wp_parse_args( (array) $instance, array('title'=>'', 'texto'=> MPWDGT_DEFAULT_TEXT) );

      $title = htmlspecialchars($instance['title']);
      $qtde = htmlspecialchars($instance['texto']);

      // Output the options
	  // Title
      echo '<p><label for="' . $this->get_field_name('title') . '">' . __('Title:') . ' <input style="width: 100px;" id="' . $this->get_field_id('title') . '" name="' . $this->get_field_name('title') . '" type="text" value="' . $title . '" /></label></p>';
	  // Texto
	  echo '<p><label for="' . $this->get_field_name('texto') . '">' . __('Texto a ser exibido') . '<br /> <textarea style="width: 100%;" id="' . $this->get_field_id('texto') . '" name="' . $this->get_field_name('texto') . '">'.$url.'</textarea></label></p>';
      
  }

}

add_action('widgets_init', create_function('', 'return register_widget("ExampleWidget");'));
