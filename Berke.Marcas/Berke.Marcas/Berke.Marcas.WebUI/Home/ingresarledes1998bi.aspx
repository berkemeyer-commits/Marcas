<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.IngresarLedes1998BI" CodeFile="IngresarLedes1998BI.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LedesIngresar</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Tools/css/ssDgPopUp.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssPopUp.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:panel id="PanelCabecera" runat="server" Width="756px" Height="152px">
				<P class="titulo">
					<asp:label id="lbIngresarFactura" runat="server">Ingresar Factura - Formato LEDES1998BI</asp:label><BR>
					<BR>
					<TABLE class="infoMacro" id="Table1" style="WIDTH: 100%" cellSpacing="1" cellPadding="1"
						width="608">
						<TR>
							<TD style="WIDTH: 9.62%" align="left">
								<P align="left">ID:</P>
							</TD>
							<TD style="WIDTH: 66px">
								<asp:TextBox id="tbID" runat="server" Width="64px" CssClass="inpform" ReadOnly="True" BackColor="#E0E0E0"></asp:TextBox></TD>
							<TD style="WIDTH: 14.11%" align="right">
								<P align="left">Fecha Creacion:</P>
							</TD>
							<TD style="WIDTH: 96px">
								<asp:TextBox id="tbFechaCreacion" runat="server" Width="96px" CssClass="inpform" ReadOnly="True"
									BackColor="#E0E0E0"></asp:TextBox></TD>
							<TD style="WIDTH: 7.95%" align="right">
								<P class="Etiqueta2">Estado:</P>
							</TD>
							<TD style="WIDTH: 139px">
								<asp:TextBox id="tbCodEstado" runat="server" Width="16px" CssClass="inpform" ReadOnly="True"
									BackColor="#E0E0E0"></asp:TextBox>
								<asp:TextBox id="tbDescripEstado" runat="server" Width="112px" CssClass="inpform" ReadOnly="True"
									BackColor="#E0E0E0"></asp:TextBox></TD>
							<TD class="etiqueta2" style="WIDTH: 89px">Usuario:</TD>
							<TD>
								<asp:TextBox id="tbUsuario" runat="server" Width="112px" CssClass="inpform" ReadOnly="True" BackColor="#E0E0E0"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE class="infoMacro" id="Table2" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 11.82%" align="right">
								<P align="left">Invoice date:</P>
							</TD>
							<TD>
								<asp:TextBox id="tbInvoiceDate" runat="server" Width="72px" CssClass="inpform"></asp:TextBox></TD>
							<TD style="WIDTH: 16.45%" align="right">
								<P class="Etiqueta2">Invoice number:</P>
							</TD>
							<TD style="WIDTH: 83px">
								<asp:TextBox id="tbInvoiceNumber" runat="server" Width="96px" CssClass="inpform"></asp:TextBox></TD>
							<TD style="WIDTH: 11.01%" align="right">
								<P class="Etiqueta2">Cliente Id:</P>
							</TD>
							<TD style="WIDTH: 20%">
								<asp:TextBox id="tbClienteId" runat="server" Width="80%" CssClass="inpform"></asp:TextBox></TD>
							<TD style="WIDTH: 17.47%" align="right">
								<P class="Etiqueta2">Client Matter&nbsp;Id:</P>
							</TD>
							<TD>
								<asp:TextBox id="tbClientMatterId" runat="server" Width="96px" CssClass="inpform"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE class="infoMacro" id="Table3" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 9.87%" align="right">
								<P align="left">Invoice total:</P>
							</TD>
							<TD style="WIDTH: 68px">
								<asp:TextBox id="tbInvoiceTotal" runat="server" Width="80px" CssClass="inpform"></asp:TextBox></TD>
							<TD style="WIDTH: 9.3%" align="right">
								<P class="Etiqueta2">Billing start date:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbBillingStartDate" runat="server" Width="176px" CssClass="inpform"></asp:TextBox></TD>
							<TD style="WIDTH: 20%" align="right">
								<P class="Etiqueta2">Billing end date:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbBillingEndDate" runat="server" Width="126px"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE class="infoMacro" id="Table4" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 10.87%" align="right">
								<P align="left">Descripción:</P>
							</TD>
							<TD align="right">
								<asp:TextBox id="tbDescription" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE class="infoMacro" id="Table5" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 9%" align="right">
								<P align="left">Time Keeper Id:</P>
							</TD>
							<TD style="WIDTH: 6%" align="right">
								<asp:TextBox id="tbTimeKeeperId" runat="server" Width="100%"></asp:TextBox></TD>
							<TD style="WIDTH: 8%" align="right">
								<P class="Etiqueta2">Time Keeper Name:</P>
							</TD>
							<TD style="WIDTH: 17.94%" align="right">
								<asp:TextBox id="tbTimeKeeperName" runat="server" Width="100%"></asp:TextBox></TD>
							<TD style="WIDTH: 8%" align="right">
								<P class="Etiqueta2">Time Keeper Classification:</P>
							</TD>
							<TD style="WIDTH: 20.44%" align="right">
								<asp:DropDownList id="ddlTimeKeeperClassification" runat="server" Height="16px" Width="100%">
									<asp:ListItem></asp:ListItem>
									<asp:ListItem Value="PT">Partner</asp:ListItem>
									<asp:ListItem Value="AS">Associate</asp:ListItem>
									<asp:ListItem Value="OC">Counsel</asp:ListItem>
									<asp:ListItem Value="LA">Legal assistant</asp:ListItem>
									<asp:ListItem Value="OT">Other Timekeeper</asp:ListItem>
								</asp:DropDownList></TD>
							<TD style="WIDTH: 6%" align="right">
								<P class="Etiqueta2">Law Firm Id:</P>
							</TD>
							<TD style="WIDTH: 6%" align="right">
								<asp:TextBox id="tbLawFirmId" runat="server" Width="100%"></asp:TextBox></TD>
							<TD style="WIDTH: 8%" align="right">
								<P class="Etiqueta2">Law Firm Matter Id:</P>
							</TD>
							<TD style="WIDTH: 10%" align="right">
								<asp:TextBox id="tbLawFirmMatterId" runat="server" Width="100%"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE class="infoMacro" id="Table66" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 10%" align="left">
								<P align="left">Po Number:</P>
							</TD>
							<TD style="WIDTH: 10%">
								<asp:TextBox id="tbPoNumber" runat="server" Width="90%"></asp:TextBox></TD>
							<TD style="WIDTH: 10%" align="right">
								<P class="Etiqueta2" align="left">Client Tax ID:</P>
							</TD>
							<TD style="WIDTH: 10%" align="left">
								<asp:TextBox id="tbClientTaxID" runat="server" Width="87px"></asp:TextBox></TD>
							<TD style="WIDTH: 10%" align="right">
								<P class="Etiqueta2">Matter Name:</P>
							</TD>
							<TD style="WIDTH: 50%" align="right">
								<asp:TextBox id="tbMatterName" runat="server" Width="378px"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE class="infoMacro" id="Table67" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 14.1%" align="right">
								<P align="left">Invoice tax total:</P>
							</TD>
							<TD style="WIDTH: 23.95%">
								<asp:TextBox id="tbInvoiceTaxTotal" runat="server" Width="90%"></asp:TextBox></TD>
							<TD style="WIDTH: 20%" align="right">
								<P class="Etiqueta2">Invoice net total:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbInvoiceNetTotal" runat="server" Width="176px"></asp:TextBox></TD>
							<TD style="WIDTH: 20%" align="right">
								<P class="Etiqueta2">Invoice currency:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbInvoiceCurrency" runat="server" Width="70px"></asp:TextBox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 14.1%" align="right">
								<P align="left">Timekeeper lastname:</P>
							</TD>
							<TD style="WIDTH: 23.95%">
								<asp:TextBox id="tbTimekeeperLastname" runat="server" Width="90%"></asp:TextBox></TD>
							<TD style="WIDTH: 20%" align="right">
								<P class="Etiqueta2">Timekeeper firstname:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbTimekeeperFirstname" runat="server" Width="176px"></asp:TextBox></TD>
							<TD style="WIDTH: 20%" align="right">
								<P class="Etiqueta2">Account Type:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:DropDownList id="ddlAccountType" runat="server" Height="16px" Width="100%">
									<asp:ListItem></asp:ListItem>
									<asp:ListItem Value="O">Own Account</asp:ListItem>
									<asp:ListItem Value="T">Third Party</asp:ListItem>
								</asp:DropDownList></TD>
						</TR>
					</TABLE>
					<TABLE class="infoMacro" id="Table68" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 8.16%" align="right">
								<P align="left">Law Firm Name:</P>
							</TD>
							<TD style="WIDTH: 16.59%">
								<asp:TextBox id="tbLawFirmName" runat="server" Width="120px"></asp:TextBox></TD>
							<TD style="WIDTH: 10%" align="right">
								<P class="Etiqueta2">Law Firm Address 1:</P>
							</TD>
							<TD style="WIDTH: 20.52%" align="right">
								<asp:TextBox id="tbLawFirmAddress1" runat="server" Width="187px"></asp:TextBox></TD>
							<TD style="WIDTH: 10%" align="right">
								<P class="Etiqueta2">Law Firm Address 2:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbLawFirmAddress2" runat="server" Width="203px"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE class="infoMacro" id="Table69" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 14.65%" align="right">
								<P align="left">Law Firm City:</P>
							</TD>
							<TD style="WIDTH: 22.09%">
								<asp:TextBox id="tbLawFirmCity" runat="server" Width="120px"></asp:TextBox></TD>
							<TD style="WIDTH: 17.38%" align="right">
								<P class="Etiqueta2">Law Firm State or Region:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbLawFirmStateorRegion" runat="server" Width="176px"></asp:TextBox></TD>
							<TD style="WIDTH: 20%" align="right">
								<P class="Etiqueta2">Law Firm Postcode:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbLawFirmPostcode" runat="server" Width="70px"></asp:TextBox></TD>
							<TD style="WIDTH: 20%" align="right">
								<P class="Etiqueta2">Law Firm Country:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbLawFirmCountry" runat="server" Width="70px"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE class="infoMacro" id="Table70" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 8.88%" align="right">
								<P align="left">Client Name:</P>
							</TD>
							<TD style="WIDTH: 16.59%">
								<asp:TextBox id="tbClientName" runat="server" Width="120px"></asp:TextBox></TD>
							<TD style="WIDTH: 10%" align="right">
								<P class="Etiqueta2">Client Address 1:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbClientAddress1" runat="server" Width="185px"></asp:TextBox></TD>
							<TD style="WIDTH: 10%" align="right">
								<P class="Etiqueta2">Client Address 2:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbClientAddress2" runat="server" Width="203px"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE class="infoMacro" id="Table71" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 14.65%" align="right">
								<P align="left">Client City:</P>
							</TD>
							<TD style="WIDTH: 26.72%">
								<asp:TextBox id="tbClientCity" runat="server" Width="120px"></asp:TextBox></TD>
							<TD style="WIDTH: 17.14%" align="right">
								<P class="Etiqueta2">Client State or Region:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbClientStateorRegion" runat="server" Width="176px"></asp:TextBox></TD>
							<TD style="WIDTH: 20%" align="right">
								<P class="Etiqueta2">Client Postcode:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbClientPostcode" runat="server" Width="70px"></asp:TextBox></TD>
							<TD style="WIDTH: 20%" align="right">
								<P class="Etiqueta2">Client Country:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbClientCountry" runat="server" Width="70px"></asp:TextBox></TD>
						</TR>
					</TABLE>
				</P>
			</asp:panel><asp:panel id="PanelDetalle" runat="server" Width="70%" Height="220px">
				<asp:button id="Button1" runat="server" Width="100%" Enabled="False" Visible="False" Text="Agregar Detalle"></asp:button>
				<BR>
				<TABLE cellSpacing="0" cellPadding="0" width="98%" align="center">
					<TR>
						<TD width="100%">
							<TABLE class="grid_head">
								<TR>
									<TD>
										<asp:label id="Label1" runat="server">Detalles de la Factura</asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="100%">
							<asp:datagrid id="GridLedesDetalle" runat="server" Height="20%" Width="1016px" CssClass="tbl"
								Font-Size="Smaller" PageSize="2" AutoGenerateColumns="False" onselectedindexchanged="GridLedesDetalle_SelectedIndexChanged">
								<FooterStyle HorizontalAlign="Left" CssClass="cell"></FooterStyle>
								<ItemStyle CssClass="EstiloItem"></ItemStyle>
								<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Sel.">
										<HeaderStyle Width="5mm"></HeaderStyle>
										<ItemTemplate>
											<asp:CheckBox id="cbSel" runat="server" Width="90%"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="ID">
										<HeaderStyle Width="5mm"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lbId" runat="server" Width="90%"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Item Number">
										<HeaderStyle Width="10mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbItemNumber" runat="server" Width="90%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Inv. Adj. Type">
										<HeaderStyle Width="20mm"></HeaderStyle>
										<ItemTemplate>
											<asp:DropDownList id="ddlInvAdjType" runat="server" Width="112px" Height="16px">
												<asp:ListItem></asp:ListItem>
												<asp:ListItem Value="E">Expense</asp:ListItem>
												<asp:ListItem Value="F">Fee</asp:ListItem>
												<asp:ListItem Value="IF">Invoice-Fees</asp:ListItem>
												<asp:ListItem Value="IE">Invoice-Expenses</asp:ListItem>
											</asp:DropDownList>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Number of Units">
										<HeaderStyle Width="20mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbNumberUnits" runat="server" Width="90%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Unit Cost">
										<HeaderStyle Width="25mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbUnitCost" runat="server" Width="80%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Adjustment amount">
										<HeaderStyle Width="25mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbAdjustmentAmount" runat="server" Width="80%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Item Tax Type">
										<HeaderStyle Width="20mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbItemTaxType" runat="server" Width="80%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Item Tax Rate">
										<HeaderStyle Width="25mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbItemTaxRate" runat="server" Width="80%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Item Tax Total">
										<HeaderStyle Width="25mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbItemTaxTotal" runat="server" Width="80%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Item Total">
										<HeaderStyle Width="25mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbTotal" runat="server" Width="80%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Item Date">
										<HeaderStyle Width="30mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbDate" runat="server" Width="100%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Task Code">
										<HeaderStyle Width="15mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbTaskCode" runat="server" Width="80%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Expense Code">
										<HeaderStyle Width="15mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbExpenseCode" runat="server" Width="80%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Activity Code">
										<HeaderStyle Width="15mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbActivityCode" runat="server" Width="80%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Description">
										<HeaderStyle Width="40mm"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id="tbDescripcion" runat="server" Width="90%"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
				<P>
					<TABLE id="Table6" style="WIDTH: 755px; HEIGHT: 32px" cellSpacing="1" cellPadding="1" width="755"
						align="center">
						<TR>
							<TD style="WIDTH: 38px"></TD>
							<TD style="WIDTH: 112px">
								<asp:button id="btnEliminarDetalle" runat="server" Width="111px" CssClass="Button" Text="Eliminar Detalle"
									Font-Bold="True" onclick="btnEliminarDetalle_Click"></asp:button></TD>
							<TD style="WIDTH: 41px"></TD>
							<TD style="WIDTH: 85px">
								<asp:button id="btnNuevo" runat="server" Width="80px" CssClass="Button" Text="Nuevo" Font-Bold="True" onclick="btnNuevo_Click"></asp:button></TD>
							<TD style="WIDTH: 41px"></TD>
							<TD style="WIDTH: 85px">
								<asp:button id="btnGrabar" runat="server" Width="80px" CssClass="Button" Text="Grabar" Font-Bold="True" onclick="btnGrabar_Click"></asp:button></TD>
							<TD style="WIDTH: 48px"></TD>
							<TD style="WIDTH: 81px">
								<asp:button id="btnConfirmar" runat="server" Width="80px" CssClass="Button" Text="Confirmar"
									Font-Bold="True" onclick="btnConfirmar_Click"></asp:button></TD>
							<TD style="WIDTH: 46px"></TD>
							<TD style="WIDTH: 82px">
								<asp:button id="btnAnular" runat="server" Width="80px" CssClass="Button" Text="Anular" Font-Bold="True" onclick="btnAnular_Click"></asp:button></TD>
							<TD style="WIDTH: 36px"></TD>
							<TD style="WIDTH: 108px">
								<asp:button id="btnGenerarArchivo" runat="server" Width="109px" CssClass="Button" Text="Generar Archivo"
									Font-Bold="True" onclick="btnGenerarArchivo_Click"></asp:button></TD>
							<TD></TD>
						</TR>
					</TABLE>
			</asp:panel></P></form>
	</body>
</HTML>
