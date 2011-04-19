using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Caching;
using System.Web;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;

namespace Common
{


	/**
	 * Common.Util
	 * @since 08/07/2008
	 * @version 1.0.2
	 * @author Regis Bittencourt - rbittencourt@fbiz.com.br, Marcelo Miranda Carneiro - mcarneiro@gmail.com
	 */

	/// Métodos úteis a todas as classes
	public static partial class Util
	{

		private static string Slash {
			get {
				string app = HttpContext.Current.Request.ApplicationPath;
				return (app[app.Length - 1].ToString() == "/" ? "" : "/");
			}
		}

		private static string Port {
			get {
				HttpContext curr = HttpContext.Current;
				return (curr.Request.Url.Port.ToString() == "80") ? "" : (":" + curr.Request.Url.Port);
			}
		}

		/**
		 * @return String the project root path
		 */
		public static string Root {
			get {
				HttpContext curr = HttpContext.Current;
				return curr.Request.Url.Scheme + "://" +
					curr.Request.Url.Host + Port +
					curr.Request.ApplicationPath + Slash;
			}
		}
		
		/**
		 * @return String the assets (jpg, swf, xml, etc) "root". Defined in Web.Config "ROOT-ASSETS" key. If not defined, uses Config.Util.Root
		 */
		public static string AssetsRoot {
			get {
				string ra = ConfigurationManager.AppSettings["ROOT-ASSETS"];
				return (!string.IsNullOrEmpty(ra) ? ra : Root);
			}
		}
		
		/**
		 * @return String the "upload root" (jpg, swf, xml, etc uploaded from a admin). Defined in Web.Config "ROOT-UPLOADS" key. If not defined, uses Config.Util.AssetsRoot
		 */
		public static string UploadsRoot {
			get {
				string ru = ConfigurationManager.AppSettings["ROOT-UPLOADS"];
				return (!string.IsNullOrEmpty(ru) ? ru : AssetsRoot);
			}
		}
		
		/**
		 * @return String the global path for external files (xml, css, jgs, gif, swf)
		 */
		public static string GlobalPath {
			get {
				return AssetsRoot + "locales/global/";
			}
		}

		/**
		 * @return String the especific path for external files (xml, css, jpg, gif, swf)
		 */
		public static string LanguagePath {
			get {
				return AssetsRoot + "locales/pt-BR/";
			}
		}
		
		/**
		 * @return String the global path for external dynamic files (xml, css, jgs, gif, swf uploaded from a admin or in another server)
		 */
		public static string GlobalUploadPath {
			get {
				return UploadsRoot + "locales/global/uploads/";
			}
		}

		/**
		 * @return String the especific path for external files (xml, css, jpg, gif, swf)
		 */
		public static string LanguageUploadPath {
			get {
				return UploadsRoot + "locales/" + Language + "/uploads/";
			}
		}

		/**
		 * @return String the especific path for external files (xml, css, jpg, gif, swf)
		 */
		public static string Language {
			get {
				return "pt-BR";
			}
		}

		/**
		 * @see RegularExpression from: http://www.geekzilla.co.uk/View0CBFD9A7-621D-4B0C-9554-91FD48AADC77.htm
		 * @return Boolean if the website is running on "http://localhost" or under an IP Address "111.111.11.11"
		 */
		public static bool isLocal {
			get {
				Regex reg = new Regex(@"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
				string siteDomain = reg.Match(Common.Util.Root).ToString();
				return siteDomain == "" ? true : !Common.Util.Root.Contains(".") ;
			}
		}

		/**
		 * @return String For bypass-cache
		 */
		public static string Bpc {
			get {
				return HttpContext.Current.Request.QueryString["bpc"];
			}
		}

		/**
		 * @return String Version defined random number when bpc=[ANY] or pre-defined value on web.config
		 */
		public static string Version {
			get {
				return !string.IsNullOrEmpty(Bpc) ? new Random().Next(100000000).ToString() : ConfigurationManager.AppSettings["CURRENT-VERSION"];
			}
		}

		public static string RawUrl {
			get {
				string ret = HttpContext.Current.Request.RawUrl.Substring(HttpContext.Current.Request.ApplicationPath.Length);
				return !string.IsNullOrEmpty(ret) && ret.EndsWith("/") ? ret : ret + "/";
			}
		}
		
		public static string HttpPost(string uri) {
			return HttpPost(uri, 8000);
		}
		public static string HttpPost(string uri, int Timeout) {
			HttpWebRequest webRequest;
			StreamReader cStream = null;
			try {
				webRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
				webRequest.KeepAlive = true;
				webRequest.Connection = "keepalive";
				webRequest.ContentType = "application/x-www-form-urlencoded";
				webRequest.Method = "GET";
		
				webRequest.Timeout = Timeout;
		
				using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse()) {
					if (webResponse == null) {
						return null;
					}
					cStream = new StreamReader(webResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
					return cStream.ReadToEnd();
				}
			} catch (Exception) {
				return "";
			}
		}
		
		private static bool IsGZipSupported() {
			try {
				string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
				if (!string.IsNullOrEmpty(AcceptEncoding))
					if (AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate"))
						return true;
			} catch { }
			return false;
		}
		public static void GZipEncodePage() {
			try {
				if (!IsGZipSupported()){
					return;
				}
				string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
				HttpResponse Response = HttpContext.Current.Response;

				if (AcceptEncoding.Contains("gzip")) {
					Response.Filter = new System.IO.Compression.GZipStream(Response.Filter, System.IO.Compression.CompressionMode.Compress);
					Response.AppendHeader("Content-Encoding", "gzip");
				} else {
					Response.Filter = new System.IO.Compression.DeflateStream(Response.Filter, System.IO.Compression.CompressionMode.Compress);
					Response.AppendHeader("Content-Encoding", "deflate");
				}
			} catch { }
		}
	}
}