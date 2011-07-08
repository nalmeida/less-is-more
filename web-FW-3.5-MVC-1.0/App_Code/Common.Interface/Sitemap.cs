//<%@ WebHandler Language="C#" Class="Common.Sitemap" %>
//uncomment this line above if you want to use as a standalone file. also rename it as minify.ashx and place it at root folder
using System.Net;
using System.Web;
using System.Xml;



namespace Common
{
    public class Sitemap : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(context.Response.OutputStream, System.Text.Encoding.UTF8) { Formatting = Formatting.Indented };

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("urlset");
            xmlWriter.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            xmlWriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xmlWriter.WriteAttributeString("xsi:schemaLocation", @"http://www.sitemaps.org/schemas/sitemap/0.9
            http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");


            xmlWriter.WriteEndElement();//urlset

            context.Response.ContentType = "text/xml";
            xmlWriter.Flush();
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