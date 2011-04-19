<li <?php comment_class(); ?> id="comment-<?php comment_ID() ?>">
	<div id="comment-<?php comment_ID(); ?>">
		<div class="comment-author vcard">
			<cite class="fn"><?php comment_author();?></cite> <span class="says">disse:</span>
		</div>
		<?php if ($comment->comment_approved == '0') : ?>
			<em>Seu commentário está aguardando moderação</em>
			<br />
		<?php endif; ?>
		<div class="comment-meta commentmetadata">
			<a href="<?php echo htmlspecialchars( get_comment_link( $comment->comment_ID ) ) ?>"></a><?php edit_comment_link(__('(Edit)'),'  ','') ?></div>
			<?php comment_text() ?>
		</div>
     
<?php // Não feche o elemento li. O próprio Wordpress fará isso.