<?php
/**
 * Single article code file
 * @package WordPress
 * @subpackage modulo_padrao
 */
?><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" <?php language_attributes(); ?>>
<head>
	<?php get_header(); ?>
</head>
<body>	
	
	<!-- Wrapper -->
		<div id="page">
			<?php include(TEMPLATEPATH . "/inc/cabecalho.php"); ?>
			<!-- Content -->
			<div class="content">
				
				<!-- Posts -->
				<div id="container">
				
					<div class="wrapper">
						<!-- Me Apague -->
						<div class="title-explain">
							Módulo Padrão <?php echo MPLIM_VERSION;?> - page.php
						</div>
						<p>
							Esse arquivo é o page.php. Esse arquivo é usado para exibir uma página do site.<br />
							A página é um tipo de conteúdo que é ordenado de forma hierárquica e atemporal.
						</p>
						<!-- //Me Apague -->
						
						<!-- início do código The loop -->
						<?php if(have_posts()): ?>
							<?php while(have_posts()): the_post(); ?>
							
							<!-- Posts-->
							<div class="post">
								<h1 class="post-title">
									<?php single_post_title(); ?>
								</h1>
								<div class="entry">
									<?php the_excerpt("leia mais..."); ?>
								</div>
								<div class="post-meta">
									<span class="posted">
										Publicado em, <abbr class="published"> <?php the_time("F j, Y") ?></abbr> por <?php the_author_posts_link(); ?>
									</span><br />
									<span class="post-cat">Categoria: <?php the_category(", ") ?></span><br />
									<span class="taxonomy">Tags: <?php the_tags( "", ",", "" ); ?></span>
								</div>
							</div>
							
							<!-- // Posts -->
							<?php endwhile; ?>
						<?php endif; ?>
						<!-- //The loop -->
												
					</div>
					
					<?php get_sidebar();?>
				</div>
				<!-- // Posts -->
			</div>
			<!-- // Content -->
			<?php include(TEMPLATEPATH."/inc/rodape.php")?>
		<!-- // Wrapper -->
		</div>
		
<?php get_footer(); ?>
</body>
</html>