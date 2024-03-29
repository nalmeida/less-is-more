using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.IO;
using Common;
using T = VO.Seo;


namespace BL
{
	public class Seo
	{

		private DataSet ds;

		public int Total(short status)
		{
			if (PopulaDataSet())
			{
				return ds.Tables["pages"].Rows.Count;
			}
			else
			{
				return 0;
			}

		}

		public T Obter(string url)
		{
			VO.Seo ret = new VO.Seo();

			if (PopulaDataSet())
			{
				DataRow[] seo = ds.Tables["pages"].Select("url = '" + url + "'");

				if (seo.Length == 0){ //TODO: deixar mais elegante
					seo = ds.Tables["pages"].Select("url = '" + Util.NoSlash(url) + "'");
				}
				if (seo.Length == 0){
					seo = ds.Tables["pages"].Select("url = '" + Util.Slash(url) + "'");
				}
				if (seo.Length == 0){
					seo = ds.Tables["pages"].Select("url = '/'");
				}

				foreach (DataRow row in seo)
				{
					ret.Title = row["title"].ToString();
					ret.Description = row["description"].ToString();
					ret.Keywords = row["keywords"].ToString();
					ret.H1 = row["h1"].ToString();
				}
			}

			return ret;

		}

		public T Obter()
		{
			return Obter(Common.Util.RawPath);
		}
	   

		private bool PopulaDataSet()
		{

			try
			{
				//tenta carregar o objeto de SEO primeiramente do cache
				if (HttpContext.Current.Cache["SEO"] != null)
				{
					ds = (DataSet)HttpContext.Current.Cache["SEO"];
					return true;
				}
				else if (File.Exists(HttpContext.Current.Server.MapPath("~/App_Data/seo.xml")))
				{
						ds = new DataSet();
						ds.ReadXml(HttpContext.Current.Server.MapPath("~/App_Data/seo.xml"));
						//grava os dados em cache, que expira quando h� altera��es no arquivo
						HttpContext.Current.Cache.Add("SEO", ds, new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("~/App_Data/seo.xml")), System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
						return true;
				}
				else
					return false;
			}
			catch
			{
				return false;
			}

		}
	
		internal void LoadMetaTags()
		{
			throw new NotImplementedException();
		}
	}
}