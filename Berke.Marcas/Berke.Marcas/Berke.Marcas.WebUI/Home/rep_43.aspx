<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.Rep_43" CodeFile="Rep_43.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Rep_43</title>
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
			<P class="titulo">Vencimientos de Marcas ( Por Agente y Prop. P/ Enviar)</P>
			<!-- 

   Fin Cabecera

   /-->
			<P class="subtitulo">Filtro</P>
			<P></P>
			<TABLE id="tblBuscar" class="infoMacro">
				<TR>
					<TD style="WIDTH: 591px; HEIGHT: 20px" width="591"><asp:label id="Label1" runat="server" CssClass="Etiqueta2" Width="105px">Fecha Vto.</asp:label><asp:textbox id="txtVencim" runat="server" Width="216px"></asp:textbox></TD>
				</TR>
				<TR>
					<td>&nbsp;</td>
				</TR>
				<TR>
					<TD style="WIDTH: 591px; HEIGHT: 20px" width="591"><asp:label id="Label2" runat="server" CssClass="Etiqueta2" Width="105px">Con Instruccion</asp:label><asp:textbox id="txtInstruc" runat="server" Width="216px"></asp:textbox>
						<asp:CheckBox id="chkIncluirNulos" runat="server" Text="<-Incluir los que no tienen instrucción"
							Checked="True"></asp:CheckBox></TD>
				</TR>
				<TR>
					<td>&nbsp;</td>
				</TR>
				<TR>
					<TD style="WIDTH: 591px; HEIGHT: 20px" width="620"><asp:label id="Label3" runat="server" CssClass="Etiqueta2" Width="105px">Omitir Instrucción</asp:label><asp:textbox id="txtOmitir" runat="server" Width="216px">R,N,O,na</asp:textbox></TD>
				</TR>
				<TR>
					<td>&nbsp;</td>
				</TR>
				<TR>
					<TD style="WIDTH: 591px; HEIGHT: 20px" width="600">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:CheckBox id="chkUltimaInstruccion" runat="server" Checked="True" Text="<-Tener en cuenta sólo la última instrucción"></asp:CheckBox></TD>
				</TR>
				<TR>
					<td>&nbsp;</td>
				</TR>
				<TR>
					<TD style="WIDTH: 591px; HEIGHT: 20px" width="591"><asp:label id="lblPropietarioID" runat="server" CssClass="Etiqueta2" Width="105px">Propietario</asp:label>
						<ecctrl:eccombo id="cbxPropietarioID" runat="server" Width="368px" ShowLabel="False"></ecctrl:eccombo>
						<asp:textbox id="txtPropietarioID" runat="server" Width="90px" ToolTip="Lista de IDs" Visible="False"
							Enabled="False"></asp:textbox></TD>
				</TR>
				<TR>
					<td>&nbsp;</td>
				</TR>
				<TR>
					<TD style="WIDTH: 591px; HEIGHT: 20px" width="591"><asp:label id="lblClienteID" runat="server" CssClass="Etiqueta2" Width="105px">Cliente</asp:label>
						<ecctrl:eccombo id="cbxClienteID" runat="server" Width="368px" ShowLabel="False"></ecctrl:eccombo>
						<asp:textbox id="txtClienteID" runat="server" Width="90px" ToolTip="Lista de IDs" Visible="False"
							Enabled="False"></asp:textbox></TD>
				</TR>
			</TABLE>
			<P></P>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0"> <!--     
      region:  Boton Buscar    				  
      /-->
				<TR>
					<TD style="WIDTH: 65px; HEIGHT: 19px"></TD>
					<TD style="WIDTH: 206px; HEIGHT: 19px">
						<P align="left"><asp:button id="btBuscar" runat="server" Font-Bold="True" CssClass="Button" Width="128px" Height="22px"
								Text="Generar Documento" onclick="btBuscar_Click"></asp:button>&nbsp;<asp:hyperlink id="lnkDocum" runat="server" Width="56px"></asp:hyperlink></P>
					</TD>
				</TR> <!--  
       endregion: Boton Buscar 
      /--> <!--							
      region:  Mensaje /
      -->
				<TR>
					<TD colSpan="7"><asp:label id="lblMensaje" runat="server" Font-Size="X-Small" Font-Bold="True"></asp:label><asp:label id="lblHtml" runat="server"></asp:label></TD>
				</TR> <!-- 			
      endregion: Mensaje  		
      /--></TABLE>
		</form>
	</body>
</HTML>
