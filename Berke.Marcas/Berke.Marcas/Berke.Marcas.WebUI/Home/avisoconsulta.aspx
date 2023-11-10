<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.AvisoConsulta" CodeFile="AvisoConsulta.aspx.cs" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Aviso</title>
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
  window.opener.document.Form1.AvisoID.value = pCod ;
  window.opener.focus();
  window.close();
}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('btBuscar');">
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<P class="titulo">Consultar Aviso</P>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnlBuscar" runat="server">
				<P class="subtitulo">Búsqueda</P>
				<P></P>
				<TABLE id="tblBuscar" class="infoMacro" width="750">
					<TR>
						<TD style="WIDTH: 352px" width="352">
							<asp:Label id="lblID_min" runat="server" CssClass="Etiqueta2" Width="90px">
          ID
        </asp:Label>
							<asp:textbox id="txtID_min" runat="server" Width="102px"></asp:textbox>
							<asp:Label id="lblID_max" runat="server" CssClass="Etiqueta2" Width="8px"> - </asp:Label>
							<asp:textbox id="txtID_max" runat="server" Width="102px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 352px" width="352">
							<asp:Label id="lblFechaAlta_min" runat="server" CssClass="Etiqueta2" Width="90px">
          Fecha Alta
        </asp:Label>
							<asp:textbox id="txtFechaAlta_min" runat="server" Width="102px"></asp:textbox>
							<asp:Label id="lblFechaAlta_max" runat="server" CssClass="Etiqueta2" Width="8px">
           - 
        </asp:Label>
							<asp:textbox id="txtFechaAlta_max" runat="server" Width="102px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblAsunto" runat="server" CssClass="Etiqueta2" Width="90px">Asunto</asp:Label>
							<asp:textbox id="txtAsunto" runat="server" Width="210px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 352px" width="352">
							<asp:Label id="lblFechaAviso_min" runat="server" CssClass="Etiqueta2" Width="90px">
          Fecha Aviso
        </asp:Label>
							<asp:textbox id="txtFechaAviso_min" runat="server" Width="102px"></asp:textbox>
							<asp:Label id="lblFechaAviso_max" runat="server" CssClass="Etiqueta2" Width="8px">
           - 
        </asp:Label>
							<asp:textbox id="txtFechaAviso_max" runat="server" Width="102px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblContenido" runat="server" CssClass="Etiqueta2" Width="90px">Contenido</asp:Label>
							<asp:textbox id="txtContenido" runat="server" Width="210px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 352px" width="352">
							<asp:Label id="lblPendiente" runat="server" CssClass="Etiqueta2" Width="90px">
         Pendiente
        </asp:Label>
							<asp:DropDownList id="ddlPendiente" runat="server" Width="102px">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="Si">Si</asp:ListItem>
								<asp:ListItem Value="No">No</asp:ListItem>
							</asp:DropDownList></TD>
						<TD>
							<asp:Label id="lblIndicaciones" runat="server" CssClass="Etiqueta2" Width="90px">Indicaciones</asp:Label>
							<asp:textbox id="txtIndicaciones" runat="server" Width="208px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblRemitente" runat="server" CssClass="Etiqueta2" Width="90px">Remitente</asp:Label>
							<CUSTOM:DROPDOWN id="ddlRemitente" runat="server" Width="153px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
						<TD style="HEIGHT: 22px">
							<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="90px">Prioridad</asp:Label>
							<CUSTOM:DROPDOWN id="ddlPrioridad" runat="server" Width="153px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 352px" width="352">
							<asp:Label id="lblDestinatario" runat="server" CssClass="Etiqueta2" Width="90px">
         Destinatario
       </asp:Label>
							<CUSTOM:DROPDOWN id="ddlDestinatario" runat="server" Width="153px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
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
								<asp:button id="btBuscar" runat="server" CssClass="Button" Text="Buscar" Font-Bold="True" onclick="btBuscar_Click"></asp:button></P>
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
				<P class="subtitulo">Resultado de la Búsqueda</p>
				<p><asp:Button id="btnMostrarFiltro" runat="server" Width="56px" Text="Filtrar" Height="18px" onclick="btnMostrarFiltro_Click"></asp:Button></P>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head">
								<TR><TD>
										<asp:label id="lblTituloGrid" runat="server"></asp:label>
									</TD>
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
									<asp:TemplateColumn HeaderText="Sel.">
										<ItemTemplate>
											<asp:CheckBox id="chkSel" runat="server" Checked="False" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:ButtonColumn ItemStyle-HorizontalAlign="Left" Text="Descripcion" DataTextField="Asunto" HeaderText="Asunto"
										CommandName="Select"></asp:ButtonColumn>
									<asp:BoundColumn DataField="Origen" ItemStyle-HorizontalAlign="Left" HeaderText="Origen"></asp:BoundColumn>
									<asp:BoundColumn DataField="Destino" ItemStyle-HorizontalAlign="Left" HeaderText="Destino"></asp:BoundColumn>
									<asp:BoundColumn DataField="Contenido" ItemStyle-HorizontalAlign="Left" HeaderText="Contenido"></asp:BoundColumn>
									<asp:BoundColumn DataField="Indicaciones" ItemStyle-HorizontalAlign="Left" HeaderText="Indicaciones"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="ID">
										<ItemTemplate>
											<asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' />
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
							<asp:Button id="btnAtender" CssClass="Button" runat="server" Text="Concluido" ToolTip='Marca como "Concluidos" los mensajes seleccionados' onclick="btnAtender_Click"></asp:Button>&nbsp;&nbsp;
							<asp:Button id="btnMarcar" CssClass="Button" runat="server" Text="Selec.Todo" ToolTip="Seleccionar todos los registros de la grilla" onclick="btnMarcar_Click"></asp:Button></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!---
------------------ FIN Panel Resultado ------------
--></form>
		</TR></TBODY></TABLE></FORM>
	</body>
</HTML>
