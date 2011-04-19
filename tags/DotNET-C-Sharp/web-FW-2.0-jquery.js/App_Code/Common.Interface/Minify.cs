//<%@ WebHandler Language="C#" Class="Common.Minify" %>
//uncomment this line above if you want to use as a standalone file. also rename it as minify.ashx and place it at root folder

using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

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
		// to call a specific language css
		
		<style type="text/css" src="minify.aspx?file1.css|file2.css,pt-BR"></style>
		// CSSs that will load:
		// <root>/locales/global/css/file1.css
		// <root>/locales/pt-BR/css/file2.css
	</code>
*/


namespace Common
{
	public class Minify : IHttpHandler
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

		public DateTime lastModifiedFileGlobal = DateTime.MinValue;
		public string[] vtArquivo;

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

			String MinifyFiles = Execute(Request.QueryString);

			if (vtArquivo[0].Contains(".js"))
			{
				Response.ContentType = "text/javascript";
			}
			else
			{
				Response.ContentType = "text/css";
			}

			// CACHE
			if (string.IsNullOrEmpty(Util.Bpc))
			{
				DoCache(context, lastModifiedFileGlobal);
			}

			// GZIP ENCODE
			Util.GZipEncodePage();

			//OUTPUT
			Response.Write("/**\n * @author " + Common.Config.AuthorName + " - " + Common.Config.AuthorAddress + "\n */\n");
			Response.Write(MinifyFiles);
		}

		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		public string Execute(String Files, String bpc, String v)
		{
			NameValueCollection MinifyExecutionConfiguration = new NameValueCollection();
			MinifyExecutionConfiguration.Add("Files", Files);
			MinifyExecutionConfiguration.Add("bpc", bpc);
			MinifyExecutionConfiguration.Add("v", v);

			return Execute(MinifyExecutionConfiguration);

		}
		private string Execute(NameValueCollection Files)
		{
			Server = HttpContext.Current.Server;

			// READING FILES
			StringBuilder sbToStrip = new StringBuilder();
			vtArquivo = Files[0].Split(Convert.ToChar("|"));

			Encoding utf8 = Encoding.GetEncoding("utf-8");
			StreamReader srArquivo;
			lastModifiedFileGlobal = DateTime.MinValue;

			string filePath;
			DateTime fileLastModified;

			if (vtArquivo[0].Contains(".css"))
			{
				foreach (string stNomeArquivo in vtArquivo)
				{
					// set folder and file name
					string file;
					string folder;
					if (stNomeArquivo.Contains(","))
					{
						string[] fileFolder = stNomeArquivo.Split(',');
						file = fileFolder[0];
						folder = fileFolder[1];
					}
					else
					{
						file = stNomeArquivo;
						folder = "global";
					}

					if (string.IsNullOrEmpty(folder))
						continue;

					filePath = HttpContext.Current.Request.PhysicalApplicationPath + "locales//" + folder + "//css//" + file;

					fileLastModified = File.GetLastWriteTime(filePath);
					lastModifiedFileGlobal = fileLastModified > lastModifiedFileGlobal ? fileLastModified : lastModifiedFileGlobal;

					try
					{
						srArquivo = new StreamReader(filePath, utf8);
						sbToStrip.Append(Environment.NewLine);
						sbToStrip.Append("/*************** File loaded successfully: \"" + file + "\" / \"" + folder + "\" ***************/");
						sbToStrip.Append(Environment.NewLine);
						sbToStrip.Append(Environment.NewLine);
						sbToStrip.Append(srArquivo.ReadToEnd());
						sbToStrip.Append(Environment.NewLine);
						srArquivo.Close();
					}
					catch
					{
						sbToStrip.Append("/* ERROR:  Missing file " + filePath + " */");
					}
				}
			}
			if (vtArquivo[0].Contains(".js"))
			{
				foreach (string stNomeArquivo in vtArquivo)
				{
					filePath = HttpContext.Current.Request.PhysicalApplicationPath + "js\\" + stNomeArquivo;

					fileLastModified = File.GetLastWriteTime(filePath);
					lastModifiedFileGlobal = fileLastModified > lastModifiedFileGlobal ? fileLastModified : lastModifiedFileGlobal;

					srArquivo = new StreamReader(filePath, utf8);
					sbToStrip.Append(srArquivo.ReadToEnd());
					sbToStrip.Append(Environment.NewLine);
					srArquivo.Close();
				}
			}


			//DO REPLACEMENT
			string stContent = sbToStrip.ToString();

			// CSS
			if (vtArquivo[0].Contains(".css"))
			{
				// replaces
				stContent = stContent.Replace("$root/", Util.AssetsRoot);
				stContent = stContent.Replace("$global/", Util.GlobalPath);
				stContent = stContent.Replace("$language/", Util.LanguagePath);
				stContent = stContent.Replace("$upload-root/", Util.UploadsRoot);
				stContent = stContent.Replace("$upload-global/", Util.GlobalUploadPath);
				stContent = stContent.Replace("$upload-language/", Util.LanguageUploadPath);

				Dictionary<string, string> dicVariables = new Dictionary<string, string>();

				const string varRegEx = @"\$(?<varname>[^{}$]*){(?<varvalue>[^}$]*)}";
				const RegexOptions varRegExOptions = RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;

				MatchCollection variables = Regex.Matches(stContent, varRegEx, varRegExOptions); //finds all variables in the css document
				foreach (Match match in variables)
				{
					dicVariables.Add(match.Groups["varname"].Value.Trim(), match.Groups["varvalue"].Value.Trim());  //stores it in a dictionary for a later use
				}
				stContent = Regex.Replace(stContent, varRegEx + @"[^\r\n]*[\r\n]", string.Empty, varRegExOptions); // removes all variables to clean the css

				foreach (string varname in dicVariables.Keys)
				{
					stContent = stContent.Replace("$" + varname, dicVariables[varname]);  //replaces each variable with its value
				}

				if (Files["v"] != null)
				{
					stContent = Regex.Replace(stContent, "url\\((.[^\\)]*\\?.*)\\)", "url($1&v=" + Files["v"] + ")"); // replace all images paths with a query adding the &v=version 
					stContent = Regex.Replace(stContent, "url\\((.[^\\)\\?]*)\\)", "url($1?v=" + Files["v"] + ")"); // replace all images paths adding the ?v=version 
				}
			}

			return stContent.ToString();
		}
	}
}