<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Tools.Pages._Default" CodeFile="Message.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Signals</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../tools/css/globalstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body topMargin="0">
		<form id="Form1" method="post" runat="server">
			<p><uc1:header id="Header1" runat="server"></uc1:header>
				<TABLE id="Table5" cellSpacing="1" cellPadding="1" border="0">
					<TR>
						<TD width="12"></TD>
						<TD>
							<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="400" border="0">
								<TR>
									<TD class="h1" vAlign="bottom" height="32">Mensaje</TD>
								</TR>
								<TR>
									<TD class="h1" style="HEIGHT: 16px" vAlign="bottom" height="16"></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 17px" align="left"><asp:label id="lblMessage" runat="server">Su requerimiento no pudo ser procesado.<br><br>  Por favor reporte el problema a los Administradores del Sistema. <br><br>Para facilitar el diagnóstico, tenga  la gentileza de copiar la información que aparece a continuación</asp:label></TD>
								</TR>
								<TR>
									<TD align="right"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</p>
			<P>
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="1">
					<TR>
						<TD><asp:label id="lblServer" runat="server">*</asp:label></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblUser" runat="server">*</asp:label></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblMensaje" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblPage" runat="server">Page</asp:Label></TD>
					</TR>
				</TABLE>
				</FONT></P>
		</form>
	</body>
</HTML>
