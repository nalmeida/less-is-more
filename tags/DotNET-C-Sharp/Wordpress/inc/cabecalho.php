<?php
/**
 * Cabecalho do site
 * Neste arquivo fica contido o header do site
 * @package WordPress
 * @subpackage modulo_padrao
 */
?><!-- Header -->
<div id="header">
	<div class="content">
		<?php if(is_home()):?>
			<h1 class="site-name">
				Módulo Padrão Wordpress <?php echo MPLIM_VERSION;?>
			</h1>
		<?php else:?>
			<div class="site-name">
				<a href="<?php bloginfo("url")?>" rel="index contents">
					Módulo Padrão Wordpress <?php echo MPLIM_VERSION;?>
				</a>
			</div>
		<?php endif;?>
		<p>
			Esse é a parte que contém o header do site. Ele se encontra em /inc/cabecalho.php. O header.php só há as metatags.
		</p>
	</div>
</div>

<!--

 // Header -->