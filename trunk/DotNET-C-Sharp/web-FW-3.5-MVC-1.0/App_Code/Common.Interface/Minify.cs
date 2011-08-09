using System;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Common;

namespace Common {

	public class Minify {

		// static
		static private MinifyDynamic _instance;
		static private MinifyDynamic instance {
			get {
				if(_instance == null){
					_instance = new MinifyDynamic();
				}
				return _instance;
			} 
		}
		static public MinifyDynamic Clear(){ return instance.Clear(); }
		static public MinifyDynamic Add(string File){ return instance.Add(File); }
		static public string GetTag() { return instance.GetTag(); }
		static public MinifyDynamic Write(){ return instance.Write(); }
		static public string GetPath(string Path){ return instance.GetPath(Path); }
		static public string GetPath(List<string> Paths){ return instance.GetPath(Paths); }

		// alias para adaptar a minify antiga ao formato da CombineAndMinify
		static public string GetCSSPath(string Path){ return instance.GetPath(Path); }
		static public string GetCSSPath(List<string> Paths){ return instance.GetPath(Paths); }
		static public string GetJSPath(string Path){ return instance.GetPath(Path); }
		static public string GetJSPath(List<string> Paths){ return instance.GetPath(Paths); }

	}
}