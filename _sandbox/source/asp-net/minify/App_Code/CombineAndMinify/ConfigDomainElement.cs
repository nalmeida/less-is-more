using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace CombineAndMinify
{
	/// <summary>
	/// Needed to create the array of cookieless domain names in web.config
	/// </summary>
	public class ConfigDomainElement: ConfigurationElement
	{
		private static ConfigurationProperty _domainProperty;
		private static ConfigurationPropertyCollection _properties;

		static ConfigDomainElement()
		{
			_domainProperty = new ConfigurationProperty(
				"domain",
				typeof(string)
			);

			_properties = new ConfigurationPropertyCollection();
			_properties.Add(_domainProperty);
		}

		[ConfigurationProperty("domain", IsRequired = false)]
		public string Domain
		{
			get { return (string)base[_domainProperty]; }
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get { return _properties; }
		}
	}
}
