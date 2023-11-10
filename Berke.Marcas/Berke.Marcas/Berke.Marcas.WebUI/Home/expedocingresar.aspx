<%@ Register TagPrefix="uc" TagName="ExpeDatos" Src="ExpeDatos.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.ExpeDocIngresar" CodeFile="ExpeDocIngresar.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
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
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:panel id="pnlCabecera" runat="server">
				<P>
					<uc1:header id="Header1" runat="server"></uc1:header></P>
				<P><STRONG><FONT size="4">&nbsp;Ingresar Instrucciones</FONT></STRONG>
				</P>
			</asp:panel>
			<p></p>
			<asp:panel id="pnlBuscar" runat="server">
				<P><STRONG><FONT size="2"><U>Ubicar Expediente</U></FONT></STRONG></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR width="383">
						<TD style="WIDTH: 344px">
							<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="95px">Registro</asp:Label>
							<asp:textbox id="txtRegistroNro" runat="server" Width="75px"></asp:textbox></TD>
						<TD>
							<asp:Label id="Label4" runat="server" CssClass="Etiqueta2" Width="95px">Exped. ID</asp:Label>
							<asp:textbox id="txtExpeID" runat="server" Width="75px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 344px; HEIGHT: 23px" width="344">
							<asp:Label id="Label3" runat="server" CssClass="Etiqueta2" Width="95px">Acta / Año</asp:Label>
							<asp:TextBox id="txtActaNro" runat="server" Width="75px"></asp:TextBox>&nbsp;/
							<asp:textbox id="txtActaAnio" runat="server" Width="75px"></asp:textbox></TD>
						<TD>
							<asp:Label id="Label6" runat="server" CssClass="Etiqueta2" Width="95px">Marca ID</asp:Label>
							<asp:textbox id="txtMarcaID" runat="server" Width="75px"></asp:textbox></TD>
					<TR>
						<TD style="WIDTH: 344px; HEIGHT: 19px" align="right">
							<asp:button id="txtBuscar" runat="server" CssClass="Button" Width="96px" Height="22px" Text="Buscar"
								Font-Bold="True" onclick="txtBuscar_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD colSpan="7">
							<asp:label id="lblMensaje" runat="server" Font-Bold="True" Font-Size="X-Small"></asp:label></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!-- 
										Panel  Datos de Expediente
			--><asp:panel id="pnlExpeDatos" runat="server">
				<TABLE id="tblDatosExpediente" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 485px; HEIGHT: 49px" width="485">
							<asp:Panel id="Panel1" runat="server" Width="616px" Height="168px" Wrap="False" BorderStyle="Outset"
								BorderColor="Transparent">
								<P><STRONG><FONT size="2"><U>Datos del Expediente</U></FONT></STRONG></P>
								<P>
									<asp:Label id="lblSeparador1" runat="server" CssClass="Etiqueta2" Width="60px"></asp:Label>
									<asp:Label id="lblExpeDescrip" runat="server" CssClass="Texto" Width="440px">lblExpeDescrip</asp:Label></P>
								<P>
									<asp:Label id="Label2" runat="server" CssClass="Etiqueta2" Width="60px"></asp:Label>
									<asp:Label id="lblMarcaDescrip" runat="server" CssClass="Texto" Width="448px" Height="8px">lblMarcaDescrip</asp:Label></P>
								<P></P>
								<asp:Label id="lblInstrucciones" runat="server" CssClass="Texto" Width="592px" Height="8px">lblInstrucciones</asp:Label>
								<BR>
								<asp:Label id="Label8" runat="server" CssClass="Etiqueta2" Width="400px"></asp:Label>
								<asp:LinkButton id="btnEliminarInstruccion" runat="server" Width="152px" onclick="btnEliminarInstruccion_Click_1">Eliminar última Instrucción</asp:LinkButton>
								<asp:CheckBox id="chkDeleteInstruc" runat="server" Text="."></asp:CheckBox>
								<P></P>
								<P></P>
							</asp:Panel></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!-- 
										Panel Instruccion
			--><asp:panel id="pnlInstruccion" runat="server">
				<P><STRONG><FONT size="2"><U>INSTRUCCION</U></FONT></STRONG></P>
				<TABLE>
					<TR>
						<TD>
							<asp:Label id="lblInstruccion" runat="server" CssClass="Etiqueta2" Width="70px">Tipo</asp:Label>
							<CUSTOM:DROPDOWN id="ddlInstruccion" runat="server" Width="210px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label7" runat="server" CssClass="Etiqueta2" Width="70px" Height="50px">Observación</asp:Label>
							<asp:TextBox id="txtObservacion" runat="server" Width="488px" Font-Size="X-Small" Rows="3" TextMode="MultiLine"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<TABLE id="tblInstruccion" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="HEIGHT: 21px">
							<asp:Label id="lblCorrespondencia" runat="server" CssClass="Etiqueta2" Width="70px">Corresp.Nro.</asp:Label>
							<asp:textbox id="txtNro" runat="server" Width="80px"></asp:textbox>&nbsp;Año
							<asp:textbox id="txtAnio" runat="server" Width="80px"></asp:textbox>&nbsp;&nbsp;
							<asp:Button id="btnCheckCorresp" runat="server" Height="17px" Text="Verificar" onclick="btnCheckCorresp_Click"></asp:Button></TD>
					</TR>
					</TR></TABLE>
				<asp:panel id="pnlCorrespResult" runat="server">
					<TABLE id="tblCorresp" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<TR>
							<TD style="WIDTH: 485px; HEIGHT: 49px" width="485">
								<asp:Panel id="Panel2" runat="server" Width="568px" Height="92px" Wrap="False" BorderStyle="Outset"
									BorderColor="Transparent">
									<P><STRONG><FONT size="2"><U>Datos de la Correspondencia</U></FONT></STRONG></P>
									<P>
										<asp:Label id="Label5" runat="server" CssClass="Etiqueta2" Width="72px"></asp:Label>
										<asp:Label id="lblCorresp" runat="server" CssClass="Texto" Width="472px"></asp:Label></P>
								</asp:Panel></TD>
						</TR>
					</TABLE>
				</asp:panel>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 485px; HEIGHT: 19px" align="right">
							<asp:button id="btnAceptar" runat="server" CssClass="Button" Width="96px" Height="22px" Text="Aceptar"
								Font-Bold="True" onclick="btnAceptar_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<P></P>
		</form>
		</TD></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
