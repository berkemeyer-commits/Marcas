<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.PoderConsulta" CodeFile="PoderConsulta.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consulta de Poderes </title>
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
			<p class="titulo">Consultar Poderes</p>
			<!-- 

   Fin Cabecera


   /-->
			<TABLE width="720">
				<TR>
					<TD align="right"><A onclick="closeDiv('tblDivBuscar');" href="#">Cick aquí para 
							ocultar/visualizar el formulario </A>
					</TD>
				</TR>
			</TABLE>
			<div id="tblDivBuscar">
				<asp:panel id="pnlBuscar" runat="server">
					<P class="subtitulo">Criterio de Búsqueda</P>
					<P></P>
					<TABLE class="infoMacro" id="tblBuscar">
						<TR>
							<TD style="WIDTH: 110px" width="110">
								<asp:Label id="lblID_min" runat="server" CssClass="Etiqueta2" Width="90px">ID</asp:Label>
							<TD style="WIDTH: 279px">
								<asp:textbox id="txtID_min" runat="server" Width="160px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 110px" width="110">
								<asp:Label id="lblDenominacion" runat="server" CssClass="Etiqueta2" Width="90px">
          Denominación
        </asp:Label></TD>
							<TD style="WIDTH: 279px">
								<asp:textbox id="txtDenominacion" runat="server" Width="250px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 110px" width="110">
								<asp:Label id="lblConcepto" runat="server" CssClass="Etiqueta2" Width="90px">Concepto</asp:Label>
							<TD style="WIDTH: 279px">
								<asp:textbox id="txtConcepto" runat="server" Width="250px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 110px" width="110">
								<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="88px">Nro.Acta</asp:Label></TD>
							<TD style="WIDTH: 279px">
								<asp:textbox id="txtActaNro" runat="server" Width="250px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 110px" width="110">
								<asp:Label id="Label2" runat="server" CssClass="Etiqueta2" Width="88px">Año Acta</asp:Label></TD>
							<TD style="WIDTH: 279px">
								<asp:textbox id="txtActaAnio" runat="server" Width="250px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 110px" width="110">
								<asp:Label id="lblInscripcionNro" runat="server" CssClass="Etiqueta2" Width="88px">Nro.Inscripción</asp:Label></TD>
							<TD style="WIDTH: 279px">
								<asp:textbox id="txtInscripcionNro" runat="server" Width="250px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 110px" width="110">
								<asp:Label id="Label3" runat="server" CssClass="Etiqueta2" Width="88px">Año Inscripción</asp:Label></TD>
							<TD style="WIDTH: 279px">
								<asp:textbox id="txtInscripcionAnio" runat="server" Width="250px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 110px" width="110">
								<asp:Label id="lblPais" runat="server" CssClass="Etiqueta2" Width="88px">Pais</asp:Label></TD>
							<TD style="WIDTH: 279px">
								<asp:textbox id="txtPais" runat="server" Width="250px"></asp:textbox></TD>
						</TR>
                        <TR>
							<TD style="WIDTH: 110px" width="110">
								<asp:Label id="Label4" runat="server" CssClass="Etiqueta2" Width="88px">Domicilio</asp:Label></TD>
							<TD style="WIDTH: 279px">
								<asp:textbox id="txtDomicilio" runat="server" Width="250px"></asp:textbox></TD>
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
									<asp:button id="btBuscar" runat="server" CssClass="Button" Width="96px" Height="22px" Text="Buscar"
										Font-Bold="True" onclick="btBuscar_Click"></asp:button></P>
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
			</div>
			<P></P>
			<!-- 

   Fin Cabecera

   /-->
			<P></P>
			<!---
------------------ PANEL RESULTADO ------------
-->
			<!---
------------------ FIN Panel Resultado ------------
-->
			<asp:panel id="pnlResultado" Runat="server">
				<P class="subtitulo">Resultado de la Búsqueda</P>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE width="100%" class="grid_head">
								<TR>
									<TD >
										<asp:label id="lblTituloGrid" runat="server" Font-Bold="True" Font-Size="X-Small">Poderes</asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="dgResult" runat="server" CssClass="tbl" Width="100%" AutoGenerateColumns="False"
								DataKeyField="ID" HorizontalAlign="Center">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="Poder ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="Denominacion" HeaderText="Poderdante (s)">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Inscripcion" HeaderText="Inscripcion"></asp:BoundColumn>
									<asp:BoundColumn DataField="Concepto" HeaderText="Concepto">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ActaNro" HeaderText="Nro.Acta">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ActaAnio" HeaderText="A&#241;o Acta">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AltaFecha" HeaderText="Fec.Alta" DataFormatString="{0:d}">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Domicilio" HeaderText="Domicilio">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PoderStr" HeaderText="Poder"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
		<DIV></DIV>
	</body>
</HTML>
