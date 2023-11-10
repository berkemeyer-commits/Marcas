<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.ExpeMarcaCambioEstado" CodeFile="ExpeMarcaCambioEstado.aspx.cs" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Cambios de estado</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server"  onkeypress="form_onEnter('txtBuscar');"> 
			<asp:panel id="pnlCabecera" runat="server" Height="56px">
				<P>
					<UC1:HEADER id="Header1" runat="server"></UC1:HEADER></P>
				<P class="titulo">Cambio de Estado de&nbsp;Marcas&nbsp;</P>
			</asp:panel>
			<br>
		<div style="margin-left:5px; width:98%" class="hideTitle" title="Ocultar/visualizar formulario de b&uacute;squeda" onclick="closeDiv('pnlBuscar')">Ubicar Expediente
		</div>   
			<asp:panel id="pnlBuscar" runat="server">
				<TABLE id="tblBuscar" width="98%" class="infoMacro">
					<TR width="383">
						<TD style="WIDTH: 485px">
							<asp:Label id="Label4" runat="server" CssClass="Etiqueta2" Width="95px">
      Exped. ID
        </asp:Label>
							<asp:textbox id="txtExpeID" runat="server" Width="75px"></asp:textbox></TD>
					</TR>
					<TR width="383">
						<TD style="WIDTH: 485px">
							<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="95px">
      Registro
        </asp:Label>
							<asp:textbox id="txtRegistroNro" runat="server" Width="75px"></asp:textbox></TD>
					</TR>
					<TR width="383">
						<TD style="WIDTH: 485px">
							<asp:Label id="Label6" runat="server" CssClass="Etiqueta2" Width="95px">
      Marca ID
        </asp:Label>
							<asp:textbox id="txtMarcaID" runat="server" Width="75px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 485px; HEIGHT: 23px" width="485">
							<asp:Label id="Label3" runat="server" CssClass="Etiqueta2" Width="95px">
      Acta / Año
						</asp:Label>
							<asp:TextBox id="txtActaNro" runat="server" Width="75px"></asp:TextBox>&nbsp;/
							<asp:textbox id="txtActaAnio" runat="server" Width="75px"></asp:textbox></TD>
					<TR>
						<TD style="WIDTH: 485px; HEIGHT: 19px" align="right">
							<asp:button id="txtBuscar" runat="server" Height="22px" CssClass="Button" Width="96px" Text="Buscar"
								Font-Bold="True" onclick="txtBuscar_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD colSpan="7">
							<asp:label id="lblMensaje" runat="server" Font-Bold="True" Font-Size="X-Small"></asp:label></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!-- 
										Panel  Datos de Expediente
			--><asp:panel id="pnlExpeDatos" runat="server" Width="100%">
				<TABLE id="tblDatosExpediente" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD>
							<div style="margin-left:5px; margin-top:1px; width:98%" class="hideTitle" title="Ocultar/visualizar formulario de b&uacute;squeda" onclick="closeDiv('Panel1')">Datos del Expediente
							</div> 
							<asp:Panel id="Panel1" runat="server" Width="98%" CssClass="infoMacro">
								
								<P>
									<asp:Label id="lblSeparador1" runat="server" CssClass="Etiqueta2" Width="60px"></asp:Label>
									<asp:Label id="lblExpeDescrip" runat="server" CssClass="Texto" Width="370px">lblExpeDescrip</asp:Label></P>
								<P>
									<asp:Label id="Label2" runat="server" CssClass="Etiqueta2" Width="60px"></asp:Label>
									<asp:Label id="lblMarcaDescrip" runat="server" Height="8px" CssClass="Texto" Width="370px">lblMarcaDescrip</asp:Label></P>
							</asp:Panel></TD>
					</TR>
				</TABLE>
				</asp:panel><asp:panel id="pnlDatos" runat="server">
					<P class="subtitulo">Actualización</P>
					<P>
						<TABLE id="tblDatos" class="infoMacro" width="98%">
							<TR>
								<TD style="HEIGHT: 11px">
									<asp:Label id="Label5" runat="server" CssClass="Etiqueta2" Width="80px">Pasar A</asp:Label>
									<asp:DropDownList id="ddlOperacion" runat="server" Width="300px" AutoPostBack="True" onselectedindexchanged="ddlOperacion_SelectedIndexChanged"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 12px">
									<asp:Label id="lblMotivo" runat="server" CssClass="Etiqueta2" Width="80px">Motivo</asp:Label>
									<asp:DropDownList id="ddlMotivo" runat="server" Width="448px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 55px" vAlign="middle">
									<asp:Label id="Label8" runat="server" Height="51px" CssClass="Etiqueta2" Width="80px">Observación</asp:Label>
									<asp:TextBox id="txtObs" runat="server" Height="52px" Width="536px" TextMode="MultiLine" Rows="3"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD vAlign="middle">
									<asp:Label id="lblPropietario" runat="server" Height="16px" CssClass="Etiqueta2" Width="80px">Id. Propietario</asp:Label>
									<asp:TextBox id="txtPropietarios" runat="server" Height="20px" Width="192px" TextMode="SingleLine"
										Rows="1"></asp:TextBox>
									<asp:Label id="lblPoder" runat="server" Height="16px" CssClass="Etiqueta2" Width="80px">Id. Poder</asp:Label>
									<asp:TextBox id="txtPoder" runat="server" Height="20px" Width="192px" TextMode="SingleLine" Rows="1"></asp:TextBox></TD>
							</TR>
						</TABLE>
					</P>
				</asp:panel>
			<!-- 
										Panel  de Incorporar 
			 --><asp:panel id="pnlIncorporar" runat="server">
				<TABLE id="tblIncorporar" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 72px; HEIGHT: 23px">
							<asp:Label id="Label7" runat="server" CssClass="Etiqueta2" Width="80px">Facturable</asp:Label></TD>
						<TD style="WIDTH: 624px; HEIGHT: 23px">
							<asp:DropDownList id="ddlFacturable" runat="server">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="Si">Si</asp:ListItem>
								<asp:ListItem Value="No">No</asp:ListItem>
							</asp:DropDownList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 72px">
							<asp:Label id="Label11" runat="server" CssClass="Etiqueta2" Width="80px">Cliente</asp:Label></TD>
						<TD style="WIDTH: 624px">
							<ecctrl:ecCombo id="cbxCliente" runat="server" Width="520px" ShowLabel="False"></ecctrl:ecCombo></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 72px">
							<asp:Label id="Label12" runat="server" CssClass="Etiqueta2" Width="80px">Agente Local</asp:Label></TD>
						<TD style="WIDTH: 624px">
							<CUSTOM:DROPDOWN id="ddlAgenteBerke" runat="server" Width="368px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
					</TR>
				</TABLE>
			</asp:panel><!-- 
										Panel  Botones 
			 -->
			<P></P>
			<asp:panel id="pnlBotones" runat="server">
				<TABLE id="tblBotones" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR align="center">
						<TD>
							<asp:Button id="btnGrabar" runat="server" Height="27px" Width="78px" Text="Grabar" onclick="btnGrabar_Click"></asp:Button></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<P></P>
		</form>
	</body>
</HTML>
