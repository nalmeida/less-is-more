using System;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Common;

namespace Common
{

	public class MinifyDynamic
	{

		// private
		private Hashtable Tags; // string, string
		private Hashtable CurrentList; // string, string
		
		private HttpContext _context;
		private HttpContext context
		{
			get {
				if(_context != HttpContext.Current){
					_context = HttpContext.Current;
				}
				return _context;
			}
		}

		private string assetsFolderName
		{
			get {
				string fn = ConfigurationManager.AppSettings["ASSETS-FOLDER"];
				if(string.IsNullOrEmpty(fn)){
					fn = "assets";
				}
				return fn;
			}
		}
		private string address
		{
			get {
				return Util.AssetsRoot+"minify.aspx?{0}";
			}
		}
		
		public MinifyDynamic()
		{
			
			CurrentList = new Hashtable(); 
			Tags = new Hashtable();

			// Register tags
			Tags.Add("CSS", "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" {1}/>");
			Tags.Add("JS", "<script type=\"text/javascript\" src=\"{0}\" {1}></script>");
		}
		
		public MinifyDynamic Clear()
		{
			CurrentList.Clear();
			return this;
		}
		public MinifyDynamic Add(string File)
		{
			if(string.IsNullOrEmpty(File))
			{
				return this;
			}
	
			Regex extRegexp = new Regex(@"\.(css|js)$");
			string type = Convert.ToString(extRegexp.Match(File).Groups[1]).ToUpper();
            
			if(extRegexp.Match(File).Groups.Count <= 1)
			{
				return this;
			}
			
			// Add to current list
			if(!CurrentList.ContainsKey(type))
			{
				CurrentList.Add(type, new List<string>());
			}
			List<string> current = (List<string>)CurrentList[type];
			
			if(!current.Contains(File))
			{
				current.Add(File.Replace(Util.AssetsRoot, ""));
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
		public string GetTag(string extra)
		{
			List<string> tags = new List<string>();
			Hashtable currList = (Hashtable)CurrentList.Clone();
			
			foreach(DictionaryEntry item in currList)
			{
				if(Tags.ContainsKey(item.Key))
				{
					if(IsDebug)
					{
						string currItem;
						foreach(string listItem in (List<string>)item.Value)
						{
							currItem = assetsFolderName + "/" + listItem.Split('.')[1] + "/" + listItem + "?v="+Util.Version;
							tags.Add(
								string.Format(
									(string)Tags[item.Key],
									currItem,
									extra
								)
							);
						}
					}
					else
					{
						tags.Add(
							string.Format(
								(string)Tags[item.Key],
								GetPath( (List<string>)item.Value ),
								extra
							)
						);
					}
				}
			}
			
			return string.Join("\n", tags.ToArray());
		}
		public string GetTag()
		{
			return GetTag("");
		}

		/**
		 * Write tag on the current context (wrapper)
		 */
		public MinifyDynamic Write(string extra)
		{
			string cont = GetTag(extra);
			context.Response.Write(cont);
			return Clear();
		}
		public MinifyDynamic Write()
		{
			return Write("");
		}

		/**
		 * debug getter
		 */
		public bool IsDebug
		{
			get {
				return !string.IsNullOrEmpty(Util.Bpc);
			}
		}
		
		
		public string GetPath(string Path)
		{
			return GetPath(new List<string>(){Path});
		}
		public string GetPath(List<string> Paths)
		{
			return string.Format(address, string.Join("|", Paths.ToArray()))
				+ (!string.IsNullOrEmpty(Util.Version) ? "&v="+Util.Version : "");
		}

	}
}