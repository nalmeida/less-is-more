using System;
using System.Collections.Generic;
using System.Web;
using Common;

/// <summary>
/// Summary description for MinifyHandler
/// </summary>
/// 

namespace Minify
{
    public class MinifyHandler : IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            String GroupId = context.Request.QueryString[0].Split(Convert.ToChar("|"))[0];  

            if (Minify.Minifyzer.GroupExists(GroupId))
            {
                context.Response.Write(Minify.Minifyzer.Write(GroupId, context));
            }
            else
            {
                Random rdm = new Random();
                GroupId = rdm.Next().ToString();

                foreach (string item in context.Request.QueryString[0].Split(Convert.ToChar("|")))
                {
                    Minify.Minifyzer.Add(GroupId, item);
                }

                Util.GZipEncodePage();
                context.Response.Write(Minify.Minifyzer.Write(GroupId, context));
            }
        }

        #endregion
    }
}