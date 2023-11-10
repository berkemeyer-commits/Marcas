<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.repTramiteSitSigte" CodeFile="repTramiteSitSigte.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Reporte Tramite Situación Siguiente</title>
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
			<P><STRONG><FONT size="4">&nbsp;Reporte de Trámites</FONT> </STRONG>
			</P>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnlBuscar" runat="server">
				<P><FONT size="3"><STRONG>&nbsp;&nbsp; <U>Criterio de Búsqueda</U>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						</STRONG></FONT>
				</P>
				<P></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 593px; HEIGHT: 27px" width="593"></TD>
						<TD style="HEIGHT: 27px">&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 593px; HEIGHT: 14px" width="593">
							<asp:Label id="lblTramiteID" runat="server" Width="90px" CssClass="Etiqueta2">
         Trámite
       </asp:Label>
							<CUSTOM:DROPDOWN id="ddlTramiteID" runat="server" Width="227px" AutoPostBack="True" onselectedindexchanged="ddlTramiteID_SelectedIndexChanged"></CUSTOM:DROPDOWN></TD>
						<TD style="HEIGHT: 14px">&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 593px" width="593">
							<asp:Label id="lblTramiteSitID" runat="server" Width="90px" CssClass="Etiqueta2">
         Situación
       </asp:Label>
							<CUSTOM:DROPDOWN id="ddlTramiteSitID" runat="server" Width="227px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 593px" width="593"></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 593px" width="593"></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 593px" width="593"></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 593px" width="593"></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 593px" width="593"></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 593px" width="593"></TD>
						<TD>&nbsp;
						</TD>
					</TR>
				</TABLE>
				<P></P>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 428px; HEIGHT: 33px"></TD>
						<TD style="WIDTH: 126px; HEIGHT: 33px">
							<P align="left">
								<asp:button id="btnGenerar" runat="server" Width="112px" CssClass="Button" Font-Bold="True"
									Text="Buscar" Height="22px" onclick="btnGenerar_Click"></asp:button></P>
						</TD>
						<TD style="HEIGHT: 33px">
							<asp:button id="btnGenExcel" runat="server" Width="112px" CssClass="Button" Font-Bold="True"
								Text="Generar en Excel" Height="22px" Visible="False" onclick="btnGenExcel_Click"></asp:button></TD>
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
