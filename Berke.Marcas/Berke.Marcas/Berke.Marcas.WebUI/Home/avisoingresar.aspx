<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.AvisoIngresar" CodeFile="AvisoIngresar.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ingresar Aviso</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" >
			<!-- 

   Cabecera 
		
   /-->
			
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo">Ingresar Aviso</P>
			<!-- 

   Fin Cabecera

   /-->
			<asp:panel id="pnlBuscar" runat="server" CssClass="infoMacro" Width="98%">
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="HEIGHT: 13px">
							<asp:Label id="lblDestinatario" runat="server" CssClass="Etiqueta2" Width="90px">
         Para
       </asp:Label>
							<CUSTOM:DROPDOWN id="ddlDestinatario" runat="server" Width="208px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
						<TD style="HEIGHT: 13px">&nbsp;
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblAsunto" runat="server" CssClass="Etiqueta2" Width="90px">Asunto</asp:Label>
							<asp:textbox id="txtAsunto" runat="server" Width="456px" MaxLength="120"></asp:textbox></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="left">
							<asp:Label id="lblContenido" runat="server" CssClass="Etiqueta2" Width="90px" Height="39px">Contenido</asp:Label>
							<asp:textbox id="txtContenido" runat="server" Width="496px" MaxLength="200" Height="40px" TextMode="MultiLine"
								Rows="5"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblIndicaciones" runat="server" CssClass="Etiqueta2" Width="90px">Indicaciones</asp:Label>
							<asp:textbox id="txtIndicaciones" runat="server" Width="496px" MaxLength="200"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblFechaAviso" runat="server" CssClass="Etiqueta2" Width="90px">FechaAviso</asp:Label>
							<asp:textbox id="txtFechaAviso" runat="server" Width="128px"></asp:textbox>
							<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="100px">Prioridad</asp:Label>
							<CUSTOM:DROPDOWN id="ddlPrioridad" runat="server" Width="208px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
					</TR>
				</TABLE>
				<P></P>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 122px; HEIGHT: 19px"></TD>
						<TD style="WIDTH: 206px; HEIGHT: 19px">
							<P align="left">
								<asp:button id="btGrabar" runat="server" CssClass="Button" Text="Grabar" Font-Bold="True" onclick="btGrabar_Click"></asp:button></P>
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
			<P>
				<asp:Literal id="litPaginaAnterior" runat="server" Visible="False"></asp:Literal></P>
		</form>
	</body>
</HTML>
