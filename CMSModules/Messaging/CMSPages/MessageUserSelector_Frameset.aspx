<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Messaging_CMSPages_MessageUserSelector_Frameset"
    EnableEventValidation="false" CodeFile="MessageUserSelector_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Message user selector</title>

    <script type="text/javascript">
        //<![CDATA[
        function CloseAndRefresh(userId, mText, mId, mId2) {
            wopener.FillUserName(userId, mText, mId, mId2);
            window.close();
        }

        function Refresh() {
            wopener.document.location.replace(wopener.document.location);
        }

        //]]>
    </script>

</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="MessageUserSelectorHeader" src="MessageUserSelector_Header.aspx?showtab=<%=QueryHelper.GetText("showtab", "")%>&hidid=<%=QueryHelper.GetText("hidid", "")%>&mid=<%=QueryHelper.GetText("mid", "")%>&refresh=<%=QueryHelper.GetText("refresh", "")%>"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="MessageUserSelectorContent" src="MessageUserSelector_<%=QueryHelper.GetText("showtab", "")%>.aspx?hidid=<%=QueryHelper.GetText("hidid", "")%>&mid=<%=QueryHelper.GetText("mid", "")%>&refresh=<%=QueryHelper.GetText("refresh", "")%>"
        scrolling="auto" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
