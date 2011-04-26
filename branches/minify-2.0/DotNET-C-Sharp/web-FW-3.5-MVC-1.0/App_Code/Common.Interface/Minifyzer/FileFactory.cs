using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using Common.Minifyzer.Abstracts;

namespace Common.Minifyzer {

	public static class FileFactory {
		
		public static AType CreateFile(string File) {
			
			string extesion = File.Split('.')[File.Split('.').Length-1];

			AType file = null;
			switch(extesion){
				case "css":
					file = new CSS();
					break;
				case "js":
					file = new JS();
					break;
				default:
					throw new Exception("Tipo inválido de arquivo.");
					break;
			}
			
			file.LoadFile(File);
			file.Filter();
			
			return file;
		}
		
	}

}
