using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using Common;

namespace CombineAndMinify {

	public class StandAlone {

		private Hashtable Tags; // FileTypeUtilities.FileType, string
		private Hashtable CurrentList; // FileTypeUtilities.FileType, string		private HttpContext context;
		private ConfigSection cs;
		private UrlProcessor urlProcessor;		public StandAlone(){
			context = HttpContext.Current;
			cs = ConfigSection.CurrentConfigSection();
			urlProcessor = new UrlProcessor(
				cs.CookielessDomains,
				cs.MakeImageUrlsLowercase,
				cs.InsertVersionIdInImageUrls,
				cs.InsertVersionIdInFontUrls,
				ConfigSection.OptionIsActive(cs.EnableCookielessDomains),
				cs.PreloadAllImages,
				ConfigSection.OptionIsActive(cs.ExceptionOnMissingFile),
				context.IsDebuggingEnabled
			);
			CurrentList = new Hashtable(); 
			Tags = new Hashtable();

			// Register tags
			Tags.Add(FileTypeUtilities.FileType.CSS, "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />");
			Tags.Add(FileTypeUtilities.FileType.JavaScript, "<script type=\"text/javascript\" src=\"{0}\" ></script>");
		}		public StandAlone Clear(){
			CurrentList.Clear();
			return this;
		}
		public StandAlone Add(string File){
	
			// Add to current list
			FileTypeUtilities.FileType type = FileTypeUtilities.FileTypeOfUrl(File);
			if(!CurrentList.ContainsKey(type)){
				CurrentList.Add(type, new List<string>());
			}
			List<string> current = (List<string>)CurrentList[type];
			if(!current.Contains(File)){
				current.Add(File);
			}
			
			return this;
		}

		/**
		 * Get content of file as string 
		 */
		// TODO: get file content
		// public string GetCode() {
			// return "";
		// }

		/**
		 * Get the html tag
		 */
		public string GetTag() {
			
			List<string> tags = new List<string>();
			foreach(DictionaryEntry item in CurrentList){
				if(Tags.ContainsKey(item.Key)){
					if(IsDebug){
						foreach(string listItem in (List<string>)item.Value){
							tags.Add(
								string.Format(
									(string)Tags[item.Key],
									listItem
								)
							);
						}
					}else{
						tags.Add(
							string.Format(
								(string)Tags[item.Key],
								GetCodePath(
									(List<string>)item.Value,
									(FileTypeUtilities.FileType)item.Key
								)
							)
						);
					}
				}
			}
			
			return string.Join("\n", tags.ToArray());
		}

		/**
		 * Write tag or code on the current context (wrapper)
		 */
		public StandAlone Write(){
			context.Response.Write(GetTag());
			CurrentList.Clear();
			return this;
		}

		/**
		 * debug getter
		 */
		public bool IsDebug{
			get {
				// TODO: mecanism with token via querystring
				if(cs.Active == ConfigSection.ActiveOption.Never){
					return true;
				}
				if(cs.Active == ConfigSection.ActiveOption.ReleaseModeOnly){
					return context.IsDebuggingEnabled;
				}
				if(cs.Active == ConfigSection.ActiveOption.DebugModeOnly){
					return !context.IsDebuggingEnabled;
				}
				return false;
			}
		}		
		/**
		 * CombineAndMinify Getters
		 */
		public string GetImagePath(string Path){
			
			if(!IsDebug && urlProcessor.ImagesNeedProcessing()){
				// TODO: verifica se o arquivo é local ou em outro server e usa o web service;
				Path = urlProcessor.ProcessedUrl(
					Path,
					FileTypeUtilities.FuzzyFileType.Image,
					false,
					HttpContext.Current.Request.Url,
					CombinedFile.LastUpdateTime(Path, urlProcessor.ThrowExceptionOnMissingFile)
				);
			}
			
			return Path;
		}		public string GetJSPath(string Path){
			return GetJSPath(new List<string>(){Path});
		}
		public string GetJSPath(List<string> Paths){
			return GetCodePath(Paths, FileTypeUtilities.FileType.JavaScript);
		}


		public string GetCSSPath(string Path){
			return GetCSSPath(new List<string>(){Path});
		}
		public string GetCSSPath(List<string> Paths){
			return GetCodePath(Paths, FileTypeUtilities.FileType.CSS);
		}		
		private string GetCodePath(List<string> Paths, FileTypeUtilities.FileType Type){
			
			List<Uri> uriList = new List<Uri>();
			
			foreach(string url in Paths){
				if(url.Contains(Util.Root)){
					uriList.Add(new Uri(HttpContext.Current.Request.Url, url));
				}else{
					uriList.Add(new Uri(url));
				}
			}
			
			ConfigSection cs = ConfigSection.CurrentConfigSection();
			
			return CombinedFile.Url(
				HttpContext.Current, uriList, Type,
				cs.MinifyCSS, cs.MinifyJavaScript,
				urlProcessor, new List<string>());
		}	}
}