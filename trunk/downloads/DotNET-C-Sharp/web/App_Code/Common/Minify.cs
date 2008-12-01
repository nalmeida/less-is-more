//<%@ WebHandler Language="C#" Class="Common.Minify" %>
//uncomment this line above if you want to use as a standalone file. also rename it as minify.ashx and place it at root folder

using System;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

/* 
Minify

@author Marcelo Miranda Carneiro - mail: mcarneiro@gmail.com. Thanks to Leandro Ribeiro and Nicholas Almeida.
 * updated Regis Bittencourt - changed into a http handler .net class. 
@since 04/01/2008
@version 2.0.0
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


namespace Common
{
    public class Minify : IHttpHandler
    {

        private HttpServerUtility _server;
        public HttpServerUtility Server
        {
            get { return _server; }
            set { _server = value; }
        }


        private HttpRequest _request;
        public HttpRequest Request
        {
            get { return _request; }
            set { _request = value; }
        }


        private HttpResponse _response;
        public HttpResponse Response
        {
            get { return _response; }
            set { _response = value; }
        }

        public void DoCache(HttpContext context, DateTime lastModifiedUnc)
        {
            string sDtModHdr = string.Empty;
            sDtModHdr = Request.Headers.Get("If-Modified-Since");
            // does header contain If-Modified-Since?
            if (!string.IsNullOrEmpty(sDtModHdr))
            {
                sDtModHdr = sDtModHdr.Split(';')[0];
                // convert to UNC date
                DateTime dtModHdrUnc = Convert.ToDateTime(sDtModHdr).ToUniversalTime();
                dtModHdrUnc = dtModHdrUnc.AddMilliseconds(dtModHdrUnc.Millisecond * -1);
                lastModifiedUnc = lastModifiedUnc.ToUniversalTime();
                lastModifiedUnc = lastModifiedUnc.AddMilliseconds(lastModifiedUnc.Millisecond * -1);

                // if it was within the last month, return 304 and exit
                if (DateTime.Compare(
                    new DateTime(
                    dtModHdrUnc.Year, 
                    dtModHdrUnc.Month, 
                    dtModHdrUnc.Day, 
                    dtModHdrUnc.Hour,
                    dtModHdrUnc.Minute, 
                    dtModHdrUnc.Second), 
                    new DateTime(
                    lastModifiedUnc.Year,
                    lastModifiedUnc.Month,
                    lastModifiedUnc.Day,
                    lastModifiedUnc.Hour,
                    lastModifiedUnc.Minute,
                    lastModifiedUnc.Second)) == 0)
                {
                    Response.StatusCode = 304;
                    Response.StatusDescription = "Not Modified";
                    Response.CacheControl = "public";
                    Response.End();
                }
            }
            Response.Cache.SetLastModified(lastModifiedUnc);
            Response.CacheControl = "public";
        }

        public void ProcessRequest(HttpContext context)
        {
            Request = context.Request;
            Response = context.Response;
            Server = context.Server;

           
            // READING FILES
            StringBuilder sbToStrip = new StringBuilder();
            string file = string.Empty;
            string folder = string.Empty;

            string[] vtArquivo = Request.QueryString[0].ToString().Split(Convert.ToChar("|"));

            Encoding utf8 = Encoding.GetEncoding("utf-8");
            StreamReader srArquivo;
            DateTime lastModifiedFileGlobal = DateTime.MinValue;

            string filePath;
            DateTime fileLastModified;
            if (vtArquivo[0].Contains(".css"))
            {
                foreach (string stNomeArquivo in vtArquivo)
                {
                    Response.ContentType = "text/css";
                    // set folder and file name
                    if (stNomeArquivo.Contains(","))
                    {
                        string[] fileFolder = stNomeArquivo.Split(',');
                        file = fileFolder[0];
                        folder = fileFolder[1];
                    }
                    else
                    {
                        file = stNomeArquivo;
                        folder = "global";
                    }
                    filePath = Server.MapPath("locales/" + folder + "/css/").ToString() + file;

                    fileLastModified = File.GetLastWriteTime(filePath);
                    lastModifiedFileGlobal = fileLastModified > lastModifiedFileGlobal ? fileLastModified : lastModifiedFileGlobal;
                    
                    srArquivo = new StreamReader(filePath, utf8);
                    sbToStrip.Append(srArquivo.ReadToEnd());
                    sbToStrip.Append(Environment.NewLine);
                    srArquivo.Close();
                }
            }
            else
            {
                Response.ContentType = "text/javascript";
                foreach (string stNomeArquivo in vtArquivo)
                {
                    filePath = Server.MapPath("js/") + stNomeArquivo;

                    fileLastModified = File.GetLastWriteTime(filePath);
                    lastModifiedFileGlobal = fileLastModified > lastModifiedFileGlobal ? fileLastModified : lastModifiedFileGlobal;

                    srArquivo = new StreamReader(filePath, utf8);
                    sbToStrip.Append(srArquivo.ReadToEnd());
                    sbToStrip.Append(";");
                    sbToStrip.Append(Environment.NewLine);
                    srArquivo.Close();
                };
            }

            
            //DO CLEANING AND TAG REPLACING
            string stContent = sbToStrip.ToString();

            if (Util.Bpc == string.Empty)
            {
                stContent = Regex.Replace(stContent, "([^:^'^\"^\\\\])(//.*)", "$1"); // delete line comments

                // remove unecessary characters
                stContent = stContent.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
                stContent = stContent.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                
                stContent = stContent.Replace("var(root)/", Common.Util.Root); // replace root tag
                
                stContent = Regex.Replace(stContent, "/\\*[\\d\\D]*?\\*/", string.Empty); // delete block comments
            }
            {
                stContent = stContent.Replace("var(root)/", Common.Util.Root); // replace root tag
            }

            stContent = Regex.Replace(stContent, "url\\((.[^\\)]*)\\)", "url($1?v=" + Request.QueryString["v"] + ")"); // replace all images paths adding the v=version 


            // CACHE
            if (Util.Bpc == string.Empty)
            {
                DoCache(context, lastModifiedFileGlobal);
            }

            // GZIP ENCODE
            Util.GZipEncodePage();

            
            //OUTPUT

            Response.Write("/**\n * @author F.biz - http://www.fbiz.com.br/\n */\n");
            Response.Write(stContent);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

    }
}