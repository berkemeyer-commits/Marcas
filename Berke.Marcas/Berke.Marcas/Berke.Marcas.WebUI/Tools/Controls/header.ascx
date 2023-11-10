<%@ Register TagPrefix="uc1" TagName="FormValidator" Src="FormValidator.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FieldValidator" Src="FieldValidator.ascx" %>
<%@ Control Language="c#" Inherits="Berke.Marcas.WebUI.Controls.LoggedInHeader" CodeFile="Header.ascx.cs" %>
<HEAD>
	<LINK href="../../tools/css/globalstyle.css" type="text/css" rel="stylesheet">
	<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
	<LINK href="../Tools/js/window/css/modal-message.css" type="text/css" rel="stylesheet">
</HEAD>
<script type="text/javascript">
		<!--
			function redimensionar(imagen){
				if (imagen.width>imagen.height){
					imagen.width=200;
				}
				else {
					imagen.height=200;
				}
				return true;
			}
		function setVisible(element) {
			var me = document.getElementById(element);
			var imagen = document.getElementById('img_'+element);
			//alert(me.style.display);
			if (me.style.display=="none" ){
				me.style.display="inline";
				imagen.src='../Tools/imx/minus.bmp';
			}
			else {
				if (me.style.display=="inline" || me.style.display=="block" || me.style.display=="" ){
					me.style.display="none";
					imagen.src='../Tools/imx/more.bmp';
				}
			}
		}		
		
		function closeDiv(element) {
			var me = document.getElementById(element);

			//alert(me.style.display);
			if (me.style.display=="none" ){
				me.style.display="inline";				
			}
			else {
				if (me.style.display=="inline" || me.style.display=="block" || me.style.display=="" ){
					me.style.display="none";
				}
			}
			return true;
		}	
		
		function form_onEnter(boton){
			if(event.keyCode==13){
				event.keyCode=null;
				window.document.getElementById(boton).click();
			}    
			return true;
		}
		
		-->
</script>
<!-- BEGIN - Necesario para los Validators -->
<script src="../Tools/js/window/js/ajax.js" type="text/javascript"></script>
<script src="../Tools/js/window/js/modal-message.js" type="text/javascript"></script>
<script src="../Tools/js/window/js/ajax-dynamic-content.js" type="text/javascript"></script>
<script src="../Tools/js/validators.js" type="text/javascript"></script>
<!-- END - Necesario para los Validators -->
<!-- NAVIGATION-->
<div class="headHide" onclick="javascript:closeDiv('divHeaderPrint'); closeDiv('divHeader');">
<asp:label id="lblDBName" Font-Bold="True" ForeColor="White" Font-Size="XX-Small" runat="server"></asp:label>
<asp:label id="lblGreeting" BorderColor="Gray" Font-Bold="True" ForeColor="Black" BackColor="Transparent"
			runat="server" enableviewstate="False"></asp:label>
</div>
<div class="headPrint" id="divHeaderPrint"><span class="titulo">Berkemeyer</span></div>
<script type="text/javascript">
		// Ocultamos por defecto la cabecera para Impresión
		closeDiv('divHeaderPrint');
</script>
<div id="divHeader">
	<table id="Branding" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TBODY>
			<tr>
				<td vAlign="top">
					<!-- MAIN CONTENT BEGINS-->
					<table id="mainTable" cellSpacing="0" cellPadding="0" width="100%" background="../Tools/imx/header.jpg"
						border="0">
						<TR>
							<TD align="right" width="65%"><IMG height="56" src="../Tools/imx/invisible.gif" width="1" align="left">
								<div style="MARGIN-TOP: 0px">&nbsp;</div>
							</TD>
							<td class="search" vAlign="middle" align="right" background="../Tools/imx/titlebg.gif">
								<table width="100%">
									<tr>
										<td vAlign="middle"><asp:dropdownlist id="ddlSearch" runat="server">
												<asp:ListItem Value="0">Registro</asp:ListItem>
												<asp:ListItem Value="1">Acta</asp:ListItem>
												<asp:ListItem Value="2">ExpedienteID</asp:ListItem>
											</asp:dropdownlist><asp:textbox id="txtBuscar" runat="server" Width="72px" CssClass="search"></asp:textbox><asp:button id="btnBuscar" runat="server" CssClass="btnSearch" Text="Buscar" onclick="btnBuscar_Click"></asp:button></td>
									</tr>
								</table>
							</td>
						</TR>
					</table>
					<table style="MARGIN-TOP: 0pt" cellSpacing="0" cellPadding="0" width="100%" background="../Tools/imx/head_borde.gif">
						<tr>
							<td><IMG height="6" src="../Tools/imx/invisible.gif" width="1" align="right"></td>
						</tr>
					</table>
					<table style="MARGIN-TOP: 0pt" cellSpacing="0" cellPadding="0" width="100%" background="../Tools/imx/head_fondo.gif">
						<tr>
							<td vAlign="top" align="center" width="20%"><asp:label id="lbMenu1" Font-Bold="True" runat="server" Width="100%" CssClass="FuenteViasDirPF1"></asp:label></td>
							<td vAlign="top" align="center" width="20%"><asp:label id="lbMenu2" Font-Bold="True" runat="server" Width="100%" CssClass="FuenteViasDirPF1"></asp:label></td>
							<td vAlign="top" align="center" width="20%"><asp:label id="lbMenu3" Font-Bold="True" runat="server" Width="100%" CssClass="FuenteViasDirPF1"></asp:label></td>
							<td vAlign="top" align="center" width="20%"><asp:label id="lbMenu4" Font-Bold="True" runat="server" Width="100%" CssClass="FuenteViasDirPF1"></asp:label></td>
							<td vAlign="top" align="center" width="20%"><asp:label id="lbMenu5" Font-Bold="True" runat="server" Width="100%" CssClass="FuenteViasDirPF1"></asp:label></td>
						</tr>
					</table>
					<table cellSpacing="0" cellPadding="0" width="100%" background="../Tools/imx/head_fondo.gif">
						<tr>
							<td vAlign="top" width="20%"><asp:dropdownlist id="ddlMenu1" runat="server" Width="100%" CssClass="ddmenu" AutoPostBack="True" onselectedindexchanged="ddlMenu1_SelectedIndexChanged"></asp:dropdownlist></td>
							<td vAlign="top" width="20%" colSpan="2"><asp:dropdownlist id="ddlMenu2" runat="server" Width="100%" CssClass="ddmenu" AutoPostBack="True" onselectedindexchanged="ddlMenu1_SelectedIndexChanged"></asp:dropdownlist></td>
							<td vAlign="top" width="20%"><asp:dropdownlist id="ddlMenu3" runat="server" Width="100%" CssClass="ddmenu" AutoPostBack="True" onselectedindexchanged="ddlMenu1_SelectedIndexChanged"></asp:dropdownlist></td>
							<td vAlign="top" width="20%"><asp:dropdownlist id="ddlMenu4" runat="server" Width="100%" CssClass="ddmenu" AutoPostBack="True" onselectedindexchanged="ddlMenu1_SelectedIndexChanged"></asp:dropdownlist></td>
							<td vAlign="top" width="20%"><asp:dropdownlist id="ddlMenu5" runat="server" Width="100%" CssClass="ddmenu" AutoPostBack="True" onselectedindexchanged="ddlMenu1_SelectedIndexChanged"></asp:dropdownlist></td>
						</tr>
					</table>
				</td>
			</tr>
		</TBODY>
	</table>
	</TR></TBODY></TABLE>
	<table cellSpacing="0" cellPadding="0" width="90%" align="right">
		<tr>
			<td width="90%"></td>
			<td vAlign="middle" align="right" width="5%"><IMG hspace="5" src="../Tools/imx/icon_inicio.gif">
			</td>
			<td vAlign="middle" align="left"><A href="../Home/Login.aspx">Inicio</A></SPAN>
			</td>
		</tr>
	</table>
</div>
<!--BEGIN Validacion para la Busqueda--><uc1:formvalidator id="Formvalidator4" runat="server" Message="Problemas en la Búsqueda" ButtonId="Header1_btnBuscar">
	<uc1:FieldValidator runat="server" type="required" ControlToValidate="Header1_txtBuscar" Message="Debe especificar el criterio de filtro."
		ID="Fieldvalidator5" NAME="Fieldvalidator4" />
</uc1:formvalidator><asp:label id="lblScript" runat="server" Visible="False">lblScript</asp:label>
<!--END Validacion para la Busqueda-->
