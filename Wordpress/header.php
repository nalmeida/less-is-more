<?php 
/**
 * Headers do site
 * Nesse arquivo são inseridas as metatags que ficarão dentro da tag head
 * ATENÇÃO, não coloquem tags script e nem link rel="stylesheet", use a função wp_register_style/wp_register_script no arquivo /inc/config.php
 * 
 * @package WordPress
 * @subpackage modulo_padrao
 */
?>
<meta http-equiv="Content-Type" content="<?php bloginfo('html_type'); ?>; charset=<?php bloginfo('charset'); ?>" />
	<title><?php wp_title();?></title>
	<link rel="alternate" type="application/rss+xml" title="RSS <?php bloginfo("name")?>" href="<?php bloginfo('rss2_url'); ?>" />
	<link rel="pingback" href="<?php bloginfo('pingback_url'); ?>" />
	
	<!--[if IE]>
	<style type="text/css">
		body {
			behavior:  url("<?php bloginfo('template_url') ?>/js/csshover.htc");
		}
		.pngfix{behavior: url(<?php bloginfo('template_url');?>/js/iepngfix.htc);}
	</style>
	<![endif]-->
<?php wp_head(); ?>
