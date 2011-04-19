<?php
/**
 * add a default-gravatar to options
 */
if ( !function_exists('theme_addgravatar') ) {
	function theme_addgravatar( $avatar_defaults ) {
		$avatar_dir = @ dir(TEMPLATEPATH. '/img/avatar');
		$i = 0;
		if ($avatar_dir){
			while(($avatar_file = $avatar_dir->read()) !== false) {
				if (!preg_match('|^\.+$|', $avatar_file) && preg_match('|\.gif$|', $avatar_file)){
					$myavatar = get_bloginfo('template_directory') . '/img/avatar/'.$avatar_file;
					$avatar_defaults[$myavatar] = 'custom_avatar_'.++$i;
				}
			}
		}
 
		return $avatar_defaults;
	}
 
	add_filter( 'avatar_defaults', 'theme_addgravatar' );
}