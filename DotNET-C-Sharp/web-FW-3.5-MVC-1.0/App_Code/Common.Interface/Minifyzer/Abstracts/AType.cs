using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Caching;
using System.Text.RegularExpressions;
using Common.Minifyzer.Interfaces;

namespace Common.Minifyzer.Abstracts {
	
	public class AType : IType {
		
		public HttpContext context { get { return HttpContext.Current; } }
		
		public string Extension { get { return ""; } }
		public string ContentType { get { return ""; } }
		public string Tag { get { return ""; } }
		
		public string VirtualPath { get; set; }
		public string PhysicalPath { get; set; }

		private string content;
		public string Content { 
			get {
				return ReadFile();
			}
			set {
				content = value;
			}
		}
		
		public CacheDependency FileCacheDependency { get; set; }
		public DateTime LastModified { get; set; }

		public bool HasModification(){
			DateTime FileModified = File.GetLastWriteTime(PhysicalPath);
			return DateTime.Compare( FileModified, LastModified) > 0;
		}
		public void LoadFile(string Path) {
			
			VirtualPath = Path;
			
			if(VirtualPath.Contains("~/")){
				VirtualPath = VirtualPath.Replace("~/", Common.Util.Root);
			}

			if(VirtualPath.Contains(Common.Util.Root)){
				// local file
				PhysicalPath = context.Server.MapPath(VirtualPath.Replace(Common.Util.Root, context.Request.ApplicationPath+"/"));
				LastModified = File.GetLastWriteTime(PhysicalPath);
			}else{
				// external file
				LastModified = DateTime.Now;
			}

			ReadFile();
		}

		public void Filter() { }

		private string ReadFile() {
			if(context.Cache[VirtualPath] != null){
				content = (string)context.Cache[VirtualPath];
			}else if (!string.IsNullOrEmpty(PhysicalPath)) {
				ReadLocalFile();
			} else {
				content = Common.Util.HttpPost(VirtualPath, 2000);
			}
			return content;
		}
		private void ReadLocalFile(){
			if(File.Exists(PhysicalPath)){
				FileCacheDependency = new CacheDependency(PhysicalPath);
				using (StreamReader srContent = new StreamReader(PhysicalPath, Encoding.GetEncoding("utf-8"))) {
					content = srContent.ReadToEnd();
				}
				DoServerCache();
			}else{
				content = "/* Coundn't find the file \""+VirtualPath+"\" */";
			}
		}
		
		private void DoServerCache(){
			context.Cache.Insert(VirtualPath, content, FileCacheDependency);
		}

	}
}