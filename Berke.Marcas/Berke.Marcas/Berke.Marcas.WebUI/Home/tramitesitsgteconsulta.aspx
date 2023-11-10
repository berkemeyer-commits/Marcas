<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#"  smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.SituacionSiguienteConsulta" CodeFile="TramiteSitSgteConsulta.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Situación Siguiente</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('btBuscar');">
			<!-- 

   Cabecera 
		
   /-->
		
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo">Consultar Transiciones entre Situaciones</P>
			<!-- 

   Fin Cabecera

   /-->
			<asp:panel id="pnlBuscar" runat="server" Width="98%" CssClass="infoMacro">
				<P class="subtitulo">Criterio de Búsqueda</P>
				<P></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 390px" width="390"></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblTramiteID" runat="server" Width="130px" CssClass="Etiqueta2">Trámite</asp:Label>
							<CUSTOM:DROPDOWN id="ddlTramite" runat="server" Width="210px" AutoPostBack="True" onselectedindexchanged="ddlTramite_SelectedIndexChanged"></CUSTOM:DROPDOWN></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px; HEIGHT: 12px" width="390"></TD>
						<TD style="HEIGHT: 12px">&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px; HEIGHT: 15px" width="390">
							<asp:Label id="lblTramiteSit" runat="server" Width="130px" CssClass="Etiqueta2">Situación Origen</asp:Label>
							<CUSTOM:DROPDOWN id="ddlTramiteSit" runat="server" Width="210px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
						<TD style="HEIGHT: 15px">&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390"></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblTramiteSitSgte" runat="server" Width="130px" CssClass="Etiqueta2">Situación Destino</asp:Label>
							<CUSTOM:DROPDOWN id="ddlTramiteSitSgte" runat="server" Width="210px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390"></TD>
						<TD>&nbsp;
						</TD>
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
								<asp:button id="btBuscar" runat="server" Width="96px" CssClass="Button" Font-Bold="True" Text="Buscar"
									Height="22px" onclick="btBuscar_Click"></asp:button></P>
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
			<asp:panel id="pnlResultado" runat="server" Visible="True">
				<P class="subtitulo">Resultado de la Búsqueda</P>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head" width="100%">
								<TR>
									<TD>
										<asp:label id="lblTituloGrid" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="dgResult" runat="server" Width="100%" CssClass="tbl" HorizontalAlign="Center"
								DataKeyField="id" AutoGenerateColumns="False">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="id" HeaderText="id"></asp:BoundColumn>
									<asp:BoundColumn DataField="T_Orig" HeaderText="Descripcion"></asp:BoundColumn>
									<asp:BoundColumn DataField="Origen" HeaderText="Origen">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Destino" HeaderText="Destino">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!---
------------------ FIN Panel Resultado ------------
--></form>
	</body>
</HTML>
