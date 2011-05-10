using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace CombineAndMinify
{
	/// <summary>
	/// Contains the results of an analysis of the head of a page.
	/// </summary>
	public class HeadAnalysis
	{
		// Specifies a string replacement in the head 
		public class Replacement
		{
			public string original { get; set; }
			public string replacement { get; set; }
		}

		// All the replacements that need to be made in the head
		public List<Replacement> Replacements { get; set; }

		// The urls of all images used in the CSS files loaded in the head
		public List<string> ProcessedImageUrls { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="headHtml">
		/// the current contents of the head
		/// </param>
		/// <param name="totalFileNames">
		/// totalFileNames will be filled with a list of the names of all
		/// CSS and JavaScript files loaded in the head (that is, those
		/// that get combined and/or minified).
		/// </param>
		/// <param name="combineCSSFiles">
		/// If true, the CSS files in the group are combined into a single file.
		/// </param>
		/// <param name="combineJavaScriptFiles">
		/// If true, the JavaScript files in the group are combined into a single file.
		/// </param>
		/// <param name="urlProcessor">
		/// Use this UrlProcessor to for example insert version ids.
		/// The ProcessedImageUrls property of this UrlProcessor has already been loaded with the 
		/// urls of the images on the page.
		/// </param>
		/// <returns>
		/// New content of the head
		/// </returns>
		public HeadAnalysis(
			string headHtml, List<string> totalFileNames,
			ConfigSection.CombineOption combineCSSFiles, ConfigSection.CombineOption combineJavaScriptFiles,
			bool minifyCSS, bool minifyJavaScript,
			UrlProcessor urlProcessor)
		{
			// Find groups of script or link tags that load a script or css file from
			// the local site. That is, their source does not start with
			// http:// or https://
			//
			// A script tag that has code between the <script> and </script>
			// has inline script, so we're not interested in that either.
			//
			// The files within each group will be served up as a single file
			// (which only exists in cache).
			// By combining js and css files only within the groups, we make sure
			// that the order of the script files is not changed.
			// 
			// An improvement would be to provide an option to 
			// move all css link tags above the script tags, so you could
			// combine them all. Move them above, in case any javascript depends on the css.

			string regexpScriptLink =
				@"<script[^>]*?src=(?:""|')(?<src>" + 
				RegexExpressionRejectOtherHosts() + 
				@"[^""']*?)(?:""|')[^>]*?>[\s\n\r]*</script>";

			const string propertyNameUrlScript = "src";
			const string propertyNameMediaScript = null;

			const string tagTemplateScript = "<script type=\"text/javascript\" src=\"{0}\"></script>";

			string regexpCssLink =
				@"<link"+
				@"(?=[^>]*?href=(?:""|')(?<href>" +
					RegexExpressionRejectOtherHosts() +
					@"[^""']*?\.css)(?:""|'))" + // positive look ahead for href property, ensuring there is a correct href
				@"(?:(?:[^>]*?media=(?:""|')(?<media>[^""']*?)(?:""|'))*)" + // match zero or more media properties
				@"[^>]*?>"; // end of the link statement

			const string propertyNameUrlCss = "href";
			const string propertyNameMediaCss = "media";

			const string tagTemplateCss = "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" {1}/>";

			// ProcessFileType adds records to the Replacements list

			Replacements = new List<Replacement>();

			ProcessFileType(
				headHtml,
				regexpScriptLink,
				propertyNameUrlScript,
				propertyNameMediaScript,
				FileTypeUtilities.FileType.JavaScript,
				tagTemplateScript,
				totalFileNames,
				combineJavaScriptFiles,
				true,
				minifyCSS, minifyJavaScript,
				urlProcessor);

			ProcessFileType(
				headHtml,
				regexpCssLink,
				propertyNameUrlCss,
				propertyNameMediaCss,
				FileTypeUtilities.FileType.CSS,
				tagTemplateCss,
				totalFileNames,
				combineCSSFiles,
				false,
				minifyCSS, minifyJavaScript,
				urlProcessor);

			// The urlProcessor now contains all image urls contained in CSS files.
			// Copy those urls to this.ProcessedImageUrls.
			ProcessedImageUrls = new List<string>(urlProcessor.ProcessedImageUrls);
			urlProcessor.ProcessedImageUrls.Clear();
		}

		/// <summary>
		/// Returns the "server" bit of the current url. For example
		/// http://www.mydomain.com:1080
		/// If the port being used is 80, no port is included.
		/// </summary>
		/// <returns></returns>
		private string CurrentProtocolHostPort()
		{
			Uri currentUri = HttpContext.Current.Request.Url;
			
			int currentPort = currentUri.Port;

			string result =
				currentUri.Scheme + "://" + currentUri.Host + (currentPort == 80 ? "" : ":" + currentPort.ToString());

			return result;
		}

		/// <summary>
		/// Generates a regular expression sub expression that you'd put at the beginning of an expression
		/// matching a url. 
		/// 
		/// It rejects all urls located on a host other than the current host.
		/// </summary>
		private string RegexExpressionRejectOtherHosts()
		{
			string escapedCurrentHost = EscapedForRegex(CurrentProtocolHostPort());
			string result = "(?:(?:(?!http://)(?!https://))|(?=" + escapedCurrentHost + "))";
			return result;
		}

		/// <summary>
		/// Escapes a string for regular expressions.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		private string EscapedForRegex(string s)
		{
			return Regex.Escape(s).Replace(":", @"\:");
		}

		// This type maps a media (such as "screen" or "printer" to a list of urls)
		// The default name for a media is "all". So even if a link (or script tag)
		// doesn't belong to a specific media, it is associated here with the media "all".
		private class MediaSpecificUrls : Dictionary<string, List<Uri>> 
		{ 
			public void Add(string media, Uri url)
			{
				if (!ContainsKey(media))
				{
					base.Add(media, new List<Uri>());
				}

				base[media].Add(url);
			}		 
		}

		// This type stores all info about a group of links:
		// * The text of the file group, so it can be easily removed from the head
		// * The urls in the group grouped by their media.
		private class groupInfo
		{
			public string linkGroupText;
			public MediaSpecificUrls mediaSpecificUrls;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="headHtmlSb"></param>
		/// <param name="linkRegexp">
		/// Regular expression that matches a single link (that is, a CSS link or a script tag).
		/// </param>
		/// <param name="propertyNameUrl">
		/// The name of the property within the link that holds the url.
		/// </param>
		/// <param name="propertyNameMedia">
		/// The name of the property within the link that holds the media.
		/// null if there is no such property.
		/// </param>
		/// <param name="tagTemplate"></param>
		/// <param name="totalFileNames">
		/// The urls of all the links in the group get added to this list.
		/// </param>
		/// <param name="combineFiles"></param>
		/// <param name="placeCombinedFilesAtEnd">
		/// This is only relevant if combineFiles equals All.
		/// If placeCombinedFilesAtEnd is true, the tag loading the combined file
		/// replaces the very last file group (important if you're loading js, because it means that if any
		/// js is dependent on a library loaded from a CDN, all the js will load after that library.
		/// 
		/// If placeCombinedFilesAtEnd is false, the tag replaces the very first file group.
		/// You'd use this with CSS, to get it load possibly sooner than the js.
		/// </param>
		/// <param name="urlProcessor"></param>
		private void ProcessFileType(
			string headHtml,
			string linkRegexp,
			string propertyNameUrl,
			string propertyNameMedia,
			FileTypeUtilities.FileType fileType,
			string tagTemplate,
			List<string> totalFileNames,
			ConfigSection.CombineOption combineFiles,
			bool placeCombinedFilesAtEnd,
			bool minifyCSS, bool minifyJavaScript,
			UrlProcessor urlProcessor)
		{
			List<groupInfo> allGroups = new List<groupInfo>();
			List<Uri> totalFileUrlsList = new List<Uri>();
			MediaSpecificUrls mediaSpecificUrlsAllGroups = new MediaSpecificUrls();

			// Create a regular expression matching groups of the given linkRegexp.
			// Two links are in the same group if they are separated by no more than white space.
			string groupRegexp = 
				@"(?:" + linkRegexp + @"[\s\n\r]*)+";

			Regex regexGroup = new Regex(groupRegexp, RegexOptions.IgnoreCase);
			Match matchGroup = regexGroup.Match(headHtml);

			// Visit each group of script or link tags. Record the html of each file group
			// and a list of the urls in the tags in that file group in allGroups.
			while (matchGroup.Success)
			{
				string linkGroupText = matchGroup.Value;
				MediaSpecificUrls mediaSpecificUrlsInGroup = new MediaSpecificUrls();

				AnalyzeGroup(
					linkGroupText, linkRegexp, propertyNameUrl, propertyNameMedia,
					mediaSpecificUrlsInGroup, mediaSpecificUrlsAllGroups,
					totalFileNames);

				allGroups.Add(new groupInfo() { linkGroupText = linkGroupText, mediaSpecificUrls = mediaSpecificUrlsInGroup });
				matchGroup = matchGroup.NextMatch();
			}

			// Process each file group in allGroups
			if (allGroups.Count > 0)
			{
				switch (combineFiles)
				{
					case ConfigSection.CombineOption.None:
						// In each group, process all URLs individually into tags.
						// Note that CombinedFile.Url not only has the ability to combine urls, but also
						// to insert version info - and we still want that to be able to use far future cache expiry,
						// even if not combining files.
						// Concatenate the tags and replace the group with the concatenated tags.
						foreach (groupInfo g in allGroups)
						{
							StringBuilder tagsInGroup = new StringBuilder();

							foreach (string media in g.mediaSpecificUrls.Keys)
							{
								List<Uri> fileUrlsList = g.mediaSpecificUrls[media];

								foreach (Uri u in fileUrlsList)
								{
									string versionedUrl = CombinedFile.Url(
										HttpContext.Current, new List<Uri>(new Uri[] { u }), fileType,
										minifyCSS, minifyJavaScript,
										urlProcessor, totalFileNames);
									string versionedFileTag =
										string.Format(tagTemplate, versionedUrl, MediaProperty(media));
									tagsInGroup.Append(versionedFileTag);
								}
							}
							// Be sure to trim the group before storing it (that is, remove space at the front and end).
							// If you don't, you may store a group with white space at either end, that then doesn't match
							// a group in some other file that is exactly the same, except for the white space at either end.
							Replacements.Add(new Replacement { original = g.linkGroupText.Trim(), replacement = tagsInGroup.ToString() });
						}

						break;

					case ConfigSection.CombineOption.PerGroup:
						// In each group, process all URLs together into a combined tag.
						// Replace the group with that one tag.
						foreach (groupInfo g in allGroups)
						{
							StringBuilder tagsInGroup = new StringBuilder();

							foreach (string media in g.mediaSpecificUrls.Keys)
							{
								List<Uri> fileUrlsList = g.mediaSpecificUrls[media];

								string combinedFileUrl = CombinedFile.Url(
									HttpContext.Current, fileUrlsList, fileType, minifyCSS, minifyJavaScript, urlProcessor, totalFileNames);

								string combinedFileTag =
									string.Format(tagTemplate, combinedFileUrl, MediaProperty(media));

								tagsInGroup.Append(combinedFileTag);
							}

							Replacements.Add(new Replacement { original = g.linkGroupText.Trim(), replacement = tagsInGroup.ToString() });
						}
						break;

					case ConfigSection.CombineOption.All:
						// Combine all urls into a single tag. Then insert that tag in the head.
						// Also, remove all groups.
						{
							StringBuilder tagsInGroup = new StringBuilder();

							foreach (string media in mediaSpecificUrlsAllGroups.Keys)
							{
								List<Uri> fileUrlsList = mediaSpecificUrlsAllGroups[media];

								string combinedFileUrl = CombinedFile.Url(
									HttpContext.Current, fileUrlsList, fileType, minifyCSS, minifyJavaScript, urlProcessor, totalFileNames);
								string combinedFileTag =
									string.Format(tagTemplate, combinedFileUrl, MediaProperty(media));

								tagsInGroup.Append(combinedFileTag);
							}

							int idxFileGroupToReplace = placeCombinedFilesAtEnd ? (allGroups.Count - 1) : 0;

							Replacements.Add(
								new Replacement
								{
									original = allGroups[idxFileGroupToReplace].linkGroupText.Trim(),
									replacement = tagsInGroup.ToString()
								});

							// Replace all file groups with empty string, except for the one
							// we just replaced with the tag.
							allGroups.RemoveAt(idxFileGroupToReplace);
							foreach (groupInfo g in allGroups)
							{
								Replacements.Add(
									new Replacement { original = g.linkGroupText.Trim(), replacement = "" });
							}
						}
						break;

					default:
						throw new ArgumentException("ProcessFileType - combineFiles=" + combineFiles.ToString());
				}
			}
		}

		/// <summary>
		/// Generates a media property based on a given media type (screen, print, etc.)
		/// </summary>
		/// <param name="mediaType"></param>
		/// <returns></returns>
		private string MediaProperty(string mediaType)
		{
			string result = "";

			if ((!string.IsNullOrEmpty(mediaType)) &&
				(string.Compare(mediaType, "all", true) != 0))
			{
				// Be sure to have at least one space after the generated property
				result = string.Format("media=\"{0}\" ", mediaType);
			}

			return result;
		}

		/// <summary>
		/// Analyzes a group of links. A link is a link tag for CSS files or a script tag
		/// for JavaScript files.
		/// </summary>
		/// <param name="linkGroupText">
		/// Text of the group.
		/// </param>
		/// <param name="linkRegexp">
		/// Regular expression that matches a single link (that is, a CSS link or a script tag).
		/// </param>
		/// <param name="groupNameUrl">
		/// Name of the capture group that holds the url
		/// </param>
		/// <param name="groupNameMedia">
		/// Name of the capture group that holds the media.
		/// null if there is no such group.
		/// </param>
		/// <param name="mediaSpecificUrls">
		/// Adds the url in the link to this object, with the correct media.
		/// </param>
		/// <param name="mediaSpecificUrls2">
		/// Adds the url in the link to this object too, with the correct media.
		/// </param>
		/// <param name="totalFileNames">
		/// The urls in the group get added to this list.
		/// </param>
		private void AnalyzeGroup(
			string linkGroupText,
			string linkRegexp,
			string groupNameUrl, string groupNameMedia,
			MediaSpecificUrls mediaSpecificUrls, MediaSpecificUrls mediaSpecificUrls2, 
			List<string> totalFileNames)
		{
			// Visit each link within the group
			Regex regexLink = new Regex(linkRegexp, RegexOptions.IgnoreCase);
			Match matchLink = regexLink.Match(linkGroupText);

			while (matchLink.Success)
			{
				string link = matchLink.Value;

				// ---------------
				// Get the url

				CaptureCollection urls = matchLink.Groups[groupNameUrl].Captures;
				string linkUrl = null;

				if (urls.Count > 0)
				{
					linkUrl = urls[0].Value;
				}

				if (string.IsNullOrEmpty(linkUrl))
				{
					throw new Exception(
						string.Format("Tag {0} in group {1} has no url", link, linkGroupText));
				}

				// ---------------
				// Get the media

				string linkMedia = null;
				if (!string.IsNullOrEmpty(groupNameMedia))
				{
					CaptureCollection medias = matchLink.Groups[groupNameMedia].Captures;
					if (medias.Count > 0)
					{
						linkMedia = medias[0].Value;
					}
				}

				if (string.IsNullOrEmpty(linkMedia))
				{
					linkMedia = "all"; // "all" is default value for media
				}


				// ---------------
				// Process the url and media

				Uri linkUri = new Uri(HttpContext.Current.Request.Url, linkUrl);
				mediaSpecificUrls.Add(linkMedia, linkUri);
				mediaSpecificUrls2.Add(linkMedia, linkUri);

				matchLink = matchLink.NextMatch();
			}
		}
	}
}
