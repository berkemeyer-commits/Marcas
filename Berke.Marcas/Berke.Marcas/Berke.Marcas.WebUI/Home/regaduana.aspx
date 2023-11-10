<%@ Reference Page="~/home/renovacion.aspx" %>
<%@ Register TagPrefix="uc1" TagName="FieldValidator" Src="../Tools/Controls/FieldValidator.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FormValidator" Src="../Tools/Controls/FormValidator.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Page language="c#" smartnavigation="True" validateRequest="false" Inherits="Berke.Marcas.WebUI.Home.RegAduana" CodeFile="RegAduana.aspx.cs" %>
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
		<!-- BEGIN - Necesario para los Validators --><LINK href="../Tools/js/window/css/modal-message.css" type="text/css" rel="stylesheet">
		<script src="../Tools/js/window/js/ajax.js" type="text/javascript"></script>
		<script src="../Tools/js/window/js/modal-message.js" type="text/javascript"></script>
		<script src="../Tools/js/window/js/ajax-dynamic-content.js" type="text/javascript"></script>
		<script src="../Tools/js/validators.js" type="text/javascript"></script>
		<!-- END - Necesario para los Validators -->
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P><uc1:header id="Header1" runat="server"></uc1:header></P>
			<P class="titulo">Hoja de Inicio de Registro en Aduanas&nbsp;
				<asp:label id="lRenovacion" runat="server"></asp:label>&nbsp;&nbsp; &nbsp;
				<asp:label id="lUser" runat="server" Font-Names="Verdana"></asp:label></P>
			<P><asp:panel id="pnlRenovacion" CssClass="infoMacro" Width="98%" Runat="server">
					<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="98%" border="0">
						<TR>
							<TD class="Etiqueta2" width="15%">Correspondencia N°</TD>
							<TD width="1%"></TD>
							<TD>
								<asp:TextBox id="txtReferenciaNro" runat="server" Columns="10" MaxLength="8"></asp:TextBox>/&nbsp;
								<asp:TextBox id="txtReferenciaAnio" runat="server" Columns="5" MaxLength="4"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:button id="bCorrespondencia" runat="server" Width="159px" CssClass="Button" Text="Verificar Correspondencia"
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
						</TR>
						<TR>
							<TD class="Etiqueta2">Cliente</TD>
							<TD></TD>
							<TD>
								<ecCtrl:ecCombo id="eccCliente" runat="server" Width="45%" ShowLabel="False"></ecCtrl:ecCombo>&nbsp;
								<FONT face="Verdana" size="1"><FONT face="Verdana" size="1"><STRONG>
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
							<TD>
								<asp:label id="lPoder" runat="server" Width="100%" CssClass="Etiqueta2">Poder</asp:label></TD>
							<TD></TD>
							<TD>
								<asp:TextBox id="txtIDPoder" runat="server" Columns="7" MaxLength="7"></asp:TextBox>&nbsp;
								<asp:button id="btnAsignarP" runat="server" Width="118px" CssClass="Button" Text="Elegir Poder"
									Font-Bold="True"></asp:button>
								<asp:CheckBox id="chkDerechoPropio" runat="server" Text="Derecho Propio" AutoPostBack="True"></asp:CheckBox></TD>
						</TR>
						<TR>
							<TD></TD>
							<TD></TD>
							<TD>
								<asp:DataGrid id="dgPoderActual" runat="server" Width="100%" CssClass="tbl" Height="28px" AutoGenerateColumns="False">
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
							<TD><FONT face="Verdana" size="1">
									<asp:textbox id="txtObservacion" runat="server" Width="593px" TextMode="MultiLine" Rows="5" Height="30px"></asp:textbox></FONT></TD>
						</TR>
					</TABLE>
					<TABLE id="TblIngOT" cellSpacing="0" cellPadding="0" width="98%" border="0">
					</TABLE>
				</asp:panel></P>
			<asp:panel id="pnlMarcasElegir" Width="100%" Runat="server">
				<TABLE class="tbl" id="tblAsignarMarcas" style="MARGIN-LEFT: 5px" cellSpacing="2" cellPadding="0"
					width="98%" border="0">
					<TR>
						<TD style="WIDTH: 144px" align="right"></TD>
						<TD style="WIDTH: 6px"></TD>
						<TD style="WIDTH: 293px">
							<asp:textbox id="txtActaNro" runat="server" Columns="7" MaxLength="7" Visible="False"></asp:textbox>&nbsp;
							<asp:textbox id="txtActaAnio" runat="server" Columns="4" MaxLength="4" Visible="False"></asp:textbox></TD>
						<TD class="Etiqueta2" style="WIDTH: 186px">Registro Nro.&nbsp;
							<asp:textbox id="txtRegistroNro" runat="server" Columns="7" MaxLength="7"></asp:textbox></TD>
						<TD>
							<asp:button id="btnAsignarMarcas" runat="server" CssClass="Button" Text="Elegir Marca" Font-Bold="True"></asp:button></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<P><asp:panel id="pnlMarcasAsignar" CssClass="infoMacro" Width="98%" Runat="server">&nbsp; 
<TABLE class="tbl" cellSpacing="0" cellPadding="2" width="100%">
						<TR>
							<TD class="cell_header" align="center" width="30%">Denominación
							</TD>
							<TD class="cell_header" width="10%">
								<DIV align="center">Registro</DIV>
							</TD>
							<TD class="cell_header" width="15%">
								<DIV align="center">Clase</DIV>
							</TD>
							<TD class="cell_header" width="9.7%">
								<DIV align="center">Tipo</DIV>
							</TD>
							<TD class="cell_header" width="12%">
								<DIV align="center">Fec. Vto.</DIV>
							</TD>
						</TR>
						<TR>
							<TD class="cell" style="WIDTH: 30%">
								<DIV align="center">
									<asp:Label id="lblDenominacion" runat="server"></asp:Label>
									<asp:TextBox id="txtDenominacion" runat="server" Width="52px" Visible="False"></asp:TextBox>
									<asp:Label id="lblMarcaID" runat="server" Visible="False">lblMarcaID</asp:Label></DIV>
							</TD>
							<TD class="cell" style="WIDTH: 10%">
								<DIV align="center">
									<asp:Label id="lRegistro" runat="server"></asp:Label>
									<asp:Label id="lIdMarca" runat="server" Visible="False" Enabled="False"></asp:Label></DIV>
							</TD>
							<TD class="cell" style="WIDTH: 15%">
								<DIV align="center">
									<asp:Label id="LClaseAnt" runat="server"></asp:Label></DIV>
							</TD>
							<TD class="cell" style="WIDTH: 7.56%">
								<DIV align="center">
									<asp:Label id="LblTipo" runat="server"></asp:Label></DIV>
							</TD>
							<TD class="cell" style="WIDTH: 18%">
								<DIV align="center">
									<asp:Label id="LVencimiento" runat="server"></asp:Label></DIV>
							</TD>
						</TR>
					</TABLE>
<TABLE class="tbl" style="WIDTH: 100%" cellSpacing="0" cellPadding="0">
						<TR>
							<TD class="cell_header" style="WIDTH: 100%; HEIGHT: 13px">
								<DIV align="center">Descripción de la Clase</DIV>
							</TD>
						</TR>
						<TR>
							<TD class="cell" style="WIDTH: 1000%">
								<DIV align="center">
									<asp:TextBox id="txtDescripcion" runat="server" Width="100%" TextMode="MultiLine" Height="89px"></asp:TextBox></DIV>
							</TD>
						</TR>
					</TABLE>
<TABLE class="tbl" style="WIDTH: 100%" cellSpacing="0" cellPadding="0">
						<TR>
							<TD class="cell_header" style="WIDTH: 35.73%">
								<DIV align="center"><STRONG>Referencia</STRONG></DIV>
							</TD>
						</TR>
						<TR>
							<TD class="cell" style="WIDTH: 358.01%">
								<DIV align="center">
									<asp:TextBox id="txtRefMarca" runat="server" Width="100%" TextMode="MultiLine" Height="31px"></asp:TextBox></DIV>
							</TD>
						</TR>
					</TABLE>
<TABLE class="tbl" style="WIDTH: 100%" cellSpacing="0" cellPadding="0">
						<TR>
							<TD class="cell_header" style="WIDTH: 35.73%">
								<DIV align="center"><STRONG>Distribuidores</STRONG></DIV>
							</TD>
						</TR>
					</TABLE>
<TABLE class="tbl" style="WIDTH: 100%" cellSpacing="0" cellPadding="0">
						<TR>
							<TD class="cell_header" style="WIDTH: 330px" align="left" width="330"><STRONG>Producto/Servicio</STRONG></TD>
							<TD class="cell_header" style="WIDTH: 330px" align="left" width="330"><STRONG>Distribuidor</STRONG></TD>
							<TD class="cell_header" align="left"><STRONG>Acción</STRONG></TD>
						</TR>
						<TR>
							<TD class="cell" style="WIDTH: 330px" align="left" width="330">
								<asp:TextBox id="txtProducto_Servicio" runat="server" Width="317px" Columns="10" MaxLength="60"></asp:TextBox></TD>
							<TD class="cell" style="WIDTH: 330px" align="left" width="330">
								<ecCtrl:ecCombo id="eccDistribuidor" runat="server" ShowLabel="False" width="305px"></ecCtrl:ecCombo></TD>
							<TD class="cell" align="center">
								<asp:Button id="btnAsignarDistribuidores" runat="server" CssClass="Button" Text="Asignar" Font-Bold="True"></asp:Button></TD>
						</TR>
					</TABLE>
<asp:panel id="pnlDistribuidores" Runat="server" Width="100%" CssClass="infoMacro">
						<TABLE class="tbl" style="WIDTH: 100%" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="cell" style="WIDTH: 35.73%">
									<asp:DataGrid id="dgDistribuidores" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False"
										ShowHeader="False">
										<ItemStyle HorizontalAlign="Center" CssClass="cell"></ItemStyle>
										<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="Producto_Servicio" HeaderText="Producto/Servicio">
												<HeaderStyle HorizontalAlign="Left" Width="326px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="DistribuidorNombre" HeaderText="Distribuidor">
												<HeaderStyle HorizontalAlign="Left" Width="383px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:ButtonColumn Text="Eliminar" CommandName="Delete"></asp:ButtonColumn>
											<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="MarcaID" HeaderText="Id. Marca"></asp:BoundColumn>
										</Columns>
									</asp:DataGrid></TD>
							</TR>
						</TABLE>
					</asp:panel></asp:panel></P>
			<P><asp:panel id="pnlEditar" Width="100%" Runat="server" Height="119px" BorderColor="SteelBlue"
					BorderWidth="1px">&nbsp; 
<TABLE class="tbl" style="WIDTH: 100%" cellSpacing="0" cellPadding="0" border="0">
						<TR>
							<TD class="cell_header" style="WIDTH: 126px">
								<DIV align="center"><STRONG>Idioma</STRONG></DIV>
							</TD>
							<TD class="cell_header" style="WIDTH: 540px">
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
									<asp:button id="bAceptarlimitaciones" runat="server" Width="114px" CssClass="Button" Text="Aceptar"
										Font-Bold="True" Height="17px" Visible="True"></asp:button></DIV>
							</TD>
						</TR>
					</TABLE></asp:panel></P>
			<asp:panel id="pnlMarcasLimitaciones" Width="100%" Runat="server" Height="92px" BorderColor="SteelBlue"
				BorderWidth="1" BorderStyle="None">
				<TABLE id="tblMarcasLimitaciones" style="WIDTH: 100%; HEIGHT: 103px" cellSpacing="0" cellPadding="0"
					width="100%" border="0">
					<TR>
						<TD style="WIDTH: 100%; HEIGHT: 103px" width="100%">
							<asp:DataGrid id="dgMarcasLimitaciones" runat="server" Width="100%" CssClass="tbl" Height="59px"
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
			</asp:panel><asp:panel id="pnlMarcasBotones" Width="100%" Runat="server">
				<TABLE id="tblmarcasbotones" cellSpacing="0" cellPadding="0" width="98%" border="0">
					<TR>
						<TD style="WIDTH: 429px"></TD>
						<TD>
							<asp:button id="btnMarcasCancelar" runat="server" Width="114px" CssClass="Button" Text="Cancelar"
								Font-Bold="True" Height="17px" Visible="True"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
							&nbsp;&nbsp;
							<asp:button id="BntMarcasGrabar" runat="server" Width="114px" CssClass="Button" Text="Asignar Marca"
								Font-Bold="True" Height="17px" Visible="True" onclick="BntMarcasGrabar_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<P><asp:panel id="pnlMarcasRenovar" Width="100%" Runat="server" Height="128px" BorderColor="SteelBlue"
					BorderWidth="1" BorderStyle="None">
<TABLE id="Table5" style="MARGIN-LEFT: 5px; HEIGHT: 103px" cellSpacing="0" cellPadding="0"
						width="98%" border="0">
						<TR>
							<TD style="WIDTH: 100%; HEIGHT: 146px" width="100%">
								<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
									<TR>
										<TD width="1%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
										<TD class="titletable1" width="98%" bgColor="#7bb5e7">
											<asp:Label id="lTotalMarcas" runat="server" Font-Bold="True">Marcas a ser Registradas en Aduanas:</asp:Label></TD>
									</TR>
								</TABLE>
								<asp:DataGrid id="dgMarcasAsignadas" runat="server" Width="100%" CssClass="tbl" Height="73px"
									AutoGenerateColumns="False">
									<ItemStyle CssClass="cell"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="Denominacion" HeaderText="Denominaci&#243;n"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="DenominacionClave" HeaderText="Denominacion Clave"></asp:BoundColumn>
										<asp:BoundColumn DataField="RegistroNro" HeaderText="Registro"></asp:BoundColumn>
										<asp:BoundColumn DataField="ClaseAntDescrip" HeaderText="Clase"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="ClaseDescrip" HeaderText="Clase Nueva"></asp:BoundColumn>
										<asp:TemplateColumn Visible="False" HeaderText="Lim.">
											<ItemTemplate>
												<asp:CheckBox ID="chkLimitado" Runat="server" Enabled="False" Checked='<%# DataBinder.Eval(Container.DataItem, "Limitada")%>'>
												</asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="DesEspLim" HeaderText="Descripci&#243;n"></asp:BoundColumn>
										<asp:BoundColumn DataField="Vencimiento" HeaderText="Fec.Vto.Reg." DataFormatString="{0:d}"></asp:BoundColumn>
										<asp:BoundColumn DataField="Referencia" HeaderText="Referencia"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="LogotipoID" HeaderText="ID Logo"></asp:BoundColumn>
										<asp:ButtonColumn Text="Eliminar" HeaderText="Acci&#243;n" CommandName="Delete"></asp:ButtonColumn>
										<asp:ButtonColumn Text="Modificar" HeaderText="Acci&#243;n" CommandName="Update"></asp:ButtonColumn>
									</Columns>
								</asp:DataGrid></TD>
						</TR>
					</TABLE></TD></TR><TR>
						<TD style="WIDTH: 100%; HEIGHT: 25px" width="100%"></TD>
					</TR></TABLE></asp:panel></P>
			<asp:panel id="pnlBotones" Width="100%" Runat="server">
				<TABLE id="tblbotones" cellSpacing="0" cellPadding="0" width="98%" border="0">
					<TR>
						<TD style="WIDTH: 430px"></TD>
						<TD>
							<asp:button id="btnCancelar" runat="server" Width="114px" CssClass="Button" Text="Cancelar"
								Font-Bold="True" Height="17px" Visible="True"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
							&nbsp;&nbsp;
							<asp:button id="BntGrabar" runat="server" Width="114px" CssClass="Button" Text="Grabar" Font-Bold="True"
								Height="17px" Visible="True" onclick="BntGrabar_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
		<!--BEGIN Validacion para correspondencia--><uc1:formvalidator id="FormValidator1" runat="server" ButtonId="bCorrespondencia" Message="Verificar Datos">
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtReferenciaNro,txtReferenciaAnio"
				Message="Debe especificar el nro/a&ntilde;o de la correspondencia" />
		</uc1:formvalidator>
		<!--END Validacion para correspondencia-->
		<!--BEGIN Validacion para Seleccion de Marca--><uc1:formvalidator id="Formvalidator3" runat="server" ButtonId="btnAsignarMarcas" Message="Verificar Datos">
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtRegistroNro" Message="Debe especificar un Nro. de registro."
				ID="Fieldvalidator4" NAME="Fieldvalidator4" />
		</uc1:formvalidator>
		<!--END Validacion para Seleccion de Marca-->
		<!--BEGIN Validacion para Seleccion de Poder--><uc1:formvalidator id="Formvalidator4" runat="server" ButtonId="btnAsignarP" Message="Verificar Datos">
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtIDPoder" Message="Debe especificar Poder/Propietario."
				ID="Fieldvalidator5" NAME="Fieldvalidator4" />
		</uc1:formvalidator>
		<!--END Validacion para Seleccion de Poder-->
		<!--BEGIN Validacion para Grabar --><uc1:formvalidator id="Formvalidator2" runat="server" ButtonId="BntGrabar" Message="Verificar Datos">
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
