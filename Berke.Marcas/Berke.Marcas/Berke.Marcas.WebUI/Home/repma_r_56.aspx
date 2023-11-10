<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.repMA_R_56" CodeFile="repMA_R_56.aspx.cs" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>repMA_R_56</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('btBuscar');">
			<!-- 

   Cabecera 
		
   /-->
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo"><asp:label id="lblTitulo" runat="server" >Marcas Nuestras Registradas y en Tramite </asp:label><asp:label id="lblTituloAclaracion" runat="server" >( Por Cliente )</asp:label></P>
			<!-- 

   Fin Cabecera

   /-->
			<P class="subtitulo">Filtro</P>
			<P></P>
			<TABLE id="tblBuscar" class="infoMacro" width="98%">
				<TR>
					<TD style="WIDTH: 591px; HEIGHT: 20px" width="591"><asp:label id="lblClienteID" runat="server" Width="90px" CssClass="Etiqueta2">Cliente</asp:label>&nbsp;
						<ecctrl:eccombo id="cbxClienteID" runat="server" Width="440px" ShowLabel="False"></ecctrl:eccombo></TD>
				</TR>
				<TR>
					<td>&nbsp;</td>
				</TR>
				<TR>
					<TD style="WIDTH: 591px" width="591"><asp:label id="lblPropietarioID" runat="server" Width="90px" CssClass="Etiqueta2">Propietario</asp:label>&nbsp;
						<ecctrl:eccombo id="cbxPropietarioID" runat="server" Width="440px" ShowLabel="False"></ecctrl:eccombo></TD>
				</TR>
				<TR>
					<td>&nbsp;</td>
				</TR>
				<TR>
					<TD style="WIDTH: 591px; HEIGHT: 32px" width="591"><asp:label id="Label1" runat="server" Width="90px" CssClass="Etiqueta2" Height="16px">Idioma Documento</asp:label>&nbsp;
						<CUSTOM:DROPDOWN id="ddlIdiomaDoc" runat="server" Width="144px" Height="26px" AutoPostBack="False"></CUSTOM:DROPDOWN>&nbsp;&nbsp; 
						&nbsp;
					</TD>
					</TD></TR>
			</TABLE>
			<P></P>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
				<TR>
					<TD style="WIDTH: 122px; HEIGHT: 19px"></TD>
					<TD style="WIDTH: 206px; HEIGHT: 19px">
						<P align="left"><asp:button id="btBuscar" runat="server" Font-Bold="True" Width="96px" CssClass="Button" Height="22px"
								Text="Buscar" onclick="btBuscar_Click"></asp:button></P>
					</TD>
				</TR> <!--  
       endregion: Boton Buscar 
      /--> <!--							
      region:  Mensaje /
      -->
				<TR>
					<TD colSpan="7">
						<asp:label id="lblMensaje" runat="server" Font-Bold="True" Font-Size="X-Small"></asp:label>
						<asp:Label id="lblHtml" runat="server"></asp:Label></TD>
				</TR> <!-- 			
      endregion: Mensaje  		
      /--></TABLE>
		</form>
	</body>
</HTML>
