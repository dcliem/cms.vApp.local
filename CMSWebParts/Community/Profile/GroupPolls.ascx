<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSWebParts_Community_Profile_GroupPolls" CodeFile="~/CMSWebParts/Community/Profile/GroupPolls.ascx.cs" %>
<%@ Register Src="~/CMSModules/Polls/Controls/Polls.ascx" TagName="Polls" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/System/PermissionMessage.ascx" TagName="PermissionMessage"
    TagPrefix="cms" %>
<cms:PermissionMessage ID="messageElem" runat="server" Visible="false" EnableViewState="false" />
<cms:Polls ID="pollsElem" runat="server" DelayedReload="false" />
