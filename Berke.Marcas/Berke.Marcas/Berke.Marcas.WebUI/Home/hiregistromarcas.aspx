<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.HIRegistroMarcas" CodeFile="HIRegistroMarcas.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="FormValidator" Src="../Tools/Controls/FormValidator.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FieldValidator" Src="../Tools/Controls/FieldValidator.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HIRegistroMarcas</title>
		<meta name="vs_snapToGrid" content="False">
		<meta name="vs_showGrid" content="False">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/globalstyle.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssDgPopUp.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssDgEstiloGeneral.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssMainStyle.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssPopUp.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssDgMarcaClase.css">
		<!-- BEGIN - Necesario para los Validators --><LINK rel="stylesheet" type="text/css" href="../Tools/js/window/css/modal-message.css">
		<script type="text/javascript" src="../Tools/js/window/js/ajax.js"></script>
		<script type="text/javascript" src="../Tools/js/window/js/modal-message.js"></script>
		<script type="text/javascript" src="../Tools/js/window/js/ajax-dynamic-content.js"></script>
		<script type="text/javascript" src="../Tools/js/validators.js"></script>
		<!-- END - Necesario para los Validators -->
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<P class="titulo">Hoja de Inicio de Registro
				<asp:label id="lbCabecera" runat="server" Font-Names="Verdana"></asp:label></P>
			<asp:panel id="PanelCabecera" runat="server" Width="98%">
				<FIELDSET><LEGEND>Datos generales</LEGEND><FONT size="4" face="Verdana"><STRONG></STRONG></FONT>
					<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD style="WIDTH: 10%">
								<P class="Etiqueta2">Corresp. (Nro./Año)</P>
							</TD>
							<TD style="WIDTH: 15%">
								<asp:TextBox id="tbNroCorrespondencia" runat="server" Width="100%" CssClass="FuenteViasDirPF1"></asp:TextBox></TD>
							<TD style="WIDTH: 1%">
								<P class="Etiqueta2">/</P>
							</TD>
							<TD style="WIDTH: 15%">
								<asp:TextBox id="tbAnho" runat="server" Width="100%" CssClass="FuenteViasDirPF1"></asp:TextBox></TD>
							<TD style="WIDTH: 12%">
								<P class="Etiqueta2">Facturable:</P>
							</TD>
							<TD style="WIDTH: 4%">
								<asp:CheckBox id="cbFacturable" runat="server" Width="100%" Checked="True" Height="24px"></asp:CheckBox></TD>
							<TD style="WIDTH: 6%">
								<P class="Etiqueta2">Sustituida:</P>
							</TD>
							<TD style="WIDTH: 4%">
								<asp:CheckBox id="cbSustituida" runat="server" Width="100%" Checked="True" Height="24px" AutoPostBack="True"></asp:CheckBox></TD>
							<TD style="WIDTH: 8%">
								<P class="Etiqueta2">Por derecho propio:</P>
							</TD>
							<TD style="WIDTH: 25%">
								<asp:CheckBox id="cbDerechoPropio" runat="server" Width="100%" Checked="True" Height="24px" AutoPostBack="True"></asp:CheckBox></TD>
						</TR>
					</TABLE>
					<TABLE id="Table108" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD style="WIDTH: 10%" class="noteMacro">
								<asp:Label id="lblAgenteLocal" runat="server" Width="100%" CssClass="Etiqueta2" Visible="False">Agente Local</asp:Label></TD>
							<TD style="WIDTH: 90%" class="noteMacro">
								<ecctrl:eccombo id="eccAgenteLocal" runat="server" Visible="False" ShowLabel="False" width="60%"></ecctrl:eccombo>
								<asp:Label id="lblInfoSustituidas" runat="server" Width="40%" Height="12px" Visible="False"
									Font-Bold="True" BackColor="Transparent" ForeColor="#C00000">Esta información es requerida para marcas sustituidas</asp:Label></TD>
						</TR>
					</TABLE>
					<TABLE id="table109" cellSpacing="0" cellPadding="1" width="100%">
						<TR>
							<TD style="WIDTH: 10%">
								<asp:Label id="Label4" runat="server" Width="100%" CssClass="Etiqueta2">Tipo de Atención</asp:Label></TD>
							<TD style="WIDTH: 35%">
								<asp:RadioButtonList style="Z-INDEX: 0" id="rblTipoAtencion" runat="server" Width="100%" AutoPostBack="True"
									RepeatDirection="Horizontal">
									<asp:ListItem Value="0" Selected="True">Por tr&#225;mite (Normal)</asp:ListItem>
									<asp:ListItem Value="1">Por Marca</asp:ListItem>
									<asp:ListItem Value="2">Por Bussiness Unit</asp:ListItem>
								</asp:RadioButtonList></TD>
							<TD style="WIDTH: 8%">
								<asp:Label id="lblCliente" runat="server" Width="100%" CssClass="Etiqueta2">Cliente</asp:Label></TD>
							<TD style="WIDTH: 47%">
								<ecctrl:eccombo id="eccCliente" runat="server" ShowLabel="False" width="100%"></ecctrl:eccombo></TD>
						</TR>
					</TABLE>
					<TABLE cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD style="WIDTH: 10%">
								<asp:label id="lbAtencion" Width="100%" CssClass="Etiqueta2" Runat="server">Atención</asp:label></TD>
							<TD colSpan="9">
								<asp:DropDownList id="ddlAtencion" runat="server" Width="300px" Height="16px"></asp:DropDownList>
								<asp:textbox id="tbNuevaAtencion" runat="server" Width="300px" Enabled="False"></asp:textbox>
								<asp:button id="btnNuevaAtencion" runat="server" Width="97px" CssClass="Button" Font-Bold="True"
									Text="Nueva atención"></asp:button></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 10%">
								<asp:label id="lbBU" Width="100%" CssClass="Etiqueta2" Visible="False" Runat="server">Bussiness Unit</asp:label></TD>
							<TD colSpan="9">
								<asp:DropDownList id="ddlBussinessUnit" runat="server" Width="300px" Height="16px" AutoPostBack="True"
									Visible="False"></asp:DropDownList>
								<asp:label id="lbAtencionAsigandaBU" Width="30px" CssClass="Etiqueta2" Visible="False" Runat="server">Atención</asp:label>
								<asp:textbox id="tbAtencionBU" runat="server" Width="300px" Visible="False" ReadOnly="True"></asp:textbox></TD>
						</TR>
						<TR>
							<TD></TD>
							<TD colSpan="9"><!--<BR>
								<BR>-->
								<TABLE class="tbl" cellSpacing="0" cellPadding="0" width="98%">
									<TR align="center">
										<TD style="WIDTH: 70%" class="cell_header">
											<P>Direccion Cliente</P>
										</TD>
										<TD style="WIDTH: 30%" class="cell_header">
											<P>Pais Cliente</P>
										</TD>
									</TR>
									<TR>
										<TD style="WIDTH: 70%" class="cell">
											<asp:Label id="lbDireccionCliente" runat="server" Width="100%"></asp:Label></TD>
										<TD style="WIDTH: 30%" class="cell">
											<asp:Label id="lbCiudadCliente" runat="server" Width="100%"></asp:Label></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
					<TABLE id="Table21" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD style="WIDTH: 10%">
								<P class="Etiqueta2">Referencia cliente:</P>
							</TD>
							<TD style="WIDTH: 90%">
								<asp:TextBox id="tbRefCliente" runat="server" Width="100%" CssClass="FuenteViasDirPF1" TextMode="MultiLine"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE style="WIDTH: 100%; HEIGHT: 32px" id="Table20" cellSpacing="1" cellPadding="1">
						<TR>
							<TD style="WIDTH: 10%">
								<asp:Label id="lbPoderPropietario" runat="server" Width="100%" CssClass="Etiqueta2"></asp:Label></TD>
							<TD style="WIDTH: 10%">
								<asp:TextBox id="tbIdPoder" runat="server" Width="100%" CssClass="FuenteViasDirPF1"></asp:TextBox></TD>
							<TD style="WIDTH: 10%">
								<asp:button id="btnBuscar" runat="server" Width="100%" CssClass="Button" Font-Bold="True" Text="Buscar"></asp:button></TD>
							<TD style="WIDTH: 70%">
								<P>&nbsp;</P>
							</TD>
						</TR>
						<TR>
							<TD></TD>
							<TD colSpan="3">
								<TABLE class="tbl" cellSpacing="0" cellPadding="1" width="98%">
									<TR align="center">
										<TD style="WIDTH: 35%" class="cell_header">
											<P>Denominacion</P>
										</TD>
										<TD style="WIDTH: 35%" class="cell_header">
											<P>Domicilio</P>
										</TD>
										<TD style="WIDTH: 10%" class="cell_header">
											<P>Acta</P>
										</TD>
										<TD style="WIDTH: 10%" class="cell_header">
											<P>Inscripción</P>
										</TD>
									</TR>
									<TR>
										<TD style="WIDTH: 35%" class="cell">
											<asp:Label id="lbDenominacion" runat="server" Width="100%"></asp:Label></TD>
										<TD style="WIDTH: 35%" class="cell">
											<asp:Label id="lbDomicilio" runat="server" Width="100%"></asp:Label></TD>
										<TD style="WIDTH: 10%" class="cell">
											<asp:Label id="lbActa" runat="server" Width="100%"></asp:Label></TD>
										<TD style="WIDTH: 10%" class="cell">
											<asp:Label id="lbInscripcion" runat="server" Width="100%"></asp:Label></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
					<TABLE style="WIDTH: 100%; HEIGHT: 32px" id="Table80" cellSpacing="1" cellPadding="1">
						<TR>
							<TD style="WIDTH: 10%">
								<P class="Etiqueta2">Instrucción Poder:</P>
							</TD>
							<TD style="WIDTH: 40%">
								<asp:TextBox id="tbInstruccionPoder" runat="server" Width="100%" CssClass="FuenteViasDirPF1"
									Height="32px" TextMode="MultiLine"></asp:TextBox></TD>
							<TD style="WIDTH: 10%">
								<P class="Etiqueta2">Instrucción Contabilidad:</P>
							</TD>
							<TD style="WIDTH: 40%">
								<asp:TextBox id="tbInstruccionContabilidad" runat="server" Width="100%" CssClass="FuenteViasDirPF1"
									Height="32px" TextMode="MultiLine"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE style="WIDTH: 100%; HEIGHT: 32px" id="Table3" cellSpacing="1" cellPadding="1">
						<TR>
							<TD style="WIDTH: 10%">
								<P class="Etiqueta2">Observación:</P>
							</TD>
							<TD style="WIDTH: 90%">
								<asp:TextBox id="tbObservacion" runat="server" Width="100%" CssClass="FuenteViasDirPF1" Height="32px"
									TextMode="MultiLine"></asp:TextBox></TD>
						</TR>
					</TABLE>
				</FIELDSET>
			</asp:panel><br>
			<asp:panel id="Panel2" runat="server" Width="98%" CssClass="infoMacro" Visible="False">
				<TABLE style="WIDTH: 100%; HEIGHT: 45px" id="Table4" cellSpacing="1" cellPadding="1" width="100%">
					<TR>
						<TD style="WIDTH: 40%">
							<asp:Label id="Label1" runat="server" Width="100%" CssClass="subtitulo" Height="16px">Detalle de Marcas</asp:Label></TD>
						<TD style="WIDTH: 10%">
							<asp:Button id="btnRestaurarClase" runat="server" Width="100%" CssClass="Button" Font-Bold="True"
								Text="Restaurar Clase"></asp:Button></TD>
						<TD style="WIDTH: 25%">
							<asp:DropDownList id="ddlIdioma" runat="server" Width="100%" Height="21px" Visible="false">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="I">Ingl&#233;s</asp:ListItem>
								<asp:ListItem Value="F">Franc&#233;s</asp:ListItem>
								<asp:ListItem Value="A">Alem&#225;n</asp:ListItem>
							</asp:DropDownList></TD>
						<TD style="WIDTH: 25%">
							<asp:Button id="btnCopiarClase" runat="server" Width="168px" CssClass="Button" Visible="True"
								Font-Bold="True" Text="Copiar Descripción de Clase"></asp:Button></TD>
						<TD style="WIDTH: 25%">
							<asp:Button id="btnCopiarPrioridad" runat="server" Width="168px" CssClass="Button" Visible="True"
								Font-Bold="True" Text="Copiar Prioridad"></asp:Button></TD>
					</TR>
				</TABLE>
				<asp:datagrid id="GridMarcas2" runat="server" Width="100%" CssClass="tbl" Height="60px" PageSize="1"
					AutoGenerateColumns="False" Font-Size="Smaller">
					<ItemStyle CssClass="cell"></ItemStyle>
					<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
					<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Sel.">
							<ItemTemplate>
								<asp:CheckBox id="cbSel" runat="server" Width="100%"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Marcas">
							<HeaderStyle Width="35%"></HeaderStyle>
							<ItemTemplate>
								<asp:TextBox id="tbDatosMarcas" runat="server" Width="100%" Height="55px" CssClass="FuenteViasDirPF1"
									TextMode="MultiLine" BackColor="White" ReadOnly="True"></asp:TextBox>
								<asp:Label id="Label2" runat="server" Width="20%" Height="10px">Ref.Marca</asp:Label>
								<asp:TextBox id="tbReferenciaMarca" runat="server" Width="75%" Height="20px" CssClass="FuenteViasDirPF1"
									BackColor="White" ReadOnly="False"></asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Descripci&#243;n de clase">
							<HeaderStyle Width="35%"></HeaderStyle>
							<ItemTemplate>
								<asp:TextBox id="tbDescripClase" runat="server" Width="100%" Height="70px" TextMode="MultiLine"
									CssClass="FuenteViasDirPF1"></asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Limitada">
							<HeaderStyle Width="5%"></HeaderStyle>
							<ItemTemplate>
								<asp:CheckBox id="cbLimitada" runat="server" Height="24px" Width="100%" Checked="True"></asp:CheckBox>
								<asp:TextBox id="tbIdExpediente2" runat="server" Visible="False"></asp:TextBox>
								<asp:TextBox id="tbDenominacion2" runat="server" Visible="False"></asp:TextBox>
								<asp:TextBox id="tbClave2" runat="server" Visible="False"></asp:TextBox>
								<asp:TextBox id="tbTipoMarca2" runat="server" Visible="False"></asp:TextBox>
								<asp:TextBox id="tbClase2" runat="server" Visible="False"></asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Nro. prioridad">
							<HeaderStyle Width="8%"></HeaderStyle>
							<ItemTemplate>
								<asp:TextBox id="tbNroPrioridad" runat="server" Width="100%" CssClass="cell"></asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Fecha prioridad dd/mm/aaaa">
							<HeaderStyle Width="10%"></HeaderStyle>
							<ItemTemplate>
								<asp:TextBox id="tbFechaPrioridad" runat="server" Width="100%" CssClass="cell"></asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Pais prioridad">
							<HeaderStyle Width="10%"></HeaderStyle>
							<ItemTemplate>
								<asp:DropDownList id="ddlPaisPrioridad" runat="server" Width="100%" Height="16px"></asp:DropDownList>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="ID Logo">
							<HeaderStyle Width="8%"></HeaderStyle>
							<ItemTemplate>
								<asp:TextBox id="tbIdLogotipo" runat="server" Width="100%" CssClass="cell"></asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:ButtonColumn Text="Elegir Logo" CommandName="Delete"></asp:ButtonColumn>
					</Columns>
				</asp:datagrid>
				<TABLE style="WIDTH: 100%; HEIGHT: 32px" id="Table5">
					<TR>
						<TD style="WIDTH: 30%">
							<P></P>
						</TD>
						<TD style="WIDTH: 1%">
							<asp:Button id="btnAtras" runat="server" Width="100%" CssClass="Button" Font-Bold="True" Enabled="False"
								Text="Atras"></asp:Button></TD>
						<TD style="WIDTH: 10%">
							<P></P>
						</TD>
						<TD style="WIDTH: 10%">
							<asp:Button id="btnFinalizar" runat="server" Width="100%" CssClass="Button" Font-Bold="True"
								Text="Finalizar"></asp:Button></TD>
						<TD style="WIDTH: 20%">
							<P></P>
						</TD>
						<TD style="WIDTH: 10%">
							<asp:Button id="btnCancelar2" runat="server" Width="100%" CssClass="Button" Font-Bold="True"
								Text="Cancelar"></asp:Button></TD>
						<TD style="WIDTH: 30%">
							<P></P>
						</TD>
					</TR>
				</TABLE>
			</asp:panel><br>
			<asp:panel id="Panel1" runat="server" Width="98%" CssClass="infoMacro">
				<TABLE style="WIDTH: 100%; HEIGHT: 32px" id="Table6" cellSpacing="1" cellPadding="1">
					<TR>
						<TD style="WIDTH: 40%">
							<asp:Label id="Label3" runat="server" Width="100%" CssClass="subtitulo" Height="16px">Detalle de Marcas</asp:Label></TD>
						<TD style="WIDTH: 10%">
							<P></P>
						</TD>
						<TD style="WIDTH: 10%">
							<asp:Button id="btnAgregarDetalle" runat="server" Width="100%" CssClass="Button" Font-Bold="True"
								Text="Agregar Detalle"></asp:Button></TD>
						<TD style="WIDTH: 10%">
							<P></P>
						</TD>
						<TD style="WIDTH: 10%">
							<asp:Button id="btnEliminarDetalle" runat="server" Width="100%" CssClass="Button" Font-Bold="True"
								Enabled="False" Text="Eliminar Detalle"></asp:Button></TD>
						<TD style="WIDTH: 10%">
							<P></P>
						</TD>
					</TR>
				</TABLE>
				<asp:datagrid id="GridMarcas1" runat="server" Width="780px" CssClass="tbl" Height="80px" PageSize="1"
					AutoGenerateColumns="False" Font-Size="Smaller" onselectedindexchanged="GridMarcas1_SelectedIndexChanged">
					<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
					<ItemStyle CssClass="cell"></ItemStyle>
					<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Sel.">
							<HeaderStyle Width="2%"></HeaderStyle>
							<ItemTemplate>
								<asp:CheckBox id="cbSel1" runat="server" Width="100%"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Denominaci&#243;n">
							<HeaderStyle Width="18%"></HeaderStyle>
							<ItemTemplate>
								<asp:TextBox id="tbDenominacion" runat="server" CssClass="cell" Width="100%" Height="32px" TextMode="MultiLine" OnTextChanged="tbDenominacion_TextChanged"></asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Clave">
							<HeaderStyle Width="18%"></HeaderStyle>
							<ItemTemplate>
								<asp:TextBox id="tbClave" runat="server" CssClass="cell" Width="163px" Height="32px" TextMode="MultiLine" BackColor="#EFEFEF" ReadOnly="True"></asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Tipo">
							<HeaderStyle Width="8%"></HeaderStyle>
							<ItemTemplate>
								<asp:DropDownList id="ddlTipoMarca" runat="server" Width="100%" Height="24px" CssClass="cell">
									<asp:ListItem></asp:ListItem>
									<asp:ListItem Value="D">Denominativa</asp:ListItem>
									<asp:ListItem Value="F">Figurativa</asp:ListItem>
									<asp:ListItem Value="M">Mixta</asp:ListItem>
                                    <asp:ListItem Value="T">Tridimensional</asp:ListItem>
                                    <asp:ListItem Value="O">Olfativa</asp:ListItem>
                                    <asp:ListItem Value="S">Sonora</asp:ListItem>
								</asp:DropDownList>
								<asp:Label id="lbIdExpediente" runat="server" Visible="False"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Clases">
							<HeaderStyle Width="4%"></HeaderStyle>
							<ItemTemplate>
								<asp:TextBox id="tbClase" runat="server" Width="100%" CssClass="cell"></asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:ButtonColumn Text="Elegir Clase" CommandName="Delete"></asp:ButtonColumn>
					</Columns>
				</asp:datagrid>
				<TABLE style="WIDTH: 100%; HEIGHT: 32px" id="Table7" cellSpacing="1" cellPadding="1">
					<TR>
						<TD style="WIDTH: 35%">
							<P></P>
						</TD>
						<TD style="WIDTH: 10%">
							<asp:Button id="btnSiguiente" runat="server" Width="100%" CssClass="Button" Font-Bold="True"
								Text="Siguiente" OnClick="btnSiguiente_Click1"></asp:Button></TD>
						<TD style="WIDTH: 10%">
							<P></P>
						</TD>
						<TD style="WIDTH: 10%">
							<asp:Button id="btnCancelar" runat="server" Width="100%" CssClass="Button" Font-Bold="True"
								Text="Cancelar"></asp:Button></TD>
						<TD style="WIDTH: 45%">
							<P></P>
						</TD>
					</TR>
				</TABLE>
			</asp:panel></form>
		<uc1:formvalidator id="FormValidator1" runat="server" Message="Verificar Datos" ButtonId="btnSiguiente">
			<uc1:FieldValidator runat="server" type="required" DataType="ID" ControlToValidate="tbNroCorrespondencia"
				Message="Debe especificar el Nro. de Correspondencia." />
			<uc1:FieldValidator runat="server" type="required" DataType="Date.Year" ControlToValidate="tbAnho" Message="Debe especificar el a&ntilde;o de la Correspondencia." />
			<uc1:FieldValidator runat="server" type="oneRequired" DataType="ID" ControlToValidate="eccCliente_TextBox1,eccCliente_Combo"
				Message="Debe especificar el Cliente." />
			<uc1:FieldValidator runat="server" type="oneRequired" DataType="ID" ControlToValidate="eccAgenteLocal_TextBox1,eccAgenteLocal_Combo"
				Message="Debe especificar el Agente de la sustituci&oacute;n." Depends="cbSustituida" />
			<uc1:FieldValidator runat="server" type="oneRequired" ControlToValidate="ddlAtencion,tbNuevaAtencion,ddlBussinessUnit"
				Message="Debe especificar la Atenci&oacute;n" />
			<uc1:FieldValidator runat="server" type="required" DataType="ID" ControlToValidate="tbIdPoder" Message="Debe especificar el Poder/Propietario." />
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="tbRefCliente" Message="Debe especificar la referencia del Cliente."
				ID="Fieldvalidator1" />
		</uc1:formvalidator><br>
	</body>
</HTML>
