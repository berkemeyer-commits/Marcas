<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.TramitesVariosDetalle" CodeFile="TramitesVariosDetalle.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Trámites Varios</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/ssDgPopUp.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssPopUp.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="100%" height="25"><uc1:header id="Header1" runat="server"></uc1:header></td>
				</tr>
			</table>
			<br>
			<br>
			<!-- 
			================================================================================
			TITULO PRINCIPAL DEL TRAMITE VARIO
			================================================================================
			
			<table style="MARGIN-TOP: 2pt" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="100%" bgColor="#6699cc" height="25">
						<p class="Titulo" style="COLOR: #ffffff">Trámites Varios -
							</p>
					</td>
				</tr>
			</table>
			-->
			<span class="Titulo">Trámites Varios -
				<asp:label id="lblTV" Runat="server"></asp:label></span><br>
			<span class="subtitulo">
				<asp:label id="lblHI" runat="server"></asp:label></span><br>
			<span class="subtitulo">
				<asp:label id="lblUser" runat="server"></asp:label></span> 
			<!--
			================================================================================
			PANEL CLIENTE - SELECCION Y CARGA DE DATOS PARTICULARES
			================================================================================
			--><asp:panel id="pnlCliente" Runat="server" Width="98%" Visible="True" Enabled="True" HorizontalAlign="Center">
				<TABLE class="tbl" style="MARGIN-LEFT: 5px" cellSpacing="0" cellPadding="5" width="100%"
					border="0">
					<TR>
						<TH class="cell_vert" width="20%">
							Funcionario</TH>
						<TD class="cell_vert" width="*">
							<asp:label id="lFuncionario" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Fecha de Alta</TH>
						<TD class="cell_vert">
							<asp:label id="lfecha" runat="server"></asp:label></TD>
					</TR>
				</TABLE>
				<BR>
				<TABLE class="tbl" style="MARGIN-LEFT: 5px" cellSpacing="0" cellPadding="5" width="100%"
					align="center">
					<TR>
						<TH class="cell_vert" width="20%">
							Cliente</TH>
						<TD class="cell_vert" width="80%">
							<asp:Label id="txtCliente" runat="server" CssClass="EtiqSubTitulo"></asp:Label>(
							<asp:Label id="lCodCliente" runat="server" CssClass="EtiqSubTitulo"></asp:Label>)
						</TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Atención</TH>
						<TD class="cell_vert">
							<asp:Label id="lbAtencion" runat="server" CssClass="EtiqSubTitulo"></asp:Label></TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Corresp. Nº</TH>
						<TD class="cell_vert">
							<asp:Label id="txtCorrespNro" runat="server" CssClass="EtiqSubTitulo"></asp:Label>&nbsp;/&nbsp;
							<asp:Label id="txtCorrespAnio" runat="server" CssClass="EtiqSubTitulo"></asp:Label></TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Ref. Corresp
						</TH>
						<TD class="cell_vert">
							<asp:Label id="txtRefCorresp" runat="server" CssClass="EtiqSubTitulo"></asp:Label></TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Facturable</TH>
						<TD class="cell_vert">
							<asp:checkbox id="chkFacturable" Runat="server" Enabled="False" CssClass="EtiqSubTitulo" Checked="True"></asp:checkbox></TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							<asp:Label id="lbDerechoPropio" runat="server">Por Derecho Propio:</asp:Label></TH>
						<TD class="cell_vert">
							<asp:checkbox id="cbDerechoPropio" Runat="server" Enabled="False" Checked="True"></asp:checkbox></TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Observaciones</TH>
						<TD class="cell_vert">
							<asp:Label id="txtObsClientes" runat="server" CssClass="EtiqSubTitulo"></asp:Label></TD>
					</TR>
				</TABLE>
				<BR>
				<TABLE class="tbl" style="MARGIN-LEFT: 5px" cellSpacing="0" cellPadding="5" width="100%"
					align="center">
					<TR>
						<TH class="cell_vert" width="20%">
							Instrucción Poder</TH>
						<TD class="cell_vert">
							<asp:Label id="lbInstruccionPoder" runat="server" CssClass="EtiqSubTitulo"></asp:Label></TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Instrucción Contabilidad</TH>
						<TD class="cell_vert">
							<asp:Label id="lbInstruccionContabilidad" runat="server" CssClass="EtiqSubTitulo"></asp:Label></TD>
					</TR>
					<TR>
						<TH class="cell_vert">
							Referencia del Cliente</TH>
						<TD class="cell_vert">
							<asp:Label id="lbReferenciaCliente" runat="server" CssClass="EtiqSubTitulo"></asp:Label></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<br>
			<!--
			================================================================================ 
			PANEL ASIGNAR MARCAS - MUESTRA LAS MARCAS EN UNA GRILLA 
			================================================================================
			--><asp:panel id="pnlAsignarMarcas" Runat="server" Visible="True" Enabled="True" Width="98%" HorizontalAlign="Left">
				<asp:Panel id="pnlMostrarMarcas" Runat="server" Enabled="True" Visible="True">
					<TABLE class="grid_head" style="MARGIN-LEFT: 5px; WIDTH: 99%">
						<TR>
							<TD>Marcas Asignadas</TD>
						</TR>
					</TABLE>
					<TABLE cellPadding="4" width="100%" border="0">
						<TR>
							<TD>
								<asp:DataGrid id="dgMarcasAsignadas" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False" onselectedindexchanged="dgMarcasAsignadas_SelectedIndexChanged">
									<ItemStyle CssClass="cell"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="Denominacion" HeaderText="Denominaci&#243;n">
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="ClaseAntDescrip" HeaderText="Clase"></asp:BoundColumn>
										<asp:BoundColumn DataField="RegistroNro" HeaderText="Registro"></asp:BoundColumn>
										<asp:BoundColumn DataField="ActaNro" HeaderText="Acta"></asp:BoundColumn>
										<asp:BoundColumn DataField="ActaAnio" HeaderText="A&#241;o"></asp:BoundColumn>
										<asp:BoundColumn DataField="ConcesionFecha" HeaderText="Concesi&#243;n" DataFormatString="{0:d}"></asp:BoundColumn>
										<asp:BoundColumn DataField="Vencimiento" HeaderText="Fec.Vto.Reg." DataFormatString="{0:d}"></asp:BoundColumn>
										<asp:TemplateColumn>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:Label id=Label1 runat="server" Text='<%# String.Format( @"<A href=""HDescTramVarios.aspx?ExpedienteId={0}&amp;TipoTrabajoID={1}&amp;HI_Nro={2}&amp;HI_Anho={3}"">Ver Hoja Descriptiva</A>", DataBinder.Eval(Container.DataItem, "ExpedienteId"), DataBinder.Eval(Container.DataItem, "TipoTrabajoID"),&#13;&#10;DataBinder.Eval(Container.DataItem, "HI_Nro"), DataBinder.Eval(Container.DataItem, "HI_Anho")) %>'>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
								</asp:DataGrid></TD>
						</TR>
					</TABLE>
				</asp:Panel>
			</asp:panel>
			<!--
			================================================================================
			PANEL LOGICA DE NEGOCIOS - SELECCION DEL PODER
			================================================================================
			--><asp:panel id="pnlLogicaTV" Runat="server" Visible="True" Enabled="True" Width="98%">
				<asp:Panel id="pnlAnterior" Runat="server" Enabled="True" Visible="True">
					<TABLE class="grid_head" style="MARGIN-LEFT: 5px; WIDTH: 99%">
						<TR>
							<TD>Propietarios Anteriores</TD>
						</TR>
					</TABLE>
					<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
						<TR>
							<TD>
								<asp:DataGrid id="dgPoderAnterior" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False">
									<ItemStyle CssClass="cell"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
									<Columns>
										<asp:BoundColumn HeaderText="Propietario" DataField="Denominacion" ItemStyle-Width="14%"></asp:BoundColumn>
										<asp:BoundColumn HeaderText="Domicilio" DataField="Domicilio" ItemStyle-Width="13%"></asp:BoundColumn>
										<asp:BoundColumn HeaderText="Pais" DataField="Pais" ItemStyle-Width="13%"></asp:BoundColumn>
									</Columns>
								</asp:DataGrid></TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<asp:Panel id="pnlActual" Runat="server" Enabled="True" Visible="True" Width="100%">
					<asp:Panel id="pnlActualPoder" Runat="server" Enabled="True" Visible="True">
						<TABLE class="grid_head" style="MARGIN-LEFT: 5px; WIDTH: 99%">
							<TR>
								<TD>Poder o Propietario Actual</TD>
							</TR>
						</TABLE>
						<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
							<TR>
								<TD>
									<asp:DataGrid id="dgPoderActual" runat="server" Width="100%" AutoGenerateColumns="False">
										<HeaderStyle CssClass="cell_header" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle CssClass="cell"></ItemStyle>
										<Columns>
											<asp:BoundColumn HeaderText="C&#243;d." DataField="ID" ItemStyle-Width="8%"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Propietario" DataField="Denominacion" ItemStyle-Width="14%"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Domicilio" DataField="Domicilio" ItemStyle-Width="13%"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Concepto" DataField="Concepto" ItemStyle-Width="13%"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Insc." DataField="Inscripcion" ItemStyle-Width="13%"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Acta" DataField="Acta" ItemStyle-Width="13%"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Pais" DataField="Pais" ItemStyle-Width="13%"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Observaci&#243;n" DataField="Obs" ItemStyle-Width="13%"></asp:BoundColumn>
										</Columns>
									</asp:DataGrid></TD>
							</TR>
						</TABLE>
					</asp:Panel>
				</asp:Panel>
			</asp:panel>
			<!-- 
			================================================================================
			PANEL ASIGNAR DOCUMENTOS - INGRESA DOCUMENTOS Y DATOS CONCERNIENTES AL TV
			================================================================================
			--><asp:panel id="pnlAsignarDocumentos" Runat="server" Visible="False" Enabled="True" Width="98%">
				<TABLE class="grid_head" style="MARGIN-LEFT: 5px; WIDTH: 99%">
					<TR>
						<TD>Documentos Asignados</TD>
					</TR>
				</TABLE>
				<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
					<TR>
						<TD align="left" width="97%">
							<asp:DataGrid id="dgAsignarDocumentos" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False">
								<HeaderStyle CssClass="cell_header" HorizontalAlign="Center"></HeaderStyle>
								<ItemStyle CssClass="cell"></ItemStyle>
								<Columns>
									<asp:BoundColumn ItemStyle-VerticalAlign="Top" HeaderText="Descripción" DataField="Campo" ItemStyle-Width="20%"></asp:BoundColumn>
									<asp:BoundColumn ItemStyle-VerticalAlign="Top" HeaderText="Valor" DataField="Valor" ItemStyle-Width="80%"></asp:BoundColumn>
								</Columns>
							</asp:DataGrid></TD>
					</TR>
				</TABLE>
			</asp:panel><asp:panel id="pnlGrabar" Runat="server" Visible="True" Enabled="True">
				<TABLE cellSpacing="5" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 20%" align="right"><A onclick="javascript:window.print();" href="javascript:;">Imprimir</A></TD>
						<TD width="20%">
							<P></P>
						</TD>
						<TD vAlign="middle" align="center" width="15%">
							<asp:button id="btnGrabar" runat="server" Width="100%" CssClass="Button" Font-Bold="True" Text="Aceptar" onclick="btnGrabar_Click"></asp:button></TD>
						<TD width="20%">
							<P></P>
						</TD>
						<TD style="WIDTH: 15%" vAlign="middle" align="center">
							<asp:button id="btnEliminar" runat="server" Width="100%" CssClass="Button" Font-Bold="True"
								Text="Eliminar HI" onclick="btnEliminar_Click"></asp:button></TD>
						<TD width="10%">
							<P></P>
						</TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!--
			================================================================================
			FIN DE PANELES
			================================================================================
			--></form>
	</body>
</HTML>
