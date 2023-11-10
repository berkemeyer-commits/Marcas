<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.repBoletinGeneral" CodeFile="repBoletinGeneral.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Boletin</title>
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
  window.opener.document.Form1.repInstruccionID.value = pCod ;
  window.opener.focus();
  window.close();
}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('btnGenerar');">
			<!-- 

   Cabecera 
		
   /-->
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<span class="titulo">Boletín General</span><br>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnlBuscar" runat="server">
				<P class="subtitulo">Filtros</P>
				<TABLE class="infoMacro" id="tblBuscar" cellSpacing="0" cellPadding="0" width="700" border="0">
					<TR>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px; HEIGHT: 24px" width="498">
							<asp:Label id="lblBoletinNro" runat="server" CssClass="Etiqueta2" Width="85px">Boletín Nro.</asp:Label>
							<asp:textbox id="txtBoletinNro" runat="server" Width="96px"></asp:textbox>
							<asp:Label id="Label3" runat="server" CssClass="Etiqueta2" Width="28px">
         Año
        </asp:Label>
							<asp:textbox id="txtAnio" runat="server" Width="96px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px; HEIGHT: 24px" width="498">
							<asp:Label id="Label6" runat="server" CssClass="Etiqueta2" Width="85px">Carpeta Nro.</asp:Label>
							<asp:textbox id="txtCarpeta" runat="server" Width="96px"></asp:textbox>
							<asp:Label id="Label7" runat="server" CssClass="Etiqueta2" Width="28px">
         Año
        </asp:Label>
							<asp:textbox id="txtProcAnio" runat="server" Width="96px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px" width="498">
							<asp:Label id="lblFecha" runat="server" CssClass="Etiqueta2" Width="85px">Fecha Solic.</asp:Label>
							<asp:textbox id="txtFecha" runat="server" Width="224px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px; HEIGHT: 20px" width="498">
							<asp:Label id="lblbActa" runat="server" CssClass="Etiqueta2" Width="85px">Acta Nro.</asp:Label>
							<asp:textbox id="txtActa" runat="server" Width="192px"></asp:textbox>
							<asp:Label id="Label5" runat="server" CssClass="Etiqueta2" Width="56px">
         Año
        </asp:Label>
							<asp:textbox id="txtActaAnio" runat="server" Width="96px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px; HEIGHT: 20px" width="498">
							<asp:Label id="lblDenom" runat="server" CssClass="Etiqueta2" Width="85px">Denominación</asp:Label>
							<asp:textbox id="txtDenom" runat="server" Width="192px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px; HEIGHT: 20px" width="498">
							<asp:Label id="Label15" runat="server" CssClass="Etiqueta2" Width="85px">Tipo (D/F/M)</asp:Label>
							<asp:textbox id="txtTipoMarca" runat="server" Width="64px"></asp:textbox>
							<asp:Label id="Label14" runat="server" CssClass="Etiqueta2" Width="55px">Clase</asp:Label>
							<asp:textbox id="txtClase" runat="server" Width="96px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px; HEIGHT: 18px" width="498">
							<asp:Label id="lblPropietario" runat="server" CssClass="Etiqueta2" Width="85px">Solicitante</asp:Label>
							<asp:textbox id="txtPropietario" runat="server" Width="192px"></asp:textbox>
							<asp:Label id="Label2" runat="server" CssClass="Etiqueta2" Width="55px">
         Pais
        </asp:Label>
							<asp:textbox id="txtPais" runat="server" Width="96px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px" width="498">
							<asp:Label id="lblAgLoc" runat="server" CssClass="Etiqueta2" Width="85px">Agente Loc.</asp:Label>
							<asp:textbox id="txtAgLoc" runat="server" Width="96px"></asp:textbox>
							<asp:LinkButton id="lbtnAgBerke" runat="server" onclick="lbtnAgBerke_Click"><-Agentes Berke</asp:LinkButton></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px" width="498">
							<asp:Label id="Label4" runat="server" CssClass="Etiqueta2" Width="85px">Trámite</asp:Label>
							<asp:textbox id="txtTramite" runat="server" Width="96px"></asp:textbox>
							<asp:Button id="btnTrSug" runat="server" Width="56px" Text="Sugerir" onclick="btnTrSug_Click"></asp:Button></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px" width="498">
							<asp:Label id="lblTrSug" runat="server" Visible="False"></asp:Label></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px; HEIGHT: 20px" width="498">
							<asp:Label id="Label9" runat="server" CssClass="Etiqueta2" Width="85px">Ref. Acta Nro.</asp:Label>
							<asp:textbox id="txtRefActaNro" runat="server" Width="192px"></asp:textbox>
							<asp:Label id="Label10" runat="server" CssClass="Etiqueta2" Width="55px">
         Año
        </asp:Label>
							<asp:textbox id="txtRefActaAnho" runat="server" Width="96px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px; HEIGHT: 20px" width="498">
							<asp:Label id="Label11" runat="server" CssClass="Etiqueta2" Width="85px">Ref. Reg. Nro.</asp:Label>
							<asp:textbox id="txtRefRegNro" runat="server" Width="192px"></asp:textbox>
							<asp:Label id="Label12" runat="server" CssClass="Etiqueta2" Width="55px">
         Año
        </asp:Label>
							<asp:textbox id="txtRefRegAnho" runat="server" Width="96px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px; HEIGHT: 20px" width="498">
							<asp:Label id="Label13" runat="server" CssClass="Etiqueta2" Width="85px">Observacion</asp:Label>
							<asp:textbox id="txtObservacion" runat="server" Width="192px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 498px" width="498">
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="300" border="0">
								<TR>
									<TD vAlign="middle">
										<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="85px" Height="23px">Trámites</asp:Label></TD>
									<TD vAlign="middle">
										<asp:RadioButtonList id="rbTramite" runat="server" BorderColor="White" BorderWidth="1px" BorderStyle="Solid"
											RepeatDirection="Horizontal">
											<asp:ListItem Value="1" Selected="True">Todos</asp:ListItem>
											<asp:ListItem Value="2">Nuestros</asp:ListItem>
											<asp:ListItem Value="3">Terceros</asp:ListItem>
										</asp:RadioButtonList></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>&nbsp;</TD>
					</TR>
				</TABLE>
				<P></P>
				<TABLE class="tbl" style="MARGIN-LEFT: 5px" cellSpacing="0" cellPadding="0" width="700"
					border="0">
					<TR>
						<TD width="19%">&nbsp;<STRONG>&nbsp;&nbsp; Ordenar por :</STRONG></TD>
						<TD style="WIDTH: 25.43%">
							<asp:Label id="lblCriterio1" runat="server" CssClass="Etiqueta2" Width="58px">
         Campo 1
        </asp:Label>
							<asp:DropDownList id="ddOrden1" runat="server" Width="106px">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="Fecha">Fecha</asp:ListItem>
								<asp:ListItem Value="Expediente">Expediente</asp:ListItem>
								<asp:ListItem Value="Clase">Clase</asp:ListItem>
								<asp:ListItem Value="Denominacion">Denominacion</asp:ListItem>
								<asp:ListItem Value="Solicitante">Solicitante</asp:ListItem>
								<asp:ListItem Value="Agente">Agente</asp:ListItem>
							</asp:DropDownList></TD>
						<TD width="66%">&nbsp;&nbsp;
							<asp:Label id="Label8" runat="server" CssClass="Etiqueta2" Width="58px">
        Campo 2
        </asp:Label>
							<asp:DropDownList id="ddOrden2" runat="server" Width="106px">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="Fecha">Fecha</asp:ListItem>
								<asp:ListItem Value="Expediente">Expediente</asp:ListItem>
								<asp:ListItem Value="Clase">Clase</asp:ListItem>
								<asp:ListItem Value="Denominacion">Denominacion</asp:ListItem>
								<asp:ListItem Value="Solicitante">Solicitante</asp:ListItem>
								<asp:ListItem Value="Agente">Agente</asp:ListItem>
							</asp:DropDownList></TD>
					</TR>
				</TABLE>
				<TABLE style="WIDTH: 786px; HEIGHT: 51px" cellSpacing="0" cellPadding="0" width="786" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 168px; HEIGHT: 43px">
							<P align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:button id="btnGenerar" runat="server" CssClass="Button" Width="110px" Height="22px" Text="Generar Reporte"
									Font-Bold="True" onclick="btnGenerar_Click"></asp:button></P>
						</TD>
						<TD style="WIDTH: 240px; HEIGHT: 43px">
							<P align="right">
								<asp:button id="btnGenExcel" runat="server" CssClass="Button" Width="132px" Height="22px" Text="Generar Documento"
									Font-Bold="True" onclick="btnGenExcel_Click"></asp:button></P>
						</TD>
					</TR> <!--  
       endregion: Boton Buscar 
      /--> <!--							
      region:  Mensaje /
      -->
					<TR>
						<TD colSpan="7">&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:label id="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="X-Small"></asp:label></TD>
					</TR> <!-- 			
      endregion: Mensaje  		
      /--></TABLE>
			</asp:panel>
			<P>&nbsp;</P>
			<!---
------------------ PANEL RESULTADO ------------
-->
			<!---
------------------ FIN Panel Resultado ------------
--></form>
	</body>
</HTML>
