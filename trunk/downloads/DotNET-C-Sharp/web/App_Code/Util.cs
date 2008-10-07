using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace COMMON{
	/**
	 * COMMON.Util
	 * @since 08/07/2008
	 * @version 1.0.1
	 * @author Regis Bittencourt - rbittencourt@fbiz.com.br, Marcelo Miranda Carneiro - mcarneiro@gmail.com
	 */
	public static class Util{
		
		private static string Slash{
			get{
				return (HttpContext.Current.Request.ApplicationPath.ToString()[HttpContext.Current.Request.ApplicationPath.ToString().Length-1].ToString() == "/" ? "" : "/");
			}
		} 
		
		private static string Port{
	        get{
	            return ":" + HttpContext.Current.Request.Url.Port.ToString();
	        }
		}
	
	    /**
		 * COMMON.Util.Root Returns the project root path
		 * @return String the project root path
		 * @usage
				<code>
					<%=COMMON.Util.Root%> // writes "http://ROOT/"
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
		 * COMMON.Util.GlobalPath Returns the global path for external files (xml, css, jpg, gif, swf)
		 * @return String the global path for external files (xml, css, jgs, gif, swf)
		 * @usage
				<code>
					<%=COMMON.Util.GlobalPath%> // writes "http://ROOT/locales/global/"
				</code>
		 */
	    public static string GlobalPath{
	        get{
	            return Root + "locales/global/";
	        }
	    }
	    /**
		 * COMMON.Util.LanguagePath 
		 * @return String the especific path for external files (xml, css, jpg, gif, swf)
		 * @usage
				<code>
					<%=COMMON.Util.LanguagePath%> // writes "http://ROOT/locales/pt-BR/"
				</code>
		 */
	    public static string LanguagePath{
	        get{
	            return Root + "locales/pt-BR/";
	        }
	    }
	    /**
		 * COMMON.Util.Language 
		 * @return String the especific path for external files (xml, css, jpg, gif, swf)
		 * @usage
				<code>
					<%=COMMON.Util.Language%> // writes "pt-BR"
				</code>
		 */
	    public static string Language{
	        get{
	            return "pt-BR";
	        }
	    }
	}
}
