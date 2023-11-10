<%@ Reference Page="~/home/cambioestado.aspx" %>
<%@ Page language="c#" smartnavigation="true" Inherits="Berke.Marcas.WebUI.Home.MarcaDetalleL" CodeFile="MarcaDetalleL.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Detalles de la Marca</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
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
		}			
		-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table width="90%">
				<tr>
					<td align="right" width="90%"></td>
					<td align="right"><asp:Label id="lblModif" runat="server">Modificar</asp:Label></td>
				</tr>
			</table>
			<P><asp:label id="lblMarcaBasic" runat="server" CssClass="Texto">lblMarcaBasic</asp:label></P>
			<P><asp:label id="lblExpedienteBasic" runat="server" CssClass="Texto">lblExpedienteBasic</asp:label>
			</P>
			<p></p>
			<asp:label id="lblInstrucciones" runat="server" CssClass="Texto">lblInstrucciones</asp:label>
			<P></P>
			<p><asp:label id="lblCambioEstado" runat="server" CssClass="Texto">lblCambioEstado</asp:label></p>
			<asp:Button id="btnEliminar" style="Z-INDEX: 101; LEFT: 616px; POSITION: absolute; TOP: 160px"
				runat="server" Visible="False" Text="Eliminar Marca" Width="104px" Height="20px" onclick="btnEliminar_Click"></asp:Button>
			<asp:LinkButton id="lnkEliminar" style="Z-INDEX: 102; LEFT: 648px; POSITION: absolute; TOP: 160px"
				runat="server" onclick="lnkEliminar_Click">Eliminar</asp:LinkButton>
			<asp:CheckBox id="chkCascada" style="Z-INDEX: 103; LEFT: 616px; POSITION: absolute; TOP: 176px"
				runat="server" Text="Cascada"></asp:CheckBox>
			<P></P>
			<asp:linkbutton id="lnkJerarquia" runat="server" Font-Size="X-Small" onclick="lnkJerarquia_Click"> Jerarquía de Expedientes</asp:linkbutton>
			<asp:label id="lblJerarquia" runat="server">lblJerarquia</asp:label>
			<P>
				<asp:linkbutton id="lnkHistorico" runat="server" Font-Size="X-Small" onclick="Linkbutton1_Click">Histórico de Situaciones</asp:linkbutton>
				<asp:label id="lblHistorico" runat="server" CssClass="Texto" Visible="False">Historico</asp:label>
			</P>
			<P>
				<asp:linkbutton id="lnkCorresp" runat="server" Font-Size="X-Small" onclick="lnkCorresp_Click">Correspondencia</asp:linkbutton>
				<asp:label id="lblCorresp" runat="server" CssClass="Texto" Visible="False">Corresp</asp:label>
			</P>
			<P><asp:linkbutton id="lnkMerge" runat="server" Font-Size="X-Small" Visible="False" OnClick="lnkMerge_Click">Merge</asp:linkbutton><asp:label id="lblMerge" runat="server" CssClass="Texto" Visible="True">Merge</asp:label></P>
			<P><asp:linkbutton id="lnkAvisosOpo" runat="server" Font-Size="X-Small" Visible="False">Avisos de Oposición</asp:linkbutton><asp:label id="lblAvisosOpo" runat="server" CssClass="Texto">AvisosOpo</asp:label></P>
			<P><asp:linkbutton id="lnkDistribuidores" runat="server" Font-Size="X-Small" Visible="False">Distribuidores de la Marca</asp:linkbutton>
				<asp:Label id="lblDistribuidores" runat="server">Distribuidores</asp:Label></P>
			<P>
				<asp:linkbutton id="lnkCambioCLiente" runat="server" Font-Size="X-Small" onclick="lnkCambioCLiente_Click">Cambios de Cliente</asp:linkbutton>
				<asp:label id="lblCambioCLiente" runat="server" CssClass="Texto" Visible="False">CambioCLiente</asp:label>
			</P>
			<P>
				<asp:linkbutton id="lnkBoletin" runat="server" Font-Size="X-Small" onclick="lnkBoletin_Click">Datos de Boletin</asp:linkbutton>
				<asp:label id="lblBoletin" runat="server" CssClass="Texto" Visible="False">lblBoletin</asp:label>
			</P>
		</form>
		<br>
	</body>
</HTML>
