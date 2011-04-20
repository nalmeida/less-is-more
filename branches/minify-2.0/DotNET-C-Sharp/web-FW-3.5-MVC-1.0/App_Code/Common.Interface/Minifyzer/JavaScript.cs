using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Common.Minifyzer {
	
	public class JavaScript : IFile {
		
		public string Tag { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public string PhisicalFolder { get; set; }
		public string Content { get; set; }
		public HttpContext context { get { return HttpContext.Current; } }
		public string BaseFolderRoot { get; set; }
		public string BaseFolderAsset { get; set; }
		public string Url { get; set; }
		public CacheDependency FileCacheDependency { get; set; }
		public DateTime LastModified { get; set; }

		public JavaScript(){
			Tag = "<script type=\"text/javascript\" src=\"{0}\" ></script>";
		}

		public void LoadFile(string file, string id) {
			
			if (string.IsNullOrEmpty(BaseFolderRoot)) {
				BaseFolderAsset = "js/";
				BaseFolderRoot = context.Server.MapPath(context.Request.ApplicationPath) + "/";
			}

			Name = file;
			Id = id;
			LastModified = DateTime.Now;
			ReadFile();
		}
		public void LoadFile(string file, string UrlPath, string id) {
			Url = UrlPath;
			LoadFile(file, id);
		}
		
		public string GetVirtualPath(){
			return Url != null ? Url : Common.Util.Root + BaseFolderAsset + Name;
		}
		public string GetPhysicalPath(){
			return Url != null ? Url : BaseFolderRoot + BaseFolderAsset + Name;
		}

		public void Filter() {
			Content = "\n\n /* " + Id + " | " + LastModified + " */ \n\n" + Content;
		}

		private void ReadFile() {
			// TODO: use a third party application to compress even more .eg.: google closure api or yahoo yui compressor
			if (!string.IsNullOrEmpty(Url)) {
				Content = Common.Util.HttpPost(Url, 2000);
				if(string.IsNullOrEmpty(Content)){
					ReadLocalFile();
				}
			} else {
				ReadLocalFile();
			}
		}
		private void ReadLocalFile(){
			FileCacheDependency = new CacheDependency(BaseFolderRoot + BaseFolderAsset + Name);
			try{
				using (StreamReader srContent = new StreamReader(BaseFolderRoot + BaseFolderAsset + Name, Encoding.GetEncoding("utf-8"))) {
					Content = srContent.ReadToEnd();
				}
			}catch(Exception ex){
				Content = "/* ERROR: Não foi possível encontrar \""+Id+"\" */\n";
			}
		}
	}
}