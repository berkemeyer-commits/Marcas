<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.DiarioConsulta" CodeFile="DiarioConsulta.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar DocumentoTipo</title>
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
		
   /-->
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo">Consultar Diarios </P>
			<!-- 

   Fin Cabecera

   /-->
			<P></P>
			<!---
------------------ PANEL RESULTADO ------------
--><asp:panel id="pnlResultado" runat="server" Visible="True">
				<P class="subtitulo">Resultado de la Búsqueda</P>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head" width="100%" align="center" border="0">
								<TR>

									<TD >
										<asp:label id="lblTituloGrid" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="dgResult" runat="server" CssClass="tbl" HorizontalAlign="Center" DataKeyField="ID" AutoGenerateColumns="False"
								 Width="100%">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="Descrip" HeaderText="Descripcion"></asp:BoundColumn>
									<asp:BoundColumn DataField="Abrev" HeaderText="Abrev">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!---
------------------ FIN Panel Resultado ------------
--></form>
	</body>
</HTML>
