<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.CambioEstado" CodeFile="CambioEstado.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Cambios de estado</title>
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
			<asp:panel id="pnlCabecera" runat="server" Height="56px">
				<P>
					<UC1:HEADER id="Header1" runat="server"></UC1:HEADER></P>
				<P class="titulo">Cambio de Estado de Marcas
				</P>
			</asp:panel>
			<p></p>
			<asp:panel id="pnlDatos" runat="server">
				<P class="subtitulo">Actualización</P>
				<P>
					<TABLE class="infoMacro" id="tblDatos">
						<TR>
							<TD style="HEIGHT: 11px">
								<asp:Label id="Label5" runat="server" CssClass="Etiqueta2" Width="80px">Pasar A</asp:Label>
								<asp:DropDownList id="ddlOperacion" runat="server" Width="300px" AutoPostBack="True" onselectedindexchanged="ddlOperacion_SelectedIndexChanged"></asp:DropDownList></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 12px">
								<asp:Label id="lblMotivo" runat="server" CssClass="Etiqueta2" Width="80px">Motivo</asp:Label>
								<asp:DropDownList id="ddlMotivo" runat="server" Width="448px"></asp:DropDownList></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 55px" vAlign="middle">
								<asp:Label id="Label8" runat="server" Height="51px" CssClass="Etiqueta2" Width="80px">Observación</asp:Label>
								<asp:TextBox id="txtObs" runat="server" Height="52px" Width="536px" TextMode="MultiLine" Rows="3"></asp:TextBox></TD>
						</TR>
						<TR>
							<TD vAlign="middle">
								<asp:Label id="lblPoder" runat="server" Height="16px" CssClass="Etiqueta2" Width="80px">Id. Poder</asp:Label>
								<asp:TextBox id="txtPoder" runat="server" Height="20px" Width="192px" TextMode="SingleLine" Rows="1"></asp:TextBox></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="80px">Propietario</asp:Label>
								<ecctrl:ecCombo id="cbxPropietario" runat="server" Width="520px" ShowLabel="False"></ecctrl:ecCombo></TD>
						</TR>
					</TABLE>
				</P>
			</asp:panel><asp:panel id="pnlBuscar" runat="server">
				<P>
					<TABLE class="infoMacro" style="WIDTH: 744px; HEIGHT: 52px">
						<TR>
							<TD style="WIDTH: 102px; HEIGHT: 28px">
								<P>
									<asp:DropDownList id="ddlIdentificador" runat="server" Width="104px">
										<asp:ListItem Value="Acta">Acta</asp:ListItem>
										<asp:ListItem Value="Reg">Registro</asp:ListItem>
										<asp:ListItem Value="ExpeID">Expediente ID</asp:ListItem>
										<asp:ListItem Value="MarcaID">Marca ID</asp:ListItem>
									</asp:DropDownList></P>
							</TD>
							<TD style="HEIGHT: 28px">
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
									<asp:Button id="btnBuscar" runat="server" Width="128px" Text="Buscar" onclick="btnBuscar_Click"></asp:Button></P>
							</TD>
						<TR>
							<TD colSpan="7">
								<asp:label id="lblMensaje" runat="server" Font-Bold="True" Font-Size="X-Small"></asp:label></TD>
						</TR>
						</TR></TABLE>
				</P>
			</asp:panel>
			<!-- 
										Panel  de Incorporar 
			 --><asp:panel id="pnlIncorporar" runat="server">
				<TABLE class="infoMacro" id="tblIncorporar">
					<TR>
						<TD style="WIDTH: 72px; HEIGHT: 23px">
							<asp:Label id="Label7" runat="server" CssClass="Etiqueta2" Width="80px">Facturable</asp:Label></TD>
						<TD style="WIDTH: 624px; HEIGHT: 23px">
							<asp:RadioButtonList id="rbFacturable" runat="server" Width="120px" BorderWidth="1px" BorderStyle="Solid"
								RepeatDirection="Horizontal">
								<asp:ListItem Value="Si" Selected="True">Si</asp:ListItem>
								<asp:ListItem Value="No">No</asp:ListItem>
							</asp:RadioButtonList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 72px">
							<asp:Label id="Label11" runat="server" CssClass="Etiqueta2" Width="80px">Cliente</asp:Label></TD>
						<TD style="WIDTH: 624px">
							<ecctrl:ecCombo id="cbxCliente" runat="server" Width="520px" ShowLabel="False"></ecctrl:ecCombo></TD>
					</TR>
				</TABLE>
			</asp:panel><asp:panel id="pnlAgenteLocal" runat="server">
				<TABLE class="infoMacro" id="tblAgenteLocal">
					<TR>
						<TD style="WIDTH: 72px">
							<asp:Label id="lblAgLocal" runat="server" CssClass="Etiqueta2" Width="80px">Agente Local</asp:Label></TD>
						<TD style="WIDTH: 624px">
							<CUSTOM:DROPDOWN id="ddlAgenteBerke" runat="server" Width="368px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!-- 
										Panel  Datos de Expediente
			-->
			<TABLE>
				<TR>
					<TD colSpan="5"><asp:datagrid id="dgResult" runat="server" CssClass="Grilla" AutoGenerateColumns="False" HorizontalAlign="Center">
							<ItemStyle CssClass="EstiloItem"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" CssClass="EstiloHeader"></HeaderStyle>
							<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Sel.">
									<ItemTemplate>
										<asp:CheckBox ID="lblchkOk" runat="server" Checked="False" Enabled="False" />
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
								<asp:TemplateColumn HeaderText="Acta">
									<ItemTemplate>
										<asp:label ID="lblActa" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Expediente">
									<ItemTemplate>
										<asp:label ID="lblExpediente" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Marca">
									<ItemTemplate>
										<asp:label ID="lblMarca" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Exp.ID">
									<ItemTemplate>
										<asp:label ID="datExpeID" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Error">
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:label ID="lblError" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Ag.Local">
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:label ID="datAgLocalID" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
			<!-- 
										Panel  Botones 
			 -->
			<P></P>
			<asp:panel id="pnlBotones" runat="server">
				<TABLE id="tblBotones" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR align="center">
						<TD>
							<asp:Button id="btnGrabar" runat="server" Height="27px" Width="127px" Text="Grabar" onclick="btnGrabar_Click"></asp:Button></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<P></P>
		</form>
	</body>
</HTML>
