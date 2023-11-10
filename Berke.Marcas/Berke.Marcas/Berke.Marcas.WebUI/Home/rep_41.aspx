<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.Rep_41" CodeFile="Rep_41.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Cambios de Situación de Expedientes de Marcas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
		<script language="JavaScript">
function restablecer(pCod){
  /* alert('ID a transferir ' + pCod ); */
  window.opener.document.Form1.ExpeMarcaCambioSitID.value = pCod ;
  window.opener.focus();
  window.close();
}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<!-- 

   Cabecera 
		
   /--><BR>
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P><STRONG><FONT size="4">&nbsp;Ordenes de Publicación </FONT></STRONG>
			</P>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnlBuscar" runat="server">
				<P><FONT size="3"><STRONG>&nbsp;&nbsp;&nbsp; <U>Criterio de Búsqueda</U>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						</STRONG></FONT>
				</P>
				<P></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 535px" width="535">
							<asp:Label id="lblAltaFecha_min" runat="server" CssClass="Etiqueta2" Width="90px">
          AltaFecha
        </asp:Label>
							<asp:textbox id="txtAltaFecha_min" runat="server" Width="136px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 535px; HEIGHT: 16px" width="535">
							<asp:Label id="lblTramiteID" runat="server" CssClass="Etiqueta2" Width="90px">
         Tramite
       </asp:Label>
							<CUSTOM:DROPDOWN id="ddlTramiteID" runat="server" Width="227px" AutoPostBack="True"></CUSTOM:DROPDOWN></TD>
						<TD style="HEIGHT: 16px">&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 535px" width="535"></TD>
						<TD>&nbsp;
						</TD>
					</TR>
				</TABLE>
				<P></P>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 124px; HEIGHT: 33px"></TD>
						<TD style="WIDTH: 126px; HEIGHT: 33px">
							<P align="left">&nbsp;</P>
						</TD>
						<TD style="HEIGHT: 33px">
							<asp:button id="btBuscar" runat="server" CssClass="Button" Width="120px" Text="Generar Documento"
								Height="22px" Font-Bold="True" onclick="btBuscar_Click"></asp:button>
							<asp:HyperLink id="lnkDocum" runat="server"></asp:HyperLink></TD>
					</TR> <!--  
       endregion: Boton Buscar 
      /--> <!--							
      region:  Mensaje /
      -->
					<TR>
						<TD colSpan="7">
							<asp:label id="lblMensaje" runat="server" Font-Bold="True" Font-Size="X-Small"></asp:label></TD>
					</TR> <!-- 			
      endregion: Mensaje  		
      /--></TABLE>
			</asp:panel>
			<P></P>
			<!---
------------------ PANEL RESULTADO ------------
-->
			<!---
------------------ FIN Panel Resultado ------------
--></form>
	</body>
</HTML>
