<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.PropietarioDatos" CodeFile="PropietarioDatos.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PropietarioDatos</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header2" runat="server"></uc1:header>
			<P class="titulo">Datos del Propietario</P>
			<TABLE id="tblPropietario" class="infoMacro">
				<TR>
					<TD style="WIDTH: 92px; HEIGHT: 13px" align="right"><FONT size="1"><STRONG>ID Propietario</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 13px"></TD>
					<TD style="HEIGHT: 13px"><FONT size="1"><asp:label id="lblIDPropietario" runat="server" Width="320px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 92px; HEIGHT: 7px" align="right"><FONT size="1"><STRONG>Nombre</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 7px"></TD>
					<TD style="HEIGHT: 7px"><FONT size="1"><asp:label id="lblNombre" runat="server" Width="320px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 92px; HEIGHT: 13px" align="right"><FONT size="1"><STRONG>Dirección</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 13px"></TD>
					<TD style="HEIGHT: 13px"><FONT size="1"><asp:label id="lblDireccion" runat="server" Width="320px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 92px; HEIGHT: 6px" align="right"><FONT size="1"><STRONG>Pais</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 6px"></TD>
					<TD style="HEIGHT: 6px"><FONT size="1"><asp:label id="lblPais" runat="server" Width="320px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 92px; HEIGHT: 11px" align="right"><FONT size="1"><STRONG>Ciudad</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 11px"></TD>
					<TD style="HEIGHT: 11px"><FONT size="1"><asp:label id="lblCiudad" runat="server" Width="320px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 92px; HEIGHT: 2px" align="right"><FONT size="1"><STRONG>Idioma</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 2px"></TD>
					<TD style="HEIGHT: 2px"><FONT size="1"><asp:label id="lblIdioma" runat="server" Width="320px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 92px; HEIGHT: 2px" align="right"><FONT size="1"><STRONG>Observación</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 2px"></TD>
					<TD style="HEIGHT: 2px"><FONT size="1"><asp:label id="lblObs" runat="server" Width="320px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 92px; HEIGHT: 10px" align="right"><FONT size="1"><STRONG>Personeria</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 10px"></TD>
					<TD style="HEIGHT: 10px"><FONT size="1"><asp:label id="lblPersoneria" runat="server" Width="320px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 92px; HEIGHT: 7px" align="right"><FONT size="1"><STRONG>RUC</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 7px"></TD>
					<TD style="HEIGHT: 7px"><FONT size="1"><asp:label id="lblRuc" runat="server" Width="320px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 92px; HEIGHT: 11px" align="right"><FONT size="1"><STRONG>Documento</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 11px"></TD>
					<TD style="HEIGHT: 11px"><FONT size="1"><asp:label id="lblDocumento" runat="server" Width="320px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 92px; HEIGHT: 13px" align="right"><FONT size="1"><STRONG>Grupo 
								Empresarial</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 13px"></TD>
					<TD style="HEIGHT: 13px"><FONT size="1"><asp:label id="lblGrupoEmp" runat="server" Width="320px"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 92px; HEIGHT: 17px" align="right"></TD>
					<TD style="WIDTH: 6px; HEIGHT: 17px"></TD>
					<TD style="HEIGHT: 17px"></TD>
				</TR>
			</TABLE>
			<br>
			<asp:panel id="pnlVias" runat="server" Width="70%" Visible="False">
				<TABLE id="tblPropietarioXVia" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD>
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="Label5" runat="server">Vías de comunicación</asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:datagrid id="dgPropietarioXVia" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False"
								HorizontalAlign="Left">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="DescripcionVia" ReadOnly="True" HeaderText="Via">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ValorVia" ReadOnly="True" HeaderText="Valor">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
				<BR>
			</asp:panel>
			<asp:panel id="pnlPoderes" runat="server" Width="70%" Visible="False">
				<TABLE id="tblPoderes" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="Label1" runat="server">Poderes del Propietario</asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 609px">
							<asp:datagrid id="dgPoderes" runat="server" Width="100%" Visible="True" CssClass="tbl" AutoGenerateColumns="False"
								HorizontalAlign="Left">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="id" ReadOnly="True" HeaderText="ID">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="actanro" ReadOnly="True" HeaderText="Acta">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="inscripcion" HeaderText="Inscrip.">
										<ItemStyle Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="denominacion" HeaderText="Denominacion">
										<ItemStyle Width="30%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="domicilio" HeaderText="Domicilio">
										<ItemStyle Width="30%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
				<BR>
			</asp:panel>
			<asp:panel id="pnlInstrucciones" runat="server" width="90%">
				<TABLE id="tblInstrucciones" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD width="70%">
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="Label6" runat="server">Instrucciones de Vigilancia</asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:datagrid id="dgInstrucXVigilancia" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False"
								HorizontalAlign="Left">
								<FooterStyle CssClass="EstiloFooter"></FooterStyle>
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="Descrip" ReadOnly="True" HeaderText="Instrucci&#243;n">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="18%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="clientenombre" ReadOnly="True" HeaderText="Nombre Cliente">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="18%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="fecalta" ReadOnly="True" HeaderText="Fecha Alta">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="15%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="funcregnombre" HeaderText="Registrado por">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="funcrecnombre" ReadOnly="True" HeaderText="Recibido por">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="correspondencia" ReadOnly="True" HeaderText="Corresp.">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="obs" ReadOnly="True" HeaderText="Observaciones">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="19%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
				<BR>
			</asp:panel>
			<br>
		</form>
	</body>
</HTML>
