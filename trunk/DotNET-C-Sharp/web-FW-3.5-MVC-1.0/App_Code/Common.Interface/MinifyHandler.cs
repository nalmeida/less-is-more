//<%@ WebHandler Language="C#" Class="Common.Minify" %>
//uncomment this line above if you want to use as a standalone file. also rename it as minify.ashx and place it at root folder

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq;

/* 
Minify

@author Marcelo Miranda Carneiro - mail: mcarneiro@gmail.com. Thanks to Leandro Ribeiro and Nicholas Almeida.
* updated Regis Bittencourt - changed into a http handler .net class. 
@since 04/01/2008
@version 2.0.2
@usage
	<code>
		// just call the Javascript file or CSS file as parameters:
		<script type="text/javascript" src="minify.aspx?file1.js|file2.js"><\/script>
		<style type="text/css" src="minify.aspx?file1.css|file2.css"></style>
	</code>
*/


namespace Common
{
	public class MinifyHandler : IHttpHandler
	{

		private HttpServerUtility _server;
		public HttpServerUtility Server
		{
			get { return _server; }
			set { _server = value; }
		}


		private HttpRequest _request;
		public HttpRequest Request
		{
			get { return _request; }
			set { _request = value; }
		}


		private HttpResponse _response;
		public HttpResponse Response
		{
			get { return _response; }
			set { _response = value; }
		}
		
		private string assetsFolderName
		{
			get {
				string fn = ConfigurationManager.AppSettings["ASSETS-FOLDER"];
				if(string.IsNullOrEmpty(fn)){
					fn = "assets";
				}
				return fn;
			}
		}

		public void DoCache(HttpContext context, DateTime lastModifiedUnc)
		{
			try
			{
				string sDtModHdr = Request.Headers.Get("If-Modified-Since");
				// does header contain If-Modified-Since?
				if (!string.IsNullOrEmpty(sDtModHdr))
				{

					sDtModHdr = sDtModHdr.Split(';')[0];
					// convert to UNC date
					DateTime dtModHdrUnc = Convert.ToDateTime(sDtModHdr).ToUniversalTime();
					dtModHdrUnc = dtModHdrUnc.AddMilliseconds(dtModHdrUnc.Millisecond * -1);
					lastModifiedUnc = lastModifiedUnc.ToUniversalTime();
					lastModifiedUnc = lastModifiedUnc.AddMilliseconds(lastModifiedUnc.Millisecond * -1);

					// if it was within the last month, return 304 and exit
					if (DateTime.Compare(
						new DateTime(
						dtModHdrUnc.Year,
						dtModHdrUnc.Month,
						dtModHdrUnc.Day,
						dtModHdrUnc.Hour,
						dtModHdrUnc.Minute,
						dtModHdrUnc.Second),
						new DateTime(
						lastModifiedUnc.Year,
						lastModifiedUnc.Month,
						lastModifiedUnc.Day,
						lastModifiedUnc.Hour,
						lastModifiedUnc.Minute,
						lastModifiedUnc.Second)) == 0)
					{
						Response.StatusCode = 304;
						Response.StatusDescription = "Not Modified";
						Response.CacheControl = "public";
						Response.End();
					}
				}
				Response.Cache.SetLastModified(lastModifiedUnc);
				Response.CacheControl = "public";
			}
			catch
			{ }
		}

		public void ProcessRequest(HttpContext context)
		{
			Request = context.Request;
			Response = context.Response;
			Server = context.Server;


			// READING FILES
			StringBuilder sbToStrip = new StringBuilder();
			bool isScript = false;

			string arquivos = Request.QueryString[0];

			string[] vtArquivo = arquivos.Split(Convert.ToChar("|"));

			Encoding utf8 = Encoding.GetEncoding("utf-8");
			StreamReader srArquivo;
			DateTime lastModifiedFileGlobal = DateTime.MinValue;

			string assetsFolder = assetsFolderName;
			string filePath;
			DateTime fileLastModified;
			
			string extension = Path.GetExtension(Server.MapPath("~") + "\\" + vtArquivo[0].Replace("/", "\\").ToLower());

			switch(extension){
				// CSS
				case ".css":
					foreach (string stNomeArquivo in vtArquivo)
					{
						Response.ContentType = "text/css";

						string file = stNomeArquivo;
						filePath = Server.MapPath("~/"+assetsFolder+"/css/") + file;

						fileLastModified = File.GetLastWriteTime(filePath);
						lastModifiedFileGlobal = fileLastModified > lastModifiedFileGlobal ? fileLastModified : lastModifiedFileGlobal;

						if (Path.GetExtension(filePath) == ".css")
						{
							try
							{
								srArquivo = new StreamReader(filePath, utf8);
								sbToStrip.Append(Environment.NewLine);
								sbToStrip.Append("/*************** File loaded successfully: \"" + file + "\" ***************/");
								sbToStrip.Append(Environment.NewLine);
								sbToStrip.Append(Environment.NewLine);
								sbToStrip.Append(srArquivo.ReadToEnd());
								sbToStrip.Append(Environment.NewLine);
								srArquivo.Close();
							}
							catch
							{
								sbToStrip.Append("/* ERROR: Missing file " + file + " */");
							}
						}
					}
				break;

				// JS
				case ".js":
					Response.ContentType = "text/javascript";
					isScript = true;
					foreach (string stNomeArquivo in vtArquivo)
					{
						filePath = Server.MapPath("~/"+assetsFolder+"/js/") + stNomeArquivo;

						fileLastModified = File.GetLastWriteTime(filePath);
						lastModifiedFileGlobal = fileLastModified > lastModifiedFileGlobal ? fileLastModified : lastModifiedFileGlobal;

						if (Path.GetExtension(filePath) == ".js")
						{					
							srArquivo = new StreamReader(filePath, utf8);
							sbToStrip.Append(srArquivo.ReadToEnd());
							sbToStrip.Append(Environment.NewLine);
							srArquivo.Close();
						}
					}
				break;
			}

			/**
			 * DO REPLACEMENT
			 */
			string stContent = sbToStrip.ToString();

			switch(extension)
			{
				// CSS
				case ".css":
					// replaces
					stContent = parseVars(stContent, @"\$(?<globalVar>[A-z0-9_]+?)/", "${0}/");

					// replace custom CSS vars
					Dictionary<string, string> dicVariables = new Dictionary<string, string>();

					const string varRegEx = @"\$(?<varname>[^{}$]*){(?<varvalue>[^}$]*)}";
					const RegexOptions varRegExOptions = RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;

					MatchCollection variables = Regex.Matches(stContent, varRegEx, varRegExOptions); //finds all variables in the css document
					foreach (Match match in variables)
					{
						dicVariables.Add(match.Groups["varname"].Value.Trim(), match.Groups["varvalue"].Value.Trim()); //stores it in a dictionary for a later use
					}
					stContent = Regex.Replace(stContent, varRegEx + @"[^\r\n]*[\r\n]", string.Empty, varRegExOptions); // removes all variables to clean the css

					foreach (string varname in dicVariables.Keys)
					{
						stContent = stContent.Replace("$" + varname, dicVariables[varname]);  //replaces each variable with its value
					}

					// apply util.version in CSS urls()
					if (!string.IsNullOrEmpty(Util.Version))
					{
						stContent = Regex.Replace(stContent, "url\\((\"|'|)(.[^\\)]*\\?.*?)\\1\\)", "url($1$2&v=" + Util.Version + "$1)"); // replace all images paths with a query adding the &v=version 
						stContent = Regex.Replace(stContent, "url\\((\"|'|)(.[^\\)\\?]*?)\\1\\)", "url($1$2?v=" + Util.Version + "$1)"); // replace all images paths adding the ?v=version 
					}

					break;
			}

			// CACHE
			if (string.IsNullOrEmpty(Util.Bpc))
			{
				DoCache(context, lastModifiedFileGlobal);
			}

			// GZIP ENCODE
			Util.GZipEncodePage();

			//OUTPUT
			Response.Write(stContent);
		}

		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
		
		/**
		 * parse content using reflection (common.util will be available inside these files)
		 */
		public static string parseVars(string content, string regex, string delimiter)
		{
			string varRegEx = regex;
			string currentKey = String.Empty;

			Dictionary<string, string> dicVariables = new Dictionary<string, string>();

			RegexOptions varRegExOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase;
			MatchCollection variables = Regex.Matches(content, varRegEx, varRegExOptions);
			PropertyInfo[] pinfo = typeof(global::Common.Util).GetProperties();

			foreach (Match match in variables)
			{
				currentKey = match.Groups["globalVar"].Value.Trim();
				if (!dicVariables.ContainsKey(currentKey))
				{
					var pi = (from x in pinfo where x.Name.ToLower() == currentKey.ToLower() select x).FirstOrDefault();
					if (pi != null)
					{
						dicVariables.Add(currentKey, pi.GetValue(null, null).ToString());
					}
				}
			}

			foreach (string varname in dicVariables.Keys)
			{
				content = content.Replace(string.Format(delimiter, varname), dicVariables[varname]); //replaces each variable with its value
			}

			return content;
		}

	}
}