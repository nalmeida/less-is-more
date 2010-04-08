using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.IO;
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

                if (seo.Length == 0)
                    seo = ds.Tables["pages"].Select("url = '/'");

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

        private bool PopulaDataSet()
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("~/App_Data/seo.xml")))
            {
                ds = new DataSet();
                ds.ReadXml(HttpContext.Current.Server.MapPath("~/App_Data/seo.xml"));
                return true;
            }
            else
                return false;
        }
    }
}