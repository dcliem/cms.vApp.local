﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.Controls.CMSAbstractTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.Controls" Assembly="CMS.Controls" %><%@ Register TagPrefix="cc1" Namespace="CMS.Controls" Assembly="CMS.Controls" %><%@ Register Src="~/CMSModules/MessageBoards/Controls/MessageActions.ascx" TagName="MessageActions" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AbuseReport/Controls/InlineAbuseReport.ascx" TagName="AbuseReport" TagPrefix="cms" %>
<div class="CommentDetail">
    <asp:Panel ID="pnlRating" runat="server" CssClass="CommentRating" />
    <table width="100%">
        <tr>
            <td class="CommentUserName" style="width: 100%">
                <%# IfEmpty(Eval("MessageURL"), TrimSitePrefix(Eval("MessageUserName", true)), "<a href=\"" + Eval("MessageURL", true) + "\" target=\"_blank\">" + TrimSitePrefix(Eval("MessageUserName", true)) + "</a>") %>
	    </td>
        </tr>
        <tr>
            <td class="CommentText">
                <%# CMS.GlobalHelper.TextHelper.EnsureLineEndings(Convert.ToString(Eval("MessageText", true)), "<br />")%>
	    </td>
        </tr>
        <tr>
            <td class="CommentDate">
		<%# Eval("MessageInserted") %>
            </td>
        </tr>
        <tr>
            <td align="right" class="CommentAction">
                <div class="buttonpedding">
			<cms:MessageActions ID="messageActions" runat="server" />
<cms:AbuseReport ID="ucAbuse" runat="server" ReportObjectType="board.board" ReportObjectID='<%# Eval("MessageID") %>' ReportTitle='<%# "Message board report abuse: " + Eval("MessageText") %>' />
                </div>
            </td>
        </tr>
    </table>
</div>
<hr style="border: 1px solid #CCCCCC;"/>