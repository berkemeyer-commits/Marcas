<%@ Page language="c#" validateRequest="false" Inherits="Berke.Marcas.WebUI.Limitada" CodeFile="Limitada.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Limitada</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Tools/css/globalstyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssMainStyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Tools/css/ssDgEstiloGeneral.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="98%" border="0">
				<TR>
					<TD>
						<P>&nbsp;</P>
						<P>
							<asp:Label id="lblTitIdioma" Runat="server" Font-Names="Verdana" Font-Size="Medium" Font-Italic="True"></asp:Label></P>
					</TD>
				</TR>
				<TR>
					<TD width="100%">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<TR>
								<TD width="2%"><IMG src="../tools/imx/ang_sup_izq.bmp"></TD>
								<TD class="titletable1" width="98%" bgColor="#7bb5e7">Idiomas</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="center" width="15%" bgColor="lightgrey"><FONT size="1"><STRONG>Idioma</STRONG></FONT></TD>
								<TD align="center" width="75%" bgColor="lightgrey"><FONT size="1"><STRONG>Descripción</STRONG></FONT></TD>
								<TD align="center" width="15%" bgColor="lightgrey"><FONT size="1"><STRONG>Acciones</STRONG></FONT></TD>
							</TR>
							<TR>
								<TD align="center"><FONT face="Verdana" size="1">Español</FONT></TD>
								<TD>
									<asp:TextBox id="txtClaseDescrip" runat="server" Width="504px" Rows="3" TextMode="MultiLine"></asp:TextBox></TD>
								<TD>
									<P style="MARGIN-TOP: 3px">
										<asp:LinkButton id="lnkGuardar" Runat="server" text="Guardar" onclick="lnkGuardar_Click"></asp:LinkButton></P>
								</TD>
							</TR>
							<TR>
								<TD bgColor="lightgrey"><FONT size="1"></FONT></TD>
								<TD bgColor="lightgrey"></TD>
								<TD bgColor="lightgrey"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Button id="btnAtras" Runat="server" Text="<Atrás" Font-Bold="True" onclick="btnAtras_Click"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
