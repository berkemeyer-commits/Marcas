<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.CtrlGenHDesc" CodeFile="ctrlgenhdesc.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Exped. de Marca</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<!--<meta content="JavaScript" name="vs_defaultClientScript">-->
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/globalstyle.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssMainStyle.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssDgEstiloGeneral.css">
		<script language="JavaScript">
function restablecer(pCod){
  /* alert('ID a transferir ' + pCod ); */
  window.opener.document.Form1.ExpeID.value = pCod ;
  window.opener.focus();
  window.close();
}
			
		</script>
	</HEAD>
	<body>
		<form id="Form1" onkeypress="form_onEnter('btBuscar');" method="post" runat="server">
			<!-- 

   Cabecera 
		
   /-->
			<p><uc1:header id="Header1" runat="server"></uc1:header></p>
			<span class="titulo">
				<asp:label id="lblTitulo" runat="server" Font-Bold="True" Font-Size="Medium" CssClass="titulo">Consulta de Generación de Hojas Descriptivas</asp:label></span><br>
			<!-- 

   Fin Cabecera

   /-->
			<table id="Table1" width="720">
				<tr>
					<td><asp:label id="lblTituloAclaracion" runat="server" CssClass="subtitulo" Width="304px"></asp:label></td>
					<td>
						<P><asp:label id="lblMensaje" runat="server" Font-Bold="True" CssClass="msg" Width="396px" ForeColor="#C00000"></asp:label></P>
					</td>
				</tr>
			</table>
			<div style="WIDTH: 720px; MARGIN-LEFT: 5px" class="hideTitle" title="Ocultar/visualizar formulario de búsqueda"
				onclick="closeDiv('pnlBuscar')">Criterio de búsqueda
			</div>
			<div id="formBusqueda"><asp:panel id="pnlBuscar" runat="server" Width="720px">
					<TABLE id="Table2" border="0" width="100%">
						<TR>
							<TD vAlign="top">
								<TABLE id="Table3" class="infoMacro" width="99%">
									<TR>
										<TD colSpan="1">&nbsp;</TD>
									</TR>
									<TR>
										<TD width="80px">
											<asp:Label ID="Label4" runat="server" CssClass="Etiqueta1" DESIGNTIMEDRAGDROP="1808" Width="75px">Expediente ID</asp:Label>
                                        </TD>
										<TD>
											<asp:textbox id="txtExpedienteID_min" runat="server" CssClass="inpform" Width="88px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="Label6" runat="server" CssClass="Etiqueta1" Width="56px" DESIGNTIMEDRAGDROP="1513">HI Nro</asp:Label></TD>
										<TD>
											<asp:textbox id="txtHINro" runat="server" Width="79px" DESIGNTIMEDRAGDROP="1514" Height="20px"></asp:textbox>
											<asp:Label id="Label7" runat="server" CssClass="Etiqueta1" Width="44px" Height="16px">Hi Año</asp:Label>
											<asp:textbox id="txtHiAnio" runat="server" Width="60px"></asp:textbox></TD>
									</TR>
                                    <TR>
										<TD>
											<asp:Label id="lblActaNro_min" runat="server" CssClass="Etiqueta1" Width="56px" DESIGNTIMEDRAGDROP="1513">Acta</asp:Label></TD>
										<TD>
											<asp:textbox id="txtActaNro_min" runat="server" Width="79px" DESIGNTIMEDRAGDROP="1514" Height="20px"></asp:textbox>
											<asp:Label id="lblActaAnio" runat="server" CssClass="Etiqueta1" Width="44px" Height="16px">Año</asp:Label>
											<asp:textbox id="txtActaAnio" runat="server" Width="60px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD>
                                            <asp:Label ID="Label3" runat="server" CssClass="Etiqueta1" DESIGNTIMEDRAGDROP="1808" Width="56px">Denominación</asp:Label>
                                        </TD>
										<TD>
                                            <asp:TextBox ID="txtDenominacion" runat="server" CssClass="inpform" Width="298px"></asp:TextBox>
                                        </TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 22px">
											<asp:Label id="lblTramiteID" runat="server" CssClass="Etiqueta1" Width="71px" Height="16px">Trámite</asp:Label></TD>
										<TD style="HEIGHT: 22px">
											<CUSTOM:DROPDOWN id="ddlTramiteID" runat="server" Width="208px" AutoPostBack="True"></CUSTOM:DROPDOWN></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label ID="Label2" runat="server" CssClass="Etiqueta1" DESIGNTIMEDRAGDROP="1808" Width="75px">Agente Local</asp:Label>
                                        </TD>
										<TD>
											<ecctrl:ecCombo ID="cbxAgenteLocalID" runat="server" ShowLabel="False" Width="306px" Label="cbxAgenteLocalID" />
                                        </TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="Label1" runat="server" CssClass="Etiqueta1" Width="56px" DESIGNTIMEDRAGDROP="1808">Usuario</asp:Label></TD>
										<TD>
                                            <ecctrl:ecCombo ID="cbxUsuarioID" runat="server" ShowLabel="False" Width="306px" Label="cbxUsuarioID" />
											</TD>
									</TR>
									<TR>
										<TD>
											<asp:Label ID="Label5" runat="server" CssClass="Etiqueta1" DESIGNTIMEDRAGDROP="1808" Width="56px">Fecha Gen.</asp:Label>
                                        </TD>
										<TD>
											<asp:TextBox ID="txtFechaGeneracion" runat="server" DESIGNTIMEDRAGDROP="1807" Width="152px"></asp:TextBox>
                                        </TD>
									</TR>
								</TABLE>
								<P align="left">
									<asp:button id="btBuscar" runat="server" CssClass="Button" Font-Bold="True" Width="72px" Text="Buscar" onclick="btBuscar_Click"></asp:button></P>
							</TD>
							
						</TR>
					</TABLE>
				</asp:panel></div>
			<!---
------------------ PANEL RESULTADO ------------
--><asp:panel id="pnlResultado" runat="server" Visible="True"><SPAN class="subtitulo">Resultado de 
					la Búsqueda </SPAN>
				<BR>
				<BR>
				<asp:button id="btnGenDoc" runat="server" CssClass="btn_doc" Font-Bold="True" Width="48px" Text="doc" onclick="btnGenDoc_Click"></asp:button>
				<asp:button id="btnGenXls" runat="server" CssClass="btn_xls" Font-Bold="True" Width="48px" Text="xls" onclick="btnGenXls_Click"></asp:button>
				<BR>
				<BR>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="lblTituloGrid" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="dgResult" runat="server" CssClass="tbl" Width="100%" HorizontalAlign="Center"
								DataKeyField="ExpedienteID" AutoGenerateColumns="False">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<FooterStyle HorizontalAlign="Left"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="HINro" HeaderText="HI Nro"></asp:BoundColumn>
									<asp:BoundColumn DataField="HIAnio" HeaderText="Hi Año">
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NombrePila" HeaderText="Usuario Gen.">
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Denominacion" HeaderText="Denominación">
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ActaNro" HeaderText="Acta Nro."></asp:BoundColumn>
									<asp:BoundColumn DataField="ActaAnio" HeaderText="Acta Año">
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ExpedienteID" HeaderText="Exp. ID">
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Tramite" HeaderText="Trámite">
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Nombre" HeaderText="Agente">
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FechaHoraGeneracion" HeaderText="Fecha Hora Gen.">
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!---
------------------ FIN Panel Resultado ------------
--><br>
			<br>
		</form>
	</body>
</HTML>
