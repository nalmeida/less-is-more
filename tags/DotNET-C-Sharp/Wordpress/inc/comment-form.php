<?php 
/**
 * Arquivo com o formulário de comentário
 * 
 * TODO: Criar validação do formulário via javascript
 * 
 * @package Wordpress
 * @subpackage modulo_padrao
 */
?>
<?php if ( comments_open() ) : ?>

	<div id="respond" class="send-comment">
		
		<h3>Envie Seu commentário</h3>
		
		<div id="cancel-comment-reply"> 
			<small><?php cancel_comment_reply_link() ?></small>
		</div> 
		
		<?php if ( get_option('comment_registration') && !is_user_logged_in() ) : ?>
			<p><?php printf('Você deve estar <a href="%s">logado</a> para postar um comentário.', wp_login_url( get_permalink() )); ?></p>
		<?php else : ?>
			
			<form action="<?php echo get_option('siteurl'); ?>/wp-comments-post.php" method="post" id="commentform">
				
				<?php if ( is_user_logged_in() ) : ?>
				
					<p><?php printf(__('Logado como <a href="%1$s">%2$s</a>.'), get_option('siteurl') . '/wp-admin/profile.php', $user_identity); ?> <a href="<?php echo wp_logout_url(get_permalink()); ?>" title="<?php _e('Sair'); ?>"><?php _e('Sair &raquo;', 'kubrick'); ?></a></p>
				
				<?php else : ?>
					<fieldset class="deslogado">
						<label>Nome (obrigatório)</label>
						<input type="text" name="author" class="fld" id="author" value="<?php echo esc_attr($comment_author); ?>" size="22" tabindex="1" <?php if ($req) echo "aria-required='true'"; ?> />
						
						<label>Email (não será publicado) (obrigatório)</label>
						<input type="text" name="email" class="fld" id="email" value="<?php echo esc_attr($comment_author_email); ?>" size="22" tabindex="2" <?php if ($req) echo "aria-required='true'"; ?> />
					
						<label>Website</label>
						<input type="text" name="url" class="fld" id="url" value="<?php echo  esc_attr($comment_author_url); ?>" size="22" tabindex="3" />
					</fieldset>
				<?php endif; ?>
				<fieldset>
					<label>Comentário</label>
					<textarea name="comment" id="comment" cols="100%" rows="10" tabindex="4"></textarea>
				</fieldset>
				
				<fieldset class="send-button-container">
					<input type="submit" class="button" value="Enviar"/>
					<?php comment_id_fields(); ?> 
				</fieldset>
				<?php do_action('comment_form', $post->ID); ?>
		</form>
		
		<?php endif; ?>
	</div>
	
<?php endif; ?>