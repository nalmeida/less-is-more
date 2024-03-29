﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Common
{
	/**
	 * Common.Config
	 * @since 08/07/2008
	 * @version 1.0
	 * @author Regis Bittencourt - rbittencourt@fbiz.com.br, Marcelo Miranda Carneiro - mcarneiro@gmail.com
	 */
	public static partial class Config
	{
		private static VO.Seo getSeo()
		{
			return Singleton<BL.Seo>.Instancia.Obter();
		}
		/**
		 * Common.Config.Title
		 * @return String the project title name
		 * @usage
				<code>
					<%=Common.Config.Title%> //writes " - ##PAGE_TITLE##" on the page.
				</code>
		 */
		public static string Title
		{
			get
			{
				return getSeo().Title;
			}
		}
		public static string Description
		{
			get
			{
				return getSeo().Description;
			}
		}
		public static string Keywords
		{
			get
			{
				return getSeo().Keywords;
			}
		}
		public static string H1
		{
			get
			{
				return getSeo().H1;
			}
		}

		public static string AuthorName
		{
			get
			{
				return "";
			}
		}

		public static string AuthorAddress
		{
			get
			{
				return "";
			}
		}

		public static string CompanyName
		{
			get
			{
				return "";
			}
		}

		public static string MinifyEncryptKey
		{
			get 
			{
				return RetornarConfig("MinifyEncryptKey");
			}
		}

		private static string RetornarConfig(string config)
		{
			string configvalue = ConfigurationManager.AppSettings.Get(config);
			if (string.IsNullOrEmpty(config))
			{
				throw new ConfigurationErrorsException("A chave " + config + " não está definida. " +
				"No Web.Config, crie dentro de AppSetings a chave " + config);
			}
			return configvalue;
		}
	}
}
