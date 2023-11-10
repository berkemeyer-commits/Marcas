<%@ Reference Page="~/home/tramitesvarios.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.OtMarcaConsulta" CodeFile="OtMarcaConsulta.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar HI Marcas</title>
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
  window.opener.document.Form1.OtMarcaID.value = pCod ;
  window.opener.focus();
  window.close();
}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('btBuscar');">
			<!-- 

   Cabecera 
		
   /-->
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo">Consultar Hoja de Inicio de Marcas
			</P>
			<!-- 

   Fin Cabecera

   /-->
			<div style="MARGIN-LEFT:5px; WIDTH:98%" class="hideTitle" title="Ocultar/visualizar formulario de búsqueda"
				onclick="closeDiv('pnlBuscar')">Criterio de búsqueda
			</div>
			<asp:panel id="pnlBuscar" runat="server" Width="98%">
				<TABLE class="infoMacro" id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 390px; HEIGHT: 15px" width="390"></TD>
						<TD style="HEIGHT: 15px"></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px; HEIGHT: 19px" width="390">
							<asp:Label id="lblTramiteID" runat="server" Width="90px" CssClass="Etiqueta2">
         Tramite
       </asp:Label>
							<CUSTOM:DROPDOWN id="ddlTramiteID" runat="server" Width="216px" AutoPostBack="true"></CUSTOM:DROPDOWN></TD>
						<TD style="HEIGHT: 19px">
							<asp:Label id="lblSituacionID" runat="server" Width="90px" CssClass="Etiqueta2">
         Situacion
       </asp:Label>
							<CUSTOM:DROPDOWN id="ddlSituacionID" runat="server" Width="208px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 366px" width="366">
							<asp:Label id="lblOtID_min" runat="server" Width="90px" CssClass="Etiqueta2">
          Id
        </asp:Label>
							<asp:textbox id="txtOtID_min" runat="server" Width="60px"></asp:textbox>
							<asp:Label id="lblOtID_max" runat="server" Width="15px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtOtID_max" runat="server" Width="60px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblClaseID" runat="server" Width="90px" CssClass="Etiqueta2">
         Clase ID
       </asp:Label>
							<CUSTOM:DROPDOWN id="ddlClaseID" runat="server" Width="120px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 366px" width="366">
							<asp:Label id="lblNro_min" runat="server" Width="90px" CssClass="Etiqueta2">
          Nro H.I.
        </asp:Label>
							<asp:textbox id="txtNro_min" runat="server" Width="60px"></asp:textbox>
							<asp:Label id="lblNro_max" runat="server" Width="15px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtNro_max" runat="server" Width="60px"></asp:textbox>
							<asp:Label id="lblAnio" runat="server" Width="32px" CssClass="Etiqueta2">
          Año
        </asp:Label>
							<asp:textbox id="txtAnio" runat="server" Width="60px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblDenominacion" runat="server" Width="88px" CssClass="Etiqueta2">
          Denominacion
        </asp:Label>
							<asp:textbox id="txtDenominacion" runat="server" Width="224px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 366px" width="366">
							<asp:Label id="lblActaNro_min" runat="server" Width="90px" CssClass="Etiqueta2">
          Acta Nro.
        </asp:Label>
							<asp:textbox id="txtActaNro_min" runat="server" Width="60px"></asp:textbox>
							<asp:Label id="lblActaNro_max" runat="server" Width="15px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtActaNro_max" runat="server" Width="60px"></asp:textbox>
							<asp:Label id="lblActaAnio" runat="server" Width="32px" CssClass="Etiqueta2">
          Año
        </asp:Label>
							<asp:textbox id="txtActaAnio" runat="server" Width="60px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblObs" runat="server" Width="88px" CssClass="Etiqueta2">
          Observación
        </asp:Label>
							<asp:textbox id="txtObs" runat="server" Width="224px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 366px" width="366">
							<asp:Label id="lblRegistroNro_min" runat="server" Width="90px" CssClass="Etiqueta2">
          Registro Nro.
        </asp:Label>
							<asp:textbox id="txtRegistroNro_min" runat="server" Width="60px"></asp:textbox>
							<asp:Label id="lblRegistroNro_max" runat="server" Width="15px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtRegistroNro_max" runat="server" Width="60px"></asp:textbox>
							<asp:Label id="lblRegistroAnio" runat="server" Width="32px" CssClass="Etiqueta2">
          Año
        </asp:Label>
							<asp:textbox id="txtRegistroAnio" runat="server" Width="60px"></asp:textbox></TD>
						<TD>&nbsp;
							<asp:Label id="Label3" runat="server" Width="72px" CssClass="Etiqueta2" ForeColor="Firebrick">Corresp. Nro.        </asp:Label>
							<asp:textbox id="txtCorrespNro" runat="server" Width="60px"></asp:textbox>
							<asp:Label id="Label2" runat="server" Width="15px" CssClass="Etiqueta2" ForeColor="#C04000">Año</asp:Label>
							<asp:textbox id="txtCorrespAnio" runat="server" Width="60px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD colSpan="2">
							<asp:Label id="lblClienteID" runat="server" Width="90px" CssClass="Etiqueta2">
          Cliente
        </asp:Label>
							<ecctrl:ecCombo id="cbxClienteID" runat="server" Width="568px" ShowLabel="False"></ecctrl:ecCombo></TD>
					</TR>
					<TR>
						<TD colSpan="2">
							<asp:Label id="lblFuncionarioID" runat="server" Width="90px" CssClass="Etiqueta2">
          Funcionario
        </asp:Label>
							<ecctrl:ecCombo id="cbxFuncionarioID" runat="server" Width="560px" ShowLabel="False"></ecctrl:ecCombo></TD>
					</TR>
					<TR>
						<TD colSpan="2"></TD>
					</TR>
					<TR>
						<TD colSpan="2"></TD>
					</TR>
				</TABLE>
				<P></P>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 122px; HEIGHT: 19px"></TD>
						<TD style="WIDTH: 206px; HEIGHT: 19px">
							<P align="left">
								<asp:button id="btBuscar" runat="server" Width="96px" CssClass="Button" Text="Buscar" Font-Bold="True"
									Height="22px" onclick="btBuscar_Click"></asp:button></P>
						</TD>
					</TR> <!--  
       endregion: Boton Buscar 
      /--> <!--							
      region:  Mensaje /
      -->
					<TR>
						<TD colSpan="7">
							<asp:label id="lblMensaje" runat="server" Font-Bold="True" Font-Size="X-Small"></asp:label></TD>
					</TR> <!-- 			
      endregion: Mensaje  		
      /--></TABLE>
			</asp:panel>
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
							<asp:datagrid id="dgResult" runat="server" Width="100%" CssClass="tbl" HorizontalAlign="Center"
								DataKeyField="OtID" AutoGenerateColumns="False">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header" HorizontalAlign="Center"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter" HorizontalAlign="Left"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="OtID" HeaderText="HI ID" Visible="true"></asp:BoundColumn>
									<asp:ButtonColumn ItemStyle-HorizontalAlign="Left" Text="Descripcion" DataTextField="OrdenTrabajo"
										HeaderText="O.T." CommandName="Select"></asp:ButtonColumn>
									<asp:BoundColumn DataField="Obs" ItemStyle-HorizontalAlign="Left" HeaderText="Obs"></asp:BoundColumn>
									<asp:BoundColumn DataField="AltaFecha" ItemStyle-HorizontalAlign="Left" HeaderText="AltaFecha"></asp:BoundColumn>
									<asp:BoundColumn DataField="Acta" ItemStyle-HorizontalAlign="Left" HeaderText="Acta"></asp:BoundColumn>
									<asp:BoundColumn DataField="TramiteAbrev" ItemStyle-HorizontalAlign="Left" HeaderText="Tram"></asp:BoundColumn>
									<asp:BoundColumn DataField="SituacionAbrev" ItemStyle-HorizontalAlign="Left" HeaderText="Sit"></asp:BoundColumn>
									<asp:BoundColumn DataField="Denominacion" ItemStyle-HorizontalAlign="Left" HeaderText="Denominación"></asp:BoundColumn>
									<asp:BoundColumn DataField="Registro" ItemStyle-HorizontalAlign="Left" HeaderText="Registro"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!---
------------------ FIN Panel Resultado ------------
--></form>
		<br>
	</body>
</HTML>
