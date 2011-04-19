using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for ContentFactory
/// </summary>
/// 

namespace Minify
{
    public static class ContentFactory
    {

        public static IContent CreateContent(string File)
        {
            string extesion = File.Split('.')[1];
            IContent content = null;
            if (extesion == "css")
            {
                content = new CSSContent();
            }
            else if (extesion == "js")
            {
                content = new JavaScriptContent();
            }
            else
            {
                throw new Exception("Tipo inválido de arquivo.");
            }

            return content;
        }
    }

}
