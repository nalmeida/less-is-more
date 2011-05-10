using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Configuration;
using System.Text;
using CombineAndMinify;

namespace Common{
	public static class Minify {
	
		// STATIC STUFF
		private static CombineAndMinify.StandAlone singleInst;
		private static CombineAndMinify.StandAlone single {
			get {
				if(singleInst == null){
					singleInst = new CombineAndMinify.StandAlone();
				}
				return singleInst;
			}
		}
		public static CombineAndMinify.StandAlone Clear(){
			return single.Clear();
		}
		public static CombineAndMinify.StandAlone Add(string File){
			return single.Add(File);
		}
		public static CombineAndMinify.StandAlone Write(){
			return single.Write();
		}
		public static string GetImagePath(string File){
			return single.GetImagePath(File);
		}
		public static bool IsDebug{
			get {
				return single.IsDebug;
			}
		}
	}
}