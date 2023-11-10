<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.OrdenTrabajoDetalle" CodeFile="OrdenTrabajoDetalle.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeaderOrdenTrabajo" Src="../Tools/Controls/HeaderOrdenTrabajo.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>OrdenTrabajoDetalle</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
		<!--
			function redimensionar(imagen){
				if (imagen.width>imagen.height){
					imagen.width=200;
				}
				else {
					imagen.height=200;
				}
				return true;
			}
		-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P><uc1:header id="Header1" runat="server"></uc1:header></P>
			<P><uc1:headerordentrabajo id="HeaderOrdenTrabajo1" runat="server"></uc1:headerordentrabajo><STRONG><FONT face="Verdana" size="4"></FONT></STRONG></P>
			<P>
				<TABLE id="Table1" class="tbl_recuadro" cellSpacing="0" cellPadding="2" width="98%" border="0">
					<TR>
						<TH class="cell_vert">
							Nro. Hoja de Inicio</TH>
						<TD class="cell_vert"></TD>
						<TD class="cell_vert"><asp:label id="lblNroHojaInicio" runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Cliente</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lblCliente" runat="server" Width="328px"></asp:label>&nbsp;
						</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							&nbsp;</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lblCiudadPais" runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Correo</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert">
							<asp:TextBox id="txtCorreo" runat="server" Width="532px" ReadOnly="True" TextMode="MultiLine"
								Wrap="False" Height="151px" Columns="10" MaxLength="10"></asp:TextBox></TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							RUC</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lbRuc" runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Atención</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lblAtencion" Runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Facturable</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lblFacturable" Runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							<asp:label id="lbltitSustituida" Runat="server">Sustituida</asp:label></TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert">
							<asp:label id="lblSustituida" Runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							<asp:label id="lbltitAgenteSustit" Runat="server" Visible="False">Agente Local</asp:label></TH>
						<TD class="cell_vert">
							<asp:label id="lblAgenteEspacio" Width="2px" Height="2px" Runat="server" Visible="False"></asp:label></TD>
						<TD class="cell_vert">
							<asp:label id="lblAgenteSustit" Runat="server" Visible="False"></asp:label></TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Observación</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lblObservacion" runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Instr. Poder</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lblInstruccionPoder" runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Instr. Contabilidad</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lblInstruccionContabilidad" runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Correspondencia N°</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lblRefNro" runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Ref. Cliente</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lblRefCliente" runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Fecha Alta</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lblFechaAlta" runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Funcionario</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lblFuncionario" runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Por Derecho Propio</TH>
						<TD class="cell_vert">&nbsp;</TD>
						<TD class="cell_vert"><asp:label id="lblDerechoPropio" runat="server"></asp:label>&nbsp;</TD>
					</TR>
					<TR>
						<TD colSpan="3">&nbsp;</TD>
					</TR>
				</TABLE>
				<br>
				<asp:panel id="pnlPropietario" Runat="server" Visible="False">
					<TABLE id="tblPropietario" style="MARGIN-LEFT: 5px" cellSpacing="0" cellPadding="0" width="98%"
						border="0">
						<TR>
							<TD width="100%">
								<TABLE class="grid_head">
									<TR>
										<TD>Propietario</TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD width="100%">
								<asp:DataGrid id="dgPropietarios" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="False"
									CssClass="tbl">
									<ItemStyle HorizontalAlign="center" CssClass="cell"></ItemStyle>
									<HeaderStyle CssClass="cell_header"></HeaderStyle>
									<FooterStyle CssClass="EstiloFooter"></FooterStyle>
									<Columns>
										<asp:BoundColumn HeaderStyle-HorizontalAlign="Center" DataField="ID" ReadOnly="True" HeaderText="ID Poder"
											ItemStyle-Width="8%"></asp:BoundColumn>
										<asp:BoundColumn HeaderStyle-HorizontalAlign="Center" DataField="Denominacion" ReadOnly="True" HeaderText="Denominacion"
											ItemStyle-Width="35%"></asp:BoundColumn>
										<asp:BoundColumn HeaderStyle-HorizontalAlign="Center" DataField="Domicilio" ReadOnly="True" HeaderText="Domicilio"
											ItemStyle-Width="37%"></asp:BoundColumn>
										<asp:BoundColumn HeaderStyle-HorizontalAlign="Center" DataField="Inscripcion" ReadOnly="True" HeaderText="Inscripción Nro."
											ItemStyle-Width="10%"></asp:BoundColumn>
										<asp:BoundColumn HeaderStyle-HorizontalAlign="Center" DataField="ActaNro" ReadOnly="True" HeaderText="Acta Nro."
											ItemStyle-Width="5%"></asp:BoundColumn>
										<asp:BoundColumn HeaderStyle-HorizontalAlign="Center" DataField="ActaAnio" ReadOnly="True" HeaderText="Acta Año"
											ItemStyle-Width="5%"></asp:BoundColumn>
									</Columns>
								</asp:DataGrid></TD>
						</TR>
						<TR>
							<TD width="100%">&nbsp;</TD>
						</TR>
					</TABLE>
				</asp:panel><asp:panel id="pnlPoderObs" Runat="server" Visible="False">
					<TABLE style="MARGIN-LEFT: 5px" cellSpacing="0" cellPadding="0" width="98%" border="0">
						<TR>
							<TD width="100%">
								<TABLE class="grid_head">
									<TR>
										<TD>Observaciones del Poder</TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD width="100%">
								<asp:DataGrid id="dgPoderObs" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="False"
									CssClass="tbl">
									<ItemStyle HorizontalAlign="center" CssClass="cell"></ItemStyle>
									<HeaderStyle CssClass="cell_header"></HeaderStyle>
									<FooterStyle CssClass="EstiloFooter"></FooterStyle>
									<Columns>
										<asp:BoundColumn HeaderStyle-HorizontalAlign="Center" DataField="ID" ReadOnly="True" HeaderText="ID Poder"
											ItemStyle-Width="8%"></asp:BoundColumn>
										<asp:BoundColumn HeaderStyle-HorizontalAlign="Center" DataField="Denominacion" ReadOnly="True" HeaderText="Denominacion"
											ItemStyle-Width="30%"></asp:BoundColumn>
										<asp:BoundColumn HeaderStyle-HorizontalAlign="Center" DataField="Concepto" ReadOnly="True" HeaderText="Concepto"
											ItemStyle-Width="30%"></asp:BoundColumn>
										<asp:BoundColumn HeaderStyle-HorizontalAlign="Center" DataField="Obs" ReadOnly="True" HeaderText="Observaciones"
											ItemStyle-Width="32%"></asp:BoundColumn>
									</Columns>
								</asp:DataGrid></TD>
						</TR>
					</TABLE>
				</asp:panel><asp:panel id="panel_DList_Dgrid" Runat="server">
					<TABLE style="MARGIN-LEFT: 5px" cellSpacing="0" cellPadding="0" width="98%" border="0">
						<TR>
							<TD width="100%">&nbsp;
							</TD>
						</TR>
						<TR>
							<TD width="100%">
								<TABLE class="grid_head">
									<TR>
										<TD>Detalles de las Marcas y sus clases</TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD width="100%">
								<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
									<asp:datalist id="dtlMarcaClase" runat="server" Width="100%" HorizontalAlign="Center" CssClass="tbl"
										DataKeyField="ExpedienteID" CellPadding="4">
										<FooterTemplate>
											<tr class="EstiloFooter">
												<td colspan="4">
												</td>
											</tr>
										</FooterTemplate>
										<ItemTemplate>
											<tr>
												<TD width="150" class="Etiqueta2">
													Marca:
												</TD>
												<td class="cell_vert">
													<asp:label id="lblMarca" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Denominacion") %>'>
													</asp:label>
												</td>
											</tr>
											<tr>
												<TD class="Etiqueta2">
													Clase:
												</TD>
												<td class="cell_vert">
													<asp:label id="lblClase" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Clase") %>'>
													</asp:label>
												</td>
											</tr>
											<tr>
												<TD class="Etiqueta2">
													Denominación Clave:
												</TD>
												<td class="cell_vert">
													<asp:label id="lblDenomClave" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "DenominacionClave") %>'>
													</asp:label>
												</td>
											</tr>
											<tr>
												<TD class="Etiqueta2">
													Tipo:
												</TD>
												<td class="cell_vert">
													<asp:label id="lblTipo" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "MarcaTipo") %>'>
													</asp:label>
												</td>
											</tr>
											<asp:Panel ID="pnlRegistro" Visible="False" Runat="server">
												<tr>
													<TD class="Etiqueta2">
														Prioridad Nro.:
													</TD>
													<td class="cell_vert">
														<asp:label id="lblPrioridad" runat="server"></asp:label>
													</td>
												</tr>
												<tr>
													<TD class="Etiqueta2">
														Prioridad Fecha:
													</TD>
													<td class="cell_vert">
														<asp:label id="lblPrioridadFecha" runat="server"></asp:label>
													</td>
												</tr>
												<tr>
													<TD>
														<P class="Etiqueta2">Prioridad Pais:</P>
													</TD>
													<td class="cell_vert">
														<asp:label id="lblPrioridadPais" runat="server"></asp:label>
													</td>
												</tr>
											</asp:Panel>
											<tr>
												<TD valign="top" class="Etiqueta2">
													Descripción de la Clase
													<asp:label id="lblLimitada" Visible="False" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Limitada") %>'>
													</asp:label>
													<asp:Label ID="lblClaseLim" Visible="False" Runat="server"></asp:Label>:
												</TD>
												<td class="cell_vert">
													<asp:label id="lblClaseDescripEsp" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "ClaseDescripEsp") %>'>
													</asp:label>
												</td>
											</tr>
											<tr>
												<TD valign="top" class="Etiqueta2">
													Referencia de la Marca:
												</TD>
												<td class="cell_vert">
													<asp:label id="lblReferenciaMarca" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Label") %>'>
													</asp:label>
												</td>
											</tr>
											<tr>
												<TD class="Etiqueta2" valign="top">
													Etiqueta:
												</TD>
												<td class="cell_vert">
													<asp:label id="lblLogo" runat="server"></asp:label>
												</td>
											</tr>
											<tr>
												<TD class="Etiqueta2" valign="top">
													<asp:label id="lblIdExpediente" Visible="False" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "ExpedienteId") %>'>
													</asp:label>
													<asp:label id="lblGenHD" runat="server"></asp:label>
												</TD>
												<TD class="cell_vert" valign="top">
													&nbsp;
												</TD>
											</tr>
										</ItemTemplate>
										<SeparatorTemplate>
											<tr>
												<td colspan="2">
													<hr width="100%">
												</td>
											</tr>
										</SeparatorTemplate>
									</asp:datalist></TABLE>
							</TD>
						</TR>
					</TABLE>
				</asp:panel></P>
			<P>
				<TABLE class="Table3Ent" cellSpacing="0" cellPadding="0" align="center">
					<TR>
						<TD align="right" width="20%">
							<p></p>
						</TD>
						<TD align="center" width="20%">
							<A onclick="javascript:window.print();" href="javascript:;">Imprimir</A>
						</TD>
						<TD align="center" width="20%">
							<asp:button id="btnSalir" Runat="server" CssClass="Button" Text="Salir" Font-Bold="True" onclick="btnSalir_Click"></asp:button>
						</TD>
						<TD align="center" width="20%">
							<asp:button id="btnEliminar" Runat="server" CssClass="Button" Text="Eliminar HI" Font-Bold="True" onclick="btnEliminar_Click"></asp:button>
						</TD>
						<TD align="right" width="20%">
							<p></p>
						</TD>
					</TR>
				</TABLE>
			</P>
		</form>
	</body>
</HTML>
