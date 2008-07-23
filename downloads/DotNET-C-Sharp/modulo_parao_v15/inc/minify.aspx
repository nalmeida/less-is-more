<script runat="server" language="C#">
	/* 
	Minify

	@author Marcelo Miranda Carneiro - mail: mcarneiro@gmail.com. Thanks to Leandro Ribeiro and Nicholas Almeida.
	@since 04/01/2008
	@version 1.0.1
	@usage
		<code>
			// just call the Javascript file or CSS file as parameters:
			<script type="text/javascript" src="minify.aspx?file1.js|file2.js"><\/script>
			<style type="text/css" src="minify.aspx?file1.css|file2.css"></style>
		</code>
	*/
	private void Page_Load(object sender, System.EventArgs e){

		Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
		HttpContext.Current.Response.Charset = "ISO-8859-1";
		//HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
		HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Today.AddDays(5);

		try{
			string[] vtArquivo = Request.QueryString[0].ToString().Split(Convert.ToChar("|"));
			System.Text.StringBuilder sbToStrip = new System.Text.StringBuilder();
			System.IO.StreamReader srArquivo;
			System.Text.RegularExpressions.Regex obRegExBlockComm = new System.Text.RegularExpressions.Regex("/\\*[\\d\\D]*?\\*/");
			System.Text.RegularExpressions.Regex obRegExLineComm = new System.Text.RegularExpressions.Regex("([^:^'^\"^\\\\])(//.*)");

			string verificador = vtArquivo[0].Substring(vtArquivo[0].IndexOf('.') + 1, 1);
			if(verificador == "c"){
				Response.ContentType = "text/css";
				foreach (string stNomeArquivo in vtArquivo){
					srArquivo = new System.IO.StreamReader(Server.MapPath("../css/").ToString() + stNomeArquivo,System.Text.Encoding.GetEncoding("utf-8"));
					sbToStrip.Append(srArquivo.ReadToEnd());
					sbToStrip.Append(Environment.NewLine);
					srArquivo.Close();
				};
			}else{
				Response.ContentType = "text/javascript";
				foreach (string stNomeArquivo in vtArquivo){
					srArquivo = new System.IO.StreamReader(Server.MapPath("../js/").ToString() + stNomeArquivo,System.Text.Encoding.GetEncoding("utf-8"));
					sbToStrip.Append(srArquivo.ReadToEnd());
					sbToStrip.Append(";");
					sbToStrip.Append(Environment.NewLine);
					srArquivo.Close();
				};
			}

			string stJs = sbToStrip.ToString();
			
			stJs = obRegExLineComm.Replace(stJs, "$1"); //retira comentarios de linha
			
			//retira caracteres desnecessários
			stJs = stJs.Replace("\n", "").Replace("\r", "").Replace("\t", "");
			stJs = stJs.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
			//stJs = stJs.Replace("{root}", "http://"+Request.Url.Host+"/lojamaster/");
			
			stJs = obRegExBlockComm.Replace(stJs, ""); //retira comentarios de bloco
			Response.Write(stJs);

			obRegExBlockComm = null;
			obRegExLineComm = null;
			srArquivo = null;
			sbToStrip = null;

		}catch (Exception ex){
			Response.Write(ex.Message);
		}
	}
</script>