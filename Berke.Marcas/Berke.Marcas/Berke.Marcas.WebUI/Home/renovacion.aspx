<%@ Register TagPrefix="uc1" TagName="FieldValidator" Src="../Tools/Controls/FieldValidator.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FormValidator" Src="../Tools/Controls/FormValidator.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Page language="c#" smartnavigation="True" validateRequest="false" Inherits="Berke.Marcas.WebUI.Home.Renovacion" CodeFile="Renovacion.aspx.cs" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Renovación</title>
		<meta name="vs_snapToGrid" content="False">
		<meta name="vs_showGrid" content="False">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/globalstyle.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssMainStyle.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssDgEstiloGeneral.css">
		<!-- BEGIN - Necesario para los Validators --><LINK rel="stylesheet" type="text/css" href="../Tools/js/window/css/modal-message.css">
		<script type="text/javascript" src="../Tools/js/window/js/ajax.js"></script>
		<script type="text/javascript" src="../Tools/js/window/js/modal-message.js"></script>
		<script type="text/javascript" src="../Tools/js/window/js/ajax-dynamic-content.js"></script>
		<script type="text/javascript" src="../Tools/js/validators.js"></script>
		<!-- END - Necesario para los Validators -->
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P><uc1:header id="Header1" runat="server"></uc1:header></P>
			<P class="titulo">Hoja de Inicio de Renovación&nbsp;
				<asp:label id="lRenovacion" runat="server"></asp:label>&nbsp;&nbsp; &nbsp;
				<asp:label id="lUser" runat="server" Font-Names="Verdana"></asp:label></P>
			<P><asp:panel id="pnlRenovacion" Runat="server" Width="98%" CssClass="infoMacro">
					<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="2" width="98%">
						<TR>
							<TD class="Etiqueta2" width="15%">Correspondencia N°</TD>
							<TD width="1%"></TD>
							<TD>
								<asp:TextBox id="txtReferenciaNro" runat="server" Columns="10" MaxLength="8"></asp:TextBox>/&nbsp;
								<asp:TextBox id="txtReferenciaAnio" runat="server" Columns="5" MaxLength="4"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:button id="bCorrespondencia" runat="server" CssClass="Button" Width="159px" Text="Verificar Correspondencia"
									Font-Bold="True"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:label id="lCorID" runat="server" Font-Names="Verdana"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:checkbox id="chkFacturable" runat="server" Text="Facturable" Checked="True"></asp:checkbox></TD>
						</TR>
						<TR>
							<TD class="Etiqueta2">Ref. del Cliente</TD>
							<TD></TD>
							<TD>
								<asp:TextBox id="txtRefCorrespondencia" runat="server" Width="593" TextMode="MultiLine" Rows="2"
									Height="30"></asp:TextBox></TD>
						</TR> <!--<TABLE id="table109" cellSpacing="0" cellPadding="1" width="100%">-->
						<TR> <!--<TD style="WIDTH: 10%">-->
							<TD style="WIDTH: 15%" class="Etiqueta2">Tipo de Atención</TD> <!--	<asp:Label id="Label4" runat="server" Width="100%" CssClass="Etiqueta2">Tipo de Atención</asp:Label></TD>-->
							<TD></TD>
							<TD>
								<asp:RadioButtonList style="Z-INDEX: 0" id="rblTipoAtencion" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
									<asp:ListItem Value="0" Selected="True">Por tr&#225;mite (Normal)</asp:ListItem>
									<asp:ListItem Value="1">Por Marca</asp:ListItem>
									<asp:ListItem Value="2">Por Bussiness Unit</asp:ListItem>
								</asp:RadioButtonList></TD>
						</TR>
						<TR>
							<TD class="Etiqueta2">Cliente</TD>
							<TD></TD>
							<TD>
								<ecCtrl:ecCombo id="eccCliente" runat="server" Width="45%" ShowLabel="False"></ecCtrl:ecCombo>&nbsp;
								<FONT size="1" face="Verdana"><FONT size="1" face="Verdana"><STRONG>
											<asp:label id="lAtencion" runat="server" Font-Names="Verdana"></asp:label></STRONG></FONT></FONT>
								<asp:DropDownList id="ddlAtencion" runat="server" Visible="False"></asp:DropDownList>
								<asp:textbox id="tbNuevaAtencion" runat="server" Enabled="False"></asp:textbox>
								<asp:button id="btnNuevaAtencion" runat="server" CssClass="Button" Text="Nueva atención" Font-Bold="True"
									Enabled="False"></asp:button></TD>
						</TR>
						<TR>
							<TD></TD>
							<TD></TD>
							<TD>
								<asp:Label id="lDireccionCte" runat="server"></asp:Label></TD>
						</TR>
						<TR>
							<TD class="Etiqueta2">
								<asp:label id="lbBU" CssClass="Etiqueta2" Width="100%" Runat="server" Visible="False">Bussiness Unit</asp:label></TD>
							<TD></TD>
							<TD>
								<asp:DropDownList id="ddlBussinessUnit" runat="server" Width="300px" Height="16px" AutoPostBack="True"
									Visible="False"></asp:DropDownList>
								<asp:label id="lbAtencionAsigandaBU" CssClass="Etiqueta2" Width="30px" Runat="server" Visible="False">Atención</asp:label>
								<asp:textbox id="tbAtencionBU" runat="server" Width="300px" Visible="False" ReadOnly="True"></asp:textbox></TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="lPoder" runat="server" CssClass="Etiqueta2" Width="100%">Poder</asp:label></TD>
							<TD></TD>
							<TD>
								<asp:TextBox id="txtIDPoder" runat="server" Columns="7" MaxLength="7"></asp:TextBox>&nbsp;
								<asp:button id="btnAsignarP" runat="server" CssClass="Button" Width="118px" Text="Elegir Poder"
									Font-Bold="True"></asp:button>
								<asp:CheckBox id="chkDerechoPropio" runat="server" Text="Derecho Propio" AutoPostBack="True"></asp:CheckBox></TD>
						</TR>
						<TR>
							<TD></TD>
							<TD></TD>
							<TD>
								<asp:DataGrid id="dgPoderActual" runat="server" CssClass="tbl" Width="100%" Height="28px" AutoGenerateColumns="False">
									<ItemStyle HorizontalAlign="Center" CssClass="cell"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="ID" HeaderText="C&#243;d.">
											<ItemStyle Width="8%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Denominacion" HeaderText="Propietario">
											<ItemStyle Width="23%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Domicilio" HeaderText="Domicilio">
											<ItemStyle Width="23%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Concepto" HeaderText="Concepto">
											<ItemStyle Width="23%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Obs" HeaderText="Observaci&#243;n">
											<ItemStyle Width="23%"></ItemStyle>
										</asp:BoundColumn>
									</Columns>
								</asp:DataGrid></TD>
						</TR>
						<TR>
							<TD>
								<P class="Etiqueta2">Instrucción Poder</P>
							</TD>
							<TD></TD>
							<TD>
								<asp:textbox id="txtInstPoder" runat="server" Width="593px" TextMode="MultiLine" Rows="5" Height="30px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD>
								<P class="Etiqueta2">Instrucción Contabilidad</P>
							</TD>
							<TD></TD>
							<TD>
								<asp:textbox id="txtInstContabilidad" runat="server" Width="593px" TextMode="MultiLine" Rows="5"
									Height="30px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD>
								<P class="Etiqueta2">Observación</P>
							</TD>
							<TD></TD>
							<TD><FONT size="1" face="Verdana">
									<asp:textbox id="txtObservacion" runat="server" Width="593px" TextMode="MultiLine" Rows="5" Height="30px"></asp:textbox></FONT></TD>
						</TR>
					</TABLE>
					<TABLE id="TblIngOT" border="0" cellSpacing="0" cellPadding="0" width="98%">
					</TABLE>
				</asp:panel></P>
			<asp:panel id="pnlMarcasElegir" Runat="server" Width="100%">
				<TABLE style="MARGIN-LEFT: 5px" id="tblAsignarMarcas" class="tbl" border="0" cellSpacing="2"
					cellPadding="0" width="98%">
					<TR>
						<TD style="WIDTH: 144px" align="right"></TD>
						<TD style="WIDTH: 6px"></TD>
						<TD style="WIDTH: 293px">
							<asp:textbox id="txtActaNro" runat="server" Columns="7" MaxLength="7" Visible="False"></asp:textbox>&nbsp;
							<asp:textbox id="txtActaAnio" runat="server" Columns="4" MaxLength="4" Visible="False"></asp:textbox></TD>
						<TD style="WIDTH: 186px" class="Etiqueta2">Registro Nro.&nbsp;
							<asp:textbox id="txtRegistroNro" runat="server" Columns="7" MaxLength="7"></asp:textbox></TD>
						<TD>
							<asp:button id="btnAsignarMarcas" runat="server" CssClass="Button" Text="Elegir Marca" Font-Bold="True"></asp:button></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<P><asp:panel id="pnlMarcasAsignar" Runat="server" Width="98%" CssClass="infoMacro">&nbsp; 
<TABLE class="tbl" cellSpacing="0" cellPadding="2" width="100%">
						<TR>
							<TD class="cell_header" width="30%" align="center">Denominación</TD>
							<TD class="cell_header" width="30%" align="center">Denominación Clave</TD>
							<TD class="cell_header" width="10%">
								<DIV align="center">Registro</DIV>
							</TD>
							<TD class="cell_header" width="15%">
								<DIV align="center">Clase Anterior</DIV>
							</TD>
							<TD class="cell_header" width="9.7%">
								<DIV align="center">Tipo</DIV>
							</TD>
							<TD class="cell_header" width="18%">
								<DIV align="center">Clase Nueva</DIV>
							</TD>
							<TD class="cell_header" width="8%">
								<DIV align="center">Limitada</DIV>
							</TD>
							<TD class="cell_header" width="12%">
								<DIV align="center">Fec. Vto.</DIV>
							</TD>
							<TD class="cell_header" width="6%">
								<DIV align="center">ID Logo</DIV>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 30%" class="cell">
								<DIV align="center">
									<asp:TextBox id="txtDenominacion" runat="server" Width="210px"></asp:TextBox></DIV>
							</TD>
							<TD style="WIDTH: 30%" class="cell">
								<DIV align="center">
									<asp:TextBox id="txtDenominacionClave" runat="server" Width="210px" BackColor="#EFEFEF" ReadOnly="True"></asp:TextBox></DIV>
							</TD>
							<TD style="WIDTH: 10%" class="cell">
								<DIV align="center">
									<asp:Label id="lRegistro" runat="server"></asp:Label>
									<asp:Label id="lIdMarca" runat="server" Visible="False" Enabled="False"></asp:Label></DIV>
							</TD>
							<TD style="WIDTH: 15%" class="cell">
								<DIV align="center">
									<asp:Label id="LClaseAnt" runat="server"></asp:Label></DIV>
							</TD>
							<TD style="WIDTH: 7.56%" class="cell">
								<DIV align="center">
									<asp:Label id="LblTipo" runat="server"></asp:Label></DIV>
							</TD>
							<TD style="WIDTH: 14%" class="cell">
								<DIV align="center">
									<asp:DropDownList id="ddlClaseNueva" runat="server"></asp:DropDownList>
									<asp:Button id="bDescripcion" runat="server" Text="V" Font-Bold="True"></asp:Button></DIV>
							</TD>
							<TD style="WIDTH: 8%" class="cell">
								<DIV align="center">
									<asp:CheckBox id="cbLimitada" runat="server"></asp:CheckBox></DIV>
							</TD>
							<TD style="WIDTH: 18%" class="cell">
								<DIV align="center">
									<asp:Label id="LVencimiento" runat="server"></asp:Label></DIV>
							</TD>
							<TD style="WIDTH: 6%" class="cell">
								<asp:TextBox id="tbIdLogo" runat="server" Width="100%"></asp:TextBox>
								<asp:LinkButton id="lbElegirLogo" runat="server">Elegir Logo</asp:LinkButton></TD>
						</TR>
					</TABLE>
<TABLE style="WIDTH: 100%" class="tbl" cellSpacing="0" cellPadding="0">
						<TR>
							<TD style="WIDTH: 100%" class="cell_header">
								<DIV align="center">Descripción de la Marca</DIV>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 1000%" class="cell">
								<DIV align="center">
									<asp:TextBox id="txtDescripcion" runat="server" Width="100%" TextMode="MultiLine" Height="89px"></asp:TextBox></DIV>
							</TD>
						</TR>
					</TABLE>
<TABLE style="WIDTH: 100%" class="tbl" cellSpacing="0" cellPadding="0">
						<TR>
							<TD style="WIDTH: 100%" class="cell_header">
								<DIV align="center"><STRONG>Referencia</STRONG></DIV>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 1000%" class="cell">
								<DIV align="center">
									<asp:TextBox id="txtRefMarca" runat="server" Width="100%" TextMode="MultiLine" Height="31px"></asp:TextBox></DIV>
							</TD>
						</TR>
					</TABLE></asp:panel></P>
			<P><asp:panel id="pnlEditar" Runat="server" Width="100%" Height="119px" BorderWidth="1px" BorderColor="SteelBlue">&nbsp; 
<TABLE style="WIDTH: 100%" class="tbl" border="0" cellSpacing="0" cellPadding="0">
						<TR>
							<TD style="WIDTH: 126px" class="cell_header">
								<DIV align="center"><STRONG>Idioma</STRONG></DIV>
							</TD>
							<TD style="WIDTH: 540px" class="cell_header">
								<DIV align="center">Descripción</DIV>
							</TD>
						</TR>
						<TR>
							<TD class="cell">
								<DIV align="center">
									<asp:Label id="LIdioma" runat="server" Width="20%" Height="17px"></asp:Label></DIV>
							</TD>
							<TD style="WIDTH: 540px">
								<DIV align="center">
									<asp:TextBox id="txtIdioma" runat="server" Width="80%" TextMode="MultiLine" Height="54px"></asp:TextBox></DIV>
							</TD>
						</TR>
						<TR>
							<TD>
							<TD>
								<DIV align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="bAceptarlimitaciones" runat="server" CssClass="Button" Width="114px" Text="Aceptar"
										Font-Bold="True" Height="17px" Visible="True"></asp:button></DIV>
							</TD>
						</TR>
					</TABLE></asp:panel></P>
			<asp:panel id="pnlMarcasLimitaciones" Runat="server" Width="100%" Height="92px" BorderWidth="1"
				BorderColor="SteelBlue" BorderStyle="None">
				<TABLE style="WIDTH: 100%; HEIGHT: 103px" id="tblMarcasLimitaciones" border="0" cellSpacing="0"
					cellPadding="0" width="100%">
					<TR>
						<TD style="WIDTH: 100%; HEIGHT: 103px" width="100%">
							<asp:DataGrid id="dgMarcasLimitaciones" runat="server" CssClass="tbl" Width="100%" Height="59px"
								AutoGenerateColumns="False">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="MarcaID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ClaseID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="IdiomaID"></asp:BoundColumn>
									<asp:BoundColumn DataField="Idioma" ReadOnly="True" HeaderText="Idioma"></asp:BoundColumn>
									<asp:BoundColumn DataField="Descrip" HeaderText="Descripci&#243;n"></asp:BoundColumn>
									<asp:ButtonColumn Text="Editar" HeaderText="Acci&#243;n" CommandName="Select"></asp:ButtonColumn>
								</Columns>
							</asp:DataGrid></TD>
					</TR>
				</TABLE>
			</asp:panel><asp:panel id="pnlMarcasBotones" Runat="server" Width="100%">
				<TABLE id="tblmarcasbotones" border="0" cellSpacing="0" cellPadding="0" width="98%">
					<TR>
						<TD style="WIDTH: 429px"></TD>
						<TD>
							<asp:button id="btnMarcasCancelar" runat="server" CssClass="Button" Width="114px" Text="Cancelar"
								Font-Bold="True" Height="17px" Visible="True"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
							&nbsp;&nbsp;
							<asp:button id="BntMarcasGrabar" runat="server" CssClass="Button" Width="114px" Text="Asignar Marca"
								Font-Bold="True" Height="17px" Visible="True"></asp:button></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<P><asp:panel id="pnlMarcasRenovar" Runat="server" Width="100%" Height="128px" BorderWidth="1"
					BorderColor="SteelBlue" BorderStyle="None">
<TABLE style="HEIGHT: 103px; MARGIN-LEFT: 5px" id="Table5" border="0" cellSpacing="0" cellPadding="0"
						width="98%">
						<TR>
							<TD style="WIDTH: 100%; HEIGHT: 146px" width="100%">
								<TABLE id="Table6" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
									<TR>
										<TD width="1%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
										<TD class="titletable1" bgColor="#7bb5e7" width="98%">
											<asp:Label id="lTotalMarcas" runat="server" Font-Bold="True">Marcas a ser Renovadas : </asp:Label></TD>
									</TR>
								</TABLE>
								<asp:DataGrid id="dgMarcasAsignadas" runat="server" CssClass="tbl" Width="100%" Height="73px"
									AutoGenerateColumns="False">
									<ItemStyle CssClass="cell"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="Denominacion" HeaderText="Denominaci&#243;n"></asp:BoundColumn>
										<asp:BoundColumn DataField="DenominacionClave" HeaderText="Denominacion Clave"></asp:BoundColumn>
										<asp:BoundColumn DataField="RegistroNro" HeaderText="Registro"></asp:BoundColumn>
										<asp:BoundColumn DataField="ClaseAntDescrip" HeaderText="Clase Anterior"></asp:BoundColumn>
										<asp:BoundColumn DataField="ClaseDescrip" HeaderText="Clase Nueva"></asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Lim.">
											<ItemTemplate>
												<asp:CheckBox id=chkLimitado Runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Limitada")%>' Enabled="False">
												</asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="DesEspLim" HeaderText="Descripci&#243;n"></asp:BoundColumn>
										<asp:BoundColumn DataField="Vencimiento" HeaderText="Fec.Vto.Reg." DataFormatString="{0:d}"></asp:BoundColumn>
										<asp:BoundColumn DataField="Referencia" HeaderText="Referencia"></asp:BoundColumn>
										<asp:BoundColumn DataField="LogotipoID" HeaderText="ID Logo"></asp:BoundColumn>
										<asp:ButtonColumn Text="Eliminar" HeaderText="Acci&#243;n" CommandName="Delete"></asp:ButtonColumn>
										<asp:ButtonColumn Text="Modificar" HeaderText="Acci&#243;n" CommandName="Update"></asp:ButtonColumn>
									</Columns>
								</asp:DataGrid></TD>
						</TR>
					</TABLE></TD></TR><TR>
						<TD style="WIDTH: 100%; HEIGHT: 25px" width="100%"></TD>
					</TR></TABLE></asp:panel></P>
			<asp:panel id="pnlBotones" Runat="server" Width="100%">
				<TABLE id="tblbotones" border="0" cellSpacing="0" cellPadding="0" width="98%">
					<TR>
						<TD style="WIDTH: 430px"></TD>
						<TD>
							<asp:button id="btnCancelar" runat="server" CssClass="Button" Width="114px" Text="Cancelar"
								Font-Bold="True" Height="17px" Visible="True"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
							&nbsp;&nbsp;
							<asp:button id="BntGrabar" runat="server" CssClass="Button" Width="114px" Text="Grabar" Font-Bold="True"
								Height="17px" Visible="True" onclick="BntGrabar_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
		<!--BEGIN Validacion para correspondencia--><uc1:formvalidator id="FormValidator1" runat="server" Message="Verificar Datos" ButtonId="bCorrespondencia">
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtReferenciaNro,txtReferenciaAnio"
				Message="Debe especificar el nro/a&ntilde;o de la correspondencia" />
		</uc1:formvalidator>
		<!--END Validacion para correspondencia-->
		<!--BEGIN Validacion para Seleccion de Marca--><uc1:formvalidator id="Formvalidator3" runat="server" Message="Verificar Datos" ButtonId="btnAsignarMarcas">
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtRegistroNro" Message="Debe especificar un Nro. de registro."
				ID="Fieldvalidator4" NAME="Fieldvalidator4" />
		</uc1:formvalidator>
		<!--END Validacion para Seleccion de Marca-->
		<!--BEGIN Validacion para Seleccion de Poder--><uc1:formvalidator id="Formvalidator4" runat="server" Message="Verificar Datos" ButtonId="btnAsignarP">
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtIDPoder" Message="Debe especificar Poder/Propietario."
				ID="Fieldvalidator5" NAME="Fieldvalidator4" />
		</uc1:formvalidator>
		<!--END Validacion para Seleccion de Poder-->
		<!--BEGIN Validacion para Grabar --><uc1:formvalidator id="Formvalidator2" runat="server" Message="Verificar Datos" ButtonId="BntGrabar">
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtReferenciaNro,txtReferenciaAnio"
				Message="Debe especificar el nro/a&ntilde;o de la correspondencia" ID="Fieldvalidator1" NAME="Fieldvalidator1" />
			<uc1:FieldValidator runat="server" type="oneRequired" DataType="ID" ControlToValidate="eccCliente_TextBox1,eccCliente_Combo"
				Message="Debe especificar el Cliente." ID="Fieldvalidator2" NAME="Fieldvalidator2" />
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtIDPoder" Message="Debe especificar el poder."
				ID="Fieldvalidator3" NAME="Fieldvalidator1" />
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtRefCorrespondencia" Message="Debe especificar la Referencia del Cliente."
				ID="Fieldvalidator6" NAME="Fieldvalidator1" />
		</uc1:formvalidator>
		<!--END Validacion para Grabar -->
	</body>
</HTML>
