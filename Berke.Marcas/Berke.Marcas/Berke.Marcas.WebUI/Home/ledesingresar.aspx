<%@ Register TagPrefix="uc1" TagName="Header" Src="../Tools/Controls/Header.ascx" %>
<%@ Page language="c#" Inherits="Berke.Marcas.WebUI.Home.LedesIngresar" CodeFile="LedesIngresar.aspx.cs" %>
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
			<uc1:header id="Header1" runat="server"></uc1:header>
			<asp:panel id="PanelCabecera" runat="server" Width="756px" Height="152px">
				<P class="titulo">
					<asp:label id="lbIngresarFactura" runat="server">Ingresar Factura - Formato LEDES</asp:label><BR>
					<BR>
					<TABLE id="Table1" style="WIDTH: 100%" cellSpacing="1" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 11.54%" align="right">
								<P class="Etiqueta2">ID:</P>
							</TD>
							<TD style="WIDTH: 66px">
								<asp:TextBox id="tbID" runat="server" Width="64px" BackColor="#E0E0E0" ReadOnly="True"></asp:TextBox></TD>
							<TD style="WIDTH: 14.11%" align="right">
								<P class="Etiqueta2">Fecha Creacion:</P>
							</TD>
							<TD style="WIDTH: 96px">
								<asp:TextBox id="tbFechaCreacion" runat="server" Width="96px" BackColor="#E0E0E0" ReadOnly="True"></asp:TextBox></TD>
							<TD style="WIDTH: 7.95%" align="right">
								<P class="Etiqueta2">Estado:</P>
							</TD>
							<TD style="WIDTH: 139px">
								<asp:TextBox id="tbCodEstado" runat="server" Width="16px" BackColor="#E0E0E0" ReadOnly="True"></asp:TextBox>
								<asp:TextBox id="tbDescripEstado" runat="server" Width="112px" BackColor="#E0E0E0" ReadOnly="True"></asp:TextBox></TD>
							<TD class="etiqueta2" style="WIDTH: 89px">Usuario:</TD>
							<TD>
								<asp:TextBox id="tbUsuario" runat="server" Width="112px" BackColor="#E0E0E0" ReadOnly="True"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE id="Table2" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 13.76%" align="right">
								<P class="Etiqueta2">Invoice date:</P>
							</TD>
							<TD>
								<asp:TextBox id="tbInvoiceDate" runat="server" Width="72px"></asp:TextBox></TD>
							<TD style="WIDTH: 16.45%" align="right">
								<P class="Etiqueta2">Invoice number:</P>
							</TD>
							<TD style="WIDTH: 83px">
								<asp:TextBox id="tbInvoiceNumber" runat="server" Width="96px"></asp:TextBox></TD>
							<TD style="WIDTH: 11.01%" align="right">
								<P class="Etiqueta2">Cliente Id:</P>
							</TD>
							<TD style="WIDTH: 20%">
								<asp:TextBox id="tbClienteId" runat="server" Width="80%"></asp:TextBox></TD>
							<TD style="WIDTH: 17.47%" align="right">
								<P class="Etiqueta2">Client Matter&nbsp;Id:</P>
							</TD>
							<TD>
								<asp:TextBox id="tbClientMatterId" runat="server" Width="96px"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE id="Table3" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 14.65%" align="right">
								<P class="Etiqueta2">Invoice total:</P>
							</TD>
							<TD style="WIDTH: 25%">
								<asp:TextBox id="tbInvoiceTotal" runat="server" Width="90%"></asp:TextBox></TD>
							<TD style="WIDTH: 20%" align="right">
								<P class="Etiqueta2">Billing start date:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbBillingStartDate" runat="server" Width="176px"></asp:TextBox></TD>
							<TD style="WIDTH: 20%" align="right">
								<P class="Etiqueta2">Billing end date:</P>
							</TD>
							<TD style="WIDTH: 25%" align="right">
								<asp:TextBox id="tbBillingEndDate" runat="server" Width="126px"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE id="Table4" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 10.75%" align="right">
								<P class="Etiqueta2">Descripcion:</P>
							</TD>
							<TD align="right">
								<asp:TextBox id="tbDescription" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox></TD>
						</TR>
					</TABLE>
					<TABLE id="Table5" style="WIDTH: 100%" cellPadding="1" width="608">
						<TR>
							<TD style="WIDTH: 9%" align="right">
								<P class="Etiqueta2">Time Keeper Id:</P>
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
							<TD style="WIDTH: 15%" align="right">
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
				</P>
			</asp:panel>
			<asp:panel id="PanelDetalle" runat="server" Width="70%" Height="220px">
				<P>
					<TABLE id="Table7" style="WIDTH: 760px; HEIGHT: 32px" cellSpacing="1" cellPadding="1" width="760">
						<TR>
							<TD style="FONT-WEIGHT: bold; WIDTH: 85%">Detalles de la Factura</TD>
							<TD>
								<asp:button id="btnAgregarDetalle" runat="server" Width="100%" Text="Agregar Detalle" Visible="False"
									Enabled="False"></asp:button></TD>
							<TD>
								<asp:button id="btnEliminarDetalle" runat="server" Width="100%" Text="Eliminar Detalle" Font-Bold="True"
									CssClass="Button" onclick="btnEliminarDetalle_Click"></asp:button></TD>
						</TR>
					</TABLE>
				</P>
				<P>
					<asp:datagrid id="GridLedesDetalle" runat="server" Height="20%" Width="100%" CssClass="tbl" AutoGenerateColumns="False"
						PageSize="2" Font-Size="Smaller">
						<FooterStyle HorizontalAlign="Left" CssClass="cell"></FooterStyle>
						<ItemStyle CssClass="EstiloItem"></ItemStyle>
						<HeaderStyle HorizontalAlign="Center" CssClass="cell_header"></HeaderStyle>
						<Columns>
							<asp:TemplateColumn HeaderText="Sel.">
								<ItemTemplate>
									<asp:CheckBox id="cbSel" runat="server" Width="90%"></asp:CheckBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="ID">
								<ItemTemplate>
									<asp:Label id="lbId" runat="server" Width="90%"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Item Number">
								<ItemTemplate>
									<asp:TextBox id="tbItemNumber" runat="server" Width="90%"></asp:TextBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Inv. Adj. Type">
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
								<ItemTemplate>
									<asp:TextBox id="tbNumberUnits" runat="server" Width="90%"></asp:TextBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Unit Cost">
								<ItemTemplate>
									<asp:TextBox id="tbUnitCost" runat="server" Width="80%"></asp:TextBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Adjustment amount">
								<ItemTemplate>
									<asp:TextBox id="tbAdjustmentAmount" runat="server" Width="80%"></asp:TextBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Item Total">
								<ItemTemplate>
									<asp:TextBox id="tbTotal" runat="server" Width="80%"></asp:TextBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Item Date">
								<ItemTemplate>
									<asp:TextBox id="tbDate" runat="server" Width="100%"></asp:TextBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Task Code">
								<ItemTemplate>
									<asp:TextBox id="tbTaskCode" runat="server" Width="80%"></asp:TextBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Expense Code">
								<ItemTemplate>
									<asp:TextBox id="tbExpenseCode" runat="server" Width="80%"></asp:TextBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Activity Code">
								<ItemTemplate>
									<asp:TextBox id="tbActivityCode" runat="server" Width="80%"></asp:TextBox>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Description">
								<ItemTemplate>
									<asp:TextBox id="tbDescripcion" runat="server" Width="90%"></asp:TextBox>
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:datagrid></P>
				<P>
					<TABLE id="Table6" style="WIDTH: 755px; HEIGHT: 32px" cellSpacing="1" cellPadding="1" width="755"
						align="center">
						<TR>
							<TD style="WIDTH: 38px"></TD>
							<TD style="WIDTH: 85px">
								<asp:button id="btnNuevo" runat="server" Width="80px" Text="Nuevo" Font-Bold="True" CssClass="Button" onclick="btnNuevo_Click"></asp:button></TD>
							<TD style="WIDTH: 41px"></TD>
							<TD style="WIDTH: 85px">
								<asp:button id="btnGrabar" runat="server" Width="80px" Text="Grabar" Font-Bold="True" CssClass="Button" onclick="btnGrabar_Click"></asp:button></TD>
							<TD style="WIDTH: 48px"></TD>
							<TD style="WIDTH: 81px">
								<asp:button id="btnConfirmar" runat="server" Width="80px" Text="Confirmar" Font-Bold="True"
									CssClass="Button" onclick="btnConfirmar_Click"></asp:button></TD>
							<TD style="WIDTH: 46px"></TD>
							<TD style="WIDTH: 82px">
								<asp:button id="btnAnular" runat="server" Width="80px" Text="Anular" Font-Bold="True" CssClass="Button" onclick="btnAnular_Click"></asp:button></TD>
							<TD style="WIDTH: 36px"></TD>
							<TD style="WIDTH: 108px">
								<asp:button id="btnGenerarArchivo" runat="server" Width="109px" Text="Generar Archivo" Font-Bold="True"
									CssClass="Button" onclick="btnGenerarArchivo_Click"></asp:button></TD>
							<TD></TD>
						</TR>
					</TABLE>
			</asp:panel></P>
		</form>
	</body>
</HTML>
