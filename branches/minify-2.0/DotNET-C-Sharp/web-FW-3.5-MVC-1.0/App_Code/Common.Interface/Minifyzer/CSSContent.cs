using System;
using System.Collections.Generic;
using System.Web;

namespace Common.Minifyzer{
	
	public class CSSContent : IContent{

	    public string Extension { get { return "css"; } }
	    public string Type { get { return "text/css"; } }

	    public string CustomHeader() {
	        return "";
	    }

	}
}
