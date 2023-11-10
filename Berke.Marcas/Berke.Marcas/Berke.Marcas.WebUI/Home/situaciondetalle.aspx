<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.SituacionDetalle" CodeFile="SituacionDetalle.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SituacionDetalle</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<!-- 

   Cabecera 
		
   /-->
			<BR>
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P><STRONG><FONT size="4">&nbsp;&nbsp; <U>Situación</U>
						<asp:label id="Label2" runat="server" Font-Bold="True" Width="10px"></asp:label>
						<asp:label id="lblDestino" runat="server" Font-Size="Medium" Font-Italic="True"></asp:label></FONT></STRONG></P>
			</TD>
			<TR>
				<TD>&nbsp;
				</TD>
			</TR>
			<TR>
				<TD>&nbsp;
				</TD>
			</TR>
			<!-- 




   Fin Cabecera

   /-->
			<asp:panel id="pnlBuscar" runat="server">
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 570px" width="570">
							<asp:Label id="lblID_min" runat="server" CssClass="Etiqueta2" Width="101px">
          ID
        </asp:Label>
							<asp:textbox id="txtID" runat="server" Width="90px"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;
						</TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 570px" width="570">
							<asp:Label id="lblDescrip" runat="server" CssClass="Etiqueta2" Width="101px">
          Descripción
        </asp:Label>
							<asp:textbox id="txtDescrip" runat="server" Width="320px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 570px" width="570">
							<asp:Label id="lblAbrev" runat="server" CssClass="Etiqueta2" Width="101px">
          Abreviado
        </asp:Label>
							<asp:textbox id="txtAbrev" runat="server" Width="90px"></asp:textbox>
							<asp:Label id="lblVolver" runat="server" Width="72px" Visible="False"></asp:Label></TD>
						<TD>&nbsp;
						</TD>
					</TR>
				</TABLE>
				<P></P>
				<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				</P>
				<P>&nbsp;</P>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 695px">
							<asp:Panel id="pnlEditar" runat="server" Width="520px" BorderStyle="Groove" HorizontalAlign="Center"
								Height="31px">&nbsp; 
<asp:Button id="btnConfirmar" runat="server" Width="95px" Height="22px" Text="Confirmar" onclick="btnConfirmar_Click"></asp:Button>
<asp:Label id="lblSep4" runat="server" Width="3px"></asp:Label>
<asp:Button id="btnCancelar" runat="server" Width="95px" Height="22px" Text="Cancelar" onclick="btnCancelar_Click"></asp:Button></asp:Panel>
						<TD></TD>
					<TR>
					<TR>
						<TD style="WIDTH: 695px">
							<asp:Panel id="pnlMostrar" runat="server" Width="520px" BorderStyle="Inset" HorizontalAlign="Center"
								Height="52px">&nbsp; 
<asp:Button id="btnAgregar" runat="server" Width="95px" Height="22px" Text="Agregar" onclick="btnAgregar_Click"></asp:Button>
<asp:Label id="lblSep1" runat="server" Width="1px"></asp:Label>
<asp:Button id="btnModificar" runat="server" Width="95px" Height="22px" Text="Modificar" onclick="btnModificar_Click"></asp:Button>
<asp:Label id="lblSep2" runat="server" Width="1px"></asp:Label>
<asp:Button id="btnEliminar" runat="server" Width="95px" Height="22px" Text="Eliminar" onclick="btnEliminar_Click"></asp:Button>
<asp:Label id="lblSep3" runat="server" Width="1px"></asp:Label>
<asp:Button id="btnVolver" runat="server" Width="95px" Height="22px" Text="Volver"></asp:Button></asp:Panel>
						<TD></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 695px; HEIGHT: 19px">
							<P>&nbsp;</P>
						</TD>
						<TD style="WIDTH: 206px; HEIGHT: 19px">
							<P align="left">&nbsp;</P>
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
		</form>
	</body>
</HTML>
