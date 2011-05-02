using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace CombineAndMinify
{
	/// <summary>
	/// Needed to create the array of urls in web.config
	/// </summary>
	public class ConfigUrlElement: ConfigurationElement
	{
		private static ConfigurationProperty _urlProperty;
		private static ConfigurationPropertyCollection _properties;

		static ConfigUrlElement()
		{
			_urlProperty = new ConfigurationProperty(
				"url",
				typeof(string)
			);

			_properties = new ConfigurationPropertyCollection();
			_properties.Add(_urlProperty);
		}

		[ConfigurationProperty("url", IsRequired = false)]
		public string Url
		{
			get { return (string)base[_urlProperty]; }
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get { return _properties; }
		}
	}
}
