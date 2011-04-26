using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Configuration;
using System.Text;
using Common.Minifyzer;

namespace Common{
	public static class Minify {
	
		// STATIC STUFF
		private static string HandlerPath;
		private static Common.Minifyzer.Minify singleInst;
		private static Common.Minifyzer.Minify single {
			get {
				if(singleInst == null){
					singleInst = new Common.Minifyzer.Minify();
				}
				return singleInst;
			}
		}
		public static string GetHandlerPath(){
			if(string.IsNullOrEmpty(HandlerPath)){
				HttpHandlersSection handlers = (HttpHandlersSection)ConfigurationManager.GetSection("system.web/httpHandlers");
				foreach(HttpHandlerAction handler in handlers.Handlers){
					if(handler.Type == typeof(MinifyHandler).FullName){
						HandlerPath = handler.Path;
						break;
					}
				}
			}
			return HandlerPath;
		}
		
		public static Common.Minifyzer.Minify Clear(){
			return single.Clear();
		}
		public static Common.Minifyzer.Minify Add(string File){
			return single.Add(File);
		}
		public static Common.Minifyzer.Minify Write(){
			return single.Write();
		}
		public static Common.Minifyzer.Minify Tag(){
			return single.Tag();
		}
		
	}
}