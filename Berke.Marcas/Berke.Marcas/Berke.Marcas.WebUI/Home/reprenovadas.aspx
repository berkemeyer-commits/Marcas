<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.repRenovadas" CodeFile="repRenovadas.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Reporte de Control de Renovaciones</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form onkeypress="form_onEnter('btBuscar');" id="Form1" method="post" runat="server">
			<!-- 

   Cabecera 
		
   /-->
			<p>
				<uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo"><asp:label id="lblTitulo" runat="server"> Control de Renovaciones </asp:label></P>
			<asp:label id="Label3" runat="server">(Para que una determinada marca aparezca en el listado debe tener asignada obligatoriamente una instrucción RENOVAR)</asp:label>
			<!-- 

   Fin Cabecera

   /-->
			<P class="subtitulo">Filtro</P>
			<P></P>
			<TABLE class="infoMacro" id="tblBuscar" width="98%">
				<TR>
					<td colspan="2">&nbsp;</td>
				</TR>
				<TR>
					<TD style="WIDTH: 360px; HEIGHT: 20px"><asp:label id="Label1" runat="server" Width="105px" CssClass="Etiqueta2">Fecha Vto. Registro</asp:label><asp:textbox id="txtVencimReg" runat="server" Width="216px"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</TD>
					<td colspan="2">
						<asp:CheckBox id="cbPerGracia" runat="server" Text="Tener en cuenta el periodo de gracia" AutoPostBack="True"></asp:CheckBox>
					</td>
				</TR>
				<TR>
					<td style="WIDTH: 360px">&nbsp;</td>
					<td colspan="2"></td>
				</TR>
				<TR>
					<TD style="WIDTH: 360px; HEIGHT: 20px" width="360"><asp:label id="Label2" runat="server" Width="105px" CssClass="Etiqueta2">Fecha Instrucción</asp:label><asp:textbox id="txtFechaInstruc" runat="server" Width="216px"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					<td colspan="2">
						<asp:Label id="lblAdvertencia" runat="server" Font-Bold="True" ForeColor="Red">Label</asp:Label></td>
				</TR>
				<TR>
					<td style="WIDTH: 360px">&nbsp;</td>
				</TR>
				<TR>
					<TD style="WIDTH: 360px; HEIGHT: 20px" width="360">
						<asp:RadioButtonList id="rblOpciones" runat="server" Width="264px">
							<asp:ListItem Value="0">Las que tienen s&#243;lo hoja de inicio</asp:ListItem>
							<asp:ListItem Value="1">Las que tienen hoja de inicio y acta</asp:ListItem>
							<asp:ListItem Value="2">Las que no tienen ni hoja de inicio ni acta</asp:ListItem>
							<asp:ListItem Value="3" Selected="True">Mostras todas</asp:ListItem>
						</asp:RadioButtonList>
					</TD>
				</TR>
			</TABLE>
			<P></P>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
				<TR>
					<TD style="WIDTH: 65px; HEIGHT: 19px"></TD>
					<TD style="WIDTH: 206px; HEIGHT: 19px">
						<P align="left"><asp:button id="btBuscar" runat="server" Width="128px" CssClass="Button" Text="Generar Documento"
								Font-Bold="True" Height="22px" onclick="btBuscar_Click"></asp:button>&nbsp;<asp:hyperlink id="lnkDocum" runat="server" Width="56px"></asp:hyperlink></P>
					</TD>
				</TR> <!--  
       endregion: Boton Buscar 
      /--> <!--							
      region:  Mensaje /
      -->
				<TR>
					<TD colSpan="7"><asp:label id="lblMensaje" runat="server" Font-Bold="True" Font-Size="X-Small"></asp:label><asp:label id="lblHtml" runat="server"></asp:label></TD>
				</TR> <!-- 			
      endregion: Mensaje  		
      /--></TABLE>
		</form>
	</body>
</HTML>
