using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Text;

/// <summary>
/// Summary description for FilleCollections
/// </summary>
/// 

namespace Minify
{
    public static class Minifyzer
    {
        private static FileGroup Group { get; set; }
        private static HttpContext context = HttpContext.Current;

        static Minifyzer()
        {
            Group = new FileGroup();
        }

        public static void Register(string Key, string File)
        {
            if (context.Cache[Key] == null)
            {
                IFile MinifiedFile = CreateFile(File, Key);
                context.Cache.Insert(Key, MinifiedFile, MinifiedFile.FileCacheDependency);
            }
        }
        public static void Register(string Key, string File, string BaseFolder)
        {
            if (context.Cache[Key] == null)
            {
                IFile MinifiedFile = CreateFile(File, BaseFolder, Key);
                context.Cache.Insert(Key, MinifiedFile, MinifiedFile.FileCacheDependency);
            }
        }
        public static void Add(string GroupId, string FileId)
        {
            try
            {
                Group.Add(GroupId, FileId);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static string Write(String GroupId)
        {
            String FileDates = "";

            StringBuilder sbGroupContent = new StringBuilder();
            List<IFile> GroupItens = Group.FilesList[GroupId];

            sbGroupContent.Append(Group.Content.CustonHeader());

            foreach (IFile item in GroupItens)
            {
                if (context.Cache[item.Id] != null)
                {
                    FileDates += "\n /* " + item.Id + " | " + item.LastModified + " */ \n";
                    sbGroupContent.Append(item.Content);
                }
                else
                {
                    IFile ifile = (IFile)context.Cache[item.Id];
                    FileDates += "\n /* " + ifile.Id + " | " + ifile.LastModified + " */ \n";
                    Register(item.Id, item.Name);
                    sbGroupContent.Append(ifile.Content);
                }
                
            }
            sbGroupContent.Insert(0, FileDates);
            return sbGroupContent.ToString();
        }
        public static string Write(String GroupId, HttpContext context)
        {
            String FileDates = "";

            StringBuilder sbGroupContent = new StringBuilder();
            List<IFile> GroupItens = Group.FilesList[GroupId];

            sbGroupContent.Append(Group.Content.CustonHeader());

            foreach (IFile item in GroupItens)
            {
                
                if (context.Cache[item.Id] != null)
                {
                    FileDates += "\n /* " + item.Id + " | " + item.LastModified + " */ \n";
                    sbGroupContent.Append(item.Content);
                }
                else
                {
                    Register(item.Id, item.Name);
                    IFile ifile = (IFile)context.Cache[item.Id];
                    FileDates += "\n /* " + ifile.Id + " | " + ifile.LastModified + " */ \n";
                    sbGroupContent.Append(ifile.Content);
                }
            }

            sbGroupContent.Insert(0, FileDates);
            context.Response.ContentType = Group.Content.Type;
            return sbGroupContent.ToString();
        }
        public static string WriteTag(String GroupId)
        {
            String Response = "";

            if (context.Cache[GroupId] != null)
            {
                IFile TagFile = (IFile)context.Cache[GroupId];
                String FileExtension = TagFile.Name.Split('.')[1].ToLower();
                if (FileExtension == "js")
                {
                    Response = String.Format("<script type=\"text/javascript\" src=\"{0}\" /></script>", Common.Util.Root + "alphaminify.aspx?" + GroupId);
                }
                else if (FileExtension == "css")
                {
                    Response = String.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" media=\"screen\" />", Common.Util.Root + "alphaminify.aspx?" + GroupId);
                }
                else
                {
                    throw new Exception("Extensão desconhecida");
                }
            }
            else if (Group.FilesList.ContainsKey(GroupId))
            {

                if (Group.Content.Type == "text/javascript")
                {
                    Response = String.Format("<script type=\"text/javascript\" src=\"{0}\" />", Common.Util.Root + "alphaminify.aspx?" + GroupId);
                }
                else if (Group.Content.Type == "text/css")
                {
                    Response = String.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" media=\"screen\" />", Common.Util.Root + "alphaminify.aspx?" + GroupId);
                }
            }


            return Response;

        }
        public static bool GroupExists(String GroupId)
        {
            return Group.FilesList.ContainsKey(GroupId);
        }
        private static IFile CreateFile(string File, string Key)
        {
            IFile mFile = FileFactory.CreateFile(File);
            mFile.LoadFile(File, Key);
            mFile.Filter();
            return mFile;
        }
        private static IFile CreateFile(string File, string BaseFolder, string Key)
        {
            IFile mFile = FileFactory.CreateFile(File);
            mFile.LoadFile(File, BaseFolder, Key);
            mFile.Filter();
            return mFile;
        }


    }
}

