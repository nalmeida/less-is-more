using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

/// <summary>
/// Interface for files target to minify.
/// </summary>
/// 

public interface IFile
{
    string Id { get; set; }
    string Name { get; set; }
    string PhisicalFolder { get; set; }
    string Content { get; set; }
    HttpContext context { get; }
    string BaseFolder { get; set; }
    void LoadFile(string file, string id);
    void LoadFile(string file, string baseFile, string id);
    void Filter();
    CacheDependency FileCacheDependency { get; set; }
    DateTime LastModified { get; set; }
    

}
