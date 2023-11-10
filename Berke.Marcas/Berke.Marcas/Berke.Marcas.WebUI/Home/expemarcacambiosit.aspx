<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#"  smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.ExpeMarcaCambioSit" CodeFile="ExpeMarcaCambioSit.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ExpeMarcaCambioSit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/globalstyle.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssMainStyle.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssDgEstiloGeneral.css">
	</HEAD>
	<body>
		<form id="Form1" onkeypress="form_onEnter('btnBuscar');" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header></TD><asp:panel id="pnlCabecera" runat="server" Width="780px">
				<P>&nbsp;</P>
				<P class="titulo">Ingresar Cambios de Situación</P>
				<TABLE style="WIDTH: 744px; HEIGHT: 48px" class="infoMacro">
					<TR>
						<TD align="right">Tramite:
						</TD>
						<TD>
							<CUSTOM:DROPDOWN id="ddlTramite" runat="server" Width="280px" AutoPostBack="true"></CUSTOM:DROPDOWN></TD>
                    </TR>
					<TR>
						<TD>
							<P class="Etiqueta2">Pasar a Situación:</P>
						</TD>
						<TD>
							<CUSTOM:DROPDOWN id="ddlTramiteSitDestino" runat="server" Width="280px" AutoPostBack="true"></CUSTOM:DROPDOWN>
							<CUSTOM:DROPDOWN id="ddlSituacionDestino" runat="server" Width="280px" AutoPostBack="true"></CUSTOM:DROPDOWN>
						</TD>
						<TD>
							<P class="Etiqueta2">&nbsp;
								<asp:Label id="lblPlazo" runat="server">lblPlazo</asp:Label></P>
						</TD>
					</TR>
				</TABLE>
				<%--<P>--%>
					<TABLE style="WIDTH: 744px; HEIGHT: 52px" class="infoMacro">
						<TR>
							<TD style="WIDTH: 102px">
								<P>
									<asp:DropDownList id="ddlIdentificador" runat="server" Width="104px">
										<asp:ListItem Value="Acta">Acta</asp:ListItem>
										<asp:ListItem Value="RegVig">Registro Vigente</asp:ListItem>
										<asp:ListItem Value="Reg">Registro</asp:ListItem>
										<asp:ListItem Value="HI">Hoja Inicio</asp:ListItem>
										<asp:ListItem Value="ExpeID">Expediente ID</asp:ListItem>
										<asp:ListItem Value="MarcaID">Marca ID</asp:ListItem>
									</asp:DropDownList></P>
							</TD>
							<TD>
								<P>
									<asp:TextBox id="txtIdentificadores" runat="server" Width="584px"></asp:TextBox></P>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 102px">
								<P class="Etiqueta2">Año
								</P>
							</TD>
							<TD>
								<P>
									<asp:TextBox id="txtAnio" runat="server" Width="80px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Button id="btnBuscar" runat="server" Width="96px" Text="Buscar" onclick="btnBuscar_Click"></asp:Button></P>
							</TD>
						</TR>
					</TABLE>
				<%--</P>--%>
			</asp:panel><asp:panel id="pnlGrillas" runat="server">
				<asp:panel id="pnlReplicar" runat="server">
					<TABLE style="WIDTH: 742px; HEIGHT: 28px" class="infoMacro">
						<TR>
							<TD style="WIDTH: 260px" vAlign="bottom"><FONT size="2"><STRONG>Resultados</STRONG></FONT></TD>
							<TD>
								<asp:DropDownList id="ddlReplicar" runat="server" Width="104px" AutoPostBack="True" onselectedindexchanged="ddlReplicar_SelectedIndexChanged">
									<asp:ListItem Value="txtFecha">Fecha </asp:ListItem>
								</asp:DropDownList>=
								<asp:TextBox id="txtReplicar" runat="server" Width="110px"></asp:TextBox>
								<asp:Button id="btnReplicar" runat="server" Text="<-Replicar" onclick="btnReplicar_Click"></asp:Button></TD>
							<TD>
								<asp:Button id="btnCalcVencim" runat="server" Text="Ver. Vto." onclick="btnCalcVencim_Click"></asp:Button></TD>
						</TR>
					</TABLE>
				</asp:panel>
				<TABLE>
					<TR>
						<TD>
							<asp:datagrid id="dgResult" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
								CssClass="Grilla">
								<ItemStyle CssClass="EstiloItem"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="EstiloHeader"></HeaderStyle>
								<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Sel.">
										<ItemTemplate>
											<asp:CheckBox ID="chkSel" runat="server" Checked="False" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Exp.ID">
										<ItemTemplate>
											<asp:label ID="lblExpeID" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Denominacion">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:label ID="lblDenominacion" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Clase">
										<ItemTemplate>
											<asp:label ID="lblClase" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tr&#225;m.">
										<ItemTemplate>
											<asp:label ID="lblTramiteAbrev" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Registro">
										<ItemTemplate>
											<asp:label ID="lblRegistro" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Vencim.">
										<ItemTemplate>
											<asp:label id="lblVencimTitulo" runat="server"></asp:label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="ActaMarca">
										<ItemTemplate>
											<asp:label ID="lblActaMarca" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Acta">
										<ItemTemplate>
											<asp:label ID="lblActa" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Fecha">
										<ItemTemplate>
											<asp:textbox ID="txtFecha" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Fin Plazo">
										<ItemTemplate>
											<asp:textbox ID="txtFinPlazo" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Bib/Exp">
										<ItemTemplate>
											<asp:textbox ID="txtBibExp" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Registro">
										<ItemTemplate>
											<asp:textbox ID="txtRegistro" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Acta">
										<ItemTemplate>
											<asp:textbox ID="txtActa" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Venc.Titulo">
										<ItemTemplate>
											<asp:textbox ID="txtVencimTitulo" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pag/A&#241;o">
										<ItemTemplate>
											<asp:textbox ID="txtPagAnio" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Diario">
										<ItemTemplate>
											<asp:textbox ID="txtDiario" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ag.Local">
										<ItemTemplate>
											<asp:textbox ID="txtAgLocal" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Observaci&#243;n">
										<ItemTemplate>
											<asp:textbox ID="txtObs" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Error">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:label ID="lblError" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="TrmSitID">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:label ID="lblTramiteSitID" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel><asp:panel id="pnlBotones" runat="server">
				<P>
					<TABLE style="WIDTH: 712px; HEIGHT: 25px">
						<TR>
							<TD align="center">
								<asp:Button id="btnGrabar" runat="server" Width="248px" Text="Grabar" Height="32px" onclick="btnGrabar_Click"></asp:Button></TD>
						</TR>
					</TABLE>
				</P>
			</asp:panel><asp:panel id="pnlProceso" runat="server">
				<asp:Label id="Label1" runat="server" CssClass="subtitulo">Resumen del Proceso</asp:Label>
				<asp:datagrid id="dgProceso" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
					CssClass="Grilla" Visible="False">
					<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
					<ItemStyle CssClass="EstiloItem"></ItemStyle>
					<HeaderStyle HorizontalAlign="Center" CssClass="EstiloHeader"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Proc.">
							<ItemTemplate>
								<asp:CheckBox ID="chkpSel" runat="server" Checked="False" Enabled="false" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Exp.ID">
							<ItemTemplate>
								<asp:label ID="lblpExpeID" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Denominacion">
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
							<ItemTemplate>
								<asp:label ID="lblpDenominacion" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Clase">
							<ItemTemplate>
								<asp:label ID="lblpClase" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Tr&#225;m.">
							<ItemTemplate>
								<asp:label ID="lblpTramiteAbrev" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Fec. Sit.">
							<ItemTemplate>
								<asp:label id="lblpFecSit" runat="server"></asp:label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Venc. Sit.">
							<ItemTemplate>
								<asp:label id="lblpVencSit" runat="server"></asp:label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Registro">
							<ItemTemplate>
								<asp:label ID="lblpRegistro" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Vencim.">
							<ItemTemplate>
								<asp:label id="lblpVencimTitulo" runat="server"></asp:label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Acta">
							<ItemTemplate>
								<asp:label ID="lblpActa" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Error">
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
							<ItemTemplate>
								<asp:label ID="lblpError" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:datagrid>
			</asp:panel></form>
		<P></P>
	</body>
</HTML>
