﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.Controls.CMSAbstractTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.Controls" Assembly="CMS.Controls" %><%@ Register TagPrefix="cc1" Namespace="CMS.Controls" Assembly="CMS.Controls" %><table>
<tr>
<td><strong>Item text:</strong></td>
<td><%# Eval("ItemText") %> <a href='?itemid=<%# Eval("ItemID") %>'>detail</a></td>
</tr>
</table>
