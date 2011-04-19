﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace Common.Minifyzer{
	
	public interface IFile {
		
		string Tag { get; set; }
		string Id { get; set; }
		string Name { get; set; }
		string PhisicalFolder { get; set; }
		string Content { get; set; }
		HttpContext context { get; }
		string BaseFolder { get; set; }
		void LoadFile(string file, string id);
		void LoadFile(string file, string baseFile, string id);
		void Filter();
		CacheDependency FileCacheDependency { get; set; }
		DateTime LastModified { get; set; }

	}
}