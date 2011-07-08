<!-- Accordion Menu -->	 
<ul id="main-nav">
	<li>
		<a href="<%=Common.Util.Root%>Home" class="nav-top-item no-submenu"> <!-- Add the class "no-submenu" to menu items with no sub menu -->
			Home
		</a>	   
	</li>

	<li> 
		<a href="#" class="nav-top-item current"> <!-- Add the class "current" to current menu item -->
		Teste Submenus
		</a>
		<ul style="display: none;">
			<li><a href="#">Link</a></li>
			<li><a class="current" href="<%=Common.Util.Root%>Usage">Como Usar</a></li> <!-- Add class "current" to sub menu items also -->
		</ul>
	</li>
</ul>
