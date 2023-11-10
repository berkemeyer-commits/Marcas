'------------------------------------------------------------
' Ejemplo de uso de las funciones administrativas del
' MBI desde un script. 
' Eugenio Pace - eugeniop@microsoft.com
' Mayo 2002 
'
' MBI expone gran parte de sus funciones administrativas
' a través de "System Actions".
' Técnicamente son equivalentes a cualquier otra IAction,
' con la excepción que se encuentran implementadas en 
' el core del MBI.
' En este ejemplo se invocan las siguientes System Actions:
'	- Ping:			
'		. Para verificar que el servicio responda
'	- ListConfigParams:
'		. Obtiene la configuración completa de la
'		  instalacion en XML
' 	- ListActions:
'		. Obtiene la lista de Acciones declaradas
'		  en el sistema
'	- ReloadConfig:
'		. Recarga la configuracion del fwk
'------------------------------------------------------------

Dim ac
Dim req
Dim res
Dim Shell

'Objetos utilitarios
Set res = Wscript.CreateObject( "MSXML.DOMDocument" )
set Shell = WScript.CreateObject("WScript.Shell")

'Adaptador de canal COM
Set ac = WScript.CreateObject( "Framework.COMChannel" )

'Configura remoting
ac.DispatcherURL="[svr_url]"

'Ejecuta la "Systemaction" PING.

'Wscript.Echo ac.ExecuteAction( "<Request><Action>system.Ping</Action><TestReq><Param><Application>Framework</Application></Param></TestReq></Request>" )

res.loadxml ac.ExecuteAction( CreateRequest( "system.Ping" ) )
res.save "c:\response.xml" 

Shell.Run "c:\response.xml"

Wscript.Quit(0)
msgbox "Next"

'Ejecuta la SystemAction LISTACTIONS 
res.loadxml ac.ExecuteAction( CreateRequest( "system.ListActions" ) )
res.save "c:\response.xml" 

Shell.Run "c:\response.xml"

msgbox "Next"


'Ejecuta la SystemAction LISTMESSAGES
res.loadxml ac.ExecuteAction( CreateRequest( "system.ListMessages" ) )
res.save "c:\response.xml" 

Shell.Run "c:\response.xml"

msgbox "Next"


'Ejecuta la SystemAction LISTCONFIGPARAMS 
res.loadxml ac.ExecuteAction( CreateRequest( "system.ListConfigParams" ) )
res.save "c:\response.xml" 

Shell.Run "c:\response.xml"

msgbox "Next"


'Ejecuta la SystemAction RELOADCONFIG
'res.loadxml ac.ExecuteAction( CreateRequest( "system.ReloadConfig" ) )
'res.save "c:\response.xml" 

Shell.Run "c:\response.xml"


Function CreateRequest( action )
	CreateRequest="<Request><Action>" & action & _
	      "</Action><TestReq><Param></Param></TestReq></Request>"
End Function
