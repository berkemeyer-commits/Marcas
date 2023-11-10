<%@ Reference Page="~/home/tramitesvarios.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.HIConsulta" CodeFile="HIConsulta.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar HI</title>
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
  window.opener.document.Form1.HIID.value = pCod ;
  window.opener.focus();
  window.close();
}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<!-- 

   Cabecera 
		
   /-->
			<BR>
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P><STRONG><FONT size="4">&nbsp;Consultar HI</FONT> </STRONG>
			</P>
			<!-- 

   Fin Cabecera

   /-->
			<asp:panel id="pnlBuscar" runat="server">
				<P><FONT size="3"><STRONG>&nbsp;&nbsp;&nbsp; <U>Criterio de Bú</U></STRONG></FONT><FONT size="3"><STRONG><U>squeda</U>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						</STRONG></FONT>
				</P>
				<P></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 283px" width="283">
							<asp:Label id="lblNro_min" runat="server" Width="90px" CssClass="Etiqueta2">
          HI Nro.
        </asp:Label>
							<asp:textbox id="txtNro_min" runat="server" Width="80px"></asp:textbox>
							<asp:Label id="lblNro_max" runat="server" Width="11px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtNro_max" runat="server" Width="80px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblAnio" runat="server" Width="30px" CssClass="Etiqueta2">
          Año
        </asp:Label>
							<asp:textbox id="txtAnio" runat="server" Width="60px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 283px" width="283">
							<asp:Label id="lblAltaFecha_min" runat="server" Width="90px" CssClass="Etiqueta2">
          Fecha de Alta
        </asp:Label>
							<asp:textbox id="txtAltaFecha_min" runat="server" Width="80px"></asp:textbox>
							<asp:Label id="lblAltaFecha_max" runat="server" Width="11px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtAltaFecha_max" runat="server" Width="80px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
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
								<asp:button id="btBuscar" runat="server" Width="96px" CssClass="Button" Height="22px" Font-Bold="True"
									Text="Buscar" onclick="btBuscar_Click"></asp:button></P>
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
-->
			<asp:panel id="pnlResultado" runat="server" Visible="True">
				<P><FONT size="3"><STRONG>&nbsp;&nbsp;&nbsp; <U>Resultado de la Búsqueda</U>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						</STRONG></FONT>
				</P>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
									<TD class="titletable1" width="100%" bgColor="#7bb5e7">
										<asp:label id="lblTituloGrid" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="dgResult" runat="server" Width="100%" CssClass="Grilla" HorizontalAlign="Center"
								DataKeyField="OrdenTrabajo" AutoGenerateColumns="False">
								<ItemStyle CssClass="EstiloItem"></ItemStyle>
								<HeaderStyle CssClass="EstiloHeader" HorizontalAlign="Center"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter" HorizontalAlign="Left"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID" Visible="true"></asp:BoundColumn>
									<asp:BoundColumn DataField="OrdenTrabajo" HeaderText="Número" Visible="true"></asp:BoundColumn>
									<asp:BoundColumn DataField="Descrip" HeaderText="Descripción" Visible="true"></asp:BoundColumn>
									<asp:BoundColumn DataField="AltaFecha" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Left"
										HeaderText="Fecha de Alta"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!---
------------------ FIN Panel Resultado ------------
-->
		</form>
	</body>
</HTML>
