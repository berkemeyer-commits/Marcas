<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.ClienteConsulta" CodeFile="ClienteConsulta.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Cliente</title>
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
  window.opener.document.Form1.ClienteID.value = pCod ;
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
			<P class="titulo">Consultar Cliente</P>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnlBuscar" runat="server">
				<P class="subtitulo">Criterio de Búsqueda</P>
				<P></P>
				<TABLE class="infoMacro" id="tblBuscar">
					<TR>
						<TD style="WIDTH: 110px" width="110">
							<asp:Label id="lblID_min" runat="server" CssClass="Etiqueta2" Width="90px">ID</asp:Label>
						<TD style="WIDTH: 304px">
							<asp:textbox id="txtID_min" runat="server" Width="160px"></asp:textbox></TD>
						<TD style="WIDTH: 126px"></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 110px" width="110">
							<asp:Label id="lblDescrip" runat="server" CssClass="Etiqueta2" Width="90px">
          Nombre
        </asp:Label></TD>
						<TD style="WIDTH: 304px">
							<asp:textbox id="txtNombre" runat="server" Width="250px"></asp:textbox></TD>
						<TD style="WIDTH: 126px">
							<asp:Label id="Label2" runat="server" CssClass="Etiqueta2" Width="112px">Solo clientes Activos</asp:Label></TD>
						<TD>
							<asp:CheckBox id="chkActivo" runat="server" Width="96px" Checked="True"></asp:CheckBox></TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 110px" width="110">
							<asp:Label id="lblCorreo" runat="server" CssClass="Etiqueta2" Width="90px">Correo</asp:Label>
						<TD style="WIDTH: 304px">
							<asp:textbox id="txtCorreo" runat="server" Width="250px"></asp:textbox></TD>
						<TD style="WIDTH: 126px">
							<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="112px">Solo No ubicables</asp:Label></TD>
						<TD>
							<asp:CheckBox id="chkInubicable" runat="server" Width="113px"></asp:CheckBox></TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 110px" width="110">
							<asp:Label id="lblPais" runat="server" CssClass="Etiqueta2" Width="88px">Pais</asp:Label></TD>
						<TD style="WIDTH: 304px">
							<asp:textbox id="txtPais" runat="server" Width="250px"></asp:textbox></TD>
						<TD style="WIDTH: 126px">
							<asp:Label id="Label3" runat="server" CssClass="Etiqueta2" Width="112px">Solo clientes  Multiple</asp:Label></TD>
						<TD>
							<asp:CheckBox id="chkMultiple" runat="server" Width="113px"></asp:CheckBox></TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 110px" width="110">
							<asp:Label id="lblCiudad" runat="server" CssClass="Etiqueta2" Width="88px">Ciudad</asp:Label></TD>
						<TD style="WIDTH: 304px">
							<asp:textbox id="txtCiudad" runat="server" Width="250px"></asp:textbox></TD>
						<TD style="WIDTH: 126px"></TD>
						<TD></TD>
						<TD>&nbsp;</TD>
					</TR>
                    <TR>
						<TD style="WIDTH: 110px" width="110">
							<asp:Label id="lblObservacion" runat="server" CssClass="Etiqueta2" Width="88px">Observación</asp:Label></TD>
						<TD style="WIDTH: 304px;">
							<asp:textbox id="txtObservacion" runat="server" Width="250px"></asp:textbox></TD>
						<TD style="WIDTH: 126px"></TD>
						<TD></TD>
						<TD>&nbsp;</TD>
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
								<asp:button id="btBuscar" runat="server" CssClass="Button" Width="96px" Text="Buscar" Font-Bold="True"
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
							<asp:datagrid id="dgResult" runat="server" CssClass="tbl" Width="100%" AutoGenerateColumns="False"
								DataKeyField="ID" HorizontalAlign="Center">
								<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:ButtonColumn Text="Descripcion" DataTextField="Nombre" HeaderText="Descripci&#243;n" CommandName="Select">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:ButtonColumn>
									<asp:BoundColumn DataField="pais" HeaderText="Pa&#237;s"></asp:BoundColumn>
									<asp:BoundColumn DataField="Direccion" HeaderText="Direcci&#243;n"></asp:BoundColumn>
									<asp:BoundColumn DataField="Obs" HeaderText="Observaci&#243;n">
										<ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Activo">
										<ItemTemplate>
											<asp:CheckBox ID="Activo" Runat="server" Enabled="False" Checked='<%# DataBinder.Eval(Container.DataItem, "Activo")%>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="FechaAlta" HeaderText="Fec.Alta"></asp:BoundColumn>
									<asp:BoundColumn DataField="clientes_relacionados" HeaderText="Clientes Relacionados"></asp:BoundColumn>
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
