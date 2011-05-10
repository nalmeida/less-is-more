using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text.RegularExpressions;

namespace CombineAndMinify
{
	public class HttpHandler : IHttpHandler
	{

		public bool IsReusable
		{
			get { return true; }
		}

		public void ProcessRequest(HttpContext context)
		{
			
			ConfigSection cs = ConfigSection.CurrentConfigSection();

			// --------------

			Uri url = context.Request.Url;
			string path = url.AbsolutePath;
			FileTypeUtilities.FileType fileType = FileTypeUtilities.FileTypeOfUrl(path);
			
			// verify if file is a CombineAndMinify file
			GroupCollection MatchedFileName = new Regex(@"/([^\/]*?)__[^\/]*?\.\w{2,4}(?:$|\?)").Match(url.PathAndQuery).Groups;
			
			// --------------

			context.Response.AddHeader(
				"Content-Type",
				FileTypeUtilities.FileTypeToContentType(fileType));

			if(MatchedFileName.Count == 1 && MatchedFileName[0].Value == ""){
				context.Response.AddHeader("Cache-Control", "public");
				context.Response.WriteFile(path);
				return;
			}
				
			const int yearInSeconds = 60 * 60 * 24 * 365;

			int maxAge = yearInSeconds;
			if (context.IsDebuggingEnabled || 
				((!cs.InsertVersionIdInImageUrls) && FileTypeUtilities.FileTypeIsImage(fileType)) ||
				((!cs.InsertVersionIdInFontUrls) && FileTypeUtilities.FileTypeIsFont(fileType)) )
			{
				maxAge = 0;
			}

			// --------------

			if ((fileType == FileTypeUtilities.FileType.JavaScript) ||
				(fileType == FileTypeUtilities.FileType.CSS))
			{
				UrlProcessor urlProcessor =
					new UrlProcessor(
						cs.CookielessDomains, cs.MakeImageUrlsLowercase, cs.InsertVersionIdInImageUrls, cs.InsertVersionIdInFontUrls,
						ConfigSection.OptionIsActive(cs.EnableCookielessDomains), cs.PreloadAllImages,
						ConfigSection.OptionIsActive(cs.ExceptionOnMissingFile), HttpContext.Current.IsDebuggingEnabled);

				string content =
					CombinedFile.Content(
						context, path,
						cs.MinifyCSS, cs.MinifyJavaScript,
						urlProcessor, out fileType);

				context.Response.AddHeader("Cache-Control", "public,max-age=" + maxAge.ToString());
				context.Response.AddHeader("Vary", "Accept-Encoding");
				context.Response.Write(content);
			}
			else
			{
				// The file type is not JavaScript or CSS, so it is an image or a font file.
				// In case the image or font file has a version id in it, deversion the path
				string deversionedPath = path;

				if ((cs.InsertVersionIdInImageUrls && FileTypeUtilities.FileTypeIsImage(fileType)) ||
					(cs.InsertVersionIdInFontUrls && FileTypeUtilities.FileTypeIsFont(fileType)))
				{
					bool deversioned;
					deversionedPath = UrlVersioner.DeversionedImageUrl(path, out deversioned);
				}

				string fileName =
					CombinedFile.MapPath(deversionedPath, ConfigSection.OptionIsActive(cs.ExceptionOnMissingFile));

				if (!String.IsNullOrEmpty(fileName))
				{
					context.Response.AddHeader("Cache-Control", "public,max-age=" + maxAge.ToString());
					context.Response.WriteFile(fileName);
				}
			}
			
			// Appply Gzip to JS and CSS
			if(!FileTypeUtilities.FileTypeIsImage(fileType) && !FileTypeUtilities.FileTypeIsFont(fileType)){
				Common.Util.GZipEncodePage();
			}
		}
	}
}
