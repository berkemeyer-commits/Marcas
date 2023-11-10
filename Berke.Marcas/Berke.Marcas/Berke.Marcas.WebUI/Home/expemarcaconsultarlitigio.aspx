<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.ExpeMarcaConsultarLitigio" CodeFile="ExpeMarcaConsultarLitigio.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Exped. de Marca</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
           

            function restablecer(pCod){
                /* alert('ID a transferir ' + pCod ); */
                window.opener.document.Form1.ExpeID.value = pCod ;
                window.opener.focus();
                window.close();
            }

            function checkAll() {
                var grid = document.getElementById("dgResult");
                //var chk = document.getElementById("chkMarcar").checked;

                var inputs = grid.getElementsByTagName("input");

                for (var i = 0; i < inputs.length; i++) {
                    if ((inputs[i].type === "checkbox") && (inputs[i].id.indexOf("_cbSel") > -1)) {
                        //document.getElementById(inputs[i].id).checked = chk;
                        document.getElementById(inputs[i].id).checked = !document.getElementById(inputs[i].id).checked;
                    }
                }
            }

            function cntSel() {
                var grid = document.getElementById("dgResult");
                //var chk = document.getElementById("chkMarcar").checked;

                var inputs = grid.getElementsByTagName("input");

                var cnt = 0;
                for (var i = 0; i < inputs.length; i++) {
                    if ((inputs[i].type === "checkbox") && (inputs[i].id.indexOf("_cbSel") > -1)) {
                        if (document.getElementById(inputs[i].id).checked)
                         cnt++;
                    }
                }
                
                return cnt;
            }

            function checkSel() {
                var cnt = cntSel();
                
                if (cnt == 0) {
                    document.getElementById('lblError').innerHTML = 'Debe seleccionar alguna marca para generar el reporte';
                    //$('#lblError').innerHTML = 'Debe seleccionar alguna marca para generar el reporte';
                }
                else {
                    //var obj = document.getElementById("btGenDoc");
                    document.getElementById('btnGenReporte').click();
                }
            }
		</script>
	</HEAD>
	<body>
		<form onkeypress="form_onEnter('btBuscar');" id="Form1" method="post" runat="server">
			<!-- 

   Cabecera 
		
   /-->
			<p><uc1:header id="Header1" runat="server"></uc1:header>
            </p>
			<span class="titulo">
				<asp:label id="lblTitulo" runat="server" Font-Size="Medium" CssClass="titulo"> Consulta de Marcas</asp:label></span>
			<table width="720">
				<tr>
					<td><asp:label id="lblTituloAclaracion" runat="server" CssClass="subtitulo" Width="304px">Último trámite REG/GEN</asp:label></td>
					<td>
						<P><asp:label id="lblMensaje" runat="server" CssClass="msg" Width="396px" ForeColor="#C00000"
								Font-Bold="True"></asp:label></P>
					</td>
				</tr>
			</table>
			<!-- 

   Fin Cabecera

   /-->
			<div class="hideTitle" title="Ocultar/visualizar formulario de búsqueda" style="WIDTH: 720px; MARGIN-LEFT: 5px"
				onclick="closeDiv('pnlBuscar')">Criterio de búsqueda
			</div>
			<asp:panel id="pnlBuscar" runat="server" Width="768px">
				<DIV id="formBusqueda">
					<TABLE id="tblBuscar">
						<TR>
							<TD style="/*HEIGHT: 318px*/" vAlign="top" width="50%">
								<TABLE class="infoMacro" width="99%">
									<TR>
										<TD colSpan="2">Marca</TD>
									</TR>
									<TR>
										<TD style="WIDTH: 79px">
											<asp:Label id="lblDenominacion" runat="server" CssClass="Etiqueta2" Width="72px">Denominación</asp:Label></TD>
										<TD>
											<asp:textbox id="txtDenomEmpieza" runat="server" Width="208px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 79px">
											<asp:Label id="Label4" runat="server" CssClass="Etiqueta2" Width="72px">Clase</asp:Label></TD>
										<TD>
											<asp:textbox id="txtClaseNro" runat="server" Width="60px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 79px"></TD>
										<TD>
											<asp:RadioButtonList id="rbMarcaVigilada" runat="server" Width="32px" Height="7px" RepeatDirection="Horizontal"
												BorderStyle="Solid" BorderWidth="1px" ToolTip="Tipo de expediente">
												<asp:ListItem Value=" " Selected="True">Todas</asp:ListItem>
												<asp:ListItem Value="Si">Propias</asp:ListItem>
												<asp:ListItem Value="No">Terceros</asp:ListItem>
											</asp:RadioButtonList></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 79px">
											<asp:Label id="Label3" runat="server" CssClass="Etiqueta2" Width="72px">Tipo</asp:Label></TD>
										<TD>
											<asp:CheckBoxList id="chkTipo" runat="server" RepeatDirection="Horizontal">
												<asp:ListItem Value="D">Denominativa</asp:ListItem>
												<asp:ListItem Value="F">Figurativa</asp:ListItem>
												<asp:ListItem Value="M">Mixta</asp:ListItem>
											</asp:CheckBoxList></TD>
                                        </TR>
                                        <TR>
										<TD></TD>
										<TD>
											<asp:CheckBoxList id="chkTipo1" runat="server" RepeatDirection="Horizontal">
												<asp:ListItem Value="T">Tridimensional</asp:ListItem>
                                                <asp:ListItem Value="O">Olfativa&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="S">Sonora</asp:ListItem>
                                            </asp:CheckBoxList></TD>
                                        	
									</TR>
									<TR>
										<TD style="WIDTH: 79px">
											<asp:Label id="lblActiva" runat="server" CssClass="Etiqueta2" Width="72px">Activa</asp:Label></TD>
										<TD>
											<asp:CheckBox id="chkActiva" runat="server"></asp:CheckBox></TD>
									</TR>
								</TABLE>
								<TABLE id="Table4" class="infoMacro" width="99%">
									<TR>
										<TD style="HEIGHT: 14px" colSpan="2">Propietario</TD>
									</TR>
									<TR>
										<TD style="WIDTH: 61px">
											<asp:Label id="lblPropietarioID" runat="server" CssClass="Etiqueta2" Width="72px">Codigo</asp:Label></TD>
										<TD>
											<asp:textbox id="txtPropietarioID" runat="server" Width="90px" ToolTip="Lista de IDs"></asp:textbox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 61px">
											<asp:Label id="Label5" runat="server" CssClass="Etiqueta2" Width="72px">Nombre</asp:Label></TD>
										<TD>
											<asp:textbox id="txtPropietarioNombre" runat="server" Width="208px" ToolTip="Subcadena"></asp:textbox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 61px">
											<asp:Label id="Label6" runat="server" CssClass="Etiqueta2" Width="72px">País</asp:Label></TD>
										<TD>
											<asp:textbox id="txtPropietarioPais" runat="server" Width="64px"></asp:textbox></TD>
									</TR>
								</TABLE>
								<BR>
								<asp:button id="btBuscar" runat="server" CssClass="Button" Width="80px" Font-Bold="True" Text="Buscar"></asp:button>&nbsp;
                                <%--<asp:button id="btGenDoc" runat="server" CssClass="Button" Width="88px" Font-Bold="True" Text="Generar Doc." Visible="false"></asp:button>--%>
								<%--<asp:CheckBox id="chkTablaPorMarca" runat="server" Text="Una tabla por marca" Checked="True" Visible="false"></asp:CheckBox>--%>
                                <%--<br />
                                <asp:Label ID="lblError" runat="server" Text="lblError" ForeColor="Red" Font-Bold="true" Visible="false" ClientIDMode="Static"></asp:Label>--%>
							</TD>
							<TD style="/*HEIGHT: 318px*/" vAlign="top" width="50%">
								<TABLE class="infoMacro" width="99%">
									<TR>
										<TD colSpan="2">Registro</TD>
									</TR>
									<TR>
										<TD style="WIDTH: 64px">
											<asp:Label id="Label2" runat="server" CssClass="Etiqueta2" Width="72px">Registro</asp:Label></TD>
										<TD>
											<asp:textbox id="txtRegistroNro_min" runat="server" Width="120px"></asp:textbox>
											<asp:Label id="lblRegistroAnio" runat="server" CssClass="Etiqueta2" Width="8px">Año</asp:Label>
											<asp:textbox id="txtRegistroAnio" runat="server" Width="60px"></asp:textbox>
											<asp:CheckBox id="chkBoxRegistroAnterior" runat="server" Text="Consultar Registro Anterior"></asp:CheckBox></TD>
									</TR>
								</TABLE>
								<TABLE id="Table1" class="infoMacro" width="99%">
									<TR>
										<TD colSpan="2">Expediente</TD>
									</TR>
									<TR>
										<TD style="WIDTH: 68px">
											<asp:Label id="lblActaNro_min" runat="server" CssClass="Etiqueta2" Width="72px">Acta</asp:Label></TD>
										<TD>
											<asp:textbox id="txtActaNro_min" runat="server" Width="112px"></asp:textbox>
											<asp:Label id="lblActaAnio" runat="server" CssClass="Etiqueta2" Width="24px">Año</asp:Label>
											<asp:textbox id="txtActaAnio" runat="server" Width="60px"></asp:textbox></TD>
									</TR>
								</TABLE>
								<TABLE id="Table2" class="infoMacro" width="99%">
									<TR>
										<TD style="HEIGHT: 14px" colSpan="2">Cliente/Agente</TD>
									</TR>
									<TR>
										<TD style="WIDTH: 57px">
											<asp:Label id="lblClienteID" runat="server" CssClass="Etiqueta2" Width="72px" DESIGNTIMEDRAGDROP="2272">Codigo</asp:Label></TD>
										<TD>
											<asp:textbox id="txtClienteID" runat="server" Width="90px" ToolTip="Lista de IDs" DESIGNTIMEDRAGDROP="2273"></asp:textbox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 57px">
											<asp:Label id="Label1" runat="server" CssClass="Etiqueta2" Width="72px" DESIGNTIMEDRAGDROP="2272">Nombre</asp:Label></TD>
										<TD>
											<asp:textbox id="txtNombreCli" runat="server" Width="208px" ToolTip="Subcadena" DESIGNTIMEDRAGDROP="2274"></asp:textbox></TD>
									</TR>
								</TABLE>
								<TABLE id="Table3" class="infoMacro" width="99%">
									<TR>
										<TD style="HEIGHT: 14px">Agente Local</TD>
									</TR>
									<TR>
										<TD>
											<ecctrl:eccombo id="cbxAgenteLocalID" runat="server" Width="368px" ShowLabel="False"></ecctrl:eccombo></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD vAlign="top" width="50%" align="center"></TD>
							<TD vAlign="top" width="50%" align="center"></TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:panel>
			<%--<P></P>--%>
			<!--------------------- PANEL RESULTADO -------------->
            
            <asp:panel id="pnlResultado" runat="server" Visible="True">
				<P class="subtitulo">Resultado de la Búsqueda</P>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="lblTituloGrid" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
							<%--<asp:button id="Button1" runat="server" CssClass="Button" Width="168px" Font-Bold="True" Text="Marcar/Desmarcar Todos" onclick="Button1_Click"></asp:button></TD>--%>
                            <div id="divBtn">
                                <asp:button id="Button1" runat="server" CssClass="Button" Width="168px" Font-Bold="True" Text="Marcar/Desmarcar Todos" OnClientClick="checkAll(); return false;"></asp:button>
                                <asp:button id="btnGenReporte" runat="server" CssClass="Button" Width="88px" Font-Bold="True" Text="Generar Doc." Style="display:none;" ClientIDMode="Static"></asp:button>
                                <asp:button id="btGenDoc" runat="server" CssClass="Button" Width="88px" Font-Bold="True" Text="Generar Doc." Visible="true" OnClientClick="checkSel(); return false;"></asp:button>
                                <asp:CheckBox id="chkUnaTablaPorMarca" runat="server" Text="Una tabla por marca" Checked="True" Visible="true"></asp:CheckBox>
                                <asp:Label ID="lblError" runat="server" Text="lblError" ForeColor="Red" Font-Bold="true" Visible="true" ClientIDMode="Static"></asp:Label>
                            </div>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="dgResult" runat="server" CssClass="tbl" Width="100%" AutoGenerateColumns="False"
								DataKeyField="ExpedienteID" HorizontalAlign="Center" onselectedindexchanged="dgResult_SelectedIndexChanged">
								<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Sel.">
										<ItemTemplate>
											<asp:CheckBox id="cbSel" runat="server" Width="100%" Visible="True"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Expediente">
										<ItemTemplate>
											<asp:Label id=ExpedienteID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ExpedienteID") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=Textbox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ExpedienteID") %>' NAME="Textbox1">
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Propias" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
                                            <asp:CheckBox id=Vigilada Checked='<%# DataBinder.Eval(Container.DataItem, "Vigilada")%>' Enabled="False" Runat="server">
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Denominacion" HeaderText="Denominacion">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ClaseNro" HeaderText="Clase">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="marcatipo" HeaderText="Tipo"></asp:BoundColumn>
									<asp:BoundColumn DataField="PropietarioNombre" HeaderText="Propietario">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn DataField="PrimerRegistro" HeaderText="Primer<br />Registro">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Acta" HeaderText="Acta">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PresentacionFecha" HeaderText="Fecha de Solicitud" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
									<asp:BoundColumn DataField="RegistroNro" HeaderText="Registro">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ConcesionFecha" HeaderText="Fecha de Concesion" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
									<asp:BoundColumn DataField="VencimientoFecha" HeaderText="Vencim." DataFormatString="{0:dd/MM/yy}">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TramiteAbrev" HeaderText="Tramite">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Marca Activa" >
										<ItemTemplate>
											<asp:CheckBox id=Checkbox2 Checked='<%# DataBinder.Eval(Container.DataItem, "Activa")%>' Enabled="False" Runat="server">
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ClienteNombre" HeaderText="Cliente/Agente">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn DataField="AtencionPorMarca" HeaderText="At. Marca">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SituacionDescrip" HeaderText="Situacion">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="str_public" HeaderText="Publicacion">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="bolinfo" HeaderText="Boletines"></asp:BoundColumn>
									<asp:BoundColumn DataField="OrdenTrabajo" HeaderText="H.I."></asp:BoundColumn>
									<asp:BoundColumn DataField="str_AgenteLocal" HeaderText="Agente Local"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				
                <TR>
                    <td>
				        <asp:button id="btnMarcarDes" runat="server" CssClass="Button" Width="168px" Font-Bold="True" 
                            Text="Marcar/Desmarcar Todos" OnClientClick="checkAll(); return false;"></asp:button>
                    </td>
                </TR>
                </TABLE>
			</asp:panel>
			<!--------------------- FIN Panel Resultado -------------->

		</form>
	</body>
</HTML>
