<?php
/**
 * @package WordPress
 * @subpackage modulo_padrao
 */

/**
 * Registra os sidebars e inclui os widgets.
 * Os widgets que serão incluidos devem estar na pasta inc/widget deste tema
 * @return void
 */
function modp_theme_sidebar_init() {
	if ( !function_exists('register_sidebars') )
		return;

	// Register Widgetized areas.
	// Area 1 index.php
    register_sidebar(array(
       	'name' => 'Sidebar1',
       	'id' => 'dynamic-sidebar1',
       	'before_widget' => '<div id="%1$s" class="sidebox %2$s">',
       	'after_widget' => "</div>",
		'before_title' => '<h3 class="sidebox-title">',
		'after_title' => "</h3>",
    ));
	
    //NÃO EDITE ESSA PARTE DO CÓDIGO!!!
	$widgets_dir = @ dir(TEMPLATEPATH. '/inc/widgets');
	if ($widgets_dir){
		while(($widgetFile = $widgets_dir->read()) !== false) {
			if (!preg_match('|^\.+$|', $widgetFile) && preg_match('|\.php$|', $widgetFile)){
				include(TEMPLATEPATH. '/inc/widgets/' . $widgetFile);
			}
		}
	}
	//init widgets
	do_action('widgets_init');
	//NÃO EDITE ESSA PARTE DO CÓDIGO!!!
}

// Runs our code at the end to check that everything needed has loaded
add_action( 'init', 'modp_theme_sidebar_init' );