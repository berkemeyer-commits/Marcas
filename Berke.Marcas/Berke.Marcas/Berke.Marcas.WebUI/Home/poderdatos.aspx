<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.PoderDatos" CodeFile="PoderDatos.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consulta de Poderes</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<br>
			<br>
			<span class="titulo">
				<asp:label id="Label1" runat="server" Width="270px" Height="37px">Datos del Poder</asp:label></span>
			<TABLE id="Table1" style="FONT-SIZE: 10pt; Z-INDEX: 102; LEFT: 16px; WIDTH: 744px; FONT-FAMILY: Verdana; HEIGHT: 218px"
				borderColor="#3366cc" cellSpacing="2" cellPadding="2" width="744" border="0">
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 6px" align="right"><FONT size="1"><STRONG>ID Poder</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 6px">:</TD>
					<TD style="WIDTH: 300px; HEIGHT: 6px"><FONT size="1"><asp:label id="lblIDPoder" runat="server"></asp:label></FONT></TD>
					<TD style="WIDTH: 121px; HEIGHT: 6px"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 6px"></TD>
					<TD style="HEIGHT: 6px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 26px" align="right"><FONT size="1"><STRONG>Concepto</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 26px">:</TD>
					<TD style="WIDTH: 300px; HEIGHT: 26px"><FONT size="1"><asp:label id="lblConcepto" runat="server" Width="256px"></asp:label></FONT></TD>
					<TD style="WIDTH: 80px; HEIGHT: 24px" align="right"><FONT size="1"><STRONG>Nuestra</STRONG></FONT></TD>
					<TD style="WIDTH: 7px; HEIGHT: 26px">:</TD>
					<TD style="WIDTH: 297px; HEIGHT: 26px"><FONT size="1"><asp:label id="lblNuestra" runat="server" Width="116px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 11px" align="right"><FONT size="1"><STRONG>Denominación</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 11px">:</TD>
					<TD style="WIDTH: 300px; HEIGHT: 11px"><FONT size="1"><asp:label id="lblDenominacion" runat="server" Width="280px"></asp:label></FONT></TD>
					<TD style="WIDTH: 80px; HEIGHT: 24px" align="right"><FONT size="1"><STRONG>Legaliz. 
								Notarial</STRONG></FONT></TD>
					<TD style="WIDTH: 7px; HEIGHT: 11px">:</TD>
					<TD style="WIDTH: 297px; HEIGHT: 26px"><FONT size="1"><asp:label id="lblLegNot" runat="server" Width="116px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 7px" align="right"><FONT size="1"><STRONG>Domicilio</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 7px">:</TD>
					<TD style="WIDTH: 300px; HEIGHT: 7px"><FONT size="1"><asp:label id="lblDomicilio" runat="server" Width="328px"></asp:label></FONT></TD>
					<TD style="WIDTH: 80px; HEIGHT: 24px" align="right"><FONT size="1"><STRONG>Consular</STRONG></FONT></TD>
					<TD style="WIDTH: 7px; HEIGHT: 7px">:</TD>
					<TD style="WIDTH: 297px; HEIGHT: 26px"><FONT size="1"><asp:label id="lblLegCons" runat="server" Width="92px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 13px" align="right"><FONT size="1"><STRONG>Pais</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 13px">:</TD>
					<TD style="WIDTH: 300px; HEIGHT: 13px"><FONT size="1"><asp:label id="lblPais" runat="server" Width="304px"></asp:label></FONT></TD>
					<TD style="WIDTH: 80px; HEIGHT: 24px" align="right"><FONT size="1"><STRONG>Legaliz.Rel.Ext.</STRONG></FONT></TD>
					<TD style="WIDTH: 7px; HEIGHT: 13px">:</TD>
					<TD style="WIDTH: 297px; HEIGHT: 26px"><FONT size="1"><asp:label id="lblLegRelExt" runat="server" Width="116px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 24px" align="right"><FONT size="1"><STRONG>Acta/Año</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 24px">:</TD>
					<TD style="WIDTH: 300px; HEIGHT: 24px"><FONT size="1"><asp:label id="lblActa" runat="server" Width="304px" Height="19px"></asp:label></FONT></TD>
					<TD style="WIDTH: 80px; HEIGHT: 24px" align="right"><FONT size="1"><STRONG>Original</STRONG></FONT></TD>
					<TD style="WIDTH: 7px; HEIGHT: 24px">:</TD>
					<TD style="WIDTH: 297px; HEIGHT: 26px"><FONT size="1"><asp:label id="lblOriginal" runat="server" Width="114px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 24px" align="right"><FONT size="1"><STRONG>Inscripcion</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 16px">:</TD>
					<TD style="WIDTH: 300px; HEIGHT: 16px"><FONT size="1"><asp:label id="lblInscripcion" runat="server"></asp:label></FONT></TD>
					<TD style="WIDTH: 121px; HEIGHT: 16px"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 16px"></TD>
					<TD style="HEIGHT: 16px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 24px" align="right"><FONT size="1"><STRONG>Nro.Inscripcion/Año</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 16px">:</TD>
					<TD style="WIDTH: 300px; HEIGHT: 16px"><FONT size="1"><asp:label id="lblInscAnho" runat="server"></asp:label></FONT></TD>
					<TD style="WIDTH: 121px; HEIGHT: 16px"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 16px"></TD>
					<TD style="HEIGHT: 16px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 24px" align="right"><FONT size="1"><STRONG>Observación</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 16px">:</TD>
					<TD style="WIDTH: 300px; HEIGHT: 16px"><FONT size="1"><asp:label id="lblObs" runat="server"></asp:label></FONT></TD>
					<TD style="WIDTH: 121px; HEIGHT: 16px"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 16px"></TD>
					<TD style="HEIGHT: 16px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 17px" align="right"></TD>
					<TD style="WIDTH: 6px; HEIGHT: 17px"></TD>
					<TD style="WIDTH: 300px; HEIGHT: 17px"></TD>
					<TD style="WIDTH: 121px; HEIGHT: 17px"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 17px"></TD>
					<TD style="HEIGHT: 17px"></TD>
				</TR>
			</TABLE>
			<asp:panel id="pnlPropietarioXPoder" style="Z-INDEX: 104; LEFT: 16px" runat="server" Width="744px"
				Height="164px">
				<TABLE id="tblPropietarioXPoder" style="WIDTH: 738px" cellSpacing="0" cellPadding="0" width="738"
					border="0">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head" width="100%">
								<TR>
									<TD>
										<asp:label id="lblTituloGrid" runat="server" Font-Size="X-Small" Font-Bold="True">Propietario(s)</asp:label></TD>
								</TR>
							</TABLE> <!--<TABLE id="Table3" style="WIDTH: 738px; HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="738"
								align="center" border="0">
								<TR>
									<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
									<TD class="titletable1" width="98%" bgColor="#7bb5e7">Propietario(s)</TD>
								</TR>
							</TABLE>--></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 564px" width="564">
							<asp:datagrid id="dgPropietarioXPoder" runat="server" Width="736px" CssClass="tbl" AutoGenerateColumns="False"
								HorizontalAlign="Left">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="Nombre" ReadOnly="True" HeaderText="Nombre">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Direccion" ReadOnly="True" HeaderText="Direccion">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FechaAlta" ReadOnly="True" HeaderText="Fecha Alta">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Pais" ReadOnly="True" HeaderText="Pais">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Grupo" ReadOnly="True" HeaderText="Grupo">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel>
		</form>
	</body>
</HTML>
