using System;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections;

namespace Common{

	public static class Browser{
		
		public static Hashtable browserList;
		public static String userAgent;
		
		static Browser(){
			browserList = new Hashtable();
			browserList.Add("internet-explorer", "msie");
			browserList.Add("ie", "msie");
			browserList.Add("firefox", "firefox");
			browserList.Add("google-chrome", "chrome");
			browserList.Add("chrome", "chrome");
			browserList.Add("safari", "safari,chrome"); // avoid "chrome"
			browserList.Add("iphone", "iphone");
			browserList.Add("ipad", "ipad");
			browserList.Add("opera", "opera");
			
		}
		
		/**
		 * Is exactly the browser by name
		 * @param value Browser name as registered in the Hashtable
		 */
		public static bool Is(String value){
			userAgent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToLower();
			String[] splitted;
			
			foreach( DictionaryEntry item in browserList ){
				splitted = item.Value.ToString().Split(new char[] {','});
				if(item.Key == value){
					if(splitted.Length > 1){
						if(Regex.IsMatch(userAgent, @""+splitted[0], RegexOptions.IgnoreCase) && !Regex.IsMatch(userAgent, @""+splitted[1], RegexOptions.IgnoreCase)){
							return true;
						}
					}else{
						return Regex.IsMatch(userAgent, @""+item.Value, RegexOptions.IgnoreCase);
					}
				}
			}
			return false;
		}
		
		/**
		 * Is exctly the browser by name and the version
		 * @param value Browser name as registered in the Hashtable
		 * @param version Browser version, operators can be used before the version witout any spaces: "<=0.0.0"
		 */
		public static bool Is(String value, String version){
			if(Is(value)){
				String[] val = browserList[value].ToString().Split(new char[] {','});
				Match versionVerifier = Regex.Match(version, @"(^[<>=]+)?(.*)");
				bool toReturn = false;
				switch(val[0]){
					case "firefox":
					case "chrome":
						toReturn = Regex.IsMatch(userAgent, @""+val[0]+"/"+versionVerifier.Groups[2].Value, RegexOptions.IgnoreCase);
						break;
					case "msie":
						toReturn = Regex.IsMatch(userAgent, @""+val[0]+" "+versionVerifier.Groups[2].Value, RegexOptions.IgnoreCase);
						break;
					case "safari":
					case "iphone":
					case "ipad":
					case "opera":
						toReturn = Regex.IsMatch(userAgent, @"version/"+versionVerifier.Groups[2].Value, RegexOptions.IgnoreCase);
						break;
				}
				if(versionVerifier.Groups[1].Value != ""){
					String[] currentVersion = getVersion(value).Split(new char[] {'.'});
					String[] desiredVersion = versionVerifier.Groups[2].Value.Split(new char[] {'.'});
					int currentVersionVal = 0;
					int desiredVersionVal = 0;
					bool isHigher = false;
					for(int i=0; i < desiredVersion.Length; i++){
						currentVersionVal = currentVersion.Length > i ? Convert.ToInt32(currentVersion[i]) : 0;
						desiredVersionVal = Convert.ToInt32(desiredVersion[i]);
						switch(versionVerifier.Groups[1].Value){
							case ">=":
								toReturn = currentVersionVal >= desiredVersionVal;
								break;
							case ">":
								toReturn = currentVersionVal > desiredVersionVal;
								break;
							case "<":
								toReturn = currentVersionVal < desiredVersionVal;
								break;
							case "<=":
								toReturn = currentVersionVal <= desiredVersionVal;
								break;
						}
					}
				}
				return toReturn;
			}
			return false;
		}
		
		/**
		 * Is any of the browsers in the argument list
		 * @param arguments each argument folloews the rule of Is method divided by space: "firefox >3.6", "safari 5", "chrome"
		 * @return boolean if one of the browsers is matched
		 */
		public static bool IsAny(params String[] args){
			
			String[] splitted;
			int valid = 0;
			for(int i = 0; i < args.Length; i++){

				splitted = args[i].Split(new char[] {' '});
				if((splitted.Length == 1 && Is(splitted[0])) || (splitted.Length > 1 && Is(splitted[0], splitted[1]))){
					valid ++;
				}
				
			}
			return valid > 0;
		}
		
		/**
		 * @private
		 */
		private static String getVersion(String value){
			if(Is(value)){
				String[] val = browserList[value].ToString().Split(new char[] {','});
				Match matchResult = null;
				switch(val[0]){
					case "firefox":
					case "chrome":
						matchResult = Regex.Match(userAgent, @""+val[0]+"/([0-9\\\\.]+)", RegexOptions.IgnoreCase);
						break;
					case "msie":
						matchResult = Regex.Match(userAgent, @""+val[0]+" ([0-9\\\\.]+)", RegexOptions.IgnoreCase);
						break;
					case "safari":
					case "iphone":
					case "ipad":
					case "opera":
						matchResult = Regex.Match(userAgent, @"version/([0-9\\\\.]+)", RegexOptions.IgnoreCase);
						break;
				}
				return matchResult.Groups[1].Value.ToString();
			}
			return "0";
		}
		
		/**
		 * Get browser version based on user agent
		 * @return a string with the browser version: "0.0.0", "0.0", etc
		 */
		public static String getVersion(){
			foreach( DictionaryEntry item in browserList ){
				if(Is(item.Key.ToString())){
					return getVersion(item.Key.ToString());
				}
			}
			return "0";
		}

		/**
		 * Get browser name based on registered Hashtable (if alias is created, the last item added will be used)
		 */
		public static String getName(){
			foreach( DictionaryEntry item in browserList ){
				if(Is(item.Key.ToString())){
					return item.Key.ToString();
				}
			}
			return "";
		}

		/**
		 * Writes a css class-name to be used for cross-browser styles. Ex.: "msie msie6"
		 */
		public static String getCSSClassName(){
			String[] ver = getVersion().Split(new char[] {'.'});
			String browserName = getName();
			return browserName+" "+browserName+""+ver[0];
		}
		
		private static void write(String value){
			HttpContext.Current.Response.Write(value+"<br />");
		}
	}
}