using System;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections;

namespace Common{

	public static class Browser{
		
		public static Hashtable browserList;
		public static String userAgent;
		
		/**
		 * Verify wich browser is by user agent
		 * @usage
			<code>
				<%if(!Common.Browser.IsAny("safari", "firefox", "chrome")){%>
				<link rel="stylesheet" type="text/css" href="<%=Common.Util.GlobalPath%>css/png_gradients.css" media="all" />
				<%}%>
				<% if(Common.Browser.Is("ie", "6")) {// IE6 Only%>
				<link rel="stylesheet" type="text/css" href="<%=Common.Util.GlobalPath%>css/png_ie6_fix.css" media="all" />
				<%}%>
				<% if(Common.Browser.Is("ie")) {// IE6 Only%>
				<link rel="stylesheet" type="text/css" href="<%=Common.Util.GlobalPath%>css/ie_pie.css" media="all" />
				<%}%>
			</code>
		
		 */
		
		static Browser(){
			browserList = new Hashtable();
			browserList.Add("internet explorer", "msie");
			browserList.Add("ie", "msie");
			browserList.Add("firefox", "firefox");
			browserList.Add("google chrome", "chrome");
			browserList.Add("chrome", "chrome");
			browserList.Add("safari", "safari,chrome"); // avoid "chrome"
			browserList.Add("iphone", "iphone");
			browserList.Add("ipad", "ipad");
			browserList.Add("opera", "opera");
		}
		
		/**
		 * verify if is ONE OF the browsers listed
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
		 * verify if IS the browser
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
		 * verify if is a specific version of a browser (as described in user agent)
		 */
		public static bool Is(String value, String version){
			if(Is(value)){
				String[] val = browserList[value].ToString().Split(new char[] {','});
				switch(val[0]){
					case "firefox":
					case "chrome":
						return Regex.IsMatch(userAgent, @""+val[0]+"/"+version, RegexOptions.IgnoreCase);
						break;
					case "msie":
						return Regex.IsMatch(userAgent, @""+val[0]+" "+version, RegexOptions.IgnoreCase);
						break;
					case "safari":
					case "opera":
						return Regex.IsMatch(userAgent, @"version/"+version, RegexOptions.IgnoreCase);
						break;
				}
			}
			return false;
		}
	}
}