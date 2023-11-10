<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.RepOrdenPublicacion" CodeFile="RepOrdenPublicacion.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RepOrdenPublicacion</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('btnGenerar');">
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<p class="titulo">Reporte de Órdenes de Publicación</p>
			<br>
			<table style="WIDTH: 424px; HEIGHT: 18px">
				<tr>
					<td>
						<asp:label id="lblMensaje" runat="server" Width="396px" CssClass="msg" ForeColor="#C00000"
							Font-Bold="True"></asp:label></td>
				</tr>
			</table>
			<table class="infoMacro" style="WIDTH: 288px; HEIGHT: 114px">
				<tr>
					<td style="WIDTH: 102px"><asp:label id="Label1" runat="server" CssClass="Etiqueta2" Width="88px">Fecha de la orden</asp:label></td>
					<td><asp:textbox id="txtFecha" runat="server" Width="152px"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WIDTH: 102px"><asp:label id="Label2" runat="server" CssClass="Etiqueta2" Width="88px">Trámite</asp:label></td>
					<td><asp:radiobuttonlist id="rbTramite" runat="server" Width="152px" RepeatDirection="Horizontal" BorderStyle="Solid"
							BorderWidth="1px" BorderColor="White">
							<asp:ListItem Value="REG,REN" Selected="True">Registro/Renovaci&#243;n</asp:ListItem>
							<asp:ListItem Value="TRA">Transferencia</asp:ListItem>
							<asp:ListItem Value="LIC">Licencia</asp:ListItem>
						</asp:radiobuttonlist></td>
				</tr>
				<TR>
					<TD style="WIDTH: 102px"><asp:label id="Label3" runat="server" CssClass="Etiqueta2" Width="88px">Reporte</asp:label></TD>
					<TD><asp:radiobuttonlist id="rbTipoListado" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="0" Selected="True">Listado</asp:ListItem>
							<asp:ListItem Value="1">Ampliado</asp:ListItem>
						</asp:radiobuttonlist></TD>
				</TR>
			</table>
			<table style="MARGIN-LEFT: 5px; WIDTH: 288px; HEIGHT: 25px">
				<tr>
					<td style="WIDTH: 88px"></td>
					<TD><asp:button id="btnGenerar" runat="server" CssClass="Button" Text="Generar" onclick="btnGenerar_Click"></asp:button></TD>
				</tr>
			</table>
		</form>
	</body>
</HTML>
