<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#"  smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.ClaseClaseConsulta" CodeFile="ClaseClaseConsulta.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Clase-Clase</title>
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
  window.opener.document.Form1.ClaseClaseID.value = pCod ;
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
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P><STRONG><FONT size="4">&nbsp;Consultar Clase - Clase</FONT> </STRONG>
			</P>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnlBuscar" runat="server">
				<P><FONT size="3"><STRONG>&nbsp;&nbsp;&nbsp;&nbsp;<FONT size="2"><EM><U>CRITERIO</U></EM>&nbsp; 
								de Búsqueda</FONT>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						</STRONG></FONT>
				</P>
				<P></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblID_min" runat="server" CssClass="Etiqueta2" Width="100px">
          ID
        </asp:Label>
							<asp:textbox id="txtID_min" runat="server" Width="65px"></asp:textbox>
							<asp:Label id="lblID_max" runat="server" CssClass="Etiqueta2" Width="15px">
           - 
        </asp:Label>
							<asp:textbox id="txtID_max" runat="server" Width="65px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px; HEIGHT: 22px" width="390">
							<asp:Label id="lblClaseID" runat="server" CssClass="Etiqueta2" Width="100px">
         Clase ID
       </asp:Label>
							<CUSTOM:DROPDOWN id="ddlClaseID" runat="server" Width="230px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
						<TD style="HEIGHT: 22px">&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px; HEIGHT: 23px" width="390">
							<asp:Label id="lblClaseRelacID" runat="server" CssClass="Etiqueta2" Width="100px">
         Clase Relac. ID
       </asp:Label>
							<CUSTOM:DROPDOWN id="ddlClaseRelacID" runat="server" Width="230px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
						<TD style="HEIGHT: 23px">&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px; HEIGHT: 22px" width="390">
							<asp:Label id="lblAncestro" runat="server" CssClass="Etiqueta2" Width="100px">
         Ancestro
        </asp:Label>
							<asp:DropDownList id="ddlAncestro" runat="server" Width="65px">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="Si">Si</asp:ListItem>
								<asp:ListItem Value="No">No</asp:ListItem>
							</asp:DropDownList></TD>
						<TD style="HEIGHT: 22px">&nbsp;
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
			<!---
------------------ PANEL RESULTADO ------------
--><asp:panel id="pnlResultado" runat="server" Visible="True">
				<P><FONT size="3"><STRONG>&nbsp;&nbsp;&nbsp;&nbsp;<FONT size="2"><EM><U>RESULTADO</U> </EM>&nbsp;de 
								la Búsqueda</FONT>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
							<asp:datagrid id="dgResult" runat="server" CssClass="Grilla" Width="100%" AutoGenerateColumns="False"
								DataKeyField="ID" HorizontalAlign="Center">
								<ItemStyle CssClass="EstiloItem"></ItemStyle>
								<HeaderStyle CssClass="EstiloHeader" HorizontalAlign="Center"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter" HorizontalAlign="Left"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID" Visible="true"></asp:BoundColumn>
									<asp:ButtonColumn ItemStyle-HorizontalAlign="Left" Text="Descripcion" DataTextField="ClaseDescrip"
										HeaderText="ClaseDescrip" CommandName="Select"></asp:ButtonColumn>
									<asp:BoundColumn DataField="ClaseRelacDescrip" ItemStyle-HorizontalAlign="Left" HeaderText="ClaseRelacDescrip"></asp:BoundColumn>
									<asp:BoundColumn DataField="Ancestro" ItemStyle-HorizontalAlign="Left" HeaderText="Ancestro"></asp:BoundColumn>
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
