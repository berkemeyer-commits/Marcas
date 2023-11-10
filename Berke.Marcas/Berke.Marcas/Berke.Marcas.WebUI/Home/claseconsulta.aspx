<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.ClaseConsulta" CodeFile="ClaseConsulta.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Clase</title>
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
  window.opener.document.Form1.ClaseID.value = pCod ;
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
			<P class="titulo">Consultar Clase</P>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnlBuscar" runat="server" CssClass="infoMacro" Width="98%">
				<P class="subtitulo">Criterio de B�squeda</P>
				<P></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 383px; HEIGHT: 30px" width="383">
							<asp:Label id="lblNizaEdicionID" runat="server" Width="90px" CssClass="Etiqueta2">
         Niza Edici�n ID
       </asp:Label>
							<CUSTOM:DROPDOWN id="ddlNizaEdicionID" runat="server" Width="210px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
						<TD style="HEIGHT: 30px">&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 383px" width="383">
							<asp:Label id="lblID_min" runat="server" Width="90px" CssClass="Etiqueta2">
          ID
        </asp:Label>
							<asp:textbox id="txtID_min" runat="server" Width="95px"></asp:textbox>
							<asp:Label id="lblID_max" runat="server" Width="12px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtID_max" runat="server" Width="95px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblDescrip" runat="server" Width="90px" CssClass="Etiqueta2">
          Descripci�n
        </asp:Label>
							<asp:textbox id="txtDescrip" runat="server" Width="210px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 383px" width="383">
							<asp:Label id="lblNro" runat="server" Width="90px" CssClass="Etiqueta2">
          Clase N�mero
        </asp:Label>
							<asp:textbox id="txtNro" runat="server" Width="210px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblNizaAbrev" runat="server" Width="90px" CssClass="Etiqueta2">
          Niza Abreviado
        </asp:Label>
							<asp:textbox id="txtNizaAbrev" runat="server" Width="210px"></asp:textbox></TD>
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
--><asp:panel id="pnlResultado" runat="server" Visible="True">
				<P class="subtitulo">Resultado de la B�squeda</P>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head" width="100%" align="center" border="0">
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
								DataKeyField="ID" AutoGenerateColumns="False">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header" HorizontalAlign="Center"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter" HorizontalAlign="Left"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID" Visible="true"></asp:BoundColumn>
									<asp:BoundColumn DataField="NizaAbrev" HeaderText="Edici�n" Visible="true"></asp:BoundColumn>
									<asp:BoundColumn DataField="Nro" HeaderText="Clase Nro." Visible="true"></asp:BoundColumn>
									<asp:BoundColumn DataField="Descrip" ItemStyle-HorizontalAlign="Left" HeaderText="Descripci�n"></asp:BoundColumn>
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
