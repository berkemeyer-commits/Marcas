<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.ExpeMarcaConsultar" CodeFile="ExpeMarcaConsultar.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Exped. de Marca</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<!--<meta content="JavaScript" name="vs_defaultClientScript">-->
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/globalstyle.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssMainStyle.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssDgEstiloGeneral.css">
		<script language="JavaScript">
            function restablecer(pCod){
              /* alert('ID a transferir ' + pCod ); */
              window.opener.document.Form1.ExpeID.value = pCod ;
              window.opener.focus();
              window.close();
            }

            function redimensionar(imagen) {
                if (imagen.width > imagen.height) {
                    imagen.width = 200;
                }
                else {
                    imagen.height = 200;
                }
                return true;
            }
			
		</script>
	</HEAD>
	<body>
		<form id="Form1" onkeypress="form_onEnter('btBuscar');" method="post" runat="server">
			<!-- 

   Cabecera 
		
   /-->
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<span class="titulo">
				<asp:label id="lblTitulo" runat="server" Font-Bold="True" Font-Size="Medium" CssClass="titulo">Consulta de Marcas</asp:label></span><br>
			<!-- 

   Fin Cabecera

   /-->
			<table id="Table1" width="720">
				<tr>
					<td><asp:label id="lblTituloAclaracion" runat="server" CssClass="subtitulo" Width="304px"></asp:label></td>
					<td>
						<P><asp:label id="lblMensaje" runat="server" Font-Bold="True" CssClass="msg" Width="396px" ForeColor="#C00000"></asp:label></P>
					</td>
				</tr>
			</table>
			<div style="WIDTH: 720px; MARGIN-LEFT: 5px" class="hideTitle" title="Ocultar/visualizar formulario de búsqueda"
				onclick="closeDiv('pnlBuscar')">Criterio de búsqueda
			</div>
			<div id="formBusqueda"><asp:panel id="pnlBuscar" runat="server" Width="720px">
					<TABLE id="Table2" border="0" width="100%">
						<TR>
							<TD vAlign="top">
								<TABLE id="Table3" class="infoMacro" width="99%">
									<TR>
										<TD colSpan="2">Expediente</TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="lblExpedienteID_min" runat="server" CssClass="Etiqueta2" Width="56px">Exped.ID</asp:Label></TD>
										<TD>
											<asp:textbox id="txtExpedienteID_min" runat="server" CssClass="inpform" Width="88px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="lblActaNro_min" runat="server" CssClass="Etiqueta2" Width="56px" DESIGNTIMEDRAGDROP="1513">Acta</asp:Label></TD>
										<TD>
											<asp:textbox id="txtActaNro_min" runat="server" Width="120px" DESIGNTIMEDRAGDROP="1514"></asp:textbox>
											<asp:Label id="lblActaAnio" runat="server" CssClass="Etiqueta2" Width="16px">Año</asp:Label>
											<asp:textbox id="txtActaAnio" runat="server" Width="60px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD></TD>
										<TD></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 22px">
											<asp:Label id="lblTramiteID" runat="server" CssClass="Etiqueta2" Width="56px" Height="16px">Trámite</asp:Label></TD>
										<TD style="HEIGHT: 22px">
											<CUSTOM:DROPDOWN id="ddlTramiteID" runat="server" Width="208px" AutoPostBack="True"></CUSTOM:DROPDOWN></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="Label2" runat="server" CssClass="Etiqueta2" Width="56px">En trámite</asp:Label></TD>
										<TD>
											<asp:RadioButtonList id="rbEnTramite" runat="server" Width="120px" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
												BorderStyle="Solid" BorderWidth="1px">
												<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
												<asp:ListItem Value="Si">Si</asp:ListItem>
												<asp:ListItem Value="No">No</asp:ListItem>
											</asp:RadioButtonList></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="56px" DESIGNTIMEDRAGDROP="1808">Alta</asp:Label></TD>
										<TD>
											<asp:textbox id="txtAltaFecha" runat="server" Width="152px" DESIGNTIMEDRAGDROP="1807"></asp:textbox></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="lblTramiteSitID" runat="server" CssClass="Etiqueta2" Width="56px" DESIGNTIMEDRAGDROP="3862"
												Height="4px">Situación</asp:Label></TD>
										<TD>
											<CUSTOM:DROPDOWN id="ddlTramiteSitID" runat="server" Width="256px" AutoPostBack="True"></CUSTOM:DROPDOWN></TD>
									</TR>
								</TABLE>
								<TABLE id="Table4" class="infoMacro" width="99%">
									<TR>
										<TD colSpan="2">Marca</TD>
									</TR>
									<TR>
										<TD style="WIDTH: 76px">
											<asp:Label id="Label3" runat="server" CssClass="Etiqueta2" Width="72px">Marca ID</asp:Label></TD>
										<TD>
											<asp:textbox id="txtMarcaID_min" runat="server" Width="120px"></asp:textbox>
											<asp:Label id="Label4" runat="server" CssClass="Etiqueta2" Width="56px">Clase</asp:Label>
											<asp:textbox id="txtClaseNro" runat="server" Width="53px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 76px">
											<asp:Label id="lblDenominacion" runat="server" CssClass="Etiqueta2" Width="72px">Denominación</asp:Label></TD>
										<TD>
											<asp:textbox id="txtDenomEmpieza" runat="server" Width="240px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="Label9" runat="server" CssClass="Etiqueta2" Width="72px">Tipo</asp:Label></TD>
										<TD>
											<asp:CheckBoxList id="chkTipo" runat="server" RepeatDirection="Horizontal">
												<asp:ListItem Value="D">Denominativa&nbsp;</asp:ListItem>
												<asp:ListItem Value="F">Figurativa</asp:ListItem>
												<asp:ListItem Value="M">Mixta</asp:ListItem>
                                            </asp:CheckBoxList></TD>
                                        	
									</TR>
                                    <TR>
										<TD></TD>
										<TD>
											<asp:CheckBoxList id="chkTipo1" runat="server" RepeatDirection="Horizontal">
												<asp:ListItem Value="T">Tridimensional</asp:ListItem>
                                                <asp:ListItem Value="O">Olfativa&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="S">Sonora</asp:ListItem>
                                            </asp:CheckBoxList></TD>
                                        	
									</TR>
									<TR>
										<TD style="WIDTH: 76px; HEIGHT: 19px">
											<asp:Label id="lblMarcaNuestra" runat="server" CssClass="Etiqueta2" Width="72px">Nuestra</asp:Label></TD>
										<TD style="HEIGHT: 19px">
											<asp:RadioButtonList id="rbMarcaNuestra" runat="server" Width="120px" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
												BorderStyle="Solid" BorderWidth="1px">
												<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
												<asp:ListItem Value="Si">Si</asp:ListItem>
												<asp:ListItem Value="No">No</asp:ListItem>
											</asp:RadioButtonList></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 76px; HEIGHT: 19px">
											<asp:Label id="lblMarcaActiva" runat="server" CssClass="Etiqueta2" Width="72px">Activa</asp:Label></TD>
										<TD style="HEIGHT: 19px">
											<asp:RadioButtonList id="rbMarcaActiva" runat="server" Width="120px" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
												BorderStyle="Solid" BorderWidth="1px">
												<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
												<asp:ListItem Value="Si">Si</asp:ListItem>
												<asp:ListItem Value="No">No</asp:ListItem>
											</asp:RadioButtonList></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 76px; HEIGHT: 20px">
											<asp:Label id="lblMarcaVigilada" runat="server" CssClass="Etiqueta2" Width="72px">Vigilada</asp:Label></TD>
										<TD style="HEIGHT: 20px">
											<asp:RadioButtonList id="rbMarcaVigilada" runat="server" Width="32px" Height="7px" ToolTip="Tipo de expediente"
												RepeatDirection="Horizontal" BorderStyle="Solid" BorderWidth="1px">
												<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
												<asp:ListItem Value="Si">Si</asp:ListItem>
												<asp:ListItem Value="No">No</asp:ListItem>
											</asp:RadioButtonList></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 76px; HEIGHT: 1px">
											<asp:Label id="lblStandBy" runat="server" CssClass="Etiqueta2" Width="72px">Stand By</asp:Label></TD>
										<TD style="HEIGHT: 1px">
											<asp:RadioButtonList id="rbStandBy" runat="server" Width="104px" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
												BorderStyle="Solid" BorderWidth="1px">
												<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
												<asp:ListItem Value="Si">Si</asp:ListItem>
												<asp:ListItem Value="No">No</asp:ListItem>
											</asp:RadioButtonList></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 76px">
											<asp:Label id="lblSustituida" runat="server" CssClass="Etiqueta2" Width="72px">Sustitida</asp:Label></TD>
										<TD>
											<asp:RadioButtonList id="rbSustituida" runat="server" Width="120px" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
												BorderStyle="Solid" BorderWidth="1px">
												<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
												<asp:ListItem Value="Si">Si</asp:ListItem>
												<asp:ListItem Value="No">No</asp:ListItem>
											</asp:RadioButtonList></TD>
									</TR>
								</TABLE>
								<P align="left">
									<asp:button id="btBuscar" runat="server" CssClass="Button" Font-Bold="True" Width="72px" Text="Buscar" onclick="btBuscar_Click"></asp:button></P>
							</TD>
							<TD vAlign="top" width="50%">
								<TABLE class="infoMacro" width="99%">
									<TR>
										<TD colSpan="2">Registro</TD>
									</TR>
									<TR>
										<TD>
											<asp:DropDownList id="ddlTipoReg" runat="server" Width="95px">
												<asp:ListItem Value="REG">Registro</asp:ListItem>
												<asp:ListItem Value="REG_VIG">Reg.Vigte</asp:ListItem>
											</asp:DropDownList></TD>
										<TD>
											<asp:textbox id="txtRegistroNro_min" runat="server" Width="120px"></asp:textbox>
											<asp:Label id="lblRegistroAnio" runat="server" CssClass="Etiqueta2" Width="8px" DESIGNTIMEDRAGDROP="1890">Año</asp:Label>
											<asp:textbox id="txtRegistroAnio" runat="server" Width="60px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="Label5" runat="server" CssClass="Etiqueta2" Width="88px">Registro Vigente</asp:Label></TD>
										<TD>
											<asp:RadioButtonList id="rbVigente" runat="server" Width="120px" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
												BorderStyle="Solid" BorderWidth="1px">
												<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
												<asp:ListItem Value="Si">Si</asp:ListItem>
												<asp:ListItem Value="No">No</asp:ListItem>
											</asp:RadioButtonList></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="lblVencimientoFecha_min" runat="server" CssClass="Etiqueta2" Width="90px" DESIGNTIMEDRAGDROP="438">Vencimiento Reg.</asp:Label></TD>
										<TD>
											<asp:textbox id="txtVencimientoFecha_min" runat="server" Width="136px" DESIGNTIMEDRAGDROP="439"></asp:textbox></TD>
									</TR>
								</TABLE>
								<TABLE class="infoMacro" width="99%">
									<TR>
										<TD colSpan="2">Hoja de Inicio</TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="lblOtNro_min" runat="server" CssClass="Etiqueta2" Width="90px">HI Nro.</asp:Label></TD>
										<TD>
											<asp:textbox id="txtOtNro_min" runat="server" Width="88px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="lblOtAnio" runat="server" CssClass="Etiqueta2" Width="90px">Año</asp:Label></TD>
										<TD>
											<asp:textbox id="txtOtAnio" runat="server" Width="88px"></asp:textbox></TD>
									</TR>
								</TABLE>
								<TABLE class="infoMacro" width="99%">
									<TR>
										<TD colSpan="2">Agente Local</TD>
									</TR>
									<TR>
										<TD></TD>
										<TD>
											<ecctrl:eccombo id="cbxAgenteLocalID" runat="server" Width="306px" ShowLabel="False"></ecctrl:eccombo></TD>
									</TR>
								</TABLE>
								<TABLE class="infoMacro" width="99%">
									<TR>
										<TD colSpan="2">Cliente</TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="lblClienteID" runat="server" CssClass="Etiqueta2" Width="90px">Cliente ID</asp:Label></TD>
										<TD>
											<asp:textbox id="txtClienteID" runat="server" Width="160px" ToolTip="Lista de IDs"></asp:textbox></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="Label6" runat="server" CssClass="Etiqueta2" Width="90px">Nombre</asp:Label></TD>
										<TD>
											<asp:textbox id="txtNombreCli" runat="server" Width="208px"></asp:textbox></TD>
									</TR>
								</TABLE>
								<TABLE class="infoMacro" width="99%">
									<TR>
										<TD colSpan="2">Propietario</TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="lblPropietarioID" runat="server" CssClass="Etiqueta2" Width="90px" DESIGNTIMEDRAGDROP="1716">Propietario ID</asp:Label></TD>
										<TD>
											<asp:textbox id="txtPropietarioID" runat="server" Width="90px" ToolTip="Lista de IDs"></asp:textbox></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="Label7" runat="server" CssClass="Etiqueta2" Width="90px" DESIGNTIMEDRAGDROP="1716">Nombre</asp:Label></TD>
										<TD>
											<asp:textbox id="txtPropietarioNombre" runat="server" Width="208px" DESIGNTIMEDRAGDROP="1718"
												ToolTip="Subcadena"></asp:textbox></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="Label8" runat="server" CssClass="Etiqueta2" Width="90px" DESIGNTIMEDRAGDROP="1716">Pais</asp:Label></TD>
										<TD>
											<asp:textbox id="txtPropietarioPais" runat="server" Width="64px"></asp:textbox></TD>
									</TR>
								</TABLE>
								<DIV id="divPublicacion">
									<TABLE class="infoMacro" width="99%">
										<TR>
											<TD colSpan="2">Publicación</TD>
										</TR>
										<TR>
											<TD>
												<asp:Label id="Label10" runat="server" CssClass="Etiqueta2" Width="90px" DESIGNTIMEDRAGDROP="1716">Diario</asp:Label></TD>
											<TD>
												<CUSTOM:DROPDOWN id="ddlDiarioID" runat="server" Width="208px" AutoPostBack="True"></CUSTOM:DROPDOWN></TD>
										</TR>
										<TR>
											<TD>
												<asp:Label id="Label11" runat="server" CssClass="Etiqueta2" Width="90px" DESIGNTIMEDRAGDROP="1716">Año</asp:Label></TD>
											<TD>
												<asp:textbox id="txtAnhoPublic" runat="server" Width="62px" DESIGNTIMEDRAGDROP="1718" ToolTip="Subcadena"></asp:textbox></TD>
										</TR>
										<TR>
											<TD>
												<asp:Label id="Label12" runat="server" CssClass="Etiqueta2" Width="90px" DESIGNTIMEDRAGDROP="1716">Página</asp:Label></TD>
											<TD>
												<asp:textbox id="txtPaginaPublic" runat="server" Width="64px"></asp:textbox>
												<asp:Label id="lblOcultarPub" runat="server">LblPub</asp:Label></TD>
										</TR>
									</TABLE>
								</DIV>
								<DIV id="divBoletinDet">
									<TABLE class="infoMacro" width="99%">
										<TR>
											<TD style="HEIGHT: 14px" colSpan="2">Boletín</TD>
										</TR>
										<TR>
											<TD>
												<asp:Label id="Label13" runat="server" CssClass="Etiqueta2" Width="90px">Boletín Nro.</asp:Label></TD>
											<TD>
												<asp:textbox id="txtBoletinNro" runat="server" Width="88px"></asp:textbox></TD>
										</TR>
										<TR>
											<TD>
												<asp:Label id="Label14" runat="server" CssClass="Etiqueta2" Width="90px">Año</asp:Label></TD>
											<TD>
												<asp:textbox id="txtBoletinAnio" runat="server" Width="88px"></asp:textbox>
												<asp:Label id="lblOcultarBol" runat="server">LblBol</asp:Label></TD>
										</TR>
									</TABLE>
								</DIV>
                                <div>
                                    <asp:Panel id="pnlVerLogos" runat="server" Visible="true">
									<TABLE class="infoMacro" width="99%">
										<TR>
											<TD style="HEIGHT: 14px">Logos</TD>
										</TR>
										<TR>
											<TD>
												<asp:CheckBox id="chkMostrarLogos" runat="server" Width="336px" Text="Mostrar logos en los resultados"
													Enabled="true"></asp:CheckBox></TD>
											<asp:Label id="Label15" runat="server"></asp:Label>
                                        </TR>
									</TABLE>
								</asp:Panel>
                                </div>
								<asp:Panel id="pnlTituloSituacion" runat="server" Visible="true">
									<TABLE class="infoMacro" width="99%">
										<TR>
											<TD style="HEIGHT: 14px" colSpan="2">Situación</TD>
										</TR>
										<TR>
											<TD colSpan="2">
												<asp:CheckBox id="chkTitulosRetiradoCorregido" runat="server" Width="336px" Text="Sólo títulos retirados o corregidos (Última Situación)"
													Enabled="False"></asp:CheckBox></TD>
											<asp:Label id="lblTituloSituacion" runat="server"></asp:Label>
                                        </TR>
									</TABLE>
								</asp:Panel>

							</TD>
						</TR>
					</TABLE>
				</asp:panel></div>
			<!---
------------------ PANEL RESULTADO ------------
--><asp:panel id="pnlResultado" runat="server" Visible="True"><SPAN class="subtitulo">Resultado de 
					la Búsqueda </SPAN>
				<BR>
				<BR>
				<asp:button id="btnGenDoc" runat="server" CssClass="btn_doc" Font-Bold="True" Width="48px" Text="doc" onclick="btnGenDoc_Click"></asp:button>
				<asp:button id="btnGenXls" runat="server" CssClass="btn_xls" Font-Bold="True" Width="48px" Text="xls" onclick="btnGenXls_Click"></asp:button>
                <asp:button runat="server" id="btnReporteLogos" CssClass="btn_rv" Font-Bold="True" Width="115px" Text="Reporte Logos" OnClientClick="openReportPage(); return false;" CausesValidation="False" Visible="False"></asp:button>
				<BR>
				<BR>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="lblTituloGrid" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
                            <%--<form id="form2" runat="server">--%>
                                <%--<asp:GridView ID="GridView1" CssClass="tbl" runat="server" AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center"
                                    DataKeyNames="ExpedienteID">
								    <HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                <Columns>
                                    <asp:BoundField DataField="ExpedienteID" HeaderText="Exped.ID" />
                                    <asp:BoundField DataField="Denominacion" HeaderText="Denominacion" />
                                    <asp:TemplateField HeaderText="Logo">
    	                                <ItemTemplate>
	                                      <asp:Image ID="Logotipo" runat="server" Width="200px"
                                               ImageUrl = '<%# Eval("LogotipoID", GetUrl("Imagen.aspx?logotipoID={0}")) %>'>
                                          </asp:Image>
	                                    </ItemTemplate>
		                            </asp:TemplateField>
                                </Columns>
                                </asp:GridView>--%>
                            <%--</form>--%>
							<asp:datagrid id="dgResult" runat="server" CssClass="tbl" Width="100%" HorizontalAlign="Center"
								DataKeyField="ExpedienteID" AutoGenerateColumns="False" OnItemDataBound="dgResult_ItemDataBound">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<FooterStyle HorizontalAlign="Left"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="ExpedienteID" HeaderText="Exped.ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="Denominacion" HeaderText="Denominacion">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PropietarioNombre" HeaderText="Propietario">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ClaseNro" HeaderText="Clase">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MarcaTipo" HeaderText="Tipo"></asp:BoundColumn>
									<asp:BoundColumn DataField="Acta" HeaderText="Acta">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Registro" HeaderText="Registro">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VencimientoFecha" HeaderText="Vencim." DataFormatString="{0:dd/MM/yy}">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TramiteAbrev" HeaderText="Tramite">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SituacionDecrip" HeaderText="Situacion">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="OrdenTrabajo" HeaderText="H.I.">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TramiteDescrip" HeaderText="H.Descriptiva"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="LogotipoID" HeaderText="Logo" Visible="false">
                                        
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Logo">
                                        <ItemTemplate>
                                            <asp:Image ID="Logotipo" runat="server" Width="200px"
                                             ImageUrl = '<%# Eval("LogotipoID", GetUrl("Imagen.aspx?logotipoID={0}")) %>'>
                                            </asp:Image>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    
								</Columns>
							</asp:datagrid>

						</TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!---
------------------ FIN Panel Resultado ------------
--><br>
			<br>
		</form>
	</body>
</HTML>
