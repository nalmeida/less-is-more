using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for JavaScript
/// </summary>
public class JavaScript : IFile
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string PhisicalFolder { get; set; }
    public string Content { get; set; }
    public HttpContext context { get { return HttpContext.Current; } }
    public string BaseFolder { get; set; }
    public CacheDependency FileCacheDependency { get; set; }
    public DateTime LastModified { get; set; }


    public void LoadFile(string file, string id)
    {
        if (string.IsNullOrEmpty(BaseFolder))
        {
            BaseFolder = context.Server.MapPath(context.Request.ApplicationPath) + "/js/";
        }

        Name = file;
        Id = id;
        LastModified = DateTime.Now;
        ReadFile();
    }

    public void LoadFile(string file, string FilePath, string id)
    {
        if (FilePath.ToLower().StartsWith("http://") || FilePath.ToLower().StartsWith("https://"))
        {
            BaseFolder = FilePath;
        }
        else
        {
            BaseFolder = context.Server.MapPath(FilePath);
        }

        LoadFile(file, id);
    }

    public void Filter()
    {
        Content = "\n\n /* " + Name + " | " + LastModified + " */ \n\n" + Content;
    }

    private void ReadFile()
    {

        if (BaseFolder.ToLower().StartsWith("http://") || BaseFolder.ToLower().StartsWith("https://"))
        {
            Content = Common.Util.HttpPost(BaseFolder + Name);
        }
        else
        {
            FileCacheDependency = new CacheDependency(BaseFolder + Name);
            using (StreamReader srContent = new StreamReader(BaseFolder + Name, Encoding.GetEncoding("utf-8")))
            {
                Content = srContent.ReadToEnd();
            }
        }
    }
}
