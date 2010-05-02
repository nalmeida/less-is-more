<?php
/**
 * Esse Arquivo é praticamente o core do tema. Ele é o penultimo arquivo a ser carregado. 
 * Depois dele só o arquivo pluggable.php é carregado.
 * Qualquer função contida no arquivo pluggable.php pode ser sobrescrita aqui.
 * 
 * 
 * @package WordPress
 * @subpackage ceea_theme
 */

/*
 * Arquivo de configuração do site. Colocar todas as constantes e configurações aqui
 */
include(TEMPLATEPATH.'/inc/config.php');

/*
 * Arquivo com todas as classes e funções de bibliotecas que serão ultilizadas no site.
 * Use esse arquivo para funções/classes Ultilitárias no site
 */
include(TEMPLATEPATH.'/inc/library.php');

/*
 * Arquivo para adicionar sidebars e widgets
 */
include(TEMPLATEPATH.'/inc/sidebars.php');
