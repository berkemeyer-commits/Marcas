<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Reports.ClientActivityReport" CodeFile="ClientActivityReport.aspx.cs" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Actividad de Clientes</title>
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
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<h1 class="titulo">Informes Gerenciales</h1>
			<h2 class="subtitulo">Actividad de Cliente</h2>
			<br>
			<div class="filterbox-wrapper">
				<div class="filterbox">
					<div class="filterbox-title">Criterios de Filtro</div>
					<br>
					<asp:label id="Label1" runat="server" Width="64px">Fecha</asp:label><asp:textbox id="txtFecInicio" runat="server" Width="72px"></asp:textbox><asp:textbox id="txtFecFin" runat="server" Width="72px"></asp:textbox><br>
					<asp:label id="Label2" runat="server" Width="64px">Cliente</asp:label><ecctrl:eccombo id="cbxClienteID" runat="server" Width="368px" ShowLabel="False" Label="cbxCliente"></ecctrl:eccombo><br>
					<asp:label id="Label3" runat="server" Width="64px">Propietario</asp:label><ecctrl:eccombo id="cbxPropietarioID" runat="server" Width="368px" ShowLabel="False" Label="cbxPropietario"></ecctrl:eccombo>
					<hr>
					Opciones
					<br>
					<asp:checkbox id="chkIncNuestras" runat="server" Width="168px" Text="Incluir Marcas Nuestras"
						Checked="True"></asp:checkbox><asp:checkbox id="chkIncResumen" runat="server" Text="Incluir Resumen" Checked="True"></asp:checkbox><br>
					<asp:checkbox id="chkInTerceros" runat="server" Width="168px" Text="Incluir Marcas Vigiladas"
						Checked="True"></asp:checkbox><br>
				</div>
				<div class="btnBar"><asp:button id="btnGenerar" runat="server" Text="Generar Reporte" CssClass="Button" onclick="btnGenerar_Click"></asp:button></div>
				<br>
				<div class="infobox">
					<div class="infobox-title">Información</div>
					<ul>
						<li>
						Fecha: Corresponde a la fecha de&nbsp;vencimiento de las marcas&nbsp;
						<li>
						Cliente: Cliente de la marca
						<LI>
							Propietario: Propietario de la Marca.&nbsp;
						</LI>
					</ul>
				</div>
			</div>
		</form>
	</body>
</HTML>
