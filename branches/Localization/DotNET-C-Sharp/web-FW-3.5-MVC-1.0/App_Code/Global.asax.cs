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
        routes.IgnoreRoute("minify.aspx");
        routes.IgnoreRoute("fakeimage.aspx");
        routes.IgnoreRoute("sitemap.xml");
        routes.IgnoreRoute("alphaminify.aspx");
        

        /* Novas páginas aqui */

        /* Fim das páginas novas */

        routes.MapRoute(
            "Default",                                              // Route name
            "{controller}/{action}/{id}",                           // URL with parameters
            new { controller = "Default", action = "Index", id = "" }  // Parameter defaults
        );

    }
    public static void RegisterFiles()
    {
        
        Minify.Minifyzer.Register("JQuery", "jquery.js");
        Minify.Minifyzer.Register("SwfObject", "swfobject.js");
        Minify.Minifyzer.Register("FlashStructure", "flash-structure.css");
        Minify.Minifyzer.Register("Reset", "reset.css");
        Minify.Minifyzer.Register("TerraJS", "capa.js", "http://s1.trrsf.com.br/metrics/js/br/");

         
        Minify.Minifyzer.Add("GaleraDoHino", "JQuery");
        Minify.Minifyzer.Add("GaleraDoHino", "SwfObject");
        Minify.Minifyzer.Add("GaleraDoHino", "TerraJS");
    }

    protected void Application_Start()
    {
        RegisterRoutes(RouteTable.Routes);
        RegisterFiles();
    }
}