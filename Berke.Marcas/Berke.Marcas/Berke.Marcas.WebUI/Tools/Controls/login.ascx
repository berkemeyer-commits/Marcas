<%@ Control Language="c#" Inherits="Berke.Marcas.WebUI.Controls.Login" CodeFile="Login.ascx.cs" %>
<link href="../../tools/css/globalstyle.css" type="text/css" rel="stylesheet">
<div>
	<DIV>
		<TABLE id="Table1" cellSpacing="1" cellPadding="1" border="0">
			<TR>
				<TD align="right">Usuario</TD>
				<TD></TD>
				<TD>
					<asp:textbox id="txtAtmCardNumber" runat="server" enableviewstate="False" MaxLength="8" Columns="20"></asp:textbox>
					<asp:requiredfieldvalidator id="vldReqAtmCardNumber" runat="server" ControlToValidate="txtAtmCardNumber" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
			</TR>
			<TR>
				<TD align="right">Clave</TD>
				<TD></TD>
				<TD>
					<asp:textbox id="txtPin" runat="server" enableviewstate="False" MaxLength="8" Columns="20" TextMode="Password"
						CssClass="txtBox"></asp:textbox>
					<asp:requiredfieldvalidator id="vldReqPin" runat="server" ControlToValidate="txtPin" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
			</TR>
			<TR>
				<TD style="HEIGHT: 21px" align="right" colSpan="4">
					<asp:Button id="btnLogin" runat="server" Text="Ingresar" onclick="btnLogin_Click"></asp:Button></TD>
			</TR>
			<TR>
				<TD style="HEIGHT: 21px" align="center" colSpan="4">
					<asp:Label id="lblMessage" runat="server" Visible="False">Login incorrecto</asp:Label></TD>
			</TR>
		</TABLE>
	</DIV>
</div>
<div></div>
