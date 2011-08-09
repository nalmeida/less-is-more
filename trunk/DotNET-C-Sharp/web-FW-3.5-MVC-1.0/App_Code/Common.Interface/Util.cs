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
using System.Reflection;
using System.Collections.Generic;

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

		private static HttpContext curr
		{
			get
			{
				return HttpContext.Current;
			}
		}
		private static string Port
		{
			get
			{
				return (curr.Request.Url.Port.ToString() == "80") ? "" : (":" + curr.Request.Url.Port);
			}
		}

		public static string Slash(string value)
		{
			if(value.Length > 0){
				return (value[value.Length - 1].ToString() == "/" ? value : value+"/");
			}
			return "/";
		}

		/**
		 * @return String the project root path
		 */
		public static string Root
		{
			get
			{
				return Domain + Slash(curr.Request.ApplicationPath);
			}
		}
		
		/**
		 * @return String the project domain
		 */
		public static string Domain
		{
			get
			{
				return curr.Request.Url.Scheme + "://" + curr.Request.Url.Host + Port;
			}
		}
		
		/**
		 * @return String the assets (jpg, swf, xml, etc) "root". Defined in Web.Config "ROOT-ASSETS" key.
		 * If not defined, uses Config.Util.Root
		 */
		public static string AssetsRoot
		{
			get
			{
				string ra = ConfigurationManager.AppSettings["ROOT-ASSETS"];
				return (!string.IsNullOrEmpty(ra) ? ra : Root + "assets/");
			}
		}
		
		/**
		 * @return String the "upload root" (jpg, swf, xml, etc uploaded from a admin).
		 * Defined in Web.Config "ROOT-UPLOADS" key. If not defined, uses Config.Util.AssetsRoot
		 */
		public static string UploadsRoot
		{
			get
			{
				string ru = ConfigurationManager.AppSettings["ROOT-UPLOADS"];
				return (!string.IsNullOrEmpty(ru) ? ru : AssetsRoot);
			}
		}
		
		/**
		 * @return String returns the raw path (url - root)
		 */
		public static string RawPath
		{
			get
			{
				return curr.Request.RawUrl.Substring(curr.Request.ApplicationPath.Length);
			}
		}
		
		// public static string GetPath(string value)
		// {
		// 	
		// }
		
		/**
		 * @return String the especific path for external files (xml, css, jpg, gif, swf)
		 */
		public static string Language
		{
			get
			{
				return "pt-BR";
			}
		}

		/**
		 * @return String For is local match (based on web.config)
		 */
		public static bool isLocal
		{
			get
			{
				return ConfigurationManager.AppSettings["IS-LOCAL"] == "1";
			}
		}

		/**
		 * @return String For bypass-cache
		 */
		public static string Bpc
		{
			get
			{
				string q = curr.Request.QueryString["bpc"];
				return  !string.IsNullOrEmpty(q) ? q : Convert.ToString(ConfigurationManager.AppSettings["BPC"]);
			}
		}

		/**
		 * @return String Version defined random number when bpc=[ANY] or pre-defined value on web.config
		 */
		public static string Version
		{
			get
			{
				string v = curr.Request.QueryString["v"];
				return !string.IsNullOrEmpty(Bpc) ? new Random().Next(100000000).ToString() : 
					!string.IsNullOrEmpty(v) ? v : Convert.ToString(ConfigurationManager.AppSettings["CURRENT-VERSION"]);
			}
		}

		public static bool IsAjax
		{
			get
			{
				return curr.Request.Headers["X-Requested-With"] == "XMLHttpRequest" || curr.Request.QueryString["isAjax"] == "1";
			}
		}
		
		public static string HttpPost(string uri)
		{
			return HttpPost(uri, 8000);
		}
		public static string HttpPost(string uri, int Timeout)
		{
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
		
		private static bool IsGZipSupported
		{
			get
			{
				try
				{
					string AcceptEncoding = curr.Request.Headers["Accept-Encoding"];
					if (!string.IsNullOrEmpty(AcceptEncoding))
						if (AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate"))
							return true;
				} catch { }
				return false;
			}
		}
		public static void GZipEncodePage()
		{
			try
			{
				if (!IsGZipSupported)
				{
					return;
				}
				string AcceptEncoding = curr.Request.Headers["Accept-Encoding"];
				HttpResponse Response = curr.Response;

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
			} catch { }
		}
	}
}