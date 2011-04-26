using System;
using System.Collections.Generic;
using System.Web;
using Common;

namespace Common.Minifyzer {

	public class MinifyHandler : IHttpHandler {

		public bool IsReusable {
			get { return true; }
		}
		public HttpContext Context {
			get; set;
		}

		public void ProcessRequest(HttpContext context) {
			// Context = context;
			// String Id = Context.Request.QueryString[0].Split('|')[0]; 
			// FileGroup Group;
			// IFile File;
			// 
			// // Cache.Run(Context);
			// Util.GZipEncodePage();
			// //fileLastModified = File.GetLastWriteTime(filePath);
			// //lastModifiedFileGlobal = fileLastModified > lastModifiedFileGlobal ? fileLastModified : lastModifiedFileGlobal;
			// 
			// Group = Minify.GetGroupById(Id);
			// if (Group != null) {
			// 	WriteGroupContent(Id);
			// } else {
			// 	File = Minify.GetFileById(Id);
			// 	WriteFileContent(Id);
			// }
		}

		// private void WriteContent(string GroupId, string ContentType){
		// 	if(!string.IsNullOrEmpty(ContentType)){
		// 		Context.Response.ContentType = ContentType;
		// 	}
		// 	Context.Response.Write(Minify.GetCode(GroupId, Context));
		// }
		// private void WriteGroupContent(string GroupId){
		// 	WriteContent(GroupId, Minify.GetGroupContentType(GroupId));
		// }
		// private void WriteFileContent(string GroupId){
		// 	WriteContent(GroupId, Minify.GetFileContentType(GroupId));
		// }
	}
}