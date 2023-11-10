<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.HDescTramVarios" CodeFile="HDescTramVarios.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Hoja Descriptiva - Trámites Varios</title>
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
			<uc1:header id="Header1" runat="server"></uc1:header><asp:panel id="Panel1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 144px" runat="server"
				Height="570px" Width="752px">
				<TABLE id="Table120" cellSpacing="0" cellPadding="0" width="100%" align="center"> <!--Tabla-->
					<TR>
						<TD>
							<asp:panel id="PanelRegistroTransferencia" runat="server" Width="760px" Height="0px" Visible="False">
								<TABLE id="Table21" cellSpacing="0" cellPadding="0" width="100%">
									<TR>
										<TD width="2%" height="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
										<TD class="titletable1" align="left" width="25%" bgColor="#7bb5e7">&nbsp;
											<asp:Label id="Label1" runat="server" Width="176px" BackColor="Transparent" BorderColor="Transparent">Registro N°</asp:Label></TD>
										<TD class="titletable1" align="left" width="24%" bgColor="#7bb5e7">&nbsp;
											<asp:Label id="Label2" runat="server" Width="166px" BackColor="Transparent" BorderColor="Transparent">En Fecha</asp:Label></TD>
										<TD class="titletable1" align="left" width="24%" bgColor="#7bb5e7">&nbsp;
											<asp:Label id="Label3" runat="server" Width="135px" BackColor="Transparent" BorderColor="Transparent">Área</asp:Label></TD>
										<TD class="titletable1" align="left" width="25%" bgColor="#7bb5e7">&nbsp;
											<asp:Label id="Label4" runat="server" Width="194px" BackColor="Transparent" BorderColor="Transparent">Clasificación</asp:Label></TD>
									</TR>
								</TABLE> <!--Segunda Tabla-->
								<TABLE id="Table23" style="FONT-SIZE: 10pt; WIDTH: 760px; FONT-FAMILY: Verdana" cellSpacing="0"
									cellPadding="0" width="100%">
									<TR>
										<TD class="EstiloResaltar" align="center" width="231">
											<asp:Label id="lbNroRegistroTr" runat="server" Width="192px" Height="8px">lbNroRegistroTr</asp:Label></TD>
										<TD class="EstiloResaltar" align="center" width="174">
											<asp:Label id="lbFechaRegistroTr" runat="server" Width="100%" Height="8px">lbFechaRegistroTr</asp:Label></TD>
										<TD class="EstiloResaltar" align="center" width="143">
											<asp:Label id="lbAreaTr" runat="server" Width="100%" Height="8px">lbAreaTr</asp:Label></TD>
										<TD class="EstiloResaltar" align="center">
											<asp:Label id="lbClasificacionTr" runat="server" Width="100%" Height="8px">lbClasificacionTr</asp:Label></TD>
									</TR>
								</TABLE> <!--Segunda Tabla--></asp:panel></TD>
					</TR> <!--/Tabla--> <!-- Desde acá -->
					<TR>
						<TD>
							<asp:panel id="PanelDenominacion" runat="server" Width="760px" Height="0px" Visible="False">
								<TABLE id="Table22" style="FONT-SIZE: 10pt; WIDTH: 752px; FONT-FAMILY: Verdana; HEIGHT: 43px"
									cellSpacing="1" cellPadding="1" width="752">
									<TR>
										<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>Denominación</STRONG></FONT></TD>
										<TD style="WIDTH: 9px"></TD>
										<TD><FONT size="1">
												<asp:Label id="lbDenominacionTr" runat="server" Width="446px" Height="9px">lbDenominacionTr</asp:Label></FONT></TD>
									</TR>
								</TABLE>
							</asp:panel></TD>
					</TR> <!-- Hasta acá -->
					<TR>
						<TD>
							<asp:panel id="PanelPriTitular" runat="server" Width="760px" Height="0px">
								<TABLE id="Table15" cellSpacing="0" cellPadding="0" width="100%" align="center">
									<TR>
										<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
										<TD class="titletable1" width="98%" bgColor="#7bb5e7">&nbsp;
											<asp:Label id="lblTitulo1" runat="server" Width="344px" BackColor="Transparent" BorderColor="Transparent">1° TITULAR</asp:Label></TD>
									</TR>
								</TABLE>
								<TABLE id="Table3" style="FONT-SIZE: 10pt; WIDTH: 752px; FONT-FAMILY: Verdana; HEIGHT: 43px"
									cellSpacing="1" cellPadding="1" width="752">
									<asp:panel id="PanelDatosPriTutular" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 144px"
										runat="server" Width="760px" Height="0px">
										<TBODY>
											<TR>
												<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>Nombre</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
														<asp:Label id="lbNombre" runat="server" Width="446px" Height="9px">lbNombre</asp:Label></FONT></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>Domicilio</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
														<asp:Label id="lbDomicilio" runat="server" Width="446px" Height="9px">lbDomicilio</asp:Label></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD style="WIDTH: 30px" align="left"><FONT size="1"><STRONG>País</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
														<asp:Label id="lbPais" runat="server" Width="100%" Height="9px">lbPais</asp:Label></FONT></TD>
											</TR>
                                            <TR>
												<TD style="WIDTH: 65px" align="left"><FONT size="1"><STRONG>E-mail</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
														<asp:Label id="lbEmail" runat="server" Width="446px" Height="9px">lbEmail</asp:Label></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD style="WIDTH: 65px" align="left"><FONT size="1"><STRONG>Teléfono</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
														<asp:Label id="lbTelefono" runat="server" Width="100%" Height="9px">lbTelefono</asp:Label></FONT></TD>
											</TR>
									</asp:panel>
									<asp:panel id="PanelDatosMarcaPriTitular" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 144px"
										runat="server" Width="760px" Height="0px">
										<TR>
											<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>Denominación</STRONG></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD><FONT size="1">
													<asp:Label id="lbDenominacion" runat="server" Width="446px" Height="9px">lbDenominación</asp:Label></FONT></TD>
                                            <TD style="WIDTH: 9px"></TD>
                                            <TD style="WIDTH: 65px" align="left"><FONT size="1"><STRONG>N° Reg.</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
												<asp:Label id="lbNro" runat="server" Width="100%" Height="9px">lbNro</asp:Label></FONT></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>Clase</STRONG></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD><FONT size="1">
													<asp:Label id="lbClase" runat="server" Width="180px" Height="9px">lbClase</asp:Label></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD style="WIDTH: 30px" align="left"><FONT size="1"><STRONG>Año</STRONG></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD><FONT size="1">
													<asp:Label id="lbAnho" runat="server" Width="100%" Height="9px">lbAnho</asp:Label></FONT></TD>
											
										</TR>
									</asp:panel>
									<asp:panel id="PanelDatosRegistroPriTitular" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 144px"
										runat="server" Width="760px" Height="0px">
										<TR>
                                            <TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>N° Registro</STRONG></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD><FONT size="1">
													<asp:Label id="lbNroRegistro" runat="server" Width="160" Height="9px">lbNroRegistro</asp:Label></FONT></TD>
                                            <TD style="WIDTH: 9px"></TD>
											<TD style="WIDTH: 30px" align="left"><FONT size="1"><STRONG>Fecha</STRONG></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD><FONT size="1">
													<asp:Label id="lbFechaRegistro" runat="server" Width="100%" Height="9px">lbFechaRegistro</asp:Label></FONT></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>N° Expediente</STRONG></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD><FONT size="1">
													<asp:Label id="lbNroExpediente" runat="server" Width="160" Height="9px">lbNroExpediente</asp:Label></FONT></TD>
										</TR>
									</asp:panel></TABLE>
							</asp:panel></TD>
					</TR>
					<TR>
						<TD>
							<asp:panel id="PanelSegTitular" runat="server" Width="760px" Height="0px">
								<TABLE id="Table8" cellSpacing="0" cellPadding="0" width="100%" align="center">
									<TR>
										<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
										<TD class="titletable1" width="98%" bgColor="#7bb5e7">
											<asp:Label id="lblTitulo2" runat="server" Width="360px">2° TITULAR</asp:Label></TD>
									</TR>
								</TABLE>
								<TABLE id="Table9" style="FONT-SIZE: 10pt; WIDTH: 752px; FONT-FAMILY: Verdana; HEIGHT: 43px"
									cellSpacing="1" cellPadding="1" width="752">
                                    <TBODY>
									<asp:panel id="PanelDatosSegTutular" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 144px"
										runat="server" Width="760px" Height="0px">
										
											<TR>
												<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>Nombre</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
														<asp:Label id="lbNombre2" runat="server" Width="446px" Height="9px">lbNombre2</asp:Label></FONT></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>Domicilio</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
														<asp:Label id="lbDomicilio2" runat="server" Width="446px" Height="9px">lbDomicilio2</asp:Label></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD style="WIDTH: 30px" align="left"><FONT size="1"><STRONG>País</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
														<asp:Label id="lbPais2" runat="server" Width="100%" Height="9px">lbPais2</asp:Label></FONT></TD>
											</TR>
                                            <TR>
												<TD style="WIDTH: 65px" align="left"><FONT size="1"><STRONG>E-mail</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
														<asp:Label id="lbEmail2" runat="server" Width="446px" Height="9px">lbEmail2</asp:Label></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD style="WIDTH: 65px" align="left"><FONT size="1"><STRONG>Teléfono</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
														<asp:Label id="lbTelefono2" runat="server" Width="100%" Height="9px">lbTelefono2</asp:Label></FONT></TD>
											</TR>
									</asp:panel>
									<asp:panel id="PanelDatosMarcaSegTitular" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 144px"
										runat="server" Width="760px" Height="0px">
										<TR>
											<TD style="WIDTH: 100px; HEIGHT: 14px" align="left"><FONT size="1"><STRONG>Denominación</STRONG></FONT></TD>
											<TD style="WIDTH: 9px; HEIGHT: 14px"></TD>
											<TD style="HEIGHT: 14px"><FONT size="1">
													<asp:Label id="lbDenominacion2" runat="server" Width="446px" Height="9px">lbDenominación2</asp:Label></FONT></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>Clase</STRONG></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD><FONT size="1">
													<asp:Label id="lbClase2" runat="server" Width="160" Height="9px">lbClase2</asp:Label></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD style="WIDTH: 30px" align="left"><FONT size="1"><STRONG>Año</STRONG></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD><FONT size="1">
													<asp:Label id="lbAnho2" runat="server" Width="100%" Height="9px">lbAnho2</asp:Label></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD style="WIDTH: 30px" align="left"><FONT size="1"><STRONG>N°</STRONG></FONT></TD>
											<TD><FONT size="1">
													<asp:Label id="lbNro2" runat="server" Width="100%" Height="9px">lbNro2</asp:Label></FONT></TD>
										</TR>
									</asp:panel>
									<asp:panel id="PanelDatosRegistroSegTitular" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 144px"
										runat="server" Width="760px" Height="0px">
										<TR>
											<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>N° Registro</STRONG></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD><FONT size="1">
													<asp:Label id="lbNroRegistro2" runat="server" Width="160" Height="9px">lbNroRegistro2</asp:Label></FONT></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>N° Expediente</STRONG></FONT></TD>
											<TD style="WIDTH: 9px"></TD>
											<TD><FONT size="1">
													<asp:Label id="lbNroExpediente2" runat="server" Width="160" Height="9px">lbNroExpediente2</asp:Label></FONT></TD>
										</TR>
									</asp:panel>
                                   </TBODY>
                                </TABLE>
							</asp:panel></TD>
					</TR>
					<TR>
						<TD>
							<asp:panel id="PanelApoderado" runat="server" Width="760px" Height="66px">
								<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" align="center">
									<TR>
										<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
										<TD class="titletable1" width="98%" bgColor="#7bb5e7">APODERADO</TD>
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
											<asp:TextBox id="tbDomicilioAgente" runat="server" Width="100%" Height="22px">Benjam&#237;n Constant 835, Asunci&#243;n, PARAGUAY</asp:TextBox></TD>
										<TD style="WIDTH: 198px" align="right">
											<asp:Label id="lbNroPoder" runat="server" Width="100%" Height="8px">lbNroPoder</asp:Label></TD>
									</TR>
								</TABLE>
							</asp:panel></TD>
					</TR>
					<TR>
						<TD>
							<asp:panel id="PanelNombAdq" runat="server" Width="760px" Height="48px">
								<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="100%" align="center">
									<TR>
										<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
										<TD class="titletable1" width="98%" bgColor="#7bb5e7">
											<asp:Label id="lblTitulo3" runat="server">NOMBRE QUE ADQUIERE</asp:Label></TD>
									</TR>
								</TABLE>
								<TABLE id="Table10" style="FONT-SIZE: 10pt; WIDTH: 752px; FONT-FAMILY: Verdana; HEIGHT: 43px"
									cellSpacing="1" cellPadding="1" width="752">
									<TR>
										<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>Nombre</STRONG></FONT></TD>
										<TD style="WIDTH: 9px"></TD>
										<TD><FONT size="1">
												<asp:Label id="lbNombreCN" runat="server" Width="446px" Height="9px">lbNombreCN</asp:Label></FONT></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>Domicilio</STRONG></FONT></TD>
										<TD style="WIDTH: 9px"></TD>
										<TD><FONT size="1">
												<asp:Label id="lbDomicilioCN" runat="server" Width="446px" Height="9px">lbDomicilioCN</asp:Label></FONT></TD>
										<TD style="WIDTH: 9px"></TD>
										<TD style="WIDTH: 30px" align="left"><FONT size="1"><STRONG>País</STRONG></FONT></TD>
										<TD style="WIDTH: 9px"></TD>
										<TD><FONT size="1">
												<asp:Label id="lbPaisCN" runat="server" Width="100%" Height="9px">lbPaisCN</asp:Label></FONT></TD>
									</TR>
                                    <TR>
												<TD style="WIDTH: 65px" align="left"><FONT size="1"><STRONG>E-mail</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
														<asp:Label id="lbEmailCN" runat="server" Width="446px" Height="9px">lbEmailCN</asp:Label></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD style="WIDTH: 65px" align="left"><FONT size="1"><STRONG>Teléfono</STRONG></FONT></TD>
												<TD style="WIDTH: 9px"></TD>
												<TD><FONT size="1">
														<asp:Label id="lbTelefonoCN" runat="server" Width="100%" Height="9px">lbTelefonoCN</asp:Label></FONT></TD>
											</TR>
								</TABLE>
							</asp:panel></TD>
					</TR>
					<TR>
						<TD>
							<asp:panel id="PanelNvoDom" runat="server" Width="760px" Height="56px">
								<TABLE id="Table12" cellSpacing="0" cellPadding="0" width="100%" align="center">
									<TR>
										<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
										<TD class="titletable1" width="98%" bgColor="#7bb5e7">
											<asp:Label id="lblTitulo4" runat="server">NUEVO DOMICILIO</asp:Label></TD>
									</TR>
								</TABLE>
								<TABLE id="Table16" style="FONT-SIZE: 10pt; WIDTH: 752px; FONT-FAMILY: Verdana; HEIGHT: 43px"
									cellSpacing="1" cellPadding="1" width="752">
									<TR>
										<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>Anterior</STRONG></FONT></TD>
										<TD style="WIDTH: 9px"></TD>
										<TD><FONT size="1">
												<asp:Label id="lbDomicilioAnterior" runat="server" Width="446px" Height="9px">lbDomicilioAnterior</asp:Label></FONT></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 100px" align="left"><FONT size="1"><STRONG>Nuevo Domicilio</STRONG></FONT></TD>
										<TD style="WIDTH: 9px"></TD>
										<TD><FONT size="1">
												<asp:Label id="lbDomicilioNuevo" runat="server" Width="446px" Height="9px">lbDomicilioNuevo</asp:Label></FONT></TD>
										<TD style="WIDTH: 9px"></TD>
										<TD style="WIDTH: 30px" align="left"><FONT size="1"><STRONG>País</STRONG></FONT></TD>
										<TD style="WIDTH: 9px"></TD>
										<TD><FONT size="1">
												<asp:Label id="lbPaisCD" runat="server" Width="100%" Height="9px">lbPaisCD</asp:Label></FONT></TD>
									</TR>
								</TABLE>
							</asp:panel></TD>
					</TR>
					<TR>
						<TD>
							<asp:panel id="Panel7" runat="server" Width="760px" Height="40px">
								<TABLE class="Table3Ent" id="Table7" style="FONT-SIZE: 10pt; WIDTH: 760px; FONT-FAMILY: Verdana; HEIGHT: 46px"
									cellSpacing="0" cellPadding="0" width="100%">
									<TR>
										<TD class="EstiloResaltarBordeSuperior" vAlign="bottom" width="50">
											<asp:RadioButtonList id="rblTipoDocumento" runat="server" Width="146px" Height="22px" Visible="False"
												BorderStyle="None">
												<asp:ListItem Value="4" Selected="True">Cambio de Nombre</asp:ListItem>
												<asp:ListItem Value="6">Cambio de Domicilio</asp:ListItem>
											</asp:RadioButtonList>
                                        </TD>
                                        <TD style="WIDTH: 180px" vAlign="middle" align="center"><FONT size="1"><STRONG>Tipo Modelo</STRONG></FONT></TD>
                                        <TD style="WIDTH: 180px" align="left">
							                <asp:DropDownList ID="ddlTipoModelo" runat="server" CssClass="cell" Height="24px" Width="100%">
                                            <asp:ListItem Value="N">Modelo 2015</asp:ListItem>
                                            <asp:ListItem Value="D">Modelo 2015 (Sólo Datos)</asp:ListItem>
                                            <asp:ListItem Value="A">Modelo Anterior</asp:ListItem>
                                            </asp:DropDownList>
                                        </TD>
                                        <TD vAlign="middle" align="center"><FONT size="1">
							                <asp:Button id="btnImprimir" runat="server" Width="112px" Text="Generar archivo" CssClass="Button"
									                Font-Bold="True" onclick="btnImprimir_Click"></asp:Button></FONT>
						                </TD>
										<%--<TD class="EstiloResaltarBordeSuperior" vAlign="middle"><FONT size="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
												&nbsp;&nbsp;
												<asp:Button id="btnImprimir" runat="server" Width="112px" Text="Generar archivo" CssClass="Button"
													Font-Bold="True" onclick="btnImprimir_Click"></asp:Button>&nbsp;</FONT></TD>--%>
									</TR>
								</TABLE>
							</asp:panel>

						</TD>
					</TR>
				</TABLE>
			</asp:panel><asp:label id="lbTituloHDesc" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 112px"
				runat="server" Height="24px" Width="746px" Font-Bold="True" Font-Size="Medium"></asp:label></form>
	</body>
</HTML>
