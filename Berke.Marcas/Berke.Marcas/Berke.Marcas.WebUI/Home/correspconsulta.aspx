<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<%@ Register TagPrefix="ecctrl" Namespace="ecWebControls" Assembly="ecWebControls" %>
<%@ Page language="c#"  smartnavigation="True" Inherits="Berke.Marcas.WebUI.Home.CorrespConsulta" CodeFile="CorrespConsulta.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consultar Correspondencia</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/globalstyle.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssMainStyle.css">
		<LINK rel="stylesheet" type="text/css" href="../Tools/css/ssDgEstiloGeneral.css">
        <!--<script language="JavaScript">-->
		<script type="text/javascript"> 
		    function restablecer(pCod){
              /* alert('ID a transferir ' + pCod ); */
              window.opener.document.Form1.CorrespID.value = pCod ;
              window.opener.focus();
              window.close();
            }

            function checkedChanged(clickedChkID, chkToChangeId) {
                var res = clickedChkID.split("_");

                var clickedChkID = "";
                for (var i = 0; i < res.length - 1; i++) {
                    clickedChkID = clickedChkID + res[i] + "_";
                }
                
                var x = document.getElementById("ddlAsignacion");
                
                if (x === null) {
                    if (res[res.length - 1] != "chkSel") {
                        document.getElementById(clickedChkID + "chkSel").checked = document.getElementById(clickedChkID + "chkAcu").checked || document.getElementById(clickedChkID + "chkProc").checked;
                    }
                    else {
                        if (document.getElementById(clickedChkID + "chkSel").checked) {
                            document.getElementById(clickedChkID + "chkProc").checked = true;
                        }
                        else {
                            document.getElementById(clickedChkID + "chkAcu").checked = false;
                            document.getElementById(clickedChkID + "chkProc").checked = false;
                        }

                    }
                }
                else {
                    if (res[res.length - 1] != "chkSel") {
                        document.getElementById(clickedChkID + "chkSel").checked = document.getElementById(clickedChkID + "chkFact").checked;
                    }
                }
            }

            function selectAll() {
                var grid = document.getElementById("dgAsignacion");
                var chk = document.getElementById("chkMarcar").checked;

                var x = document.getElementById("ddlAsignacion");
                
                for (var i = 0, row; row = grid.rows[i]; i++) {
                    //iterate through rows
                    //rows would be accessed using the "row" variable assigned in the for loop
                    for (var j = 0, col; col = row.cells[j]; j++) {
                        //iterate through columns
                        //columns would be accessed using the "col" variable assigned in the for loop
                        if (x === null) {
                            if (col.innerHTML.indexOf("type=\"checkbox\"") > -1) {
                                var id = /id="(.+)" type/.exec(col.innerHTML)[1];

                                //if ((id.indexOf("_chkSel") > -1) || (id.indexOf("_chkProc") > -1)) {
                                if (id.indexOf("_chkSel") > -1) {
                                    document.getElementById(id).checked = chk;
                                }
                            }
                        }
                        else {
                            if (col.innerHTML.indexOf("type=\"checkbox\"") > -1) {
                                var id = /id="(.+)" type/.exec(col.innerHTML);

                                if ((id != null) && (id[1].indexOf("_chkSel") > -1)) {
                                    document.getElementById(id[1]).checked = chk;
                                }
                                //var id = /id="(.+)" on/.exec(col.innerHTML)[1];

                                //if ((id.indexOf("_chkSel") > -1) || (id.indexOf("_chkFact") > -1)) {
                                //    document.getElementById(id).checked = chk;
                                //}
                            }
                        }
                    }  
                }
            }

            function checkAll() {
                var grid = document.getElementById("dgAsignacion");
                var chk = document.getElementById("chkMarcar").checked;

                var inputs = grid.getElementsByTagName("input");

                for (var i = 0; i < inputs.length; i++) {
                    if ((inputs[i].type === "checkbox") && (inputs[i].id.indexOf("_chkSel") > -1)) {
                        document.getElementById(inputs[i].id).checked = chk;
                    }
                }

                


                //var grid = document.getElementById("dgAsignacion");
                //var chk = document.getElementById("chkMarcar").checked;

                ////var x = document.getElementById("ddlAsignacion");

                //var checkboxes = $(grid).find("td input:checkbox");

                //for (var i = 0; i < checkboxes.length; i++) {
                //    if (checkboxes[i].id.indexOf("_chkSel") > -1) {
                //        $(checkboxes[i]).prop('checked', chk);
                //    }
                //}
            }
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<!-- 

   Cabecera 
		
   /--><uc1:header id="Header1" runat="server"></uc1:header>
			<P></P>
			<P class="titulo">&nbsp;Consultar Correspondencia</P>
			<!-- 

   Fin Cabecera

   /--><asp:panel id="pnlBuscar" runat="server">
				<P class="subtitulo">Criterio de Búsqueda</P>
				<P></P>
				<TABLE id="tblBuscar" class="infoMacro">
					<TR>
						<TD style="WIDTH: 356px" width="356">
							<asp:Label id="lblID_min" runat="server" Width="90px" CssClass="Etiqueta2">
          ID
        </asp:Label>
							<asp:textbox id="txtID_min" runat="server" Width="60px"></asp:textbox>
							<asp:Label id="lblID_max" runat="server" Width="15px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtID_max" runat="server" Width="60px"></asp:textbox></TD>
						<TD>
							<asp:Label id="lblPrioridadID" runat="server" Width="70px" CssClass="Etiqueta2">Prioridad</asp:Label>
							<CUSTOM:DROPDOWN id="ddlPrioridadID" runat="server" Width="104px" AutoPostBack="False"></CUSTOM:DROPDOWN></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 356px; HEIGHT: 15px" width="356">
							<asp:Label id="lblNro_min" runat="server" Width="90px" CssClass="Etiqueta2">
          Nro
        </asp:Label>
							<asp:textbox id="txtNro_min" runat="server" Width="60px"></asp:textbox>
							<asp:Label id="lblNro_max" runat="server" Width="15px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtNro_max" runat="server" Width="60px"></asp:textbox>
							<asp:Label id="lblAnio" runat="server" Width="24px" CssClass="Etiqueta2">Año</asp:Label>
							<asp:textbox id="txtAnio" runat="server" Width="60px"></asp:textbox></TD>
						<TD style="HEIGHT: 15px">
                            <asp:Label ID="Label10" runat="server" CssClass="Etiqueta2" Width="70px">Area Distrib.</asp:Label>
                            <Custom:DropDown ID="ddlAreaDistrib" runat="server" AutoPostBack="True" onselectedindexchanged="ddlArea_SelectedIndexChanged" Width="256px"></Custom:DropDown>
                        </TD>
					<TR>
						<TD style="WIDTH: 356px; HEIGHT: 16px" width="356">
							<asp:Label id="lblFechaAlta_min" runat="server" Width="90px" CssClass="Etiqueta2">
          FechaAlta
        </asp:Label>
							<asp:textbox id="txtFechaAlta_min" runat="server" Width="88px"></asp:textbox>
							<asp:Label id="lblFechaAlta_max" runat="server" Width="15px" CssClass="Etiqueta2">
           - 
        </asp:Label>
							<asp:textbox id="txtFechaAlta_max" runat="server" Width="88px"></asp:textbox></TD>
						<TD style="HEIGHT: 16px"></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 356px" width="356">
							<asp:Label id="lblNombre" runat="server" Width="90px" CssClass="Etiqueta2">Cliente  ID </asp:Label>
							<asp:textbox id="txtClienteID" runat="server" Width="72px"></asp:textbox>
							<asp:Label id="Label1" runat="server" Width="166px" CssClass="Etiqueta2">Nombre       </asp:Label></TD>
						<TD>&nbsp;
							<asp:textbox id="txtNombre" runat="server" Width="210px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 356px" width="356">
							<asp:Label id="lblRefCorresp" runat="server" Width="90px" CssClass="Etiqueta2">
          RefCorresp
        </asp:Label>
							<asp:textbox id="txtRefCorresp" runat="server" Width="210px"></asp:textbox></TD>
						<TD>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD colSpan="2">
							<asp:Label id="lblTrabajoTipo" runat="server" Width="90px" CssClass="Etiqueta2">Indicación  </asp:Label>
							<CUSTOM:DROPDOWN id="ddlTrabajoTipo" runat="server" Width="368px"></CUSTOM:DROPDOWN></TD>
						</TD></TR>
					<TR>
						<TD style="HEIGHT: 1px">
							<asp:Label id="Label5" runat="server" Width="90px" CssClass="Etiqueta2">Area</asp:Label>
							<CUSTOM:DROPDOWN id="ddlArea" runat="server" Width="256px" AutoPostBack="True" onselectedindexchanged="ddlArea_SelectedIndexChanged"></CUSTOM:DROPDOWN></TD>
						<TD style="HEIGHT: 1px" width="356">
							<asp:Label id="Label3" runat="server" Width="90px" CssClass="Etiqueta2">
          Asignado a
        </asp:Label>
							<CUSTOM:DROPDOWN id="ddlFuncionarioFiltro" runat="server" Width="256px"></CUSTOM:DROPDOWN></TD>
					</TR>
					<TR>
						<TD>
							<TABLE border="0">
								<TR>
									<TD>
										<asp:Label id="Label4" runat="server" Width="90px" CssClass="Etiqueta2">Procesado</asp:Label></TD>
									<TD>
										<asp:RadioButtonList id="rbProcesado" runat="server" Width="128px" BorderWidth="0px" BorderStyle="Solid"
											RepeatDirection="Horizontal">
											<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
											<asp:ListItem Value="Si">Si</asp:ListItem>
											<asp:ListItem Value="No">No</asp:ListItem>
										</asp:RadioButtonList></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<TABLE border="0">
								<TR>
									<TD>
										<asp:Label id="Label8" runat="server" Width="90px" CssClass="Etiqueta2">Acusado</asp:Label></TD>
									<TD>
										<asp:RadioButtonList id="rbAcusado" runat="server" Width="128px" BorderWidth="0px" BorderStyle="Solid"
											RepeatDirection="Horizontal">
											<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
											<asp:ListItem Value="Si">Si</asp:ListItem>
											<asp:ListItem Value="No">No</asp:ListItem>
										</asp:RadioButtonList></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<TABLE border="0">
								<TR>
									<TD>
										<asp:Label id="Label9" runat="server" Width="90px" CssClass="Etiqueta2">Facturable</asp:Label></TD>
									<TD>
										<asp:RadioButtonList id="rbFacturable" runat="server" Width="128px" BorderWidth="0px" BorderStyle="Solid"
											RepeatDirection="Horizontal">
											<asp:ListItem Value=" " Selected="True">Omitir</asp:ListItem>
											<asp:ListItem Value="Si">Si</asp:ListItem>
											<asp:ListItem Value="No">No</asp:ListItem>
										</asp:RadioButtonList></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<P></P>
				<TABLE border="0" cellSpacing="0" cellPadding="0" width="100%"> <!--     
      region:  Boton Buscar    				  
      /-->
					<TR>
						<TD style="WIDTH: 122px; HEIGHT: 19px">
							<asp:button id="btBuscar" runat="server" CssClass="Button" Font-Bold="True" Text="Buscar" onclick="btBuscar_Click"></asp:button></TD>
						<TD style="WIDTH: 206px; HEIGHT: 19px">
							<P align="left">&nbsp;</P>
						</TD>
					</TR> <!--  
       endregion: Boton Buscar 
      /--> <!--							
      region:  Mensaje /
      -->
					<TR>
						<TD colSpan="2">
							<asp:label id="lblMensaje" runat="server" Font-Bold="True" Font-Size="X-Small"></asp:label>&nbsp;
							<asp:label id="Label6" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					</TR> <!-- 			
      endregion: Mensaje  		
      /--></TABLE>
			</asp:panel>
			<P></P>
			<!---
------------------ PANEL RESULTADO ------------
--><asp:panel id="pnlResultado" runat="server" Visible="True">
				<P class="subtitulo">Resultado de la 
					Búsqueda&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</P>
				<P class="subtitulo"></P>
				<asp:button id="btnGenDoc" runat="server" Width="48px" CssClass="btn_doc" Font-Bold="True" Text="doc" onclick="btnGenDoc_Click"></asp:button>
				<asp:button id="btnGenXls" runat="server" Width="48px" CssClass="btn_xls" Font-Bold="True" Text="xls" onclick="btnGenXls_Click"></asp:button>
				<BR>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="lblTituloGrid" runat="server"></asp:label></TD>
									<TD align="right"></TD>
									<TD align="right">
										<asp:button id="btnAsignar" runat="server" CssClass="Button" Font-Bold="True" Text="Ir a asignaciones de correspondencia" onclick="btnAsignar_Click"></asp:button>
										<asp:button id="btnProcesado" runat="server" CssClass="Button" Font-Bold="True" Text="Marcar como procesado"
											Visible="True" onclick="btnProcesado_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="dgResult" runat="server" Width="100%" CssClass="tbl" AutoGenerateColumns="False"
								HorizontalAlign="Center" DataKeyField="ID">
								<ItemStyle CssClass="cell"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="Nro" HeaderText="Nro">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Anio" HeaderText="A&#241;o">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FechaAlta" HeaderText="Fecha Alta" DataFormatString="{0:d}">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RefCorresp" HeaderText="Ref.Corresp">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ClienteID" HeaderText="Cliente ID">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Nombre" HeaderText="Nombre">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="movObs" HeaderText="Obs">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Asignado" HeaderText="Asignado a"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="Estado" HeaderText="Estado"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Facturable">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id=chkFacturable Checked='<%# GenerateBindString(DataBinder.Eval(Container.DataItem, "Facturable"))%>' Enabled="False" Runat="server">
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Acusado">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id=chkAcusado Checked='<%# GenerateBindString(DataBinder.Eval(Container.DataItem, "Acusado"))%>' Enabled="False" Runat="server">
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Procesado">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id=chkProcesado Checked='<%# GenerateBindString(DataBinder.Eval(Container.DataItem, "Estado"))%>' Enabled="False" Runat="server">
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD>
							<P align="left">&nbsp;</P>
						</TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!---
------------------ FIN Panel Resultado ------------

--><asp:panel id="pnlAsignacion" runat="server" Visible="False">
				<P class="subtitulo">Asignar correspondencias</P>
				<P>
					<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="98%" align="center">
						<TR>
							<TD width="100%">
								<TABLE id="Table2" class="grid_head">
									<TR>
										<TD>
											<asp:label id="Label2" runat="server">Listado de correspondencia</asp:label></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD width="100%">
								<TABLE class="grid_head" cellSpacing="0" cellPadding="0" width="98%" align="center">
									<TR>
										<TD><FONT size="2"><STRONG><FONT class="EstiloItem">&nbsp;
														<asp:CheckBox id="chkMarcar" runat="server" Width="128px" Text="Marcar Todo"
															Checked="False" OnClick="checkAll()"></asp:CheckBox></FONT>&nbsp;&nbsp;&nbsp;
													<asp:label id="lblAreaAsignacion" runat="server">Seleccionar Area:</asp:label>&nbsp;&nbsp;
													<CUSTOM:DROPDOWN id="ddlAreaAsignacion" runat="server" Width="256px" AutoPostBack="True" onselectedindexchanged="ddlAreaAsignacion_SelectedIndexChanged"></CUSTOM:DROPDOWN>
													<asp:label id="lblAsignar" runat="server">Asignar a:</asp:label>
													<CUSTOM:DROPDOWN id="ddlAsignacion" runat="server" Width="368px" Visible="True"></CUSTOM:DROPDOWN>
													<asp:button id="btnAsignacion" runat="server" CssClass="Button" Font-Bold="True" Text="Asignar"
														Visible="True" onclick="btnAsignacion_Click"></asp:button>
													<asp:button id="btnGrabar" runat="server" CssClass="Button" Font-Bold="True" Text="Grabar" Visible="True" onclick="btnGrabar_Click"></asp:button>
													<asp:button id="btnGrabarProcesado" runat="server" CssClass="Button" Font-Bold="True" Text="Grabar"
														Visible="True" onclick="btnGrabarProcesado_Click"></asp:button></STRONG></FONT></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD width="98%">
								<asp:datagrid id="dgAsignacion" runat="server" Width="100%" CssClass="Grilla" ForeColor="Black"
									Visible="True" AutoGenerateColumns="False" HorizontalAlign="Center" DataKeyField="ID">
									<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
									<ItemStyle CssClass="cell"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
									<Columns>
										<asp:TemplateColumn HeaderText="Sel.">
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<%--<asp:CheckBox id="chkSel" runat="server" Checked="False" OnClick='checkedChanged(this.id, "")'></asp:CheckBox>--%>
                                                <asp:CheckBox id="chkSel" runat="server" Checked="False"></asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="ID" HeaderText="ID"></asp:BoundColumn>
										<asp:BoundColumn DataField="Nro" HeaderText="Nro">
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Anio" HeaderText="A&#241;o">
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="FechaAlta" HeaderText="Fecha Alta" DataFormatString="{0:d}">
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="RefCorresp" HeaderText="Ref.Corresp">
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="ClienteID" HeaderText="Cliente ID">
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Nombre" HeaderText="Nombre">
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="movObs" HeaderText="Obs">
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Asignado" HeaderText="Asignado a"></asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Asignar a">
											<ItemTemplate>
												<asp:textbox id="txtAsignado" runat="server" Width="100%" Font-Bold="True" ReadOnly="True"></asp:textbox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Facturable">
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<%--<asp:CheckBox id="chkFact" runat="server" OnClick='checkedChanged(this.id, "chkSel")' Checked='<%# GenerateBindString(DataBinder.Eval(Container.DataItem, "Facturable"))%>'>--%>
                                                <asp:CheckBox id="chkFact" runat="server" Checked='<%# GenerateBindString(DataBinder.Eval(Container.DataItem, "Facturable"))%>'>
												</asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Acusado">
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:CheckBox id="chkAcu" runat="server" Checked='<%# GenerateBindString(DataBinder.Eval(Container.DataItem, "Acusado"))%>'>
												</asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Procesado">
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:CheckBox id="chkProc" runat="server" Checked='<%# GenerateBindString(DataBinder.Eval(Container.DataItem, "Estado"))%>'>
												</asp:CheckBox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn Visible="False" HeaderText="IdFuncionario">
											<ItemTemplate>
												<asp:textbox id="txtIdFuncionario" runat="server" Width="100%" Font-Bold="True" Visible="False"
													ReadOnly="True"></asp:textbox>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn Visible="False" HeaderText="Mensaje de error">
											<ItemStyle Font-Bold="True" ForeColor="Red"></ItemStyle>
										</asp:BoundColumn>
									</Columns>
								</asp:datagrid></TD>
						</TR>
					</TABLE>
				</P>
			</asp:panel></form>
	</body>
</HTML>
