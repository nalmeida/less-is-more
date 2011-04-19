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

namespace Common
{
    /**
     * Common.Config
     * @since 08/07/2008
     * @version 1.0
     * @author Regis Bittencourt - rbittencourt@fbiz.com.br, Marcelo Miranda Carneiro - mcarneiro@gmail.com
     */
    public static partial class Config
    {
        /**
         * Common.Config.Title
         * @return String the project title name
         * @usage
                <code>
                    <%=Common.Config.Title%> //writes " - ##PAGE_TITLE##" on the page.
                </code>
         */
        public static string Title
        {
            get
            {
                return " - ##PAGE_TITLE##";
            }
        }

        public static string AuthorName
        {
            get
            {
                return "";
            }
        }

        public static string AuthorAddress
        {
            get
            {
                return "";
            }
        }

        public static string CompanyName
        {
            get
            {
                return "";
            }
        }
    }
}
