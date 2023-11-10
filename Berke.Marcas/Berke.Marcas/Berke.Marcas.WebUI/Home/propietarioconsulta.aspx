<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.PropietarioConsulta" CodeFile="PropietarioConsulta.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Propietario</title>
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
  window.opener.document.Form1.PropietarioID.value = pCod ;
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
			<P class="titulo">Consultar Propietario</P>
			<!-- 

   Fin Cabecera

   /-->
   
   
<asp:panel id="pnlBuscar" CssClass="infoMacro" Width="98%"
				runat="server">
				<P class="subtitulo">Criterio de Búsqueda</P>
				<P></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 132px" width="132">
							<asp:Label id="lblID_min" runat="server" Width="90px" CssClass="Etiqueta2">ID</asp:Label>
						<TD style="WIDTH: 279px">
							<asp:textbox id="txtID_min" runat="server" Width="160px"></asp:textbox></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 132px">
							<asp:Label id="lblDescrip" runat="server" Width="90px" CssClass="Etiqueta2">Nombre</asp:Label>
						<TD style="WIDTH: 279px">
							<asp:textbox id="txtDescrip" runat="server" Width="250px"></asp:textbox></TD>
						<TD></TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 132px" width="132">
							<asp:Label id="lblPais" runat="server" Width="88px" CssClass="Etiqueta2">Pais</asp:Label></TD>
						<TD style="WIDTH: 279px">
							<asp:textbox id="txtPais" runat="server" Width="250px"></asp:textbox></TD>
						<TD></TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 132px" width="132">
							<asp:Label id="lblCiudad" runat="server" Width="88px" CssClass="Etiqueta2">Ciudad</asp:Label></TD>
						<TD style="WIDTH: 279px">
							<asp:textbox id="txtCiudad" runat="server" Width="250px"></asp:textbox></TD>
						<TD></TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 132px" width="132">
							<asp:Label id="lblGrupo" runat="server" Width="90px" CssClass="Etiqueta2">Grupo Empresarial</asp:Label>
						<TD style="WIDTH: 279px">
							<asp:textbox id="txtGrupo" runat="server" Width="250px"></asp:textbox></TD>
						<TD></TD>
						<TD>&nbsp;</TD>
					</TR>
				</TABLE>
				<P></P>
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 122px; HEIGHT: 19px"></TD>
						<TD style="WIDTH: 206px; HEIGHT: 19px">
							<P align="left">
								<asp:button id="btBuscar" runat="server" Width="96px" CssClass="Button" Height="22px" Font-Bold="True"
									Text="Buscar" onclick="btBuscar_Click_1"></asp:button></P>
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
			<!---
------------------ FIN Panel Resultado ------------
--><asp:panel id="pnlResultado" Width="98%"
				runat="server" Visible="True">
				<P class="subtitulo">Resultado de la Búsqueda</P>
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
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:ButtonColumn Text="Descripcion" DataTextField="Nombre" HeaderText="Descripci&#243;n" CommandName="Select">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:ButtonColumn>
									<asp:BoundColumn DataField="Direccion" HeaderText="Direccion">
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
	</body>
</HTML>
