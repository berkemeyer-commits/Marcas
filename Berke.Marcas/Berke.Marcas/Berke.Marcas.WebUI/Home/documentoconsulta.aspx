<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#"  smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.DocumentoConsulta" CodeFile="DocumentoConsulta.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Documentos</title>
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
  window.opener.document.Form1.DocumentoID.value = pCod ;
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
			<P class="titulo">Consultar Documentos
			</P>
			<!-- 

   Fin Cabecera

   /-->
			<asp:panel id="pnlBuscar" runat="server" Width="98%" CssClass="infoMacro">
				<P class="subtitulo">Criterio de Búsqueda</P>
				<P></P>
				<TABLE id="tblBuscar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblID_min" runat="server" Width="90px" CssClass="Etiqueta2">
          ID
        </asp:Label>
							<asp:textbox id="txtID_min" runat="server" Width="64px"></asp:textbox>
							<asp:Label id="lblID_max" runat="server" Width="15px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtID_max" runat="server" Width="60px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblEsEscritoVario" runat="server" Width="90px" CssClass="Etiqueta2">Escrito Vario?  </asp:Label>
							<asp:DropDownList id="ddlEsEscritoVario" runat="server" Width="56px">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="Si">Si</asp:ListItem>
								<asp:ListItem Value="No">No</asp:ListItem>
							</asp:DropDownList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblFecha_min" runat="server" Width="90px" CssClass="Etiqueta2">
          Fecha
        </asp:Label>
							<asp:textbox id="txtFecha_min" runat="server" Width="75px"></asp:textbox>
							<asp:Label id="lblFecha_max" runat="server" Width="15px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtFecha_max" runat="server" Width="75px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblExpeID" runat="server" Width="90px" CssClass="Etiqueta2"> Expediente ID</asp:Label>
							<asp:textbox id="txtExpeID" runat="server" Width="60px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblDocNro" runat="server" Width="90px" CssClass="Etiqueta2">Doc. Nro</asp:Label>
							<asp:textbox id="txtDocNro" runat="server" Width="60px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblActaNro" runat="server" Width="90px" CssClass="Etiqueta2">Acta Nro</asp:Label>
							<asp:textbox id="txtActaNro" runat="server" Width="60px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblDocAnio" runat="server" Width="90px" CssClass="Etiqueta2">Doc. Año</asp:Label>
							<asp:textbox id="txtDocAnio" runat="server" Width="60px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblActaAnio" runat="server" Width="90px" CssClass="Etiqueta2">Acta Año    </asp:Label>
							<asp:textbox id="txtActaAnio" runat="server" Width="60px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblDescrip" runat="server" Width="90px" CssClass="Etiqueta2">Descripción</asp:Label>
							<asp:textbox id="txtDescrip" runat="server" Width="210px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblDocRefExt" runat="server" Width="90px" CssClass="Etiqueta2">Ref. Externa</asp:Label>
							<asp:textbox id="txtDocRefExt" runat="server" Width="210px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 390px" width="390">
							<asp:Label id="lblDocTipoID" runat="server" Width="90px" CssClass="Etiqueta2">Tipo Docum.</asp:Label>
							<CUSTOM:DROPDOWN id="ddlDocTipoID" runat="server" Width="240px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
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
								<asp:button id="btBuscar" runat="server" CssClass="Button" Font-Bold="True" Text="Buscar" onclick="btBuscar_Click"></asp:button></P>
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
-->
			<asp:panel id="pnlResultado" runat="server" Visible="True">
				<P class="subtitulo">Resultado de la Búsqueda</P>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head" width="100%" >
								<TR>
									<TD><asp:label id="lblTituloGrid" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="dgResult" runat="server" Width="100%" CssClass="tbl" HorizontalAlign="Center"
								DataKeyField="ID" AutoGenerateColumns="False">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle CssClass="cell_header" HorizontalAlign="Center"></HeaderStyle>
								<FooterStyle CssClass="EstiloFooter" HorizontalAlign="Left"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID" Visible="true"></asp:BoundColumn>
									<asp:BoundColumn DataField="DocTipoAbrev" ItemStyle-HorizontalAlign="Left" HeaderText="Tipo"></asp:BoundColumn>
									<asp:BoundColumn DataField="Identficador" ItemStyle-HorizontalAlign="Left" HeaderText="Identif."></asp:BoundColumn>
									<asp:BoundColumn DataField="DocNro" ItemStyle-HorizontalAlign="Left" HeaderText="DocNro"></asp:BoundColumn>
									<asp:BoundColumn DataField="DocAnio" ItemStyle-HorizontalAlign="Left" HeaderText="DocAnio"></asp:BoundColumn>
									<asp:BoundColumn DataField="Fecha" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha" DataFormatString="{0:d}"></asp:BoundColumn>
									<asp:BoundColumn DataField="Descrip" ItemStyle-HorizontalAlign="Left" HeaderText="Descrip"></asp:BoundColumn>
									<asp:BoundColumn DataField="DocRefExt" ItemStyle-HorizontalAlign="Left" HeaderText="Ref.Externa"></asp:BoundColumn>
									<asp:BoundColumn DataField="Acta" ItemStyle-HorizontalAlign="Left" HeaderText="Acta"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!---
------------------ FIN Panel Resultado ------------
-->
		</form>
	</body>
</HTML>
