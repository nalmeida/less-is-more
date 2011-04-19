<?php
/**
 * Comment list code file
 * @package WordPress
 * @subpackage modulo_padrao
 */

// Não apague essas linhas
	if (isset($_SERVER['SCRIPT_FILENAME']) && 'comments.php' == basename($_SERVER['SCRIPT_FILENAME']))
		die ('Please do not load this page directly. Thanks!');
	
	if ( post_password_required() ) { ?>
		<p class="nocomments"><?php _e('Esse post está protegido por senha. Coloque sua senha para ver os comentários.'); ?></p> 
	<?php
		return;
	}
?>

<?php /*Você pode começar a editar a partir daqui*/?>

<?php if ( have_comments() ) : ?>

	<div class="post-comments">
		<h2 id="comments">Comentários</h2>
		<p><?php comments_number('Nenhuma resposta', 'Uma resposta', '% respostas');?> <?php printf('para &#8220;%s&#8221;', the_title('', '', false)); ?></p>
		<!-- Comments List -->
		<ol class="comments-list">
			<?php wp_list_comments("callback=custom_comments");?>
		</ol>
		<!-- //Comments List -->
	</div>
	
	<div class="comments-navigation">
		<div class="alignleft"><?php previous_comments_link() ?></div>
		<div class="alignright"><?php next_comments_link() ?></div>
	</div>

<?php else : // this is displayed if there are no comments so far ?>

	<?php if ( comments_open() ) : ?>
		<!-- If comments are open, but there are no comments. -->
		<p class="no-comments">Não há Comentários no momento.</p>

	<?php else : // comments are closed ?>
		<!-- If comments are closed. -->
		<p class="comments-closed">Os comentários estão fechados no momento</p>
		
	<?php endif; ?>
<?php endif; ?>

<?php include(TEMPLATEPATH."/inc/comment-form.php"); ?>