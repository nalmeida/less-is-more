using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Caching;
using System.Text.RegularExpressions;
using Common.Minifyzer.Abstracts;
using Common.Minifyzer.Interfaces;

namespace Common.Minifyzer {
	
	public class CSS : AType, IType{
		
		public string Extension { get { return "css"; }}
		public string ContentType { get { return "text/css"; }}
		public string Tag { get { return "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />"; }}

		public void Filter() {
			
			Content = "\n\n /* " + VirtualPath + " | " + LastModified + " */ \n\n" + Content;
			Content = Content
				.Replace("$root/", Common.Util.AssetsRoot)
				.Replace("$global/", Common.Util.GlobalPath)
				.Replace("$language/", Common.Util.LanguagePath)
				.Replace("$upload-root/", Common.Util.UploadsRoot)
				.Replace("$upload-global/", Common.Util.GlobalUploadPath)
				.Replace("$upload-language/", Common.Util.LanguageUploadPath);

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

	}
}