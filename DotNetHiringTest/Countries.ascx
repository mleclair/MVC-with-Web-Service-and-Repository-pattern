<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Countries.ascx.cs" Inherits="DotNetHiringTest.Countries" %>

<asp:Panel runat="server">
	<asp:Label
		runat="server"
		ID="CountryDropDownLabel"
		Visible="false">Choose a Country:</asp:Label>
	<asp:DropDownList
		runat="server"
		ID="CountryDropDownList"
		AutoPostBack="true"
		Visible="false"
		OnSelectedIndexChanged="CountrySelectionChange"></asp:DropDownList>
	<span style="width:30px">&nbsp;</span>
	<asp:Label
		runat="server"
		Visible="false"
		ID="DisplayLanguageLabel">Select a display Language:</asp:Label>
	<asp:DropDownList
		runat="server"
		ID="DisplayLanguageDropDownList"
		AutoPostBack="true"
		Visible="false"
		OnSelectedIndexChanged="DisplayLanguageSelectionChange" />
</asp:Panel>
<asp:Panel runat="server">
	<asp:Table runat="server" ID="SelectedCountryTable" ClientIDMode="AutoID">
	</asp:Table>
</asp:Panel>
