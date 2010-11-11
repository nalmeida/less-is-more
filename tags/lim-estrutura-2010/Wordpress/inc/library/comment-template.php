<?php
/**
 * Aplica um template customizado para o comentário
 * @param $comment
 * @param $args
 * @param $depth
 * @return void
 */
function custom_comments($comment, $args, $depth) {
	$GLOBALS['comment'] = $comment; 
	ob_start();
		include(TEMPLATEPATH."/inc/templates/comment.php");
		$out = ob_get_contents();
	ob_clean();
	echo($out);
}