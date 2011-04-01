using System;
using System.Collections.Generic;
using System.Web;
using System.Net;

/// <summary>
/// Summary description for Factory
/// </summary>
/// 

namespace Minify
{
    public static class FileFactory
    {
        public static IFile CreateFile(string File)
        {
            string extesion = File.Split('.')[1];
            IFile file = null;
            if (extesion == "css")
            {
                file = new CSS();
            }
            else if (extesion == "js")
            {
                file = new JavaScript();
            }
            else
            {
                throw new Exception("Tipo inválido de arquivo.");
            }

            return file;
        }
        
    }

}
