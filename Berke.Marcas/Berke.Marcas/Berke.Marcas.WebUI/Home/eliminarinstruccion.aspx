<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.EliminarInstruccion" CodeFile="EliminarInstruccion.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="uc" TagName="ExpeDatos" Src="ExpeDatos.ascx" %>
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
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('txtBuscar');">
			<asp:panel id="pnlCabecera" runat="server">
				<P>
					<uc1:header id="Header1" runat="server"></uc1:header></P>
				<P class="titulo">Eliminar Instrucciones</P>
			</asp:panel>
			<p></p>
			<asp:panel id="pnlBuscar" runat="server" CssClass="infoMacro" Width="98%">
				<P class="subtitulo">Ubicar Expediente</P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR width="383">
						<TD style="WIDTH: 344px">
							<asp:Label id="Label1" runat="server" Width="95px" CssClass="Etiqueta2">Registro</asp:Label>
							<asp:textbox id="txtRegistroNro" runat="server" Width="75px"></asp:textbox></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 344px; HEIGHT: 23px" width="344">
							<asp:Label id="Label3" runat="server" Width="95px" CssClass="Etiqueta2">Acta / Año</asp:Label>
							<asp:TextBox id="txtActaNro" runat="server" Width="75px"></asp:TextBox>&nbsp;/
							<asp:textbox id="txtActaAnio" runat="server" Width="75px"></asp:textbox></TD>
						<TD></TD>
					<TR>
						<TD style="WIDTH: 344px; HEIGHT: 19px" align="right">
							<asp:button id="txtBuscar" runat="server" Width="96px" CssClass="Button" Font-Bold="True" Text="Buscar"
								Height="22px" onclick="txtBuscar_Click"></asp:button></TD>
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
							<asp:Panel id="Panel1" runat="server" Width="616px" Height="168px" BorderColor="Transparent"
								BorderStyle="Outset" Wrap="False">
								<P class="subtitulo">Datos del Expediente</P>
								<P>
									<asp:Label id="lblSeparador1" runat="server" Width="60px" CssClass="Etiqueta2"></asp:Label>
									<asp:Label id="lblExpeDescrip" runat="server" Width="440px" CssClass="Texto">lblExpeDescrip</asp:Label></P>
								<P>
									<asp:Label id="Label2" runat="server" Width="60px" CssClass="Etiqueta2"></asp:Label>
									<asp:Label id="lblMarcaDescrip" runat="server" Width="448px" CssClass="Texto" Height="8px">lblMarcaDescrip</asp:Label></P>
								<P></P>
								<asp:Label id="lblInstrucciones" runat="server" Width="592px" CssClass="Texto" Height="8px">lblInstrucciones</asp:Label>
								<BR>
								<asp:Label id="Label8" runat="server" Width="400px" CssClass="Etiqueta2"></asp:Label>
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
			-->
			<P></P>
		</form>
		</TD></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
