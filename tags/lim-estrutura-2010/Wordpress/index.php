<?php
/**
 * Home code file
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
						<h2>
							Módulo Padrão <?php echo MPLIM_VERSION;?> - index.php
						</h2>
						<p>
							Esse arquivo é o index.php. Esse arquivo é o arquivo base do tema.<br/>
							Caso não exista nenhum outro arquivo para carregar post, página e listagem de post, essa será usada em seu lugar.
						</p>
						<p>
							A lista de arquivos segue o seguinte esquema:
						</p>
						<ul>
							<li>
								home.php - Arquivo usado para a home do site. Crie uma se a home do site não listar os ultimos posts do site.
							</li>
							<li>
								search.php - página para resultado de buscas.
							</li>
							<li>
								category.php, category-slug-da-categoria.php, category-id.php, archieve.php - para exibição de posts de uma categoría específica.
							</li>
							<li>
								tag.php, tag-slug-da-tag.php, tag-id.php, archieve.php - para exibição de posts de uma tag específica.
							</li>
							<li>
								page.php, nomedapagina.php, page-id.php, page-slug.php - para exibição de páginas estáticas, ou não do site.
							</li>
						</ul>
						<p>
							Para um exemplo melhor sobre a hierarquia dos arquivos do template favor consulte a imagem em <a href="http://codex.wordpress.org/images/1/18/Template_Hierarchy.png" rel="external" target="_blank">http://codex.wordpress.org/images/1/18/Template_Hierarchy.png</a>
						</p>
						<p>
							Abaixo segue um exemplo do "the loop" (trecho de código que tras posts específicos da url em questão)
						</p>
						<!--// Me Apague -->
						<!-- início do código The loop -->
						<?php if(have_posts()): ?>
							<?php while(have_posts()): the_post(); ?>
							
							<!-- Posts-->
							<div class="post">
								<h2 class="post-title">
									<a href="<?php the_permalink() ?>" rel="bookmark"><?php the_title() ?></a>
								</h2>
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