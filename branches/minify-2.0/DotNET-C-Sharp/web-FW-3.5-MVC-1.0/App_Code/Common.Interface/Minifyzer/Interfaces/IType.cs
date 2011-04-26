using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Caching;
using System.Text.RegularExpressions;

namespace Common.Minifyzer.Interfaces {
	
	public interface IType {
		
		string Extension { get; }
		string ContentType { get; }
		string Tag { get; }
		string VirtualPath { get; set; }
		string PhysicalPath { get; set; }

	}
}