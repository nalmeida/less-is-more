using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Interface{
	/**
	 * Interface.Util
	 * @since 08/07/2008
	 * @version 1.0
	 * @author Regis Bittencourt - rbittencourt@fbiz.com.br, Marcelo Miranda Carneiro - mcarneiro@gmail.com
	 */
	public class Util{
		
		private static string Slash{
			get{
				return (HttpContext.Current.Request.ApplicationPath.ToString()[HttpContext.Current.Request.ApplicationPath.ToString().Length-1].ToString() == "/" ? "" : "/");
			}
		} 
		
		private static string Port{
	        get{
	            return (HttpContext.Current.Request.Url.Port.ToString() == "80") ? "" : (":" + HttpContext.Current.Request.Url.Port.ToString());
	        }
		}
	
	    /**
		 * Interface.Util.Root Returns the project root path
		 * @return String the project root path
		 * @usage
				<code>
					<%=Interface.Util.Root;%> // writes "http://ROOT/"
				</code>
		 */
	    public static string Root{
	        get{
	            return HttpContext.Current.Request.Url.Scheme + "://" +
	                HttpContext.Current.Request.Url.Host + Port + 
	                HttpContext.Current.Request.ApplicationPath + Slash;
	        }
	    }
	    /**
		 * Interface.Util.GlobalPath Returns the global path for external files (xml, css, jpg, gif, swf)
		 * @return String the global path for external files (xml, css, jgs, gif, swf)
		 * @usage
				<code>
					<%=Interface.Util.GlobalPath;%> // writes "http://ROOT/locales/global/"
				</code>
		 */
	    public static string GlobalPath{
	        get{
	            return Root + "locales/global/";
	        }
	    }
	    /**
		 * Interface.Util.LanguagePath 
		 * @return String the especific path for external files (xml, css, jpg, gif, swf)
		 * @usage
				<code>
					<%=Interface.Util.LanguagePath;%> // writes "http://ROOT/locales/pt-BR/"
				</code>
		 */
	    public static string LanguagePath{
	        get{
	            return Root + "locales/"+ Language + "/";
	        }
	    }
		/**
		 * Interface.Util.Bpc 
		 * @return String "&bpc=1" if parameter bpc=1 is passed at the site URL
		 * @usage
				<code>
					<%=Interface.Util.Bpc;%>
				</code>
		 */
		public static string Bpc {
			get {
				string bpc = string.Empty;
				if(HttpContext.Current.Request.QueryString["bpc"] != ""){
					bpc = "&bpc="+HttpContext.Current.Request.QueryString["bpc"];
				}
			
				return bpc;
			}
		}
		/**
		 * Interface.Util.Version 
		 * @return String "X.X.X"
		 * @usage
				<code>
					<%=Interface.Util.Version;%>
				</code>
		 */
		public static string Version {
			get {
				string version = "1.0.0";
				
				if(Interface.Util.Bpc != string.Empty) {
					version = new Random().Next(100000000).ToString();
				}
				return version;
			}
		}
	    /**
		 * Interface.Util.Language 
		 * @return String the especific path for external files (xml, css, jpg, gif, swf)
		 * @usage
				<code>
					<%=Interface.Util.Language;%> // writes "pt-BR"
				</code>
		 */
	    public static string Language{
	        get{
	            return ConfigurationSettings.AppSettings.Get("Language");
	        }
	    }
	}
}