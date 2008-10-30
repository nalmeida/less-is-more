<script runat="server" language="C#">
	/* 
	Minify

	@author Marcelo Miranda Carneiro - mail: mcarneiro@gmail.com. Thanks to Leandro Ribeiro and Nicholas Almeida.
	@since 04/01/2008
	@version 1.2.2 (1 month cache + gzip)
	@usage
		<code>
			// just call the Javascript file or CSS file as parameters:
			<script type="text/javascript" src="minify.aspx?file1.js|file2.js"><\/script>
			<style type="text/css" src="minify.aspx?file1.css|file2.css"></style>
			// to call a specific language css
			<style type="text/css" src="minify.aspx?file1.css|file2.css,pt-BR"></style>
			// CSSs that will load:
			// <root>/locales/global/css/file1.css
			// <root>/locales/pt-BR/css/file2.css
		</code>
	*/
	private void Page_Load(object sender, System.EventArgs e){

		// CACHE
		DateTime dtNowUnc = DateTime.Now.ToUniversalTime();
		string sDtModHdr = Request.Headers.Get("If-Modified-Since");
		// does header contain If-Modified-Since?
		if ((sDtModHdr != "")){
			// convert to UNC date
			DateTime dtModHdrUnc = Convert.ToDateTime(sDtModHdr).ToUniversalTime();
			// if it was within the last month, return 304 and exit
			if(DateTime.Compare(dtModHdrUnc, dtNowUnc.AddMonths(-1)) > 0){
				Response.StatusCode = 304;
				Response.StatusDescription = "Not Modified";
				Response.End();
			}
		}
		
		GZipEncodePage();
		
		Response.Cache.SetLastModified(DateTime.Now);
		Response.CacheControl = "public";
		Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
		HttpContext.Current.Response.Charset = "UTF-8";
		HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Today.AddDays(5);

		try{
			string[] vtArquivo = Request.QueryString[0].ToString().Split(Convert.ToChar("|"));
			System.Text.StringBuilder sbToStrip = new System.Text.StringBuilder();
			System.IO.StreamReader srArquivo;
			System.Text.RegularExpressions.Regex obRegExBlockComm = new System.Text.RegularExpressions.Regex("/\\*[\\d\\D]*?\\*/");
			System.Text.RegularExpressions.Regex obRegExLineComm = new System.Text.RegularExpressions.Regex("([^:^'^\"^\\\\])(//.*)");

			string file = "";
			string folder = "";
			
			string verificador = vtArquivo[0].Substring(vtArquivo[0].IndexOf('.') + 1, 1);
			if(verificador == "c"){
				Response.ContentType = "text/css";
				foreach (string stNomeArquivo in vtArquivo){
					// set folder and file name
					if(stNomeArquivo.IndexOf(',') == -1){
						file = stNomeArquivo;
						folder = "global";
					}else{
						file = stNomeArquivo.Substring(0,stNomeArquivo.IndexOf(','));
						folder = stNomeArquivo.Substring(stNomeArquivo.IndexOf(',')+1);
					}
					//debug
					//Response.Write("file: "+file+", folder: "+folder+"\n");
					srArquivo = new System.IO.StreamReader(Server.MapPath("../locales/"+folder+"/css/").ToString() + file,System.Text.Encoding.GetEncoding("utf-8"));
					sbToStrip.Append(srArquivo.ReadToEnd());
					sbToStrip.Append(Environment.NewLine);
					srArquivo.Close();
				};
			}else{
				Response.ContentType = "text/javascript";
				foreach (string stNomeArquivo in vtArquivo){
					srArquivo = new System.IO.StreamReader(Server.MapPath("../js/") + stNomeArquivo,System.Text.Encoding.GetEncoding("utf-8"));
					sbToStrip.Append(srArquivo.ReadToEnd());
					sbToStrip.Append(";");
					sbToStrip.Append(Environment.NewLine);
					srArquivo.Close();
				};
			}

			string stJs = sbToStrip.ToString();
			
			stJs = obRegExLineComm.Replace(stJs, "$1"); //deleve line comments
			
			//remove unecessary characters
			stJs = stJs.Replace("\n", "").Replace("\r", "").Replace("\t", "");
			stJs = stJs.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
			stJs = stJs.Replace("var(root)/", COMMON.Util.Root);
			
			stJs = obRegExBlockComm.Replace(stJs, ""); //delete block comments
			Response.Write(stJs);

			obRegExBlockComm = null;
			obRegExLineComm = null;
			srArquivo = null;
			sbToStrip = null;

		}catch (Exception ex){
			Response.Write(ex.Message);
		}
	}
	public static bool IsGZipSupported() {
	    string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
	    if (!string.IsNullOrEmpty(AcceptEncoding) && AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate"))
			return true;
	    return false;
	}
	public static void GZipEncodePage() {
	    if (IsGZipSupported()) {
	        HttpResponse Response = HttpContext.Current.Response;
	        string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
	        if (AcceptEncoding.Contains("gzip")) {
	            Response.Filter =  new System.IO.Compression.GZipStream(Response.Filter, System.IO.Compression.CompressionMode.Compress);
	            Response.AppendHeader("Content-Encoding", "gzip");
	        } else {
	            Response.Filter =  new System.IO.Compression.DeflateStream(Response.Filter, System.IO.Compression.CompressionMode.Compress);
	            Response.AppendHeader("Content-Encoding", "deflate");
	        }
	    }
	}
</script>