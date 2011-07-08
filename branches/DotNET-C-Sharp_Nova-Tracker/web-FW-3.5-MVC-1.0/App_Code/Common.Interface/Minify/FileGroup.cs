using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for FileGroup
/// </summary>
/// 

namespace Minify
{
    public class FileGroup
    {
        public string Id { get; set; }
        public IContent Content { get; set; }
        public Dictionary<String, List<IFile>> FilesList { get; set; }
        
        public FileGroup()
        {
            FilesList = new Dictionary<string, List<IFile>>();
        }
        public void Add(String GroupId, String File)
        {
            IFile CachedFile = getFileFromCache(File);
            if (!FilesList.ContainsKey(GroupId))
            {
                List<IFile> listFile = new List<IFile>();
                listFile.Add(CachedFile);
                FilesList.Add(GroupId, listFile);
            }
            else
            {
                FilesList[GroupId].Add(CachedFile);
            }
            Content = ContentFactory.CreateContent(CachedFile.Name);
            HttpContext.Current.Response.ContentType = Content.Type;

        }

        private IFile getFileFromCache(string FileId)
        {
            if (HttpContext.Current.Cache[FileId] == null)
            {
                throw new ArgumentException("The File " + FileId + " unregistered");
            }
            return (IFile)HttpContext.Current.Cache[FileId];
        }
    }

}
