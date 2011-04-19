using System;
using System.Collections.Generic;
using System.Web;

namespace Common.Minifyzer{
	
	public interface IContent {
	    string Extension { get; }
	    string Type { get; }
	    string CustomHeader();
	}
	
}