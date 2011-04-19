<!-- Sidebar -->
				<div id="sidebar">
					<div class="sidebox">
						<h3>Sidebar</h3>
						<p>Esse sidebar de encontra no arquivo: sidebar.php, nele pode ser colocados widgets no sidebar com id Sidebar1 (no admin)</p>
						<p>Existe a possibilidade de ser criado mais de um sidebar. Para isto é preciso criar um arquivo sidebar_nome.php (subistitua o nome pelo nome do seu sidebar). Achamada para o sidebar fica mudada de get_sidebar(); para get_sidebar('nome');</p>
					</div>
					<?php dynamic_sidebar("Sidebar1");?>
				</div>
				<!-- // Sidebar -->	