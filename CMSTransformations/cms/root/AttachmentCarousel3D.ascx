﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.Controls.CMSAbstractTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.Controls" Assembly="CMS.Controls" %><%@ Register TagPrefix="cc1" Namespace="CMS.Controls" Assembly="CMS.Controls" %><%@ Register TagPrefix="cc1" Namespace="CMS.GlobalHelper" Assembly="CMS.GlobalHelper" %>
<%# IfCompare(ImageHelper.IsImage((string)Eval("AttachmentExtension")), true,
"<div style=\"text-align:center;width: 350px;\"><div style=\"font-size: 11px;line-height: 12px;position:relative;z-index:1000;margin:auto;width:140px;\"><a target=\"_blank\" href=\"" + GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID")) + "\"><img style=\"border: none;\" src=\"" + GetAttachmentIconUrl(Eval("AttachmentExtension"), null) + "\" alt=\"" + Eval("AttachmentName") + "\" /></a><p>" + ResHelper.GetString("attach.openfile") + "</p></div></div>",
"<img src=\"" + GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID")) + "?maxsidesize=150\" class=\"cloudcarousel3d\" alt=\"" + Eval("AttachmentTitle", true) + "\" title=\"" + Eval("AttachmentDescription", true) + "\" />") %>