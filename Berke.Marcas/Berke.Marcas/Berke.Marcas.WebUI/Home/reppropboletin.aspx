<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.RepPropBoletin" CodeFile="RepPropBoletin.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RepPropBoletin</title>
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
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<table cellPadding="5">
				<tr>
					<td>
						<P><asp:label id="lblTitulo" runat="server" CssClass="Titulo"> Verificar Nuestras desde Bolet&iacute;n </asp:label>&nbsp;&nbsp;&nbsp;&nbsp;</P>
						<br>
						<table width="400">
							<tr>
								<td style="WIDTH: 122px"><asp:dropdownlist id="ddlTipoFiltro" runat="server" Width="88px">
										<asp:ListItem Value="0" Selected="True">Boletin</asp:ListItem>
										<asp:ListItem Value="1">Carpeta</asp:ListItem>
									</asp:dropdownlist></td>
								<td><asp:textbox id="txtCarpetaNro" runat="server" Width="97px"></asp:textbox>&nbsp;
									<asp:label id="Label3" runat="server">Año</asp:label><asp:textbox id="txtCarpetaAnio" runat="server" Width="95px"></asp:textbox></td>
							</tr>
							<tr>
								<td style="WIDTH: 122px"><asp:label id="Label2" runat="server" Width="88px">Rango de actas</asp:label></td>
								<td><asp:textbox id="txtActaDe" runat="server" Width="61px"></asp:textbox>-
									<asp:textbox id="txtActaHasta" runat="server" Width="61px"></asp:textbox>&nbsp;
									<asp:label id="Label4" runat="server">Año</asp:label><asp:textbox id="txtActaAnio" runat="server" Width="64px"></asp:textbox></td>
							</tr>
							<TR>
								<TD style="WIDTH: 122px; HEIGHT: 33px"><asp:label id="Label1" runat="server" Width="128px" Font-Bold="True" ForeColor="#0000C0">Nivel de sensibilidad para denominaciones</asp:label></TD>
								<TD style="HEIGHT: 33px"><asp:radiobuttonlist id="rbAproximacion" runat="server" RepeatDirection="Horizontal" Height="40px">
										<asp:ListItem Value="-1">Exacto</asp:ListItem>
										<asp:ListItem Value="20">Alto</asp:ListItem>
										<asp:ListItem Value="15" Selected="True">Medio</asp:ListItem>
										<asp:ListItem Value="10">Bajo</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<tr>
								<td style="WIDTH: 122px"></td>
								<td><br>
									<asp:button id="btnVerificar" runat="server" CssClass="Button" Text="Verificar" onclick="btnVerificar_Click"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
