<%
Option Explicit

dim objFBIZOrigem
dim idUsuario, idOrigem 
dim vRetorno
dim stURL

idUsuario = replace(Request("idUsuario")," ","")
idOrigem  = Int(0 & replace(Request("idOrigem")," ",""))

if Not IsNumeric(trim(IdUsuario)) then
	idUsuario = -1
else 
    idUsuario = int(idUsuario)
end if

if IdOrigem > 0 then 

    Set objFBIZOrigem = Server.CreateObject("FBIZOrigem.clsFBIZOrigem")
    vRetorno = objFBIZOrigem.fncContabilizaV2(Int(IdOrigem), _
    		  								  Int(IdUsuario), _
    										  cStr(Application("cnStrFBIZ")))
    Set objFBIZOrigem = Nothing

    vRetorno = Split(vRetorno,"||")

    If Int(vRetorno(1)) = 1 Then
    	Response.End
    End If
   
    if len(trim(vRetorno(0))) > 0 and not IsNull(vRetorno(0)) then
        stURL = Replace(lcase(vRetorno(0)), "<idusuario>", idUsuario)
    	Response.Redirect stURL
    	Response.End 
    else
        stURL = cstr("http://"& Request.ServerVariables("HTTP_HOST"))
        Response.Redirect stURL
        Response.End
    end if
    
else
    stURL = cstr("http://"& Request.ServerVariables("HTTP_HOST"))
    Response.Redirect stURL
    Response.End

end if

%>