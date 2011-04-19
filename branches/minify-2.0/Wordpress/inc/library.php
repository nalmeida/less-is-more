<?php
/**
 * Adiciona as bibliotecas
 * O código das bibliotecas devem estar salvas em /inc/library
 * É recomendado deixar os códigos separados por arquivos. 
 * 
 * @package wordpress
 * @subpackage modulo_padrao
 * 
 */ 
//NÃO EDITE ESSA PARTE DO CÓDIGO!!!
	$library_dir = @ dir(TEMPLATEPATH. '/inc/library');
	if ($library_dir){
		while(($libraryFile = $library_dir->read()) !== false) {
			if (!preg_match('|^\.+$|', $libraryFile) && preg_match('|\.php$|', $libraryFile)){
				include(TEMPLATEPATH. '/inc/library/' . $libraryFile);
			}
		}
	}