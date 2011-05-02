using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace CombineAndMinify
{
	[ConfigurationCollection(typeof(string), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class ConfigUrlElementCollection : ConfigurationElementCollection
	{
		private static ConfigurationPropertyCollection _properties;
		protected override ConfigurationPropertyCollection Properties
		{
			get { return _properties; }
		}

		static ConfigUrlElementCollection()
		{
			_properties = new ConfigurationPropertyCollection();
		}

		public ConfigUrlElementCollection()
		{
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
		}

		public ConfigUrlElement this[int index]
		{
			get { return (ConfigUrlElement)base.BaseGet(index); }
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
			return new ConfigUrlElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as ConfigUrlElement).Url;
		}
	}
}

