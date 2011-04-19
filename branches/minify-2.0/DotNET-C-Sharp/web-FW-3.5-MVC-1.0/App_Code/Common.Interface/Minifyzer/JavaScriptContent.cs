using System;
using System.Collections.Generic;
using System.Web;


namespace Common.Minifyzer {
	
	public class JavaScriptContent : IContent {

		public string Extension { get { return "js"; } }
		public string Type { get { return "text/javascript"; } }

		public string CustomHeader() {

			return @"window['Common'] = window['Common'] || {Util: { 
				Root: '" + Common.Util.Root + @"',
				AssetsRoot: '" + Common.Util.AssetsRoot + @"', 
				GlobalPath: '" + Common.Util.GlobalPath + @"', 
				LanguagePath: '" + Common.Util.LanguagePath + @"',
				UploadsRoot: '" + Common.Util.UploadsRoot + @"',
				GlobalUploadPath: '" + Common.Util.GlobalUploadPath + @"',
				LanguageUploadPath: '" + Common.Util.LanguageUploadPath + "'}}; \n";
		}
	}
}