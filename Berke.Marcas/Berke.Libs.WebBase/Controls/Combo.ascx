<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Combo.ascx.cs" Inherits="Berke.Libs.WebBase.Controls.Combo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="Custom" Namespace="Berke.Libs.WebBase.Controls" Assembly = "Berke.Libs.WebBase" %>
<LINK href="~/tools/css/globalstyle.css" type="text/css"
	rel="stylesheet">
<asp:TextBox id="txtSearchPattern" runat="server" Columns="3" MaxLength="4" AutoPostBack="True"
	CssClass="TextBox"></asp:TextBox>
<Custom:DropDown id="ddFoundEntries" runat="server" AutoPostBack="True"></Custom:DropDown>
<asp:RequiredFieldValidator id="vldReqFoundEntries" ErrorMessage="*" ControlToValidate="ddFoundEntries" Display="Dynamic"
	runat="server"></asp:RequiredFieldValidator>
<asp:Literal id="lSearcher" runat="server" Visible="False"></asp:Literal>
