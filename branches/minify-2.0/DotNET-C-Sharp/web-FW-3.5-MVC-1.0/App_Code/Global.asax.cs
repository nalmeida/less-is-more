using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
// visit http://go.microsoft.com/?LinkId=9394801

public class MvcApplication : System.Web.HttpApplication
{
	public static void RegisterRoutes(RouteCollection routes)
	{
		routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
		routes.IgnoreRoute(Common.Minify.GetHandlerPath());
		routes.IgnoreRoute("fakeimage.aspx");
		routes.IgnoreRoute("sitemap.xml");

		/* Novas páginas aqui */

		/* Fim das páginas novas */

		routes.MapRoute(
			"Default",													// Route name
			"{controller}/{action}/{id}",								// URL with parameters
			new { controller = "Default", action = "Index", id = "" }	// Parameter defaults
		);

	}
	public static void RegisterFiles()
	{
		
		// CSS
		Common.Minify.Register("Reset", "reset.css");
		Common.Minify.Add("StructureCSS", "Reset");
	}

	protected void Application_Start()
	{
		RegisterRoutes(RouteTable.Routes);
		RegisterFiles();
	}
}