﻿<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalControls.CMSAbstractLayout" %> 
<%@ Register Assembly="CMS.PortalControls" Namespace="CMS.PortalControls" TagPrefix="cc1" %> 
<%@ Register Assembly="CMS.Controls" Namespace="CMS.Controls" TagPrefix="cc1" %> 
<cc1:CMSWebPartZone ID="zoneTop" runat="server" />      
<div style="width: 100%">
<table border="0" width="100%">
  <tr valign="top">
    <td width="80%">
      <cc1:CMSWebPartZone ID="zoneLeft" runat="server" />      
    </td>
    <td width="20%">
      <cc1:CMSWebPartZone ID="zoneRight" runat="server" />      
    </td>
  </tr>
</table>
</div>
<cc1:CMSWebPartZone ID="zoneBottom" runat="server" />      