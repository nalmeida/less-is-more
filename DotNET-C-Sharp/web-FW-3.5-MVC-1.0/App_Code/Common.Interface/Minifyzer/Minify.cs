using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Configuration;
using System.Text;
using Common.Minifyzer;
using Common.Minifyzer.Abstracts;
using Common.Minifyzer.Interfaces;

namespace Common.Minifyzer{

	public class Minify{

		private HttpContext context = HttpContext.Current;

		private Hashtable Files;
		public List<AType> CurrentList;

		public Minify(){
			Files = new Hashtable();
			CurrentList = new List<AType>();
		}
		public Minify Clear(){
			Files.Clear();
			CurrentList.Clear();
			return this;
		}
		public Minify Add(string File){
	
			AType CurrentFile = (AType)Files[File];

			// Add to hasTable (total items)
			if(CurrentFile == null){
				CurrentFile = CreateFile(File);
				Files.Add(File, CurrentFile);
			}
			
			// Add to current list
			if(!CurrentList.Contains(CurrentFile)){
				CurrentList.Add(CurrentFile);
			}

			return this;
		}

		/**
		 * Get content of file as string 
		 */
		public string GetCode() {
			// string CurrentContent = "";
			// foreach(AType item in CurrentList){
			// 	CurrentContent += item.Content;
			// }
			// CurrentList.Clear();
			// return CurrentContent;
			return "";
		}

		/**
		 * Get a html tag calling the handler (group or File id)
		 */
		public string GetTag() {
			Hashtable AddressList = new Hashtable();
			Hashtable PropertyList = new Hashtable();
			string CurrentType = "";
			string ReturnValue = "";
			List<string> GroupList = new List<string>();
			
			foreach(IType item in CurrentList){
				CurrentType = item.Extension;
				if(string.IsNullOrEmpty(item.Extension)){
					continue;
				}
				if(AddressList[CurrentType] == null){
					PropertyList.Add(CurrentType, item);
					AddressList.Add(CurrentType, new List<string>());
				}
				// context.Response.Write(item.Extension+", "+item.VirtualPath+"<br>");
				((List<string>)AddressList[CurrentType]).Add(item.VirtualPath);
			}
			
			
			string CurrentAddress = "";
			foreach(DictionaryEntry item in AddressList){
				CurrentAddress = string.Join("|", ((List<string>)item.Value).ToArray());
				ReturnValue += string.Format(((IType)PropertyList[item.Key]).Tag, Common.Util.Root + Common.Minify.GetHandlerPath() + "?" + CurrentAddress) + "\n";
			}
			
			return ReturnValue;
			// string Response = "";
			// 
			// FileGroup group = GetGroupById(GroupId);
			// 
			// if(group != null) {
			// 	Response = IsDebug() ?
			// 		GetDebugTag(group.FilesList[GroupId]) : 
			// 		string.Format(group.Tag, Common.Util.Root + GetHandlerPath() + "?" + GroupId);
			// } else if (context.Cache[GroupId] != null) {
			// 	AType TagFile = (AType)context.Cache[GroupId];
			// 	Response = IsDebug() ? 
			// 		GetDebugTag(TagFile) :
			// 		string.Format(TagFile.Tag, Common.Util.Root + GetHandlerPath() + "?" + GroupId);
			// }else{
			// 	throw new Exception("Grupo ou Arquivo não registrado: "+GroupId);
			// }
			// 
			// return Response;
			return "";
		}

		/**
		 * Write tag or code on the current context (wrapper)
		 */
		public Minify Write(){
			HttpContext.Current.Response.Write(GetTag());
			return this;
		}
		public Minify Tag(){
			HttpContext.Current.Response.Write("");
			return this;
		}

		/**
		 * Get Tag without minify management (for debugging purpose)
		 */
		public string GetDebugTag(List<AType> list){
			// string Response = "";
			// foreach(AType item in list){
			// 	Response += GetDebugTag(item);
			// }
			// return Response;
			return "";
		}
		public string GetDebugTag(AType file){
			// return string.Format(file.Tag, file.VirtualPath);
			return "";
		}


		/**
		 * Create File based on the file name
		 */
		private AType CreateFile(string File) {
			return FileFactory.CreateFile(File);
		}

		/**
		 * debug getter
		 */
		public bool IsDebug(){
			// TODO: mecanism with token via querystring
			return ConfigurationManager.AppSettings["MinifyDebug"] == "true";
		}
	}
}