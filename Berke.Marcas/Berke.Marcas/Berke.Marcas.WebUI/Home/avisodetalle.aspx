<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.AvisoDetalle" CodeFile="AvisoDetalle.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Aviso</title>
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
			<!-- 
   Cabecera 
   /--><BR>
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P><STRONG><FONT size="3">&nbsp;Detalle de Aviso</FONT> </STRONG>
			</P>
			<!-- 
   Fin Cabecera
   /--><asp:panel id="pnlDatos" runat="server">
				<asp:Label id="lblCabecera" runat="server">lblCabecera</asp:Label>
				<P></P>
				<asp:Label id="lblIndicaciones" runat="server">lblIndicaciones</asp:Label>
			</asp:panel>
			<p></p>
			<asp:panel id="Panel1" runat="server" Height="64px">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD style="HEIGHT: 15px" align="center" width="650"></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label2" runat="server" CssClass="Etiqueta2" Width="90px">Indicaciones</asp:Label>
							<asp:TextBox id="txtIndicaciones" runat="server" Width="472px"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 18px">
							<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="90px">Destinatario</asp:Label>
							<CUSTOM:DROPDOWN id="ddlDestinatario" runat="server" Width="153px" AutoPostBack="False"></CUSTOM:DROPDOWN>
							<asp:Label id="Label4" runat="server" CssClass="Etiqueta2" Width="208px">Concluido</asp:Label>
							<asp:CheckBox id="chkMarcarAtendido" runat="server"></asp:CheckBox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label3" runat="server" CssClass="Etiqueta2" Width="90px">Avisar</asp:Label>
							<asp:TextBox id="txtFechaAviso" runat="server" Width="176px"></asp:TextBox>
							<asp:Label id="Label5" runat="server" CssClass="Etiqueta2" Width="184px">Prioridad</asp:Label>
							<CUSTOM:DROPDOWN id="ddlPrioridad" runat="server" Width="153px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
					</TR>
				</TABLE>
				<TABLE>
					<TR>
						<TD style="WIDTH: 165px"></TD>
						<TD style="WIDTH: 119px; HEIGHT: 40px" align="center" width="119">
							<asp:Button id="btnDerivar" runat="server" Height="19px" CssClass="Button" Width="96px" Font-Bold="True"
								Text="Grabar" onclick="btnDerivar_Click"></asp:Button></TD>
						<TD style="WIDTH: 25px"></TD>
						<TD>
							<asp:Button id="btnVolver" runat="server" Height="19px" CssClass="Button" Width="96px" Font-Bold="True"
								Text="Volver"></asp:Button></TD>
					</TR>
				</TABLE>
			</asp:panel><asp:literal id="litPaginaAnterior" runat="server" Visible="False"></asp:literal></form>
	</body>
</HTML>
