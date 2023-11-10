<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.ClienteDatos" CodeFile="ClienteDatos.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consulta de Clientes</title>
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
			<P><uc1:header id="Header1" runat="server"></uc1:header></P>
			<P><asp:label id="Label1" runat="server" CssClass="titulo" Height="37px" Width="270px">Datos del Cliente</asp:label></P>
			<TABLE class="infoMacro" id="Table1">
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 15px" align="right"><FONT size="1"><STRONG>ID Cliente:</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 15px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 15px"><FONT size="1"><asp:label id="lblIDCliente" runat="server"></asp:label></FONT></TD>
					<TD style="WIDTH: 119px; HEIGHT: 15px"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 15px"></TD>
					<TD style="HEIGHT: 15px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 13px" align="right"><FONT size="1"><STRONG>Nombre:</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 13px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 13px"><FONT size="1"><asp:label id="lblNombre" runat="server" Width="320px"></asp:label></FONT></TD>
					<TD style="WIDTH: 80px; HEIGHT: 13px" align="left"><FONT size="1"><STRONG>TraducciónAutomática:</STRONG></FONT></TD>
					<TD style="WIDTH: 7px; HEIGHT: 13px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 20px"><FONT size="1"><asp:label id="lblTraduccion" runat="server"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 13px" align="right"><FONT size="1"><STRONG>Pais:</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 13px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 13px"><FONT size="1"><asp:label id="lblPais" runat="server" Width="304px"></asp:label></FONT></TD>
					<TD class="etiqueta2"><FONT size="1"><STRONG>Activo:</STRONG></FONT></TD>
					<TD style="WIDTH: 7px; HEIGHT: 13px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 13px"><FONT size="1"><asp:label id="lblActivo" runat="server"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 11px" align="right"><FONT size="1"><STRONG>Ciudad:</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 11px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 11px"><FONT size="1"><asp:label id="lblCiudad" runat="server" Width="304px"></asp:label></FONT></TD>
					<TD class="etiqueta2"><FONT size="1"><STRONG>Es no Ubicable:</STRONG></FONT></TD>
					<TD style="WIDTH: 7px; HEIGHT: 11px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 23px"><FONT size="1"><asp:label id="lblNoUbicable" runat="server"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 7px" align="right"><FONT size="1"><STRONG>DDI Pais 
								&nbsp;Ciudad:</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 7px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 11px"><FONT size="1"><asp:label id="lblDDI" runat="server" Width="304px"></asp:label></FONT></TD>
					<TD class="etiqueta2"><FONT size="1"><STRONG>Cliente Multiple:</STRONG></FONT></TD>
					<TD style="WIDTH: 7px; HEIGHT: 7px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 16px"><asp:label id="lblMultiple" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 7px" align="right"><FONT size="1"><STRONG>Idioma:</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 7px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 7px"><FONT size="1"><asp:label id="lblIdioma" runat="server" Width="328px"></asp:label></FONT></TD>
					<TD class="etiqueta2"><FONT size="1"><STRONG>Grupo Empresarial:</STRONG></FONT></TD>
					<TD style="WIDTH: 7px; HEIGHT: 7px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 16px"><FONT size="1"><asp:label id="lblGrupoEmp" runat="server"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 9px" align="right"><FONT size="1"><STRONG>Correo:</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 9px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 9px"><FONT size="1"><asp:label id="lblCorreo" runat="server" Height="3px" Width="144px"></asp:label></FONT></TD>
					<TD class="etiqueta2"><FONT size="1"><STRONG>Es distribuidor?:</STRONG></FONT></TD>
					<TD style="WIDTH: 7px; HEIGHT: 9px"></TD>
					<TD style="HEIGHT: 9px">
						<asp:label id="lblDistribuidor" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 11px" align="right"><FONT size="1"><STRONG>Observación:</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 11px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 11px"><FONT size="1"><asp:label id="lblObs" runat="server" BorderColor="White"></asp:label></FONT></TD>
					<TD style="WIDTH: 119px; HEIGHT: 11px"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 11px"></TD>
					<TD style="HEIGHT: 11px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 4px" align="right"><FONT size="1"><STRONG>Personeria:</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 4px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 4px"><FONT size="1"><asp:label id="lblPersoneria" runat="server"></asp:label></FONT></TD>
					<TD style="WIDTH: 119px; HEIGHT: 4px"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 4px"></TD>
					<TD style="HEIGHT: 4px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 17px" align="right"><FONT size="1"><STRONG>RUC:</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 17px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 17px"><FONT size="1"><asp:label id="lblRuc" runat="server"></asp:label></FONT></TD>
					<TD style="WIDTH: 119px; HEIGHT: 17px"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 17px"></TD>
					<TD style="HEIGHT: 17px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 11px" align="right"><FONT size="1"><STRONG>Documento:</STRONG></FONT></TD>
					<TD style="WIDTH: 6px; HEIGHT: 11px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 11px"><FONT size="1"><asp:label id="lblDocumento" runat="server"></asp:label></FONT></TD>
					<TD style="WIDTH: 119px; HEIGHT: 11px"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 11px"></TD>
					<TD style="HEIGHT: 11px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 11px" align="right"></TD>
					<TD style="WIDTH: 6px; HEIGHT: 11px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 11px"></TD>
					<TD style="WIDTH: 119px; HEIGHT: 11px"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 11px"></TD>
					<TD style="HEIGHT: 11px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 80px; HEIGHT: 11px" align="right"></TD>
					<TD style="WIDTH: 6px; HEIGHT: 11px"></TD>
					<TD style="WIDTH: 310px; HEIGHT: 11px"></TD>
					<TD style="WIDTH: 119px; HEIGHT: 11px"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 11px"></TD>
					<TD style="HEIGHT: 11px"></TD>
				</TR>
			</TABLE>
			<br>
			<asp:panel id="pnlClienteXTramite" runat="server" Width="70%">
				<TABLE id="tblClienteXTramite" border="0" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="lblTituloGrid" runat="server">Clientes relacionados por Trámite</asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					</TD></TR>
					<TR>
						<TD>
							<asp:datagrid id="dgClienteXTramite" runat="server" Width="100%" CssClass="tbl" HorizontalAlign="Left"
								AutoGenerateColumns="False">
								<FooterStyle CssClass="EstiloFooter"></FooterStyle>
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_Header"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="id" HeaderText="ID"></asp:BoundColumn>
									<asp:ButtonColumn Text="Button" DataTextField="nombre" HeaderText="Cliente"></asp:ButtonColumn>
									<asp:BoundColumn DataField="descrip" ReadOnly="True" HeaderText="Tramite">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
				<BR>
			</asp:panel>
			<asp:panel id="pnlVias" runat="server" Width="70%">
				<TABLE id="tblClienteXVia" border="0" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="Label3" runat="server">Vías de Comunicación</asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:datagrid id="dgClienteXVia" runat="server" Width="100%" CssClass="tbl" HorizontalAlign="Left"
								AutoGenerateColumns="False">
								<FooterStyle CssClass="EstiloFooter"></FooterStyle>
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header"></HeaderStyle>
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
			<asp:panel id="pnlAtenciones" runat="server" Width="100%">
				<TABLE id="tblAtenciones" border="0" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="Label2" runat="server">Atenciones</asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:datagrid id="dgAtenciones" runat="server" Width="100%" CssClass="tbl" HorizontalAlign="Left"
								AutoGenerateColumns="False">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="Nombre" ReadOnly="True" HeaderText="Nombre">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="19%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Tarjeta" HeaderText="Tarjeta">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="5%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Descrip" ReadOnly="True" HeaderText="Area">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="strVias" HeaderText="V&#237;as">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="22%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Obs" ReadOnly="True" HeaderText="Observaci&#243;n">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="23%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="UsuariosTarjeta" HeaderText="Func. Berke/Congreso">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="21%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
				<BR>
			</asp:panel>
			<asp:panel id="pnlInstrucciones" runat="server" width="90%">
				<TABLE id="tblInstrucciones" border="0" cellSpacing="0" cellPadding="0" width="100%">
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
							<asp:datagrid id="dgInstrucXVigilancia" runat="server" Width="100%" CssClass="tbl" HorizontalAlign="Left"
								AutoGenerateColumns="False">
								<FooterStyle CssClass="EstiloFooter"></FooterStyle>
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="Descrip" ReadOnly="True" HeaderText="Instrucci&#243;n">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="18%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="propnombre" ReadOnly="True" HeaderText="Nombre Prop.">
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
			<asp:panel id="pnlObs" runat="server" Width="70%" Visible="False">
				<TABLE id="tblClienteObs" border="0" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="Label4" runat="server">Observaciones por Area</asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:datagrid id="dgClienteObs" runat="server" Width="100%" CssClass="tbl" HorizontalAlign="Left"
								AutoGenerateColumns="False">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="Descrip" ReadOnly="True" HeaderText="Area">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Obs" ReadOnly="True" HeaderText="Observación">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="80%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
					<TR>
					</TR>
				</TABLE>
				<BR>
			</asp:panel>
			<asp:panel id="pnlContactos" runat="server" Width="70%" Visible="False">
				<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="Label5" runat="server">Contactos Internos</asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:datagrid id="dgClienteXUsuario" runat="server" Width="100%" CssClass="tbl" HorizontalAlign="Left"
								AutoGenerateColumns="False">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="Nombre" ReadOnly="True" HeaderText="Nombre">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Email" ReadOnly="True" HeaderText="EMail">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" Width="80%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
					<TR>
					</TR>
				</TABLE>
				<BR>
			</asp:panel>
			<br>
		</form>
	</body>
</HTML>
