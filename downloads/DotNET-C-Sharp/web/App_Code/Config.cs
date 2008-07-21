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
	 * COMMON.Config
	 * @since 08/07/2008
	 * @version 1.0
	 * @author Regis Bittencourt - rbittencourt@fbiz.com.br, Marcelo Miranda Carneiro - mcarneiro@gmail.com
	 */
	public static class Config{
	    /**
		 * COMMON.Config.Title
		 * @return String the project title name
		 * @usage
				<code>
					<%=COMMON.Config.Title;%> //writes " - ##PAGE_TITLE##" on the page.
				</code>
		 */
		public static string Title{
	        get{
	            return " - ##PAGE_TITLE##";
	        }
	    }
	}
}
