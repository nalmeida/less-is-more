using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Caching;
using System.Text.RegularExpressions;

namespace Common.Minifyzer {
	
	public class Cache {

		static public void Run(HttpContext Context){
			Run(Context, DateTime.Now);
		}
		static public void Run(HttpContext Context, DateTime LastModified) {
			try {
				string HeadModSince = Context.Request.Headers.Get("If-Modified-Since");

				if (!string.IsNullOrEmpty(HeadModSince)) {

					HeadModSince = HeadModSince.Split(';')[0];
					// convert to UNC date
					DateTime SinceModified = Convert.ToDateTime(HeadModSince).ToUniversalTime();
					SinceModified = SinceModified.AddMilliseconds(SinceModified.Millisecond * -1);
					LastModified = LastModified.ToUniversalTime();
					LastModified = LastModified.AddMilliseconds(LastModified.Millisecond * -1);

					// if it was within the last month, return 304 and exit
					Context.Response.Write("/*"+DateTime.Compare( SinceModified, LastModified)+" - "+SinceModified.ToString()+" / "+LastModified.ToString()+"*/");
					if (DateTime.Compare( SinceModified, LastModified) == 0) {
						Context.Response.StatusCode = 304;
						Context.Response.StatusDescription = "Not Modified";
						Context.Response.CacheControl = "public";
						Context.Response.End();
					}
				}
				Context.Response.Cache.SetLastModified(LastModified);
				Context.Response.CacheControl = "public";
			} catch { }
		}
	}
}
