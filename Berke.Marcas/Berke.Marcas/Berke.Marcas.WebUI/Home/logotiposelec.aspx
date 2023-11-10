<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.LogotipoSelec" CodeFile="LogotipoSelec.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LogotipoSelec</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
		<script language="JavaScript">
			function restablecer(valores){
				window.opener.document.Form1.tbLogotipoID.value = valores;
				window.opener.focus();
				window.close();
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P><FONT size="4"><STRONG>Seleccionar Logo</STRONG></FONT></P>
			<asp:panel id="pnlResultado" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 200px"
				runat="server" Height="232px" Visible="True">
				<P><FONT size="3"><STRONG>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="btnMarcar" runat="server" Height="22px" CssClass="Button" Text="Marcar/Desmarcar"
								Font-Bold="True" Width="120px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="btnCerrar" runat="server" Height="22px" CssClass="Button" Text="Cerrar" Font-Bold="True"
								Width="120px" onclick="btnCerrar_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="btnVerLogotipo" runat="server" Height="22px" CssClass="Button" Text="Ver Logo"
								Font-Bold="True" Width="120px" onclick="btnVerLogotipo_Click"></asp:button></STRONG></FONT></P>
				<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
									<TD class="titletable1" width="100%" bgColor="#7bb5e7">
										<asp:label id="lblTituloGrid" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="dgResult" runat="server" CssClass="Grilla" Width="100%" HorizontalAlign="Center"
								DataKeyField="ID" AutoGenerateColumns="False">
								<ItemStyle CssClass="EstiloItem"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="EstiloHeader"></HeaderStyle>
								<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:TemplateColumn FooterStyle-Width="5%" HeaderText="Sel.">
										<ItemTemplate>
											<asp:CheckBox id="cbSel" runat="server" Width="100%"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="ID" FooterStyle-Width="10%">
										<ItemTemplate>
											<asp:Label id="lbID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Denominacion" HeaderText="Nombre" FooterStyle-Width="40%"></asp:BoundColumn>
									<asp:BoundColumn DataField="FechaAlta" HeaderText="Fecha Alta" FooterStyle-Width="20%"></asp:BoundColumn>
									<asp:BoundColumn DataField="Nombre" HeaderText="Usuario" FooterStyle-Width="25%"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel><asp:panel id="pnlBuscar" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 32px" runat="server"
				Height="128px" Width="760px">
				<TABLE>
					<TR>
						<TD style="WIDTH: 374px" width="374">
							<TABLE width="100%">
								<TR>
									<TD style="WIDTH: 127px" align="right">
										<asp:Label id="lbDenominacion" runat="server" CssClass="Etiqueta2" Width="100%">ID</asp:Label></TD>
									<TD>
										<asp:TextBox id="tbID" runat="server"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 127px; HEIGHT: 22px" align="right">
										<asp:Label id="lbPropietario" runat="server" CssClass="Etiqueta2" Width="100%">Nombre</asp:Label></TD>
									<TD style="HEIGHT: 22px">
										<asp:TextBox id="tbDenominacion" runat="server" Width="232px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 127px" align="right">
										<asp:Label id="lbAgente" runat="server" CssClass="Etiqueta2" Width="100%">Fecha Alta</asp:Label></TD>
									<TD>
										<asp:TextBox id="tbFechaAlta" runat="server"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 127px" align="right">
										<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="100%">Usuario</asp:Label></TD>
									<TD>
										<asp:DropDownList id="ddlUsuario" runat="server" Width="176px"></asp:DropDownList></TD>
								</TR>
							</TABLE>
						</TD>
						<TD vAlign="top" width="50%">
							<asp:Label id="lbEtiqueta" runat="server" Font-Bold="True" Font-Size="Small">Vista previa del Logo</asp:Label><BR>
							<asp:Image id="ImgLogotipo" runat="server" Height="100px" Width="100px"></asp:Image></TD>
					</TR>
				</TABLE>
				<TABLE width="100%">
					<TR>
						<TD style="WIDTH: 178px" width="178"></TD>
						<TD>
							<asp:button id="btnBuscar" runat="server" Height="22px" CssClass="Button" Text="Buscar" Font-Bold="True"
								Width="80px" onclick="btnBuscar_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
						</TD>
					</TR>
				</TABLE>
				<asp:Label id="lbNoReg" runat="server" Font-Bold="True" Font-Size="X-Small">No existen registros que cumplan con la condición especificada</asp:Label>
			</asp:panel></form>
	</body>
</HTML>
