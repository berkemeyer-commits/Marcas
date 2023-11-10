<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.LedesConsulta" CodeFile="LedesConsulta.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LedesConsulta</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" onkeypress="form_onEnter('btnBuscar');">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<asp:panel id="PanelBuscar" 
				runat="server" Height="190px" Width="664px">
			<P class="titulo">Consulta de Facturas formato LEDES</P>
				<P class="subtitulo">Criterio de Búsqueda</P>
				
						<P>
							<TABLE id="Table1" class="infoMacro" width="98%">
								<TR>
									<TD style="WIDTH: 325px">&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Ledes 
										ID&nbsp;&nbsp;
										<asp:textbox id="tbLedesId" runat="server" Width="152px"></asp:textbox></TD>
									<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										&nbsp;Estado&nbsp;
										<asp:DropDownList id="ddlEstado" runat="server" Width="168px" Height="21px">
											<asp:ListItem Selected="True"></asp:ListItem>
											<asp:ListItem Value="A">Abierto</asp:ListItem>
											<asp:ListItem Value="C">Confirmado</asp:ListItem>
											<asp:ListItem Value="U">Anulado</asp:ListItem>
										</asp:DropDownList></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 325px">&nbsp;&nbsp; &nbsp;Invoice date &nbsp;
										<asp:textbox id="tbInvoiceDate" runat="server" Width="152px"></asp:textbox></TD>
									<TD>&nbsp;&nbsp;&nbsp; Invoice number&nbsp;
										<asp:textbox id="tbInvoiceNumber" runat="server" Width="168px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 325px">&nbsp;&nbsp;&nbsp;&nbsp;Invoice total&nbsp;&nbsp;
										<asp:textbox id="tbInvoiceTotal" runat="server" Width="152px"></asp:textbox></TD>
									<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										Client Id&nbsp;
										<asp:textbox id="tbClienteId" runat="server" Width="168px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 325px">Time Keeper Id&nbsp;&nbsp;
										<asp:textbox id="tbTimeKeeperId" runat="server" Width="152px"></asp:textbox></TD>
									<TD>Time Keeper Name
										<asp:textbox id="tbTimeKeeperName" runat="server" Width="168px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 325px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; 
										Usuario&nbsp;&nbsp;
										<asp:textbox id="tbUsuario" runat="server" Width="152px"></asp:textbox></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</P>
						<P>	<asp:Button id="btnBuscar" runat="server" Width="72px" Height="19px" Text="Buscar" CssClass="Button"
								Font-Bold="True" onclick="btnBuscar_Click"></asp:Button></P>
						<P>
						</P>
					
			</asp:panel><asp:panel id="PanelResultado" 
				runat="server" Height="192px" Width="98%">
				<P class="subtitulo">Resultado de la Búsqueda</P>
				<P>
					<TABLE style="margin-left:5px" id="Table2" cellSpacing="1" cellPadding="1" width="100%"
						>
						<TR>
							<TD>
								<asp:DataGrid id="dgResult" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="False"
									CssClass="tbl">
									<ItemStyle CssClass="cell"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
									<FooterStyle HorizontalAlign="Left" CssClass="EstiloFooter"></FooterStyle>
									<Columns>
										<asp:BoundColumn DataField="invoice_description" HeaderText="ID">
											<HeaderStyle Width="5%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" Width="5%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="invoice_number" HeaderText="Invoice number">
											<HeaderStyle Width="10%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Estado" HeaderText="Estado">
											<HeaderStyle Width="5%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" Width="5%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="invoice_date" HeaderText="Invoice date" DataFormatString="{0:d}">
											<HeaderStyle Width="15%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="client_id" HeaderText="Client Id">
											<HeaderStyle Width="12%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="invoice_total" HeaderText="Invoice Total" DataFormatString="{0:N}">
											<HeaderStyle Width="18%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right" Width="18%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="timekeeper_id" HeaderText="TimeKeeper Id">
											<HeaderStyle Width="12%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="timekeeper_name" HeaderText="TimeKeeper Name">
											<HeaderStyle Width="30%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="usuario" HeaderText="Usuario"></asp:BoundColumn>
									</Columns>
								</asp:DataGrid></TD>
						</TR>
					</TABLE>
				</P>
			</asp:panel></form>
	</body>
</HTML>
