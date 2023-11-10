<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.CambioCliente" CodeFile="CambioCliente.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Cambio de Cliente</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgPopUp.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssPopUp.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="100%" height="25"><uc1:header id="Header1" runat="server"></uc1:header></td>
				</tr>
			</table>
			<!-- 
			================================================================================
			TITULO PRINCIPAL DEL TRAMITE VARIO
			================================================================================
			-->
			<!--<table style="MARGIN-TOP: 2pt" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="100%" bgColor="#6699cc" height="25">-->
			<p class="Titulo">Cambio de Cliente REG/REN</p>
			<!--</td>
				</tr>
			</table>-->
			<!--
			================================================================================
			PANEL CLIENTE - SELECCION Y CARGA DE DATOS PARTICULARES
			================================================================================
			--><asp:panel id="pnlCliente" Runat="server" Enabled="True" Visible="True">
				<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="Etiqueta" style="WIDTH: 100%" vAlign="middle">Seleccionar Cliente
						</TD>
					</TR>
				</TABLE>
				<HR width="100%" color="#6699cc" SIZE="1">
				<TABLE cellSpacing="5" cellPadding="0" width="100%" align="center">
					<TR>
						<TD vAlign="middle" width="3%">&nbsp;
						</TD>
						<TD vAlign="middle" width="10%">
							<P class="EtiqSubTitulo">Cliente Nuevo</P>
						</TD>
						<TD vAlign="middle" width="1%">
							<P class="EtiqSubTitulo">:
							</P>
						</TD>
						<TD style="WIDTH: 56%" vAlign="middle">
							<ecctrl:eccombo id="eccClienteNuevo" runat="server" ShowLabel="False"></ecctrl:eccombo></TD>
                        <TD vAlign="middle" width="30%">
                            <TABLE width="100%">
                                <TR>
                                    <TD vAlign="middle" width="60%">
							            <P class="EtiqSubTitulo"><asp:Label id="Label1" runat="server">Eliminar instruciones de Vigilancia</asp:Label></P>
                                    </TD>
                                    <TD vAlign="middle" width="40%">
                                        <asp:CheckBox id="chkEliminarInstrVig" runat="server" Width="96px" Checked="True"></asp:CheckBox>
                                    </TD>
                                </TR>
                            </TABLE>
					</TR>
					<TR>
						<TD vAlign="middle" width="3%">&nbsp;
						</TD>
						<TD vAlign="middle" width="10%">
							<P class="EtiqSubTitulo">Corresp. Nº
							</P>
						</TD>
						<TD vAlign="middle" width="1%">
							<P class="EtiqSubTitulo">:
							</P>
						</TD>
						<TD style="WIDTH: 56%" vAlign="middle">
							<asp:textbox id="txtCorrespNro" runat="server" Width="55px"></asp:textbox>&nbsp;/&nbsp;
							<asp:textbox id="txtCorrespAnio" runat="server" Width="55px"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp; 
							&nbsp;
							<asp:button id="bVerificar" runat="server" Width="152px" Font-Bold="True" Text="Verificar Correspondencia"
								CssClass="Button" onclick="bVerificar_Click"></asp:button></TD>
						<TD vAlign="middle" width="30%">
							<P class="EtiqSubTitulo">
								<asp:Label id="lCorrID" runat="server"></asp:Label></P>
						</TD>
					</TR>
					<TR>
						<TD vAlign="middle" width="3%"></TD>
						<TD vAlign="middle" width="10%"></TD>
						<TD vAlign="middle" width="1%"></TD>
						<TD style="WIDTH: 56%" vAlign="middle">
							<asp:Label id="lblCorrespondencia" runat="server" Width="368px">[lblCorrespondencia]</asp:Label></TD>
						<TD vAlign="middle" width="30%"></TD>
					</TR>
					<TR>
						<TD vAlign="middle" width="3%">&nbsp;
						</TD>
						<TD vAlign="top" width="10%">
							<P class="EtiqSubTitulo">Observaciones
							</P>
						</TD>
						<TD vAlign="top" width="1%">
							<P class="EtiqSubTitulo">:
							</P>
						</TD>
						<TD style="WIDTH: 86%" vAlign="top" colSpan="7">
							<asp:textbox id="txtObsClientes" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:textbox></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!--
			================================================================================ 
			PANEL ASIGNAR MARCAS - MUESTRA LAS MARCAS EN UNA GRILLA 
			================================================================================
			--><asp:panel id="pnlAsignarMarcas" Runat="server" Enabled="True" Visible="True">
				<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="Etiqueta" style="WIDTH: 100%" vAlign="middle">Asignar Marcas</TD>
					</TR>
				</TABLE>
				<HR style="WIDTH: 100.32%; HEIGHT: 1px" width="100.32%" color="#6699cc" SIZE="1">
				<asp:Panel id="pnlBusquedaMarcas" Visible="True" Enabled="True" Runat="server">
					<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
						<TR>
							<TD vAlign="middle" width="3%">&nbsp;
							</TD>
							<TD vAlign="middle" width="10%">
								<P class="EtiqSubTitulo">Cliente Ant.</P>
							</TD>
							<TD vAlign="middle" width="1%">
								<P class="EtiqSubTitulo">:</P>
							</TD>
							<TD style="WIDTH: 86%" vAlign="middle">
								<ecctrl:eccombo id="eccCliente" runat="server" ShowLabel="False"></ecctrl:eccombo></TD>
						</TR>
						<TR>
							<TD vAlign="middle" width="3%">&nbsp;
							</TD>
							<TD vAlign="middle" width="10%">
								<P class="EtiqSubTitulo">Propietario</P>
							</TD>
							<TD vAlign="middle" width="1%">
								<P class="EtiqSubTitulo">:</P>
							</TD>
							<TD style="WIDTH: 86%" vAlign="middle">
								<ecctrl:eccombo id="eccPropietario" runat="server" ShowLabel="False"></ecctrl:eccombo></TD>
						</TR>
						<TR>
							<TD vAlign="middle" width="3%">&nbsp;
							</TD>
							<TD vAlign="middle" width="10%">
								<P class="EtiqSubTitulo">Registros</P>
							</TD>
							<TD vAlign="middle" width="1%">
								<P class="EtiqSubTitulo">:</P>
							</TD>
							<TD style="WIDTH: 86%" vAlign="middle">
								<asp:textbox id="txtRegistros" runat="server" Width="70%"></asp:textbox></TD>
						</TR>
						<TR>
							<TD vAlign="middle" width="3%">&nbsp;
							</TD>
							<TD vAlign="middle" width="10%">
								<P class="EtiqSubTitulo">Actas</P>
							</TD>
							<TD vAlign="middle" width="1%">
								<P class="EtiqSubTitulo">:</P>
							</TD>
							<TD style="WIDTH: 86%" vAlign="middle">
								<asp:textbox id="txtActas" runat="server" Width="70%"></asp:textbox>
								<asp:button id="btnAsignarMarcas" runat="server" Width="114px" Font-Bold="True" Text="Elegir Marcas"
									CssClass="Button" onclick="btnAsignarMarcas_Click"></asp:button></TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<asp:Panel id="pnlMostrarMarcas" Visible="False" Enabled="True" Runat="server">
					<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
						<TR>
							<TD vAlign="middle" width="3%"></TD>
							<TD vAlign="middle" width="97%">
								<asp:DataGrid id="dgMarcasAsignadas" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False">
									<ItemStyle CssClass="cell"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
									<Columns>
										<asp:TemplateColumn HeaderText="Sel.">
											<ItemTemplate>
												<asp:CheckBox id="chkSel" runat="server" Checked="True" />
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="Denominacion" HeaderText="Marca"></asp:BoundColumn>
										<asp:BoundColumn DataField="DescripBreve" HeaderText="Clase"></asp:BoundColumn>
										<asp:BoundColumn DataField="Registro" HeaderText="Registro"></asp:BoundColumn>
										<asp:BoundColumn DataField="Acta" HeaderText="Acta"></asp:BoundColumn>
										<asp:BoundColumn DataField="Propietario" HeaderText="Propietario"></asp:BoundColumn>
										<asp:BoundColumn DataField="Cliente" HeaderText="Cliente"></asp:BoundColumn>
									</Columns>
								</asp:DataGrid></TD>
						</TR>
						<TR>
							<TD vAlign="middle" width="3%"></TD>
							<TD vAlign="middle" align="right" width="97%">
								<asp:button id="btnVolverMarcas" runat="server" Width="114px" Font-Bold="True" Text="Reelegir Marcas"
									CssClass="Button" onclick="btnVolverMarcas_Click"></asp:button>&nbsp; &nbsp;&nbsp;
								<asp:button id="bConfirmar" runat="server" Width="114px" Font-Bold="True" Text="Confirmar" CssClass="Button" onclick="bConfirmar_Click"></asp:button></TD>
						</TR>
					</TABLE>
				</asp:Panel>
			</asp:panel>
			<!-- 
			================================================================================
			PANEL CONFIRMACION
			================================================================================
			--><asp:panel id="pnlConfirmacion" Runat="server" Enabled="True" Visible="False">
				<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="Etiqueta" style="WIDTH: 100%" vAlign="middle">Confirmar el Cambio de 
							Cliente de las siguentes Marcas:</TD>
					</TR>
				</TABLE>
				<HR style="WIDTH: 100%; HEIGHT: 1px" width="100%" color="#6699cc" SIZE="1">
				<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="middle" width="3%"></TD>
						<TD vAlign="middle" width="97%">
							<asp:DataGrid id="dgConfirmacion" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="Denominacion" HeaderText="Marca"></asp:BoundColumn>
									<asp:BoundColumn DataField="DescripBreve" HeaderText="Clase"></asp:BoundColumn>
									<asp:BoundColumn DataField="Registro" HeaderText="Registro"></asp:BoundColumn>
									<asp:BoundColumn DataField="Acta" HeaderText="Acta"></asp:BoundColumn>
									<asp:BoundColumn DataField="Propietario" HeaderText="Propietario"></asp:BoundColumn>
									<asp:BoundColumn DataField="Cliente" HeaderText="Cliente"></asp:BoundColumn>
								</Columns>
							</asp:DataGrid></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!-- 
			================================================================================
			PANEL ASIGNAR DOCUMENTOS - INGRESA DOCUMENTOS Y DATOS CONCERNIENTES AL TV
			================================================================================
			--><asp:panel id="pnlGrabar" Runat="server" Enabled="True" Visible="False">
				<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="middle" align="right" width="100%">
							<asp:button id="btnSalir" runat="server" Width="114px" Font-Bold="True" Text="Cancelar" CssClass="Button" onclick="btnSalir_Click"></asp:button>&nbsp;&nbsp;&nbsp;
							<asp:button id="btnGrabar" runat="server" Width="114px" Font-Bold="True" Text="Grabar" CssClass="Button" onclick="btnGrabar_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!--
			================================================================================
			FIN DE PANELES
			================================================================================
			--></form>
	</BODY>
</HTML>
