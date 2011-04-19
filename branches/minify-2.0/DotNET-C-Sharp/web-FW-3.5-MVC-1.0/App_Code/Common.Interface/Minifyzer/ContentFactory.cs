using System;
using System.Collections.Generic;
using System.Web;

namespace Common.Minifyzer {
	
	public static class ContentFactory {

		public static IContent CreateContent(string File) {
			
			string extesion = File.Split('.')[File.Split('.').Length-1];
			IContent content = null;
			switch(extesion){
				case "css":
					content = new CSSContent();
					break;
				case "js":
					content = new JavaScriptContent();
					break;
				default:
					throw new Exception("Tipo inválido de arquivo.");
					break;
			}
			return content;
		}
	}

}
