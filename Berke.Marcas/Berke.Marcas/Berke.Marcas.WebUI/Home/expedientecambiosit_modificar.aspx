<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Page language="c#" CodeFile="ExpedienteCambioSit_Modificar.aspx.cs" smartnavigation="True" AutoEventWireup="false" Inherits="Berke.Marcas.WebUI.Home.ExpedienteCambioSit_Modificar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Cambio de Situación</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form2" method="post" runat="server" onkeypress="form_onEnter('btnBuscar');">
			<P><uc1:header id="Header1" runat="server"></uc1:header></P>
			<P class="titulo">Modificar Cambio de Situación</P>
			<asp:panel id="pnlBuscar" runat="server" Width="98%" CssClass="infoMacro"> <!--- PANEL BUSCAR -->
				<P class="subtitulo">Buscar Expediente</P>
					<TABLE id="tblSitBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<TR>
							<TD style="WIDTH: 168px; HEIGHT: 21px">
								<P class="Etiqueta2">Trámite</P>
							</TD>
							<TD style="WIDTH: 13px; HEIGHT: 21px"></TD>
							<TD style="WIDTH: 539px; HEIGHT: 21px">
								<CUSTOM:DROPDOWN id="ddlTramite" runat="server" Width="232px" AutoPostBack="true"></CUSTOM:DROPDOWN>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
							<TD style="WIDTH: 43px; HEIGHT: 21px">
								<P align="left">
									<asp:button id="btnBuscar" runat="server" Font-Bold="True" CssClass="Button" Text="Buscar" Enabled="True"></asp:button></P>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 168px; HEIGHT: 2px">
								<P class="Etiqueta2">Situación</P>
							</TD>
							<TD style="WIDTH: 13px; HEIGHT: 2px"></TD>
							<TD style="WIDTH: 539px; HEIGHT: 2px">
								<CUSTOM:DROPDOWN id="ddlTramiteSit" runat="server" Width="232px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
							<TD style="WIDTH: 43px; HEIGHT: 2px"></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 168px">
								<P class="Etiqueta2">Denominación</FONT></P>
							</TD>
							<TD style="WIDTH: 13px"></TD>
							<TD style="WIDTH: 539px">
								<asp:textbox id="txtDenominacion" runat="server" Width="232px"></asp:textbox></TD>
							<TD style="WIDTH: 43px"></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 168px; HEIGHT: 20px">
								<P class="Etiqueta2">Numero de</P>
							</TD>
							<TD style="WIDTH: 10px; HEIGHT: 20px"></TD>
							<TD style="WIDTH: 476px; HEIGHT: 20px">
								<CUSTOM:DROPDOWN id="ddNro" runat="server"></CUSTOM:DROPDOWN>&nbsp;&nbsp;
								<asp:TextBox id="txtNroDesde" runat="server" Width="48px"></asp:TextBox>&nbsp;&nbsp;-&nbsp;
								<asp:TextBox id="txtNroHasta" runat="server" Width="48px"></asp:TextBox>&nbsp; 
								/Año
								<asp:TextBox id="txtNroAnio" runat="server" Width="48px"></asp:TextBox></TD>
							<TD style="WIDTH: 233px; HEIGHT: 20px">&nbsp;</TD>
						</TR>
						<TR>
							<TD colSpan="7">
								<asp:label id="lblMensaje" runat="server" Font-Bold="True" Font-Size="X-Small"></asp:label></TD>
						</TR>
					</TABLE>
				</P>
			</asp:panel><asp:panel id="pnlResultado" runat="server"><!--- PANEL RESULTADO -->
				<P><FONT size="3"><STRONG><U>Resultado de la 
								Búsqueda&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							</U></STRONG></FONT>
				</P>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head">
								<TR>
									<TD>Expedientes</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="dgMarcas" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False"
								DataKeyField="ExpedienteID" HorizontalAlign="Center">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header" HorizontalAlign="Center"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter" HorizontalAlign="Left"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="ExpedienteID" HeaderText="ID" Visible="True"></asp:BoundColumn>
									<asp:ButtonColumn ItemStyle-HorizontalAlign="Left" Text="Denominacion" DataTextField="Denominacion"
										HeaderText="Denominacion" CommandName="Select"></asp:ButtonColumn>
									<asp:BoundColumn DataField="Clase" ItemStyle-HorizontalAlign="Right" HeaderText="Clase"></asp:BoundColumn>
									<asp:BoundColumn DataField="Acta" ItemStyle-HorizontalAlign="Right" HeaderText="Acta"></asp:BoundColumn>
									<asp:BoundColumn DataField="Registro" ItemStyle-HorizontalAlign="Right" HeaderText="Registro"></asp:BoundColumn>
									<asp:BoundColumn DataField="TramiteAbrev" HeaderText="Tr&#225;mite"></asp:BoundColumn>
									<asp:BoundColumn DataField="SituacionDecrip" HeaderText="Situacion"></asp:BoundColumn>
									<asp:BoundColumn DataField="TramiteID" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="TramiteSitID" Visible="False"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
				<P></P>
			</asp:panel>
			<!-- PANEL ACTUALIZAR ------><asp:panel id="pnlActualizar" runat="server">
				<P class="subtitulo">Expediente a Actualizar</P>
				<TABLE class="infoMacro" style="WIDTH: 648px; HEIGHT: 40px" cellSpacing="0" cellPadding="0" width="648" border="0">
					<TR>
						<TD style="WIDTH: 76px; HEIGHT: 12px">
							<P class="Etiqueta2">Exped. ID:</P>
						</TD>
						<TD style="WIDTH: 295px; HEIGHT: 12px">
							<asp:label id="lblExpeID" runat="server" Font-Bold="True" CssClass="Texto">lblExpeID</asp:label></TD>
						<TD style="WIDTH: 82px; HEIGHT: 12px">
							<P class="Etiqueta2">Hoja Inicio:</P>
						</TD>
						<TD style="HEIGHT: 12px">
							<asp:label id="lblHI" runat="server" Font-Bold="True" CssClass="Texto">lblHI</asp:label></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 76px">
							<P class="Etiqueta2">Nro.Acta:</P>
						</TD>
						<TD style="WIDTH: 295px">
							<asp:label id="lblActa" runat="server" Font-Bold="True" CssClass="Texto">lblActa</asp:label></TD>
						<TD style="WIDTH: 82px">
							<P class="Etiqueta2">Registro:</P>
						</TD>
						<TD>
							<asp:label id="lblRegistro" runat="server" Font-Bold="True" CssClass="Texto">lblRegistro</asp:label></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 76px; HEIGHT: 12px">
							<P class="Etiqueta2">Marca:</P>
						</TD>
						<TD style="WIDTH: 295px; HEIGHT: 12px">
							<asp:label id="lblMarca" runat="server" Font-Bold="True" CssClass="Texto">lblMarca</asp:label></TD>
						<TD style="WIDTH: 82px; HEIGHT: 12px">
							<P class="Etiqueta2">Clase:</P>
						</TD>
						<TD style="HEIGHT: 12px">
							<asp:label id="lblClase" runat="server" Font-Bold="True" CssClass="Texto">lblClase</asp:label></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 79px">
							<P class="Etiqueta2">&nbsp;&nbsp;</P>
						</TD>
						<TD style="WIDTH: 314px"></TD>
						<TD style="WIDTH: 112px; HEIGHT: 13px">
							<P class="Etiqueta2">Vencim.:</P>
						</TD>
						<TD style="HEIGHT: 13px">
							<asp:label id="lblVencim" runat="server" Font-Bold="True" CssClass="Texto">lblVencim</asp:label></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 76px">
							<P class="Etiqueta2">Tramite:</P>
						</TD>
						<TD style="WIDTH: 295px">
							<asp:label id="lblTramite" runat="server" Font-Bold="True" CssClass="Texto">lblTramite</asp:label></TD>
						<TD style="WIDTH: 82px">
							<P class="Etiqueta2">&nbsp;</P>
						</TD>
						<TD></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 76px; HEIGHT: 11px">
							<P class="Etiqueta2">Situación:</P>
						</TD>
						<TD style="WIDTH: 295px">
							<asp:label id="lblSituacion" runat="server" Font-Bold="True" CssClass="Texto">lblSituacion</asp:label></TD>
						<TD style="WIDTH: 82px">
							<P class="Etiqueta2">&nbsp;</P>
						</TD>
						<TD></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 76px">
							<P class="Etiqueta2">&nbsp;</P>
						</TD>
						<TD style="WIDTH: 295px"></TD>
						<TD style="WIDTH: 82px">
							<P class="Etiqueta2">&nbsp;</P>
						</TD>
						<TD></TD>
					</TR>
					<TR>
						<TD class="Etiqueta2" style="WIDTH: 76px">Sit. Anterior :</TD>
						<TD colSpan="3">
							<asp:TextBox id="txtSitAnterior" runat="server" Width="254px" ReadOnly="True" BackColor="WhiteSmoke"></asp:TextBox>
							<asp:label id="Label2" runat="server" CssClass="Etiqueta2">Fecha</asp:label>
							<asp:TextBox id="txtFechaSitAnterior" runat="server" Width="112px" ReadOnly="True" BackColor="WhiteSmoke"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="Etiqueta2" style="WIDTH: 76px">Sit. Actual :</TD>
						<TD colSpan="3">
							<asp:TextBox id="txtSitActual" runat="server" Width="254px" ReadOnly="True" BackColor="WhiteSmoke"></asp:TextBox>
							<asp:label id="Label1" runat="server" CssClass="Etiqueta2">Fecha</asp:label>
							<asp:TextBox id="txtFechaSitActual" runat="server" Width="112px" ReadOnly="True" BackColor="WhiteSmoke"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
							<%--<asp:button id="btnRevertir" runat="server" Font-Bold="True" CssClass="Button" Text="<- Revertir"
								Enabled="True" OnClick="btnRevertir_Click"></asp:button>--%>
                            <asp:button id="btnRevertir" runat="server" Font-Bold="True" CssClass="Button" Text="<- Revertir"
								Enabled="True"></asp:button>
							<asp:CheckBox id="chkRevertir" runat="server" Text="."></asp:CheckBox></TD>
					</TR>
				</TABLE> <!-- PANEL PLAZO -->
				<asp:panel id="pnlPlazo" runat="server" Height="38px">
					<TABLE style="WIDTH: 640px; HEIGHT: 26px">
						<TR>
							<TD style="WIDTH: 79px">
								<P class="Etiqueta2">Observación:</P>
							</TD>
							<TD colSpan="3">
								<asp:textbox id="txtObservacion" runat="server" Width="481px" TextMode="MultiLine" MaxLength="200"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 148px">
								<P class="Etiqueta2">Vencimiento Sit. Actual&nbsp; :</P>
							</TD>
							<TD noWrap>
								<asp:TextBox id="txtFechaVencim" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
								Fecha de Situacion :
								<asp:TextBox id="txtFechaSituacion" runat="server"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<P></P>
				</asp:panel> <!-- Fin Panel Plazo --> <!-- PANEL AGENTE LOCAL -->
				<asp:panel id="pnlAgenteLocal" runat="server" Height="40px">
					<TABLE style="WIDTH: 500px; HEIGHT: 21px" cellSpacing="0" cellPadding="0" width="648" border="0">
						<TR>
							<TD style="WIDTH: 76px" noWrap>
								<P class="Etiqueta2">Agente Local:</P>
							</TD>
							<TD style="WIDTH: 319px">
								<CUSTOM:DROPDOWN id="ddlAgenteLocal" runat="server" Width="280px" AutoPostBack="false"></CUSTOM:DROPDOWN></TD>
							<TD style="WIDTH: 23px; HEIGHT: 30px">
								<P class="Etiqueta2">Nro.Acta</P>
							</TD>
							<TD style="WIDTH: 76px; HEIGHT: 30px">
								<asp:TextBox id="txtActaNro" runat="server" Width="75px"></asp:TextBox></TD>
							<TD>
								<P class="Etiqueta2">Año</P>
							</TD>
							<TD>
								<asp:TextBox id="txtActaAnio" runat="server" Width="75px"></asp:TextBox></TD>
						</TR>
						</TR></TABLE>
					<P></P>
				</asp:panel> <!-- Fin Panel Agente Local --> <!-- PANEL REGISTRO -->
				<asp:panel id="pnlRegistro" runat="server" Height="38px">
					<TABLE style="WIDTH: 648px; HEIGHT: 21px" cellSpacing="0" cellPadding="0" width="648" border="0">
						<TR>
							<TD style="WIDTH: 76px">
								<P class="Etiqueta2">Nro.Registro:</P>
							</TD>
							<TD>
								<asp:TextBox id="txtRegistroNro" runat="server"></asp:TextBox></TD>
							<TD style="WIDTH: 76px">
								<P class="Etiqueta2">Vencim.Registro:</P>
							</TD>
							<TD noWrap>
								<asp:TextBox id="txtRegVto" runat="server"></asp:TextBox>
								<asp:Button id="btnRecalcVencimReg" runat="server" Width="80px" Text="<-Calc. Vto." Height="18px"></asp:Button></TD>
						</TR>
						</TR></TABLE>
					<P></P>
				</asp:panel> <!-- Fin Panel Registro --> <!-- PANEL PUBLICACION -->
				<asp:panel id="pnlPublicacion" runat="server" Height="38px">
					<TABLE style="WIDTH: 648px; HEIGHT: 21px" cellSpacing="0" cellPadding="0" width="648" border="0">
						<TR>
							<TD style="WIDTH: 76px">
								<P class="Etiqueta2">Diario:</P>
							</TD>
							<TD style="WIDTH: 186px">
								<CUSTOM:DROPDOWN id="ddlDiario" runat="server" Width="153px" AutoPostBack="false"></CUSTOM:DROPDOWN></TD>
							<TD style="WIDTH: 76px">
								<P class="Etiqueta2">Pág.Publicacion:</P>
							</TD>
							<TD>&nbsp;
								<asp:TextBox id="txtPubicPagina" runat="server"></asp:TextBox></TD>
							<TD style="WIDTH: 76px">
								<P class="Etiqueta2">Año:</P>
							</TD>
							<TD>&nbsp;
								<asp:TextBox id="txtPublicAnio" runat="server"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<P></P>
				</asp:panel> <!-- Fin Panel Publicacion -->
				<asp:panel id="pnlArchivada" runat="server" Height="38px">
					<TABLE style="WIDTH: 648px; HEIGHT: 21px" cellSpacing="0" cellPadding="0" width="648" border="0">
						<TR>
							<TD style="WIDTH: 76px">
								<P class="Etiqueta2">Biblio. Nro.:</P>
							</TD>
							<TD>
								<asp:TextBox id="txtBib" runat="server" Width="80px"></asp:TextBox></TD>
							<TD style="WIDTH: 76px">
								<P class="Etiqueta2">Indice :</P>
							</TD>
							<TD>
								<asp:TextBox id="txtExp" runat="server" Width="74px"></asp:TextBox></TD>
						</TR>
					</TABLE>
				</asp:panel> <!-- Botones  -->
				<TABLE style="WIDTH: 648px; HEIGHT: 21px" cellSpacing="0" cellPadding="0" width="648" border="0">
					<TR>
						<TD style="WIDTH: 76px">
							<P class="Etiqueta2"></P>
						</TD>
						<TD style="WIDTH: 76px">
							<P class="Etiqueta2">
								<asp:button id="btnAceptar" runat="server" Font-Bold="True" CssClass="Button" Text="Grabar Modificaciones"
									Enabled="True"></asp:button></P>
						</TD>
						<TD style="WIDTH: 76px">
							<P class="Etiqueta2">
								<asp:button id="btnCancelar" runat="server" Font-Bold="True" CssClass="Button" Text="Cancelar"
									Enabled="True"></asp:button></P>
						</TD>
						<TD style="WIDTH: 76px">
							<P class="Etiqueta2">&nbsp;</P>
						</TD>
					</TR>
				</TABLE>
				<P></P>
			</asp:panel>
			<!-- Fin Panel Modificar-------------></form>
	</body>
</HTML>
