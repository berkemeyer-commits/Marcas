<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.Login" CodeFile="login.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Login</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

        <LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">		
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
		
	</HEAD>
	<body>
		<form style="MARGIN-TOP:0px" id="Form1" method="post" runat="server">
			<TABLE style="MARGIN-TOP:0px" id="Table1" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD>
						<uc1:Header id="Header1" runat="server"></uc1:Header></TD>
				</TR>
			</TABLE>
				<p></p><asp:Label id="lblMenu" runat="server">Menu</asp:Label></p>
				<asp:HyperLink id="lnkNotif" style="Z-INDEX: 101; LEFT: 400px; POSITION: absolute; TOP: 48px" runat="server"
					Width="199px" BackColor="#FFFF80" NavigateUrl="../Home/AvisoConsulta.aspx">Tiene Notificaciones Pendientes</asp:HyperLink>
			
			<P align="center"><STRONG><EM><FONT size="7"></FONT></EM></STRONG></P>
			<P>&nbsp;</P>
            
		</form>
			
	</body>
</HTML>
