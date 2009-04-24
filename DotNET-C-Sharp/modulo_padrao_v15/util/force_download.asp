<%
Option Explicit

dim obSmartUpload

dim stArquivo

stArquivo = Request("file")

if stArquivo <> "" then

	if Right(stArquivo, 3) = "jpg" or Right(stArquivo, 3) = "gif" or Right(stArquivo, 3) = "zip" or Right(stArquivo, 3) = "pdf" then
		stArquivo = Server.MapPath("../") & "\download\" & stArquivo
 		set obSmartUpload = Server.CreateObject("aspSmartUpload.SmartUpload")
		obSmartUpload.DownloadFile(stArquivo)
		set obSmartUpload = nothing
	end if
end if
%>  