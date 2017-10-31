<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Transformation_Theme"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Title="Web part container - Theme"
    CodeFile="DocumentType_Edit_Transformation_Theme.aspx.cs" EnableEventValidation="false" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/CSS/ThemeEditor.ascx" TagName="ThemeEditor"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ThemeEditor ID="themeElem" runat="server" />
</asp:Content>
