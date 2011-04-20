using System;
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
		public string BaseFolderRoot { get; set; }
		public string BaseFolderAsset { get; set; }
		public string Url { get; set; }
		public CacheDependency FileCacheDependency { get; set; }
		public DateTime LastModified { get; set; }

		public CSS(){
			Tag = "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" media=\"screen\" />";
		}

		public void LoadFile(string file, string id) {
			if (string.IsNullOrEmpty(BaseFolderRoot)) {
				BaseFolderAsset = "locales/global/css/";
				BaseFolderRoot = context.Server.MapPath(context.Request.ApplicationPath) + "/";
			}
			Name = file;
			Id = id;
			ReadFile();
		}
		public void LoadFile(string file, string UrlPath, string id) {
			Url = UrlPath;
			LoadFile(file, id);
		}

		public string GetVirtualPath(){
			// TODO: Get global / languagePath
			return Url != null ? Url : Common.Util.Root + BaseFolderAsset + Name;
		}
		public string GetPhysicalPath(){
			return Url != null ? Url : BaseFolderRoot + BaseFolderAsset + Name;
		}
		
		public void Filter() {
			
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
			foreach (Match match in variables) {
				dicVariables.Add(match.Groups["varname"].Value.Trim(), match.Groups["varvalue"].Value.Trim());	//stores it in a dictionary for a later use
			}
			Content = Regex.Replace(Content, varRegEx + @"[^\r\n]*[\r\n]", string.Empty, varRegExOptions); // removes all variables to clean the css

			foreach (string varname in dicVariables.Keys) {
				Content = Content.Replace("$" + varname, dicVariables[varname]);  //replaces each variable with its value
			}

			//if (Files["v"] != null)
			//{
			//	  stContent = Regex.Replace(stContent, "url\\((.[^\\)]*\\?.*)\\)", "url($1&v=" + Files["v"] + ")"); // replace all images paths with a query adding the &v=version 
			//	  stContent = Regex.Replace(stContent, "url\\((.[^\\)\\?]*)\\)", "url($1?v=" + Files["v"] + ")"); // replace all images paths adding the ?v=version 
			//}
		}

		private void ReadFile() {
			if (!string.IsNullOrEmpty(Url)) {
				Content = Common.Util.HttpPost(Url, 2000);
				if(string.IsNullOrEmpty(Content)){
					ReadLocalFile();
				}
			} else {
				ReadLocalFile();
			}
		}
		private void ReadLocalFile(){
			FileCacheDependency = new CacheDependency(BaseFolderRoot + BaseFolderAsset + Name);
			using (StreamReader srContent = new StreamReader(BaseFolderRoot + BaseFolderAsset + Name, Encoding.GetEncoding("utf-8"))) {
				Content = srContent.ReadToEnd();
			}
		}

	}
}