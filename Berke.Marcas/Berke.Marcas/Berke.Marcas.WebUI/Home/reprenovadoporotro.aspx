<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.repRenovadoPorOtro" CodeFile="repRenovadoPorOtro.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Marcas Renovadas por Otro</title>
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
  window.opener.document.Form1.repRenovadoPorOtroID.value = pCod ;
  window.opener.focus();
  window.close();
}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('btnGenerar');">
			<!-- 

   Cabecera 
		
   /-->
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo">Marcas Renovadas por Otro
			</P>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnlBuscar" runat="server">
				<TABLE width="100%">
					<TR>
						<TD class="subtitulo">Filtro</TD>
						<TD>
							<P>
								<asp:label id="lblMensaje" runat="server" Width="335px" CssClass="msg" Font-Bold="True" ForeColor="#C00000"></asp:label></P>
						</TD>
					</TR>
				</TABLE>
				<P></P>
				<TABLE class="infoMacro" id="tblBuscar" style="WIDTH: 517px; HEIGHT: 62px" cellSpacing="0"
					cellPadding="0" width="517" border="0">
					<TR>
						<TD>
							<asp:Label id="lblFechaAlta" runat="server" Width="104px" CssClass="Etiqueta2">Fecha Alta</asp:Label></TD>
						<TD>
							<asp:textbox id="txtFechaAlta" runat="server" Width="160px"></asp:textbox>&nbsp;
						</TD>
						<TD>&nbsp;
							<asp:Label id="Label1" runat="server" Width="264px" CssClass="Etiqueta2" Font-Bold="True">Corresponde a la fecha de procesamiento del Boletin</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblFuncionario" runat="server" Width="104px" CssClass="Etiqueta2">Fecha de Solicitud</asp:Label></TD>
						<TD>
							<asp:textbox id="txtFechaSol" runat="server" Width="160px"></asp:textbox></TD>
						<TD>&nbsp;
							<asp:Label id="Label2" runat="server" Width="280px" CssClass="Etiqueta2" Font-Bold="True">Corresponde a la fecha de la presentacion de la solicitud</asp:Label></TD>
					</TR>
					<TR>
						<TD></TD>
						<TD colSpan="2">
							<asp:CheckBox id="chkEspacio" runat="server" Width="192px" Text="Dejar espacio entre líneas"></asp:CheckBox></TD>
					</TR>
				</TABLE>
				<P></P>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 270px; HEIGHT: 43px">&nbsp;</TD>
						<TD style="WIDTH: 168px; HEIGHT: 43px">
							<asp:button id="btnGenerar" runat="server" Width="120px" CssClass="Button" Font-Bold="True"
								Height="20px" Text="Generar Reporte" onclick="btnGenerar_Click"></asp:button></TD>
						<TD style="WIDTH: 168px; HEIGHT: 43px">
							<P align="left">
								<asp:button id="btnGenExcel" runat="server" Width="112px" CssClass="Button" Font-Bold="True"
									Height="21px" Text="Generar en Excel" Enabled="False" Visible="False" onclick="btnGenExcel_Click"></asp:button></P>
						</TD>
						<TD style="WIDTH: 206px; HEIGHT: 43px">
							<P align="right">&nbsp;</P>
						</TD>
					</TR> <!--  
       endregion: Boton Buscar 
      /--> <!--							
      region:  Mensaje /
      -->
					<TR>
						<TD colSpan="7"></TD>
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
