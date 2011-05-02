using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace CombineAndMinify
{
	[ConfigurationCollection(typeof(string), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class ConfigDomainElementCollection : ConfigurationElementCollection
	{
		private static ConfigurationPropertyCollection _properties;
		protected override ConfigurationPropertyCollection Properties
		{
			get { return _properties; }
		}

		static ConfigDomainElementCollection()
		{
			_properties = new ConfigurationPropertyCollection();
		}

		public ConfigDomainElementCollection()
		{
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
		}

		public ConfigDomainElement this[int index]
		{
			get { return (ConfigDomainElement)base.BaseGet(index); }
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				base.BaseAdd(index, value);
			}
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ConfigDomainElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as ConfigDomainElement).Domain;
		}
	}
}

