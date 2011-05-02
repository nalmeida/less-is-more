using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;

namespace CombineAndMinify {

	public class StandAlone {

		private Hashtable Tags; // FileTypeUtilities.FileType, string
		private Hashtable CurrentList; // FileTypeUtilities.FileType, string
		
		private HttpContext context = HttpContext.Current;
		private ConfigSection cs;
		private UrlProcessor urlProcessor;
		
		public StandAlone(){
			cs = ConfigSection.CurrentConfigSection();
			urlProcessor = new UrlProcessor(
				cs.CookielessDomains,
				cs.MakeImageUrlsLowercase,
				cs.InsertVersionIdInImageUrls,
				cs.InsertVersionIdInFontUrls,
				ConfigSection.OptionIsActive(cs.EnableCookielessDomains),
				cs.PreloadAllImages,
				ConfigSection.OptionIsActive(cs.ExceptionOnMissingFile),
				HttpContext.Current.IsDebuggingEnabled
			);
			CurrentList = new Hashtable(); 
			Tags = new Hashtable();
			Tags.Add(FileTypeUtilities.FileType.CSS, "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />");
			Tags.Add(FileTypeUtilities.FileType.JavaScript, "<script type=\"text/javascript\" src=\"{0}\" ></script>");
		}
		
		public StandAlone Clear(){
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
		public string GetCode() {
			// string CurrentContent = "";
			// foreach(AType item in CurrentList){
			// 	CurrentContent += item.Content;
			// }
			// CurrentList.Clear();
			// return CurrentContent;
			return "";
		}

		/**
		 * Get the html tag
		 */
		public string GetTag() {
			
			List<string> tags = new List<string>();
			foreach(DictionaryEntry item in CurrentList){
				if(Tags.ContainsKey(item.Key)){
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
			
			return string.Join("\n", tags.ToArray());
		}

		/**
		 * Write tag or code on the current context (wrapper)
		 */
		public StandAlone Write(){
			HttpContext.Current.Response.Write(GetTag());
			CurrentList.Clear();
			return this;
		}

		// /**
		//  * Get Tag without minify management (for debugging purpose)
		//  */
		// public string GetDebugTag(List<AType> list){
		// 	// string Response = "";
		// 	// foreach(AType item in list){
		// 	// 	Response += GetDebugTag(item);
		// 	// }
		// 	// return Response;
		// 	return "";
		// }
		// public string GetDebugTag(AType file){
		// 	// return string.Format(file.Tag, file.VirtualPath);
		// 	return "";
		// }

		/**
		 * debug getter
		 */
		// public bool IsDebug(){
		// 	// TODO: mecanism with token via querystring
		// 	return ConfigurationManager.AppSettings["MinifyDebug"] == "true";
		// }
		
		
		/**
		 * Getters
		 */
		public string GetImagePath(string Path){
			
			if(urlProcessor.ImagesNeedProcessing()){
				Path = urlProcessor.ProcessedUrl(
					Path,
					FileTypeUtilities.FuzzyFileType.Image,
					false,
					HttpContext.Current.Request.Url,
					CombinedFile.LastUpdateTime(Path, urlProcessor.ThrowExceptionOnMissingFile)
				);
			}
			
			return Path;
		}
		
		public string GetJSPath(string Path){
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
				uriList.Add(new Uri(HttpContext.Current.Request.Url, url));
			}
			
			// TODO: get fileType
			// TODO: create Uri List
			
			ConfigSection cs = ConfigSection.CurrentConfigSection();
			
			return CombinedFile.Url(
				HttpContext.Current, uriList, Type,
				cs.MinifyCSS, cs.MinifyJavaScript,
				urlProcessor, new List<string>());
		}
		
	}
}