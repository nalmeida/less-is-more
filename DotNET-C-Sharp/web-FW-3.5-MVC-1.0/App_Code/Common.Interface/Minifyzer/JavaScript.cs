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
		public string BaseFolder { get; set; }
		public string Url { get; set; }
		public CacheDependency FileCacheDependency { get; set; }
		public DateTime LastModified { get; set; }

		public JavaScript(){
			Tag = "<script type=\"text/javascript\" src=\"{0}\" ></script>";
		}

		public void LoadFile(string file, string id) {
			
			if (string.IsNullOrEmpty(BaseFolder)) {
				BaseFolder = context.Server.MapPath(context.Request.ApplicationPath) + "/js/";
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

		public void Filter() {
			Content = "\n\n /* " + Id + " | " + LastModified + " */ \n\n" + Content;
		}

		private void ReadFile() {
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
			FileCacheDependency = new CacheDependency(BaseFolder + Name);
			try{
				using (StreamReader srContent = new StreamReader(BaseFolder + Name, Encoding.GetEncoding("utf-8"))) {
					Content = srContent.ReadToEnd();
				}
			}catch(Exception ex){
				Content = "/* ERROR: Não foi possível encontrar \""+Id+"\" */\n";
			}
		}
	}
}