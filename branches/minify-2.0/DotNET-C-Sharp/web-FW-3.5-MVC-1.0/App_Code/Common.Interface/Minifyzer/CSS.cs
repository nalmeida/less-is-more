﻿using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Caching;
using System.Text.RegularExpressions;

namespace Common.Minifyzer {
	
	public class CSS : IFile {

		public string Tag { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public string PhisicalFolder { get; set; }
		public string Content { get; set; }
		public HttpContext context { get { return HttpContext.Current; } }
		public string BaseFolder { get; set; }
		public CacheDependency FileCacheDependency { get; set; }
		public DateTime LastModified { get; set; }

		public CSS(){
			Tag = "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" media=\"screen\" />";
		}

		public void LoadFile(string file, string id)
		{
			if (string.IsNullOrEmpty(BaseFolder))
			{
				BaseFolder = context.Server.MapPath(context.Request.ApplicationPath) + "/locales/global/css/";
			}
			Name = file;
			Id = id;
			ReadFile();
		}
		public void LoadFile(string file, string FilePath, string id)
		{
			if (FilePath.ToLower().StartsWith("http://") || FilePath.ToLower().StartsWith("https://"))
			{
				BaseFolder = FilePath;
			}
			else
			{
				BaseFolder = context.Server.MapPath(FilePath);
			}

			LoadFile(file, id);
		}


		public void Filter()
		{
			Content = "\n\n /* " + Name + " | " + LastModified + " */ \n\n" + Content;

			Content = Content.Replace("$root/", Common.Util.AssetsRoot);
			Content = Content.Replace("$global/", Common.Util.GlobalPath);
			Content = Content.Replace("$language/", Common.Util.LanguagePath);
			Content = Content.Replace("$upload-root/", Common.Util.UploadsRoot);
			Content = Content.Replace("$upload-global/", Common.Util.GlobalUploadPath);
			Content = Content.Replace("$upload-language/", Common.Util.LanguageUploadPath);

			Dictionary<string, string> dicVariables = new Dictionary<string, string>();

			const string varRegEx = @"\$(?<varname>[^{}$]*){(?<varvalue>[^}$]*)}";
			const RegexOptions varRegExOptions = RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;

			MatchCollection variables = Regex.Matches(Content, varRegEx, varRegExOptions); //finds all variables in the css document
			foreach (Match match in variables)
			{
				dicVariables.Add(match.Groups["varname"].Value.Trim(), match.Groups["varvalue"].Value.Trim());	//stores it in a dictionary for a later use
			}
			Content = Regex.Replace(Content, varRegEx + @"[^\r\n]*[\r\n]", string.Empty, varRegExOptions); // removes all variables to clean the css

			foreach (string varname in dicVariables.Keys)
			{
				Content = Content.Replace("$" + varname, dicVariables[varname]);  //replaces each variable with its value
			}

			//if (Files["v"] != null)
			//{
			//	  stContent = Regex.Replace(stContent, "url\\((.[^\\)]*\\?.*)\\)", "url($1&v=" + Files["v"] + ")"); // replace all images paths with a query adding the &v=version 
			//	  stContent = Regex.Replace(stContent, "url\\((.[^\\)\\?]*)\\)", "url($1?v=" + Files["v"] + ")"); // replace all images paths adding the ?v=version 
			//}
		}

		private void ReadFile()
		{
			if (BaseFolder.ToLower().StartsWith("http://") || BaseFolder.ToLower().StartsWith("https://"))
			{
				Content = Common.Util.HttpPost(BaseFolder + Name);
			}
			else
			{
				FileCacheDependency = new CacheDependency(BaseFolder + Name);
				using (StreamReader srContent = new StreamReader(BaseFolder + Name, Encoding.GetEncoding("utf-8")))
				{
					Content = srContent.ReadToEnd();
				}
			}
		}

	}
}