<%@ Page language="c#" validateRequest="false" Inherits="Berke.Marcas.WebUI.Home.Limitaciones" CodeFile="Limitaciones.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Limitaciones</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:panel id="pnldgIdiomaClase" Runat="server" Visible="False" BorderStyle="Solid" BorderWidth="1"
				BorderColor="SteelBlue">
				<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="98%" border="0">
					<TR>
						<TD>
							<P>&nbsp;</P>
							<P>
								<asp:Label id="lblTitIdioma" Runat="server" Font-Names="Verdana" Font-Size="Medium" Font-Italic="True"></asp:Label></P>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
									<TD class="titletable1" width="98%" bgColor="#7bb5e7">Idiomas</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD align="center" width="15%" bgColor="lightgrey"><FONT size="1"><STRONG>Idioma</STRONG></FONT></TD>
									<TD align="center" width="75%" bgColor="lightgrey"><FONT size="1"><STRONG>Descripción</STRONG></FONT></TD>
									<TD align="center" width="15%" bgColor="lightgrey"><FONT size="1"><STRONG>Acciones</STRONG></FONT></TD>
								</TR>
								<TR>
									<TD align="center"><FONT face="Verdana" size="1">Español</FONT></TD>
									<TD>
										<asp:TextBox id="txtClaseDescrip" AutoPostBack=True runat="server" Width="504px" Rows="3" TextMode="MultiLine" ontextchanged="txtClaseDescrip_TextChanged"></asp:TextBox></TD>
									<TD>
										<P style="MARGIN-TOP: 3px">
											<asp:LinkButton id="lnkGuardar" Runat="server" CommandName="Guardar" text="Guardar" onclick="lnkGuardar_Click"></asp:LinkButton></P>
									</TD>
								</TR>
								<TR>
									<TD bgColor="lightgrey"><FONT size="1"></FONT></TD>
									<TD bgColor="lightgrey"></TD>
									<TD bgColor="lightgrey"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
									<TD class="titletable1" width="98%" bgColor="#7bb5e7">Idiomas</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:datagrid id="dgIdioma" runat="server" Visible="True" ShowFooter="True" DataKeyField="ExpedienteID"
								HorizontalAlign="Center" AutoGenerateColumns="False" Width="100%">
								<ItemStyle CssClass="EstiloItem"></ItemStyle>
								<HeaderStyle CssClass="EstiloHeader" HorizontalAlign="Center"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter" HorizontalAlign="Center"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Nro." ItemStyle-Width="15%">
										<ItemTemplate>
											<asp:label ID="ID" Runat=server Text='<%# DataBinder.Eval(Container.DataItem, "ExpedienteID")%>'>
											</asp:label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Idioma" ItemStyle-Width="20%">
										<ItemTemplate>
											Español
										</ItemTemplate>
										<EditItemTemplate>
											Español
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Descripción" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "ClaseDescripEsp")%>
										</ItemTemplate>
										<EditItemTemplate>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Acciones" ItemStyle-Width="15%"></asp:TemplateColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD align="center">
							<asp:Button id="btnAtras" Runat="server" Text="<Atrás" Font-Bold="True" onclick="btnAtras_Click"></asp:Button></TD>
					</TR>
				</TABLE>
			</asp:panel>
		</form>
	</body>
</HTML>
