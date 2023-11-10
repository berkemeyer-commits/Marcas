<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.ExpeMarcaCambioSitConsulta" CodeFile="ExpeMarcaCambioSitConsulta.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Cambios de Situación de Expedientes de Marcas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
		<script language="JavaScript">
function restablecer(pCod){
  /* alert('ID a transferir ' + pCod ); */
  window.opener.document.Form1.ExpeMarcaCambioSitID.value = pCod ;
  window.opener.focus();
  window.close();
}



		</script>
	</HEAD>
	<body>
		<form onkeypress="form_onEnter('btBuscar');" id="Form1" method="post" runat="server">
			<!-- 

   Cabecera 
		
   /-->
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo">Consultar Cambios de Situación de Expedientes de Marcas</P>
			<!-- 

   Fin Cabecera


   /-->
			<div class="hideTitle" title="Ocultar/visualizar formulario de búsqueda" style="MARGIN-LEFT: 5px; WIDTH: 98%"
				onclick="closeDiv('pnlBuscar')">Criterio de búsqueda
			</div>
			<div id="formBusqueda"><asp:panel id="pnlBuscar" runat="server" Width="98%">
                    <TABLE class="infoMacro" id="TABLE1" cellSpacing="0" cellPadding="0" width="100%" border="0">
                        <tr>
                            <td style="width: 10%"><asp:Label id="lblSitFecha" runat="server" Width="168px">Situaci&oacute;n Fecha</asp:Label></td>
                            <td style="width: 20%"><asp:textbox id="txtSitFecha" runat="server" Width="168px"></asp:textbox></td>
                            <td style="width: 10%"></td>
                            <td style="width: 20%"></td>
                            <td style="width: 20%"></td>
                            <td style="width: 20%"></td>
                        </tr>
                        <tr>
                            <td style="width: 10%"><asp:Label id="lblAltaFecha_min" runat="server" Width="90px">Fecha grabación</asp:Label></td>
                            <td style="width: 20%"><asp:textbox id="txtAltaFecha_min" runat="server" Width="168px"></asp:textbox></td>
                            <td style="width: 10%"><asp:Label id="Label3" runat="server" Width="96px">Vencim.Plazo</asp:Label></td>
                            <td style="width: 20%"><asp:textbox id="txtVencimPlazo" runat="server" Width="168px"></asp:textbox></td>
                            <td style="width: 20%"><asp:CheckBox id="chkSitActual" runat="server" TextAlign="Right" Text="Última situación de la marca" ToolTip="Mostrar sólo la situacion actual"></asp:CheckBox>&nbsp;
                            </td>
                            <td style="width: 20%">
                                <asp:CheckBox id="chkUltimaSituacion" runat="server" TextAlign="Right" Text="Última situación"></asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%"><asp:Label id="lblTramiteID" runat="server" Width="90px">Tr&aacute;mite</asp:Label></td>
                            <td style="width: 20%"><CUSTOM:DROPDOWN id="ddlTramiteID" runat="server" Width="227px" AutoPostBack="True"></CUSTOM:DROPDOWN></td>
                            <td style="width: 10%"><asp:Label id="Label6" runat="server">Ids</asp:Label></td>
                            <td style="width: 20%"><asp:TextBox id="txtTrmList" runat="server" Width="168px"></asp:TextBox></td>
                            <td style="width: 20%"><asp:CheckBox id="chkMarcasInactivas" runat="server" TextAlign="Right" Text="Incluir Marcas Inactivas" Checked="True"></asp:CheckBox>
                            </td>
                            <td style="width: 20%"><asp:CheckBox id="chkAbandonadas" runat="server" TextAlign="Right" Text="NO Incluir Abandonadas" Checked="True"></asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%"><asp:Label id="lblTramiteSitID" runat="server" Width="90px">Situaci&oacute;n</asp:Label></td>
                            <td style="width: 20%"><CUSTOM:DROPDOWN id="ddlTramiteSitID" runat="server" Width="227px" AutoPostBack="False"></CUSTOM:DROPDOWN></td>
                            <td style="width: 10%"></td>
                            <td style="width: 20%"></td>
                            <td style="width: 20%"><asp:CheckBox id="chkObs" runat="server" TextAlign="Right" Text="Sólo con Observación"></asp:CheckBox></td>
                            <td style="width: 20%"></td>
                        </tr>
                        <tr>
                            <td style="width: 10%"><asp:Label id="lblDenominacion" runat="server" Width="90px">Denominacion</asp:Label></td>
                            <td style="width: 20%"><asp:textbox id="txtDenominacion" runat="server" Width="192px"></asp:textbox></td>
                            <td style="width: 10%"><asp:Label id="Label7" runat="server">Clase</asp:Label></td>
                            <td style="width: 20%"><asp:TextBox id="txtClaseNro" runat="server" Width="88px"></asp:TextBox></td>
                            <td style="width: 20%"><asp:Label id="lblExpePropio" runat="server" Width="130px">Expedientes Nuestros</asp:Label></td>
                            <td style="width: 20%">
                                <asp:RadioButtonList id="rbExpePropio" runat="server" Width="100%" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
									BorderStyle="Solid" BorderWidth="0px">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%"><asp:Label id="lblActaNro_min" runat="server" Width="90px">Acta</asp:Label></td>
                            <td style="width: 20%"><asp:textbox id="txtActaNro_min" runat="server" Width="168px"></asp:textbox></td>
                            <td style="width: 10%"><asp:Label id="lblActaAnio" runat="server" Width="24px">Año</asp:Label></td>
                            <td style="width: 20%"><asp:textbox id="txtActaAnio" runat="server" Width="60px"></asp:textbox></td>
                            <td style="width: 20%"><asp:Label id="Label11" runat="server" Width="130px">Marcas Nuestras</asp:Label></td>
                            <td style="width: 20%">
                                <asp:RadioButtonList id="rbMarcaNuestra" runat="server" Width="100%" ToolTip="Tipo de Marca" RepeatDirection="Horizontal"
									BorderStyle="Solid" BorderWidth="0px">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%"><asp:Label id="lblRegistroNro_min" runat="server" Width="90px">Registro</asp:Label></td>
                            <td style="width: 20%"><asp:textbox id="txtRegistroNro_min" runat="server" Width="168px"></asp:textbox></td>
                            <td style="width: 10%"><asp:Label id="lblRegistroAnio" runat="server" Width="24px">Año</asp:Label></td>
                            <td style="width: 20%"><asp:textbox id="txtRegistroAnio" runat="server" Width="60px"></asp:textbox></td>
                            <td style="width: 20%"><asp:Label id="lblSustituida" runat="server" Width="120px">Vigilada</asp:Label></td>
                            <td style="width: 20%">
                                <asp:RadioButtonList id="rbVigilada" runat="server" Width="100%" ToolTip="Vigilada" RepeatDirection="Horizontal"
									BorderStyle="Solid" BorderWidth="0px" Height="7px">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%"><asp:Label id="lblClienteID" runat="server" Width="90px">Cliente</asp:Label></td>
                            <td style="width: 20%"><asp:textbox id="txtClienteID" runat="server" Width="72px"></asp:textbox></td>
                            <td style="width: 10%"><asp:Label id="Label8" runat="server" Width="96px"><- ID  Nombre -></asp:Label></td>
                            <td style="width: 20%"><asp:textbox id="txtClienteNombre" runat="server" Width="167px"></asp:textbox></td>
                            <td style="width: 20%"><asp:Label id="Label2" runat="server" Width="120px">En Trámite</asp:Label></td>
                            <td style="width: 20%">
                                <asp:RadioButtonList id="rbEnTramite" runat="server" Width="100%" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
									BorderStyle="Solid" BorderWidth="0px" Height="7px">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%"><asp:Label id="lblExpeID_min" runat="server" Width="90px">Expediente ID</asp:Label></td>
                            <td style="width: 20%"><asp:textbox id="txtExpeID_min" runat="server" Width="168px"></asp:textbox></td>
                            <td style="width: 10%"><asp:Label id="Label4" runat="server" Width="56px">Instrucción</asp:Label></td>
                            <td style="width: 20%">
                                <asp:textbox id="txtInstruc" runat="server" Width="82px"></asp:textbox>
                                <CUSTOM:DROPDOWN id="ddlInstrucFunc" runat="server" Width="144px" AutoPostBack="False"></CUSTOM:DROPDOWN>
                            </td>
                            <td style="width: 20%"><asp:Label id="Label10" runat="server" Width="120px">Sustituidas</asp:Label></td>
                            <td style="width: 20%">
                                <asp:RadioButtonList id="rbSustituida" runat="server" Width="100%" RepeatDirection="Horizontal" BorderStyle="Solid"
									BorderWidth="0px" Height="7px">
									<asp:ListItem Value=" ">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No" Selected="True">No</asp:ListItem>
								</asp:RadioButtonList>
                            </td>
                        </tr>
                        <%--<TR>
							<TD>
								
								</TD>
							<TD>&nbsp;
								</TD>
						</TR>--%>
                        <tr>
                            <td style="width: 10%"><asp:Label id="Label1" runat="server" Width="90px">Usuario</asp:Label></td>
                            <td style="width: 20%"><CUSTOM:DROPDOWN id="ddlFuncionario" runat="server" Width="227px" AutoPostBack="False"></CUSTOM:DROPDOWN></td>
                            <td style="width: 10%"></td>
                            <td style="width: 20%"></td>
                            <td style="width: 20%"><asp:Label id="Label9" runat="server" Width="120px">En Stand By</asp:Label></td>
                            <td style="width: 20%">
                                <asp:RadioButtonList id="rbEnStandBy" runat="server" Width="100%" RepeatDirection="Horizontal" BorderStyle="Solid"
									BorderWidth="0px" Height="7px">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList>
                            </td>
                        </tr>
                    </TABLE>
					<%--<TABLE class="infoMacro" id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
                        <TR>
							<TD style="WIDTH: 385px">
								<asp:Label id="lblSitFecha" runat="server" Width="90px" CssClass="Etiqueta2">Situacion Fecha</asp:Label>
								<asp:textbox id="txtSitFecha" runat="server" Width="104px"></asp:textbox>
							<TD style="WIDTH: 154px">&nbsp;
							</TD>
							<TD></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblAltaFecha_min" runat="server" Width="90px" CssClass="Etiqueta2">Fecha grabación</asp:Label>
								<asp:textbox id="txtAltaFecha_min" runat="server" Width="104px"></asp:textbox>
								<asp:Label id="Label3" runat="server" Width="96px" CssClass="Etiqueta2">Vencim.Plazo</asp:Label>
								<asp:textbox id="txtVencimPlazo" runat="server" Width="96px"></asp:textbox></TD>
							<TD>&nbsp;
								<asp:CheckBox id="chkSitActual" runat="server" TextAlign="Left" Text="Última situación" ToolTip="Mostrar sólo la situacion actual"></asp:CheckBox>&nbsp;
								<BR>
								&nbsp;
								<asp:Label id="Label11" runat="server">de la marca</asp:Label></TD>
							<TD>
								<asp:CheckBox id="chkUltimaSituacion" runat="server" TextAlign="Left" Text="Última situación"></asp:CheckBox><BR>
								<asp:Label id="Label12" runat="server">(Sólo última publicación)</asp:Label></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblTramiteID" runat="server" Width="90px" CssClass="Etiqueta2">
         Tramite
       </asp:Label>
								<CUSTOM:DROPDOWN id="ddlTramiteID" runat="server" Width="200px" AutoPostBack="True"></CUSTOM:DROPDOWN>
								<asp:Label id="Label6" runat="server">Ids</asp:Label>
								<asp:TextBox id="txtTrmList" runat="server" Width="97px"></asp:TextBox></TD>
							<TD vAlign="middle">
								<asp:CheckBox id="chkMarcasInactivas" runat="server" TextAlign="Left" Text="Incluir Marcas Inactivas"
									Checked="True"></asp:CheckBox>
							<TD></TD>
							</TD></TR>
						<TR>
							<TD>
								<asp:Label id="lblTramiteSitID" runat="server" Width="90px" CssClass="Etiqueta2">
         Situación
       </asp:Label>
								<CUSTOM:DROPDOWN id="ddlTramiteSitID" runat="server" Width="227px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
							<TD vAlign="middle">
								<asp:Label id="lblExpePropio" runat="server" Width="130px" CssClass="Etiqueta2">Expedientes Nuestros</asp:Label></TD>
							<TD>
								<asp:RadioButtonList id="rbExpePropio" runat="server" Width="130px" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
									BorderStyle="Solid" BorderWidth="0px">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblDenominacion" runat="server" Width="90px" CssClass="Etiqueta2">
          Denominacion
        </asp:Label>
								<asp:textbox id="txtDenominacion" runat="server" Width="192px"></asp:textbox>
								<asp:Label id="Label7" runat="server">Clase</asp:Label>
								<asp:TextBox id="txtClaseNro" runat="server" Width="88px"></asp:TextBox></TD>
							<TD>
								<asp:Label id="lblSustituida" runat="server" Width="120px" CssClass="Etiqueta2">Vigilada</asp:Label></TD>
							<TD>
								<asp:RadioButtonList id="rbVigilada" runat="server" Width="32px" ToolTip="Vigilada" RepeatDirection="Horizontal"
									BorderStyle="Solid" BorderWidth="0px" Height="7px">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblActaNro_min" runat="server" Width="90px" CssClass="Etiqueta2">
          Acta
        </asp:Label>
								<asp:textbox id="txtActaNro_min" runat="server" Width="168px"></asp:textbox>
								<asp:Label id="lblActaAnio" runat="server" Width="24px" CssClass="Etiqueta2">
          Año
        </asp:Label>
								<asp:textbox id="txtActaAnio" runat="server" Width="60px"></asp:textbox></TD>
							<TD>&nbsp;
								<asp:Label id="Label2" runat="server" Width="120px" CssClass="Etiqueta2">En Trámite</asp:Label></TD>
							<TD>
								<asp:RadioButtonList id="rbEnTramite" runat="server" Width="32px" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
									BorderStyle="Solid" BorderWidth="0px" Height="7px">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList></TD>
						<TR>
							<TD>
								<asp:Label id="lblRegistroNro_min" runat="server" Width="90px" CssClass="Etiqueta2">
          Registro
        </asp:Label>
								<asp:textbox id="txtRegistroNro_min" runat="server" Width="168px"></asp:textbox>
								<asp:Label id="lblRegistroAnio" runat="server" Width="24px" CssClass="Etiqueta2">
          Año
        </asp:Label>
								<asp:textbox id="txtRegistroAnio" runat="server" Width="60px"></asp:textbox></TD>
							<TD style="WIDTH: 154px; HEIGHT: 39px">
								<asp:Label id="Label10" runat="server" Width="120px" CssClass="Etiqueta2">Sustituidas</asp:Label></TD>
							<TD>
								<asp:RadioButtonList id="rbSustituida" runat="server" Width="32px" RepeatDirection="Horizontal" BorderStyle="Solid"
									BorderWidth="0px" Height="7px">
									<asp:ListItem Value=" ">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No" Selected="True">No</asp:ListItem>
								</asp:RadioButtonList></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblClienteID" runat="server" Width="90px" CssClass="Etiqueta2">
          Cliente
        </asp:Label>
								<asp:textbox id="txtClienteID" runat="server" Width="72px"></asp:textbox>
								<asp:Label id="Label8" runat="server" Width="96px" CssClass="Etiqueta2"><- ID  Nombre -></asp:Label>
								<asp:textbox id="txtClienteNombre" runat="server" Width="167px"></asp:textbox></TD>
							<TD>&nbsp;&nbsp;
								<asp:Label id="Label9" runat="server" Width="120px" CssClass="Etiqueta2">En Stand By</asp:Label></TD>
							<TD>
								<asp:RadioButtonList id="rbEnStandBy" runat="server" Width="32px" RepeatDirection="Horizontal" BorderStyle="Solid"
									BorderWidth="0px" Height="7px">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList>
								<asp:CheckBox id="chkAbandonadas" runat="server" TextAlign="Left" Text="NO Incluir Abandonadas"
									Checked="True"></asp:CheckBox></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblExpeID_min" runat="server" Width="90px" CssClass="Etiqueta2">
          Expe.ID
        </asp:Label>
								<asp:textbox id="txtExpeID_min" runat="server" Width="168px"></asp:textbox></TD>
							<TD>&nbsp;
								<asp:Label id="Label4" runat="server" Width="56px" CssClass="Etiqueta2">Instrucción</asp:Label>
								<asp:textbox id="txtInstruc" runat="server" Width="82px"></asp:textbox></TD>
							<TD>
								<CUSTOM:DROPDOWN id="ddlInstrucFunc" runat="server" Width="144px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="Label1" runat="server" Width="90px" CssClass="Etiqueta2">
          Usuario
        </asp:Label>
								<CUSTOM:DROPDOWN id="ddlFuncionario" runat="server" Width="227px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
							<TD>&nbsp;
								<asp:CheckBox id="chkObs" runat="server" TextAlign="Left" Text="Sólo con Observación"></asp:CheckBox></TD>
						</TR>
						<TR>
							<TD></TD>
							<TD></TD>
						</TR>
					</TABLE>--%>
					<P></P>
					<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
						<TR>
							<TD style="WIDTH: 219px; HEIGHT: 33px">
								<asp:Label id="Label5" runat="server">Límite </asp:Label>
								<asp:TextBox id="txtLimite" runat="server" Width="56px" Font-Size="XX-Small">1000</asp:TextBox></TD>
							<TD style="WIDTH: 126px; HEIGHT: 33px">
								<P align="left">
									<asp:button id="btBuscar" runat="server" Width="112px" CssClass="Button" Text="Buscar" Height="22px"
										Font-Bold="True" onclick="btBuscar_Click"></asp:button></P>
							</TD>
							<TD style="HEIGHT: 33px">
								<asp:button id="btnGenDocum" runat="server" Width="120px" CssClass="Button" Text="Generar Documento"
									Height="22px" Font-Bold="True" onclick="btnGenDocum_Click"></asp:button>
								<asp:HyperLink id="lnkDocum" runat="server"></asp:HyperLink></TD>
						</TR> <!--  
       endregion: Boton Buscar 
      /--> <!--							
      region:  Mensaje /
      -->
						<TR>
							<TD colSpan="3">
								<asp:label id="lblMensaje" runat="server" Font-Size="X-Small" Font-Bold="True"></asp:label></TD>
						</TR> <!-- 			
      endregion: Mensaje  		
      /--></TABLE>
				</asp:panel></div>
			<P></P>
			<!---
------------------ PANEL RESULTADO ------------
--><asp:panel id="pnlResultado" runat="server" Visible="True">
				<P class="subtitulo">Resultado de la Búsqueda</P>
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
							<asp:datagrid id="dgResult" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False"
								DataKeyField="ExpeSitID" HorizontalAlign="Center">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="ExpeID" HeaderText="Expe.ID"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="E. Nuestro">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id=chkNuestra Checked='<%# GenerateBindString(DataBinder.Eval(Container.DataItem, "expeNuestra"))%>' Runat="server" Enabled="false">
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Vigilada">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id=chkVigilada Checked='<%# GenerateBindString(DataBinder.Eval(Container.DataItem, "Vigilada"))%>' Runat="server" Enabled="false">
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="StandBy">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id=chkStandBy Checked='<%# GenerateBindString(DataBinder.Eval(Container.DataItem, "expeStandBy"))%>' Runat="server" Enabled="false">
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Sustituida">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id=chkSustituida Checked='<%# GenerateBindString(DataBinder.Eval(Container.DataItem, "marSustituida"))%>' Runat="server" Enabled="false">
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Denominacion" HeaderText="Denominacion">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="M. Nuestra">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id=chkNuestra Checked='<%# GenerateBindString(DataBinder.Eval(Container.DataItem, "Nuestra"))%>' Runat="server" Enabled="false">
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ClaseNro" HeaderText="Clase">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SituacionFecha" HeaderText="Fecha" DataFormatString="{0:d}">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="tramiteAbrev" HeaderText="Tram">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SitAbrev" HeaderText="Sit">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SitVencimientoFecha" HeaderText="Venc.Plazo" DataFormatString="{0:d}">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn DataField="Acta" HeaderText="Acta">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Registro" HeaderText="Registro">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="OrdenTrabajo" HeaderText="H.I.">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Obs_CambioSit" HeaderText="Obs.">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ClienteNombre" HeaderText="Cliente"></asp:BoundColumn>
									<asp:BoundColumn DataField="AltaFecha" HeaderText="Alta" DataFormatString="{0:d}">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="UsuarioNombreCorto" HeaderText="Usuario">
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
