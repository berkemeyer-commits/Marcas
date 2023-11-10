<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.RepMarcasTramites" CodeFile="RepMarcasTramites.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Reporte de Marcas en Trámite</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('BtnGenerar');">
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo">Marcas en trámite</P>
			<asp:panel id="Panel1" runat="server" Height="176px" Width="100%">
				<TABLE class="infoMacro" id="Table1" style="WIDTH: 400px; HEIGHT: 86px" cellSpacing="1"
					cellPadding="1" width="400" border="0">
					<TR>
						<TD style="WIDTH: 95px">
							<asp:Label id="Label1" runat="server" Height="16px" Width="96px" CssClass="Etiqueta2">Desde</asp:Label></TD>
						<TD>
							<asp:TextBox id="txtDesde" runat="server" Height="24px" Width="120px"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 95px; HEIGHT: 3px">
							<asp:Label id="Label2" runat="server" Height="16px" Width="96px" CssClass="Etiqueta2">Hasta</asp:Label></TD>
						<TD style="HEIGHT: 3px">
							<asp:TextBox id="txtHasta" runat="server" Height="24px" Width="120px"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 95px"></TD>
						<TD>
							<P>
								<asp:CheckBox id="chkDetalles" runat="server" Height="24px" Width="136px" Text="Mostrar trámites"
									Checked="True"></asp:CheckBox></P>
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 95px"><asp:Label id="Label5" runat="server">Límite </asp:Label></TD>
						<TD>
                            <asp:TextBox id="txtLimite" runat="server" Width="56px" Font-Size="XX-Small">1000</asp:TextBox>
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 95px"></TD>
						<TD>
							<asp:Button id="BtnGenerar" runat="server" Height="16px" Width="104px" Text="Generar listado"
								CssClass="Button" onclick="BtnGenerar_Click_1"></asp:Button></TD>
					</TR>
				</TABLE>
				<P>&nbsp;</P>
				<P>&nbsp;</P>
			</asp:panel><br>
			<p></p>
		</form>
	</body>
</HTML>
