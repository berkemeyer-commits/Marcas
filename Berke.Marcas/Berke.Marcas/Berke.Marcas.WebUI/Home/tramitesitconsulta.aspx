<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#"  smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.TramiteSitConsulta" CodeFile="TramiteSitConsulta.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar TramiteSit</title>
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
  window.opener.document.Form1.TramiteSitID.value = pCod ;
  window.opener.focus();
  window.close();
}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('btBuscar');">
			<!-- Cabecera -->
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo">Consultar Situaciones por Trámite</P>
			<!-- Fin Cabecera-->
			
			<asp:panel id="pnlBuscar" runat="server" CssClass="infoMacro" Width="98%">
				<P class="subtitulo"> Criterio de B&uacute;squeda</P>
				<P></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblID_min" runat="server" CssClass="Etiqueta2" Width="90px">
          ID
        </asp:Label>
							<asp:textbox id="txtID_min" runat="server" Width="64px"></asp:textbox>
							<asp:Label id="lblID_max" runat="server" CssClass="Etiqueta2" Width="15px">
           - 
        </asp:Label>
							<asp:textbox id="txtID_max" runat="server" Width="64px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
					</TR> <!-- desde aquí -->
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="90px">
          Descripción
        </asp:Label>
							<asp:textbox id="txtDescrip" runat="server" Width="190px"></asp:textbox></TD>
					</TR> <!-- hasta aquí -->
					<TR>
						<TD style="WIDTH: 390px; HEIGHT: 25px" width="390">
							<asp:Label id="lblTramiteID" runat="server" CssClass="Etiqueta2" Width="90px">
         Trámite ID
       </asp:Label>
							<CUSTOM:DROPDOWN id="ddlTramiteID" runat="server" Width="190px" AutoPostBack="True"></CUSTOM:DROPDOWN></TD> <!-- desde aqui -->
						<TD style="HEIGHT: 25px">
							<asp:Label id="Label2" runat="server" CssClass="Etiqueta2" Width="90px">
         Vigente
        </asp:Label>
							<asp:DropDownList id="ddlVigente" runat="server" Width="120px">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="Si">Si</asp:ListItem>
								<asp:ListItem Value="No">No</asp:ListItem>
							</asp:DropDownList></TD> <!-- desde aqui -->
						<TD style="HEIGHT: 25px">&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px; HEIGHT: 22px" width="390"></TD>
						<TD>
							<asp:Label id="Label3" runat="server" CssClass="Etiqueta2" Width="90px">
         Automático
        </asp:Label>
							<asp:DropDownList id="ddlAutomatico" runat="server" Width="120px">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="Si">Si</asp:ListItem>
								<asp:ListItem Value="No">No</asp:ListItem>
							</asp:DropDownList></TD>
					</TR>
					<TR>
					</TR>
				</TABLE>
				<P></P>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 122px; HEIGHT: 19px">
							<asp:Label id="Label4" runat="server" CssClass="Etiqueta2" Width="312px" Visible="False"></asp:Label></TD>
						<TD style="WIDTH: 206px; HEIGHT: 19px">
							<P align="left">
								<asp:button id="btBuscar" runat="server" CssClass="Button" Width="96px" Height="22px" Text="Buscar"
									Font-Bold="True" onclick="btBuscar_Click"></asp:button></P>
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
			<!--PANEL RESULTADO-->
			<asp:panel id="pnlResultado" runat="server" Visible="True">
				<P class="subtitulo">Resultado de la B&uacute;squeda</P>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head" width="100%">
								<TR>
									<TD>
										<asp:label id="lblTituloGrid" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="dgResult" runat="server" CssClass="tbl" Width="100%" AutoGenerateColumns="False"
								DataKeyField="ID" HorizontalAlign="Center">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header" HorizontalAlign="Center"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter" HorizontalAlign="Left"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID" Visible="true"></asp:BoundColumn>
									<asp:ButtonColumn ItemStyle-HorizontalAlign="Left" Text="Descripcion" DataTextField="Descrip" HeaderText="Descrip"
										CommandName="Select"></asp:ButtonColumn>
									<asp:BoundColumn DataField="Vigente" ItemStyle-HorizontalAlign="Left" HeaderText="Vigente"></asp:BoundColumn>
									<asp:BoundColumn DataField="Automatico" ItemStyle-HorizontalAlign="Left" HeaderText="Automatico"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!-- FIN Panel Resultado-->
		</form>
	</body>
</HTML>
