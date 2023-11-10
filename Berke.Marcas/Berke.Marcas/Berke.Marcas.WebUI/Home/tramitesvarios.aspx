<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.TramitesVarios" CodeFile="TramitesVarios.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="uc1" TagName="FormValidator" Src="../Tools/Controls/FormValidator.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FieldValidator" Src="../Tools/Controls/FieldValidator.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Trámites Varios: Cambio de Nombre</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgPopUp.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssPopUp.css" type="text/css" rel="stylesheet">
		<!-- BEGIN - Necesario para los Validators -->
		<LINK href="../Tools/js/window/css/modal-message.css" type="text/css" rel="stylesheet">
		<script src="../Tools/js/window/js/ajax.js" type="text/javascript"></script>
		<script src="../Tools/js/window/js/modal-message.js" type="text/javascript"></script>
		<script src="../Tools/js/window/js/ajax-dynamic-content.js" type="text/javascript"></script>
		<script src="../Tools/js/validators.js" type="text/javascript"></script>
		<!-- END - Necesario para los Validators -->
	</HEAD>
	<body>
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
			<p class="titulo">Trámites Varios -
				<asp:label id="lblTV" Runat="server"></asp:label>&nbsp;</p>
			<!--
			================================================================================
			PANEL CLIENTE - SELECCION Y CARGA DE DATOS PARTICULARES
			================================================================================
			--><asp:panel id="pnlCliente" Runat="server" Visible="True" Enabled="True" CssClass="infoMacro"
				Width="98%">
				<TABLE class="grid_head">
					<TR>
						<TD>Seleccionar Cliente
						</TD>
					</TR>
				</TABLE>
				<TABLE cellSpacing="5" cellPadding="0" width="100%" align="center">
					<TR>
						<TD vAlign="middle" width="10%">
							<P class="Etiqueta2">Cliente
							</P>
						</TD>
						<TD width="35%">
							<ecctrl:eccombo id="eccCliente" runat="server" width="100%" ShowLabel="False"></ecctrl:eccombo></TD>
						<TD align="right" width="55%">
							<asp:label id="lbAtencion" Runat="server" Width="115px" CssClass="EtiqSubTitulo">Atención:</asp:label>
							<asp:DropDownList id="ddlAtencion" runat="server" Width="250px"></asp:DropDownList>
							<asp:textbox id="tbNuevaAtencion" runat="server" Width="250px" Enabled="False"></asp:textbox>
							<asp:button id="btnNuevaAtencion" runat="server" Width="105px" CssClass="Button" Font-Bold="True"
								Text="Nueva atención"></asp:button></TD>
					</TR>
					<TR>
						<TD colSpan="6">
							<TABLE class="tbl" cellSpacing="0" cellPadding="0" width="98%">
								<TR align="center">
									<TD class="cell_header" style="WIDTH: 70%">
										<P>Direccion Cliente</P>
									</TD>
									<TD class="cell_header" style="WIDTH: 30%">
										<P>Pais Cliente</P>
									</TD>
								</TR>
								<TR>
									<TD class="cell" style="WIDTH: 70%">
										<asp:Label id="lbDireccionCliente" runat="server" Width="100%"></asp:Label></TD>
									<TD class="cell" style="WIDTH: 30%">
										<asp:Label id="lbCiudadCliente" runat="server" Width="100%"></asp:Label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="Etiqueta2">Corresp. Nº
						</TD>
						<TD>
							<asp:textbox id="txtCorrespNro" runat="server" Width="55px"></asp:textbox>&nbsp;/&nbsp;
							<asp:textbox id="txtCorrespAnio" runat="server" Width="55px"></asp:textbox></TD>
						<TD>
							<asp:checkbox id="chkFacturable" Runat="server" Width="136px" Text="Facturable" Checked="True"></asp:checkbox></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label1" Runat="server" Width="100%" CssClass="Etiqueta2">Ref. Corresp</asp:label></TD>
						<TD>
							<asp:textbox id="txtRefCorresp" runat="server"></asp:textbox></TD>
						<TD>
							<asp:checkbox id="cbDerechoPropio" Runat="server" Width="144px" Text="Por derecho propio" Checked="False"></asp:checkbox></TD>
					</TR>
					<TR>
						<TD class="Etiqueta2">Observaciones
						</TD>
						<TD colSpan="2">
							<asp:textbox id="txtObsClientes" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:textbox></TD>
					</TR>
					<TR>
						<TD class="Etiqueta2">Instrucción Poder
						</TD>
						<TD>
							<asp:textbox id="tbInstruccionPoder" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:textbox></TD>
						<TD>
							<TABLE cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD width="10%">
										<asp:label id="Label2" Runat="server" Width="10%" CssClass="Etiqueta2">Instrucción Contabilidad</asp:label></TD>
									<TD width="90%">
										<asp:textbox id="tbInstruccionContabilidad" runat="server" Width="100%" TextMode="MultiLine"
											Rows="3" Columns="10"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="Etiqueta2">Referencia del Cliente
						</TD>
						<TD colSpan="2">
							<asp:textbox id="tbReferenciaCliente" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:textbox></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!--
			================================================================================ 
			PANEL ASIGNAR MARCAS - MUESTRA LAS MARCAS EN UNA GRILLA 
			================================================================================
			--><asp:panel id="pnlAsignarMarcas" Runat="server" Visible="True" Enabled="True" CssClass="infoMacro"
				Width="98%">
				<TABLE class="grid_head" width="100%" border="0">
					<TR>
						<TD>Asignar Marcas
						</TD>
					</TR>
				</TABLE>
				<asp:Panel id="pnlBusquedaMarcas" Runat="server" Enabled="True" Visible="True">
					<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
						<TR>
							<TD vAlign="middle" width="3%">&nbsp;
							</TD>
							<TD vAlign="middle" width="10%">
								<P class="EtiqSubTitulo">Registros
								</P>
							</TD>
							<TD vAlign="middle" width="1%">
								<P class="EtiqSubTitulo">:
								</P>
							</TD>
							<TD style="WIDTH: 86%" vAlign="middle">
								<asp:textbox id="txtRegistros" runat="server" Width="30%"></asp:textbox></TD>
						</TR>
						<TR>
							<TD vAlign="middle" width="3%">&nbsp;
							</TD>
							<TD vAlign="middle" width="10%">
								<P class="EtiqSubTitulo">Actas
								</P>
							</TD>
							<TD vAlign="middle" width="1%">
								<P class="EtiqSubTitulo">:
								</P>
							</TD>
							<TD style="WIDTH: 86%" vAlign="middle">
								<asp:textbox id="txtActas" runat="server" Width="30%"></asp:textbox>
								<asp:button id="btnAsignarMarcas" runat="server" Width="114px" CssClass="Button" Font-Bold="True"
									Text="Elegir Marcas" onclick="btnAsignarMarcas_Click"></asp:button></TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<asp:Panel id="pnlMostrarMarcas" Runat="server" Enabled="True" Visible="False">
					<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
						<TR>
							<TD vAlign="middle" width="3%"></TD>
							<TD vAlign="middle" width="97%">
								<asp:DataGrid id="dgMarcasAsignadas" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False">
									<ItemStyle CssClass="cell"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="Denominacion" HeaderText="Denominaci&#243;n"></asp:BoundColumn>
										<asp:BoundColumn DataField="ClaseAntDescrip" HeaderText="Clase"></asp:BoundColumn>
										<asp:BoundColumn DataField="RegistroNro" HeaderText="Registro"></asp:BoundColumn>
										<asp:BoundColumn DataField="ActaNro" HeaderText="Acta"></asp:BoundColumn>
										<asp:BoundColumn DataField="ActaAnio" HeaderText="Año"></asp:BoundColumn>
										<asp:BoundColumn DataField="ConcesionFecha" HeaderText="Conseción" DataFormatString="{0:d}"></asp:BoundColumn>
										<asp:BoundColumn DataField="Vencimiento" HeaderText="Fec.Vto.Reg." DataFormatString="{0:d}"></asp:BoundColumn>
									</Columns>
								</asp:DataGrid></TD>
						</TR>
						<TR>
							<TD vAlign="middle" width="3%"></TD>
							<TD vAlign="middle" align="right" width="97%">
								<asp:button id="btnVolverMarcas" runat="server" Width="114px" CssClass="Button" Font-Bold="True"
									Text="Reelegir Marcas"></asp:button></TD>
						</TR>
					</TABLE>
				</asp:Panel>
			</asp:panel>
			<!--
			================================================================================
			PANEL LOGICA DE NEGOCIOS - SELECCION DEL PODER
			================================================================================
			--><asp:panel id="pnlLogicaTV" Runat="server" Visible="True" Enabled="True" CssClass="infoMacro"
				Width="98%">
				<asp:Panel id="pnlAnterior" Runat="server" Enabled="True" Visible="False">
					<TABLE class="grid_head" width="100%" border="0">
						<TR>
							<TD>Propietario Anterior
							</TD>
						</TR>
					</TABLE>
					<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
						<TR>
							<TD vAlign="middle" width="3%"></TD>
							<TD align="left" width="97%">
								<asp:DataGrid id="dgPoderAnterior" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False">
									<ItemStyle CssClass="cell"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
									<Columns>
										<asp:BoundColumn HeaderText="Propietario" DataField="Denominacion" ItemStyle-Width="23%" ItemStyle-Font-Bold="True"></asp:BoundColumn>
										<asp:BoundColumn HeaderText="Domicilio" DataField="Domicilio" ItemStyle-Width="23%"></asp:BoundColumn>
										<asp:BoundColumn HeaderText="País" DataField="Obs" ItemStyle-Width="23%"></asp:BoundColumn>
									</Columns>
								</asp:DataGrid></TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<asp:Panel id="pnlActual" Runat="server" Enabled="True" Visible="True">
					<TABLE class="grid_head" width="100%" border="0">
						<TR>
							<TD>Seleccionar Poder o Propietario
							</TD>
						</TR>
					</TABLE>
					<asp:Panel id="pnlAsignarPoderes" Runat="server" Enabled="True" Visible="True">
						<TABLE cellSpacing="5" cellPadding="0" width="100%" align="center">
							<TR>
								<TD vAlign="middle" width="3%">&nbsp;
								</TD>
								<TD vAlign="middle" width="10%">
									<P class="EtiqSubTitulo">Cód. Poder o Propietario
									</P>
								</TD>
								<TD vAlign="middle" width="1%">
									<P class="EtiqSubTitulo">:
									</P>
								</TD>
								<TD style="WIDTH: 27.26%" vAlign="middle">
									<asp:TextBox id="txtIDPoder" Runat="server" Width="100%"></asp:TextBox></TD>
								<TD style="WIDTH: 60%" vAlign="middle" align="left">
									<asp:Label id="lblSeleccionarPoder" runat="server" Enabled="False" Visible="False">
										<A onclick="window.open('PoderSelec.aspx','', ' scrollbars=yes, width=700, height=450'); return false;"
											href="#" class="LinkBuscar">Elegir</A></asp:Label></TD>
							</TR>
							<TR>
								<TD vAlign="middle" width="3%">&nbsp;
								</TD>
								<TD vAlign="middle" width="10%">
									<P class="EtiqSubTitulo"></P>
								</TD>
								<TD vAlign="middle" width="1%">
									<P class="EtiqSubTitulo"></P>
								</TD>
								<TD style="WIDTH: 26.54%" vAlign="middle" align="right">
									<asp:button id="btnPoderActual" runat="server" Width="114px" CssClass="Button" Font-Bold="True"
										Text="Asignar Poder"></asp:button></TD>
								<TD style="WIDTH: 60%" vAlign="middle" align="left"></TD>
							</TR>
						</TABLE>
					</asp:Panel>
					<asp:Panel id="pnlActualPoder" Runat="server" Enabled="True" Visible="True">
						<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 107px" vAlign="middle" width="3%"></TD>
								<TD style="HEIGHT: 107px" align="left" width="97%">
									<asp:DataGrid id="dgPoderActual" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False">
										<HeaderStyle CssClass="cell_header" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle CssClass="cell"></ItemStyle>
										<Columns>
											<asp:BoundColumn HeaderText="C&#243;d." DataField="ID" ItemStyle-Width="8%"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Propietario" DataField="Denominacion" ItemStyle-Width="23%"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Domicilio" DataField="Domicilio" ItemStyle-Width="23%"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Concepto" DataField="Concepto" ItemStyle-Width="23%"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Observaci&#243;n" DataField="Obs" ItemStyle-Width="23%"></asp:BoundColumn>
										</Columns>
									</asp:DataGrid></TD>
							</TR>
							<TR>
								<TD vAlign="middle" width="3%"></TD>
								<TD align="right" width="97%">
									<asp:button id="btnVolverPoderes" runat="server" Width="114px" CssClass="Button" Font-Bold="True"
										Text="Reasignar Poder"></asp:button></TD>
							</TR>
						</TABLE>
					</asp:Panel>
				</asp:Panel>
			</asp:panel>
			<!-- 
			================================================================================
			PANEL ASIGNAR DOCUMENTOS - INGRESA DOCUMENTOS Y DATOS CONCERNIENTES AL TV
			================================================================================
			--><asp:panel id="pnlAsignarDocumentos" Runat="server" Visible="False" Enabled="True">
				<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="Etiqueta" style="WIDTH: 100%" vAlign="middle">Asignar Documentos
						</TD>
					</TR>
				</TABLE>
				<HR width="100%" color="#6699cc" SIZE="1">
				<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="middle" width="3%"></TD>
						<TD align="left" width="97%">
							<asp:DataGrid id="dgAsignarDocumentos" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False">
								<HeaderStyle CssClass="cell_header" HorizontalAlign="Center"></HeaderStyle>
								<ItemStyle CssClass="cell"></ItemStyle>
								<Columns>
									<asp:BoundColumn ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderText="Descripción"
										DataField="Campo" ItemStyle-Width="20%"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Valor" ItemStyle-Width="80%">
										<ItemTemplate>
											<p style="MARGIN-TOP: 1pt; MARGIN-BOTTOM: 1pt">
												<asp:TextBox ID="txtValor" Width="95%" Runat="server"></asp:TextBox></p>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:DataGrid></TD>
					</TR>
				</TABLE>
			</asp:panel><asp:panel id="pnlGrabar" Runat="server" Visible="False" Enabled="True">
				<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
					<TR>
						<TD vAlign="middle" align="right" width="100%">
							<asp:button id="btnSalir" runat="server" Width="114px" CssClass="Button" Font-Bold="True" Text="Salir"></asp:button>
							<asp:button id="btnGrabar" runat="server" Width="114px" CssClass="Button" Font-Bold="True" Text="Grabar"></asp:button></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!--
			================================================================================
			FIN DE PANELES
			================================================================================
			--></form>
		<br>
		<!--BEGIN Validacion para Grabar-->
		<uc1:formvalidator id="Formvalidator4" runat="server" ButtonId="btnGrabar" Message="Verificar Datos">
			<uc1:FieldValidator id="Fieldvalidator5" runat="server" Message="Debe especificar la atenci&oacute;n."
				NAME="Fieldvalidator4" ControlToValidate="ddlAtencion,tbNuevaAtencion" type="oneRequired"></uc1:FieldValidator>
			<uc1:FieldValidator runat="server" type="oneRequired" DataType="ID" ControlToValidate="eccCliente_TextBox1,eccCliente_Combo"
				Message="Debe especificar el Cliente." ID="Fieldvalidator2" NAME="Fieldvalidator2" />
			<uc1:FieldValidator id="Fieldvalidator6" runat="server" Message="Debe especificar la correspondencia."
				NAME="Fieldvalidator4" ControlToValidate="txtCorrespNro,txtCorrespAnio" type="required"></uc1:FieldValidator>
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="tbReferenciaCliente" Message="Debe especificar la referencia del Cliente."
				ID="Fieldvalidator1" NAME="Fieldvalidator4" />
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtRefCorresp" Message="Debe especificar la referencia de la Correspondencia."
				ID="Fieldvalidator7" NAME="Fieldvalidator4" />
		</uc1:formvalidator>
		<!--END Validacion para Grabar-->
		<!--BEGIN Validacion para Seleccion de Poder-->
		<uc1:formvalidator id="Formvalidator1" runat="server" ButtonId="btnPoderActual" Message="Verificar Datos">
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtIDPoder" Message="Debe especificar el poder/propietario."
				ID="Fieldvalidator3" NAME="Fieldvalidator4" />
		</uc1:formvalidator>
		<!--END Validacion para Seleccion de Poder-->
		<!--BEGIN Validacion para Seleccion de Marca-->
		<uc1:formvalidator id="Formvalidator3" runat="server" ButtonId="btnAsignarMarcas" Message="Verificar Datos">
			<uc1:FieldValidator runat="server" type="oneRequired" ControlToValidate="txtRegistros,txtActas" Message="Debe especificar Nro.Registro o acta/a&ntilde;o."
				ID="Fieldvalidator4" NAME="Fieldvalidator4" />
		</uc1:formvalidator>
		<!--END Validacion para Seleccion de Marca-->
	</body>
</HTML>
