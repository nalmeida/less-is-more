using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CombineAndMinify {

	public static class Filter {

		public static string Execute(string content, FileTypeUtilities.FileType type){
			switch(type){
				case FileTypeUtilities.FileType.CSS:
					return CSS(content);
				case FileTypeUtilities.FileType.JavaScript:
					return JavaScript(content);
			}
			return content;
		}		public static string CSS(string content){
			content = content
				.Replace("$root/", Common.Util.AssetsRoot)
				.Replace("$global/", Common.Util.GlobalPath)
				.Replace("$language/", Common.Util.LanguagePath);

			Dictionary<string, string> dicVariables = new Dictionary<string, string>();

			const string varRegEx = @"\$(?<varname>[^{}$]*){(?<varvalue>[^}$]*)}";
			const RegexOptions varRegExOptions = RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;

			MatchCollection variables = Regex.Matches(content, varRegEx, varRegExOptions); //finds all variables in the css document
			string currentKey = "";
			foreach (Match match in variables) {
				currentKey = match.Groups["varname"].Value.Trim();
				if(!dicVariables.ContainsKey(currentKey)){
					dicVariables.Add(currentKey, match.Groups["varvalue"].Value.Trim());	//stores it in a dictionary for a later use
				}
			}
			content = Regex.Replace(content, varRegEx + @"[^\r\n]*[\r\n]", string.Empty, varRegExOptions); // removes all variables to clean the css

			foreach (string varname in dicVariables.Keys) {
				content = content.Replace("$" + varname, dicVariables[varname]);  //replaces each variable with its value
			}
			return content;
		}
		public static string JavaScript(string content){
			return content;
		}

	}
}