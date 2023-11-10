<%@ Reference Page="~/home/limitada.aspx" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#"   smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.MarcaModif" CodeFile="MarcaModif.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar MarcaModif</title>
		<meta content="True" name="vs_snapToGrid">
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
  window.opener.document.Form1.MarcaID.value = pCod ;
  window.opener.focus();
  window.close();
}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<!-- 

   Cabecera 
		
   /--><BR>
			<P class="titulo">Modificar Marca</P>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnltodo" runat="server">
				<asp:panel id="pnlMarca" runat="server" CssClass="tbl_recuadro">
					<TABLE class="tbl" id="tblMarca">
						<TR height="30">
							<TD>
								<asp:Label id="Label28" runat="server" CssClass="Etiqueta1" Width="88px" Font-Underline="True"
									Font-Size="X-Small" Font-Bold="True">Marca&nbsp;&nbsp;</asp:Label></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblID" runat="server" CssClass="Etiqueta2" Width="90px">
          ID:       </asp:Label></TD>
							<TD>
								<asp:textbox id="txtID" runat="server" Width="60px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblDenominacion" runat="server" CssClass="Etiqueta2" Width="90px">Denominacion:        </asp:Label></TD>
							<TD>
								<asp:textbox id="txtDenominacion" runat="server" Width="210px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblDenominacionClave" runat="server" CssClass="Etiqueta2" Width="90px">Denom. Clave:</asp:Label></TD>
							<TD>
								<asp:textbox id="txtDenominacionClave" runat="server" Width="210px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 79px">
								<asp:Label id="Label34" runat="server" CssClass="Etiqueta2" Width="72px">Tipo</asp:Label></TD>
							<TD>
								<asp:CheckBoxList id="chkTipo" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="1">Denominativa</asp:ListItem>
									<asp:ListItem Value="2">Figurativa</asp:ListItem>
									<asp:ListItem Value="3">Mixta</asp:ListItem>
                                    <asp:ListItem Value="4">Tridimensional</asp:ListItem>
                                    <asp:ListItem Value="5">Olfativa</asp:ListItem>
                                    <asp:ListItem Value="6">Sonora</asp:ListItem>
								</asp:CheckBoxList></TD>
						</TR>
                        <%--<TR>
										<TD style="WIDTH: 79px"></TD>
										<TD>
											<asp:CheckBoxList id="chkTipo1" runat="server" RepeatDirection="Horizontal">
												<asp:ListItem Value="T">Tridimensional</asp:ListItem>
                                                <asp:ListItem Value="O">Olfativa&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="S">Sonora</asp:ListItem>
                                            </asp:CheckBoxList></TD>
                                        	
									</TR>--%>
						<TR>
							<TD style="HEIGHT: 9px">
								<asp:Label id="Label33" runat="server" CssClass="Etiqueta2" Width="90px">Clase</asp:Label></TD>
							<TD>
								<CUSTOM:DROPDOWN id="ddlClaseID" runat="server" Width="136px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblLimitada" runat="server" CssClass="Etiqueta2" Width="90px">
                                 Limitada </asp:Label></TD>
							<TD style="HEIGHT: 34px">
								<asp:RadioButtonList id="rbLimitada" runat="server" Width="120px" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
									BorderWidth="1px" BorderStyle="Solid">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblClaseDescripEsp" runat="server" CssClass="Etiqueta2" Width="90px" Height="49px">ClaseDescripEsp:       </asp:Label></TD>
							<TD>
								<asp:textbox id="txtClaseDescripEsp" runat="server" Width="552px" Height="54px" TextMode="MultiLine"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 76px; HEIGHT: 34px">
								<asp:Label id="lblMarcaNuestra" runat="server" CssClass="Etiqueta2" Width="72px">Nuestra</asp:Label></TD>
							<TD style="HEIGHT: 34px">
								<asp:RadioButtonList id="rbMarcaNuestra" runat="server" Width="120px" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
									BorderWidth="1px" BorderStyle="Solid">
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
									RepeatDirection="Horizontal" BorderWidth="1px" BorderStyle="Solid">
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
									BorderWidth="1px" BorderStyle="Solid">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 76px; HEIGHT: 1px">
								<asp:Label id="Label35" runat="server" CssClass="Etiqueta2" Width="72px">Stand By</asp:Label></TD>
							<TD style="HEIGHT: 1px">
								<asp:RadioButtonList id="rbStandBy" runat="server" Width="104px" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
									BorderWidth="1px" BorderStyle="Solid">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 76px">
								<asp:Label id="Label36" runat="server" CssClass="Etiqueta2" Width="72px">Sustituida</asp:Label></TD>
							<TD>
								<asp:RadioButtonList id="rbSustituida" runat="server" Width="120px" ToolTip="Tipo de expediente" RepeatDirection="Horizontal"
									BorderWidth="1px" BorderStyle="Solid">
									<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:RadioButtonList></TD>
						</TR>
					</TABLE>
					<TABLE class="tbl" id="tblRegistro">
						<TR height="30">
							<TD>
								<asp:Label id="Label19" runat="server" CssClass="Etiqueta1" Width="88px" Font-Underline="True"
									Font-Size="X-Small" Font-Bold="True">Registro&nbsp;&nbsp;</asp:Label>
								<asp:Label id="lblMarcaRegRenID" runat="server" CssClass="Etiqueta2" Width="80px"> ID</asp:Label></TD>
						</TR>
						<TR height="30">
							<TD>
								<asp:Label id="Label4" runat="server" CssClass="Etiqueta2" Width="90px">Número:</asp:Label>
								<asp:TextBox id="txtRegNro" runat="server" Width="72px"></asp:TextBox>
								<asp:Label id="Label5" runat="server" CssClass="Etiqueta2" Width="24px">Año</asp:Label>
								<asp:TextBox id="txtRegAnio" runat="server" Width="48px"></asp:TextBox>
								<asp:Label id="Label6" runat="server" CssClass="Etiqueta2" Width="72px">Concesión</asp:Label>
								<asp:TextBox id="txtRegConcesion" runat="server" Width="80px"></asp:TextBox>
								<asp:Label id="Label7" runat="server" CssClass="Etiqueta2" Width="50px">Vence</asp:Label>
								<asp:TextBox id="txtRegVencim" runat="server" Width="80px"></asp:TextBox>
								<asp:Label id="Label8" runat="server" CssClass="Etiqueta2" Width="50px">Vigente</asp:Label>
								<asp:DropDownList id="ddlRegVigente" runat="server" Width="40px">
									<asp:ListItem></asp:ListItem>
									<asp:ListItem Value="Si">Si</asp:ListItem>
									<asp:ListItem Value="No">No</asp:ListItem>
								</asp:DropDownList></TD>
						</TR>
					</TABLE>
				</asp:panel>
				<HR>
				<asp:panel id="pnlDatosComunes" runat="server">
					<TABLE class="tbl" id="tblCambioPropietario">
						<TR height="30">
							<TD>
								<asp:Label id="Label20" runat="server" CssClass="Etiqueta1" Width="144px" Font-Underline="True"
									Font-Size="X-Small" Font-Bold="True">Datos Comunes</asp:Label></TD>
							<TD class="borde_izq" colSpan="2">
								<asp:Label id="Label30" runat="server" CssClass="Etiqueta1" Width="144px" Font-Underline="True"
									Font-Size="X-Small" Font-Bold="True">Modificar</asp:Label></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 477px">
								<asp:Label id="lblPropietarID" runat="server" CssClass="Etiqueta2" Width="90px">Propietario:      </asp:Label>
								<asp:Label id="lblPropietarioNombre" runat="server" Width="352px">lblPropietarioNombre</asp:Label></TD>
							<TD class="borde_izq" style="WIDTH: 113px">
								<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="16px">IDs</asp:Label>
								<asp:textbox id="txtPropietarioID" runat="server" Width="80px" ToolTip="Lista separada por comas"></asp:textbox></TD>
							<TD>
								<asp:CheckBox id="chkModExpePropietario" runat="server" Text="Modif.Exped."></asp:CheckBox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 477px; HEIGHT: 22px">
								<asp:Label id="Label3" runat="server" CssClass="Etiqueta2" Width="90px">Cliente:      </asp:Label>
								<asp:Label id="lblClienteNombre" runat="server" Width="376px">lblClienteNombre</asp:Label></TD>
							<TD class="borde_izq" style="WIDTH: 113px; HEIGHT: 21px">
								<asp:Label id="Label15" runat="server" CssClass="Etiqueta2" Width="16px">ID&nbsp;</asp:Label>
								<asp:textbox id="txtClienteID" runat="server" Width="60px"></asp:textbox></TD>
							<TD style="HEIGHT: 21px">
								<asp:CheckBox id="chkModExpeCliente" runat="server" Text="Modif.Exped."></asp:CheckBox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 477px">
								<asp:Label id="lblAgenteLocalID" runat="server" CssClass="Etiqueta2" Width="90px">Agente Local:        </asp:Label>
								<asp:Label id="lblAgLocalNombre" runat="server" Width="376px">lblAgLocalNombre</asp:Label></TD>
							<TD class="borde_izq" style="WIDTH: 113px">
								<asp:Label id="Label2" runat="server" CssClass="Etiqueta2" Width="16px">ID&nbsp;</asp:Label>
								<asp:textbox id="txtAgLocID" runat="server" Width="60px"></asp:textbox></TD>
							<TD>
								<asp:CheckBox id="chkModExpeAgLocal" runat="server" Text="Modif.Exped."></asp:CheckBox></TD>
						</TR>
					</TABLE>
				</asp:panel>
				<HR>
				<asp:panel id="pnlDatPropEnMarca" runat="server">
					<TABLE class="tbl" id="tblCambioPropietariotxt">
						<TR height="30">
							<TD style="WIDTH: 411px">
								<asp:Label id="Label27" runat="server" CssClass="Etiqueta1" Width="232px" Font-Underline="True"
									Font-Size="X-Small" Font-Bold="True">Datos de Propietario en Marca</asp:Label></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 411px">
								<asp:Label id="Label24" runat="server" CssClass="Etiqueta2" Width="90px">Propietario: </asp:Label>
								<asp:textbox id="txtPropietarioTexto" runat="server" Width="250px"></asp:textbox></TD>
							<TD rowSpan="3"><B>Corresponden a los datos de la marca &nbsp;que podrían diferir de 
									los datos del Propietario.</B>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 411px">
								<asp:Label id="Label25" runat="server" CssClass="Etiqueta2" Width="90px">Dirección: </asp:Label>
								<asp:textbox id="txtDireccionTexto" runat="server" Width="250px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 411px">
								<asp:Label id="Label26" runat="server" CssClass="Etiqueta2" Width="90px">País: </asp:Label>
								<asp:textbox id="txtPaisTexto" runat="server" Width="250px"></asp:textbox></TD>
						</TR>
					</TABLE>
				</asp:panel>
				<HR>
				<asp:panel id="pnlExpediente" runat="server">
					<TABLE class="tbl" id="tblExpediente">
						<TR height="30">
							<TD>
								<asp:Label id="Label21" runat="server" CssClass="Etiqueta1" Width="112px" Font-Underline="True"
									Font-Size="X-Small" Font-Bold="True">Expediente&nbsp;&nbsp;</asp:Label>
								<asp:Label id="lblExpedID" runat="server" Width="72px">ID: XX</asp:Label>&nbsp;&nbsp;
								<asp:Label id="Label32" runat="server" Width="56px">Trámite:</asp:Label>
								<asp:Label id="lblDescTramite" runat="server">TRAMITEDESC</asp:Label></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblActa" runat="server" CssClass="Etiqueta2" Width="90px">Acta Nro/Año:</asp:Label>
								<asp:textbox id="txtActaNro" runat="server" Width="80px"></asp:textbox>
								<asp:Label id="Label9" runat="server" CssClass="Etiqueta2" Width="8px">/</asp:Label>
								<asp:textbox id="txtActaAnio" runat="server" Width="60px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="Label10" runat="server" CssClass="Etiqueta2" Width="90px">Public. Pág/Año:</asp:Label>
								<asp:textbox id="txtPubPag" runat="server" Width="80px"></asp:textbox>
								<asp:Label id="Label11" runat="server" CssClass="Etiqueta2" Width="8px">/</asp:Label>
								<asp:textbox id="txtPubAnio" runat="server" Width="60px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="Label12" runat="server" CssClass="Etiqueta2" Width="90px">Archivo Bib/Exp:</asp:Label>
								<asp:textbox id="txtBib" runat="server" Width="80px"></asp:textbox>
								<asp:Label id="Label13" runat="server" CssClass="Etiqueta2" Width="8px">/</asp:Label>
								<asp:textbox id="txtExp" runat="server" Width="60px"></asp:textbox></TD>
						</TR>
						<TR>
							<TD>
								<TABLE class="tbl">
									<TR>
										<TD>
											<asp:Label id="lblTituloPoder" runat="server" CssClass="Etiqueta1" Width="112px" Font-Size="XX-Small"
												Font-Bold="True">Etiqueta</asp:Label><BR>
											<asp:Label id="Label14" runat="server" CssClass="Etiqueta2" Width="90px">Poder:</asp:Label>
											<asp:Label id="lblPoderNombre" runat="server" Width="216px">lblPoderNombre</asp:Label>
											<asp:Label id="Label17" runat="server" CssClass="Etiqueta2" Width="16px">ID</asp:Label>
											<asp:textbox id="txtPoderID" runat="server" Width="60px"></asp:textbox></TD>
										<TD class="borde_izq">
											<asp:Panel id="pnlTienePoder" runat="server" Height="36px" Visible="False">&nbsp; 
<asp:Label id="Label31" runat="server" CssClass="Etiqueta1" Width="168px" Font-Size="XX-Small"
													Font-Bold="True">Pasar a derecho Propio</asp:Label><BR>
<asp:Label id="Label29" runat="server" Width="105px">&nbsp;&nbsp;&nbsp;Id. Propietario</asp:Label>
<asp:TextBox id="txtBoxIdPropPaso" runat="server" Width="88px"></asp:TextBox></asp:Panel></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 2px"></TD>
						</TR>
					</TABLE>
				</asp:panel>
				<HR>
				<asp:Panel id="pnlEnlaces" runat="server">
					<TABLE class="tbl" id="tblDatosDeEnlace">
						<TR height="30">
							<TD>
								<asp:Label id="Label22" runat="server" CssClass="Etiqueta1" Width="128px" Font-Underline="True"
									Font-Size="X-Small" Font-Bold="True" Visible="False">Enlaces &nbsp;&nbsp;</asp:Label></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="lblExpedienteVigenteID" runat="server" CssClass="Etiqueta2" Width="176px" Visible="False"><b>
										MARCA</b>:   &nbsp;&nbsp;Expe. Vigente ID</asp:Label>
								<asp:textbox id="txtExpedienteVigenteID" runat="server" Width="60px" Visible="False"></asp:textbox>
								<asp:Label id="lbl1" runat="server" CssClass="Etiqueta2" Width="90px" Visible="False">
          MarcaRegRenID
        </asp:Label>
								<asp:textbox id="txtMarcaRegRenID" runat="server" Width="60px" Visible="False"></asp:textbox></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="Label18" runat="server" CssClass="Etiqueta2" Width="176px" Visible="False"><b>
										EXPEDIENTE</b>:  &nbsp; &nbsp; &nbsp;&nbsp;Marca ID</asp:Label>
								<asp:textbox id="txtExpeMarcaID" runat="server" Width="60px" Visible="False"></asp:textbox>
								<asp:Label id="Label16" runat="server" CssClass="Etiqueta2" Width="90px" Visible="False">
          MarcaRegRenID
        </asp:Label>
								<asp:textbox id="txtExpeRegRenID" runat="server" Width="60px" Visible="False"></asp:textbox>
								<asp:Label id="Label23" runat="server" CssClass="Etiqueta2" Width="90px" Visible="False">Exped.Padre</asp:Label>
								<asp:textbox id="txtExpedPadre" runat="server" Width="60px" Visible="False"></asp:textbox></TD>
						</TR>
						<TR>
							<TD></TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<P></P>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 276px"></TD>
						<TD>
							<asp:button id="btnGrabar" runat="server" CssClass="Button" Font-Bold="True" Text="GRABAR" onclick="btnGrabar_Click"></asp:button></TD>
					</TR> <!--  
       endregion: Boton Buscar 
      /--> <!--							
      region:  Mensaje /
      -->
					<TR>
						<TD colSpan="7"></TD>
					</TR> <!-- 			
      endregion: Mensaje  		
      /--></TABLE>
			</asp:panel>
			<!---
------------------ PANEL RESULTADO ------------
-->
			<!---
------------------ FIN Panel Resultado ------------
--></form>
	</body>
</HTML>
