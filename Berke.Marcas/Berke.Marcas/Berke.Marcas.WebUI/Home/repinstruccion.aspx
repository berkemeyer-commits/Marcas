<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#"  smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.repInstruccion" CodeFile="repInstruccion.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Instrucciones</title>
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
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('btnGenerar');">
			<!-- 

   Cabecera 
		
   /-->
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo">Instrucciones</P>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnlBuscar" runat="server" Width="98%" CssClass="infoMacro">
				<P class="subtitulo">Filtro</P>
				<P></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblFechaAlta" runat="server" CssClass="Etiqueta2" Width="90px">Fecha Alta</asp:Label>
							<asp:textbox id="txtFechaAlta" runat="server" Width="96px"></asp:textbox>&nbsp;-&nbsp;
							<asp:textbox id="txtFechaAlta_Max" runat="server" Width="96px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblFuncionario" runat="server" CssClass="Etiqueta2" Width="90px">Funcionario</asp:Label>
							<CUSTOM:DROPDOWN id="ddlFuncionario" runat="server" Width="209px" AutoPostBack="True"></CUSTOM:DROPDOWN></TD>
						<TD>&nbsp;
							<asp:Label id="lblInstruccion" runat="server" CssClass="Etiqueta2" Width="30px">Tipo</asp:Label>
							<CUSTOM:DROPDOWN id="ddlInstruccion" runat="server" Width="210px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
					</TR>
				</TABLE>
				<P></P>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 270px; HEIGHT: 43px">&nbsp;</TD>
						<TD style="WIDTH: 168px; HEIGHT: 43px">
							<asp:button id="btnGenerar" runat="server" CssClass="Button" Width="120px" Text="Generar Reporte"
								Font-Bold="True" Height="20px" onclick="btnGenerar_Click"></asp:button></TD>
						<TD style="WIDTH: 168px; HEIGHT: 43px">
							<P align="left">
								<asp:button id="btnGenExcel" runat="server" CssClass="Button" Width="112px" Text="Generar en Excel"
									Font-Bold="True" Height="21px" onclick="btnGenExcel_Click"></asp:button></P>
						</TD>
						<TD style="WIDTH: 206px; HEIGHT: 43px">
							<P align="right">
								<asp:button id="btnContar" runat="server" CssClass="Button" Width="138px" Text="Contar" Font-Bold="True"
									Height="20px" Visible="False" onclick="btnContar_Click"></asp:button></P>
						</TD>
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
