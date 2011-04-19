using System;
using System.Collections.Generic;
using System.Web;
using Common;

namespace Common.Minifyzer {

	public class MinifyHandler : IHttpHandler {

		public bool IsReusable {
			get { return true; }
		}

		public void ProcessRequest(HttpContext context) {
			String GroupId = context.Request.QueryString[0].Split('|')[0]; 
			string contentType;
			Util.GZipEncodePage();
			
			if (Minify.GetGroupById(GroupId) != null) {
				
				contentType = Minify.getGroupContentType(GroupId);
				if(!string.IsNullOrEmpty(contentType)){
					context.Response.ContentType = contentType;
				}
				context.Response.Write(Minify.Write(GroupId, context));

			} else {
				contentType = Minify.getFileContentType(GroupId);
				if(!string.IsNullOrEmpty(contentType)){
					context.Response.ContentType = contentType;
				}
				context.Response.Write(Minify.Write(GroupId, context));
			}
		}

	}
}