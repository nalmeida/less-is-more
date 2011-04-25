using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Configuration;
using System.Text;
using Common.Minifyzer;

namespace Common{
	public static class Minify {
	
		private static HttpContext context = HttpContext.Current;
		private static List<FileGroup> groups;
		private static string HandlerPath;

		/**
		 * Register a file to be loaded, parsed and cached
		 */
		public static void Register(string File) {
			Register(File, File);
		}
		public static void Register(string Key, string File) {
			Register(Key, File, null);
		}
		public static void Register(string Key, string File, string Url) {
			IFile MinifiedFile = (IFile)context.Cache[Key];
			if (MinifiedFile == null || MinifiedFile.Name != File) {
				MinifiedFile = CreateFile(File, Url, Key);
				context.Cache.Insert(Key, MinifiedFile, MinifiedFile.FileCacheDependency);
			}
		}
	
		/**
		 * Group files do groups
		 */
		public static FileGroup Add(string GroupId, string FileId) {
			if(groups == null){
				groups = new List<FileGroup>();
			}
		
			IFile cache = (IFile)context.Cache[FileId];
			if(cache == null){
				throw new Exception("Erro ao adicionar arquivo ao grupo: "+FileId+" não está registrado.");
			}
			IContent content = ContentFactory.CreateContent(cache.Name);
		
			foreach (FileGroup item in groups) {
				if(item.Content.Type == content.Type){
					item.Add(GroupId, FileId);
					return item;
				}
			}
			FileGroup group = new FileGroup();
			group.Add(GroupId, FileId);
			groups.Add(group);
			return group;
		}
	
		/**
		 * Get content of group or file as string 
		 */
		public static string GetCode(string GroupId) {
			return GetCode(GroupId, context);
		}
		public static string GetCode(string GroupId, HttpContext context) {

			FileGroup group = GetGroupById(GroupId);
			string FileDates = "";
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
				FileDates = "";
				sbGroupContent.Append(ifile.Content);
			}else{
				throw new Exception("Grupo ou Arquivo não registrado: "+GroupId);
			}

			sbGroupContent.Insert(0, FileDates);
			return sbGroupContent.ToString();
		}
	
		/**
		 * Get a html tag calling the handler (group or File id)
		 */
		public static string GetTag(string GroupId) {
			string Response = "";

			FileGroup group = GetGroupById(GroupId);
			
			if(group != null) {
				Response = IsDebug() ?
					GetDebugTag(group.FilesList[GroupId]) : 
					string.Format(group.Tag, Common.Util.Root + GetHandlerPath() + "?" + GroupId);
			} else if (context.Cache[GroupId] != null) {
				IFile TagFile = (IFile)context.Cache[GroupId];
				Response = IsDebug() ? 
					GetDebugTag(TagFile) :
					string.Format(TagFile.Tag, Common.Util.Root + GetHandlerPath() + "?" + GroupId);
			}else{
				throw new Exception("Grupo ou Arquivo não registrado: "+GroupId);
			}
			
			return Response;
		}
		
		/**
		 * Write tag or code on the current context (wrapper)
		 */
		public static void WriteTag(string GroupId){
			HttpContext.Current.Response.Write(GetTag(GroupId));
		}
		public static void Write(string GroupId){
			HttpContext.Current.Response.Write(GetCode(GroupId));
		}
		
		/**
		 * Get Tag without minify management (for debugging purpose)
		 */
		public static string GetDebugTag(List<IFile> list){
			string Response = "";
			foreach(IFile item in list){
				Response += GetDebugTag(item);
			}
			return Response;
		}
		public static string GetDebugTag(IFile file){
			return string.Format(file.Tag, file.GetVirtualPath());
		}
	

		/**
		 * Create File based on the file name
		 */
		private static IFile CreateFile(string File, string Key) {
			return CreateFile(File, null, Key);
		}
		private static IFile CreateFile(string File, string Url, string Key) {
			IFile mFile = FileFactory.CreateFile(File);
			mFile.LoadFile(File, Url, Key);
			mFile.Filter();
			return mFile;
		}
		
		/**
		 * debug getter
		 */
		public static bool IsDebug(){
			// TODO: mecanism with token via querystring
			return ConfigurationManager.AppSettings["MinifyDebug"] == "true";
		}
		
		/**
		 * Groups Getters By Id
		 */
		public static FileGroup GetGroupById(string Id){
			foreach (FileGroup item in groups) {
				if(item.FilesList.ContainsKey(Id)){
					return item;
				}
			}
			return null;
		} 
		public static string GetGroupContentType(string GroupId) {
			FileGroup group = GetGroupById(GroupId);
			return group != null ? group.Content.Type : null;
		}
		public static string GetFileContentType(string GroupId) {
			IFile file = (IFile)context.Cache[GroupId];
			return file != null ? ContentFactory.CreateContent(file.Name).Type : null;
		}
		public static string GetHandlerPath(){
			if(string.IsNullOrEmpty(HandlerPath)){
				HttpHandlersSection handlers = (HttpHandlersSection)ConfigurationManager.GetSection("system.web/httpHandlers");
				foreach(HttpHandlerAction handler in handlers.Handlers){
					if(handler.Type == typeof(MinifyHandler).FullName){
						HandlerPath = handler.Path;
						break;
					}
				}
			}
			return HandlerPath;
		}
	}
}