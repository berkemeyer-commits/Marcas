<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Page language="c#" smartnavigation="True" validateRequest="false" Inherits="Berke.Marcas.WebUI.Home.RegAduanaDetalle" CodeFile="RegAduanaDetalle.aspx.cs" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Registro en Aduanas</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="False" name="vs_showGrid">
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
				//return true;
			}
		-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P><uc1:header id="Header1" runat="server"></uc1:header></P>
			<P><FONT face="Verdana" size="4"><STRONG>Hoja de Inicio de Registro en Aduana&nbsp;
						<asp:label id="lRenovacion" runat="server" Font-Size="Medium"></asp:label>&nbsp;
					</STRONG></FONT>
			</P>
			<P><FONT face="Verdana" size="4"><STRONG><STRONG><FONT face="Verdana" size="1">Funcionario:&nbsp;
								<asp:label id="lFuncionario" runat="server" Font-Size="Larger" Font-Names="Verdana"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
								Fecha de Alta:
								<asp:label id="lfecha" runat="server" Font-Size="Larger" Font-Names="Verdana"></asp:label></P>
			</FONT></STRONG></STRONG></FONT>
			<P><asp:panel id="pnlRenovacion" Width="100%" BorderColor="SteelBlue" BorderWidth="1px" BorderStyle="None"
					Runat="server"><FONT face="Verdana" size="4"><STRONG></STRONG></FONT>
					<TABLE class="infoMacro" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<TR>
							<TD style="WIDTH: 20.26%; HEIGHT: 22px" align="right"><FONT face="Verdana" size="1"><STRONG>Detalle 
										de Registros y vencimientos</STRONG></FONT></TD>
							<TD style="WIDTH: 2.55%; HEIGHT: 22px"></TD>
							<TD style="HEIGHT: 22px">
								<asp:label id="lblRegistros" runat="server" Font-Size="Larger" Font-Names="Verdana" Font-Bold="True"></asp:label></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 20.26%; HEIGHT: 22px" align="right"><FONT face="Verdana" size="1"><STRONG>Correspondencia 
										N°</STRONG></FONT></TD>
							<TD style="WIDTH: 2.55%; HEIGHT: 22px"></TD>
							<TD style="HEIGHT: 22px">
								<asp:TextBox id="txtReferenciaNro" runat="server" MaxLength="10" Columns="10" ReadOnly="True"></asp:TextBox>/
								<asp:TextBox id="txtReferenciaAnio" runat="server" MaxLength="5" Columns="5" ReadOnly="True"></asp:TextBox>
								<asp:checkbox id="chkFacturable" runat="server" Font-Bold="True" Enabled="False" Checked="True"
									Text="Facturable"></asp:checkbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 20.26%; HEIGHT: 28px" align="right"><FONT face="Verdana" size="1"><FONT face="Verdana" size="2"><STRONG>Cliente</STRONG></FONT></FONT></TD>
							<TD style="WIDTH: 2.55%; HEIGHT: 28px"></TD>
							<TD style="HEIGHT: 28px">
								<P>
									<asp:Label id="txtCliente" runat="server" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;&nbsp;
									<FONT size="1"><STRONG>Cód.</STRONG></FONT>
									<asp:label id="lCodCliente" runat="server" Font-Names="Verdana"></asp:label><FONT size="1">&nbsp;<FONT size="1"><STRONG>Idioma</STRONG></FONT>
										<asp:label id="lidioma" runat="server" Font-Names="Verdana"></asp:label><FONT size="1">&nbsp;
										</FONT>
								</P>
								</FONT></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 20.26%; HEIGHT: 28px" align="right"><FONT face="Verdana" size="1"><FONT face="Verdana" size="1"><STRONG>Correo</STRONG></FONT></FONT></TD>
							<TD style="WIDTH: 2.55%; HEIGHT: 28px"></TD>
							<TD style="HEIGHT: 28px">
								<P>
									<asp:TextBox id="txtCorreo" runat="server" Width="532px" MaxLength="10" Columns="10" ReadOnly="True"
										Height="151px" Wrap="False" TextMode="MultiLine"></asp:TextBox><FONT size="1"></P>
								</FONT></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 20.26%; HEIGHT: 30px" align="right"><FONT face="Verdana" size="1"><STRONG>Ref. 
										del Cliente</STRONG></FONT></TD>
							<TD style="WIDTH: 2.55%; HEIGHT: 30px"></TD>
							<TD style="HEIGHT: 30px">
								<asp:Label id="lRefCorr" runat="server"></asp:Label></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 20.26%; HEIGHT: 31px">
								<P align="right"><STRONG><FONT face="Verdana" size="1">Atención</FONT></STRONG></P>
							</TD>
							<TD style="WIDTH: 2.55%; HEIGHT: 31px"></TD>
							<TD style="HEIGHT: 31px"><FONT face="Verdana" size="1">
									<asp:Label id="lAtencion" runat="server"></asp:Label></FONT></TD>
						</TR>
						<TR>
							<TD colSpan="3">
								<asp:DataList id="dtlPropietario" runat="server" BorderStyle="None" Width="100%" CssClass="Grilla"
									CellPadding="0">
									<HeaderTemplate>
										<P class="Etiqueta1" align="center">&nbsp;</P>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR class="EstiloItem">
												<TD align="right" width="20.26%">
													<FONT face="Verdana" size="2"><STRONG>Propietario</STRONG></FONT>
												</TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=Label23 runat="server" Font-Size="Medium" Font-Bold="True" text='<%# DataBinder.Eval(Container.DataItem, "Nombre") %>'>
													</asp:label>&nbsp;Cód:
													<asp:label id=Label10 runat="server" Font-Bold="True" text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'>
													</asp:label>
												</TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%">
													<FONT face="Verdana" size="1"><STRONG>Dirección</STRONG></FONT>
												</TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=Label3 runat="server" Font-Size="Small" text='<%# DataBinder.Eval(Container.DataItem, "Direccion") %>'>
													</asp:label>
												</TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<SeparatorTemplate>
										<tr>
											<td colspan="3">
												<hr width="100%">
											</td>
										</tr>
									</SeparatorTemplate>
								</asp:DataList></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 20.26%; HEIGHT: 26px">
								<P align="right"><STRONG><FONT face="Verdana" size="1">Instrucción Poder</FONT></STRONG></P>
							</TD>
							<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
							<TD style="HEIGHT: 26px"><FONT face="Verdana" size="1">
									<asp:Label id="lInstPoder" runat="server"></asp:Label></FONT></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 20.26%; HEIGHT: 26px">
								<P align="right"><STRONG><FONT face="Verdana" size="1">Instrucción Contabilidad</FONT></STRONG></P>
							</TD>
							<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
							<TD style="HEIGHT: 26px"><FONT face="Verdana" size="1">
									<asp:Label id="lInstCont" runat="server"></asp:Label></FONT></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 20.26%; HEIGHT: 26px">
								<P align="right"><STRONG><FONT face="Verdana" size="1">Observación</FONT></STRONG></P>
							</TD>
							<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
							<TD style="HEIGHT: 26px"><FONT face="Verdana" size="1">
									<asp:Label id="lObs" runat="server"></asp:Label></FONT></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 20.26%; HEIGHT: 11px" align="right"></TD>
							<TD style="WIDTH: 2.55%; HEIGHT: 11px"></TD>
							<TD style="HEIGHT: 11px"></TD>
						</TR>
					</TABLE>
					<TABLE id="TblIngOT" cellSpacing="0" cellPadding="0" width="98%" border="0">
					</TABLE>
				</asp:panel></P>
			<P><asp:panel id="pnlMarcasRenovar" Width="100%" BorderColor="SteelBlue" BorderWidth="1" BorderStyle="None"
					Runat="server" Height="128px">
					<TABLE id="Table5" style="WIDTH: 100%; HEIGHT: 103px" cellSpacing="0" cellPadding="0" width="100%"
						border="0">
						<TR>
							<TD style="WIDTH: 100%; HEIGHT: 25px" width="100%">
								<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
									<TR>
										<TD width="1%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
										<TD class="titletable1" width="99%" bgColor="#7bb5e7">
											<asp:Label id="lPoder" runat="server" Font-Bold="True">Observaciones del Poder</asp:Label></TD>
									</TR>
								</TABLE>
								<asp:DataGrid id="dgPoderActual" runat="server" Width="100%" AutoGenerateColumns="False">
									<HeaderStyle CssClass="EstiloHeader" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle CssClass="EstiloItem"></ItemStyle>
									<Columns>
										<asp:BoundColumn HeaderText="ID" DataField="ID" ItemStyle-Width="10%"></asp:BoundColumn>
										<asp:BoundColumn ItemStyle-Font-Bold="True" HeaderText="Nro Inscrip." DataField="Inscripcion" ItemStyle-Width="10%"></asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Nro. Acta" ItemStyle-Width="10%" ItemStyle-Font-Bold="True">
											<ItemTemplate>
												<%# DataBinder.Eval(Container.DataItem, "ActaNro")%>
												/<%# DataBinder.Eval(Container.DataItem, "ActaAnio")%>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn ItemStyle-Font-Bold="True" ItemStyle-Font-Size="12" HeaderText="Propietario" DataField="Denominacion"
											ItemStyle-Width="30%"></asp:BoundColumn>
										<asp:BoundColumn ItemStyle-Font-Bold="True" HeaderText="Domicilio" DataField="Domicilio" ItemStyle-Width="30%"></asp:BoundColumn>
										<asp:BoundColumn HeaderText="Observaci&#243;n" DataField="Obs" ItemStyle-Width="10%"></asp:BoundColumn>
									</Columns>
								</asp:DataGrid>
								<asp:DataList id="dtlPoderActual" runat="server" Width="100%" CssClass="Grilla" HorizontalAlign="Center">
									<ItemTemplate>
										<TABLE id="TablePoder" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>ID</STRONG></FONT></TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=lblIDPoder runat="server" Font-Bold="False" text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'>
													</asp:label></TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Nro. Inscripcion</STRONG></FONT></TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=lblNroInsc runat="server" Font-Bold="False" text='<%# DataBinder.Eval(Container.DataItem, "Inscripcion") %>'>
													</asp:label></TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Nro. Acta</STRONG></FONT></TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=lblNroActa runat="server" Font-Bold="False" text='<%# DataBinder.Eval(Container.DataItem, "ActaNro") %>'>
													</asp:label>/
													<asp:label id=lblActaAnio runat="server" Font-Bold="False" text='<%# DataBinder.Eval(Container.DataItem, "ActaAnio") %>'>
													</asp:label></TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Propietario</STRONG></FONT></TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=lblPropietario runat="server" Font-Size="Medium" Font-Bold="True" text='<%# DataBinder.Eval(Container.DataItem, "Denominacion") %>'>
													</asp:label></TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Domicilio</STRONG></FONT></TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=lblDomicilio runat="server" Font-Bold="False" text='<%# DataBinder.Eval(Container.DataItem, "Domicilio") %>'>
													</asp:label>&nbsp;
												</TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Observacion</STRONG></FONT></TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=lblDescripcion runat="server" Font-Bold="False" text='<%# DataBinder.Eval(Container.DataItem, "Obs") %>'>
													</asp:label></TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%">&nbsp;</TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left"></TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%">&nbsp;</TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left"></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<SeparatorTemplate>
										<tr>
											<td colspan="2">
												<hr width="100%">
											</td>
										</tr>
									</SeparatorTemplate>
								</asp:DataList>
								<P>&nbsp;</P>
								<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
									<TR>
										<TD width="1%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
										<TD class="titletable1" width="99%" bgColor="#7bb5e7">
											<asp:Label id="lTotalMarcas" runat="server" Font-Bold="True">Marcas Registradas en Aduana : </asp:Label></TD>
									</TR>
								</TABLE>
								<asp:DataGrid id="dgMarcasAsignadas" runat="server" Width="100%" Height="53px" AutoGenerateColumns="False">
									<ItemStyle CssClass="EstiloItem"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="EstiloHeader"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="Denominacion" HeaderText="Denominaci&#243;n">
											<ItemStyle Font-Bold="True"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="RegistroNro" HeaderText="Registro">
											<ItemStyle Font-Size="14pt" Font-Bold="True"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="DesEsp" HeaderText="Tipo">
											<ItemStyle Font-Size="14pt" Font-Bold="True"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="ClaseAntDescrip" HeaderText="Clase Anterior"></asp:BoundColumn>
										<asp:BoundColumn DataField="Vencimiento" HeaderText="Fec.Vto.Reg." DataFormatString="{0:d}">
											<ItemStyle Font-Size="14pt" Font-Bold="True"></ItemStyle>
										</asp:BoundColumn>
									</Columns>
								</asp:DataGrid>
								<asp:DataList id="dtlMarcasAsignadas" runat="server" Width="100%" CssClass="Grilla" HorizontalAlign="Center">
									<ItemTemplate>
										<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Denominación</STRONG></FONT>
												</TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=lblMarca runat="server" Font-Size="Medium" Font-Bold="True" text='<%# DataBinder.Eval(Container.DataItem, "Denominacion") %>'>
													</asp:label></TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Registro</STRONG></FONT>
												</TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=Label11 runat="server" Font-Size="Medium" Font-Bold="True" text='<%# DataBinder.Eval(Container.DataItem, "RegistroNro") %>'>
													</asp:label></TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Tipo</STRONG></FONT>
												</TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=Label12 runat="server" Font-Size="Medium" Font-Bold="True" text='<%# DataBinder.Eval(Container.DataItem, "DesEsp") %>'>
													</asp:label>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
													<asp:label id=Label13 runat="server" Visible="False" text='<%# String.Format( @"<A href=""DetalleHojaDescriptiva.aspx?ExpedienteId={0}"">Ver Hoja Descriptiva</A>", DataBinder.Eval(Container.DataItem, "ExpedienteId"))%>'>
													</asp:label></TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Clase</STRONG></FONT>
												</TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=Label2 runat="server" Font-Bold="True" text='<%# DataBinder.Eval(Container.DataItem, "ClaseAntDescrip") %>'>
													</asp:label></TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Referencia</STRONG></FONT>
												</TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=Label14 runat="server" Font-Bold="True" text='<%# DataBinder.Eval(Container.DataItem, "Referencia") %>'>
													</asp:label></TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Descripción</STRONG></FONT>
												</TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:label id=Label4 runat="server" Font-Bold="True" text='<%# DataBinder.Eval(Container.DataItem, "DesEspLim") %>'>
													</asp:label>
													<asp:label id=lblIdExpediente runat="server" Enabled="False" Visible="False" text='<%# DataBinder.Eval(Container.DataItem, "ExpedienteID") %>'>
													</asp:label></TD>
											</TR>
											<TR class="EstiloItem">
												<TD style="HEIGHT: 26px" align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Fec.Vto.Reg.</STRONG></FONT>
												</TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD style="HEIGHT: 26px" align="left">
													<asp:label id=Label5 runat="server" Font-Size="Medium" Font-Bold="True" text='<%# string.Format("{0:d}", DataBinder.Eval(Container.DataItem, "Vencimiento") )%>'>
													</asp:label></TD>
											</TR>
											<TR class="EstiloItem">
												<TD align="right" width="20.26%"><FONT face="Verdana" size="1"><STRONG>Distribuidores</STRONG></FONT>
												</TD>
												<TD style="WIDTH: 2.55%; HEIGHT: 26px"></TD>
												<TD align="left">
													<asp:Label id="lblDistribuidores" runat="server" Font-Italic="True">No existen distribuidores asignados.</asp:Label></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<SeparatorTemplate>
										<tr>
											<td colspan="2">
												<hr width="100%">
											</td>
										</tr>
									</SeparatorTemplate>
								</asp:DataList></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 100%; HEIGHT: 25px" width="100%"></TD>
						</TR>
					</TABLE>
				</asp:panel></P>
			<asp:panel id="pnlBotones" Width="100%" Runat="server">
				<TABLE id="tblbotones" cellSpacing="0" cellPadding="0" width="98%" border="0">
					<TR>
						<TD style="WIDTH: 30%" align="right"><A onclick="javascript:window.print();" href="javascript:;">Imprimir</A></TD>
						<TD align="center">
							<asp:button id="BntGrabar" runat="server" Width="114px" Font-Bold="True" Text="Aceptar" Height="17px"
								CssClass="Button" Visible="True" onclick="BntGrabar_Click"></asp:button></TD>
						<TD align="center">
							<asp:Button id="btnEliminar" runat="server" Font-Bold="True" Text="Eliminar HI" CssClass="Button" onclick="btnEliminar_Click"></asp:Button></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
	</body>
</HTML>
