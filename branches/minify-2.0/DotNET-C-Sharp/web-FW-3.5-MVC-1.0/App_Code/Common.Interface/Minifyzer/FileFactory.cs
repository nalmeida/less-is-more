using System;
using System.Collections.Generic;
using System.Web;
using System.Net;

namespace Common.Minifyzer {

	public static class FileFactory {
		
		public static IFile CreateFile(string File) {
			
			string extesion = File.Split('.')[File.Split('.').Length-1];

			IFile file = null;
			switch(extesion){
				case "css":
					file = new CSS();
					break;
				case "js":
					file = new JavaScript();
					break;
				default:
					throw new Exception("Tipo inválido de arquivo.");
					break;
			}
			return file;
		}
		
	}

}
