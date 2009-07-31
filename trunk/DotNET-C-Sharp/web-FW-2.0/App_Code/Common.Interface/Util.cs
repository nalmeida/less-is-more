using System;
using System.Web;

namespace Common
{


    /**
     * Common.Util
     * @since 08/07/2008
     * @version 1.0.1
     * @author Regis Bittencourt - rbittencourt@fbiz.com.br, Marcelo Miranda Carneiro - mcarneiro@gmail.com
     */

    /// M�todos �teis a todas as classes
    public static partial class Util
    {

        private static string Slash
        {
            get
            {
                return (HttpContext.Current.Request.ApplicationPath[HttpContext.Current.Request.ApplicationPath.Length - 1].ToString() == "/" ? "" : "/");
            }
        }

        private static string Port
        {
            get
            {
                return (HttpContext.Current.Request.Url.Port.ToString() == "80") ? "" : (":" + HttpContext.Current.Request.Url.Port);
            }
        }

        /**
         * Common.Util.Root Returns the project root path
         * @return String the project root path
         * @usage
                <code>
                    <%=Common.Util.Root;%> // writes "http://ROOT/"
                </code>
         */
        public static string Root
        {
            get
            {
                return HttpContext.Current.Request.Url.Scheme + "://" +
                    HttpContext.Current.Request.Url.Host + Port +
                    HttpContext.Current.Request.ApplicationPath + Slash;
            }
        }

        /**
         * Common.Util.GlobalPath Returns the global path for external files (xml, css, jpg, gif, swf)
         * @return String the global path for external files (xml, css, jgs, gif, swf)
         * @usage
                <code>
                    <%=Common.Util.GlobalPath;%> // writes "http://ROOT/locales/global/"
                </code>
         */
        public static string GlobalPath
        {
            get
            {
                return Root + "locales/global/";
            }
        }

        /**
         * Common.Util.LanguagePath 
         * @return String the especific path for external files (xml, css, jpg, gif, swf)
         * @usage
                <code>
                    <%=Common.Util.LanguagePath;%> // writes "http://ROOT/locales/pt-BR/"
                </code>
         */
        public static string LanguagePath
        {
            get
            {
                return Root + "locales/pt-BR/";
            }
        }

        /**
         * Common.Util.Bpc 
         * @return String "&bpc=1" if parameter bpc=1 is passed at the site URL
         * @usage
                <code>
                    <%=Common.Util.Bpc;%>
                </code>
         */
        public static string Bpc
        {
            get
            {
                return HttpContext.Current.Request.QueryString["bpc"];
            }
        }

        /**
         * Common.Util.Version 
         * @return String "X.X.X"
         * @usage
                <code>
                    <%=Common.Util.Version;%>
                </code>
         */
        public static string Version
        {
            get
            {
                string version = "1.0.0";

                if (!string.IsNullOrEmpty(Bpc))
                {
                    version = new Random().Next(100000000).ToString();
                }
                return version;
            }
        }

        /**
         * Common.Util.Language 
         * @return String the especific path for external files (xml, css, jpg, gif, swf)
         * @usage
                <code>
                    <%=Common.Util.Language;%> // writes "pt-BR"
                </code>
         */
        public static string Language
        {
            get
            {
                return "pt-BR";
            }
        }

        private static bool IsGZipSupported()
        {
            try
            {
                string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
                if (!string.IsNullOrEmpty(AcceptEncoding))
                    if (AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate"))
                        return true;
            }
            catch { }
            return false;
        }
        public static void GZipEncodePage()
        {
            try
            {
                if (!IsGZipSupported()) return;
                string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
                HttpResponse Response = HttpContext.Current.Response;
                if (AcceptEncoding.Contains("gzip"))
                {
                    Response.Filter = new System.IO.Compression.GZipStream(Response.Filter, System.IO.Compression.CompressionMode.Compress);
                    Response.AppendHeader("Content-Encoding", "gzip");
                }
                else
                {
                    Response.Filter = new System.IO.Compression.DeflateStream(Response.Filter, System.IO.Compression.CompressionMode.Compress);
                    Response.AppendHeader("Content-Encoding", "deflate");
                }
            }
            catch { }
        }
    }
}