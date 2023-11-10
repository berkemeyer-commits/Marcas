<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.ClaseSelec" CodeFile="ClaseSelec.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Seleccionar Clase</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
		<script language="JavaScript">

			function restablecer(valores){
				window.opener.document.Form1.tbClasesEleg.value = valores ;
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
			<P><STRONG><FONT size="4"><asp:panel id="pnlBuscar" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 32px" runat="server"
							Width="424px" Height="32px"></P>
			<P></P>
			<P>
				<asp:label id="lblMensaje" runat="server" Font-Bold="True" Font-Size="X-Small"></asp:label></P>
			<P>
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /--> <!--  
       endregion: Boton Buscar 
      /-->  <!--							
      region:  Mensaje /
      --> <!-- 			
      endregion: Mensaje  		
      
      
      /--></TABLE>
				</asp:panel><asp:panel id="pnlResultado" style="Z-INDEX: 102; LEFT: 16px; POSITION: absolute; TOP: 96px"
					runat="server" Visible="True"></P>
			<P><FONT size="3"><STRONG><asp:button id="btnMarcar" runat="server" Width="190px" CssClass="Button" Height="22px" Font-Bold="True"
							Text="Marcar/Desmarcar (Todos)" onclick="btnMarcar_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="btnCerrar" runat="server" Width="184px" CssClass="Button" Height="22px" Font-Bold="True"
							Text="Cerrar" onclick="btnCerrar_Click"></asp:button>&nbsp;&nbsp;</STRONG></FONT></P>
			<P>
				<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
									<TD class="titletable1" width="100%" bgColor="#7bb5e7"><asp:label id="lblTituloGrid" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%"><asp:datagrid id="dgResult" runat="server" Width="100%" CssClass="Grilla" HorizontalAlign="Center"
								DataKeyField="ID" AutoGenerateColumns="False">
								<ItemStyle CssClass="EstiloItem"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="EstiloHeader"></HeaderStyle>
								<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Sel.">
										<ItemTemplate>
											<asp:CheckBox id="cbSel" runat="server" Width="100%"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="NizaAbrev" HeaderText="Edici&#243;n"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Clase Nro.">
										<ItemTemplate>
											<asp:Label id="Nro" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Nro") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Nro") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Descrip" HeaderText="Descripci&#243;n">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
				</asp:panel>&nbsp;Seleccionar Clase</FONT> </STRONG></P>
			<!-- 

   Fin Cabecera

   /-->
			<!---
------------------ PANEL RESULTADO ------------
-->
			<!---
------------------ FIN Panel Resultado ------------
--></form>
	</body>
</HTML>
