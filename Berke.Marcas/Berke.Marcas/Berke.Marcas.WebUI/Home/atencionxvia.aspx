<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.AtencionXVia" CodeFile="AtencionXVia.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consulta de Clientes</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P>
				<asp:label id="lblMensaje" style="Z-INDEX: 104; LEFT: 24px; POSITION: absolute; TOP: 152px"
					runat="server" Width="584px" Font-Size="X-Small" Font-Bold="True"></asp:label>
				<uc1:header id="Header1" runat="server"></uc1:header></P>
			<P><STRONG><FONT face="Verdana" size="4"><asp:label id="Label1" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 112px" runat="server"
							Width="270px" Height="37px">Vias de la Atención</asp:label></FONT></STRONG></P>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 102; LEFT: 592px; POSITION: absolute; TOP: 1112px"
				runat="server" Width="32px"></asp:TextBox>
			<asp:Panel id="pnlAtencionXVia" style="Z-INDEX: 103; LEFT: 16px; POSITION: absolute; TOP: 176px"
				runat="server" Width="688px" Height="164px" Visible="False">
				<TABLE id="tblClienteXVia" style="WIDTH: 544px" cellSpacing="0" cellPadding="0" width="544"
					border="0">
					<TR>
						<TD style="WIDTH: 370px" width="370">
							<TABLE id="Table3" style="WIDTH: 545px; HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="545"
								align="center" border="0">
								<TR>
									<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
									<TD class="titletable1" width="98%" bgColor="#7bb5e7">Vias de Comunicación</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 369px" width="369">
							<asp:datagrid id="dgAtencionXVia" runat="server" Width="544px" CssClass="Grilla" AutoGenerateColumns="False"
								HorizontalAlign="Left">
								<ItemStyle CssClass="EstiloItem"></ItemStyle>
								<HeaderStyle CssClass="EstiloHeader"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="DescripcionVia" ReadOnly="True" HeaderText="Via">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ValorVia" ReadOnly="True" HeaderText="Valor">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle Width="20%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:Panel>
		</form>
	</body>
</HTML>
