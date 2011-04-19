using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Text;
using Common.Minifyzer;

namespace Common{
	public static class Minify {
	
		private static HttpContext context = HttpContext.Current;
		private static List<FileGroup> groups;

		public static void Register(string File) {
			Register(File, File);
		}
		public static void Register(string Key, string File) {
			Register(Key, File, null);
		}
		public static void Register(string Key, string File, string BaseFolder) {
			if (context.Cache[Key] == null) {
				IFile MinifiedFile = CreateFile(File, BaseFolder, Key);
				context.Cache.Insert(Key, MinifiedFile, MinifiedFile.FileCacheDependency);
			}
		}
	
	
		public static void Add(string GroupId, string FileId) {
			CreateGroup(GroupId, FileId);
		}
	
	
		public static string Write(String GroupId) {
			return Write(GroupId, context);
		}
		public static string Write(String GroupId, HttpContext context) {

			FileGroup group = GetGroupById(GroupId);
			String FileDates = "";
			StringBuilder sbGroupContent = new StringBuilder();
			IFile ifile;
		
			if(group != null){
				List<IFile> GroupItens = group.FilesList[GroupId];
				sbGroupContent.Append(group.Content.CustomHeader());
				foreach (IFile item in GroupItens) {
					FileDates += "\n /* " + item.Id + " | " + item.LastModified + " */ \n";
					if (context.Cache[item.Id] != null) {
						sbGroupContent.Append(item.Content);
					} else {
						Register(item.Id, item.Name);
						ifile = (IFile)context.Cache[item.Id];
						sbGroupContent.Append(ifile.Content);
					}
				}
			}else if(context.Cache[GroupId] != null){ // GroupId is FileId
				ifile = (IFile)context.Cache[GroupId];
				FileDates += "\n /* " + GroupId + " | " + ifile.LastModified + " */ \n";
				sbGroupContent.Append(ifile.Content);
			}else{
				throw new Exception("Group or File not registered: "+GroupId);
			}

			sbGroupContent.Insert(0, FileDates);
			return sbGroupContent.ToString();
		}
	
		public static string WriteTag(String GroupId) {
			String Response = "";

			FileGroup group = GetGroupById(GroupId);
			if(group != null) {
				Response = String.Format(group.Tag, Common.Util.Root + "minify.aspx?" + GroupId);
			} else if (context.Cache[GroupId] != null) {
				IFile TagFile = (IFile)context.Cache[GroupId];
				Response = String.Format(TagFile.Tag, Common.Util.Root + "minify.aspx?" + GroupId);
			}else{
				throw new Exception("Group or File not registered: "+GroupId);
			}
			return Response;
		}
	
		private static FileGroup CreateGroup(string Id, string File){
			if(groups == null){
				groups = new List<FileGroup>();
			}
		
			IFile cache = (IFile)context.Cache[File];
			if(cache == null){
				throw new Exception("Erro ao adicionar arquivo ao grupo: "+File+" não está registrado.");
			}
			IContent content = ContentFactory.CreateContent(cache.Name);
		
			foreach (FileGroup item in groups) {
				if(item.Content.Type == content.Type){
					item.Add(Id, File);
					return item;
				}
			}
			FileGroup group = new FileGroup();
			group.Add(Id, File);
			groups.Add(group);
			return group;
		} 
		public static FileGroup GetGroupById(string Id){
			foreach (FileGroup item in groups) {
				if(item.FilesList.ContainsKey(Id)){
					return item;
				}
			}
			return null;
		} 
		public static string getGroupContentType(string GroupId) {
			FileGroup group = GetGroupById(GroupId);
			return group != null ? group.Content.Type : null;
		}
		public static string getFileContentType(string GroupId) {
			IFile file = (IFile)context.Cache[GroupId];
			return file != null ? ContentFactory.CreateContent(file.Name).Type : null;
		}
	
		private static IFile CreateFile(string File, string Key) {
			return CreateFile(File, null, Key);
		}
		private static IFile CreateFile(string File, string BaseFolder, string Key) {
			IFile mFile = FileFactory.CreateFile(File);
			if(BaseFolder != null){
				mFile.LoadFile(File, BaseFolder, Key);
			}else{
				mFile.LoadFile(File, Key);
			}
			mFile.Filter();
			return mFile;
		}
	}
}