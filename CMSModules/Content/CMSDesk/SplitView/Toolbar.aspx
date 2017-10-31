<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Toolbar.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master"
    Inherits="CMSModules_Content_CMSDesk_SplitView_Toolbar" Theme="Default" Title="CMSDesk - Split view toolbar" %>

<%@ Register Src="~/CMSModules/Content/Controls/SplitView/Documents/DocumentToolbar.ascx"
    TagName="DocumentToolbar" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
    <cms:DocumentToolbar ID="documentToolbar" runat="server" />
</asp:Content>
