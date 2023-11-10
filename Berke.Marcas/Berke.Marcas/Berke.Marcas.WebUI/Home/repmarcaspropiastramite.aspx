<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.RepMarcasPropiasTramite" CodeFile="RepMarcasPropiasTramite.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Reporte de Marcas Propias en Trámite</title>
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
			<P class="titulo">Marcas Propias en Trámite *</P>
			<asp:panel id="Panel1" runat="server" Width="98%"  CssClass="infoMacro">
				<TABLE id="Table1" style="WIDTH: 400px; HEIGHT: 86px" cellSpacing="1" cellPadding="1" width="400"
					border="0">
					<TR>
						<TD>
							<asp:Label id="Label1" runat="server" Height="16px" Width="64px">Desde</asp:Label></TD>
						<TD>
							<asp:TextBox id="txtDesde" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 22px">
							<asp:Label id="Label2" runat="server" Height="16px" Width="64px">Hasta</asp:Label></TD>
						<TD style="HEIGHT: 22px">
							<asp:TextBox id="txtHasta" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD></TD>
						<TD>
							<P>&nbsp;</P>
						</TD>
					</TR>
					<TR>
						<TD></TD>
						<TD>
							<P>
								<asp:Button id="BtnGenerar" runat="server" Height="24px" Width="120px" Text="Generar documento"
									CssClass="Button" onclick="BtnGenerar_Click"></asp:Button></P>
						</TD>
					</TR>
					<TR>
						<TD></TD>
						<TD></TD>
					</TR>
				</TABLE>
				<P>
					<asp:Label id="Label3" runat="server">* El rango debe corresponder a fechas de presentación.</asp:Label></P>
				
			</asp:panel><br>
			<p></p>
		</form>
	</body>
</HTML>
