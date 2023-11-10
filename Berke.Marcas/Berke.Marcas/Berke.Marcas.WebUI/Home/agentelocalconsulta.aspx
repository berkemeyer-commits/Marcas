<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.AgenteLocalConsulta" CodeFile="AgenteLocalConsulta.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Agente Local</title>
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
  window.opener.document.Form1.AgenteLocalId.value = pCod ;
  window.opener.focus();
  window.close();
}
		</script>
	</HEAD>
	<body>
	
				
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('btBuscar');">
	
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo">Consultar Agente Local
			</P>
	
	<asp:panel id="pnlBuscar" CssClass="infoMacro" Width="98%"
				runat="server">
				<P class="subtitulo">Criterio de Búsqueda</P>
				<P></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
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
							<asp:Label id="lblNombre" runat="server" CssClass="Etiqueta2" Width="90px">
          Nombre
        </asp:Label></TD>
						<TD style="WIDTH: 304px">
							<asp:textbox id="txtNombre" runat="server" Width="250px"></asp:textbox></TD>
						<TD style="WIDTH: 126px">
							<asp:Label id="Label2" runat="server" CssClass="Etiqueta2" Width="112px">Nuestro</asp:Label></TD>
						<TD>
							<asp:CheckBox id="chkNuestro" runat="server" Width="96px"></asp:CheckBox></TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 110px" width="110">
							<asp:Label id="lblDireccion" runat="server" CssClass="Etiqueta2" Width="90px">Dirección</asp:Label>
						<TD style="WIDTH: 304px">
							<asp:textbox id="txtDireccion" runat="server" Width="250px"></asp:textbox></TD>
						<TD style="WIDTH: 126px"></TD>
						<TD></TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 110px" width="110">
							<asp:Label id="lblMatricula" runat="server" CssClass="Etiqueta2" Width="88px">Matricula</asp:Label></TD>
						<TD style="WIDTH: 304px">
							<asp:textbox id="txtMatricula" runat="server" Width="250px"></asp:textbox></TD>
						<TD style="WIDTH: 126px"></TD>
						<TD></TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 110px" width="110">
							<asp:Label id="lblEstudio" runat="server" CssClass="Etiqueta2" Width="88px">Estudio</asp:Label></TD>
						<TD style="WIDTH: 304px">
							<asp:textbox id="txtEstudio" runat="server" Width="250px"></asp:textbox></TD>
						<TD style="WIDTH: 126px"></TD>
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
			</asp:panel><asp:panel id="pnlResultado" 
				runat="server" Visible="True">
				<P class="subtitulo">Resultado de la Búsqueda</P>
				<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE width="100%" class="grid_head">
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
								DataKeyField="IDAgloc" HorizontalAlign="Center">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="IDAgloc" HeaderText="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="Nombre" HeaderText="Nombre"></asp:BoundColumn>
									<asp:BoundColumn DataField="Direccion" HeaderText="Direcci&#243;n"></asp:BoundColumn>
									<asp:BoundColumn DataField="NroMatricula" HeaderText="Matricula"></asp:BoundColumn>
									<asp:BoundColumn DataField="Obs" HeaderText="Observaci&#243;n">
										<ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Nuestro">
										<ItemTemplate>
											<asp:CheckBox ID="Nuestro" Runat="server" Enabled="False" Checked='<%# DataBinder.Eval(Container, "DataItem.Nuestro")%>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel><BR>

			<!-- 

   Fin Cabecera

   /-->
			<P></P>
			<!---
------------------ PANEL RESULTADO ------------
-->
			<!---
------------------ FIN Panel Resultado ------------
--></form>
	</body>
</HTML>
