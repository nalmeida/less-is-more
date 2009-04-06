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
	 * Interface.Config
	 * @since 08/07/2008
	 * @version 1.0
	 * @author Regis Bittencourt - rbittencourt@fbiz.com.br * mod for 1.1, Marcelo Miranda Carneiro - mcarneiro@gmail.com 
	 */
	public class Config{
	    /**
		 * Interface.Config.Title
		 * @return String the project title name
		 * @usage
				<code>
					<%=Interface.Config.Title%> //writes " - ##PAGE_TITLE##" on the page.
				</code>
		 */
		public static string Title{
	        get{
	            return Get("Title");
	        }
	    }

		/**
		 * Interface.Config.Title
		 * @return Any string stored in Web.config
		 * @usage
				<code>
					<%=Interface.Config.Get("dummy")%> //writes the value 0 from key dummy in web.config: <add key="dummy" value="0"/>
				</code>
		 */
		public static string Get(string key)
		{
			return ConfigurationSettings.AppSettings.Get(key);
		}
	}
}
