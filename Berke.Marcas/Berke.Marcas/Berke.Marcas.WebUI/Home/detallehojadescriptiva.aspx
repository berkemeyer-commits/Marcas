<%@ Reference Page="~/home/imagen.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.DetalleHojaDescriptiva" CodeFile="DetalleHojaDescriptiva.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DetalleHojaDescriptiva</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
		<!--
			function redimensionar(imagen){
				if (imagen.width>imagen.height){
					imagen.width=200;
				}
				else {
					imagen.height=200;
				}
				return true;
			}
		-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:panel id="Panel1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 152px" runat="server"
				Width="752px" Height="304px">
				<TABLE id="Table1" style="FONT-SIZE: 10pt; WIDTH: 752px; FONT-FAMILY: Verdana; HEIGHT: 86px"
					cellSpacing="1" cellPadding="1" width="752">
					<TR>
						<TD style="WIDTH: 201px" align="right"><FONT size="1"><STRONG>Denominación</STRONG></FONT></TD>
						<TD style="WIDTH: 9px"></TD>
						<TD><FONT size="1">
								<asp:Label id="lbDenominacion" runat="server" Height="9px" Width="100%">lbDenominacion</asp:Label></FONT></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 201px" align="right"><FONT size="1"><STRONG>Clase</STRONG></FONT></TD>
						<TD style="WIDTH: 9px"></TD>
						<TD><FONT size="1">
								<asp:Label id="lbClase" runat="server" Height="9px" Width="394px">lbClase</asp:Label>
								<asp:Label id="lbClaseNro" runat="server" Height="8px" Width="26px" Visible="False">lbClaseNro</asp:Label>
								<asp:Label id="lbClaseTipo" runat="server" Height="8px" Width="26px" Visible="False">lbClaseTipo</asp:Label></FONT></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 201px" align="right"><FONT size="1"><STRONG>Tipo de Marca</STRONG></FONT></TD>
						<TD style="WIDTH: 9px"></TD>
						<TD><FONT size="1">
								<asp:Label id="lbTipoMarca" runat="server" Height="9px" Width="410px">lbTipoMarca</asp:Label>
								<asp:Label id="lbTipoMarcaId" runat="server" Height="8px" Width="74px" Visible="False">lbTipoMarcaId</asp:Label></FONT></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 201px" align="right"><FONT size="1"><STRONG>Nro. de Registro Anterior</STRONG></FONT></TD>
						<TD style="WIDTH: 9px"></TD>
						<TD><FONT size="1">
								<asp:Label id="lbNroRegistroAnterior" runat="server" Height="9px" Width="144px">lbNroRegistroAnterior</asp:Label>
								<asp:Label id="lbTipoTramite" runat="server" Height="9px" Width="144px" Visible="False">lbTipoTramite</asp:Label>
								<asp:Label id="lbLogotipoId" runat="server" Height="8px" Width="74px" Visible="False">lbLogotipoId</asp:Label></FONT></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 201px" align="right"><FONT size="1"><STRONG>Fecha de Vencimiento</STRONG></FONT></TD>
						<TD style="WIDTH: 9px"></TD>
						<TD><FONT size="1">
								<asp:Label id="lbFechaVencimiento" runat="server" Height="9px" Width="144px">lbFechaVencimiento</asp:Label></FONT></TD>
					</TR>
				</TABLE>
                <asp:panel id="PanelPrioridad" runat="server" Width="100%" Height="0px" Visible="False">
                <TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" align="center">
					<TR>
						<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
						<TD class="titletable1" width="98%" bgColor="#7bb5e7">Prioridad</TD>
					</TR>
				</TABLE>
				<TABLE id="Table9" style="FONT-SIZE: 10pt; WIDTH: 100%; FONT-FAMILY: Verdana; HEIGHT: 48px"
					cellSpacing="1" cellPadding="1" width="704">
					<TR class="EstiloHeader">
						<TD align="center"><FONT size="1"><STRONG>Fecha</STRONG></FONT></TD>
						<TD align="center"><FONT size="1"><STRONG>N° de Solicitud</STRONG></FONT></TD>
						<TD align="center"><FONT size="1"><STRONG>País</STRONG></FONT></TD>
					</TR>
					<TR class="EstiloItem">
						<TD style="WIDTH: 20%" align="left">
							<asp:Label id="lbFechaPrioridad" runat="server" Height="8px" Width="100%">lbFechaPrioridad</asp:Label></TD>
						<TD style="WIDTH: 40%" align="right">
							<asp:Label id="lbNroPrioridad" runat="server" Height="8px" Width="100%">lbNroPrioridad</asp:Label></TD>
						<TD align="left">
							<asp:Label id="lbPaisPrioridad" runat="server" Height="8px" Width="100%">lbPaisPrioridad</asp:Label></TD>
					</TR>
                </TABLE>
                <BR>
                </asp:panel>
                <TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="center">
					<TR>
						<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
						<TD class="titletable1" width="98%" bgColor="#7bb5e7">Solicitante</TD>
					</TR>
				</TABLE>
                <TABLE id="Table3" style="FONT-SIZE: 10pt; WIDTH: 100%; FONT-FAMILY: Verdana; HEIGHT: 48px"
					cellSpacing="1" cellPadding="1" width="704">
					<TR class="EstiloHeader">
						<TD style="WIDTH: 173px" align="center"><FONT size="1"><STRONG>Nombre</STRONG></FONT></TD>
						<TD style="WIDTH: 48px" align="center"><FONT size="1"><STRONG>País</STRONG></FONT></TD>
						<TD style="WIDTH: 198px" align="center"><FONT size="1"><STRONG>Domicilio</STRONG></FONT></TD>
					</TR>
					<TR class="EstiloItem">
						<TD style="WIDTH: 40%" align="left">
							<asp:Label id="lbNombreSolicitante" runat="server" Height="8px" Width="100%">lbNombreSolicitante</asp:Label></TD>
						<TD style="WIDTH: 20%" align="left">
							<asp:Label id="lbPaisSolicitante" runat="server" Height="8px" Width="100%">lbPaisSolicitante</asp:Label></TD>
						<TD align="left">
							<asp:Label id="lbDomicilioSolicitante" runat="server" Height="8px" Width="100%">lbDomicilioSolicitante</asp:Label></TD>
					</TR>
                    <TR class="EstiloHeader">
						<TD style="WIDTH: 173px" align="center"><FONT size="1"><STRONG>Teléfono</STRONG></FONT></TD>
						<TD style="WIDTH: 48px" align="center"><FONT size="1"><STRONG>E-Mail</STRONG></FONT></TD>
					</TR>
                    <TR class="EstiloItem">
						<TD style="WIDTH: 40%" align="left">
							<asp:Label id="lbTelefonoSolicitante" runat="server" Height="8px" Width="100%">lbTelefonoSolicitante</asp:Label></TD>
						<TD style="WIDTH: 20%" align="left">
							<asp:Label id="lbEmailSolicitante" runat="server" Height="8px" Width="100%">lbEmailSolicitante</asp:Label></TD>
					</TR>
				</TABLE>
				<BR>
				<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" align="center">
					<TR>
						<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
						<TD class="titletable1" width="98%" bgColor="#7bb5e7">Agente</TD>
					</TR>
				</TABLE>
				<TABLE id="Table5" style="FONT-SIZE: 10pt; WIDTH: 100%; FONT-FAMILY: Verdana; HEIGHT: 48px"
					cellSpacing="1" cellPadding="1" width="704">
					<TR class="EstiloHeader">
						<TD style="WIDTH: 242px; HEIGHT: 16px" align="center"><FONT size="1"><STRONG>Nombre</STRONG></FONT></TD>
						<TD style="WIDTH: 395px; HEIGHT: 16px" align="center"><FONT size="1"><STRONG>Domicilio</STRONG></FONT></TD>
						<TD style="WIDTH: 198px; HEIGHT: 16px" align="center"><FONT size="1"><STRONG>Nro. Poder</STRONG></FONT></TD>
					</TR>
					<TR class="EstiloItem">
						<TD style="WIDTH: 242px" align="left">
							<CUSTOM:DROPDOWN id="ddlAgente" runat="server" Width="248px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
						<TD style="WIDTH: 395px" align="left">
							<asp:TextBox id="tbDomicilioAgente" runat="server" Height="22px" Width="100%">Benjam&#237;n Constant 835, Asunci&#243;n, PARAGUAY</asp:TextBox></TD>
						<TD style="WIDTH: 198px" align="left">
							<asp:Label id="lbNroPoder" runat="server" Height="8px" Width="100%">lbNroPoder</asp:Label></TD>
					</TR>
				</TABLE>
				<BR>
				<TABLE id="Table6" style="FONT-SIZE: 10pt; WIDTH: 752px; FONT-FAMILY: Verdana; HEIGHT: 46px"
					cellSpacing="1" cellPadding="1" width="752">
					<TR>
						<TD style="WIDTH: 202px" vAlign="middle" align="right"><FONT size="1"><STRONG>Descripción 
									de la clase</STRONG></FONT></TD>
						<TD style="WIDTH: 9px"></TD>
						<TD vAlign="middle"><FONT size="1">
								<asp:Label id="lbDescripClase" runat="server" Height="8px" Width="100%">lbDescripClase</asp:Label></FONT></TD>
					</TR>
				</TABLE>
				<TABLE id="Table66" style="FONT-SIZE: 10pt; WIDTH: 752px; FONT-FAMILY: Verdana; HEIGHT: 46px"
					cellSpacing="1" cellPadding="1" width="752">
					<TR>
						<TD style="WIDTH: 202px" vAlign="middle" align="right"><FONT size="1"><STRONG>Etiqueta</STRONG></FONT></TD>
						<TD style="WIDTH: 9px"></TD>
						<TD vAlign="middle"><FONT size="1">
								<asp:Label id="lblLogo" runat="server">Logotipo</asp:Label></FONT></TD>
					</TR>
				</TABLE>
				<BR>
                <TABLE id="Table8" style="FONT-SIZE: 10pt; WIDTH: 752px; FONT-FAMILY: Verdana; HEIGHT: 48px"
					cellSpacing="1" cellPadding="1" width="704">
                    <TR class="EstiloItem">
						<TD style="WIDTH: 202px" vAlign="middle" align="right"><FONT size="1"><STRONG>Tipo Modelo</STRONG></FONT></TD>
						<TD style="WIDTH: 9px"></TD>
						<TD style="WIDTH: 150px" align="left">
							<asp:DropDownList ID="ddlTipoModelo" runat="server" CssClass="cell" Height="24px" Width="100%">
                                <asp:ListItem Value="N">Modelo 2015</asp:ListItem>
                                <asp:ListItem Value="D">Modelo 2015 (Sólo Datos)</asp:ListItem>
                                <asp:ListItem Value="A">Modelo Anterior</asp:ListItem>
                            </asp:DropDownList>
                        </TD>
						<TD style="WIDTH: 9px"></TD>	
						<TD vAlign="middle"><FONT size="1">
							<asp:Button id="btnImprimir" runat="server" Width="112px" Text="Generar archivo" CssClass="Button"
									Font-Bold="True" onclick="btnImprimir_Click"></asp:Button></FONT>
						</TD>
					</TR>
                </TABLE>
				
			</asp:panel><asp:label id="lbTitulo" style="Z-INDEX: 102; LEFT: 16px; POSITION: absolute; TOP: 112px" runat="server"
				Width="504px" Height="24px" Font-Bold="True" Font-Size="Medium">Hoja Descriptiva de Registro de Marcas</asp:label></form>
	</body>
</HTML>
