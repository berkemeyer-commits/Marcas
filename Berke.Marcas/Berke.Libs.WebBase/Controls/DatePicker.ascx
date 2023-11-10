<%@ Control Language="c#" AutoEventWireup="false" Codebehind="DatePicker.ascx.cs" Inherits="Berke.Libs.WebBase.Controls.DatePicker" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<link href="/Berke.RedIntercable.WebUI/tools/css/globalstyle.css" type="text/css" rel="stylesheet">
<table id="tblDate" cellSpacing="1" cellPadding="0" border="0">
	<tr>
		<td>
			<asp:label id="lblDate" runat="server"></asp:label></td>
		<td></td>
	</tr>
	<tr>
		<td>
			<asp:textbox id="txtDate" runat="server" MaxLength="11" Columns="12" AutoPostBack="True"></asp:textbox></td>
		<td><img id="_imgButton" runat="server" onmousedown="mcalChooseDate.style.display=''" alt=""
				src="~/Tools/imx/CalendarIcon.GIF">
			<asp:requiredfieldvalidator id="vldReqDate" runat="server" ControlToValidate="txtDate" Text="*">*</asp:requiredfieldvalidator>
			<asp:comparevalidator id="vldCmpDate" runat="server" ControlToValidate="txtDate" Text="*" Type="Date"
				Display="Dynamic" Operator="GreaterThanEqual" ValueToCompare="1900/01/01">*</asp:comparevalidator></td>
	</tr>
</table>
<div id="pnlCalendar" style="Z-INDEX: 2000; POSITION: absolute">
	<asp:calendar id="mcalChooseDate" runat="server" BackColor="White" DayNameFormat="FirstTwoLetters"
		ForeColor="Black" Font-Size="8pt" Font-Names="Verdana" BorderColor="#999999" CellPadding="4"
		TitleFormat="Month" FirstDayOfWeek="Sunday">
		<TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
		<SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
		<NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
		<DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle>
		<SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
		<TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999"></TitleStyle>
		<WeekendDayStyle BackColor="#FFFFCC"></WeekendDayStyle>
		<OtherMonthDayStyle ForeColor="Gray"></OtherMonthDayStyle>
	</asp:calendar>
</div>
