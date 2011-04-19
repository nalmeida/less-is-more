<?php
/**
 * Arquivo de configuração para o funcionamento do Template
 * @package WordPress
 * @subpackage modulo_padrao
 */

//Constants
if(!defined('MPLIM_VERSION')){
	/*
	 * Versão do tema
	 */
	define('MPLIM_VERSION','1.0');
}

//Constants
if(!defined('GOOGLE_ANALYTICS')){
	/*
	 * Constante para o track Google Analitycs
	 */
	define('GOOGLE_ANALYTICS','');
}

if(!defined('THEME_NAME')){
	/*
	 * Constante com o nome da pasta onde está localizado o tema
	 * Precisa ser mudada caso o nome da pasta mude
	 */
	define('THEME_NAME','modulo-padrao');
}


/*
 * Registro dos Estilos (css).
 * Seguir padrão abaixo.
 * @see wp_register_style
 */
wp_register_style('reset-style', WP_CONTENT_URL.'/themes/'.THEME_NAME.'/css/reset.css');
wp_register_style('main-style', WP_CONTENT_URL.'/themes/'.THEME_NAME.'/css/main.css',array('reset-style'));

/*
 * Registro dos Scripts (js).
 * Seguir padrão abaixo.
 * O último parâmetro indica que ele deve ser impresso no footer, caso contrário deveria ser colocado false 
 * @see wp_register_script
 */
wp_register_script('main-functions',WP_CONTENT_URL.'/themes/'.THEME_NAME.'/js/functions.js',array('jquery'),'1.0', true);

if(!is_admin()){
	/*
	 * Enfileiramento dos estilos/scripts
	 * Note que não é necessário a inserção do resetStyle e nem no jquery pois ambos estão como
	 * dependencias do mainStyle e do main-functions respectivamente 
	 */
	wp_enqueue_script('main-functions');
	wp_enqueue_style('main-style');
}