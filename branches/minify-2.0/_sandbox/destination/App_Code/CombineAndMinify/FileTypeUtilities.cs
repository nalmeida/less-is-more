using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace CombineAndMinify
{
	public class FileTypeUtilities
	{
		public enum FileType
		{
			CSS,
			JavaScript,
			Jpeg,
			Png,
			Gif,
			Woff,
			Ttf,
			Svg,
			Eot
		}
		public enum FuzzyFileType
		{
			CssOrJavaScript,
			Image,
			ImageOrFont
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fuzzyFileType"></param>
		/// <param name="url"></param>
		/// <param name="isStaticImage"></param>
		/// <param name="isStaticFont"></param>
		/// <param name="isGeneratedImageOrFont"></param>
		/// <param name="isCssOrJavaScript"></param>
		public static void ClassifyFuzzyFileType(
			FuzzyFileType fuzzyFileType, string url,
			out bool isStaticImage, out bool isStaticFont, out bool isGeneratedImageOrFont, out bool isInlinedImage, out bool isCssOrJavaScript)
		{
			isStaticImage = false;
			isStaticFont = false;
			isGeneratedImageOrFont = false;
			isInlinedImage = false;
			isCssOrJavaScript = false;

			if (fuzzyFileType == FuzzyFileType.CssOrJavaScript)
			{
				isCssOrJavaScript = true;
				return;
			}

			// At this stage, the url is either a font or a image file.

			if (url.Contains("?"))
			{
				isGeneratedImageOrFont = true;
				return;
			}

			if (IsInlinedImage(url))
			{
				isInlinedImage = true;
				return;
			}


			// Note that FileTypeOfUrl makes it classification based on the extension
			// of the file. That means that generated files (eg. a .aspx file generating an image)
			// won't be recognised. This helps to keep generated files out.

			FileType fileType = FileTypeOfUrl(url);
			if (FileTypeIsImage(fileType))
			{
				isStaticImage = true;
			}
			// Call FileTypeIsFont to make extra sure this is actually a font file
			else if (FileTypeIsFont(fileType))
			{
				isStaticFont = true;
			}

			return;
		}

		/// <summary>
		/// Returns whether a given file type is that of an image.
		/// </summary>
		/// <param name="fileType"></param>
		/// <returns></returns>
		public static bool FileTypeIsImage(FileType fileType)
		{
			return (fileType == FileType.Gif) || (fileType == FileType.Jpeg) || (fileType == FileType.Png);
		}

		/// <summary>
		/// Returns whether a given file type is that of a font file.
		/// </summary>
		/// <param name="fileType"></param>
		/// <returns></returns>
		public static bool FileTypeIsFont(FileType fileType)
		{
			return (fileType == FileType.Woff) || (fileType == FileType.Ttf) || (fileType == FileType.Svg) || (fileType == FileType.Eot);
		}

		/// <summary>
		/// Returns an extension based on the given file type.
		/// </summary>
		/// <param name="fileType"></param>
		/// <returns></returns>
		public static string FileTypeToExtension(FileType fileType)
		{
			string extension = null;

			switch (fileType)
			{
				case FileType.CSS:
					extension = ".css";
					break;

				case FileType.JavaScript:
					extension = ".js";
					break;

				case FileType.Gif:
					extension = ".gif";
					break;

				case FileType.Jpeg:
					extension = ".jpg";
					break;

				case FileType.Png:
					extension = ".png";
					break;

				case FileType.Woff:
					extension = ".woff";
					break;

				case FileType.Ttf:
					extension = ".ttf";
					break;

				case FileType.Svg:
					extension = ".svg";
					break;

				case FileType.Eot:
					extension = ".eot";
					break;

				default:
					throw new Exception("FileTypeToExtension - unknown file type: " + fileType.ToString());
			}

			return extension;
		}

		/// <summary>
		/// Returns the file type of a url.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static FileType FileTypeOfUrl(string url)
		{
			int idxExtension = url.LastIndexOf('.');

			if (idxExtension == -1)
			{
				throw new Exception("FileTypeOfUrl - cannot find extension in: " + url);
			}

			// Instead of looking for the last period, we could see whether the string
			// ends in a particular extension.
			//
			// Note that urls of .svg files can end in #...., such as
			// rondaitcbybt-bold-webfont.svg#webfontlviBzS9u

			string fourLetterExtension = CombinedFile.SafeSubstring(url, idxExtension, 5);
			string threeLetterExtension = CombinedFile.SafeSubstring(url, idxExtension, 4);
			string twoLetterExtension = CombinedFile.SafeSubstring(url, idxExtension, 3);

			if (string.Compare(threeLetterExtension, ".css", true, CultureInfo.InvariantCulture) == 0)
				return FileType.CSS;
			else if (string.Compare(twoLetterExtension, ".js", true, CultureInfo.InvariantCulture) == 0)
				return FileType.JavaScript;
			else if (string.Compare(threeLetterExtension, ".gif", true, CultureInfo.InvariantCulture) == 0)
				return FileType.Gif;
			else if (string.Compare(threeLetterExtension, ".jpg", true, CultureInfo.InvariantCulture) == 0)
				return FileType.Jpeg;
			else if (string.Compare(threeLetterExtension, ".png", true, CultureInfo.InvariantCulture) == 0)
				return FileType.Png;
			else if (string.Compare(fourLetterExtension, ".woff", true, CultureInfo.InvariantCulture) == 0)
				return FileType.Woff;
			else if (string.Compare(threeLetterExtension, ".ttf", true, CultureInfo.InvariantCulture) == 0)
				return FileType.Ttf;
			else if (string.Compare(threeLetterExtension, ".svg", true, CultureInfo.InvariantCulture) == 0)
				return FileType.Svg;
			else if (string.Compare(threeLetterExtension, ".eot", true, CultureInfo.InvariantCulture) == 0)
				return FileType.Eot;

			throw new Exception(
				string.Format(
					"FileTypeOfUrl - unknown extension. Extensions: {0}, {1}, {2}, Index: {3}, url: {4}",
					fourLetterExtension, threeLetterExtension, twoLetterExtension,
					idxExtension, url));
		}

		/// <summary>
		/// Converts a file type to a mime.
		/// </summary>
		/// <param name="fileType"></param>
		/// <returns></returns>
		public static string FileTypeToContentType(FileType fileType)
		{
			string mime = null;

			switch (fileType)
			{
				case FileType.CSS:
					mime = "text/css";
					break;

				case FileType.JavaScript:
					mime = "text/javascript";
					break;

				case FileType.Gif:
					mime = "image/gif";
					break;

				case FileType.Jpeg:
					mime = "image/jpeg";
					break;

				case FileType.Png:
					mime = "image/png";
					break;

				case FileType.Woff:
					mime = "application/octet-stream";
					break;

				case FileType.Ttf:
					mime = "application/octet-stream";
					break;

				case FileType.Svg:
					mime = "application/octet-stream";
					break;

				case FileType.Eot:
					mime = "application/vnd.ms-fontobject";
					break;

				default:
					throw new Exception("FileTypeToContentType - unknown file type: " + fileType.ToString());
			}

			return mime;
		}

		/// <summary>
		/// Determines whether a url is an inlined image (so its contents is not in an external file, but sits in
		/// the CSS or HTML itself).
		/// </summary>
		/// <param name="url">
		/// This would be the src of an img tag, or a url(..) in CSS.
		/// </param>
		/// <returns>
		/// true: this is an inlined image
		/// false: this is not an inlined image
		/// </returns>
		public static bool IsInlinedImage(string url)
		{
			return url.StartsWith("data:image");
		}
	}
}
