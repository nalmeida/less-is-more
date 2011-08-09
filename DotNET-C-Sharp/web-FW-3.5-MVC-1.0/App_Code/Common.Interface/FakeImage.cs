//<%@ WebHandler Language="C#" Class="Common.FakeImage" %>
//uncomment this line above if you want to use as a standalone file. also rename it as fake_image.ashx and place it at root folder

using System;
using System.Web;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;

/* 
FakeImage

	 @description migué pra gerar imagens "fake" no desenvolvimento de páginas
	 @author Marcelo Miranda Carneiro - mcarneiro@gmail.com * tapa by Regis Bittencourt - rbittencourt@fbiz.com.br
	 @since 10/07/2008
	 @version 1.0.1
	 @usage
		<code>
			<img src="/locales/global/img/fake_image.aspx?quad=100" alt="teste" title="teste" /> ' gera imagem quadrada com 100px
			<img src="/locales/global/img/fake_image.aspx?width=120&height=100" alt="teste" title="teste" /> ' gera imagem com 120 x 100px
		</code>
*/


namespace Common
{
	public class FakeImage : IHttpHandler
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

		private Color stringToColor(string paramValue)
		{
			int red = System.Int32.Parse(paramValue.Substring(0, (2) - (0)), System.Globalization.NumberStyles.AllowHexSpecifier);
			int green = System.Int32.Parse(paramValue.Substring(2, (4) - (2)), System.Globalization.NumberStyles.AllowHexSpecifier);
			int blue = System.Int32.Parse(paramValue.Substring(4, (6) - (4)), System.Globalization.NumberStyles.AllowHexSpecifier);
			return Color.FromArgb(red, green, blue);
		}

		public void ProcessRequest(HttpContext context)
		{
			Request = context.Request;
			Response = context.Response;
			Server = context.Server;

			string qQuad = Request.QueryString["quad"];
			string qWidth = Request.QueryString["width"];
			string qHeight = Request.QueryString["height"];
			string qColor = Request.QueryString["color"];

			if (!(qQuad == null || qQuad == string.Empty))
			{
				qWidth = qQuad;
				qHeight = qQuad;
			}
			else 
			{
				if (qWidth == null || qWidth == string.Empty) 
				{
					qWidth = "100";
				}
		    
				if (qHeight == null || qHeight == string.Empty) 
				{
					qHeight = "100";
				}
			}

			if (qColor == null || qColor == string.Empty || qColor.Length != 6) 
			{
				qColor = "E4E4E4";
			}

			int letterWidth = 5;
			int letterHeight = 9;
			int fontSize = 9;

			int width = Convert.ToInt32(qWidth);
			int height = Convert.ToInt32(qHeight);

			Bitmap oBitmap = new Bitmap(width, height);

			Graphics oGraphic = Graphics.FromImage(oBitmap);

			string sText = width + "x" + height;
			string sFont = "Courier";
			Color ccolor = stringToColor(qColor);

			SolidBrush oBrush = new SolidBrush(ccolor);
			SolidBrush oBrushWrite = new SolidBrush(Color.Black);

			oGraphic.FillRectangle(oBrush, 0, 0, width, height);

			Font oFont = new Font(sFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
			PointF oPoint = new PointF((int.Parse(qWidth) / 2) - ((sText.Length * letterWidth) / 2), (int.Parse(qHeight) / 2) - (letterHeight / 2));

			oGraphic.DrawString(sText, oFont, oBrushWrite, oPoint);

			Response.ContentType = "image/jpeg";
			oBitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
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