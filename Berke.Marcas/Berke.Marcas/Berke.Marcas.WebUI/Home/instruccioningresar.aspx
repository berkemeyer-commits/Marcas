<%@ Register TagPrefix="uc1" TagName="FieldValidator" Src="../Tools/Controls/FieldValidator.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FormValidator" Src="../Tools/Controls/FormValidator.ascx" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.InstruccionIngresar" CodeFile="InstruccionIngresar.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Carga de Instrucción</title>
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
			<asp:panel id="pnlCabecera" runat="server">
				<P>
					<uc1:header id="Header1" runat="server"></uc1:header></P>
				<P class="titulo">Ingresar Instrucciones de Clientes
				</P>
			</asp:panel>
			<!-- 
										Panel Instruccion
			--><asp:panel id="pnlInstruccion" runat="server">
				<P class="subtitulo">Correspondencia</P>
				<TABLE class="infoMacro" width="700">
					<TR>
						<TD>
							<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="95px">Corresp.Nro.</asp:Label>
							<asp:textbox id="txtNro" runat="server" Width="80px"></asp:textbox>&nbsp;Año
							<asp:textbox id="txtAnio" runat="server" Width="80px"></asp:textbox>&nbsp;&nbsp;
							<asp:Button id="btnCheckCorresp" runat="server" Height="17px" Text="Verificar" onclick="btnCheckCorresp_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:CheckBox id="chkNoCorresp" runat="server" Width="136px" Text=" Sin correspondencia" TextAlign="Left"></asp:CheckBox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label5" runat="server" CssClass="Etiqueta2" Width="72px"></asp:Label>
							<asp:Label id="lblCorresp" runat="server" CssClass="Texto" Width="472px"></asp:Label></TD>
					</TR>
				</TABLE>
				<P class="subtitulo">Instrucción</P>
				<TABLE class="infoMacro" width="700">
					<TR>
						<TD style="HEIGHT: 15px">
							<asp:Label id="lblInstruccion" runat="server" CssClass="Etiqueta2" Width="95px">Tipo</asp:Label>
							<CUSTOM:DROPDOWN id="ddlInstruccion" runat="server" Width="256px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label7" runat="server" CssClass="Etiqueta2" Width="96px" Height="53px">Observacion</asp:Label>
							<asp:TextBox id="txtObservacion" runat="server" Width="472px" Rows="3" TextMode="MultiLine"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<P class="subtitulo">Marcas Afectadas</P>
				<TABLE class="infoMacro" width="700">
					<TR>
						<TD>
							<asp:Label id="Label2" runat="server" CssClass="Etiqueta2" Width="96px" Height="7px">Registros</asp:Label>
							<asp:TextBox id="txtRegistros" runat="server" Width="472px"></asp:TextBox>&nbsp;&nbsp;
							<asp:Button id="btnCheckMarcas" runat="server" Height="17px" Text="Verificar" onclick="btnCheckMarcas_Click"></asp:Button></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label4" runat="server" CssClass="Etiqueta2" Width="96px" Height="7px">Actas</asp:Label>
							<asp:TextBox id="txtActas" runat="server" Width="424px"></asp:TextBox>&nbsp;&nbsp;
							<asp:Label id="Label6" runat="server" CssClass="Etiqueta2" Width="24px" Height="7px">Año</asp:Label>
							<asp:TextBox id="txtActaAnio" runat="server" Width="70px"></asp:TextBox>&nbsp;&nbsp;
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblMarcas" runat="server" CssClass="Etiqueta2" Width="632px" Height="7px"></asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblNotif" runat="server" CssClass="Etiqueta2" Width="96px" Height="7px">Notificar el </asp:Label>
							<asp:TextBox id="txtFechaNotif" runat="server" Width="88px"></asp:TextBox>
							<asp:CheckBox id="chkNotif" runat="server" Text="  " Checked="True"></asp:CheckBox>
							<asp:Label id="Label3" runat="server" CssClass="Etiqueta2" Width="120px" Height="7px">( Vencim. y Cotizac.) </asp:Label></TD>
					</TR>
				</TABLE>
				<P></P>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 485px; HEIGHT: 19px" align="right">&nbsp;
							<asp:button id="btnAceptar" runat="server" CssClass="Button" Width="96px" Height="22px" Text="Grabar"
								Font-Bold="True" onclick="btnAceptar_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
		<!--BEGIN Validacion para Grabar --><uc1:formvalidator id="Formvalidator2" runat="server" Message="Verificar Datos" ButtonId="btnAceptar">
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtNro,txtAnio" Message="Debe especificar el nro/a&ntilde;o de la correspondencia"
				Depends="chkNoCorresp" DependsType="haveNoValue" ID="Fieldvalidator1" NAME="Fieldvalidator1" />
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="ddlInstruccion" Message="Debe especificar el tipo de instrucci&oacute;n."
				ID="Fieldvalidator2" NAME="Fieldvalidator2" />
		</uc1:formvalidator>
		<!--END Validacion para Grabar --> 
		<!--BEGIN Validación para verificar marcas -->
		<uc1:formvalidator id="Formvalidator1" runat="server" ButtonId="btnCheckMarcas" Message="Verificar Datos">
			<uc1:FieldValidator runat="server" type="oneRequired" ControlToValidate="txtRegistros,txtActas" Message="Debe especificar acta/registro"
				Depends="txtActaAnio" DependsType="haveNoValue"  ID="Fieldvalidator3" NAME="Fieldvalidator1" />
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtActas,txtActaAnio" Message="Debe especificar acta/a&ntilde;o"
				Depends="txtActas" DependsType="haveValue" ID="Fieldvalidator6" NAME="Fieldvalidator1" />				
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtActas,txtActaAnio" Message="Debe especificar acta/a&ntilde;o"
				Depends="txtActaAnio" DependsType="haveValue" ID="Fieldvalidator8" NAME="Fieldvalidator1" />								
			<uc1:FieldValidator runat="server" type="noRequired" ControlToValidate="txtActas,txtActaAnio" Message="No puede ingresar actas y registros a la vez. Ingrese o actas o registros."
				Depends="txtRegistros" DependsType="haveValue" ID="Fieldvalidator7" NAME="Fieldvalidator1" />								
		</uc1:formvalidator>
		<!--END Validacion para verificar marcas -->
		<!--BEGIN Validacion para Verificar correspondencia -->
		<uc1:formvalidator id="Formvalidator3" runat="server" Message="Verificar Datos" ButtonId="btnCheckCorresp">
			<uc1:FieldValidator runat="server" type="required" ControlToValidate="txtNro,txtAnio" Message="Debe especificar el nro/a&ntilde;o de la correspondencia"
				Depends="chkNoCorresp" DependsType="haveNoValue" ID="Fieldvalidator4" NAME="Fieldvalidator1" />
			<uc1:FieldValidator runat="server" type="noRequired" ControlToValidate="chkNoCorresp" Message="Usted ha seleccionado la opci&oacute;n SIN CORRESPONDENCIA"
				ID="Fieldvalidator5" NAME="Fieldvalidator1" />				
		</uc1:formvalidator>	
		<!--END Validacion para Verificar correspondencia --> 		
	</body>
</HTML>
