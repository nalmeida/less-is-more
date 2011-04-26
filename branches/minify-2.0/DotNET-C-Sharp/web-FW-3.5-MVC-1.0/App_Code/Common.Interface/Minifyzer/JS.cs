using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Common.Minifyzer.Abstracts;

namespace Common.Minifyzer {
	
	
	public class JS : AType, IType {
		
		public string Extension { get { return "js"; }}
		public string ContentType { get { return "text/javascript"; }}
		public string Tag { get { return "<script type=\"text/javascript\" src=\"{0}\" ></script>"; }}

		public void Filter() {
			Content = "\n\n /* " + VirtualPath + " | " + LastModified + " */ \n\n" + Content;
			Content += @"window['Common'] = window['Common'] || {Util: { 
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