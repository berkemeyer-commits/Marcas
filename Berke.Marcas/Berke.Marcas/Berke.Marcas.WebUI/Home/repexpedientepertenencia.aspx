<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.repExpedientePertenencia" CodeFile="repExpedientePertenencia.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>repExpedientePertenencia</title>
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
  window.opener.document.Form1.repInstruccionID.value = pCod ;
  window.opener.focus();
  window.close();
}
		</script>
	</HEAD>
	<body>
		<form id="Form2" method="post" runat="server" onkeypress="form_onEnter('btnGenerar');">
			<!-- Cabecera -->
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo">Cambios de Estado</P>
			<!-- Fin Cabecera-->
			<asp:panel id="pnlBuscar" runat="server">
				<P class="subtitulo">Filtro</P>
				<P></P>
				<TABLE id="tblBuscar" class="infoMacro" width="98%">
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblFechaAlta" runat="server" CssClass="Etiqueta2" Width="144px">Fecha de Alta (rango)</asp:Label>
							<asp:textbox id="txtFechaAlta" runat="server" Width="96px"></asp:textbox>&nbsp;-&nbsp;
							<asp:textbox id="txtFechaAlta_Max" runat="server" Width="96px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
					</TR>
				</TABLE>
				<P></P>
				<TABLE id="table1" cellSpacing="0" cellPadding="0" width="51%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 13px; HEIGHT: 19px"></TD>
						<TD style="WIDTH: 94px; HEIGHT: 19px">
							<P align="left"></P>
						<TD style="WIDTH: 112px">
							<asp:button id="btnGenerar" runat="server" CssClass="Button" Width="112px" Text="Generar Reporte"
								Font-Bold="True" Height="21px" onclick="btnGenerar_Click"></asp:button>&nbsp;</TD>
						<TD style="WIDTH: 115px">
							<asp:button id="btnGenExcel" runat="server" CssClass="Button" Width="112px" Text="Generar en Excel"
								Font-Bold="True" Height="21px" onclick="btnGenExcel_Click"></asp:button></TD>
						
						</TD></TR> <!--  
    
       endregion: Boton Buscar 
      /--> <!--							
      region:  Mensaje /
      -->
					<TR>
						<TD style="WIDTH: 474px" colSpan="7">
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
		<!-- hasta aquí -->
	</body>
</HTML>
