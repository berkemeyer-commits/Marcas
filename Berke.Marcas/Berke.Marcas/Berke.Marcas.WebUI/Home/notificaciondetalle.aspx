<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.NotificacionDetalle" CodeFile="NotificacionDetalle.aspx.cs" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NotificacionDetalle</title>
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
			<P><STRONG><FONT size="4">&nbsp;</FONT> </STRONG>
			</P>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnlBuscar" runat="server">
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR style="WIDTH: 7px; HEIGHT: 9px" width="7">
						<TD style="HEIGHT: 9px">
							<P><STRONG><FONT size="4">&nbsp;&nbsp; <U>Notificación</U>
										<asp:label id="Label2" runat="server" Font-Bold="True" Width="10px"></asp:label>
										<asp:label id="lblDestino" runat="server" Font-Size="Medium" Font-Italic="True"></asp:label></FONT></STRONG></P>
						</TD>
					<TR>
					</TR>
					<TR>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 679px" width="679">
							<asp:Label id="lblID" runat="server" Width="120px" CssClass="Etiqueta2">
          ID
        </asp:Label>
							<asp:textbox id="txtID" runat="server" Width="90px"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;
						</TD>
					<TR>
						<TD style="WIDTH: 679px" width="679">
							<asp:Label id="lblDescrip" runat="server" Width="120px" CssClass="Etiqueta2">
          Descripción
        </asp:Label>
							<asp:textbox id="txtDescrip" runat="server" Width="500px" MaxLength="100"></asp:textbox></TD>
					<TR>
						<TD style="WIDTH: 679px" width="679">
							<asp:Label id="lblMail_Destino" runat="server" Width="120px" CssClass="Etiqueta2">
          Mail  Destino
        </asp:Label>
							<asp:textbox id="txtMail_Destino" runat="server" Width="500px" MaxLength="100"></asp:textbox></TD>
					<TR>
						<TD style="WIDTH: 679px">
							<asp:Label id="lblFunc_Destino" runat="server" Width="120px" CssClass="Etiqueta2">
          Funcionario Destino
        </asp:Label>
							<asp:textbox id="txtFunc_Destino" runat="server" Width="500px" MaxLength="50"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 679px">
							<asp:Label id="lblActivo" runat="server" Width="120px" CssClass="Etiqueta2">
         Activo
        </asp:Label>
							<asp:textbox id="txtActivo" runat="server" Width="90px"></asp:textbox>
							<asp:Label id="lblVolver" runat="server" Width="72px" Visible="False"></asp:Label></TD>
					</TR>
					</TD></TR>
					<TR>
						<TD style="WIDTH: 679px" width="679"></TD>
					</TR>
				</TABLE>
				<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				</P>
				<P>&nbsp;</P>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 695px">
							<asp:Panel id="pnlEditar" runat="server" Width="520px" Height="31px" HorizontalAlign="Center"
								BorderStyle="Outset">&nbsp; 
<asp:Button id="btnConfirmar" runat="server" Width="95px" Height="22px" Text="Confirmar" onclick="btnConfirmar_Click"></asp:Button>
<asp:Label id="lblSep4" runat="server" Width="3px"></asp:Label>
<asp:Button id="btnCancelar" runat="server" Width="95px" Height="22px" Text="Cancelar" onclick="btnCancelar_Click"></asp:Button></asp:Panel>
						<TD></TD>
					<TR>
					<TR>
						<TD style="WIDTH: 695px">
							<asp:Panel id="pnlMostrar" runat="server" Width="520px" Height="52px" HorizontalAlign="Center"
								BorderStyle="Outset">&nbsp; 
<asp:Button id="btnAgregar" runat="server" Width="95px" Height="22px" Text="Agregar" onclick="btnAgregar_Click"></asp:Button>
<asp:Label id="lblSep1" runat="server" Width="1px"></asp:Label>
<asp:Button id="btnModificar" runat="server" Width="95px" Height="22px" Text="Modificar" onclick="btnModificar_Click"></asp:Button>
<asp:Label id="lblSep2" runat="server" Width="1px"></asp:Label>
<asp:Button id="btnEliminar" runat="server" Width="95px" Height="22px" Text="Eliminar" onclick="btnEliminar_Click"></asp:Button>
<asp:Label id="lblSep3" runat="server" Width="1px"></asp:Label>
<asp:Button id="btnVolver" runat="server" Width="95px" Height="22px" Text="Volver" onclick="btnVolver_Click"></asp:Button></asp:Panel>
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
			</asp:panel></form>
		</TR></TBODY></TABLE></FORM>
	</body>
</HTML>
