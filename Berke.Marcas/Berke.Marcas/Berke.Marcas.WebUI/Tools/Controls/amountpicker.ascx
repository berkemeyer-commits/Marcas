<%@ Control Language="c#" AutoEventWireup="false" Inherits="Berke.Libs.WebBase.Controls.AmountPicker" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<LINK href="../../tools/css/globalstyle.css" type="text/css" rel="stylesheet">
<asp:label id="lblAmount" runat="server"></asp:label><BR>
<asp:textbox id="txtAmount" runat="server" Columns="16"></asp:textbox>
<asp:RequiredFieldValidator id="vldReqAmount" runat="server" ControlToValidate="txtAmount" Text="*"></asp:RequiredFieldValidator>
<asp:CompareValidator id="vldCmpAmount" runat="server" ControlToValidate="txtAmount" Text="*" ValueToCompare="0"
	Operator="GreaterThan" Type="Currency" Display="Dynamic"></asp:CompareValidator>
