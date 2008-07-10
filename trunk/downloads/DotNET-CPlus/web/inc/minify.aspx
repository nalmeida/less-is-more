﻿<script runat="server" language="C#">
	/* 
	Minify

	@author Marcelo Miranda Carneiro - mail: mcarneiro@gmail.com. Thanks to Leandro Ribeiro and Nicholas Almeida.
	@since 04/01/2008
	@version 1.1.1
	@usage
		<code>
			// just call the Javascript file or CSS file as parameters:
			<script type="text/javascript" src="minify.aspx?file1.js|file2.js"><\/script>
			<style type="text/css" src="minify.aspx?file1.css|file2.css"></style>
			// para chamar um css referente a alguma língua / tema específico
			<style type="text/css" src="minify.aspx?file1.css|file2.css,pt-BR"></style>
			// serão chamados:
			// <root>/locales/global/css/file1.css
			// <root>/locales/pt-BR/css/file2.css
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

			string file = "";
			string folder = "";
			
			string verificador = vtArquivo[0].Substring(vtArquivo[0].IndexOf('.') + 1, 1);
			if(verificador == "c"){
				Response.ContentType = "text/css";
				foreach (string stNomeArquivo in vtArquivo){
					// set folder and file name
					if(vtArquivo[0].IndexOf(',') == -1){
						file = stNomeArquivo;
						folder = "global";
					}else{
						file = stNomeArquivo.Substring(0,stNomeArquivo.IndexOf(','));
						folder = stNomeArquivo.Substring(stNomeArquivo.IndexOf(',')+1);
					}
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
			
			stJs = obRegExLineComm.Replace(stJs, "$1"); //retira comentarios de linha
			
			//retira caracteres desnecessários
			stJs = stJs.Replace("\n", "").Replace("\r", "").Replace("\t", "");
			stJs = stJs.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
			stJs = stJs.Replace("var(root)/", COMMON.Util.Root);
			
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