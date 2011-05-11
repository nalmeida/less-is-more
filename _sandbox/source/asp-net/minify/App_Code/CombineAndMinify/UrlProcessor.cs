using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web;

namespace CombineAndMinify
{
	public class UrlProcessor
	{
		private List<string> _cookielessDomains = null;
		private bool _makeImageUrlsLowercase = false;
		private bool _insertVersionIdInImageUrls = false;
		private bool _insertVersionIdInFontUrls = false;
		private bool _enableCookielessDomains = false;
		private bool _preloadAllImages = false;
		private bool _debugModeActive = false;

		

		// Each time an image url is processed, it is added to this list
		public List<string> ProcessedImageUrls { get; private set; }

		public bool ThrowExceptionOnMissingFile { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cookielessDomains"></param>
		/// <param name="makeImageUrlsLowercase"></param>
		/// <param name="insertVersionIdInImageUrls">
		/// </param>
		public UrlProcessor(
			List<string> cookielessDomains, bool makeImageUrlsLowercase,
			bool insertVersionIdInImageUrls, bool insertVersionIdInFontUrls, bool enableCookielessDomains,
			bool preloadAllImages, bool throwExceptionOnMissingFile,
			bool debugModeActive)
		{
			this._cookielessDomains = cookielessDomains;
			this._makeImageUrlsLowercase = makeImageUrlsLowercase;
			this._insertVersionIdInImageUrls = insertVersionIdInImageUrls;
			this._insertVersionIdInFontUrls = insertVersionIdInFontUrls;
			this._enableCookielessDomains = enableCookielessDomains;
			this._preloadAllImages = preloadAllImages;
			this._debugModeActive = debugModeActive;

			ThrowExceptionOnMissingFile = throwExceptionOnMissingFile;

			this.ProcessedImageUrls = new List<string>();
		}

		/// <summary>
		/// Processes a url and returns the result.
		/// Adds a cookieless domain (if any specified in _cookielessDomains).
		/// 
		/// Inserts version id. For images, this only happens if _insertVersionIdInImageUrls is true, for font files 
		/// only if _insertVersionIdInFontUrls is true.
		///
		/// Makes the url lowercase, if the url is an image and _makeImageUrlsLowercase is true.
		/// Note that non-images (css and js files) are never made lowercase, because their filenames are hashes.
		/// 
		/// The resulting url is not only returned, but also added to property ProcessedImageUrls (provided it is 
		/// an image url).
		/// </summary>
		/// <param name="url">
		/// Url to process.
		/// </param>
		/// <param name="fullyFileType">
		/// File type of the url, as specific or unspecific as known by the caller.
		/// </param>
		/// <param name="alwaysMakeAbsolute">
		/// Set true if the url should be made absolute, even if there are no cookieless domains
		/// specified.
		/// </param>
		/// <param name="alwaysMakeAbsolute">
		/// Set true if the url should be made absolute, even if there are no cookieless domains
		/// specified.
		/// </param>
		/// <param name="baseUri">
		/// Base Url to be used when making the url absolute, and when adding a cookieless domain (which also
		/// involves making the url absolute).
		/// 
		/// If this is null, the Url of the current request is used.
		/// </param>
		/// <param name="versionId">
		/// Version id to be inserted into the url.
		/// If this is null or empty, the method gets the version id itself via the url.
		/// </param>
		/// <returns></returns>
		public string ProcessedUrl(
			string url, FileTypeUtilities.FuzzyFileType fuzzyFileType, bool alwaysMakeAbsolute, Uri baseUri, string versionId)
		{
			try
			{
				if (string.IsNullOrEmpty(url))
				{
					return url;
				}

				// In the url(...) attribute in CSS files, it is legal to put the url in quotes, like in
				// background: #FFFFFF url("Images/gradient.png") repeat-x;
				string cleanedUrl = url.Trim(new char[] { '\'', '"' });

				bool isStaticImageUrl;
				bool isStaticFontUrl;
				bool isGeneratedImageOrFont;
				bool isInlinedImage;
				bool isCssOrJavaScript;
				FileTypeUtilities.ClassifyFuzzyFileType(
					fuzzyFileType, cleanedUrl, out isStaticImageUrl, out isStaticFontUrl, out isGeneratedImageOrFont, out isInlinedImage, out isCssOrJavaScript);

				// If the image url is actually for an inlined image, there is no point in trying to process it.
				if (isInlinedImage)
				{
					return url;
				}

				// If the image or font file is generated, don't process it.
				// In that case, you don't want to preload it or cache it.
				if (isGeneratedImageOrFont)
				{
					return url;
				}

				// If baseUri is null, get the default
				if (baseUri == null)
				{
					baseUri = HttpContext.Current.Request.Url;
				}

				string result = cleanedUrl;

				// If the url is absolute or starts with a tilde (so it will be absolute after it has
				// been resolved), deal with that now.

				if (result.StartsWith("/"))
				{
					// url is absolute. Make sure it works under Cassini.
					result = CombinedFile.ResolveUrl("~" + result);
				}
				else if (result.StartsWith("~"))
				{
					// Url needs to be resolved. Do that now. You'll wind up with 
					// absolute url that works under Cassini too.
					result = CombinedFile.ResolveUrl(result);
				}

				// -------------------
				// Make the url lowercase first, before inserting the version id. That way, the version id
				// remains unchanged.
				if (isStaticImageUrl && _makeImageUrlsLowercase)
				{
					result = result.ToLower();
				}

				// If the url is not actually on the same site as the baseUri (that is, we're getting
				// a file from some other domain, than 
				// * it is already absolute
				// * we don't want to change to a cookieless domain
				// * we don't want to insert a version id.
				string relativeUrlScheme;
				if (SameDomain(cleanedUrl, baseUri, out relativeUrlScheme))
				{
					// ----------------------
					// Add cookieless domain or make absolute url.
					// Note that if we add the cookieless domain, that will make it absolute in itself.

					if (!UsingCookielessDomains())
					{
						if (alwaysMakeAbsolute)
						{
							result = AbsoluteUrl(result, baseUri);
						}
					}
					else
					{
						result = ReplaceDomain(result, baseUri, relativeUrlScheme, _cookielessDomains);
					}

					// -----------------------
					// Insert version id
					if (isCssOrJavaScript ||
						(isStaticImageUrl && _insertVersionIdInImageUrls) ||
						(isStaticFontUrl && _insertVersionIdInFontUrls))
					{
						string usedVersionId = versionId;
						if (string.IsNullOrEmpty(usedVersionId))
						{
							// Get absolute path of the image or file. This is relevant if the url is relative
							// to for example the location of a CSS file.
							string resolvedPath = (new Uri(baseUri, cleanedUrl)).AbsolutePath;
							usedVersionId = CombinedFile.LastUpdateTime(resolvedPath, ThrowExceptionOnMissingFile);
						}

						result = UrlVersioner.InsertVersionId(result, usedVersionId);
					}
				}

				// -------------

				if (isStaticImageUrl)
				{
					ProcessedImageUrls.Add(result);
				}

				return result;
			}
			catch(Exception e)
			{
				// Something went wrong. 
				// Rather than crashing the page, in Release mode just return the original url.

				if (_debugModeActive)
				{
					throw new Exception("Exception in ProcessedUrl. See inner exception for details.", e);
				}

				return url;
			}
		}

		/// <summary>
		/// Returns true if images need to be processed by the UrlProcessor.
		/// </summary>
		/// <returns></returns>
		public bool ImagesNeedProcessing()
		{
			return 
				_makeImageUrlsLowercase ||
				_insertVersionIdInImageUrls ||
				_preloadAllImages ||
				UsingCookielessDomains();
		}

		private bool UsingCookielessDomains()
		{
			return (_enableCookielessDomains && (_cookielessDomains != null) && (_cookielessDomains.Count > 0));
		}

		/// <summary>
		/// Makes sure that a url is absolute.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="baseUri">
		/// </param>
		/// <returns></returns>
		private string AbsoluteUrl(string url, Uri baseUri)
		{
			if (url.StartsWith("/") || CombinedFile.UrlStartsWithProtocol(url)) { return url; }

			// .svg urls (a type of font file) can have a fragment!
			// So preserve the fragment here.

			Uri resolvedUri = (new Uri(baseUri, url));
			string absolutePath = resolvedUri.AbsolutePath;
			string fragment = resolvedUri.Fragment; // fragment is the stuff after #

			string absoluteUrl = absolutePath + fragment;

			return absoluteUrl;
		}

		/// <summary>
		/// Turns a url into an absolute url, and then changes its domain.
		/// If the url starts with http or https, the domain doesn't get changed.
		/// </summary>
		/// <param name="url">
		/// The cookieless domain will be added to this url
		/// </param>
		/// <param name="baseUri">
		/// Uri of the page where this url is used. Used to make the url into an abslute url.
		/// </param>
		/// <param name="urlScheme">
		/// Scheme of the url
		/// </param>
		/// <param name="cookielessDomains">
		/// The list of cookieless domains that is available. One of these domains will be chosen, based on
		/// the hash of the absolute version of the url.
		/// By basing it on the absolute version, urls that point to the same actual resource but that have 
		/// different paths will still get the same cookieless domain.
		/// </param>
		/// <param name="newDomain">
		/// New domain of the url.
		/// </param>
		/// <returns>
		/// The resulting url.
		/// </returns>
		private string ReplaceDomain(string url, Uri baseUri, string urlScheme, List<string> cookielessDomains)
		{
			string result = url;

			string absoluteUrl = (new Uri(baseUri, url)).AbsolutePath;
			
			int nbrCookielessDomains = cookielessDomains.Count;
			int imageUrlHash = Math.Abs(absoluteUrl.GetHashCode());
			string chosenCookielessDomain = cookielessDomains[imageUrlHash % nbrCookielessDomains];

			// Only replace the current domain with the new cookieless domain if they both use the
			// same scheme (http or https).
			string schemeWithColon = urlScheme + ":";
			if (chosenCookielessDomain.StartsWith(schemeWithColon))
			{
				result = chosenCookielessDomain + absoluteUrl;
			}

			return result;
		}

		private bool SameDomain(string inUrl, Uri baseUri, out string inUrlScheme)
		{
			Uri inUri = new Uri(baseUri, inUrl);

			inUrlScheme = inUri.Scheme;

			return (inUri.Host == baseUri.Host);
		}
	}
}
