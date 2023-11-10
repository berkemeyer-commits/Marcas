<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.Test_UI" CodeFile="Test_UI.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Seleccionar Tabla Pais</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<!-- 

   Cabecera 
		
   /--><BR>
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P><STRONG><FONT size="4">&nbsp;Seleccionar Tabla Pais</FONT> </STRONG>
			</P>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnlBuscar" runat="server">
				<P><FONT size="3"><STRONG><U>Criterio de 
								Busqueda&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							</U></STRONG></FONT>
				</P>
				<P></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblidpais_min" runat="server" Width="90px" CssClass="Etiqueta2">
          idpais
        </asp:Label>
							<asp:textbox id="txtidpais_min" runat="server" Width="60px"></asp:textbox>
							<asp:Label id="lblidpais_max" runat="server" Width="15px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtidpais_max" runat="server" Width="60px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblpaisalfa" runat="server" Width="90px" CssClass="Etiqueta2">
          paisalfa
        </asp:Label>
							<asp:textbox id="txtpaisalfa" runat="server" Width="210px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lbldescrip" runat="server" Width="90px" CssClass="Etiqueta2">
          descrip
        </asp:Label>
							<asp:textbox id="txtdescrip" runat="server" Width="210px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblpaistel" runat="server" Width="90px" CssClass="Etiqueta2">
          paistel
        </asp:Label>
							<asp:textbox id="txtpaistel" runat="server" Width="210px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblabrev" runat="server" Width="90px" CssClass="Etiqueta2">
          abrev
        </asp:Label>
							<asp:textbox id="txtabrev" runat="server" Width="210px"></asp:textbox></TD>
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
								<asp:button id="btBuscar" runat="server" CssClass="Button" Font-Bold="True" Text="Buscar"></asp:button></P>
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
				<P><FONT size="3"><STRONG><U>Resultado de la 
								Bsqueda&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							</U></STRONG></FONT>
				</P>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<TR>
									<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
									<TD class="titletable1" width="100%" bgColor="#7bb5e7">CPais</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="dgResult" runat="server" Width="100%" CssClass="Grilla" HorizontalAlign="Center"
								DataKeyField="idpais" AutoGenerateColumns="False">
								<ItemStyle CssClass="EstiloItem"></ItemStyle>
								<HeaderStyle CssClass="EstiloHeader" HorizontalAlign="Center"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter" HorizontalAlign="Left"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="idpais" HeaderText="idpais" Visible="true"></asp:BoundColumn>
									<asp:ButtonColumn ItemStyle-HorizontalAlign="Left" Text="Descripcion" DataTextField="descrip" HeaderText="descrip"
										CommandName="Select"></asp:ButtonColumn>
									<asp:BoundColumn DataField="abrev" ItemStyle-HorizontalAlign="Left" HeaderText="abrev"></asp:BoundColumn>
									<asp:BoundColumn DataField="paisalfa" ItemStyle-HorizontalAlign="Left" HeaderText="paisalfa"></asp:BoundColumn>
									<asp:BoundColumn DataField="paistel" ItemStyle-HorizontalAlign="Left" HeaderText="paistel"></asp:BoundColumn>
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
